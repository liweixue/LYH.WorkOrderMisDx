namespace WorkOrder.ProcCard.UI
{
	partial class FrmProcCardEmployee
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.btnExport = new DevExpress.XtraEditors.SimpleButton();
			this.btnImport = new DevExpress.XtraEditors.SimpleButton();
			this.winGridViewPager1 = new LYH.Pager.WinControl.WinGridViewPager();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);            
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();    


			this.txtName = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			 
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();

			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();    

			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
 

 

					
			this.SuspendLayout();
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(698, 65);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(69, 22);
			this.btnSearch.TabIndex = 14;
			this.btnSearch.Text = "查询";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnAddNew
			// 
			this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddNew.Location = new System.Drawing.Point(773, 65);
			this.btnAddNew.Name = "btnAddNew";
			this.btnAddNew.Size = new System.Drawing.Size(69, 22);
			this.btnAddNew.TabIndex = 15;
			this.btnAddNew.Text = "新建";
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImport.Location = new System.Drawing.Point(848, 65);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(69, 22);
			this.btnImport.TabIndex = 15;
			this.btnImport.Text = "导入";
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExport.Location = new System.Drawing.Point(923, 65);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(69, 22);
			this.btnExport.TabIndex = 15;
			this.btnExport.Text = "导出";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
			// 
			// winGridViewPager1
			// 
			this.winGridViewPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.winGridViewPager1.AppendedMenu = null;
			this.winGridViewPager1.DataSource = null;
			this.winGridViewPager1.DisplayColumns = "";
			this.winGridViewPager1.Location = new System.Drawing.Point(12, 95);
			this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
			this.winGridViewPager1.Name = "winGridViewPager1";
			this.winGridViewPager1.PrintTitle = "";
			this.winGridViewPager1.ShowCheckBox = false;
			this.winGridViewPager1.Size = new System.Drawing.Size(980, 580);
			this.winGridViewPager1.TabIndex = 11;
			// 
			// layoutControl1
			//             
			this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.layoutControl1.Location = new System.Drawing.Point(12, 8);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(70, 185, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(980, 53);
			this.layoutControl1.TabIndex = 12;
			this.layoutControl1.Text = "layoutControl1";


			this.layoutControl1.Controls.Add(this.txtName);

 

			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.txtName;
			this.layoutControlItem1.CustomizationFormText = "姓名";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.MaxSize = new System.Drawing.Size(150, 25);
			this.layoutControlItem1.MinSize = new System.Drawing.Size(80, 25);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(150, 25);
			this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
			this.layoutControlItem1.Text = "姓名";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14); 
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(64, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(122, 20);
			this.txtName.StyleController = this.layoutControl1;
			this.txtName.TabIndex = 1;


 
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {

							this.layoutControlItem1
					});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(605, 363);
			this.layoutControlGroup1.Text = "layoutControlGroup1";
			this.layoutControlGroup1.TextVisible = false;          

			// 
			// FrmProcCard_Employee
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1004, 680);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.winGridViewPager1);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnAddNew);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Name = "FrmProcCardEmployee";
			this.Text = "员工管理";
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);            
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();

			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();            
 
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnSearch;
		private DevExpress.XtraEditors.SimpleButton btnAddNew;
		private DevExpress.XtraEditors.SimpleButton btnImport;
		private DevExpress.XtraEditors.SimpleButton btnExport;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private LYH.Pager.WinControl.WinGridViewPager winGridViewPager1;

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;


		private DevExpress.XtraEditors.TextEdit txtName; 
 
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
 
	}
}