namespace SOURCE_FORM_QUOTATION.Presentation
{
    partial class frmReportThongTinKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportThongTinKhachHang));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dte_todate_S = new DevExpress.XtraEditors.DateEdit();
            this.btn_tim_S = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.glue_idquotationtype_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glue_IDEMP_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dte_fromdate_S = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gct_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_EXPORTDETAIL_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idquotationtype_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_EXPORTDETAIL_C)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupControl2);
            this.panel1.Controls.Add(this.groupControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 53);
            this.panel1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btn_exit_S);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(522, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(335, 53);
            this.groupControl2.TabIndex = 22;
            this.groupControl2.Text = "groupControl2";
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_QUOTATION.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(258, 5);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(72, 41);
            this.btn_exit_S.TabIndex = 10;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dte_todate_S);
            this.groupControl1.Controls.Add(this.btn_tim_S);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.glue_idquotationtype_I1);
            this.groupControl1.Controls.Add(this.glue_IDEMP_I1);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.dte_fromdate_S);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(522, 53);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "groupControl1";
            // 
            // dte_todate_S
            // 
            this.dte_todate_S.EditValue = null;
            this.dte_todate_S.Location = new System.Drawing.Point(319, 5);
            this.dte_todate_S.Name = "dte_todate_S";
            this.dte_todate_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dte_todate_S.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_S.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_todate_S.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dte_todate_S.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dte_todate_S.Size = new System.Drawing.Size(103, 20);
            this.dte_todate_S.TabIndex = 2;
            // 
            // btn_tim_S
            // 
            this.btn_tim_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_tim_S.Image")));
            this.btn_tim_S.Location = new System.Drawing.Point(428, 5);
            this.btn_tim_S.Name = "btn_tim_S";
            this.btn_tim_S.Size = new System.Drawing.Size(76, 41);
            this.btn_tim_S.TabIndex = 8;
            this.btn_tim_S.Text = "F10  Tìm";
            this.btn_tim_S.Click += new System.EventHandler(this.btn_tim_S_Click);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Location = new System.Drawing.Point(527, 31);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(61, 13);
            this.labelControl12.TabIndex = 20;
            this.labelControl12.Text = "Loại báo giá:";
            this.labelControl12.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(10, 29);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 13);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "Nhân viên:";
            // 
            // glue_idquotationtype_I1
            // 
            this.glue_idquotationtype_I1.EditValue = "";
            this.glue_idquotationtype_I1.Location = new System.Drawing.Point(624, 29);
            this.glue_idquotationtype_I1.Name = "glue_idquotationtype_I1";
            this.glue_idquotationtype_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_idquotationtype_I1.Properties.View = this.gridView2;
            this.glue_idquotationtype_I1.Size = new System.Drawing.Size(48, 20);
            this.glue_idquotationtype_I1.TabIndex = 6;
            this.glue_idquotationtype_I1.Visible = false;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // glue_IDEMP_I1
            // 
            this.glue_IDEMP_I1.Location = new System.Drawing.Point(89, 26);
            this.glue_IDEMP_I1.Name = "glue_IDEMP_I1";
            this.glue_IDEMP_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_IDEMP_I1.Properties.View = this.gridView1;
            this.glue_IDEMP_I1.Size = new System.Drawing.Size(333, 20);
            this.glue_IDEMP_I1.TabIndex = 5;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Từ ngày:";
            // 
            // dte_fromdate_S
            // 
            this.dte_fromdate_S.EditValue = null;
            this.dte_fromdate_S.Location = new System.Drawing.Point(89, 5);
            this.dte_fromdate_S.Name = "dte_fromdate_S";
            this.dte_fromdate_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_S.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_S.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_fromdate_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_fromdate_S.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_fromdate_S.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_fromdate_S.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dte_fromdate_S.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dte_fromdate_S.Size = new System.Drawing.Size(98, 20);
            this.dte_fromdate_S.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(251, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 420);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(857, 10);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gct_list_C);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(857, 367);
            this.panel3.TabIndex = 2;
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(0, 0);
            this.gct_list_C.MainView = this.gv_EXPORTDETAIL_C;
            this.gct_list_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(857, 367);
            this.gct_list_C.TabIndex = 9;
            this.gct_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_EXPORTDETAIL_C});
            // 
            // gv_EXPORTDETAIL_C
            // 
            this.gv_EXPORTDETAIL_C.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_EXPORTDETAIL_C.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gv_EXPORTDETAIL_C.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gv_EXPORTDETAIL_C.ColumnPanelRowHeight = 50;
            this.gv_EXPORTDETAIL_C.GridControl = this.gct_list_C;
            this.gv_EXPORTDETAIL_C.IndicatorWidth = 35;
            this.gv_EXPORTDETAIL_C.Name = "gv_EXPORTDETAIL_C";
            this.gv_EXPORTDETAIL_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_EXPORTDETAIL_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_EXPORTDETAIL_C.OptionsBehavior.Editable = false;
            this.gv_EXPORTDETAIL_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_EXPORTDETAIL_C.OptionsView.ColumnAutoWidth = false;
            this.gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_EXPORTDETAIL_C.OptionsView.ShowGroupPanel = false;
            this.gv_EXPORTDETAIL_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_EXPORTDETAIL_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // frmReportThongTinKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 430);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmReportThongTinKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: THỐNG KÊ BÁO GIÁ THÔNG TIN KHÁCH HÀNG ::.";
            this.Load += new System.EventHandler(this.frmSearchPurchar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchPurchar_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idquotationtype_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_EXPORTDETAIL_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.DateEdit dte_fromdate_S;
        private DevExpress.XtraEditors.GridLookUpEdit glue_IDEMP_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dte_todate_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraGrid.GridControl gct_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_EXPORTDETAIL_C;
        private DevExpress.XtraEditors.SimpleButton btn_tim_S;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.GridLookUpEdit glue_idquotationtype_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}