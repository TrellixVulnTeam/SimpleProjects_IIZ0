namespace PX.Objects.PM
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public partial class PMAccountGroupRate : PX.Data.IBqlTable
	{
		#region RateDefinitionID
		public abstract class rateDefinitionID : PX.Data.IBqlField
		{
		}
		protected int? _RateDefinitionID;
		[PXDBInt(IsKey = true)]
		[PXDefault(typeof(PMRateSequence.rateDefinitionID))]
		[PXParent(typeof(Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMAccountGroupRate.rateDefinitionID>>>>))]
		public virtual int? RateDefinitionID
		{
			get
			{
				return this._RateDefinitionID;
			}
			set
			{
				this._RateDefinitionID = value;
			}
		}
		#endregion
		#region RateCodeID
		public abstract class rateCodeID : PX.Data.IBqlField
		{
		}
		protected String _RateCodeID;

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault(typeof(PMRateSequence.rateCodeID))]
		public virtual String RateCodeID
		{
			get
			{
				return this._RateCodeID;
			}
			set
			{
				this._RateCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[PXDefault()]
		[AccountGroup(IsKey=true)]
		public virtual Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
	}
}
