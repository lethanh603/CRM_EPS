namespace SOURCE_FORM_QUOTATION.Presentation
{
    partial class frm_ReportQuotationDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ReportQuotationDetail));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gct_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_EXPORTDETAIL_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_EXPORTDETAIL_C)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_exit_S);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 413);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(905, 41);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(818, 6);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 21;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gct_list_C);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(905, 413);
            this.panelControl2.TabIndex = 1;
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(2, 2);
            this.gct_list_C.MainView = this.gv_EXPORTDETAIL_C;
            this.gct_list_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(901, 409);
            this.gct_list_C.TabIndex = 100;
            this.gct_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_EXPORTDETAIL_C});
            // 
            // gv_EXPORTDETAIL_C
            // 
            this.gv_EXPORTDETAIL_C.GridControl = this.gct_list_C;
            this.gv_EXPORTDETAIL_C.IndicatorWidth = 35;
            this.gv_EXPORTDETAIL_C.Name = "gv_EXPORTDETAIL_C";
            this.gv_EXPORTDETAIL_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_EXPORTDETAIL_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_EXPORTDETAIL_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_EXPORTDETAIL_C.OptionsView.ShowGroupPanel = false;
            this.gv_EXPORTDETAIL_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_EXPORTDETAIL_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // frm_ReportQuotationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 454);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_ReportQuotationDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết báo giá";
            this.Load += new System.EventHandler(this.frm_ReportQuotationDetail_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ReportQuotationDetail_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_EXPORTDETAIL_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraGrid.GridControl gct_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_EXPORTDETAIL_C;
    }
}