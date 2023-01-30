namespace SOURCE_FORM_REPORT.Presentation
{
    partial class frmCSKH_DETAIL
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
            this.gct_device_C = new DevExpress.XtraGrid.GridControl();
            this.gv_device_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gct_device_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_device_C)).BeginInit();
            this.SuspendLayout();
            // 
            // gct_device_C
            // 
            this.gct_device_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_device_C.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gct_device_C.Location = new System.Drawing.Point(0, 0);
            this.gct_device_C.MainView = this.gv_device_C;
            this.gct_device_C.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gct_device_C.Name = "gct_device_C";
            this.gct_device_C.Size = new System.Drawing.Size(904, 482);
            this.gct_device_C.TabIndex = 7;
            this.gct_device_C.UseEmbeddedNavigator = true;
            this.gct_device_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_device_C});
            // 
            // gv_device_C
            // 
            this.gv_device_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25});
            this.gv_device_C.GridControl = this.gct_device_C;
            this.gv_device_C.IndicatorWidth = 35;
            this.gv_device_C.Name = "gv_device_C";
            this.gv_device_C.OptionsBehavior.Editable = false;
            this.gv_device_C.OptionsView.ColumnAutoWidth = false;
            this.gv_device_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_device_C.OptionsView.ShowFooter = true;
            this.gv_device_C.OptionsView.ShowGroupPanel = false;
            this.gv_device_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_device_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nhân viên liên hệ";
            this.gridColumn1.FieldName = "StaffName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Ngày liên hệ";
            this.gridColumn20.FieldName = "datecontact";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 1;
            this.gridColumn20.Width = 98;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Người liên hệ";
            this.gridColumn21.FieldName = "contactname";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 2;
            this.gridColumn21.Width = 96;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Kết quả CSKH";
            this.gridColumn22.FieldName = "statusname";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 3;
            this.gridColumn22.Width = 180;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Nội dung liên hệ";
            this.gridColumn23.FieldName = "description";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 4;
            this.gridColumn23.Width = 176;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Nội dung phản hồi";
            this.gridColumn24.FieldName = "description2";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 5;
            this.gridColumn24.Width = 154;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "Ghi chú";
            this.gridColumn25.FieldName = "note";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 6;
            this.gridColumn25.Width = 159;
            // 
            // frmCSKH_DETAIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 482);
            this.Controls.Add(this.gct_device_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCSKH_DETAIL";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CHI TIẾT CSKH ::.";
            this.Load += new System.EventHandler(this.frmCSHK_DETAIL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gct_device_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_device_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gct_device_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_device_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}