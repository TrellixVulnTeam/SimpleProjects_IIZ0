using System;
using PX.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.GL;
using PX.Objects.CS;
using CRLocation = PX.Objects.CR.Standalone.Location;
using Branch = PX.Objects.GL.Branch;
using PX.SM;

namespace PX.Objects.CR
{
	[PXSubstitute(GraphType = typeof(LocationMaint))]
    [Serializable]
	public partial class SelectedLocation : Location
	{
		#region BAccountID
		public new abstract class bAccountID : PX.Data.IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault(typeof(SelectedLocation.bAccountID))]
		[PXUIField(DisplayName = "Customer", TabOrder = 0)]
		[PXDimensionSelector("BIZACCT", typeof(Search2<BAccount.bAccountID,
			LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactID, Equal<BAccount.defContactID>>>,
			LeftJoin<Address, On<Address.bAccountID, Equal<BAccount.bAccountID>, And<Address.addressID, Equal<BAccount.defAddressID>>>>>,
			Where<BAccount.type, Equal<BAccountType.customerType>,
				Or<BAccount.type, Equal<BAccountType.prospectType>,
				Or<BAccount.type, Equal<BAccountType.combinedType>>>>>), typeof(BAccount.acctCD),
			typeof(BAccount.acctCD),
			typeof(BAccount.acctName), typeof(BAccount.classID), typeof(BAccount.status), typeof(Contact.phone1), typeof(Address.city), typeof(Address.countryID),
			DescriptionField = typeof(BAccount.acctName))]
		[PXParent(typeof(Select<BAccount,
			Where<BAccount.bAccountID,
			Equal<Current<Location.bAccountID>>>>)
			)]
		public override Int32? BAccountID
		{
			get
			{
				return base._BAccountID;
			}
			set
			{
				base._BAccountID = value;
			}
		}
		#endregion
		#region IsAddressSameAsMain
		public new abstract class isAddressSameAsMain : PX.Data.IBqlField
		{
		}
		[PXBool()]
		[PXUIField(DisplayName = "Same As Main")]
		public override bool? IsAddressSameAsMain
		{
			get
			{
				return this._IsAddressSameAsMain;
			}
			set
			{
				this._IsAddressSameAsMain = value;
			}
		}
		#endregion
		#region IsContactSameAsMain
		public new abstract class isContactSameAsMain : PX.Data.IBqlField
		{
		}
		[PXBool()]
		[PXUIField(DisplayName = "Same As Main")]
		public override bool? IsContactSameAsMain
		{
			get
			{
				return this._IsContactSameAsMain;
			}
			set
			{
				this._IsContactSameAsMain = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select<CRLocation>), Persistent = false)]
    [PXCacheName(Messages.LocationARAccountSub)]
    [Serializable]
	public partial class LocationARAccountSub : IBqlTable
	{
		#region BAccountID
		public abstract class bAccountID : PX.Data.IBqlField
		{
		}
		protected int? _BAccountID;
		[PXDBInt(BqlField = typeof(CRLocation.bAccountID), IsKey = true)]
		public virtual int? BAccountID
		{
			get
			{
				return this._BAccountID;
			}
			set
			{
				this._BAccountID = value;
			}
		}
		#endregion
		#region LocationID
		public abstract class locationID : PX.Data.IBqlField
		{
		}
		protected int? _LocationID;
		[PXDBInt(BqlField = typeof(CRLocation.locationID), IsKey = true)]
		public virtual int? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
		#region CARAccountLocationID
		public abstract class cARAccountLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _CARAccountLocationID;
		[PXDBInt(BqlField = typeof(CRLocation.cARAccountLocationID))]
		public virtual Int32? CARAccountLocationID
		{
			get
			{
				return this._CARAccountLocationID;
			}
			set
			{
				this._CARAccountLocationID = value;
			}
		}
		#endregion
		#region CARAccountID
		public abstract class cARAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _CARAccountID;
		[Account(BqlField = typeof(CRLocation.cARAccountID), DisplayName = "AR Account", DescriptionField = typeof(Account.description))]
		public virtual Int32? CARAccountID
		{
			get
			{
				return this._CARAccountID;
			}
			set
			{
				this._CARAccountID = value;
			}
		}
		#endregion
		#region CARSubID
		public abstract class cARSubID : PX.Data.IBqlField
		{
		}
		protected Int32? _CARSubID;
		[SubAccount(typeof(LocationARAccountSub.cARAccountID), BqlField = typeof(CRLocation.cARSubID), DisplayName = "AR Sub.", DescriptionField = typeof(Sub.description))]
		public virtual Int32? CARSubID
		{
			get
			{
				return this._CARSubID;
			}
			set
			{
				this._CARSubID = value;
			}
		}
		#endregion
	}

	public class LocationMaint : LocationMaintBase<Location, Location, Where<Location.bAccountID, Equal<Optional<Location.bAccountID>>>>
	{
		#region Buttons

		public PXSave<Location> Save;
		public PXAction<Location> cancel;
		public PXInsert<Location> Insert;
		public PXDelete<Location> Delete;
		public PXFirst<Location> First;
		public PXAction<Location> previous;
		public PXAction<Location> next;
		public PXLast<Location> Last;
		public PXAction<Location> viewOnMap;
		public PXAction<Location> validateAddresses;		
		#endregion

		#region ButtonDelegates

		[PXUIField(DisplayName = ActionsMessages.Cancel, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXCancelButton]
		protected virtual IEnumerable Cancel(PXAdapter adapter)
		{
			int? acctid = Location.Current != null ? Location.Current.BAccountID : null;
			foreach (Location loc in (new PXCancel<Location>(this, "Cancel")).Press(adapter))
			{
				if (!IsImport && Location.Cache.GetStatus(loc) == PXEntryStatus.Inserted
						&& (acctid != loc.BAccountID || string.IsNullOrEmpty(loc.LocationCD)))
				{
					foreach (Location first in First.Press(adapter))
					{
						return new object[] { first };
					}
					loc.LocationCD = null;
					return new object[] { loc };
				}
				else
				{
					return new object[] { loc };
				}
			}
			return new object[0];
		}

		[PXUIField(DisplayName = ActionsMessages.Previous, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXPreviousButton]
		protected virtual IEnumerable Previous(PXAdapter adapter)
		{
			foreach (Location loc in (new PXPrevious<Location>(this, "Prev")).Press(adapter))
			{
				if (Location.Cache.GetStatus(loc) == PXEntryStatus.Inserted)
				{
					return Last.Press(adapter);
				}
				else
				{
					return new object[] { loc };
				}
			}
			return new object[0];
		}

		[PXUIField(DisplayName = ActionsMessages.Next, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXNextButton]
		protected virtual IEnumerable Next(PXAdapter adapter)
		{
			foreach (Location loc in (new PXNext<Location>(this, "Next")).Press(adapter))
			{
				if (Location.Cache.GetStatus(loc) == PXEntryStatus.Inserted)
				{
					return First.Press(adapter);
				}
				else
				{
					return new object[] { loc };
				}
			}
			return new object[0];
		}

		[PXUIField(DisplayName = Messages.ViewOnMap, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton()]
		public virtual IEnumerable ViewOnMap(PXAdapter adapter)
		{

			BAccountUtility.ViewOnMap(this.Address.Current);
			return adapter.Get();
		}

		[PXUIField(DisplayName = CS.Messages.ValidateAddresses, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, FieldClass = CS.Messages.ValidateAddress)]
		[PXButton]
		public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
		{
			Location primary = this.LocationCurrent.Current;
			if (primary != null)
			{
				bool needSave = false;
				Save.Press();
				BAccount acct = BAccountUtility.FindAccount(this, primary.BAccountID);
				bool isSameAsMain = (acct != null && acct.DefAddressID == primary.DefAddressID);
				Address address = this.Address.Current;
				if (address != null && isSameAsMain == false && address.IsValidated == false)
				{
					if (PXAddressValidator.Validate<Address>(this, address, true))
						needSave = true;
				}
				if (needSave)
					this.Save.Press();
			}
			return adapter.Get();
		}

		#endregion

		[PXFilterable]
		[PXViewDetailsButton(typeof(Location))]
		[PXViewDetailsButton(typeof(Location),
			typeof(Select<Contact,
				Where<Contact.contactID, Equal<Current<CROpportunity.contactID>>>>))]
		public PXSelectJoin<CROpportunity, 
			LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>, 
			LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>>>,
			Where<CROpportunity.bAccountID, Equal<Current<Location.bAccountID>>,
				And<CROpportunity.locationID, Equal<Current<Location.locationID>>>>> 
			Opportunities;

		[PXFilterable]
		[PXViewDetailsButton(typeof(Location))]
		[PXViewDetailsButton(typeof(Location),
			typeof(Select<Contact,
				Where<Contact.contactID, Equal<Current<CRCase.contactID>>>>))]
		public PXSelect<CRCase, 
			Where<CRCase.customerID, Equal<Current<Location.bAccountID>>,
				And<CRCase.locationID, Equal<Current<Location.locationID>>>>> 
			Cases;

		public PXSelect<LocationARAccountSub, Where<LocationARAccountSub.bAccountID, Equal<Current<Location.bAccountID>>, And<LocationARAccountSub.locationID, Equal<Current<Location.cARAccountLocationID>>>>> ARAccountSubLocation;

		public LocationMaint()
		{
			if (Company.Current.BAccountID.HasValue == false)
			{
                throw new PXSetupNotEnteredException(ErrorMessages.SetupNotEntered, typeof(Branch), CS.Messages.BranchMaint);
			}
			PXUIFieldAttribute.SetDisplayName(Location.Cache, "LocationCD", Messages.LocationID);
			PXUIFieldAttribute.SetDisplayName(LocationCurrent.Cache, "CLeadTime", Messages.LeadTimeDays);

			Views.Caches.Remove(typeof(LocationARAccountSub));

			var bAccountCache = Caches[typeof(BAccount)];
			PXUIFieldAttribute.SetDisplayName<BAccount.acctCD>(bAccountCache, Messages.BAccountCD);
			PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(bAccountCache, Messages.BAccountName);
		}

		public override void Persist()
		{
			base.Persist();
			this.ARAccountSubLocation.Cache.Clear();
		}

		public PXSetup<GL.Branch> Company;

		#region Location Events

		/*[CustomerProspectVendor(DisplayName = "Buisness Account", IsKey = true)]
		protected virtual void Location_BAccountID_CacheAttached(PXCache sender, PXRowSelectedEventArgs e)
		{
			
		}*/

		protected override void Location_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			BAccount baccount = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));

			PXUIFieldAttribute.SetEnabled<CR.Location.isARAccountSameAsMain>(sender, e.Row, baccount!=null && !object.Equals(baccount.DefLocationID, ((Location)e.Row).LocationID));

			base.Location_RowSelected(sender, e);
		}

		protected virtual void Location_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			if (e.Row != null)
			{
				Location record = (Location)e.Row;

				record.IsARAccountSameAsMain = !object.Equals(record.LocationID, record.CARAccountLocationID);
			}
		}

		protected override void Location_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			Location record = (Location)e.Row;

			record.IsARAccountSameAsMain = !object.Equals(record.LocationID, record.CARAccountLocationID);

			base.Location_RowInserted(sender, e);
		}

		protected virtual void Location_IsARAccountSameAsMain_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CR.Location record = (CR.Location)e.Row;

			if (record.IsARAccountSameAsMain == false)
			{
				LocationARAccountSub mainloc = ARAccountSubLocation.Select();
				record.CARAccountID = mainloc.CARAccountID;
				record.CARSubID = mainloc.CARSubID;
				record.CARAccountLocationID = record.LocationID;

				LocationARAccountSub copyloc = new LocationARAccountSub();
				copyloc.BAccountID = record.BAccountID;
				copyloc.LocationID = record.LocationID;
				copyloc.CARAccountID = record.CARAccountID;
				copyloc.CARSubID = record.CARSubID;

				BusinessAccount.Cache.Current = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				ARAccountSubLocation.Insert(copyloc);
			}
			if (record.IsARAccountSameAsMain == true)
			{
				record.CARAccountID = null;
				record.CARSubID = null;
				BAccount baccount = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				if (baccount != null)
				{
					record.CARAccountLocationID = baccount.DefLocationID;
				}
			}
		}

		protected virtual void Location_VBranchID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = null;
			e.Cancel = true;
		}
		#endregion

		#region LocationARAccountSub Events

		protected virtual void LocationARAccountSub_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (Location.Current != null)
			{
				PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals(Location.Current.LocationID, Location.Current.CARAccountLocationID));
			}
		}

		protected virtual void LocationARAccountSub_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			LocationARAccountSub record = (LocationARAccountSub)e.Row;

			if (!sender.ObjectsEqual<LocationARAccountSub.cARAccountID, LocationARAccountSub.cARSubID>(e.Row, e.OldRow))
			{
				Location mainloc = Location.Current;
				mainloc.CARAccountID = record.CARAccountID;
				mainloc.CARSubID = record.CARSubID;

				if (Location.Cache.GetStatus(mainloc) == PXEntryStatus.Notchanged)
				{
					Location.Cache.SetStatus(mainloc, PXEntryStatus.Updated);
				}
			}
		}
		#endregion

	}

	public class LocationMaintBase<Base, Primary, Where> : PXGraph<LocationMaintBase<Base, Primary, Where>>
		where Base : Location, new()
		where Primary : class, IBqlTable, new()
		where Where : class, IBqlWhere, new()
	{

		#region Public Selects

		public PXSelect<BAccount> BusinessAccount;

		public PXSelect<Base, Where> Location;


		public PXSelect<Location, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>>>> LocationCurrent;
		public PXSelect<Address, Where<Address.bAccountID, Equal<Current<Location.bAccountID>>,
										And<Address.addressID, Equal<Current<Location.defAddressID>>>>> Address;
		public PXSelect<Contact, Where<Contact.bAccountID, Equal<Current<Location.bAccountID>>,
										And<Contact.contactID, Equal<Current<Location.defContactID>>>>> Contact;

		public LocationMaintBase()
		{
            PXUIFieldAttribute.SetDisplayName<Contact.salutation>(Contact.Cache, CR.Messages.Attention);
		}

		#endregion

		#region Main Record Events

		protected virtual void Location_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			SelectedLocation row = e.Row as SelectedLocation;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					if (!row.DefAddressID.HasValue)
					{
						row.DefAddressID = acct.DefAddressID; //Set default value
					}
					row.IsAddressSameAsMain = (row.DefAddressID == acct.DefAddressID);
					if (!row.DefContactID.HasValue)
					{
						row.DefContactID = acct.DefContactID;
					}
					row.IsContactSameAsMain = (row.DefContactID == acct.DefContactID);
				}

				bool VendorDetailsVisible = (row.LocType == LocTypeList.VendorLoc || row.LocType == LocTypeList.CombinedLoc);
				bool CustomerDetailsVisible = (row.LocType == LocTypeList.CustomerLoc || row.LocType == LocTypeList.CombinedLoc);
				bool CompanyDetailsVisible = (row.LocType == LocTypeList.CompanyLoc);
				PXUIFieldAttribute.SetEnabled<Location.vTaxZoneID>(cache, null, VendorDetailsVisible || CompanyDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vExpenseAcctID>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vExpenseSubID>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vLeadTime>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vBranchID>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vCarrierID>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vFOBPointID>(cache, null, VendorDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.vShipTermsID>(cache, null, VendorDetailsVisible);

				PXUIFieldAttribute.SetEnabled<Location.cTaxZoneID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cSalesAcctID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cSalesSubID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cSalesAcctID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cDiscountAcctID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cDiscountSubID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cFreightAcctID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cFreightSubID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cBranchID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cCarrierID>(cache, null, CustomerDetailsVisible);
				
				PXUIFieldAttribute.SetEnabled<Location.cFOBPointID>(cache, null, CustomerDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cShipTermsID>(cache, null, CustomerDetailsVisible);

				PXUIFieldAttribute.SetEnabled<Location.cMPSalesSubID>(cache, null, CompanyDetailsVisible);
				PXUIFieldAttribute.SetEnabled<Location.cMPExpenseSubID>(cache, null, CompanyDetailsVisible);

				bool isGroundCollectVisible = false;

				if ( row.CCarrierID != null)
				{
					Carrier carrier = PXSelect<Carrier, Where<Carrier.carrierID, Equal<Required<Carrier.carrierID>>>>.Select(this, row.CCarrierID);

					if (carrier != null && carrier.IsExternal == true && !string.IsNullOrEmpty(carrier.CarrierPluginID))
					{
						ICarrierService service = CarrierPluginMaint.CreateCarrierService(this, carrier.CarrierPluginID);
						if (service != null)
							isGroundCollectVisible = service.Attributes.Contains("COLLECT");
					}
				}

				PXUIFieldAttribute.SetVisible<Location.cGroundCollect>(cache, row, isGroundCollectVisible);

				EstablishCTaxZoneRule();
				EstablishVTaxZoneRule();
			}
		}

		protected virtual void Location_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
		{
			Location row = (Location)e.Row;
			if (row != null)
			{
				BAccount account = BAccountUtility.FindAccount(this, row.BAccountID);
				if (account != null)
				{
					row.DefAddressID = account.DefAddressID;
					row.DefContactID = account.DefContactID;
				}
				else
				{
					//Inserting Address record
					Address addr = new Address();
					addr = this.Address.Insert(addr);
					row.DefAddressID = addr.AddressID;
					this.Address.Cache.IsDirty = false;
					//Inserting Contact record
					Contact cont = new Contact();
					cont = this.Contact.Insert(cont);
					row.DefContactID = cont.ContactID;
					this.Contact.Cache.IsDirty = false;
				}
				//Updating inserted record status
				this.Location.Cache.IsDirty = false;
			}
		}

		protected virtual void Location_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
		{
			Location row = e.Row as Location;
			BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID); ;
			if (acct != null)
			{
				if (acct.DefLocationID == row.LocationID)
				{
					throw new PXException(Messages.CannotDeleteDefaultLoc);
				}
			}
		}

		protected virtual void Location_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
		{
			Location row = e.Row as Location;
			BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID); ;
			if (acct != null)
			{
				if (row.DefAddressID.HasValue && row.DefAddressID != acct.DefAddressID)
				{
					Address addr = (Address)this.Address.Current;
					if (row.DefAddressID == addr.AddressID)
						this.Address.Delete(addr);
				}
				if (row.DefContactID.HasValue && row.DefContactID != acct.DefContactID)
				{
					Contact cnt = (Contact)this.Contact.Current;
					if (row.DefContactID == cnt.ContactID)
						this.Contact.Delete(cnt);
				}
			}
		}

		protected virtual void Location_LocType_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					switch (acct.Type)
					{
						case BAccountType.VendorType:
							e.NewValue = LocTypeList.VendorLoc;
							break;
						case BAccountType.CustomerType:
						case BAccountType.EmpCombinedType:
							e.NewValue = LocTypeList.CustomerLoc;
							break;
						case BAccountType.CombinedType:
							e.NewValue = LocTypeList.CombinedLoc;
							break;
						default:
							e.NewValue = LocTypeList.CompanyLoc;
							break;
					}
				}
			}
		}

		protected virtual void Location_DefAddressID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefAddressID;
				}
			}
		}

		protected virtual void Location_DefContactID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefContactID;
				}
			}
		}

		protected virtual void Location_CARAccountLocationID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefLocationID;
				}
			}
		}

		protected virtual void Location_VAPAccountLocationID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefLocationID;
				}
			}
		}

		protected virtual void Location_VPaymentInfoLocationID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefLocationID;
				}
			}
		}

		protected virtual void Location_VRemitAddressID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefAddressID;
				}
			}
		}

		protected virtual void Location_VRemitContactID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location row = e.Row as Location;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				if (acct != null)
				{
					e.NewValue = acct.DefContactID;
				}
			}
		}

		protected virtual void Location_VExpenseAcctID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.VendorType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Location defLocation = PXSelect<Location>.Search<Location.locationID>(this, acct.DefLocationID);
					if (defLocation != null && defLocation.VExpenseAcctID != null)
					{
						e.NewValue = defLocation.VExpenseAcctID;
						e.Cancel = true;
					}
				}
			}
		}

		protected virtual void Location_VExpenseSubID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.VendorType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Location defLocation = PXSelect<Location>.Search<Location.locationID>(this, acct.DefLocationID);
					if (defLocation != null && defLocation.VExpenseSubID != null)
					{
						e.NewValue = defLocation.VExpenseSubID;
						e.Cancel = true;
					}
				}
			}
		}

		protected virtual void Location_CSalesAcctID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.CustomerType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Location defLocation = PXSelect<Location>.Search<Location.locationID>(this, acct.DefLocationID);
					if (defLocation != null && defLocation.CSalesAcctID != null)
					{
						e.NewValue = defLocation.CSalesAcctID;
						e.Cancel = true;
					}
				}
			}
		}

		protected virtual void Location_CSalesSubID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.CustomerType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Location defLocation = PXSelect<Location>.Search<Location.locationID>(this, acct.DefLocationID);
					if (defLocation != null && defLocation.CSalesSubID != null)
					{
						e.NewValue = defLocation.CSalesSubID;
						e.Cancel = true;
					}
				}
			}
		}
		protected virtual void Location_CPriceClassID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.CustomerType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Location defLocation = PXSelect<Location>.Search<Location.locationID>(this, acct.DefLocationID);
					if (defLocation != null && defLocation.CPriceClassID != null)
					{
						e.NewValue = defLocation.CPriceClassID;
						e.Cancel = true;
					}
				}
			}
		}
		protected virtual void Location_VTaxZoneID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.VendorType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Vendor vendor = PXSelect<Vendor>.Search<Vendor.bAccountID>(this, acct.BAccountID);
					VendorClass vClass = PXSelect<VendorClass>.Search<VendorClass.vendorClassID>(this, vendor.VendorClassID);
					if (vClass != null && vClass.TaxZoneID != null)
					{
						e.NewValue = vClass.TaxZoneID;
						e.Cancel = true;
					}
				}
			}
		}
		protected virtual void Location_CTaxZoneID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc != null && loc.BAccountID != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
				if (acct != null &&
						 acct.DefLocationID != null &&
						 (acct.Type == BAccountType.CustomerType || acct.Type == BAccountType.CombinedType) &&
						 loc.LocationID != acct.DefLocationID)
				{
					Customer customer = PXSelect<Customer>.Search<Customer.bAccountID>(this, acct.BAccountID);
					CustomerClass cClass = PXSelect<CustomerClass>.Search<CustomerClass.customerClassID>(this, customer.CustomerClassID);
					if (cClass != null && cClass.TaxZoneID != null)
					{
						e.NewValue = cClass.TaxZoneID;
						e.Cancel = true;
					}
				}
			}
		}

		protected virtual void Location_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			Location loc = e.Row as Location;
			if (loc == null || loc.BAccountID == null) return;
			
			BAccount acct = BAccountUtility.FindAccount(this, loc.BAccountID);
			if (acct != null)
			{
				if (loc.LocationID == acct.DefLocationID && loc.IsActive != true)
				{
					cache.RaiseExceptionHandling<Location.isActive>(loc, null, new PXSetPropertyException(Messages.DefaultLocationCanNotBeNotActive, typeof(Location.isActive).Name));
				}
				if (acct.Type == BAccountType.CustomerType || acct.Type == BAccountType.CombinedType)
				{
					if (loc.CSalesAcctID == null)
					{
						cache.RaiseExceptionHandling<Location.cSalesAcctID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.cSalesAcctID).Name));
					}
					if (loc.CSalesSubID == null)
					{
						cache.RaiseExceptionHandling<Location.cSalesSubID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.cSalesSubID).Name));
					}
					Customer customer = PXSelect<Customer>.Search<Customer.bAccountID>(this, acct.BAccountID);
					CustomerClass cClass = PXSelect<CustomerClass>.Search<CustomerClass.customerClassID>(this, customer.CustomerClassID);
					if (cClass != null && cClass.TaxZoneID != null && loc.CTaxZoneID == null)
					{
						cache.RaiseExceptionHandling<Location.cTaxZoneID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.cTaxZoneID).Name));
					}
				}
				if (acct.Type == BAccountType.VendorType || acct.Type == BAccountType.CombinedType)
				{
					Vendor vendor = PXSelect<Vendor>.Search<Vendor.bAccountID>(this, acct.BAccountID);
					VendorClass vClass = PXSelect<VendorClass>.Search<VendorClass.vendorClassID>(this, vendor.VendorClassID);
					if (vendor != null && vendor.TaxAgency == true && loc.VExpenseAcctID == null)
					{
						cache.RaiseExceptionHandling<Location.vExpenseAcctID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.vExpenseAcctID).Name));
					}
					if (vendor != null && vendor.TaxAgency == true && loc.VExpenseSubID == null)
					{
						cache.RaiseExceptionHandling<Location.vExpenseSubID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.vExpenseSubID).Name));
					}
					if (vClass != null && vClass.TaxZoneID != null && loc.VTaxZoneID == null)
					{
						cache.RaiseExceptionHandling<Location.vTaxZoneID>(loc, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(Location.vTaxZoneID).Name));
					}
				}
			}
		}

		object _KeyToAbort = null;

		protected virtual void Location_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
		{
			if (e.Operation == PXDBOperation.Insert)
			{
				if (e.TranStatus == PXTranStatus.Open)
				{
					if ((int?)sender.GetValue<CR.Location.vAPAccountLocationID>(e.Row) < 0)
					{
						_KeyToAbort = sender.GetValue<Location.locationID>(e.Row);

						PXDatabase.Update<Location>(
							new PXDataFieldAssign("VAPAccountLocationID", _KeyToAbort),
							new PXDataFieldRestrict("LocationID", _KeyToAbort),
							PXDataFieldRestrict.OperationSwitchAllowed);

						sender.SetValue<CR.Location.vAPAccountLocationID>(e.Row, _KeyToAbort);
					}

					if ((int?)sender.GetValue<CR.Location.vPaymentInfoLocationID>(e.Row) < 0)
					{
						_KeyToAbort = sender.GetValue<Location.locationID>(e.Row);

						PXDatabase.Update<Location>(
							new PXDataFieldAssign("VPaymentInfoLocationID", _KeyToAbort),
							new PXDataFieldRestrict("LocationID", _KeyToAbort),
							PXDataFieldRestrict.OperationSwitchAllowed);

						sender.SetValue<CR.Location.vPaymentInfoLocationID>(e.Row, _KeyToAbort);
					}

					if ((int?)sender.GetValue<CR.Location.cARAccountLocationID>(e.Row) < 0)
					{
						_KeyToAbort = sender.GetValue<Location.locationID>(e.Row);

						PXDatabase.Update<Location>(
							new PXDataFieldAssign("CARAccountLocationID", _KeyToAbort),
							new PXDataFieldRestrict("LocationID", _KeyToAbort),
							PXDataFieldRestrict.OperationSwitchAllowed);

						sender.SetValue<CR.Location.cARAccountLocationID>(e.Row, _KeyToAbort);
					}
				}
				else
				{
					if (e.TranStatus == PXTranStatus.Aborted)
					{
						if (object.Equals(_KeyToAbort, sender.GetValue<CR.Location.vAPAccountLocationID>(e.Row)))
						{
							object KeyAborted = sender.GetValue<Location.locationID>(e.Row);
							sender.SetValue<CR.Location.vAPAccountLocationID>(e.Row, KeyAborted);
						}

						if (object.Equals(_KeyToAbort, sender.GetValue<CR.Location.vPaymentInfoLocationID>(e.Row)))
						{
							object KeyAborted = sender.GetValue<Location.locationID>(e.Row);
							sender.SetValue<CR.Location.vPaymentInfoLocationID>(e.Row, KeyAborted);
						}

						if (object.Equals(_KeyToAbort, sender.GetValue<CR.Location.cARAccountLocationID>(e.Row)))
						{
							object KeyAborted = sender.GetValue<Location.locationID>(e.Row);
							sender.SetValue<CR.Location.cARAccountLocationID>(e.Row, KeyAborted);
						}
					}
					_KeyToAbort = null;
				}
			}
		}

		public virtual void Location_IsAddressSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			SelectedLocation row = (SelectedLocation)e.Row;
			if (row != null && row.BAccountID != null)
			{
				PXResult<BAccount, Address> res = (PXResult<BAccount, Address>)PXSelectJoin<BAccount, LeftJoin<Address, On<Address.addressID, Equal<BAccount.defAddressID>>>,
														Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Select(this, row.BAccountID);
				if (res != null)
				{
					Address defAddress = res;
					BAccount account = res;
					if (row.IsAddressSameAsMain == true)
					{
						if (row.DefAddressID != account.DefAddressID)
						{
							Address addr = this.Address.Current;
							if (addr == null || addr.AddressID != row.DefAddressID)
								addr = PXSelect<Address, Where<Address.addressID, Equal<Required<Address.addressID>>>>.Select(this, row.DefAddressID);
							if (addr != null && addr.AddressID == row.DefAddressID)
							{
								this.Address.Cache.Delete(addr);
							}
							row.DefAddressID = account.DefAddressID;
							//this.Location.View.RequestRefresh();							
						}
					}
					else
					{
						Address addr = PXCache<Address>.CreateCopy(defAddress);
						addr.BAccountID = row.BAccountID;
						addr.AddressID = null;
						//Copy from default here
						addr = (Address)this.Address.Cache.Insert(addr);
						row.DefAddressID = addr.AddressID;
						this.Location.View.RequestRefresh();
					}
				}
			}
		}

		public virtual void Location_IsContactSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			SelectedLocation row = (SelectedLocation)e.Row;
			//BAccount account = BAccountUtility.FindAccount(this, row.BAccountID); ;
			if (row != null && row.BAccountID != null)
			{
				PXResult<BAccount, Contact> res = (PXResult<BAccount, Contact>)PXSelectJoin<BAccount, LeftJoin<Contact, On<Contact.contactID, Equal<BAccount.defContactID>>>,
														   Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Select(this, row.BAccountID);
				if (res != null)
				{
					Contact defContact = res;
					BAccount account = res;
					if (row.IsContactSameAsMain == true)
					{
						if (row.DefContactID != account.DefContactID)
						{
							Contact cont = this.Contact.Current;
							if (cont == null || cont.ContactID != row.DefContactID)
								cont = PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>.Select(this, row.DefContactID);
							if (cont.ContactID == row.DefContactID)
							{
								this.Contact.Cache.Delete(cont);
							}
							row.DefContactID = account.DefContactID;
							this.Location.View.RequestRefresh();
						}
					}
					else
					{
						Contact cont = new Contact();
						if (defContact != null && defContact.ContactID.HasValue)
							PXCache<Contact>.RestoreCopy(cont, defContact);
						cont.ContactType = ContactTypesAttribute.BAccountProperty;
						cont.BAccountID = account.BAccountID;
						cont.ContactID = null;
						//Copy from default here
						cont = (Contact)this.Contact.Cache.Insert(cont);
						row.DefContactID = cont.ContactID;
						this.Location.View.RequestRefresh();
					}
				}
			}
		}

		#endregion

		#region Address Events

		protected virtual void Address_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            Address row = e.Row as Address;
			BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID); ;
			bool isSameAsMain = false;
			if (acct != null)
			{
				isSameAsMain = (row.AddressID == acct.DefAddressID);
			}
			PXUIFieldAttribute.SetEnabled(cache, row, !isSameAsMain);
			PXUIFieldAttribute.SetEnabled<Address.isValidated>(cache, row, false);
		}

		#endregion

		#region Contact Events

		protected virtual void Contact_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            Contact row = e.Row as Contact;
			BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID); ;
			bool isSameAsMain = false;
			if (acct != null)
			{
				isSameAsMain = (row.ContactID == acct.DefContactID);
			}
			PXUIFieldAttribute.SetEnabled(cache, row, !isSameAsMain);
		}
		#endregion

		protected virtual void EstablishCTaxZoneRule()
		{
			PXResult<AR.Customer, AR.CustomerClass> res = (PXResult<AR.Customer, AR.CustomerClass>)PXSelectJoin<AR.Customer, InnerJoin<AR.CustomerClass, On<AR.Customer.customerClassID, Equal<AR.CustomerClass.customerClassID>>>,
										Where<AR.Customer.bAccountID, Equal<Optional<Location.bAccountID>>>>.Select(this);
			if (res != null)
			{
				AR.CustomerClass customerClass = res;
				if (customerClass != null)
				{
					bool isRequired = (customerClass.RequireTaxZone ?? false);
					PXDefaultAttribute.SetPersistingCheck<Location.cTaxZoneID>(this.Location.Cache, this.Location.Current, isRequired ? PXPersistingCheck.Null : PXPersistingCheck.Nothing);
					PXUIFieldAttribute.SetRequired<Location.cTaxZoneID>(this.Location.Cache, isRequired);
				}
			}
		}

		protected virtual void EstablishVTaxZoneRule()
		{
			PXResult<AP.Vendor, AP.VendorClass> res = (PXResult<AP.Vendor, AP.VendorClass>)PXSelectJoin<AP.Vendor, InnerJoin<AP.VendorClass, On<AP.Vendor.vendorClassID, Equal<AP.VendorClass.vendorClassID>>>,
										Where<AP.Vendor.bAccountID, Equal<Optional<Location.bAccountID>>>>.Select(this);
			if (res != null)
			{
				AP.VendorClass vendorClass = res;
				if (vendorClass != null)
				{
					bool isRequired = (vendorClass.RequireTaxZone ?? false);
					PXDefaultAttribute.SetPersistingCheck<Location.vTaxZoneID>(this.Location.Cache, this.Location.Current, isRequired ? PXPersistingCheck.Null : PXPersistingCheck.Nothing);
					PXUIFieldAttribute.SetRequired<Location.vTaxZoneID>(this.Location.Cache, isRequired);
				}
			}
		}
	}
}
