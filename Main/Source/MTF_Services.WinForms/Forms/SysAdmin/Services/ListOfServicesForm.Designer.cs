namespace MTF_Services.WinForms.Forms.SysAdmin.Services
{
    partial class ListOfServicesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListOfServicesForm));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttmi_ShowAllServices = new System.Windows.Forms.ToolStripMenuItem();
            this.ttmi_ShowServicesBySelectedPlatform = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.созданиеНовойПлатформыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеВыбраннойПлатформыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеВыбраннойПлатформыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.созданиеНовогоСервисаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеВыбранногоСервисаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеВыбранногоСервисаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PlatformsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_ServiceTotalCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_ServiceSelectedCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picBtn_DeleteSelectedPlatform = new System.Windows.Forms.PictureBox();
            this.picBtn_AddNewPlatform = new System.Windows.Forms.PictureBox();
            this.picBtn_DeleteSelectedService = new System.Windows.Forms.PictureBox();
            this.picBtn_AddNewService = new System.Windows.Forms.PictureBox();
            this.picBtn_EditSelectedPlatform = new System.Windows.Forms.PictureBox();
            this.picBtn_EditSelectedService = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dg_Platforms = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_Platform = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddNewPlatfrom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_EditSelectedPlatform = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DeleteSelectedPlatform = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_AboutSelectedPlatform = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_ShowServicesBySelectedPlatform = new System.Windows.Forms.ToolStripMenuItem();
            this.platformServiceUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dg_Services = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paasTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costPerHourDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userCountDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_Service = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddNewService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_EditSelectedService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DeleteSelectedService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_AboutSelectedService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_ShowAllServices = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceDetailInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSelectedPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddNewPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSelectedService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddNewService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_EditSelectedPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_EditSelectedService)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Platforms)).BeginInit();
            this.cms_Platform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.platformServiceUserBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Services)).BeginInit();
            this.cms_Service.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceDetailInfoBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.видToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(914, 24);
            this.menuBar.TabIndex = 0;
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ttmi_ShowAllServices,
            this.ttmi_ShowServicesBySelectedPlatform});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // ttmi_ShowAllServices
            // 
            this.ttmi_ShowAllServices.Image = global::MTF_Services.WinForms.Properties.Resources.eye;
            this.ttmi_ShowAllServices.Name = "ttmi_ShowAllServices";
            this.ttmi_ShowAllServices.Size = new System.Drawing.Size(288, 22);
            this.ttmi_ShowAllServices.Text = "Просмотреть все сервисы";
            this.ttmi_ShowAllServices.Click += new System.EventHandler(this.ttmi_ShowAllServices_Click);
            // 
            // ttmi_ShowServicesBySelectedPlatform
            // 
            this.ttmi_ShowServicesBySelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.search;
            this.ttmi_ShowServicesBySelectedPlatform.Name = "ttmi_ShowServicesBySelectedPlatform";
            this.ttmi_ShowServicesBySelectedPlatform.Size = new System.Drawing.Size(288, 22);
            this.ttmi_ShowServicesBySelectedPlatform.Text = "Отбор сервисов по выбранной платформе";
            this.ttmi_ShowServicesBySelectedPlatform.Click += new System.EventHandler(this.ttmi_ShowServicesBySelectedPlatform_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.созданиеНовойПлатформыToolStripMenuItem,
            this.редактированиеВыбраннойПлатформыToolStripMenuItem,
            this.удалениеВыбраннойПлатформыToolStripMenuItem,
            this.toolStripSeparator1,
            this.созданиеНовогоСервисаToolStripMenuItem,
            this.редактированиеВыбранногоСервисаToolStripMenuItem,
            this.удалениеВыбранногоСервисаToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // созданиеНовойПлатформыToolStripMenuItem
            // 
            this.созданиеНовойПлатформыToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.созданиеНовойПлатформыToolStripMenuItem.Name = "созданиеНовойПлатформыToolStripMenuItem";
            this.созданиеНовойПлатформыToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.созданиеНовойПлатформыToolStripMenuItem.Text = "Создание новой платформы";
            // 
            // редактированиеВыбраннойПлатформыToolStripMenuItem
            // 
            this.редактированиеВыбраннойПлатформыToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеВыбраннойПлатформыToolStripMenuItem.Name = "редактированиеВыбраннойПлатформыToolStripMenuItem";
            this.редактированиеВыбраннойПлатформыToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.редактированиеВыбраннойПлатформыToolStripMenuItem.Text = "Редактирование выбранной платформы";
            // 
            // удалениеВыбраннойПлатформыToolStripMenuItem
            // 
            this.удалениеВыбраннойПлатформыToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.удалениеВыбраннойПлатформыToolStripMenuItem.Name = "удалениеВыбраннойПлатформыToolStripMenuItem";
            this.удалениеВыбраннойПлатформыToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.удалениеВыбраннойПлатформыToolStripMenuItem.Text = "Удаление выбранной платформы";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // созданиеНовогоСервисаToolStripMenuItem
            // 
            this.созданиеНовогоСервисаToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.созданиеНовогоСервисаToolStripMenuItem.Name = "созданиеНовогоСервисаToolStripMenuItem";
            this.созданиеНовогоСервисаToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.созданиеНовогоСервисаToolStripMenuItem.Text = "Создание нового сервиса";
            // 
            // редактированиеВыбранногоСервисаToolStripMenuItem
            // 
            this.редактированиеВыбранногоСервисаToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактированиеВыбранногоСервисаToolStripMenuItem.Name = "редактированиеВыбранногоСервисаToolStripMenuItem";
            this.редактированиеВыбранногоСервисаToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.редактированиеВыбранногоСервисаToolStripMenuItem.Text = "Редактирование выбранного сервиса";
            // 
            // удалениеВыбранногоСервисаToolStripMenuItem
            // 
            this.удалениеВыбранногоСервисаToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.удалениеВыбранногоСервисаToolStripMenuItem.Name = "удалениеВыбранногоСервисаToolStripMenuItem";
            this.удалениеВыбранногоСервисаToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.удалениеВыбранногоСервисаToolStripMenuItem.Text = "Удаление выбранного сервиса";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label,
            this.lbl_PlatformsCount,
            this.lbl_ServiceTotalCount,
            this.lbl_ServiceSelectedCount});
            this.statusBar.Location = new System.Drawing.Point(0, 462);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(914, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // tip_Label
            // 
            this.tip_Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tip_Label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tip_Label.Name = "tip_Label";
            this.tip_Label.Size = new System.Drawing.Size(4, 17);
            // 
            // lbl_PlatformsCount
            // 
            this.lbl_PlatformsCount.Name = "lbl_PlatformsCount";
            this.lbl_PlatformsCount.Size = new System.Drawing.Size(99, 17);
            this.lbl_PlatformsCount.Text = "Кол-во платформ:";
            // 
            // lbl_ServiceTotalCount
            // 
            this.lbl_ServiceTotalCount.Name = "lbl_ServiceTotalCount";
            this.lbl_ServiceTotalCount.Size = new System.Drawing.Size(208, 17);
            this.lbl_ServiceTotalCount.Text = "Общее кол-во конфигураций сервисов:";
            // 
            // lbl_ServiceSelectedCount
            // 
            this.lbl_ServiceSelectedCount.Name = "lbl_ServiceSelectedCount";
            this.lbl_ServiceSelectedCount.Size = new System.Drawing.Size(137, 17);
            this.lbl_ServiceSelectedCount.Text = "Отобрано конфигураций:";
            // 
            // picBtn_DeleteSelectedPlatform
            // 
            this.picBtn_DeleteSelectedPlatform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_DeleteSelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_DeleteSelectedPlatform.Location = new System.Drawing.Point(3, 75);
            this.picBtn_DeleteSelectedPlatform.Name = "picBtn_DeleteSelectedPlatform";
            this.picBtn_DeleteSelectedPlatform.Size = new System.Drawing.Size(30, 30);
            this.picBtn_DeleteSelectedPlatform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_DeleteSelectedPlatform.TabIndex = 14;
            this.picBtn_DeleteSelectedPlatform.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_DeleteSelectedPlatform, "Удаление выбранной");
            this.picBtn_DeleteSelectedPlatform.Click += new System.EventHandler(this.picBtn_DeleteSelectedPlatform_Click);
            // 
            // picBtn_AddNewPlatform
            // 
            this.picBtn_AddNewPlatform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_AddNewPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.picBtn_AddNewPlatform.Location = new System.Drawing.Point(3, 3);
            this.picBtn_AddNewPlatform.Name = "picBtn_AddNewPlatform";
            this.picBtn_AddNewPlatform.Size = new System.Drawing.Size(30, 30);
            this.picBtn_AddNewPlatform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_AddNewPlatform.TabIndex = 12;
            this.picBtn_AddNewPlatform.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_AddNewPlatform, "Создание новой");
            this.picBtn_AddNewPlatform.Click += new System.EventHandler(this.picBtn_AddNewPlatform_Click);
            // 
            // picBtn_DeleteSelectedService
            // 
            this.picBtn_DeleteSelectedService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_DeleteSelectedService.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_DeleteSelectedService.Location = new System.Drawing.Point(3, 75);
            this.picBtn_DeleteSelectedService.Name = "picBtn_DeleteSelectedService";
            this.picBtn_DeleteSelectedService.Size = new System.Drawing.Size(30, 30);
            this.picBtn_DeleteSelectedService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_DeleteSelectedService.TabIndex = 14;
            this.picBtn_DeleteSelectedService.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_DeleteSelectedService, "Удаление выбранного");
            this.picBtn_DeleteSelectedService.Click += new System.EventHandler(this.picBtn_DeleteSelectedService_Click);
            // 
            // picBtn_AddNewService
            // 
            this.picBtn_AddNewService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_AddNewService.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.picBtn_AddNewService.Location = new System.Drawing.Point(3, 3);
            this.picBtn_AddNewService.Name = "picBtn_AddNewService";
            this.picBtn_AddNewService.Size = new System.Drawing.Size(30, 30);
            this.picBtn_AddNewService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_AddNewService.TabIndex = 12;
            this.picBtn_AddNewService.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_AddNewService, "Создание нового");
            this.picBtn_AddNewService.Click += new System.EventHandler(this.picBtn_AddNewService_Click);
            // 
            // picBtn_EditSelectedPlatform
            // 
            this.picBtn_EditSelectedPlatform.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn_EditSelectedPlatform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_EditSelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.picBtn_EditSelectedPlatform.Location = new System.Drawing.Point(3, 39);
            this.picBtn_EditSelectedPlatform.Name = "picBtn_EditSelectedPlatform";
            this.picBtn_EditSelectedPlatform.Size = new System.Drawing.Size(30, 30);
            this.picBtn_EditSelectedPlatform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_EditSelectedPlatform.TabIndex = 30;
            this.picBtn_EditSelectedPlatform.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_EditSelectedPlatform, "Редактирование выбранной");
            this.picBtn_EditSelectedPlatform.Click += new System.EventHandler(this.picBtn_EditSelectedPlatform_Click);
            // 
            // picBtn_EditSelectedService
            // 
            this.picBtn_EditSelectedService.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBtn_EditSelectedService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_EditSelectedService.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.picBtn_EditSelectedService.Location = new System.Drawing.Point(3, 39);
            this.picBtn_EditSelectedService.Name = "picBtn_EditSelectedService";
            this.picBtn_EditSelectedService.Size = new System.Drawing.Size(30, 30);
            this.picBtn_EditSelectedService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_EditSelectedService.TabIndex = 30;
            this.picBtn_EditSelectedService.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_EditSelectedService, "Редактирование выбранного сервиса");
            this.picBtn_EditSelectedService.Click += new System.EventHandler(this.picBtn_EditSelectedService_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.69584F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(914, 438);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dg_Platforms);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Платформы";
            // 
            // dg_Platforms
            // 
            this.dg_Platforms.AllowUserToAddRows = false;
            this.dg_Platforms.AllowUserToDeleteRows = false;
            this.dg_Platforms.AutoGenerateColumns = false;
            this.dg_Platforms.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Platforms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Platforms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.serviceCountDataGridViewTextBoxColumn,
            this.userCountDataGridViewTextBoxColumn});
            this.dg_Platforms.ContextMenuStrip = this.cms_Platform;
            this.dg_Platforms.DataSource = this.platformServiceUserBindingSource;
            this.dg_Platforms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Platforms.Location = new System.Drawing.Point(40, 25);
            this.dg_Platforms.Name = "dg_Platforms";
            this.dg_Platforms.ReadOnly = true;
            this.dg_Platforms.RowHeadersVisible = false;
            this.dg_Platforms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Platforms.Size = new System.Drawing.Size(865, 185);
            this.dg_Platforms.TabIndex = 9;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 250;
            // 
            // serviceCountDataGridViewTextBoxColumn
            // 
            this.serviceCountDataGridViewTextBoxColumn.DataPropertyName = "ServiceCount";
            this.serviceCountDataGridViewTextBoxColumn.HeaderText = "Кол-во сервисов";
            this.serviceCountDataGridViewTextBoxColumn.Name = "serviceCountDataGridViewTextBoxColumn";
            this.serviceCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.serviceCountDataGridViewTextBoxColumn.Width = 160;
            // 
            // userCountDataGridViewTextBoxColumn
            // 
            this.userCountDataGridViewTextBoxColumn.DataPropertyName = "UserCount";
            this.userCountDataGridViewTextBoxColumn.HeaderText = "Кол-во пользователей";
            this.userCountDataGridViewTextBoxColumn.Name = "userCountDataGridViewTextBoxColumn";
            this.userCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.userCountDataGridViewTextBoxColumn.Width = 200;
            // 
            // cms_Platform
            // 
            this.cms_Platform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddNewPlatfrom,
            this.tsmi_EditSelectedPlatform,
            this.tsmi_DeleteSelectedPlatform,
            this.toolStripSeparator4,
            this.tsmi_AboutSelectedPlatform,
            this.toolStripSeparator5,
            this.tsmi_ShowServicesBySelectedPlatform});
            this.cms_Platform.Name = "cms_Service";
            this.cms_Platform.Size = new System.Drawing.Size(289, 126);
            // 
            // tsmi_AddNewPlatfrom
            // 
            this.tsmi_AddNewPlatfrom.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.tsmi_AddNewPlatfrom.Name = "tsmi_AddNewPlatfrom";
            this.tsmi_AddNewPlatfrom.Size = new System.Drawing.Size(288, 22);
            this.tsmi_AddNewPlatfrom.Text = "Создать новую";
            // 
            // tsmi_EditSelectedPlatform
            // 
            this.tsmi_EditSelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.tsmi_EditSelectedPlatform.Name = "tsmi_EditSelectedPlatform";
            this.tsmi_EditSelectedPlatform.Size = new System.Drawing.Size(288, 22);
            this.tsmi_EditSelectedPlatform.Text = "Редактировать выбранную";
            // 
            // tsmi_DeleteSelectedPlatform
            // 
            this.tsmi_DeleteSelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.tsmi_DeleteSelectedPlatform.Name = "tsmi_DeleteSelectedPlatform";
            this.tsmi_DeleteSelectedPlatform.Size = new System.Drawing.Size(288, 22);
            this.tsmi_DeleteSelectedPlatform.Text = "Удалить выбранную";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmi_AboutSelectedPlatform
            // 
            this.tsmi_AboutSelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.info;
            this.tsmi_AboutSelectedPlatform.Name = "tsmi_AboutSelectedPlatform";
            this.tsmi_AboutSelectedPlatform.Size = new System.Drawing.Size(288, 22);
            this.tsmi_AboutSelectedPlatform.Text = "Просмотр подробной информации";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(285, 6);
            // 
            // tsmi_ShowServicesBySelectedPlatform
            // 
            this.tsmi_ShowServicesBySelectedPlatform.Image = global::MTF_Services.WinForms.Properties.Resources.search;
            this.tsmi_ShowServicesBySelectedPlatform.Name = "tsmi_ShowServicesBySelectedPlatform";
            this.tsmi_ShowServicesBySelectedPlatform.Size = new System.Drawing.Size(288, 22);
            this.tsmi_ShowServicesBySelectedPlatform.Text = "Отбор сервисов по выбранной платформе";
            // 
            // platformServiceUserBindingSource
            // 
            this.platformServiceUserBindingSource.DataSource = typeof(MTF_Services.Model.Views.PlatformServiceUser);
            this.platformServiceUserBindingSource.CurrentChanged += new System.EventHandler(this.platformServiceUserBindingSource_CurrentChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBtn_EditSelectedPlatform);
            this.panel1.Controls.Add(this.picBtn_DeleteSelectedPlatform);
            this.panel1.Controls.Add(this.picBtn_AddNewPlatform);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(37, 185);
            this.panel1.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg_Services);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(908, 213);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сервисы";
            // 
            // dg_Services
            // 
            this.dg_Services.AllowUserToAddRows = false;
            this.dg_Services.AllowUserToDeleteRows = false;
            this.dg_Services.AutoGenerateColumns = false;
            this.dg_Services.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Services.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Services.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.paasTypeDataGridViewTextBoxColumn,
            this.serviceTypeDataGridViewTextBoxColumn,
            this.createDateDataGridViewTextBoxColumn,
            this.deleteDateDataGridViewTextBoxColumn,
            this.serviceStateDataGridViewTextBoxColumn,
            this.costPerHourDataGridViewTextBoxColumn,
            this.userCountDataGridViewTextBoxColumn1});
            this.dg_Services.ContextMenuStrip = this.cms_Service;
            this.dg_Services.DataSource = this.serviceDetailInfoBindingSource;
            this.dg_Services.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Services.Location = new System.Drawing.Point(40, 25);
            this.dg_Services.Name = "dg_Services";
            this.dg_Services.ReadOnly = true;
            this.dg_Services.RowHeadersVisible = false;
            this.dg_Services.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Services.Size = new System.Drawing.Size(865, 185);
            this.dg_Services.TabIndex = 9;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "№ п/п";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 90;
            // 
            // paasTypeDataGridViewTextBoxColumn
            // 
            this.paasTypeDataGridViewTextBoxColumn.DataPropertyName = "PaasType";
            this.paasTypeDataGridViewTextBoxColumn.HeaderText = "Платформа";
            this.paasTypeDataGridViewTextBoxColumn.Name = "paasTypeDataGridViewTextBoxColumn";
            this.paasTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serviceTypeDataGridViewTextBoxColumn
            // 
            this.serviceTypeDataGridViewTextBoxColumn.DataPropertyName = "ServiceType";
            this.serviceTypeDataGridViewTextBoxColumn.HeaderText = "Вид";
            this.serviceTypeDataGridViewTextBoxColumn.Name = "serviceTypeDataGridViewTextBoxColumn";
            this.serviceTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createDateDataGridViewTextBoxColumn
            // 
            this.createDateDataGridViewTextBoxColumn.DataPropertyName = "Create_Date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = "В обработке";
            this.createDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.createDateDataGridViewTextBoxColumn.HeaderText = "Дата создания";
            this.createDateDataGridViewTextBoxColumn.Name = "createDateDataGridViewTextBoxColumn";
            this.createDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deleteDateDataGridViewTextBoxColumn
            // 
            this.deleteDateDataGridViewTextBoxColumn.DataPropertyName = "Delete_Date";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = "В использовании";
            this.deleteDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.deleteDateDataGridViewTextBoxColumn.HeaderText = "Дата удаления";
            this.deleteDateDataGridViewTextBoxColumn.Name = "deleteDateDataGridViewTextBoxColumn";
            this.deleteDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serviceStateDataGridViewTextBoxColumn
            // 
            this.serviceStateDataGridViewTextBoxColumn.DataPropertyName = "ServiceState";
            this.serviceStateDataGridViewTextBoxColumn.HeaderText = "Состояние";
            this.serviceStateDataGridViewTextBoxColumn.Name = "serviceStateDataGridViewTextBoxColumn";
            this.serviceStateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // costPerHourDataGridViewTextBoxColumn
            // 
            this.costPerHourDataGridViewTextBoxColumn.DataPropertyName = "CostPerHour";
            this.costPerHourDataGridViewTextBoxColumn.HeaderText = "Стоимость в час";
            this.costPerHourDataGridViewTextBoxColumn.Name = "costPerHourDataGridViewTextBoxColumn";
            this.costPerHourDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userCountDataGridViewTextBoxColumn1
            // 
            this.userCountDataGridViewTextBoxColumn1.DataPropertyName = "UserCount";
            this.userCountDataGridViewTextBoxColumn1.HeaderText = "Кол-во пользователей";
            this.userCountDataGridViewTextBoxColumn1.Name = "userCountDataGridViewTextBoxColumn1";
            this.userCountDataGridViewTextBoxColumn1.ReadOnly = true;
            this.userCountDataGridViewTextBoxColumn1.Width = 150;
            // 
            // cms_Service
            // 
            this.cms_Service.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddNewService,
            this.tsmi_EditSelectedService,
            this.tsmi_DeleteSelectedService,
            this.toolStripSeparator2,
            this.tsmi_AboutSelectedService,
            this.toolStripSeparator3,
            this.tsmi_ShowAllServices});
            this.cms_Service.Name = "cms_Service";
            this.cms_Service.Size = new System.Drawing.Size(246, 126);
            // 
            // tsmi_AddNewService
            // 
            this.tsmi_AddNewService.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.tsmi_AddNewService.Name = "tsmi_AddNewService";
            this.tsmi_AddNewService.Size = new System.Drawing.Size(245, 22);
            this.tsmi_AddNewService.Text = "Создать новый";
            // 
            // tsmi_EditSelectedService
            // 
            this.tsmi_EditSelectedService.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.tsmi_EditSelectedService.Name = "tsmi_EditSelectedService";
            this.tsmi_EditSelectedService.Size = new System.Drawing.Size(245, 22);
            this.tsmi_EditSelectedService.Text = "Редактировать выбранный";
            // 
            // tsmi_DeleteSelectedService
            // 
            this.tsmi_DeleteSelectedService.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.tsmi_DeleteSelectedService.Name = "tsmi_DeleteSelectedService";
            this.tsmi_DeleteSelectedService.Size = new System.Drawing.Size(245, 22);
            this.tsmi_DeleteSelectedService.Text = "Удалить выбранный";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(242, 6);
            // 
            // tsmi_AboutSelectedService
            // 
            this.tsmi_AboutSelectedService.Image = global::MTF_Services.WinForms.Properties.Resources.info;
            this.tsmi_AboutSelectedService.Name = "tsmi_AboutSelectedService";
            this.tsmi_AboutSelectedService.Size = new System.Drawing.Size(245, 22);
            this.tsmi_AboutSelectedService.Text = "Просмотр подробной информации";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(242, 6);
            // 
            // tsmi_ShowAllServices
            // 
            this.tsmi_ShowAllServices.Image = global::MTF_Services.WinForms.Properties.Resources.eye;
            this.tsmi_ShowAllServices.Name = "tsmi_ShowAllServices";
            this.tsmi_ShowAllServices.Size = new System.Drawing.Size(245, 22);
            this.tsmi_ShowAllServices.Text = "Просмотр всех сервисов";
            // 
            // serviceDetailInfoBindingSource
            // 
            this.serviceDetailInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServiceDetailInfo);
            this.serviceDetailInfoBindingSource.CurrentChanged += new System.EventHandler(this.serviceDetailInfoBindingSource_CurrentChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picBtn_EditSelectedService);
            this.panel2.Controls.Add(this.picBtn_DeleteSelectedService);
            this.panel2.Controls.Add(this.picBtn_AddNewService);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(37, 185);
            this.panel2.TabIndex = 8;
            // 
            // ListOfServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 484);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "ListOfServicesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование платформ и сервисов";
            this.Activated += new System.EventHandler(this.ListOfServicesForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListOfServicesForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListOfServicesForm_KeyDown);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSelectedPlatform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddNewPlatform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSelectedService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddNewService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_EditSelectedPlatform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_EditSelectedService)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Platforms)).EndInit();
            this.cms_Platform.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.platformServiceUserBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Services)).EndInit();
            this.cms_Service.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serviceDetailInfoBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource platformServiceUserBindingSource;
        private System.Windows.Forms.BindingSource serviceDetailInfoBindingSource;
        private System.Windows.Forms.DataGridView dg_Platforms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBtn_DeleteSelectedPlatform;
        private System.Windows.Forms.PictureBox picBtn_AddNewPlatform;
        private System.Windows.Forms.DataGridView dg_Services;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBtn_DeleteSelectedService;
        private System.Windows.Forms.PictureBox picBtn_AddNewService;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paasTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deleteDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costPerHourDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userCountDataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PlatformsCount;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ServiceTotalCount;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ServiceSelectedCount;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ttmi_ShowAllServices;
        private System.Windows.Forms.ToolStripMenuItem ttmi_ShowServicesBySelectedPlatform;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.PictureBox picBtn_EditSelectedPlatform;
        private System.Windows.Forms.PictureBox picBtn_EditSelectedService;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem созданиеНовойПлатформыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеВыбраннойПлатформыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеВыбраннойПлатформыToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem созданиеНовогоСервисаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеВыбранногоСервисаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеВыбранногоСервисаToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_Service;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddNewService;
        private System.Windows.Forms.ToolStripMenuItem tsmi_EditSelectedService;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DeleteSelectedService;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AboutSelectedService;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ShowAllServices;
        private System.Windows.Forms.ContextMenuStrip cms_Platform;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddNewPlatfrom;
        private System.Windows.Forms.ToolStripMenuItem tsmi_EditSelectedPlatform;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DeleteSelectedPlatform;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AboutSelectedPlatform;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ShowServicesBySelectedPlatform;
    }
}