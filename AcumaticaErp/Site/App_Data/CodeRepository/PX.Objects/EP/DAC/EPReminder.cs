using System;
using PX.Data;

namespace PX.Objects.EP
{
	[Serializable]
	public partial class EPReminder : IBqlTable
	{
		#region NoteID
		public abstract class noteID : IBqlField { }

		protected Int64? _NoteID;
		[PXDBLong(IsKey = true)]
		public virtual Int64? NoteID
		{
			get { return _NoteID; }
			set { _NoteID = value; }
		}
		#endregion

		#region UserID
		public abstract class userID : IBqlField { }

		protected Guid? _UserID;
		[PXDBGuid(IsKey = true)]
		public virtual Guid? UserID
		{
			get { return _UserID; }
			set { _UserID = value; }
		}
		#endregion

		#region Date

		public abstract class date : IBqlField { }

		protected DateTime? _Date;

		[PXDBDate(PreserveTime = true)]
		[PXDefault]
		public virtual DateTime? Date
		{
			get { return _Date; }
			set { _Date = value; }
		}

		#endregion

		#region Dismiss

		public abstract class dismiss : IBqlField { }

		protected Boolean? _Dismiss;

		[PXDBBool]
		[PXDefault(false)]
		public virtual Boolean? Dismiss
		{
			get { return _Dismiss; }
			set { _Dismiss = value; }
		}

		#endregion

		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
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
		[PXDBCreatedByScreenID]
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
		[PXDBCreatedDateTime]
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
		[PXDBLastModifiedByID]
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
		[PXDBLastModifiedByScreenID]
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
		[PXDBLastModifiedDateTime]
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
