namespace PX.Objects.IN
{
	using System;
	using PX.Data;

	[System.SerializableAttribute()]	
    [PXCacheName(Messages.ItemReplenishmentSettings)]
	public partial class INItemRep : PX.Data.IBqlTable
	{
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", Visible = false)]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<INItemRep.inventoryID>>>>))]
		[PXDBDefault(typeof(InventoryItem.inventoryID))]
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
		#region ReplenishmentClassID
		public abstract class replenishmentClassID : PX.Data.IBqlField
		{
		}
		protected String _ReplenishmentClassID;
		[PXDefault()]
		[PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
        [PXUIField(DisplayName = "Repl. Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<INReplenishmentClass.replenishmentClassID>))]
		public virtual String ReplenishmentClassID
		{
			get
			{
				return this._ReplenishmentClassID;
			}
			set
			{
				this._ReplenishmentClassID = value;
			}
		}
		#endregion
		#region ReplenishmentMethod
		public abstract class replenishmentMethod : PX.Data.IBqlField
		{
		}
		protected string _ReplenishmentMethod;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Method")]
		[PXDefault(INReplenishmentMethod.None, 
			typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.replenishmentMethod))]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		[INReplenishmentMethod.List]		
		public virtual string ReplenishmentMethod
		{
			get
			{
				return this._ReplenishmentMethod;
			}
			set
			{
				this._ReplenishmentMethod = value;
			}
		}
		#endregion				
		#region ReplenishmentSource
		public abstract class replenishmentSource : PX.Data.IBqlField
		{
		}
		protected string _ReplenishmentSource;
		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Source")]
		[PXDefault(INReplenishmentSource.Purchased,
			typeof(Coalesce<Search<INItemClassRep.replenishmentSource,
					Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
						And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>,
				Search<INReplenishmentClass.replenishmentSource,
						Where<INReplenishmentClass.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>))]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		[INReplenishmentSource.List]		
		public virtual string ReplenishmentSource
		{
			get
			{
				return this._ReplenishmentSource;
			}
			set
			{
				this._ReplenishmentSource = value;
			}
		}
		#endregion
		#region ReplenishmentSourceSiteID
		public abstract class replenishmentSourceSiteID : PX.Data.IBqlField
		{
		}
		protected Int32? _ReplenishmentSourceSiteID;
		[PXDefault(typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.replenishmentSourceSiteID), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		[IN.ReplenishmentSourceSite(typeof(INItemClassRep.replenishmentSource), DisplayName = "Replenishment Warehouse")]
		public virtual Int32? ReplenishmentSourceSiteID
		{
			get
			{
				return this._ReplenishmentSourceSiteID;
			}
			set
			{
				this._ReplenishmentSourceSiteID = value;
			}
		}
		#endregion
		#region ReplenishmentPolicyID
		public abstract class replenishmentPolicyID : PX.Data.IBqlField
		{
		}
		protected String _ReplenishmentPolicyID;
		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
		[PXUIField(DisplayName = "Seasonality")]
		[PXSelector(typeof(Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof(INReplenishmentPolicy.descr))]
		[PXDefault(typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.replenishmentPolicyID))]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		public virtual String ReplenishmentPolicyID
		{
			get
			{
				return this._ReplenishmentPolicyID;
			}
			set
			{
				this._ReplenishmentPolicyID = value;
			}
		}
		#endregion

		#region MaxShelfLife
		public abstract class maxShelfLife : PX.Data.IBqlField
		{
		}
		protected Int32? _MaxShelfLife;
		[PXDBInt()]
		[PXUIField(DisplayName = "Max. Shelf Life (Days)")]
		[PXDefault(0)]
		public virtual Int32? MaxShelfLife
		{
			get
			{
				return this._MaxShelfLife;
			}
			set
			{
				this._MaxShelfLife = value;
			}
		}
		#endregion
		#region LaunchDate
		public abstract class launchDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _LaunchDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Launch Date")]
		[PXDefault(typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.launchDate), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		public virtual DateTime? LaunchDate
		{
			get
			{
				return this._LaunchDate;
			}
			set
			{
				this._LaunchDate = value;
			}
		}
		#endregion
		#region TerminationDate
		public abstract class terminationDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TerminationDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Termination Date")]
		[PXDefault(typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.terminationDate), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		public virtual DateTime? TerminationDate
		{
			get
			{
				return this._TerminationDate;
			}
			set
			{
				this._TerminationDate = value;
			}
		}
		#endregion
		#region ServiceLevel
		public abstract class serviceLevel : PX.Data.IBqlField
		{
		}
		protected decimal? _ServiceLevel;
		[PXDBDecimal(6, MinValue = 0.500001, MaxValue = 0.999999)]
		[PXUIField(DisplayName = "Service Level", Visible = true)]
		[PXDefault(TypeCode.Decimal, "0.840000", typeof(Select<INItemClassRep,
			Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.serviceLevel), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INItemRep.replenishmentClassID>))]
		public virtual decimal? ServiceLevel
		{
			get
			{
				return this._ServiceLevel;
			}
			set
			{
				this._ServiceLevel = value;
			}
		}
		#endregion
		#region ServiceLevelPct
		public abstract class serviceLevelPct : PX.Data.IBqlField
		{
		}

		[PXDecimal(4, MinValue = 50.0001, MaxValue = 99.9999)]
		[PXUIField(DisplayName = "Service Level (%)", Visible = true)]
		[PXDefault(TypeCode.Decimal, "84.0000")]
		public virtual decimal? ServiceLevelPct
		{
			[PXDependsOnFields(typeof(serviceLevel))]
			get
			{
				return this._ServiceLevel * 100.0m;
			}
			set
			{
				this._ServiceLevel = value / 100.0m;
			}
		}
		#endregion
		#region SafetyStock
		public abstract class safetyStock : PX.Data.IBqlField
		{
		}
		protected Decimal? _SafetyStock;
		[PXDBQuantity]
		[PXUIField(DisplayName = "Safety Stock")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? SafetyStock
		{
			get
			{
				return this._SafetyStock;
			}
			set
			{
				this._SafetyStock = value;
			}
		}
		#endregion
		#region MinQty
		public abstract class minQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _MinQty;
		[PXDBQuantity]
		[PXUIField(DisplayName = "Reorder Point")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? MinQty
		{
			get
			{
				return this._MinQty;
			}
			set
			{
				this._MinQty = value;
			}
		}
		#endregion
		#region MaxQty
		public abstract class maxQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _MaxQty;
		[PXDBQuantity]
		[PXUIField(DisplayName = "Max Qty.")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? MaxQty
		{
			get
			{
				return this._MaxQty;
			}
			set
			{
				this._MaxQty = value;
			}
		}
		#endregion
		#region TransferERQ
		public abstract class transferERQ : PX.Data.IBqlField
		{
		}
		protected Decimal? _TransferERQ;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0",
				typeof(Select<INItemClassRep,
					Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
					And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
					SourceField = typeof(INItemClassRep.transferERQ), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Transfer ERQ")]
		public virtual Decimal? TransferERQ
		{
			get
			{
				return this._TransferERQ;
			}
			set
			{
				this._TransferERQ = value;
			}
		}
		#endregion
		#region ForecastModelType
		public abstract class forecastModelType : PX.Data.IBqlField
		{
		}
		protected String _ForecastModelType;
		[PXDBString(3, IsFixed = true)]
		[DemandForecastModelType.List()]
		[PXUIField(DisplayName = "Demand Forecast Model")]		
		[PXDefault(DemandForecastModelType.None,
			typeof(Select<INItemClassRep,Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.forecastModelType), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<INItemRep.forecastModelType>))]
		public virtual String ForecastModelType
		{
			get
			{
				return this._ForecastModelType;
			}
			set
			{
				this._ForecastModelType = value;
			}
		}
		#endregion
		#region ForecastPeriodType
		public abstract class forecastPeriodType : PX.Data.IBqlField
		{
		}
		protected String _ForecastPeriodType;
		[PXDBString(2, IsFixed = true)]
		[DemandPeriodType.List()]
		[PXDefault(DemandPeriodType.Month, typeof(Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.forecastPeriodType), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Forecast Period Type")]
		public virtual String ForecastPeriodType
		{
			get
			{
				return this._ForecastPeriodType;
			}
			set
			{
				this._ForecastPeriodType = value;
			}
		}
		#endregion

		#region HistoryDepth
		public abstract class historyDepth : PX.Data.IBqlField
		{
		}
		protected Int32? _HistoryDepth;

		[PXDBInt(MinValue = 0, MaxValue = 20000)]
		[PXDefault(0, typeof(Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
					And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
					SourceField = typeof(INItemClassRep.historyDepth), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Periods to Analyze")]
		public virtual Int32? HistoryDepth
		{
			get
			{
				return this._HistoryDepth;
			}
			set
			{
				this._HistoryDepth = value;
			}
		}
		#endregion
		#region ESSmoothingConstantL
		public abstract class eSSmoothingConstantL : PX.Data.IBqlField
		{
		}
		protected Decimal? _ESSmoothingConstantL;
		[PXDBDecimal(9, MinValue = 0.0, MaxValue = 1.0)]
		[PXUIField(DisplayName = "Level Smoothing Constant")]
		[PXDefault(TypeCode.Decimal, "0.0",
			typeof(Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.eSSmoothingConstantL),
			PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? ESSmoothingConstantL
		{
			get
			{
				return this._ESSmoothingConstantL;
			}
			set
			{
				this._ESSmoothingConstantL = value;
			}
		}
		#endregion
		#region ESSmoothingConstantT
		public abstract class eSSmoothingConstantT : PX.Data.IBqlField
		{
		}
		protected Decimal? _ESSmoothingConstantT;
		[PXDBDecimal(9)]
		[PXDefault(TypeCode.Decimal, "0.0",
			typeof(Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.eSSmoothingConstantT),
			PersistingCheck = PXPersistingCheck.Nothing)]		
		[PXUIField(DisplayName = "Trend Smoothing Constant")]
		public virtual Decimal? ESSmoothingConstantT
		{
			get
			{
				return this._ESSmoothingConstantT;
			}
			set
			{
				this._ESSmoothingConstantT = value;
			}
		}
		#endregion
		#region ESSmoothingConstantS
		public abstract class eSSmoothingConstantS : PX.Data.IBqlField
		{
		}
		protected Decimal? _ESSmoothingConstantS;
		[PXDBDecimal(9)]
		[PXDefault(TypeCode.Decimal, "0.0",
			typeof(Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.itemClassID>>,
			And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>),
			SourceField = typeof(INItemClassRep.eSSmoothingConstantS),
			PersistingCheck = PXPersistingCheck.Nothing)]		
		[PXUIField(DisplayName = "Seasonality Smoothing Constant")]
		public virtual Decimal? ESSmoothingConstantS
		{
			get
			{
				return this._ESSmoothingConstantS;
			}
			set
			{
				this._ESSmoothingConstantS = value;
			}
		}
		#endregion
		#region AutoFitModel
		public abstract class autoFitModel : PX.Data.IBqlField
		{
		}
		protected Boolean? _AutoFitModel;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Auto Fit Model", Visibility = PXUIVisibility.Invisible)]
		public virtual Boolean? AutoFitModel
		{
			get
			{
				return this._AutoFitModel;
			}
			set
			{
				this._AutoFitModel = value;
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
	}
}
