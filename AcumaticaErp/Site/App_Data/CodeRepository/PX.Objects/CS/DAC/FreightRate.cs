namespace PX.Objects.CS
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public partial class FreightRate : PX.Data.IBqlTable
	{
		#region CarrierID
		public abstract class carrierID : PX.Data.IBqlField
		{
		}
		protected String _CarrierID;
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
		[PXDefault(typeof(Carrier.carrierID))]
		public virtual String CarrierID
		{
			get
			{
				return this._CarrierID;
			}
			set
			{
				this._CarrierID = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXLineNbr(typeof(Carrier))]
		[PXParent(typeof(Select<Carrier, Where<Carrier.carrierID, Equal<Current<FreightRate.carrierID>>>>), LeaveChildren = true)]
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
		#region Weight
		public abstract class weight : PX.Data.IBqlField
		{
		}
		protected Decimal? _Weight;
		[PXDBDecimal(2)]
		[PXDefault()]
		[PXUIField(DisplayName = "Weight")]
		public virtual Decimal? Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				this._Weight = value;
			}
		}
		#endregion
		#region Volume
		public abstract class volume : PX.Data.IBqlField
		{
		}
		protected Decimal? _Volume;
		[PXDBDecimal(2)]
		[PXDefault()]
		[PXUIField(DisplayName = "Volume")]
		public virtual Decimal? Volume
		{
			get
			{
				return this._Volume;
			}
			set
			{
				this._Volume = value;
			}
		}
		#endregion
		#region ZoneID
		public abstract class zoneID : PX.Data.IBqlField
		{
		}
		protected String _ZoneID;
		[PXDBString(15, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Zone ID")]
		[PXSelector(typeof(ShippingZone.zoneID))]
		public virtual String ZoneID
		{
			get
			{
				return this._ZoneID;
			}
			set
			{
				this._ZoneID = value;
			}
		}
		#endregion
		#region Rate
		public abstract class rate : PX.Data.IBqlField
		{
		}
		protected Decimal? _Rate;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Rate")]
		public virtual Decimal? Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				this._Rate = value;
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
