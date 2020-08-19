namespace LYH.WorkOrder
{
    public partial class FrmProcCardInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProcCardInput));
            this.txtProcCardNo = new System.Windows.Forms.TextBox();
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.txtProcessSeq = new System.Windows.Forms.TextBox();
            this.txtProcessQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtAdjust = new System.Windows.Forms.TextBox();
            this.txtSingle = new System.Windows.Forms.TextBox();
            this.txtSeqPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSubsidy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtOrderQty = new System.Windows.Forms.TextBox();
            this.btnIns = new System.Windows.Forms.Button();
            this.ssbTeam = new SpellSearchBoxEx.SpellSearchBoxEx();
            this.ssbProcesser = new SpellSearchBoxEx.SpellSearchBoxEx();
            this.ssbDebugger = new SpellSearchBoxEx.SpellSearchBoxEx();
            this.label13 = new System.Windows.Forms.Label();
            this.ssbCraft = new SpellSearchBoxEx.SpellSearchBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProcCardNo
            // 
            this.txtProcCardNo.Location = new System.Drawing.Point(79, 5);
            this.txtProcCardNo.Name = "txtProcCardNo";
            this.txtProcCardNo.Size = new System.Drawing.Size(99, 21);
            this.txtProcCardNo.TabIndex = 0;
            this.txtProcCardNo.TextChanged += new System.EventHandler(this.txtProcCardNo_TextChanged);
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(79, 26);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(99, 21);
            this.txtSeq.TabIndex = 1;
            this.txtSeq.TextChanged += new System.EventHandler(this.txtSeq_TextChanged);
            this.txtSeq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeq_KeyDown);
            // 
            // txtProcessSeq
            // 
            this.txtProcessSeq.Location = new System.Drawing.Point(79, 47);
            this.txtProcessSeq.Name = "txtProcessSeq";
            this.txtProcessSeq.Size = new System.Drawing.Size(99, 21);
            this.txtProcessSeq.TabIndex = 2;
            this.txtProcessSeq.TextChanged += new System.EventHandler(this.txtProcessSeq_TextChanged);
            this.txtProcessSeq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessSeq_KeyDown);
            // 
            // txtProcessQty
            // 
            this.txtProcessQty.Location = new System.Drawing.Point(79, 89);
            this.txtProcessQty.Name = "txtProcessQty";
            this.txtProcessQty.Size = new System.Drawing.Size(99, 21);
            this.txtProcessQty.TabIndex = 4;
            this.txtProcessQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessQty_KeyDown);
            this.txtProcessQty.Leave += new System.EventHandler(this.txtProcessQty_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "工艺卡号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(38, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "序号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "加工工序";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(10, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "加工数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(10, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "加工班组";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(184, 65);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 28);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&S保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Location = new System.Drawing.Point(184, 95);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(61, 28);
            this.btnUpd.TabIndex = 10;
            this.btnUpd.Text = "&X修改";
            this.btnUpd.UseVisualStyleBackColor = true;
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(184, 125);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(61, 28);
            this.btnDel.TabIndex = 11;
            this.btnDel.Text = "&D删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(184, 5);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(61, 28);
            this.btnFinish.TabIndex = 12;
            this.btnFinish.Text = "&A下一单";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(562, 204);
            this.dataGridView1.TabIndex = 14;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 210);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1010, 430);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(10, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 16;
            this.label6.Text = "完成日期";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(79, 173);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(99, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // txtAdjust
            // 
            this.txtAdjust.Location = new System.Drawing.Point(326, 9);
            this.txtAdjust.Name = "txtAdjust";
            this.txtAdjust.Size = new System.Drawing.Size(100, 21);
            this.txtAdjust.TabIndex = 21;
            // 
            // txtSingle
            // 
            this.txtSingle.Location = new System.Drawing.Point(326, 29);
            this.txtSingle.Name = "txtSingle";
            this.txtSingle.Size = new System.Drawing.Size(100, 21);
            this.txtSingle.TabIndex = 22;
            // 
            // txtSeqPrice
            // 
            this.txtSeqPrice.Location = new System.Drawing.Point(326, 49);
            this.txtSeqPrice.Name = "txtSeqPrice";
            this.txtSeqPrice.Size = new System.Drawing.Size(100, 21);
            this.txtSeqPrice.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(257, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "调机时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(257, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 14);
            this.label8.TabIndex = 25;
            this.label8.Text = "单件时间";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(285, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 26;
            this.label9.Text = "序价";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(285, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 27;
            this.label10.Text = "补助";
            // 
            // txtSubsidy
            // 
            this.txtSubsidy.Location = new System.Drawing.Point(326, 69);
            this.txtSubsidy.Name = "txtSubsidy";
            this.txtSubsidy.Size = new System.Drawing.Size(100, 21);
            this.txtSubsidy.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(24, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 31;
            this.label11.Text = "生产员";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(24, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 33;
            this.label12.Text = "调机员";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(448, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 204);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtId);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.txtOrderQty);
            this.panel2.Controls.Add(this.btnIns);
            this.panel2.Controls.Add(this.ssbTeam);
            this.panel2.Controls.Add(this.ssbProcesser);
            this.panel2.Controls.Add(this.ssbDebugger);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btnFinish);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Controls.Add(this.btnUpd);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtProcessQty);
            this.panel2.Controls.Add(this.ssbCraft);
            this.panel2.Controls.Add(this.txtProcessSeq);
            this.panel2.Controls.Add(this.txtSeq);
            this.panel2.Controls.Add(this.txtProcCardNo);
            this.panel2.Controls.Add(this.txtSubsidy);
            this.panel2.Controls.Add(this.txtSeqPrice);
            this.panel2.Controls.Add(this.txtSingle);
            this.panel2.Controls.Add(this.txtAdjust);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 204);
            this.panel2.TabIndex = 0;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(260, 125);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(166, 21);
            this.txtId.TabIndex = 36;
            this.txtId.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(257, 97);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 35;
            this.label17.Text = "工单数量";
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.BackColor = System.Drawing.Color.LightGreen;
            this.txtOrderQty.Location = new System.Drawing.Point(326, 95);
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.ReadOnly = true;
            this.txtOrderQty.Size = new System.Drawing.Size(100, 21);
            this.txtOrderQty.TabIndex = 34;
            this.txtOrderQty.TabStop = false;
            // 
            // btnIns
            // 
            this.btnIns.Location = new System.Drawing.Point(184, 35);
            this.btnIns.Name = "btnIns";
            this.btnIns.Size = new System.Drawing.Size(61, 28);
            this.btnIns.TabIndex = 13;
            this.btnIns.TabStop = false;
            this.btnIns.Text = "&Q插入";
            this.btnIns.UseVisualStyleBackColor = true;
            this.btnIns.Click += new System.EventHandler(this.btnIns_Click);
            // 
            // ssbTeam
            // 
            this.ssbTeam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ssbTeam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ssbTeam.Location = new System.Drawing.Point(79, 110);
            this.ssbTeam.MaxItemCount = 10;
            this.ssbTeam.Name = "ssbTeam";
            this.ssbTeam.SearchMode = SpellSearchBoxEx.SearchMode.Contains;
            this.ssbTeam.Size = new System.Drawing.Size(99, 21);
            this.ssbTeam.SpellSearchSource = null;
            this.ssbTeam.TabIndex = 5;
            this.ssbTeam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ssbTeam_KeyDown);
            this.ssbTeam.Leave += new System.EventHandler(this.ssbTeam_Leave);
            // 
            // ssbProcesser
            // 
            this.ssbProcesser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ssbProcesser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ssbProcesser.Location = new System.Drawing.Point(79, 152);
            this.ssbProcesser.MaxItemCount = 10;
            this.ssbProcesser.Name = "ssbProcesser";
            this.ssbProcesser.SearchMode = SpellSearchBoxEx.SearchMode.Contains;
            this.ssbProcesser.Size = new System.Drawing.Size(99, 21);
            this.ssbProcesser.SpellSearchSource = null;
            this.ssbProcesser.TabIndex = 7;
            // 
            // ssbDebugger
            // 
            this.ssbDebugger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ssbDebugger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ssbDebugger.Location = new System.Drawing.Point(79, 131);
            this.ssbDebugger.MaxItemCount = 10;
            this.ssbDebugger.Name = "ssbDebugger";
            this.ssbDebugger.SearchMode = SpellSearchBoxEx.SearchMode.Contains;
            this.ssbDebugger.Size = new System.Drawing.Size(99, 21);
            this.ssbDebugger.SpellSearchSource = null;
            this.ssbDebugger.TabIndex = 6;
            this.ssbDebugger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ssbDebugger_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(10, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 14);
            this.label13.TabIndex = 7;
            this.label13.Text = "工序名称";
            // 
            // ssbCraft
            // 
            this.ssbCraft.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ssbCraft.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ssbCraft.Location = new System.Drawing.Point(79, 68);
            this.ssbCraft.MaxItemCount = 5;
            this.ssbCraft.Name = "ssbCraft";
            this.ssbCraft.SearchMode = SpellSearchBoxEx.SearchMode.Contains;
            this.ssbCraft.Size = new System.Drawing.Size(99, 21);
            this.ssbCraft.SpellSearchSource = null;
            this.ssbCraft.TabIndex = 3;
            this.ssbCraft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ssbCraft_KeyDown);
            // 
            // FrmProcCardInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1010, 640);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmProcCardInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工艺卡录入";
            this.Load += new System.EventHandler(this.FrmProcCardEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtProcCardNo;
        private System.Windows.Forms.TextBox txtSeq;
        private System.Windows.Forms.TextBox txtProcessSeq;
        private System.Windows.Forms.TextBox txtProcessQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtAdjust;
        private System.Windows.Forms.TextBox txtSingle;
        private System.Windows.Forms.TextBox txtSeqPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSubsidy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private SpellSearchBoxEx.SpellSearchBoxEx ssbTeam;
        private SpellSearchBoxEx.SpellSearchBoxEx ssbDebugger;
        private SpellSearchBoxEx.SpellSearchBoxEx ssbProcesser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnIns;
        private System.Windows.Forms.Label label13;
        private SpellSearchBoxEx.SpellSearchBoxEx ssbCraft;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtId;
    }
}