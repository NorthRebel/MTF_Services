namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    partial class PlanningSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanningSchedule));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.всеЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запланированныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.завершенныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.длительностьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.минутыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.часыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяПозицияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьВыбраннуюПозициюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВыбраннуюПозициюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PositionTotalCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_PositionSelectedCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.dg_Schedule = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beginDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.минутыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.часыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Schedule)).BeginInit();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.видToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(635, 24);
            this.menuBar.TabIndex = 0;
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.всеЗаписиToolStripMenuItem,
            this.запланированныеToolStripMenuItem,
            this.завершенныеToolStripMenuItem,
            this.toolStripSeparator1,
            this.длительностьToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // всеЗаписиToolStripMenuItem
            // 
            this.всеЗаписиToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.beos_query;
            this.всеЗаписиToolStripMenuItem.Name = "всеЗаписиToolStripMenuItem";
            this.всеЗаписиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.всеЗаписиToolStripMenuItem.Text = "Все записи";
            this.всеЗаписиToolStripMenuItem.Click += new System.EventHandler(this.всеЗаписиToolStripMenuItem_Click);
            // 
            // запланированныеToolStripMenuItem
            // 
            this.запланированныеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.scheduled_tasks;
            this.запланированныеToolStripMenuItem.Name = "запланированныеToolStripMenuItem";
            this.запланированныеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.запланированныеToolStripMenuItem.Text = "Запланированные";
            this.запланированныеToolStripMenuItem.Click += new System.EventHandler(this.запланированныеToolStripMenuItem_Click);
            // 
            // завершенныеToolStripMenuItem
            // 
            this.завершенныеToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.tasks;
            this.завершенныеToolStripMenuItem.Name = "завершенныеToolStripMenuItem";
            this.завершенныеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.завершенныеToolStripMenuItem.Text = "Завершенные";
            this.завершенныеToolStripMenuItem.Click += new System.EventHandler(this.завершенныеToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // длительностьToolStripMenuItem
            // 
            this.длительностьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.минутыToolStripMenuItem,
            this.часыToolStripMenuItem});
            this.длительностьToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.gnome_panel_clock;
            this.длительностьToolStripMenuItem.Name = "длительностьToolStripMenuItem";
            this.длительностьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.длительностьToolStripMenuItem.Text = "Длительность";
            // 
            // минутыToolStripMenuItem
            // 
            this.минутыToolStripMenuItem.Checked = true;
            this.минутыToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.минутыToolStripMenuItem.Name = "минутыToolStripMenuItem";
            this.минутыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.минутыToolStripMenuItem.Text = "Минуты";
            this.минутыToolStripMenuItem.Click += new System.EventHandler(this.минутыToolStripMenuItem_Click);
            // 
            // часыToolStripMenuItem
            // 
            this.часыToolStripMenuItem.Name = "часыToolStripMenuItem";
            this.часыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.часыToolStripMenuItem.Text = "Часы";
            this.часыToolStripMenuItem.Click += new System.EventHandler(this.часыToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяПозицияToolStripMenuItem,
            this.редактироватьВыбраннуюПозициюToolStripMenuItem,
            this.удалитьВыбраннуюПозициюToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // новаяПозицияToolStripMenuItem
            // 
            this.новаяПозицияToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.новаяПозицияToolStripMenuItem.Name = "новаяПозицияToolStripMenuItem";
            this.новаяПозицияToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.новаяПозицияToolStripMenuItem.Text = "Новая позиция";
            this.новаяПозицияToolStripMenuItem.Click += new System.EventHandler(this.новаяПозицияToolStripMenuItem_Click);
            // 
            // редактироватьВыбраннуюПозициюToolStripMenuItem
            // 
            this.редактироватьВыбраннуюПозициюToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.редактироватьВыбраннуюПозициюToolStripMenuItem.Name = "редактироватьВыбраннуюПозициюToolStripMenuItem";
            this.редактироватьВыбраннуюПозициюToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.редактироватьВыбраннуюПозициюToolStripMenuItem.Text = "Редактировать выбранную позицию";
            this.редактироватьВыбраннуюПозициюToolStripMenuItem.Click += new System.EventHandler(this.редактироватьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // удалитьВыбраннуюПозициюToolStripMenuItem
            // 
            this.удалитьВыбраннуюПозициюToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.удалитьВыбраннуюПозициюToolStripMenuItem.Name = "удалитьВыбраннуюПозициюToolStripMenuItem";
            this.удалитьВыбраннуюПозициюToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.удалитьВыбраннуюПозициюToolStripMenuItem.Text = "Удалить выбранную позицию";
            this.удалитьВыбраннуюПозициюToolStripMenuItem.Click += new System.EventHandler(this.удалитьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label,
            this.lbl_PositionTotalCount,
            this.lbl_PositionSelectedCount});
            this.statusBar.Location = new System.Drawing.Point(0, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(635, 22);
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
            // lbl_PositionTotalCount
            // 
            this.lbl_PositionTotalCount.Name = "lbl_PositionTotalCount";
            this.lbl_PositionTotalCount.Size = new System.Drawing.Size(127, 17);
            this.lbl_PositionTotalCount.Text = "Общее кол-во позиций:";
            // 
            // lbl_PositionSelectedCount
            // 
            this.lbl_PositionSelectedCount.Name = "lbl_PositionSelectedCount";
            this.lbl_PositionSelectedCount.Size = new System.Drawing.Size(105, 17);
            this.lbl_PositionSelectedCount.Text = "Отобрано позиций:";
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator4,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(635, 25);
            this.toolBar.TabIndex = 2;
            // 
            // dg_Schedule
            // 
            this.dg_Schedule.AllowUserToAddRows = false;
            this.dg_Schedule.AllowUserToDeleteRows = false;
            this.dg_Schedule.AutoGenerateColumns = false;
            this.dg_Schedule.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Schedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.beginDateDataGridViewTextBoxColumn,
            this.endDateDataGridViewTextBoxColumn,
            this.durationDataGridViewTextBoxColumn});
            this.dg_Schedule.ContextMenuStrip = this.cms;
            this.dg_Schedule.DataSource = this.scheduleItemBindingSource;
            this.dg_Schedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Schedule.Location = new System.Drawing.Point(0, 49);
            this.dg_Schedule.Name = "dg_Schedule";
            this.dg_Schedule.ReadOnly = true;
            this.dg_Schedule.RowHeadersVisible = false;
            this.dg_Schedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Schedule.Size = new System.Drawing.Size(635, 374);
            this.dg_Schedule.TabIndex = 7;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "№ п/п";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // beginDateDataGridViewTextBoxColumn
            // 
            this.beginDateDataGridViewTextBoxColumn.DataPropertyName = "BeginDate";
            this.beginDateDataGridViewTextBoxColumn.HeaderText = "Начало";
            this.beginDateDataGridViewTextBoxColumn.Name = "beginDateDataGridViewTextBoxColumn";
            this.beginDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.beginDateDataGridViewTextBoxColumn.Width = 150;
            // 
            // endDateDataGridViewTextBoxColumn
            // 
            this.endDateDataGridViewTextBoxColumn.DataPropertyName = "EndDate";
            this.endDateDataGridViewTextBoxColumn.HeaderText = "Окончание";
            this.endDateDataGridViewTextBoxColumn.Name = "endDateDataGridViewTextBoxColumn";
            this.endDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.endDateDataGridViewTextBoxColumn.Width = 150;
            // 
            // durationDataGridViewTextBoxColumn
            // 
            this.durationDataGridViewTextBoxColumn.DataPropertyName = "Duration";
            this.durationDataGridViewTextBoxColumn.HeaderText = "Длительность";
            this.durationDataGridViewTextBoxColumn.Name = "durationDataGridViewTextBoxColumn";
            this.durationDataGridViewTextBoxColumn.ReadOnly = true;
            this.durationDataGridViewTextBoxColumn.Width = 200;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator2,
            this.toolStripMenuItem4});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(263, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.toolStripMenuItem1.Text = "Новая позиция";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.новаяПозицияToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(262, 22);
            this.toolStripMenuItem2.Text = "Редактировать выбранную позицию";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.редактироватьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(262, 22);
            this.toolStripMenuItem3.Text = "Удалить выбранную позицию";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.удалитьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(259, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.toolStripMenuItem4.Image = global::MTF_Services.WinForms.Properties.Resources.gnome_panel_clock;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(262, 22);
            this.toolStripMenuItem4.Text = "Длительность";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Checked = true;
            this.toolStripMenuItem5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem5.Text = "Минуты";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.минутыToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem6.Text = "Часы";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.часыToolStripMenuItem_Click);
            // 
            // scheduleItemBindingSource
            // 
            this.scheduleItemBindingSource.DataSource = typeof(MTF_Services.Model.Views.ScheduleItem);
            this.scheduleItemBindingSource.CurrentChanged += new System.EventHandler(this.scheduleItemBindingSource_CurrentChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MTF_Services.WinForms.Properties.Resources.beos_query;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.всеЗаписиToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::MTF_Services.WinForms.Properties.Resources.scheduled_tasks;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.запланированныеToolStripMenuItem_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MTF_Services.WinForms.Properties.Resources.tasks;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.завершенныеToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.минутыToolStripMenuItem1,
            this.часыToolStripMenuItem1});
            this.toolStripDropDownButton1.Image = global::MTF_Services.WinForms.Properties.Resources.gnome_panel_clock;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.новаяПозицияToolStripMenuItem_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::MTF_Services.WinForms.Properties.Resources.edit;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.Click += new System.EventHandler(this.редактироватьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.Click += new System.EventHandler(this.удалитьВыбраннуюПозициюToolStripMenuItem_Click);
            // 
            // минутыToolStripMenuItem1
            // 
            this.минутыToolStripMenuItem1.Name = "минутыToolStripMenuItem1";
            this.минутыToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.минутыToolStripMenuItem1.Text = "Минуты";
            this.минутыToolStripMenuItem1.Click += new System.EventHandler(this.минутыToolStripMenuItem_Click);
            // 
            // часыToolStripMenuItem1
            // 
            this.часыToolStripMenuItem1.Name = "часыToolStripMenuItem1";
            this.часыToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.часыToolStripMenuItem1.Text = "Часы";
            this.часыToolStripMenuItem1.Click += new System.EventHandler(this.часыToolStripMenuItem_Click);
            // 
            // PlanningSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 445);
            this.Controls.Add(this.dg_Schedule);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "PlanningSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Планирование расписания обслуживания";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Schedule)).EndInit();
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scheduleItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.DataGridView dg_Schedule;
        private System.Windows.Forms.BindingSource scheduleItemBindingSource;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem всеЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запланированныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem завершенныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem длительностьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem минутыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem часыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяПозицияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьВыбраннуюПозициюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьВыбраннуюПозициюToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn beginDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PositionTotalCount;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PositionSelectedCount;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem минутыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem часыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
    }
}