namespace SOURCE_FORM_PURCHASE.Presentation
{
    partial class frmSearchPurchar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchPurchar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_tim_S = new DevExpress.XtraEditors.SimpleButton();
            this.glue_idprovider_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMancc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTenNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSodt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDiachi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rad_outlet_I6 = new DevExpress.XtraEditors.RadioGroup();
            this.dte_todate_S = new DevExpress.XtraEditors.DateEdit();
            this.dte_fromdate_S = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_idpurchase_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.glue_IDEMP_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_view_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gct_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_PURCHASEDETAIL_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idprovider_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad_outlet_I6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idpurchase_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PURCHASEDETAIL_C)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_tim_S);
            this.panel1.Controls.Add(this.glue_idprovider_I1);
            this.panel1.Controls.Add(this.rad_outlet_I6);
            this.panel1.Controls.Add(this.dte_todate_S);
            this.panel1.Controls.Add(this.dte_fromdate_S);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.txt_idpurchase_IK1);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.labelControl6);
            this.panel1.Controls.Add(this.glue_IDEMP_I1);
            this.panel1.Controls.Add(this.labelControl8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 102);
            this.panel1.TabIndex = 0;
            // 
            // btn_tim_S
            // 
            this.btn_tim_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_tim_S.Image")));
            this.btn_tim_S.Location = new System.Drawing.Point(244, 75);
            this.btn_tim_S.Name = "btn_tim_S";
            this.btn_tim_S.Size = new System.Drawing.Size(92, 25);
            this.btn_tim_S.TabIndex = 0;
            this.btn_tim_S.Text = "F9  Tìm";
            this.btn_tim_S.Click += new System.EventHandler(this.btn_tim_S_Click);
            // 
            // glue_idprovider_I1
            // 
            this.glue_idprovider_I1.Location = new System.Drawing.Point(109, 30);
            this.glue_idprovider_I1.Name = "glue_idprovider_I1";
            this.glue_idprovider_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_idprovider_I1.Properties.View = this.gridLookUpEdit1View;
            this.glue_idprovider_I1.Size = new System.Drawing.Size(672, 20);
            this.glue_idprovider_I1.TabIndex = 15;
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
            // rad_outlet_I6
            // 
            this.rad_outlet_I6.EditValue = true;
            this.rad_outlet_I6.Location = new System.Drawing.Point(425, 6);
            this.rad_outlet_I6.Name = "rad_outlet_I6";
            this.rad_outlet_I6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rad_outlet_I6.Properties.Appearance.Options.UseBackColor = true;
            this.rad_outlet_I6.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Tiền mặt"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Công nợ")});
            this.rad_outlet_I6.Size = new System.Drawing.Size(356, 22);
            this.rad_outlet_I6.TabIndex = 18;
            // 
            // dte_todate_S
            // 
            this.dte_todate_S.EditValue = null;
            this.dte_todate_S.Location = new System.Drawing.Point(310, 8);
            this.dte_todate_S.Name = "dte_todate_S";
            this.dte_todate_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_S.Size = new System.Drawing.Size(109, 20);
            this.dte_todate_S.TabIndex = 16;
            // 
            // dte_fromdate_S
            // 
            this.dte_fromdate_S.EditValue = null;
            this.dte_fromdate_S.Location = new System.Drawing.Point(109, 9);
            this.dte_fromdate_S.Name = "dte_fromdate_S";
            this.dte_fromdate_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_S.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_S.Size = new System.Drawing.Size(100, 20);
            this.dte_fromdate_S.TabIndex = 16;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(222, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // txt_idpurchase_IK1
            // 
            this.txt_idpurchase_IK1.Location = new System.Drawing.Point(109, 74);
            this.txt_idpurchase_IK1.Name = "txt_idpurchase_IK1";
            this.txt_idpurchase_IK1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_idpurchase_IK1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_idpurchase_IK1.Size = new System.Drawing.Size(129, 20);
            this.txt_idpurchase_IK1.TabIndex = 19;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Từ ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Nhà cung cấp (*)";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 77);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(64, 13);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "Mã phiếu (*):";
            // 
            // glue_IDEMP_I1
            // 
            this.glue_IDEMP_I1.Location = new System.Drawing.Point(109, 53);
            this.glue_IDEMP_I1.Name = "glue_IDEMP_I1";
            this.glue_IDEMP_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_IDEMP_I1.Properties.View = this.gridView1;
            this.glue_IDEMP_I1.Size = new System.Drawing.Size(672, 20);
            this.glue_IDEMP_I1.TabIndex = 17;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 56);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 13);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "Nhân viên:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_view_S);
            this.panel2.Controls.Add(this.btn_exit_S);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 432);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 44);
            this.panel2.TabIndex = 1;
            // 
            // btn_view_S
            // 
            this.btn_view_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_view_S.Image")));
            this.btn_view_S.Location = new System.Drawing.Point(521, 2);
            this.btn_view_S.Name = "btn_view_S";
            this.btn_view_S.Size = new System.Drawing.Size(109, 40);
            this.btn_view_S.TabIndex = 0;
            this.btn_view_S.Text = "F11 Xem";
            this.btn_view_S.Click += new System.EventHandler(this.btn_view_S_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = global::SOURCE_FORM_PURCHASE.Properties.Resources.bbi_thoat_s;
            this.btn_exit_S.Location = new System.Drawing.Point(632, 2);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(109, 40);
            this.btn_exit_S.TabIndex = 0;
            this.btn_exit_S.Text = "F10 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gct_list_C);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 102);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 330);
            this.panel3.TabIndex = 2;
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(0, 0);
            this.gct_list_C.MainView = this.gv_PURCHASEDETAIL_C;
            this.gct_list_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(784, 330);
            this.gct_list_C.TabIndex = 9;
            this.gct_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_PURCHASEDETAIL_C});
            // 
            // gv_PURCHASEDETAIL_C
            // 
            this.gv_PURCHASEDETAIL_C.GridControl = this.gct_list_C;
            this.gv_PURCHASEDETAIL_C.IndicatorWidth = 35;
            this.gv_PURCHASEDETAIL_C.Name = "gv_PURCHASEDETAIL_C";
            this.gv_PURCHASEDETAIL_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_PURCHASEDETAIL_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_PURCHASEDETAIL_C.OptionsBehavior.Editable = false;
            this.gv_PURCHASEDETAIL_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_PURCHASEDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_PURCHASEDETAIL_C.OptionsView.ShowGroupPanel = false;
            this.gv_PURCHASEDETAIL_C.DoubleClick += new System.EventHandler(this.gv_PURCHASEDETAIL_C_DoubleClick);
            // 
            // frmSearchPurchar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 476);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmSearchPurchar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: TÌM PHIẾU NHẬP ::.";
            this.Load += new System.EventHandler(this.frmSearchPurchar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchPurchar_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idprovider_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad_outlet_I6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idpurchase_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_IDEMP_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PURCHASEDETAIL_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.GridLookUpEdit glue_idprovider_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gcMancc;
        private DevExpress.XtraGrid.Columns.GridColumn gcTenNCC;
        private DevExpress.XtraGrid.Columns.GridColumn gcSodt;
        private DevExpress.XtraGrid.Columns.GridColumn gcFax;
        private DevExpress.XtraGrid.Columns.GridColumn gcDiachi;
        private DevExpress.XtraEditors.RadioGroup rad_outlet_I6;
        private DevExpress.XtraEditors.DateEdit dte_fromdate_S;
        private DevExpress.XtraEditors.TextEdit txt_idpurchase_IK1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.GridLookUpEdit glue_IDEMP_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dte_todate_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_view_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraGrid.GridControl gct_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_PURCHASEDETAIL_C;
        private DevExpress.XtraEditors.SimpleButton btn_tim_S;
    }
}