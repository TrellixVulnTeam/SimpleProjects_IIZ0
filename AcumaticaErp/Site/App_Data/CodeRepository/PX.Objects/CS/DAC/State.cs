namespace PX.Objects.CS
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	[PXCacheName(Messages.State)]
	[PXPrimaryGraph(
		new Type[] { typeof(CountryMaint)},
		new Type[] { typeof(Select<State, 
			Where<State.countryID, Equal<Current<State.countryID>>, 
			  And<State.stateID, Equal<Current<State.stateID>>>>>)
		})]
	public partial class State : PX.Data.IBqlTable
	{
		#region CountryID
		public abstract class countryID : PX.Data.IBqlField
		{
		}
		protected String _CountryID;
		[PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">??")]
		[PXDefault(typeof(Country.countryID))]
		[PXUIField(DisplayName = "Country",Visible = false)]
		[PXSelector(typeof(Country.countryID), DirtyRead = true)]
		[PXParent(typeof(Select<Country,Where<Country.countryID,Equal<Current<State.countryID>>>>))]
		public virtual String CountryID
		{
			get
			{
				return this._CountryID;
			}
			set
			{
				this._CountryID = value;
			}
		}
		#endregion
		#region StateID
		public abstract class stateID : PX.Data.IBqlField
		{
		}
		protected String _StateID;
        [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = "State ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
		public virtual String StateID
		{
			get
			{
				return this._StateID;
			}
			set
			{
				this._StateID = value;
			}
		}
		#endregion
		#region Name
		public abstract class name : PX.Data.IBqlField
		{
		}
		protected String _Name;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "State Name", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}
		#endregion
		#region IsTaxRegistrationRequired
		public abstract class isTaxRegistrationRequired : PX.Data.IBqlField
		{
		}
		protected Boolean? _IsTaxRegistrationRequired;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Tax Registration Required")]
		public virtual Boolean? IsTaxRegistrationRequired
		{
			get
			{
				return this._IsTaxRegistrationRequired;
			}
			set
			{
				this._IsTaxRegistrationRequired = value;
			}
		}
		#endregion
		#region TaxRegistrationMask
		public abstract class taxRegistrationMask : PX.Data.IBqlField
		{
		}
		protected String _TaxRegistrationMask;
		[PXDBString(50)]
		[PXUIField(DisplayName = "Tax Registration Mask")]
		public virtual String TaxRegistrationMask
		{
			get
			{
				return this._TaxRegistrationMask;
			}
			set
			{
				this._TaxRegistrationMask = value;
			}
		}
		#endregion
		#region TaxRegistrationRegexp
		public abstract class taxRegistrationRegexp : PX.Data.IBqlField
		{
		}
		protected String _TaxRegistrationRegexp;
		[PXDBString(255)]
		[PXUIField(DisplayName = "Tax Registration Reg. Exp.")]
		public virtual String TaxRegistrationRegexp
		{
			get
			{
				return this._TaxRegistrationRegexp;
			}
			set
			{
				this._TaxRegistrationRegexp = value;
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
