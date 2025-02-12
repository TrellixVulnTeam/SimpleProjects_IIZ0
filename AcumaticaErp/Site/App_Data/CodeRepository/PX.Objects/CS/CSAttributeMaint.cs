using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Data;

namespace PX.Objects.CS
{
	public class CSAttributeMaint : PXGraph<CSAttributeMaint, CSAttribute>
	{
		public PXSelect<CSAttribute> Attributes;
		public PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAttribute.attributeID>>>> CurrentAttribute;
		[PXImport(typeof(CSAttribute))]
		public PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<CSAttribute.attributeID>>>> AttributeDetails;

		protected virtual void CSAttribute_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
            if (e.Row == null) return;

            var row = e.Row as CSAttribute;
			SetControlsState(row, sender);

			ValidateAttributeID(sender, row);
		}

		protected virtual void CSAttributeDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			var row = e.Row as CSAttributeDetail;
			if(row != null)
			{
				CSAnswers ans = PXSelect<CSAnswers, 
					Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>, 
						And<CSAnswers.value, Equal<Required<CSAnswers.value>>>>>.
					SelectWindowed(this, 0, 1, row.AttributeID, row.ValueID);
				CSAttributeGroup group = PXSelect<CSAttributeGroup, 
					Where<CSAttributeGroup.attributeID, Equal<Required<CSAttribute.attributeID>>>>.
					SelectWindowed(this, 0, 1, row.AttributeID);
				if (ans != null && group != null)
					e.Cancel = true;
			}
		}

		protected virtual void CSAttributeDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		{
			CSAttributeDetail row = e.Row as CSAttributeDetail;

			if (row != null && CurrentAttribute.Current != null)
			{
				row.AttributeID = CurrentAttribute.Current.AttributeID;
			}
		}

		protected virtual void CSAttribute_ControlType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SetControlsState(e.Row as CSAttribute, sender);
		}

		protected virtual void CSAttribute_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			CSAttribute row = e.Row as CSAttribute;
			if (row != null)
			{
				if ( string.IsNullOrEmpty(row.Description))
				{
					if (sender.RaiseExceptionHandling<CSAttribute.description>(e.Row, row.Description, new PXSetPropertyException(Data.ErrorMessages.FieldIsEmpty, typeof(CSAttribute.description).Name)))
					{
						throw new PXSetPropertyException(typeof(CSAttribute.description).Name, null, Data.ErrorMessages.FieldIsEmpty, typeof(CSAttribute.description).Name);
					}
				}
				if (!ValidateAttributeID(sender, row))
				{
					var displayName = ((PXFieldState)sender.GetStateExt(row, typeof(CSAttribute.attributeID).Name)).DisplayName;
					if (string.IsNullOrEmpty(displayName)) displayName = typeof(CSAttribute.attributeID).Name;
					throw new PXSetPropertyException(
						string.Concat(displayName, ": ", PXUIFieldAttribute.GetError<CSAttribute.attributeID>(sender, row)));
				}
			}
		}

		private static bool ValidateAttributeID(PXCache sender, CSAttribute row)
		{
			if (row == null || string.IsNullOrEmpty(row.AttributeID)) return true;

			if (Char.IsDigit(row.AttributeID[0]))
			{
				PXUIFieldAttribute.SetError<CSAttribute.attributeID>(sender, row, Messages.CannotStartWithDigit);
				return false;
			}

			if (row.AttributeID.Contains(" "))
			{
				PXUIFieldAttribute.SetError<CSAttribute.attributeID>(sender, row, Messages.CannotContainEmptyChars);
				return false;
			}

			return true;
		}

		private void SetControlsState(CSAttribute row, PXCache cache)
		{
			if (row != null)
			{
				AttributeDetails.Cache.AllowDelete = (row.ControlType == CSAttribute.Combo || row.ControlType == CSAttribute.MultiSelectCombo);
				AttributeDetails.Cache.AllowUpdate = (row.ControlType == CSAttribute.Combo || row.ControlType == CSAttribute.MultiSelectCombo);
				AttributeDetails.Cache.AllowInsert = (row.ControlType == CSAttribute.Combo || row.ControlType == CSAttribute.MultiSelectCombo);

				if (cache.GetStatus(row) == PXEntryStatus.Notchanged)
				{
					CSAnswers ans = PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>.SelectWindowed(this, 0, 1, row.AttributeID);
					CSAttributeGroup group = PXSelect<CSAttributeGroup, Where<CSAttributeGroup.attributeID, Equal<Required<CSAttribute.attributeID>>>>.SelectWindowed(this, 0, 1, row.AttributeID);

					bool enabled = (ans == null && group == null);

					PXUIFieldAttribute.SetEnabled<CSAttribute.controlType>(cache, row, enabled);
					cache.AllowDelete = enabled;
				}

				PXUIFieldAttribute.SetEnabled<CSAttribute.entryMask>(cache, row, row.ControlType == CSAttribute.Text);
				PXUIFieldAttribute.SetEnabled<CSAttribute.regExp>(cache, row, row.ControlType == CSAttribute.Text);
			}
		}

		public override void Persist()
		{
			if (Attributes.Current != null)
			{
				string old = Attributes.Current.List;
				Attributes.Current.List = null;
				foreach (CSAttributeDetail det in AttributeDetails.Select())
				{
					if (!String.IsNullOrEmpty(det.ValueID))
					{
						if (Attributes.Current.List == null)
						{
							Attributes.Current.List = det.ValueID + '\0' + det.Description ?? "";
						}
						else
						{
							Attributes.Current.List = Attributes.Current.List + '\t' + det.ValueID + '\0' + det.Description ?? "";
						}
					}
				}
				if (!String.Equals(old, Attributes.Current.List) && Attributes.Cache.GetStatus(Attributes.Current) == PXEntryStatus.Notchanged)
				{
					Attributes.Cache.SetStatus(Attributes.Current, PXEntryStatus.Updated);
				}
			}
			base.Persist();
		}
	}
}
