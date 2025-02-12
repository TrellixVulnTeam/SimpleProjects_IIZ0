using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.CCProcessing;
using PX.TM;
using ARCashSale = PX.Objects.AR.Standalone.ARCashSale;

namespace PX.Objects.AR
{
	public class ARCashSaleEntry : ARDataEntryGraph<ARCashSaleEntry, ARCashSale>
	{
		public PXSelect<InventoryItem> dummy_nonstockitem_for_redirect_newitem;

		public ToggleCurrency<ARCashSale> CurrencyView;
        [PXCopyPasteHiddenFields(typeof(ARCashSale.extRefNbr))]
		public PXSelectJoin<ARCashSale,
			LeftJoin<SOInvoice, On<SOInvoice.docType, Equal<ARCashSale.docType>, And<SOInvoice.refNbr, Equal<ARCashSale.refNbr>>>,
			LeftJoin<Customer, On<Customer.bAccountID, Equal<ARCashSale.customerID>>>>,
			Where<ARCashSale.docType, Equal<Optional<ARCashSale.docType>>,
			And2<Where<SOInvoice.refNbr, IsNull, Or<ARCashSale.released, Equal<boolTrue>>>,
			And<Where<Customer.bAccountID, IsNull,
			Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> Document;
		public PXSelect<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Current<ARCashSale.refNbr>>>>> CurrentDocument;

		public PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> Transactions;
		public PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARCashSale.docType>>, And<ARTax.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTax.tranType, Asc<ARTax.refNbr, Asc<ARTax.taxID>>>>> Tax_Rows;		
        public PXSelectJoin<ARTaxTran, InnerJoin<Tax, On<Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>>> Taxes;
		public PXSelect<ARAddress, Where<ARAddress.addressID, Equal<Current<ARCashSale.billAddressID>>>> Billing_Address;
		public PXSelect<ARContact, Where<ARContact.contactID, Equal<Current<ARCashSale.billContactID>>>> Billing_Contact;


		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<ARCashSale.curyInfoID>>>> currencyinfo;
		public PXSelect<CATran> dummy_CATran;


		public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;

		public PXSetup<Customer, Where<Customer.bAccountID, Equal<Optional<ARCashSale.customerID>>>> customer;
		public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>> customerclass;
		public PXSetup<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<ARCashSale.cashAccountID>>>> cashaccount;
		public PXSetup<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Current<ARCashSale.adjFinPeriodID>>>> finperiod;
		public PXSetup<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>>> paymentmethod;

		public PXSetup<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<ARCashSale.taxZoneID>>>> taxzone;
		public PXSetup<Location, Where<Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<Location.locationID, Equal<Optional<ARCashSale.customerLocationID>>>>> location;

		public PXSetup<GLSetup> glsetup;
		public PXSetup<ARSetup> arsetup;
		public CMSetupSelect cmsetup;

		
		public PXSelect<ARBalances> arbalances;

		public PXSelectReadonly<CCProcTran, Where<CCProcTran.refNbr, Equal<Current<ARCashSale.refNbr>>, And<CCProcTran.docType, Equal<Current<ARCashSale.docType>>,
											Or<Where<CCProcTran.refNbr, Equal<Current<ARCashSale.origRefNbr>>, And<CCProcTran.docType, Equal<Current<ARCashSale.origDocType>>>>>>>,
											OrderBy<Desc<CCProcTran.tranNbr>>> ccProcTran; //CC Transactions are available for both main and voiding document

		public PXSelect<CustSalesPeople, Where<CustSalesPeople.bAccountID, Equal<Current<ARCashSale.customerID>>,
												And<CustSalesPeople.locationID, Equal<Current<ARCashSale.customerLocationID>>>>> salesPerSettings;
		public PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Current<ARCashSale.docType>>,
												And<ARSalesPerTran.refNbr, Equal<Current<ARCashSale.refNbr>>, 
												And<ARSalesPerTran.adjdDocType, Equal<ARDocType.undefined>, 
												And2<Where<Current<ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byInvoice>, Or<Current<ARCashSale.released>, Equal<boolFalse>>>,
												Or<ARSalesPerTran.adjdDocType, Equal<Current<ARCashSale.docType>>,
												And<ARSalesPerTran.adjdRefNbr, Equal<Current<ARCashSale.refNbr>>, 
												And<Current<ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byPayment>>>>>>>>> salesPerTrans;

        public PXSelect<ARPaymentChargeTran,
                    Where<ARPaymentChargeTran.docType, Equal<Current<ARCashSale.docType>>,
                    And<ARPaymentChargeTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>> PaymentCharges;

		[CRBAccountReference(typeof(Select<Customer, Where<Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
		[CRDefaultMailTo(typeof(Select<ARContact, Where<ARContact.contactID, Equal<Current<ARCashSale.billContactID>>>>))]
		public CRActivityList<ARCashSale>
			Activity;


		public ARCashSaleEntry()
			: base()
		{
			{
				ARSetup record = arsetup.Select();
			}

			{
				GLSetup record = glsetup.Select();
			}
            FieldUpdated.AddHandler<ARCashSale.branchID>(ParentBranchUpdated);
			TaxAttribute.SetTaxCalc<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(Transactions.Cache, null, TaxCalc.ManualLineCalc);

            FieldDefaulting.AddHandler<InventoryItem.stkItem>((sender, e) => { if (e.Row != null) e.NewValue = false; });
		}

        protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
        {
            if (!sender.ObjectsEqual<ARCashSale.docDate, ARCashSale.finPeriodID, ARCashSale.curyID>(e.Row, e.OldRow))
            {
                foreach (ARTran tran in Transactions.Select())
                {
                    if (Transactions.Cache.GetStatus(tran) == PXEntryStatus.Notchanged)
                    {
                        Transactions.Cache.SetStatus(tran, PXEntryStatus.Updated);
                    }
                }
            }
        }

		#region Cache Attached
        #region InventoryItem
        #region COGSSubID
        [PXDefault(typeof(Search<INPostClass.cOGSSubID, Where<INPostClass.postClassID, Equal<Current<InventoryItem.postClassID>>>>))]
        [SubAccount(typeof(InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof(Sub.description))]
        public virtual void InventoryItem_COGSSubID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #endregion

		#region ARSalesPerTran
		[PXDBString(3, IsKey = true, IsFixed = true)]
		[PXDBDefault(typeof(ARRegister.docType))]
		protected virtual void ARSalesPerTran_DocType_CacheAttached(PXCache sender)
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(ARCashSale.refNbr))]
		[PXParent(typeof(Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARSalesPerTran.docType>>,
						 And<ARCashSale.refNbr, Equal<Current<ARSalesPerTran.refNbr>>>>>))]
		protected virtual void ARSalesPerTran_RefNbr_CacheAttached(PXCache sender)
		{
		}

        [PXDBInt()]
        [PXDBDefault(typeof(ARCashSale.branchID))]
        protected virtual void ARSalesPerTran_BranchID_CacheAttached(PXCache sender)
        {
        }
		[SalesPerson(DirtyRead = true, Enabled = false, IsKey = true, DescriptionField = typeof(Contact.displayName))]
		protected virtual void ARSalesPerTran_SalespersonID_CacheAttached(PXCache sender)
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0", typeof(Coalesce<Search<CustSalesPeople.commisionPct, Where<CustSalesPeople.bAccountID, Equal<Current<ARCashSale.customerID>>,
				And<CustSalesPeople.locationID, Equal<Current<ARCashSale.customerLocationID>>,
				And<CustSalesPeople.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>>,
			Search<SalesPerson.commnPct, Where<SalesPerson.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>))]
		[PXUIField(DisplayName = "Commission %")]
		protected virtual void ARSalesPerTran_CommnPct_CacheAttached(PXCache sender)
		{
		}
		[PXDBCurrency(typeof(ARSalesPerTran.curyInfoID), typeof(ARSalesPerTran.commnblAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
        [PXFormula(null, typeof(SumCalc<ARCashSale.curyCommnblAmt>))]
		protected virtual void ARSalesPerTran_CuryCommnblAmt_CacheAttached(PXCache sender)
		{
		}
		[PXDBCurrency(typeof(ARSalesPerTran.curyInfoID), typeof(ARSalesPerTran.commnAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXFormula(typeof(Mult<ARSalesPerTran.curyCommnblAmt, Div<ARSalesPerTran.commnPct, decimal100>>), typeof(SumCalc<ARCashSale.curyCommnAmt>))]
		[PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
		protected virtual void ARSalesPerTran_CuryCommnAmt_CacheAttached(PXCache sender)
		{
		}
		#endregion

        #region ARPaymentChargeTran
        #region LineNbr
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
        [PXLineNbr(typeof(ARCashSale.chargeCntr))]
        public virtual void ARPaymentChargeTran_LineNbr_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #region CashAccountID
        [PXDBInt()]
        [PXDefault(typeof(ARCashSale.cashAccountID))]
        [PXUIField(DisplayName = "Cash AccountID")]
        public virtual void ARPaymentChargeTran_CashAccountID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #region EntryTypeID
        [PXDBString(10, IsUnicode = true)]
        [PXDefault()]
        [PXSelector(typeof(Search2<CAEntryType.entryTypeId,
                            InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>,
                            Where<CashAccountETDetail.accountID, Equal<Current<ARCashSale.cashAccountID>>,
                            And<CAEntryType.drCr, Equal<CADrCr.cACredit>>>>))]
        [PXUIField(DisplayName = "Entry Type")]
        public virtual void ARPaymentChargeTran_EntryTypeID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #region TranDate
        [PXDBDate()]
        [PXDefault(typeof(ARCashSale.adjDate))]
        [PXUIField(DisplayName = "TranDate")]
        public virtual void ARPaymentChargeTran_TranDate_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #region FinPeriodID
        [PXDBString(6, IsFixed = true)]
        [PXDefault(typeof(ARCashSale.adjFinPeriodID))]
        [PXUIField(DisplayName = "FinPeriodID")]
        public virtual void ARPaymentChargeTran_FinPeriodID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #region TranPeriodID
        [PXDBString(6, IsFixed = true)]
        [PXDefault(typeof(ARCashSale.adjTranPeriodID))]
        [PXUIField(DisplayName = "TranPeriodID")]
        public virtual void ARPaymentChargeTran_TranPeriodID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #endregion

        [PXDBInt()]
		[PXDefault(typeof(ARCashSale.projectID), PersistingCheck = PXPersistingCheck.Nothing)]
		protected virtual void ARTran_ProjectID_CacheAttached(PXCache sender)
		{ }


		#endregion

		#region Credit Card Processing Buttons
		public PXAction<ARCashSale> captureCCPayment;
		[PXUIField(DisplayName = "Capture CC Payment", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable CaptureCCPayment(PXAdapter adapter)
		{
			List<ARCashSale> list = new List<ARCashSale>();
			foreach (ARCashSale doc in adapter.Get<ARCashSale>())
			{
				list.Add(doc);
				CCPaymentEntry.CaptureCCPayment<ARCashSale>(doc, ccProcTran, doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true);
			}
			return list;
		}

		public PXAction<ARCashSale> authorizeCCPayment;
		[PXUIField(DisplayName = "Authorize CC Payment", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable AuthorizeCCPayment(PXAdapter adapter)
		{
			List<ARCashSale> list = new List<ARCashSale>();
			foreach (ARCashSale doc in adapter.Get<ARCashSale>())
			{
				list.Add(doc);
				CCPaymentEntry.AuthorizeCCPayment<ARCashSale>(doc, ccProcTran);
			}
			return list;
		}

		public PXAction<ARCashSale> voidCCPayment;
		[PXUIField(DisplayName = "Void CC Payment", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable VoidCCPayment(PXAdapter adapter)
		{
			List<ARCashSale> list = new List<ARCashSale>();
			foreach (ARCashSale doc in adapter.Get<ARCashSale>())
			{
				list.Add(doc);
				CCPaymentEntry.VoidCCPayment<ARCashSale>(doc, ccProcTran, doc.VoidAppl == true && doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true);
			}

			return list;
		}

		public PXAction<ARCashSale> creditCCPayment;
		[PXUIField(DisplayName = "Refund CC Payment", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable CreditCCPayment(PXAdapter adapter)
		{
			List<ARCashSale> list = new List<ARCashSale>();
			foreach (ARCashSale doc in adapter.Get<ARCashSale>())
			{
				list.Add(doc);
				string PCRefTranNbr = doc.RefTranExtNbr;
				if (String.IsNullOrEmpty(doc.RefTranExtNbr))
				{
					this.Document.Cache.RaiseExceptionHandling<ARCashSale.refTranExtNbr>(this.Document.Current, doc.RefTranExtNbr, new PXSetPropertyException(Messages.ERR_PCTransactionNumberOfTheOriginalPaymentIsRequired));
				}
				else
				{
					CCPaymentEntry.ReleaseDelegate releaseDelegate = (doc.Released == false && this.arsetup.Current.IntegratedCCProcessing == true) ?
						CCPaymentEntry.ReleaseARDocument : (CCPaymentEntry.ReleaseDelegate)null;
					CCProcTran capturedTran = null;
					PXSelectBase<CCProcTran> capturedTranSelect = null;
					if (doc.OrigDocType != null && doc.OrigRefNbr != null)
					{
						capturedTranSelect = new PXSelect<CCProcTran, Where<CCProcTran.pCTranNumber, Equal<Optional<ARCashSale.refTranExtNbr>>,
							And<Where<CCProcTran.tranType, Equal<CCTranTypeCode.authorizeAndCapture>, Or<CCProcTran.tranType, Equal<CCTranTypeCode.priorAuthorizedCapture>>>>>,
							OrderBy<Desc<CCProcTran.tranNbr>>>(this);
						capturedTran = capturedTranSelect.SelectWindowed(0, 1, doc.RefTranExtNbr);
					}
					if (capturedTran != null && capturedTran.Amount == doc.CuryDocBal)
					{
						CCPaymentEntry.VoidCCPayment(doc, capturedTranSelect, releaseDelegate, null);
					}
					else
					{
						CCPaymentEntry.CreditCCPayment<ARCashSale>(doc, PCRefTranNbr, ccProcTran, releaseDelegate, null);
					}
				}
			}

			return list;
		}
		#endregion

		#region Other Buttons
		[PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public override IEnumerable Release(PXAdapter adapter)
		{
			PXCache cache = Document.Cache;
			List<ARRegister> list = new List<ARRegister>();
			foreach (ARCashSale ardoc in adapter.Get<ARCashSale>())
			{
				if (ardoc.Hold == false)
				{
					cache.Update(ardoc);
					list.Add(ardoc);
				}
			}
			if (list.Count == 0)
			{
				throw new PXException(Messages.Document_Status_Invalid);
			}
			Save.Press();
			PXLongOperation.StartOperation(this, delegate() { ARDocumentRelease.ReleaseDoc(list, false); });
			return list;
		}

		[PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public override IEnumerable VoidCheck(PXAdapter adapter)
		{
			if (Document.Current != null && Document.Current.Released == true && Document.Current.Voided == false && Document.Current.DocType == ARDocType.CashSale)
			{
				ARCashSale doc = PXCache<ARCashSale>.CreateCopy(Document.Current);

				if (finperiod.Current != null && (finperiod.Current.Active == false || finperiod.Current.ARClosed == true && glsetup.Current.PostClosedPeriods == false))
				{
					FinPeriod firstopen = PXSelect<FinPeriod, Where<FinPeriod.aRClosed, Equal<boolFalse>, And<FinPeriod.active, Equal<boolTrue>, And<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.Select(this, doc.AdjFinPeriodID);
					if (firstopen == null)
					{
						Document.Cache.RaiseExceptionHandling<ARCashSale.finPeriodID>(Document.Current, Document.Current.FinPeriodID, new PXSetPropertyException(GL.Messages.NoOpenPeriod));
						return adapter.Get();
					}
					doc.AdjFinPeriodID = firstopen.FinPeriodID;
				}

				try
				{
					_IsVoidCheckInProgress = true;
					this.VoidCheckProc(doc);
				}
				finally
				{
					_IsVoidCheckInProgress = false;
				}

				Document.Cache.RaiseExceptionHandling<ARCashSale.finPeriodID>(Document.Current, Document.Current.FinPeriodID, null);

				List<ARCashSale> rs = new List<ARCashSale>();
				rs.Add(Document.Current);
				return rs;
			}
			else if (Document.Current != null && Document.Current.Released == false && Document.Current.Voided == false && Document.Current.DocType == ARDocType.CashSale)
			{
				if (CCPaymentEntry.HasCCTransactions(ccProcTran))
				{
					ARCashSale doc = Document.Current;
					doc.Voided = true;
					doc.OpenDoc = false;
					doc = this.Document.Update(doc);
					this.Save.Press();
				}
			}
			return adapter.Get();
		}

		public PXAction<ARCashSale> customerDocuments;
		[PXUIField(DisplayName = "Customer Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable CustomerDocuments(PXAdapter adapter)
		{
			if (customer.Current != null)
			{
				ARDocumentEnq graph = PXGraph.CreateInstance<ARDocumentEnq>();
				graph.Filter.Current.CustomerID = customer.Current.BAccountID;
				graph.Filter.Select();
				throw new PXRedirectRequiredException(graph, "Customer Details");
			}
			return adapter.Get();
		}

		public PXAction<ARCashSale> sendARInvoiceMemo;
		[PXUIField(DisplayName = "Send AR Invoice/Memo", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable SendARInvoiceMemo(PXAdapter adapter)
		{
			ARCashSale invoice = Document.Current;
			if (Document.Current != null)
			{
				Dictionary<string, string> mailParams = new Dictionary<string, string>();
				mailParams["DocType"] = invoice.DocType;
				mailParams["RefNbr"] = invoice.RefNbr;
				if (!ReportNotificationGenerator.Send("AR641000", mailParams).Any())
				{
					throw new PXException(ErrorMessages.MailSendFailed);
				}
				Clear();
				Document.Current = Document.Search<ARInvoice.refNbr>(invoice.RefNbr, invoice.DocType);
			}
			return adapter.Get();
		}

		
		public PXAction<ARCashSale> validateAddresses;
		[PXUIField(DisplayName = CS.Messages.ValidateAddresses, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, FieldClass = CS.Messages.ValidateAddress)]
		[PXButton]
		public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
		{
			foreach (ARCashSale current in adapter.Get<ARCashSale>())
			{
				//ARCashSale current = this.CurrentDocument.Current;
				if (current != null)
				{
					bool needSave = false;
					Save.Press();
					ARAddress address = this.Billing_Address.Current;
					if (address != null && address.IsDefaultAddress == false && address.IsValidated == false)
					{
						if (PXAddressValidator.Validate<ARAddress>(this, address, true))
							needSave = true;
					}
					if (needSave == true)
						this.Save.Press();
				}
				yield return current;
			}
			//return adapter.Get();
		}
		#endregion

		#region ARCashSale Events
		protected virtual void ARCashSale_DocType_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = ARDocType.CashSale;
			e.Cancel = true;
		}

		protected virtual void ARCashSale_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			customer.RaiseFieldUpdated(sender, e.Row);

			{
				sender.SetDefaultExt<ARCashSale.customerLocationID>(e.Row);
				sender.SetDefaultExt<ARCashSale.printInvoice>(e.Row);

                sender.SetDefaultExt<ARPayment.paymentMethodID>(e.Row);
				if (((ARCashSale)e.Row).DocType != ARDocType.CreditMemo)
				{
					sender.SetDefaultExt<ARCashSale.termsID>(e.Row);
				}
				else
				{
					sender.SetValueExt<ARCashSale.termsID>(e.Row, null);
				}
			}

			ARAddressAttribute.DefaultRecord<ARCashSale.billAddressID>(sender, e.Row);
			ARContactAttribute.DefaultRecord<ARCashSale.billContactID>(sender, e.Row);
		}

		protected virtual void ARCashSale_CustomerLocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			location.RaiseFieldUpdated(sender, e.Row);

				sender.SetDefaultExt<ARCashSale.aRAccountID>(e.Row);
				sender.SetDefaultExt<ARCashSale.aRSubID>(e.Row);
			sender.SetDefaultExt<ARCashSale.branchID>(e.Row);
			sender.SetDefaultExt<ARCashSale.taxZoneID>(e.Row);
			sender.SetDefaultExt<ARCashSale.salesPersonID>(e.Row);
			sender.SetDefaultExt<ARCashSale.workgroupID>(e.Row);
			sender.SetDefaultExt<ARCashSale.ownerID>(e.Row);
			if (PM.ProjectAttribute.IsPMVisible(this, BatchModule.AR))
			{
				sender.SetDefaultExt<ARCashSale.projectID>(e.Row);
			}
		}

		protected virtual void ARCashSale_ExtRefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
            ARCashSale row = (ARCashSale)e.Row;
			if (e.Row != null && ((ARCashSale)e.Row).DocType == ARDocType.VoidPayment)
			{
				//avoid webdialog in PaymentRef attribute
				e.Cancel = true;
			}
			else
			{
				if (row!= null && string.IsNullOrEmpty((string)e.NewValue) == false && String.IsNullOrEmpty(row.PaymentMethodID))
				{
                    PaymentMethod pm = this.paymentmethod.Current;
                    ARCashSale dup = null;
                    if (pm != null && pm.IsAccountNumberRequired == true)
                    {
                        dup = PXSelectReadonly<ARCashSale, Where<ARCashSale.customerID, Equal<Current<ARCashSale.customerID>>, And<ARCashSale.pMInstanceID, Equal<Current<ARCashSale.pMInstanceID>>, And<ARCashSale.extRefNbr, Equal<Required<ARCashSale.extRefNbr>>, And<ARCashSale.voided, Equal<boolFalse>, And<Where<ARCashSale.docType, NotEqual<Current<ARCashSale.docType>>, Or<ARCashSale.refNbr, NotEqual<Current<ARCashSale.refNbr>>>>>>>>>>.Select(this, e.NewValue);
                    }
                    else 
                    {
                        dup = PXSelectReadonly<ARCashSale, Where<ARCashSale.customerID, Equal<Current<ARCashSale.customerID>>, And<ARCashSale.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<ARCashSale.extRefNbr, Equal<Required<ARCashSale.extRefNbr>>, And<ARCashSale.voided, Equal<boolFalse>, And<Where<ARCashSale.docType, NotEqual<Current<ARCashSale.docType>>, Or<ARCashSale.refNbr, NotEqual<Current<ARCashSale.refNbr>>>>>>>>>>.Select(this, e.NewValue);
                    }
					if (dup != null)
					{
                        sender.RaiseExceptionHandling<ARCashSale.extRefNbr>(e.Row, e.NewValue, new PXSetPropertyException(Messages.DuplicateCustomerPayment, PXErrorLevel.Warning, dup.ExtRefNbr, dup.DocDate, dup.DocType, dup.RefNbr));
					}
				}
			}
		}



		private object GetAcctSub<Field>(PXCache cache, object data)
			where Field : IBqlField
		{
			object NewValue = cache.GetValueExt<Field>(data);
			if (NewValue is PXFieldState)
			{
				return ((PXFieldState)NewValue).Value;
			}
			else
			{
				return NewValue;
			}
		}

		protected virtual void ARCashSale_ARAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (location.Current != null && e.Row != null)
			{
				e.NewValue = GetAcctSub<Location.aRAccountID>(location.Cache, location.Current);
			}
		}

		protected virtual void ARCashSale_ARSubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (location.Current != null && e.Row != null)
			{
				e.NewValue = GetAcctSub<Location.aRSubID>(location.Cache, location.Current);
			}
		}

        protected virtual void ARCashSale_PaymentMethodID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            sender.SetDefaultExt<ARCashSale.pMInstanceID>(e.Row);
            sender.SetDefaultExt<ARCashSale.cashAccountID>(e.Row);
        }

		protected virtual void ARCashSale_PMInstanceID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetValueExt<ARPayment.refTranExtNbr>(e.Row, null);
			sender.SetDefaultExt<ARCashSale.cashAccountID>(e.Row);
        }

        protected virtual void ARCashSale_CashAccountID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
        {
            ARCashSale cashSale = (ARCashSale)e.Row;
			if (cashSale == null) return;
            bool wasFirstErrorCharge = false;
            foreach (ARPaymentChargeTran charge in PaymentCharges.Select())
            {
                CashAccountETDetail eTDetail = (CashAccountETDetail)PXSelectJoin<CashAccountETDetail,
                                                                    InnerJoin<Account, On<Account.accountID, Equal<CashAccountETDetail.accountID>>>,
                                                                    Where<Account.accountCD, Equal<Required<ARCashSale.cashAccountID>>,
                                                                    And<CashAccountETDetail.entryTypeID, Equal<Required<ARPaymentChargeTran.entryTypeID>>>>>.
                                                                    Select(this, e.NewValue, charge.EntryTypeID);
                if (eTDetail == null)
                {
                    if (!wasFirstErrorCharge)
                    {
                        if (Document.Ask(Messages.Warning, AP.Messages.SomeChargeNotRelatedWithCashAccount, MessageButtons.YesNo) == WebDialogResult.No)
                        {
                            e.NewValue = cashSale.CashAccountID;
                            break;
                        }
                        wasFirstErrorCharge = true;
                    }
                    PaymentCharges.Cache.Delete(charge);
                }
            }
        }
		protected virtual void ARCashSale_CashAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARCashSale payment = (ARCashSale)e.Row;
			if (cashaccount.Current == null || cashaccount.Current.AccountID != payment.CashAccountID)
			{
				cashaccount.Current = (CashAccount)PXSelectorAttribute.Select<ARCashSale.cashAccountID>(sender, e.Row);
			}

			if (_IsVoidCheckInProgress == false && cmsetup.Current.MCActivated == true)
			{
				CurrencyInfo info = CurrencyInfoAttribute.SetDefaults<ARCashSale.curyInfoID>(sender, e.Row);

				string message = PXUIFieldAttribute.GetError<CurrencyInfo.curyEffDate>(currencyinfo.Cache, info);
				if (string.IsNullOrEmpty(message) == false)
				{
					sender.RaiseExceptionHandling<ARCashSale.adjDate>(e.Row, payment.DocDate, new PXSetPropertyException(message, PXErrorLevel.Warning));
				}

				if (info != null)
				{
					payment.CuryID = info.CuryID;
				}
			}

			//sender.SetDefaultExt<ARCashSale.branchID>(e.Row);
			payment.Cleared = false;
			payment.ClearDate = null;
			sender.SetDefaultExt<ARCashSale.depositAsBatch>(e.Row);
			sender.SetDefaultExt<ARCashSale.depositAfter>(e.Row);


			if ((cashaccount.Current != null) && (cashaccount.Current.Reconcile == false))
			{
				payment.Cleared = true;
				payment.ClearDate = payment.DocDate;
			}
		}
		protected virtual void ARCashSale_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARCashSale payment = (ARCashSale)e.Row;
			if (payment.Cleared == true)
			{
				if (payment.ClearDate == null)
				{
					payment.ClearDate = payment.DocDate;
				}
			}
			else
			{
				payment.ClearDate = null;
			}
		}
		protected virtual void ARCashSale_AdjDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARCashSale payment = (ARCashSale)e.Row;
			if (payment.Released == false && payment.DocType != ARDocType.CashReturn)
			{
				CurrencyInfoAttribute.SetEffectiveDate<ARCashSale.adjDate>(sender, e);

				sender.SetDefaultExt<ARCashSale.depositAfter>(e.Row);
			}
		}

		protected virtual void ARCashSale_AdjDate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if ((bool)((ARCashSale)e.Row).VoidAppl == false)
			{
				//foreach (PXResult<ARAdjust, ARInvoice> res in Adjustments_raw.Select((object)null))
				//{
				//	throw new PXSetPropertyException(Messages.Cannot_Change_Details_Exists);
				//}
			}
		}

		protected virtual void ARCashSale_DepositAfter_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ARCashSale row = (ARCashSale)e.Row;
			if ((row.DocType == ARDocType.Payment || row.DocType == ARDocType.CashSale || row.DocType == ARDocType.Refund || row.DocType == ARDocType.CashReturn)
				&& row.DepositAsBatch == true)
			{
				e.NewValue = row.AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_DepositAsBatch_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARCashSale row = (ARCashSale)e.Row;
			if ((row.DocType == ARDocType.Payment || row.DocType == ARDocType.CashSale || row.DocType == ARDocType.Refund) || row.DocType == ARDocType.CashReturn)
			{
				sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
			}
		}
		
		protected virtual void ARCashSale_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			ARCashSale doc = (ARCashSale)e.Row;

			if (doc.CashAccountID == null)
			{
				if (sender.RaiseExceptionHandling<ARCashSale.cashAccountID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARCashSale.cashAccountID).Name)))
				{
					throw new PXRowPersistingException(typeof(ARCashSale.cashAccountID).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARCashSale.cashAccountID).Name);
				}
			}

            if (String.IsNullOrEmpty(doc.PaymentMethodID))
            {
                if (sender.RaiseExceptionHandling<ARCashSale.paymentMethodID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARCashSale.paymentMethodID).Name)))
                {
                    throw new PXRowPersistingException(typeof(ARCashSale.paymentMethodID).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARCashSale.paymentMethodID).Name);
                }
            }

			if (doc.PMInstanceID == null)
			{
                PaymentMethod pm = this.paymentmethod.Current;
                if (pm.IsAccountNumberRequired == true)
                {
                    if (sender.RaiseExceptionHandling<ARCashSale.pMInstanceID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARCashSale.pMInstanceID).Name)))
                    {
                        throw new PXRowPersistingException(typeof(ARCashSale.pMInstanceID).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARCashSale.pMInstanceID).Name);
                    }
                }
			}

			if (string.IsNullOrEmpty(doc.ExtRefNbr))
			{
				if (sender.RaiseExceptionHandling<ARCashSale.extRefNbr>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARCashSale.extRefNbr).Name)))
				{
					throw new PXRowPersistingException(typeof(ARCashSale.extRefNbr).Name, null, ErrorMessages.FieldIsEmpty, typeof(ARCashSale.extRefNbr).Name);
				}
			}

			Terms terms = (Terms)PXSelectorAttribute.Select<ARCashSale.termsID>(Document.Cache, doc);

			if (terms == null)
			{
				sender.SetValue<ARCashSale.termsID>(doc, null);
				return;
			}

			if (terms.InstallmentType == CS.TermsInstallmentType.Multiple)
			{
				sender.RaiseExceptionHandling<ARCashSale.termsID>(doc, doc.TermsID, new PXSetPropertyException(Messages.Cash_Sale_Cannot_Have_Multiply_Installments, typeof(ARCashSale.termsID).Name));
			}

			PaymentRefAttribute.SetUpdateCashManager<ARCashSale.extRefNbr>(sender, e.Row, ((ARCashSale)e.Row).DocType != ARDocType.VoidPayment);
		}

		protected bool InternalCall = false;

		protected virtual void ARCashSale_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			ARCashSale doc = e.Row as ARCashSale;

			if (doc == null || InternalCall)
			{
				return;
			}

			bool isPayment = (doc.DocType == ARDocType.Payment || doc.DocType == ARDocType.CashSale);
			PXUIFieldAttribute.SetVisible<ARCashSale.depositAfter>(cache, doc, isPayment && (doc.DepositAsBatch == true));

            bool isPMInstanceRequired = false;
            if (!String.IsNullOrEmpty(doc.PaymentMethodID))
            {
                PaymentMethod paymentMethod = this.paymentmethod.Current;
                if (paymentMethod != null)
                {
                    isPMInstanceRequired = paymentMethod.IsAccountNumberRequired ?? false;
                }
            }
                       

			bool curyenabled = false;
			bool clearEnabled = doc.Hold == true && (cashaccount.Current != null) && (cashaccount.Current.Reconcile == true);

			#region Credit Card Processing

			bool enableCCProcess = false;
			bool docTypePayment = (doc.DocType == ARDocType.CashSale);

			doc.IsCCPayment = false;

			if (paymentmethod.Current != null && paymentmethod.Current.PaymentType == CA.PaymentMethodType.CreditCard)
			{
				doc.IsCCPayment = true;
				enableCCProcess = (docTypePayment || doc.DocType == ARDocType.CashReturn);
			}

			bool isCCVoided = false;
			bool isCCCaptured = false;
			bool isCCPreAuthorized = false;
			bool isCCRefunded = false;
			bool isCCVoidingAttempted = false; //Special flag for VoidPayment Release logic 

			if (enableCCProcess)
			{
				if ((bool)doc.Voided)
				{
					enableCCProcess = false;
				}
			}
			if (enableCCProcess)
			{
				CCPaymentState ccPaymentState = CCPaymentEntry.ResolveCCPaymentState(ccProcTran.Select());
				isCCVoided = (ccPaymentState & CCPaymentState.Voided) != 0;
				isCCCaptured = (ccPaymentState & CCPaymentState.Captured) != 0;
				isCCPreAuthorized = (ccPaymentState & CCPaymentState.PreAuthorized) != 0;
				isCCRefunded = (ccPaymentState & CCPaymentState.Refunded) != 0;
				isCCVoidingAttempted = (ccPaymentState & CCPaymentState.VoidFailed) != 0;
				doc.CCPaymentStateDescr = CCPaymentEntry.FormatCCPaymentState(ccPaymentState);
			}

			bool canAuthorize = (doc.Hold == false) && docTypePayment && !(isCCPreAuthorized || isCCCaptured);
			bool canCapture = (doc.Hold == false) && docTypePayment && !(isCCCaptured);
			bool canVoid = (doc.Hold == false) && ((doc.DocType == ARDocType.CashReturn) && (isCCCaptured || isCCPreAuthorized)) || (isCCPreAuthorized && docTypePayment);
			bool canCredit = (doc.Hold == false) && ((doc.DocType == ARDocType.CashReturn) && !isCCRefunded);

            this.authorizeCCPayment.SetEnabled(enableCCProcess && canAuthorize);
            this.captureCCPayment.SetEnabled(enableCCProcess && canCapture);
            this.voidCCPayment.SetEnabled(enableCCProcess && canVoid);
			this.creditCCPayment.SetEnabled(enableCCProcess && canCredit);

			PXUIFieldAttribute.SetVisible<ARCashSale.cCPaymentStateDescr>(cache, doc, enableCCProcess);
			PXUIFieldAttribute.SetVisible<ARCashSale.refTranExtNbr>(cache, doc, ((doc.DocType == ARDocType.CashReturn) && enableCCProcess));
			#endregion
            PXUIFieldAttribute.SetRequired<ARPayment.pMInstanceID>(cache, isPMInstanceRequired);
            PXDefaultAttribute.SetPersistingCheck<ARPayment.pMInstanceID>(cache, doc, (isPMInstanceRequired) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);


			if (doc.Released == true || doc.Voided == true)
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = false;
				Transactions.Cache.AllowDelete = false;
				Transactions.Cache.AllowUpdate = false;
				Transactions.Cache.AllowInsert = false;
				release.SetEnabled(false);
				voidCheck.SetEnabled(doc.Voided == false);
			}
			else if (enableCCProcess && (isCCPreAuthorized || isCCCaptured))
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARCashSale.adjDate>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARCashSale.adjFinPeriodID>(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARCashSale.hold>(cache, doc, true);

				//calculate only on data entry, differences from the applications will be moved to RGOL upon closure
				PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDocBal>(cache, null, true);
				PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDiscBal>(cache, null, true);

				cache.AllowDelete = false;
				cache.AllowUpdate = true;
				Transactions.Cache.AllowDelete = true;
				Transactions.Cache.AllowUpdate = true;
				Transactions.Cache.AllowInsert = (doc.CustomerID != null) && (doc.CustomerLocationID != null);
				release.SetEnabled(doc.Hold == false);
				voidCheck.SetEnabled(false);
			}
			else
			{
				PXUIFieldAttribute.SetEnabled(cache, doc, true);
				PXUIFieldAttribute.SetEnabled<ARCashSale.status>(cache, doc, false);
				PXUIFieldAttribute.SetEnabled<ARCashSale.curyID>(cache, doc, curyenabled);
				PXUIFieldAttribute.SetEnabled<ARCashSale.refTranExtNbr>(cache, doc, enableCCProcess && ((doc.DocType == ARDocType.CashReturn) && !isCCRefunded));

				//calculate only on data entry, differences from the applications will be moved to RGOL upon closure
				PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDocBal>(cache, null, true);
				PXDBCurrencyAttribute.SetBaseCalc<ARCashSale.curyDiscBal>(cache, null, true);

                PXUIFieldAttribute.SetEnabled<ARPayment.pMInstanceID>(cache, doc, isPMInstanceRequired);                

			    cache.AllowDelete = !CCPaymentEntry.HasCCTransactions(ccProcTran);
				cache.AllowUpdate = true;
				Transactions.Cache.AllowDelete = true;
				Transactions.Cache.AllowUpdate = true;
				Transactions.Cache.AllowInsert = (doc.CustomerID != null) && (doc.CustomerLocationID != null);
				release.SetEnabled(doc.Hold == false);
				voidCheck.SetEnabled(false);				
	

			}

			PXUIFieldAttribute.SetEnabled<ARCashSale.docType>(cache, doc);
			PXUIFieldAttribute.SetEnabled<ARCashSale.refNbr>(cache, doc);
			PXUIFieldAttribute.SetEnabled<ARCashSale.batchNbr>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.curyLineTotal>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.curyTaxTotal>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.curyDocBal>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.curyCommnblAmt>(cache, doc, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.curyCommnAmt>(cache, doc, false);
            PXUIFieldAttribute.SetEnabled<ARCashSale.curyVatExemptTotal>(cache, doc, false);
            PXUIFieldAttribute.SetEnabled<ARCashSale.curyVatTaxableTotal>(cache, doc, false);

			PXUIFieldAttribute.SetEnabled<ARCashSale.cleared>(cache, doc, clearEnabled);
			PXUIFieldAttribute.SetEnabled<ARCashSale.clearDate>(cache, doc, clearEnabled && doc.Cleared == true);			

			PXUIFieldAttribute.SetEnabled<ARCashSale.depositAsBatch>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.deposited>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.depositType>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.depositNbr>(cache, null, false);
			PXUIFieldAttribute.SetEnabled<ARCashSale.depositDate>(cache, null, false);

			PXUIFieldAttribute.SetEnabled<ARCashSale.depositAfter>(cache, doc, isPayment && (doc.DepositAsBatch == true));
			PXUIFieldAttribute.SetRequired<ARCashSale.depositAfter>(cache, isPayment && (doc.DepositAsBatch == true));
			PXDefaultAttribute.SetPersistingCheck<ARCashSale.depositAfter>(cache, doc, isPayment && (doc.DepositAsBatch == true) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);

			bool enableVoidCheck = (doc.Released == true) && docTypePayment && (doc.Voided == false);
			bool isCCStateClear = (!(isCCCaptured || isCCPreAuthorized)) || isCCVoided || isCCRefunded;

			if ((doc.Released == false) && !enableVoidCheck && docTypePayment && (doc.Voided == false))
			{
				if (CCPaymentEntry.HasCCTransactions(ccProcTran) && (isCCStateClear || (isCCPreAuthorized && isCCVoidingAttempted)))
					enableVoidCheck = true;
			}

			voidCheck.SetEnabled(enableVoidCheck);

			#region CCProcessing integrated with doc
			if (enableCCProcess && (arsetup.Current.IntegratedCCProcessing ?? false))
			{
				if ((doc.Released == false))
				{
					if (doc.VoidAppl == false)
					{
						release.SetEnabled((doc.Hold == false) && (doc.OpenDoc == true) && isCCCaptured);
					}
					else if (doc.DocType == ARDocType.CashReturn)
					{
						release.SetEnabled((doc.Hold == false) && (doc.OpenDoc == true) && isCCRefunded);
					}
					else
					{
						//We should allow release if CCPayment has just pre-authorization - it will expire anyway.
						release.SetEnabled((doc.Hold == false) && (doc.OpenDoc == true) && (isCCStateClear || (isCCPreAuthorized && isCCVoidingAttempted)));
					}
				}
			}

			#endregion

			if (doc.CustomerID != null)
			{
				if (Transactions.Select().Count > 0)
				{
					PXUIFieldAttribute.SetEnabled<ARCashSale.customerID>(cache, doc, false);
				}
			}

			PXUIFieldAttribute.SetEnabled<ARCashSale.cCPaymentStateDescr>(cache, null, false);

			bool isDeposited = (string.IsNullOrEmpty(doc.DepositNbr) == false && string.IsNullOrEmpty(doc.DepositType) == false);
			CashAccount cashAccount = this.cashaccount.Current;
			bool isClearingAccount = (cashAccount != null && cashAccount.CashAccountID == doc.CashAccountID && cashAccount.ClearingAccount == true);
			bool enableDepositEdit = !isDeposited && cashAccount != null && (isClearingAccount || doc.DepositAsBatch != isClearingAccount);
			if (enableDepositEdit)
			{
				cache.AllowUpdate = true;
				if (doc.DepositAsBatch != isClearingAccount)
					cache.RaiseExceptionHandling<ARCashSale.depositAsBatch>(doc, doc.DepositAsBatch, new PXSetPropertyException(Messages.DocsDepositAsBatchSettingDoesNotMatchClearingAccountFlag, PXErrorLevel.Warning));
				else
					cache.RaiseExceptionHandling<ARCashSale.depositAsBatch>(doc, doc.DepositAsBatch, null);
			}
			PXUIFieldAttribute.SetEnabled<ARCashSale.depositAsBatch>(cache, doc, enableDepositEdit);
			PXUIFieldAttribute.SetEnabled<ARCashSale.depositAfter>(cache, doc, ((!isDeposited) && isClearingAccount && doc.DepositAsBatch == true));

			ARAddress address = this.Billing_Address.Select();
			bool enableAddressValidation = (doc.Released == false) && (address != null && address.IsDefaultAddress == false && address.IsValidated ==false);
			this.validateAddresses.SetEnabled(enableAddressValidation);

			this.PaymentCharges.Cache.AllowInsert = ((doc.DocType == ARDocType.CashSale || doc.DocType == ARDocType.CashReturn) && doc.Released != true);
			this.PaymentCharges.Cache.AllowUpdate = ((doc.DocType == ARDocType.CashSale || doc.DocType == ARDocType.CashReturn) && doc.Released != true);
			this.PaymentCharges.Cache.AllowDelete = ((doc.DocType == ARDocType.CashSale || doc.DocType == ARDocType.CashReturn) && doc.Released != true);

			Taxes.Cache.AllowInsert = Transactions.Cache.AllowInsert;
			Taxes.Cache.AllowUpdate = Transactions.Cache.AllowUpdate;
			Taxes.Cache.AllowDelete = Transactions.Cache.AllowDelete;
		}

        protected virtual void ARCashSale_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
        {
			if (e.Row != null)
			{
				using (new PXConnectionScope())
				{
					PXFormulaAttribute.CalcAggregate<ARPaymentChargeTran.curyTranAmt>(PaymentCharges.Cache, e.Row, true);
				}
			}
        }

		protected virtual void ARCashSale_DocDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (e.Row != null && ((ARCashSale)e.Row).Released == false && ((ARCashSale)e.Row).VoidAppl == false)
			{
				e.NewValue = ((ARCashSale)e.Row).AdjDate;
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_FinPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (e.Row != null && ((ARCashSale)e.Row).Released == false)
			{
				e.NewValue = ((ARCashSale)e.Row).AdjFinPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_TranPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (e.Row != null && ((ARCashSale)e.Row).Released == false)
			{
				e.NewValue = ((ARCashSale)e.Row).AdjTranPeriodID;
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARCashSale row = e.Row as ARCashSale;
			if (row != null)
			{
				foreach (ARTran tran in Transactions.Select())
				{
					Transactions.Cache.SetDefaultExt<ARTran.projectID>(tran);
				}

			}
		}
		
		protected virtual void ARCashSale_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			ARReleaseProcess.UpdateARBalances(this, (ARRegister)e.Row, -(((ARRegister)e.Row).OrigDocAmt));
		}

		protected virtual void ARCashSale_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			ARCashSale payment = (ARCashSale)e.Row;
			if (payment.Released == false)
			{
				//for Void application cannot change DocDate not to screw up DBDefault for AdjgDocDate 
				//which will cause incorrect voucher balance calculation
				if (payment.VoidAppl == false)
				{
					payment.DocDate = payment.AdjDate;
				}

				payment.FinPeriodID = payment.AdjFinPeriodID;
				payment.TranPeriodID = payment.AdjTranPeriodID;

				sender.RaiseExceptionHandling<ARCashSale.finPeriodID>(e.Row, payment.FinPeriodID, null);
			}

			ARReleaseProcess.UpdateARBalances(this, (ARRegister)e.Row, (((ARRegister)e.Row).OrigDocAmt));
		}

		protected virtual void ARCashSale_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			ARCashSale doc = e.Row as ARCashSale;
			if (doc.Released != true)
			{
				//for Void application cannot change DocDate not to screw up DBDefault for AdjgDocDate 
				//which will cause incorrect voucher balance calculation
				if (doc.VoidAppl != true)
				{
					doc.DocDate = doc.AdjDate;
				}

				doc.FinPeriodID  = doc.AdjFinPeriodID;
				doc.TranPeriodID = doc.AdjTranPeriodID;

				sender.RaiseExceptionHandling<ARCashSale.finPeriodID>(doc, doc.FinPeriodID, null);

				if (sender.ObjectsEqual<ARCashSale.curyDocBal, ARCashSale.curyOrigDiscAmt>(e.Row, e.OldRow) == false && doc.CuryDocBal - doc.CuryOrigDiscAmt != doc.CuryOrigDocAmt)
				{
					if (doc.CuryDocBal != null && doc.CuryOrigDiscAmt != null && doc.CuryDocBal != 0)
						sender.SetValueExt<ARCashSale.curyOrigDocAmt>(doc, doc.CuryDocBal - doc.CuryOrigDiscAmt);
					else
						sender.SetValueExt<ARCashSale.curyOrigDocAmt>(doc, 0m);
				}
				else if (sender.ObjectsEqual<ARCashSale.curyOrigDocAmt>(e.Row, e.OldRow) == false)
				{
					if (doc.CuryDocBal != null && doc.CuryOrigDocAmt != null && doc.CuryDocBal != 0)
						sender.SetValueExt<ARCashSale.curyOrigDiscAmt>(doc, doc.CuryDocBal - doc.CuryOrigDocAmt);
					else
						sender.SetValueExt<ARCashSale.curyOrigDiscAmt>(doc, 0m);
				}
				if (doc.Hold != true)
				{
					if (doc.CuryDocBal < doc.CuryOrigDocAmt)
					{
						sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>(doc, doc.CuryOrigDocAmt, new PXSetPropertyException(Messages.CashSaleOutOfBalance));
					}
					else if (doc.CuryOrigDocAmt < 0)
					{
						sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>(doc, doc.CuryOrigDocAmt, new PXSetPropertyException(Messages.DocumentBalanceNegative));
					}
					else
					{
						sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>(doc, doc.CuryOrigDocAmt, null);
					}
				}
			}

			if (e.OldRow != null)
			{
				ARReleaseProcess.UpdateARBalances(this, (ARRegister)e.OldRow, -(((ARRegister)e.OldRow).OrigDocAmt));
			}
			ARReleaseProcess.UpdateARBalances(this, (ARRegister)e.Row, (((ARRegister)e.Row).OrigDocAmt));
		}
        
        protected virtual void ParentBranchUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            foreach (ARSalesPerTran tran in salesPerTrans.Select())
            {
                if (this.salesPerTrans.Cache.GetStatus(tran) == PXEntryStatus.Notchanged)
                {
                    this.salesPerTrans.Cache.SetStatus(tran, PXEntryStatus.Updated);
                }
            }
        }
		#endregion

		#region CurrencyInfo Events
		protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			CurrencyInfo info = e.Row as CurrencyInfo;
			if (info != null)
			{
				bool curyenabled = info.AllowUpdate(this.Transactions.Cache);

				if (customer.Current != null && customer.Current.AllowOverrideRate == false)
				{
					curyenabled = false;
				}

				ARCashSale doc = PXSelect<ARCashSale, Where<ARCashSale.curyInfoID, Equal<Current<CurrencyInfo.curyInfoID>>>>.Select(sender.Graph);
				if (doc != null && doc.DocType == ARDocType.CashReturn)
				{
					curyenabled = false;
				}

				PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyRateTypeID>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyEffDate>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleCuryRate>(sender, info, curyenabled);
				PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleRecipRate>(sender, info, curyenabled);
			}
		}

		protected virtual void CurrencyInfo_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)cmsetup.Current.MCActivated)
			{
				if (cashaccount.Current != null && !string.IsNullOrEmpty(cashaccount.Current.CuryID))
				{
					e.NewValue = cashaccount.Current.CuryID;
					e.Cancel = true;
				}
			}
		}

		protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if ((bool)cmsetup.Current.MCActivated)
			{
				if (cashaccount.Current != null && !string.IsNullOrEmpty(cashaccount.Current.CuryRateTypeID))
				{
					e.NewValue = cashaccount.Current.CuryRateTypeID;
					e.Cancel = true;
				}
			}
		}

		protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Cache.Current != null)
			{
				e.NewValue = ((ARCashSale)Document.Cache.Current).DocDate;
				e.Cancel = true;
			}
		}
		#endregion

		#region CATran Events
		protected virtual void CATran_CashAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_TranPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_ReferenceID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void CATran_CuryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}
		#endregion

		#region ARTran events
		protected virtual void ARTran_AccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ARTran tran = (ARTran)e.Row;
			if (tran != null && tran.InventoryID == null && location.Current != null)
			{
				e.NewValue = location.Current.CSalesAcctID;
				e.Cancel = true;
			}
		}

		protected virtual void ARTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ARTran tran = (ARTran)e.Row;
			if (tran != null && tran.AccountID != null && location.Current != null)
			{
				InventoryItem item = (InventoryItem)PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Select(this, tran.InventoryID);
				EPEmployee employee = (EPEmployee)PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Select(this, PXAccess.GetUserID());
				Location companyloc =
					(Location)PXSelectJoin<Location, InnerJoin<BAccountR, On<Location.bAccountID, Equal<BAccountR.bAccountID>, And<Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<GL.Branch, On<BAccountR.bAccountID, Equal<GL.Branch.bAccountID>>>>, Where<GL.Branch.branchID, Equal<Required<ARTran.branchID>>>>.Select(this, tran.BranchID);
				SalesPerson salesperson = (SalesPerson)PXSelect<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<ARTran.salesPersonID>>>>.SelectSingleBound(this, new object[] { e.Row });

				int? customer_SubID = (int?)Caches[typeof(Location)].GetValue<Location.cSalesSubID>(location.Current);
				int? item_SubID = (int?)Caches[typeof(InventoryItem)].GetValue<InventoryItem.salesSubID>(item);
				int? employee_SubID = (int?)Caches[typeof(EPEmployee)].GetValue<EPEmployee.salesSubID>(employee);
				int? company_SubID = (int?)Caches[typeof(Location)].GetValue<Location.cMPSalesSubID>(companyloc);
				int? salesperson_SubID = (int?)Caches[typeof(SalesPerson)].GetValue<SalesPerson.salesSubID>(salesperson);

				object value = SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>(this, arsetup.Current.SalesSubMask,
					new object[] { customer_SubID, item_SubID, employee_SubID, company_SubID, salesperson_SubID },
					new Type[] { typeof(Location.cSalesSubID), typeof(InventoryItem.salesSubID), typeof(EPEmployee.salesSubID), typeof(Location.cMPSalesSubID), typeof(SalesPerson.salesSubID) });

				sender.RaiseFieldUpdating<ARTran.subID>(e.Row, ref value);

				e.NewValue = (int?)value;
				e.Cancel = true;
			}
		}

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Category")]
        [ARCashSaleTax(typeof(ARCashSale), typeof(ARTax), typeof(ARTaxTran))]
        [PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
        [PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
        [PXDefault(typeof(Search<InventoryItem.taxCategoryID,
            Where<InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>>>),
            PersistingCheck = PXPersistingCheck.Nothing, SearchOnDefault = false)]
        protected virtual void ARTran_TaxCategoryID_CacheAttached(PXCache sender)
        {
        }

		protected virtual void ARTran_TaxCategoryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (TaxAttribute.GetTaxCalc<ARTran.taxCategoryID>(sender, e.Row) == TaxCalc.Calc && taxzone.Current != null && !string.IsNullOrEmpty(taxzone.Current.DfltTaxCategoryID) && ((ARTran)e.Row).InventoryID == null)
			{
				e.NewValue = taxzone.Current.DfltTaxCategoryID;
			}
		}

		protected virtual void ARTran_UnitPrice_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (((ARTran)e.Row).InventoryID == null)
			{
				e.NewValue = 0m;
			}
		}

        protected virtual void ARTran_CuryUnitPrice_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            ARTran row = e.Row as ARTran;
            if (row == null || row.InventoryID == null || row.UOM == null)
            {
                e.NewValue = sender.GetValue<ARTran.curyUnitPrice>(e.Row);
                e.Cancel = e.NewValue != null;
                return;
            }

            ARCashSale doc = this.Document.Current;
            if (doc != null && doc.CustomerID != null && row.Qty != null)
            {
                string customerPriceClass = ARPriceClass.EmptyPriceClass;
                Location c = location.Select();
                if (c != null && !string.IsNullOrEmpty(c.CPriceClassID))
                    customerPriceClass = c.CPriceClassID;

                e.NewValue = ARSalesPriceMaint.CalculateSalesPrice(sender, customerPriceClass, doc.CustomerID, ((ARTran)e.Row).InventoryID, currencyinfo.Select(), ((ARTran)e.Row).UOM, row.Qty, doc.DocDate.Value, row.CuryUnitPrice) ?? 0m;
            }
        }

		protected virtual void ARTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<ARTran.unitPrice>(e.Row);
			sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
			sender.SetValue<ARTran.unitPrice>(e.Row, null);
		}

		protected virtual void ARTran_Qty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ARTran row = e.Row as ARTran;
			if (row != null)
			{
                sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
			}
		}


		protected virtual void ARTran_InventoryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (!e.ExternalCall)
			{
				e.Cancel = true;
			}
		}

		protected virtual void ARTran_SOShipmentNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void ARTran_SalesPersonID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<ARTran.subID>(e.Row);
		}

		protected virtual void ARTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<ARTran.accountID>(e.Row);
			sender.SetDefaultExt<ARTran.subID>(e.Row);
			sender.SetDefaultExt<ARTran.taxCategoryID>(e.Row);
			sender.SetDefaultExt<ARTran.deferredCode>(e.Row);
			sender.SetDefaultExt<ARTran.uOM>(e.Row);

			sender.SetDefaultExt<ARTran.unitPrice>(e.Row);
			sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);

			ARTran tran = e.Row as ARTran;
			IN.InventoryItem item = PXSelectorAttribute.Select<IN.InventoryItem.inventoryID>(sender, tran) as IN.InventoryItem;
			if (item != null && tran != null)
			{
				tran.TranDesc = item.Descr;
			}
		}
		        
		protected virtual void ARTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			ARTran row = (ARTran)e.Row;
			ARTran oldRow = (ARTran)e.OldRow;
			if (row != null)
			{
				if (!sender.ObjectsEqual<ARTran.qty,
					ARTran.uOM>(e.OldRow, e.Row))
				{
					SetFlatUnitPrice(sender, row);
				}

                RecalculateDiscounts(sender, row);


				TaxAttribute.Calculate<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(sender, e);
			}
		}

		protected virtual void ARTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			ARTran tran = (ARTran)e.Row;
			if (tran != null)
			{
				RecalculateDiscounts(sender, (ARTran)e.Row);
			}
			TaxAttribute.Calculate<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(sender, e);
		}

		protected virtual void ARTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
		}

		protected virtual void ARTran_DrCr_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Current != null)
			{
				e.NewValue = ARInvoiceType.DrCr(Document.Current.DocType);
				e.Cancel = true;
			}
		}
		#endregion

		#region ARTaxTran Events
		protected virtual void ARTaxTran_TaxZoneID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Document.Current != null)
			{
				e.NewValue = Document.Current.TaxZoneID;
				e.Cancel = true;
			}
		}

		protected virtual void ARTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			if (Document.Current != null && (e.Operation == PXDBOperation.Insert || e.Operation == PXDBOperation.Update))
			{
				((ARTaxTran)e.Row).TaxZoneID = Document.Current.TaxZoneID;
			}
		}
		#endregion

		#region ARSalesPerTran events

		protected virtual void ARSalesPerTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			ARSalesPerTran row = (ARSalesPerTran)e.Row;
			foreach (ARSalesPerTran iSpt in this.salesPerTrans.Select())
			{
				if (iSpt.SalespersonID == row.SalespersonID)
				{
					PXEntryStatus status = this.salesPerTrans.Cache.GetStatus(iSpt);
					if (!(status == PXEntryStatus.InsertedDeleted || status == PXEntryStatus.Deleted))
					{
						sender.RaiseExceptionHandling<ARSalesPerTran.salespersonID>(e.Row, null, new PXException(Messages.ERR_DuplicatedSalesPersonAdded));
						e.Cancel = true;
						break;
					}
				}
			}
		}
		#endregion

        #region ARPaymentChargeTran Events

        public virtual void ARPaymentChargeTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            ARPaymentChargeTran charge = (ARPaymentChargeTran)e.Row;
			if (charge.CuryTranAmt <= 0m)
            {
                sender.RaiseExceptionHandling<ARPaymentChargeTran.curyTranAmt>(e.Row, charge.CuryTranAmt, new PXSetPropertyException(CS.Messages.Entry_GT, "0"));
            }
            if (charge.AccountID == null)
            {
                sender.RaiseExceptionHandling<ARPaymentChargeTran.accountID>(e.Row, charge.AccountID, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARPaymentChargeTran.accountID).Name));
            }
            if (charge.SubID == null)
            {
                sender.RaiseExceptionHandling<ARPaymentChargeTran.subID>(e.Row, charge.SubID, new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(ARPaymentChargeTran.subID).Name));
            }

        }

        #endregion

        #region Voiding
        private bool _IsVoidCheckInProgress = false;

		protected virtual void ARCashSale_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		protected virtual void ARCashSale_AdjFinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (_IsVoidCheckInProgress)
			{
				e.Cancel = true;
			}
		}

		public virtual void VoidCheckProc(ARCashSale doc)
		{
			this.Clear(PXClearOption.PreserveTimeStamp);

			TaxAttribute.SetTaxCalc<ARTran.taxCategoryID>(this.Transactions.Cache, null, TaxCalc.NoCalc);

			foreach (PXResult<ARCashSale, CurrencyInfo> res in PXSelectJoin<ARCashSale, InnerJoin<CurrencyInfo, On<CurrencyInfo.curyInfoID, Equal<ARCashSale.curyInfoID>>>, Where<ARCashSale.docType, Equal<Required<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Required<ARCashSale.refNbr>>>>>.Select(this, (object)doc.DocType, doc.RefNbr))
			{
				CurrencyInfo info = PXCache<CurrencyInfo>.CreateCopy((CurrencyInfo)res);
				info.CuryInfoID = null;
				info.IsReadOnly = false;
				info = PXCache<CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(info));

				ARCashSale payment = new ARCashSale();
				payment.DocType = ARDocType.CashReturn;
				payment.RefNbr = null;
				payment.CuryInfoID = info.CuryInfoID;
				payment.OrigDocType = ((ARCashSale)res).DocType;
				payment.OrigRefNbr = ((ARCashSale)res).RefNbr;                

				payment = Document.Insert(payment);

				if (payment.RefNbr == null)
				{
					//manual numbering, check for occasional duplicate
					ARCashSale duplicate = PXSelect<ARCashSale>.Search<ARCashSale.docType, ARCashSale.refNbr>(this, payment.DocType, payment.OrigRefNbr);
					if (duplicate != null)
					{
						throw new PXException(ErrorMessages.RecordExists);
					}

					payment.RefNbr = payment.OrigRefNbr;
					this.Document.Cache.Normalize();
					payment = this.Document.Update(payment);
				}
				
				payment = PXCache<ARCashSale>.CreateCopy((ARCashSale)res);

				CCProcTran lastCapturedTran = PXSelect<CCProcTran, Where<CCProcTran.refNbr, Equal<Required<ARCashSale.refNbr>>, And<Where<CCProcTran.docType, Equal<Required<ARCashSale.docType>>,
					And<Where<CCProcTran.tranType, Equal<CCTranTypeCode.authorizeAndCapture>, Or<CCProcTran.tranType, Equal<CCTranTypeCode.priorAuthorizedCapture>>>>>>>,
					OrderBy<Desc<CCProcTran.tranNbr>>>.SelectWindowed(this, 0, 1, doc.RefNbr, doc.DocType);
				if (lastCapturedTran != null)
				{
					payment.RefTranExtNbr = lastCapturedTran.PCTranNumber;
				}
				payment.CuryInfoID = info.CuryInfoID;
				payment.DocType = Document.Current.DocType;
				payment.RefNbr = Document.Current.RefNbr;
				payment.OrigDocType = Document.Current.OrigDocType;
				payment.OrigRefNbr = Document.Current.OrigRefNbr;
				payment.CATranID = null;
				payment.NoteID = null;
				payment.RefNoteID = null;

				//must set for _RowSelected
				payment.OpenDoc = true;
				payment.Released = false;
				payment.Hold = false;
				payment.LineCntr = 0;
				payment.BatchNbr = null;
				payment.AdjDate = doc.DocDate;
				payment.AdjFinPeriodID = doc.AdjFinPeriodID;
				payment.CuryDocBal = payment.CuryOrigDocAmt + payment.CuryOrigDiscAmt;
				payment.CuryChargeAmt = 0;
				payment.CuryConsolidateChargeTotal = 0;
				payment.ClosedFinPeriodID = null;
				payment.ClosedTranPeriodID = null;

				payment.Deposited = false;
				payment.DepositDate = null;
				payment.DepositType = null;
				payment.DepositNbr = null;

                payment.CuryVatTaxableTotal = 0m;
                payment.CuryVatExemptTotal = 0m;
				payment = this.Document.Update(payment);

				if (info != null)
				{
					CurrencyInfo b_info = (CurrencyInfo)PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<ARCashSale.curyInfoID>>>>.Select(this, null);
					b_info.CuryID = info.CuryID;
					b_info.CuryEffDate = info.CuryEffDate;
					b_info.CuryRateTypeID = info.CuryRateTypeID;
					b_info.CuryRate = info.CuryRate;
					b_info.RecipRate = info.RecipRate;
					b_info.CuryMultDiv = info.CuryMultDiv;
					this.currencyinfo.Update(b_info);
				}
			}

			this.FieldDefaulting.AddHandler<ARTran.salesPersonID>((sender, e) => { e.NewValue = null; e.Cancel = true; });

			foreach (ARTran tran in PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Select(this, doc.DocType, doc.RefNbr))
			{
				tran.TranType = null;
				tran.RefNbr = null;
				tran.DrCr = null;
				tran.Released = null;
                tran.CuryInfoID = null;

				SalesPerson sp = (SalesPerson)PXSelectorAttribute.Select<ARTran.salesPersonID>(this.Transactions.Cache, tran);
				if (sp == null || sp.IsActive == false)
					tran.SalesPersonID = null;

				this.Transactions.Insert(tran);
			}

			this.RowInserting.AddHandler<ARSalesPerTran>((sender, e) => { e.Cancel = true; });

			foreach (ARSalesPerTran salespertran in PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>>>>.Select(this, doc.DocType, doc.RefNbr))
			{
				ARSalesPerTran newtran = PXCache<ARSalesPerTran>.CreateCopy(salespertran);

				newtran.DocType = Document.Current.DocType;
				newtran.RefNbr = Document.Current.RefNbr;
				newtran.Released = false;
				newtran.CuryInfoID = null;
				newtran.CuryCommnblAmt *= -1m;
				newtran.CuryCommnAmt *= -1m;

				SalesPerson sp = (SalesPerson)PXSelectorAttribute.Select<ARSalesPerTran.salespersonID>(this.salesPerTrans.Cache, newtran);
				if (!(sp == null || sp.IsActive == false))
				{
					this.salesPerTrans.Update(newtran);
				}
			}    

			TaxAttribute.SetTaxCalc<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(this.Transactions.Cache, null, TaxCalc.ManualCalc);
         
            foreach (ARTaxTran tax in PXSelect<ARTaxTran, Where<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>>>>.Select(this, doc.DocType, doc.RefNbr))
            {
                ARTaxTran new_artax = new ARTaxTran();
                new_artax.TaxID = tax.TaxID;

                new_artax = this.Taxes.Insert(new_artax);

                if (new_artax != null)
                {
                    new_artax = PXCache<ARTaxTran>.CreateCopy(new_artax);
                    new_artax.TaxRate = tax.TaxRate;
                    new_artax.CuryTaxableAmt = tax.CuryTaxableAmt;
                    new_artax.CuryTaxAmt = tax.CuryTaxAmt;
                    new_artax = this.Taxes.Update(new_artax);
                }
            }

			foreach (PXResult<ARPaymentChargeTran> paycharge in PXSelect<ARPaymentChargeTran, Where<ARPaymentChargeTran.docType, Equal<Required<ARPayment.docType>>, And<ARPaymentChargeTran.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Select(this, doc.DocType, doc.RefNbr))
			{
				ARPaymentChargeTran charge = PXCache<ARPaymentChargeTran>.CreateCopy((ARPaymentChargeTran)paycharge);
				charge.DocType = Document.Current.DocType;
				charge.Released = false;
				charge.CuryInfoID = Document.Current.CuryInfoID;
				PaymentCharges.Insert(charge);
			}
		}
		#endregion
	}

	public class ARDataEntryGraph<TGraph, TPrimary> : PXGraph<TGraph, TPrimary>
		where TGraph : PXGraph
		where TPrimary : ARRegister, new()
	{
		public PXAction<TPrimary> release;
		[PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable Release(PXAdapter adapter)
		{
			return adapter.Get();
		}

		public PXAction<TPrimary> voidCheck;
		[PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
		[PXProcessButton]
		public virtual IEnumerable VoidCheck(PXAdapter adapter)
		{
			return adapter.Get();
		}

		public PXAction<TPrimary> viewBatch;
		[PXUIField(DisplayName = "Review Batch", Visible = false, MapEnableRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable ViewBatch(PXAdapter adapter)
		{
			foreach (TPrimary ardoc in adapter.Get<TPrimary>())
			{
				if (!String.IsNullOrEmpty(ardoc.BatchNbr))
				{
					JournalEntry graph = PXGraph.CreateInstance<JournalEntry>();
					graph.BatchModule.Current = PXSelect<Batch,
						Where<Batch.module, Equal<BatchModule.moduleAR>,
						And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>
						.Select(this, ardoc.BatchNbr);
					throw new PXRedirectRequiredException(graph, "Current batch record");
				}
			}
			return adapter.Get();
		}

		public PXAction<TPrimary> action;
		[PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
		[PXButton]
		protected virtual IEnumerable Action(PXAdapter adapter,
			[PXString()]
			string ActionName)
		{
			if (!string.IsNullOrEmpty(ActionName))
			{
				PXAction action = this.Actions[ActionName];

				if (action != null)
				{
					Save.Press();
					List<object> result = new List<object>();
					foreach (object data in action.Press(adapter))
					{
						result.Add(data);
					}
					return result;
				}
			}			
			return adapter.Get();
		}

		public PXAction<TPrimary> inquiry;
		[PXUIField(DisplayName = "Inquiries", MapEnableRights = PXCacheRights.Select)]
		[PXButton(MenuAutoOpen = true)]
		protected virtual IEnumerable Inquiry(PXAdapter adapter,
			[PXString()]
			string ActionName
			)
		{
			if (!string.IsNullOrEmpty(ActionName))
			{
				PXAction action = this.Actions[ActionName];

				if (action != null)
				{
					Save.Press();
					foreach (object data in action.Press(adapter)) ;
				}
			}
			return adapter.Get();
		}

		public PXAction<TPrimary> report;
		[PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
		[PXButton(SpecialType = PXSpecialButtonType.Report)]
		protected virtual IEnumerable Report(PXAdapter adapter,
			[PXString(8)]
			[PXStringList(new string[] { "AR610500", "AR622000", "AR641000" }, 
				new string[] { "AR Edit Detailed", "AR Register Detailed", "Invoice/Memo Form" })]
			string reportID
			)
		{
		    PXReportRequiredException ex = null;
			foreach (TPrimary doc in adapter.Get<TPrimary>())
			{
				var parameters = new Dictionary<string, string>();
				if (this.Caches[typeof(TPrimary)].GetStatus(doc) == PXEntryStatus.Notchanged)
				{
					this.Caches[typeof(TPrimary)].SetStatus(doc, PXEntryStatus.Updated);
				}

				if (reportID == "AR641000")
				{
					parameters["ARInvoice.DocType"] = doc.DocType;
					parameters["ARInvoice.RefNbr" ] = doc.RefNbr;
				}
				else if (reportID == "AR610500")
				{
					parameters["DocType"] = doc.DocType;
					parameters["RefNbr"] = doc.RefNbr;
					parameters["BranchID"] = null;
					parameters["PeriodTo"] = null;
					parameters["PeriodFrom"] = null;
				}
				else if (reportID == "AR622000")
				{
					parameters["DocType"] = doc.DocType;
					parameters["RefNbr"] = doc.RefNbr;
					parameters["BranchID"] = null;
					parameters["LedgerID"] = null;
					parameters["StartPeriodID"] = null;
					parameters["EndPeriodID"] = null;
				}

				ex = PXReportRequiredException.CombineReport(ex, GetCustomerReportID(reportID, doc), parameters);					
			}
			
			this.Save.Press();
			if (ex != null)	throw ex;
			
			return adapter.Get();
		}

		protected virtual string GetCustomerReportID(string reportID, TPrimary doc)
		{
			return reportID;
		}

		public ARDataEntryGraph()
			: base()
		{
			FieldDefaulting.AddHandler<BAccountR.type>((sender, e) => { if (e.Row != null) e.NewValue = BAccountType.CustomerType; });
		}

		#region Discount
		/// <summary>
		/// Finds Flat price in Discounts and sets UnitPrice.
		/// True if Flat price is applicable for the given line; otherwise false.
		/// </summary>
		protected virtual bool SetFlatUnitPrice(PXCache sender, ARTran row)
		{
			decimal? curyFlatPrice = GetFlatUnitPrice(sender, row);
			if (curyFlatPrice != null)
			{
				sender.SetValueExt<ARTran.curyUnitPrice>(row, curyFlatPrice);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns Flat price if applicable; otherwise null
		/// </summary>
		protected virtual decimal? GetFlatUnitPrice(PXCache sender, ARTran row)
		{
			if (row.InventoryID != null && row.UOM != null && SODiscountEngine<ARTran>.IsFlatPriceApplicable(sender, row))
			{
				int? customerLocationID = (int?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.customerLocationID>(this.Caches[typeof(TPrimary)].Current);
				DateTime? docDate = (DateTime?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.docDate>(this.Caches[typeof(TPrimary)].Current);

				if (row.TranType == ARDocType.CreditMemo && row.OrigInvoiceDate != null)
				{
					docDate = row.OrigInvoiceDate.Value;
				}


				return SODiscountEngine<ARTran>.CalculateFlatPrice(sender, row, customerLocationID, docDate.Value);
			}

			return null;
		}

		

		/// <summary>
		/// Sets Discounts DetDisc1, DetDisc2, DocDisc1, DocDisc2 for the given line. 
		/// </summary>
		protected virtual void SetDiscounts(PXCache sender, ARTran row)
		{
			if (row != null && row.IsFree != true
							&& row.LineType != SOLineType.Discount
							&& row.LineType != SOLineType.Freight
							&& row.SOOrderType != SOOrderTypeConstants.CreditMemo)
			{
				int? customerLocationID = (int?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.customerLocationID>(this.Caches[typeof(TPrimary)].Current);
				DateTime? docDate = (DateTime?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.docDate>(this.Caches[typeof(TPrimary)].Current);

				if (row.TranType == ARDocType.CreditMemo && row.OrigInvoiceDate != null)
				{
					docDate = row.OrigInvoiceDate.Value;
				}

				SODiscountEngine<ARTran>.SetDiscounts(sender, row, customerLocationID, docDate.Value);
			}
		}

		/// <summary>
		/// Calculates Line discount.  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="line"></param>
		protected virtual void RecalculateDiscounts(PXCache sender, ARTran line)
		{
			if (line.Qty != null && line.CuryExtPrice != null )
			{
				if (line.ManualDisc == false)
				{
					int? customerLocationID = (int?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.customerLocationID>(this.Caches[typeof(TPrimary)].Current);
					DateTime? docDate = (DateTime?)this.Caches[typeof(TPrimary)].GetValue<ARRegister.docDate>(this.Caches[typeof(TPrimary)].Current);

					if (line.TranType == ARDocType.CreditMemo && line.OrigInvoiceDate != null)
					{
						docDate = line.OrigInvoiceDate.Value;
					}

                    SODiscountEngine<ARTran>.CalculateLineDiscounts(sender, line, customerLocationID, docDate.Value);
				}
			}
		}
		#endregion
	}	
}
