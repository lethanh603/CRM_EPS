namespace LoyalHRM.Presentation
{
    partial class frmThongBaoBaoTri
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
            this.gct_history_C = new DevExpress.XtraGrid.GridControl();
            this.gv_history_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.loaibaotri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gct_kehoach_C = new DevExpress.XtraGrid.GridControl();
            this.gv_kehoach_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gct_history_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_history_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_kehoach_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_kehoach_C)).BeginInit();
            this.SuspendLayout();
            // 
            // gct_history_C
            // 
            this.gct_history_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_history_C.Location = new System.Drawing.Point(0, 0);
            this.gct_history_C.MainView = this.gv_history_C;
            this.gct_history_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_history_C.Name = "gct_history_C";
            this.gct_history_C.Size = new System.Drawing.Size(1038, 298);
            this.gct_history_C.TabIndex = 11;
            this.gct_history_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_history_C});
            // 
            // gv_history_C
            // 
            this.gv_history_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn4,
            this.loaibaotri,
            this.gridColumn7});
            this.gv_history_C.GridControl = this.gct_history_C;
            this.gv_history_C.IndicatorWidth = 35;
            this.gv_history_C.Name = "gv_history_C";
            this.gv_history_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_history_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_history_C.OptionsBehavior.Editable = false;
            this.gv_history_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_history_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_history_C.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã bảo trì";
            this.gridColumn1.FieldName = "mabaotri";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số PO";
            this.gridColumn2.FieldName = "idquotation";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 105;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nhân viên phụ trách";
            this.gridColumn3.FieldName = "staffname";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 154;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Khách hàng";
            this.gridColumn6.FieldName = "customer";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 221;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khách hàng/ công ty";
            this.gridColumn4.FieldName = "khachhang";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 109;
            // 
            // loaibaotri
            // 
            this.loaibaotri.Caption = "Theo tháng/ theo giờ";
            this.loaibaotri.FieldName = "loaibaotri";
            this.loaibaotri.Name = "loaibaotri";
            this.loaibaotri.Visible = true;
            this.loaibaotri.VisibleIndex = 5;
            this.loaibaotri.Width = 113;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Địa chỉ bảo hành";
            this.gridColumn7.FieldName = "diachibaohanh";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 122;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gct_history_C);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gct_kehoach_C);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1038, 645);
            this.splitContainerControl1.SplitterPosition = 298;
            this.splitContainerControl1.TabIndex = 12;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gct_kehoach_C
            // 
            this.gct_kehoach_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_kehoach_C.Location = new System.Drawing.Point(0, 0);
            this.gct_kehoach_C.MainView = this.gv_kehoach_C;
            this.gct_kehoach_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_kehoach_C.Name = "gct_kehoach_C";
            this.gct_kehoach_C.Size = new System.Drawing.Size(1038, 342);
            this.gct_kehoach_C.TabIndex = 12;
            this.gct_kehoach_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_kehoach_C});
            // 
            // gv_kehoach_C
            // 
            this.gv_kehoach_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gv_kehoach_C.GridControl = this.gct_kehoach_C;
            this.gv_kehoach_C.IndicatorWidth = 35;
            this.gv_kehoach_C.Name = "gv_kehoach_C";
            this.gv_kehoach_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_kehoach_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_kehoach_C.OptionsBehavior.Editable = false;
            this.gv_kehoach_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_kehoach_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_kehoach_C.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mã KH";
            this.gridColumn5.FieldName = "manhiemvu";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 78;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Kế hoạch";
            this.gridColumn8.FieldName = "congviecduocgiao";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 105;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Loại kế hoạch";
            this.gridColumn9.FieldName = "plantype";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 154;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Số PO";
            this.gridColumn10.FieldName = "idquotation";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 221;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Nhân viên phụ trách";
            this.gridColumn11.FieldName = "nhanvienphutrach";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            this.gridColumn11.Width = 109;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Nhân viên thực hiện";
            this.gridColumn12.FieldName = "nhanvienthuchien";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 113;
            // 
            // frmThongBaoBaoTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 645);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmThongBaoBaoTri";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: THÔNG BÁO BẢO TRÌ / KẾ HOẠCH ::.";
            this.Load += new System.EventHandler(this.frmThongBaoBaoTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gct_history_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_history_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_kehoach_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_kehoach_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gct_history_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_history_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn loaibaotri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gct_kehoach_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_kehoach_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;

    }
}