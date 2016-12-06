namespace LYH.WorkOrder
{
    partial class FrmContructionHandwriting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContructionHandwriting));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtWONo = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.txtOrderQt = new System.Windows.Forms.TextBox();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.txtCraft = new System.Windows.Forms.TextBox();
            this.txtTeam = new System.Windows.Forms.TextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxCust = new System.Windows.Forms.ComboBox();
            this.dtpDelivery = new System.Windows.Forms.DateTimePicker();
            this.txtCompleteNum = new System.Windows.Forms.TextBox();
            this.txtProcessNum = new System.Windows.Forms.TextBox();
            this.txtPriceNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSubsidy = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPageNo = new System.Windows.Forms.TextBox();
            this.lblCheck = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOriWONo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(0, 168);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(841, 425);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.TabStop = false;
            // 
            // txtWONo
            // 
            this.txtWONo.Location = new System.Drawing.Point(159, 12);
            this.txtWONo.Name = "txtWONo";
            this.txtWONo.ReadOnly = true;
            this.txtWONo.Size = new System.Drawing.Size(80, 21);
            this.txtWONo.TabIndex = 0;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(71, 124);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(80, 21);
            this.txtPartNo.TabIndex = 4;
            this.txtPartNo.Text = "如单";
            this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(83, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "工单号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(361, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "工 序 号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(361, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "工序名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(361, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "生产班组";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(528, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "完成数量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(528, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "加 工 数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(528, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "序价代码";
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(246, 54);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(90, 21);
            this.txtPartName.TabIndex = 5;
            this.txtPartName.Text = "如单";
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            // 
            // txtOrderQt
            // 
            this.txtOrderQt.Location = new System.Drawing.Point(246, 89);
            this.txtOrderQt.Name = "txtOrderQt";
            this.txtOrderQt.Size = new System.Drawing.Size(90, 21);
            this.txtOrderQt.TabIndex = 6;
            this.txtOrderQt.TextChanged += new System.EventHandler(this.txtOrderQt_TextChanged);
            this.txtOrderQt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderQt_KeyDown);
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(430, 54);
            this.txtSn.Name = "txtSn";
            this.txtSn.ReadOnly = true;
            this.txtSn.Size = new System.Drawing.Size(70, 21);
            this.txtSn.TabIndex = 18;
            this.txtSn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSn_KeyDown);
            // 
            // txtCraft
            // 
            this.txtCraft.Location = new System.Drawing.Point(430, 89);
            this.txtCraft.Name = "txtCraft";
            this.txtCraft.Size = new System.Drawing.Size(70, 21);
            this.txtCraft.TabIndex = 8;
            this.txtCraft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCraft_KeyDown);
            // 
            // txtTeam
            // 
            this.txtTeam.Location = new System.Drawing.Point(430, 124);
            this.txtTeam.Name = "txtTeam";
            this.txtTeam.Size = new System.Drawing.Size(70, 21);
            this.txtTeam.TabIndex = 9;
            this.txtTeam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTeam_KeyDown);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(9, 8);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(61, 28);
            this.btnInsert.TabIndex = 22;
            this.btnInsert.Text = "&A新增";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(690, 51);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "&S添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(690, 86);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(61, 28);
            this.btnFinish.TabIndex = 14;
            this.btnFinish.Text = "&W完成";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 27;
            this.label9.Text = "客    户";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(6, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 14);
            this.label10.TabIndex = 28;
            this.label10.Text = "产品图号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(177, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 29;
            this.label11.Text = "产品名称";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(177, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 14);
            this.label12.TabIndex = 30;
            this.label12.Text = "数    量";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(177, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 14);
            this.label13.TabIndex = 31;
            this.label13.Text = "交 货 期";
            // 
            // cbxCust
            // 
            this.cbxCust.FormattingEnabled = true;
            this.cbxCust.Items.AddRange(new object[] {
            "德展自用",
            "精达信"});
            this.cbxCust.Location = new System.Drawing.Point(71, 89);
            this.cbxCust.Name = "cbxCust";
            this.cbxCust.Size = new System.Drawing.Size(80, 20);
            this.cbxCust.TabIndex = 3;
            this.cbxCust.Text = "德展自用";
            this.cbxCust.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxCust_KeyDown);
            // 
            // dtpDelivery
            // 
            this.dtpDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDelivery.Location = new System.Drawing.Point(246, 124);
            this.dtpDelivery.Name = "dtpDelivery";
            this.dtpDelivery.Size = new System.Drawing.Size(90, 21);
            this.dtpDelivery.TabIndex = 7;
            this.dtpDelivery.Value = new System.DateTime(2013, 11, 19, 0, 0, 0, 0);
            this.dtpDelivery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDelivery_KeyDown);
            // 
            // txtCompleteNum
            // 
            this.txtCompleteNum.Location = new System.Drawing.Point(597, 54);
            this.txtCompleteNum.Name = "txtCompleteNum";
            this.txtCompleteNum.Size = new System.Drawing.Size(71, 21);
            this.txtCompleteNum.TabIndex = 10;
            this.txtCompleteNum.TextChanged += new System.EventHandler(this.txtCompleteNum_TextChanged);
            this.txtCompleteNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompleteNum_KeyDown);
            // 
            // txtProcessNum
            // 
            this.txtProcessNum.Location = new System.Drawing.Point(597, 89);
            this.txtProcessNum.Name = "txtProcessNum";
            this.txtProcessNum.Size = new System.Drawing.Size(71, 21);
            this.txtProcessNum.TabIndex = 11;
            this.txtProcessNum.TextChanged += new System.EventHandler(this.txtProcessNum_TextChanged);
            this.txtProcessNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessNum_KeyDown);
            // 
            // txtPriceNo
            // 
            this.txtPriceNo.Location = new System.Drawing.Point(597, 124);
            this.txtPriceNo.Name = "txtPriceNo";
            this.txtPriceNo.Size = new System.Drawing.Size(71, 21);
            this.txtPriceNo.TabIndex = 12;
            this.txtPriceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPriceNo_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(6, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 38;
            this.label14.Text = "生产单号";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(71, 54);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(80, 21);
            this.txtOrderNo.TabIndex = 2;
            this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(568, 12);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(39, 21);
            this.txtPrice.TabIndex = 39;
            this.txtPrice.UseSystemPasswordChar = true;
            this.txtPrice.Visible = false;
            // 
            // txtSubsidy
            // 
            this.txtSubsidy.Location = new System.Drawing.Point(628, 12);
            this.txtSubsidy.Name = "txtSubsidy";
            this.txtSubsidy.Size = new System.Drawing.Size(39, 21);
            this.txtSubsidy.TabIndex = 40;
            this.txtSubsidy.UseSystemPasswordChar = true;
            this.txtSubsidy.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPageNo);
            this.panel1.Controls.Add(this.lblCheck);
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtOriWONo);
            this.panel1.Controls.Add(this.txtSubsidy);
            this.panel1.Controls.Add(this.txtPrice);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtOrderNo);
            this.panel1.Controls.Add(this.txtPriceNo);
            this.panel1.Controls.Add(this.txtProcessNum);
            this.panel1.Controls.Add(this.txtCompleteNum);
            this.panel1.Controls.Add(this.dtpDelivery);
            this.panel1.Controls.Add(this.cbxCust);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnFinish);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.txtTeam);
            this.panel1.Controls.Add(this.txtCraft);
            this.panel1.Controls.Add(this.txtSn);
            this.panel1.Controls.Add(this.txtOrderQt);
            this.panel1.Controls.Add(this.txtPartName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPartNo);
            this.panel1.Controls.Add(this.txtWONo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 162);
            this.panel1.TabIndex = 41;
            // 
            // txtPageNo
            // 
            this.txtPageNo.Location = new System.Drawing.Point(448, 12);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.ReadOnly = true;
            this.txtPageNo.Size = new System.Drawing.Size(79, 21);
            this.txtPageNo.TabIndex = 45;
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.BackColor = System.Drawing.Color.Gold;
            this.lblCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCheck.Font = new System.Drawing.Font("楷体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCheck.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCheck.Location = new System.Drawing.Point(672, 0);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(169, 50);
            this.lblCheck.TabIndex = 44;
            this.lblCheck.Text = "已核对";
            this.lblCheck.Visible = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(690, 122);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(61, 28);
            this.btnCheck.TabIndex = 15;
            this.btnCheck.Text = "&C核对";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(252, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 42;
            this.label8.Text = "借用工单号";
            // 
            // txtOriWONo
            // 
            this.txtOriWONo.Location = new System.Drawing.Point(356, 12);
            this.txtOriWONo.Name = "txtOriWONo";
            this.txtOriWONo.Size = new System.Drawing.Size(79, 21);
            this.txtOriWONo.TabIndex = 1;
            this.txtOriWONo.TextChanged += new System.EventHandler(this.txtOriWONo_TextChanged);
            this.txtOriWONo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOriWONo_KeyDown);
            // 
            // FrmContructionHandwriting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(841, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmContructionHandwriting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手写工单录入";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtWONo;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPartName;
        private System.Windows.Forms.TextBox txtOrderQt;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.TextBox txtCraft;
        private System.Windows.Forms.TextBox txtTeam;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxCust;
        private System.Windows.Forms.DateTimePicker dtpDelivery;
        private System.Windows.Forms.TextBox txtCompleteNum;
        private System.Windows.Forms.TextBox txtProcessNum;
        private System.Windows.Forms.TextBox txtPriceNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSubsidy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOriWONo;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.TextBox txtPageNo;
    }
}