namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary
{
    partial class EditCPUSocketForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCPUSocketForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picBtn_ClearCondition = new System.Windows.Forms.PictureBox();
            this.picBtn_FindByCondition = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_ManufacturersToFind = new System.Windows.Forms.ComboBox();
            this.manufaturerToFindBS = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ConditionToFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.but_Select = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cpuSocketBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.manufacturerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.socketDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPUSocketInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_ClearCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_FindByCondition)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manufaturerToFindBS)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpuSocketBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manufacturerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPUSocketInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // picBtn_ClearCondition
            // 
            this.picBtn_ClearCondition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn_ClearCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_ClearCondition.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_ClearCondition.Location = new System.Drawing.Point(582, 12);
            this.picBtn_ClearCondition.Name = "picBtn_ClearCondition";
            this.picBtn_ClearCondition.Size = new System.Drawing.Size(30, 30);
            this.picBtn_ClearCondition.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_ClearCondition.TabIndex = 22;
            this.picBtn_ClearCondition.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_ClearCondition, "Отмена");
            this.picBtn_ClearCondition.Click += new System.EventHandler(this.picBtn_ClearCondition_Click);
            // 
            // picBtn_FindByCondition
            // 
            this.picBtn_FindByCondition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn_FindByCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_FindByCondition.Image = global::MTF_Services.WinForms.Properties.Resources.search;
            this.picBtn_FindByCondition.Location = new System.Drawing.Point(540, 12);
            this.picBtn_FindByCondition.Name = "picBtn_FindByCondition";
            this.picBtn_FindByCondition.Size = new System.Drawing.Size(30, 30);
            this.picBtn_FindByCondition.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_FindByCondition.TabIndex = 21;
            this.picBtn_FindByCondition.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_FindByCondition, "Поиск");
            this.picBtn_FindByCondition.Click += new System.EventHandler(this.picBtn_FindByCondition_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_ManufacturersToFind);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_ConditionToFind);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.picBtn_ClearCondition);
            this.panel1.Controls.Add(this.picBtn_FindByCondition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 55);
            this.panel1.TabIndex = 1;
            // 
            // comboBox_ManufacturersToFind
            // 
            this.comboBox_ManufacturersToFind.DataSource = this.manufaturerToFindBS;
            this.comboBox_ManufacturersToFind.DisplayMember = "Name";
            this.comboBox_ManufacturersToFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ManufacturersToFind.FormattingEnabled = true;
            this.comboBox_ManufacturersToFind.Location = new System.Drawing.Point(400, 13);
            this.comboBox_ManufacturersToFind.Name = "comboBox_ManufacturersToFind";
            this.comboBox_ManufacturersToFind.Size = new System.Drawing.Size(134, 29);
            this.comboBox_ManufacturersToFind.TabIndex = 26;
            this.comboBox_ManufacturersToFind.ValueMember = "Id";
            // 
            // manufaturerToFindBS
            // 
            this.manufaturerToFindBS.DataSource = typeof(MTF_Services.Model.Manufacturer);
            this.manufaturerToFindBS.CurrentChanged += new System.EventHandler(this.manufaturerToFindBS_CurrentChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 25;
            this.label2.Text = "Производитель";
            // 
            // textBox_ConditionToFind
            // 
            this.textBox_ConditionToFind.Location = new System.Drawing.Point(135, 13);
            this.textBox_ConditionToFind.Name = "textBox_ConditionToFind";
            this.textBox_ConditionToFind.Size = new System.Drawing.Size(133, 29);
            this.textBox_ConditionToFind.TabIndex = 24;
            this.textBox_ConditionToFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 23;
            this.label1.Text = "Наименование";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.36459F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.63542F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.97436F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.02564F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 390);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(410, 310);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(211, 77);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "Сохранить";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.but_Select);
            this.panel2.Controls.Add(this.btn_Cancel);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(410, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(211, 301);
            this.panel2.TabIndex = 3;
            // 
            // but_Select
            // 
            this.but_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.but_Select.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.but_Select.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_Select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.but_Select.Location = new System.Drawing.Point(0, 270);
            this.but_Select.Name = "but_Select";
            this.but_Select.Size = new System.Drawing.Size(211, 31);
            this.but_Select.TabIndex = 4;
            this.but_Select.Text = "Выбрать";
            this.but_Select.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.but_Select.UseVisualStyleBackColor = true;
            this.but_Select.Visible = false;
            this.but_Select.Click += new System.EventHandler(this.but_Select_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Cancel.Enabled = false;
            this.btn_Cancel.Location = new System.Drawing.Point(0, 58);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(211, 29);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.Location = new System.Drawing.Point(0, 29);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(211, 29);
            this.button3.TabIndex = 1;
            this.button3.Text = "Редактировать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 29);
            this.button2.TabIndex = 0;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox2, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 310);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(401, 77);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "Производитель";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 21);
            this.label3.TabIndex = 24;
            this.label3.Text = "Наименование";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cpuSocketBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(203, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(195, 29);
            this.textBox2.TabIndex = 27;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // cpuSocketBindingSource
            // 
            this.cpuSocketBindingSource.DataSource = typeof(MTF_Services.Model.CpuSocket);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.cpuSocketBindingSource, "ManufacturerId", true));
            this.comboBox2.DataSource = this.manufacturerBindingSource;
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(203, 47);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(195, 29);
            this.comboBox2.TabIndex = 28;
            this.comboBox2.ValueMember = "Id";
            // 
            // manufacturerBindingSource
            // 
            this.manufacturerBindingSource.DataSource = typeof(MTF_Services.Model.Manufacturer);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.manufacturerDataGridViewTextBoxColumn,
            this.socketDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.cPUSocketInfoBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(401, 301);
            this.dataGridView1.TabIndex = 5;
            // 
            // manufacturerDataGridViewTextBoxColumn
            // 
            this.manufacturerDataGridViewTextBoxColumn.DataPropertyName = "Manufacturer";
            this.manufacturerDataGridViewTextBoxColumn.HeaderText = "Производитель";
            this.manufacturerDataGridViewTextBoxColumn.Name = "manufacturerDataGridViewTextBoxColumn";
            this.manufacturerDataGridViewTextBoxColumn.ReadOnly = true;
            this.manufacturerDataGridViewTextBoxColumn.Width = 160;
            // 
            // socketDataGridViewTextBoxColumn
            // 
            this.socketDataGridViewTextBoxColumn.DataPropertyName = "Socket";
            this.socketDataGridViewTextBoxColumn.HeaderText = "Разъем";
            this.socketDataGridViewTextBoxColumn.Name = "socketDataGridViewTextBoxColumn";
            this.socketDataGridViewTextBoxColumn.ReadOnly = true;
            this.socketDataGridViewTextBoxColumn.Width = 140;
            // 
            // cPUSocketInfoBindingSource
            // 
            this.cPUSocketInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.CPUSocketInfo);
            this.cPUSocketInfoBindingSource.CurrentChanged += new System.EventHandler(this.cPUSocketInfoBindingSource_CurrentChanged);
            // 
            // EditCPUSocketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "EditCPUSocketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование разъемов процессоров";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditCPUSocketForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_ClearCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_FindByCondition)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manufaturerToFindBS)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpuSocketBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manufacturerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPUSocketInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_ManufacturersToFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ConditionToFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBtn_ClearCondition;
        private System.Windows.Forms.PictureBox picBtn_FindByCondition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn socketDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource cPUSocketInfoBindingSource;
        private System.Windows.Forms.BindingSource cpuSocketBindingSource;
        private System.Windows.Forms.BindingSource manufacturerBindingSource;
        private System.Windows.Forms.Button but_Select;
        private System.Windows.Forms.BindingSource manufaturerToFindBS;
    }
}