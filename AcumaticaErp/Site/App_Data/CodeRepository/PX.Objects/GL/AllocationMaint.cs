using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.Data;

namespace PX.Objects.GL
{
	public class AllocationMaint : PXGraph<AllocationMaint, GLAllocation>, PXImportAttribute.IPXPrepareItems
	{

		#region Ctor+Public Members
		public AllocationMaint()
		{
			GLSetup setup = GLSetup.Current;
			PXUIFieldAttribute.SetEnabled(this.Batches.Cache, null, false);
			this.Batches.Cache.AllowInsert = false;
			this.Batches.Cache.AllowDelete = false;
			this.Batches.Cache.AllowUpdate = false;			
		}

		public PXSelect<GLAllocation> AllocationHeader;
		public PXSelect<GLAllocation, Where<GLAllocation.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>> Allocation;
		[PXImport(typeof(GLAllocation))]
		public PXSelect<GLAllocationSource, Where<GLAllocationSource.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Asc<GLAllocationSource.lineID>>> Source;
		[PXImport(typeof(GLAllocation))]
		public PXSelect<GLAllocationDestination, Where<GLAllocationDestination.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Asc<GLAllocationDestination.lineID>>> Destination;
		public PXSelectJoin<Batch, InnerJoin<GLAllocationHistory,
														On<Batch.batchNbr, Equal<GLAllocationHistory.batchNbr>,
														And<Batch.module, Equal<GLAllocationHistory.module>>>>,
									Where<GLAllocationHistory.gLAllocationID, Equal<Current<GLAllocation.gLAllocationID>>>, OrderBy<Desc<Batch.tranPeriodID,Desc<Batch.batchNbr>>>> Batches;
		public PXSetup<Company> Company;
		public PXSetup<GLSetup> GLSetup;

		#endregion

		#region Buttons
		public PXAction<GLAllocation> viewBatch;
		[PXUIField(DisplayName = Messages.BatchDetails, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable ViewBatch(PXAdapter adapter)
		{
			Batch row = Batches.Current;
			if (row != null)
			{
				JournalEntry graph = CreateInstance<JournalEntry>();
				graph.BatchModule.Current = row;
				throw new PXRedirectRequiredException(graph, true, "View Batch"){Mode = PXBaseRedirectException.WindowMode.NewWindow};
			}
			return adapter.Get();
		}

		#endregion

		#region GLAllocation Events Handlers
		protected virtual void GLAllocation_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			GLAllocation row = (GLAllocation)e.Row;
			if ((e.Operation & PXDBOperation.Command) != PXDBOperation.Delete)
			{
				if (row.Active == true && (this.Source.Select().Count == 0))
				{
					throw new PXException(Messages.SourceAccountNotSpecified);
				}

				//In the case of external rule Distribution is defined dynamically in external table
				if (row.Active == true && 
					row.AllocMethod != Constants.AllocationMethod.ByExternalRule 
						&& this.Destination.Select().Count == 0)
				{
					throw new PXException(Messages.DestAccountNotSpecified);
				}

				if (row.StartFinPeriodID != null && row.EndFinPeriodID != null)
				{
					int startYr, endYr;
					int startNbr, endNbr;
					if (!ParseFiscalPeriodID(row.StartFinPeriodID, out startYr, out startNbr)) 
					{
                                                                                                                                                      
						cache.RaiseExceptionHandling<GLAllocation.startFinPeriodID>(e.Row, row.StartFinPeriodID, new PXSetPropertyException(Messages.AllocationStartPeriodHasIncorrectFormat));
					}

					if (!ParseFiscalPeriodID(row.EndFinPeriodID, out endYr, out endNbr))
					{
						cache.RaiseExceptionHandling<GLAllocation.endFinPeriodID>(e.Row, row.EndFinPeriodID, new PXSetPropertyException(Messages.AllocationEndPeriodHasIncorrectFormat));
					}
					bool isReversed = (endYr < startYr) || (endYr == startYr && startNbr > endNbr);
					if (isReversed)
					{
						cache.RaiseExceptionHandling<GLAllocation.endFinPeriodID>(e.Row, row.EndFinPeriodID, new PXSetPropertyException(Messages.AllocationEndPeriodIsBeforeStartPeriod));
					}
				}
				if (!this.ValidateSrcAccountsForCurrency()) 
				{
					throw new PXException(Messages.AccountsNotInBaseCury);
				}

				GLAllocationSource src;
				if (!this.ValidateSrcAccountsForInterlacing(out src))
				{
					this.Source.Cache.RaiseExceptionHandling<GLAllocationSource.accountCD>(src, src.AccountCD, new PXSetPropertyException(Messages.AllocationSourceAccountSubInterlacingDetected, PXErrorLevel.RowError));
				}

				if (row.Active == true 
					&& this.isWeigthRecalcRequired())
				{
					decimal total = 0.00m;
                    GLAllocationDestination dest = null; 
					foreach (GLAllocationDestination iDest in this.Destination.Select())
					{
						if (iDest.Weight.HasValue)
						{
							total += iDest.Weight.Value;
							decimal weight = (decimal)iDest.Weight.Value;							
							if (weight <= 0.00m || weight > 100.00m)
							{
								this.Destination.Cache.RaiseExceptionHandling<GLAllocationDestination.weight>(iDest, iDest.Weight, 
										new PXSetPropertyException(string.Format(Messages.ValueForWeight,0,100)));
								return;
							}
						}
                        if (dest == null)
                            dest = iDest;
						
					}
					if (Math.Abs(total - 100.0m) >= 0.000001m)
					{
                        if (dest != null)
                        {
                            this.Destination.Cache.RaiseExceptionHandling<GLAllocationDestination.weight>(dest, dest.Weight,
                                        new PXSetPropertyException(Messages.SumOfDestsMustBe100));
                        }
                        else
                        {
                            throw new PXException(Messages.SumOfDestsMustBe100);
                        }
					}
				}

			}
		}
	
		protected virtual void GLAllocation_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            GLAllocation record = (GLAllocation)e.Row;
			bool isBasisAcctEnabled = (record.AllocMethod == Constants.AllocationMethod.ByAcctPTD) || (record.AllocMethod == Constants.AllocationMethod.ByAcctYTD);
			PXUIFieldAttribute.SetEnabled<GLAllocation.basisLederID>(cache, record, isBasisAcctEnabled);
			PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisBranchID>(this.Destination.Cache, null, isBasisAcctEnabled);
			PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisAccountCD>(this.Destination.Cache, null, isBasisAcctEnabled);
            PXUIFieldAttribute.SetEnabled<GLAllocationDestination.basisSubCD>(this.Destination.Cache, null, isBasisAcctEnabled);
			
            PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisBranchID>(this.Destination.Cache, null, isBasisAcctEnabled);
			PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisAccountCD>(this.Destination.Cache, null, isBasisAcctEnabled);
            PXUIFieldAttribute.SetVisible<GLAllocationDestination.basisSubCD>(this.Destination.Cache, null, isBasisAcctEnabled);

			bool isWeightEnabled = (record.AllocMethod == Constants.AllocationMethod.ByWeight) || (record.AllocMethod == Constants.AllocationMethod.ByPercent);
			PXUIFieldAttribute.SetEnabled<GLAllocationDestination.weight>(this.Destination.Cache, null, isWeightEnabled);
			PXUIFieldAttribute.SetVisible<GLAllocationDestination.weight>(this.Destination.Cache, null, isWeightEnabled);

		}

		protected virtual void GLAllocation_AllocMethod_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			GLAllocation record = (GLAllocation)e.Row;
			if ((string)e.OldValue == Constants.AllocationMethod.ByAcctPTD || ((string)e.OldValue == Constants.AllocationMethod.ByAcctYTD)) 
			{
				bool isBasisAcctEnabled = (record.AllocMethod == Constants.AllocationMethod.ByAcctPTD) || (record.AllocMethod == Constants.AllocationMethod.ByAcctYTD);
				//Reset BasisAccount Fields
				if (!isBasisAcctEnabled) 
				{
					record.BasisLederID = null;
					foreach (GLAllocationDestination iDest in this.Destination.Select()) 
					{
						iDest.BasisAccountCD = iDest.BasisSubCD = null;
						this.Destination.Cache.Update(iDest);
					}					
				}
			}
			if (((string)e.OldValue == Constants.AllocationMethod.ByPercent) || ((string)e.OldValue == Constants.AllocationMethod.ByWeight))
			{
				bool isWeightEnabled = (record.AllocMethod == Constants.AllocationMethod.ByWeight) || (record.AllocMethod == Constants.AllocationMethod.ByPercent);
				if (!isWeightEnabled) 
				{
					//Reset Weight/Percent field
					foreach (GLAllocationDestination iDest in this.Destination.Select())
					{
						iDest.Weight = null;
						this.Destination.Cache.Update(iDest);
					}
				}
			}
		}

		public virtual void GLAllocation_StartFinPeriodID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			PXSelectBase selectdate = new PXSelect<FinPeriod, Where<FinPeriod.startDate, LessEqual<Required<FinPeriod.startDate>>, And<FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>>>>(this);
			FinPeriod res = selectdate.View.SelectSingle(Accessinfo.BusinessDate, Accessinfo.BusinessDate) as FinPeriod;
			if (res != null)
				e.NewValue = FinPeriodIDFormattingAttribute.FormatForDisplay(res.FinPeriodID);
		}

		public virtual void GLAllocation_EndFinPeriodID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			PXSelectBase selectdate = new PXSelect<FinPeriod, Where<FinPeriod.startDate, LessEqual<Required<FinPeriod.startDate>>, And<FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>>>>(this);
			FinPeriod res = selectdate.View.SelectSingle(Accessinfo.BusinessDate, Accessinfo.BusinessDate) as FinPeriod;
			if (res != null)
			{
				PXSelectBase selectperiod = new PXSelect<FinPeriod, Where<FinPeriod.startDate, Less<FinPeriod.endDate>, And<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>>>, OrderBy<Desc<FinPeriod.periodNbr>>>(this);
				FinPeriod period = selectperiod.View.SelectSingle(res.FinYear) as FinPeriod;
				if (period != null)
					e.NewValue = FinPeriodIDFormattingAttribute.FormatForDisplay(period.FinPeriodID);
			}
		}

		protected virtual void GLAllocation_AllocLedgerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			GLAllocation row = (GLAllocation)e.Row;
			row.SourceLedgerID = row.AllocLedgerID;
		}
		#endregion
		#region GLAllocationSource Events Handlers
		protected virtual void GLAllocationSource_LimitPercent_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			decimal? percent= (decimal? )e.NewValue; 
			if(percent.HasValue && (percent.Value <0.0m || percent.Value > 100.0m))
			{
				throw new PXSetPropertyException(Messages.ValueForLimitPercent);
			}   
		}
		protected virtual void GLAllocationSource_LimitPercent_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			GLAllocationSource src = (GLAllocationSource)e.Row;
			if (src.LimitPercent.HasValue) 
			{
				src.LimitAmount = null;
				//this.Source.Cache.Update(src);
			}
		}
		protected virtual void GLAllocationSource_LimitAmount_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			GLAllocationSource src = (GLAllocationSource)e.Row;
			if (src.LimitAmount.HasValue)
			{
				src.LimitPercent = null;
				//this.Source.Cache.Update(src);
			}
		}
		protected virtual void GLAllocationSource_SubCD_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			//if(String.IsNullOrEmpty((string)e.NewValue))
			//{
			//    throw new PXSetPropertyException(Messages.AllocationSrcEmptySubAccMask);
			//}
			//e.NewValue = ((string)e.NewValue).Replace(SubCDUtils.CS_SUB_CD_FILL_CHAR, SubCDUtils.CS_SUB_CD_WILDCARD);
			e.Cancel = true;
		}
		protected virtual void GLAllocationSource_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
		{
			this.UpdateParentStatus();
		}
		protected virtual void GLAllocationSource_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
		{
			this.UpdateParentStatus();
		}
		protected virtual void GLAllocationSource_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
		{
			this.UpdateParentStatus();
		}

        protected virtual void GLAllocationSource_BranchID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
        {
            GLAllocationSource row = (GLAllocationSource)e.Row;
        }
		protected virtual void GLAllocationSource_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			GLAllocationSource row = (GLAllocationSource)e.Row;
			if (string.IsNullOrEmpty(row.AccountCD.Trim())) 
			{
				if (cache.RaiseExceptionHandling<GLAllocationSource.accountCD>(e.Row, row.AccountCD, new PXSetPropertyException(Messages.AllocationSrcEmptyAccMask, PXErrorLevel.Error)))
				{
					throw new PXRowPersistingException(typeof(GLAllocationSource.accountCD).Name, row.AccountCD, Messages.AllocationSrcEmptyAccMask );
				}				
			}
		}
		
		#endregion
		#region GLAllocation Destination Event Handlers 
		protected virtual void GLAllocationDestination_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
		{
			if (this.isWeigthRecalcRequired())
			{
				GLAllocationDestination record = (GLAllocationDestination)e.Row;
				if (record.Weight == null)
				{
					record.Weight = 100;
					foreach (GLAllocationDestination it in this.Destination.Select())
					{
						if (object.ReferenceEquals(it, record)) continue;
						record.Weight -= it.Weight;
					}

					this.justInserted = record.LineID;
				}
			}
			else
				this.justInserted = null;
		}
		protected virtual void GLAllocationDestination_Weight_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			if (this.isWeigthRecalcRequired())
			{
				GLAllocationDestination dest = (GLAllocationDestination)e.Row;
				decimal newWeght = (decimal)e.NewValue;
				if (newWeght <= 0.00m || newWeght > 100.00m ) 
				{
					throw new PXSetPropertyException(string.Format(Messages.ValueForWeight,0,100));
				}
			}
		}
        protected virtual void GLAllocationDestination_BasisSubCD_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
        {
            e.Cancel = true;
        }
		protected virtual void GLAllocationDestination_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
		{
			if (this.isWeigthRecalcRequired())
			{
				if (PXEntryStatus.Notchanged == this.Allocation.Cache.GetStatus(this.Allocation.Current))
				{
					//Set State  of the Terms To Modified - for the validation
					this.Allocation.Cache.SetStatus(this.Allocation.Current, PXEntryStatus.Updated);
				}
			}
			UpdateParentStatus();
		}
		protected virtual void GLAllocationDestination_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
		{
			if(this.Allocation.Cache.GetStatus(this.Allocation.Current) != PXEntryStatus.Deleted && this.isWeigthRecalcRequired())
			{
				GLAllocationDestination row = (GLAllocationDestination)e.Row;
                
				if (!this.isMassDelete)
				{
					if (!(this.justInserted.HasValue && this.justInserted.Value == row.LineID))
					{                        
						this.distributePercentOf(row);
						this.Destination.View.RequestRefresh();
					}
					this.Allocation.Cache.Update(this.Allocation.Current);
				}
			}
		}
		protected virtual void GLAllocationDestination_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
		{
            
			this.UpdateParentStatus();
        }
        protected virtual void GLAllocationDestination_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
        {
            GLAllocationDestination row = (GLAllocationDestination)e.Row;			
			if ((e.Operation & PXDBOperation.Command) != PXDBOperation.Delete)
			{
				GLAllocation parent = this.Allocation.Current;
				List<GLAllocationDestination> duplicated = FindDuplicated(row, parent);
				//if (parent.AllocMethod == Constants.AllocationMethod.ByAcctPTD || parent.AllocMethod == Constants.AllocationMethod.ByAcctYTD)
				{
					if (duplicated.Count > 0)
					{
						duplicated.Add(row);
						foreach (GLAllocationDestination it in duplicated)
						{
							PXErrorLevel level = (parent.AllocMethod == Constants.AllocationMethod.ByAcctPTD || parent.AllocMethod == Constants.AllocationMethod.ByAcctYTD) ? PXErrorLevel.RowError : PXErrorLevel.RowWarning;
							string message = (parent.AllocMethod == Constants.AllocationMethod.ByAcctPTD || parent.AllocMethod == Constants.AllocationMethod.ByAcctYTD) ? Messages.ERR_AllocationDestinationAccountMustNotBeDuplicated : Messages.AllocationDestinationAccountAreIdentical;
							cache.RaiseExceptionHandling<GLAllocationDestination.accountID>(it, it.AccountID, new PXSetPropertyException(message, level));
						}
					}
				}
			}
        }
		#endregion

		#region Helper Functions
		protected virtual void UpdateParentStatus()
		{
			if (this.Allocation.Current != null)
			{
				if (this.Allocation.Cache.GetStatus(this.Allocation.Current) == PXEntryStatus.Notchanged)
				{
					this.Allocation.Cache.SetStatus(this.Allocation.Current, PXEntryStatus.Updated);
				}
			}
		}
		protected virtual void distributePercentOf(GLAllocationDestination row) 
		{
			if (row != null && row.Weight.HasValue && row.Weight != null)
			{
				//Find last
				GLAllocationDestination lastRow = null;
                Decimal total = Decimal.Zero;
				foreach (GLAllocationDestination it in this.Destination.Select())
				{
					if (object.ReferenceEquals(it, row)) continue;
                    total += it.Weight??Decimal.Zero;
					if (lastRow == null || (it.LineID.HasValue && it.LineID.Value > lastRow.LineID.Value))
					{
						lastRow = it;
					}
				}

                Decimal remainder = (100.0m - total);
                remainder = remainder < row.Weight.Value ? remainder : row.Weight.Value;
				if (lastRow != null && remainder > Decimal.Zero)
				{

					lastRow.Weight += remainder;					
				}
			}

		}
		protected virtual bool ValidateSrcAccountsForCurrency() 
		{
			PXSelectBase<Account> acctSel = new PXSelect<Account,Where<Account.accountCD,Like<Required<Account.accountCD>>,
															And<Account.curyID,IsNotNull, And<Account.curyID,NotEqual<Current<Company.baseCuryID>>>>>>(this); 
			foreach (GLAllocationSource iSrc in this.Source.Select()) 
			{
				string acctCDWildCard = SubCDUtils.CreateSubCDWildcard(iSrc.AccountCD, AccountAttribute.DimensionName);
				foreach (Account iAcct in acctSel.Select(acctCDWildCard))
				{
					if (iAcct != null) return false;
				}
			}
			return true;
		}

		protected virtual bool ValidateSrcAccountsForInterlacing(out GLAllocationSource aRow)
		{
			aRow = null;
			Dictionary<AllocationProcess.BranchAccountSubKey, AllocationSourceDetail> sources = new Dictionary<AllocationProcess.BranchAccountSubKey, AllocationSourceDetail>(); 
			foreach (GLAllocationSource iSrc in this.Source.Select()) 
			{
				if (IsSourceInterlacingWithDictionary(iSrc, sources))
				{
					aRow = iSrc;
					return false;
				}
			}
			return true;
		}

		protected virtual bool IsSourceInterlacingWithDictionary(GLAllocationSource aSrc, Dictionary<AllocationProcess.BranchAccountSubKey, AllocationSourceDetail> aSrcDict) 
		{
			string acctCDWildCard = SubCDUtils.CreateSubCDWildcard(aSrc.AccountCD, AccountAttribute.DimensionName);
			string subCDWildCard = SubCDUtils.CreateSubCDWildcard(aSrc.SubCD, SubAccountAttribute.DimensionName);
			foreach (Account iAcct in PXSelect<Account, Where<Account.accountCD, Like<Required<Account.accountCD>>>>.Select(this, acctCDWildCard))
			{
				foreach (Sub iSub in PXSelect<Sub, Where<Sub.subCD, Like<Required<Sub.subCD>>>>.Select(this, subCDWildCard))
				{
					AllocationProcess.BranchAccountSubKey key = new AllocationProcess.BranchAccountSubKey(aSrc.BranchID.Value, iAcct.AccountID.Value, iSub.SubID.Value);
					if (aSrcDict.ContainsKey(key))
					{
						return true;
					}
					else
					{
						AllocationSourceDetail detail = new AllocationSourceDetail(aSrc);

						detail.AccountID = iAcct.AccountID;
						detail.SubID = iSub.SubID;
						if (detail.ContraAccountID != null && detail.ContraSubID == null)
						{
							detail.ContraSubID = detail.SubID;
						}
						aSrcDict[key] = detail;
					}
				}
			}
			return false;
		}

		protected virtual bool isWeigthRecalcRequired()
		{
			return (this.Allocation.Current.AllocMethod == Constants.AllocationMethod.ByPercent);
		}

        protected virtual List<GLAllocationDestination> FindDuplicated(GLAllocationDestination aDest, GLAllocation aDefinition)
        {
            List<GLAllocationDestination> duplicated = new List<GLAllocationDestination>();
            bool includeBasis = (aDefinition.AllocMethod == Constants.AllocationMethod.ByAcctPTD || aDefinition.AllocMethod == Constants.AllocationMethod.ByAcctYTD);
            foreach (GLAllocationDestination jDest in this.Destination.Select())
            {
                if (object.ReferenceEquals(aDest, jDest)
                       || (aDest.GLAllocationID == jDest.GLAllocationID && aDest.LineID == jDest.LineID)) continue; //Skip self

                if (aDest.AccountID == jDest.AccountID
                     && aDest.SubID == jDest.SubID
					 && aDest.BranchID == jDest.SubID)
                {
                    duplicated.Add(jDest);
                }                    
            }
            return duplicated;
        }  
		#endregion
		#region Utility functions
		public static bool ParseFiscalPeriodID( string fiscalPeriodID, out int year, out int periodNbr) 
		{
			try
			{
				year = int.Parse( FiscalPeriodUtils.FiscalYear(fiscalPeriodID));
				periodNbr = int.Parse(FiscalPeriodUtils.PeriodInYear(fiscalPeriodID));
			}
			catch(FormatException) 
			{
				year = -1;
				periodNbr = -1;
				return false; 
			}
			return true;
		}
		public class AllocationSourceDetail
		{
			public AllocationSourceDetail() { }
			public AllocationSourceDetail(GLAllocationSource aSrc) 
			{
				this.CopyFrom(aSrc);
			}

			public int? LineID;
            public int? BranchID;
			public int? AccountID;
			public int? SubID;
			public int? ContraAccountID;
			public int? ContraSubID;
			public decimal? LimitAmount;
			public decimal? LimitPercent;
			public virtual void CopyFrom(GLAllocationSource aSrc) 
			{
				this.LineID = aSrc.LineID;
                this.BranchID = aSrc.BranchID;
				this.ContraAccountID = aSrc.ContrAccountID;
				this.ContraSubID = aSrc.ContrSubID;
				this.LimitAmount = aSrc.LimitAmount;
				this.LimitPercent = aSrc.LimitPercent;
			}
		}

		#endregion

		#region private members
		private int? justInserted;
		private bool isMassDelete = false; 
		#endregion


		#region IPXPrepareItems Members

		public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
		{
			return true;
		}

		public bool RowImporting(string viewName, object row)
		{
			return row == null;
		}

		public bool RowImported(string viewName, object row, object oldRow)
		{
			return oldRow == null;
		}

		public void PrepareItems(string viewName, IEnumerable items)
		{
			
		}

		#endregion
				
	}
}
