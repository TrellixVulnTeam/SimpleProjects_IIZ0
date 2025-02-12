namespace PX.Objects.IN
{
	using System;
	using PX.Data;
	using PX.Objects.CS;
	using PX.Objects.GL;
	using PX.Objects.DR;
    using PX.Objects.CM;
	
	[System.SerializableAttribute()]
	[PXCacheName(Messages.DeferredRevenueComponents)]
	public partial class INComponent : PX.Data.IBqlTable
	{
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[PXDBDefault(typeof(InventoryItem.inventoryID))]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<INComponent.inventoryID>>>>))]
		[PXDBInt(IsKey=true)]
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
		#region ComponentID
		public abstract class componentID : PX.Data.IBqlField
		{
		}
		protected Int32? _ComponentID;
		[PXDefault()]
		[Inventory(Filterable = true, IsKey = true, DisplayName = "Inventory ID")]
		public virtual Int32? ComponentID
		{
			get
			{
				return this._ComponentID;
			}
			set
			{
				this._ComponentID = value;
			}
		}
		#endregion
		#region DeferredCode
		public abstract class deferredCode : PX.Data.IBqlField
		{
		}
		protected String _DeferredCode;
		[PXDefault(typeof(InventoryItem.deferredCode))]
		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
		[PXUIField(DisplayName = "Deferral Code", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(DRDeferredCode.deferredCodeID))]
		public virtual String DeferredCode
		{
			get
			{
				return this._DeferredCode;
			}
			set
			{
				this._DeferredCode = value;
			}
		}
		#endregion
		#region Percentage
		public abstract class percentage : PX.Data.IBqlField
		{
		}
		protected Decimal? _Percentage;
		[PXDBDecimal(9, MinValue=0, MaxValue=100)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Percentage")]
		[PXFormula(null, typeof(SumCalc<InventoryItem.totalPercentage>))]
		public virtual Decimal? Percentage
		{
			get
			{
				return this._Percentage;
			}
			set
			{
				this._Percentage = value;
			}
		}
		#endregion
		#region SalesAcctID
		public abstract class salesAcctID : PX.Data.IBqlField
		{
		}
		protected Int32? _SalesAcctID;
		[Account(DisplayName = "Sales Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description))]
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
		[SubAccount(typeof(INComponent.salesAcctID), DisplayName = "Sales Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description))]
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
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[INUnit(typeof(INComponent.componentID))]
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
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		protected Decimal? _Qty;
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBQuantity]
		[PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? Qty
		{
			get
			{
				return this._Qty;
			}
			set
			{
				this._Qty = value;
			}
		}
		#endregion
        #region FixedAmt
        public abstract class fixedAmt : PX.Data.IBqlField
        {
        }
        protected Decimal? _FixedAmt;
        [PXDBBaseCuryAttribute()]
        [PXUIField(DisplayName = "Fixed Amount")]
        public virtual Decimal? FixedAmt
        {
            get
            {
                return this._FixedAmt;
            }
            set
            {
                this._FixedAmt = value;
            }
        }
        #endregion
		#region AmtOption
        public abstract class amtOption : PX.Data.IBqlField
        {
        }
        protected String _AmtOption;
        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Amount Option", Visibility = PXUIVisibility.Visible)]
        [PXDefault(INAmountOption.Percentage)]
        [INAmountOption.List()]
        public virtual String AmtOption
        {
            get
            {
                return this._AmtOption;
            }
            set
            {
                this._AmtOption = value;
            }
        }
        #endregion
        #region System Columns
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
		#endregion
	}

    public static class INAmountOption
    {
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(
                new string[] { Percentage, FixedAmt },
                new string[] { Messages.Percentage, Messages.FixedAmt }) { }
        }

        public const string Percentage = "P";
        public const string FixedAmt = "F";

        public class percentage : Constant<string>
        {
            public percentage() : base(Percentage) { ;}
        }

        public class fixedAmt : Constant<string>
        {
            public fixedAmt() : base(FixedAmt) { ;}
        }

    }
}
