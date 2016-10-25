namespace LYH.WorkOrder
{
    partial class FrmConstructionEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConstructionEntry));
            this.txtWONo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.txtCraft = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCompleteNum = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnIns = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.txtProcessNum = new System.Windows.Forms.TextBox();
            this.txtOrderQt = new System.Windows.Forms.TextBox();
            this.txtPageNo = new System.Windows.Forms.TextBox();
            this.txtDeliveryDate = new System.Windows.Forms.TextBox();
            this.txtCust = new System.Windows.Forms.TextBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCheck = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSubsidy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSumQt = new System.Windows.Forms.Label();
            this.txtPriceNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTeam = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtWONo
            // 
            this.txtWONo.Location = new System.Drawing.Point(84, 6);
            this.txtWONo.Name = "txtWONo";
            this.txtWONo.Size = new System.Drawing.Size(100, 21);
            this.txtWONo.TabIndex = 0;
            this.txtWONo.TextChanged += new System.EventHandler(this.txtWONo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "工单号";
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(84, 34);
            this.txtSn.Name = "txtSn";
            this.txtSn.Size = new System.Drawing.Size(100, 21);
            this.txtSn.TabIndex = 1;
            this.txtSn.TextChanged += new System.EventHandler(this.txtSn_TextChanged);
            this.txtSn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSn_KeyDown);
            // 
            // txtCraft
            // 
            this.txtCraft.Location = new System.Drawing.Point(84, 62);
            this.txtCraft.Name = "txtCraft";
            this.txtCraft.Size = new System.Drawing.Size(100, 21);
            this.txtCraft.TabIndex = 2;
            this.txtCraft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCraft_KeyDown);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1179, 457);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "工 序 号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "生产班组";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "完成数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 18;
            this.label5.Text = "加 工 数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(7, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 18;
            this.label6.Text = "序价代码";
            // 
            // txtCompleteNum
            // 
            this.txtCompleteNum.Location = new System.Drawing.Point(84, 118);
            this.txtCompleteNum.Name = "txtCompleteNum";
            this.txtCompleteNum.Size = new System.Drawing.Size(100, 21);
            this.txtCompleteNum.TabIndex = 4;
            this.txtCompleteNum.TextChanged += new System.EventHandler(this.txtCompleteNum_TextChanged);
            this.txtCompleteNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompleteNum_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(208, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 28);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&S保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnIns
            // 
            this.btnIns.Location = new System.Drawing.Point(208, 36);
            this.btnIns.Name = "btnIns";
            this.btnIns.Size = new System.Drawing.Size(61, 28);
            this.btnIns.TabIndex = 10;
            this.btnIns.TabStop = false;
            this.btnIns.Text = "&Q插入";
            this.btnIns.UseVisualStyleBackColor = true;
            this.btnIns.Click += new System.EventHandler(this.btnIns_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Location = new System.Drawing.Point(208, 70);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(61, 28);
            this.btnUpd.TabIndex = 9;
            this.btnUpd.TabStop = false;
            this.btnUpd.Text = "&X修改";
            this.btnUpd.UseVisualStyleBackColor = true;
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(208, 104);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(61, 28);
            this.btnDel.TabIndex = 8;
            this.btnDel.TabStop = false;
            this.btnDel.Text = "&D删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // txtProcessNum
            // 
            this.txtProcessNum.Location = new System.Drawing.Point(84, 146);
            this.txtProcessNum.Name = "txtProcessNum";
            this.txtProcessNum.Size = new System.Drawing.Size(100, 21);
            this.txtProcessNum.TabIndex = 5;
            this.txtProcessNum.TextChanged += new System.EventHandler(this.txtProcessNum_TextChanged);
            this.txtProcessNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessNum_KeyDown);
            // 
            // txtOrderQt
            // 
            this.txtOrderQt.BackColor = System.Drawing.Color.LightGreen;
            this.txtOrderQt.Location = new System.Drawing.Point(80, 145);
            this.txtOrderQt.Name = "txtOrderQt";
            this.txtOrderQt.ReadOnly = true;
            this.txtOrderQt.Size = new System.Drawing.Size(178, 21);
            this.txtOrderQt.TabIndex = 21;
            this.txtOrderQt.TabStop = false;
            // 
            // txtPageNo
            // 
            this.txtPageNo.Location = new System.Drawing.Point(80, 124);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.ReadOnly = true;
            this.txtPageNo.Size = new System.Drawing.Size(178, 21);
            this.txtPageNo.TabIndex = 22;
            this.txtPageNo.TabStop = false;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.Location = new System.Drawing.Point(80, 103);
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.ReadOnly = true;
            this.txtDeliveryDate.Size = new System.Drawing.Size(178, 21);
            this.txtDeliveryDate.TabIndex = 23;
            this.txtDeliveryDate.TabStop = false;
            // 
            // txtCust
            // 
            this.txtCust.Location = new System.Drawing.Point(80, 19);
            this.txtCust.Name = "txtCust";
            this.txtCust.ReadOnly = true;
            this.txtCust.Size = new System.Drawing.Size(368, 21);
            this.txtCust.TabIndex = 17;
            this.txtCust.TabStop = false;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(80, 40);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.ReadOnly = true;
            this.txtOrderNo.Size = new System.Drawing.Size(368, 21);
            this.txtOrderNo.TabIndex = 18;
            this.txtOrderNo.TabStop = false;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(80, 61);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.ReadOnly = true;
            this.txtPartNo.Size = new System.Drawing.Size(368, 21);
            this.txtPartNo.TabIndex = 19;
            this.txtPartNo.TabStop = false;
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(80, 82);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.ReadOnly = true;
            this.txtPartName.Size = new System.Drawing.Size(368, 21);
            this.txtPartName.TabIndex = 20;
            this.txtPartName.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblCheck);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtSubsidy);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lblSumQt);
            this.groupBox1.Controls.Add(this.txtCust);
            this.groupBox1.Controls.Add(this.txtOrderNo);
            this.groupBox1.Controls.Add(this.txtDeliveryDate);
            this.groupBox1.Controls.Add(this.txtPartNo);
            this.groupBox1.Controls.Add(this.txtPageNo);
            this.groupBox1.Controls.Add(this.txtPartName);
            this.groupBox1.Controls.Add(this.txtOrderQt);
            this.groupBox1.Location = new System.Drawing.Point(301, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 196);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生产单资料";
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.BackColor = System.Drawing.Color.Gold;
            this.lblCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCheck.Font = new System.Drawing.Font("楷体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCheck.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCheck.Location = new System.Drawing.Point(279, 120);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(169, 50);
            this.lblCheck.TabIndex = 32;
            this.lblCheck.Text = "已核对";
            this.lblCheck.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(11, 127);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 31;
            this.label15.Text = "页    码";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(267, 171);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(100, 21);
            this.txtPrice.TabIndex = 30;
            this.txtPrice.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(11, 106);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 14);
            this.label16.TabIndex = 29;
            this.label16.Text = "交 货 期";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(11, 148);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 30;
            this.label17.Text = "下单数量";
            // 
            // txtSubsidy
            // 
            this.txtSubsidy.Location = new System.Drawing.Point(373, 171);
            this.txtSubsidy.Name = "txtSubsidy";
            this.txtSubsidy.ReadOnly = true;
            this.txtSubsidy.Size = new System.Drawing.Size(100, 21);
            this.txtSubsidy.TabIndex = 31;
            this.txtSubsidy.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(11, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 27;
            this.label11.Text = "产品名称";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(11, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 14);
            this.label12.TabIndex = 28;
            this.label12.Text = "产品图号";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(11, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 14);
            this.label13.TabIndex = 25;
            this.label13.Text = "生产单号";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(11, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 26;
            this.label14.Text = "客    户";
            // 
            // lblSumQt
            // 
            this.lblSumQt.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSumQt.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblSumQt.Location = new System.Drawing.Point(80, 168);
            this.lblSumQt.Name = "lblSumQt";
            this.lblSumQt.Size = new System.Drawing.Size(178, 25);
            this.lblSumQt.TabIndex = 24;
            this.lblSumQt.Tag = "";
            this.lblSumQt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPriceNo
            // 
            this.txtPriceNo.BackColor = System.Drawing.Color.White;
            this.txtPriceNo.Location = new System.Drawing.Point(84, 174);
            this.txtPriceNo.Name = "txtPriceNo";
            this.txtPriceNo.Size = new System.Drawing.Size(100, 21);
            this.txtPriceNo.TabIndex = 6;
            this.txtPriceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPriceNo_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(0, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1185, 477);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工序明细";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(208, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(61, 28);
            this.btnNext.TabIndex = 11;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "&A下一单";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(7, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "工序名称";
            // 
            // txtTeam
            // 
            this.txtTeam.Location = new System.Drawing.Point(84, 89);
            this.txtTeam.Name = "txtTeam";
            this.txtTeam.Size = new System.Drawing.Size(100, 21);
            this.txtTeam.TabIndex = 3;
            this.txtTeam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTeam_KeyDown);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(208, 172);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(61, 28);
            this.btnCheck.TabIndex = 32;
            this.btnCheck.Text = "&C核对";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Controls.Add(this.txtTeam);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.txtPriceNo);
            this.panel1.Controls.Add(this.txtProcessNum);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnUpd);
            this.panel1.Controls.Add(this.btnIns);
            this.panel1.Controls.Add(this.txtCompleteNum);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCraft);
            this.panel1.Controls.Add(this.txtSn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtWONo);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 202);
            this.panel1.TabIndex = 33;
            // 
            // FrmConstructionEntry
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmConstructionEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工单录入";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmWin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtWONo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.TextBox txtCraft;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCompleteNum;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnIns;
        private System.Windows.Forms.Button btnUpd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox txtProcessNum;
        private System.Windows.Forms.TextBox txtOrderQt;
        private System.Windows.Forms.TextBox txtPageNo;
        private System.Windows.Forms.TextBox txtDeliveryDate;
        private System.Windows.Forms.TextBox txtCust;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtPartName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtPriceNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSubsidy;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSumQt;
        private System.Windows.Forms.TextBox txtTeam;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Panel panel1;
    }
}