namespace SOURCE_FORM_QUOTATION.Presentation
{
    partial class frmReportPhanLoaiKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportPhanLoaiKhachHang));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_tim_S = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.glue_idcampaign_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dte_todate_S = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.glue_IDEMP_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glue_idgroup_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMancc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTenNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSodt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDiachi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.glue_idcampaign_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idgroup_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(857, 65);
            this.panel1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btn_exit_S);
            this.groupControl2.Controls.Add(this.btn_tim_S);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(692, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(165, 65);
            this.groupControl2.TabIndex = 22;
            this.groupControl2.Text = "groupControl2";
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_QUOTATION.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(88, 5);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(72, 41);
            this.btn_exit_S.TabIndex = 10;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // btn_tim_S
            // 
            this.btn_tim_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_tim_S.Image")));
            this.btn_tim_S.Location = new System.Drawing.Point(6, 5);
            this.btn_tim_S.Name = "btn_tim_S";
            this.btn_tim_S.Size = new System.Drawing.Size(76, 41);
            this.btn_tim_S.TabIndex = 8;
            this.btn_tim_S.Text = "F10  Tìm";
            this.btn_tim_S.Click += new System.EventHandler(this.btn_tim_S_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.glue_idcampaign_I1);
            this.groupControl1.Controls.Add(this.dte_todate_S);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.glue_IDEMP_I1);
            this.groupControl1.Controls.Add(this.glue_idgroup_I1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.dte_fromdate_S);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(692, 65);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "groupControl1";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Location = new System.Drawing.Point(347, 30);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(53, 13);
            this.labelControl12.TabIndex = 22;
            this.labelControl12.Text = "Chiến dịch:";
            // 
            // glue_idcampaign_I1
            // 
            this.glue_idcampaign_I1.EditValue = "";
            this.glue_idcampaign_I1.Location = new System.Drawing.Point(427, 27);
            this.glue_idcampaign_I1.Name = "glue_idcampaign_I1";
            this.glue_idcampaign_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_idcampaign_I1.Properties.View = this.gridView2;
            this.glue_idcampaign_I1.Size = new System.Drawing.Size(259, 20);
            this.glue_idcampaign_I1.TabIndex = 21;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // dte_todate_S
            // 
            this.dte_todate_S.EditValue = null;
            this.dte_todate_S.Location = new System.Drawing.Point(241, 4);
            this.dte_todate_S.Name = "dte_todate_S";
            this.dte_todate_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_todate_S.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_S.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_todate_S.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dte_todate_S.Size = new System.Drawing.Size(96, 20);
            this.dte_todate_S.TabIndex = 2;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(348, 6);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 13);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "Nhân viên:";
            // 
            // glue_IDEMP_I1
            // 
            this.glue_IDEMP_I1.Location = new System.Drawing.Point(427, 3);
            this.glue_IDEMP_I1.Name = "glue_IDEMP_I1";
            this.glue_IDEMP_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_IDEMP_I1.Properties.View = this.gridView1;
            this.glue_IDEMP_I1.Size = new System.Drawing.Size(259, 20);
            this.glue_IDEMP_I1.TabIndex = 5;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // glue_idgroup_I1
            // 
            this.glue_idgroup_I1.Location = new System.Drawing.Point(89, 30);
            this.glue_idgroup_I1.Name = "glue_idgroup_I1";
            this.glue_idgroup_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_idgroup_I1.Properties.View = this.gridLookUpEdit1View;
            this.glue_idgroup_I1.Size = new System.Drawing.Size(248, 20);
            this.glue_idgroup_I1.TabIndex = 4;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMancc,
            this.gcTenNCC,
            this.gcSodt,
            this.gcFax,
            this.gcDiachi});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gcMancc
            // 
            this.gcMancc.Caption = "Mã NCC";
            this.gcMancc.FieldName = "idprovider";
            this.gcMancc.Name = "gcMancc";
            this.gcMancc.Visible = true;
            this.gcMancc.VisibleIndex = 0;
            // 
            // gcTenNCC
            // 
            this.gcTenNCC.Caption = "Tên NCC";
            this.gcTenNCC.FieldName = "provider";
            this.gcTenNCC.Name = "gcTenNCC";
            this.gcTenNCC.Visible = true;
            this.gcTenNCC.VisibleIndex = 1;
            // 
            // gcSodt
            // 
            this.gcSodt.Caption = "Số ĐT";
            this.gcSodt.FieldName = "tel";
            this.gcSodt.Name = "gcSodt";
            // 
            // gcFax
            // 
            this.gcFax.Caption = "Fax";
            this.gcFax.FieldName = "fax";
            this.gcFax.Name = "gcFax";
            // 
            // gcDiachi
            // 
            this.gcDiachi.Caption = "Địa chỉ";
            this.gcDiachi.FieldName = "address";
            this.gcDiachi.Name = "gcDiachi";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Đội (*)";
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
            this.dte_fromdate_S.Size = new System.Drawing.Size(89, 20);
            this.dte_fromdate_S.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(182, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 421);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(857, 10);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gct_list_C);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 65);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(857, 356);
            this.panel3.TabIndex = 2;
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(0, 0);
            this.gct_list_C.MainView = this.gv_EXPORTDETAIL_C;
            this.gct_list_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(857, 356);
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
            // frmReportPhanLoaiKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 431);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmReportPhanLoaiKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: THỐNG KÊ PHÂN LOẠI KHÁCH HÀNG ::.";
            this.Load += new System.EventHandler(this.frmSearchPurchar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchPurchar_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idcampaign_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idgroup_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
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
        private DevExpress.XtraEditors.GridLookUpEdit glue_idgroup_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gcMancc;
        private DevExpress.XtraGrid.Columns.GridColumn gcTenNCC;
        private DevExpress.XtraGrid.Columns.GridColumn gcSodt;
        private DevExpress.XtraGrid.Columns.GridColumn gcFax;
        private DevExpress.XtraGrid.Columns.GridColumn gcDiachi;
        private DevExpress.XtraEditors.DateEdit dte_fromdate_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
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
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.GridLookUpEdit glue_idcampaign_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}