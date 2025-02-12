using PX.SM;

namespace PX.Objects.RQ
{
	using System;
	using PX.Data;
	using PX.Objects.AP;
	using PX.Objects.CS;
	using PX.Objects.CR;
	using PX.Objects.CM;
	using PX.Objects.IN;
	using PX.Objects.AR;
	using PX.TM;
	using PX.Objects.EP;
	using PX.Objects.PO;	

	[System.SerializableAttribute()]
	[PXPrimaryGraph(typeof(RQRequestEntry))]
	[PXCacheName(Messages.RQRequest)]
	[PXEMailSource]
	public partial class RQRequest : PX.Data.IBqlTable, PX.Data.EP.IAssign
	{
		#region Selected
		public abstract class selected : PX.Data.IBqlField
		{
		}
		protected bool? _Selected = false;
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
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
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[GL.Branch()]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region OrderNbr
		public abstract class orderNbr : PX.Data.IBqlField
		{
		}
		protected String _OrderNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]		
		[AutoNumber(
			 typeof(RQSetup.requestNumberingID), 
			 typeof(RQRequest.orderDate))]
		[PXSelectorAttribute(
			typeof(Search2<RQRequest.orderNbr,
				LeftJoin<Customer, On<Customer.bAccountID, Equal<RQRequest.employeeID>>>,
				Where<Customer.bAccountID, IsNull,
									Or<Match<Customer, Current<AccessInfo.userName>>>>>),
			typeof(RQRequest.orderNbr),
			typeof(RQRequest.orderDate),
			typeof(RQRequest.status),
			typeof(RQRequest.employeeID),
			typeof(RQRequest.departmentID),
			typeof(RQRequest.locationID),
			Filterable = true)]
		[PX.Data.EP.PXFieldDescription]
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
		#region OrderDate
		public abstract class orderDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _OrderDate;

		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? OrderDate
		{
			get
			{
				return this._OrderDate;
			}
			set
			{
				this._OrderDate = value;
			}
		}
		#endregion				
		#region ReqClassID
		public abstract class reqClassID : PX.Data.IBqlField
		{
		}
		protected string _ReqClassID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault(typeof(RQSetup.defaultReqClassID))]
		[PXUIField(DisplayName = "Request Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(RQRequestClass.reqClassID), DescriptionField = typeof(RQRequestClass.descr))]
		public virtual string ReqClassID
		{
			get
			{
				return this._ReqClassID;
			}
			set
			{
				this._ReqClassID = value;
			}
		}
		#endregion
		#region Priority
		public abstract class priority : PX.Data.IBqlField
		{
		}
		protected Int32? _Priority;
		[PXDBInt]
		[PXUIField]
		[PXDefault(1)]
		[PXIntList(new int[] { 0, 1, 2 },
			new string[] { "Low", "Normal", "High" })]		
		public virtual Int32? Priority
		{
			get
			{
				return this._Priority;
			}
			set
			{
				this._Priority = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(RQRequestStatus.Hold)]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[RQRequestStatus.List()]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
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
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		protected String _FinPeriodID;
		[APOpenPeriod(typeof(RQRequest.orderDate), ValidatePeriod = PX.Objects.GL.PeriodValidation.Nothing)]
		[PXUIField(DisplayName = "Fin. Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String FinPeriodID
		{
			get
			{
				return this._FinPeriodID;
			}
			set
			{
				this._FinPeriodID = value;
			}
		}
		#endregion
		
		#region Hold
		public abstract class hold : PX.Data.IBqlField
		{
		}
		protected Boolean? _Hold;
		[PXDBBool()]
		[PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
		[PXDefault(true)]
		[PXNoUpdate]
		public virtual Boolean? Hold
		{
			get
			{
				return this._Hold;
			}
			set
			{
				this._Hold = value;
			}
		}
		#endregion
		#region Approved
		public abstract class approved : PX.Data.IBqlField
		{
		}
		protected Boolean? _Approved;
		[PXDBBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Approved", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Boolean? Approved
		{
			get
			{
				return this._Approved;
			}
			set
			{
				this._Approved = value;
			}
		}
		#endregion
		#region Rejected
		public abstract class rejected : IBqlField
		{
		}
		protected bool? _Rejected = false;
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Reject", Visibility = PXUIVisibility.Visible)]
		public bool? Rejected
		{
			get
			{
				return _Rejected;
			}
			set
			{
				_Rejected = value;
			}
		}
		#endregion
		#region Cancelled
		public abstract class cancelled : PX.Data.IBqlField
		{
		}
		protected Boolean? _Cancelled;
		[PXDBBool()]
		[PXUIField(DisplayName = "Cancel", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false)]
		public virtual Boolean? Cancelled
		{
			get
			{
				return this._Cancelled;
			}
			set
			{
				this._Cancelled = value;
			}
		}
		#endregion		

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
		#region VendorHidden
		public abstract class vendorHidden : PX.Data.IBqlField
		{
		}
		protected bool? _VendorHidden;
		[PXBool()]
		[PXUIField(DisplayName = "Vendor Hidden", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? VendorHidden
		{
			get
			{
				return this._VendorHidden;
			}
			set
			{
				this._VendorHidden = value;
			}
		}
		#endregion		
		#region CustomerRequest
		public abstract class customerRequest : PX.Data.IBqlField
		{
		}
		protected bool? _CustomerRequest;
		[PXBool()]
		[PXUIField(DisplayName = "Customer Request", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? CustomerRequest
		{
			get
			{
				return this._CustomerRequest;
			}
			set
			{
				this._CustomerRequest = value;
			}
		}
		#endregion		
		#region BudgetValidation
		public abstract class budgetValidation : PX.Data.IBqlField
		{
		}
		protected bool? _BudgetValidation;
		[PXBool()]
		[PXUIField(DisplayName = "Budget Validation", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? BudgetValidation
		{
			get
			{
				return this._BudgetValidation;
			}
			set
			{
				this._BudgetValidation = value;
			}
		}
		#endregion		
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[VendorNonEmployeeActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true)]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region VendorLocationID
		public abstract class vendorLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorLocationID;
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<RQRequest.vendorID>>>), DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(typeof(Search<Vendor.defLocationID, Where<Vendor.bAccountID, Equal<Current<RQRequest.vendorID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? VendorLocationID
		{
			get
			{
				return this._VendorLocationID;
			}
			set
			{
				this._VendorLocationID = value;
			}
		}
		#endregion				

		#region WorkgroupID
		public abstract class workgroupID : PX.Data.IBqlField
		{
		}
		protected int? _WorkgroupID;
		[PXDBInt]
		[PXUIField(DisplayName = "Workgroup", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSubordinateGroupSelectorAttribute]
		public virtual int? WorkgroupID
		{
			get
			{
				return this._WorkgroupID;
			}
			set
			{
				this._WorkgroupID = value;
			}
		}
		#endregion
		#region OwnerID
		public abstract class ownerID : IBqlField
		{
		}
		protected Guid? _OwnerID;
		[PXDBGuid()]
		[PXDefault(typeof(Vendor.ownerID), PersistingCheck = PXPersistingCheck.Nothing)]
		[PX.TM.PXOwnerSelector(typeof(RQRequisition.workgroupID))]
		[PXUIField(DisplayName = "Owner", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Guid? OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				this._OwnerID = value;
			}
		}
		#endregion
		#region CheckBudget
		public abstract class checkBudget : PX.Data.IBqlField
		{
		}
		protected Boolean? _CheckBudget;
		[PXDBBool()]
		[PXUIField(DisplayName = "Check Budget", Visibility = PXUIVisibility.Visible)]
		[PXDefault(false)]
		public virtual Boolean? CheckBudget
		{
			get
			{
				return this._CheckBudget;
			}
			set
			{
				this._CheckBudget = value;
			}
		}
		#endregion		

		#region ShipDestType
		public abstract class shipDestType : PX.Data.IBqlField
		{
		}
		protected String _ShipDestType;
		[PXDBString(1, IsFixed = true)]
		[POShippingDestination.ListSimple()]
		[PXDefault(POShippingDestination.CompanyLocation)]
		[PXUIField(DisplayName = "Shipping Destination Type")]
		public virtual String ShipDestType
		{
			get
			{
				return this._ShipDestType;
			}
			set
			{
				this._ShipDestType = value;
			}
		}
		#endregion		
		#region ShipToBAccountID
		public abstract class shipToBAccountID : PX.Data.IBqlField
		{
		}
		protected Int32? _ShipToBAccountID;
		[PXDBInt()]
		[PXSelector(typeof(Search2<BAccount2.bAccountID,
			LeftJoin<Vendor,
					  On<Vendor.bAccountID, Equal<BAccount2.bAccountID>,
					 And<Vendor.type,NotEqual<BAccountType.employeeType>>>,
			LeftJoin<AR.Customer, On<AR.Customer.bAccountID, Equal<BAccount2.bAccountID>>,
			LeftJoin<GL.Branch, On<GL.Branch.bAccountID, Equal<BAccount2.bAccountID>>>>>,
			 Where<Vendor.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.vendor>,
			Or<Where<GL.Branch.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.company>,
			Or<Where<AR.Customer.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.customer>>>
				>>>>>>>),
				typeof(BAccount.acctCD), typeof(BAccount.acctName), typeof(BAccount.type), typeof(BAccount.acctReferenceNbr), typeof(BAccount.parentBAccountID),
			SubstituteKey = typeof(BAccount.acctCD), DescriptionField = typeof(BAccount.acctName))]
		[PXUIField(DisplayName = "Ship To")]
		[PXDefault((object)null, typeof(Search<GL.Branch.bAccountID, Where<Branch.branchID,Equal<Current<AccessInfo.branchID>>, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.company>>>>))]
		public virtual Int32? ShipToBAccountID
		{
			get
			{
				return this._ShipToBAccountID;
			}
			set
			{
				this._ShipToBAccountID = value;
			}
		}
		#endregion
		#region ShipToLocationID
		public abstract class shipToLocationID : PX.Data.IBqlField
		{
		}
		protected Int32? _ShipToLocationID;

		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>>), DescriptionField = typeof(Location.descr))]
		[PXDefault((object)null, typeof(Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>>>))]
		[PXUIField(DisplayName = "Shipping Location")]
		public virtual Int32? ShipToLocationID
		{
			get
			{
				return this._ShipToLocationID;
			}
			set
			{
				this._ShipToLocationID = value;
			}
		}
		#endregion
		#region ShipAddressID
		public abstract class shipAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _ShipAddressID;
		[PXDBInt()]
		[POShipAddress(typeof(Select2<Address,
					LeftJoin<Location, On<Address.bAccountID, Equal<Location.bAccountID>,
						And<Address.addressID, Equal<Location.defAddressID>,
						And<Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>,
						And<Location.locationID, Equal<Current<RQRequest.shipToLocationID>>>>>>,
					LeftJoin<POShipAddress, On<POShipAddress.bAccountID, Equal<Address.bAccountID>,
						And<POShipAddress.bAccountAddressID, Equal<Address.addressID>,
						And<POShipAddress.revisionID, Equal<Address.revisionID>,
						And<POShipAddress.isDefaultAddress, Equal<boolTrue>>>>>>>,
					Where<Location.locationCD, IsNotNull>>))]
		[PXUIField()]
		public virtual Int32? ShipAddressID
		{
			get
			{
				return this._ShipAddressID;
			}
			set
			{
				this._ShipAddressID = value;
			}
		}
		#endregion
		#region ShipContactID
		public abstract class shipContactID : PX.Data.IBqlField
		{
		}
		protected Int32? _ShipContactID;
		[PXDBInt()]
		[POShipContactAttribute(typeof(Select2<Contact,
					LeftJoin<Location, On<Contact.bAccountID, Equal<Location.bAccountID>,
						And<Contact.contactID, Equal<Location.defContactID>,
						And<Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>,
						And<Location.locationID, Equal<Current<RQRequest.shipToLocationID>>>>>>,
					LeftJoin<POShipContact, On<POShipContact.bAccountID, Equal<Contact.bAccountID>,
						And<POShipContact.bAccountContactID, Equal<Contact.contactID>,
						And<POShipContact.revisionID, Equal<Contact.revisionID>,
						And<POShipContact.isDefaultContact, Equal<boolTrue>>>>>>>,
					Where<Location.locationCD, IsNotNull>>))]
		[PXUIField()]
		public virtual Int32? ShipContactID
		{
			get
			{
				return this._ShipContactID;
			}
			set
			{
				this._ShipContactID = value;
			}
		}
		#endregion				
		#region RemitAddressID
		public abstract class remitAddressID : PX.Data.IBqlField
		{
		}
		protected Int32? _RemitAddressID;
		[PXDBInt()]
		[PORemitAddress(typeof(Select2<BAccount2,
			InnerJoin<Location, On<Location.bAccountID, Equal<BAccount2.bAccountID>>,
			InnerJoin<Address, On<Address.bAccountID, Equal<Location.bAccountID>, And<Address.addressID, Equal<Location.defAddressID>>>,
			LeftJoin<PORemitAddress, On<PORemitAddress.bAccountID, Equal<Address.bAccountID>,
						And<PORemitAddress.bAccountAddressID, Equal<Address.addressID>,
				And<PORemitAddress.revisionID, Equal<Address.revisionID>, And<PORemitAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>,
			Where<Location.bAccountID, Equal<Current<RQRequest.vendorID>>, And<Location.locationID, Equal<Current<RQRequest.vendorLocationID>>>>>), Required = false)]
		[PXUIField()]
		public virtual Int32? RemitAddressID
		{
			get
			{
				return this._RemitAddressID;
			}
			set
			{
				this._RemitAddressID = value;
			}
		}
		#endregion
		#region RemitContactID
		public abstract class remitContactID : PX.Data.IBqlField
		{
		}
		protected Int32? _RemitContactID;
		[PXDBInt()]
		[PORemitContactAttribute(typeof(Select2<Location,
				InnerJoin<Contact, On<Contact.bAccountID, Equal<Location.bAccountID>, And<Contact.contactID, Equal<Location.defContactID>>>,
				LeftJoin<PORemitContact, On<PORemitContact.bAccountID, Equal<Contact.bAccountID>,
				And<PORemitContact.bAccountContactID, Equal<Contact.contactID>,
						And<PORemitContact.revisionID, Equal<Contact.revisionID>,
				And<PORemitContact.isDefaultContact, Equal<boolTrue>>>>>>>,
				Where<Location.bAccountID, Equal<Current<RQRequest.vendorID>>, And<Location.locationID, Equal<Current<RQRequest.vendorLocationID>>>>>), Required = false)]
		public virtual Int32? RemitContactID
		{
			get
			{
				return this._RemitContactID;
			}
			set
			{
				this._RemitContactID = value;
			}
		}
		#endregion

		#region EmployeeID
		public abstract class employeeID : PX.Data.IBqlField
		{
		}
		protected Int32? _EmployeeID;
		[PXDBInt()]
		[PXDefault(typeof(Search2<EPEmployee.bAccountID, 
			InnerJoin<RQRequestClass, 
			On<RQRequestClass.reqClassID, Equal<Current<RQRequest.reqClassID>>,
			And<RQRequestClass.customerRequest, NotEqual<boolTrue>>>>,
			Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
		//[PXSubordinateSelector]
		[RQRequesterSelector(typeof(RQRequest.reqClassID))]
		[PXUIField(DisplayName = "Requested By", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual Int32? EmployeeID
		{
			get
			{
				return this._EmployeeID;
			}
			set
			{
				this._EmployeeID = value;
			}
		}
		#endregion
		#region DepartmentID
		public abstract class departmentID : PX.Data.IBqlField
		{
		}
		protected String _DepartmentID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault(typeof(Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<RQRequest.employeeID>>>>), PersistingCheck= PXPersistingCheck.Nothing)]
		[PXSelector(typeof(EPDepartment.departmentID), DescriptionField = typeof(EPDepartment.description))]
		[PXUIField(DisplayName = "Department", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				this._DepartmentID = value;
			}
		}
		#endregion
		#region LocationID
		public abstract class locationID : IBqlField { }
		protected int? _LocationID;
		[PXDefault(typeof(Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<RQRequest.employeeID>>>>))]
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<RQRequest.employeeID>>>), 
													DescriptionField = typeof(Location.descr), 
													Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Location")]
		public int? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
	
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(typeof(Search<GL.Company.baseCuryID>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		protected Int64? _CuryInfoID;
		[PXDBLong()]
		[CurrencyInfo(ModuleCode = "PO")]
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
		#region OpenOrderQty
		public abstract class openOrderQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _OpenOrderQty;
		[PXDBQuantity()]
		[PXUIField(DisplayName = "Open Qty.", Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? OpenOrderQty
		{
			get
			{
				return this._OpenOrderQty;
			}
			set
			{
				this._OpenOrderQty = value;
			}
		}
		#endregion
		#region CuryEstExtCostTotal
		public abstract class curyEstExtCostTotal : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryEstExtCostTotal;

		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCurrency(typeof(RQRequest.curyInfoID), typeof(RQRequest.estExtCostTotal))]
		[PXUIField(DisplayName = "Est. Ext. Cost", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual Decimal? CuryEstExtCostTotal
		{
			get
			{
				return this._CuryEstExtCostTotal;
			}
			set
			{
				this._CuryEstExtCostTotal = value;
			}
		}
		#endregion
		#region EstExtCostTotal
		public abstract class estExtCostTotal : PX.Data.IBqlField
		{
		}
		protected Decimal? _EstExtCostTotal;
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? EstExtCostTotal
		{
			get
			{
				return this._EstExtCostTotal;
			}
			set
			{
				this._EstExtCostTotal = value;
			}
		}
		#endregion

		#region Purpose
		public abstract class purpose : PX.Data.IBqlField
		{
		}
		protected String _Purpose;
		[PXDBString(100, IsUnicode = true)]
		[PXUIField(DisplayName = "Purpose")]
		public virtual String Purpose
		{
			get
			{
				return this._Purpose;
			}
			set
			{
				this._Purpose = value;
			}
		}
		#endregion

		#region LineCntr
		public abstract class lineCntr : PX.Data.IBqlField
		{
		}
		protected int? _LineCntr;
		[PXDBInt()]
		[PXDefault(0)]
		public virtual int? LineCntr
		{
			get
			{
				return this._LineCntr;
			}
			set
			{
				this._LineCntr = value;
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

	public class RQRequestStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Hold, Open, PendingApproval, Canceled, Closed, Issued, Rejected },
				new string[] { Messages.Hold, Messages.Open, Messages.PendingApproval, Messages.Canceled, Messages.Closed, Messages.Issued, Messages.Rejected }) { ; }
		}

		public const string Hold = "H";
		public const string PendingApproval = "P";
		public const string Rejected = "R";
		public const string Open = "N";				
		public const string Closed = "C";
		public const string Issued = "I";		
		public const string Canceled = "L";



		public class hold : Constant<string>
		{
			public hold() : base(Hold) { ;}
		}		
		public class pendingApproval : Constant<string>
		{
			public pendingApproval() : base(PendingApproval) { ;}
		}
		public class rejected : Constant<string>
		{
			public rejected() : base(Rejected) { ;}
		}
		public class open : Constant<string>
		{
			public open() : base(Open) { ;}
		}
		public class closed : Constant<string>
		{
			public closed() : base(Closed) { ;}
		}
		public class canceled : Constant<string>
		{
			public canceled() : base(Canceled) { ;}
		}
		public class issued : Constant<string>
		{
			public issued() : base(Issued) { ;}
		}

	}
}
