using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using System.Collections;
using PX.Objects.IN;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.Objects.PO
{
	[System.SerializableAttribute()]
	public partial class POUpdateEffectivePriceFilter : PX.Data.IBqlTable
	{
		#region PendingPriceDate
		public abstract class pendingPriceDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _PendingPriceDate;
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Pending Price Date")]
		public virtual DateTime? PendingPriceDate
		{
			get
			{
				return this._PendingPriceDate;
			}
			set
			{
				this._PendingPriceDate = value;
			}
		}
		#endregion
	}
	[PXProjection(typeof(
		Select2<CRLocation,
		InnerJoin<Vendor, On<Vendor.bAccountID, Equal<CRLocation.bAccountID>>>>))]
	[PXCacheName(Messages.VendorLocation)]
    [Serializable]
	public partial class VendorLocation : IBqlTable
	{		
		#region BAccountID
		public abstract class bAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _BAccountID;		
		[PXUIField(DisplayName = "Vendor")]
		[VendorNonEmployeeActive(IsKey = true, BqlField=typeof(CRLocation.bAccountID))]
		public virtual Int32? BAccountID
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

		#region AcctCD
		public abstract class acctCD : PX.Data.IBqlField
		{
		}
		protected string _AcctCD;
		[VendorRaw(BqlField=typeof(Vendor.acctCD))]
		[PXDefault()]
		[PX.Data.EP.PXFieldDescription]
		public virtual String AcctCD
		{
			get
			{
				return this._AcctCD;
			}
			set
			{
				this._AcctCD = value;
			}
		}
		#endregion

		#region LocationID
		public abstract class locationID : PX.Data.IBqlField
		{
		}
		protected Int32? _LocationID;
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<VendorLocation.bAccountID>>>),
			DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Location", IsKey=true, BqlField=typeof(CRLocation.locationID))]
		[PXDefault(typeof(Search<Vendor.defLocationID, Where<Vendor.bAccountID, Equal<Current<VendorLocation.bAccountID>>>>))]
		[PXFormula(typeof(Default<VendorLocation.bAccountID>))]
		[PX.Data.EP.PXFieldDescription]
		public virtual Int32? LocationID
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

		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, BqlField=typeof(Vendor.curyID))]
		[PXSelector(typeof(Currency.curyID), CacheGlobal = true)]
		[PXDefault(typeof(Search<VendorClass.curyID, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Currency", Enabled=false)]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion

		#region VSiteID
		public abstract class vSiteID : PX.Data.IBqlField
		{
		}
		protected Int32? _VSiteID;
		[PXDBInt(BqlField=typeof(CRLocation.vSiteID))]
		[PXUIField(DisplayName = "Warehouse", Visibility = PXUIVisibility.Visible, Enabled=false)]
		[PXDimensionSelector(SiteAttribute.DimensionName, typeof(INSite.siteID), typeof(INSite.siteCD), DescriptionField = typeof(INSite.descr))]
		public virtual Int32? VSiteID
		{
			get
			{
				return this._VSiteID;
			}
			set
			{
				this._VSiteID = value;
			}
		}
		#endregion

		#region VLeadTime
		public abstract class vLeadTime : PX.Data.IBqlField
		{
		}
		protected Int16? _VLeadTime;
		[PXDBShort(MinValue = 0, MaxValue = 100000, BqlField=typeof(CRLocation.vLeadTime))]
		[PXUIField(DisplayName = "Lead Time (days)", Enabled=false)]
		public virtual Int16? VLeadTime
		{
			get
			{
				return this._VLeadTime;
			}
			set
			{
				this._VLeadTime = value;
			}
		}
		#endregion

		#region NoteID
		public abstract class noteID : PX.Data.IBqlField { }

		[PXNote(DescriptionField = typeof(VendorLocation.acctCD),
			Selector = typeof(VendorLocation.acctCD),
			BqlField = typeof(Vendor.noteID))]
		public virtual Int64? NoteID { get; set; }
		#endregion
	}

	public class POVendorCatalogueMaint : PXGraph<POVendorCatalogueMaint>, PXImportAttribute.IPXPrepareItems	
	{
		#region Cahce Attached
		[Inventory(Filterable = true)]
		protected virtual void POVendorInventory_InventoryID_CacheAttached(PXCache sender)
		{
			
		}
		[PXDBInt]
		[PXDefault(typeof(VendorLocation.bAccountID), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void POVendorInventory_VendorID_CacheAttached(PXCache sender)
		{

		}
		[PXDefault(typeof(VendorLocation.locationID), PersistingCheck = PXPersistingCheck.Nothing)]
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<VendorLocation.bAccountID>>>),
				DescriptionField = typeof(Location.descr), DisplayName = "Vendor Location", Visible = false)]
		protected virtual void POVendorInventory_VendorLocationID_CacheAttached(PXCache sender)
		{

		}
		[PXBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "All Locations")]
		[PXDBCalced(typeof(Switch<Case<Where<POVendorInventory.vendorLocationID, IsNull>, True>, False>), typeof(bool))]
		protected virtual void POVendorInventory_AllLocations_CacheAttached(PXCache sender)
		{
		}

		[PXDefault(typeof(Search<InventoryItem.purchaseUnit, Where<InventoryItem.inventoryID, Equal<Current<POVendorInventory.inventoryID>>>>))]
		[PXFormula(typeof(Default<POVendorInventory.inventoryID>))]
		[INUnit(typeof(POVendorInventory.inventoryID), DisplayName = "Purchase Unit", Visibility = PXUIVisibility.Visible)]
		[PXCheckUnique(typeof(POVendorInventory.vendorID), typeof(POVendorInventory.vendorLocationID), typeof(POVendorInventory.inventoryID), typeof(POVendorInventory.subItemID), IgnoreNulls = false)]
		protected virtual void POVendorInventory_PurchaseUnit_CacheAttached(PXCache sender)
		{

		}

		#endregion

		#region Ctor + Public Members
		public PXSelect<VendorLocation> BAccount;

		#region Standard Actions
		public PXSave<VendorLocation> Save;
		public PXAction<VendorLocation> cancel;
		public PXFirst<VendorLocation> First;
		public PXPrevious<VendorLocation> Prev;
		public PXNext<VendorLocation> Next;
		public PXLast<VendorLocation> Last;		
		#endregion

        [PXImport(typeof(VendorLocation))]
        public POVendorInventorySelect<POVendorInventory,
            LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<POVendorInventory.inventoryID>>,
            LeftJoin<Vendor, On<Vendor.bAccountID, Equal<POVendorInventory.vendorID>>,
            LeftJoin<Location,
                        On<Location.bAccountID, Equal<POVendorInventory.vendorID>,
                        And<Location.locationID, Equal<POVendorInventory.vendorLocationID>>>>>>,
            Where<POVendorInventory.vendorID, Equal<Current<VendorLocation.bAccountID>>,
            And<Where<POVendorInventory.vendorLocationID, Equal<Current<VendorLocation.locationID>>,
            Or<POVendorInventory.vendorLocationID, IsNull>>>>,
            VendorLocation> VendorCatalogue;

		public PXSetup<Company> Company;
		public PXSelect<APSalesPrice> VendorPrice;

		#region Setups
		public PXSetup<POSetup> posetup;
		public PXSetup<INSetup> insetup;
		public CMSetupSelect    cmsetup;
		#endregion

		#region Fields
		private readonly string _inventoryID;
		private readonly string _subItemID;
		private readonly string _uOM;
		private readonly string _recordID;
		#endregion

		#region Ctors
		
		public POVendorCatalogueMaint()
		{			
			this.BAccount.Cache.AllowDelete = false;
			PXUIFieldAttribute.SetVisible<VendorLocation.curyID>(BAccount.Cache, null, (bool)cmsetup.Current.MCActivated);

			_inventoryID = VendorCatalogue.Cache.GetField(typeof(POVendorInventory.inventoryID));
			_subItemID = VendorCatalogue.Cache.GetField(typeof(POVendorInventory.subItemID));
			_uOM = VendorCatalogue.Cache.GetField(typeof(POVendorInventory.purchaseUnit));
			_recordID = VendorCatalogue.Cache.GetField(typeof(POVendorInventory.recordID));
		}

		#endregion		

		#endregion

		#region Vendor Events		
		[PXUIField(DisplayName = ActionsMessages.Cancel, MapEnableRights = PXCacheRights.Select)]
		[PXCancelButton]
		protected virtual IEnumerable Cancel(PXAdapter adapter)
		{
			VendorLocation vendor = this.BAccount.Current;
			if (vendor != null && adapter.Searches.Length == 2 && adapter.Searches[0] != null && vendor.AcctCD != null)
			{
				if (vendor.AcctCD.Trim() != adapter.Searches[0].ToString().Trim())
				{
					PXResult<BAccountR, Location> r =
					(PXResult<BAccountR, Location>)PXSelectJoin<BAccountR,
						InnerJoin<Location, On<Location.locationID, Equal<BAccountR.defLocationID>>>,
					Where<BAccountR.acctCD, Equal<Required<BAccountR.acctCD>>>>
					.SelectWindowed(this, 0, 1, adapter.Searches[0]);
					Location location = r;
					adapter.Searches[1] =	location.LocationCD;
				}
			}

			foreach (VendorLocation i in (new PXCancel<VendorLocation>(this, "Cancel")).Press(adapter))
			{
				yield return i;
			}						
		}
		protected virtual void VendorLocation_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{			
			e.Cancel = true;			
		}
		protected virtual void VendorLocation_CuryID_FieldSelecting(PXCache sedner, PXFieldSelectingEventArgs e)
		{
			if (e.ReturnValue == null)
				e.ReturnValue = this.Company.Current.BaseCuryID;
		}		
		#endregion

		#region POVendorCatalogue Events		

		protected virtual void POVendorInventory_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			POVendorInventory row = (POVendorInventory )e.Row;
			if (row != null && BAccount.Current != null)
			{
				row.CuryID = BAccount.Current.CuryID ?? Company.Current.BaseCuryID;				
			}
		}

		protected virtual void POVendorInventory_AllLocations_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			POVendorInventory row = (POVendorInventory)e.Row;
			if (e.NewValue != null && (bool)e.NewValue == true)
			{
				POVendorInventory parent =
				PXSelect<POVendorInventory,
				Where<POVendorInventory.inventoryID, Equal<Current<POVendorInventory.inventoryID>>,
					And<POVendorInventory.subItemID, Equal<Current<POVendorInventory.subItemID>>,
					And<POVendorInventory.purchaseUnit, Equal<Current<POVendorInventory.purchaseUnit>>,
					And<POVendorInventory.vendorID, Equal<Current<POVendorInventory.vendorID>>,
					And<POVendorInventory.vendorLocationID, IsNull,
					And<POVendorInventory.recordID, NotEqual<Current<POVendorInventory.recordID>>>>>>>>>
					.SelectSingleBound(this, new object[] { e.Row }, null);

				if (parent != null)
				{
					throw new PXSetPropertyException(Messages.POVendorInventoryDuplicate);
				}
			}
		}

		protected virtual void POVendorInventory_AllLocations_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			POVendorInventory row = (POVendorInventory)e.Row;
			if (row.AllLocations == true)
			{
				row.VendorLocationID = null;
			}
			else
			{
				row.VendorLocationID = this.BAccount.Current.LocationID;
			}
		}
		protected virtual void POVendorInventory_PendingPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			POVendorInventory row = (POVendorInventory)e.Row;
			if (row.PendingDate == null)
				sender.SetDefaultExt<POVendorInventory.pendingDate>(row);
		}

		protected virtual void POVendorInventory_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			POVendorInventory row = (POVendorInventory)e.Row;
			if (row != null && row.InventoryID != null && row.SubItemID != null && row.PurchaseUnit != null)
			{
				this.VendorCatalogue.View.RequestRefresh();
			}
		}

		protected virtual void POVendorInventory_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			POVendorInventory row = (POVendorInventory)e.Row;
			if (row != null && row.InventoryID != null && row.SubItemID != null && row.PurchaseUnit != null)
			{
				if (!sender.ObjectsEqual<POVendorInventory.pendingDate>(e.Row, e.OldRow) ||
				!sender.ObjectsEqual<POVendorInventory.pendingPrice>(e.Row, e.OldRow) ||
				!sender.ObjectsEqual<POVendorInventory.effPrice>(e.Row, e.OldRow))
				{
					APSalesPrice sp = getAPSalesPrice(row);

					if (sp != null)
					{
						APSalesPrice copy = PXCache<APSalesPrice>.CreateCopy(sp);
						copy.PendingPrice = row.PendingPrice;
						copy.EffectiveDate = row.PendingDate;
						copy.SalesPrice = row.EffPrice;
						copy.LastDate = row.EffDate;
						copy.LastPrice = row.LastPrice;
						VendorPrice.Update(copy);
					}
				}

				this.VendorCatalogue.View.RequestRefresh();
			}
		}

		protected virtual void POVendorInventory_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			POVendorInventory vendor = e.Row as POVendorInventory;
			APSalesPrice price = getAPSalesPrice(vendor);
			if (price != null)
				VendorPrice.Delete(price);
			this.VendorCatalogue.View.RequestRefresh();
		}

		protected virtual void POVendorInventory_InventoryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = null;
			e.Cancel = true;
		}
		#endregion								

		public override void Persist()
		{
			foreach (POVendorInventory vi in VendorCatalogue.Cache.Inserted)
			{
				InsertAPVendorPrice(vi);
			}
			base.Persist();
		}

		#region private function
		private void InsertAPVendorPrice(POVendorInventory inventory)
		{
			APSalesPrice price = getAPSalesPrice(inventory);
			if (price == null)
			{
				APSalesPrice vp = new APSalesPrice();
				vp.InventoryID = inventory.InventoryID;
				vp.VendorID = inventory.VendorID;
				vp.VendorLocationID = inventory.VendorLocationID;
				vp.UOM = inventory.PurchaseUnit;
				vp.SubItemID = inventory.SubItemID;
				vp.CuryID = inventory.CuryID;
				vp.EffectiveDate = inventory.PendingDate;
				vp.PendingPrice = inventory.PendingPrice;
				vp.LastDate = inventory.EffDate;
				vp.SalesPrice = inventory.EffPrice;
				vp.LastPrice = inventory.LastPrice;
				vp.IsPromotionalPrice = false;
				vp.PendingBreakQty = 0;
				vp.BreakQty = 0;
				vp.LastBreakQty = 0;
				VendorPrice.Insert(vp);
			}
		}

		private APSalesPrice getAPSalesPrice(POVendorInventory inventory)
		{
			return PXSelect<APSalesPrice,
										Where<APSalesPrice.inventoryID, Equal<Required<POVendorInventory.inventoryID>>,
											And<APSalesPrice.vendorID, Equal<Required<POVendorInventory.vendorID>>,
											And<APSalesPrice.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>,
											And<APSalesPrice.uOM, Equal<Required<POVendorInventory.purchaseUnit>>,
											And<APSalesPrice.curyID, Equal<Required<POVendorInventory.curyID>>,
											And<APSalesPrice.subItemID, Equal<Required<POVendorInventory.subItemID>>,
											And<APSalesPrice.isPromotionalPrice, Equal<boolFalse>,
											And<APSalesPrice.pendingBreakQty, Equal<decimal0>,
											And<APSalesPrice.breakQty, Equal<decimal0>>>>>>>>>>>.Select(this, inventory.InventoryID, inventory.VendorID, inventory.VendorLocationID, inventory.PurchaseUnit, inventory.CuryID, inventory.SubItemID);
		}
		#endregion

		#region PXImportAttribute.IPXPrepareItems
		public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
		{
			if (string.Compare(viewName, "VendorCatalogue", true) == 0)
			{
		
				string inventoryCD = (string)values[_inventoryID];

				var r = (PXResult<InventoryItem, INSubItem>)
					PXSelectJoin<InventoryItem, 
					LeftJoin<INSubItem, 
							  On<INSubItem.subItemCD, Equal<Required<INSubItem.subItemCD>>,
				        Or<Where<Required<INSubItem.subItemCD>, IsNull, 
				               And<INSubItem.subItemID, Equal<InventoryItem.defaultSubItemID>>>>>>,
					Where<InventoryItem.inventoryCD, Equal<Required<InventoryItem.inventoryCD>>>>.Select(this, 
					values[_subItemID], values[_subItemID], inventoryCD);

				InventoryItem item = r;
				INSubItem subitem = r;
				if (item != null && item.InventoryID != null)
				{
					string uom = (string) values[_uOM] ?? item.PurchaseUnit;

					if (values[_subItemID] == null)
						values[_subItemID] = subitem.SubItemCD;					
					
					POVendorInventory old_row =
						subitem.SubItemID != null
							? PXSelect<POVendorInventory,
								  Where<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>,
								  And<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>,
								  And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>,
								  And<POVendorInventory.purchaseUnit, Equal<Required<POVendorInventory.purchaseUnit>>,
								  And<Where<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>>>>>>>>
									.SelectWindowed(this, 0, 1, this.BAccount.Current.BAccountID, this.BAccount.Current.LocationID, item.InventoryID, uom, subitem.SubItemID)
							: PXSelect<POVendorInventory,
								  Where<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>,
								  And<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>,
								  And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>,
								  And<POVendorInventory.purchaseUnit, Equal<Required<POVendorInventory.purchaseUnit>>,
								  And<POVendorInventory.subItemID,IsNull>>>>>>
									.SelectWindowed(this, 0, 1, this.BAccount.Current.BAccountID, this.BAccount.Current.LocationID, item.InventoryID, uom);

					if (old_row != null)
					{
						if (values[_uOM] == null)
							values[_uOM] = old_row.PurchaseUnit;

						if (keys.Contains(_recordID))
						{
							keys[_recordID] = old_row.RecordID;
							values[_recordID] = old_row.RecordID;
						}
					}
				}
			}
			return true;
		}

		public bool RowImporting(string viewName, object row)
		{
			return row == null;
		}

		public bool RowImported(string viewName, object row, object oldRow)
		{
			return oldRow == null;
		}

		public virtual void PrepareItems(string viewName, IEnumerable items)
		{
		}
		#endregion
	}
}
