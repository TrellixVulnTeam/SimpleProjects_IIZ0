namespace PX.Objects.PM
{
	using System;
	using PX.Data;
	using PX.Objects.IN;
using PX.Objects.GL;
using PX.Objects.CS;
using System.Collections.Generic;
	
	[System.SerializableAttribute()]
	public partial class PMAllocationStep : PX.Data.IBqlTable
	{
		#region AllocationID
		public abstract class allocationID : PX.Data.IBqlField
		{
		}
		protected String _AllocationID;
		[PXDBString(PMAllocation.allocationID.Length, IsKey = true, IsUnicode = true)]
		[PXDefault(typeof(PMAllocation.allocationID))]
		[PXParent(typeof(Select<PMAllocation, Where<PMAllocation.allocationID, Equal<Current<PMAllocationStep.allocationID>>>>))]
		public virtual String AllocationID
		{
			get
			{
				return this._AllocationID;
			}
			set
			{
				this._AllocationID = value;
			}
		}
		#endregion
		#region StepID
		public abstract class stepID : PX.Data.IBqlField
		{
		}
		protected Int32? _StepID;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Step ID")]
		public virtual Int32? StepID
		{
			get
			{
				return this._StepID;
			}
			set
			{
				this._StepID = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		public virtual String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region SelectOption
		public abstract class selectOption : PX.Data.IBqlField
		{
		}
		protected String _SelectOption;
		[PMSelectOption.List]
		[PXDefault(PMSelectOption.Transaction)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Select Transactions")]
		public virtual String SelectOption
		{
			get
			{
				return this._SelectOption;
			}
			set
			{
				this._SelectOption = value;
			}
		}
		#endregion
		#region Post
		public abstract class post : PX.Data.IBqlField
		{
		}
		protected Boolean? _Post;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Create Allocation Transaction")]
		public virtual Boolean? Post
		{
			get
			{
				return this._Post;
			}
			set
			{
				this._Post = value;
			}
		}
		#endregion
		#region QtyFormula
		public abstract class qtyFormula : PX.Data.IBqlField
		{
		}
		protected String _QtyFormula;
		[PXDBString(4000, IsUnicode = true)]
		[PXUIField(DisplayName = "Quantity Formula")]
		public virtual String QtyFormula
		{
			get
			{
				return this._QtyFormula;
			}
			set
			{
				this._QtyFormula = value;
			}
		}
		#endregion
		#region BillableQtyFormula
		public abstract class billableQtyFormula : PX.Data.IBqlField
		{
		}
		protected String _BillableQtyFormula;
		[PXDBString(4000, IsUnicode = true)]
		[PXUIField(DisplayName = "Billable Qty. Formula")]
		public virtual String BillableQtyFormula
		{
			get
			{
				return this._BillableQtyFormula;
			}
			set
			{
				this._BillableQtyFormula = value;
			}
		}
		#endregion
		#region AmountFormula
		public abstract class amountFormula : PX.Data.IBqlField
		{
		}
		protected String _AmountFormula;
		[PXDBString(4000, IsUnicode = true)]
		[PXUIField(DisplayName = "Amount Formula")]
		public virtual String AmountFormula
		{
			get
			{
				return this._AmountFormula;
			}
			set
			{
				this._AmountFormula = value;
			}
		}
		#endregion
		#region DescriptionFormula
		public abstract class descriptionFormula : PX.Data.IBqlField
		{
		}
		protected String _DescriptionFormula;
		[PXDBString(4000, IsUnicode = true)]
		[PXUIField(DisplayName = "Description Formula")]
		public virtual String DescriptionFormula
		{
			get
			{
				return this._DescriptionFormula;
			}
			set
			{
				this._DescriptionFormula = value;
			}
		}
		#endregion
		#region RangeStart
		public abstract class rangeStart : PX.Data.IBqlField
		{
		}
		protected Int32? _RangeStart;
		[PXDBInt()]
		[PXUIField(DisplayName = "Range Start")]
		public virtual Int32? RangeStart
		{
			get
			{
				return this._RangeStart;
			}
			set
			{
				this._RangeStart = value;
			}
		}
		#endregion
		#region RangeEnd
		public abstract class rangeEnd : PX.Data.IBqlField
		{
		}
		protected Int32? _RangeEnd;
		[PXDBInt()]
		[PXUIField(DisplayName = "Range End")]
		public virtual Int32? RangeEnd
		{
			get
			{
				return this._RangeEnd;
			}
			set
			{
				this._RangeEnd = value;
			}
		}
		#endregion
        #region RateTypeID
        public abstract class rateTypeID : PX.Data.IBqlField
		{
		}
		protected String _RateTypeID;
		[PXDBString(PMRateType.rateTypeID.Length, IsUnicode = true)]
		[PXSelector(typeof(PMRateType.rateTypeID))]
		[PXUIField(DisplayName="Rate Type")]
        public virtual String RateTypeID
		{
			get
			{
                return this._RateTypeID;
			}
			set
			{
                this._RateTypeID = value;
			}
		}
		#endregion
		#region AccountGroupFrom
		public abstract class accountGroupFrom : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupFrom;
		[AccountGroup(DisplayName="Account Group From")]
		public virtual Int32? AccountGroupFrom
		{
			get
			{
				return this._AccountGroupFrom;
			}
			set
			{
				this._AccountGroupFrom = value;
			}
		}
		#endregion
		#region AccountGroupTo
		public abstract class accountGroupTo : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupTo;
		[AccountGroup(DisplayName = "Account Group To")]
		public virtual Int32? AccountGroupTo
		{
			get
			{
				return this._AccountGroupTo;
			}
			set
			{
				this._AccountGroupTo = value;
			}
		}
		#endregion
        #region Method
        public abstract class method : PX.Data.IBqlField
        {
        }
        protected String _Method;
        [PMMethod.List]
        [PXDefault(PMMethod.Transaction)]
        [PXDBString(1)]
        [PXUIField(DisplayName = "Allocation Method")]
        public virtual String Method
        {
            get
            {
                return this._Method;
            }
            set
            {
                this._Method = value;
            }
        }
        #endregion
		#region UpdateGL
		public abstract class updateGL : PX.Data.IBqlField
		{
		}
		protected Boolean? _UpdateGL;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Post Transaction to GL")]
		public virtual Boolean? UpdateGL
		{
			get
			{
				return this._UpdateGL;
			}
			set
			{
				this._UpdateGL = value;
			}
		}
		#endregion

		#region BranchOrigin
		public abstract class branchOrigin : PX.Data.IBqlField
		{
		}
		protected String _BranchOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Transaction Branch")]
		public virtual String BranchOrigin
		{
			get
			{
				return this._BranchOrigin;
			}
			set
			{
				this._BranchOrigin = value;
			}
		}
		#endregion
		#region SourceBranchID
		public abstract class sourceBranchID : PX.Data.IBqlField
		{
		}
		protected Int32? _SourceBranchID;
		[Branch(DisplayName = "Source Branch ID", IsDetail = true, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? SourceBranchID
		{
			get
			{
				return this._SourceBranchID;
			}
			set
			{
				this._SourceBranchID = value;
			}
		}
		#endregion
		#region OffsetBranchOrigin
		public abstract class offsetBranchOrigin : PX.Data.IBqlField
		{
		}
		protected String _OffsetBranchOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Transaction Branch")]
		public virtual String OffsetBranchOrigin
		{
			get
			{
				return this._OffsetBranchOrigin;
			}
			set
			{
				this._OffsetBranchOrigin = value;
			}
		}
		#endregion
		#region TargetBranchID
		public abstract class targetBranchID : PX.Data.IBqlField
		{
		}
		protected Int32? _TargetBranchID;
		[Branch(DisplayName = "Target Branch ID", IsDetail = true, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? TargetBranchID
		{
			get
			{
				return this._TargetBranchID;
			}
			set
			{
				this._TargetBranchID = value;
			}
		}
		#endregion
		#region ProjectOrigin
		public abstract class projectOrigin : PX.Data.IBqlField
		{
		}
		protected String _ProjectOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Project")]
		public virtual String ProjectOrigin
		{
			get
			{
				return this._ProjectOrigin;
			}
			set
			{
				this._ProjectOrigin = value;
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[Project]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region TaskOrigin
		public abstract class taskOrigin : PX.Data.IBqlField
		{
		}
		protected String _TaskOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Task")]
		public virtual String TaskOrigin
		{
			get
			{
				return this._TaskOrigin;
			}
			set
			{
				this._TaskOrigin = value;
			}
		}
		#endregion
		#region TaskID
		public abstract class taskID : PX.Data.IBqlField
		{
		}
		protected Int32? _TaskID;
		[ProjectTask(typeof(PMAllocationStep.projectID), AllowNull=true )]
		public virtual Int32? TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				this._TaskID = value;
			}
		}
		#endregion
		#region AccountGroupOrigin
		public abstract class accountGroupOrigin : PX.Data.IBqlField
		{
		}
		protected String _AccountGroupOrigin;
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Account Group")]
		public virtual String AccountGroupOrigin
		{
			get
			{
				return this._AccountGroupOrigin;
			}
			set
			{
				this._AccountGroupOrigin = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[AccountGroup(typeof(Where<Current<PMAllocationStep.updateGL>, Equal<True>,
		And<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>,
		Or<Current<PMAllocationStep.updateGL>, Equal<False>>>>), DisplayName = "Account Group")]
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
		#region AccountOrigin
		public abstract class accountOrigin : PX.Data.IBqlField
		{
		}
		protected String _AccountOrigin;
		[PMOrigin.DebitAccountListAttribute]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Account Origin")]
		public virtual String AccountOrigin
		{
			get
			{
				return this._AccountOrigin;
			}
			set
			{
				this._AccountOrigin = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, typeof(Search<Account.accountID, Where<Account.accountGroupID, IsNotNull>>))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubMask
		public abstract class subMask : PX.Data.IBqlField
		{
		}
		protected String _SubMask;
		[PMSubAccountMask(DisplayName = "Subaccount")]
		public virtual String SubMask
		{
			get
			{
				return this._SubMask;
			}
			set
			{
				this._SubMask = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(typeof(PMAllocationStep.accountID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		
		#region OffsetProjectOrigin
		public abstract class offsetProjectOrigin : PX.Data.IBqlField
		{
		}
		protected String _OffsetProjectOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Project")]
		public virtual String OffsetProjectOrigin
		{
			get
			{
				return this._OffsetProjectOrigin;
			}
			set
			{
				this._OffsetProjectOrigin = value;
			}
		}
		#endregion
		#region OffsetProjectID
		public abstract class offsetProjectID : PX.Data.IBqlField
		{
		}
		protected Int32? _OffsetProjectID;
		[Project(DisplayName="Project")]
		public virtual Int32? OffsetProjectID
		{
			get
			{
				return this._OffsetProjectID;
			}
			set
			{
				this._OffsetProjectID = value;
			}
		}
		#endregion
		#region OffsetTaskOrigin
		public abstract class offsetTaskOrigin : PX.Data.IBqlField
		{
		}
		protected String _OffsetTaskOrigin;
		[PMOrigin.List]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Task")]
		public virtual String OffsetTaskOrigin
		{
			get
			{
				return this._OffsetTaskOrigin;
			}
			set
			{
				this._OffsetTaskOrigin = value;
			}
		}
		#endregion
		#region OffsetTaskID
		public abstract class offsetTaskID : PX.Data.IBqlField
		{
		}
		protected Int32? _OffsetTaskID;
		[ProjectTask(typeof(PMAllocationStep.offsetProjectID), AllowNull=true, DisplayName="Project Task")]
		public virtual Int32? OffsetTaskID
		{
			get
			{
				return this._OffsetTaskID;
			}
			set
			{
				this._OffsetTaskID = value;
			}
		}
		#endregion
		#region OffsetAccountGroupOrigin
		public abstract class offsetAccountGroupOrigin : PX.Data.IBqlField
		{
		}
		protected String _OffsetAccountGroupOrigin;
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Account Group")]
		public virtual String OffsetAccountGroupOrigin
		{
			get
			{
				return this._OffsetAccountGroupOrigin;
			}
			set
			{
				this._OffsetAccountGroupOrigin = value;
			}
		}
		#endregion
		#region OffsetAccountGroupID
		public abstract class offsetAccountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _OffsetAccountGroupID;
		[AccountGroup(typeof(Where<Current<PMAllocationStep.updateGL>, Equal<True>,
		And<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>,
		Or<Current<PMAllocationStep.updateGL>, Equal<False>>>>), DisplayName = "Account Group")]
		public virtual Int32? OffsetAccountGroupID
		{
			get
			{
				return this._OffsetAccountGroupID;
			}
			set
			{
				this._OffsetAccountGroupID = value;
			}
		}
		#endregion
		#region OffsetAccountOrigin
		public abstract class offsetAccountOrigin : PX.Data.IBqlField
		{
		}
		protected String _OffsetAccountOrigin;
		[PMOrigin.CreditAccountListAttribute]
		[PXDefault(PMOrigin.Source)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Account Origin")]
		public virtual String OffsetAccountOrigin
		{
			get
			{
				return this._OffsetAccountOrigin;
			}
			set
			{
				this._OffsetAccountOrigin = value;
			}
		}
		#endregion
		#region OffsetAccountID
		public abstract class offsetAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _OffsetAccountID;
		[Account(null, typeof(Search<Account.accountID, Where<Account.accountGroupID, IsNotNull>>), DisplayName = "Account")]
		public virtual Int32? OffsetAccountID
		{
			get
			{
				return this._OffsetAccountID;
			}
			set
			{
				this._OffsetAccountID = value;
			}
		}
		#endregion
		#region OffsetSubMask
		public abstract class offsetSubMask : PX.Data.IBqlField
		{
		}
		protected String _OffsetSubMask;
		[PMSubAccountMask(DisplayName = "Subaccount")]
		public virtual String OffsetSubMask
		{
			get
			{
				return this._OffsetSubMask;
			}
			set
			{
				this._OffsetSubMask = value;
			}
		}
		#endregion
		#region OffsetSubID
		public abstract class offsetSubID : PX.Data.IBqlField
		{
		}
		protected Int32? _OffsetSubID;
		[SubAccount(typeof(PMAllocationStep.offsetAccountID), DisplayName="Subaccount")]
		public virtual Int32? OffsetSubID
		{
			get
			{
				return this._OffsetSubID;
			}
			set
			{
				this._OffsetSubID = value;
			}
		}
		#endregion

		#region Reverse
		public abstract class reverse : PX.Data.IBqlField
		{
		}
		protected String _Reverse;
		[PMReverse.List]
		[PXDefault(PMReverse.OnInvoice)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Reverse Allocation")]
		public virtual String Reverse
		{
			get
			{
				return this._Reverse;
			}
			set
			{
				this._Reverse = value;
			}
		}
		#endregion
		#region NoRateOption
		public abstract class noRateOption : PX.Data.IBqlField
		{
		}
		protected String _NoRateOption;
		[PMNoRateOption.List]
		[PXDefault(PMNoRateOption.SetOne)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "If @Rate is not defined")]
		public virtual String NoRateOption
		{
			get
			{
				return this._NoRateOption;
			}
			set
			{
				this._NoRateOption = value;
			}
		}
		#endregion
		#region DateSource
		public abstract class dateSource : PX.Data.IBqlField
		{
		}
		protected String _DateSource;
		[PMDateSource.List]
		[PXDefault(PMDateSource.Transaction)]
		[PXDBString(1)]
		[PXUIField(DisplayName = "Date Source")]
		public virtual String DateSource
		{
			get
			{
				return this._DateSource;
			}
			set
			{
				this._DateSource = value;
			}
		}
		#endregion
		
		#region GroupByItem
		public abstract class groupByItem : PX.Data.IBqlField
		{
		}
		protected Boolean? _GroupByItem;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "By Item")]
		public virtual Boolean? GroupByItem
		{
			get
			{
				return this._GroupByItem;
			}
			set
			{
				this._GroupByItem = value;
			}
		}
		#endregion
		#region GroupByEmployee
		public abstract class groupByEmployee : PX.Data.IBqlField
		{
		}
		protected Boolean? _GroupByEmployee;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "By Employee")]
		public virtual Boolean? GroupByEmployee
		{
			get
			{
				return this._GroupByEmployee;
			}
			set
			{
				this._GroupByEmployee = value;
			}
		}
		#endregion
		#region GroupByDate
		public abstract class groupByDate : PX.Data.IBqlField
		{
		}
		protected Boolean? _GroupByDate;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "By Date")]
		public virtual Boolean? GroupByDate
		{
			get
			{
				return this._GroupByDate;
			}
			set
			{
				this._GroupByDate = value;
			}
		}
		#endregion
		#region GroupByVendor
		public abstract class groupByVendor : PX.Data.IBqlField
		{
		}
		protected Boolean? _GroupByVendor;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "By Vendor")]
		public virtual Boolean? GroupByVendor
		{
			get
			{
				return this._GroupByVendor;
			}
			set
			{
				this._GroupByVendor = value;
			}
		}
		#endregion
		#region FullDetail
		public virtual Boolean FullDetail
		{
			get
			{
				return GroupByItem != true && GroupByEmployee != true && GroupByDate != true && GroupByVendor != true;
			}
		}
		#endregion

		#region Allocation
		public abstract class allocation : PX.Data.IBqlField
		{
		}
		protected int? _Allocation;
		[PXInt]
		[PXUIField(DisplayName = "Allocation")]
		public virtual int? Allocation
		{
			get
			{
				return StepID;
			}
			set
			{
			}
		}
		#endregion
		#region AllocationText
		public abstract class allocationText : PX.Data.IBqlField
		{
		}
		protected String _AllocationText;
		[PXString(10)]
		public virtual String AllocationText
		{
			get
			{
				return this._AllocationText;
			}
			set
			{
				this._AllocationText = value;
			}
		}
		#endregion
		
		#region AllocateZeroAmount
		public abstract class allocateZeroAmount : PX.Data.IBqlField
		{
		}
		protected Boolean? _AllocateZeroAmount;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Create Transaction with Zero Amount")]
		public virtual Boolean? AllocateZeroAmount
		{
			get
			{
				return this._AllocateZeroAmount;
			}
			set
			{
				this._AllocateZeroAmount = value;
			}
		}
		#endregion
		#region AllocateZeroQty
		public abstract class allocateZeroQty : PX.Data.IBqlField
		{
		}
		protected Boolean? _AllocateZeroQty;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Create Transaction with Zero Qty.")]
		public virtual Boolean? AllocateZeroQty
		{
			get
			{
				return this._AllocateZeroQty;
			}
			set
			{
				this._AllocateZeroQty = value;
			}
		}
		#endregion
		#region AllocateNonBillable
		public abstract class allocateNonBillable : PX.Data.IBqlField
		{
		}
		protected Boolean? _AllocateNonBillable;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Create Non-Billable Transaction")]
		public virtual Boolean? AllocateNonBillable
		{
			get
			{
				return this._AllocateNonBillable;
			}
			set
			{
				this._AllocateNonBillable = value;
			}
		}
		#endregion
		

		#region CopyNotes
		public abstract class copyNotes : PX.Data.IBqlField
		{
		}
		protected Boolean? _CopyNotes;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Copy Notes", Visibility=PXUIVisibility.Visible, Visible=false)]
		public virtual Boolean? CopyNotes
		{
			get
			{
				return this._CopyNotes;
			}
			set
			{
				this._CopyNotes = value;
			}
		}
		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Int64? _NoteID;
        [PXNote]
		public virtual Int64? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
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
		#endregion
	}


	public static class PMOrigin
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Source, Change },
				new string[] { Messages.Origin_Source, Messages.Origin_Change }) { ; }
		}

		public class DebitAccountListAttribute : PXStringListAttribute
		{
			public DebitAccountListAttribute()
				: base(
				new string[] { Source, Change, OtherSource },
				new string[] { Messages.Origin_Source, Messages.Origin_Change, Messages.Origin_CreditSource }) { ; }
		}

		public class CreditAccountListAttribute : PXStringListAttribute
		{
			public CreditAccountListAttribute()
				: base(
				new string[] { Source, Change, OtherSource },
				new string[] { Messages.Origin_Source, Messages.Origin_Change, Messages.Origin_DebitSource }) { ; }
		}

		/// <summary>
		/// List of available Account Group sources. 
		/// Account Group can be taken either from Source object, from Account or specified directly.
		/// </summary>
		public class AccountGroupListAttribute : PXStringListAttribute
		{
			public AccountGroupListAttribute()
				: base(
				new string[] { Source, Change, FromAccount },
				new string[] { Messages.Origin_Source, Messages.Origin_Change, Messages.Origin_FromAccount }) { ; }
		}
				
		public const string Source = "S";
		public const string Change = "C";
		public const string FromAccount = "F";
		public const string None = "N";
		public const string OtherSource = "X";
	}


    public static class PMMethod
    {
        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute()
                : base(
                new string[] { Transaction, Budget },
                new string[] { Messages.PMMethod_Transaction, Messages.PMMethod_Budget }) { ; }
        }

        public const string Transaction = "T";
        public const string Budget = "B";

        public class transaction : Constant<string>
        {
            public transaction() : base(Transaction) { ;}
        }

        public class budget : Constant<string>
        {
            public budget() : base(Budget) { ;}
        }

    }



	public static class PMReverse
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { OnInvoice, OnBilling, Never },
				new string[] { Messages.PMReverse_OnInvoice, Messages.PMReverse_OnBilling, Messages.PMReverse_Never }) { ; }
		}

		public const string OnInvoice = "I";
		public const string OnBilling = "B";
		public const string Never = "N";

	}

	public static class PMNoRateOption
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { SetOne, SetZero, RaiseError, DontAllocate },
				new string[] { Messages.PMNoRateOption_SetOne, Messages.PMNoRateOption_SetZero, Messages.PMNoRateOption_RaiseError, Messages.PMNoRateOption_NoAllocate }) { ; }
		}

		public const string SetOne = "1";
		public const string SetZero = "0";
		public const string RaiseError = "E";
		public const string DontAllocate = "N";

	}

	public static class PMDateSource
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Transaction, Allocation },
				new string[] { Messages.PMDateSource_Transaction, Messages.PMDateSource_Allocation }) { ; }
		}

		public const string Transaction = "T";
		public const string Allocation = "A";
	}

	public static class PMSelectOption
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Transaction, Step },
				new string[] { Messages.PMSelectOption_Transaction, Messages.PMSelectOption_Step }) { ; }
		}

		public const string Transaction = "T";
		public const string Step = "S";

		public class transaction : Constant<string>
		{
			public transaction() : base(Transaction) { ;}
		}

		public class step : Constant<string>
		{
			public step() : base(Step) { ;}
		}

	}


}
