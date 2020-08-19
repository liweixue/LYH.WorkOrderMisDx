namespace WorkOrder.ProcCard.UI
{
	partial class FrmEditProcCardEmployee
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.txtName = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.txtTeamId = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTeamId.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(239, 74);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(425, 74);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(332, 74);
			// 
			// dataNavigator1
			// 
			this.dataNavigator1.Location = new System.Drawing.Point(12, 69);
			// 
			// picPrint
			// 
			this.picPrint.Location = new System.Drawing.Point(202, 71);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.layoutControl1.Controls.Add(this.txtName);
			this.layoutControl1.Controls.Add(this.txtTeamId);
			this.layoutControl1.Location = new System.Drawing.Point(12, 8);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(493, 45);
			this.layoutControl1.TabIndex = 6;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(275, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(206, 20);
			this.txtName.StyleController = this.layoutControl1;
			this.txtName.TabIndex = 2;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
			this.layoutControlItem1,
			this.layoutControlItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(493, 45);
			this.layoutControlGroup1.Text = "layoutControlGroup1";
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.txtName;
			this.layoutControlItem2.CustomizationFormText = "姓名";
			this.layoutControlItem2.Location = new System.Drawing.Point(236, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(237, 25);
			this.layoutControlItem2.Text = "姓名";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(24, 14);
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.txtTeamId;
			this.layoutControlItem1.CustomizationFormText = "班组";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(236, 25);
			this.layoutControlItem1.Text = "班组";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 14);
			// 
			// txtTeamId
			// 
			this.txtTeamId.EditValue = "";
			this.txtTeamId.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtTeamId.Location = new System.Drawing.Point(39, 12);
			this.txtTeamId.Name = "txtTeamId";
			this.txtTeamId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.txtTeamId.Size = new System.Drawing.Size(205, 20);
			this.txtTeamId.StyleController = this.layoutControl1;
			this.txtTeamId.TabIndex = 1;
			// 
			// FrmEditProcCardEmployee
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(512, 109);
			this.Controls.Add(this.layoutControl1);
			this.Name = "FrmEditProcCardEmployee";
			this.Text = "员工管理";
			this.Controls.SetChildIndex(this.layoutControl1, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnAdd, 0);
			this.Controls.SetChildIndex(this.dataNavigator1, 0);
			this.Controls.SetChildIndex(this.picPrint, 0);
			((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTeamId.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		  private DevExpress.XtraEditors.TextEdit txtName;
		 private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraEditors.ComboBoxEdit txtTeamId;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
	}
}