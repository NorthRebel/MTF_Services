namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    partial class RegisterNewIdle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterNewIdle));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_AddToUsed = new System.Windows.Forms.Button();
            this.btn_RemoveFromUsed = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_EndHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_EndMinutes = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_Begin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_BeginHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_BeginMinutes = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_DurationType = new System.Windows.Forms.ComboBox();
            this.textBox_Duration = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.idleReasonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.idleTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dg_UsedUser = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvalibleUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dg_AvalibleUser = new System.Windows.Forms.DataGridView();
            this.tabNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.UsedUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndMinutes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeginHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeginMinutes)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idleReasonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idleTypeBindingSource)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_UsedUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvalibleUserBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_AvalibleUser)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsedUserBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_AddToUsed
            // 
            this.btn_AddToUsed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_AddToUsed.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddToUsed.Location = new System.Drawing.Point(3, 66);
            this.btn_AddToUsed.Name = "btn_AddToUsed";
            this.btn_AddToUsed.Size = new System.Drawing.Size(38, 93);
            this.btn_AddToUsed.TabIndex = 0;
            this.btn_AddToUsed.Text = "->";
            this.toolTip.SetToolTip(this.btn_AddToUsed, "Добавить в задействованным");
            this.btn_AddToUsed.UseVisualStyleBackColor = true;
            this.btn_AddToUsed.Click += new System.EventHandler(this.btn_AddToUsed_Click);
            // 
            // btn_RemoveFromUsed
            // 
            this.btn_RemoveFromUsed.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_RemoveFromUsed.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveFromUsed.Location = new System.Drawing.Point(3, 165);
            this.btn_RemoveFromUsed.Name = "btn_RemoveFromUsed";
            this.btn_RemoveFromUsed.Size = new System.Drawing.Size(38, 93);
            this.btn_RemoveFromUsed.TabIndex = 1;
            this.btn_RemoveFromUsed.Text = "<-";
            this.toolTip.SetToolTip(this.btn_RemoveFromUsed, "Удалить из задействованных");
            this.btn_RemoveFromUsed.UseVisualStyleBackColor = true;
            this.btn_RemoveFromUsed.Click += new System.EventHandler(this.btn_RemoveFromUsed_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.90295F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.90295F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.940275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.940275F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.31355F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 624);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.70646F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.81018F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.28767F));
            this.tableLayoutPanel5.Controls.Add(this.btn_OK, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Cancel, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 581);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(618, 40);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OK.Location = new System.Drawing.Point(475, 4);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(141, 31);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "Ок";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Cancel.Location = new System.Drawing.Point(309, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(162, 31);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 68);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Окончание";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dateTimePicker_End, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown_EndHours, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown_EndMinutes, 5, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(612, 40);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 40);
            this.label4.TabIndex = 0;
            this.label4.Text = "Дата";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(52, 3);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(200, 29);
            this.dateTimePicker_End.TabIndex = 1;
            this.dateTimePicker_End.ValueChanged += new System.EventHandler(this.dateTimePicker_End_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(258, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 40);
            this.label5.TabIndex = 2;
            this.label5.Text = "часы";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(427, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 40);
            this.label6.TabIndex = 3;
            this.label6.Text = "минуты";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_EndHours
            // 
            this.numericUpDown_EndHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_EndHours.Location = new System.Drawing.Point(309, 3);
            this.numericUpDown_EndHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDown_EndHours.Name = "numericUpDown_EndHours";
            this.numericUpDown_EndHours.Size = new System.Drawing.Size(112, 29);
            this.numericUpDown_EndHours.TabIndex = 4;
            this.numericUpDown_EndHours.ValueChanged += new System.EventHandler(this.numericUpDown_EndHours_ValueChanged);
            // 
            // numericUpDown_EndMinutes
            // 
            this.numericUpDown_EndMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_EndMinutes.Location = new System.Drawing.Point(496, 3);
            this.numericUpDown_EndMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDown_EndMinutes.Name = "numericUpDown_EndMinutes";
            this.numericUpDown_EndMinutes.Size = new System.Drawing.Size(113, 29);
            this.numericUpDown_EndMinutes.TabIndex = 5;
            this.numericUpDown_EndMinutes.ValueChanged += new System.EventHandler(this.numericUpDown_EndMinutes_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Начало";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker_Begin, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown_BeginHours, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown_BeginMinutes, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(612, 40);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker_Begin
            // 
            this.dateTimePicker_Begin.Location = new System.Drawing.Point(52, 3);
            this.dateTimePicker_Begin.Name = "dateTimePicker_Begin";
            this.dateTimePicker_Begin.Size = new System.Drawing.Size(200, 29);
            this.dateTimePicker_Begin.TabIndex = 1;
            this.dateTimePicker_Begin.ValueChanged += new System.EventHandler(this.dateTimePicker_Begin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(258, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "часы";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(427, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "минуты";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_BeginHours
            // 
            this.numericUpDown_BeginHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_BeginHours.Location = new System.Drawing.Point(309, 3);
            this.numericUpDown_BeginHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDown_BeginHours.Name = "numericUpDown_BeginHours";
            this.numericUpDown_BeginHours.Size = new System.Drawing.Size(112, 29);
            this.numericUpDown_BeginHours.TabIndex = 4;
            this.numericUpDown_BeginHours.ValueChanged += new System.EventHandler(this.numericUpDown_BeginHours_ValueChanged);
            // 
            // numericUpDown_BeginMinutes
            // 
            this.numericUpDown_BeginMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_BeginMinutes.Location = new System.Drawing.Point(496, 3);
            this.numericUpDown_BeginMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDown_BeginMinutes.Name = "numericUpDown_BeginMinutes";
            this.numericUpDown_BeginMinutes.Size = new System.Drawing.Size(113, 29);
            this.numericUpDown_BeginMinutes.TabIndex = 5;
            this.numericUpDown_BeginMinutes.ValueChanged += new System.EventHandler(this.numericUpDown_BeginMinutes_ValueChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.67232F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.32768F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.comboBox_DurationType, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBox_Duration, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 151);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(618, 39);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(232, 39);
            this.label7.TabIndex = 0;
            this.label7.Text = "Продолжительность";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_DurationType
            // 
            this.comboBox_DurationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_DurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DurationType.FormattingEnabled = true;
            this.comboBox_DurationType.Items.AddRange(new object[] {
            "Минуты",
            "Часы"});
            this.comboBox_DurationType.Location = new System.Drawing.Point(241, 3);
            this.comboBox_DurationType.Name = "comboBox_DurationType";
            this.comboBox_DurationType.Size = new System.Drawing.Size(200, 29);
            this.comboBox_DurationType.TabIndex = 1;
            this.comboBox_DurationType.SelectedIndexChanged += new System.EventHandler(this.comboBox_DurationType_SelectedIndexChanged);
            // 
            // textBox_Duration
            // 
            this.textBox_Duration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Duration.Location = new System.Drawing.Point(447, 3);
            this.textBox_Duration.Name = "textBox_Duration";
            this.textBox_Duration.ReadOnly = true;
            this.textBox_Duration.Size = new System.Drawing.Size(168, 29);
            this.textBox_Duration.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.comboBox2, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 196);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(618, 39);
            this.tableLayoutPanel6.TabIndex = 7;
            // 
            // comboBox2
            // 
            this.comboBox2.DataSource = this.idleReasonBindingSource;
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(372, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(243, 29);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.ValueMember = "Id";
            // 
            // idleReasonBindingSource
            // 
            this.idleReasonBindingSource.DataSource = typeof(MTF_Services.Model.IdleReason);
            this.idleReasonBindingSource.CurrentChanged += new System.EventHandler(this.idleReasonBindingSource_CurrentChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 39);
            this.label8.TabIndex = 0;
            this.label8.Text = "Тип";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.idleTypeBindingSource;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(44, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(242, 29);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.ValueMember = "Id";
            // 
            // idleTypeBindingSource
            // 
            this.idleTypeBindingSource.DataSource = typeof(MTF_Services.Model.IdleType);
            this.idleTypeBindingSource.CurrentChanged += new System.EventHandler(this.idleTypeBindingSource_CurrentChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(292, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 39);
            this.label9.TabIndex = 2;
            this.label9.Text = "Причина";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 241);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(618, 331);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dg_UsedUser);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(337, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 325);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Задействованный персонал";
            // 
            // dg_UsedUser
            // 
            this.dg_UsedUser.AllowUserToAddRows = false;
            this.dg_UsedUser.AllowUserToDeleteRows = false;
            this.dg_UsedUser.AutoGenerateColumns = false;
            this.dg_UsedUser.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_UsedUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_UsedUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dg_UsedUser.DataSource = this.AvalibleUserBindingSource;
            this.dg_UsedUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_UsedUser.Location = new System.Drawing.Point(3, 25);
            this.dg_UsedUser.Name = "dg_UsedUser";
            this.dg_UsedUser.ReadOnly = true;
            this.dg_UsedUser.RowHeadersVisible = false;
            this.dg_UsedUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_UsedUser.Size = new System.Drawing.Size(272, 297);
            this.dg_UsedUser.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TabNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "Таб №";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Fio";
            this.dataGridViewTextBoxColumn2.HeaderText = "ФИО";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // AvalibleUserBindingSource
            // 
            this.AvalibleUserBindingSource.DataSource = typeof(MTF_Services.Model.User);
            this.AvalibleUserBindingSource.CurrentChanged += new System.EventHandler(this.AvalibleUserBindingSource_CurrentChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dg_AvalibleUser);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 325);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Доступный персонал";
            // 
            // dg_AvalibleUser
            // 
            this.dg_AvalibleUser.AllowUserToAddRows = false;
            this.dg_AvalibleUser.AllowUserToDeleteRows = false;
            this.dg_AvalibleUser.AutoGenerateColumns = false;
            this.dg_AvalibleUser.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_AvalibleUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_AvalibleUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tabNoDataGridViewTextBoxColumn,
            this.Fio});
            this.dg_AvalibleUser.DataSource = this.AvalibleUserBindingSource;
            this.dg_AvalibleUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_AvalibleUser.Location = new System.Drawing.Point(3, 25);
            this.dg_AvalibleUser.Name = "dg_AvalibleUser";
            this.dg_AvalibleUser.ReadOnly = true;
            this.dg_AvalibleUser.RowHeadersVisible = false;
            this.dg_AvalibleUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_AvalibleUser.Size = new System.Drawing.Size(272, 297);
            this.dg_AvalibleUser.TabIndex = 5;
            // 
            // tabNoDataGridViewTextBoxColumn
            // 
            this.tabNoDataGridViewTextBoxColumn.DataPropertyName = "TabNo";
            this.tabNoDataGridViewTextBoxColumn.HeaderText = "Таб №";
            this.tabNoDataGridViewTextBoxColumn.Name = "tabNoDataGridViewTextBoxColumn";
            this.tabNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Fio
            // 
            this.Fio.DataPropertyName = "Fio";
            this.Fio.HeaderText = "ФИО";
            this.Fio.Name = "Fio";
            this.Fio.ReadOnly = true;
            this.Fio.Width = 150;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.btn_RemoveFromUsed, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.btn_AddToUsed, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(287, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(44, 325);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // UsedUserBindingSource
            // 
            this.UsedUserBindingSource.DataSource = typeof(MTF_Services.Model.User);
            this.UsedUserBindingSource.CurrentChanged += new System.EventHandler(this.UsedUserBindingSource_CurrentChanged);
            // 
            // RegisterNewIdle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 624);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "RegisterNewIdle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация нового простоя";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndMinutes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeginHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeginMinutes)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idleReasonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idleTypeBindingSource)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_UsedUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvalibleUserBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_AvalibleUser)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UsedUserBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_EndHours;
        private System.Windows.Forms.NumericUpDown numericUpDown_EndMinutes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Begin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_BeginHours;
        private System.Windows.Forms.NumericUpDown numericUpDown_BeginMinutes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_DurationType;
        private System.Windows.Forms.TextBox textBox_Duration;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button btn_RemoveFromUsed;
        private System.Windows.Forms.Button btn_AddToUsed;
        private System.Windows.Forms.BindingSource idleReasonBindingSource;
        private System.Windows.Forms.BindingSource idleTypeBindingSource;
        private System.Windows.Forms.BindingSource AvalibleUserBindingSource;
        private System.Windows.Forms.BindingSource UsedUserBindingSource;
        private System.Windows.Forms.DataGridView dg_AvalibleUser;
        private System.Windows.Forms.DataGridView dg_UsedUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn tabNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fio;
    }
}