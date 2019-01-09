namespace MTF_Services.WinForms.Forms.Employee
{
    partial class EmployeeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeForm));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подписатьсяНаНовыйСервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.dg_Service = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cms_Service = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Service)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userServiceBindingSource)).BeginInit();
            this.cms_Service.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.правкаToolStripMenuItem,
            this.toolStripMenuItem3});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(624, 24);
            this.menuBar.TabIndex = 0;
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подписатьсяНаНовыйСервисToolStripMenuItem,
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // подписатьсяНаНовыйСервисToolStripMenuItem
            // 
            this.подписатьсяНаНовыйСервисToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.подписатьсяНаНовыйСервисToolStripMenuItem.Name = "подписатьсяНаНовыйСервисToolStripMenuItem";
            this.подписатьсяНаНовыйСервисToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.подписатьсяНаНовыйСервисToolStripMenuItem.Text = "Подписаться на новый сервис";
            this.подписатьсяНаНовыйСервисToolStripMenuItem.Click += new System.EventHandler(this.подписатьсяНаНовыйСервисToolStripMenuItem_Click);
            // 
            // отписатьсяОтВыбранногоСервисаToolStripMenuItem
            // 
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem.Name = "отписатьсяОтВыбранногоСервисаToolStripMenuItem";
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem.Text = "Отписаться от выбранного сервиса";
            this.отписатьсяОтВыбранногоСервисаToolStripMenuItem.Click += new System.EventHandler(this.отписатьсяОтВыбранногоСервисаToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label});
            this.statusBar.Location = new System.Drawing.Point(0, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(624, 22);
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
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(624, 25);
            this.toolBar.TabIndex = 2;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.подписатьсяНаНовыйСервисToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.отписатьсяОтВыбранногоСервисаToolStripMenuItem_Click);
            // 
            // dg_Service
            // 
            this.dg_Service.AllowUserToAddRows = false;
            this.dg_Service.AllowUserToDeleteRows = false;
            this.dg_Service.AutoGenerateColumns = false;
            this.dg_Service.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dg_Service.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Service.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.PaasName,
            this.stateDataGridViewTextBoxColumn});
            this.dg_Service.DataSource = this.userServiceBindingSource;
            this.dg_Service.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Service.Location = new System.Drawing.Point(0, 49);
            this.dg_Service.Name = "dg_Service";
            this.dg_Service.ReadOnly = true;
            this.dg_Service.RowHeadersVisible = false;
            this.dg_Service.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Service.Size = new System.Drawing.Size(624, 374);
            this.dg_Service.TabIndex = 9;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 180;
            // 
            // PaasName
            // 
            this.PaasName.DataPropertyName = "PaasName";
            this.PaasName.HeaderText = "Платформа";
            this.PaasName.Name = "PaasName";
            this.PaasName.ReadOnly = true;
            this.PaasName.Width = 130;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "Состояние";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            this.stateDataGridViewTextBoxColumn.Width = 150;
            // 
            // userServiceBindingSource
            // 
            this.userServiceBindingSource.DataSource = typeof(MTF_Services.Model.Views.UserService);
            this.userServiceBindingSource.CurrentChanged += new System.EventHandler(this.userServiceBindingSource_CurrentChanged);
            // 
            // cms_Service
            // 
            this.cms_Service.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.cms_Service.Name = "cms_Service";
            this.cms_Service.Size = new System.Drawing.Size(257, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(256, 22);
            this.toolStripMenuItem1.Text = "Подписаться на новый сервис";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.подписатьсяНаНовыйСервисToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::MTF_Services.WinForms.Properties.Resources.edit_remove;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(256, 22);
            this.toolStripMenuItem2.Text = "Отписаться от выбранного сервиса";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.отписатьсяОтВыбранногоСервисаToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator2,
            this.toolStripMenuItem6});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(99, 20);
            this.toolStripMenuItem3.Text = "Учетная запись";
            this.toolStripMenuItem3.ToolTipText = "Управление текущей учетной записью";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::MTF_Services.WinForms.Properties.Resources.user;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem4.Text = "Просмотр профиля";
            this.toolStripMenuItem4.ToolTipText = "Просмотр и изменение учетной записи";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.просмотрПрофиляToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = global::MTF_Services.WinForms.Properties.Resources.application_exit;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem5.Text = "Сменить пользователя";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.сменитьПользователяToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = global::MTF_Services.WinForms.Properties.Resources.exit;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem6.Text = "Завершение работы";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.завершениеРаботыToolStripMenuItem_Click);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.dg_Service);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "EmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ПАО МТФ - Облачные вычисления [Сотрудник]";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Service)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userServiceBindingSource)).EndInit();
            this.cms_Service.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.DataGridView dg_Service;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подписатьсяНаНовыйСервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отписатьсяОтВыбранногоСервисаToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_Service;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.BindingSource userServiceBindingSource;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    }
}