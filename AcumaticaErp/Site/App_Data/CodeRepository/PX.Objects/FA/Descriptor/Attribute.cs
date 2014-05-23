using System;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CS;

namespace PX.Objects.FA
{
    public class PXHashSet<T> : HashSet<T>
        where T : class, IBqlTable 
    {
        public PXHashSet(PXGraph graph)
            : base(new Comparer<T>(graph))
        { 
        }

        public List<T> ToList()
        {
            return new List<T>(this);
        }

        public class Comparer<TT> : IEqualityComparer<TT>
            where TT : T
        {
            protected PXCache _cache;
            public Comparer(PXGraph graph)
            {
                _cache = graph.Caches[typeof(TT)];
            }

            public bool Equals(TT a, TT b)
            {
                return _cache.ObjectsEqual(a, b);
            }

            public int GetHashCode(TT a)
            {
                return _cache.GetObjectHashCode(a);
            }
        }
    }

    public sealed class RowExt<Field> : IBqlOperand, IBqlCreator
    where Field : IBqlField
    {
        public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
        {
            if (fields != null)
            {
                fields.Add(typeof(Field));
            }
        }
        public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
        {
            object fs;
            value = (fs = cache.GetValueExt(item, typeof(Field).Name)) is PXFieldState ? ((PXFieldState)fs).Value : fs;
        }
    }

	#region PercentDBDecimalAttribute
	public class PercentDBDecimalAttribute : PXDBDecimalAttribute
	{
	    protected decimal _Factor = 100m;
        protected int? _RoundPrecision;
        protected Rounding _RoundType = Rounding.Round;

        public enum Rounding
        {
            Round,
            Truncate
        }

	    public double Factor
	    {
	        get { return (double)_Factor; }
            set { _Factor = (decimal)value; }
	    }

        public int RoundPrecision
	    {
            get { return _RoundPrecision ?? 4; }
            set { _RoundPrecision = value; }
	    }

	    public Rounding RoundType
	    {
	        get { return _RoundType; }
            set { _RoundType = value; }
	    }

		public PercentDBDecimalAttribute()
			: base(4)
		{
			MinValue = -99999.0;
			MaxValue = 99999.0;
		}
        public PercentDBDecimalAttribute(int precision)
            : base(precision)
        {
            MinValue = -99999.0;
            MaxValue = 99999.0;
        }
        public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
		{
			base.FieldUpdating(sender, e);
			if (e.NewValue != null)
			{
			    switch (RoundType)
			    {
			        case Rounding.Round:
                        e.NewValue = ((decimal)e.NewValue) / _Factor;
                        if(_RoundPrecision != null)
                        {
                            e.NewValue = Math.Round((decimal)e.NewValue, (int)_RoundPrecision);
                        }
                        break;
                    case Rounding.Truncate:
			            int precDigs = _RoundPrecision ?? 4;
                        decimal prec = (decimal)Math.Pow(10, precDigs);
                        e.NewValue = Math.Truncate(prec*((decimal)e.NewValue) / _Factor)/prec;
                        break;
			    }
			}
		}

		public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
		{
			base.FieldSelecting(sender, e);
			if (e.ReturnValue != null && (!(e.ReturnState is PXFieldState) || String.IsNullOrEmpty(((PXFieldState)e.ReturnState).Error)))
			{
                switch (RoundType)
                {
                    case Rounding.Round:
                        e.ReturnValue = ((decimal)e.ReturnValue) * _Factor;
                        break;
                    case Rounding.Truncate:
                        decimal prec = (decimal)Math.Pow(10, _Precision ?? 4);
                        e.ReturnValue = Math.Ceiling(prec * ((decimal)e.ReturnValue) * _Factor) / prec;
                        break;
                }
			}
		}
	}
	#endregion
	
	#region PercentTotalDBDecimalAttribute
	public class PercentTotalDBDecimalAttribute : PercentDBDecimalAttribute
	{
		#region state
		protected Type _MapErrorTo;
		protected PXPersistingCheck _PersistingCheck = PXPersistingCheck.Null;
		public virtual PXPersistingCheck PersistingCheck
		{
			get
			{
				return _PersistingCheck;
			}
			set
			{
				_PersistingCheck = value;
			}
		}
		public virtual Type MapErrorTo
		{
			get
			{
				return _MapErrorTo;
			}
			set
			{
				_MapErrorTo = value;
			}
		}
		#endregion

		public PercentTotalDBDecimalAttribute()
		{
			MinValue = 0.0;
			MaxValue = 99999.0;
		}
		public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			object val;
			if (_PersistingCheck != PXPersistingCheck.Nothing &&
				((e.Operation & PXDBOperation.Command) == PXDBOperation.Insert ||
				(e.Operation & PXDBOperation.Command) == PXDBOperation.Update) &&
				(val = sender.GetValue(e.Row, _FieldOrdinal)) != null && (decimal)val != 1m)
			{
				val = (decimal)val * 100m;
				if (_MapErrorTo == null)
				{
					if (sender.RaiseExceptionHandling(_FieldName, e.Row, val, new PXSetPropertyException(PXMessages.LocalizeFormat(Messages.WrongValue, _FieldName))))
					{
						throw new PXRowPersistingException(_FieldName, null, Messages.WrongValue, _FieldName);
					}
				}
				else
				{
					string name = _MapErrorTo.Name;
					name = char.ToUpper(name[0]) + name.Substring(1);
					val = sender.GetValueExt(e.Row, name);
					if (val is PXFieldState)
					{
						val = ((PXFieldState)val).Value;
					}
					if (sender.RaiseExceptionHandling(name, e.Row, val, new PXSetPropertyException(PXMessages.LocalizeFormat(Messages.WrongValue, name, _FieldName))))
					{
						throw new PXRowPersistingException(_FieldName, null, Messages.WrongValue, _FieldName);
					}
				}
			}
		}
	}
	#endregion

	#region FABookPeriodIDAttribute

	public class PXFABookPeriodException : PXException
	{
		public PXFABookPeriodException(): base(Messages.NoPeriodsDefined){}

		public PXFABookPeriodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		
		}

	}

	public class PXFABookCalendarException : PXException
	{
		public PXFABookCalendarException(): base(Messages.NoCalendarDefined){}

		public PXFABookCalendarException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		
		}
	}

	public class FABookPeriodIDAttribute : PeriodIDAttribute, IPXFieldVerifyingSubscriber
	{
		protected int _SelAttrIndex = -1;
		#region Ctor
		public FABookPeriodIDAttribute()
			:base()
		{
		}

		public FABookPeriodIDAttribute(Type BookType)
			: this(BookType, null)
		{
		}

		public FABookPeriodIDAttribute(Type BookType, Type SourceType)
			: base(SourceType, BqlCommand.Compose(
				typeof(Search<,>),
						typeof(FABookPeriod.finPeriodID),
						typeof(Where<,,>),
						typeof(FABookPeriod.bookID),
						typeof(Equal<>),
						typeof(Current<>),
						BookType,
						typeof(And<FABookPeriod.startDate, LessEqual<Required<FABookPeriod.startDate>>, And<FABookPeriod.endDate, Greater<Required<FABookPeriod.endDate>>>>))
				)
		{
			if (BookType != null)
			{
				Type search = BqlCommand.Compose(
					typeof(Search<,,>),
					typeof(FABookPeriod.finPeriodID),
					typeof(Where<,>),
					typeof(FABookPeriod.bookID),
					typeof(Equal<>),
					typeof(Optional<>),
					BookType,
					typeof(OrderBy<Asc<FABookPeriod.finPeriodID>>));

				_Attributes.Add(new PXSelectorAttribute(search));
				_SelAttrIndex = _Attributes.Count - 1;
			}
		}
		#endregion

		#region Initialization
		public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
		{
			if (typeof(ISubscriber) == typeof(IPXFieldVerifyingSubscriber))
			{
				subscribers.Add(this as ISubscriber);
			}
			else
			{
				base.GetSubscriber<ISubscriber>(subscribers);
			}
		}
		#endregion

		#region Implementation
		public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			try
			{
				if (_SelAttrIndex != -1)
					((IPXFieldVerifyingSubscriber)_Attributes[_SelAttrIndex]).FieldVerifying(sender, e);
			}
			catch (PXSetPropertyException)
			{
				e.NewValue = FormatPeriod((string)e.NewValue);
				throw;
			}
		}

        public static string FormatForError(string period)
        {
            return FinPeriodIDFormattingAttribute.FormatForError(period);
        }

		public static string FormatPeriod(string period)
		{
			return FinPeriodIDFormattingAttribute.FormatForDisplay(period);
		}

		public static string UnFormatPeriod(string period)
		{
			return FinPeriodIDFormattingAttribute.FormatForStoring(period);
		}
		#endregion

		#region Period Manipulation

		public static FABookPeriod FABookPeriodFromDate(PXGraph graph, DateTime? d, int? BookID)
		{
			FABookPeriod p = PXSelect<FABookPeriod, 
								Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, 
								And<FABookPeriod.startDate, LessEqual<Required<FABookPeriod.startDate>>, 
								And<FABookPeriod.endDate, Greater<Required<FABookPeriod.endDate>>>>>>.Select(graph, BookID, d, d);

			if (p == null)
			{
				throw new PXFABookPeriodException();
			}
			return p;
		}

		public static string PeriodFromDate(PXGraph graph, DateTime? d, int? BookID)
		{
			return FABookPeriodFromDate(graph, d, BookID ?? 0).FinPeriodID;
		}

		public static short QuarterNumFromDate(PXGraph graph, DateTime? d, int? BookID)
		{
			decimal PeriodNbr = Convert.ToDecimal(FABookPeriodFromDate(graph, d, BookID).PeriodNbr);
			return (short)Decimal.Ceiling(PeriodNbr/3);
		}

		public static short PeriodNumFromDate(PXGraph graph, DateTime? d, int? BookID)
		{
			return Convert.ToInt16(FABookPeriodFromDate(graph, d, BookID).PeriodNbr);
		}

		public static IYearSetup GetBookCalendar(PXGraph graph, int? BookID)
		{
			FABook book = PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Select(graph, BookID);
			if (book.UpdateGL ?? false)
				return (FinYearSetup)PXSelect<FinYearSetup>.Select(graph);
			else
				return (FABookYearSetup)PXSelect<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Required<FABookYearSetup.bookID>>>>.Select(graph, BookID);
		}

		public static int GetBookPeriodsInYear(PXGraph graph, int? BookID)
		{
			FABook book = PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Select(graph, BookID);
			IPeriodSetup periodsInYear;
			if (book.UpdateGL ?? false)
			{
				periodsInYear = (FinPeriodSetup)PXSelectGroupBy<FinPeriodSetup,
													Where<FinPeriodSetup.endDate, Greater<FinPeriodSetup.startDate>>,
													Aggregate<Max<FinPeriodSetup.periodNbr>>>.Select(graph);
			}
			else
			{
				periodsInYear = (FABookPeriodSetup)PXSelectGroupBy<FABookPeriodSetup,
																Where<FABookPeriodSetup.endDate, Greater<FABookPeriodSetup.startDate>,
																	And<FABookPeriodSetup.bookID, Equal<Required<FABookPeriodSetup.bookID>>>>,
																Aggregate<Max<FABookPeriodSetup.periodNbr>>>.Select(graph, BookID);
			}
			if (periodsInYear == null || periodsInYear.PeriodNbr == null) throw new PXFABookCalendarException();
			return Convert.ToInt32(periodsInYear.PeriodNbr);
		}

        public static int? PeriodMinusPeriod(PXGraph graph, string FiscalPeriodID1, string FiscalPeriodID2, int? BookID)
        {
            int count = PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<Where<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>, Or<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>>.Select(graph, BookID, FiscalPeriodID1, FiscalPeriodID2).Count;
            if (count < 2 && string.Equals(FiscalPeriodID1, FiscalPeriodID2) == false)
            {
                throw new PXException(Messages.NoCalendarDefined);
            }

            PXResult res = PXSelectGroupBy<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.endDate, Greater<FABookPeriod.startDate>>>>>, Aggregate<GroupBy<FABookPeriod.bookID, Count>>>.Select(graph, BookID, FiscalPeriodID1, FiscalPeriodID2);
            return res != null ? res.RowCount : null;
        }

		public static string PeriodPlusPeriod(PXGraph graph, string FiscalPeriodID, int counter, int? BookID)
		{
		    FABook book = PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Select(graph, BookID);
            IYearSetup setup;
            if(book.UpdateGL == true)
            {
                setup = (FinYearSetup)PXSelect<FinYearSetup>.Select(graph);
            }
            else
            {
                setup = (FABookYearSetup)PXSelect<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Required<FABookYearSetup.bookID>>>>.Select(graph, BookID);
            }

            IPeriodSetup periodsInYear;
            if (book.UpdateGL == true)
            {
                periodsInYear = (FinPeriodSetup)PXSelectGroupBy<FinPeriodSetup, Where<FinPeriodSetup.endDate, Greater<FinPeriodSetup.startDate>>,
                                                                    Aggregate<Max<FinPeriodSetup.periodNbr>>>.Select(graph);
            }
            else
            {
                periodsInYear = (FABookPeriodSetup)PXSelectGroupBy<FABookPeriodSetup, Where<FABookPeriodSetup.endDate, Greater<FABookPeriodSetup.startDate>, And<FABookPeriodSetup.bookID, Equal<Required<FABookPeriodSetup.bookID>>>>,
                                                                    Aggregate<Max<FABookPeriodSetup.periodNbr>>>.Select(graph, BookID);
            }

            if (setup != null && FiscalPeriodSetupCreator.IsFixedLengthPeriod(setup.FPType) &&
                periodsInYear != null && periodsInYear.PeriodNbr != null)
            {
                return OffsetPeriod(graph, FiscalPeriodID, counter, Convert.ToInt32(periodsInYear.PeriodNbr));
            }
            else if (counter > 0)
            {
                PXResultset<FABookPeriod> res = PXSelect<FABookPeriod, Where<FABookPeriod.finPeriodID, Greater<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>.SelectWindowed(graph, 0, counter, FiscalPeriodID, BookID);

                if (res.Count < counter)
                {
                    throw new PXFABookPeriodException();
                }

                return ((FABookPeriod)res[res.Count - 1]).FinPeriodID;
            }
            else if (counter < 0)
            {
                PXResultset<FABookPeriod> res = PXSelect<FABookPeriod, Where<FABookPeriod.finPeriodID, Less<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>>>>, OrderBy<Desc<FABookPeriod.finPeriodID>>>.SelectWindowed(graph, 0, -counter, FiscalPeriodID, BookID);

                if (res.Count < -counter)
                {
                    throw new PXFABookPeriodException();
                }

                return ((FABookPeriod)res[res.Count - 1]).FinPeriodID;
            }
            else
            {
                return FiscalPeriodID;
            }
        }

		public static string NextPeriod(PXGraph graph, string FiscalPeriodID, int? BookID)
		{
			return PeriodPlusPeriod(graph, FiscalPeriodID, 1, BookID);
		}

		public static DateTime PeriodStartDate(PXGraph graph, string FiscalPeriodID, int? BookID)
		{
			FABookPeriod p = PXSelect<FABookPeriod, 
								Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>,
								And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>.Select(graph, BookID, FiscalPeriodID);

			if (p == null || p.StartDate == null)
			{
				throw new PXFABookPeriodException();
			}

			return (DateTime)p.StartDate;
		}

		public static DateTime PeriodEndDate(PXGraph graph, string FiscalPeriodID, int? BookID)
		{
			FABookPeriod p = PXSelect<FABookPeriod,
								Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, 
								And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>.Select(graph, BookID, FiscalPeriodID);

			if (p == null || p.EndDate == null)
			{
				throw new PXFABookPeriodException();
			}

			return ((DateTime)p.EndDate).AddDays(-1);
		}

		#endregion
	}
	#endregion

	#region SubAccountMaskAttribute
	public class FAAcctSubDefault
	{
		public class CustomListAttribute : PXStringListAttribute
		{
			public string[] AllowedValues
			{
				get
				{
					return _AllowedValues;
				}
			}

			public string[] AllowedLabels
			{
				get
				{
					return _AllowedLabels;
				}
			}

			public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
				: base(AllowedValues, AllowedLabels)
			{
			}
		}
		public class ClassListAttribute : CustomListAttribute
		{
			public ClassListAttribute()
				: base(new [] { MaskAsset, MaskLocation, MaskDepartment, MaskClass },
                        new[] { Messages.MaskAsset, Messages.MaskLocation, Messages.MaskDepartment, Messages.MaskClass }) { }
		}

	    public const string MaskAsset = "A";
        public const string MaskLocation = "L";
		public const string MaskDepartment = "D";
		public const string MaskClass = "C";
	}


	[PXDBString(30, IsUnicode = true, InputMask = "")]
	[PXUIField(DisplayName = "Subaccount Mask", Visibility = PXUIVisibility.Visible, FieldClass=_DimensionName)]
	public sealed class SubAccountMaskAttribute : AcctSubAttribute
	{
		private const string _DimensionName = "SUBACCOUNT";
		private const string _MaskName = "FASETUP";
		public SubAccountMaskAttribute()
		{
			PXDimensionMaskAttribute attr = new PXDimensionMaskAttribute(_DimensionName, _MaskName, FAAcctSubDefault.MaskClass, new FAAcctSubDefault.ClassListAttribute().AllowedValues, new FAAcctSubDefault.ClassListAttribute().AllowedLabels);
			attr.ValidComboRequired = false;
			_Attributes.Add(attr);
			_SelAttrIndex = _Attributes.Count - 1;
		}

		public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields)
			where Field : IBqlField
		{
			try
			{
				return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new FAAcctSubDefault.ClassListAttribute().AllowedValues, 3, sources);
			}
			catch (PXMaskArgumentException)
			{
                // default source subID is null
                return null;

                //PXCache cache = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
                //string fieldName = fields[ex.SourceIdx].Name;
                //throw new PXMaskArgumentException(new FAAcctSubDefault.ClassListAttribute().AllowedLabels[ex.SourceIdx], PXUIFieldAttribute.GetDisplayName(cache, fieldName));
			}
		}
	}

	#endregion

    #region FAOpenPeriodAttribute
    public class FAOpenPeriodAttribute : OpenPeriodAttribute
    {
        public FAOpenPeriodAttribute()
            :this(null)
        {
        }

        public FAOpenPeriodAttribute(Type SourceType)
            : base(typeof(Search<FinPeriod.finPeriodID, Where<FinPeriod.fAClosed, NotEqual<True>, And<FinPeriod.active, Equal<True>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>>>>>), SourceType)
        {
        }
    }
    #endregion

    #region RecoveryStartPeriod
    public class RecoveryStartPeriod<StartDate, BookID, DepreciationMethodID, AveragingConvention, MidMonthType, MidMonthDay> : BqlFormula<StartDate, BookID, DepreciationMethodID, AveragingConvention, MidMonthType, MidMonthDay>
        where StartDate : IBqlOperand
        where BookID : IBqlOperand
        where DepreciationMethodID : IBqlOperand
        where AveragingConvention : IBqlOperand
        where MidMonthType : IBqlOperand
        where MidMonthDay : IBqlOperand
    {
        public override void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
        {
            int? bookID = (int?)Calculate<BookID>(cache, item);

            try
            {
                string recoveryStartPeriod = DepreciationCalc.GetRecoveryStartPeriod(cache.Graph, (FABookBalance)item);
                value = FABookPeriodIDAttribute.FormatPeriod(recoveryStartPeriod);
            }
            catch (PXException ex)
            {
                throw new PXSetPropertyException(ex.Message, PXErrorLevel.Error);
            }
            catch
            {
                value = null;
            }
        }
    }
    #endregion

    #region OffsetBookDate
    public class OffsetBookDate<BookDate, Offset, BookID, DepreciationMethodID, AveragingConvention> : BqlFormula<BookDate, Offset, BookID, DepreciationMethodID, AveragingConvention>
		where BookDate : IBqlOperand
		where Offset : IBqlOperand
		where BookID : IBqlOperand
		where DepreciationMethodID : IBqlOperand
		where AveragingConvention : IBqlOperand
	{
		public override void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			int? bookID = (int?)Calculate<BookID>(cache, item);

			try
			{
				value = DepreciationCalc.GetRecoveryEndDate(cache.Graph, (FABookBalance)item);
			}
			catch (PXException ex)
			{
				throw new PXSetPropertyException(ex.Message, PXErrorLevel.Error);
			}
			catch
			{
				value = null;
			}
		}
	}
	#endregion

	#region OffsetBookDateToPeriod
	public class OffsetBookDateToPeriod<BookDate, Offset, BookID, DepreciationMethodID, AveragingConvention> : OffsetBookDate<BookDate, Offset, BookID, DepreciationMethodID, AveragingConvention>//, IBqlDefault
		where BookDate : IBqlOperand
		where Offset : IBqlOperand
		where BookID : IBqlOperand
		where DepreciationMethodID : IBqlOperand
		where AveragingConvention : IBqlOperand
	{
		public override void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			base.Verify(cache, item, pars, ref result, ref value);

			if (value == null)
			{
				return;
			}
			DateTime? recoveryEndDate = (DateTime?)value;

			int? bookID = (int?)Calculate<BookID>(cache, item);

			string recoveryEndPeriod = FABookPeriodIDAttribute.PeriodFromDate(cache.Graph, recoveryEndDate, bookID);
			value = FABookPeriodIDAttribute.FormatPeriod(recoveryEndPeriod);
		}
	}
	#endregion

	#region GetBonusRate
	public class GetBonusRate<Date, BonusID> : BqlFormula<Date, BonusID>
		where Date : IBqlOperand
		where BonusID : IBqlOperand
	{
		public override void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			DateTime? date = (DateTime?)Calculate<Date>(cache, item);
			int? bonusID = (int?)Calculate<BonusID>(cache, item);

			if (bonusID == null || date == null)
			{
				value = 0m;
				return;
			}

			FABonusDetails det = PXSelect<FABonusDetails, Where<FABonusDetails.bonusID, Equal<Required<FABonus.bonusID>>,
				And<FABonusDetails.startDate, LessEqual<Required<FABookBalance.deprFromDate>>,
				And<FABonusDetails.endDate, GreaterEqual<Required<FABookBalance.deprFromDate>>>>>>.Select(cache.Graph, bonusID, date, date);

			if (det == null)
			{
				value = 0m;
				return;
			}

			value = det.BonusPercent;
		}

	}
	#endregion

	#region FA Reports
    [Serializable]
	public partial class FABookPeriodSelection : IBqlTable
	{
		#region GLBookCD
		public abstract class gLBookCD : IBqlField
		{
		}
		protected string _GLBookCD;
		[PXString]
		[GLBookDefault]
		public virtual string GLBookCD
		{
			get
			{
				return _GLBookCD;
			}
			set
			{
				_GLBookCD = value;
			}
		}
		#endregion
		#region CurPeriodID
		public abstract class curPeriodID : IBqlField
		{
		}
		protected string _CurPeriodID;
		[PXString]
		[CurrentGLBookPeriodDefault]
		public virtual string CurPeriodID
		{
			get
			{
				return _CurPeriodID;
			}
			set
			{
				_CurPeriodID = value;
			}
		}
		#endregion
	}

	public class GLBookDefaultAttribute : PXDefaultAttribute
	{
		public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			base.FieldDefaulting(sender, e);

			FABook book = PXSelect<FABook, Where<FABook.updateGL, Equal<boolTrue>>>.SelectSingleBound(sender.Graph, new object[0]);

			if (book != null)
			{
				e.NewValue = book.BookCode;
			}
		}
	}

	public class CurrentGLBookPeriodDefaultAttribute : PXDefaultAttribute
	{
		public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			base.FieldDefaulting(sender, e);

			FABookPeriod period = PXSelectJoin<FABookPeriod, LeftJoin<FABook, On<FABookPeriod.bookID, Equal<FABook.bookID>>>,
				Where<FABookPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>, And<FABookPeriod.endDate, Greater<Current<AccessInfo.businessDate>>, And<FABook.updateGL, Equal<boolTrue>>>>>.SelectSingleBound(sender.Graph, new object[0]);

			if (period != null)
			{
				e.NewValue = FinPeriodIDFormattingAttribute.FormatForDisplay(period.FinPeriodID);
			}
		}
	}
	#endregion

}
