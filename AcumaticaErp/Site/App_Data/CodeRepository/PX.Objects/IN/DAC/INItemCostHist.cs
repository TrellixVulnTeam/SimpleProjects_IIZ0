namespace PX.Objects.IN
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public partial class INItemCostHist : PX.Data.IBqlTable
	{
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[StockItem(IsKey = true)]
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
		#region CostSubItemID
		public abstract class costSubItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostSubItemID;
		[SubItem(IsKey = true)]
		[PXDefault()]
		public virtual Int32? CostSubItemID
		{
			get
			{
				return this._CostSubItemID;
			}
			set
			{
				this._CostSubItemID = value;
			}
		}
		#endregion
		#region CostSiteID
		public abstract class costSiteID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostSiteID;
		[Site(IsKey = true)]
		[PXDefault()]
		public virtual Int32? CostSiteID
		{
			get
			{
				return this._CostSiteID;
			}
			set
			{
				this._CostSiteID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[GL.FinPeriodID(IsKey = true)]
		[PXDefault()]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		#region FinPtdCostIssued
		public abstract class finPtdCostIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostIssued;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? FinPtdCostIssued
		{
			get
			{
				return this._FinPtdCostIssued;
			}
			set
			{
				this._FinPtdCostIssued = value;
			}
		}
		#endregion
		#region FinPtdCostReceived
		public abstract class finPtdCostReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostReceived;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? FinPtdCostReceived
		{
			get
			{
				return this._FinPtdCostReceived;
			}
			set
			{
				this._FinPtdCostReceived = value;
			}
		}
		#endregion
		#region FinBegCost
		public abstract class finBegCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinBegCost;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? FinBegCost
		{
			get
			{
				return this._FinBegCost;
			}
			set
			{
				this._FinBegCost = value;
			}
		}
		#endregion
		#region FinYtdCost
		public abstract class finYtdCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinYtdCost;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		public virtual Decimal? FinYtdCost
		{
			get
			{
				return this._FinYtdCost;
			}
			set
			{
				this._FinYtdCost = value;
			}
		}
		#endregion
		#region FinPtdQtyIssued
		public abstract class finPtdQtyIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyIssued;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyIssued
		{
			get
			{
				return this._FinPtdQtyIssued;
			}
			set
			{
				this._FinPtdQtyIssued = value;
			}
		}
		#endregion
		#region FinPtdQtyReceived
		public abstract class finPtdQtyReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyReceived;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyReceived
		{
			get
			{
				return this._FinPtdQtyReceived;
			}
			set
			{
				this._FinPtdQtyReceived = value;
			}
		}
		#endregion
		#region FinBegQty
		public abstract class finBegQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinBegQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinBegQty
		{
			get
			{
				return this._FinBegQty;
			}
			set
			{
				this._FinBegQty = value;
			}
		}
		#endregion
		#region FinYtdQty
		public abstract class finYtdQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinYtdQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinYtdQty
		{
			get
			{
				return this._FinYtdQty;
			}
			set
			{
				this._FinYtdQty = value;
			}
		}
		#endregion
		#region FinPtdCOGS
		public abstract class finPtdCOGS : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCOGS;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCOGS
		{
			get
			{
				return this._FinPtdCOGS;
			}
			set
			{
				this._FinPtdCOGS = value;
			}
		}
		#endregion
		#region FinPtdCOGSCredits
		public abstract class finPtdCOGSCredits : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCOGSCredits;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCOGSCredits
		{
			get
			{
				return this._FinPtdCOGSCredits;
			}
			set
			{
				this._FinPtdCOGSCredits = value;
			}
		}
		#endregion
		#region FinPtdCOGSDropShips
		public abstract class finPtdCOGSDropShips : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCOGSDropShips;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCOGSDropShips
		{
			get
			{
				return this._FinPtdCOGSDropShips;
			}
			set
			{
				this._FinPtdCOGSDropShips = value;
			}
		}
		#endregion
		#region FinPtdCostTransferIn
		public abstract class finPtdCostTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostTransferIn;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCostTransferIn
		{
			get
			{
				return this._FinPtdCostTransferIn;
			}
			set
			{
				this._FinPtdCostTransferIn = value;
			}
		}
		#endregion
		#region FinPtdCostTransferOut
		public abstract class finPtdCostTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostTransferOut;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCostTransferOut
		{
			get
			{
				return this._FinPtdCostTransferOut;
			}
			set
			{
				this._FinPtdCostTransferOut = value;
			}
		}
		#endregion
		#region FinPtdCostAssemblyIn
		public abstract class finPtdCostAssemblyIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostAssemblyIn;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCostAssemblyIn
		{
			get
			{
				return this._FinPtdCostAssemblyIn;
			}
			set
			{
				this._FinPtdCostAssemblyIn = value;
			}
		}
		#endregion
		#region FinPtdCostAssemblyOut
		public abstract class finPtdCostAssemblyOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostAssemblyOut;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCostAssemblyOut
		{
			get
			{
				return this._FinPtdCostAssemblyOut;
			}
			set
			{
				this._FinPtdCostAssemblyOut = value;
			}
		}
		#endregion
		#region FinPtdCostAdjusted
		public abstract class finPtdCostAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCostAdjusted;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCostAdjusted
		{
			get
			{
				return this._FinPtdCostAdjusted;
			}
			set
			{
				this._FinPtdCostAdjusted = value;
			}
		}
		#endregion
		#region FinPtdQtySales
		public abstract class finPtdQtySales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtySales;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtySales
		{
			get
			{
				return this._FinPtdQtySales;
			}
			set
			{
				this._FinPtdQtySales = value;
			}
		}
		#endregion
		#region FinPtdQtyCreditMemos
		public abstract class finPtdQtyCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyCreditMemos;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyCreditMemos
		{
			get
			{
				return this._FinPtdQtyCreditMemos;
			}
			set
			{
				this._FinPtdQtyCreditMemos = value;
			}
		}
		#endregion
		#region FinPtdQtyDropShipSales
		public abstract class finPtdQtyDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyDropShipSales;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyDropShipSales
		{
			get
			{
				return this._FinPtdQtyDropShipSales;
			}
			set
			{
				this._FinPtdQtyDropShipSales = value;
			}
		}
		#endregion
		#region FinPtdQtyTransferIn
		public abstract class finPtdQtyTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyTransferIn;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyTransferIn
		{
			get
			{
				return this._FinPtdQtyTransferIn;
			}
			set
			{
				this._FinPtdQtyTransferIn = value;
			}
		}
		#endregion
		#region FinPtdQtyTransferOut
		public abstract class finPtdQtyTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyTransferOut;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyTransferOut
		{
			get
			{
				return this._FinPtdQtyTransferOut;
			}
			set
			{
				this._FinPtdQtyTransferOut = value;
			}
		}
		#endregion
		#region FinPtdQtyAssemblyIn
		public abstract class finPtdQtyAssemblyIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyAssemblyIn;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyAssemblyIn
		{
			get
			{
				return this._FinPtdQtyAssemblyIn;
			}
			set
			{
				this._FinPtdQtyAssemblyIn = value;
			}
		}
		#endregion
		#region FinPtdQtyAssemblyOut
		public abstract class finPtdQtyAssemblyOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyAssemblyOut;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyAssemblyOut
		{
			get
			{
				return this._FinPtdQtyAssemblyOut;
			}
			set
			{
				this._FinPtdQtyAssemblyOut = value;
			}
		}
		#endregion
		#region FinPtdQtyAdjusted
		public abstract class finPtdQtyAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyAdjusted;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdQtyAdjusted
		{
			get
			{
				return this._FinPtdQtyAdjusted;
			}
			set
			{
				this._FinPtdQtyAdjusted = value;
			}
		}
		#endregion
		#region FinPtdSales
		public abstract class finPtdSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdSales;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdSales
		{
			get
			{
				return this._FinPtdSales;
			}
			set
			{
				this._FinPtdSales = value;
			}
		}
		#endregion
		#region FinPtdCreditMemos
		public abstract class finPtdCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdCreditMemos;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdCreditMemos
		{
			get
			{
				return this._FinPtdCreditMemos;
			}
			set
			{
				this._FinPtdCreditMemos = value;
			}
		}
		#endregion
		#region FinPtdDropShipSales
		public abstract class finPtdDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdDropShipSales;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? FinPtdDropShipSales
		{
			get
			{
				return this._FinPtdDropShipSales;
			}
			set
			{
				this._FinPtdDropShipSales = value;
			}
		}
		#endregion
		#region TranPtdCostReceived
		public abstract class tranPtdCostReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostReceived;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostReceived
		{
			get
			{
				return this._TranPtdCostReceived;
			}
			set
			{
				this._TranPtdCostReceived = value;
			}
		}
		#endregion
		#region TranPtdCostIssued
		public abstract class tranPtdCostIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostIssued;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostIssued
		{
			get
			{
				return this._TranPtdCostIssued;
			}
			set
			{
				this._TranPtdCostIssued = value;
			}
		}
		#endregion
		#region TranPtdQtyReceived
		public abstract class tranPtdQtyReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyReceived;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyReceived
		{
			get
			{
				return this._TranPtdQtyReceived;
			}
			set
			{
				this._TranPtdQtyReceived = value;
			}
		}
		#endregion
		#region TranPtdQtyIssued
		public abstract class tranPtdQtyIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyIssued;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyIssued
		{
			get
			{
				return this._TranPtdQtyIssued;
			}
			set
			{
				this._TranPtdQtyIssued = value;
			}
		}
		#endregion
		#region TranPtdCOGS
		public abstract class tranPtdCOGS : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCOGS;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCOGS
		{
			get
			{
				return this._TranPtdCOGS;
			}
			set
			{
				this._TranPtdCOGS = value;
			}
		}
		#endregion
		#region TranPtdCOGSCredits
		public abstract class tranPtdCOGSCredits : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCOGSCredits;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCOGSCredits
		{
			get
			{
				return this._TranPtdCOGSCredits;
			}
			set
			{
				this._TranPtdCOGSCredits = value;
			}
		}
		#endregion
		#region TranPtdCOGSDropShips
		public abstract class tranPtdCOGSDropShips : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCOGSDropShips;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCOGSDropShips
		{
			get
			{
				return this._TranPtdCOGSDropShips;
			}
			set
			{
				this._TranPtdCOGSDropShips = value;
			}
		}
		#endregion
		#region TranPtdCostTransferIn
		public abstract class tranPtdCostTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostTransferIn;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostTransferIn
		{
			get
			{
				return this._TranPtdCostTransferIn;
			}
			set
			{
				this._TranPtdCostTransferIn = value;
			}
		}
		#endregion
		#region TranPtdCostTransferOut
		public abstract class tranPtdCostTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostTransferOut;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostTransferOut
		{
			get
			{
				return this._TranPtdCostTransferOut;
			}
			set
			{
				this._TranPtdCostTransferOut = value;
			}
		}
		#endregion
		#region TranPtdCostAssemblyIn
		public abstract class tranPtdCostAssemblyIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostAssemblyIn;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostAssemblyIn
		{
			get
			{
				return this._TranPtdCostAssemblyIn;
			}
			set
			{
				this._TranPtdCostAssemblyIn = value;
			}
		}
		#endregion
		#region TranPtdCostAssemblyOut
		public abstract class tranPtdCostAssemblyOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostAssemblyOut;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostAssemblyOut
		{
			get
			{
				return this._TranPtdCostAssemblyOut;
			}
			set
			{
				this._TranPtdCostAssemblyOut = value;
			}
		}
		#endregion
		#region TranPtdCostAdjusted
		public abstract class tranPtdCostAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCostAdjusted;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCostAdjusted
		{
			get
			{
				return this._TranPtdCostAdjusted;
			}
			set
			{
				this._TranPtdCostAdjusted = value;
			}
		}
		#endregion
		#region TranPtdQtySales
		public abstract class tranPtdQtySales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtySales;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtySales
		{
			get
			{
				return this._TranPtdQtySales;
			}
			set
			{
				this._TranPtdQtySales = value;
			}
		}
		#endregion
		#region TranPtdQtyCreditMemos
		public abstract class tranPtdQtyCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyCreditMemos;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyCreditMemos
		{
			get
			{
				return this._TranPtdQtyCreditMemos;
			}
			set
			{
				this._TranPtdQtyCreditMemos = value;
			}
		}
		#endregion
		#region TranPtdQtyDropShipSales
		public abstract class tranPtdQtyDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyDropShipSales;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyDropShipSales
		{
			get
			{
				return this._TranPtdQtyDropShipSales;
			}
			set
			{
				this._TranPtdQtyDropShipSales = value;
			}
		}
		#endregion
		#region TranPtdQtyTransferIn
		public abstract class tranPtdQtyTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyTransferIn;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyTransferIn
		{
			get
			{
				return this._TranPtdQtyTransferIn;
			}
			set
			{
				this._TranPtdQtyTransferIn = value;
			}
		}
		#endregion
		#region TranPtdQtyTransferOut
		public abstract class tranPtdQtyTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyTransferOut;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyTransferOut
		{
			get
			{
				return this._TranPtdQtyTransferOut;
			}
			set
			{
				this._TranPtdQtyTransferOut = value;
			}
		}
		#endregion
		#region TranPtdQtyAssemblyIn
		public abstract class tranPtdQtyAssemblyIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyAssemblyIn;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyAssemblyIn
		{
			get
			{
				return this._TranPtdQtyAssemblyIn;
			}
			set
			{
				this._TranPtdQtyAssemblyIn = value;
			}
		}
		#endregion
		#region TranPtdQtyAssemblyOut
		public abstract class tranPtdQtyAssemblyOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyAssemblyOut;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyAssemblyOut
		{
			get
			{
				return this._TranPtdQtyAssemblyOut;
			}
			set
			{
				this._TranPtdQtyAssemblyOut = value;
			}
		}
		#endregion
		#region TranPtdQtyAdjusted
		public abstract class tranPtdQtyAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyAdjusted;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdQtyAdjusted
		{
			get
			{
				return this._TranPtdQtyAdjusted;
			}
			set
			{
				this._TranPtdQtyAdjusted = value;
			}
		}
		#endregion
		#region TranPtdSales
		public abstract class tranPtdSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdSales;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdSales
		{
			get
			{
				return this._TranPtdSales;
			}
			set
			{
				this._TranPtdSales = value;
			}
		}
		#endregion
		#region TranPtdCreditMemos
		public abstract class tranPtdCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdCreditMemos;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdCreditMemos
		{
			get
			{
				return this._TranPtdCreditMemos;
			}
			set
			{
				this._TranPtdCreditMemos = value;
			}
		}
		#endregion
		#region TranPtdDropShipSales
		public abstract class tranPtdDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdDropShipSales;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranPtdDropShipSales
		{
			get
			{
				return this._TranPtdDropShipSales;
			}
			set
			{
				this._TranPtdDropShipSales = value;
			}
		}
		#endregion
		#region TranBegCost
		public abstract class tranBegCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranBegCost;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranBegCost
		{
			get
			{
				return this._TranBegCost;
			}
			set
			{
				this._TranBegCost = value;
			}
		}
		#endregion
		#region TranYtdCost
		public abstract class tranYtdCost : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranYtdCost;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranYtdCost
		{
			get
			{
				return this._TranYtdCost;
			}
			set
			{
				this._TranYtdCost = value;
			}
		}
		#endregion
		#region TranBegQty
		public abstract class tranBegQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranBegQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranBegQty
		{
			get
			{
				return this._TranBegQty;
			}
			set
			{
				this._TranBegQty = value;
			}
		}
		#endregion
		#region TranYtdQty
		public abstract class tranYtdQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranYtdQty;
		[PXDBQuantity()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? TranYtdQty
		{
			get
			{
				return this._TranYtdQty;
			}
			set
			{
				this._TranYtdQty = value;
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
	}
}
