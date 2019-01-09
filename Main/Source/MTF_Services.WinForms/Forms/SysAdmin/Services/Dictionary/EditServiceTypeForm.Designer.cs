namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary
{
    partial class EditServiceTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditServiceTypeForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picBtn_ClearCondition = new System.Windows.Forms.PictureBox();
            this.picBtn_FindByCondition = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_ConditionToFind = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.serviceTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_Save = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.but_Select = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_ClearCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_FindByCondition)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBtn_ClearCondition
            // 
            this.picBtn_ClearCondition.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn_ClearCondition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_ClearCondition.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_ClearCondition.Location = new System.Drawing.Point(342, 12);
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
            this.picBtn_FindByCondition.Location = new System.Drawing.Point(300, 12);
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
            this.panel1.Controls.Add(this.picBtn_ClearCondition);
            this.panel1.Controls.Add(this.picBtn_FindByCondition);
            this.panel1.Controls.Add(this.textBox_ConditionToFind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 55);
            this.panel1.TabIndex = 4;
            // 
            // textBox_ConditionToFind
            // 
            this.textBox_ConditionToFind.Location = new System.Drawing.Point(12, 12);
            this.textBox_ConditionToFind.Name = "textBox_ConditionToFind";
            this.textBox_ConditionToFind.Size = new System.Drawing.Size(282, 29);
            this.textBox_ConditionToFind.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.36459F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.63542F));
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.6129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.3871F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 310);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.CausesValidation = false;
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serviceTypeBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(3, 266);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(245, 29);
            this.textBox2.TabIndex = 0;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // serviceTypeBindingSource
            // 
            this.serviceTypeBindingSource.DataSource = typeof(MTF_Services.Model.ServiceType);
            this.serviceTypeBindingSource.CurrentChanged += new System.EventHandler(this.serviceTypeBindingSource_CurrentChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(254, 255);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(127, 52);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "Сохранить";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.serviceTypeBindingSource;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(245, 246);
            this.listBox1.TabIndex = 2;
            this.listBox1.ValueMember = "Id";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.but_Select);
            this.panel2.Controls.Add(this.btn_Cancel);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(254, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(127, 246);
            this.panel2.TabIndex = 3;
            // 
            // but_Select
            // 
            this.but_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.but_Select.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.but_Select.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_Select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.but_Select.Location = new System.Drawing.Point(0, 215);
            this.but_Select.Name = "but_Select";
            this.but_Select.Size = new System.Drawing.Size(127, 31);
            this.but_Select.TabIndex = 3;
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
            this.btn_Cancel.Size = new System.Drawing.Size(127, 29);
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
            this.button3.Size = new System.Drawing.Size(127, 29);
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
            this.button2.Size = new System.Drawing.Size(127, 29);
            this.button2.TabIndex = 0;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditServiceTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 365);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "EditServiceTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование типов сервиса";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditServiceTypeForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditServiceTypeForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_ClearCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_FindByCondition)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBtn_ClearCondition;
        private System.Windows.Forms.PictureBox picBtn_FindByCondition;
        private System.Windows.Forms.TextBox textBox_ConditionToFind;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button but_Select;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource serviceTypeBindingSource;
    }
}