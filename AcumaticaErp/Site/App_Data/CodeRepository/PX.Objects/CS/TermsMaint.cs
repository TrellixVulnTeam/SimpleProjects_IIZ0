#define ASP_NET_VISIBLE_BUG_WRKAROUND
using System;
using PX.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace PX.Objects.CS
{
	public class TermsMaint : PXGraph<TermsMaint, Terms>
	{
		public PXSelect<Terms> TermsDef;
		public PXSelect<TermsInstallments, Where<TermsInstallments.termsID, Equal<Current<Terms.termsID>>>, OrderBy<Asc<TermsInstallments.instDays>>> Installments;

		#region Terms Events

		protected virtual void Terms_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            Terms term = (Terms)e.Row;
			bool hasMultipleInst = (term.InstallmentType == TermsInstallmentType.Multiple);
			bool hasCustomSchedule = term.InstallmentMthd == TermsInstallmentMethod.SplitByPercents;
#if (!ASP_NET_VISIBLE_BUG_WRKAROUND) 
			PXUIFieldAttribute.SetVisible<Terms.installmentCntr>(cache, term, hasMultipleInst);
			PXUIFieldAttribute.SetVisible<Terms.installmentFreq>(cache, term, hasMultipleInst);
			PXUIFieldAttribute.SetVisible<Terms.installmentMthd>(cache, term, hasMultipleInst);
#endif
			PXUIFieldAttribute.SetEnabled<Terms.installmentCntr>(cache, term, hasMultipleInst && !hasCustomSchedule);
			PXUIFieldAttribute.SetEnabled<Terms.installmentFreq>(cache, term, hasMultipleInst && !hasCustomSchedule);
			PXUIFieldAttribute.SetEnabled<Terms.installmentMthd>(cache, term, hasMultipleInst);


			bool isInstScheduleEnabled = (hasMultipleInst && hasCustomSchedule);
			PXUIFieldAttribute.SetEnabled(this.Installments.Cache, null, isInstScheduleEnabled);
			this.Installments.Cache.AllowInsert = isInstScheduleEnabled;
			this.Installments.Cache.AllowUpdate = isInstScheduleEnabled;


			bool hasCustomDueDay = (term.DueType == TermsDueType.Custom);
			bool isEndOfNextMonth = (term.DueType == TermsDueType.EndOfNextMonth);
			bool isEndOfMonth = (term.DueType == TermsDueType.EndOfMonth);
			PXUIFieldAttribute.SetEnabled<Terms.dayDue00>(cache, term, hasCustomDueDay || (!(isEndOfMonth || isEndOfNextMonth)));
			PXUIFieldAttribute.SetEnabled<Terms.dayFrom00>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetEnabled<Terms.dayDue01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetEnabled<Terms.dayFrom01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetEnabled<Terms.dayTo01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetEnabled<Terms.dayTo00>(cache, term, hasCustomDueDay);


#if (!ASP_NET_VISIBLE_BUG_WRKAROUND)
			PXUIFieldAttribute.SetVisible<Terms.dayFrom00>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetVisible<Terms.dayDue01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetVisible<Terms.dayFrom01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetVisible<Terms.dayTo01>(cache, term, hasCustomDueDay);
			PXUIFieldAttribute.SetVisible<Terms.dayTo00>(cache, term, hasCustomDueDay);
#endif
			bool isDiscEOM = (term.DiscType == TermsDueType.EndOfMonth);
			bool isDiscEONM = (term.DiscType == TermsDueType.EndOfNextMonth);
			//bool isDiscDOM = (term.DiscType == TermsDueType.DayOfTheMonth);
            bool isFixDayNbr = (term.DueType == TermsDueType.FixedNumberOfDays) || (term.DueType == TermsDueType.Prox); 
            PXUIFieldAttribute.SetEnabled<Terms.discPercent>(cache, term, !hasMultipleInst && !isEndOfMonth);
            PXUIFieldAttribute.SetEnabled<Terms.discType>(cache, term, !hasMultipleInst && !isEndOfMonth && !isFixDayNbr);
            PXUIFieldAttribute.SetEnabled<Terms.dayDisc>(cache, term, !hasMultipleInst && !(isEndOfMonth || isDiscEOM || isDiscEONM));

#if (!ASP_NET_VISIBLE_BUG_WRKAROUND)
			PXUIFieldAttribute.SetVisible<Terms.discPercent>(cache, term, !isEndOfMonth);
			PXUIFieldAttribute.SetVisible<Terms.discType>(cache, term, !isEndOfMonth);
			PXUIFieldAttribute.SetVisible<Terms.dayDisc>(cache, term, !isEndOfMonth);
#endif


		}

		protected virtual void Terms_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if ((e.Operation & PXDBOperation.Command) != PXDBOperation.Delete)
			{

				if (term.DueType == TermsDueType.DayOfNextMonth || term.DueType == TermsDueType.DayOfTheMonth)
				{
					if (term.DayDue00.Value <= 0 || term.DayDue00.Value > 31)
					{
						cache.RaiseExceptionHandling<Terms.dayDue00>(e.Row, term.DayDue00.Value, new PXSetPropertyException(Messages.ValueNotInRange));
						return;
					}
				}

				if (term.DueType == TermsDueType.Custom)
				{
					if (term.DayFrom00.Value > term.DayTo00.Value)
					{
						cache.RaiseExceptionHandling<Terms.dayTo00>(e.Row, term.DayTo00.Value, new PXSetPropertyException(Messages.DayToDayFrom));
						cache.RaiseExceptionHandling<Terms.dayFrom00>(e.Row, term.DayFrom00.Value, new PXSetPropertyException(Messages.DayToDayFrom));
						return;
					}

					if (term.DayFrom01.Value > term.DayTo01.Value)
					{
						cache.RaiseExceptionHandling<Terms.dayTo01>(e.Row, term.DayTo01.Value, new PXSetPropertyException(Messages.DayToDayFrom));
						cache.RaiseExceptionHandling<Terms.dayFrom01>(e.Row, term.DayFrom01.Value, new PXSetPropertyException(Messages.DayToDayFrom));
						return;
					}
				}

				if (term.DiscType == TermsDueType.DayOfNextMonth || term.DueType == TermsDueType.DayOfTheMonth)
				{
					if (term.DayDisc.Value <= 0 || term.DayDisc.Value > 31)
					{
						cache.RaiseExceptionHandling<Terms.dayDisc>(e.Row, term.DayDisc.Value, new PXSetPropertyException(Messages.ValueNotInRange));
						return;
					}
				}


				if ((term.DiscType == TermsDueType.DayOfNextMonth || term.DiscType == TermsDueType.FixedNumberOfDays || term.DueType == TermsDueType.DayOfTheMonth) && term.DueType == term.DiscType)
				{
					if (term.DayDisc.Value > term.DayDue00.Value)
					{
						cache.RaiseExceptionHandling<Terms.dayDisc>(e.Row, term.DayDisc.Value, new PXSetPropertyException(Messages.ValueMustBeLessEqualDueDay));
						cache.RaiseExceptionHandling<Terms.dayDue00>(e.Row, term.DayDue00.Value, new PXSetPropertyException(Messages.ValueMustBeGreaterEqualDiscDay));
						return;
					}
				}

				if (term.InstallmentMthd == TermsInstallmentMethod.SplitByPercents)
				{
					decimal totalPercentage = 0.0m;
					int count = 0;
					foreach (TermsInstallments it in this.Installments.Select())
					{
						count++;
						if (it.InstPercent.HasValue)
							totalPercentage += it.InstPercent.Value;
					}

					if (term.InstallmentCntr != count)
					{
						throw new PXException(Messages.NumberOfInstallmentsDoesntMatch);
					}

					if (Math.Abs(totalPercentage - 100.0m) > 0.01m)
					{
						throw new PXException(Messages.SumOfInstallmentsMustBe100);
					}
				}

				if (term.InstallmentType == TermsInstallmentType.Multiple)
				{
					if (!term.InstallmentCntr.HasValue || term.InstallmentCntr <= 0)
					{
						cache.RaiseExceptionHandling<Terms.installmentCntr>(e.Row, term.InstallmentCntr.Value, new PXSetPropertyException(Messages.NumberOfInstalments0));
					}
				}
			}
		}

		protected virtual void Terms_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
		{
			this.isMassDelete = true; //Prevent normal validation logic for installments
		}

		protected virtual void Terms_InstallmentType_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if ((term.InstallmentType != (string)e.OldValue)
					&& (term.InstallmentType != TermsInstallmentType.Multiple))
			{
				term.InstallmentCntr = 0;
				term.InstallmentFreq = TermsInstallmentFrequency.Weekly;
				term.InstallmentMthd = TermsInstallmentMethod.EqualParts;
				this.DeleteInstallments();
			}
		}

		protected virtual void Terms_InstallmentMthd_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if ((term.InstallmentMthd != (string)e.OldValue))
			{
				if ((string)e.OldValue == TermsInstallmentMethod.SplitByPercents)
				{
					this.DeleteInstallments();
				}
				if (term.InstallmentMthd == TermsInstallmentMethod.SplitByPercents)
				{
					term.InstallmentCntr = 0;
				}
			}
		}

		protected virtual void Terms_DiscType_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if ((string)e.OldValue != term.DiscType)
			{
				if (term.DiscType == TermsDueType.EndOfMonth || term.DiscType == TermsDueType.EndOfNextMonth)
				{
					term.DayDisc = 0;
				}
			}
		}

		protected virtual void Terms_DiscType_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if (TermsDueType.EndOfNextMonth == term.DueType)
			{
				if (TermsDueType.FixedNumberOfDays == (string)e.NewValue || TermsDueType.Prox == (string)e.NewValue)
				{
					throw new PXSetPropertyException(Messages.OptionCantBeSelected);
				}
			}

			if (TermsDueType.FixedNumberOfDays == term.DueType || TermsDueType.Prox == term.DueType)
			{
				if (TermsDueType.FixedNumberOfDays != (string)e.NewValue && TermsDueType.Prox != (string)e.NewValue)
				{
					throw new PXSetPropertyException(Messages.OptionFixedNumberOfDays);
				}
			}

			if (TermsDueType.DayOfNextMonth == term.DueType)
			{
				if (TermsDueType.EndOfMonth != (string)e.NewValue
					&& TermsDueType.DayOfNextMonth != (string)e.NewValue
                    && TermsDueType.DayOfTheMonth != (string)e.NewValue)
				{
					throw new PXSetPropertyException(Messages.OptionEndOfMonth);
				}
			}

			if (TermsDueType.DayOfTheMonth == term.DueType)
			{
				if (TermsDueType.DayOfTheMonth != (string)e.NewValue)
				{
					throw new PXSetPropertyException(Messages.OptionDayOfTheMonth);
				}
			}
		}

		protected virtual void Terms_DueType_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			Terms term = (Terms)e.Row;
			if (term.DueType != (string)e.OldValue)
			{
				if ((string)e.OldValue == TermsDueType.Custom)
				{
					term.DayDue01 = 0;
					term.DayFrom00 = 0;
					term.DayFrom01 = 0;
					term.DayTo00 = 0;
					term.DayTo01 = 0;
				}
				term.DayDue00 = 0; //Reset in all cases - value has different meaning for all the options
				if (term.DueType == TermsDueType.EndOfMonth)
				{
					term.DayDisc = 0;
					term.DiscType = TermsDueType.EndOfMonth;
					term.DiscPercent = 0.0m;
				}

				if (term.DueType == TermsDueType.EndOfNextMonth)
				{
					term.DayDisc = 1;
					term.DiscType = TermsDueType.DayOfNextMonth;
					term.DiscPercent = 0.0m;
				}

				if (term.DueType == TermsDueType.FixedNumberOfDays || term.DueType == TermsDueType.Prox ||
					  term.DueType == TermsDueType.DayOfNextMonth || term.DueType == TermsDueType.DayOfTheMonth)
				{
					term.DayDisc = term.DayDue00;
					term.DiscType = term.DueType;
					term.DiscPercent = 0.0m;
				}

			}
		}

		
		#endregion

		#region Terms Installments Events
		protected virtual void TermsInstallments_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
		{
			TermsInstallments record = (TermsInstallments)e.Row;
			short lastID = 0;
			if (!record.InstallmentNbr.HasValue || record.InstallmentNbr.Value == 0)
			{
				//record.InstPercent = 100;
				foreach (TermsInstallments it in this.Installments.Select())
				{
					if (object.ReferenceEquals(it, record)) continue;
					if (it.InstallmentNbr.HasValue && it.InstallmentNbr.Value > lastID)
						lastID = it.InstallmentNbr.Value;
					//record.InstPercent -= it.InstPercent;
				}
				lastID++; //IncrementID
				record.InstallmentNbr = lastID;
				this.justInserted = record.InstallmentNbr;
				/*if (record.InstPercent <= 0)
				{
					throw new PXException(Messages.ListOfInstallmentsComplete);
				}*/
			}
			else
				this.justInserted = null;
		}

		protected virtual void TermsInstallments_InstPercent_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			TermsInstallments record = (TermsInstallments)e.Row;
			Decimal? percent = 100;
			foreach (TermsInstallments it in this.Installments.Select())
			{
				if (object.ReferenceEquals(it, record)) continue;
				percent -= it.InstPercent;
			}
			record.InstPercent = percent;
		}

		protected virtual void TermsInstallments_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
		{
			Terms term = this.TermsDef.Current;
			term.InstallmentCntr++;
			this.TermsDef.Cache.Update(term);
		}

		protected virtual void TermsInstallments_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
		{
			if (PXEntryStatus.Notchanged == this.TermsDef.Cache.GetStatus(this.TermsDef.Current))
			{
				//Set State  of the Terms To Modified - for the validation
				this.TermsDef.Cache.SetStatus(this.TermsDef.Current, PXEntryStatus.Updated);
			}
		}

		protected virtual void TermsInstallments_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
		{
			TermsInstallments row = (TermsInstallments)e.Row;
			if (!this.isMassDelete)
			{
				Terms term = this.TermsDef.Current;
				term.InstallmentCntr--;
				if (!(this.justInserted.HasValue && this.justInserted.Value == row.InstallmentNbr))
				{
					this.distributePercentOf(row);
					this.Installments.View.RequestRefresh();
				}
				this.TermsDef.Cache.Update(term);
			}
		} 
		#endregion

		private int? justInserted;
		private bool isMassDelete;
		#region Private Functions
		private bool DeleteInstallments()
		{
			isMassDelete = true;
			try
			{
				///Add code to remove all the rows from this.Periods here
				foreach (TermsInstallments it in this.Installments.Select())
				{
					this.Installments.Cache.Delete(it);
				}
			}
			finally { isMassDelete = false; };
			this.TermsDef.Current.InstallmentCntr = 0;	
			return true;
		}

		private void distributePercentOf(TermsInstallments row)
		{
			if (row != null && row.InstPercent.HasValue && row.InstPercent != null)
			{
				//Find last
				TermsInstallments lastRow = null;
				foreach (TermsInstallments it in this.Installments.Select())
				{
					if (object.ReferenceEquals(it, row)) continue;
					if (lastRow == null || (it.InstallmentNbr.HasValue && it.InstallmentNbr.Value > lastRow.InstallmentNbr))
					{
						lastRow = it;
					}
				}
				if (lastRow != null)
				{
					lastRow.InstPercent += row.InstPercent;
					row.InstPercent = 0;
					this.Installments.Cache.Update(lastRow);
				}
			}
		}
		#endregion
	
	}


}
