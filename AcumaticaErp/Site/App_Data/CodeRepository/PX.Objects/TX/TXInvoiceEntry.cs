using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Data.EP;

namespace PX.Objects.TX
{
	public class TXInvoiceEntry : APInvoiceEntry
	{
		public PXFilter<AddBillFilter> Filter;
		public PXSelect<APInvoice> DocumentList;

		public PXAction<APInvoice> addInvoices;
		[PXUIField(DisplayName = "Add Documents", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Refresh)]
		public virtual IEnumerable AddInvoices(PXAdapter adapter)
		{
			if (DocumentList.View.AskExt() == WebDialogResult.OK)
			{
				AddInvoiceProc();
			}
			return adapter.Get();
		}

		public PXAction<APInvoice> addInvoicesOK;
		[PXUIField(DisplayName = "Add", Visible = false)]
		[PXButton()]
		public virtual IEnumerable AddInvoicesOK(PXAdapter adapter)
		{
			AddInvoiceProc();
			return adapter.Get();
		}

		protected virtual void AddInvoiceProc()
		{ 
			foreach(APInvoice item in DocumentList.Cache.Updated)
			{
				try
				{
					if (item.Selected == true)
					{
						APTaxTran tran = new APTaxTran();
						tran.TranType = Document.Current.DocType;
						tran.RefNbr = Document.Current.RefNbr;
						tran.OrigTranType = item.DocType;
						tran.OrigRefNbr = item.RefNbr;
						tran.TaxID = Filter.Current.TaxID;

						Taxes.Insert(tran);
					}
				}
				finally
				{
					DocumentList.Cache.SetStatus(item, PXEntryStatus.Notchanged);
					DocumentList.Cache.Remove(item);
				}
			}
		}

		public IEnumerable documentlist()
		{
			PXSelectBase<APInvoice> cmd = new PXSelectJoin<APInvoice, LeftJoin<APTaxTran, On<APTaxTran.tranType, Equal<APInvoice.docType>, And<APTaxTran.refNbr, Equal<APInvoice.refNbr>, And<APTaxTran.taxID, Equal<Current<AddBillFilter.taxID>>>>>>, Where<APInvoice.released, Equal<True>, And<APInvoice.origModule, NotEqual<GL.BatchModule.moduleTX>, And<APTaxTran.refNbr, IsNull>>>>(this);

			if (string.IsNullOrEmpty(Filter.Current.TaxID))
			{
				yield break;
			}

			if (Filter.Current.StartDate != null)
			{
				cmd.WhereAnd<Where<APInvoice.docDate, GreaterEqual<Current<AddBillFilter.startDate>>>>();
			}

			if (Filter.Current.EndDate != null)
			{
				cmd.WhereAnd<Where<APInvoice.docDate, LessEqual<Current<AddBillFilter.endDate>>>>();
			}

			if (Filter.Current.VendorID != null)
			{
				cmd.WhereAnd<Where<APInvoice.vendorID, Equal<Current<AddBillFilter.vendorID>>>>();
			}

			if (string.IsNullOrEmpty(Filter.Current.InvoiceNbr) == false)
			{
				cmd.WhereAnd<Where<APInvoice.invoiceNbr, Equal<Current<AddBillFilter.invoiceNbr>>>>();
			}

			foreach (APInvoice item in cmd.Select())
			{
				yield return item;
			}
		}

		public TXInvoiceEntry()
		{
			Document.View = new PXView(this, false, new Select2<APInvoice,
			LeftJoin<Vendor, On<APInvoice.vendorID, Equal<Vendor.bAccountID>>>,
			Where<APInvoice.docType, Equal<Optional<APInvoice.docType>>,
			And<APInvoice.origModule, Equal<GL.BatchModule.moduleTX>,
			And<Where<Vendor.bAccountID, IsNull,
			Or<Match<Vendor, Current<AccessInfo.userName>>>>>>>>());

			this.Views["Document"] = Document.View;

			PXUIFieldAttribute.SetVisible<TaxRev.taxID>(Caches[typeof(TaxRev)], null);
		}

		public override IEnumerable ExecuteSelect(string viewName, object[] parameters, object[] searches, string[] sortcolumns, bool[] descendings, PXFilterRow[] filters, ref int startRow, int maximumRows, ref int totalRows)
		{
			APInvoice current = Document.Current;
			try
			{
				return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
			}
			finally
			{
				if (viewName == "DocumentList")
				{
					Document.Current = current;
				}
			}
		}

		public override int ExecuteUpdate(string viewName, IDictionary keys, IDictionary values, params object[] parameters)
		{
			APInvoice current = Document.Current;
			try
			{
				return base.ExecuteUpdate(viewName, keys, values, parameters);
			}
			finally
			{
				if (viewName == "DocumentList")
				{
					Document.Current = current;
				}
			}
		}

		[PXDBString(3, IsKey = true, IsFixed = true)]
		[APInvoiceType.TaxInvoiceList()]
		[PXDefault(APInvoiceType.Invoice)]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
		[PXFieldDescription]
		protected virtual void APInvoice_DocType_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
		[APInvoiceType.RefNbr(typeof(Search2<APInvoice.refNbr, 
			InnerJoin<Vendor, On<APInvoice.vendorID, Equal<Vendor.bAccountID>>>,
			Where<APInvoice.docType,Equal<Current<APInvoice.docType>>, 
			And<APInvoice.origModule, Equal<GL.BatchModule.moduleTX>,
			And<Match<Vendor, Current<AccessInfo.userName>>>>>, OrderBy<Desc<APInvoice.refNbr>>>), Filterable = true)]
		[APInvoiceType.Numbering()]
		[PXFieldDescription]
		protected virtual void APInvoice_RefNbr_CacheAttached(PXCache sender)
		{ 
		}

		[PXFormula(typeof(APInvoice.curyTaxTotal))]
		[PXDBCurrency(typeof(APRegister.curyInfoID), typeof(APRegister.docBal), BaseCalc = false)]
		[PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		protected virtual void APInvoice_CuryDocBal_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(3, IsFixed = true, IsKey = true)]
		[PXUIField(DisplayName = "Orig. Tran. Type")]
		[APInvoiceType.TaxInvoiceList()]
		[PXDefault(APInvoiceType.Invoice)]
		protected virtual void APTaxTran_OrigTranType_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXUIField(DisplayName = "Orig. Doc. Number")]
		[PXSelectorAttribute(typeof(Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Optional<APTaxTran.origTranType>>, And<APInvoice.released, Equal<True>, And<APInvoice.origModule, NotEqual<GL.BatchModule.moduleTX>>>>>))]
		[PXDefault()]
		protected virtual void APTaxTran_OrigRefNbr_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(10, IsUnicode = true)]
		[PXFormula(typeof(Selector<APTaxTran.origRefNbr, APInvoice.taxZoneID>))]
		[PXSelector(typeof(Search<TaxZone.taxZoneID>))]
		[PXUIField(DisplayName = "Tax Zone")]
		protected virtual void APTaxTran_TaxZoneID_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search2<TaxRev.taxID, InnerJoin<Tax, On<Tax.taxID, Equal<TaxRev.taxID>>>, Where<Tax.directTax, Equal<True>, And<TaxRev.taxType, Equal<TaxType.purchase>>>>), 
			new Type[] { typeof(TaxRev.taxID), typeof(Tax.descr) } )]
		protected virtual void APTaxTran_TaxID_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBDecimal(6)]
		[PXFormula(typeof(Selector<APTaxTran.taxID, TaxRev.taxRate>))]
		[PXUIField(DisplayName = "Tax Rate", Visibility = PXUIVisibility.Visible, Enabled = true)]
		protected virtual void APTaxTran_TaxRate_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBCurrency(typeof(APTaxTran.curyInfoID), typeof(APTaxTran.taxableAmt))]
		[PXUIField(DisplayName = "Taxable Amount")]
		[PXFormula(typeof(Switch<Case<Where<APTaxTran.origRefNbr, IsNotNull>, CuryConvert<Selector<APTaxTran.origRefNbr, APInvoice.curyOrigDocAmt>, Selector<APTaxTran.origRefNbr, APInvoice.curyInfoID>, Parent<APInvoice.curyInfoID>>>, decimal0>))]
		protected virtual void APTaxTran_CuryTaxableAmt_CacheAttached(PXCache sender)
		{
		}

		[PXDBCurrency(typeof(APTaxTran.curyInfoID), typeof(APTaxTran.taxAmt))]
		[PXUIField(DisplayName = "Tax Amount")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXFormula(typeof(Mult<APTaxTran.curyTaxableAmt, Div<APTaxTran.taxRate, decimal100>>))]
		[PXUnboundFormula(typeof(Switch<
					Case<Where<APTaxTran.tranType, Equal<APInvoiceType.debitAdj>, And<APTaxTran.origTranType, NotEqual<APInvoiceType.debitAdj>,
					Or<APTaxTran.tranType, NotEqual<APInvoiceType.debitAdj>, And<APTaxTran.origTranType, Equal<APInvoiceType.debitAdj>>>>>,
					Minus<APTaxTran.curyTaxAmt>>, APTaxTran.curyTaxAmt>), typeof(SumCalc<APInvoice.curyTaxTotal>))] 
		protected virtual void APTaxTran_CuryTaxAmt_CacheAttached(PXCache sender)
		{ 
		}

		[PXDBString(10, IsUnicode = true)]
		protected new virtual void APTran_TaxCategoryID_CacheAttached(PXCache sender)
		{ 
		}

		protected override void APInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			if (e.Row == null) return;

			APInvoice item = (APInvoice)e.Row;

			if (item.OrigModule == GL.BatchModule.TX)
			{
				base.APInvoice_RowSelected(sender, e);

				addInvoices.SetEnabled(item.VendorID != null && item.VendorLocationID != null && item.Released == false && item.Voided == false);
			}
			else
			{
				PXUIFieldAttribute.SetEnabled(DocumentList.Cache, e.Row, false);
				PXUIFieldAttribute.SetEnabled<APInvoice.selected>(DocumentList.Cache, e.Row, true);
			}

			Transactions.Cache.AllowInsert = false;
			Transactions.Cache.AllowUpdate = false;
			Transactions.Cache.AllowDelete = false;
		}

		protected virtual void APTaxTran_TaxID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<APTaxTran.vendorID>(e.Row);
			sender.SetDefaultExt<APTaxTran.taxType>(e.Row);
			sender.SetDefaultExt<APTaxTran.taxBucketID>(e.Row);
			sender.SetDefaultExt<APTaxTran.accountID>(e.Row);
			sender.SetDefaultExt<APTaxTran.subID>(e.Row);

			APTaxTran tran = (APTaxTran)e.Row;

			APTaxTran orig_tran = PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<APTaxTran.taxID, Equal<Required<APTaxTran.taxID>>>>>>.Select(this, tran.OrigTranType, tran.OrigRefNbr, tran.TaxID);
			if (orig_tran != null)
			{
				sender.RaiseExceptionHandling<APTaxTran.taxID>(e.Row, null, new PXSetPropertyException(Messages.OriginalDocumentAlreadyContainsTaxRecord, PXErrorLevel.Warning));
				sender.SetValueExt<APTaxTran.curyTaxableAmt>(e.Row, 0m);
			}
		}

		protected override void APTaxTran_TaxZoneID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
		}

		protected override void APTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			APTaxTran row = (APTaxTran)e.Row;
			if ((e.Operation == PXDBOperation.Insert || e.Operation == PXDBOperation.Update) && string.IsNullOrEmpty(row.TaxZoneID))
			{
				sender.RaiseExceptionHandling<APTaxTran.taxZoneID>(e.Row, null, new PXSetPropertyException(ErrorMessages.FieldIsEmpty));
			}
		}

		protected virtual void APInvoice_OrigModule_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			e.NewValue = GL.BatchModule.TX;
			e.Cancel = true;
		}

	}

	public class CuryConvert<CuryAmount, CuryInfoID, ToCuryInfoID> : BqlFormula<CuryAmount, CuryInfoID, ToCuryInfoID>, IBqlOperand 
		where CuryAmount : IBqlOperand
		where CuryInfoID : IBqlOperand
		where ToCuryInfoID : IBqlOperand 
	{
		public override void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			decimal? curyAmount = (decimal?)Calculate<CuryAmount>(cache, item);
			long? curyInfoID = (long?)Calculate<CuryInfoID>(cache, item);
			long? toCuryInfoID = (long?)Calculate<ToCuryInfoID>(cache, item);

			CurrencyInfo orig_info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>.Select(cache.Graph, curyInfoID);
			CurrencyInfo info = PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>.Select(cache.Graph, toCuryInfoID);

			decimal val;
			decimal curyval;

			if (curyAmount == null || info == null || orig_info == null)
			{
				value = 0m;
				return;
			}

			if (string.Equals(info.CuryID, orig_info.CuryID))
			{
				value = curyAmount;
			}
			else
			{
				PXCurrencyAttribute.CuryConvBase(cache, orig_info, (decimal)curyAmount, out val);
				PXCurrencyAttribute.CuryConvCury(cache, info, val, out curyval);
				value = curyval;
			}
		}
	}

	[Serializable()]
	public class AddBillFilter : IBqlTable
	{
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		protected string _TaxID;
		[PXDBString(Tax.taxID.Length, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search<Tax.taxID, Where<Tax.directTax, Equal<True>>>))]
		public virtual String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true, Required = false)]
		[PXDefault()]
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
		#region InvoiceNbr
		public abstract class invoiceNbr : PX.Data.IBqlField
		{
		}
		protected String _InvoiceNbr;
		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String InvoiceNbr
		{
			get
			{
				return this._InvoiceNbr;
			}
			set
			{
				this._InvoiceNbr = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : IBqlField
		{
		}
		protected DateTime? _StartDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Date From")]
		public virtual DateTime? StartDate
		{
			get
			{
				return _StartDate;
			}
			set
			{
				_StartDate = value;
			}
		}
		#endregion
		#region EndDate
		public abstract class endDate : IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate]
		[PXUIField(DisplayName = "Date To")]
		[PXDefault(typeof(APInvoice.docDate))]
		public virtual DateTime? EndDate
		{
			get
			{
				return _EndDate;
			}
			set
			{
				_EndDate = value;
			}
		}
		#endregion	
	}
}
