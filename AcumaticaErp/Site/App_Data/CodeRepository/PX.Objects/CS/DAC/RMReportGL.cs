using System;
using PX.Data;
using PX.CS;

namespace PX.Objects.CS
{
	[Serializable]
	public partial class RMReportGL : PXCacheExtension<RMReport>
	{
		#region Type
		public abstract class type : PX.Data.IBqlField
		{
		}
		protected String _Type;
		[PXDBString(2, IsFixed = true)]
		[RMType.List()]
		[PXDefault(RMType.GL)]
		[PXUIField(DisplayName = "Type", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
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

		#region RequestLedgerID
		public abstract class requestLedgerID : PX.Data.IBqlField
		{
		}
		protected bool? _RequestLedgerID;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestLedgerID
		{
			get
			{
				return this._RequestLedgerID;
			}
			set
			{
				this._RequestLedgerID = value;
			}
		}
		#endregion
		#region RequestAccountClassID
		public abstract class requestAccountClassID : PX.Data.IBqlField
		{
		}
		protected bool? _RequestAccountClassID;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestAccountClassID
		{
			get
			{
				return this._RequestAccountClassID;
			}
			set
			{
				this._RequestAccountClassID = value;
			}
		}
		#endregion
		#region RequestStartAccount
		public abstract class requestStartAccount : PX.Data.IBqlField
		{
		}
		protected bool? _RequestStartAccount;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestStartAccount
		{
			get
			{
				return this._RequestStartAccount;
			}
			set
			{
				this._RequestStartAccount = value;
			}
		}
		#endregion
		#region RequestEndAccount
		public abstract class requestEndAccount : PX.Data.IBqlField
		{
		}
		protected bool? _RequestEndAccount;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestEndAccount
		{
			get
			{
				return this._RequestEndAccount;
			}
			set
			{
				this._RequestEndAccount = value;
			}
		}
		#endregion
		#region RequestStartSub
		public abstract class requestStartSub : PX.Data.IBqlField
		{
		}
		protected bool? _RequestStartSub;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestStartSub
		{
			get
			{
				return this._RequestStartSub;
			}
			set
			{
				this._RequestStartSub = value;
			}
		}
		#endregion
		#region RequestEndSub
		public abstract class requestEndSub : PX.Data.IBqlField
		{
		}
		protected bool? _RequestEndSub;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Request")]
		public virtual bool? RequestEndSub
		{
			get
			{
				return this._RequestEndSub;
			}
			set
			{
				this._RequestEndSub = value;
			}
		}
		#endregion	
	}
}
