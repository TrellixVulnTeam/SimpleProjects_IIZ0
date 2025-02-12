using System;
using System.Collections;
using System.Collections.Generic;
using PX.Objects.CM;
using PX.SM;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.AP;


namespace PX.Objects.EP
{
    [System.SerializableAttribute()]
    public partial class MyExpenseClaimsEnqFilter : PX.Data.IBqlTable
    {
        #region StartDate
        public abstract class startDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _StartDate;
        [PXDBDate()]
        [PXUIField(DisplayName = "Start Date", Visibility = PXUIVisibility.Visible)]
        public virtual DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }
        #endregion
        #region EndDate
        public abstract class endDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _EndDate;
        [PXDBDate()]
        [PXUIField(DisplayName = "End Date", Visibility = PXUIVisibility.Visible)]
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
        #region OnHold
        public abstract class onHold : PX.Data.IBqlField
        {
        }
        protected bool? _OnHold;
        [PXDefault(true)]
        [PXDBBool()]
        [PXUIField(DisplayName = "On Hold", Visibility = PXUIVisibility.Visible)]
        public virtual bool? OnHold
        {
            get
            {
                return this._OnHold;
            }
            set
            {
                this._OnHold = value;
            }
        }
        #endregion
        #region Pending
        public abstract class pending : PX.Data.IBqlField
        {
        }
        protected bool? _Pending;
        [PXDefault(true)]
        [PXDBBool()]
        [PXUIField(DisplayName = "Pending Approval", Visibility = PXUIVisibility.Visible)]
        public virtual bool? Pending
        {
            get
            {
                return this._Pending;
            }
            set
            {
                this._Pending = value;
            }
        }
        #endregion
        #region Approved
        public abstract class approved : PX.Data.IBqlField
        {
        }
        protected bool? _Approved;
        [PXDBBool()]
        [PXUIField(DisplayName = "Approved", Visibility = PXUIVisibility.Visible)]
        public virtual bool? Approved
        {
            get
            {
                return this._Approved;
            }
            set
            {
                this._Approved = value;
            }
        }
        #endregion
        #region Released
        public abstract class released : PX.Data.IBqlField
        {
        }
        protected bool? _Released;
        [PXDBBool()]
        [PXUIField(DisplayName = "Released", Visibility = PXUIVisibility.Visible)]
        public virtual bool? Released
        {
            get
            {
                return this._Released;
            }
            set
            {
                this._Released = value;
            }
        }
        #endregion
        #region Voided
        public abstract class voided : PX.Data.IBqlField
        {
        }
        protected bool? _Voided;
        [PXDBBool()]
        [PXUIField(DisplayName = "Rejected", Visibility = PXUIVisibility.Visible)]
        public virtual bool? Voided
        {
            get
            {
                return this._Voided;
            }
            set
            {
                this._Voided = value;
            }
        }
        #endregion
    }

    public class MyExpenseClaimsEnq : PXGraph<MyExpenseClaimsEnq>
    {
        public PXCancel<MyExpenseClaimsEnqFilter> cancel;
        public PXFilter<MyExpenseClaimsEnqFilter> Filter;

        [PXFilterable]
        public PXSelectJoin<EPExpenseClaim, LeftJoin<APInvoice, On<APInvoice.refNbr, Equal<EPExpenseClaim.aPRefNbr>>>> ExpenseClaimRecords;

        public PXSelect<APInvoice> Invoices; 

        public PXSetup<EPSetup> EPSetup;
		
		protected virtual IEnumerable expenseClaimRecords()
		{
            MyExpenseClaimsEnqFilter filter = Filter.Current as MyExpenseClaimsEnqFilter;

			PXSelectBase<EPExpenseClaim> select =
				new PXSelectJoin<EPExpenseClaim,
                    LeftJoin<APInvoice, On<APInvoice.refNbr, Equal<EPExpenseClaim.aPRefNbr>>, 
					InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaim.employeeID>>>>,
					Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>,
					OrderBy<Desc<EPExpenseClaim.docDate,
						Asc<EPExpenseClaim.refNbr>>>>(this);

			if(filter.StartDate.HasValue)
				select.WhereAnd<Where<EPExpenseClaim.docDate,GreaterEqual<Current<MyExpenseClaimsEnqFilter.startDate>>>>();
			if(filter.EndDate.HasValue)
                select.WhereAnd<Where<EPExpenseClaim.docDate, LessEqual<Current<MyExpenseClaimsEnqFilter.endDate>>>>();

			foreach (PXResult<EPExpenseClaim, APInvoice> res in select.Select())
			{
			    EPExpenseClaim claim = (EPExpenseClaim) res;
				if ((((filter.Released??false) && claim.Status == EPClaimStatus.Released) ||
                    ((filter.Approved ?? false) && claim.Status == EPClaimStatus.Approved) ||
                    ((filter.Pending ?? false) && claim.Status == EPClaimStatus.Balanced) ||
                    ((filter.Voided ?? false) && claim.Status == EPClaimStatus.Voided) ||
                    ((filter.OnHold ?? false) && claim.Status == EPClaimStatus.Hold)))
				{
					yield return res;
				}
			}
		}

        public PXAction<MyExpenseClaimsEnqFilter> claimDetails;
		[PXUIField(DisplayName = Messages.ClaimDetails, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.DataEntry)]
		public virtual IEnumerable ClaimDetails(PXAdapter adapter)
		{
			if (ExpenseClaimRecords.Current != null && Filter.Current != null)
			{
				ExpenseClaimEntry graph = PXGraph.CreateInstance<ExpenseClaimEntry>();
				graph.ExpenseClaim.Current = ExpenseClaimRecords.Current;
				throw new PXRedirectRequiredException(graph, true, Messages.ClaimDetails){Mode = PXBaseRedirectException.WindowMode.NewWindow};
			}
			return adapter.Get();
		}

        [PXString(1, IsFixed = true)]
        [PXUIField(DisplayName = "AP Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [APDocStatus.List()]
        protected virtual void APInvoice_Status_CacheAttached(PXCache sender)
        {
        }

        [PXDBCurrency(typeof(APRegister.curyInfoID), typeof(APRegister.origDocAmt))]
        [PXUIField(DisplayName = "AP Amount", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        protected virtual void APInvoice_CuryOrigDocAmt_CacheAttached(PXCache sender)
        {
        }
    }
}
