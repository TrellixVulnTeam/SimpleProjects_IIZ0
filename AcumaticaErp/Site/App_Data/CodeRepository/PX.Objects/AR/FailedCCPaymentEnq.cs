using System;
using System.Collections.Generic;
using System.Collections;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CS;

namespace PX.Objects.AR
{
	[PX.Objects.GL.TableAndChartDashboardType]
    [Serializable]
	public class FailedCCPaymentEnq : PXGraph<FailedCCPaymentEnq>
	{
		#region Internal Types
		[Serializable]
		public partial class CCPaymentFilter : IBqlTable
		{
			#region BeginDate
			public abstract class beginDate : PX.Data.IBqlField
			{
			}

			protected DateTime? _BeginDate;

			[PXDBDate()]
			[PXDefault(typeof(AccessInfo.businessDate))]
			[PXUIField(DisplayName = "Start Date")]
			public virtual DateTime? BeginDate
			{
				get
				{
					return this._BeginDate;
				}
				set
				{
					this._BeginDate = value;
				}
			}
			#endregion
			#region EndDate
			public abstract class endDate : PX.Data.IBqlField
			{
			}

			protected DateTime? _EndDate;
			[PXDBDate()]
			[PXDefault(typeof(AccessInfo.businessDate))]
			[PXUIField(DisplayName = "End Date")]
			public virtual DateTime? EndDate
			{
				get
				{
					return this._EndDate;
				}
				set
				{
					this._EndDate = value;
				}
			}
			#endregion			
			#region CustomerClassID
			public abstract class customerClassID : PX.Data.IBqlField
			{
			}
			protected String _CustomerClassID;
			[PXDBString(10, IsUnicode = true)]
			[PXSelector(typeof(CustomerClass.customerClassID), DescriptionField = typeof(CustomerClass.descr), CacheGlobal = true)]
			[PXUIField(DisplayName = "Customer Class")]
			public virtual String CustomerClassID
			{
				get
				{
					return this._CustomerClassID;
				}
				set
				{
					this._CustomerClassID = value;
				}
			}
			#endregion
			#region ProcessingCenterID
			public abstract class processingCenterID : PX.Data.IBqlField
			{
			}
			protected String _ProcessingCenterID;
			[PXDBString(10, IsUnicode = true)]
			[PXSelector(typeof(Search<CCProcessingCenter.processingCenterID>),DescriptionField = typeof(CCProcessingCenter.name))]
			[PXUIField(DisplayName = "Proc. Center ID", Visibility = PXUIVisibility.SelectorVisible)]
			public virtual String ProcessingCenterID
			{
				get
				{
					return this._ProcessingCenterID;
				}
				set
				{
					this._ProcessingCenterID = value;
				}
			}
			#endregion
			#region CustomerID
			public abstract class customerID : PX.Data.IBqlField
			{
			}
			protected Int32? _CustomerID;
			[Customer(DescriptionField = typeof(Customer.acctName))]
			public virtual Int32? CustomerID
			{
				get
				{
					return this._CustomerID;
				}
				set
				{
					this._CustomerID = value;
				}
			}
			#endregion
			#region DisplayType
			public abstract class displayType: PX.Data.IBqlField
			{
			}
			protected String _DisplayType;
			[PXDBString(3,IsFixed=true,IsUnicode =false)]			
			[DisplayTypes.List()]
			[PXDefault(DisplayTypes.All)]
			[PXUIField(DisplayName ="Display Transactions")]
			public virtual String DisplayType
			{
				get
				{
					return this._DisplayType;
				}
				set
				{
					this._DisplayType = value;
				}
			}
			#endregion


		}
		private static class DisplayTypes
		{
			public const string All ="ALL";
			public const string Failed ="FLD";

			public class ListAttribute: PXStringListAttribute
			{
				public ListAttribute(): base(
					new string[] { All, Failed},
					new string[] { Messages.AllTransactions, Messages.FailedOnlyTransactions}) { ; }
			}
		}

        [Serializable]
		public partial class CCProcTranH : CCProcTran 
		{
			#region TranNbr
			public new abstract class tranNbr : PX.Data.IBqlField
			{
			}
			#endregion
			#region PMInstanceID
			public new abstract class pMInstanceID : PX.Data.IBqlField
			{
			}
			#endregion
			#region ProcessingCenterID
			public new abstract class processingCenterID : PX.Data.IBqlField
			{
			}
			#endregion
			#region DocType
			public new abstract class docType : PX.Data.IBqlField
			{
			}
			
			#endregion
			#region RefNbr
			public new abstract class refNbr : PX.Data.IBqlField
			{
			}
			#endregion
		}
		#endregion

		#region CTor + public Member Decalaration
		public PXFilter<CCPaymentFilter> Filter;
		public PXCancel<CCPaymentFilter> Cancel;
		public FailedCCPaymentEnq()
		{
			this.PaymentTrans.Cache.AllowInsert = false;
			this.PaymentTrans.Cache.AllowUpdate = false;
			this.PaymentTrans.Cache.AllowDelete = false;
		}

		#region Sub-screen Navigation Buttons
		public PXAction<CCPaymentFilter> viewDocument;
		[PXUIField(DisplayName = Messages.ViewDocument, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton()]
		public virtual IEnumerable ViewDocument(PXAdapter adapter)
		{
			CCProcTran tran = this.PaymentTrans.Current;
			if (tran != null)
			{
				PXGraph target = CCTransactionsHistoryEnq.FindSourceDocumentGraph(tran);
				if (target != null)
					throw new PXRedirectRequiredException(target, true, Messages.ViewDocument){Mode = PXBaseRedirectException.WindowMode.NewWindow};			
			}
			return Filter.Select();
		}

		public PXAction<CCPaymentFilter> viewCustomer;
		[PXUIField(DisplayName = Messages.ViewCustomer, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable ViewCustomer(PXAdapter adapter)
		{
			if (this.PaymentTrans.Current != null)
			{
				CCProcTran row = this.PaymentTrans.Current;
				CustomerPaymentMethod pmInstance = PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Select(this, row.PMInstanceID);
				if (pmInstance!= null)
				{
					CustomerMaint graph = PXGraph.CreateInstance<CustomerMaint>();
					graph.BAccount.Current = graph.BAccount.Search<Customer.bAccountID>(pmInstance.BAccountID);
					if (graph.BAccount.Current != null)
					{
						throw new PXRedirectRequiredException(graph, true, Messages.ViewDocument) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
					}
				}
			}
			return adapter.Get();
		}

		public PXAction<CCPaymentFilter> viewCreditCard;
		[PXUIField(DisplayName = Messages.ViewPaymentMethod, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton()]
		public virtual IEnumerable ViewCreditCard(PXAdapter adapter)
		{
			if (this.PaymentTrans.Current != null)
			{
				CCProcTran row = this.PaymentTrans.Current;
				CustomerPaymentMethod pmInstance = PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Select(this, row.PMInstanceID);
				CustomerPaymentMethodMaint graph = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
				graph.CustomerPaymentMethod.Current = graph.CustomerPaymentMethod.Search<CustomerPaymentMethod.pMInstanceID>(pmInstance.PMInstanceID, pmInstance.BAccountID);
				if (graph.CustomerPaymentMethod.Current != null)
				{
					throw new PXRedirectRequiredException(graph, true, Messages.ViewDocument) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}				
			}
			return Filter.Select();
		}


		#endregion

		[PXFilterable]
		public PXSelectJoin<CCProcTran,
									InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID,Equal<CCProcTran.pMInstanceID>>,
									InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>,
									LeftJoin<ARRegister, On<CCProcTran.refNbr, Equal<ARPayment.refNbr>,
											And<CCProcTran.docType, Equal<ARPayment.docType>>>,									
									LeftJoin <CCProcTranH, On<CCProcTranH.docType,Equal<CCProcTran.docType>,
											And<CCProcTranH.refNbr,Equal<CCProcTran.refNbr>,
											And<CCProcTranH.tranNbr,Greater<CCProcTran.tranNbr>>>>>>>>,
									Where<CCProcTran.startTime, GreaterEqual<Current<CCPaymentFilter.beginDate>>,
										And<CCProcTran.startTime, Less<Current<CCPaymentFilter.endDate>>,
										And<CCProcTranH.tranNbr, IsNull,
										And<Where<CCProcTran.tranStatus, NotEqual<CCTranStatusCode.approved>,
											Or<CCProcTran.tranStatus,IsNull>>>>>>,
									OrderBy<Desc<ARPayment.refNbr>>> PaymentTrans;
		#endregion

		#region Delegates
		public virtual IEnumerable paymentTrans()
		{

			CCPaymentFilter filter = this.Filter.Current;
			if (filter != null)
			{

				PXSelectBase<CCProcTran> select = new PXSelectJoin<CCProcTran,
									InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID,Equal<CCProcTran.pMInstanceID>>,
									InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>,
									LeftJoin<ARPayment, On<CCProcTran.refNbr, Equal<ARPayment.refNbr>,
											And<CCProcTran.docType, Equal<ARPayment.docType>>>,
									//LeftJoin<Customer, On<ARPayment.customerID, Equal<Customer.bAccountID>>,
									LeftJoin<CCProcTranH, On<CCProcTranH.docType, Equal<CCProcTran.docType>,
											And<CCProcTranH.refNbr, Equal<CCProcTran.refNbr>,
											And<CCProcTranH.tranNbr, Greater<CCProcTran.tranNbr>>>>>>>>,
									Where<CCProcTran.startTime, GreaterEqual<Required<CCPaymentFilter.beginDate>>,
										And<CCProcTran.startTime, LessEqual<Required<CCPaymentFilter.endDate>>>>,
									OrderBy<Desc<ARPayment.refNbr>>>(this);

				if (!string.IsNullOrEmpty(filter.ProcessingCenterID))
				{
					select.WhereAnd<Where<CCProcTran.processingCenterID, Equal<Current<CCPaymentFilter.processingCenterID>>>>();
				}

				if (!string.IsNullOrEmpty(filter.CustomerClassID))
				{
					select.WhereAnd<Where<Customer.customerClassID, Equal<Current<CCPaymentFilter.customerClassID>>>>();
				}

				if (filter.CustomerID.HasValue)
				{
					select.WhereAnd<Where<Customer.bAccountID, Equal<Current<CCPaymentFilter.customerID>>>>();					
				}

				if (filter.DisplayType == DisplayTypes.Failed) 
				{
					select.WhereAnd<Where<CCProcTranH.tranNbr, IsNull,
										And<Where<CCProcTran.tranStatus, NotEqual<CCTranStatusCode.approved>,
											Or<CCProcTran.tranStatus, IsNull>>>>>();
				}
				if (filter.EndDate != null)
				{
					foreach (PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment, CCProcTranH> it in select.Select(filter.BeginDate, filter.EndDate.Value.AddDays(1)))
					{
						ARPayment payment = it;
						CCProcTran ccTran = it;
						if (ccTran.TranType == CCTranTypeCode.Credit || ccTran.TranType == CCTranTypeCode.VoidTran)
						{
							ARPayment voiding = PXSelect<ARPayment, Where<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>,
								And<ARPayment.docType, Equal<ARDocType.voidPayment>>>>.Select(this, payment.RefNbr);
							if (voiding != null)
							{
								((ARPayment) it).DocType = ARDocType.VoidPayment;

							}
						}
						yield return it;
					}
				}

			}
			yield break;
		} 
		#endregion

		#region Filter Event Handlers

		protected virtual void CCPaymentFilter_BeginDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CCPaymentFilter row = (CCPaymentFilter)e.Row;
			if (row.BeginDate.HasValue && row.EndDate.HasValue && row.EndDate.Value < row.BeginDate.Value)
			{
				row.EndDate = row.BeginDate;
			}
		}

		protected virtual void CCPaymentFilter_EndDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			CCPaymentFilter row = (CCPaymentFilter)e.Row;
			if (row.BeginDate.HasValue && row.EndDate.HasValue && row.EndDate.Value < row.BeginDate.Value)
			{
				row.BeginDate = row.EndDate;
			}
		}
		
		#endregion
	}
}
