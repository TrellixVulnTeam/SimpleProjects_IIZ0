using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.GL.Constants;

namespace PX.Objects.GL
{
	[PX.Objects.GL.TableAndChartDashboardType]
    [Serializable]
	public class AllocationProcess : PXGraph<AllocationProcess>
	{

		#region InternalTypes
        [System.SerializableAttribute()]
        public partial class AllocationFilter : IBqlTable
        {
            #region DateEntered
            public abstract class dateEntered : PX.Data.IBqlField
            {
            }
            protected DateTime? _DateEntered;
            [PXDBDate(MaxValue = "06/06/2079", MinValue = "01/01/1900")]
            [PXDefault(typeof(AccessInfo.businessDate))]
            [PXUIField(DisplayName = "Allocation Date", Visibility = PXUIVisibility.Visible)]
            public virtual DateTime? DateEntered
            {
                get
                {
                    return this._DateEntered;
                }
                set
                {
                    this._DateEntered = value;
                }
            }
            #endregion
            #region FinPeriod
            public abstract class finPeriodID : PX.Data.IBqlField
            {
            }
            protected String _FinPeriodID;
            [OpenPeriod(typeof(AllocationFilter.dateEntered))]
            [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible)]
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
        }

        [System.SerializableAttribute()]
        public partial class AllocationExt : GLAllocation
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
            #region AllocMethod
            [PXDBString(1, IsFixed = true)]
            [PXDefault(AllocationMethod.ByPercent)]
            [PXUIField(DisplayName = "Distribution Method")]
            [AllocationMethod.List()]
            public override String AllocMethod
            {
                get
                {
                    return this._AllocMethod;
                }
                set
                {
                    this._AllocMethod = value;
                }
            }
            #endregion
            #region Module
            public abstract class module : PX.Data.IBqlField
            {
            }
            protected String _Module;
            [PXString(2)]
            [PXDefault()]
            [PXUIField(DisplayName = "Module", Visibility = PXUIVisibility.SelectorVisible)]
            [BatchModule.List()]
            public virtual String Module
            {
                get
                {
                    return this._Module;
                }
                set
                {
                    this._Module = value;
                }
            }
            #endregion
            #region BatchNbr
            public abstract class batchNbr : PX.Data.IBqlField
            {
            }
            protected String _BatchNbr;
            [PXString(15, IsUnicode = true)]
            [PXUIField(DisplayName = "Last Batch", Visibility = PXUIVisibility.SelectorVisible)]
            public virtual String BatchNbr
            {
                get
                {
                    return this._BatchNbr;
                }
                set
                {
                    this._BatchNbr = value;
                }
            }
            #endregion
            #region BatchPeriod
            public abstract class batchPeriod : PX.Data.IBqlField
            {
            }
            protected String _BatchPeriod;
            [FinPeriodID(IsDBField = false)]
            [PXUIField(DisplayName = "Batch Period")]
            public virtual String BatchPeriod
            {
                get
                {
                    return this._BatchPeriod;
                }
                set
                {
                    this._BatchPeriod = value;
                }
            }
            #endregion
            #region Status
            public abstract class status : PX.Data.IBqlField
            {
            }
            protected String _Status;
            [PXString(1, IsFixed = true)]
            [PXDefault()]
            [PXUIField(DisplayName = "Batch Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
            [BatchStatus.List()]
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
            #region ControlTotal
            public abstract class controlTotal : PX.Data.IBqlField
            {
            }
            protected Decimal? _ControlTotal;
            //[PXDecimal(4)]
            [PXBaseCury()]
            [PXUIField(DisplayName = "Batch amount")]
            public virtual Decimal? ControlTotal
            {
                get
                {
                    return this._ControlTotal;
                }
                set
                {
                    this._ControlTotal = value;
                }
            }
            #endregion
            public static void CopyFrom(AllocationExt aAi, Batch aBatch)
            {
                aAi.Module = aBatch.Module;
                aAi.BatchNbr = aBatch.BatchNbr;
                aAi.BatchPeriod = aBatch.FinPeriodID;
                aAi.Status = aBatch.Status;
                aAi.ControlTotal = aBatch.ControlTotal;
            }
        }

        [Serializable]
		public partial class ReversingBatch : Batch 
		{
			#region Module
			public new abstract class module : PX.Data.IBqlField
			{
			}
			#endregion
			#region BatchNbr
			public new abstract class batchNbr : PX.Data.IBqlField
			{
			}
			#endregion
			#region AutoReverseCopy
			public new abstract class autoReverseCopy : PX.Data.IBqlField
			{
			}
			#endregion
			#region OrigModule
			public new abstract class origModule : PX.Data.IBqlField
			{
			}
			#endregion
			#region OrigBatchNbr
			public new abstract class origBatchNbr : PX.Data.IBqlField
			{
			}
			#endregion
		}

		public class BranchAccountSubKey : AP.Triplet<int, int, int>
		{
			public BranchAccountSubKey(int aFirst, int aSecond, int aThird ) : base(aFirst, aSecond,aThird) { }

			public override int GetHashCode() 
			{
                return (this.first + 13 * (this.second + 67 * this.third));
			}
		}
		#endregion

		#region Ctor + Members
		public AllocationProcess()
		{
			GLSetup setup = GLSetup.Current;
			AllocationFilter filter = Filter.Current;
            Allocations.SetProcessDelegate<AllocationProcess>(
                delegate(AllocationProcess processor, AllocationExt allocation)
                {
                    processor.ProcessAllocation(allocation, filter.FinPeriodID,filter.DateEntered);
                }
            );
		}
		public PXFilter<AllocationFilter> Filter;
		public PXCancel<AllocationFilter> Cancel;
		[PXFilterable]
		public PXFilteredProcessing<AllocationExt, AllocationFilter> Allocations;
		#endregion

		public PXSetup<GLSetup> GLSetup;

		public PXAction<AllocationFilter> ViewAllocation;
		[PXUIField(DisplayName = "View Allocation", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton]
		public virtual IEnumerable viewAllocation(PXAdapter adapter)
		{
			if (Allocations.Current != null)
			{
				PXRedirectHelper.TryRedirect(Allocations.Cache, Allocations.Current, "Allocation", PXRedirectHelper.WindowMode.NewWindow);
			}
			return adapter.Get();
		}

		public PXAction<AllocationFilter> ViewBatch;
		[PXUIField(DisplayName = "View Batch", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton]
		public virtual IEnumerable viewBatch(PXAdapter adapter)
		{
			if (Allocations.Current != null)
			{
				Batch batch = PXSelect<Batch, Where<Batch.module, Equal<Current<AllocationExt.module>>, And<Batch.batchNbr, Equal<Current<AllocationExt.batchNbr>>>>>.SelectSingleBound(this, new object[] { Allocations.Current });
				if (batch == null) throw new PXException(Messages.LastBatchCanNotBeFound);
				JournalEntry graph = CreateInstance<JournalEntry>();
				graph.BatchModule.Current = batch;
				throw new PXRedirectRequiredException(graph, true, "Batch"){Mode = PXBaseRedirectException.WindowMode.NewWindow};
			}
			return adapter.Get();
		}


		#region Delegates
        protected virtual IEnumerable allocations()
		{
			AllocationFilter filter = Filter.Current;
			PXSelectBase<AllocationExt> cmd = new PXSelect<AllocationExt, Where<AllocationExt.active, Equal<BQLConstants.BitOn>>, OrderBy<Asc<AllocationExt.sortOrder>>>(this);
			PXSelectBase<Batch> selBatch = new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>,
																And<Batch.module, Equal<GLAllocationHistory.module>>>>, 
																Where<GLAllocationHistory.gLAllocationID,Equal<Required<GLAllocationHistory.gLAllocationID>>>,
																OrderBy<Desc<Batch.tranPeriodID, Desc<Batch.batchNbr>>>>(this);
			PXSelectBase<Batch> selBatchInPeriod = new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory, On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>,
																And<Batch.module, Equal<GLAllocationHistory.module>>>>,
																Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
																And<Batch.tranPeriodID, Equal<Required<Batch.tranPeriodID>>>>,
																OrderBy<Desc<Batch.tranPeriodID, Desc<Batch.batchNbr>>>>(this);
            foreach (AllocationExt it in cmd.Select())
            {
                if (!isApplicable(it, filter.FinPeriodID)) continue;
				Batch lastBatch =null;
				if (it.AllocCollectMethod == AllocationCollectMethod.AcctPTD)
				{
					lastBatch = selBatchInPeriod.SelectWindowed(0, 1, it.GLAllocationID, filter.FinPeriodID);
				}
				else
				{
					lastBatch = selBatch.SelectWindowed(0, 1, it.GLAllocationID);
				}
                if (lastBatch != null)
                {
                    if (ComparePeriods(lastBatch.TranPeriodID, Filter.Current.FinPeriodID, false) > 0) continue;
                    AllocationExt.CopyFrom(it, lastBatch);
                }
                yield return it;
            }
			this.Allocations.Cache.IsDirty = false;
        }
        #endregion

        #region Processing Implementation
		protected virtual void ProcessAllocation(GLAllocation allocation, string aFiscalPeriod, DateTime? aDate) 
		{
			if (string.IsNullOrEmpty(aFiscalPeriod) || aDate == null) 
			{
				throw new PXException(Messages.AllocationProcessingRequireFinPeriodIDAndDate);
			}

			ValidateFinPeriod(aFiscalPeriod);
			
			if (!isApplicable(allocation, aFiscalPeriod)) 
			{
				throw new PXException(Messages.AllocationIsNotApplicableForThePeriod);
			} 
			
			if (this.isOutOfQueue(allocation, aFiscalPeriod)) 
			{
				throw new PXException(Messages.AllocCantBeProcessed);
			}
			Company company = PXSelect<Company>.Select(this);
			Batch periodBatch = this.FindLastAllocationBatchAfter(allocation.GLAllocationID, aFiscalPeriod, aDate.Value);
			if (periodBatch!=null)
			{
				throw new PXException(Messages.AllocationBatchDateIsBeforeTheExistingBatchForPeriod,periodBatch.DateEntered);
			}
			
			string allocStartPeriod = aFiscalPeriod; //By default, only aFiscalPeriod will be used
			if(allocation.AllocCollectMethod == GL.Constants.AllocationCollectMethod.FromPrevAllocation)
			{
				if (this.FindFutureAllocationApplication(allocation.GLAllocationID, aFiscalPeriod) != null)
				{
					throw new PXException(Messages.FutureAllocationApplicationDetected,allocation.GLAllocationID);
				}

				allocStartPeriod = this.FindLastAllocationPeriod(allocation.GLAllocationID, aFiscalPeriod);

				string yearStartPeriod = FirstPeriodOfYear(aFiscalPeriod);
				if((bool)allocation.Recurring)
				{
					yearStartPeriod = ConvertToSameYear(allocation.StartFinPeriodID, aFiscalPeriod);
				}
				else
				{
					if((!string.IsNullOrEmpty(allocation.StartFinPeriodID) && (ComparePeriods(allocation.StartFinPeriodID, yearStartPeriod, false) >0)))
						yearStartPeriod  = allocation.StartFinPeriodID;
				}

				if (string.IsNullOrEmpty(allocStartPeriod))
				{
					allocStartPeriod = yearStartPeriod;
				}
				else
				{
					if (ComparePeriods(allocStartPeriod, yearStartPeriod, false) < 0)
					{
						allocStartPeriod = yearStartPeriod;
					}
				}
			}

			if (this.HasUnpostedBatch(allocation,allocStartPeriod, aFiscalPeriod))
			{
				throw new PXException(Messages.UnpostedBatches);
			} 

			PXSelectBase<GLAllocationSource> cmd = new PXSelect<GLAllocationSource, Where<GLAllocationSource.gLAllocationID, Equal<Required<GLAllocationSource.gLAllocationID>>>>(this);
			PXSelectBase<GLHistory> acctHistorySelect = new PXSelectJoin<GLHistory,
																							InnerJoin<Account,On<GLHistory.accountID,Equal<Account.accountID>,And<Account.active,Equal<True>>>,
																							InnerJoin<Sub,On<GLHistory.subID,Equal<Sub.subID>>>>,
																							Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>,
                                                                                            And<GLHistory.branchID,Equal<Required<GLHistory.branchID>>,
																							And<GLHistory.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>,
																							And<GLHistory.finPeriodID, LessEqual<Required<GLHistory.finPeriodID>>,
																							And<Account.accountCD, Like<Required<Account.accountCD>>,
																							And<Sub.subCD,Like<Required<Sub.subCD>>>>>>>>>(this);
			
			JournalEntry batchGraph = PXGraph.CreateInstance<JournalEntry>();
			Batch batch = new Batch();
			batch.FinPeriodID = aFiscalPeriod;
			batch.DateEntered = aDate;
            batch.BranchID = allocation.BranchID;
			batch.LedgerID = allocation.AllocLedgerID;
			batch.Module = BatchModule.GL; //Correct Later
			batch.NumberCode = "GLALC";
			batch.Description = string.IsNullOrEmpty(allocation.Descr) ? allocation.GLAllocationID : allocation.Descr;
			//Set Batch Currency
			Ledger allocLedger = this.FindLedger(allocation.AllocLedgerID);
			CM.CurrencyInfo info = new CurrencyInfo();
			info.BaseCuryID= allocLedger.BaseCuryID;
			info = batchGraph.currencyinfo.Insert(info);
			batch = batchGraph.BatchModule.Insert(batch);

			CurrencyInfo b_info = (CurrencyInfo)PXSelect<CM.CurrencyInfo, Where<CM.CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>>.Select(batchGraph, null);
			if (b_info != null)
			{
				b_info.BaseCuryID = allocLedger.BaseCuryID;
				batchGraph.currencyinfo.Update(b_info);
			}

			decimal totalCredit = Decimal.Zero;
			decimal totalDebit = Decimal.Zero;
			int? sourceLedgerID  = allocation.SourceLedgerID.HasValue?allocation.SourceLedgerID:allocation.AllocLedgerID;
			//Collect allocation amount
			//AccountWeightedList sourceAccounts = new AccountWeightedList();

			Dictionary<BranchAccountSubKey, GLAllocationAccountHistory> allocationTrans = new Dictionary<BranchAccountSubKey, GLAllocationAccountHistory>();
			Dictionary<BranchAccountSubKey, int> processed = new Dictionary<BranchAccountSubKey, int>(); //this list is used to prevent sharing of the source account between different allocation sources
			Dictionary<int, Account> acctDefs = new Dictionary<int, Account>();
			foreach (GLAllocationSource iSrc in cmd.Select(allocation.GLAllocationID)) 
			{
				string acctCDWildCard = SubCDUtils.CreateSubCDWildcard(iSrc.AccountCD, AccountAttribute.DimensionName); //Check if it works correctly
				string subCDWildCard = SubCDUtils.CreateSubCDWildcard(iSrc.SubCD, SubAccountAttribute.DimensionName);
				int? contrAcctId = iSrc.ContrAccountID; 
				int? contrSubId = iSrc.ContrSubID;
				if(contrAcctId.HasValue) 
				{
					if(!acctDefs.ContainsKey(contrAcctId.Value))
					{
                        Account contraAccount = (Account)PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Select(this, contrAcctId);
                        if (contraAccount != null && contraAccount.Active != true)
                        {
                            throw new PXException(Messages.AllocationSourceAccountsContainInactiveContraAccount);
                        }
                        acctDefs[contrAcctId.Value] = contraAccount;
					}				
				}
				foreach (PXResult<GLHistory, Account, Sub> iRes in acctHistorySelect.Select(sourceLedgerID, iSrc.BranchID, allocStartPeriod, aFiscalPeriod, acctCDWildCard, subCDWildCard)) 
				{
					GLHistory iAcctHst = (GLHistory)iRes;
					Account iAcctDef = (Account)iRes;
					decimal amountFromPriorPeriods =decimal.Zero;

					if (!IsDenominationMatch(iAcctDef,allocLedger)) 
					{
						continue; //Skip Accounts, which are denominated in currency other then currency of the allocation ledger						
					}
                                  

					decimal saldo = (iAcctHst.FinPtdDebit ?? 0.0m) - (iAcctHst.FinPtdCredit ?? 0.0m);
					decimal balance =  AccountRules.IsCreditBalance(iAcctDef.Type)? (-saldo) :(saldo);

					//Reconsider - probably we need to take into account updates made by other allocations!!
					decimal allocated;                //= (iAcct.AllocPtdBalance ?? 0.0m);
					decimal allocatedForPriorPeriods; // = iAcct.AllocBegBalance??Decimal.Zero;
					//FindAllocatedAmount(allocation, iAcctHst, out allocated, out allocatedForPriorPeriods);
					FindAllocatedAmountDetailed(allocation.GLAllocationID, iAcctHst, out allocated, out allocatedForPriorPeriods);
					decimal amountToAllocate = balance;
					bool allocatedInPlace = ((contrAcctId.HasValue == false || iAcctHst.AccountID == contrAcctId) 
												&& (contrSubId.HasValue == false || iAcctHst.SubID == contrSubId )
												&& (allocation.SourceLedgerID.HasValue == false || allocation.SourceLedgerID == allocation.AllocLedgerID));
					if (iSrc.LimitAmount.HasValue)
					{
						decimal absLimit = Math.Abs(iSrc.LimitAmount.Value);
						decimal signedLimit = AccountRules.IsCreditBalance(iAcctDef.Type) ? (-absLimit) : (absLimit);
						decimal allocatedInPeriod = (allocated - allocatedForPriorPeriods);
						decimal effectiveBalance = balance;
						if (allocatedInPlace)
						{
							effectiveBalance = balance + allocated;
						}
						decimal allocAmount = Decimal.Zero;
						if (signedLimit > 0)
						{
							allocAmount = (effectiveBalance) < signedLimit ? (effectiveBalance) : signedLimit;
						}
						else
						{
							allocAmount = (effectiveBalance) > signedLimit ? (effectiveBalance) : signedLimit;
						}
						amountToAllocate = allocAmount - allocatedInPeriod;
					}
					else
					{
						if (iSrc.LimitPercent.HasValue)
						{
							if (String.IsNullOrEmpty(iSrc.PercentLimitType) || iSrc.PercentLimitType == Constants.PercentLimitType.ByPeriod)
							{
								//Per Period
								decimal allocAmount = allocated;
								if (!allocatedInPlace)
								{
									//In this case alocated amount is not deducted from the src account 
									allocAmount = (iSrc.LimitPercent.Value * balance) / 100.00m;
								}
								else
								{
									//In this case allocated was substracted  from the history - we need to add it back
									allocAmount = (iSrc.LimitPercent.Value * (balance + allocated) / 100.00m);
								}
								amountToAllocate = (allocAmount - (allocated - allocatedForPriorPeriods)); //To include "negative" entries - othervise they will be ignored							
							}
							else
							{
								throw new PXException(Messages.UnknownAllocationPercentLimitType, iSrc.PercentLimitType);
							}
						}
					}
					amountToAllocate = PXCurrencyAttribute.Round<Batch.curyInfoID>(batchGraph.BatchModule.Cache, (object)batch, amountToAllocate, CMPrecision.TRANCURY);
					if (iAcctHst.FinPeriodID != aFiscalPeriod)
						amountFromPriorPeriods += amountToAllocate;
					if(Math.Abs(amountToAllocate) >= epsilon) 
					{
						BranchAccountSubKey acctKey = new BranchAccountSubKey(iAcctHst.BranchID.Value, iAcctHst.AccountID.Value, iAcctHst.SubID.Value);
						if (processed.ContainsKey(acctKey))
						{
							if (processed[acctKey] != iSrc.LineID.Value) continue; //Account Already Processed by other source - Skip - Or throw	Exception?
						}
						else 
						{
							processed[acctKey] = iSrc.LineID.Value; 
						}

						if (!acctDefs.ContainsKey(iAcctDef.AccountID.Value)) 
						{
							acctDefs[iAcctDef.AccountID.Value] = iAcctDef;
						}
						if(!allocationTrans.ContainsKey(acctKey))
						{
							GLAllocationAccountHistory aha = new GLAllocationAccountHistory();
                            aha.BranchID = iAcctHst.BranchID;
							aha.AccountID = iAcctHst.AccountID;
							aha.SubID = iAcctHst.SubID;
							aha.ContrAccontID = contrAcctId;
							aha.ContrSubID = contrSubId;
							aha.AllocatedAmount = amountToAllocate;
							aha.PriorPeriodsAllocAmount = amountFromPriorPeriods;
							aha.SourceLedgerID = (sourceLedgerID != allocation.AllocLedgerID) ? sourceLedgerID : null;
							allocationTrans.Add(acctKey, aha);
						}
						else
						{
							GLAllocationAccountHistory aha = allocationTrans[acctKey];
							aha.AllocatedAmount += amountToAllocate;
							aha.PriorPeriodsAllocAmount += amountFromPriorPeriods;
						}
					}
				}
			}
			
			AllocateAmount(batchGraph, allocation, allocationTrans.Values, acctDefs, allocLedger, ref totalDebit, ref totalCredit);

			if (Math.Abs(totalDebit-totalCredit) < MinAmountToDistribute) return; //Amount is too small to be destributed
			//Distribute allocation amount
			AccountWeightedList destAccounts; 
			switch (allocation.AllocMethod) 
			{
				case Constants.AllocationMethod.ByWeight:
				case Constants.AllocationMethod.ByPercent:
					this.RetrievePredefinedDestribution(out destAccounts, allocation);
					break;
				case Constants.AllocationMethod.ByAcctPTD:
				case Constants.AllocationMethod.ByAcctYTD:
					this.RetrieveDestributionByAcctState(out destAccounts, allocation, aFiscalPeriod);
					break;
				case Constants.AllocationMethod.ByExternalRule:
					this.RetrieveExternalDestribution(out destAccounts, allocation, aFiscalPeriod);
					break;
				default:
					destAccounts = null;	break;
			}

			if (Math.Abs(destAccounts.totalWeight) < epsilon)
			{
				throw new PXException(Messages.AmountCantBeDistributed);
			}
			decimal toDistribute = (totalCredit - totalDebit);
			DistributeAmount(batchGraph, allocation, toDistribute, destAccounts,allocLedger);
			PrepareBatch(batchGraph,allocation);
			GLAllocationHistory ah = new GLAllocationHistory();
			ah.GLAllocationID = allocation.GLAllocationID;
			ah = (GLAllocationHistory)batchGraph.AllocationHistory.Cache.Insert(ah);
				
			batchGraph.Save.Press();

            if (batchGraph.BatchModule.Current != null && allocation is AllocationExt) 
			{
				AllocationExt.CopyFrom((AllocationExt)allocation, batchGraph.BatchModule.Current);                
			}
		}

		protected virtual string FindLastAllocationPeriod(string aAllocationID, string aBeforePeriod) 
		{
			//Adjust after the reversing batch process will be implemented
			PXSelectBase<Batch> select = new PXSelectJoin<Batch,
													InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>,
														And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>,
													LeftJoin<ReversingBatch, On<ReversingBatch.origBatchNbr, Equal<Batch.batchNbr>,
														And<ReversingBatch.origModule, Equal<Batch.module>,
														And<ReversingBatch.autoReverseCopy, Equal<BQLConstants.BitOn>>>>>>,
													Where<Batch.autoReverseCopy, NotEqual<BQLConstants.BitOn>,
														And<Batch.origBatchNbr, IsNull,
														And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
														And<Batch.tranPeriodID, LessEqual<Required<Batch.tranPeriodID>>>>>>,
														OrderBy<Desc<Batch.tranPeriodID>>>(this);
			foreach (PXResult<Batch, GLAllocationHistory, ReversingBatch> iRes in select.Select(aAllocationID, aBeforePeriod))
			{
				Batch batch = (Batch)iRes;
				ReversingBatch revBatch = (ReversingBatch)iRes;
				if (!string.IsNullOrEmpty(revBatch.BatchNbr)) continue;
				return batch.TranPeriodID;
			}
			return null;
		}

		protected virtual Batch FindFutureAllocationApplication(string aAllocationID, string aRefPeriod) 
		{
			//Adjust after the reversing batch process will be implemented
			PXSelectBase<Batch> select = new PXSelectJoin<Batch,
													InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>,
														And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>,
													LeftJoin<ReversingBatch, On<ReversingBatch.origBatchNbr, Equal<Batch.batchNbr>,
														And<ReversingBatch.origModule, Equal<Batch.module>,
														And<ReversingBatch.autoReverseCopy, Equal<BQLConstants.BitOn>>>>>>,
													Where<Batch.autoReverseCopy, NotEqual<BQLConstants.BitOn>,
														And<Batch.origBatchNbr, IsNull,
														And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
														And<Batch.tranPeriodID, Greater<Required<Batch.tranPeriodID>>>>>>,
														OrderBy<Desc<Batch.tranPeriodID>>>(this);
			foreach (PXResult<Batch, GLAllocationHistory, ReversingBatch> iRes in select.Select(aAllocationID, aRefPeriod))
			{
				Batch batch = (Batch)iRes;
				ReversingBatch revBatch = (ReversingBatch)iRes;
				if (!string.IsNullOrEmpty(revBatch.BatchNbr)) continue; //Reversing Batch is found - skip
				return batch;
			}
			return null;
		}

		protected virtual Batch FindLastAllocationBatchAfter(string aAllocID, string aFinPeriodID, DateTime aAfter)
		{
			PXSelectBase<Batch> select = new PXSelectJoin<Batch,
								InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>,
								And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>>,
								Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
								And<Batch.finPeriodID, Equal<Required<Batch.finPeriodID>>,
								And<Batch.dateEntered, Greater<Required<Batch.dateEntered>>>>>, OrderBy<Desc<Batch.dateEntered>>>(this);
			PXResultset<Batch> res = select.SelectWindowed(0, 1, aAllocID, aFinPeriodID, aAfter);
			if (res.Count > 0) 
			{
				return (Batch)res[0];
			}
			return null;
		}

		protected virtual void RetrievePredefinedDestribution(out AccountWeightedList aAccounts, GLAllocation allocation) 
		{
			aAccounts = new AccountWeightedList();
            PXSelectBase<GLAllocationDestination> destSelect = new PXSelectJoin<GLAllocationDestination, InnerJoin<Account, On<Account.accountID, Equal<GLAllocationDestination.accountID>>>,
                                                                            Where<GLAllocationDestination.gLAllocationID,
                                                                            Equal<Required<GLAllocationDestination.gLAllocationID>>>>(this);
			foreach (PXResult<GLAllocationDestination, Account> iRes in destSelect.Select(allocation.GLAllocationID))
			{
                GLAllocationDestination iDest = iRes;
                Account acctDef = iRes;
                if (acctDef.Active != true)
                {
                    throw new PXException(Messages.AllocationDestinationAccountsContainInactiveAccount);
                }
				aAccounts.Add(iDest.BranchID.Value, iDest.AccountID.Value, iDest.SubID.Value, iDest.Weight.Value);
			}
		}

		//Calculates weigths for the distribution according to the current state of destination accounts
		protected virtual bool RetrieveDestributionByAcctState(out AccountWeightedList aAccounts, GLAllocation allocation, string aFiscalPeriod)
		{
            PXSelectBase<GLAllocationDestination> destSelect = new PXSelectJoin<GLAllocationDestination, 
                                                InnerJoin<Account, On<Account.accountID, Equal<GLAllocationDestination.accountID>>,                                                                                                
                                                InnerJoin<Sub, On<Sub.subID, Equal<GLAllocationDestination.subID>>>>, 
											    Where<GLAllocationDestination.gLAllocationID,
											    Equal<Required<GLAllocationDestination.gLAllocationID>>>>(this);
            int? basisLedgerID = allocation.BasisLederID.HasValue ? allocation.BasisLederID : allocation.AllocLedgerID;
			PXSelectBase<GLHistory> acctHistSelect = new PXSelectJoinGroupBy<GLHistory,
                                                InnerJoin<Account,On<Account.accountID,Equal<GLHistory.accountID>>,
                                                InnerJoin<Sub,On<Sub.subID,Equal<GLHistory.subID>>>>,
												Where<GLHistory.branchID, Equal<Required<GLHistory.branchID>>,
                                                And<Account.accountCD, Like<Required<Account.accountCD>>,
                                                And<Sub.subCD, Like<Required<Sub.subCD>>,
												And<GLHistory.finPeriodID, Equal<Required<GLHistory.finPeriodID>>,
												And<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>>>>>>,
												Aggregate<
																					Sum<GLHistory.finPtdDebit,
																					Sum<GLHistory.finPtdCredit,
																					Sum<GLHistory.finYtdBalance,
																			GroupBy<GLHistory.accountID,
																			GroupBy<GLHistory.subID,
                                                                            GroupBy<Account.active>>>>>>>>(this);
            			

			aAccounts = new AccountWeightedList();
			bool useAcctPTD = allocation.AllocMethod == Constants.AllocationMethod.ByAcctPTD;
			foreach (PXResult<GLAllocationDestination,Account,Sub> iRes in destSelect.Select(allocation.GLAllocationID))
			{
                GLAllocationDestination iDest = iRes;
                Account destAccount = iRes;                
                Sub destSubAccount = iRes;
                
                //Destination account should be checked for Active status.
                if (destAccount.Active != true)
                {
                    throw new PXException(Messages.AllocationDestinationAccountsContainInactiveAccount);
                }

                bool useBasisAccount = !String.IsNullOrEmpty(iDest.BasisAccountCD) && !String.IsNullOrEmpty(iDest.BasisSubCD);
				int? iBasisBranch = iDest.BasisBranchID.HasValue ? iDest.BasisBranchID : iDest.BranchID;
                
                string basisAcctCDWildCard = useBasisAccount ? iDest.BasisAccountCD : destAccount.AccountCD;
                string basisSubCDWildCard = useBasisAccount ? iDest.BasisSubCD : destSubAccount.SubCD;

                basisAcctCDWildCard = SubCDUtils.CreateSubCDWildcard(basisAcctCDWildCard, AccountAttribute.DimensionName);
                basisSubCDWildCard = SubCDUtils.CreateSubCDWildcard(basisSubCDWildCard, SubAccountAttribute.DimensionName);                

				AccountWeight acctWeight = new AccountWeight(iDest.BranchID.Value, iDest.AccountID.Value, iDest.SubID.Value, 0.0m);
                bool activeBaseAccountExists = false;
                bool historyRecordsExist = false;
				foreach (PXResult<GLHistory, Account, Sub> it in acctHistSelect.Select(iBasisBranch, basisAcctCDWildCard, basisSubCDWildCard, aFiscalPeriod, basisLedgerID))
				{
                    historyRecordsExist = true;
                    GLHistory iAcctHst = it;
					Account iAcctDef = it;

                    if (iAcctDef != null && iAcctDef.AccountID != null && iAcctDef.Active == true) activeBaseAccountExists = true;
                    else continue;

                    decimal balance = iAcctHst.YtdBalance?? 0.0m;
					decimal multiplier = AccountRules.IsCreditBalance(iAcctDef.Type) ? Decimal.MinusOne : Decimal.One;
					if (useAcctPTD)
					{
						balance = (iAcctHst.PtdDebit ?? Decimal.Zero) - (iAcctHst.PtdCredit ?? Decimal.Zero); 
					}
					acctWeight.Weight += balance * multiplier;                    
				}
                if (!activeBaseAccountExists && historyRecordsExist == true)
                {
                    throw new PXException(Messages.AllocationDestinationAccountsContainInactiveBasisAccount);
                }
				aAccounts.Add(acctWeight);
			}
			return true;
		}
		
		protected virtual bool RetrieveExternalDestribution(out AccountWeightedList aAccounts, GLAllocation allocation, string aFiscalPeriod) 
		{
			aAccounts = null;
			return false;
		}

		protected virtual bool HasUnpostedBatch(GLAllocation aAlloc, string aStartPeriod, string aEndPeriod) 
		{
			PXSelectBase<Batch> sel = new PXSelectJoin<Batch, InnerJoin<GLAllocationHistory,
											On<GLAllocationHistory.module, Equal<Batch.module>,
											And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>>,
											Where<Batch.posted,NotEqual<BQLConstants.BitOn>, 
											And<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
											And<Batch.tranPeriodID, GreaterEqual<Required<Batch.tranPeriodID>>,
											And<Batch.tranPeriodID,LessEqual<Required<Batch.tranPeriodID>>>>>>>(this);
			Object batch = sel.View.SelectSingle(aAlloc.GLAllocationID, aStartPeriod, aEndPeriod);
			return (batch != null);
		}

		protected static void AllocateAmount(JournalEntry aBatchGraph,GLAllocation aAlloc, IEnumerable<GLAllocationAccountHistory> aSources, Dictionary<int, Account> aAcctDefs, Ledger aAllocLedger, ref decimal totalDebit, ref decimal totalCredit) 
		{
			Batch batch = aBatchGraph.BatchModule.Current;
			foreach (GLAllocationAccountHistory aha in aSources)
			{
				if (Math.Abs(aha.AllocatedAmount.Value) > epsilon)
				{
					GLTran tran = new GLTran();
					decimal value = aha.AllocatedAmount.Value;
                    tran.BranchID = aha.BranchID;
					tran.AccountID = aha.ContrAccontID ?? aha.AccountID;
					tran.SubID = aha.ContrSubID ?? aha.SubID;
					tran.TranDesc = string.Format(Messages.AllocTranSourceDescr,(string.IsNullOrEmpty(aAlloc.Descr) ? aAlloc.GLAllocationID : aAlloc.Descr));
					Account iAcctDef = aAcctDefs[tran.AccountID.Value];
					if(!IsDenominationMatch(iAcctDef, aAllocLedger))
						throw new PXException(Messages.AccountsNotInBaseCury); //Account can't be denominated in the currency other then ledger's

					decimal absValue = PXCurrencyAttribute.Round<Batch.curyInfoID>(aBatchGraph.BatchModule.Cache, batch,Math.Abs(value),CMPrecision.TRANCURY);
					aha.AllocatedAmount = absValue * Math.Sign(aha.AllocatedAmount.Value); 
 					if (AccountRules.IsCreditBalance(iAcctDef.Type))
					{
						if (value > Decimal.Zero)
						{
							tran.CuryDebitAmt = absValue;
							totalDebit += absValue;
						}
						else
						{
							tran.CuryCreditAmt = absValue;
							totalCredit += absValue;
						}
					}
					else
					{
						if (value > Decimal.Zero)
						{
							tran.CuryCreditAmt = absValue;
							totalCredit += absValue;
						}
						else
						{
							tran.CuryDebitAmt = absValue;
							totalDebit += absValue;
						}
					}
					tran = aBatchGraph.GLTranModuleBatNbr.Insert(tran);
					aBatchGraph.AllocationAccountHistory.Insert(aha);
				}
			}
		} 
		
		protected static void DistributeAmount(JournalEntry aBatchGraph, GLAllocation aAlloc, decimal aAmount, AccountWeightedList aDestAccounts, Ledger aAllocLedger)
		{
			if (aAlloc.AllocMethod == Constants.AllocationMethod.ByPercent && (!aDestAccounts.isPercent()))
			{
                throw new PXException(Messages.SumOfDestsMustBe100);
			}
			Batch batch = aBatchGraph.BatchModule.Current;
			bool isDebit = aAmount > Decimal.Zero;
			aAmount = Math.Abs(aAmount);
			Currency currency = PXSelectReadonly<Currency, Where<Currency.curyID, Equal<Required<Currency.curyID>>>>.Select(aBatchGraph, batch.CuryID);
			int precision = ((currency!=null) && currency.DecimalPlaces.HasValue)? currency.DecimalPlaces.Value:(short)2;
			decimal remainder = aAmount;
			GLTran adjTran = null;
			foreach (AccountWeight it in aDestAccounts.List)
			{
				try
				{
					decimal amount = calcShareValue(aAmount, aDestAccounts.totalWeight, it.Weight, precision);
					amount = PXCurrencyAttribute.Round<Batch.curyInfoID>(aBatchGraph.BatchModule.Cache, batch, amount, CMPrecision.TRANCURY);
					if (isOutOfDbPrecision(amount)) 
					{
						throw new System.NotFiniteNumberException();
					}
					if (Math.Abs(amount) >= epsilon)
					{ 
						GLTran tran = new GLTran();
                        tran.BranchID = it.BranchID;
						tran.AccountID = it.AccountID;
						tran.SubID = it.SubID;
						tran.LedgerID = aAlloc.AllocLedgerID;
						Account iAcctDef = PXSelect<Account>.Search<Account.accountID>(aBatchGraph, tran.AccountID);
						if (!IsDenominationMatch(iAcctDef, aAllocLedger))
							throw new PXException(Messages.AccountsNotInBaseCury); //Account can't be denominated in the currency other then ledger's	
						if (isDebit)
						{
							if (amount > 0)
								tran.CuryDebitAmt = amount;
							else
								tran.CuryCreditAmt = -amount;
						}
						else
						{
							if (amount > 0)
								tran.CuryCreditAmt = amount;
							else
								tran.CuryDebitAmt = -amount;
						}
						tran.TranDesc = string.Format(Messages.AllocTranDestDescr, (string.IsNullOrEmpty(aAlloc.Descr) ? aAlloc.GLAllocationID : aAlloc.Descr));
						tran = (GLTran)aBatchGraph.GLTranModuleBatNbr.Cache.Insert(tran);
						if (adjTran == null || (adjTran.CuryDebitAmt < amount && adjTran.CuryCreditAmt < amount))
							adjTran = tran;
						remainder -= amount;
					}
				}
				catch (System.OverflowException)
				{
					throw new PXException(String.Format(Messages.AllocationDistributionTargetOverflowDetected, it.AccountID, it.SubID));
				}
				catch (System.NotFiniteNumberException)
				{
					throw new PXException(String.Format(Messages.AllocationDistributionTargetOverflowDetected, it.AccountID, it.SubID));
				}
			}

			if (remainder != Decimal.Zero && adjTran != null) 
			{
				if (isDebit)
					adjTran.CuryDebitAmt += remainder;
				else
					adjTran.CuryCreditAmt += remainder;
				adjTran = (GLTran)aBatchGraph.GLTranModuleBatNbr.Update(adjTran);
			}
			Batch batchCopy = (Batch)aBatchGraph.BatchModule.Cache.CreateCopy(aBatchGraph.BatchModule.Current);
			batchCopy.CuryControlTotal = batchCopy.CuryCreditTotal;
			batchCopy.ControlTotal = batchCopy.CreditTotal;
			if ((aBatchGraph.BatchModule.Current.CreditTotal ?? 0) == (aBatchGraph.BatchModule.Current.DebitTotal ?? 0))
			{
				batchCopy.Status = BatchStatus.Balanced;
			}
			batchCopy = (Batch)aBatchGraph.BatchModule.Cache.Update(batchCopy); 
		}

		protected virtual bool isOutOfQueue(GLAllocation aAlloc, string aFiscalPeriod) 
		{
			PXSelectBase<GLAllocation> sel =new PXSelectJoin<GLAllocation, 
											LeftJoin<GLAllocationHistory,
												On<GLAllocation.gLAllocationID, Equal<GLAllocationHistory.gLAllocationID>>,
											LeftJoin<Batch, On<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>,
														And<GLAllocationHistory.module,Equal<Batch.module>,
														And<Batch.tranPeriodID, Equal<Required<Batch.tranPeriodID>>>>>>>,
												Where<GLAllocation.sortOrder, Greater<Required<GLAllocation.sortOrder>>,
														And<GLAllocation.gLAllocationID,NotEqual<Required<GLAllocation.gLAllocationID>>>>>(this);
			foreach (PXResult<GLAllocation, GLAllocationHistory, Batch> it in sel.Select(aFiscalPeriod, aAlloc.SortOrder, aAlloc.GLAllocationID))
			{
				Batch batch = (Batch) it;
				if (batch != null && batch.BatchNbr != null)
				{
					continue; //Already processed
				}
				GLAllocation alloc = (GLAllocation)it;
				if(isApplicable(alloc,aFiscalPeriod)) 	return true; //Check, if alloactaion is applicable for the period
			}
			return false;
		}

		protected static bool isApplicable(GLAllocation aAllocation, string aFiscalPeriod) 
		{
            if (aFiscalPeriod != null)
            {
                bool isRecurrent = (aAllocation.Recurring.HasValue && aAllocation.Recurring.Value);
                if (aAllocation.StartFinPeriodID != null)
                {
                    if (ComparePeriods(aAllocation.StartFinPeriodID, aFiscalPeriod, false) > 0) return false; //Filter out records, which are starting in future
                    if (isRecurrent)
                    {
                        if (ComparePeriods(aAllocation.StartFinPeriodID, aFiscalPeriod, isRecurrent) > 0) return false; //We need to redo check for recurring records
                    }
                }
                if (aAllocation.EndFinPeriodID != null)
                {
                    if (ComparePeriods(aAllocation.EndFinPeriodID, aFiscalPeriod, isRecurrent) < 0) return false; //Filter out
                }
            }
            else 
            {
                return false;
            }
			return true;
		}

		protected virtual Ledger FindLedger(int? aLedgerID) 
		{
			Ledger res = PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>.Select(this, aLedgerID);
			return res;
		}

		protected virtual void ValidateFinPeriod( string aFinPeriodID)
		{
			FinPeriod period = PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>>.Select(this, aFinPeriodID);
			if (period == null)
                throw new PXException(Messages.NoPeriodsDefined);
			if (period.Closed?? false) 
			{
                throw new FiscalPeriodClosedException(period.FinPeriodID);
			}
			if (!(period.Active ?? false)) 
			{
                throw new FiscalPeriodInactiveException(period.FinPeriodID);
            }
		}

		protected virtual void FindAllocatedAmount(GLAllocation aAllocation, GLHistory aSourceAcctHistory, out decimal aAllocated, out decimal aAllocatedForPriorPeriods) 
		{
			aAllocated = Decimal.Zero;
			aAllocatedForPriorPeriods = Decimal.Zero;
			if (aAllocation.SourceLedgerID.HasValue && aAllocation.AllocLedgerID == aAllocation.SourceLedgerID)
			{
				aAllocated = (aSourceAcctHistory.AllocPtdBalance ?? Decimal.Zero);
				aAllocatedForPriorPeriods = aSourceAcctHistory.AllocBegBalance ?? Decimal.Zero;
			}
			else
			{
				GLHistory aActualHistory = PXSelect<GLHistory, Where<GLHistory.branchID, Equal<Required<GLHistory.branchID>>, 
                                                        And<GLHistory.accountID, Equal<Required<GLHistory.accountID>>,
														And<GLHistory.subID, Equal<Required<GLHistory.subID>>,
														And<GLHistory.finPeriodID, Equal<Required<GLHistory.finPeriodID>>,
                                                        And<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>>>>>>>.Select(this, aSourceAcctHistory.BranchID, aSourceAcctHistory.AccountID, aSourceAcctHistory.SubID, aSourceAcctHistory.FinPeriodID, aAllocation.AllocLedgerID);
				if (aActualHistory != null) 
				{
					aAllocated = (aActualHistory.AllocPtdBalance ?? Decimal.Zero);
					aAllocatedForPriorPeriods = aActualHistory.AllocBegBalance ?? Decimal.Zero;
				}
			}


		}

		protected virtual void FindAllocatedAmountDetailed(string aAllocationID, GLHistory aSrcAcctHistory, out decimal aAllocated, out decimal aAllocatedForPriorPeriods)
		{
			aAllocated = Decimal.Zero;
			aAllocatedForPriorPeriods = Decimal.Zero;
			foreach (PXResult<GLAllocationAccountHistory, Batch, GLAllocationHistory> it in PXSelectJoin<GLAllocationAccountHistory, InnerJoin<Batch, On<GLAllocationAccountHistory.module, Equal<Batch.module>,
																		 And<GLAllocationAccountHistory.batchNbr, Equal<Batch.batchNbr>>>,
													InnerJoin<GLAllocationHistory, On<GLAllocationHistory.module, Equal<Batch.module>,
																		 And<GLAllocationHistory.batchNbr, Equal<Batch.batchNbr>>>>>,
												Where<GLAllocationHistory.gLAllocationID, Equal<Required<GLAllocationHistory.gLAllocationID>>,
												And<Batch.finPeriodID, Equal<Required<Batch.finPeriodID>>,
                                                And<GLAllocationAccountHistory.branchID, Equal<Required<GLAllocationAccountHistory.branchID>>,
												And<GLAllocationAccountHistory.accountID, Equal<Required<GLAllocationAccountHistory.accountID>>,
												And<GLAllocationAccountHistory.subID, Equal<Required<GLAllocationAccountHistory.subID>>>>>>>>.Select(this,aAllocationID,aSrcAcctHistory.FinPeriodID,aSrcAcctHistory.BranchID, aSrcAcctHistory.AccountID,aSrcAcctHistory.SubID))
			{
				GLAllocationAccountHistory aah = (GLAllocationAccountHistory)it;
				aAllocated += aah.AllocatedAmount?? Decimal.Zero;
				aAllocatedForPriorPeriods += aah.PriorPeriodsAllocAmount ?? Decimal.Zero;
			}
		}

		protected static string GenerateRefNbr(string aAllocationID, string aPeriod, int aCount) 
		{
			string year = FiscalPeriodUtils.FiscalYear(aPeriod);
			string period = FiscalPeriodUtils.PeriodInYear(aPeriod);
			int len = aAllocationID.Trim().Length;
			int maxNbr = len>6?10:100;
			int nextNbr = (aCount%maxNbr +1);
			string separator = "-";
			string sep1 = len<=9?separator:String.Empty;
			string sep2 = len<=7?separator:String.Empty;
			string sep3 = len<=8?separator:String.Empty;
			return String.Concat(aAllocationID.Trim(), sep1, year.Substring(2, 2),sep2, period.Substring(0, 2),sep3,nextNbr.ToString());   
		} 
		#endregion

		#region Utility Functions
		protected int? FindAccountByCD(string aAcctCD)
		{
			if (aAcctCD == null) return null;
			PXSelectBase<Account> select = new PXSelect<Account, Where<Account.accountCD, Equal<Required<Account.accountCD>>>>(this);
			Account acct = (Account)select.View.SelectSingle(aAcctCD);
			if (acct == null) return null;
			return acct.AccountID;
		}

		protected int? FindSubByCD(string aSubCD)
		{
			if (aSubCD == null) return null;
			PXSelectBase<Sub> select = new PXSelect<Sub, Where<Sub.subCD, Equal<Required<Sub.subCD>>>>(this);
			Sub sub = (Sub)select.View.SelectSingle(aSubCD);
			if (sub == null) return null;
			return sub.SubID;
		}

		

		#endregion 
		#region Static Functions

		public static bool IsDenominationMatch(Account aAcct, Ledger aLedger) 
		{
			if(!String.IsNullOrEmpty(aAcct.CuryID)) 
				return (aAcct.CuryID == aLedger.BaseCuryID);
			return true;
		}

		protected static decimal calcShareValue(decimal aAmount, decimal aBasis, decimal aWeight, int aPrecision) 
		{
			return (decimal)Decimal.Round(((aAmount * aWeight) / aBasis), aPrecision, MidpointRounding.ToEven);			
		}

		protected static decimal calcShareValue(decimal aAmount, decimal aBasis, decimal aWeight)
		{
			return (aAmount * aWeight) / aBasis;
		}

		protected static void PrepareBatch(JournalEntry aBatchGraph,GLAllocation aAlloc)
		{
           
		}

		
		//Returns -1 if aPeriod1 is before(less then) aPeriod2, +1 if aPeriod1 is after (greater) then aPeriod2, 0 if theay are equal
		//If aIgnoreYear is true, only the year of the period is ignored
		static int ComparePeriods(string aPeriod1, string aPeriod2, bool aIgnoreYear) 
		{
			int year1, nbr1;
			int year2, nbr2;
			AllocationMaint.ParseFiscalPeriodID(aPeriod1, out year1, out nbr1);
			AllocationMaint.ParseFiscalPeriodID(aPeriod2, out year2, out nbr2);
			if (!aIgnoreYear) 
			{
				int yearSign = Math.Sign(year1 - year2);
				if (yearSign != 0) return yearSign;
			}
			return Math.Sign(nbr1-nbr2);
		}

		static string ConvertToSameYear(string aPeriod, string aRefPeriod) 
		{
			string refYear = FiscalPeriodUtils.FiscalYear(aRefPeriod);
			string period = FiscalPeriodUtils.FirstPeriodOfYear;
			if (!String.IsNullOrEmpty(aPeriod))
			{
				string year = FiscalPeriodUtils.FiscalYear(aPeriod);
				if (year == refYear) return aPeriod;
				period = FiscalPeriodUtils.PeriodInYear(aPeriod);
			}
			return FiscalPeriodUtils.Assemble(refYear, period);
		}

		static string FirstPeriodOfYear(string aRefPeriod) 
		{
			string refYear = FiscalPeriodUtils.FiscalYear(aRefPeriod);
			return FiscalPeriodUtils.Assemble(refYear, FiscalPeriodUtils.FirstPeriodOfYear);
		}

		private static bool isOutOfDbPrecision(decimal aValue) 
		{
			decimal limit = 1E16m;
			return Math.Abs(aValue) >= limit; 
		}

			

		#endregion
		#region Static variables
		protected static decimal MinAmountToDistribute = 0.01m;
		private const  decimal epsilon = 0.0001m;
		#endregion
		#region Filter Events
		protected virtual void AllocationFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
		    this.Allocations.Cache.IsDirty = false;
		    cache.IsDirty = false;
		}
	
		#endregion

	}
	#region UtilityClasses 
	public class AccountWeightedList 
	{
		public AccountWeightedList() 
		{
			this.List = new List<AccountWeight>();
		}
		public virtual void Add(AccountWeight item) 
		{
			int index = this.List.BinarySearch(item);
			if (index < 0)
			{
				index = ~index;
				this.List.Insert(index, item);
			}
			else 
			{
				this.List[index].Weight += item.Weight;
			}
			totalWeight += item.Weight;
		}

		public virtual int Find(AccountWeight item)
		{
			return this.List.BinarySearch(item);
		}

		public virtual bool IsExist(int aBranch, int aAccount, int aSub) 
		{
			return (this.Find(aBranch, aAccount, aSub) >= 0);
		}
		public virtual int Find(int aBranch, int aAccount, int aSub)
		{
            return this.List.BinarySearch(new AccountWeight(aBranch,aAccount, aSub, 0.0m));
		}

		public virtual void Add(int branchID, int accountId, int subID, decimal weight)
		{
            this.Add(new AccountWeight(branchID, accountId, subID, weight));
		}

		public virtual void Recalculate() 
		{
			totalWeight = 0.0m;
			foreach (AccountWeight it in this.List) 
			{
				totalWeight += it.Weight;
			}
		}
		public virtual bool isPercent() 
		{
			return (Decimal.Round(totalWeight, 2) == 100.0m); 
		}
		public List<AccountWeight> List;
		public decimal totalWeight =Decimal.Zero;
		
	}
	public class AccountWeight: IComparable, IComparable<AccountWeight> 
	{
		public AccountWeight(int aBranchID, int aAcctID, int aSubId, decimal aWeight) 
		{
            this.BranchID = aBranchID;
            this.AccountID = aAcctID;
			this.SubID = aSubId;
			this.Weight = aWeight;
		}

        public AccountWeight(int aBranchID, int aAcctID, int aSubId)
		{
            this.BranchID = aBranchID;
			this.AccountID = aAcctID;
			this.SubID = aSubId;
			this.Weight = 0.0m;
		}

        public int BranchID;
		public int AccountID;
		public int SubID;
		public decimal Weight=Decimal.Zero;
		
		#region IComparable Members

		public virtual int CompareTo(object op2)
		{
			AccountWeight aw = (AccountWeight) op2;
			return this.CompareTo(aw);
		}
	
		#endregion

		#region IComparable<AccountWeight> Members

		public virtual int CompareTo(AccountWeight op2)
		{
			if (op2 == null) return 1;
            if (op2.BranchID == this.BranchID)
            {
                if (op2.AccountID == this.AccountID)
                {
                    return Math.Sign(this.SubID - op2.SubID);
                }
                return Math.Sign(this.AccountID - op2.AccountID);
            }
            return Math.Sign(this.BranchID - op2.BranchID);
		}

		#endregion
	}
	#endregion UtilityClasses
}
