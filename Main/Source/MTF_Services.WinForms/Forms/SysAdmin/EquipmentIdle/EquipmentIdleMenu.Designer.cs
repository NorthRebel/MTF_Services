namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    partial class EquipmentIdleMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentIdleMenu));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dg_Servers = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plarformDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rAMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onMaintenanceDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.completeIdleCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheduledIdleCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_Servers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_ServerPlanningMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ServerRegisterIdle = new System.Windows.Forms.ToolStripMenuItem();
            this.serverIdleItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dg_Sans = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storageVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onMaintenanceDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.activeDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.completeIdleCountDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheduledIdleCountDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_Sans = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_SanPlanningMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SanRegisterIdle = new System.Windows.Forms.ToolStripMenuItem();
            this.sANIdleItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dg_Services = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paasTypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usersCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completeIdleCountDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheduledIdleCountDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceIdleItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.серверToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Servers)).BeginInit();
            this.cms_Servers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverIdleItemBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Sans)).BeginInit();
            this.cms_Sans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sANIdleItemBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Services)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceIdleItemBindingSource)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.серверToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(1048, 24);
            this.menuBar.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1048, 399);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dg_Servers);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1040, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Сервера";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dg_Servers
            // 
            this.dg_Servers.AllowUserToAddRows = false;
            this.dg_Servers.AllowUserToDeleteRows = false;
            this.dg_Servers.AutoGenerateColumns = false;
            this.dg_Servers.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Servers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Servers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.plarformDataGridViewTextBoxColumn,
            this.cPUDataGridViewTextBoxColumn,
            this.rAMDataGridViewTextBoxColumn,
            this.onMaintenanceDataGridViewCheckBoxColumn,
            this.activeDataGridViewCheckBoxColumn,
            this.completeIdleCountDataGridViewTextBoxColumn,
            this.sheduledIdleCountDataGridViewTextBoxColumn});
            this.dg_Servers.ContextMenuStrip = this.cms_Servers;
            this.dg_Servers.DataSource = this.serverIdleItemBindingSource;
            this.dg_Servers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Servers.Location = new System.Drawing.Point(3, 3);
            this.dg_Servers.Name = "dg_Servers";
            this.dg_Servers.ReadOnly = true;
            this.dg_Servers.RowHeadersVisible = false;
            this.dg_Servers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Servers.Size = new System.Drawing.Size(1034, 359);
            this.dg_Servers.TabIndex = 6;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "№ п/п";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // plarformDataGridViewTextBoxColumn
            // 
            this.plarformDataGridViewTextBoxColumn.DataPropertyName = "Plarform";
            this.plarformDataGridViewTextBoxColumn.HeaderText = "Платформа";
            this.plarformDataGridViewTextBoxColumn.Name = "plarformDataGridViewTextBoxColumn";
            this.plarformDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cPUDataGridViewTextBoxColumn
            // 
            this.cPUDataGridViewTextBoxColumn.DataPropertyName = "CPU";
            this.cPUDataGridViewTextBoxColumn.HeaderText = "Процессор";
            this.cPUDataGridViewTextBoxColumn.Name = "cPUDataGridViewTextBoxColumn";
            this.cPUDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rAMDataGridViewTextBoxColumn
            // 
            this.rAMDataGridViewTextBoxColumn.DataPropertyName = "RAM";
            this.rAMDataGridViewTextBoxColumn.HeaderText = "Объем ОЗУ";
            this.rAMDataGridViewTextBoxColumn.Name = "rAMDataGridViewTextBoxColumn";
            this.rAMDataGridViewTextBoxColumn.ReadOnly = true;
            this.rAMDataGridViewTextBoxColumn.Width = 150;
            // 
            // onMaintenanceDataGridViewCheckBoxColumn
            // 
            this.onMaintenanceDataGridViewCheckBoxColumn.DataPropertyName = "OnMaintenance";
            this.onMaintenanceDataGridViewCheckBoxColumn.HeaderText = "На обслуживании";
            this.onMaintenanceDataGridViewCheckBoxColumn.Name = "onMaintenanceDataGridViewCheckBoxColumn";
            this.onMaintenanceDataGridViewCheckBoxColumn.ReadOnly = true;
            this.onMaintenanceDataGridViewCheckBoxColumn.Width = 150;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Активен";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // completeIdleCountDataGridViewTextBoxColumn
            // 
            this.completeIdleCountDataGridViewTextBoxColumn.DataPropertyName = "CompleteIdleCount";
            this.completeIdleCountDataGridViewTextBoxColumn.HeaderText = "Совершено простоев";
            this.completeIdleCountDataGridViewTextBoxColumn.Name = "completeIdleCountDataGridViewTextBoxColumn";
            this.completeIdleCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sheduledIdleCountDataGridViewTextBoxColumn
            // 
            this.sheduledIdleCountDataGridViewTextBoxColumn.DataPropertyName = "SheduledIdleCount";
            this.sheduledIdleCountDataGridViewTextBoxColumn.HeaderText = "Запланировано простоев";
            this.sheduledIdleCountDataGridViewTextBoxColumn.Name = "sheduledIdleCountDataGridViewTextBoxColumn";
            this.sheduledIdleCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.sheduledIdleCountDataGridViewTextBoxColumn.Width = 130;
            // 
            // cms_Servers
            // 
            this.cms_Servers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ServerPlanningMaintenance,
            this.tsmi_ServerRegisterIdle});
            this.cms_Servers.Name = "cms_Servers";
            this.cms_Servers.Size = new System.Drawing.Size(285, 48);
            // 
            // tsmi_ServerPlanningMaintenance
            // 
            this.tsmi_ServerPlanningMaintenance.Image = global::MTF_Services.WinForms.Properties.Resources.date;
            this.tsmi_ServerPlanningMaintenance.Name = "tsmi_ServerPlanningMaintenance";
            this.tsmi_ServerPlanningMaintenance.Size = new System.Drawing.Size(284, 22);
            this.tsmi_ServerPlanningMaintenance.Text = "Планирование расписания обслуживания";
            this.tsmi_ServerPlanningMaintenance.Click += new System.EventHandler(this.tsmi_ServerPlanningMaintenance_Click);
            // 
            // tsmi_ServerRegisterIdle
            // 
            this.tsmi_ServerRegisterIdle.Image = global::MTF_Services.WinForms.Properties.Resources.error;
            this.tsmi_ServerRegisterIdle.Name = "tsmi_ServerRegisterIdle";
            this.tsmi_ServerRegisterIdle.Size = new System.Drawing.Size(284, 22);
            this.tsmi_ServerRegisterIdle.Text = "Регистрация нового простоя";
            this.tsmi_ServerRegisterIdle.Click += new System.EventHandler(this.tsmi_ServerRegisterIdle_Click);
            // 
            // serverIdleItemBindingSource
            // 
            this.serverIdleItemBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServerIdleItem);
            this.serverIdleItemBindingSource.CurrentChanged += new System.EventHandler(this.serverIdleItemBindingSource_CurrentChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dg_Sans);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1040, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Хранилища данных";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dg_Sans
            // 
            this.dg_Sans.AllowUserToAddRows = false;
            this.dg_Sans.AllowUserToDeleteRows = false;
            this.dg_Sans.AutoGenerateColumns = false;
            this.dg_Sans.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Sans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Sans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn1,
            this.manufacturerDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.storageCountDataGridViewTextBoxColumn,
            this.storageVolumeDataGridViewTextBoxColumn,
            this.onMaintenanceDataGridViewCheckBoxColumn1,
            this.activeDataGridViewCheckBoxColumn1,
            this.completeIdleCountDataGridViewTextBoxColumn1,
            this.sheduledIdleCountDataGridViewTextBoxColumn1});
            this.dg_Sans.ContextMenuStrip = this.cms_Sans;
            this.dg_Sans.DataSource = this.sANIdleItemBindingSource;
            this.dg_Sans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Sans.Location = new System.Drawing.Point(3, 3);
            this.dg_Sans.Name = "dg_Sans";
            this.dg_Sans.ReadOnly = true;
            this.dg_Sans.RowHeadersVisible = false;
            this.dg_Sans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Sans.Size = new System.Drawing.Size(1034, 334);
            this.dg_Sans.TabIndex = 7;
            // 
            // iDDataGridViewTextBoxColumn1
            // 
            this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn1.HeaderText = "№ п/п";
            this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
            this.iDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn1.Width = 90;
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
            // storageCountDataGridViewTextBoxColumn
            // 
            this.storageCountDataGridViewTextBoxColumn.DataPropertyName = "StorageCount";
            this.storageCountDataGridViewTextBoxColumn.HeaderText = "Количество накопителей";
            this.storageCountDataGridViewTextBoxColumn.Name = "storageCountDataGridViewTextBoxColumn";
            this.storageCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.storageCountDataGridViewTextBoxColumn.Width = 110;
            // 
            // storageVolumeDataGridViewTextBoxColumn
            // 
            this.storageVolumeDataGridViewTextBoxColumn.DataPropertyName = "StorageVolume";
            this.storageVolumeDataGridViewTextBoxColumn.HeaderText = "Объем накопителей";
            this.storageVolumeDataGridViewTextBoxColumn.Name = "storageVolumeDataGridViewTextBoxColumn";
            this.storageVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            this.storageVolumeDataGridViewTextBoxColumn.Width = 110;
            // 
            // onMaintenanceDataGridViewCheckBoxColumn1
            // 
            this.onMaintenanceDataGridViewCheckBoxColumn1.DataPropertyName = "OnMaintenance";
            this.onMaintenanceDataGridViewCheckBoxColumn1.HeaderText = "На обслуживании";
            this.onMaintenanceDataGridViewCheckBoxColumn1.Name = "onMaintenanceDataGridViewCheckBoxColumn1";
            this.onMaintenanceDataGridViewCheckBoxColumn1.ReadOnly = true;
            this.onMaintenanceDataGridViewCheckBoxColumn1.Width = 150;
            // 
            // activeDataGridViewCheckBoxColumn1
            // 
            this.activeDataGridViewCheckBoxColumn1.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn1.HeaderText = "Активен";
            this.activeDataGridViewCheckBoxColumn1.Name = "activeDataGridViewCheckBoxColumn1";
            this.activeDataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // completeIdleCountDataGridViewTextBoxColumn1
            // 
            this.completeIdleCountDataGridViewTextBoxColumn1.DataPropertyName = "CompleteIdleCount";
            this.completeIdleCountDataGridViewTextBoxColumn1.HeaderText = "Совершено простоев";
            this.completeIdleCountDataGridViewTextBoxColumn1.Name = "completeIdleCountDataGridViewTextBoxColumn1";
            this.completeIdleCountDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // sheduledIdleCountDataGridViewTextBoxColumn1
            // 
            this.sheduledIdleCountDataGridViewTextBoxColumn1.DataPropertyName = "SheduledIdleCount";
            this.sheduledIdleCountDataGridViewTextBoxColumn1.HeaderText = "Запланировано простоев";
            this.sheduledIdleCountDataGridViewTextBoxColumn1.Name = "sheduledIdleCountDataGridViewTextBoxColumn1";
            this.sheduledIdleCountDataGridViewTextBoxColumn1.ReadOnly = true;
            this.sheduledIdleCountDataGridViewTextBoxColumn1.Width = 130;
            // 
            // cms_Sans
            // 
            this.cms_Sans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_SanPlanningMaintenance,
            this.tsmi_SanRegisterIdle});
            this.cms_Sans.Name = "cms_Servers";
            this.cms_Sans.Size = new System.Drawing.Size(285, 48);
            // 
            // tsmi_SanPlanningMaintenance
            // 
            this.tsmi_SanPlanningMaintenance.Image = global::MTF_Services.WinForms.Properties.Resources.date;
            this.tsmi_SanPlanningMaintenance.Name = "tsmi_SanPlanningMaintenance";
            this.tsmi_SanPlanningMaintenance.Size = new System.Drawing.Size(284, 22);
            this.tsmi_SanPlanningMaintenance.Text = "Планирование расписания обслуживания";
            this.tsmi_SanPlanningMaintenance.Click += new System.EventHandler(this.tsmi_SanPlanningMaintenance_Click);
            // 
            // tsmi_SanRegisterIdle
            // 
            this.tsmi_SanRegisterIdle.Image = global::MTF_Services.WinForms.Properties.Resources.error;
            this.tsmi_SanRegisterIdle.Name = "tsmi_SanRegisterIdle";
            this.tsmi_SanRegisterIdle.Size = new System.Drawing.Size(284, 22);
            this.tsmi_SanRegisterIdle.Text = "Регистрация нового простоя";
            this.tsmi_SanRegisterIdle.Click += new System.EventHandler(this.tsmi_SanRegisterIdle_Click);
            // 
            // sANIdleItemBindingSource
            // 
            this.sANIdleItemBindingSource.DataSource = typeof(MTF_Services.Model.Views.SANIdleItem);
            this.sANIdleItemBindingSource.CurrentChanged += new System.EventHandler(this.sANIdleItemBindingSource_CurrentChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dg_Services);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1040, 340);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Сервисы";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dg_Services
            // 
            this.dg_Services.AllowUserToAddRows = false;
            this.dg_Services.AllowUserToDeleteRows = false;
            this.dg_Services.AutoGenerateColumns = false;
            this.dg_Services.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Services.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Services.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn,
            this.paasTypeNameDataGridViewTextBoxColumn,
            this.currentStateDataGridViewTextBoxColumn,
            this.usersCountDataGridViewTextBoxColumn,
            this.completeIdleCountDataGridViewTextBoxColumn2,
            this.sheduledIdleCountDataGridViewTextBoxColumn2});
            this.dg_Services.DataSource = this.serviceIdleItemBindingSource;
            this.dg_Services.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Services.Location = new System.Drawing.Point(0, 0);
            this.dg_Services.Name = "dg_Services";
            this.dg_Services.ReadOnly = true;
            this.dg_Services.RowHeadersVisible = false;
            this.dg_Services.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Services.Size = new System.Drawing.Size(1040, 340);
            this.dg_Services.TabIndex = 8;
            // 
            // iDDataGridViewTextBoxColumn2
            // 
            this.iDDataGridViewTextBoxColumn2.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn2.HeaderText = "№ п/п";
            this.iDDataGridViewTextBoxColumn2.Name = "iDDataGridViewTextBoxColumn2";
            this.iDDataGridViewTextBoxColumn2.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn2.Width = 90;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Наимеменование";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 140;
            // 
            // paasTypeNameDataGridViewTextBoxColumn
            // 
            this.paasTypeNameDataGridViewTextBoxColumn.DataPropertyName = "PaasTypeName";
            this.paasTypeNameDataGridViewTextBoxColumn.HeaderText = "Платформа";
            this.paasTypeNameDataGridViewTextBoxColumn.Name = "paasTypeNameDataGridViewTextBoxColumn";
            this.paasTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // currentStateDataGridViewTextBoxColumn
            // 
            this.currentStateDataGridViewTextBoxColumn.DataPropertyName = "CurrentState";
            this.currentStateDataGridViewTextBoxColumn.HeaderText = "Состояние";
            this.currentStateDataGridViewTextBoxColumn.Name = "currentStateDataGridViewTextBoxColumn";
            this.currentStateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usersCountDataGridViewTextBoxColumn
            // 
            this.usersCountDataGridViewTextBoxColumn.DataPropertyName = "UsersCount";
            this.usersCountDataGridViewTextBoxColumn.HeaderText = "Кол-во пользователей";
            this.usersCountDataGridViewTextBoxColumn.Name = "usersCountDataGridViewTextBoxColumn";
            this.usersCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.usersCountDataGridViewTextBoxColumn.Width = 120;
            // 
            // completeIdleCountDataGridViewTextBoxColumn2
            // 
            this.completeIdleCountDataGridViewTextBoxColumn2.DataPropertyName = "CompleteIdleCount";
            this.completeIdleCountDataGridViewTextBoxColumn2.HeaderText = "Совершено простоев";
            this.completeIdleCountDataGridViewTextBoxColumn2.Name = "completeIdleCountDataGridViewTextBoxColumn2";
            this.completeIdleCountDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // sheduledIdleCountDataGridViewTextBoxColumn2
            // 
            this.sheduledIdleCountDataGridViewTextBoxColumn2.DataPropertyName = "SheduledIdleCount";
            this.sheduledIdleCountDataGridViewTextBoxColumn2.HeaderText = "Запланировано простоев";
            this.sheduledIdleCountDataGridViewTextBoxColumn2.Name = "sheduledIdleCountDataGridViewTextBoxColumn2";
            this.sheduledIdleCountDataGridViewTextBoxColumn2.ReadOnly = true;
            this.sheduledIdleCountDataGridViewTextBoxColumn2.Width = 150;
            // 
            // serviceIdleItemBindingSource
            // 
            this.serviceIdleItemBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServiceIdleItem);
            // 
            // серверToolStripMenuItem
            // 
            this.серверToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выToolStripMenuItem,
            this.вToolStripMenuItem});
            this.серверToolStripMenuItem.Name = "серверToolStripMenuItem";
            this.серверToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.серверToolStripMenuItem.Text = "Сервер";
            // 
            // выToolStripMenuItem
            // 
            this.выToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.date;
            this.выToolStripMenuItem.Name = "выToolStripMenuItem";
            this.выToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.выToolStripMenuItem.Text = "Планирование расписания обслуживания";
            this.выToolStripMenuItem.Click += new System.EventHandler(this.tsmi_ServerPlanningMaintenance_Click);
            // 
            // вToolStripMenuItem
            // 
            this.вToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.error;
            this.вToolStripMenuItem.Name = "вToolStripMenuItem";
            this.вToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.вToolStripMenuItem.Text = "Регистрация нового простоя";
            this.вToolStripMenuItem.Click += new System.EventHandler(this.tsmi_ServerRegisterIdle_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 20);
            this.toolStripMenuItem1.Text = "Хранилища данных";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::MTF_Services.WinForms.Properties.Resources.date;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuItem2.Text = "Планирование расписания обслуживания";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.tsmi_SanPlanningMaintenance_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::MTF_Services.WinForms.Properties.Resources.error;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuItem3.Text = "Регистрация нового простоя";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.tsmi_SanRegisterIdle_Click);
            // 
            // tip_Label
            // 
            this.tip_Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tip_Label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tip_Label.Name = "tip_Label";
            this.tip_Label.Size = new System.Drawing.Size(4, 17);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label});
            this.statusBar.Location = new System.Drawing.Point(0, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1048, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // EquipmentIdleMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 445);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "EquipmentIdleMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Простой оборудования";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EquipmentIdleMenu_FormClosing);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Servers)).EndInit();
            this.cms_Servers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverIdleItemBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Sans)).EndInit();
            this.cms_Sans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sANIdleItemBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Services)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceIdleItemBindingSource)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dg_Servers;
        private System.Windows.Forms.BindingSource serverIdleItemBindingSource;
        private System.Windows.Forms.DataGridView dg_Sans;
        private System.Windows.Forms.DataGridView dg_Services;
        private System.Windows.Forms.BindingSource sANIdleItemBindingSource;
        private System.Windows.Forms.BindingSource serviceIdleItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paasTypeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usersCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn completeIdleCountDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheduledIdleCountDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn plarformDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rAMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn onMaintenanceDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn completeIdleCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheduledIdleCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn storageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn storageVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn onMaintenanceDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn completeIdleCountDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheduledIdleCountDataGridViewTextBoxColumn1;
        private System.Windows.Forms.ContextMenuStrip cms_Servers;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ServerPlanningMaintenance;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ServerRegisterIdle;
        private System.Windows.Forms.ContextMenuStrip cms_Sans;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SanPlanningMaintenance;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SanRegisterIdle;
        private System.Windows.Forms.ToolStripMenuItem серверToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.Timer timer;
    }
}