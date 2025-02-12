using System;
using System.Collections;
using System.Linq;
using PX.Data;
using PX.SM;

namespace PX.Objects.CR
{
	public class PurgeContactsProcess : PXGraph<PurgeContactsProcess>
	{
		public PXCancel<CRPurgeFilter> Cancel;

		public PXFilter<CRPurgeFilter> Filter;

		[PXFilterable]				
		[PXViewDetailsButton(typeof(CRPurgeFilter),
		typeof(Select<BAccountCRM,
			Where<BAccountCRM.bAccountID, Equal<Current<Contact.bAccountID>>>>),
		ActionName = "Items_BAccount_ViewDetails")]
		public PXFilteredProcessingJoin<Contact, CRPurgeFilter, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>> Items;

		public PXSetup<CRSetup> setup;

		public PXSelect<Address, Where<Address.addressID, Equal<Optional<Contact.defAddressID>>>> Address;
		public PXSelect<EPActivity, Where<EPActivity.refNoteID, Equal<Optional<Contact.noteID>>>> Activities;
		public PXSelect<CRRelation, Where<CRRelation.refNoteID, Equal<Optional<Contact.noteID>>>> Relations;
		public PXSelect<CRMarketingListMember, Where<CRMarketingListMember.contactID, Equal<Optional<Contact.contactID>>>> MarketingListMembers;
		public PXSelect<CRCampaignMembers, Where<CRCampaignMembers.contactID, Equal<Optional<Contact.contactID>>>> CampaignMembers;
		public PXSelect<ContactNotification, Where<ContactNotification.contactID, Equal<Optional<Contact.contactID>>>> ContactNotifications;

		protected virtual IEnumerable items()
		{
			CRPurgeFilter filter = Filter.Current;
			if (filter == null) return new Contact[0];

			if (setup.Current != null)
			{
				bool PurgeAgeOfNotConvertedLeadsWrongValue = setup.Current.PurgeAgeOfNotConvertedLeads == null || setup.Current.PurgeAgeOfNotConvertedLeads <= 0;
				bool PurgePeriodWithoutActivityWrongValue = setup.Current.PurgePeriodWithoutActivity == null || setup.Current.PurgePeriodWithoutActivity <= 0;

				if (PurgeAgeOfNotConvertedLeadsWrongValue || PurgePeriodWithoutActivityWrongValue)
				{
					string message = String.Empty;

					if (PurgeAgeOfNotConvertedLeadsWrongValue)
						message += "'" + PXUIFieldAttribute.GetDisplayName<CRSetup.purgeAgeOfNotConvertedLeads>(setup.Cache) + "' ";
					if (PurgePeriodWithoutActivityWrongValue)
						message += "'" + PXUIFieldAttribute.GetDisplayName<CRSetup.purgePeriodWithoutActivity>(setup.Cache) + "' ";

					throw new PXSetupNotEnteredException(Messages.CRSetupFieldsAreEmpty, typeof(CRSetup), typeof(CRSetup).Name, message);
				}
			}

			return PXSelectJoin<Contact,
 				LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>,
				Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Select(this)
				.Where(res =>
					{
						Contact contact = res;
						return ( //Purge Not Converted Leads that are older than
							       filter.PurgeOldNotConvertedLeads == true && filter.PurgeAgeOfNotConvertedLeads != null &&
							       contact.CreatedDateTime != null
							       && contact.ContactType == ContactTypesAttribute.Lead
							       && contact.Status != LeadStatusesAttribute.Closed
							       && ((DateTime) contact.CreatedDateTime).AddMonths((int) filter.PurgeAgeOfNotConvertedLeads) < Accessinfo.BusinessDate
						       )
						       ||
						       ( //Purge Not Active Contacts with no activities for more than
							       filter.PurgeOldInertContacts == true && filter.PurgePeriodWithoutActivity != null &&
							       contact.LastActivity != null
							       && contact.ContactType == ContactTypesAttribute.Person
							       &&
							       ((DateTime) contact.LastActivity).AddMonths((int) filter.PurgePeriodWithoutActivity) <
							       Accessinfo.BusinessDate
							       && contact.IsActive != true
						       )
						       ||
						       //Purge Closed Contacts 
							       (filter.PurgeClosedContacts == true && (contact.Status == LeadStatusesAttribute.Closed
								   || (contact.IsActive != true && contact.DuplicateStatus == DuplicateStatusAttribute.Duplicated)));
					});
		}

		protected virtual void CRPurgeFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			CRPurgeFilter filter = (CRPurgeFilter)e.Row;
			if(filter == null) return;

			PXUIFieldAttribute.SetEnabled<CRPurgeFilter.purgeAgeOfNotConvertedLeads>(sender, filter, filter.PurgeOldNotConvertedLeads == true);
			PXUIFieldAttribute.SetEnabled<CRPurgeFilter.purgePeriodWithoutActivity>(sender, filter, filter.PurgeOldInertContacts == true);

			Items.SetProcessDelegate(delegate(PurgeContactsProcess graph, Contact contact)
				{
					BAccount bacct = PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Current<Contact.bAccountID>>>>.SelectSingleBound(graph, new object[] { contact });
					if(bacct == null || bacct.DefAddressID != contact.DefAddressID)
					{
						graph.Address.Delete(new Address {AddressID = contact.DefAddressID});
					}
		
					foreach (EPActivity activity in graph.Activities.Select(new object[]{contact.NoteID}))
					{
						if (activity.IsBillable == true || !string.IsNullOrEmpty(activity.TimeCardCD) ||
						    activity.MPStatus == MailStatusListAttribute.InProcess)
						{
							throw new PXException(Messages.CannotDeleteActivity);
						}
						graph.Activities.Delete(activity);
					}

					// Relations, Marketing List members, Campaign members and Notifications delete by PXParentAttribute

					graph.Items.Delete(contact);
					graph.Actions.PressSave();
				}
			);
		}

		protected virtual void CRPurgeFilter_PurgeAgeOfNotConvertedLeads_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null) return;

			int val =  (int)e.NewValue;
			CRSetup crSetup = setup.Current;

			if (val< 0)
				throw new PXSetPropertyException(Messages.PurgeLimitCannotBeNegative);
			if (crSetup.PurgeAgeOfNotConvertedLeads != null && val < crSetup.PurgeAgeOfNotConvertedLeads)
				throw new PXSetPropertyException(Messages.PurgeLimitCannotBeLessThanInCRSetup);
				
		}

		protected virtual void CRPurgeFilter_PurgePeriodWithoutActivity_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null) return;

			int val = (int)e.NewValue;
			CRSetup crSetup = setup.Current;

			if (val < 0)
				throw new PXSetPropertyException(Messages.PurgeLimitCannotBeNegative);
			if (crSetup.PurgePeriodWithoutActivity != null && val < crSetup.PurgePeriodWithoutActivity)
				throw new PXSetPropertyException(Messages.PurgeLimitCannotBeLessThanInCRSetup);

		}

		public static void ProcessItem(PurgeContactsProcess graph, Contact contact)
		{
			
		}
	}

	[Serializable]
	public class CRPurgeFilter :  IBqlTable
	{
		#region PurgeOldNotConvertedLeads
		public abstract class purgeOldNotConvertedLeads : IBqlField { }
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Purge Unconverted Leads that Are Older than")]
		public virtual bool? PurgeOldNotConvertedLeads { get; set; }
		#endregion
		#region PurgeOldInertContacts
		public abstract class purgeOldInertContacts : IBqlField { }
		[PXDBBool]
		[PXUIField(DisplayName = "Purge Inactive Contacts with No Activities for More than")]
		public virtual bool? PurgeOldInertContacts { get; set; }
		#endregion
		#region PurgeClosedContacts
		public abstract class purgeClosedContacts : IBqlField { }
		[PXDBBool]
		[PXUIField(DisplayName = "Purge Closed Contacts and Leads")]
		public virtual bool? PurgeClosedContacts { get; set; }
		#endregion
		#region PurgeAgeOfNotConvertedLeads
		public abstract class purgeAgeOfNotConvertedLeads : IBqlField { }
		[PXDBInt(MinValue = 0)]
		[PXUIField]
		[PXDefault(typeof(CRSetup.purgeAgeOfNotConvertedLeads))]
		public virtual int? PurgeAgeOfNotConvertedLeads { get; set; }
		#endregion
		#region PurgePeriodWithoutActivity
		public abstract class purgePeriodWithoutActivity : IBqlField { }
		[PXDBInt(MinValue = 0)]
		[PXUIField]
		[PXDefault(typeof(CRSetup.purgePeriodWithoutActivity))]
		public virtual int? PurgePeriodWithoutActivity { get; set; }
		#endregion
	}
}
