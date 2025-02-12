using System;
using PX.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using PX.Objects.GL;
using PX.Objects.CR;
using PX.Objects.CA;
using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.Objects.AP
{
	[PXPrimaryGraph(typeof(VendorLocationMaint))]
	[PXSubstitute(GraphType = typeof(VendorLocationMaint))]
    [Serializable]
	public partial class SelectedVendorLocation : SelectedLocation
	{
		#region BAccountID
		public new abstract class bAccountID : PX.Data.IBqlField
		{
		}		
		[PXDefault(typeof(SelectedVendorLocation.bAccountID))]
		[Vendor(typeof(Search<Vendor.bAccountID,
			Where<Where<Vendor.type, Equal<BAccountType.vendorType>, 
			         Or<Vendor.type, Equal<BAccountType.combinedType>>>>>), IsKey = true, TabOrder = 0)]
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
		#region LocationCD
		public new abstract class locationCD : PX.Data.IBqlField
		{
		}		
		[CS.LocationRaw(typeof(Where<Location.bAccountID, Equal<Current<Location.bAccountID>>>), IsKey = true, Visibility = PXUIVisibility.SelectorVisible, DisplayName = "LocationID", TabOrder = 1)]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		public override String LocationCD
		{
			get
			{
				return this._LocationCD;
			}
			set
			{
				this._LocationCD = value;
			}
		}
		#endregion
	}

	public class VendorLocationMaint : LocationMaint
	{
		#region Buttons

		[PXUIField(DisplayName = CS.Messages.ValidateAddresses, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true, FieldClass = CS.Messages.ValidateAddress)]
		[PXButton]
		public override  IEnumerable ValidateAddresses(PXAdapter adapter)
		{
			Location main = this.Location.Current;
			if (main != null)
			{
				bool needSave = false;
				Save.Press();
				Address address = this.Address.Current;
				if (address != null && address.IsValidated == false)
				{
					if (CS.PXAddressValidator.Validate<Address>(this, address, true))
						needSave = true;
				}

				Address remitAddress = this.RemitAddress.Current;
				if (remitAddress != null && remitAddress.IsValidated == false && (remitAddress.AddressID != address.AddressID))
				{
					if (CS.PXAddressValidator.Validate<Address>(this, remitAddress, true))
						needSave = true;
				}
				if (needSave == true)
					this.Save.Press();

			}
			return adapter.Get();
		}
		#endregion
		public PXSelect<LocationAPAccountSub, Where<LocationAPAccountSub.bAccountID, Equal<Current<Location.bAccountID>>, And<LocationAPAccountSub.locationID, Equal<Current<Location.vAPAccountLocationID>>>>> APAccountSubLocation;
		public PXSelect<LocationAPPaymentInfo, Where<LocationAPPaymentInfo.bAccountID, Equal<Current<Location.bAccountID>>, And<LocationAPPaymentInfo.locationID, Equal<Current<Location.vPaymentInfoLocationID>>>>> APPaymentInfoLocation;

		public PXSelect<Address, Where<Address.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>, And<Address.addressID, Equal<Current<LocationAPPaymentInfo.vRemitAddressID>>>>> RemitAddress;
		public PXSelect<Contact, Where<Contact.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>, And<Contact.contactID, Equal<Current<LocationAPPaymentInfo.vRemitContactID>>>>> RemitContact;

		public PXSelectJoin<VendorPaymentMethodDetail,
							InnerJoin<PaymentMethod, On<VendorPaymentMethodDetail.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>,
							InnerJoin<PaymentMethodDetail, 
                                On<PaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>,
							    And<PaymentMethodDetail.detailID, Equal<VendorPaymentMethodDetail.detailID>,
                                    And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>,
                                                Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>>,
							Where<VendorPaymentMethodDetail.bAccountID, Equal<Optional<LocationAPPaymentInfo.bAccountID>>,
									And<VendorPaymentMethodDetail.locationID, Equal<Optional<LocationAPPaymentInfo.locationID>>,
									And<VendorPaymentMethodDetail.paymentMethodID, Equal<Optional<LocationAPPaymentInfo.vPaymentMethodID>>>>>,
                                    OrderBy<Asc<PaymentMethodDetail.orderIndex>>> PaymentDetails;
		public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<LocationAPPaymentInfo.vPaymentMethodID>>,
                        And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>,
                                                Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>> PaymentTypeDetails;

		public PXSelect<Address, Where<Address.bAccountID, Equal<Current<Location.bAccountID>>>> Addresses;
		public PXSelect<Contact, Where<Contact.bAccountID, Equal<Current<Location.bAccountID>>>> Contacts;

		public VendorLocationMaint()
		{
			Views.Caches.Remove(typeof(LocationAPAccountSub));
			Views.Caches.Remove(typeof(LocationAPPaymentInfo));
		}

		public override void Persist()
		{
			base.Persist();
			this.APPaymentInfoLocation.Cache.Clear();
			this.APAccountSubLocation.Cache.Clear();
		}

		public PXAction<Location> viewRemitOnMap;

		[PXUIField(DisplayName = CR.Messages.ViewOnMap, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton()]
		public virtual IEnumerable ViewRemitOnMap(PXAdapter adapter)
		{

			BAccountUtility.ViewOnMap(this.RemitAddress.Current);
			return adapter.Get();
		}

		#region Location Events
		protected override void Location_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			BAccount baccount = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
			
			PXUIFieldAttribute.SetEnabled<CR.Location.isAPAccountSameAsMain>(sender, e.Row, baccount!=null && !object.Equals(baccount.DefLocationID, ((Location)e.Row).LocationID));
            PXUIFieldAttribute.SetEnabled<CR.Location.isAPPaymentInfoSameAsMain>(sender, e.Row, baccount != null && !object.Equals(baccount.DefLocationID, ((Location)e.Row).LocationID));

			base.Location_RowSelected(sender, e);
		}

		protected override void Location_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			if (e.Row != null)
			{
				Location record = (Location)e.Row;

				record.IsAPAccountSameAsMain = !object.Equals(record.LocationID, record.VAPAccountLocationID);
				record.IsAPPaymentInfoSameAsMain = !object.Equals(record.LocationID, record.VPaymentInfoLocationID);

				base.Location_RowSelecting(sender, e);
			}
		}

		protected override void Location_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			Location record = (Location)e.Row;

			record.IsAPAccountSameAsMain = !object.Equals(record.LocationID, record.VAPAccountLocationID);
			record.IsAPPaymentInfoSameAsMain = !object.Equals(record.LocationID, record.VPaymentInfoLocationID);

			base.Location_RowInserted(sender, e);
		}

		protected virtual void Location_IsAPAccountSameAsMain_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CR.Location record = (CR.Location)e.Row;

			if (record.IsAPAccountSameAsMain == false)
			{
				LocationAPAccountSub mainloc = APAccountSubLocation.Select();
				record.VAPAccountID = mainloc.VAPAccountID;
				record.VAPSubID = mainloc.VAPSubID;
				record.VAPAccountLocationID = record.LocationID;

				LocationAPAccountSub copyloc = new LocationAPAccountSub();
				copyloc.BAccountID = record.BAccountID;
				copyloc.LocationID = record.LocationID;
				copyloc.VAPAccountID = record.VAPAccountID;
				copyloc.VAPSubID = record.VAPSubID;

				BusinessAccount.Cache.Current = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				APAccountSubLocation.Insert(copyloc);
			}
			if (record.IsAPAccountSameAsMain == true)
			{
				record.VAPAccountID = null;
				record.VAPSubID = null;
				BAccount baccount = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				if (baccount != null)
				{
					record.VAPAccountLocationID = baccount.DefLocationID;
				}
			}
		}

		protected virtual void Location_IsAPPaymentInfoSameAsMain_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CR.Location record = (CR.Location)e.Row;

			if (record.IsAPPaymentInfoSameAsMain == false)
			{
				LocationAPPaymentInfo mainloc = APPaymentInfoLocation.Select();
				record.VCashAccountID = mainloc.VCashAccountID;
				record.VPaymentMethodID = mainloc.VPaymentMethodID;
				record.VPaymentLeadTime = mainloc.VPaymentLeadTime;
				record.VPaymentByType = mainloc.VPaymentByType;				
				record.VSeparateCheck = mainloc.VSeparateCheck;
				record.IsRemitAddressSameAsMain = mainloc.IsRemitAddressSameAsMain;
				record.VRemitAddressID = mainloc.VRemitAddressID;
				record.IsRemitContactSameAsMain = mainloc.IsRemitContactSameAsMain;
				record.VRemitContactID = mainloc.VRemitContactID;
				record.VPaymentInfoLocationID = record.LocationID;

				LocationAPPaymentInfo copyloc = new LocationAPPaymentInfo();
				copyloc.BAccountID = record.BAccountID;
				copyloc.LocationID = record.LocationID;
				copyloc.VCashAccountID = record.VCashAccountID;
				copyloc.VPaymentMethodID = record.VPaymentMethodID;
				copyloc.VPaymentLeadTime = record.VPaymentLeadTime;
				copyloc.VPaymentByType = record.VPaymentByType;
				copyloc.VSeparateCheck = record.VSeparateCheck;
				copyloc.IsRemitAddressSameAsMain = record.IsRemitAddressSameAsMain;
				copyloc.VDefAddressID = record.VDefAddressID;
				copyloc.VRemitAddressID = record.VRemitAddressID;
				copyloc.IsRemitContactSameAsMain = record.IsRemitContactSameAsMain;
				copyloc.VRemitContactID = record.VRemitContactID;
				copyloc.VDefContactID = record.VDefContactID;

				if (copyloc.VDefAddressID != copyloc.VRemitAddressID)
				{
					Address copyaddr = FindAddress(copyloc.VRemitAddressID);
					copyaddr = PXCache<Address>.CreateCopy(copyaddr);
					copyaddr.AddressID = null;

					copyaddr = RemitAddress.Insert(copyaddr);
					copyloc.VRemitAddressID = copyaddr.AddressID;
				}

				if (copyloc.VDefContactID != copyloc.VRemitContactID)
				{
					Contact copycont = FindContact(copyloc.VRemitContactID);
					copycont = PXCache<Contact>.CreateCopy(copycont);
					copycont.ContactID = null;

					copycont = RemitContact.Insert(copycont);
					copyloc.VRemitContactID = copycont.ContactID;
				}

				foreach (VendorPaymentMethodDetail maindet in this.PaymentDetails.Select(mainloc.BAccountID, mainloc.LocationID, mainloc.VPaymentMethodID))
				{
					VendorPaymentMethodDetail copydet = PXCache<VendorPaymentMethodDetail>.CreateCopy(maindet);
					copydet.LocationID = copyloc.LocationID;

					this.PaymentDetails.Insert(copydet);
				}

				BusinessAccount.Cache.Current = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				APPaymentInfoLocation.Insert(copyloc);
			}
			if (record.IsAPPaymentInfoSameAsMain == true)
			{
				foreach (VendorPaymentMethodDetail copydet in this.PaymentDetails.Select(record.BAccountID, record.LocationID, record.VPaymentMethodID))
				{
					this.PaymentDetails.Delete(copydet);
				}

				BAccount baccount = (BAccount)PXParentAttribute.SelectParent(sender, e.Row, typeof(BAccount));
				if (baccount != null)
				{
					record.VPaymentInfoLocationID = baccount.DefLocationID;

					Location mainloc = PXSelect<CR.Location, Where<CR.Location.bAccountID, Equal<Required<CR.Location.bAccountID>>, And<CR.Location.locationID, Equal<Required<CR.Location.locationID>>>>>.Select(sender.Graph, baccount.BAccountID, baccount.DefLocationID);

					if (mainloc != null)
					{
						if (record.DefAddressID != record.VRemitAddressID && mainloc.VRemitAddressID != record.VRemitAddressID)
						{
							Address copyaddr = FindAddress(record.VRemitAddressID);
							RemitAddress.Delete(copyaddr);
						}

						if (record.DefContactID != record.VRemitContactID && mainloc.VRemitContactID != record.VRemitContactID)
						{
							Contact copycont = FindContact(record.VRemitContactID);
							RemitContact.Delete(copycont);
						}
					}
				}

				record.VCashAccountID = null;
				record.VPaymentMethodID = null;
				record.VPaymentLeadTime = 0;
				record.VSeparateCheck = false;
				record.VRemitAddressID = null;
				record.VRemitContactID = null;
			}
		}

		protected virtual void Location_CBranchID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = null;
			e.Cancel = true;
		}

		protected override void Location_VBranchID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
		}
		#endregion

		#region LocationAPAccountSub Events

		protected virtual void LocationAPAccountSub_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            if (Location.Current != null)
			{
				PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals(Location.Current.LocationID, Location.Current.VAPAccountLocationID));
			}
		}

		protected virtual void LocationAPAccountSub_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			LocationAPAccountSub record = (LocationAPAccountSub)e.Row;

			if (!sender.ObjectsEqual<LocationAPAccountSub.vAPAccountID, LocationAPAccountSub.vAPSubID>(e.Row, e.OldRow))
			{
				Location mainloc = Location.Current;
				mainloc.VAPAccountID = record.VAPAccountID;
				mainloc.VAPSubID = record.VAPSubID;

				if (Location.Cache.GetStatus(mainloc) == PXEntryStatus.Notchanged)
				{
					Location.Cache.SetStatus(mainloc, PXEntryStatus.Updated);
				}
			}
		}
		#endregion

		#region LocationAPPaymentInfo Events

		protected virtual void LocationAPPaymentInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            LocationAPPaymentInfo row = (LocationAPPaymentInfo)e.Row;
			if (Location.Current != null)
			{
				bool enableEdit = object.Equals(Location.Current.LocationID, Location.Current.VPaymentInfoLocationID);
				bool hasPaymentMethod = (row!= null) && (String.IsNullOrEmpty(row.VPaymentMethodID) == false);
				PXUIFieldAttribute.SetEnabled(sender, e.Row, enableEdit);
				PXUIFieldAttribute.SetEnabled<LocationAPPaymentInfo.vCashAccountID>(sender, e.Row, enableEdit && hasPaymentMethod);
			}
		}

		protected virtual void LocationAPPaymentInfo_IsRemitAddressSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			LocationAPPaymentInfo owner = (LocationAPPaymentInfo)e.Row;
			if (owner != null)
			{
				if (owner.IsRemitAddressSameAsMain == true)
				{
					if (owner.VRemitAddressID != owner.VDefAddressID)
					{
						Address extAddr = this.FindAddress(owner.VRemitAddressID);
						if (extAddr != null && extAddr.AddressID == owner.VRemitAddressID)
						{
							this.RemitAddress.Delete(extAddr);
						}
						owner.VRemitAddressID = owner.VDefAddressID;
						//if (this.Location.Cache.Locate(owner) != null)
						//  this.Location.Cache.Update(owner);
					}
				}

				if (owner.IsRemitAddressSameAsMain == false)
				{
					if (owner.VRemitAddressID != null)
					{
						if (owner.VRemitAddressID == owner.VDefAddressID)
						{
							Address defAddress = this.FindAddress(owner.VDefAddressID);
							Address addr = PXCache<Address>.CreateCopy(defAddress);
							addr.AddressID = null;
							addr.BAccountID = owner.BAccountID;
							addr = this.RemitAddress.Insert(addr);
							owner.VRemitAddressID = addr.AddressID;
							//if (this.Location.Cache.Locate(owner) != null)
							//  this.Location.Cache.Update(owner);
						}
					}
				}
			}
		}

		protected virtual void LocationAPPaymentInfo_IsRemitContactSameAsMain_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			LocationAPPaymentInfo owner = (LocationAPPaymentInfo)e.Row;
			if (owner != null)
			{
				if (owner.IsRemitContactSameAsMain == true)
				{
					if (owner.VRemitContactID != owner.VDefContactID)
					{
						Contact contact = this.FindContact(owner.VRemitContactID);
						if (contact != null && contact.ContactID == owner.VRemitContactID)
						{
							this.RemitContact.Delete(contact);
						}
						owner.VRemitContactID = owner.VDefContactID;
						//if (this.Location.Cache.Locate(owner) != null)
						//  this.Location.Cache.Update(owner);
					}
				}

				if (owner.IsRemitContactSameAsMain == false)
				{
					if (owner.VRemitContactID != null)
					{
						if (owner.VRemitContactID == owner.VDefContactID)
						{
							Contact defContact = this.FindContact(owner.VDefContactID);
							Contact cont = PXCache<Contact>.CreateCopy(defContact);
							cont.ContactID = null;
							cont.BAccountID = owner.BAccountID;
							cont.ContactType = ContactTypesAttribute.BAccountProperty;
							cont = (Contact)this.RemitContact.Cache.Insert(cont);
							owner.VRemitContactID = cont.ContactID;
							//if (this.Location.Cache.Locate(owner) != null)
							//  this.Location.Cache.Update(owner);
						}
					}
				}
			}
		}

		protected virtual void LocationAPPaymentInfo_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			LocationAPPaymentInfo record = (LocationAPPaymentInfo)e.Row;

			record.IsRemitAddressSameAsMain = object.Equals(record.VDefAddressID, record.VRemitAddressID);
			record.IsRemitContactSameAsMain = object.Equals(record.VDefContactID, record.VRemitContactID);
		}


		protected virtual void LocationAPPaymentInfo_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			if (e.Row != null)
			{
				LocationAPPaymentInfo record = (LocationAPPaymentInfo)e.Row;

				record.IsRemitAddressSameAsMain = object.Equals(record.VDefAddressID, record.VRemitAddressID);
				record.IsRemitContactSameAsMain = object.Equals(record.VDefContactID, record.VRemitContactID);
			}
		}

		protected virtual void LocationAPPaymentInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			LocationAPPaymentInfo record = (LocationAPPaymentInfo)e.Row;
            if (!sender.ObjectsEqual<LocationAPPaymentInfo.vCashAccountID, LocationAPPaymentInfo.vPaymentMethodID, LocationAPPaymentInfo.vPaymentLeadTime, LocationAPPaymentInfo.vPaymentByType, LocationAPPaymentInfo.vSeparateCheck, LocationAPPaymentInfo.isRemitAddressSameAsMain, LocationAPPaymentInfo.isRemitContactSameAsMain>(e.Row, e.OldRow))
			{
				Location mainloc = Location.Current;
				mainloc.VCashAccountID = record.VCashAccountID;
				mainloc.VPaymentMethodID = record.VPaymentMethodID;
				mainloc.VPaymentLeadTime = record.VPaymentLeadTime;
				mainloc.VPaymentByType = record.VPaymentByType;
				mainloc.VSeparateCheck = record.VSeparateCheck;
				mainloc.IsRemitAddressSameAsMain = record.IsRemitAddressSameAsMain;
				mainloc.VRemitAddressID = record.VRemitAddressID;
				mainloc.IsRemitContactSameAsMain = record.IsRemitContactSameAsMain;
				mainloc.VRemitContactID = record.VRemitContactID;

				if (Location.Cache.GetStatus(mainloc) == PXEntryStatus.Notchanged)
				{
					Location.Cache.SetStatus(mainloc, PXEntryStatus.Updated);
				}

				sender.Graph.Caches[typeof (Location)].IsDirty = true;
			}
		}        

		protected virtual void LocationAPPaymentInfo_VPaymentMethodID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			string oldValue = (string)e.OldValue; 
			LocationAPPaymentInfo row = (LocationAPPaymentInfo)e.Row;
			if (!String.IsNullOrEmpty(oldValue))
			{
				this.ClearPaymentDetails((LocationAPPaymentInfo)e.Row, oldValue, true);
			}
			this.FillPaymentDetails((LocationAPPaymentInfo)e.Row);
			sender.SetDefaultExt<LocationAPPaymentInfo.vCashAccountID>(e.Row);			
		}
		protected virtual void LocationAPPaymentInfo_VCashAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e) 
		{
			//LocationAPPaymentInfo row = (LocationAPPaymentInfo)e.Row;
			e.NewValue = null;
			e.Cancel = true;			
		}
		#endregion

		#region VendorPaymentMethodDetail Events

		protected virtual void VendorPaymentMethodDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            if (Location.Current != null)
			{
				PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals(Location.Current.LocationID, Location.Current.VPaymentInfoLocationID));
			}
		}
		#endregion

		#region Contact Events

		protected override void Contact_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			Contact row = e.Row as Contact;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				bool isSameAsMain = false;
				if (acct != null)
				{
					isSameAsMain = (row.ContactID == acct.DefContactID);
				}

				if (!isSameAsMain && Location.Current != null && APPaymentInfoLocation.Current != null && object.Equals(APPaymentInfoLocation.Current.VRemitContactID, row.ContactID))
				{
					PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals(Location.Current.LocationID, Location.Current.VPaymentInfoLocationID));
				}
				else
				{
					base.Contact_RowSelected(sender, e);
				}
			}			
		}

		#endregion

		#region Address Events

		protected override void Address_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			Address row = e.Row as Address;
			if (row != null)
			{
				BAccount acct = BAccountUtility.FindAccount(this, row.BAccountID);
				bool isSameAsMain = false;
				if (acct != null)
				{
					isSameAsMain = (row.AddressID == acct.DefAddressID);
				}

				if (!isSameAsMain && Location.Current != null && APPaymentInfoLocation.Current != null && object.Equals(APPaymentInfoLocation.Current.VRemitAddressID, row.AddressID))
				{
					PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals(Location.Current.LocationID, Location.Current.VPaymentInfoLocationID));
				}
				else
				{
					base.Address_RowSelected(sender, e);
				}
			}
		}

		protected virtual void Address_CountryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Address addr = (Address)e.Row;
			if ((string)e.OldValue != addr.CountryID)
			{
				addr.State = null;
			}
		}

		#endregion

		#region Functions

		protected virtual Address FindAddress(int? aId)
		{
			if (aId.HasValue)
			{
				foreach (Address it in this.Addresses.Select())
				{
					if (it.AddressID == aId) return it;
				}
			}
			return null;
		}

		protected virtual Contact FindContact(int? aId)
		{
			if (aId.HasValue)
			{
				foreach (Contact it in this.Contacts.Select())
				{
					if (it.ContactID == aId) return it;
				}
			}
			return null;
		}

		protected virtual void FillPaymentDetails(IPaymentTypeDetailMaster account)
		{
			if (account != null)
			{
				if (!string.IsNullOrEmpty(account.VPaymentMethodID))
				{
					PaymentMethod paymentTypeDef = PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Select(this, account.VPaymentMethodID);
					if (paymentTypeDef != null)
                    {
                        List<PaymentMethodDetail> toAdd = new List<PaymentMethodDetail>();
                        foreach (PaymentMethodDetail it in this.PaymentTypeDetails.Select(account.VPaymentMethodID))
                        {
                            VendorPaymentMethodDetail detail = null;
                            foreach (VendorPaymentMethodDetail iPDet in this.PaymentDetails.Select(account.BAccountID, account.LocationID, account.VPaymentMethodID))
                            {
                                if (iPDet.DetailID == it.DetailID)
                                {
                                    detail = iPDet;
                                    break;
                                }
                            }
                            if (detail == null)
                            {
                                toAdd.Add(it);
                            }
                        }
                        using (ReadOnlyScope rs = new ReadOnlyScope(this.PaymentDetails.Cache))
                        {
                            foreach (PaymentMethodDetail it in toAdd)
                            {
                                VendorPaymentMethodDetail detail = new VendorPaymentMethodDetail();
                                detail.BAccountID = account.BAccountID;
                                detail.LocationID = account.LocationID;
                                detail.PaymentMethodID = account.VPaymentMethodID;
                                detail.DetailID = it.DetailID;
                                detail = this.PaymentDetails.Insert(detail);
                            }
                            if (toAdd.Count > 0)
                            {
                                this.PaymentDetails.View.RequestRefresh();
                            }
                        }
                    }
				}
			}
		}

		protected virtual void ClearPaymentDetails(IPaymentTypeDetailMaster account, string paymentTypeID, bool clearNewOnly)
		{
			foreach (VendorPaymentMethodDetail it in this.PaymentDetails.Select(account.BAccountID, account.LocationID, paymentTypeID))
			{
				bool doDelete = true;
				if (clearNewOnly)
				{
					PXEntryStatus status = this.PaymentDetails.Cache.GetStatus(it);
					doDelete = (status == PXEntryStatus.Inserted);
				}
				if (doDelete)
					this.PaymentDetails.Delete(it);
			}
		}

		#endregion
	}

	[PXProjection(typeof(Select<CRLocation>), Persistent = false)]
    [PXCacheName(Messages.LocationAPAccountSub)]
    [Serializable]
	public partial class LocationAPAccountSub : IBqlTable
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
		#region VPaymentInfoLocationID
		public abstract class vPaymentInfoLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _VPaymentInfoLocationID;
		[PXDBInt(BqlField = typeof(CRLocation.vPaymentInfoLocationID))]
		public virtual Int32? VPaymentInfoLocationID
		{
			get
			{
				return this._VPaymentInfoLocationID;
			}
			set
			{
				this._VPaymentInfoLocationID = value;
			}
		}
		#endregion
		
		#region VPaymentMethodID
		public abstract class vPaymentMethodID : PX.Data.IBqlField
		{
		}
		protected String _PaymentMethodID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(CRLocation.vPaymentMethodID))]
		[PXUIField(DisplayName = "Payment Method")]
		public virtual String VPaymentMethodID
		{
			get
			{
				return this._PaymentMethodID;
			}
			set
			{
				this._PaymentMethodID = value;
			}
		}
		#endregion
		#region VCashAccountID
		public abstract class vCashAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _VCashAccountID;
		[CashAccount(BqlField = typeof(CRLocation.vCashAccountID), DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		public virtual Int32? VCashAccountID
		{
			get
			{
				return this._VCashAccountID;
			}
			set
			{
				this._VCashAccountID = value;
			}
		}
		#endregion
		#region VPaymentLeadTime
		public abstract class vPaymentLeadTime : PX.Data.IBqlField
		{
		}
		protected Int16? _VPaymentLeadTime;
		[PXDBShort(BqlField = typeof(CRLocation.vPaymentLeadTime), MinValue = 0, MaxValue = 3660)]
		[PXDefault((short)0)]
		[PXUIField(DisplayName = "Payment Lead Time (days)")]
		public Int16? VPaymentLeadTime
		{
			get
			{
				return this._VPaymentLeadTime;
			}
			set
			{
				this._VPaymentLeadTime = value;
			}
		}
		#endregion
		#region VSeparateCheck
		public abstract class vSeparateCheck : PX.Data.IBqlField
		{
		}
		protected Boolean? _VSeparateCheck;
		[PXDBBool(BqlField = typeof(CRLocation.vSeparateCheck))]
		[PXUIField(DisplayName = "Pay Separately")]
		[PXDefault(false)]
		public virtual Boolean? VSeparateCheck
		{
			get
			{
				return this._VSeparateCheck;
			}
			set
			{
				this._VSeparateCheck = value;
			}
		}
		#endregion
		#region VRemitAddressID
		public abstract class vRemitAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _VRemitAddressID;
		[PXDBInt(BqlField = typeof(CRLocation.vRemitAddressID))]
		public virtual Int32? VRemitAddressID
		{
			get
			{
				return this._VRemitAddressID;
			}
			set
			{
				this._VRemitAddressID = value;
			}
		}
		#endregion
		#region VRemitContactID
		public abstract class vRemitContactID : PX.Data.IBqlField
		{
		}
		protected Int32? _VRemitContactID;
		[PXDBInt(BqlField = typeof(CRLocation.vRemitContactID))]
		public virtual Int32? VRemitContactID
		{
			get
			{
				return this._VRemitContactID;
			}
			set
			{
				this._VRemitContactID = value;
			}
		}
		#endregion
		#region VAPAccountLocationID
		public abstract class vAPAccountLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPAccountLocationID;
		[PXDBInt(BqlField = typeof(CRLocation.vAPAccountLocationID))]
		public virtual Int32? VAPAccountLocationID
		{
			get
			{
				return this._VAPAccountLocationID;
			}
			set
			{
				this._VAPAccountLocationID = value;
			}
		}
		#endregion
		#region VAPAccountID
		public abstract class vAPAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPAccountID;
		[Account(BqlField = typeof(CRLocation.vAPAccountID), DisplayName = "AP Account", DescriptionField = typeof(Account.description))]
		public virtual Int32? VAPAccountID
		{
			get
			{
				return this._VAPAccountID;
			}
			set
			{
				this._VAPAccountID = value;
			}
		}
		#endregion
		#region VAPSubID
		public abstract class vAPSubID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPSubID;
		[SubAccount(typeof(LocationAPAccountSub.vAPAccountID), BqlField = typeof(CRLocation.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof(Sub.description))]
		public virtual Int32? VAPSubID
		{
			get
			{
				return this._VAPSubID;
			}
			set
			{
				this._VAPSubID = value;
			}
		}
		#endregion
	}

	public interface IPaymentTypeDetailMaster
	{
		int? BAccountID { get; set; }
		int? LocationID { get; set; }
		string VPaymentMethodID { get; set; }
	}

	[PXProjection(typeof(Select2<CRLocation, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<CRLocation.bAccountID>>>>), Persistent = false)]
    [PXCacheName(Messages.LocationAPPaymentInfo)]
    [Serializable]
	public partial class LocationAPPaymentInfo : IBqlTable, IPaymentTypeDetailMaster
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
		#region VDefAddressID
		public abstract class vDefAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _VDefAddressID;
		[PXDBInt(BqlField = typeof(BAccountR.defAddressID))]
		[PXDefault(typeof(Select<BAccount, Where<BAccount.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>>>), SourceField = typeof(BAccount.defAddressID))]
		public virtual Int32? VDefAddressID
		{
			get
			{
				return this._VDefAddressID;
			}
			set
			{
				this._VDefAddressID = value;
			}
		}
		#endregion
		#region VDefContactID
		public abstract class vDefContactID : PX.Data.IBqlField
		{
		}
		protected Int32? _VDefContactID;
		[PXDBInt(BqlField = typeof(BAccountR.defContactID))]
		[PXDefault(typeof(Select<BAccount, Where<BAccount.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>>>), SourceField = typeof(BAccount.defContactID))]
		public virtual Int32? VDefContactID
		{
			get
			{
				return this._VDefContactID;
			}
			set
			{
				this._VDefContactID = value;
			}
		}
		#endregion
		#region VPaymentInfoLocationID
		public abstract class vPaymentInfoLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _VPaymentInfoLocationID;
		[PXDBInt(BqlField = typeof(CRLocation.vPaymentInfoLocationID))]
		public virtual Int32? VPaymentInfoLocationID
		{
			get
			{
				return this._VPaymentInfoLocationID;
			}
			set
			{
				this._VPaymentInfoLocationID = value;
			}
		}
		#endregion
		#region VPaymentMethodID
		public abstract class vPaymentMethodID : PX.Data.IBqlField
		{
		}
		protected String _VPaymentMethodID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(CRLocation.vPaymentMethodID))]
		[PXUIField(DisplayName = "Payment Method")]
		[PXSelector(typeof(Search<PaymentMethod.paymentMethodID,
							Where<PaymentMethod.useForAP, Equal<True>,
							And<PaymentMethod.isActive, Equal<True>>>>),DescriptionField = typeof(PaymentMethod.descr))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String VPaymentMethodID
		{
			get
			{
				return this._VPaymentMethodID;
			}
			set
			{
				this._VPaymentMethodID = value;
			}
		}
		#endregion
		#region VCashAccountID
		public abstract class vCashAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _VCashAccountID;
		[CashAccount(typeof(Search2<CashAccount.cashAccountID,
						InnerJoin<PaymentMethodAccount, 
							On<PaymentMethodAccount.cashAccountID,Equal<CashAccount.cashAccountID>>>,
						Where2<Match<Current<AccessInfo.userName>>, 
							And<CashAccount.clearingAccount, Equal<False>,
							And<PaymentMethodAccount.paymentMethodID,Equal<Current<LocationAPPaymentInfo.vPaymentMethodID>>,
							And<PaymentMethodAccount.useForAP,Equal<True>>>>>>), BqlField = typeof(CRLocation.vCashAccountID), DisplayName = "Cash Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? VCashAccountID
		{
			get
			{
				return this._VCashAccountID;
			}
			set
			{
				this._VCashAccountID = value;
			}
		}
		#endregion		
		#region VPaymentLeadTime
		public abstract class vPaymentLeadTime : PX.Data.IBqlField
		{
		}
		protected Int16? _VPaymentLeadTime;
		[PXDBShort(BqlField = typeof(CRLocation.vPaymentLeadTime), MinValue = -3660, MaxValue = 3660)]
		[PXDefault((short)0)]
		[PXUIField(DisplayName = "Payment Lead Time (days)")]
		public Int16? VPaymentLeadTime
		{
			get
			{
				return this._VPaymentLeadTime;
			}
			set
			{
				this._VPaymentLeadTime = value;
			}
		}
		#endregion
		#region VSeparateCheck
		public abstract class vSeparateCheck : PX.Data.IBqlField
		{
		}
		protected Boolean? _VSeparateCheck;
		[PXDBBool(BqlField = typeof(CRLocation.vSeparateCheck))]
		[PXUIField(DisplayName = "Pay Separately")]
		[PXDefault(false)]
		public virtual Boolean? VSeparateCheck
		{
			get
			{
				return this._VSeparateCheck;
			}
			set
			{
				this._VSeparateCheck = value;
			}
		}
		#endregion
		#region VPaymentByType
		public abstract class vPaymentByType : PX.Data.IBqlField
		{
		}
		protected int? _VPaymentByType;
		[PXDBInt(BqlField = typeof(CRLocation.vPaymentByType))]
		[PXDefault(0)]
		[APPaymentBy.List]
		[PXUIField(DisplayName = "Payment By")]
		public int? VPaymentByType
		{
			get
			{
				return this._VPaymentByType;
			}
			set
			{
				this._VPaymentByType = value;
			}
		}
		#endregion
		#region IsRemitAddressSameAsMain
		public abstract class isRemitAddressSameAsMain : PX.Data.IBqlField
		{
		}
		protected bool? _IsRemitAddressSameAsMain;
		[PXBool()]
		[PXUIField(DisplayName = "Same as Main")]
		public virtual bool? IsRemitAddressSameAsMain
		{
			get
			{
				return this._IsRemitAddressSameAsMain;
			}
			set
			{
				this._IsRemitAddressSameAsMain = value;
			}
		}
		#endregion
		#region IsRemitContactSameAsMain
		public abstract class isRemitContactSameAsMain : PX.Data.IBqlField
		{
		}
		protected bool? _IsRemitContactSameAsMain;
		[PXBool()]
		[PXUIField(DisplayName = "Same as Main")]
		public virtual bool? IsRemitContactSameAsMain
		{
			get
			{
				return this._IsRemitContactSameAsMain;
			}
			set
			{
				this._IsRemitContactSameAsMain = value;
			}
		}
		#endregion
		#region VRemitAddressID
		public abstract class vRemitAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _VRemitAddressID;
		[PXDBInt(BqlField = typeof(CRLocation.vRemitAddressID))]
		public virtual Int32? VRemitAddressID
		{
			get
			{
				return this._VRemitAddressID;
			}
			set
			{
				this._VRemitAddressID = value;
			}
		}
		#endregion
		#region VRemitContactID
		public abstract class vRemitContactID : PX.Data.IBqlField
		{
		}
		protected Int32? _VRemitContactID;
		[PXDBInt(BqlField = typeof(CRLocation.vRemitContactID))]
		public virtual Int32? VRemitContactID
		{
			get
			{
				return this._VRemitContactID;
			}
			set
			{
				this._VRemitContactID = value;
			}
		}
		#endregion
		#region VAPAccountLocationID
		public abstract class vAPAccountLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPAccountLocationID;
		[PXDBInt(BqlField = typeof(CRLocation.vAPAccountLocationID))]
		public virtual Int32? VAPAccountLocationID
		{
			get
			{
				return this._VAPAccountLocationID;
			}
			set
			{
				this._VAPAccountLocationID = value;
			}
		}
		#endregion
		#region VAPAccountID
		public abstract class vAPAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPAccountID;
		[Account(BqlField = typeof(CRLocation.vAPAccountID), DisplayName = "AP Account", DescriptionField = typeof(Account.description))]
		public virtual Int32? VAPAccountID
		{
			get
			{
				return this._VAPAccountID;
			}
			set
			{
				this._VAPAccountID = value;
			}
		}
		#endregion
		#region VAPSubID
		public abstract class vAPSubID : PX.Data.IBqlField
		{
		}
		protected Int32? _VAPSubID;
		[SubAccount(typeof(LocationAPAccountSub.vAPAccountID), BqlField = typeof(CRLocation.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof(Sub.description))]
		public virtual Int32? VAPSubID
		{
			get
			{
				return this._VAPSubID;
			}
			set
			{
				this._VAPSubID = value;
			}
		}
		#endregion
	}
}
