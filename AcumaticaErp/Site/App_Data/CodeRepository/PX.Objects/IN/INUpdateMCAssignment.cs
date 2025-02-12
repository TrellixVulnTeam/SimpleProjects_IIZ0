using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using PX.SM;
using PX.Data;
using PX.Objects.BQLConstants;
using PX.Objects.CS;
using PX.Objects.CM;
using PX.Objects.GL;

namespace PX.Objects.IN
{
	#region Update Settings

	[Serializable]
	public partial class UpdateMCAssignmentSettings : PX.Data.IBqlTable  // MC = MovementClass
	{
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField
		{
		}
		protected Int32? _SiteID;
		[PXDefault()]
		[Site()]
		public virtual Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region Year
		public abstract class year : PX.Data.IBqlField
		{
		}
		protected String _Year;
		[PXDBString(4, IsFixed = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Year", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search3<FinYear.year, OrderBy<Desc<FinYear.year>>>))]
		public virtual String Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				this._Year = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _StartDate;
		[PXDBDate()]
		[PXDefault(TypeCode.DateTime, "01/01/1900")]
		[PXUIField(DisplayName = "Start Date", Enabled = false)]
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
		[PXDefault(TypeCode.DateTime, "01/01/1900")]
		[PXUIField(DisplayName = "End Date", Enabled = false)]
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
		#region PeriodNbr
		public abstract class periodNbr : PX.Data.IBqlField
		{
		}
		protected Int16 ? _PeriodNbr;
		[PXDBShort(MinValue = 1, MaxValue = 12)]
		[PXDefault((short)1)]
		[PXUIField(DisplayName = "Period Number")]
		public virtual Int16 ? PeriodNbr
		{
			get
			{
				return this._PeriodNbr;
			}
			set
			{
				this._PeriodNbr = value;
			}
		}
		#endregion
		#region StartFinPeriodID
		public abstract class startFinPeriodID : PX.Data.IBqlField
		{
		}
		protected String _StartFinPeriodID;
		[GL.FinPeriodID()]
		[PXDefault()]
		[PXUIField(DisplayName = "Start Period", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual String StartFinPeriodID
		{
			get
			{
				return this._StartFinPeriodID;
			}
			set
			{
				this._StartFinPeriodID = value;
			}
		}
		#endregion
		#region EndFinPeriodID
		public abstract class endFinPeriodID : PX.Data.IBqlField
		{
		}
		protected String _EndFinPeriodID;
		[GL.FinPeriodID()]
		[PXDefault()]
		[PXUIField(DisplayName = "End Period", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual String EndFinPeriodID
		{
			get
			{
				return this._EndFinPeriodID;
			}
			set
			{
				this._EndFinPeriodID = value;
			}
		}
		#endregion
	}

	#endregion

	#region UpdateResult
	[Serializable]
	public partial class UpdateMCAssignmentResult : PX.Data.IBqlTable
	{

		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField { }
		protected Int32? _InventoryID;
		[Inventory(IsKey = true, Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Inventory ID")]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		protected String _Descr;
		[PXString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr
		{
			get
			{
				return this._Descr;
			}
			set
			{
				this._Descr = value;
			}
		}
		#endregion

		#region OldMC
		public abstract class oldMC : PX.Data.IBqlField
		{
		}
		protected String _OldMC;
		[PXString(1)]
		[PXUIField(DisplayName = "Current Movement Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<INMovementClass.movementClassID>), DescriptionField = typeof(INMovementClass.descr))]
		public virtual String OldMC
		{
			get
			{
				return this._OldMC;
			}
			set
			{
				this._OldMC = value;
			}
		}
		#endregion
		#region MCFixed
		public abstract class mCFixed : PX.Data.IBqlField { }
		protected bool? _MCFixed = false;
		[PXBool]
		[PXUIField(DisplayName = "Fixed", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual bool? MCFixed
		{
			get
			{
				return this._MCFixed;
			}
			set
			{
				this._MCFixed = value;
			}
		}
		#endregion
		#region NewMC
		public abstract class newMC : PX.Data.IBqlField
		{
		}
		protected String _NewMC;
		[PXString(1)]
		[PXUIField(DisplayName = "Projected Movement Class", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<INMovementClass.movementClassID>), DescriptionField = typeof(INMovementClass.descr))]
		public virtual String NewMC
		{
			get
			{
				return this._NewMC;
			}
			set
			{
				this._NewMC = value;
			}
		}
		#endregion
	}
	#endregion


	[PX.Objects.GL.TableAndChartDashboardType]
	public class INUpdateMCAssignment : PXGraph<INUpdateMCAssignment>
	{
		public PXCancel<UpdateMCAssignmentSettings> Cancel;
		public PXFilter<UpdateMCAssignmentSettings> UpdateSettings;
		public PXSelectOrderBy<UpdateMCAssignmentResult, OrderBy<Asc<UpdateMCAssignmentResult.inventoryID>>> ResultPreview;
		public PXSelect<INItemSite> itemsite;
		public PXSetup<INSetup>	INSetup;

		public PXAction<UpdateMCAssignmentSettings> Process;

		[PXUIField(DisplayName = Messages.Process, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable process(PXAdapter adapter)
		{
			//  recalc and save to INItemSite
			CalcMCAssignments(true);

			return adapter.Get();
		}

		public INUpdateMCAssignment()
		{
			ResultPreview.Cache.AllowInsert = false;
			ResultPreview.Cache.AllowDelete = false;
			ResultPreview.Cache.AllowUpdate = false;
		}


		private List<UpdateMCAssignmentResult> CalcMCAssignments (bool updateDB)
		{

			UpdateMCAssignmentSettings us = UpdateSettings.Current;

			List<UpdateMCAssignmentResult> list = new List<UpdateMCAssignmentResult>();

			if (us == null) { return list; } //empty

			if ( (us.SiteID == null ) || (us.StartFinPeriodID == null) || (us.EndFinPeriodID == null) ) { return list; } //empty

			if (updateDB)
			{
				itemsite.Cache.Clear();
			}
			
			PXSelectBase<INItemSite> cmd = new PXSelectJoin<INItemSite, 
												InnerJoin<InventoryItem, 
													On<InventoryItem.inventoryID, Equal<INItemSite.inventoryID>,
													And<InventoryItem.stkItem, Equal<boolTrue>>>>,
												Where<INItemSite.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>>>(this);
		
			foreach (PXResult<INItemSite,InventoryItem>  resultset in cmd.Select())
			{
				INItemSite			currentItemSite = (INItemSite)  resultset;
				InventoryItem		inventoryItem = (InventoryItem) resultset;
				UpdateMCAssignmentResult updateMC = new UpdateMCAssignmentResult();
				updateMC.MCFixed = currentItemSite.MovementClassIsFixed;
				updateMC.Descr = inventoryItem.Descr;
				updateMC.InventoryID = currentItemSite.InventoryID;
				updateMC.OldMC = currentItemSite.MovementClassID;
				if (updateMC.MCFixed == true)
				{
					updateMC.NewMC = currentItemSite.MovementClassID;
				}
				else
				{

					PXSelectBase<FinPeriod> cmd1 = new PXSelectJoinGroupBy<FinPeriod,
					InnerJoin<INItemCostHist,
						On<INItemCostHist.finPeriodID, LessEqual<FinPeriod.finPeriodID>>,

					InnerJoin<INLocation,
						On<
							Where2<

									Where<
													INLocation.isCosted, Equal<boolFalse>,
												And<INItemCostHist.costSiteID, Equal<INLocation.siteID>>>,
								Or<
									Where<
												INLocation.isCosted, NotEqual<boolFalse>,
											And<INItemCostHist.costSiteID, Equal<INLocation.locationID>>>>>>>>,
					Where<INLocation.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>,
					  And<INItemCostHist.inventoryID, Equal<Required<INItemCostHist.inventoryID>>,
					  And<FinPeriod.finPeriodID, GreaterEqual<Current<UpdateMCAssignmentSettings.startFinPeriodID>>,
					  And<FinPeriod.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>>>>>,

					Aggregate<
						Avg<INItemCostHist.finYtdCost,			
						Avg<INItemCostHist.tranYtdCost,
						Avg<INItemCostHist.finBegQty,  
						Avg<INItemCostHist.tranBegQty,
						Avg<INItemCostHist.finYtdQty,
						Avg<INItemCostHist.tranYtdQty>>>>>>>>(this);
					INItemCostHist itemHistTranYtdQty = null;
					decimal tranYtdQty = 0m;
					foreach(PXResult<FinPeriod, INItemCostHist> ih in cmd1.Select(currentItemSite.InventoryID))
					{
						itemHistTranYtdQty = (INItemCostHist)ih;	
						tranYtdQty += itemHistTranYtdQty.TranYtdQty ?? 0m;
					}

					PXSelectBase<INItemCostHist> cmd2 = new

						PXSelectJoinGroupBy<INItemCostHist,
								LeftJoin<INLocation,
											On<
												Where2<

														Where<
																		INLocation.isCosted, Equal<boolFalse>,
																	And<INItemCostHist.costSiteID, Equal<INLocation.siteID>>>,
													Or<
														Where<
																	INLocation.isCosted, NotEqual<boolFalse>,
																And<INItemCostHist.costSiteID, Equal<INLocation.siteID>>>>>>>,
						Where<INLocation.siteID, Equal<Current<UpdateMCAssignmentSettings.siteID>>,
						  And<INItemCostHist.inventoryID, Equal<Required<INItemCostHist.inventoryID>>,
						  And<INItemCostHist.finPeriodID, GreaterEqual<Current<UpdateMCAssignmentSettings.startFinPeriodID>>,
						  And<INItemCostHist.finPeriodID, LessEqual<Current<UpdateMCAssignmentSettings.endFinPeriodID>>>>>>,
						Aggregate<	Sum<INItemCostHist.tranPtdQtyReceived,				
									Sum<INItemCostHist.tranPtdQtyIssued,
									Sum<INItemCostHist.tranPtdQtySales,
									Sum<INItemCostHist.tranPtdQtyCreditMemos,
									Sum<INItemCostHist.tranPtdQtyDropShipSales,
									Sum<INItemCostHist.tranPtdQtyTransferIn,
									Sum<INItemCostHist.tranPtdQtyTransferOut,
									Sum<INItemCostHist.tranPtdQtyAdjusted,
									Sum<INItemCostHist.finPtdQtyReceived,				
									Sum<INItemCostHist.finPtdQtyIssued,
									Sum<INItemCostHist.finPtdQtySales,
									Sum<INItemCostHist.finPtdQtyCreditMemos,
									Sum<INItemCostHist.finPtdQtyDropShipSales,
									Sum<INItemCostHist.finPtdQtyTransferIn,
									Sum<INItemCostHist.finPtdQtyTransferOut,
									Sum<INItemCostHist.finPtdQtyAdjusted,
									Avg<INItemCostHist.tranYtdQty>>>>>>>>>>>>>>>>>>>(this);

					INItemCostHist itemHistTranPtdQtySales = null; 
					decimal tranPtdQtySales = 0m;
					foreach(PXResult<INItemCostHist> ih in cmd2.Select(currentItemSite.InventoryID))
					{
						itemHistTranPtdQtySales = (INItemCostHist)ih;	
						tranPtdQtySales += (itemHistTranPtdQtySales.TranPtdQtySales ?? 0m);
					}

					if(tranYtdQty != 0m || tranPtdQtySales != 0m)
					{
						 decimal radio = MovementToStockRatio(tranYtdQty, tranPtdQtySales);
					
						 PXSelectBase<INMovementClass> cmd3 = new PXSelect<INMovementClass, 
														Where<INMovementClass.maxTurnoverPct, GreaterEqual<Required<INMovementClass.maxTurnoverPct>>>,
														OrderBy<Asc<INMovementClass.maxTurnoverPct>>>(this);

						INMovementClass movementClass = (INMovementClass) cmd3.Select(radio);
						if (movementClass != null)
						{
							updateMC.NewMC = movementClass.MovementClassID;
						}
						else
						{
							updateMC.NewMC = null;	
						}
					}
				}

				updateMC.OldMC = currentItemSite.MovementClassID;
				list.Add(updateMC);

				if (updateDB && (currentItemSite.MovementClassID != updateMC.NewMC))
				{
					currentItemSite.MovementClassID = updateMC.NewMC; 
					itemsite.Update(currentItemSite);
				}
			}
			if (updateDB)
			{
				this.Actions.PressSave();
			}
			return list;
		}

		protected virtual IEnumerable resultPreview()
		{
			return CalcMCAssignments(false);
		}

		#region Misc.

		// common code to be called from sorting delegate both for a and b parts of comparison
		private static decimal MovementToStockRatio(decimal stockYtdQty, decimal salesByPeriod)
		{
			if (stockYtdQty < 0m) { stockYtdQty = 0m; }
			decimal ratio;
			if (salesByPeriod == 0m && stockYtdQty != 0m)
			{
				ratio = 0m;
			}
			else
			{
				if (stockYtdQty == 0m)
				{
					ratio = decimal.MaxValue;
				}
				else
				{
					ratio = salesByPeriod  * 100 / stockYtdQty;
				}
			}
			return ratio;
		}

		#endregion Misc.

		
		protected virtual void UpdateMCAssignmentSettings_Year_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
		{
			UpdateMCAssignmentSettings settings = (UpdateMCAssignmentSettings)e.Row;
			if (settings == null) return;	
			DateTime date  = (DateTime)Accessinfo.BusinessDate;
			e.NewValue = date.Year.ToString();
			e.Cancel = true;
			settings.Year = date.Year.ToString();
			settings.PeriodNbr = 1;
			SetPeriods(settings);
		}
		protected virtual void UpdateMCAssignmentSettings_PeriodNbr_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			UpdateMCAssignmentSettings settings = (UpdateMCAssignmentSettings)e.Row;
			if (settings == null) return;

			SetPeriods(settings);
		}
		protected virtual void UpdateMCAssignmentSettings_PeriodNbr_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
		{
			UpdateMCAssignmentSettings settings = (UpdateMCAssignmentSettings)e.Row;
			if (settings == null) return;
			INSetup insetup = INSetup.Current;
			if (((short) e.NewValue) > insetup.TurnoverPeriodsPerYear)
			{
				cache.RaiseExceptionHandling<UpdateMCAssignmentSettings.periodNbr>(settings, e.NewValue, new PXSetPropertyException(Messages.PeriofNbrCanNotBeGreaterThenInSetup));
			}
		}
		protected virtual void UpdateMCAssignmentSettings_Year_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			UpdateMCAssignmentSettings settings = (UpdateMCAssignmentSettings)e.Row;
			if (settings == null) return;
			SetPeriods(settings);
		}
		private void SetPeriods(UpdateMCAssignmentSettings settings)
		{
			INSetup insetup = INSetup.Current;
			PXCache cache   =  UpdateSettings.Cache;
			if (insetup != null && insetup.TurnoverPeriodsPerYear != null && insetup.TurnoverPeriodsPerYear != 0m && settings.PeriodNbr != null && settings.Year != null)
			{
				String periodString = (12 / insetup.TurnoverPeriodsPerYear * (settings.PeriodNbr - 1) + 1).ToString();
				periodString = periodString.Length == 1 ? "0" + periodString : periodString;
				periodString = settings.Year + periodString;
				FinPeriod startPeriod = (FinPeriod)PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>>.Select(this, periodString);
				if (startPeriod != null)
				{
					//cache.SetValueExt<UpdateMCAssignmentSettings.startFinPeriodID>(settings, startPeriod.FinPeriodID);
					settings.StartFinPeriodID = startPeriod.FinPeriodID;
					cache.SetValueExt<UpdateMCAssignmentSettings.startDate>(settings, startPeriod.StartDate);
				}
				periodString = (12 / insetup.TurnoverPeriodsPerYear * settings.PeriodNbr).ToString();
				periodString = periodString.Length == 1 ? "0" + periodString : periodString;
				periodString = settings.Year + periodString;
				FinPeriod endPeriod = (FinPeriod)PXSelect<FinPeriod, Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>>.Select(this, periodString);
				if (endPeriod != null)
				{
					//cache.SetValueExt<UpdateMCAssignmentSettings.endFinPeriodID>(settings, endPeriod.FinPeriodID);
					settings.EndFinPeriodID = endPeriod.FinPeriodID;
					cache.SetValueExt<UpdateMCAssignmentSettings.endDate>(settings, endPeriod.EndDate);
				}
			}
		}
	}

	
	#region ItemCostHistByItemSite projection 

	[Serializable()]
	[PXProjection(typeof(Select5<INItemCostHist,
		InnerJoin<INLocation,
			On<
				Where2<

						Where<
										INLocation.isCosted, Equal<boolFalse>,
									And<INItemCostHist.costSiteID, Equal<INLocation.siteID>>>,
					Or<
						Where<
									INLocation.isCosted, NotEqual<boolFalse>,
								And<INItemCostHist.costSiteID, Equal<INLocation.locationID>>>>>>>,

		Aggregate<
			GroupBy<INLocation.siteID,
			GroupBy<INItemCostHist.inventoryID,
			GroupBy<INItemCostHist.finPeriodID,

			Sum<INItemCostHist.tranPtdQtyReceived,				
			Sum<INItemCostHist.tranPtdQtyIssued,
			Sum<INItemCostHist.tranPtdQtySales,
			Sum<INItemCostHist.tranPtdQtyCreditMemos,
			Sum<INItemCostHist.tranPtdQtyDropShipSales,
			Sum<INItemCostHist.tranPtdQtyTransferIn,
			Sum<INItemCostHist.tranPtdQtyTransferOut,
			Sum<INItemCostHist.tranPtdQtyAdjusted,

			Sum<INItemCostHist.finPtdQtyReceived,				
			Sum<INItemCostHist.finPtdQtyIssued,
			Sum<INItemCostHist.finPtdQtySales,
			Sum<INItemCostHist.finPtdQtyCreditMemos,
			Sum<INItemCostHist.finPtdQtyDropShipSales,
			Sum<INItemCostHist.finPtdQtyTransferIn,
			Sum<INItemCostHist.finPtdQtyTransferOut,
			Sum<INItemCostHist.finPtdQtyAdjusted>>>>>>>>>>>>>>>>>>>>>))]

	public partial class ItemCostHistByItemSite : PX.Data.IBqlTable
	{
		#region SiteID
		public abstract class siteID : PX.Data.IBqlField
		{
		}
		protected Int32? _SiteID;
		[Site(BqlField = typeof(INLocation.siteID))]
		public virtual Int32? SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				this._SiteID = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[Inventory(BqlField = typeof(INItemCostHist.inventoryID))]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField { };
		protected String _FinPeriodID;
		[GL.FinPeriodID(BqlField = typeof(INItemCostHist.finPeriodID))]
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

		#region TranPtdQtyReceived
		public abstract class tranPtdQtyReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyReceived;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyReceived))]
		public virtual Decimal? TranPtdQtyReceived
		{
			get
			{
				return this._TranPtdQtyReceived;
			}
			set
			{
				this._TranPtdQtyReceived = value;
			}
		}
		#endregion   
		#region FinPtdQtyReceived
		public abstract class finPtdQtyReceived : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyReceived;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyReceived))]
		public virtual Decimal? FinPtdQtyReceived
		{
			get
			{
				return this._FinPtdQtyReceived;
			}
			set
			{
				this._FinPtdQtyReceived = value;
			}
		}
		#endregion   

		#region TranPtdQtyIssued
		public abstract class tranPtdQtyIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyIssued;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyIssued))]
		public virtual Decimal? TranPtdQtyIssued
		{
			get
			{
				return this._TranPtdQtyIssued;
			}
			set
			{
				this._TranPtdQtyIssued = value;
			}
		}
		#endregion
		#region FinPtdQtyIssued
		public abstract class finPtdQtyIssued : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyIssued;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyIssued))]
		public virtual Decimal? FinPtdQtyIssued
		{
			get
			{
				return this._FinPtdQtyIssued;
			}
			set
			{
				this._FinPtdQtyIssued = value;
			}
		}
		#endregion   

		#region TranPtdQtySales
		public abstract class tranPtdQtySales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtySales;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtySales))]
		public virtual Decimal? TranPtdQtySales
		{
			get
			{
				return this._TranPtdQtySales;
			}
			set
			{
				this._TranPtdQtySales = value;
			}
		}
		#endregion
		#region FinPtdQtySales
		public abstract class finPtdQtySales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtySales;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtySales))]
		public virtual Decimal? FinPtdQtySales
		{
			get
			{
				return this._FinPtdQtySales;
			}
			set
			{
				this._FinPtdQtySales = value;
			}
		}
		#endregion   

		#region TranPtdQtyCreditMemos
		public abstract class tranPtdQtyCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyCreditMemos;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyCreditMemos))]
		public virtual Decimal? TranPtdQtyCreditMemos
		{
			get
			{
				return this._TranPtdQtyCreditMemos;
			}
			set
			{
				this._TranPtdQtyCreditMemos = value;
			}
		}
		#endregion
		#region FinPtdQtyCreditMemos
		public abstract class finPtdQtyCreditMemos : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyCreditMemos;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyCreditMemos))]
		public virtual Decimal? FinPtdQtyCreditMemos
		{
			get
			{
				return this._FinPtdQtyCreditMemos;
			}
			set
			{
				this._FinPtdQtyCreditMemos = value;
			}
		}
		#endregion   

		#region TranPtdQtyDropShipSales
		public abstract class tranPtdQtyDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyDropShipSales;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyDropShipSales))]
		public virtual Decimal? TranPtdQtyDropShipSales
		{
			get
			{
				return this._TranPtdQtyDropShipSales;
			}
			set
			{
				this._TranPtdQtyDropShipSales = value;
			}
		}
		#endregion
		#region FinPtdQtyDropShipSales
		public abstract class finPtdQtyDropShipSales : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyDropShipSales;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyDropShipSales))]
		public virtual Decimal? FinPtdQtyDropShipSales
		{
			get
			{
				return this._FinPtdQtyDropShipSales;
			}
			set
			{
				this._FinPtdQtyDropShipSales = value;
			}
		}
		#endregion   

		#region TranPtdQtyTransferIn
		public abstract class tranPtdQtyTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyTransferIn;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyTransferIn))]
		public virtual Decimal? TranPtdQtyTransferIn
		{
			get
			{
				return this._TranPtdQtyTransferIn;
			}
			set
			{
				this._TranPtdQtyTransferIn = value;
			}
		}
		#endregion
		#region FinPtdQtyTransferIn
		public abstract class finPtdQtyTransferIn : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyTransferIn;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyTransferIn))]
		public virtual Decimal? FinPtdQtyTransferIn
		{
			get
			{
				return this._FinPtdQtyTransferIn;
			}
			set
			{
				this._FinPtdQtyTransferIn = value;
			}
		}
		#endregion   

		#region TranPtdQtyTransferOut
		public abstract class tranPtdQtyTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyTransferOut;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyTransferOut))]
		public virtual Decimal? TranPtdQtyTransferOut
		{
			get
			{
				return this._TranPtdQtyTransferOut;
			}
			set
			{
				this._TranPtdQtyTransferOut = value;
			}
		}
		#endregion
		#region FinPtdQtyTransferOut
		public abstract class finPtdQtyTransferOut : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyTransferOut;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyTransferOut))]
		public virtual Decimal? FinPtdQtyTransferOut
		{
			get
			{
				return this._FinPtdQtyTransferOut;
			}
			set
			{
				this._FinPtdQtyTransferOut = value;
			}
		}
		#endregion   

		#region TranPtdQtyAdjusted
		public abstract class tranPtdQtyAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _TranPtdQtyAdjusted;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.tranPtdQtyAdjusted))]
		public virtual Decimal? TranPtdQtyAdjusted
		{
			get
			{
				return this._TranPtdQtyAdjusted;
			}
			set
			{
				this._TranPtdQtyAdjusted = value;
			}
		}
		#endregion
		#region FinPtdQtyAdjusted
		public abstract class finPtdQtyAdjusted : PX.Data.IBqlField
		{
		}
		protected Decimal? _FinPtdQtyAdjusted;
		[PXDBQuantity(BqlField = typeof(INItemCostHist.finPtdQtyAdjusted))]
		public virtual Decimal? FinPtdQtyAdjusted
		{
			get
			{
				return this._FinPtdQtyAdjusted;
			}
			set
			{
				this._FinPtdQtyAdjusted = value;
			}
		}
		#endregion   

	}

	#endregion ItemCostHistByItemSite projection


}
