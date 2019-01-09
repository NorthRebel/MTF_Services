namespace MTF_Services.WinForms.Forms.Employee
{
    partial class SubscribeServicesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubscribeServicesForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_RemoveFromUsed = new System.Windows.Forms.Button();
            this.btn_AddToUsed = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lst_UsedServices = new System.Windows.Forms.ListBox();
            this.UsedServiceTypeBS = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lst_AvalibleServices = new System.Windows.Forms.ListBox();
            this.AvalibleServiceTypeBS = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsedServiceTypeBS)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvalibleServiceTypeBS)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_RemoveFromUsed
            // 
            this.btn_RemoveFromUsed.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_RemoveFromUsed.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveFromUsed.Location = new System.Drawing.Point(3, 202);
            this.btn_RemoveFromUsed.Name = "btn_RemoveFromUsed";
            this.btn_RemoveFromUsed.Size = new System.Drawing.Size(38, 93);
            this.btn_RemoveFromUsed.TabIndex = 1;
            this.btn_RemoveFromUsed.Text = "<-";
            this.toolTip.SetToolTip(this.btn_RemoveFromUsed, "Отписаться от выбранного сервиса");
            this.btn_RemoveFromUsed.UseVisualStyleBackColor = true;
            this.btn_RemoveFromUsed.Click += new System.EventHandler(this.btn_RemoveFromUsed_Click);
            // 
            // btn_AddToUsed
            // 
            this.btn_AddToUsed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_AddToUsed.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddToUsed.Location = new System.Drawing.Point(3, 103);
            this.btn_AddToUsed.Name = "btn_AddToUsed";
            this.btn_AddToUsed.Size = new System.Drawing.Size(38, 93);
            this.btn_AddToUsed.TabIndex = 0;
            this.btn_AddToUsed.Text = "->";
            this.toolTip.SetToolTip(this.btn_AddToUsed, "Подписаться на доступный сервис");
            this.btn_AddToUsed.UseVisualStyleBackColor = true;
            this.btn_AddToUsed.Click += new System.EventHandler(this.btn_AddToUsed_Click);
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
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 405);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(624, 40);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OK.Location = new System.Drawing.Point(479, 4);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(143, 31);
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
            this.btn_Cancel.Location = new System.Drawing.Point(312, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(163, 31);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Отмена";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
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
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(624, 405);
            this.tableLayoutPanel7.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lst_UsedServices);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(340, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(281, 399);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Выбранные сервисы";
            // 
            // lst_UsedServices
            // 
            this.lst_UsedServices.DataSource = this.UsedServiceTypeBS;
            this.lst_UsedServices.DisplayMember = "Name";
            this.lst_UsedServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_UsedServices.FormattingEnabled = true;
            this.lst_UsedServices.ItemHeight = 21;
            this.lst_UsedServices.Location = new System.Drawing.Point(3, 25);
            this.lst_UsedServices.Name = "lst_UsedServices";
            this.lst_UsedServices.Size = new System.Drawing.Size(275, 371);
            this.lst_UsedServices.TabIndex = 0;
            this.lst_UsedServices.ValueMember = "Id";
            // 
            // UsedServiceTypeBS
            // 
            this.UsedServiceTypeBS.DataSource = typeof(MTF_Services.Model.ServiceType);
            this.UsedServiceTypeBS.CurrentChanged += new System.EventHandler(this.UsedServiceTypeBS_CurrentChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lst_AvalibleServices);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(281, 399);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Доступные сервисы";
            // 
            // lst_AvalibleServices
            // 
            this.lst_AvalibleServices.DataSource = this.AvalibleServiceTypeBS;
            this.lst_AvalibleServices.DisplayMember = "Name";
            this.lst_AvalibleServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_AvalibleServices.FormattingEnabled = true;
            this.lst_AvalibleServices.ItemHeight = 21;
            this.lst_AvalibleServices.Location = new System.Drawing.Point(3, 25);
            this.lst_AvalibleServices.Name = "lst_AvalibleServices";
            this.lst_AvalibleServices.Size = new System.Drawing.Size(275, 371);
            this.lst_AvalibleServices.TabIndex = 0;
            this.lst_AvalibleServices.ValueMember = "Id";
            // 
            // AvalibleServiceTypeBS
            // 
            this.AvalibleServiceTypeBS.DataSource = typeof(MTF_Services.Model.ServiceType);
            this.AvalibleServiceTypeBS.CurrentChanged += new System.EventHandler(this.AvalibleServiceTypeBS_CurrentChanged);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.btn_RemoveFromUsed, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.btn_AddToUsed, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(290, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(44, 399);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // SubscribeServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "SubscribeServicesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подписка на доступные сервисы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubscribeServicesForm_FormClosing);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UsedServiceTypeBS)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AvalibleServiceTypeBS)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lst_UsedServices;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lst_AvalibleServices;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button btn_RemoveFromUsed;
        private System.Windows.Forms.Button btn_AddToUsed;
        private System.Windows.Forms.BindingSource AvalibleServiceTypeBS;
        private System.Windows.Forms.BindingSource UsedServiceTypeBS;
    }
}