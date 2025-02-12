using System;
using PX.Data;
using PX.Objects.CM;

namespace PX.Objects.GL
{
	public class LedgerBalanceType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Actual, Report, Statistical, Budget },
				new string[] { Messages.Actual, Messages.Report, Messages.Statistical, Messages.Budget }) { ; }
		}

		public const string Actual = "A";
		public const string Report = "R";
		public const string Statistical = "S";
		public const string Budget = "B";

		public class actual : Constant<string>
		{
			public actual() : base(Actual) { ; }
		}

		public class report : Constant<string>
		{
			public report() : base(Report) { ; }
		}

		public class statistical : Constant<string>
		{
			public statistical() : base(Statistical) { ; }
		}

		public class budget : Constant<string>
		{
			public budget() : base(Budget) { ; }
		}
	}

	[PXPrimaryGraph(typeof(GeneralLedgerMaint))]
	[System.SerializableAttribute()]
	public partial class Ledger : PX.Data.IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public bool? Selected
		{
			get
			{
				return _Selected;
			}
			set
			{
				_Selected = value;
			}
		}
		#endregion
		#region LedgerID
		public abstract class ledgerID : PX.Data.IBqlField
		{
		}
		protected Int32? _LedgerID;
		[PXDBIdentity()]
		[PXUIField(DisplayName = "Ledger ID", Visibility = PXUIVisibility.Visible, Visible = false )]
		public virtual Int32? LedgerID
		{
			get
			{
				return this._LedgerID;
			}
			set
			{
				this._LedgerID = value;
			}
		}
		#endregion
		#region LedgerCD
		public abstract class ledgerCD : PX.Data.IBqlField
		{
		}
		protected string _LedgerCD;
		[PXDBString(10, IsUnicode = true, IsKey = true)]
        [PXDefault]
		[PXUIField(DisplayName = "Ledger ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string LedgerCD
		{
			get
			{
				return this._LedgerCD;
			}
			set
			{
				this._LedgerCD = value;
			}
		}
		#endregion
		#region BaseCuryID
		public abstract class baseCuryID : PX.Data.IBqlField
		{
		}
		protected String _BaseCuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXDefault(typeof(Company.baseCuryID))]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Currency.curyID))]
		public virtual String BaseCuryID
		{
			get
			{
				return this._BaseCuryID;
			}
			set
			{
				this._BaseCuryID = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;
		[PXDBString(60, IsUnicode = true)]
		[PXDefault("")]
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
		#region BalanceType
		public abstract class balanceType : PX.Data.IBqlField
		{
		}
		protected String _BalanceType;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(LedgerBalanceType.Actual)]
		[LedgerBalanceType.List()]
		[PXUIField(DisplayName = "Balance Type", Visibility = PXUIVisibility.Visible)]
		public virtual String BalanceType
		{
			get
			{
				return this._BalanceType;
			}
			set
			{
				this._BalanceType = value;
			}
		}
		#endregion
		#region DefBranchID
		public abstract class defBranchID : PX.Data.IBqlField
		{
		}
		protected Int32? _DefBranchID;
		[ConsolBranch(DisplayName = "Consol. Branch")]
		public virtual Int32? DefBranchID
		{
			get
			{
				return this._DefBranchID;
			}
			set
			{
				this._DefBranchID = value;
			}
		}
		#endregion
		#region PostInterCompany
		public abstract class postInterCompany : PX.Data.IBqlField
		{
		}
		protected Boolean? _PostInterCompany;
		[PXDBBool()]
        [PXUIField(DisplayName = "Branch Accounting")]
		[PXDefault(true)]
		public virtual Boolean? PostInterCompany
		{
			get
			{
				return this._PostInterCompany;
			}
			set
			{
				this._PostInterCompany = value;
			}
		}
		#endregion
		#region ConsolAllowed
		public abstract class consolAllowed : PX.Data.IBqlField
		{
		}
		protected bool? _ConsolAllowed;
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Is Consolidation Source")]
		public virtual bool? ConsolAllowed
		{
			get
			{
				return this._ConsolAllowed;
			}
			set
			{
				this._ConsolAllowed = value;
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
