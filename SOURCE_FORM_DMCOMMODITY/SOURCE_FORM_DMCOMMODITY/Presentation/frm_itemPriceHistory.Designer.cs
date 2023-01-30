namespace SOURCE_FORM_DMCOMMODITY.Presentation
{
    partial class frm_itemPriceHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_itemPriceHistory));
            this.gc_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_list_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dte_todate_S = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dte_fromdate_S = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_search_S = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gc_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gc_list_C
            // 
            this.gc_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_list_C.Location = new System.Drawing.Point(2, 2);
            this.gc_list_C.MainView = this.gv_list_C;
            this.gc_list_C.Name = "gc_list_C";
            this.gc_list_C.Size = new System.Drawing.Size(990, 466);
            this.gc_list_C.TabIndex = 0;
            this.gc_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_list_C});
            // 
            // gv_list_C
            // 
            this.gv_list_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gv_list_C.GridControl = this.gc_list_C;
            this.gv_list_C.Name = "gv_list_C";
            this.gv_list_C.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gv_list_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_list_C.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PO";
            this.gridColumn1.FieldName = "invoiceeps";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 69;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số BG";
            this.gridColumn3.FieldName = "quotationno";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày BG";
            this.gridColumn2.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "dateimport";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 63;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Nhân viên";
            this.gridColumn6.FieldName = "StaffName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 97;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khách hàng";
            this.gridColumn4.FieldName = "customer";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 238;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Giá";
            this.gridColumn5.DisplayFormat.FormatString = "N0";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "price";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 70;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Part Number";
            this.gridColumn7.FieldName = "partnumber";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 73;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Thông số";
            this.gridColumn8.FieldName = "spec";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 73;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Thông tin thiết bị";
            this.gridColumn9.FieldName = "equipmentinfo";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 89;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Ghi chú";
            this.gridColumn10.FieldName = "note";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 83;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dte_todate_S);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dte_fromdate_S);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btn_search_S);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(994, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // dte_todate_S
            // 
            this.dte_todate_S.EditValue = null;
            this.dte_todate_S.Location = new System.Drawing.Point(223, 10);
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
            this.dte_todate_S.Size = new System.Drawing.Size(100, 20);
            this.dte_todate_S.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(166, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // dte_fromdate_S
            // 
            this.dte_fromdate_S.EditValue = null;
            this.dte_fromdate_S.Location = new System.Drawing.Point(63, 10);
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
            this.dte_fromdate_S.Size = new System.Drawing.Size(97, 20);
            this.dte_fromdate_S.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // btn_search_S
            // 
            this.btn_search_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_search_S.Image")));
            this.btn_search_S.Location = new System.Drawing.Point(329, 9);
            this.btn_search_S.Name = "btn_search_S";
            this.btn_search_S.Size = new System.Drawing.Size(53, 23);
            this.btn_search_S.TabIndex = 0;
            this.btn_search_S.Text = "Tìm";
            this.btn_search_S.Click += new System.EventHandler(this.btn_search_S_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gc_list_C);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 38);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(994, 470);
            this.panelControl2.TabIndex = 2;
            // 
            // frm_itemPriceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 508);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_itemPriceHistory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: Item Price History  ::.";
            this.Load += new System.EventHandler(this.frm_itemPriceHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gc_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_list_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.DateEdit dte_todate_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dte_fromdate_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_search_S;
    }
}