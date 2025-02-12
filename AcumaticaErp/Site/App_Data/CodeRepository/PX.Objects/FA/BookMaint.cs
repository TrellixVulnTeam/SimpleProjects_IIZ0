using System;
using System.Collections.Generic;
using PX.Data;
using PX.Data.Search;
using PX.Objects.GL;

namespace PX.Objects.FA
{
    public class BookMaint : PXGraph<BookMaint>
    {
        public PXSavePerRow<FABook> Save;
		public PXCancel<FABook> Cancel;
		
		#region Selects Declaration
        public PXSelect<FABook> Book;
		public PXSetup<FASetup> FASetup;
		public PXSetup<GLSetup> GLSetup;
		#endregion

		#region Constructor
		public BookMaint() 
		{
			FASetup setup   = FASetup.Current;
			GLSetup setupGL = GLSetup.Current;
		}

        #endregion

		#region Buttons
		public PXAction<FABook> ShowCalendar;
		[PXUIField(DisplayName = Messages.Calendar, MapEnableRights = PXCacheRights.Select,
					MapViewRights = PXCacheRights.Select)]
		[PXButton]
		protected virtual void showCalendar()
		{
			if(Book.Current.UpdateGL == true)
			{
				FiscalYearSetupMaint graph = CreateInstance<FiscalYearSetupMaint>();
				graph.FiscalYearSetup.Current = graph.FiscalYearSetup.Select();
				if (graph.FiscalYearSetup.Current == null)
				{
					FinYearSetup calendar = new FinYearSetup();
					graph.FiscalYearSetup.Cache.Insert(calendar);
					graph.FiscalYearSetup.Cache.IsDirty = false;
				}
				throw new PXRedirectRequiredException(graph, Messages.Calendar);
			}
			else
			{
				FABookYearSetupMaint graph = CreateInstance<FABookYearSetupMaint>();
				graph.FiscalYearSetup.Current = graph.FiscalYearSetup.Search<FABookYearSetup.bookID>(Book.Current.BookCode);
				if (graph.FiscalYearSetup.Current == null)
				{
                    FABookYearSetup calendar = new FABookYearSetup { BookID = Book.Current.BookID };
                    graph.FiscalYearSetup.Cache.SetDefaultExt<FABookYearSetup.periodType>(calendar);
					graph.FiscalYearSetup.Cache.Insert(calendar);
					graph.FiscalYearSetup.Cache.IsDirty = false;
				}
				throw new PXRedirectRequiredException(graph, Messages.Calendar);
			}
		}

		#endregion

		#region Book Events
        
        protected virtual void FABook_UpdateGL_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            FABook book = (FABook)e.Row;
            if (book == null || !(book.UpdateGL ?? false)) return;

            foreach (FABook other in PXSelect<FABook, Where<FABook.bookCode, NotEqual<Current<FABook.bookCode>>>>.SelectMultiBound(this, new object[]{book}))
            {
                other.UpdateGL = false;
                Book.Update(other);
            }
            Book.View.RequestRefresh();
        }

		protected virtual void FABook_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			FABook book = (FABook)e.Row;
			if (e.Operation == PXDBOperation.Delete)
			{
				if (PXSelect<FABookSettings, Where<FABookSettings.bookID, Equal<Current<FABook.bookID>>>>.SelectSingleBound(this, new object[] { e.Row }).Count > 0)
				{
					throw new PXRowPersistingException("BookCode", book.BookCode, Messages.BookExistsHistory);
				}

				if (PXSelect<FABookBalance, Where<FABookBalance.bookID, Equal<Current<FABook.bookID>>>>.SelectSingleBound(this, new object[] { e.Row }).Count > 0)
				{
					throw new PXRowPersistingException("BookCode", book.BookCode, Messages.BookExistsHistory);
				}
			}
		}

        protected virtual void FABook_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			if (PXSelect<FABookSettings, Where<FABookSettings.bookID, Equal<Current<FABook.bookID>>>>.SelectSingleBound(this, new object[] { e.Row }).Count > 0)
			{
				throw new PXSetPropertyException(Messages.BookExistsHistory);    	
			}

			if (PXSelect<FABookBalance, Where<FABookBalance.bookID, Equal<Current<FABook.bookID>>>>.SelectSingleBound(this, new object[] { e.Row }).Count > 0)
			{
				throw new PXSetPropertyException(Messages.BookExistsHistory);    	
			}
		}
		#endregion
    }

}
