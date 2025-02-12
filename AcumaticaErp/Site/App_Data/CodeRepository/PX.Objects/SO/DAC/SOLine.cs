using PX.Objects.CR;

namespace PX.Objects.SO
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PX.Data;
	using PX.Objects.CM;
	using PX.Objects.AR;
	using PX.Objects.IN;
	using PX.Objects.TX;
	using PX.Objects.GL;
	using PX.Objects.CS;
	using PX.Objects.PO;
	using PX.Objects.PM;


	
	[System.SerializableAttribute()]
	[PXCacheName(Messages.SOLine)]
	public partial class SOLine : PX.Data.IBqlTable, ILSPrimary, IDiscountable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(typeof(SOOrder.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region OrderType
		public abstract class orderType : PX.Data.IBqlField
		{
		}
		protected String _OrderType;
		[PXDBString(2, IsKey = true, IsFixed = true)]
		[PXDefault(typeof(SOOrder.orderType))]
		[PXUIField(DisplayName = "Order Type", Visible = false)]
		public virtual String OrderType
		{
			get
			{
				return this._OrderType;
			}
			set
			{
				this._OrderType = value;
			}
		}
		#endregion
		#region OrderNbr
		public abstract class orderNbr : PX.Data.IBqlField
		{
		}
		protected String _OrderNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(SOOrder.orderNbr), DefaultForUpdate = false)]
		[PXParent(typeof(Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOLine.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOLine.orderNbr>>>>>))]
		[PXUIField(DisplayName = "Order Nbr.", Visible = false)]
		public virtual String OrderNbr
		{
			get
			{
				return this._OrderNbr;
			}
			set
			{
				this._OrderNbr = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(SOOrder.lineCntr))]
		[PXUIField(DisplayName = "Line Nbr.", Visible = false)]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region ShipComplete
		public abstract class shipComplete : PX.Data.IBqlField
		{
		}
		protected String _ShipComplete;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(SOShipComplete.CancelRemainder)]
		[SOShipComplete.List()]
		[PXUIField(DisplayName="Ship Complete")]
		public virtual String ShipComplete
		{
			get
			{
				return this._ShipComplete;
			}
			set
			{
				this._ShipComplete = value;
			}
		}
		#endregion
		#region Cancelled
		public abstract class cancelled : PX.Data.IBqlField
		{
		}
		protected Boolean? _Cancelled;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Completed", Enabled = false)]
		public virtual Boolean? Cancelled
		{
			get
			{
				return this._Cancelled;
			}
			set
			{
				this._Cancelled = value;
			}
		}
		#endregion
		#region Completed
		public abstract class completed : PX.Data.IBqlField
		{
		}
		protected Boolean? _Completed;
		[PXDBBool()]
		[DirtyFormula(typeof(Switch<Case<Where<SOLine.requireShipping, Equal<True>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<SOLine.cancelled, NotEqual<True>>>>, False>, True>), typeof(OpenLineCalc<SOOrder.openLineCntr>))]
		[PXUIField(DisplayName = "Completed")]
		public virtual Boolean? Completed
		{
			get
			{
				return this._Completed;
			}
			set
			{
				this._Completed = value;
			}
		}
		#endregion

		#region CustomerID
		public abstract class customerID : PX.Data.IBqlField
		{
		}
		protected Int32? _CustomerID;
		[PXDBInt()]
		[PXDefault(typeof(SOOrder.customerID))]
		public virtual Int32? CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				this._CustomerID = value;
			}
		}
		#endregion
		#region OrderDate
		public abstract class orderDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _OrderDate;
		[PXDBDate()]
		[PXDBDefault(typeof(SOOrder.orderDate))]
		public virtual DateTime? OrderDate
		{
			get
			{
				return this._OrderDate;
			}
			set
			{
				this._OrderDate = value;
			}
		}
		#endregion
		#region CancelDate
		public abstract class cancelDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _CancelDate;
		[PXDBDate()]
		[PXDefault(typeof(SOOrder.cancelDate))]
		[PXUIField(DisplayName = "Cancel By", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? CancelDate
		{
			get
			{
				return this._CancelDate;
			}
			set
			{
				this._CancelDate = value;
			}
		}
		#endregion
		#region RequestDate
		public abstract class requestDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _RequestDate;
		[PXDBDate()]
		[PXDefault(typeof(SOOrder.requestDate))]
		[PXUIField(DisplayName = "Requested On", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? RequestDate
		{
			get
			{
				return this._RequestDate;
			}
			set
			{
				this._RequestDate = value;
			}
		}
		#endregion
		#region ShipDate
		public abstract class shipDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ShipDate;
		[PXDBDate()]
		//[PXDefault(typeof(SOOrder.shipDate))]
		[PXFormula(typeof(DateMinusDaysNotLessThenDate<SOLine.requestDate, IsNull<Selector<Current<SOOrder.customerLocationID>,Location.cLeadTime>, decimal0>, SOLine.orderDate>))]
		[PXUIField(DisplayName = "Ship On", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? ShipDate
		{
			get
			{
				return this._ShipDate;
			}
			set
			{
				this._ShipDate = value;
			}
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : PX.Data.IBqlField
		{
		}
		protected String _InvoiceNbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Invoice Nbr.", Enabled = false, Visibility = PXUIVisibility.Dynamic)]
		public virtual String InvoiceNbr
		{
			get
			{
				return this._InvoiceNbr;
			}
			set
			{
				this._InvoiceNbr = value;
			}
		}
		#endregion
		#region InvoiceDate
		public abstract class invoiceDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _InvoiceDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Original Sale Date")]
		public virtual DateTime? InvoiceDate
		{
			get
			{
				return this._InvoiceDate;
			}
			set
			{
				this._InvoiceDate = value;
			}
		}
		#endregion
		#region InvtMult
		public abstract class invtMult : PX.Data.IBqlField
		{
		}
		protected Int16? _InvtMult;
		[PXDBShort()]
		[PXDefault((short)-1,
			typeof(Select<SOOrderTypeOperation,
			Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>,
			And<SOOrderTypeOperation.operation, Equal<Current<SOLine.operation>>>>>),
			SourceField = typeof(SOOrderTypeOperation.invtMult))]
		public virtual Int16? InvtMult
		{
			get
			{
				return this._InvtMult;
			}
			set
			{
				this._InvtMult = value;
			}
		}
		#endregion
		#region ManualPrice
		public abstract class manualPrice : PX.Data.IBqlField
		{
		}
		protected Boolean? _ManualPrice;
		[PXDBBool()]
		[PXDefault(false)]
		public virtual Boolean? ManualPrice
		{
			get
			{
				return this._ManualPrice;
			}
			set
			{
				this._ManualPrice = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[SOLineInventoryItem(Filterable=true)]
		[PXDefault()]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected String _LineType;
		[PXDBString(2, IsFixed = true)]
		[PXDefault(SOLineType.Inventory)]
		[SOLineType.List()]
		[PXUIField(DisplayName = "Line Type", Visible = false, Enabled = false)]
		[PXFormula(null, typeof(CountCalc<SOOrderSite.lineCntr>))]
		public virtual String LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region SubItemID
		public abstract class subItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubItemID;
		[PXDefault(typeof(Search<InventoryItem.defaultSubItemID,
			Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>,
			And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<SOLine.inventoryID>))]
		[SubItem(typeof(SOLine.inventoryID))]
		[SubItemStatusVeryfier(typeof(SOLine.inventoryID), typeof(SOLine.siteID), InventoryItemStatus.Inactive, InventoryItemStatus.NoSales)]
		public virtual Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBScalar(typeof(Search<SOOrderTypeOperation.iNDocType,
			Where<SOOrderTypeOperation.orderType, Equal<SOLine.orderType>,
			And<SOOrderTypeOperation.operation, Equal<SOLine.operation>>>>))]
		[PXDefault(typeof(Select<SOOrderTypeOperation, 
			Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>,
			And<SOOrderTypeOperation.operation, Equal<Current<SOLine.operation>>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderTypeOperation.iNDocType))]
		[PXString(3, IsFixed = true)]
		public virtual String TranType
		{
			get
			{
				return this._TranType;
			}
			set
			{
				this._TranType = value;
			}
		}
		#endregion
		#region TranDate
		public virtual DateTime? TranDate
		{
			get { return this._OrderDate; }
		}
		#endregion
		#region PlanType
		public abstract class planType : PX.Data.IBqlField
		{
		}
		protected String _PlanType;
		[PXDBScalar(typeof(Search<SOOrderTypeOperation.orderPlanType,
				Where<SOOrderTypeOperation.orderType, Equal<SOLine.orderType>,
				And<SOOrderTypeOperation.operation, Equal<SOLine.operation>>>>))]
		[PXDefault(typeof(Select<SOOrderTypeOperation,
			Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>,
			And<SOOrderTypeOperation.operation, Equal<Current<SOLine.operation>>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderTypeOperation.orderPlanType))]
		public virtual String PlanType
		{
			get
			{
				return this._PlanType;
			}
			set
			{
				this._PlanType = value;
			}
		}
		#endregion
		#region RequireReasonCode
		public abstract class requireReasonCode : PX.Data.IBqlField
		{
		}
		protected Boolean? _RequireReasonCode;
		[PXDBScalar(typeof(Search<SOOrderTypeOperation.requireReasonCode,
				Where<SOOrderTypeOperation.orderType, Equal<SOLine.orderType>,
				And<SOOrderTypeOperation.operation, Equal<SOLine.operation>>>>))]
		[PXDefault(typeof(Select<SOOrderTypeOperation,
			Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>,
			And<SOOrderTypeOperation.operation, Equal<Current<SOLine.operation>>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderTypeOperation.requireReasonCode))]
		public virtual Boolean? RequireReasonCode
		{
			get
			{
				return this._RequireReasonCode;
			}
			set
			{
				this._RequireReasonCode = value;
			}
		}
		#endregion
		#region RequireShipping
		public abstract class requireShipping : PX.Data.IBqlField
		{
		}
		protected bool? _RequireShipping;
		[PXDBScalar(typeof(Search<SOOrderType.requireShipping, Where<SOOrderType.orderType, Equal<SOLine.orderType>>>))]
		[PXDefault(typeof(Select<SOOrderType, Where<SOOrderType.orderType, Equal<Current<SOLine.orderType>>>>), 
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderType.requireShipping))]
		public virtual bool? RequireShipping
		{
			get
			{
				return this._RequireShipping;
			}
			set
			{
				this._RequireShipping = value;
			}
		}
		#endregion
		#region RequireAllocation
		public abstract class requireAllocation : PX.Data.IBqlField
		{
		}
		protected bool? _RequireAllocation;
		[PXDBScalar(typeof(Search<SOOrderType.requireAllocation, Where<SOOrderType.orderType, Equal<SOLine.orderType>>>))]
		[PXDefault(typeof(Select<SOOrderType, Where<SOOrderType.orderType, Equal<Current<SOLine.orderType>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderType.requireAllocation))]
		public virtual bool? RequireAllocation
		{
			get
			{
				return this._RequireAllocation;
			}
			set
			{
				this._RequireAllocation = value;
			}
		}
		#endregion
		#region RequireLocation
		public abstract class requireLocation : PX.Data.IBqlField
		{
		}
		protected bool? _RequireLocation;
		[PXDBScalar(typeof(Search<SOOrderType.requireLocation, Where<SOOrderType.orderType, Equal<SOLine.orderType>>>))]
		[PXDefault(typeof(Select<SOOrderType, Where<SOOrderType.orderType, Equal<Current<SOLine.orderType>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SourceField = typeof(SOOrderType.requireLocation))]
		public virtual bool? RequireLocation
		{
			get
			{
				return this._RequireLocation;
			}
			set
			{
				this._RequireLocation = value;
			}
		}
		#endregion
		#region PlanID
		public abstract class planID : PX.Data.IBqlField
		{
		}
		protected Int64? _PlanID;
		[PXDBLong()]
		[SOLinePlanID(typeof(SOOrder.noteID), typeof(SOOrder.hold), typeof(SOOrder.orderDate))]
		public virtual Int64? PlanID
		{
			get
			{
				return this._PlanID;
			}
			set
			{
				this._PlanID = value;
			}
		}
		#endregion
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField
		{
		}
		protected Int32? _SiteID;
		[SiteAvail(typeof(SOLine.inventoryID), typeof(SOLine.subItemID))]
		[PXParent(typeof(Select<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOLine.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOOrderSite.siteID, Equal<Current2<SOLine.siteID>>>>>>), LeaveChildren = true, ParentCreate = true)]
		[PXDefault()]
		public virtual Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region LocationID
		public abstract class locationID : PX.Data.IBqlField
		{
		}
		protected Int32? _LocationID;
		[SOLocationAvail(typeof(SOLine.inventoryID), typeof(SOLine.subItemID), typeof(SOLine.siteID), typeof(SOLine.tranType), typeof(SOLine.invtMult))]
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
		#region LotSerialNbr
		public abstract class lotSerialNbr : PX.Data.IBqlField
		{
		}
		protected String _LotSerialNbr;
		[INLotSerialNbr(typeof(SOLine.inventoryID), typeof(SOLine.subItemID), typeof(SOLine.locationID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String LotSerialNbr
		{
			get
			{
				return this._LotSerialNbr;
			}
			set
			{
				this._LotSerialNbr = value;
			}
		}
		#endregion
		#region OrigOrderType
		public abstract class origOrderType : PX.Data.IBqlField
		{
		}
		protected String _OrigOrderType;
		[PXDBString(2, IsFixed = true)]
		public virtual String OrigOrderType
		{
			get
			{
				return this._OrigOrderType;
			}
			set
			{
				this._OrigOrderType = value;
			}
		}
		#endregion
		#region OrigOrderNbr
		public abstract class origOrderNbr : PX.Data.IBqlField
		{
		}
		protected String _OrigOrderNbr;
		[PXDBString(15, IsUnicode = true)]
		public virtual String OrigOrderNbr
		{
			get
			{
				return this._OrigOrderNbr;
			}
			set
			{
				this._OrigOrderNbr = value;
			}
		}
		#endregion
		#region OrigLineNbr
		public abstract class origLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _OrigLineNbr;
		[PXDBInt()]
		public virtual Int32? OrigLineNbr
		{
			get
			{
				return this._OrigLineNbr;
			}
			set
			{
				this._OrigLineNbr = value;
			}
		}
		#endregion		
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[INUnit(typeof(SOLine.inventoryID), DisplayName="UOM")]
		[PXDefault(typeof(Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region ClosedQty
		public abstract class closedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ClosedQty;
		[PXDBCalced(typeof(Sub<SOLine.orderQty, SOLine.openQty>), typeof(decimal))]
		[PXQuantity(typeof(SOLine.uOM), typeof(SOLine.baseClosedQty))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? ClosedQty
		{
			get
			{
				return this._ClosedQty;
			}
			set
			{
				this._ClosedQty = value;
			}
		}
		#endregion
		#region BaseClosedQty
		public abstract class baseClosedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseClosedQty;
		[PXDBCalced(typeof(Sub<SOLine.baseOrderQty, SOLine.baseOpenQty>), typeof(decimal))]
		[PXQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseClosedQty
		{
			get
			{
				return this._BaseClosedQty;
			}
			set
			{
				this._BaseClosedQty = value;
			}
		}
		#endregion
		#region OrderQty
		public abstract class orderQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _OrderQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseOrderQty))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Quantity")]
		//[PXFormula(null, typeof(SumCalc<SOOrder.orderQty>))]
	[PXUnboundFormula(typeof(Switch<Case<Where<SOLine.operation, Equal<Parent<SOOrder.defaultOperation>>, 
																					 And<SOLine.lineType, NotEqual<SOLineType.miscCharge>>>, 
																				 SOLine.orderQty>, 
																				 decimal0>),
			typeof(SumCalc<SOOrder.orderQty>))]
		public virtual Decimal? OrderQty
		{
			get
			{
				return this._OrderQty;
			}
			set
			{
				this._OrderQty = value;
			}
		}
		public virtual Decimal? Qty
		{
			get
			{
				return this._OrderQty;
			}
			set
			{
				this._OrderQty = value;
			}
		}
		#endregion
		#region BaseOrderQty
		public abstract class baseOrderQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseOrderQty;
		[PXDBDecimal(6, MinValue=0)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? BaseOrderQty
		{
			get
			{
				return this._BaseOrderQty;
			}
			set
			{
				this._BaseOrderQty = value;
			}
		}
		public virtual Decimal? BaseQty
		{
			get
			{
				return this._BaseOrderQty;
			}
			set
			{
				this._BaseOrderQty = value;
			}
		}
		#endregion
		#region UnassignedQty
		public abstract class unassignedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnassignedQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? UnassignedQty
		{
			get
			{
				return this._UnassignedQty;
			}
			set
			{
				this._UnassignedQty = value;
			}
		}
		#endregion
		#region ShippedQty
		public abstract class shippedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ShippedQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseShippedQty), MinValue=0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. On Shipments", Enabled = false)]
		public virtual Decimal? ShippedQty
		{
			get
			{
				return this._ShippedQty;
			}
			set
			{
				this._ShippedQty = value;
			}
		}
		#endregion
		#region BaseShippedQty
		public abstract class baseShippedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseShippedQty;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? BaseShippedQty
		{
			get
			{
				return this._BaseShippedQty;
			}
			set
			{
				this._BaseShippedQty = value;
			}
		}
		#endregion
		#region OpenQty
		public abstract class openQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseOpenQty), MinValue = 0)]
		[PXFormula(typeof(Switch<Case<Where<SOLine.requireShipping, Equal<True>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<SOLine.cancelled, NotEqual<True>>>>, Sub<SOLine.orderQty, SOLine.closedQty>>, decimal0>), typeof(SumCalc<SOOrder.openOrderQty>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Open Qty.", Enabled = false)]
		public virtual Decimal? OpenQty
		{
			get
			{
				return this._OpenQty;
			}
			set
			{
				this._OpenQty = value;
			}
		}
		#endregion
		#region BaseOpenQty
		public abstract class baseOpenQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseOpenQty;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseOpenQty
		{
			get
			{
				return this._BaseOpenQty;
			}
			set
			{
				this._BaseOpenQty = value;
			}
		}
		#endregion
		#region ReceivedQty
		public abstract class receivedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReceivedQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseReceivedQty), MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Qty. Received", Enabled = false)]
		public virtual Decimal? ReceivedQty
		{
			get
			{
				return this._ReceivedQty;
			}
			set
			{
				this._ReceivedQty = value;
			}
		}
		#endregion
		#region BaseReceivedQty
		public abstract class baseReceivedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseReceivedQty;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseReceivedQty
		{
			get
			{
				return this._BaseReceivedQty;
			}
			set
			{
				this._BaseReceivedQty = value;
			}
		}
		#endregion
		#region BilledQty
		public abstract class billedQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BilledQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseBilledQty), MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Billed Quantity", Enabled = false)]
		public virtual Decimal? BilledQty
		{
			get
			{
				return this._BilledQty;
			}
			set
			{
				this._BilledQty = value;
			}
		}
		#endregion
		#region BaseBilledQty
		public abstract class baseBilledQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseBilledQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseBilledQty
		{
			get
			{
				return this._BaseBilledQty;
			}
			set
			{
				this._BaseBilledQty = value;
			}
		}
		#endregion
		#region UnbilledQty
		public abstract class unbilledQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnbilledQty;
		[PXDBQuantity(typeof(SOLine.uOM), typeof(SOLine.baseUnbilledQty), MinValue = 0)]
		[PXFormula(typeof(Switch<Case<Where<SOLine.cancelled, Equal<True>>, Sub<SOLine.closedQty, SOLine.billedQty>>,Sub<SOLine.orderQty, SOLine.billedQty>>), typeof(SumCalc<SOOrder.unbilledOrderQty>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unbilled Quantity", Enabled = false)]
		public virtual Decimal? UnbilledQty
		{
			get
			{
				return this._UnbilledQty;
			}
			set
			{
				this._UnbilledQty = value;
			}
		}
		#endregion
		#region BaseUnbilledQty
		public abstract class baseUnbilledQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseUnbilledQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseUnbilledQty
		{
			get
			{
				return this._BaseUnbilledQty;
			}
			set
			{
				this._BaseUnbilledQty = value;
			}
		}
		#endregion
		#region CompleteQtyMin
		public abstract class completeQtyMin : PX.Data.IBqlField
		{
		}
		protected Decimal? _CompleteQtyMin;
		[PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
		[PXDefault(TypeCode.Decimal, "100.0")]
		[PXUIField(DisplayName = "Undership Threshold (%)")]
		public virtual Decimal? CompleteQtyMin
		{
			get
			{
				return this._CompleteQtyMin;
			}
			set
			{
				this._CompleteQtyMin = value;
			}
		}
		#endregion
		#region CompleteQtyMax
		public abstract class completeQtyMax : PX.Data.IBqlField
		{
		}
		protected Decimal? _CompleteQtyMax;
		[PXDBDecimal(2, MinValue = 100.0, MaxValue = 999.0)]
		[PXDefault(TypeCode.Decimal, "100.0")]
		[PXUIField(DisplayName = "Overship Threshold (%)")]
		public virtual Decimal? CompleteQtyMax
		{
			get
			{
				return this._CompleteQtyMax;
			}
			set
			{
				this._CompleteQtyMax = value;
			}
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		protected Int64? _CuryInfoID;
		[PXDBLong()]
		[CurrencyInfo(typeof(SOOrder.curyInfoID))]
		public virtual Int64? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region CuryUnitPrice
		public abstract class curyUnitPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryUnitPrice;
		[PXDBCurrency(typeof(Search<INSetup.decPlPrcCst>),typeof(SOLine.curyInfoID), typeof(SOLine.unitPrice), MinValue = 0)]
		[PXUIField(DisplayName = "Unit Price", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryUnitPrice
		{
			get
			{
				return this._CuryUnitPrice;
			}
			set
			{
				this._CuryUnitPrice = value;
			}
		}
		#endregion
		#region UnitPrice
		public abstract class unitPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnitPrice;
		[PXDBPriceCost()]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Unit Price", Enabled = false)]
		public virtual Decimal? UnitPrice
		{
			get
			{
				return this._UnitPrice;
			}
			set
			{
				this._UnitPrice = value;
			}
		}
		#endregion
		#region CuryUnitCost
		public abstract class curyUnitCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryUnitCost;
		[PXDBCurrency(typeof(Search<INSetup.decPlPrcCst>),typeof(SOLine.curyInfoID), typeof(SOLine.unitCost))]
		[PXUIField(DisplayName = "Unit Cost", Visibility = PXUIVisibility.Dynamic)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryUnitCost
		{
			get
			{
				return this._CuryUnitCost;
			}
			set
			{
				this._CuryUnitCost = value;
			}
		}
		#endregion
		#region UnitCost
		public abstract class unitCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnitCost;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0", typeof(Search<INItemSite.tranUnitCost, Where<INItemSite.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemSite.siteID, Equal<Current<SOLine.siteID>>>>>))]
		public virtual Decimal? UnitCost
		{
			get
			{
				return this._UnitCost;
			}
			set
			{
				this._UnitCost = value;
			}
		}
		#endregion
		#region CuryExtPrice
		public abstract class curyExtPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryExtPrice;
		[PXDBCurrency(typeof(Search<INSetup.decPlPrcCst>), typeof(SOLine.curyInfoID), typeof(SOLine.extPrice))]
		[PXUIField(DisplayName = "Extended Price")]
		[PXFormula(typeof(Mult<SOLine.orderQty, SOLine.curyUnitPrice>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryExtPrice
		{
			get
			{
				return this._CuryExtPrice;
			}
			set
			{
				this._CuryExtPrice = value;
			}
		}
		#endregion
		#region ExtPrice
		public abstract class extPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _ExtPrice;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? ExtPrice
		{
			get
			{
				return this._ExtPrice;
			}
			set
			{
				this._ExtPrice = value;
			}
		}
		#endregion
		#region CuryExtCost
		public abstract class curyExtCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryExtCost;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.extCost))]
		[PXUIField(DisplayName = "Extended Cost")]
		[PXFormula(typeof(Mult<SOLine.orderQty, SOLine.curyUnitCost>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryExtCost
		{
			get
			{
				return this._CuryExtCost;
			}
			set
			{
				this._CuryExtCost = value;
			}
		}
		#endregion
		#region ExtCost
		public abstract class extCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _ExtCost;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? ExtCost
		{
			get
			{
				return this._ExtCost;
			}
			set
			{
				this._ExtCost = value;
			}
		}
		#endregion		
		#region TaxCategoryID
		public abstract class taxCategoryID : PX.Data.IBqlField
		{
		}
		protected String _TaxCategoryID;
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
		[SOTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
		[SOOpenTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
		[SOUnbilledTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
		[PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
		[PXDefault(typeof(Search<InventoryItem.taxCategoryID,
			Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>),
			PersistingCheck = PXPersistingCheck.Nothing, SearchOnDefault = false)]
		public virtual String TaxCategoryID
		{
			get
			{
				return this._TaxCategoryID;
			}
			set
			{
				this._TaxCategoryID = value;
			}
		}
		#endregion
		#region AlternateID
		public abstract class alternateID : PX.Data.IBqlField
		{
		}
		protected String _AlternateID;		
		[AlternativeItem(INPrimaryAlternateType.CPN, typeof(SOLine.inventoryID), typeof(SOLine.subItemID))]
		public virtual String AlternateID
		{
			get
			{
				return this._AlternateID;
			}
			set
			{
				this._AlternateID = value;
			}
		}
		#endregion
		#region CommnPct
		public abstract class commnPct : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommnPct;
		[PXDBDecimal(6, MinValue = 0, MaxValue=100)]
		public virtual Decimal? CommnPct
		{
			get
			{
				return this._CommnPct;
			}
			set
			{
				this._CommnPct = value;
			}
		}
		#endregion
		#region CuryCommnAmt
		public abstract class curyCommnAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryCommnAmt;
		[PXDBDecimal(4)]
		public virtual Decimal? CuryCommnAmt
		{
			get
			{
				return this._CuryCommnAmt;
			}
			set
			{
				this._CuryCommnAmt = value;
			}
		}
		#endregion
		#region CommnAmt
		public abstract class commnAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CommnAmt;
		[PXDBDecimal(4)]
		public virtual Decimal? CommnAmt
		{
			get
			{
				return this._CommnAmt;
			}
			set
			{
				this._CommnAmt = value;
			}
		}
		#endregion
		#region TranDesc
		public abstract class tranDesc : PX.Data.IBqlField
		{
		}
		protected String _TranDesc;
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Line Description")]
		[PXDefault(typeof(Search<InventoryItem.descr, Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String TranDesc
		{
			get
			{
				return this._TranDesc;
			}
			set
			{
				this._TranDesc = value;
			}
		}
		#endregion
		#region UnitWeigth
		public abstract class unitWeigth : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnitWeigth;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal,"0.0", typeof(Search<InventoryItem.baseWeight, Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
		public virtual Decimal? UnitWeigth
		{
			get
			{
				return this._UnitWeigth;
			}
			set
			{
				this._UnitWeigth = value;
			}
		}
		#endregion
		#region UnitVolume
		public abstract class unitVolume : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnitVolume;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal, "0.0", typeof(Search<InventoryItem.baseVolume, Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
		public virtual Decimal? UnitVolume
		{
			get
			{
				return this._UnitVolume;
			}
			set
			{
				this._UnitVolume = value;
			}
		}
		#endregion
		#region ExtWeight
		public abstract class extWeight : PX.Data.IBqlField
		{
		}
		protected Decimal? _ExtWeight;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXFormula(typeof(Mult<Row<SOLine.baseOrderQty, SOLine.orderQty>, SOLine.unitWeigth>), typeof(SumCalc<SOOrder.orderWeight>))]
		[PXUIField(DisplayName = "Ext. Weight")]
		public virtual Decimal? ExtWeight
		{
			get
			{
				return this._ExtWeight;
			}
			set
			{
				this._ExtWeight = value;
			}
		}
		#endregion
		#region ExtVolume
		public abstract class extVolume : PX.Data.IBqlField
		{
		}
		protected Decimal? _ExtVolume;
		[PXDBDecimal(6, MinValue = 0)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXFormula(typeof(Mult<Row<SOLine.baseOrderQty, SOLine.orderQty>, SOLine.unitVolume>), typeof(SumCalc<SOOrder.orderVolume>))]
		[PXUIField(DisplayName = "Ext. Volume")]
		public virtual Decimal? ExtVolume
		{
			get
			{
				return this._ExtVolume;
			}
			set
			{
				this._ExtVolume = value;
			}
		}
		#endregion
		#region IsFree
		public abstract class isFree : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsFree;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Free Item")]
		public virtual Boolean? IsFree
		{
			get
			{
				return this._IsFree;
			}
			set
			{
				this._IsFree = value;
			}
		}
		#endregion
		#region DiscPct
		public abstract class discPct : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscPct;
		[PXDBDecimal(6, MinValue = -100, MaxValue=100)]
		[PXUIField(DisplayName = "Discount Percent")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? DiscPct
		{
			get
			{
				return this._DiscPct;
			}
			set
			{
				this._DiscPct = value;
			}
		}
		#endregion
		#region CuryDiscAmt
		public abstract class curyDiscAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryDiscAmt;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.discAmt))]
		[PXUIField(DisplayName = "Discount Amount")]
		//[PXFormula(typeof(Div<Mult<Mult<SOLine.orderQty, SOLine.curyUnitPrice>, SOLine.discPct>, decimal100>))]->Causes SetValueExt for CuryDiscAmt 
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryDiscAmt
		{
			get
			{
				return this._CuryDiscAmt;
			}
			set
			{
				this._CuryDiscAmt = value;
			}
		}
		#endregion
		#region DiscAmt
		public abstract class discAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? DiscAmt
		{
			get
			{
				return this._DiscAmt;
			}
			set
			{
				this._DiscAmt = value;
			}
		}
		#endregion
		#region ManualDisc
		public abstract class manualDisc : PX.Data.IBqlField
		{
		}
		protected Boolean? _ManualDisc;
		[ManualDiscountMode(typeof(SOLine.curyDiscAmt), typeof(SOLine.discPct))]
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Manual Discount", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? ManualDisc
		{
			get
			{
				return this._ManualDisc;
			}
			set
			{
				this._ManualDisc = value;
			}
		}
		#endregion
		#region CuryLineAmt
		public abstract class curyLineAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryLineAmt;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.lineAmt))]
		[PXUIField(DisplayName = "Ext. Price")]
		[PXFormula(typeof(Sub<SOLine.curyExtPrice, SOLine.curyDiscAmt>))]
		[PXFormula(null, typeof(CountCalc<SOSalesPerTran.refCntr>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryLineAmt
		{
			get
			{
				return this._CuryLineAmt;
			}
			set
			{
				this._CuryLineAmt = value;
			}
		}
		#endregion
		#region LineAmt
		public abstract class lineAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _LineAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? LineAmt
		{
			get
			{
				return this._LineAmt;
			}
			set
			{
				this._LineAmt = value;
			}
		}
		#endregion
		#region CuryOpenAmt
		public abstract class curyOpenAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryOpenAmt;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.openAmt))]
		//[PXFormula(typeof(Mult<Mult<SOLine.openQty, SOLine.curyUnitPrice>, Sub<decimal1, Div<SOLine.discPct, decimal100>>>))]
		[PXFormula(typeof(Sub<Mult<SOLine.openQty, SOLine.curyUnitPrice>, Round<Div<Mult<Mult<SOLine.openQty, SOLine.curyUnitPrice>, SOLine.discPct>, decimal100>, SOLine.curyInfoID>>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryOpenAmt
		{
			get
			{
				return this._CuryOpenAmt;
			}
			set
			{
				this._CuryOpenAmt = value;
			}
		}
		#endregion
		#region OpenAmt
		public abstract class openAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? OpenAmt
		{
			get
			{
				return this._OpenAmt;
			}
			set
			{
				this._OpenAmt = value;
			}
		}
		#endregion
		#region CuryBilledAmt
		public abstract class curyBilledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryBilledAmt;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.billedAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryBilledAmt
		{
			get
			{
				return this._CuryBilledAmt;
			}
			set
			{
				this._CuryBilledAmt = value;
			}
		}
		#endregion
		#region BilledAmt
		public abstract class billedAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _BilledAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BilledAmt
		{
			get
			{
				return this._BilledAmt;
			}
			set
			{
				this._BilledAmt = value;
			}
		}
		#endregion
		#region CuryUnbilledAmt
		public abstract class curyUnbilledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryUnbilledAmt;
		[PXDBCurrency(typeof(SOLine.curyInfoID), typeof(SOLine.unbilledAmt))]
		//[PXFormula(typeof(Mult<Mult<SOLine.unbilledQty, SOLine.curyUnitPrice>, Sub<decimal1, Div<SOLine.discPct, decimal100>>>))]
		[PXFormula(typeof(Sub<Mult<SOLine.unbilledQty, SOLine.curyUnitPrice>, Round<Div<Mult<Mult<SOLine.unbilledQty, SOLine.curyUnitPrice>, SOLine.discPct>, decimal100>, SOLine.curyInfoID>>))]
		[PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryUnbilledAmt
		{
			get
			{
				return this._CuryUnbilledAmt;
			}
			set
			{
				this._CuryUnbilledAmt = value;
			}
		}
		#endregion
		#region UnbilledAmt
		public abstract class unbilledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnbilledAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? UnbilledAmt
		{
			get
			{
				return this._UnbilledAmt;
			}
			set
			{
				this._UnbilledAmt = value;
			}
		}
		#endregion
		#region CuryDiscPrice
		public abstract class curyDiscPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryDiscPrice;
		[PXDBPriceCostCalced(typeof(Switch<Case<Where<SOLine.orderQty, Equal<decimal0>>, decimal0>, Div<Sub<SOLine.curyExtPrice, SOLine.curyDiscAmt>, SOLine.orderQty>>), typeof(Decimal))]
		[PXFormula(typeof(Div<Sub<SOLine.curyExtPrice, SOLine.curyDiscAmt>, NullIf<SOLine.orderQty, decimal0>>))]
		[PXCurrency(typeof(Search<INSetup.decPlPrcCst>),typeof(SOLine.curyInfoID), typeof(SOLine.discPrice))]
		[PXUIField(DisplayName = "Disc. Unit Price", Enabled = false, Visible = false)]
		public virtual Decimal? CuryDiscPrice
		{
			get
			{
				return this._CuryDiscPrice;
			}
			set
			{
				this._CuryDiscPrice = value;
			}
		}
		#endregion
        #region GroupDiscountRate
        public abstract class groupDiscountRate : PX.Data.IBqlField
        {
        }
        protected Decimal? _GroupDiscountRate;
        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "1.0")]
        public virtual Decimal? GroupDiscountRate
        {
            get
            {
                return this._GroupDiscountRate;
            }
            set
            {
                this._GroupDiscountRate = value;
            }
        }
        #endregion
		#region DiscPrice
		public abstract class discPrice : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscPrice;
		[PXDBPriceCostCalced(typeof(Switch<Case<Where<SOLine.orderQty, Equal<decimal0>>, decimal0>, Div<SOLine.lineAmt, SOLine.orderQty>>), typeof(Decimal))]
		[PXFormula(typeof(Div<Row<SOLine.lineAmt, SOLine.curyLineAmt>, NullIf<SOLine.orderQty, decimal0>>))]
		public virtual Decimal? DiscPrice
		{
			get
			{
				return this._DiscPrice;
			}
			set
			{
				this._DiscPrice = value;
			}
		}
		#endregion
		#region QtyOnHand
		public abstract class qtyOnHand : PX.Data.IBqlField
		{
		}
		protected Decimal? _QtyOnHand;
		[PXDBScalar(typeof(Search<INItemStats.qtyOnHand, Where<INItemStats.inventoryID, Equal<SOLine.inventoryID>, And<INItemStats.siteID, Equal<SOLine.siteID>>>>))]
		[PXDefault(TypeCode.Decimal, "0.0", typeof(Search<INItemStats.qtyOnHand, Where<INItemStats.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemStats.siteID, Equal<Current<SOLine.siteID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<SOLine.inventoryID, SOLine.siteID>))]
		public virtual Decimal? QtyOnHand
		{
			get
			{
				return this._QtyOnHand;
			}
			set
			{
				this._QtyOnHand = value;
			}
		}
		#endregion
		#region TotalCost
		public abstract class totalCost : PX.Data.IBqlField
		{
		}
		protected decimal? _TotalCost;
		[PXDBScalar(typeof(Search<INItemStats.totalCost, Where<INItemStats.inventoryID, Equal<SOLine.inventoryID>, And<INItemStats.siteID, Equal<SOLine.siteID>>>>))]
		[PXDefault(TypeCode.Decimal, "0.0", typeof(Search<INItemStats.totalCost, Where<INItemStats.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemStats.siteID, Equal<Current<SOLine.siteID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<SOLine.inventoryID, SOLine.siteID>))]
		public virtual Decimal? TotalCost
		{
			get
			{
				return this._TotalCost;
			}
			set
			{
				this._TotalCost = value;
			}
		}
		#endregion
		#region AvgCost
		public abstract class avgCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _AvgCost;
		[PXPriceCost()]
		[PXUIField(DisplayName = "Average Cost", Enabled = false, Visible = false)]
		[PXDBCalced(typeof(Switch<Case<Where<SOLine.qtyOnHand, Equal<decimal0>>, decimal0>, Div<SOLine.totalCost, SOLine.qtyOnHand>>), typeof(Decimal))]
		[PXFormula(typeof(Div<SOLine.totalCost, NullIf<SOLine.qtyOnHand, decimal0>>))]
		public virtual Decimal? AvgCost
		{
			get
			{
				return this._AvgCost;
			}
			set
			{
				this._AvgCost = value;
			}
		}
		public class PXDBCalcedAttribute : PX.Data.PXDBCalcedAttribute
		{
			public PXDBCalcedAttribute(Type operand, Type type)
				: base(operand, type)
			{
			}

			public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
			{
				if ((e.Operation & PXDBOperation.Command) == PXDBOperation.Select)
				{
					if (((e.Operation & PXDBOperation.Option) == PXDBOperation.Normal ||
					(e.Operation & PXDBOperation.Option) == PXDBOperation.Internal))
					{
						base.CommandPreparing(sender, e);
					}
					else if ((e.Operation & PXDBOperation.Option) == PXDBOperation.GroupBy)
					{
						e.FieldName = BqlCommand.Null;
					}
				}
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXDBInt()]
		[PXDefault(typeof(SOOrder.projectID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ReasonCode
		public abstract class reasonCode : PX.Data.IBqlField
		{
		}
		protected String _ReasonCode;
		[PXDBString(CS.ReasonCode.reasonCodeID.Length, IsUnicode = true)]
		[PXSelector(typeof(Search<ReasonCode.reasonCodeID,
			Where<ReasonCode.usage, Equal<ReasonCodeUsages.sales>, Or<ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>>), DescriptionField = typeof(ReasonCode.descr))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName="Reason Code")]
		public virtual String ReasonCode
		{
			get
			{
				return this._ReasonCode;
			}
			set
			{
				this._ReasonCode = value;
			}
		}
		#endregion
		#region SalesPersonID
		public abstract class salesPersonID : PX.Data.IBqlField
		{
		}
		protected Int32? _SalesPersonID;
		[SalesPerson()]
		[PXParent(typeof(Select<SOSalesPerTran, Where<SOSalesPerTran.orderType, Equal<Current<SOLine.orderType>>, And<SOSalesPerTran.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOSalesPerTran.salespersonID, Equal<Current2<SOLine.salesPersonID>>>>>>), LeaveChildren = true, ParentCreate = true)]
		[PXDefault(typeof(SOOrder.salesPersonID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? SalesPersonID
		{
			get
			{
				return this._SalesPersonID;
			}
			set
			{
				this._SalesPersonID = value;
			}
		}
		#endregion
		#region SalesAcctID
		public abstract class salesAcctID : PX.Data.IBqlField
		{
		}
		protected Int32? _SalesAcctID;
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[Account(typeof(SOLine.branchID),Visible = false)]
		public virtual Int32? SalesAcctID
		{
			get
			{
				return this._SalesAcctID;
			}
			set
			{
				this._SalesAcctID = value;
			}
		}
		#endregion
		#region SalesSubID
		public abstract class salesSubID : PX.Data.IBqlField
		{
		}
		protected Int32? _SalesSubID;
		[PXFormula(typeof(Default<SOLine.branchID>))]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[SubAccount(typeof(SOLine.salesAcctID), typeof(SOLine.branchID), Visible = false)]
		public virtual Int32? SalesSubID
		{
			get
			{
				return this._SalesSubID;
			}
			set
			{
				this._SalesSubID = value;
			}
		}
		#endregion
		#region TaskID
		public abstract class taskID : PX.Data.IBqlField
		{
		}
		protected Int32? _TaskID;
		[PXDefault(typeof(Search<PMAccountTask.taskID, Where<PMAccountTask.projectID, Equal<Current<SOLine.projectID>>, And<PMAccountTask.accountID, Equal<Current<SOLine.salesAcctID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[ActiveProjectTask(typeof(SOLine.projectID), BatchModule.SO, DisplayName = "Project Task")]
		public virtual Int32? TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				this._TaskID = value;
			}
		}
		#endregion
		#region OrigPOType
		public abstract class origPOType : PX.Data.IBqlField
		{
		}
		protected String _OrigPOType;
		[PXDBString(2, IsFixed = true)]
		[POOrderType.List()]		
		public virtual String OrigPOType
		{
			get
			{
				return this._OrigPOType;
			}
			set
			{
				this._OrigPOType = value;
			}
		}
		#endregion
		#region OrigPONbr
		public abstract class origPONbr : PX.Data.IBqlField
		{
		}
		protected String _OrigPoNbr;
		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]				
		public virtual String OrigPONbr
		{
			get
			{
				return this._OrigPoNbr;
			}
			set
			{
				this._OrigPoNbr = value;
			}
		}
		#endregion
		#region OrigPOLineNbr
		public abstract class origPOLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _OrigPOLineNbr;
		[PXDBInt()]				
		public virtual Int32? OrigPOLineNbr
		{
			get
			{
				return this._OrigPOLineNbr;
			}
			set
			{
				this._OrigPOLineNbr = value;
			}
		}
		#endregion
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Int64? _NoteID;
		[PXNote()]
		public virtual Int64? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
			}
		}
		#endregion
		#region Commissionable
		public abstract class commissionable : IBqlField
		{
		}
		protected bool? _Commissionable;
		[PXDBBool()]
		[PXFormula(typeof(Switch<Case<Where<SOLine.inventoryID, IsNotNull>, Selector<SOLine.inventoryID, InventoryItem.commisionable>>, True>))]
		[PXUIField(DisplayName = "Commissionable")]
		public bool? Commissionable
		{
			get
			{
				return _Commissionable;
			}
			set
			{
				_Commissionable = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region Operation
		public abstract class operation : PX.Data.IBqlField
		{
		}
		protected String _Operation;
		[PXDBString(1, IsFixed = true, InputMask = ">a")]
		[PXUIField(DisplayName = "Operation", Visibility = PXUIVisibility.Dynamic)]
		[PXDefault(typeof(SOOrderType.defaultOperation))]
		[SOOperation.List]
		public virtual String Operation
		{
			get
			{
				return this._Operation;
			}
			set
			{
				this._Operation = value;
			}
		}
		#endregion
		#region AutoCreateIssueLine
		public abstract class autoCreateIssueLine : PX.Data.IBqlField
		{
		}
		protected bool? _AutoCreateIssueLine;
		[PXDBBool()]
		[PXDefault(false, 
			typeof(Select<SOOrderTypeOperation,
			Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>,
			And<SOOrderTypeOperation.operation, Equal<Current<SOLine.operation>>>>>),
			SourceField = typeof(SOOrderTypeOperation.autoCreateIssueLine))]
		[PXUIField(DisplayName = "Auto Create Issue", Visibility = PXUIVisibility.Dynamic)]
		public virtual bool? AutoCreateIssueLine
		{
			get
			{
				return this._AutoCreateIssueLine;
			}
			set
			{
				this._AutoCreateIssueLine = value;
			}
		}
		#endregion
		#region ExpireDate
		public abstract class expireDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ExpireDate;
		[INExpireDate(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual DateTime? ExpireDate
		{
			get
			{
				return this._ExpireDate;
			}
			set
			{
				this._ExpireDate = value;
			}
		}
		#endregion

        #region DiscountID
        public abstract class discountID : PX.Data.IBqlField
        {
        }
        protected String _DiscountID;
        [PXDBString(10, IsUnicode = true)]
        [PXSelector(typeof(Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
        [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
        public virtual String DiscountID
        {
            get
            {
                return this._DiscountID;
            }
            set
            {
                this._DiscountID = value;
            }
        }
        #endregion
        #region DiscountSequenceID
        public abstract class discountSequenceID : PX.Data.IBqlField
        {
        }
        protected String _DiscountSequenceID;
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Discount Sequence", Visible = true)]
        public virtual String DiscountSequenceID
        {
            get
            {
                return this._DiscountSequenceID;
            }
            set
            {
                this._DiscountSequenceID = value;
            }
        }
        #endregion

		#region DetDiscIDC1
		public abstract class detDiscIDC1 : PX.Data.IBqlField
		{
		}
		protected String _DetDiscIDC1;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DetDiscIDC1
		{
			get
			{
				return this._DetDiscIDC1;
			}
			set
			{
				this._DetDiscIDC1 = value;
			}
		}
		#endregion
		#region DetDiscSeqIDC1
		public abstract class detDiscSeqIDC1 : PX.Data.IBqlField
		{
		}
		protected String _DetDiscSeqIDC1;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DetDiscSeqIDC1
		{
			get
			{
				return this._DetDiscSeqIDC1;
			}
			set
			{
				this._DetDiscSeqIDC1 = value;
			}
		}
		#endregion
		#region DetDiscIDC2
		public abstract class detDiscIDC2 : PX.Data.IBqlField
		{
		}
		protected String _DetDiscIDC2;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DetDiscIDC2
		{
			get
			{
				return this._DetDiscIDC2;
			}
			set
			{
				this._DetDiscIDC2 = value;
			}
		}
		#endregion
		#region DetDiscSeqIDC2
		public abstract class detDiscSeqIDC2 : PX.Data.IBqlField
		{
		}
		protected String _DetDiscSeqIDC2;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DetDiscSeqIDC2
		{
			get
			{
				return this._DetDiscSeqIDC2;
			}
			set
			{
				this._DetDiscSeqIDC2 = value;
			}
		}
		#endregion
		#region DetDiscApp
		public abstract class detDiscApp : PX.Data.IBqlField
		{
		}
		protected Boolean? _DetDiscApp;
		[PXDBBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Boolean? DetDiscApp
		{
			get
			{
				return this._DetDiscApp;
			}
			set
			{
				this._DetDiscApp = value;
			}
		}
		#endregion
		#region PromoDiscID
		public abstract class promoDiscID : PX.Data.IBqlField
		{
		}
		protected String _PromoDiscID;
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Discount Code")]
		[PromoDiscIDSelector(typeof(Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
		public virtual String PromoDiscID
		{
			get
			{
				return this._PromoDiscID;
			}
			set
			{
				this._PromoDiscID = value;
			}
		}
		public class PromoDiscIDSelectorAttribute : PXCustomSelectorAttribute
		{
			protected BqlCommand _select;
			public PromoDiscIDSelectorAttribute(Type type)
				: base(type)
			{
				this._ViewName = "_SODiscount_LinePromo_";
			}

			public override void CacheAttached(PXCache sender)
			{
				base.CacheAttached(sender);

				_select = BqlCommand.CreateInstance(typeof(Select5<ARDiscount,
					InnerJoin<DiscountSequence, On<ARDiscount.discountID, Equal<DiscountSequence.discountID>>,
					LeftJoin<DiscountCustomer, On<DiscountCustomer.discountID, Equal<DiscountSequence.discountID>, And<DiscountCustomer.discountSequenceID, Equal<DiscountSequence.discountSequenceID>>>,
					LeftJoin<DiscountItem, On<DiscountItem.discountID, Equal<DiscountSequence.discountID>, And<DiscountItem.discountSequenceID, Equal<DiscountSequence.discountSequenceID>>>,
					LeftJoin<DiscountCustomerPriceClass, On<DiscountCustomerPriceClass.discountID, Equal<DiscountSequence.discountID>, And<DiscountCustomerPriceClass.discountSequenceID, Equal<DiscountSequence.discountSequenceID>>>,
					LeftJoin<DiscountInventoryPriceClass, On<DiscountInventoryPriceClass.discountID, Equal<DiscountSequence.discountID>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<DiscountSequence.discountSequenceID>>>>>>>>,
					Where2<
						Where<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customer>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerAndInventory>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerAndInventoryPrice>,Or<DiscountCustomer.customerID, Equal<Current<SOLine.customerID>>>>>>,
						And2<Where<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.inventory>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerAndInventory>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerPriceAndInventory>, Or<DiscountItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>>>,
						And2<Where<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerAndInventoryPrice>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerPriceAndInventoryPrice>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.inventoryPrice>, Or<DiscountInventoryPriceClass.inventoryPriceClassID, Equal<Current<InventoryItem.priceClassID>>>>>>,
						And2<Where<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerPrice>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerPriceAndInventory>, And<ARDiscount.applicableTo, NotEqual<ARDiscount.applicableTo.customerPriceAndInventoryPrice>, Or<DiscountCustomerPriceClass.customerPriceClassID, Equal<Current<CR.Location.cPriceClassID>>>>>>,
						And<DiscountSequence.isActive, Equal<True>,
						And<ARDiscount.type, Equal<DiscountType.LineDiscount>,
						And<Where<DiscountSequence.isPromotion, Equal<False>, Or<Current<SOOrder.orderDate>, Between<DiscountSequence.startDate, DiscountSequence.endDate>>>>>>>>>>,
					Aggregate<GroupBy<ARDiscount.discountID>>>));
			}

			public virtual IEnumerable GetRecords()
			{
				InventoryItem item = PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>.Select(_Graph);

				PXView view = _Graph.TypedViews.GetView(_select, true);
				return view.SelectMultiBound(new object[] { item });
			}

			public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
			{
				if (!string.IsNullOrEmpty((string)e.NewValue))
				{
					if (PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>, And<ARDiscount.type, Equal<DiscountType.LineDiscount>>>>.Select(sender.Graph, e.NewValue).Count == 0)
					{
						throw new PXSetPropertyException(ErrorMessages.ElementDoesntExist, e.NewValue);
					}
				}
			}
		}
		#endregion
		#region DocDiscIDC1
		public abstract class docDiscIDC1 : PX.Data.IBqlField
		{
		}
		protected String _DocDiscIDC1;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DocDiscIDC1
		{
			get
			{
				return this._DocDiscIDC1;
			}
			set
			{
				this._DocDiscIDC1 = value;
			}
		}
		#endregion
		#region DocDiscSeqIDC1
		public abstract class docDiscSeqIDC1 : PX.Data.IBqlField
		{
		}
		protected String _DocDiscSeqIDC1;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DocDiscSeqIDC1
		{
			get
			{
				return this._DocDiscSeqIDC1;
			}
			set
			{
				this._DocDiscSeqIDC1 = value;
			}
		}
		#endregion
		#region DocDiscIDC2
		public abstract class docDiscIDC2 : PX.Data.IBqlField
		{
		}
		protected String _DocDiscIDC2;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DocDiscIDC2
		{
			get
			{
				return this._DocDiscIDC2;
			}
			set
			{
				this._DocDiscIDC2 = value;
			}
		}
		#endregion
		#region DocDiscSeqIDC2
		public abstract class docDiscSeqIDC2 : PX.Data.IBqlField
		{
		}
		protected String _DocDiscSeqIDC2;
		[PXDBString(10, IsUnicode = true)]
		public virtual String DocDiscSeqIDC2
		{
			get
			{
				return this._DocDiscSeqIDC2;
			}
			set
			{
				this._DocDiscSeqIDC2 = value;
			}
		}
		#endregion
		#region POCreate
		public abstract class pOCreate : PX.Data.IBqlField
		{
		}
		protected Boolean? _POCreate;
		[PXDBBool()]
		[PXDefault(false, 
			typeof(Search<INItemSiteSettings.pOCreate,
							Where<INItemSiteSettings.siteID, Equal<Current<SOLine.siteID>>,
								 And<INItemSiteSettings.inventoryID, Equal<Current<SOLine.inventoryID>>>>>))]
		[PXUIField(DisplayName = "Mark for PO")]
		public virtual Boolean? POCreate
		{
			get
			{
				return this._POCreate;
			}
			set
			{
				this._POCreate = value ?? false;
			}
		}
		#endregion
		#region POSource
		public abstract class pOSource : PX.Data.IBqlField
		{
		}
		protected string _POSource;
		[PXDBString()]
		[PXDefault(INReplenishmentSource.PurchaseToOrder, 
			typeof(Search<INItemSiteSettings.pOSource,
							Where<INItemSiteSettings.siteID, Equal<Current<SOLine.siteID>>,
								 And<INItemSiteSettings.inventoryID, Equal<Current<SOLine.inventoryID>>, 
								 And<Where<INItemSiteSettings.pOSource, Equal<INReplenishmentSource.purchaseToOrder>, 
									Or<INItemSiteSettings.pOSource, Equal<INReplenishmentSource.dropShip>, 
									Or<INItemSiteSettings.pOSource, Equal<INReplenishmentSource.transferToOrder>>>>>>>>),
							PersistingCheck = PXPersistingCheck.Nothing)]
		[INReplenishmentSource.SOList]
		[PXUIField(DisplayName = "PO Source")]
		public virtual string POSource
		{
			get
			{
				return this._POSource;
			}
			set
			{
				this._POSource = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[AP.Vendor(typeof(Search<BAccountR.bAccountID,
			Where<Current<SOLine.pOSource>, NotEqual<INReplenishmentSource.transferToOrder>, And<AP.Vendor.type, NotEqual<BAccountType.employeeType>,
				Or<Current<SOLine.pOSource>, Equal<INReplenishmentSource.transferToOrder>, And<BAccountR.type, Equal<BAccountType.companyType>>>>>>))]
		[PXRestrictor(typeof(Where<AP.Vendor.status, IsNull,
								Or<AP.Vendor.status, Equal<BAccount.status.active>,
								Or<AP.Vendor.status, Equal<BAccount.status.oneTime>>>>), AP.Messages.VendorIsInStatus, typeof(AP.Vendor.status))]

		[PXDefault(typeof(Search2<BAccountR.bAccountID,
			InnerJoin<INItemSiteSettings, On<INItemSiteSettings.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<SOLine.siteID>>>>,
			LeftJoin<INSite, On<INSite.siteID, Equal<INItemSiteSettings.replenishmentSourceSiteID>>,
			LeftJoin<Branch, On<Branch.branchID, Equal<INSite.branchID>>>>>,
			Where<INItemSiteSettings.preferredVendorID, Equal<BAccountR.bAccountID>, And<Current<SOLine.pOSource>, NotEqual<INReplenishmentSource.transferToOrder>, 
			Or<Branch.bAccountID, Equal<BAccountR.bAccountID>, And<Current<SOLine.pOSource>, Equal<INReplenishmentSource.transferToOrder>>>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<SOLine.siteID>))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region POType
		public abstract class pOType : PX.Data.IBqlField
		{
		}
		protected String _POType;
		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "PO Type", Enabled = false)]
		[POOrderType.RBDList]
		public virtual String POType
		{
			get
			{
				return this._POType;
			}
			set
			{
				this._POType = value;
			}
		}
		#endregion
		#region PONbr
		public abstract class pONbr : PX.Data.IBqlField
		{
		}
		protected String _PONbr;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<SOLine.pOType>>>>), DescriptionField = typeof(POOrder.orderDesc))]
		public virtual String PONbr
		{
			get
			{
				return this._PONbr;
			}
			set
			{
				this._PONbr = value;
			}
		}
		#endregion
		#region POLineNbr
		public abstract class pOLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _POLineNbr;
		[PXDBInt()]
		[PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
		public virtual Int32? POLineNbr
		{
			get
			{
				return this._POLineNbr;
			}
			set
			{
				this._POLineNbr = value;
			}
		}
		#endregion

		#region Methods
		public static implicit operator SOLineSplit(SOLine item)
		{
			SOLineSplit ret = new SOLineSplit();
			ret.OrderType = item.OrderType;
			ret.OrderNbr = item.OrderNbr;
			ret.LineNbr = item.LineNbr;
			ret.Operation = item.Operation;
			ret.SplitLineNbr = 1;
			ret.InventoryID = item.InventoryID;
			ret.SiteID = item.SiteID;
			ret.SubItemID = item.SubItemID;
			ret.LocationID = item.LocationID;
			ret.LotSerialNbr = item.LotSerialNbr;
			ret.ExpireDate = item.ExpireDate;
			ret.Qty = item.Qty;
			ret.UOM = item.UOM;
			ret.OrderDate = item.OrderDate;
			ret.BaseQty = item.BaseQty;
			ret.InvtMult = item.InvtMult;
			ret.PlanType = item.PlanType;
			//check for ordered qty not to get problems in LSSelect_Detail_RowInserting which will retain Released = true flag while merging LSDetail
			ret.Released = (item.RequireShipping == true && item.OrderQty > 0m && item.OpenQty == 0m || item.Cancelled == true);
			ret.ShipDate = item.ShipDate;
			ret.RequireAllocation = item.RequireAllocation;
			ret.RequireLocation = item.RequireLocation;
			ret.RequireShipping = item.RequireShipping;

			return ret;
		}
		public static implicit operator SOLine(SOLineSplit item)
		{
			SOLine ret = new SOLine();
			ret.OrderType = item.OrderType;
			ret.OrderNbr = item.OrderNbr;
			ret.LineNbr = item.LineNbr;
			ret.Operation = item.Operation;
			ret.InventoryID = item.InventoryID;
			ret.SiteID = item.SiteID;
			ret.SubItemID = item.SubItemID;
			ret.LocationID = item.LocationID;
			ret.LotSerialNbr = item.LotSerialNbr;
			ret.Qty = item.Qty;
			ret.OpenQty = item.Qty;
			ret.BaseOpenQty = item.BaseQty;
			ret.UOM = item.UOM;
			ret.OrderDate = item.OrderDate;
			ret.BaseQty = item.BaseQty;
			ret.InvtMult = item.InvtMult;
			ret.PlanType = item.PlanType;
			ret.ShipDate = item.ShipDate;
			return ret;
		}
		#endregion
	}

    [Serializable]
	public partial class SOMemoLine : SOLine
	{
		public new abstract class origOrderType : PX.Data.IBqlField
		{
		}
		public new abstract class origOrderNbr : PX.Data.IBqlField
		{
		}
		public new abstract class origLineNbr : PX.Data.IBqlField
		{
		}
		public new abstract class cancelled : PX.Data.IBqlField
		{ 
		}
		public new abstract class invoiceNbr : PX.Data.IBqlField
		{
		}
	}
		
	public class SOLineType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new string[] { Inventory, NonInventory, MiscCharge  },
					new string[] { Messages.Inventory, Messages.NonInventory, Messages.MiscCharge }) { ; }
		}

		public const string Inventory = "GI";
		public const string NonInventory = "GN";
		public const string MiscCharge = "MI";
		public const string Freight = "FR";
		public const string Discount = "DS";
		public const string Reallocation = "RA";

		public class inventory : Constant<string>
		{
			public inventory() : base(Inventory) { ;}
		}

		public class nonInventory : Constant<string>
		{
			public nonInventory() : base(NonInventory) { ;}
		}

		public class miscCharge : Constant<string>
		{
			public miscCharge() : base(MiscCharge) {;}
		}

		public class freight : Constant<string>
		{
			public freight() : base(Freight) { ;}
		}

		public class discount : Constant<string>
		{
			public discount() : base(Discount) { ; }
		}

		public class reallocation : Constant<string>
		{
			public reallocation() : base(Reallocation) { ; }
		}
	}

	public class SOShipComplete
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
					new string[] { ShipComplete, BackOrderAllowed, CancelRemainder },
					new string[] { Messages.ShipComplete, Messages.BackOrderAllowed, Messages.CancelRemainder }) { ; }
		}

		public const string ShipComplete = "C";
		public const string BackOrderAllowed = "B";
		public const string CancelRemainder = "L";

		public class shipComplete : Constant<string>
		{
			public shipComplete() : base(ShipComplete) { ;}
		}

		public class backOrderAllowed : Constant<string>
		{
			public backOrderAllowed() : base(BackOrderAllowed) { ;}
		}

		public class cancelRemainder : Constant<string>
		{
			public cancelRemainder() : base(CancelRemainder) { ;}
		}
	}
}
