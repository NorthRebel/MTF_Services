namespace MTF_Services.WinForms.Forms
{
    partial class ReportingForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportingForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tip_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ServiceDetailInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ServiceRequestItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SoftwareInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusBar.SuspendLayout();
            this.menuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceDetailInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRequestItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoftwareInfoBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.statusBar.Size = new System.Drawing.Size(624, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.печатьToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(624, 24);
            this.menuBar.TabIndex = 0;
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.печатьToolStripMenuItem.Text = "Печать";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ds";
            reportDataSource1.Value = this.ServiceRequestItemBindingSource;
            reportDataSource2.Name = "ds_SoftwareInfo";
            reportDataSource2.Value = this.SoftwareInfoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MTF_Services.WinForms.Reports.ServiceRequestReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 24);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(624, 399);
            this.reportViewer1.TabIndex = 2;
            // 
            // ServiceDetailInfoBindingSource
            // 
            this.ServiceDetailInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServiceDetailInfo);
            // 
            // ServiceRequestItemBindingSource
            // 
            this.ServiceRequestItemBindingSource.DataSource = typeof(MTF_Services.Model.Views.ServiceRequestItem);
            // 
            // SoftwareInfoBindingSource
            // 
            this.SoftwareInfoBindingSource.DataSource = typeof(MTF_Services.Model.Views.SoftwareInfo);
            // 
            // ReportingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "ReportingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Предварительный просмотр отчета";
            this.Load += new System.EventHandler(this.ReportingForm_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceDetailInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRequestItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoftwareInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel tip_Label;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ServiceDetailInfoBindingSource;
        private System.Windows.Forms.BindingSource ServiceRequestItemBindingSource;
        private System.Windows.Forms.BindingSource SoftwareInfoBindingSource;
    }
}