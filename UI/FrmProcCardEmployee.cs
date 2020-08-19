using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;
using LYH.Framework.ControlUtil;

using WorkOrder.ProcCard.BLL;
using WorkOrder.ProcCard.Entity;

namespace WorkOrder.ProcCard.UI
{
	/// <summary>
	/// 员工管理
	/// </summary>	
	public partial class FrmProcCardEmployee : BaseDock
	{
		public FrmProcCardEmployee()
		{
			InitializeComponent();

			InitDictItem();

			winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
			winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
			winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
			winGridViewPager1.OnAddNew += winGridViewPager1_OnAddNew;
			winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
			winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
			winGridViewPager1.AppendedMenu = contextMenuStrip1;
			winGridViewPager1.ShowLineNumber = true;
			winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
			winGridViewPager1.gridView1.DataSourceChanged +=gridView1_DataSourceChanged;
			winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
			winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;

			//关联回车键进行查询
			foreach (Control control in layoutControl1.Controls)
			{
				control.KeyUp += SearchControl_KeyUp;
			}
		}
		void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			//if (e.Column.FieldName == "OrderStatus")
			//{
			//    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
			//    Color color = Color.White;
			//    if (status == "已审核")
			//    {
			//        e.Appearance.BackColor = Color.Red;
			//        e.Appearance.BackColor2 = Color.LightCyan;
			//    }
			//}
		}
		void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			string columnName = e.Column.FieldName;
			if (e.Column.ColumnType == typeof(DateTime))
			{   
				if (e.Value != null)
				{
					if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
					{
						e.DisplayText = "";
					}
					else
					{
						e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
					}
				}
			}
			//else if (columnName == "Age")
			//{
			//    e.DisplayText = string.Format("{0}岁", e.Value);
			//}
			//else if (columnName == "ReceivedMoney")
			//{
			//    if (e.Value != null)
			//    {
			//        e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
			//    }
			//}
		}
		
		/// <summary>
		/// 绑定数据后，分配各列的宽度
		/// </summary>
		private void gridView1_DataSourceChanged(object sender, EventArgs e)
		{
			if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
			{
				//统一设置100宽度
				foreach (DevExpress.XtraGrid.Columns.GridColumn column in winGridViewPager1.gridView1.Columns)
				{
					column.Width = 100;
				}

				//可特殊设置特别的宽度
				//SetGridColumWidth("Note", 200);
			}
		}

		private void SetGridColumWidth(string columnName, int width)
		{
			DevExpress.XtraGrid.Columns.GridColumn column = winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
			if (column != null)
			{
				column.Width = width;
			}
		}

		/// <summary>
		/// 编写初始化窗体的实现，可以用于刷新
		/// </summary>
		public override void  FormOnLoad()
		{   
			BindData();
		}
		
		/// <summary>
		/// 初始化字典列表内容
		/// </summary>
		private void InitDictItem()
		{
			//初始化代码
		}
		
		/// <summary>
		/// 分页控件刷新操作
		/// </summary>
		private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
		{
			BindData();
		}
		
		/// <summary>
		/// 分页控件删除操作
		/// </summary>
		private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
		{
			if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
			{
				return;
			}

			int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
			foreach (int iRow in rowSelected)
			{
				string ID = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
				BLLFactory<ProcCardEmployee>.Instance.Delete(ID);
			}
			 
			BindData();
		}
		
		/// <summary>
		/// 分页控件编辑项操作
		/// </summary>
		private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
		{
			string ID = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
			List<string> IDList = new List<string>();
			for (int i = 0; i < winGridViewPager1.gridView1.RowCount; i++)
			{
				string strTemp = winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
				IDList.Add(strTemp);
			}

			if (!string.IsNullOrEmpty(ID))
			{
				FrmEditProcCardEmployee dlg = new FrmEditProcCardEmployee();
				dlg.ID = ID;
				dlg.IDList = IDList;
				dlg.OnDataSaved += dlg_OnDataSaved;
				dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息
				
				if (DialogResult.OK == dlg.ShowDialog())
				{
					BindData();
				}
			}
		}        
		
		void dlg_OnDataSaved(object sender, EventArgs e)
		{
			BindData();
		}
		
		/// <summary>
		/// 分页控件新增操作
		/// </summary>        
		private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
		{
			btnAddNew_Click(null, null);
		}
		
		/// <summary>
		/// 分页控件全部导出操作前的操作
		/// </summary> 
		private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
		{
			string where = GetConditionSql();
			winGridViewPager1.AllToExport = BLLFactory<ProcCardEmployee>.Instance.FindToDataTable(where);
		 }

		/// <summary>
		/// 分页控件翻页的操作
		/// </summary> 
		private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
		{
			BindData();
		}
		
		/// <summary>
		/// 高级查询条件语句对象
		/// </summary>
		private SearchCondition advanceCondition;
		
		/// <summary>
		/// 根据查询条件构造查询语句
		/// </summary> 
		private string GetConditionSql()
		{
			//如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
			SearchCondition condition = advanceCondition;
			if (condition == null)
			{
				condition = new SearchCondition();
				condition.AddCondition("Name", txtName.Text.Trim(), SqlOperator.Like);
			}
			string where = condition.BuildConditionSql().Replace("Where", "");
			return where;
		}
		
		/// <summary>
		/// 绑定列表数据
		/// </summary>
		private void BindData()
		{
			//entity
			winGridViewPager1.DisplayColumns = "ID,TeamId,Name";
			winGridViewPager1.ColumnNameAlias = BLLFactory<ProcCardEmployee>.Instance.GetColumnNameAlias();//字段列显示名称转义

			#region 添加别名解析

			//this.winGridViewPager1.AddColumnAlias("ID", "ID");
			//this.winGridViewPager1.AddColumnAlias("TeamId", "班组");
			//this.winGridViewPager1.AddColumnAlias("Name", "姓名");

			#endregion

			string where = GetConditionSql();
				List<ProcCardEmployeeInfo> list = BLLFactory<ProcCardEmployee>.Instance.FindWithPager(where, winGridViewPager1.PagerInfo);
			winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<ProcCard_EmployeeInfo>(list);
				winGridViewPager1.PrintTitle = "员工管理报表";
		 }
		
		/// <summary>
		/// 查询数据操作
		/// </summary>
		private void btnSearch_Click(object sender, EventArgs e)
		{
			advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
			BindData();
		}
		
		/// <summary>
		/// 新增数据操作
		/// </summary>
		private void btnAddNew_Click(object sender, EventArgs e)
		{
			FrmEditProcCardEmployee dlg = new FrmEditProcCardEmployee();
			dlg.OnDataSaved += dlg_OnDataSaved;
			dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息
			
			if (DialogResult.OK == dlg.ShowDialog())
			{
				BindData();
			}
		}
		
		/// <summary>
		/// 提供给控件回车执行查询的操作
		/// </summary>
		private void SearchControl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnSearch_Click(null, null);
			}
		}        

						 
		private string moduleName = "员工管理";
		/// <summary>
		/// 导入Excel的操作
		/// </summary>          
		private void btnImport_Click(object sender, EventArgs e)
		{
			string templateFile = string.Format("{0}-模板.xls", moduleName);
			FrmImportExcelData dlg = new FrmImportExcelData();
			dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
			dlg.OnDataSave += ExcelData_OnDataSave;
			dlg.OnRefreshData += ExcelData_OnRefreshData;
			dlg.ShowDialog();
		}

		void ExcelData_OnRefreshData(object sender, EventArgs e)
		{
			BindData();
		}

		/// <summary>
		/// 如果字段存在，则获取对应的值，否则返回默认空
		/// </summary>
		/// <param name="row">DataRow对象</param>
		/// <param name="columnName">字段列名</param>
		/// <returns></returns>
		private string GetRowData(DataRow row, string columnName)
		{
			string result = "";
			if (row.Table.Columns.Contains(columnName))
			{
				result = row[columnName].ToString();
			}
			return result;
		}
		
		bool ExcelData_OnDataSave(DataRow dr)
		{
			bool success = false;
			bool converted = false;
			DateTime dtDefault = Convert.ToDateTime("1900-01-01");
			DateTime dt;
			ProcCardEmployeeInfo info = new ProcCardEmployeeInfo();
			 info.TeamId = GetRowData(dr, "班组").ToInt32();
			  info.Name = GetRowData(dr, "姓名");
			  info.Code = GetRowData(dr, "代号");
  
			success = BLLFactory<ProcCardEmployee>.Instance.Insert(info);
			 return success;
		}

		/// <summary>
		/// 导出Excel的操作
		/// </summary>
		private void btnExport_Click(object sender, EventArgs e)
		{
			string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
			if (!string.IsNullOrEmpty(file))
			{
				string where = GetConditionSql();
				List<ProcCardEmployeeInfo> list = BLLFactory<ProcCardEmployee>.Instance.Find(where);
				 DataTable dtNew = DataTableHelper.CreateTable("序号|int,班组,姓名,代号");
				DataRow dr;
				int j = 1;
				for (int i = 0; i < list.Count; i++)
				{
					dr = dtNew.NewRow();
					dr["序号"] = j++;
					 dr["班组"] = list[i].TeamId;
					 dr["姓名"] = list[i].Name;
					 dr["代号"] = list[i].Code;
					 dtNew.Rows.Add(dr);
				}

				try
				{
					string error = "";
					AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
					if (!string.IsNullOrEmpty(error))
					{
						MessageDxUtil.ShowError(string.Format("导出Excel出现错误：{0}", error));
					}
					else
					{
						if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == DialogResult.Yes)
						{
							System.Diagnostics.Process.Start(file);
						}
					}
				}
				catch (Exception ex)
				{
					LogTextHelper.Error(ex);
					MessageDxUtil.ShowError(ex.Message);
				}
			}
		 }
		 
		private FrmAdvanceSearch dlg;
		private void btnAdvanceSearch_Click(object sender, EventArgs e)
		{
			if (dlg == null)
			{
				dlg = new FrmAdvanceSearch();
				dlg.FieldTypeTable = BLLFactory<ProcCardEmployee>.Instance.GetFieldTypeList();
				dlg.ColumnNameAlias = BLLFactory<ProcCardEmployee>.Instance.GetColumnNameAlias();                
				 dlg.DisplayColumns = "TeamId,Name,Code";

				#region 下拉列表数据

				//dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("人员类型"));//字典列表
				//dlg.AddColumnListItem("Sex", "男,女");//固定列表
				//dlg.AddColumnListItem("Credit", BLLFactory<ProcCard_Employee>.Instance.GetFieldList("Credit"));//动态列表

				#endregion

				dlg.ConditionChanged += dlg_ConditionChanged;
			}
			dlg.ShowDialog();
		}

		void dlg_ConditionChanged(SearchCondition condition)
		{
			advanceCondition = condition;
			BindData();
		}
	}
}
