namespace MTF_Services.WinForms.Forms.SysAdmin.Services
{
    partial class EditPlatformForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPlatformForm));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picBtn_DeleteServer = new System.Windows.Forms.PictureBox();
            this.picBtn_AddServer = new System.Windows.Forms.PictureBox();
            this.picBtn_DeleteSAN = new System.Windows.Forms.PictureBox();
            this.picBtn_AddSAN = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.paasTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.platformDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avalibleCoreCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usedCoreCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avalibleRAMVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usedRAMVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avalibleStorageVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usedStorageVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverPaasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avalibleVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usedVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sANPaasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddSAN)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paasTypeBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPaasInfoBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANPaasInfoBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip_Label});
            this.statusBar.Location = new System.Drawing.Point(0, 507);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1036, 22);
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
            // picBtn_DeleteServer
            // 
            this.picBtn_DeleteServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_DeleteServer.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_DeleteServer.Location = new System.Drawing.Point(3, 39);
            this.picBtn_DeleteServer.Name = "picBtn_DeleteServer";
            this.picBtn_DeleteServer.Size = new System.Drawing.Size(30, 30);
            this.picBtn_DeleteServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_DeleteServer.TabIndex = 14;
            this.picBtn_DeleteServer.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_DeleteServer, "Удалить выбранный");
            this.picBtn_DeleteServer.Click += new System.EventHandler(this.picBtn_DeleteServer_Click);
            // 
            // picBtn_AddServer
            // 
            this.picBtn_AddServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_AddServer.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.picBtn_AddServer.Location = new System.Drawing.Point(3, 3);
            this.picBtn_AddServer.Name = "picBtn_AddServer";
            this.picBtn_AddServer.Size = new System.Drawing.Size(30, 30);
            this.picBtn_AddServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_AddServer.TabIndex = 12;
            this.picBtn_AddServer.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_AddServer, "Добавить новый");
            this.picBtn_AddServer.Click += new System.EventHandler(this.picBtn_AddServer_Click);
            // 
            // picBtn_DeleteSAN
            // 
            this.picBtn_DeleteSAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_DeleteSAN.Image = global::MTF_Services.WinForms.Properties.Resources.delete;
            this.picBtn_DeleteSAN.Location = new System.Drawing.Point(3, 39);
            this.picBtn_DeleteSAN.Name = "picBtn_DeleteSAN";
            this.picBtn_DeleteSAN.Size = new System.Drawing.Size(30, 30);
            this.picBtn_DeleteSAN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_DeleteSAN.TabIndex = 14;
            this.picBtn_DeleteSAN.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_DeleteSAN, "Удалить выбранный");
            this.picBtn_DeleteSAN.Click += new System.EventHandler(this.picBtn_DeleteSAN_Click);
            // 
            // picBtn_AddSAN
            // 
            this.picBtn_AddSAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_AddSAN.Image = global::MTF_Services.WinForms.Properties.Resources.plus;
            this.picBtn_AddSAN.Location = new System.Drawing.Point(3, 3);
            this.picBtn_AddSAN.Name = "picBtn_AddSAN";
            this.picBtn_AddSAN.Size = new System.Drawing.Size(30, 30);
            this.picBtn_AddSAN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtn_AddSAN.TabIndex = 12;
            this.picBtn_AddSAN.TabStop = false;
            this.toolTip.SetToolTip(this.picBtn_AddSAN, "Добавить новый");
            this.picBtn_AddSAN.Click += new System.EventHandler(this.picBtn_AddSAN_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1036, 50);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.paasTypeBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(126, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(907, 29);
            this.textBox1.TabIndex = 1;
            // 
            // paasTypeBindingSource
            // 
            this.paasTypeBindingSource.DataSource = typeof(MTF_Services.Model.PaasType);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1036, 457);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Azure;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.btn_Save, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Cancel, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 409);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1030, 45);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(889, 7);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(138, 31);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "Сохранить";
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Cancel.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Cancel.Location = new System.Drawing.Point(747, 7);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(136, 31);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1030, 197);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сервера";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.platformDataGridViewTextBoxColumn,
            this.cPUDataGridViewTextBoxColumn,
            this.avalibleCoreCountDataGridViewTextBoxColumn,
            this.usedCoreCountDataGridViewTextBoxColumn,
            this.avalibleRAMVolumeDataGridViewTextBoxColumn,
            this.usedRAMVolumeDataGridViewTextBoxColumn,
            this.avalibleStorageVolumeDataGridViewTextBoxColumn,
            this.usedStorageVolumeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.serverPaasInfoBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(40, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(987, 169);
            this.dataGridView1.TabIndex = 5;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "№ п/п";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 80;
            // 
            // platformDataGridViewTextBoxColumn
            // 
            this.platformDataGridViewTextBoxColumn.DataPropertyName = "Platform";
            this.platformDataGridViewTextBoxColumn.HeaderText = "Платформа";
            this.platformDataGridViewTextBoxColumn.Name = "platformDataGridViewTextBoxColumn";
            this.platformDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cPUDataGridViewTextBoxColumn
            // 
            this.cPUDataGridViewTextBoxColumn.DataPropertyName = "CPU";
            this.cPUDataGridViewTextBoxColumn.HeaderText = "Процессор";
            this.cPUDataGridViewTextBoxColumn.Name = "cPUDataGridViewTextBoxColumn";
            this.cPUDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // avalibleCoreCountDataGridViewTextBoxColumn
            // 
            this.avalibleCoreCountDataGridViewTextBoxColumn.DataPropertyName = "AvalibleCoreCount";
            this.avalibleCoreCountDataGridViewTextBoxColumn.HeaderText = "Доступное кол-во ядер";
            this.avalibleCoreCountDataGridViewTextBoxColumn.Name = "avalibleCoreCountDataGridViewTextBoxColumn";
            this.avalibleCoreCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usedCoreCountDataGridViewTextBoxColumn
            // 
            this.usedCoreCountDataGridViewTextBoxColumn.DataPropertyName = "UsedCoreCount";
            this.usedCoreCountDataGridViewTextBoxColumn.HeaderText = "Использованое кол-во ядер";
            this.usedCoreCountDataGridViewTextBoxColumn.Name = "usedCoreCountDataGridViewTextBoxColumn";
            this.usedCoreCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.usedCoreCountDataGridViewTextBoxColumn.Width = 120;
            // 
            // avalibleRAMVolumeDataGridViewTextBoxColumn
            // 
            this.avalibleRAMVolumeDataGridViewTextBoxColumn.DataPropertyName = "AvalibleRAMVolume";
            this.avalibleRAMVolumeDataGridViewTextBoxColumn.HeaderText = "Доступный объем ОЗУ";
            this.avalibleRAMVolumeDataGridViewTextBoxColumn.Name = "avalibleRAMVolumeDataGridViewTextBoxColumn";
            this.avalibleRAMVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usedRAMVolumeDataGridViewTextBoxColumn
            // 
            this.usedRAMVolumeDataGridViewTextBoxColumn.DataPropertyName = "UsedRAMVolume";
            this.usedRAMVolumeDataGridViewTextBoxColumn.HeaderText = "Используемый объем ОЗУ";
            this.usedRAMVolumeDataGridViewTextBoxColumn.Name = "usedRAMVolumeDataGridViewTextBoxColumn";
            this.usedRAMVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            this.usedRAMVolumeDataGridViewTextBoxColumn.Width = 120;
            // 
            // avalibleStorageVolumeDataGridViewTextBoxColumn
            // 
            this.avalibleStorageVolumeDataGridViewTextBoxColumn.DataPropertyName = "AvalibleStorageVolume";
            this.avalibleStorageVolumeDataGridViewTextBoxColumn.HeaderText = "Доступный объем накопителя";
            this.avalibleStorageVolumeDataGridViewTextBoxColumn.Name = "avalibleStorageVolumeDataGridViewTextBoxColumn";
            this.avalibleStorageVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usedStorageVolumeDataGridViewTextBoxColumn
            // 
            this.usedStorageVolumeDataGridViewTextBoxColumn.DataPropertyName = "UsedStorageVolume";
            this.usedStorageVolumeDataGridViewTextBoxColumn.HeaderText = "Используемый объем накопителя";
            this.usedStorageVolumeDataGridViewTextBoxColumn.Name = "usedStorageVolumeDataGridViewTextBoxColumn";
            this.usedStorageVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            this.usedStorageVolumeDataGridViewTextBoxColumn.Width = 130;
            // 
            // serverPaasInfoBindingSource
            // 
            this.serverPaasInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServerPaasInfo);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBtn_DeleteServer);
            this.panel1.Controls.Add(this.picBtn_AddServer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(37, 169);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1030, 197);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Хранилища данных";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.manufacturerDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.avalibleVolumeDataGridViewTextBoxColumn,
            this.usedVolumeDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.sANPaasInfoBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(40, 25);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(987, 169);
            this.dataGridView2.TabIndex = 6;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "№ п/п";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // manufacturerDataGridViewTextBoxColumn
            // 
            this.manufacturerDataGridViewTextBoxColumn.DataPropertyName = "Manufacturer";
            this.manufacturerDataGridViewTextBoxColumn.HeaderText = "Производитель";
            this.manufacturerDataGridViewTextBoxColumn.Name = "manufacturerDataGridViewTextBoxColumn";
            this.manufacturerDataGridViewTextBoxColumn.ReadOnly = true;
            this.manufacturerDataGridViewTextBoxColumn.Width = 150;
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Модель";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // avalibleVolumeDataGridViewTextBoxColumn
            // 
            this.avalibleVolumeDataGridViewTextBoxColumn.DataPropertyName = "AvalibleVolume";
            this.avalibleVolumeDataGridViewTextBoxColumn.HeaderText = "Доступный объем накопителей";
            this.avalibleVolumeDataGridViewTextBoxColumn.Name = "avalibleVolumeDataGridViewTextBoxColumn";
            this.avalibleVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            this.avalibleVolumeDataGridViewTextBoxColumn.Width = 180;
            // 
            // usedVolumeDataGridViewTextBoxColumn
            // 
            this.usedVolumeDataGridViewTextBoxColumn.DataPropertyName = "UsedVolume";
            this.usedVolumeDataGridViewTextBoxColumn.HeaderText = "Используемый объем накопителей";
            this.usedVolumeDataGridViewTextBoxColumn.Name = "usedVolumeDataGridViewTextBoxColumn";
            this.usedVolumeDataGridViewTextBoxColumn.ReadOnly = true;
            this.usedVolumeDataGridViewTextBoxColumn.Width = 180;
            // 
            // sANPaasInfoBindingSource
            // 
            this.sANPaasInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.SANPaasInfo);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picBtn_DeleteSAN);
            this.panel2.Controls.Add(this.picBtn_AddSAN);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(37, 169);
            this.panel2.TabIndex = 2;
            // 
            // EditPlatformForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 529);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "EditPlatformForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание новой платформы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditPlatformForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditPlatformForm_KeyDown);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_DeleteSAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_AddSAN)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paasTypeBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPaasInfoBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANPaasInfoBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBtn_DeleteServer;
        private System.Windows.Forms.PictureBox picBtn_AddServer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBtn_DeleteSAN;
        private System.Windows.Forms.PictureBox picBtn_AddSAN;
        private System.Windows.Forms.BindingSource paasTypeBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn platformDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avalibleCoreCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usedCoreCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avalibleRAMVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usedRAMVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avalibleStorageVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usedStorageVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource serverPaasInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avalibleVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usedVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sANPaasInfoBindingSource;
    }
}