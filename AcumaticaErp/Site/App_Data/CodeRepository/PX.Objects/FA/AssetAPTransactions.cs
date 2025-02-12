using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL;

namespace PX.Objects.FA
{
    [Serializable]
	public class AssetGLTransactions : PXGraph<AssetGLTransactions>
	{

        private readonly Dictionary<int?, FixedAsset> _PersistedAssets = new Dictionary<int?, FixedAsset>();
        #region Public Selects
		public PXCancel<GLTranFilter> Cancel;
		public PXFilter<GLTranFilter> Filter;

		public PXSelect<BAccount> BAccount;
		public PXSelect<Vendor> Vendor;
		public PXSelect<EPEmployee> Employee;

		[PXFilterable] 
		public PXFilteredProcessing<FAAccrualTran, GLTranFilter> GLTransactions;

		public PXSelect<FixedAsset> Assets;
		public PXSelect<FALocationHistory> Locations;
		public PXSelect<FADetails> Details;
		public PXSelect<FABookBalance> Balances;
		public PXSelect<FARegister> Register;
		public PXSelect<FATran, Where<FATran.gLtranID, Equal<Optional<FAAccrualTran.tranID>>, And<FATran.Tstamp, IsNull>>> FATransactions;
        public PXSelect<FABookHist> bookhist;

		public PXSetup<Company> company;
		public PXSetup<FASetup> fasetup;
		public PXSetup<GLSetup> glsetup;

        public PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>> assetNumbering;
		#endregion

		#region Ctor

		public AssetGLTransactions()
		{
		    fasetup.Current = null;
			object record = fasetup.Current;
            record = glsetup.Current;

            if (fasetup.Current.UpdateGL != true)
            {
                throw new PXSetPropertyException(Messages.OperationNotWorkInInitMode);
            }

			PXUIFieldAttribute.SetEnabled<FAAccrualTran.classID>(GLTransactions.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<FAAccrualTran.branchID>(GLTransactions.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<FAAccrualTran.custodian>(GLTransactions.Cache, null, true);
			PXUIFieldAttribute.SetEnabled<FAAccrualTran.department>(GLTransactions.Cache, null, true);
            PXUIFieldAttribute.SetEnabled<FAAccrualTran.reconciled>(GLTransactions.Cache, null, true);
            PXUIFieldAttribute.SetEnabled<FATran.tranType>(FATransactions.Cache, null, false);
		}
		#endregion

		#region View Delegates
        public static IEnumerable additions(PXGraph graph, GLTranFilter filter, PXCache accrualCache)
        {
            PXSelectBase<FAAccrualTran> cmd;
            if (filter.AccountID != null)
            {
                cmd = new PXSelectJoin<FAAccrualTran, LeftJoin<Account, On<Account.accountID, Equal<FAAccrualTran.gLTranAccountID>>>, Where<FAAccrualTran.gLTranAccountID, Equal<Current<GLTranFilter.accountID>>>>(graph);
            }
            else
            {
                cmd = new PXSelectJoin<FAAccrualTran, LeftJoin<Account, On<Account.accountID, Equal<FAAccrualTran.gLTranAccountID>>>>(graph);
                cmd.WhereAnd<Where2<Match<Account, Current<AccessInfo.userName>>,
                                    And<Account.active, Equal<boolTrue>,
                                    And2<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull,
                                        Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>,
                                    And<Where<Account.curyID, IsNull,
                                        Or<Account.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>>>>>();
            }
            cmd.WhereAnd<Where<FAAccrualTran.closedAmt, Less<FAAccrualTran.gLTranAmt>, Or<FAAccrualTran.tranID, IsNull>>>();
            cmd.WhereAnd<Where<Current<GLTranFilter.showReconciled>, Equal<True>, Or<FAAccrualTran.reconciled, NotEqual<True>, Or<FAAccrualTran.reconciled, IsNull>>>>();
            if (filter.ReconType == GLTranFilter.reconType.Addition)
            {
                cmd.WhereAnd<Where<FAAccrualTran.gLTranDebitAmt, Greater<decimal0>>>();
            }
            else
            {
                cmd.WhereAnd<Where<FAAccrualTran.gLTranCreditAmt, Greater<decimal0>>>();
            }

            if (filter.SubID != null)
            {
                cmd.WhereAnd<Where<FAAccrualTran.gLTranSubID, Equal<Current<GLTranFilter.subID>>>>();
            }

            int startRow = PXView.StartRow;
            int totalRows = 0;

            List<FAAccrualTran> list = new List<FAAccrualTran>();
            foreach (PXResult<FAAccrualTran> res in cmd.View.Select(PXView.Currents, null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows))
            {
                FAAccrualTran ext = res;
                if (ext.GLTranAmt == null)
                {
                    ext.GLTranAmt = ext.GLTranDebitAmt + ext.GLTranCreditAmt;
                    ext.GLTranQty = ext.GLTranOrigQty;
                    ext.SelectedAmt = 0m;
                    ext.SelectedQty = 0m;
                    ext.OpenAmt = ext.GLTranAmt;
                    ext.OpenQty = ext.GLTranOrigQty;
                    ext.ClosedAmt = 0m;
                    ext.ClosedQty = 0m;
                    ext.UnitCost = ext.GLTranOrigQty > 0 ? ext.GLTranAmt / ext.GLTranOrigQty : ext.GLTranAmt;
                    ext.Reconciled = false;

                    accrualCache.SetStatus(ext, PXEntryStatus.Inserted);
                    accrualCache.RaiseRowInserting(ext);
                }
		        list.Add(ext);
		    }
            PXView.StartRow = 0;
            return list;
        }

		public virtual IEnumerable gltransactions()
		{
		    return additions(this, Filter.Current, GLTransactions.Cache);
		}
		#endregion

		#region Events

		protected void SetProcessDelegate()
		{
			if (!PXLongOperation.Exists(UID))
			{
				PXGraph new_graph;
				Unload();
				using (new PXPreserveScope())
				{
					new_graph = (PXGraph)PXGraph.CreateInstance(GetType());
				}

				GLTransactions.SetProcessDelegate(
					delegate(List<FAAccrualTran> list)
					{
						new_graph.Actions.PressSave();
					});
			}
		}

		#region Filter
		protected virtual void GLTranFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			SetProcessDelegate();
			GLTransactions.SetProcessAllVisible(false);
		}

		protected virtual void GLTranFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			if (!sender.ObjectsEqual<GLTranFilter.accountID, GLTranFilter.subID>(e.Row, e.OldRow))
				ClearInserted();
			if (!sender.ObjectsEqual<GLTranFilter.branchID, GLTranFilter.custodian, GLTranFilter.department>(e.Row, e.OldRow))
			{
				GLTranFilter filter = (GLTranFilter)e.Row;
				foreach (FAAccrualTran tran in GLTransactions.Cache.Cached)
				{
					FAAccrualTran copy = (FAAccrualTran)GLTransactions.Cache.CreateCopy(tran);
					copy.BranchID = filter.BranchID;
					copy.Custodian = filter.Custodian;
					copy.Department = filter.Department;
					GLTransactions.Update(copy);
				}
			}
		}

		protected virtual void GLTranFilter_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			ClearInserted();
		}

		protected virtual void GLTranFilter_Custodian_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<GLTranFilter.branchID>(e.Row);
			sender.SetDefaultExt<GLTranFilter.department>(e.Row);
		}

		#endregion

		#region FAAccrualTran
		protected virtual void FAAccrualTran_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
		{
			FAAccrualTran row = (FAAccrualTran)e.Row;
			if (row == null || Filter.Current == null) return;

			row.BranchID = Filter.Current.BranchID;
			row.Custodian = Filter.Current.Custodian;
			row.Department = Filter.Current.Department;
		}

        protected virtual void FAAccrualTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
        {
            FATransactions.Cache.AllowInsert = e.Row != null;
        }

        public static void SetCurrentRegister(PXSelect<FARegister> _Register, int BranchID)
		{
			FARegister curRegister = null;
			foreach (FARegister reg in _Register.Cache.Inserted)
			{
                if (reg.BranchID == BranchID)
                {
                    curRegister = reg;
                    break;
                }
			}
			if (curRegister != null)
			{
				_Register.Current = curRegister;
			}
			else
			{
                string tmpRefNbr = GetTempKey<FARegister.refNbr>(_Register.Cache);
				FARegister reg = _Register.Insert(new FARegister { BranchID = BranchID, Origin = FARegister.origin.Reconcilliation });
                reg.RefNbr = tmpRefNbr;
                _Register.Cache.Normalize();

			}
		}

		protected virtual void FAAccrualTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			FAAccrualTran tran = (FAAccrualTran)e.Row;
			if (tran == null) return;
			if (tran.Selected == true && tran.ClassID != null && tran.SelectedAmt != null && tran.SelectedAmt > tran.GLTranAmt)
			{
				sender.RaiseExceptionHandling<FAAccrualTran.selectedAmt>(tran, tran.SelectedAmt,
												new PXSetPropertyException(CS.Messages.Entry_LE, tran.GLTranAmt));
			}
			else
			{
				sender.RaiseExceptionHandling<FAAccrualTran.selectedAmt>(tran, tran.SelectedAmt, null);
			}

			if (sender.ObjectsEqual<FAAccrualTran.selected, 
									FAAccrualTran.classID, 
									FAAccrualTran.branchID, 
									FAAccrualTran.custodian, 
									FAAccrualTran.department>(e.Row, e.OldRow)) return;

			foreach (FATran fatran in PXSelect<FATran, Where<FATran.gLtranID, Equal<Current<FAAccrualTran.tranID>>>>.Select(this))
			{
				if (FATransactions.Cache.GetStatus(fatran) == PXEntryStatus.Inserted)
					FATransactions.Delete(fatran);
			}
			if (tran.Selected == true && tran.ClassID != null)
			{
				decimal? qty = tran.OpenQty > 0 ? tran.OpenQty : 1m;
				for (int i = 0; i < qty; i++)
					FATransactions.Insert(new FATran());
			}
		}

		protected virtual void FAAccrualTran_BranchID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null)
			{
				e.NewValue = ((FAAccrualTran)(e.Row)).BranchID;
			}
		}
		protected virtual void FAAccrualTran_Custodian_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null)
			{
				e.NewValue = ((FAAccrualTran)(e.Row)).Custodian;
			}
		}
		protected virtual void FAAccrualTran_Department_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null)
			{
				e.NewValue = ((FAAccrualTran)(e.Row)).Department;
			}
		}
		
		#endregion
		#region FADetails
        protected virtual void FixedAsset_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            FixedAsset asset = (FixedAsset) e.Row;
            if(asset == null) return;

            _PersistedAssets[asset.AssetID] = asset;
        }


        protected virtual void FADetails_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            FADetails det = (FADetails)e.Row;
            if (det != null && fasetup.Current.CopyTagFromAssetID == true && (e.Operation & PXDBOperation.Command) == PXDBOperation.Insert)
            {
                det.TagNbr = _PersistedAssets[det.AssetID].AssetCD;
            }
        }

        protected virtual void FASetup_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
        {
			if (e.Row != null)
			{
				FASetup setup = (FASetup)e.Row;
				if (setup.CopyTagFromAssetID == true)
				{
					setup.TagNumberingID = null;
				}
			}
        }

		#endregion

		#region FATran
		protected virtual void FATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			SetProcessDelegate();
	
			FATran tran = (FATran) e.Row;
			if(tran == null) return;

			PXUIFieldAttribute.SetEnabled<FATran.component>(sender, tran, tran.NewAsset ?? false);
			PXUIFieldAttribute.SetEnabled<FATran.newAsset>(sender, tran, !(tran.Component ?? false));
			PXUIFieldAttribute.SetEnabled<FATran.targetAssetID>(sender, tran, !(tran.NewAsset ?? false) || (tran.Component ?? false));
			PXUIFieldAttribute.SetEnabled<FATran.classID>(sender, tran, (tran.NewAsset ?? false) || (tran.Component ?? false));
			PXUIFieldAttribute.SetEnabled<FATran.branchID>(sender, tran, (tran.NewAsset ?? false) || (tran.Component ?? false));
			PXUIFieldAttribute.SetEnabled<FATran.custodian>(sender, tran, (tran.NewAsset ?? false) || (tran.Component ?? false));
			PXUIFieldAttribute.SetEnabled<FATran.department>(sender, tran, (tran.NewAsset ?? false) || (tran.Component ?? false));
            PXUIFieldAttribute.SetEnabled<FATran.receiptDate>(sender, tran, tran.NewAsset == true);
            PXUIFieldAttribute.SetEnabled<FATran.deprFromDate>(sender, tran, tran.NewAsset == true);
            PXUIFieldAttribute.SetEnabled<FATran.qty>(sender, tran, tran.NewAsset == true);
            Numbering nbr = assetNumbering.Select();
            PXUIFieldAttribute.SetEnabled<FATran.assetCD>(sender, tran, nbr == null || nbr.UserNumbering == true);

		}

		protected virtual void FATran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			FATran tran = (FATran)e.Row;
			if (tran == null) return;

			FixedAsset asset = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Select(this, tran.AssetID);
            GLTran gltran = PXSelect<GLTran, Where<GLTran.tranID, Equal<Required<FAAccrualTran.tranID>>>>.Select(this, tran.GLTranID);
			FAAccrualTran ext = PXSelect<FAAccrualTran, Where<FAAccrualTran.tranID, Equal<Required<FAAccrualTran.tranID>>>>.Select(this, tran.GLTranID);
            FixedAsset cls;
            if (tran.NewAsset == true)
            {
                cls = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Select(this, tran.ClassID);
            }
            else
            {
                FixedAsset target = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Select(this, tran.TargetAssetID);
                cls = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>.SelectSingleBound(this, new object[] { target });
            }

            if (asset == null || cls == null || gltran == null) return;

            if (tran.ClassID == null)
            {
                tran.ClassID = cls.AssetID;
            }

            if(!string.IsNullOrEmpty(tran.TranDesc))
            {
                gltran.TranDesc = tran.TranDesc;
            }
			
            bool isNew = Assets.Cache.GetStatus(asset) == PXEntryStatus.Inserted;
			if(isNew && tran.TranAmt != ((FATran)e.OldRow).TranAmt)
			{
				DeleteAsset(asset);
				asset = InsertAsset(this, tran.ClassID, null, tran.AssetCD, cls.AssetType,
                    tran.ReceiptDate ?? gltran.TranDate, tran.DeprFromDate ?? gltran.TranDate, tran.TranAmt, cls.UsefulLife, tran.Qty, gltran, tran);
			    tran.AssetID = asset.AssetID;
			}

			if(sender.ObjectsEqual<FATran.newAsset, 
									FATran.component, 
									FATran.classID, 
									FATran.targetAssetID, 
									FATran.branchID,
									FATran.custodian,
									FATran.department,
                                    FATran.qty>(e.Row, e.OldRow) &&
                sender.ObjectsEqual<FATran.receiptDate,
                                    FATran.deprFromDate,
                                    FATran.tranDesc>(e.Row, e.OldRow)) return;

		    gltran.TranDesc = tran.TranDesc;
			if ((tran.NewAsset ?? false) && !(tran.Component ?? false)) // new asset (default)
			{
				if (isNew)
				{
					DeleteAsset(asset);
                    asset = InsertAsset(this, tran.ClassID, null, tran.AssetCD, cls.AssetType,
                        tran.ReceiptDate ?? gltran.TranDate, tran.DeprFromDate ?? gltran.TranDate, tran.TranAmt, cls.UsefulLife, tran.Qty, gltran, tran);
				}
				else
				{
                    asset = InsertAsset(this, tran.ClassID, null, tran.AssetCD, cls.AssetType,
                         tran.ReceiptDate ?? gltran.TranDate, tran.DeprFromDate ?? gltran.TranDate, ext.UnitCost, cls.UsefulLife, tran.Qty, gltran, tran);
				}
				tran.AssetID = asset.AssetID;
				tran.TargetAssetID = null;
			}
			else if ((tran.NewAsset ?? false) && (tran.Component ?? false)) //new component
			{
    			if (isNew)
	    		{
					DeleteAsset(asset);
                    asset = InsertAsset(this, tran.ClassID, tran.TargetAssetID, tran.AssetCD, cls.AssetType,
                         tran.ReceiptDate ?? gltran.TranDate, tran.DeprFromDate ?? gltran.TranDate, tran.TranAmt, cls.UsefulLife, tran.Qty, gltran, tran);
				}
				else
				{
                    asset = InsertAsset(this, tran.ClassID, tran.TargetAssetID, tran.AssetCD, cls.AssetType,
                         tran.ReceiptDate ?? gltran.TranDate, tran.DeprFromDate ?? gltran.TranDate, ext.UnitCost, cls.UsefulLife, tran.Qty, gltran, tran);
				}
				tran.AssetID = asset.AssetID;
			}
			else // existing asset
			{
				if (tran.TargetAssetID != null)
				{
					if (isNew)
					{
						DeleteAsset(asset);
					}
					tran.AssetID = tran.TargetAssetID;

					FABookBalance bal = PXSelectJoin<FABookBalance, 
											LeftJoin<FABook, On<FABook.bookID, Equal<FABookBalance.bookID>>>,
											Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>,
												And<FABook.updateGL, Equal<True>,
												And<FABookBalance.depreciate, Equal<boolTrue>>>>>.SelectSingleBound(this, new[]{asset});
						
					FADetails det = PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Select(this, tran.AssetID);
                    FALocationHistory loc = PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.SelectSingleBound(this, new object[]{det});
                    tran.Custodian = loc.Custodian;
                    tran.Department = loc.Department;

					if (bal != null && string.IsNullOrEmpty(bal.InitPeriod))
					{
						tran.TranAmt = det.AcquisitionCost;
					}
				}
			}
            sender.SetDefaultExt<FATran.bookID>(tran);
            sender.SetDefaultExt<FATran.finPeriodID>(tran);
            sender.SetDefaultExt<FATran.debitAccountID>(tran);
            sender.SetDefaultExt<FATran.debitSubID>(tran);

            SetProcessDelegate();
		}

        protected virtual void FATran_AssetCD_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
   		{
   		    FATran tran = (FATran) e.Row;
            if(tran == null) return;

            Numbering nbr = PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>>.Select(this);
            if ((nbr == null || nbr.UserNumbering == true) && string.IsNullOrEmpty(e.NewValue as string))
                throw new PXSetPropertyException(ErrorMessages.FieldIsEmpty, typeof(FATran.assetCD).Name);
   		}

        protected virtual void FATran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			FATran tran = (FATran)e.Row;
			if (tran == null) return;

            FAAccrualTran ext = GLTransactions.Current;
            if (ext == null)
			{
				throw new PXException(Messages.GLTranNotSelected);
			}

            if (Register.Current == null)
			{
				SetCurrentRegister(Register, (int)Filter.Current.BranchID);
			}

            ext.Selected = true;

			GLTran gltran = PXSelect<GLTran, Where<GLTran.tranID, Equal<Current<FAAccrualTran.tranID>>>>.SelectSingleBound(this, new[] { ext });
			FixedAsset cls = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FAAccrualTran.classID>>>>.Select(this, tran.ClassID ?? ext.ClassID);

			decimal tranAmt = tran.TranAmt ?? GetGLRemainder(ext);

			FixedAsset asset = InsertAsset(this, 
											cls != null ? cls.AssetID : null, 
											null, 
                                            tran.AssetCD,
											cls != null ? cls.AssetType : null,
											gltran.TranDate,
                                            gltran.TranDate,
											tranAmt,
											cls != null ? cls.UsefulLife : null,
                                            tran.Qty,
											gltran,
                                            new FALocationHistory
                                            {
                                                BranchID = tran.BranchID ?? ext.BranchID,
                                                Custodian = tran.Custodian ?? ext.Custodian,
                                                Department = tran.Department ?? ext.Department
                                            });

			tran.AssetID = asset.AssetID;
			sender.SetDefaultExt<FATran.bookID>(tran);
			tran.TranDate = gltran.TranDate;
            tran.ReceiptDate = gltran.TranDate;
            tran.DeprFromDate = tran.ReceiptDate;
            sender.SetDefaultExt<FATran.finPeriodID>(tran);
			tran.TranAmt = tranAmt;
			tran.GLTranID = gltran.TranID;
			tran.CreditAccountID = gltran.AccountID;
			tran.CreditSubID = gltran.SubID;
			tran.TranDesc = gltran.TranDesc;
            tran.Origin = Register.Current.Origin;
			if (cls != null)
			{
				tran.ClassID = cls.AssetID;
				sender.SetDefaultExt<FATran.debitAccountID>(tran);
				sender.SetDefaultExt<FATran.debitSubID>(tran);
			}
		}

		protected virtual void FATran_BookID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		protected virtual void FATran_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

        protected virtual void FATran_TranDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            sender.SetDefaultExt<FATran.finPeriodID>(e.Row);
        }

        protected virtual void FATran_ReceiptDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            sender.SetDefaultExt<FATran.deprFromDate>(e.Row);
        }

        protected virtual void FATran_NewAsset_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			FATran tran = (FATran) e.Row;
			if(tran == null) return;

			object assetID = tran.TargetAssetID;
			try
			{
				sender.RaiseFieldVerifying<FATran.targetAssetID>(tran, ref assetID);
			}
			catch(PXSetPropertyException ex)
			{
				sender.RaiseExceptionHandling<FATran.targetAssetID>(tran, assetID, ex);
			}
		}

		protected virtual void FATran_TargetAssetID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			FATran tr = (FATran)e.Row;
			if (e.NewValue == null && tr.NewAsset != true)
				throw new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<FATran.targetAssetID>(sender));
		}

		protected virtual void FATran_ClassID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null)
				throw new PXSetPropertyException(ErrorMessages.FieldIsEmpty, PXUIFieldAttribute.GetDisplayName<FATran.classID>(sender));
		}

		protected virtual void FATran_FinPeriodID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			FATran tran = (FATran) e.Row;
			if (tran == null || tran.TranDate == null || tran.BookID == null) return;

			e.NewValue = FABookPeriodIDAttribute.FormatPeriod(FABookPeriodIDAttribute.PeriodFromDate(this, tran.TranDate, tran.BookID));

		}

		protected virtual void FATran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			FATran tran = (FATran)e.Row;
			if (tran == null) return;

			FixedAsset asset = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Select(this, tran.AssetID);
			DeleteAsset(asset);
		}
		#endregion

		#endregion

		#region Functions
        private const string keyPrefix = "*##@";

        public static string GetTempKey<Field>(PXCache cache) where Field : IBqlField
        {
            int key = 0;
            foreach (object inserted in cache.Inserted)
            {
                int insertedkey = Convert.ToInt32(((string)cache.GetValue<Field>(inserted)).Substring(4));
                if (insertedkey > key)
                    key = insertedkey;
            }
            return string.Format("{0}{1}", keyPrefix, Convert.ToString(++key));
        }

        public static bool IsTempKey(string key)
        {
            return key.StartsWith(keyPrefix);
        }

		public static FixedAsset InsertAsset(PXGraph graph, int? _classID, int? _parentID, string _assetCD, string _assetType,
            DateTime? _recDate, DateTime? _deprFromDate, decimal? _cost, decimal? _usefulLife, decimal? _qty, GLTran _gltran, IFALocation _loc)
		{
            if(_assetCD == null)
			    _assetCD = GetTempKey<FixedAsset.assetCD>(graph.Caches[typeof(FixedAsset)]);

			FixedAsset asset = new FixedAsset
			{
                BranchID = _loc.BranchID,
				ClassID = _classID,
				ParentAssetID = _parentID,
				RecordType = FARecordType.AssetType,
				AssetType = _assetType,
				UsefulLife = _usefulLife,
				Description = _gltran != null ? _gltran.TranDesc : string.Empty,
                Qty = _qty
			};
			asset = (FixedAsset)graph.Caches[typeof(FixedAsset)].Insert(asset);
			asset.AssetCD = _assetCD;
			graph.Caches[typeof(FixedAsset)].Normalize();

			FALocationHistory location = new FALocationHistory
			{
				AssetID = asset.AssetID,
				BranchID = _loc.BranchID,
				Custodian = _loc.Custodian,
				Department = _loc.Department,
                TransactionDate = _recDate
			};
			location = (FALocationHistory)graph.Caches[typeof(FALocationHistory)].Insert(location);
			int? revID = location.RevisionID;

			APTran aptran = PXSelect<APTran, Where<APTran.refNbr, Equal<Current<GLTran.refNbr>>, 
										And<APTran.lineNbr, Equal<Current<GLTran.tranLineNbr>>, And<APTran.tranType, Equal<Current<GLTran.tranType>>>>>>.SelectSingleBound(graph, new object[]{_gltran});

			FADetails det = new FADetails
			{
				AssetID = asset.AssetID,
				ReceiptDate = _recDate,
                DepreciateFromDate = _deprFromDate,
				AcquisitionCost = _cost,
				LocationRevID = revID,
                BillNumber = _gltran != null ? _gltran.RefNbr : null,
				PONumber = aptran != null ? aptran.PONbr:null,
				ReceiptNbr = aptran != null ? aptran.ReceiptNbr : null,
			};
			graph.Caches[typeof(FADetails)].Insert(det);

			foreach (FABookSettings sett in PXSelect<FABookSettings,
												Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>>, OrderBy<Desc<FABookSettings.updateGL>>>.Select(graph))
			{
				FABookBalance bal = new FABookBalance
				{
					AssetID = asset.AssetID,
					ClassID = _classID,
					BookID = sett.BookID,
                    UsefulLife = _usefulLife,
				};
				bal = (FABookBalance)graph.Caches[typeof(FABookBalance)].Insert(bal);
                if(string.IsNullOrEmpty(location.PeriodID))
                {
                    location.PeriodID = bal.DeprFromPeriod;
                }
			}

		    asset.FASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(graph.Caches[typeof (FixedAsset)], asset);
            asset.AccumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(graph.Caches[typeof(FixedAsset)], asset);
            asset.DepreciatedExpenseSubID = AssetMaint.MakeSubID<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>(graph.Caches[typeof(FixedAsset)], asset);
            asset.DisposalSubID = AssetMaint.MakeSubID<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>(graph.Caches[typeof(FixedAsset)], asset);
            asset.GainSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>(graph.Caches[typeof(FixedAsset)], asset);
            asset.LossSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>(graph.Caches[typeof(FixedAsset)], asset);

			location.FAAccountID = asset.FAAccountID;
			location.FASubID = asset.FASubID;
			location.AccumulatedDepreciationAccountID = asset.AccumulatedDepreciationAccountID;
			location.AccumulatedDepreciationSubID = asset.AccumulatedDepreciationSubID;
			location.DepreciatedExpenseAccountID = asset.DepreciatedExpenseAccountID;
			location.DepreciatedExpenseSubID = asset.DepreciatedExpenseSubID;
			location.LocationID = asset.BranchID;
			location.TransactionDate = det.ReceiptDate;
			location.PeriodID = FinPeriodIDAttribute.PeriodFromDate(det.ReceiptDate);

			return asset;
		}

		protected virtual void DeleteAsset(FixedAsset asset)
		{
			if(asset.AssetID < 0) Assets.Delete(asset);
		}


		protected virtual void ClearInserted()
		{
			FATransactions.Cache.Clear();
			Balances.Cache.Clear();
			Details.Cache.Clear();
			Locations.Cache.Clear();
			Assets.Cache.Clear();
		}

		protected virtual decimal GetGLRemainder(FAAccrualTran ex)
		{
			decimal remainder = ex.OpenAmt ?? 0m - ex.SelectedAmt ?? 0m;
			return remainder > 0m ? Math.Min(remainder, ex.UnitCost ?? 0m) : 0m;
		}
		#endregion

		#region Overrides


		public override void Persist()
		{
		    foreach (FATran tran in FATransactions.Cache.Inserted)
		    {
		        try
		        {
                    object val = tran.AssetCD;
                    FATransactions.Cache.RaiseFieldVerifying<FATran.assetCD>(tran, ref val);
                }
		        catch (PXSetPropertyException)
		        {
		            throw new PXException(Messages.CannotCreateAsset);
		        }
		    }

			foreach (FixedAsset asset in Caches[typeof(FixedAsset)].Cached)
			{
				FATran tran = PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>>>.SelectSingleBound(this, new object[]{ asset });

				if (tran == null)
				{
					DeleteAsset(asset);
				}
			}

            foreach (FixedAsset asset in Caches[typeof(FixedAsset)].Inserted)
            {
                FADetails assetdet = new FADetails { AssetID = asset.AssetID };
                assetdet = Details.Locate(assetdet);
                if (assetdet != null)
                {
                    FALocationHistory newhist = new FALocationHistory { AssetID = assetdet.AssetID, RevisionID = assetdet.LocationRevID };
                    newhist = Locations.Locate(newhist);
                    if (newhist != null)
                    {
                        newhist.FAAccountID = asset.FAAccountID;
                        newhist.FASubID = asset.FASubID;
                        newhist.AccumulatedDepreciationAccountID = asset.AccumulatedDepreciationAccountID;
                        newhist.AccumulatedDepreciationSubID = asset.AccumulatedDepreciationSubID;
                        newhist.DepreciatedExpenseAccountID = asset.DepreciatedExpenseAccountID;
                        newhist.DepreciatedExpenseSubID = asset.DepreciatedExpenseSubID;
                        newhist.LocationID = asset.BranchID;
                        newhist.TransactionDate = assetdet.ReceiptDate;
                        newhist.PeriodID = FinPeriodIDAttribute.PeriodFromDate(assetdet.ReceiptDate);
                    }
                }
            }

			foreach (FAAccrualTran ex in GLTransactions.Select())
			{
				PXProcessing<FAAccrualTran>.SetCurrentItem(ex);

                foreach (FATran tran in FATransactions.Select(ex.TranID))
				{
					object assetID = tran.TargetAssetID;
					FATransactions.Cache.RaiseFieldVerifying<FATran.targetAssetID>(tran, ref assetID);
					object classID = tran.ClassID;
					FATransactions.Cache.RaiseFieldVerifying<FATran.classID>(tran, ref classID);
				}

				if (ex.Selected == true && ex.SelectedAmt > ex.GLTranAmt)
				{
    				throw new PXSetPropertyException(CS.Messages.Entry_LE, ex.GLTranAmt);
				}

			}

			foreach (FAAccrualTran ext in GLTransactions.Select())
			{
				if (ext.Selected != true) continue;
				PXProcessing<FAAccrualTran>.SetCurrentItem(ext);

				foreach (FATran tran in FATransactions.Select(ext.TranID))
				{
                    if (tran.NewAsset == true)
                    {
                        FABookBalance glbal = PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>,
                                                                                And<FABookBalance.bookID, Equal<Current<FATran.bookID>>>>>.SelectMultiBound(this, new object[] { tran });
                        tran.TranPeriodID = glbal.DeprFromPeriod;
                    }
                    foreach (FABookBalance bal in PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>, 
                                                                            And<FABookBalance.bookID, NotEqual<Current<FATran.bookID>>>>>.SelectMultiBound(this, new object[]{tran}))
					{
                        FATran newtrn = (FATran)FATransactions.Cache.CreateCopy(tran);
                        newtrn.BookID = bal.BookID;
                        FATransactions.Cache.SetDefaultExt<FATran.lineNbr>(newtrn);
                        FATransactions.Cache.SetDefaultExt<FATran.finPeriodID>(newtrn);
                        if (tran.NewAsset == true)
                        {
                            newtrn.TranPeriodID = bal.DeprFromPeriod;
                        }
                        else
                        {
                            FATransactions.Cache.SetDefaultExt<FATran.tranPeriodID>(newtrn);
                        }
                        FATransactions.Cache.SetStatus(newtrn, PXEntryStatus.Inserted);
                    }
				}
				ext.ClosedAmt = ext.GLTranAmt - ext.OpenAmt;
				ext.ClosedQty = ext.GLTranQty - ext.OpenQty;
			    if(GLTransactions.Cache.GetStatus(ext) == PXEntryStatus.Notchanged)
			    {
			        GLTransactions.Cache.SetStatus(ext, PXEntryStatus.Updated);
			    }
			}

            List<FATran> ptrans = new List<FATran>((IEnumerable<FATran>)FATransactions.Cache.Inserted);
            foreach (FATran tran in ptrans)
            {
                FATran reconTran = (FATran)FATransactions.Cache.CreateCopy(tran);
                reconTran.TranType = FATran.tranType.ReconcilliationPlus;
                reconTran.DebitAccountID = fasetup.Current.FAAccrualAcctID;
                reconTran.DebitSubID = fasetup.Current.FAAccrualSubID;
                reconTran.LineNbr = (int?)PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(FATransactions.Cache, Register.Current);
                FATransactions.Cache.SetStatus(reconTran, PXEntryStatus.Inserted);

                tran.CreditAccountID = reconTran.DebitAccountID;
                tran.CreditSubID = reconTran.DebitSubID;
                tran.GLTranID = null;

                GLTran gltran = PXSelect<GLTran, Where<GLTran.tranID, Equal<Current<FATran.gLtranID>>>>.SelectSingleBound(this, new object[] { reconTran });
                FinPeriod glperiod = PXSelect<FinPeriod, Where<FinPeriod.fAClosed, NotEqual<True>,
                                                            And<FinPeriod.active, Equal<True>,
                                                            And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
                                                            And<FinPeriod.finPeriodID, GreaterEqual<Current<GLTran.finPeriodID>>>>>>,
                                                         OrderBy<Asc<FinPeriod.finPeriodID>>>.SelectSingleBound(this, new object[] { gltran });
                FABookBalance bal = PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>, And<FABookBalance.bookID, Equal<Current<FATran.bookID>>>>>.SelectSingleBound(this, new object[] { tran });
                FADepreciationMethod method = PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Select(this, bal.DepreciationMethodID);
                FixedAsset asset = PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.SelectSingleBound(this, new object[] { tran });
                if (bal.UpdateGL == true && glperiod == null)
                {
                    throw new PXException(GL.Messages.NoOpenPeriod);
                }

                if (bal.UpdateGL == true)
                {
                    reconTran.FinPeriodID = glperiod.FinPeriodID;
                }
                else
                {
                    FATransactions.Cache.SetDefaultExt<FATran.finPeriodID>(reconTran);
                }
                reconTran.TranPeriodID = bal.DeprFromPeriod;


                if (bal.UpdateGL != true)
                    reconTran.GLTranID = null;

                if (bal.Status == FixedAssetStatus.FullyDepreciated && method.IsPureStraightLine)
                {
                    FATran deprtran = (FATran)FATransactions.Cache.CreateCopy(tran);
                    deprtran.TranType = FATran.tranType.CalculatedPlus;
                    deprtran.CreditAccountID = asset.AccumulatedDepreciationAccountID;
                    deprtran.CreditSubID = asset.AccumulatedDepreciationSubID;
                    deprtran.DebitAccountID = asset.DepreciatedExpenseAccountID;
                    deprtran.DebitSubID = asset.DepreciatedExpenseSubID;
                    deprtran.LineNbr = (int?)PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(FATransactions.Cache, Register.Current);
                    deprtran.GLTranID = null;
                    FATransactions.Cache.SetStatus(deprtran, PXEntryStatus.Inserted);
                }
            }

            // segregate transactions by BranchID
            foreach (FATran tran in FATransactions.Cache.Inserted)
            {
                SetCurrentRegister(Register, (int)tran.BranchID);
                if (tran.NewAsset == true)
                {
                    Register.Current.Origin = FARegister.origin.Purchasing;
                }
                if (tran.RefNbr != Register.Current.RefNbr)
                {
                    tran.RefNbr = Register.Current.RefNbr;
                    tran.LineNbr = (int?)PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(FATransactions.Cache, Register.Current);
                    FATransactions.Cache.Normalize();
                }
            }

            //Delete empty inserted documents
            List<FARegister> docs = new List<FARegister>((IEnumerable<FARegister>)Register.Cache.Inserted);
            for(int i = docs.Count - 1; i >= 0; --i)
            {
                FARegister doc = docs[i];
                FATran t = PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>>.SelectSingleBound(this, new object[]{doc});
                if (t == null)
                {
                    Register.Delete(doc);
                    docs.RemoveAt(i);
                }
            }

		    DocumentList<Batch> batchlist = new DocumentList<Batch>(this);
            using (PXTransactionScope ts = new PXTransactionScope())
            {
                base.Persist();
                foreach (FATran tran in ptrans)
                {
                    FABookBalance bookbal = new FABookBalance { AssetID = tran.AssetID, BookID = tran.BookID };
                    if ((bookbal = Balances.Locate(bookbal)) != null)
                    {
                        FABookHist hist = new FABookHist
                        {
                            AssetID = bookbal.AssetID,
                            BookID = bookbal.BookID,
                            FinPeriodID = bookbal.DeprFromPeriod
                        };

                        hist = bookhist.Insert(hist);

                        if (string.IsNullOrEmpty(bookbal.CurrDeprPeriod) && string.IsNullOrEmpty(bookbal.LastDeprPeriod))
                        {
                            bookbal.CurrDeprPeriod = bookbal.DeprFromPeriod;
                            Balances.Update(bookbal);
                        }
                    }
                }
                base.Persist();

                if (fasetup.Current.AutoReleaseAsset == true)
                {
                    SelectTimeStamp();
                    batchlist = AssetTranRelease.ReleaseDoc(docs, false, false);
                }
                ts.Complete(this);
            }

            PostGraph pg = PXGraph.CreateInstance<PostGraph>();
            foreach (Batch batch in batchlist)
            {
                pg.Clear();
                pg.PostBatchProc(batch);
            }

		}
		#endregion


        #region DAC Overrides
        [PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXDBLiteDefault(typeof(FARegister.refNbr), DefaultForUpdate = false)]
        [PXParent(typeof(Select<FARegister, Where<FARegister.refNbr, Equal<Current<FATran.refNbr>>>>))]
        [PXUIField(DisplayName = "Reference Number", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [PXSelector(typeof(FARegister.refNbr))]
        protected virtual void FATran_RefNbr_CacheAttached(PXCache sender)
        {
        }
        
        [PXDBInt]
        [PXDBLiteDefault(typeof(FixedAsset.assetID))]
        [PXSelector(typeof(Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>),
            SubstituteKey = typeof(FixedAsset.assetCD), DescriptionField = typeof(FixedAsset.description), DirtyRead = true)]
        [PXUIField(DisplayName = "Asset", Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void FATran_AssetID_CacheAttached(PXCache sender)
        {
        }

        [PXDBBaseCury]
        [PXUIField(DisplayName = "Transaction Amount")]
        [PXFormula(null, typeof(AddCalc<FAAccrualTran.selectedAmt>))]
        [PXFormula(null, typeof(CountCalc<FAAccrualTran.selectedQty>))]
        protected virtual void FATran_TranAmt_CacheAttached(PXCache sender)
        {
        }

        [PeriodID]
        [PXUIField(DisplayName = "Tran. Period")]
        [PXDefault]
        protected virtual void FATran_FinPeriodID_CacheAttached(PXCache sender)
        {
        }

        [PeriodID]
        [PXFormula(typeof(RowExt<FATran.finPeriodID>))]
        protected virtual void FATran_TranPeriodID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
        [PXDefault(typeof(Search<FABookBalance.bookID, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>,
            And<FABookBalance.updateGL, Equal<boolTrue>>>>))]
        [PXUIField(DisplayName = "Book", Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void FATran_BookID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
        [PXDefault(typeof(Search<FixedAsset.fAAccountID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>))]
        protected virtual void FATran_DebitAccountID_CacheAttached(PXCache sender)
        {
        }

        [PXDBInt]
        [PXDefault(typeof(Search<FixedAsset.fASubID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>))]
        protected virtual void FATran_DebitSubID_CacheAttached(PXCache sender)
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Transaction Type", Visibility = PXUIVisibility.Visible)]
        [PXDefault(FATran.tranType.PurchasingPlus)]
        [FATran.tranType.List]
        protected virtual void FATran_TranType_CacheAttached(PXCache sender)
        {
        }

        [PXInt]
		[PXSelector(typeof(Search2<FixedAsset.assetID,
			LeftJoin<FABookSettings, On<FixedAsset.assetID, Equal<FABookSettings.assetID>>,
			LeftJoin<FABook, On<FABookSettings.bookID, Equal<FABook.bookID>>>>,
			Where<FixedAsset.recordType, Equal<FARecordType.classType>,
			And<FABook.updateGL, Equal<True>,
			And<FABookSettings.depreciate, Equal<True>>>>>),
                    SubstituteKey = typeof(FixedAsset.assetCD),
                    DescriptionField = typeof(FixedAsset.description))]
        [PXDefault]
        [PXUIField(DisplayName = "Asset Class", Required = true)]
        protected virtual void FATran_ClassID_CacheAttached(PXCache sender)
        {
        }

        [PXInt]
		[PXSelector(typeof(Search2<FixedAsset.assetID,
            LeftJoin<FABookBalance, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>,
            LeftJoin<FABook, On<FABookBalance.bookID, Equal<FABook.bookID>>>>,
			Where<FixedAsset.recordType, Equal<FARecordType.assetType>,
			And<FABook.updateGL, Equal<True>,
            And<FABookBalance.depreciate, Equal<True>>>>>),
                    SubstituteKey = typeof(FixedAsset.assetCD),
                    DescriptionField = typeof(FixedAsset.description))]
        [PXUIField(DisplayName = "Asset")]
        protected virtual void FATran_TargetAssetID_CacheAttached(PXCache sender)
        {
        }

		[Branch(typeof(FAAccrualTran.branchID), Required = true)]
        protected virtual void FATran_BranchID_CacheAttached(PXCache sender)
        {
        }

        [PXGuid]
        [PXSelector(typeof(EPEmployee.userID), SubstituteKey = typeof(EPEmployee.acctCD), DescriptionField = typeof(EPEmployee.acctName))]
        [PXDefault(typeof(FAAccrualTran.custodian), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Custodian")]
        protected virtual void FATran_Custodian_CacheAttached(PXCache sender)
        {
        }

        [PXString(10, IsUnicode = true)]
        [PXDefault(typeof(FAAccrualTran.department))]
        [PXSelector(typeof(EPDepartment.departmentID), DescriptionField = typeof(EPDepartment.description))]
        [PXUIField(DisplayName = "Department", Required = true)]
        protected virtual void FATran_Department_CacheAttached(PXCache sender)
        {
        }


        [Serializable]
        public partial class GLTran : GL.GLTran
        {
            [Branch(IsDetail = false, DisplayName = "Transaction Branch", Enabled = false)]
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
        }

        #endregion

	}

	public class AdditionsFATran : PXGraph<AdditionsFATran>
	{
		#region Selects
		public PXSelect<FixedAsset> Assets;
		public PXSelect<FALocationHistory> Locations;
		public PXSelect<FADetails> Details;
		public PXSelect<FABookBalance> Balances;
		public PXSelect<FARegister> Register;
		public PXSelect<FAAccrualTran> Additions;
		public PXSelect<FATran> FATransactions;

		public PXSetup<FASetup> fasetup;
		public PXSetup<GLSetup> glsetup;
		public PXSetup<Company> company;

		#endregion

		#region Ctor
		public AdditionsFATran()
		{
			object setup = fasetup.Current;
			setup = glsetup.Current;
		}
		#endregion

		#region Funcs

		protected virtual void InsertFATransactions(FixedAsset asset, DateTime? date, Decimal? amt, GLTran gltran)
		{
			foreach (FABookBalance bal in PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.SelectMultiBound(this, new object[] { asset }))
			{
				FATran tran = new FATran
              	{
              		AssetID = asset.AssetID,
              		BookID = bal.BookID,
              		TranAmt = amt,
              		TranDate = date,
              		TranType = FATran.tranType.PurchasingPlus,
              		CreditAccountID = asset.FAAccrualAcctID,
              		CreditSubID = asset.FAAccrualSubID,
              		DebitAccountID = asset.FAAccountID,
              		DebitSubID = asset.FASubID,
              		TranDesc = gltran.TranDesc
				};
				FATransactions.Insert(tran);
                
                tran = new FATran
                {
                    AssetID = asset.AssetID,
                    BookID = bal.BookID,
                    TranAmt = amt,
                    TranDate = date,
                    GLTranID = bal.UpdateGL == true ? gltran.TranID : null,
                    TranType = FATran.tranType.ReconcilliationPlus,
                    CreditAccountID = gltran.AccountID,
                    CreditSubID = gltran.SubID,
                    DebitAccountID = asset.FAAccrualAcctID,
                    DebitSubID = asset.FAAccrualSubID,
                    TranDesc = gltran.TranDesc
                };
                FATransactions.Insert(tran);
            }

		}

		public virtual void InsertNewComponent(FixedAsset parentAsset, FixedAsset cls, DateTime? date, Decimal? amt, decimal? qty, IFALocation loc, AssetGLTransactions.GLTran gltran)
		{
		    date = date ?? gltran.TranDate;
			FixedAsset comp = AssetGLTransactions.InsertAsset(this, cls.AssetID, parentAsset.AssetID, null, cls.AssetType, date, date,
										 amt, cls.UsefulLife, qty, gltran, loc);
            if (Register.Current == null)
            {
    			AssetGLTransactions.SetCurrentRegister(Register, (int)comp.BranchID);
                Register.Current.Origin = FARegister.origin.Purchasing;
            }
			InsertFATransactions(comp, date, amt, gltran);
		}
		#endregion

		#region Overrides
		public override void Persist()
		{
			base.Persist();
			if (fasetup.Current.AutoReleaseAsset == true && Register.Current != null)
			{
				SelectTimeStamp();
				AssetTranRelease.ReleaseDoc(new List<FARegister> { Register.Current }, false);
			}

		}
		#endregion

		#region Events
		protected virtual void FATran_AssetID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}
		protected virtual void FATran_BookID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}
        #endregion
	}


	public interface IFALocation
	{
		int? BranchID { get; set; }
		Guid? Custodian { get; set; }
		string Department { get; set; }
	}

	[Serializable]
	public partial class GLTranFilter : IBqlTable
	{
		#region AccountID
		public abstract class accountID : IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, typeof(Search<Account.accountID,
				Where2<Match<Current<AccessInfo.userName>>,
				And<Account.active, Equal<boolTrue>,
				And2<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull,
				Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>,
				And<Where<Account.curyID, IsNull, Or<Account.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>>>>>),
				DisplayName = "Account", Visibility = PXUIVisibility.Visible, Filterable = false, DescriptionField = typeof(Account.description))]
		[PXDefault(typeof(FASetup.fAAccrualAcctID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? AccountID
		{
			get
			{
				return _AccountID;
			}
			set
			{
				_AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, Filterable = true)]
		[PXDefault(typeof(FASetup.fAAccrualSubID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? SubID
		{
			get
			{
				return _SubID;
			}
			set
			{
				_SubID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(null, IsDetail = false)]
		[PXDefault(typeof(Coalesce<
					Search2<Location.vBranchID, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<Location.bAccountID>, And<EPEmployee.defLocationID, Equal<Location.locationID>>>>, Where<EPEmployee.userID, Equal<Current<custodian>>>>,
					Search<Branch.branchID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
		public virtual Int32? BranchID
		{
			get
			{
				return _BranchID;
			}
			set
			{
				_BranchID = value;
			}
		}
		#endregion
		#region Custodian
		public abstract class custodian : IBqlField
		{
		}
		protected Guid? _Custodian;
		[PXDBGuid]
		[PXSelector(typeof(EPEmployee.userID), SubstituteKey = typeof(EPEmployee.acctCD), DescriptionField = typeof(EPEmployee.acctName))]
		[PXUIField(DisplayName = "Custodian")]
		public virtual Guid? Custodian
		{
			get
			{
				return _Custodian;
			}
			set
			{
				_Custodian = value;
			}
		}
		#endregion
		#region Department
		public abstract class department : IBqlField
		{
		}
		protected String _Department;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault(typeof(Search<EPEmployee.departmentID, Where<EPEmployee.userID, Equal<Current<custodian>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXSelector(typeof(EPDepartment.departmentID), DescriptionField = typeof(EPDepartment.description))]
		[PXUIField(DisplayName = "Department")]
		public virtual String Department
		{
			get
			{
				return _Department;
			}
			set
			{
				_Department = value;
			}
		}
		#endregion

		#region AcquisitionCost
		public abstract class acquisitionCost : IBqlField
		{
		}
		protected Decimal? _AcquisitionCost;
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Acquisition Cost", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", typeof(Search<FADetails.acquisitionCost, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public Decimal? AcquisitionCost
		{
			get
			{
				return _AcquisitionCost;
			}
			set
			{
				_AcquisitionCost = value;
			}
		}
		#endregion
        #region CurrentCost
        public abstract class currentCost : IBqlField
        {
        }
        protected Decimal? _CurrentCost;
        [PXDBBaseCury]
        [PXUIField(DisplayName = "Current Cost", Enabled = false)]
        //[PXDefault(TypeCode.Decimal, "0.0", typeof(Search5<FABookHistory.ptdAcquired, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, Aggregate<Sum<FABookHistory.ptdAcquired>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDefault(TypeCode.Decimal, "0.0", typeof(Search<FABookHistory.ytdAcquired, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>))]
        public Decimal? CurrentCost
        {
            get
            {
                return _CurrentCost;
            }
            set
            {
                _CurrentCost = value;
            }
        }
        #endregion
        #region AccrualBalance
        public abstract class accrualBalance : IBqlField
        {
        }
        protected Decimal? _AccrualBalance;
        [PXDBBaseCury]
        [PXUIField(DisplayName = "Accrual Balance", Enabled = false)]
 //       [PXDefault(TypeCode.Decimal, "0.0", typeof(Search5<FABookHistory.ptdReconciled, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, Aggregate<Sum<FABookHistory.ptdReconciled>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDefault(TypeCode.Decimal, "0.0", typeof(Search<FABookHistory.ytdReconciled, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>))]
        public Decimal? AccrualBalance
        {
            get
            {
                return _AccrualBalance;
            }
            set
            {
                _AccrualBalance = value;
            }
        }
        #endregion
        #region UnreconciledAmt
        public abstract class unreconciledAmt : IBqlField
        {
        }
        protected Decimal? _UnreconciledAmt;
        [PXDBBaseCury]
        [PXUIField(DisplayName = "Unreconciled Amount", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public Decimal? UnreconciledAmt
        {
            get
            {
                return _UnreconciledAmt;
            }
            set
            {
                _UnreconciledAmt = value;
            }
        }
        #endregion
        #region SelectionAmt
		public abstract class selectionAmt : IBqlField
		{
		}
		protected Decimal? _SelectionAmt;
		[PXDBBaseCury]
		[PXUIField(DisplayName = "Selection Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public Decimal? SelectionAmt
		{
			get
			{
				return _SelectionAmt;
			}
			set
			{
				_SelectionAmt = value;
			}
		}
		#endregion		
        #region ExpectedCost
        public abstract class expectedCost : IBqlField
        {
        }
        protected Decimal? _ExpectedCost;
        [PXDBBaseCury]
        [PXUIField(DisplayName = "Expected Cost", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", typeof(Search2<FABookHistory.ytdAcquired, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public Decimal? ExpectedCost
        {
            get
            {
                return _ExpectedCost;
            }
            set
            {
                _ExpectedCost = value;
            }
        }
        #endregion		
        #region ExpectedAccrualBal
        public abstract class expectedAccrualBal : IBqlField
        {
        }
        protected Decimal? _ExpectedAccrualBal;
        [PXDBBaseCury]
        [PXUIField(DisplayName = "Expected Accrual Balance", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", typeof(Search2<FABookHistory.ytdReconciled, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, OrderBy<Asc<FABookHistory.finPeriodID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public Decimal? ExpectedAccrualBal
        {
            get
            {
                return _ExpectedAccrualBal;
            }
            set
            {
                _ExpectedAccrualBal = value;
            }
        }
        #endregion		
        #region ReconType
        public abstract class reconType : PX.Data.IBqlField
        {
            #region List
            public class ListAttribute : PXStringListAttribute
            {
                public ListAttribute()
                    : base(
                    new string[] { Addition, Deduction },
                    new string[] { Messages.Addition, Messages.Deduction }) { }
            }

            public const string Addition = "+";
            public const string Deduction = "-";

            public class addition : Constant<string>
            {
                public addition() : base(Addition) { ;}
            }
            public class deduction : Constant<string>
            {
                public deduction() : base(Deduction) { ;}
            }
            #endregion
        }
        protected String _ReconType;
        [PXDBString(1, IsFixed = true)]
        [PXDefault(reconType.Addition)]
        [PXUIField(DisplayName = "Reconciliation Type")]
        [reconType.List]
        public virtual String ReconType
        {
            get
            {
                return this._ReconType;
            }
            set
            {
                this._ReconType = value;
            }
        }
        #endregion

        #region TranDate
		public abstract class tranDate : IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate]
		[PXDefault(typeof(Search<FADetails.depreciateFromDate, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>))]
		[PXUIField(DisplayName = "Tran. Date")]
		public virtual DateTime? TranDate
		{
			get
			{
				return _TranDate;
			}
			set
			{
				_TranDate = value;
			}
		}
		#endregion
        #region PeriodID
        public abstract class periodID : IBqlField
        {
        }
        protected string _PeriodID;
        [PXUIField(DisplayName = "Addition Period")]
        [FAOpenPeriod]
        [PXDefault(typeof(Search2<FinPeriod.finPeriodID, InnerJoin<FABookBalance, On<FABookBalance.deprFromPeriod, LessEqual<FinPeriod.finPeriodID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FinPeriod.active, Equal<True>, And<FinPeriod.fAClosed, Equal<False>, And<FinPeriod.endDate, Greater<FinPeriod.startDate>>>>>, OrderBy<Asc<Switch<Case<Where<FABookBalance.updateGL, Equal<True>>, int1>, int0>, Desc<FinPeriod.finPeriodID>>>>))]
        public virtual string PeriodID
        {
            get
            {
                return _PeriodID;
            }
            set
            {
                _PeriodID = value;
            }
        }
        #endregion
        #region ShowReconciled
        public abstract class showReconciled : IBqlField { }
        [PXDBBool]
        [PXUIField(DisplayName = "Show transactions marked as reconciled")]
        [PXDefault(false)]
        public bool? ShowReconciled { get; set; }
        #endregion
    }


}
