namespace PX.Objects.AR
{
	using System;
	using PX.Data;

	/// <summary>
	/// Header of Remainder notification message
	/// </summary>
	[System.SerializableAttribute()]
    public partial class ARDunningSetup : PX.Data.IBqlTable
    {
        #region DunningLetterLevel
        public abstract class dunningLetterLevel : PX.Data.IBqlField
		{
		}
        protected Int32? _DunningLetterLevel;
        [PXDBInt(IsKey = true)]
		[PXDefault(0)]
        [PXUIField(DisplayName = "Dunning Letter Level", Enabled=false)]
        [PXLineNbr(typeof(ARSetup))]
        [PXParent(typeof(Select<ARSetup>))]
        public virtual Int32? DunningLetterLevel
		{
			get
			{
                return this._DunningLetterLevel;
			}
			set
			{
                this._DunningLetterLevel = value;
			}
		}
		#endregion
        #region DueDays
        public abstract class dueDays : PX.Data.IBqlField
        {
        }
        protected Int32? _DueDays;
        [PXDBInt(MinValue=0, MaxValue=365)]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Days Past Due")]
        public virtual Int32? DueDays
        {
            get
            {
                return this._DueDays;
            }
            set
            {
                this._DueDays = value;
            }
        }
        #endregion
        #region DaysToSettle
        public abstract class daysToSettle : PX.Data.IBqlField
        {
        }
        protected Int32? _DaysToSettle;
        [PXDBInt(MinValue=0, MaxValue = 365)]
        [PXDefault(3)]
        [PXUIField(DisplayName = "Days to Settle")]
        public virtual Int32? DaysToSettle
        {
            get
            {
                return this._DaysToSettle;
            }
            set
            {
                this._DaysToSettle = value;
            }
        }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }
        protected String _Descr;
        [PXDBString(60, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Description")]
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

}
