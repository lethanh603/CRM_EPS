namespace SOURCE_FORM_RETAIL.Presentation
{
    partial class frmSplitTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplitTable));
            this.pn_left_C = new DevExpress.XtraEditors.PanelControl();
            this.pn_main1_C = new DevExpress.XtraEditors.PanelControl();
            this.gct_menu_C = new DevExpress.XtraGrid.GridControl();
            this.gv_menu_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcThucdon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.res_idcommodity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcStrQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pn_top1_C = new DevExpress.XtraEditors.PanelControl();
            this.lbl_tablechoose_S = new DevExpress.XtraEditors.LabelControl();
            this.pn_middle_C = new DevExpress.XtraEditors.PanelControl();
            this.btn_refresh_S = new DevExpress.XtraEditors.SimpleButton();
            this.txt_quantity_S = new DevExpress.XtraEditors.TextEdit();
            this.btn_right_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cancel_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_agree_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_left_S = new DevExpress.XtraEditors.SimpleButton();
            this.pn_right_C = new DevExpress.XtraEditors.PanelControl();
            this.pn_main2_C = new DevExpress.XtraEditors.PanelControl();
            this.gct_right_C = new DevExpress.XtraGrid.GridControl();
            this.gv_right_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcIdcommodity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcStrQuantity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuantity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPrice1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTotal1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pn_top2_C = new DevExpress.XtraEditors.PanelControl();
            this.lbl_tablemove_S = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pn_left_C)).BeginInit();
            this.pn_left_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pn_main1_C)).BeginInit();
            this.pn_main1_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_menu_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_menu_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_idcommodity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_top1_C)).BeginInit();
            this.pn_top1_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pn_middle_C)).BeginInit();
            this.pn_middle_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_quantity_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_right_C)).BeginInit();
            this.pn_right_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pn_main2_C)).BeginInit();
            this.pn_main2_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_right_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_right_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_top2_C)).BeginInit();
            this.pn_top2_C.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_left_C
            // 
            this.pn_left_C.Controls.Add(this.pn_main1_C);
            this.pn_left_C.Controls.Add(this.pn_top1_C);
            this.pn_left_C.Dock = System.Windows.Forms.DockStyle.Left;
            this.pn_left_C.Location = new System.Drawing.Point(0, 0);
            this.pn_left_C.Name = "pn_left_C";
            this.pn_left_C.Size = new System.Drawing.Size(382, 506);
            this.pn_left_C.TabIndex = 0;
            // 
            // pn_main1_C
            // 
            this.pn_main1_C.Controls.Add(this.gct_menu_C);
            this.pn_main1_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_main1_C.Location = new System.Drawing.Point(2, 78);
            this.pn_main1_C.Name = "pn_main1_C";
            this.pn_main1_C.Size = new System.Drawing.Size(378, 426);
            this.pn_main1_C.TabIndex = 1;
            // 
            // gct_menu_C
            // 
            this.gct_menu_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_menu_C.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gct_menu_C.Location = new System.Drawing.Point(2, 2);
            this.gct_menu_C.MainView = this.gv_menu_C;
            this.gct_menu_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_menu_C.Name = "gct_menu_C";
            this.gct_menu_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.res_idcommodity});
            this.gct_menu_C.Size = new System.Drawing.Size(374, 422);
            this.gct_menu_C.TabIndex = 11;
            this.gct_menu_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_menu_C});
            // 
            // gv_menu_C
            // 
            this.gv_menu_C.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_menu_C.Appearance.EvenRow.Options.UseFont = true;
            this.gv_menu_C.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_menu_C.Appearance.FocusedRow.Options.UseFont = true;
            this.gv_menu_C.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_menu_C.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_menu_C.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_menu_C.Appearance.Preview.Options.UseFont = true;
            this.gv_menu_C.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_menu_C.Appearance.Row.Options.UseFont = true;
            this.gv_menu_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcThucdon,
            this.gcStrQuantity,
            this.gcQuantity,
            this.gcPrice,
            this.gcTotal});
            this.gv_menu_C.GridControl = this.gct_menu_C;
            this.gv_menu_C.IndicatorWidth = 30;
            this.gv_menu_C.Name = "gv_menu_C";
            this.gv_menu_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gv_menu_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gv_menu_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_menu_C.OptionsView.ShowFooter = true;
            this.gv_menu_C.OptionsView.ShowGroupPanel = false;
            this.gv_menu_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_menu_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // gcThucdon
            // 
            this.gcThucdon.Caption = "Thực đơn";
            this.gcThucdon.ColumnEdit = this.res_idcommodity;
            this.gcThucdon.FieldName = "idcommodity";
            this.gcThucdon.Name = "gcThucdon";
            this.gcThucdon.OptionsColumn.AllowEdit = false;
            this.gcThucdon.Visible = true;
            this.gcThucdon.VisibleIndex = 0;
            this.gcThucdon.Width = 100;
            // 
            // res_idcommodity
            // 
            this.res_idcommodity.AutoHeight = false;
            this.res_idcommodity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.res_idcommodity.DisplayMember = "commodity";
            this.res_idcommodity.Name = "res_idcommodity";
            this.res_idcommodity.ValueMember = "idcommodity";
            // 
            // gcStrQuantity
            // 
            this.gcStrQuantity.Caption = "SL";
            this.gcStrQuantity.FieldName = "strquantity";
            this.gcStrQuantity.Name = "gcStrQuantity";
            this.gcStrQuantity.OptionsColumn.AllowEdit = false;
            this.gcStrQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "quantity", "{0:N0}")});
            this.gcStrQuantity.Visible = true;
            this.gcStrQuantity.VisibleIndex = 1;
            this.gcStrQuantity.Width = 39;
            // 
            // gcQuantity
            // 
            this.gcQuantity.Caption = "SL";
            this.gcQuantity.DisplayFormat.FormatString = "N0";
            this.gcQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcQuantity.FieldName = "quantity";
            this.gcQuantity.Name = "gcQuantity";
            this.gcQuantity.OptionsColumn.AllowEdit = false;
            this.gcQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "quantity", "{0:N0}")});
            this.gcQuantity.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcQuantity.Width = 25;
            // 
            // gcPrice
            // 
            this.gcPrice.Caption = "Giá";
            this.gcPrice.DisplayFormat.FormatString = "N0";
            this.gcPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPrice.FieldName = "price";
            this.gcPrice.Name = "gcPrice";
            this.gcPrice.OptionsColumn.AllowEdit = false;
            this.gcPrice.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcPrice.Visible = true;
            this.gcPrice.VisibleIndex = 2;
            this.gcPrice.Width = 45;
            // 
            // gcTotal
            // 
            this.gcTotal.Caption = "Thành tiền";
            this.gcTotal.DisplayFormat.FormatString = "N0";
            this.gcTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcTotal.FieldName = "total";
            this.gcTotal.Name = "gcTotal";
            this.gcTotal.OptionsColumn.AllowEdit = false;
            this.gcTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "total", "{0:N0}")});
            this.gcTotal.UnboundExpression = "[quantity]* [price]";
            this.gcTotal.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcTotal.Visible = true;
            this.gcTotal.VisibleIndex = 3;
            this.gcTotal.Width = 68;
            // 
            // pn_top1_C
            // 
            this.pn_top1_C.Controls.Add(this.lbl_tablechoose_S);
            this.pn_top1_C.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_top1_C.Location = new System.Drawing.Point(2, 2);
            this.pn_top1_C.Name = "pn_top1_C";
            this.pn_top1_C.Size = new System.Drawing.Size(378, 76);
            this.pn_top1_C.TabIndex = 0;
            // 
            // lbl_tablechoose_S
            // 
            this.lbl_tablechoose_S.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tablechoose_S.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_tablechoose_S.Location = new System.Drawing.Point(21, 11);
            this.lbl_tablechoose_S.Name = "lbl_tablechoose_S";
            this.lbl_tablechoose_S.Size = new System.Drawing.Size(48, 35);
            this.lbl_tablechoose_S.TabIndex = 0;
            this.lbl_tablechoose_S.Text = "Bàn";
            // 
            // pn_middle_C
            // 
            this.pn_middle_C.Controls.Add(this.btn_refresh_S);
            this.pn_middle_C.Controls.Add(this.txt_quantity_S);
            this.pn_middle_C.Controls.Add(this.btn_right_S);
            this.pn_middle_C.Controls.Add(this.btn_cancel_S);
            this.pn_middle_C.Controls.Add(this.btn_agree_S);
            this.pn_middle_C.Controls.Add(this.btn_left_S);
            this.pn_middle_C.Dock = System.Windows.Forms.DockStyle.Left;
            this.pn_middle_C.Location = new System.Drawing.Point(382, 0);
            this.pn_middle_C.Name = "pn_middle_C";
            this.pn_middle_C.Size = new System.Drawing.Size(70, 506);
            this.pn_middle_C.TabIndex = 1;
            // 
            // btn_refresh_S
            // 
            this.btn_refresh_S.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btn_refresh_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh_S.Image")));
            this.btn_refresh_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_refresh_S.Location = new System.Drawing.Point(5, 63);
            this.btn_refresh_S.Name = "btn_refresh_S";
            this.btn_refresh_S.Size = new System.Drawing.Size(59, 63);
            this.btn_refresh_S.TabIndex = 2;
            this.btn_refresh_S.Click += new System.EventHandler(this.btn_refresh_S_Click);
            // 
            // txt_quantity_S
            // 
            this.txt_quantity_S.Location = new System.Drawing.Point(6, 132);
            this.txt_quantity_S.Name = "txt_quantity_S";
            this.txt_quantity_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_quantity_S.Properties.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txt_quantity_S.Properties.Appearance.Options.UseFont = true;
            this.txt_quantity_S.Properties.Appearance.Options.UseForeColor = true;
            this.txt_quantity_S.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_quantity_S.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txt_quantity_S.Properties.DisplayFormat.FormatString = "N0";
            this.txt_quantity_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txt_quantity_S.Properties.EditFormat.FormatString = "N0";
            this.txt_quantity_S.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txt_quantity_S.Properties.Mask.EditMask = "N0";
            this.txt_quantity_S.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_quantity_S.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txt_quantity_S.Properties.NullText = "1";
            this.txt_quantity_S.Size = new System.Drawing.Size(58, 20);
            this.txt_quantity_S.TabIndex = 1;
            this.txt_quantity_S.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_quantity_S_KeyPress);
            // 
            // btn_right_S
            // 
            this.btn_right_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_right_S.Image")));
            this.btn_right_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_right_S.Location = new System.Drawing.Point(5, 158);
            this.btn_right_S.Name = "btn_right_S";
            this.btn_right_S.Size = new System.Drawing.Size(59, 33);
            this.btn_right_S.TabIndex = 0;
            this.btn_right_S.Click += new System.EventHandler(this.btn_right_S_Click);
            // 
            // btn_cancel_S
            // 
            this.btn_cancel_S.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel_S.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_cancel_S.Appearance.Options.UseFont = true;
            this.btn_cancel_S.Appearance.Options.UseForeColor = true;
            this.btn_cancel_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_cancel_S.Location = new System.Drawing.Point(5, 314);
            this.btn_cancel_S.Name = "btn_cancel_S";
            this.btn_cancel_S.Size = new System.Drawing.Size(59, 52);
            this.btn_cancel_S.TabIndex = 0;
            this.btn_cancel_S.Text = "Hủy";
            this.btn_cancel_S.Click += new System.EventHandler(this.btn_cancel_S_Click);
            // 
            // btn_agree_S
            // 
            this.btn_agree_S.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_agree_S.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_agree_S.Appearance.Options.UseFont = true;
            this.btn_agree_S.Appearance.Options.UseForeColor = true;
            this.btn_agree_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_agree_S.Location = new System.Drawing.Point(6, 256);
            this.btn_agree_S.Name = "btn_agree_S";
            this.btn_agree_S.Size = new System.Drawing.Size(59, 52);
            this.btn_agree_S.TabIndex = 0;
            this.btn_agree_S.Text = "OK";
            this.btn_agree_S.Click += new System.EventHandler(this.btn_agree_S_Click);
            // 
            // btn_left_S
            // 
            this.btn_left_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_left_S.Image")));
            this.btn_left_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_left_S.Location = new System.Drawing.Point(6, 195);
            this.btn_left_S.Name = "btn_left_S";
            this.btn_left_S.Size = new System.Drawing.Size(59, 33);
            this.btn_left_S.TabIndex = 0;
            this.btn_left_S.Click += new System.EventHandler(this.btn_left_S_Click);
            // 
            // pn_right_C
            // 
            this.pn_right_C.Controls.Add(this.pn_main2_C);
            this.pn_right_C.Controls.Add(this.pn_top2_C);
            this.pn_right_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_right_C.Location = new System.Drawing.Point(452, 0);
            this.pn_right_C.Name = "pn_right_C";
            this.pn_right_C.Size = new System.Drawing.Size(403, 506);
            this.pn_right_C.TabIndex = 2;
            // 
            // pn_main2_C
            // 
            this.pn_main2_C.Controls.Add(this.gct_right_C);
            this.pn_main2_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_main2_C.Location = new System.Drawing.Point(2, 78);
            this.pn_main2_C.Name = "pn_main2_C";
            this.pn_main2_C.Size = new System.Drawing.Size(399, 426);
            this.pn_main2_C.TabIndex = 1;
            // 
            // gct_right_C
            // 
            this.gct_right_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_right_C.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gct_right_C.Location = new System.Drawing.Point(2, 2);
            this.gct_right_C.MainView = this.gv_right_C;
            this.gct_right_C.Margin = new System.Windows.Forms.Padding(0);
            this.gct_right_C.Name = "gct_right_C";
            this.gct_right_C.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gct_right_C.Size = new System.Drawing.Size(395, 422);
            this.gct_right_C.TabIndex = 11;
            this.gct_right_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_right_C});
            // 
            // gv_right_C
            // 
            this.gv_right_C.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_right_C.Appearance.EvenRow.Options.UseFont = true;
            this.gv_right_C.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_right_C.Appearance.FocusedRow.Options.UseFont = true;
            this.gv_right_C.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_right_C.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_right_C.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_right_C.Appearance.Preview.Options.UseFont = true;
            this.gv_right_C.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_right_C.Appearance.Row.Options.UseFont = true;
            this.gv_right_C.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcIdcommodity1,
            this.gcStrQuantity1,
            this.gcQuantity1,
            this.gcPrice1,
            this.gcTotal1});
            this.gv_right_C.GridControl = this.gct_right_C;
            this.gv_right_C.IndicatorWidth = 30;
            this.gv_right_C.Name = "gv_right_C";
            this.gv_right_C.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gv_right_C.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gv_right_C.OptionsNavigation.EnterMoveNextColumn = true;
            this.gv_right_C.OptionsView.ShowFooter = true;
            this.gv_right_C.OptionsView.ShowGroupPanel = false;
            this.gv_right_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gv_right_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            // 
            // gcIdcommodity1
            // 
            this.gcIdcommodity1.Caption = "Thực đơn";
            this.gcIdcommodity1.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gcIdcommodity1.FieldName = "idcommodity";
            this.gcIdcommodity1.Name = "gcIdcommodity1";
            this.gcIdcommodity1.OptionsColumn.AllowEdit = false;
            this.gcIdcommodity1.Visible = true;
            this.gcIdcommodity1.VisibleIndex = 0;
            this.gcIdcommodity1.Width = 100;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DisplayMember = "commodity";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ValueMember = "idcommodity";
            // 
            // gcStrQuantity1
            // 
            this.gcStrQuantity1.Caption = "SL";
            this.gcStrQuantity1.FieldName = "strquantity";
            this.gcStrQuantity1.Name = "gcStrQuantity1";
            this.gcStrQuantity1.OptionsColumn.AllowEdit = false;
            this.gcStrQuantity1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "quantity", "{0:N0}")});
            this.gcStrQuantity1.Visible = true;
            this.gcStrQuantity1.VisibleIndex = 1;
            this.gcStrQuantity1.Width = 39;
            // 
            // gcQuantity1
            // 
            this.gcQuantity1.Caption = "SL";
            this.gcQuantity1.DisplayFormat.FormatString = "N0";
            this.gcQuantity1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcQuantity1.FieldName = "quantity";
            this.gcQuantity1.Name = "gcQuantity1";
            this.gcQuantity1.OptionsColumn.AllowEdit = false;
            this.gcQuantity1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "quantity", "{0:N0}")});
            this.gcQuantity1.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcQuantity1.Width = 25;
            // 
            // gcPrice1
            // 
            this.gcPrice1.Caption = "Giá";
            this.gcPrice1.DisplayFormat.FormatString = "N0";
            this.gcPrice1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPrice1.FieldName = "price";
            this.gcPrice1.Name = "gcPrice1";
            this.gcPrice1.OptionsColumn.AllowEdit = false;
            this.gcPrice1.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcPrice1.Visible = true;
            this.gcPrice1.VisibleIndex = 2;
            this.gcPrice1.Width = 45;
            // 
            // gcTotal1
            // 
            this.gcTotal1.Caption = "Thành tiền";
            this.gcTotal1.DisplayFormat.FormatString = "N0";
            this.gcTotal1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcTotal1.FieldName = "total";
            this.gcTotal1.Name = "gcTotal1";
            this.gcTotal1.OptionsColumn.AllowEdit = false;
            this.gcTotal1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "total", "{0:N0}")});
            this.gcTotal1.UnboundExpression = "[quantity]* [price]";
            this.gcTotal1.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gcTotal1.Visible = true;
            this.gcTotal1.VisibleIndex = 3;
            this.gcTotal1.Width = 68;
            // 
            // pn_top2_C
            // 
            this.pn_top2_C.Controls.Add(this.lbl_tablemove_S);
            this.pn_top2_C.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_top2_C.Location = new System.Drawing.Point(2, 2);
            this.pn_top2_C.Name = "pn_top2_C";
            this.pn_top2_C.Size = new System.Drawing.Size(399, 76);
            this.pn_top2_C.TabIndex = 0;
            // 
            // lbl_tablemove_S
            // 
            this.lbl_tablemove_S.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tablemove_S.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_tablemove_S.Location = new System.Drawing.Point(23, 11);
            this.lbl_tablemove_S.Name = "lbl_tablemove_S";
            this.lbl_tablemove_S.Size = new System.Drawing.Size(48, 35);
            this.lbl_tablemove_S.TabIndex = 1;
            this.lbl_tablemove_S.Text = "Bàn";
            // 
            // frmSplitTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 506);
            this.Controls.Add(this.pn_right_C);
            this.Controls.Add(this.pn_middle_C);
            this.Controls.Add(this.pn_left_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSplitTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: TÁCH BÀN ::.";
            this.Load += new System.EventHandler(this.frmSplitTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pn_left_C)).EndInit();
            this.pn_left_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pn_main1_C)).EndInit();
            this.pn_main1_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_menu_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_menu_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_idcommodity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_top1_C)).EndInit();
            this.pn_top1_C.ResumeLayout(false);
            this.pn_top1_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pn_middle_C)).EndInit();
            this.pn_middle_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_quantity_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_right_C)).EndInit();
            this.pn_right_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pn_main2_C)).EndInit();
            this.pn_main2_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_right_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_right_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pn_top2_C)).EndInit();
            this.pn_top2_C.ResumeLayout(false);
            this.pn_top2_C.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pn_left_C;
        private DevExpress.XtraEditors.PanelControl pn_main1_C;
        private DevExpress.XtraEditors.PanelControl pn_top1_C;
        private DevExpress.XtraEditors.PanelControl pn_middle_C;
        private DevExpress.XtraEditors.SimpleButton btn_left_S;
        private DevExpress.XtraEditors.PanelControl pn_right_C;
        private DevExpress.XtraEditors.PanelControl pn_main2_C;
        private DevExpress.XtraEditors.PanelControl pn_top2_C;
        private DevExpress.XtraEditors.SimpleButton btn_right_S;
        private DevExpress.XtraGrid.GridControl gct_menu_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_menu_C;
        private DevExpress.XtraGrid.Columns.GridColumn gcThucdon;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit res_idcommodity;
        private DevExpress.XtraGrid.Columns.GridColumn gcStrQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gcTotal;
        private DevExpress.XtraEditors.LabelControl lbl_tablechoose_S;
        private DevExpress.XtraEditors.TextEdit txt_quantity_S;
        private DevExpress.XtraGrid.GridControl gct_right_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_right_C;
        private DevExpress.XtraGrid.Columns.GridColumn gcIdcommodity1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gcStrQuantity1;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuantity1;
        private DevExpress.XtraGrid.Columns.GridColumn gcPrice1;
        private DevExpress.XtraGrid.Columns.GridColumn gcTotal1;
        private DevExpress.XtraEditors.LabelControl lbl_tablemove_S;
        private DevExpress.XtraEditors.SimpleButton btn_cancel_S;
        private DevExpress.XtraEditors.SimpleButton btn_agree_S;
        private DevExpress.XtraEditors.SimpleButton btn_refresh_S;

    }
}