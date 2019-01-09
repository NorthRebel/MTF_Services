namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Parts
{
    partial class SelectSoftwareForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSoftwareForm));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.созданиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_RecCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.softwareInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.созданиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.softTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.softwareDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.softwareInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.правкаToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(624, 24);
            this.menuBar.TabIndex = 0;
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.созданиеToolStripMenuItem,
            this.редактированиеToolStripMenuItem,
            this.удалениеToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // созданиеToolStripMenuItem
            // 
            this.созданиеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.созданиеToolStripMenuItem.Name = "созданиеToolStripMenuItem";
            this.созданиеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.созданиеToolStripMenuItem.Text = "Создание";
            this.созданиеToolStripMenuItem.ToolTipText = "Создание новой оперативной памяти";
            this.созданиеToolStripMenuItem.Click += new System.EventHandler(this.созданиеToolStripMenuItem_Click);
            // 
            // редактированиеToolStripMenuItem
            // 
            this.редактированиеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеToolStripMenuItem.Name = "редактированиеToolStripMenuItem";
            this.редактированиеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.редактированиеToolStripMenuItem.Text = "Редактирование";
            this.редактированиеToolStripMenuItem.ToolTipText = "Редактирование выбранной оперативной памяти";
            this.редактированиеToolStripMenuItem.Click += new System.EventHandler(this.редактированиеToolStripMenuItem_Click);
            // 
            // удалениеToolStripMenuItem
            // 
            this.удалениеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.удалениеToolStripMenuItem.Name = "удалениеToolStripMenuItem";
            this.удалениеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.удалениеToolStripMenuItem.Text = "Удаление";
            this.удалениеToolStripMenuItem.ToolTipText = "Удаление выбранной оперативной памяти";
            this.удалениеToolStripMenuItem.Click += new System.EventHandler(this.удалениеToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label,
            this.txt_RecCount});
            this.statusBar.Location = new System.Drawing.Point(0, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(624, 22);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusStrip1";
            // 
            // tip_Label
            // 
            this.tip_Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tip_Label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tip_Label.Name = "tip_Label";
            this.tip_Label.Size = new System.Drawing.Size(4, 17);
            // 
            // txt_RecCount
            // 
            this.txt_RecCount.Name = "txt_RecCount";
            this.txt_RecCount.Size = new System.Drawing.Size(111, 17);
            this.txt_RecCount.Text = "Общее количество: ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.3141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.4359F));
            this.tableLayoutPanel1.Controls.Add(this.btn_OK, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cancel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 383);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 40);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OK.Location = new System.Drawing.Point(485, 4);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(137, 31);
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
            this.btn_Cancel.Location = new System.Drawing.Point(353, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(128, 31);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // softwareInfoBindingSource
            // 
            this.softwareInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.SoftwareInfo);
            this.softwareInfoBindingSource.CurrentChanged += new System.EventHandler(this.softwareInfoBindingSource_CurrentChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.softTypeDataGridViewTextBoxColumn,
            this.softwareDataGridViewTextBoxColumn,
            this.costDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.softwareInfoBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(624, 359);
            this.dataGridView1.TabIndex = 28;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.созданиеToolStripMenuItem1,
            this.редактированиеToolStripMenuItem1,
            this.удалениеToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 70);
            // 
            // созданиеToolStripMenuItem1
            // 
            this.созданиеToolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.созданиеToolStripMenuItem1.Name = "созданиеToolStripMenuItem1";
            this.созданиеToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.созданиеToolStripMenuItem1.Text = "Создание";
            // 
            // редактированиеToolStripMenuItem1
            // 
            this.редактированиеToolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеToolStripMenuItem1.Name = "редактированиеToolStripMenuItem1";
            this.редактированиеToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.редактированиеToolStripMenuItem1.Text = "Редактирование";
            // 
            // удалениеToolStripMenuItem1
            // 
            this.удалениеToolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.удалениеToolStripMenuItem1.Name = "удалениеToolStripMenuItem1";
            this.удалениеToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.удалениеToolStripMenuItem1.Text = "Удаление";
            // 
            // softTypeDataGridViewTextBoxColumn
            // 
            this.softTypeDataGridViewTextBoxColumn.DataPropertyName = "SoftType";
            this.softTypeDataGridViewTextBoxColumn.HeaderText = "Тип ПО";
            this.softTypeDataGridViewTextBoxColumn.Name = "softTypeDataGridViewTextBoxColumn";
            this.softTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.softTypeDataGridViewTextBoxColumn.Width = 170;
            // 
            // softwareDataGridViewTextBoxColumn
            // 
            this.softwareDataGridViewTextBoxColumn.DataPropertyName = "Software";
            this.softwareDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.softwareDataGridViewTextBoxColumn.Name = "softwareDataGridViewTextBoxColumn";
            this.softwareDataGridViewTextBoxColumn.ReadOnly = true;
            this.softwareDataGridViewTextBoxColumn.Width = 250;
            // 
            // costDataGridViewTextBoxColumn
            // 
            this.costDataGridViewTextBoxColumn.DataPropertyName = "Cost";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.costDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.costDataGridViewTextBoxColumn.HeaderText = "Стоимость (руб.)";
            this.costDataGridViewTextBoxColumn.Name = "costDataGridViewTextBoxColumn";
            this.costDataGridViewTextBoxColumn.ReadOnly = true;
            this.costDataGridViewTextBoxColumn.Width = 170;
            // 
            // SelectSoftwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "SelectSoftwareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор программного обеспечения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectSoftwareForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectSoftwareForm_KeyDown);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.softwareInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem созданиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolStripStatusLabel txt_RecCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.BindingSource softwareInfoBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem созданиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалениеToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn softTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn softwareDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costDataGridViewTextBoxColumn;
    }
}