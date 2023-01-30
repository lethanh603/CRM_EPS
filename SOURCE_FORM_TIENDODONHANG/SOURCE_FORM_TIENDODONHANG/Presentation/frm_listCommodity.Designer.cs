namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    partial class frm_listCommodity
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_cancel_detail = new DevExpress.XtraEditors.SimpleButton();
            this.btn_detail_insert = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gct_detail_C = new DevExpress.XtraGrid.GridControl();
            this.gv_detail_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_detail_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_detail_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_cancel_detail);
            this.panelControl1.Controls.Add(this.btn_detail_insert);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 397);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 33);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_cancel_detail
            // 
            this.btn_cancel_detail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel_detail.Image = global::SOURCE_FORM_TIENDODONHANG.Properties.Resources.bbi_xoa_S;
            this.btn_cancel_detail.Location = new System.Drawing.Point(616, 5);
            this.btn_cancel_detail.Name = "btn_cancel_detail";
            this.btn_cancel_detail.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel_detail.TabIndex = 16;
            this.btn_cancel_detail.Text = "Hủy";
            this.btn_cancel_detail.Click += new System.EventHandler(this.btn_cancel_detail_Click);
            // 
            // btn_detail_insert
            // 
            this.btn_detail_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_detail_insert.Image = global::SOURCE_FORM_TIENDODONHANG.Properties.Resources.add;
            this.btn_detail_insert.Location = new System.Drawing.Point(535, 5);
            this.btn_detail_insert.Name = "btn_detail_insert";
            this.btn_detail_insert.Size = new System.Drawing.Size(75, 23);
            this.btn_detail_insert.TabIndex = 15;
            this.btn_detail_insert.Text = "Thêm";
            this.btn_detail_insert.Click += new System.EventHandler(this.btn_detail_insert_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gct_detail_C);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(700, 397);
            this.panelControl2.TabIndex = 1;
            // 
            // gct_detail_C
            // 
            this.gct_detail_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_detail_C.Location = new System.Drawing.Point(2, 2);
            this.gct_detail_C.MainView = this.gv_detail_C;
            this.gct_detail_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_detail_C.Name = "gct_detail_C";
            this.gct_detail_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gct_detail_C.Size = new System.Drawing.Size(696, 393);
            this.gct_detail_C.TabIndex = 14;
            this.gct_detail_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_detail_C});
            // 
            // gv_detail_C
            // 
            this.gv_detail_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn23});
            this.gv_detail_C.GridControl = this.gct_detail_C;
            this.gv_detail_C.IndicatorWidth = 35;
            this.gv_detail_C.Name = "gv_detail_C";
            this.gv_detail_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_detail_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_detail_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_detail_C.OptionsView.ShowGroupPanel = false;
            this.gv_detail_C.DoubleClick += new System.EventHandler(this.gv_detail_C_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "iddetail";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Mã SP";
            this.gridColumn20.FieldName = "idcommodity";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.AllowFocus = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 1;
            this.gridColumn20.Width = 69;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Tên SP";
            this.gridColumn21.FieldName = "commodity";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.AllowFocus = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 2;
            this.gridColumn21.Width = 130;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Số lượng";
            this.gridColumn23.FieldName = "quantity";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.AllowFocus = false;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 3;
            this.gridColumn23.Width = 51;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Check";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // frm_listCommodity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 430);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_listCommodity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách sản phầm";
            this.Load += new System.EventHandler(this.frm_listCommodity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_detail_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_detail_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_cancel_detail;
        private DevExpress.XtraEditors.SimpleButton btn_detail_insert;
        private DevExpress.XtraGrid.GridControl gct_detail_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_detail_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}