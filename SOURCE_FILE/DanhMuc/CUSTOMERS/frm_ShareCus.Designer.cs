namespace SOURCE_FORM_CRM.Presentation
{
    partial class frm_ShareCus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ShareCus));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gct_employees_C = new DevExpress.XtraGrid.GridControl();
            this.gv_employees_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcemp_idemp_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcemp_staffname_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_iddepartment_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repemp_check = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gct_customer_C = new DevExpress.XtraGrid.GridControl();
            this.gv_customer_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gccus_check_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccus_idcustomer_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccus_customer_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccus_customertype_S = new DevExpress.XtraGrid.Columns.GridColumn();
            this.res_cus_check_S = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_employees_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_employees_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repemp_check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_customer_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_customer_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_cus_check_S)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(532, 437);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Nhân viên";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gct_employees_C);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 21);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(528, 377);
            this.panelControl2.TabIndex = 1;
            // 
            // gct_employees_C
            // 
            this.gct_employees_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_employees_C.Location = new System.Drawing.Point(0, 0);
            this.gct_employees_C.MainView = this.gv_employees_C;
            this.gct_employees_C.Name = "gct_employees_C";
            this.gct_employees_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repemp_check});
            this.gct_employees_C.Size = new System.Drawing.Size(528, 377);
            this.gct_employees_C.TabIndex = 0;
            this.gct_employees_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_employees_C});
            // 
            // gv_employees_C
            // 
            this.gv_employees_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcemp_idemp_S,
            this.gcemp_staffname_S,
            this.gc_iddepartment_S});
            this.gv_employees_C.GridControl = this.gct_employees_C;
            this.gv_employees_C.IndicatorWidth = 35;
            this.gv_employees_C.Name = "gv_employees_C";
            this.gv_employees_C.OptionsView.ShowGroupPanel = false;
            this.gv_employees_C.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gv_employees_C_RowClick);
            this.gv_employees_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_employees_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // gcemp_idemp_S
            // 
            this.gcemp_idemp_S.Caption = "Mã NV";
            this.gcemp_idemp_S.FieldName = "idemp";
            this.gcemp_idemp_S.Name = "gcemp_idemp_S";
            this.gcemp_idemp_S.OptionsColumn.AllowEdit = false;
            this.gcemp_idemp_S.Visible = true;
            this.gcemp_idemp_S.VisibleIndex = 0;
            this.gcemp_idemp_S.Width = 92;
            // 
            // gcemp_staffname_S
            // 
            this.gcemp_staffname_S.Caption = "Tên nhân viên";
            this.gcemp_staffname_S.FieldName = "staffname";
            this.gcemp_staffname_S.Name = "gcemp_staffname_S";
            this.gcemp_staffname_S.OptionsColumn.AllowEdit = false;
            this.gcemp_staffname_S.Visible = true;
            this.gcemp_staffname_S.VisibleIndex = 1;
            this.gcemp_staffname_S.Width = 229;
            // 
            // gc_iddepartment_S
            // 
            this.gc_iddepartment_S.Caption = "Bộ phận";
            this.gc_iddepartment_S.FieldName = "department";
            this.gc_iddepartment_S.Name = "gc_iddepartment_S";
            this.gc_iddepartment_S.OptionsColumn.AllowEdit = false;
            this.gc_iddepartment_S.Visible = true;
            this.gc_iddepartment_S.VisibleIndex = 2;
            this.gc_iddepartment_S.Width = 133;
            // 
            // repemp_check
            // 
            this.repemp_check.AutoHeight = false;
            this.repemp_check.Caption = "Check";
            this.repemp_check.Name = "repemp_check";

            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelControl1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.bbi_allow_insert);
            this.panelControl1.Controls.Add(this.btn_exit_S);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 398);
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(528, 37);
            this.panelControl1.TabIndex = 0;
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbi_allow_insert.Image = ((System.Drawing.Image)(resources.GetObject("bbi_allow_insert.Image")));
            this.bbi_allow_insert.Location = new System.Drawing.Point(337, 5);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(103, 31);
            this.bbi_allow_insert.TabIndex = 15;
            this.bbi_allow_insert.Text = "F5 Chuyển";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(441, 5);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 16;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.panelControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(532, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(432, 437);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Khách hàng";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.gct_customer_C);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 21);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(428, 414);
            this.panelControl3.TabIndex = 0;
            // 
            // gct_customer_C
            // 
            this.gct_customer_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_customer_C.Location = new System.Drawing.Point(0, 0);
            this.gct_customer_C.MainView = this.gv_customer_C;
            this.gct_customer_C.Name = "gct_customer_C";
            this.gct_customer_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.res_cus_check_S});
            this.gct_customer_C.Size = new System.Drawing.Size(428, 414);
            this.gct_customer_C.TabIndex = 1;
            this.gct_customer_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_customer_C});
            // 
            // gv_customer_C
            // 
            this.gv_customer_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gccus_check_S,
            this.gccus_idcustomer_S,
            this.gccus_customer_S,
            this.gccus_customertype_S});
            this.gv_customer_C.GridControl = this.gct_customer_C;
            this.gv_customer_C.IndicatorWidth = 35;
            this.gv_customer_C.Name = "gv_customer_C";
            this.gv_customer_C.OptionsView.ShowGroupPanel = false;
    
            this.gv_customer_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_customer_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // gccus_check_S
            // 
            this.gccus_check_S.Caption = "Chọn";
            this.gccus_check_S.ColumnEdit = this.res_cus_check_S;
            this.gccus_check_S.FieldName = "check";
            this.gccus_check_S.Name = "gccus_check_S";
            this.gccus_check_S.Visible = true;
            this.gccus_check_S.VisibleIndex = 0;
            // 
            // gccus_idcustomer_S
            // 
            this.gccus_idcustomer_S.Caption = "Mã khách hàng";
            this.gccus_idcustomer_S.FieldName = "idcustomer";
            this.gccus_idcustomer_S.Name = "gccus_idcustomer_S";
            this.gccus_idcustomer_S.OptionsColumn.AllowEdit = false;
            this.gccus_idcustomer_S.Visible = true;
            this.gccus_idcustomer_S.VisibleIndex = 1;
            this.gccus_idcustomer_S.Width = 81;
            // 
            // gccus_customer_S
            // 
            this.gccus_customer_S.Caption = "Tên khách hàng";
            this.gccus_customer_S.FieldName = "customer";
            this.gccus_customer_S.Name = "gccus_customer_S";
            this.gccus_customer_S.OptionsColumn.AllowEdit = false;
            this.gccus_customer_S.Visible = true;
            this.gccus_customer_S.VisibleIndex = 2;
            this.gccus_customer_S.Width = 155;
            // 
            // gccus_customertype_S
            // 
            this.gccus_customertype_S.Caption = "Loại ";
            this.gccus_customertype_S.FieldName = "customertype";
            this.gccus_customertype_S.Name = "gccus_customertype_S";
            this.gccus_customertype_S.OptionsColumn.AllowEdit = false;
            this.gccus_customertype_S.Visible = true;
            this.gccus_customertype_S.VisibleIndex = 3;
            this.gccus_customertype_S.Width = 91;
            // 
            // res_cus_check_S
            // 
            this.res_cus_check_S.AutoHeight = false;
            this.res_cus_check_S.Caption = "Check";
            this.res_cus_check_S.Name = "res_cus_check_S";
            // 
            // frm_ShareCus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 437);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_ShareCus";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = ".:: PHÂN CHIA KHÁCH HÀNG ::.";
            this.Load += new System.EventHandler(this.frm_ShareCus_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ShareCus_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_employees_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_employees_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repemp_check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_customer_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_customer_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_cus_check_S)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gct_employees_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_employees_C;
        private DevExpress.XtraGrid.Columns.GridColumn gcemp_idemp_S;
        private DevExpress.XtraGrid.Columns.GridColumn gcemp_staffname_S;
        private DevExpress.XtraGrid.Columns.GridColumn gc_iddepartment_S;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repemp_check;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gct_customer_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_customer_C;
        private DevExpress.XtraGrid.Columns.GridColumn gccus_idcustomer_S;
        private DevExpress.XtraGrid.Columns.GridColumn gccus_customertype_S;
        private DevExpress.XtraGrid.Columns.GridColumn gccus_customer_S;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraGrid.Columns.GridColumn gccus_check_S;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit res_cus_check_S;
    }
}