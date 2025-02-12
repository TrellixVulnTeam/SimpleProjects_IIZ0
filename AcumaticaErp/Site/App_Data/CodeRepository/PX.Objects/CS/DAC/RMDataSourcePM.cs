using System;
using PX.Data;
using PX.Objects.PM;
using PX.Objects.IN;
using PX.Objects.GL;
using PX.CS;

namespace PX.Objects.CS
{
	[Serializable]
	public partial class RMDataSourcePM : PXCacheExtension<RMDataSource>
	{	
		#region StartAccountGroup
		public abstract class startAccountGroup : PX.Data.IBqlField
		{
		}
		protected string _StartAccountGroup;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Start Acc. Group")]
		[PXSelector(typeof(PM.PMAccountGroup.groupCD))]
		public virtual string StartAccountGroup
		{
			get
			{
				return this._StartAccountGroup;
			}
			set
			{
				this._StartAccountGroup = value;
			}
		}
		#endregion
		#region StartProject
		public abstract class startProject : PX.Data.IBqlField
		{
		}
		protected string _startProject;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Start Project")]
		[PXSelector(typeof(Search<PMProject.contractCD, Where<PMProject.isTemplate, Equal<False>, And<PMProject.baseType, Equal<PMProject.ProjectBaseType>, And<PMProject.nonProject, Equal<False>>>>>))]
		public virtual string StartProject
		{
			get
			{
				return this._startProject;
			}
			set
			{
				this._startProject = value;
			}
		}
		#endregion
		#region StartProjectTask
		public abstract class startProjectTask : PX.Data.IBqlField
		{
		}
		protected string _StartProjectTask;
		[ProjectTaskRawAttribute(DisplayName = "Start Task")]
		public virtual string StartProjectTask
		{
			get
			{
				return this._StartProjectTask;
			}
			set
			{
				this._StartProjectTask = value;
			}
		}
		#endregion
		#region StartInventory
		public abstract class startInventory : PX.Data.IBqlField
		{
		}
		protected string _StartInventory;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Start Inventory")]
		[PXSelector(typeof(InventoryItem.inventoryCD))]
		public virtual string StartInventory
		{
			get
			{
				return this._StartInventory;
			}
			set
			{
				this._StartInventory = value;
			}
		}
		#endregion
		#region EndAccountGroup
		public abstract class endAccountGroup : PX.Data.IBqlField
		{
		}
		protected string _EndAccountGroup;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "End Acc. Group")]
		[PXSelector(typeof(PM.PMAccountGroup.groupCD))]
		public virtual string EndAccountGroup
		{
			get
			{
				return this._EndAccountGroup;
			}
			set
			{
				this._EndAccountGroup = value;
			}
		}
		#endregion		
		#region EndProject
		public abstract class endProject : PX.Data.IBqlField
		{
		}
		protected string _endProject;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "End Project")]
		[PXSelector(typeof(Search<PMProject.contractCD, Where<PMProject.isTemplate, Equal<False>, And<PMProject.baseType, Equal<PMProject.ProjectBaseType>, And<PMProject.nonProject, Equal<False>>>>>))]
		public virtual string EndProject
		{
			get
			{
				return this._endProject;
			}
			set
			{
				this._endProject = value;
			}
		}
		#endregion		
		#region EndProjectTask
		public abstract class endProjectTask : PX.Data.IBqlField
		{
		}
		protected string _EndProjectTask;
		[ProjectTaskRawAttribute(DisplayName = "End Task")]
		public virtual string EndProjectTask
		{
			get
			{
				return this._EndProjectTask;
			}
			set
			{
				this._EndProjectTask = value;
			}
		}
		#endregion		
		#region EndInventory
		public abstract class endInventory : PX.Data.IBqlField
		{
		}
		protected string _EndInventory;
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "End Inventory")]
		[PXSelector(typeof(InventoryItem.inventoryCD))]
		public virtual string EndInventory
		{
			get
			{
				return this._EndInventory;
			}
			set
			{
				this._EndInventory = value;
			}
		}
		#endregion
	}
}
