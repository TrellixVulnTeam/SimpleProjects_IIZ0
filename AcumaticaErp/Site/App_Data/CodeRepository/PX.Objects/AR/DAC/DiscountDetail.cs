namespace PX.Objects.SO
{
	using System;
	using PX.Data;
using PX.Objects.IN;
using PX.Objects.GL;
using PX.Objects.CM;
	
	[System.SerializableAttribute()]
	public partial class DiscountDetail : PX.Data.IBqlTable
	{
		#region DiscountDetailsID
		public abstract class discountDetailsID : PX.Data.IBqlField
		{
		}
		protected Int32? _DiscountDetailsID;
		[PXDBIdentity(IsKey = true)]
		public virtual Int32? DiscountDetailsID
		{
			get
			{
				return this._DiscountDetailsID;
			}
			set
			{
				this._DiscountDetailsID = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt()]
		[PXDefault(0)]
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
		#region DiscountID
		public abstract class discountID : PX.Data.IBqlField
		{
		}
		protected String _DiscountID;
		[PXDBString(10, IsUnicode = true)]
		[PXDBDefault(typeof(DiscountSequence.discountID))]
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
		[PXDBDefault(typeof(DiscountSequence.discountSequenceID))]
		[PXParent(typeof(Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, 
			Equal<Current<DiscountDetail.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountDetail.discountID>>> >>))]
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
		#region Amount
		public abstract class amount : PX.Data.IBqlField
		{
		}
		protected Decimal? _Amount;
        [PXDBPriceCost(MinValue = 0)]
        [PXUIField(DisplayName = "Break Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
			}
		}
		#endregion
        #region AmountTo
        public abstract class amountTo : PX.Data.IBqlField
        {
        }
        protected Decimal? _AmountTo;
        [PXDBDecimal]        
        public virtual Decimal? AmountTo
        {
            get
            {
                return this._AmountTo;
            }
            set
            {
                this._AmountTo = value;
            }
        }
        #endregion
		#region LastAmount
		public abstract class lastAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _LastAmount;
		[PXDBPriceCost(MinValue = 0)]
		[PXUIField(DisplayName = "Last Break Amount", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? LastAmount
		{
			get
			{
				return this._LastAmount;
			}
			set
			{
				this._LastAmount = value;
			}
		}
		#endregion
        #region LastAmountTo
        public abstract class lastAmountTo : PX.Data.IBqlField
        {
        }
        protected Decimal? _LastAmountTo;
        [PXDBDecimal]
        public virtual Decimal? LastAmountTo
        {
            get
            {
                return this._LastAmountTo;
            }
            set
            {
                this._LastAmountTo = value;
            }
        }
        #endregion
		#region PendingAmount
		public abstract class pendingAmount : PX.Data.IBqlField
		{
		}
		protected Decimal? _PendingAmount;
		[PXDBPriceCost(MinValue = 0)]
		[PXUIField(DisplayName = "Pending Break Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? PendingAmount
		{
			get
			{
				return this._PendingAmount;
			}
			set
			{
				this._PendingAmount = value;
			}
		}
		#endregion
		#region Quantity
		public abstract class quantity : PX.Data.IBqlField
		{
		}
		protected Decimal? _Quantity;
        [PXDBQuantity(MinValue = 0)]
        [PXUIField(DisplayName = "Break Quantity", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				this._Quantity = value;
			}
		}
		#endregion
        #region QuantityTo
        public abstract class quantityTo : PX.Data.IBqlField
        {
        }
        protected Decimal? _QuantityTo;
        [PXDBDecimal]       
        public virtual Decimal? QuantityTo
        {
            get
            {
                return this._QuantityTo;
            }
            set
            {
                this._QuantityTo = value;
            }
        }
        #endregion
		#region LastQuantity
		public abstract class lastQuantity : PX.Data.IBqlField
		{
		}
		protected Decimal? _LastQuantity;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Last Break Quantity", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? LastQuantity
		{
			get
			{
				return this._LastQuantity;
			}
			set
			{
				this._LastQuantity = value;
			}
		}
		#endregion
        #region LastQuantityTo
        public abstract class lastQuantityTo : PX.Data.IBqlField
        {
        }
        protected Decimal? _LastQuantityTo;
        [PXDBDecimal]
        public virtual Decimal? LastQuantityTo
        {
            get
            {
                return this._LastQuantityTo;
            }
            set
            {
                this._LastQuantityTo = value;
            }
        }
        #endregion
		#region PendingQuantity
		public abstract class pendingQuantity : PX.Data.IBqlField
		{
		}
		protected Decimal? _PendingQuantity;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Pending Break Quantity", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? PendingQuantity
		{
			get
			{
				return this._PendingQuantity;
			}
			set
			{
				this._PendingQuantity = value;
			}
		}
		#endregion
		#region Discount
		public abstract class discount : PX.Data.IBqlField
		{
		}
		protected Decimal? _Discount;
		[PXDBDecimal(typeof(Search2<Currency.decimalPlaces, InnerJoin<Company, On<Company.baseCuryID, Equal<Currency.curyID>>>>))]
		[PXUIField(DisplayName = "Discount Amount", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? Discount
		{
			get
			{
				return this._Discount;
			}
			set
			{
				this._Discount = value;
			}
		}
		#endregion
		#region DiscountPercent
		public abstract class discountPercent : PX.Data.IBqlField
		{
		}
		[PXDecimal(2, MinValue = -100, MaxValue=100)]
        [PXUIField(DisplayName = "Discount Percent", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? DiscountPercent
		{
			get
			{
				return this.Discount;
			}
			set
			{
				this.Discount = value;
			}
		}
		#endregion

		#region LastDiscount
		public abstract class lastDiscount : PX.Data.IBqlField
		{
		}
		protected Decimal? _LastDiscount;
		[PXDBPriceCost()]
		[PXUIField(DisplayName = "Last Discount Amount", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? LastDiscount
		{
			get
			{
				return this._LastDiscount;
			}
			set
			{
				this._LastDiscount = value;
			}
		}
		#endregion
		#region LastDiscountPercent
		public abstract class lastDiscountPercent : PX.Data.IBqlField
		{
		}
		[PXDecimal(2, MinValue = -100, MaxValue = 100)]
        [PXUIField(DisplayName = "Last Discount Percent", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? LastDiscountPercent
		{
			get
			{
				return this.LastDiscount;
			}
			set
			{
				this.LastDiscount = value;
			}
		}
		#endregion
		#region PendingDiscount
		public abstract class pendingDiscount : PX.Data.IBqlField
		{
		}
		protected Decimal? _PendingDiscount;
		[PXDBPriceCost()]
		[PXUIField(DisplayName = "Pending Discount Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? PendingDiscount
		{
			get
			{
				return this._PendingDiscount;
			}
			set
			{
				this._PendingDiscount = value;
			}
		}
		#endregion
		#region PendingDiscountPercent
		public abstract class pendingDiscountPercent : PX.Data.IBqlField
		{
		}
		[PXDecimal(2, MinValue = -100, MaxValue = 100)]
        [PXUIField(DisplayName = "Pending Discount Percent", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? PendingDiscountPercent
		{
			get
			{
				return this.PendingDiscount;
			}
			set
			{
				this.PendingDiscount = value;
			}
		}
		#endregion
		#region FreeItemQty
		public abstract class freeItemQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _FreeItemQty;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Free Item Qty.", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? FreeItemQty
		{
			get
			{
				return this._FreeItemQty;
			}
			set
			{
				this._FreeItemQty = value;
			}
		}
		#endregion
		#region LastFreeItemQty
		public abstract class lastFreeItemQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _LastFreeItemQty;
		[PXDBQuantity(MinValue=0)]
		[PXUIField(DisplayName = "Last Free Item Qty.", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual Decimal? LastFreeItemQty
		{
			get
			{
				return this._LastFreeItemQty;
			}
			set
			{
				this._LastFreeItemQty = value;
			}
		}
		#endregion
		#region PendingFreeItemQty
		public abstract class pendingFreeItemQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _PendingFreeItemQty;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Pending Free Item Qty.", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? PendingFreeItemQty
		{
			get
			{
				return this._PendingFreeItemQty;
			}
			set
			{
				this._PendingFreeItemQty = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _StartDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Pending Date", Visibility = PXUIVisibility.Visible)]
		public virtual DateTime? StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				this._StartDate = value;
			}
		}
		#endregion
		#region LastDate
		public abstract class lastDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Effective Date", Visibility = PXUIVisibility.Visible, Enabled=false)]
		public virtual DateTime? LastDate
		{
			get
			{
				return this._LastDate;
			}
			set
			{
				this._LastDate = value;
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
}
