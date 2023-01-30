namespace SOURCE_FORM_DMCAMPAIGN.Presentation
{
    partial class frm_selectcustomer
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gct_listcustomer_C = new DevExpress.XtraGrid.GridControl();
            this.gv_listcustomer_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_select_S = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_listcustomer_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_listcustomer_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btn_select_S);
            this.groupControl1.Controls.Add(this.btn_exit_S);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 441);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(717, 71);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tùy chọn";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gct_listcustomer_C);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(717, 441);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "groupControl2";
            // 
            // gct_listcustomer_C
            // 
            this.gct_listcustomer_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_listcustomer_C.Location = new System.Drawing.Point(2, 2);
            this.gct_listcustomer_C.MainView = this.gv_listcustomer_C;
            this.gct_listcustomer_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_listcustomer_C.Name = "gct_listcustomer_C";
            this.gct_listcustomer_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gct_listcustomer_C.Size = new System.Drawing.Size(713, 437);
            this.gct_listcustomer_C.TabIndex = 10;
            this.gct_listcustomer_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_listcustomer_C});
            // 
            // gv_listcustomer_C
            // 
            this.gv_listcustomer_C.Appearance.Row.Options.UseBackColor = true;
            this.gv_listcustomer_C.GridControl = this.gct_listcustomer_C;
            this.gv_listcustomer_C.IndicatorWidth = 35;
            this.gv_listcustomer_C.Name = "gv_listcustomer_C";
            this.gv_listcustomer_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_listcustomer_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gv_listcustomer_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_listcustomer_C.OptionsView.ColumnAutoWidth = false;
            this.gv_listcustomer_C.OptionsView.ShowAutoFilterRow = true;
            this.gv_listcustomer_C.OptionsView.ShowGroupPanel = false;
            this.gv_listcustomer_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_listcustomer_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Check";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_DMCAMPAIGN.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(613, 24);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(99, 42);
            this.btn_exit_S.TabIndex = 0;
            this.btn_exit_S.Text = "F3 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // btn_select_S
            // 
            this.btn_select_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_select_S.Image = global::SOURCE_FORM_DMCAMPAIGN.Properties.Resources.edit;
            this.btn_select_S.Location = new System.Drawing.Point(511, 24);
            this.btn_select_S.Name = "btn_select_S";
            this.btn_select_S.Size = new System.Drawing.Size(99, 42);
            this.btn_select_S.TabIndex = 0;
            this.btn_select_S.Text = "F2 Chọn";
            this.btn_select_S.Click += new System.EventHandler(this.btn_select_S_Click);
            // 
            // frm_selectcustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 512);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "frm_selectcustomer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CHỌN KHÁCH HÀNG ::.";
            this.Load += new System.EventHandler(this.frm_selectcustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_listcustomer_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_listcustomer_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gct_listcustomer_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_listcustomer_C;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton btn_select_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
    }
}