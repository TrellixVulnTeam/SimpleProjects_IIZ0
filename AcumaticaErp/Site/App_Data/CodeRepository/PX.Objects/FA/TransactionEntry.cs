using System;
using PX.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PX.Objects.CS;
using PX.Objects.GL;
using FABookHist = PX.Objects.FA.Overrides.AssetProcess.FABookHist;

namespace PX.Objects.FA
{
	[TableAndChartDashboardType]
	public class TransactionEntry : PXGraph<TransactionEntry, FARegister>
    {
        #region Cache Attached
        [SubAccount(typeof(FATran.creditAccountID), typeof(FATran.branchID), DisplayName = "Credit Subaccount", Visibility = PXUIVisibility.Visible, Filterable = true)]
        protected virtual void FATran_CreditSubID_CacheAttached(PXCache sender)
        {
        }

        [SubAccount(typeof(FATran.debitAccountID), typeof(FATran.branchID), DisplayName = "Debit Subaccount", Visibility = PXUIVisibility.Visible, Filterable = true)]
        protected virtual void FATran_DebitSubID_CacheAttached(PXCache sender)
        {
        }
		#endregion

        #region Selects Declaration

        public PXSelect<FARegister> Document;
		public PXSelect<FAAccrualTran> Additions;
		public PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> Trans;
		public PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>> Asset;
        public PXSelect<FADetails> assetdetails;
		public PXSelect<FABookBalance> bookbalances;
		public PXSelect<FABookHist> bookhistory;
		public PXSetup<FASetup> fasetup;
		#endregion

		#region State
		DocumentList<FARegister> _created = null;
		public DocumentList<FARegister> created
		{
			get
			{
				return _created;
			}
		}

		readonly Dictionary<string, bool> _debit_enable = new Dictionary<string, bool>
		                                      					{
		           													{FATran.tranType.PurchasingPlus,    false},
		           													{FATran.tranType.PurchasingMinus,   true},
		           													{FATran.tranType.DepreciationPlus,  true},
		           													{FATran.tranType.DepreciationMinus,	false},
		           													{FATran.tranType.CalculatedPlus,	true},
		           													{FATran.tranType.CalculatedMinus,	false},
		           													{FATran.tranType.SalePlus,          true},
		           													{FATran.tranType.SaleMinus,         false},
		                                      					};

        public bool UpdateGL
        {
            get
            {
                return fasetup.Current.UpdateGL == true;
            }
        }
		#endregion

		#region Ctor
		public TransactionEntry()
		{
			var setup = fasetup.Current;
			_created = new DocumentList<FARegister>(this);
		}
		#endregion

		#region Runtime

        public static void SegregateRegister(PXGraph graph, int BranchID, string Origin, string PeriodID, DateTime? DocDate, string descr, DocumentList<FARegister> created)
		{
            PXCache doccache = graph.Caches[typeof(FARegister)];
            PXCache trancache = graph.Caches[typeof(FATran)];

            if (trancache.IsInsertedUpdatedDeleted)
            {
                graph.Actions.PressSave();
                if (doccache.Current != null && created.Find(doccache.Current) == null)
			{
                    created.Add((FARegister)doccache.Current);
                }
			}

            FARegister register = created.Find<FARegister.branchID, FARegister.origin, FARegister.finPeriodID>(BranchID, Origin, PeriodID) ?? new FARegister();

            if (register.RefNbr != null)
			{
                FARegister newreg = PXSelect<FARegister, Where<FARegister.refNbr, Equal<Current<FARegister.refNbr>>>>.SelectSingleBound(graph, new object[]{register});
				if (newreg.DocDesc != descr)
                {
                    newreg.DocDesc = string.Empty;
                    doccache.Update(newreg);
                }
                doccache.Current = newreg;
			}
			else
			{
                graph.Clear();

                register = new FARegister
                {
                    Hold = false,
                    BranchID = BranchID,
                    Origin = Origin,
                    FinPeriodID = PeriodID,
                    DocDate = DocDate,
                    DocDesc = descr
                };
                doccache.Insert(register);
			}
		}

		public override void Persist()
		{
			base.Persist();

            if(Document.Current != null)
            {
                FARegister existed = created.Find(Document.Current);
                if (existed == null)
                {
                    created.Add(Document.Current);
                }
                else
                {
                    Document.Cache.RestoreCopy(existed, Document.Current);
                }
            }
		}
		#endregion

		#region Funcs
		protected virtual void DefaultingAccSub(PXFieldDefaultingEventArgs e, Dictionary<String, Int32?> accs)
		{
			var trn = (FATran)(e.Row);
			if (trn == null || trn.AssetID == null || trn.TranType == null) return;
			try
			{
				e.NewValue = accs[trn.TranType];
				e.Cancel = true;
			}
			catch (KeyNotFoundException) { }
		}

		protected virtual void DefaultingAllAccounts(PXCache sender, FATran trn)
		{
			sender.SetDefaultExt<FATran.debitAccountID>(trn);
			sender.SetDefaultExt<FATran.debitSubID>(trn);
			sender.SetDefaultExt<FATran.creditAccountID>(trn);
			sender.SetDefaultExt<FATran.creditSubID>(trn);
		}

		protected virtual void SetCurrentAsset(PXCache sender, FATran trn)
		{
			if (Asset.Current == null || Asset.Current.AssetID != trn.AssetID)
			{
				Asset.Current = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.SelectSingleBound(this, new object[]{trn});
			}
		}

		protected virtual void ToggleAccounts(PXCache sender, FATran trn)
		{
			if (trn != null && trn.TranType != null)
			{
				bool enable;
				if (_debit_enable.TryGetValue(trn.TranType, out enable))
				{
					PXUIFieldAttribute.SetEnabled<FATran.creditAccountID>(sender, trn, !enable);
					PXUIFieldAttribute.SetEnabled<FATran.creditSubID>(sender, trn, !enable);
					PXUIFieldAttribute.SetEnabled<FATran.debitAccountID>(sender, trn, enable);
					PXUIFieldAttribute.SetEnabled<FATran.debitSubID>(sender, trn, enable);
				}
                if(trn.TranType == FATran.tranType.PurchasingPlus)
                {
                    PXUIFieldAttribute.SetEnabled<FATran.creditAccountID>(sender, trn, false);
                    PXUIFieldAttribute.SetEnabled<FATran.creditSubID>(sender, trn, false);
                    PXUIFieldAttribute.SetEnabled<FATran.debitAccountID>(sender, trn, false);
                    PXUIFieldAttribute.SetEnabled<FATran.debitSubID>(sender, trn, false);
                }
			}
		}
		#endregion

		#region Events
		protected virtual void FATran_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void FATran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
		    FATran tran = (FATran) e.Row;
            e.Cancel |= tran.TranAmt == 0m && (!string.IsNullOrEmpty(tran.MethodDesc) || tran.TranType != "C+" && tran.TranType != "P+" && tran.TranType != "S+" && tran.TranType != "S-" && tran.Origin != FARegister.origin.Adjustment);
		}

		protected virtual void FATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			FATran trn = (FATran)e.Row;
			if(trn == null) return;

            if (sender.AllowUpdate && trn.Origin != FARegister.origin.Adjustment)
            {
                PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
                PXUIFieldAttribute.SetEnabled<FATran.tranDesc>(sender, e.Row, true);
            }

            SetCurrentAsset(sender, trn);
			ToggleAccounts(sender, trn);
		}

		protected virtual void FATran_AssetID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			FATran trn = (FATran)e.Row;
			if (trn == null) return;

			SetCurrentAsset(sender, trn);
            sender.SetDefaultExt<FATran.branchID>(trn);
			DefaultingAllAccounts(sender, trn);
		}

		protected virtual void FATran_BookID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<FATran.finPeriodID>(e.Row);
		}

		protected virtual void FATran_TranType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			DefaultingAllAccounts(sender, (FATran)e.Row);

			object newValue = ((FATran)e.Row).FinPeriodID;
			try
			{
				sender.RaiseFieldVerifying<FATran.finPeriodID>(e.Row, ref newValue);
			}
			catch (PXSetPropertyException ex)
			{
				sender.SetValue<FATran.finPeriodID>(e.Row, null);
				sender.RaiseExceptionHandling<FATran.finPeriodID>(e.Row, newValue, ex);
			}
		}

		protected bool IsDefaulting = false;

		protected virtual void FATran_FinPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			IsDefaulting = true;
		}

		protected virtual void FATran_FinPeriodID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			try
			{
				if (e.Row != null && IsDefaulting)
				{
					object newValue = FinPeriodIDFormattingAttribute.FormatForStoring((string)e.NewValue);

					sender.RaiseFieldVerifying<FATran.finPeriodID>(e.Row, ref newValue);
				}
			}
			finally
			{
				IsDefaulting = false;
			}
		}

		protected virtual void FATran_FinPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			FATran tran = (FATran) e.Row;
			if(tran == null || tran.AssetID == null || tran.BookID == null) return;

			try
			{
				FABookPeriod p = PXSelect<FABookPeriod,
									Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>,
									And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>.Select(this, tran.BookID, (string)e.NewValue);
				if(p == null)
					throw new PXSetPropertyException(Messages.NoPeriodsDefined);

				FABookBalance bal = PXSelect<FABookBalance, 
					Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>, 
						And<FABookBalance.bookID, Equal<Required<FABookBalance.bookID>>>>>.Select(this, tran.AssetID, tran.BookID);


				if ((tran.TranType == FATran.tranType.DepreciationPlus || tran.TranType == FATran.tranType.DepreciationMinus) && tran.Origin == FARegister.origin.Adjustment)
				{
					if (!string.IsNullOrEmpty(bal.CurrDeprPeriod) && String.Compare((string)e.NewValue, bal.CurrDeprPeriod) >= 0)
					{
						throw new PXSetPropertyException(CS.Messages.Entry_LT, FinPeriodIDFormattingAttribute.FormatForError(bal.CurrDeprPeriod));
					}
					if (!string.IsNullOrEmpty(bal.LastDeprPeriod) && String.Compare((string)e.NewValue, bal.LastDeprPeriod) > 0)
					{
						throw new PXSetPropertyException(CS.Messages.Entry_LE, FinPeriodIDFormattingAttribute.FormatForError(bal.LastDeprPeriod));
					}
				}
			}
			catch (PXSetPropertyException)
			{
				e.NewValue = FinPeriodSelectorAttribute.FormatForDisplay((string)e.NewValue);
				throw;
			}
		}

		protected virtual void FATran_DebitAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Asset.Current == null) return;
			var accs = new Dictionary<String, Int32?>
			           	{
			           		{FATran.tranType.PurchasingPlus, Asset.Current.FAAccountID},
			           		{FATran.tranType.PurchasingMinus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.DepreciationPlus, Asset.Current.DepreciatedExpenseAccountID},
			           		{FATran.tranType.DepreciationMinus, Asset.Current.AccumulatedDepreciationAccountID},
			           		{FATran.tranType.CalculatedPlus, Asset.Current.DepreciatedExpenseAccountID},
			           		{FATran.tranType.CalculatedMinus, Asset.Current.AccumulatedDepreciationAccountID},
			           		{FATran.tranType.SalePlus, Asset.Current.DisposalAccountID},
			           		{FATran.tranType.SaleMinus, Asset.Current.FAAccountID},
			           		{FATran.tranType.ReconcilliationPlus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.ReconcilliationMinus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.PurchasingDisposal, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.PurchasingReversal, Asset.Current.FAAccountID},
			           	};

			DefaultingAccSub(e, accs);
		}

		protected virtual void FATran_DebitSubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Asset.Current == null) return;
			var accs = new Dictionary<String, Int32?>
			           	{
			           		{FATran.tranType.PurchasingPlus, Asset.Current.FASubID},
			           		{FATran.tranType.PurchasingMinus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.DepreciationPlus, Asset.Current.DepreciatedExpenseSubID},
			           		{FATran.tranType.DepreciationMinus, Asset.Current.AccumulatedDepreciationSubID},
			           		{FATran.tranType.CalculatedPlus, Asset.Current.DepreciatedExpenseSubID},
			           		{FATran.tranType.CalculatedMinus, Asset.Current.AccumulatedDepreciationSubID},
			           		{FATran.tranType.SalePlus, Asset.Current.DisposalSubID},
			           		{FATran.tranType.SaleMinus, Asset.Current.FASubID},
			           		{FATran.tranType.ReconcilliationPlus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.ReconcilliationMinus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.PurchasingDisposal, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.PurchasingReversal, Asset.Current.FASubID},
			           	};

			DefaultingAccSub(e, accs);
		}

		protected virtual void FATran_CreditAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Asset.Current == null) return;
			var accs = new Dictionary<String, Int32?>
			           	{
			           		{FATran.tranType.PurchasingPlus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.PurchasingMinus, Asset.Current.FAAccountID},
			           		{FATran.tranType.DepreciationPlus, Asset.Current.AccumulatedDepreciationAccountID},
			           		{FATran.tranType.DepreciationMinus, Asset.Current.DepreciatedExpenseAccountID},
			           		{FATran.tranType.CalculatedPlus, Asset.Current.AccumulatedDepreciationAccountID},
			           		{FATran.tranType.CalculatedMinus, Asset.Current.DepreciatedExpenseAccountID},
			           		{FATran.tranType.SalePlus, Asset.Current.FAAccountID},
			           		{FATran.tranType.SaleMinus, Asset.Current.DisposalAccountID},
			           		{FATran.tranType.ReconcilliationPlus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.ReconcilliationMinus, Asset.Current.FAAccrualAcctID},
			           		{FATran.tranType.PurchasingDisposal, Asset.Current.FAAccountID},
			           		{FATran.tranType.PurchasingReversal, Asset.Current.FAAccrualAcctID},
			           	};

			DefaultingAccSub(e, accs);
		}

		protected virtual void FATran_CreditSubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			if (Asset.Current == null) return;
			var accs = new Dictionary<String, Int32?>
			           	{
			           		{FATran.tranType.PurchasingPlus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.PurchasingMinus, Asset.Current.FASubID},
			           		{FATran.tranType.DepreciationPlus, Asset.Current.AccumulatedDepreciationSubID},
			           		{FATran.tranType.DepreciationMinus, Asset.Current.DepreciatedExpenseSubID},
			           		{FATran.tranType.CalculatedPlus, Asset.Current.AccumulatedDepreciationSubID},
			           		{FATran.tranType.CalculatedMinus, Asset.Current.DepreciatedExpenseSubID},
			           		{FATran.tranType.SalePlus, Asset.Current.FASubID},
			           		{FATran.tranType.SaleMinus, Asset.Current.DisposalSubID},
			           		{FATran.tranType.ReconcilliationPlus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.ReconcilliationMinus, Asset.Current.FAAccrualSubID},
			           		{FATran.tranType.PurchasingDisposal, Asset.Current.FASubID},
			           		{FATran.tranType.PurchasingReversal, Asset.Current.FAAccrualSubID},
			           	};

			DefaultingAccSub(e, accs);
		}

		protected virtual void FARegister_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			var reg = (FARegister)(e.Row);
			if (reg == null) return;

			if (reg.Released == true)
			{
				PXUIFieldAttribute.SetEnabled(cache, reg, false);
				cache.AllowDelete = false;
				cache.AllowUpdate = false;
				Trans.Cache.AllowDelete = false;
				Trans.Cache.AllowUpdate = false;
				Trans.Cache.AllowInsert = false;
			}
			else
			{
				PXUIFieldAttribute.SetEnabled(cache, reg, true);
				PXUIFieldAttribute.SetEnabled<FARegister.status>(cache, reg, false);
				cache.AllowDelete = true;
				cache.AllowUpdate = true;
				Trans.Cache.AllowDelete = true;
				Trans.Cache.AllowUpdate = true;
				Trans.Cache.AllowInsert = true;
			}

			if (reg.Origin == FARegister.origin.Adjustment)
			{
				PXStringListAttribute.SetList<FATran.tranType>(Trans.Cache, null, new FATran.tranType.UserListAttribute().AllowedValues, new FATran.tranType.UserListAttribute().AllowedLabels);
			}
            else
            {
                PXStringListAttribute.SetList<FATran.tranType>(Trans.Cache, null, new FATran.tranType.ListAttribute().AllowedValues, new FATran.tranType.ListAttribute().AllowedLabels);
                Trans.Cache.AllowInsert = false;
            }

			PXUIFieldAttribute.SetEnabled<FARegister.origin>(cache, reg, false);
            PXUIFieldAttribute.SetVisible<FARegister.reason>(cache, reg, reg.Origin == FARegister.origin.Disposal || reg.Origin == FARegister.origin.Transfer);
		}

        protected virtual void FARegister_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
        {
			if (e.Row != null)
			{
				using (new PXConnectionScope())
				{
					PXFormulaAttribute.CalcAggregate<FATran.tranAmt>(Trans.Cache, e.Row, true);
				}
			}
        }
		#endregion

		#region Buttons
		public PXAction<FARegister> release;
		[PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton]
		public virtual IEnumerable Release(PXAdapter adapter)
		{
            PXCache cache = Document.Cache;
            List<FARegister> list = new List<FARegister>();
            foreach (FARegister fadoc in adapter.Get<FARegister>())
            {
                if (!(bool)fadoc.Hold && !(bool)fadoc.Released)
                {
                    cache.Update(fadoc);
                    list.Add(fadoc);
                }
            }
            if (list.Count == 0)
            {
                throw new PXException(Messages.Document_Status_Invalid);
            }
            Save.Press();
            PXLongOperation.StartOperation(this, delegate() { AssetTranRelease.ReleaseDoc(list, false); });
            return list;
        }

        public PXAction<FARegister> viewBatch;
        [PXUIField(DisplayName = Messages.ViewBatch, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewBatch(PXAdapter adapter)
        {
            if (Trans.Current != null)
            {
                JournalEntry graph = CreateInstance<JournalEntry>();
                graph.BatchModule.Current = graph.BatchModule.Search<Batch.batchNbr>(Trans.Current.BatchNbr, BatchModule.FA);
                if (graph.BatchModule.Current != null)
                {
                    throw new PXRedirectRequiredException(graph, true, "ViewBatch") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
                }
            }
            return adapter.Get();
        }

        public PXAction<FARegister> viewAsset;
        [PXUIField(DisplayName = Messages.ViewAsset, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewAsset(PXAdapter adapter)
        {
            if (Trans.Current != null)
            {
                AssetMaint graph = CreateInstance<AssetMaint>();
                graph.CurrentAsset.Current = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.Select(this);
                if (graph.CurrentAsset.Current != null)
                {
                    throw new PXRedirectRequiredException(graph, true, "ViewAsset") { Mode = PXBaseRedirectException.WindowMode.Same };
                }
            }
            return adapter.Get();
        }

        public PXAction<FARegister> viewBook;
        [PXUIField(DisplayName = Messages.ViewBook, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewBook(PXAdapter adapter)
        {
            if (Trans.Current != null)
            {
                BookMaint graph = CreateInstance<BookMaint>();
                graph.Book.Current = PXSelect<FABook, Where<FABook.bookID, Equal<Current<FATran.bookID>>>>.Select(this);
                if (graph.Book.Current != null)
                {
                    throw new PXRedirectRequiredException(graph, true, "ViewBook") { Mode = PXBaseRedirectException.WindowMode.Same };
                }
            }
            return adapter.Get();
        }
        #endregion
	}
}
