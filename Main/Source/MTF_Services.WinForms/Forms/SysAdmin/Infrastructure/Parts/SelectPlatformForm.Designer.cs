namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts
{
    partial class SelectPlatformForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectPlatformForm));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.созданиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_ShowSupportedInterfacesList = new System.Windows.Forms.Button();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.созданиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.platformInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_RecordsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPUSocketDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPUCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rAMTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ramVolumeMaxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ramSocketCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuBar.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.platformInfoBindingSource)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.правкаToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(920, 24);
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
            this.созданиеToolStripMenuItem.ToolTipText = "Создание новой платформы";
            this.созданиеToolStripMenuItem.Click += new System.EventHandler(this.созданиеToolStripMenuItem_Click);
            // 
            // редактированиеToolStripMenuItem
            // 
            this.редактированиеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеToolStripMenuItem.Name = "редактированиеToolStripMenuItem";
            this.редактированиеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.редактированиеToolStripMenuItem.Text = "Редактирование";
            this.редактированиеToolStripMenuItem.ToolTipText = "Редактирование выбранной платформы";
            this.редактированиеToolStripMenuItem.Click += new System.EventHandler(this.редактированиеToolStripMenuItem_Click);
            // 
            // удалениеToolStripMenuItem
            // 
            this.удалениеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.удалениеToolStripMenuItem.Name = "удалениеToolStripMenuItem";
            this.удалениеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.удалениеToolStripMenuItem.Text = "Удаление";
            this.удалениеToolStripMenuItem.ToolTipText = "Удаление выбранной платформы";
            this.удалениеToolStripMenuItem.Click += new System.EventHandler(this.удалениеToolStripMenuItem_Click);
            // 
            // btn_ShowSupportedInterfacesList
            // 
            this.btn_ShowSupportedInterfacesList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowSupportedInterfacesList.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_ShowSupportedInterfacesList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ShowSupportedInterfacesList.Location = new System.Drawing.Point(40, 4);
            this.btn_ShowSupportedInterfacesList.Margin = new System.Windows.Forms.Padding(40, 3, 40, 3);
            this.btn_ShowSupportedInterfacesList.Name = "btn_ShowSupportedInterfacesList";
            this.btn_ShowSupportedInterfacesList.Size = new System.Drawing.Size(472, 31);
            this.btn_ShowSupportedInterfacesList.TabIndex = 3;
            this.btn_ShowSupportedInterfacesList.Text = "Список поддерживаемых интерфейсов";
            this.toolTip.SetToolTip(this.btn_ShowSupportedInterfacesList, "Просмотр списка поддерживаемых интерфейсов хранения выбранной платформы");
            this.btn_ShowSupportedInterfacesList.UseVisualStyleBackColor = true;
            this.btn_ShowSupportedInterfacesList.Click += new System.EventHandler(this.btn_ShowSupportedInterfacesList_Click);
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripSeparator2});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(920, 25);
            this.toolBar.TabIndex = 2;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::MTF_Services.WinForms.Properties.Resources.hdd;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Просмотр списка поддерживаемых интерфейсов хранения выбранной платформы";
            this.toolStripButton4.Click += new System.EventHandler(this.btn_ShowSupportedInterfacesList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Создание новой платформы";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "Редактирование выбранной платформы";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.ToolTipText = "Удаление выбранной платформы";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btn_OK, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cancel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ShowSupportedInterfacesList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 383);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(920, 40);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OK.Location = new System.Drawing.Point(738, 4);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(180, 31);
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
            this.btn_Cancel.Location = new System.Drawing.Point(554, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(180, 31);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.manufacturerDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.cPUSocketDataGridViewTextBoxColumn,
            this.cPUCountDataGridViewTextBoxColumn,
            this.rAMTypeDataGridViewTextBoxColumn,
            this.ramVolumeMaxDataGridViewTextBoxColumn,
            this.ramSocketCountDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.platformInfoBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 49);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(920, 334);
            this.dataGridView1.TabIndex = 4;
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
            this.созданиеToolStripMenuItem1.ToolTipText = "Создание новой платформы";
            // 
            // редактированиеToolStripMenuItem1
            // 
            this.редактированиеToolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеToolStripMenuItem1.Name = "редактированиеToolStripMenuItem1";
            this.редактированиеToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.редактированиеToolStripMenuItem1.Text = "Редактирование";
            this.редактированиеToolStripMenuItem1.ToolTipText = "Редактирование выбранной платформы";
            // 
            // удалениеToolStripMenuItem1
            // 
            this.удалениеToolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.удалениеToolStripMenuItem1.Name = "удалениеToolStripMenuItem1";
            this.удалениеToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.удалениеToolStripMenuItem1.Text = "Удаление";
            this.удалениеToolStripMenuItem1.ToolTipText = "Удаление выбранной платформы";
            // 
            // platformInfoBindingSource
            // 
            this.platformInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.PlatformInfo);
            this.platformInfoBindingSource.DataSourceChanged += new System.EventHandler(this.platformInfoBindingSource_DataSourceChanged);
            this.platformInfoBindingSource.CurrentChanged += new System.EventHandler(this.platformInfoBindingSource_CurrentChanged);
            // 
            // tip_Label
            // 
            this.tip_Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tip_Label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tip_Label.Name = "tip_Label";
            this.tip_Label.Size = new System.Drawing.Size(4, 17);
            // 
            // lbl_RecordsCount
            // 
            this.lbl_RecordsCount.Name = "lbl_RecordsCount";
            this.lbl_RecordsCount.Size = new System.Drawing.Size(46, 17);
            this.lbl_RecordsCount.Text = "Кол-во:";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label,
            this.lbl_RecordsCount});
            this.statusBar.Location = new System.Drawing.Point(0, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(920, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // manufacturerDataGridViewTextBoxColumn
            // 
            this.manufacturerDataGridViewTextBoxColumn.DataPropertyName = "Manufacturer";
            this.manufacturerDataGridViewTextBoxColumn.HeaderText = "Производитель";
            this.manufacturerDataGridViewTextBoxColumn.Name = "manufacturerDataGridViewTextBoxColumn";
            this.manufacturerDataGridViewTextBoxColumn.ReadOnly = true;
            this.manufacturerDataGridViewTextBoxColumn.Width = 120;
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Модель";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cPUSocketDataGridViewTextBoxColumn
            // 
            this.cPUSocketDataGridViewTextBoxColumn.DataPropertyName = "CPUSocket";
            this.cPUSocketDataGridViewTextBoxColumn.HeaderText = "Разъем процессора";
            this.cPUSocketDataGridViewTextBoxColumn.Name = "cPUSocketDataGridViewTextBoxColumn";
            this.cPUSocketDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cPUCountDataGridViewTextBoxColumn
            // 
            this.cPUCountDataGridViewTextBoxColumn.DataPropertyName = "CPU_Count";
            this.cPUCountDataGridViewTextBoxColumn.HeaderText = "Кол-во ядер";
            this.cPUCountDataGridViewTextBoxColumn.Name = "cPUCountDataGridViewTextBoxColumn";
            this.cPUCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rAMTypeDataGridViewTextBoxColumn
            // 
            this.rAMTypeDataGridViewTextBoxColumn.DataPropertyName = "RAMType";
            this.rAMTypeDataGridViewTextBoxColumn.HeaderText = "Тип памяти";
            this.rAMTypeDataGridViewTextBoxColumn.Name = "rAMTypeDataGridViewTextBoxColumn";
            this.rAMTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ramVolumeMaxDataGridViewTextBoxColumn
            // 
            this.ramVolumeMaxDataGridViewTextBoxColumn.DataPropertyName = "RamVolume_Max";
            this.ramVolumeMaxDataGridViewTextBoxColumn.HeaderText = "Макс. объем ОЗУ (ГБ.)";
            this.ramVolumeMaxDataGridViewTextBoxColumn.Name = "ramVolumeMaxDataGridViewTextBoxColumn";
            this.ramVolumeMaxDataGridViewTextBoxColumn.ReadOnly = true;
            this.ramVolumeMaxDataGridViewTextBoxColumn.Width = 150;
            // 
            // ramSocketCountDataGridViewTextBoxColumn
            // 
            this.ramSocketCountDataGridViewTextBoxColumn.DataPropertyName = "RamSocketCount";
            this.ramSocketCountDataGridViewTextBoxColumn.HeaderText = "Кол-во слотов ОЗУ";
            this.ramSocketCountDataGridViewTextBoxColumn.Name = "ramSocketCountDataGridViewTextBoxColumn";
            this.ramSocketCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.ramSocketCountDataGridViewTextBoxColumn.Width = 130;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.priceDataGridViewTextBoxColumn.HeaderText = "Цена";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // SelectPlatformForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 445);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "SelectPlatformForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор платформы";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.platformInfoBindingSource)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem созданиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource platformInfoBindingSource;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_ShowSupportedInterfacesList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem созданиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалениеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolStripStatusLabel lbl_RecordsCount;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPUSocketDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPUCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rAMTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramVolumeMaxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramSocketCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
    }
}