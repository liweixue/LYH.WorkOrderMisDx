using System;
using LYH.Framework.BaseUI;
using LYH.Framework.Commons;
using LYH.Framework.ControlUtil;
using WorkOrder.ProcCard.BLL;
using WorkOrder.ProcCard.Entity;

namespace WorkOrder.ProcCard.UI
{
	public partial class FrmEditProcCardEmployee : BaseEditForm
	{
		/// <summary>
		/// 创建一个临时对象，方便在附件管理中获取存在的GUID
		/// </summary>
		private ProcCardEmployeeInfo tempInfo = new ProcCardEmployeeInfo();
		
		public FrmEditProcCardEmployee()
		{
			InitializeComponent();
		}
				
		/// <summary>
		/// 实现控件输入检查的函数
		/// </summary>
		/// <returns></returns>
		public override bool CheckInput()
		{
			bool result = true;//默认是可以通过

			#region MyRegion
			if (this.txtTeamId.Text.Trim().Length == 0)
			{
				MessageDxUtil.ShowTips("请输入班组");
				this.txtTeamId.Focus();
				result = false;
			}
			 else if (this.txtName.Text.Trim().Length == 0)
			{
				MessageDxUtil.ShowTips("请输入姓名");
				this.txtName.Focus();
				result = false;
			}
			#endregion

			return result;
		}

		/// <summary>
		/// 初始化数据字典
		/// </summary>
		private void InitDictItem()
		{
			//初始化代码
		}                        

		/// <summary>
		/// 数据显示的函数
		/// </summary>
		public override void DisplayData()
		{
			InitDictItem();//数据字典加载（公用）

			if (!string.IsNullOrEmpty(ID))
			{
				#region 显示信息
				ProcCardEmployeeInfo info = BLLFactory<ProcCardEmployee>.Instance.FindById(ID);
				if (info != null)
				{
					tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
					
						txtTeamId.EditValue= info.TeamId;
							txtName.Text = info.Name;
							 } 
				#endregion
				//this.btnOK.Enabled = HasFunction("ProcCard_Employee/Edit");             
			}
			else
			{
  
				//this.btnOK.Enabled = Portal.gc.HasFunction("ProcCard_Employee/Add");  
			}
			
			//tempInfo在对象存在则为指定对象，新建则是全新的对象，但有一些初始化的GUID用于附件上传
			//SetAttachInfo(tempInfo);
		}

		//private void SetAttachInfo(ProcCard_EmployeeInfo info)
		//{
		//    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
		//    this.attachmentGUID.userId = LoginUserInfo.Name;

		//    string name = txtName.Text;
		//    if (!string.IsNullOrEmpty(name))
		//    {
		//        string dir = string.Format("{0}", name);
		//        this.attachmentGUID.Init(dir, tempInfo.ID, LoginUserInfo.Name);
		//    }
		//}

		public override void ClearScreen()
		{
			this.tempInfo = new ProcCardEmployeeInfo();
			base.ClearScreen();
		}

		/// <summary>
		/// 编辑或者保存状态下取值函数
		/// </summary>
		/// <param name="info"></param>
		private void SetInfo(ProcCardEmployeeInfo info)
		{
				info.TeamId = Convert.ToInt32(txtTeamId.EditValue);
					info.Name = txtName.Text;
			   }
		 
		/// <summary>
		/// 新增状态下的数据保存
		/// </summary>
		/// <returns></returns>
		public override bool SaveAddNew()
		{
			ProcCardEmployeeInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
			SetInfo(info);

			try
			{
				#region 新增数据

				bool succeed = BLLFactory<ProcCardEmployee>.Instance.Insert(info);
				if (succeed)
				{
					//可添加其他关联操作

					return true;
				}
				#endregion
			}
			catch (Exception ex)
			{
				LogTextHelper.Error(ex);
				MessageDxUtil.ShowError(ex.Message);
			}
			return false;
		}                 

		/// <summary>
		/// 编辑状态下的数据保存
		/// </summary>
		/// <returns></returns>
		public override bool SaveUpdated()
		{

			ProcCardEmployeeInfo info = BLLFactory<ProcCardEmployee>.Instance.FindById(ID);
			if (info != null)
			{
				SetInfo(info);

				try
				{
					#region 更新数据
					bool succeed = BLLFactory<ProcCardEmployee>.Instance.Update(info, info.ID);
					if (succeed)
					{
						//可添加其他关联操作
					   
						return true;
					}
					#endregion
				}
				catch (Exception ex)
				{
					LogTextHelper.Error(ex);
					MessageDxUtil.ShowError(ex.Message);
				}
			}
		   return false;
		}
	}
}
