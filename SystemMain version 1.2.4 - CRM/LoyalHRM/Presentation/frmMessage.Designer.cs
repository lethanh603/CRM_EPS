namespace LoyalHRM.Presentation
{
    partial class frmMessage
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
            this.gctr_danhmuc_C = new DevExpress.XtraGrid.GridControl();
            this.gv_danhmuc_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gctr_danhmuc_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_danhmuc_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gctr_danhmuc_C
            // 
            this.gctr_danhmuc_C.Cursor = System.Windows.Forms.Cursors.Default;
            this.gctr_danhmuc_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gctr_danhmuc_C.Location = new System.Drawing.Point(0, 0);
            this.gctr_danhmuc_C.MainView = this.gv_danhmuc_C;
            this.gctr_danhmuc_C.Name = "gctr_danhmuc_C";
            this.gctr_danhmuc_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemLookUpEdit1});
            this.gctr_danhmuc_C.Size = new System.Drawing.Size(749, 378);
            this.gctr_danhmuc_C.TabIndex = 6;
            this.gctr_danhmuc_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_danhmuc_C,
            this.gridView1});
            // 
            // gv_danhmuc_C
            // 
            this.gv_danhmuc_C.GridControl = this.gctr_danhmuc_C;
            this.gv_danhmuc_C.IndicatorWidth = 35;
            this.gv_danhmuc_C.Name = "gv_danhmuc_C";
            this.gv_danhmuc_C.OptionsView.ShowFooter = true;
            this.gv_danhmuc_C.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gv_danhmuc_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gv_danhmuc_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gv_danhmuc_C.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gv_danhmuc_C_RowStyle);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gctr_danhmuc_C;
            this.gridView1.Name = "gridView1";
            // 
            // frmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 378);
            this.Controls.Add(this.gctr_danhmuc_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMessage";
            this.Text = ".:: SINH NHẬT NHÂN VIÊN ::.";
            this.Load += new System.EventHandler(this.frmMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gctr_danhmuc_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_danhmuc_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gctr_danhmuc_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_danhmuc_C;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

    }
}