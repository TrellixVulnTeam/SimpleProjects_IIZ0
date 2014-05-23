namespace PX.Objects.RQ
{
	using System;
	using PX.Data;
	using PX.Objects.IN;


	[System.SerializableAttribute()]
	[PXPrimaryGraph(typeof(RQRequestClassMaint))]
	public partial class RQRequestClassItem : PX.Data.IBqlTable
	{
		#region LineID
		public abstract class lineID : PX.Data.IBqlField
		{
		}
		protected int? _LineID;
		[PXDBIdentity(IsKey = true)]
		public virtual int? LineID
		{
			get
			{
				return this._LineID;
			}
			set
			{
				this._LineID = value;
			}
		}
		#endregion
		#region ReqClassID
		public abstract class reqClassID : PX.Data.IBqlField
		{
		}
		protected String _ReqClassID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault(typeof(RQRequestClass.reqClassID))]
		[PXUIField(DisplayName = "Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(RQRequestClass.reqClassID), DescriptionField = typeof(RQRequestClass.descr))]
		[PXParent(typeof(Select<RQRequestClass, Where<RQRequestClass.reqClassID, Equal<Current<RQRequestClassItem.reqClassID>>>>))]
		public virtual String ReqClassID
		{
			get
			{
				return this._ReqClassID;
			}
			set
			{
				this._ReqClassID = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;		
		
		[PXDefault]
		[PXDBInt]
		[PXDimensionSelector(InventoryAttribute.DimensionName,
			typeof(Search<RQInventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), 
			 typeof(RQInventoryItem.inventoryCD),
			 DescriptionField = typeof(RQInventoryItem.descr))]
		[PXUIField(DisplayName = "Inventory ID")]
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
	}
	
	[PXProjection(typeof(Select<InventoryItem>), Persistent = false)]
	[PXPrimaryGraph(
		new Type[] { typeof(InventoryItemMaint)},
		new Type[] { typeof(Select<RQInventoryItem, 
			Where<RQInventoryItem.inventoryID, Equal<Current<RQInventoryItem.inventoryID>>>>)
		})]
    [Serializable]
	public partial class RQInventoryItem : IBqlTable
	{
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[PXDBInt(BqlField = typeof(InventoryItem.inventoryID))]
		[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible, Visible = false)]
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
		#region InventoryCD
		public abstract class inventoryCD : PX.Data.IBqlField
		{
		}
		protected String _InventoryCD;
		[PXDefault()]				
		[InventoryRaw(typeof(Where<InventoryItem.stkItem, Equal<CS.boolTrue>>), BqlField = typeof(InventoryItem.inventoryCD), IsKey = true)]
		public virtual String InventoryCD
		{
			get
			{
				return this._InventoryCD;
			}
			set
			{
				this._InventoryCD = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;
		[PXDBString(255, IsUnicode = true, BqlField = typeof(InventoryItem.descr))]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get
			{
				return this._Descr;
			}
			set
			{
				this._Descr = value;
			}
		}
		#endregion
		#region ItemClassID
		public abstract class itemClassID : PX.Data.IBqlField
		{
		}
		protected String _ItemClassID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(InventoryItem.itemClassID))]
		[PXUIField(DisplayName = "Item Class", Visibility = PXUIVisibility.SelectorVisible)]				
		public virtual String ItemClassID
		{
			get
			{
				return this._ItemClassID;
			}
			set
			{
				this._ItemClassID = value;
			}
		}
		#endregion
		#region ItemStatus
		public abstract class itemStatus : PX.Data.IBqlField
		{
		}
		protected String _ItemStatus;
		[PXDBString(2, IsFixed = true, BqlField = typeof(InventoryItem.itemStatus))]
		[PXDefault("AC")]
		[PXUIField(DisplayName = "Item Status", Visibility = PXUIVisibility.SelectorVisible)]
		[InventoryItemStatus.List]
		public virtual String ItemStatus
		{
			get
			{
				return this._ItemStatus;
			}
			set
			{
				this._ItemStatus = value;
			}
		}
		#endregion
		#region ItemType
		public abstract class itemType : PX.Data.IBqlField
		{
		}
		protected string _ItemType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(InventoryItem.itemType))]
		//[PXDefault(INItemTypes.NonStockItem, typeof(Search<INItemClass.itemType, Where<INItemClass.itemClassID, Equal<Current<InventoryItem.itemClassID>>>>))]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible)]
		[INItemTypes.List()]
		public virtual String ItemType
		{
			get
			{
				return this._ItemType;
			}
			set
			{
				this._ItemType = value;
			}
		}
		#endregion
	}
}
