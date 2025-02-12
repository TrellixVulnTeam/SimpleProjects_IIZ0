namespace PX.Objects.SO
{
	using System;
	using PX.Data;
    using PX.Objects.AR;
    using PX.Objects.IN;
	using PX.Objects.GL;
	using PX.Objects.CM;
	
	[System.SerializableAttribute()]
	[PXCacheName(Messages.SOInvoiceDiscountDetail)]
	public partial class SOInvoiceDiscountDetail : PX.Data.IBqlTable, IDiscountDetail
	{
        #region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBString(3, IsKey = true, IsFixed = true)]
		[PXDBDefault(typeof(ARInvoice.docType))]
		[PXUIField(DisplayName = "TranType")]
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
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(ARInvoice.refNbr))]
        [PXParent(typeof(Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<SOInvoiceDiscountDetail.tranType>>, And<ARInvoice.refNbr, Equal<Current<SOInvoiceDiscountDetail.refNbr>>>>>))]
		[PXUIField(DisplayName = "RefNbr")]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region OrderType
		public abstract class orderType : PX.Data.IBqlField
		{
		}
		protected String _OrderType;
		[PXDBString(2, IsKey = true, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Order Type")]
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
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXParent(typeof(Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOOrderDiscountDetail.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOOrderDiscountDetail.orderNbr>>>>>))]
		[PXUIField(DisplayName = "Order Nbr.")]
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
		        
        #region DiscountID
        public abstract class discountID : PX.Data.IBqlField
        {
        }
        protected String _DiscountID;
        [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Discount Code")]
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
        [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault()]
        [PXUIField(DisplayName = "Sequence ID")]
        [PXSelector(typeof(Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<SOInvoiceDiscountDetail.discountID>>>>>))]
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
        #region Type
        public abstract class type : PX.Data.IBqlField
        {
        }
        protected String _Type;
        [PXDBString(1, IsKey = true, IsFixed = true)]
        [PXDefault()]
        [DiscountType.List()]
        [PXUIField(DisplayName = "Type")]
        public virtual String Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
        #endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		protected Int64? _CuryInfoID;
		[PXDBLong()]
		[CurrencyInfo(typeof(SOInvoice.curyInfoID))]
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
		#region DiscountableAmt
		public abstract class discountableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscountableAmt;
		[PXDBDecimal(4)]
		public virtual Decimal? DiscountableAmt
		{
			get
			{
				return this._DiscountableAmt;
			}
			set
			{
				this._DiscountableAmt = value;
			}
		}
		#endregion
		#region CuryDiscountableAmt
		public abstract class curyDiscountableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryDiscountableAmt;
		[PXDBCurrency(typeof(SOInvoiceDiscountDetail.curyInfoID), typeof(SOInvoiceDiscountDetail.discountableAmt))]
		[PXUIField(DisplayName = "Discountable Amt.")]
		public virtual Decimal? CuryDiscountableAmt
		{
			get
			{
				return this._CuryDiscountableAmt;
			}
			set
			{
				this._CuryDiscountableAmt = value;
			}
		}
		#endregion
		#region DiscountableQty
		public abstract class discountableQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscountableQty;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Discountable Qty.")]
		public virtual Decimal? DiscountableQty
		{
			get
			{
				return this._DiscountableQty;
			}
			set
			{
				this._DiscountableQty = value;
			}
		}
		#endregion
		#region DiscountAmt
		public abstract class discountAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _DiscountAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? DiscountAmt
		{
			get
			{
				return this._DiscountAmt;
			}
			set
			{
				this._DiscountAmt = value;
			}
		}
		#endregion
		#region CuryDiscountAmt
		public abstract class curyDiscountAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryDiscountAmt;
		[PXDBCurrency(typeof(SOInvoiceDiscountDetail.curyInfoID), typeof(SOInvoiceDiscountDetail.discountAmt))]
		[PXUIField(DisplayName = "Discount Amt.")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryDiscountAmt
		{
			get
			{
				return this._CuryDiscountAmt;
			}
			set
			{
				this._CuryDiscountAmt = value;
			}
		}
		#endregion
        #region DiscountPct
        public abstract class discountPct : PX.Data.IBqlField
        {
        }
        protected Decimal? _DiscountPct;
        [PXDBQuantity()]
        [PXUIField(DisplayName = "Discount")]
        public virtual Decimal? DiscountPct
        {
            get
            {
                return this._DiscountPct;
            }
            set
            {
                this._DiscountPct = value;
            }
        }
        #endregion
		#region FreeItemID
		public abstract class freeItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _FreeItemID;
		[Inventory(DisplayName = "Free Item", Enabled = false)]
		public virtual Int32? FreeItemID
		{
			get
			{
				return this._FreeItemID;
			}
			set
			{
				this._FreeItemID = value;
			}
		}
		#endregion
		#region FreeItemQty
		public abstract class freeItemQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _FreeItemQty;
		[PXDBQuantity(MinValue = 0)]
		[PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
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
        #region IsManual
        public abstract class isManual : PX.Data.IBqlField
        {
        }
        protected Boolean? _IsManual;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
        public virtual Boolean? IsManual
        {
            get
            {
                return this._IsManual;
            }
            set
            {
                this._IsManual = value;
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
