using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL.Overrides.PostGraph;

namespace PX.Objects.CM
{
	[TableAndChartDashboardType]
	public class RevalueGLAccounts : PXGraph<RevalueGLAccounts>
	{
		public PXCancel<RevalueFilter> Cancel;
		public PXFilter<RevalueFilter> Filter;
		[PXFilterable]
		public PXFilteredProcessing<RevaluedGLHistory, RevalueFilter, Where<boolTrue, Equal<boolTrue>>, OrderBy<Asc<RevaluedGLHistory.ledgerID, Asc<RevaluedGLHistory.accountID, Asc<RevaluedGLHistory.subID>>>>> GLAccountList;
		public PXSelect<CurrencyInfo> currencyinfo;
		public PXSetup<GLSetup> glsetup;
		public PXSetup<CMSetup> cmsetup;
		public PXSetup<Company> company;

		public RevalueGLAccounts()
		{
			object setup = cmsetup.Current;
			setup = glsetup.Current;
			if (cmsetup.Current.MCActivated != true) 
				throw new Exception(Messages.MultiCurrencyNotActivated);
			GLAccountList.SetProcessCaption(Messages.Revalue);
			GLAccountList.SetProcessAllVisible(false);

			PXUIFieldAttribute.SetEnabled<RevaluedGLHistory.finPtdRevalued>(GLAccountList.Cache, null, true);
		}

		protected virtual void RevalueFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			RevalueFilter filter = (RevalueFilter)e.Row;
			if (filter != null)
			{
				GLAccountList.SetProcessDelegate(
					delegate(List<RevaluedGLHistory> list)
					{	
						Revalue(filter, list);
					}				
				);
			}
		}

		protected virtual void RevalueFilter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			if (((RevalueFilter)e.Row).CuryEffDate != null)
			{
				((RevalueFilter)e.Row).CuryEffDate = ((DateTime)((RevalueFilter)e.Row).CuryEffDate).AddDays(-1);
			}
		}

		protected virtual void RevalueFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			sender.SetDefaultExt<RevalueFilter.lastGLFinPeriodID>(e.Row);
		}

		protected virtual void RevalueFilter_FinPeriodID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<RevalueFilter.curyEffDate>(e.Row);

			if (((RevalueFilter)e.Row).CuryEffDate != null)
			{
				((RevalueFilter)e.Row).CuryEffDate = ((DateTime)((RevalueFilter)e.Row).CuryEffDate).AddDays(-1);
			}
			GLAccountList.Cache.Clear();
		}

		protected virtual void RevalueFilter_CuryEffDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			GLAccountList.Cache.Clear();
		}

		protected virtual void RevalueFilter_CuryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			GLAccountList.Cache.Clear();
		}

		protected virtual void RevalueFilter_TotalRevalued_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
		{
			if (e.Row != null)
			{
				decimal val = 0m;
				foreach (RevaluedGLHistory res in GLAccountList.Cache.Updated)
				{
					if ((bool)res.Selected)
					{
						val += (decimal)res.FinPtdRevalued;
					}
				}
				TimeSpan  timespan;
				Exception ex;
				if (PXLongOperation.GetStatus(UID, out timespan, out ex) == PXLongRunStatus.Completed)
				{
					if(val == 0)
						Filter.Cache.RaiseExceptionHandling<RevalueFilter.totalRevalued>(e.Row, val, new PXSetPropertyException(Messages.NoRevaluationEntryWasMade, PXErrorLevel.Warning));
					else
					{
						e.ReturnValue = val;
						e.Cancel = true;
					}
				}
				
			}
		}

		public virtual IEnumerable glaccountlist()
		{
			foreach (PXResult<GLHistoryByPeriod, RevaluedGLHistory, Account> res in PXSelectJoin<GLHistoryByPeriod,
				InnerJoin<RevaluedGLHistory, 
					On<RevaluedGLHistory.ledgerID, Equal<GLHistoryByPeriod.ledgerID>, 
					And<RevaluedGLHistory.branchID, Equal<GLHistoryByPeriod.branchID>, 
					And<RevaluedGLHistory.accountID, Equal<GLHistoryByPeriod.accountID>, 
					And<RevaluedGLHistory.subID, Equal<GLHistoryByPeriod.subID>, 
					And<RevaluedGLHistory.finPeriodID, Equal<GLHistoryByPeriod.lastActivityPeriod>>>>>>,
				InnerJoin<Account, 
					On<Account.accountID, Equal<GLHistoryByPeriod.accountID>, And<Account.curyID, IsNotNull>>,
				InnerJoin<Branch, On<Branch.branchID, Equal<GLHistoryByPeriod.branchID>, And<Branch.ledgerID, Equal<GLHistoryByPeriod.ledgerID>>>>>>,
				Where<Account.curyID, Equal<Current<RevalueFilter.curyID>>,
					And<GLHistoryByPeriod.finPeriodID, Equal<Current<RevalueFilter.finPeriodID>>,
					And<Where<RevaluedGLHistory.curyFinYtdBalance, NotEqual<decimal0>, Or<RevaluedGLHistory.finYtdBalance, NotEqual<decimal0>>>>>>>.Select(this))
			{
				GLHistoryByPeriod histbyper = res;
				RevaluedGLHistory hist = PXCache<RevaluedGLHistory>.CreateCopy(res);
				RevaluedGLHistory existing;

				if ((existing = GLAccountList.Locate(hist)) != null)
				{
					yield return existing;
					continue;
				}
				else
				{
					GLAccountList.Cache.SetStatus(hist, PXEntryStatus.Held);
				}

				if (string.IsNullOrEmpty(hist.CuryRateTypeID = ((Account)res).RevalCuryRateTypeId))
				{
					hist.CuryRateTypeID = cmsetup.Current.GLRateTypeReval;
				}

				if (string.IsNullOrEmpty(hist.CuryRateTypeID))
				{
					GLAccountList.Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRateTypeID>(hist, null, new PXSetPropertyException(Messages.RateTypeNotFound));
				}
				else
				{
					CurrencyRateByDate curyrate = PXSelect<CurrencyRateByDate,
						Where<CurrencyRateByDate.fromCuryID, Equal<Current<RevalueFilter.curyID>>,
					And<CurrencyRateByDate.toCuryID, Equal<Current<Company.baseCuryID>>,
					And<CurrencyRateByDate.curyRateType, Equal<Required<Account.revalCuryRateTypeId>>,
					And2<Where<CurrencyRateByDate.curyEffDate, LessEqual<Current<RevalueFilter.curyEffDate>>, Or<CurrencyRateByDate.curyEffDate, IsNull>>, And<Where<CurrencyRateByDate.nextEffDate, Greater<Current<RevalueFilter.curyEffDate>>, Or<CurrencyRateByDate.nextEffDate, IsNull>>>>>>>>.Select(this, hist.CuryRateTypeID);

					if (curyrate == null || curyrate.CuryMultDiv == null)
					{
						hist.CuryMultDiv = "M";
						hist.CuryRate = 1m;
						GLAccountList.Cache.RaiseExceptionHandling<RevaluedGLHistory.curyRate>(hist, 1m, new PXSetPropertyException(Messages.RateNotFound, PXErrorLevel.RowWarning));
					}
					else
					{
						hist.CuryRate = curyrate.CuryRate;
						hist.CuryMultDiv = curyrate.CuryMultDiv;
					}

					CurrencyInfo info = new CurrencyInfo();
					info.BaseCuryID = company.Current.BaseCuryID;
					info.CuryID = hist.CuryID;
					info.CuryMultDiv = hist.CuryMultDiv;
					info.CuryRate = hist.CuryRate;

					decimal baseval;
					PXCurrencyAttribute.CuryConvBase(currencyinfo.Cache, info, (decimal)hist.CuryFinYtdBalance, out baseval);
					hist.FinYtdRevalued = baseval;
					hist.FinPtdRevalued = hist.FinYtdRevalued - hist.FinYtdBalance;
				}

				yield return hist;
			}
		}

		public static void Revalue(RevalueFilter filter, List<RevaluedGLHistory> list)
		{
			JournalEntry je = PXGraph.CreateInstance<JournalEntry>();
			PostGraph pg = PXGraph.CreateInstance<PostGraph>();
			PXCache cache = je.Caches[typeof(CuryAcctHist)];
			PXCache basecache = je.Caches[typeof(AcctHist)];
			je.Views.Caches.Add(typeof(CuryAcctHist));
			je.Views.Caches.Add(typeof(AcctHist));

            string extRefNbrNumbering = je.CMSetup.Current.ExtRefNbrNumberingID;            
            if (string.IsNullOrEmpty(extRefNbrNumbering) == false)
            {
                RevaluationRefNbrHelper helper = new RevaluationRefNbrHelper(extRefNbrNumbering);
                je.RowPersisting.AddHandler<GLTran>(helper.OnRowPersisting);
            }

			DocumentList<Batch> created = new DocumentList<Batch>(je);

			Currency currency = PXSelect<Currency, Where<Currency.curyID, Equal<Required<Currency.curyID>>>>.Select(je, filter.CuryID);

			using (PXTransactionScope ts = new PXTransactionScope())
			{
				foreach (RevaluedGLHistory hist in list)
				{
					if (hist.FinPtdRevalued == 0m)
					{
						continue;
					}

                    if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
					{
						je.Save.Press();

						if (created.Find(je.BatchModule.Current) == null)
						{
							created.Add(je.BatchModule.Current);
						}
					}

					Batch cmbatch = created.Find<Batch.branchID>(hist.BranchID) ?? new Batch();
					if (cmbatch.BatchNbr == null)
					{
						je.Clear();

						CurrencyInfo info = new CurrencyInfo();
						info.CuryID = hist.CuryID;
						info.CuryEffDate = filter.CuryEffDate;
						info = je.currencyinfo.Insert(info) ?? info;

						cmbatch = new Batch();
						cmbatch.BranchID = hist.BranchID;
						cmbatch.Module = "CM";
						cmbatch.Status = "U";
						cmbatch.AutoReverse = false;
						cmbatch.Released = true;
						cmbatch.Hold = false;
						cmbatch.DateEntered = filter.CuryEffDate;
						cmbatch.FinPeriodID = filter.FinPeriodID;
						cmbatch.TranPeriodID = filter.FinPeriodID;
						cmbatch.CuryID = hist.CuryID;
						cmbatch.CuryInfoID = info.CuryInfoID;
						cmbatch.DebitTotal = 0m;
						cmbatch.CreditTotal = 0m;
						cmbatch.Description = filter.Description;
						je.BatchModule.Insert(cmbatch);

						CurrencyInfo b_info = je.currencyinfo.Select();
						if (b_info != null)
						{
							b_info.CuryID = hist.CuryID;
							b_info.CuryEffDate = filter.CuryEffDate;
							b_info.CuryRate = 1m;
							b_info.CuryMultDiv = "M";
							je.currencyinfo.Update(b_info);
						}
					}
					else
					{
						je.BatchModule.Current = je.BatchModule.Search<Batch.batchNbr>(cmbatch.BatchNbr, cmbatch.Module);
					}

					{
						GLTran tran = new GLTran();
						tran.SummPost = false;
						tran.AccountID = hist.AccountID;
						tran.SubID = hist.SubID;
						tran.CuryDebitAmt = 0m;
						tran.CuryCreditAmt = 0m;

						if (hist.AccountType == AccountType.Asset || hist.AccountType == AccountType.Expense)
						{
							tran.DebitAmt = (hist.FinPtdRevalued < 0m) ? 0m : hist.FinPtdRevalued;
							tran.CreditAmt = (hist.FinPtdRevalued < 0m) ? -1m * hist.FinPtdRevalued : 0m;
						}
						else
						{
							tran.DebitAmt = (hist.FinPtdRevalued < 0m) ? -1m * hist.FinPtdRevalued : 0m;
							tran.CreditAmt = (hist.FinPtdRevalued < 0m) ? 0m : hist.FinPtdRevalued;
						}

						tran.TranType = "REV";
						tran.TranClass = hist.AccountType;
						tran.RefNbr = string.Empty;
						tran.TranDesc = string.Empty;
						tran.TranPeriodID = filter.FinPeriodID;
						tran.FinPeriodID = filter.FinPeriodID;
						tran.TranDate = filter.CuryEffDate;
						tran.CuryInfoID = null;
						tran.Released = true;
						tran.ReferenceID = null;

						je.GLTranModuleBatNbr.Insert(tran);
					}

					foreach (GLTran tran in je.GLTranModuleBatNbr.SearchAll<Asc<GLTran.tranClass>>(new object[] { "G" }))
					{
						je.GLTranModuleBatNbr.Delete(tran);
					}

					{
						GLTran tran = new GLTran();
						tran.SummPost = true;
						tran.CuryDebitAmt = 0m;
						tran.CuryCreditAmt = 0m;

						if (je.BatchModule.Current.DebitTotal > je.BatchModule.Current.CreditTotal)
						{
							tran.AccountID = currency.RevalGainAcctID;
                            tran.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.revalGainSubID>(je, hist.BranchID, currency);
							tran.DebitAmt = 0m;
							tran.CreditAmt = (je.BatchModule.Current.DebitTotal - je.BatchModule.Current.CreditTotal);
						}
						else
						{
							tran.AccountID = currency.RevalLossAcctID;
                            tran.SubID = GainLossSubAccountMaskAttribute.GetSubID<Currency.revalLossSubID>(je, hist.BranchID, currency);
							tran.DebitAmt = (je.BatchModule.Current.CreditTotal - je.BatchModule.Current.DebitTotal);
							tran.CreditAmt = 0m;
						}

						tran.TranType = "REV";
						tran.TranClass = "G";
						tran.RefNbr = string.Empty;
						tran.TranDesc = string.Empty;
						tran.Released = true;
						tran.ReferenceID = null;

						je.GLTranModuleBatNbr.Insert(tran);
					}

					{
						AcctHist accthist = new AcctHist();
						accthist.BranchID = hist.BranchID;
						accthist.LedgerID = hist.LedgerID;
						accthist.AccountID = hist.AccountID;
						accthist.SubID = hist.SubID;
						accthist.FinPeriodID = filter.FinPeriodID;
						accthist.CuryID = hist.CuryID;
						accthist.BalanceType = "A";

						accthist = (AcctHist)basecache.Insert(accthist);
						accthist.FinPtdRevalued += hist.FinPtdRevalued;
					}

					{
						CuryAcctHist accthist = new CuryAcctHist();
						accthist.BranchID = hist.BranchID;
						accthist.LedgerID = hist.LedgerID;
						accthist.AccountID = hist.AccountID;
						accthist.SubID = hist.SubID;
						accthist.FinPeriodID = filter.FinPeriodID;
						accthist.CuryID = hist.CuryID;
						accthist.BalanceType = "A";
						accthist.BaseCuryID = je.currencyinfo.Current.BaseCuryID;

						accthist = (CuryAcctHist)cache.Insert(accthist);
					}
				}

				if (je.GLTranModuleBatNbr.Cache.IsInsertedUpdatedDeleted)
				{
					je.Save.Press();

					if (created.Find(je.BatchModule.Current) == null)
					{
						created.Add(je.BatchModule.Current);
					}
				}

				ts.Complete();
			}

			CMSetup cmsetup = PXSelect<CMSetup>.Select(je);

			for (int i = 0; i < created.Count; i++)
			{
				if (cmsetup.AutoPostOption == true)
				{
					pg.Clear();
					pg.PostBatchProc(created[i]);
				}
			}

            if (created.Count > 0)
            {
                je.BatchModule.Current = created[created.Count - 1];
                throw new PXRedirectRequiredException(je, "Preview");
            }
		}
	}

    [Serializable]
	[PXBreakInheritance()]
	public partial class RevaluedGLHistory : GLHistory
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
		#region CuryRateTypeID
		public abstract class curyRateTypeID : PX.Data.IBqlField
		{
		}
		protected String _CuryRateTypeID;
		[PXString(6, IsUnicode = true)]
		[PXUIField(DisplayName = "Currency Rate Type")]
		[PXSelector(typeof(CurrencyRateType.curyRateTypeID))]
		public virtual String CuryRateTypeID
		{
			get
			{
				return this._CuryRateTypeID;
			}
			set
			{
				this._CuryRateTypeID = value;
			}
		}
		#endregion
		#region CuryRate
		public abstract class curyRate : PX.Data.IBqlField
		{
		}
		protected Decimal? _CuryRate = 1m;
		[PXDecimal(6)]
		[PXUIField(DisplayName = "Currency Rate")]
		public virtual Decimal? CuryRate
		{
			get
			{
				return this._CuryRate;
			}
			set
			{
				this._CuryRate = value;
			}
		}
		#endregion
		#region CuryMultDiv
		public abstract class curyMultDiv : PX.Data.IBqlField
		{
		}
		protected String _CuryMultDiv = "M";
		[PXString(1, IsFixed = true)]
		public virtual String CuryMultDiv
		{
			get
			{
				return this._CuryMultDiv;
			}
			set
			{
				this._CuryMultDiv = value;
			}
		}
		#endregion
		#region AccountType
		public abstract class accountType : PX.Data.IBqlField
		{
		}
		protected string _AccountType;
		[PXString(1)]
		[PXDBScalar(typeof(Search<Account.type, Where<Account.accountID, Equal<GLHistory.accountID>>>))]
		[AccountType.List()]
		[PXUIField(DisplayName = "Type")]
		public virtual string AccountType
		{
			get
			{
				return this._AccountType;
			}
			set
			{
				this._AccountType = value;
			}
		}
		#endregion
		#region LedgerID
		public new abstract class ledgerID : PX.Data.IBqlField
		{
		}
		#endregion
		#region BranchID
		public new abstract class branchID : PX.Data.IBqlField
		{
		}
		[Branch(IsKey = true, IsDetail = true)]
		public override Int32? BranchID
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
		#region AccountID
		public new abstract class accountID : PX.Data.IBqlField
		{
		}
		[Account(IsKey = true, DescriptionField = typeof(Account.description))]
		public override Int32? AccountID
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
		#region SubID
		public new abstract class subID : PX.Data.IBqlField
		{
		}
		[SubAccount(IsKey = true, DescriptionField = typeof(Sub.description))]
		public override Int32? SubID
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
		#region FinPeriodID
		public new abstract class finPeriodID : PX.Data.IBqlField
		{
		}
		#endregion
		#region CuryID
		public new abstract class curyID : PX.Data.IBqlField
		{
		}
		#endregion
		#region CuryFinYtdBalance
		public new abstract class curyFinYtdBalance : PX.Data.IBqlField
		{
		}
		[PXDBCury(typeof(RevaluedGLHistory.curyID))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Foreign Currency Balance", Enabled = false)]
		public override Decimal? CuryFinYtdBalance
		{
			get
			{
				return this._CuryFinYtdBalance;
			}
			set
			{
				this._CuryFinYtdBalance = value;
			}
		}
		#endregion
		#region FinYtdBalance
		public new abstract class finYtdBalance : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Original Balance", Enabled = false)]
		public override Decimal? FinYtdBalance
		{
			get
			{
				return this._FinYtdBalance;
			}
			set
			{
				this._FinYtdBalance = value;
			}
		}
		#endregion
		#region FinYtdRevalued
		public abstract class finYtdRevalued : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinYtdRevalued;
		[PXBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Revalued Balance", Enabled = false)]
		[PXFormula(typeof(Add<RevaluedGLHistory.finYtdBalance, RevaluedGLHistory.finPtdRevalued>))]
		public virtual Decimal? FinYtdRevalued
		{
			get
			{
				return this._FinYtdRevalued;
			}
			set
			{
				this._FinYtdRevalued = value;
			}
		}
		#endregion
		#region FinPtdRevalued
		public new abstract class finPtdRevalued : PX.Data.IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Difference", Enabled = true)]
		public override Decimal? FinPtdRevalued
		{
			get
			{
				return this._FinPtdRevalued;
			}
			set
			{
				this._FinPtdRevalued = value;
			}
		}
		#endregion
	}
}
