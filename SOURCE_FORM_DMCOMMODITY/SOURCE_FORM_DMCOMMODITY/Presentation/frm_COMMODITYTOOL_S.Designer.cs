namespace SOURCE_FORM_DMCOMMODITY.Presentation
{
    partial class frm_COMMODITYTOOL_S
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gct_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_list_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.chk_showall_S = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gct_list_duplicate_C = new DevExpress.XtraGrid.GridControl();
            this.gv_list_duplicate_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txt_mahang_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_gopma_S = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_showall_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_duplicate_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_duplicate_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mahang_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl4);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(929, 567);
            this.splitContainerControl1.SplitterPosition = 527;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gct_list_C);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 37);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(527, 530);
            this.panelControl3.TabIndex = 3;
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(2, 2);
            this.gct_list_C.MainView = this.gv_list_C;
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(523, 526);
            this.gct_list_C.TabIndex = 5;
            this.gct_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_list_C});
            // 
            // gv_list_C
            // 
            this.gv_list_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gv_list_C.GridControl = this.gct_list_C;
            this.gv_list_C.IndicatorWidth = 35;
            this.gv_list_C.Name = "gv_list_C";
            this.gv_list_C.OptionsBehavior.Editable = false;
            this.gv_list_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_list_C.OptionsView.ShowGroupPanel = false;
            this.gv_list_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_list_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gv_list_C.Click += new System.EventHandler(this.gv_list_C_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên hàng hóa";
            this.gridColumn1.FieldName = "commodity";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 380;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số lượng trùng";
            this.gridColumn2.FieldName = "count_commodity";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 106;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.chk_showall_S);
            this.panelControl4.Controls.Add(this.labelControl2);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(527, 37);
            this.panelControl4.TabIndex = 2;
            // 
            // chk_showall_S
            // 
            this.chk_showall_S.Location = new System.Drawing.Point(201, 9);
            this.chk_showall_S.Name = "chk_showall_S";
            this.chk_showall_S.Properties.Caption = "Hiện tất cả";
            this.chk_showall_S.Size = new System.Drawing.Size(75, 19);
            this.chk_showall_S.TabIndex = 2;
            this.chk_showall_S.CheckedChanged += new System.EventHandler(this.chk_showall_S_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(153, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Danh sách các mã hàng bị trùng";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gct_list_duplicate_C);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 37);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(397, 530);
            this.panelControl2.TabIndex = 1;
            // 
            // gct_list_duplicate_C
            // 
            this.gct_list_duplicate_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_duplicate_C.Location = new System.Drawing.Point(2, 2);
            this.gct_list_duplicate_C.MainView = this.gv_list_duplicate_C;
            this.gct_list_duplicate_C.Name = "gct_list_duplicate_C";
            this.gct_list_duplicate_C.Size = new System.Drawing.Size(393, 526);
            this.gct_list_duplicate_C.TabIndex = 6;
            this.gct_list_duplicate_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_list_duplicate_C});
            // 
            // gv_list_duplicate_C
            // 
            this.gv_list_duplicate_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gv_list_duplicate_C.GridControl = this.gct_list_duplicate_C;
            this.gv_list_duplicate_C.IndicatorWidth = 35;
            this.gv_list_duplicate_C.Name = "gv_list_duplicate_C";
            this.gv_list_duplicate_C.OptionsBehavior.Editable = false;
            this.gv_list_duplicate_C.OptionsView.ShowGroupPanel = false;
            this.gv_list_duplicate_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_list_duplicate_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gv_list_duplicate_C.Click += new System.EventHandler(this.gv_list_duplicate_C_Click);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã hàng";
            this.gridColumn3.FieldName = "idcommodity";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 91;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tên hàng";
            this.gridColumn4.FieldName = "commodity";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 265;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txt_mahang_S);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btn_gopma_S);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(397, 37);
            this.panelControl1.TabIndex = 0;
            // 
            // txt_mahang_S
            // 
            this.txt_mahang_S.Enabled = false;
            this.txt_mahang_S.Location = new System.Drawing.Point(227, 10);
            this.txt_mahang_S.Name = "txt_mahang_S";
            this.txt_mahang_S.Size = new System.Drawing.Size(100, 20);
            this.txt_mahang_S.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(98, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Gộp tất cả về mã hàng:";
            // 
            // btn_gopma_S
            // 
            this.btn_gopma_S.Location = new System.Drawing.Point(5, 8);
            this.btn_gopma_S.Name = "btn_gopma_S";
            this.btn_gopma_S.Size = new System.Drawing.Size(87, 23);
            this.btn_gopma_S.TabIndex = 0;
            this.btn_gopma_S.Text = "Gộp mã";
            this.btn_gopma_S.Click += new System.EventHandler(this.btn_gopma_S_Click);
            // 
            // frm_COMMODITYTOOL_S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 567);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_COMMODITYTOOL_S";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = ".:: GỘP MÃ HÀNG :.";
            this.Load += new System.EventHandler(this.frm_COMMODITYTOOL_S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_showall_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_duplicate_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_duplicate_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mahang_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gct_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_list_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.GridControl gct_list_duplicate_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_list_duplicate_C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.TextEdit txt_mahang_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_gopma_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit chk_showall_S;

    }
}