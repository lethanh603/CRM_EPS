namespace SOURCE_FORM.Presentation
{
    partial class frm_NewFormList_SH
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NewFormList_SH));
            this.gctr_danhmuc_C = new DevExpress.XtraGrid.GridControl();
            this.gv_danhmuc_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel_contain_C = new DevExpress.XtraEditors.PanelControl();
            this.bar_menu_C = new DevExpress.XtraBars.BarManager(this.components);
            this.bar_topmenu_C = new DevExpress.XtraBars.Bar();
            this.bbi_insert_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi_delete_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi_edit_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bbi_print_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi_import_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi_export_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbi_exit_S = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bst_userid_S = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gctr_danhmuc_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_danhmuc_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_contain_C)).BeginInit();
            this.panel_contain_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar_menu_C)).BeginInit();
            this.SuspendLayout();
            // 
            // gctr_danhmuc_C
            // 
            this.gctr_danhmuc_C.Cursor = System.Windows.Forms.Cursors.Default;
            this.gctr_danhmuc_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gctr_danhmuc_C.Location = new System.Drawing.Point(2, 2);
            this.gctr_danhmuc_C.MainView = this.gv_danhmuc_C;
            this.gctr_danhmuc_C.Name = "gctr_danhmuc_C";
            this.gctr_danhmuc_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemLookUpEdit1});
            this.gctr_danhmuc_C.Size = new System.Drawing.Size(780, 483);
            this.gctr_danhmuc_C.TabIndex = 5;
            this.gctr_danhmuc_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_danhmuc_C,
            this.gridView1});
            this.gctr_danhmuc_C.Click += new System.EventHandler(this.gctr_danhmuc_C_Click);
            this.gctr_danhmuc_C.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gctr_danhmuc_C_KeyPress);
            this.gctr_danhmuc_C.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gctr_danhmuc_C_MouseUp);
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
            this.gv_danhmuc_C.DoubleClick += new System.EventHandler(this.gv_danhmuc_C_DoubleClick);
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
            // panel_contain_C
            // 
            this.panel_contain_C.Controls.Add(this.gctr_danhmuc_C);
            this.panel_contain_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_contain_C.Location = new System.Drawing.Point(0, 44);
            this.panel_contain_C.Name = "panel_contain_C";
            this.panel_contain_C.Size = new System.Drawing.Size(784, 487);
            this.panel_contain_C.TabIndex = 0;
            // 
            // bar_menu_C
            // 
            this.bar_menu_C.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar_topmenu_C,
            this.bar3});
            this.bar_menu_C.DockControls.Add(this.barDockControlTop);
            this.bar_menu_C.DockControls.Add(this.barDockControlBottom);
            this.bar_menu_C.DockControls.Add(this.barDockControlLeft);
            this.bar_menu_C.DockControls.Add(this.barDockControlRight);
            this.bar_menu_C.Form = this;
            this.bar_menu_C.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbi_insert_S,
            this.bst_userid_S,
            this.bbi_delete_S,
            this.bbi_edit_S,
            this.bbi_print_S,
            this.bbi_export_S,
            this.bbi_import_S,
            this.bbi_exit_S,
            this.barStaticItem1});
            this.bar_menu_C.MainMenu = this.bar_topmenu_C;
            this.bar_menu_C.MaxItemId = 29;
            this.bar_menu_C.StatusBar = this.bar3;
            // 
            // bar_topmenu_C
            // 
            this.bar_topmenu_C.BarName = "Main menu";
            this.bar_topmenu_C.DockCol = 0;
            this.bar_topmenu_C.DockRow = 0;
            this.bar_topmenu_C.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar_topmenu_C.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_insert_S, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_delete_S),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_edit_S),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_print_S),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_import_S),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_export_S),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_exit_S)});
            this.bar_topmenu_C.OptionsBar.MultiLine = true;
            this.bar_topmenu_C.OptionsBar.UseWholeRow = true;
            this.bar_topmenu_C.Text = "Main menu";
            // 
            // bbi_insert_S
            // 
            this.bbi_insert_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_insert_S.Caption = "F1 Thêm";
            this.bbi_insert_S.Glyph = global::SOURCE_FORM.Properties.Resources.add;
            this.bbi_insert_S.Id = 0;
            this.bbi_insert_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.bbi_insert_S.Name = "bbi_insert_S";
            this.bbi_insert_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_insert_S.ShortcutKeyDisplayString = "F2";
            this.bbi_insert_S.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_insert_S_ItemClick);
            // 
            // bbi_delete_S
            // 
            this.bbi_delete_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_delete_S.Caption = "F2 Xóa";
            this.bbi_delete_S.Glyph = ((System.Drawing.Image)(resources.GetObject("bbi_delete_S.Glyph")));
            this.bbi_delete_S.Id = 12;
            this.bbi_delete_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.bbi_delete_S.Name = "bbi_delete_S";
            this.bbi_delete_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_delete_S.ShortcutKeyDisplayString = "F2";
            // 
            // bbi_edit_S
            // 
            this.bbi_edit_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_edit_S.Caption = "F3 Sửa";
            this.bbi_edit_S.Glyph = global::SOURCE_FORM.Properties.Resources.edit;
            this.bbi_edit_S.Id = 14;
            this.bbi_edit_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.bbi_edit_S.Name = "bbi_edit_S";
            this.bbi_edit_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_edit_S.ShortcutKeyDisplayString = "F3";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.barStaticItem1.Id = 28;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bbi_print_S
            // 
            this.bbi_print_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_print_S.Caption = "F4 In";
            this.bbi_print_S.Glyph = ((System.Drawing.Image)(resources.GetObject("bbi_print_S.Glyph")));
            this.bbi_print_S.Id = 15;
            this.bbi_print_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.bbi_print_S.Name = "bbi_print_S";
            this.bbi_print_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_print_S.ShortcutKeyDisplayString = "F4";
            // 
            // bbi_import_S
            // 
            this.bbi_import_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_import_S.Caption = "F6 Nhập";
            this.bbi_import_S.Glyph = global::SOURCE_FORM.Properties.Resources.import;
            this.bbi_import_S.Id = 17;
            this.bbi_import_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
            this.bbi_import_S.Name = "bbi_import_S";
            this.bbi_import_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_import_S.ShortcutKeyDisplayString = "F6";
            // 
            // bbi_export_S
            // 
            this.bbi_export_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_export_S.Caption = "F5 Xuất";
            this.bbi_export_S.Glyph = global::SOURCE_FORM.Properties.Resources.Excel;
            this.bbi_export_S.Id = 16;
            this.bbi_export_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.bbi_export_S.Name = "bbi_export_S";
            this.bbi_export_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_export_S.ShortcutKeyDisplayString = "F5";
            // 
            // bbi_exit_S
            // 
            this.bbi_exit_S.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.bbi_exit_S.Caption = "F7 Thoát";
            this.bbi_exit_S.Glyph = global::SOURCE_FORM.Properties.Resources.Close;
            this.bbi_exit_S.Id = 27;
            this.bbi_exit_S.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.bbi_exit_S.Name = "bbi_exit_S";
            this.bbi_exit_S.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbi_exit_S.ShortcutKeyDisplayString = "F7";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bst_userid_S)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bst_userid_S
            // 
            this.bst_userid_S.Caption = "Nhân viên tạo: Administrator - Ngày tạo: 25/12/2014 - Giờ tạo: 12:21 -:- Nhân viê" +
                "n sửa: administrator - Ngày sửa: 23/1/2015 - Giờ sửa: 23:12";
            this.bst_userid_S.Id = 1;
            this.bst_userid_S.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Navy;
            this.bst_userid_S.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bst_userid_S.Name = "bst_userid_S";
            this.bst_userid_S.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 44);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 531);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 44);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 487);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 44);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 487);
            // 
            // frm_NewFormList_SH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 556);
            this.Controls.Add(this.panel_contain_C);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_NewFormList_SH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: NEW FORM ::.";
            this.Load += new System.EventHandler(this.frm_nhapdvt_SH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gctr_danhmuc_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_danhmuc_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_contain_C)).EndInit();
            this.panel_contain_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar_menu_C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private DevExpress.ExpressApp.Win.Templates.Controls.XafBar xafBar3;
        //private DevExpress.ExpressApp.Win.Templates.Controls.XafBar xafBar2;
        //private DevExpress.ExpressApp.Win.Templates.Controls.XafBar xafBar1;
        private DevExpress.XtraEditors.PanelControl panel_contain_C;
        private DevExpress.XtraGrid.GridControl gctr_danhmuc_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_danhmuc_C;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarManager bar_menu_C;
        private DevExpress.XtraBars.Bar bar_topmenu_C;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_insert_S;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_delete_S;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_edit_S;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bst_userid_S;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_print_S;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_export_S;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_import_S;
        private DevExpress.XtraBars.BarLargeButtonItem bbi_exit_S;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
    }
}