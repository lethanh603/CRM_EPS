namespace LoyalHRM.Presentation
{
    partial class frmTranLate
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gro_info_C = new DevExpress.XtraEditors.GroupControl();
            this.chk_hoikhiluu_S = new DevExpress.XtraEditors.CheckEdit();
            this.chk_showall_S = new DevExpress.XtraEditors.CheckEdit();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_restore_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_backup_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_export_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_import_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_delete_S = new DevExpress.XtraEditors.SimpleButton();
            this.cbo_langguage_S = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbo_field_I1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.gct_list_C = new DevExpress.XtraGrid.GridControl();
            this.gv_list_C = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dxe_error_C = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.grp_main_C = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.gro_info_C)).BeginInit();
            this.gro_info_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_hoikhiluu_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_showall_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_langguage_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_field_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxe_error_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).BeginInit();
            this.grp_main_C.SuspendLayout();
            this.SuspendLayout();
            // 
            // gro_info_C
            // 
            this.gro_info_C.Controls.Add(this.chk_hoikhiluu_S);
            this.gro_info_C.Controls.Add(this.chk_showall_S);
            this.gro_info_C.Controls.Add(this.btn_exit_S);
            this.gro_info_C.Controls.Add(this.btn_restore_S);
            this.gro_info_C.Controls.Add(this.btn_backup_S);
            this.gro_info_C.Controls.Add(this.btn_export_S);
            this.gro_info_C.Controls.Add(this.btn_import_S);
            this.gro_info_C.Controls.Add(this.btn_delete_S);
            this.gro_info_C.Controls.Add(this.cbo_langguage_S);
            this.gro_info_C.Controls.Add(this.labelControl1);
            this.gro_info_C.Controls.Add(this.cbo_field_I1);
            this.gro_info_C.Controls.Add(this.labelControl7);
            this.gro_info_C.Dock = System.Windows.Forms.DockStyle.Top;
            this.gro_info_C.Location = new System.Drawing.Point(0, 0);
            this.gro_info_C.Name = "gro_info_C";
            this.gro_info_C.Size = new System.Drawing.Size(764, 67);
            this.gro_info_C.TabIndex = 0;
            this.gro_info_C.Text = "Thông tin";
            // 
            // chk_hoikhiluu_S
            // 
            this.chk_hoikhiluu_S.Location = new System.Drawing.Point(97, 1);
            this.chk_hoikhiluu_S.Name = "chk_hoikhiluu_S";
            this.chk_hoikhiluu_S.Properties.Caption = "Hỏi khi lưu";
            this.chk_hoikhiluu_S.Size = new System.Drawing.Size(75, 19);
            this.chk_hoikhiluu_S.TabIndex = 14;
            // 
            // chk_showall_S
            // 
            this.chk_showall_S.Location = new System.Drawing.Point(252, 45);
            this.chk_showall_S.Name = "chk_showall_S";
            this.chk_showall_S.Properties.Caption = "Tất cả";
            this.chk_showall_S.Size = new System.Drawing.Size(55, 19);
            this.chk_showall_S.TabIndex = 13;
            this.chk_showall_S.CheckedChanged += new System.EventHandler(this.chk_showall_S_CheckedChanged);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = global::LoyalHRM.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_exit_S.Location = new System.Drawing.Point(673, 21);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(86, 44);
            this.btn_exit_S.TabIndex = 11;
            this.btn_exit_S.Text = "Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // btn_restore_S
            // 
            this.btn_restore_S.Image = global::LoyalHRM.Properties.Resources.restore16;
            this.btn_restore_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_restore_S.Location = new System.Drawing.Point(457, 21);
            this.btn_restore_S.Name = "btn_restore_S";
            this.btn_restore_S.Size = new System.Drawing.Size(73, 44);
            this.btn_restore_S.TabIndex = 10;
            this.btn_restore_S.Text = "Phục hồi";
            this.btn_restore_S.Click += new System.EventHandler(this.btn_restore_S_Click);
            // 
            // btn_backup_S
            // 
            this.btn_backup_S.Image = global::LoyalHRM.Properties.Resources.backup16;
            this.btn_backup_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_backup_S.Location = new System.Drawing.Point(383, 21);
            this.btn_backup_S.Name = "btn_backup_S";
            this.btn_backup_S.Size = new System.Drawing.Size(73, 44);
            this.btn_backup_S.TabIndex = 10;
            this.btn_backup_S.Text = "Lưu";
            this.btn_backup_S.Click += new System.EventHandler(this.btn_backup_S_Click);
            // 
            // btn_export_S
            // 
            this.btn_export_S.Image = global::LoyalHRM.Properties.Resources.excel;
            this.btn_export_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_export_S.Location = new System.Drawing.Point(603, 21);
            this.btn_export_S.Name = "btn_export_S";
            this.btn_export_S.Size = new System.Drawing.Size(69, 44);
            this.btn_export_S.TabIndex = 10;
            this.btn_export_S.Text = "Xuất";
            this.btn_export_S.Click += new System.EventHandler(this.btn_export_S_Click);
            // 
            // btn_import_S
            // 
            this.btn_import_S.Image = global::LoyalHRM.Properties.Resources.excel;
            this.btn_import_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_import_S.Location = new System.Drawing.Point(531, 21);
            this.btn_import_S.Name = "btn_import_S";
            this.btn_import_S.Size = new System.Drawing.Size(71, 44);
            this.btn_import_S.TabIndex = 10;
            this.btn_import_S.Text = "Nhập";
            this.btn_import_S.Click += new System.EventHandler(this.btn_import_S_Click);
            // 
            // btn_delete_S
            // 
            this.btn_delete_S.Image = global::LoyalHRM.Properties.Resources.delete;
            this.btn_delete_S.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btn_delete_S.Location = new System.Drawing.Point(313, 21);
            this.btn_delete_S.Name = "btn_delete_S";
            this.btn_delete_S.Size = new System.Drawing.Size(69, 44);
            this.btn_delete_S.TabIndex = 10;
            this.btn_delete_S.Text = "Xóa";
            this.btn_delete_S.Click += new System.EventHandler(this.btn_delete_S_Click);
            // 
            // cbo_langguage_S
            // 
            this.cbo_langguage_S.EditValue = "";
            this.cbo_langguage_S.Location = new System.Drawing.Point(97, 45);
            this.cbo_langguage_S.Name = "cbo_langguage_S";
            this.cbo_langguage_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_langguage_S.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_langguage_S.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cbo_field_I1_Properties_ButtonClick);
            this.cbo_langguage_S.Size = new System.Drawing.Size(148, 20);
            this.cbo_langguage_S.TabIndex = 9;
            this.cbo_langguage_S.SelectedIndexChanged += new System.EventHandler(this.cbo_langguage_S_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelControl1.AllowDrop = true;
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 47);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Ngôn ngữ gốc:";
            // 
            // cbo_field_I1
            // 
            this.cbo_field_I1.EditValue = "";
            this.cbo_field_I1.Location = new System.Drawing.Point(97, 22);
            this.cbo_field_I1.Name = "cbo_field_I1";
            this.cbo_field_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::LoyalHRM.Properties.Resources.add, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Insert", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::LoyalHRM.Properties.Resources.notes_edit, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "edit", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::LoyalHRM.Properties.Resources.delete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Delete", null, null, true)});
            this.cbo_field_I1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_field_I1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cbo_field_I1_Properties_ButtonClick);
            this.cbo_field_I1.Size = new System.Drawing.Size(214, 22);
            this.cbo_field_I1.TabIndex = 9;
            // 
            // labelControl7
            // 
            this.labelControl7.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelControl7.AllowDrop = true;
            this.labelControl7.AllowHtmlString = true;
            this.labelControl7.Location = new System.Drawing.Point(5, 24);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(51, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Ngôn ngữ:";
            // 
            // gct_list_C
            // 
            this.gct_list_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_list_C.Location = new System.Drawing.Point(0, 0);
            this.gct_list_C.MainView = this.gv_list_C;
            this.gct_list_C.Name = "gct_list_C";
            this.gct_list_C.Size = new System.Drawing.Size(764, 465);
            this.gct_list_C.TabIndex = 0;
            this.gct_list_C.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_list_C});
            this.gct_list_C.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gct_list_C_ProcessGridKey);
            // 
            // gv_list_C
            // 
            this.gv_list_C.GridControl = this.gct_list_C;
            this.gv_list_C.IndicatorWidth = 40;
            this.gv_list_C.Name = "gv_list_C";
            this.gv_list_C.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grid_CustomDrawRowIndicator);
            this.gv_list_C.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grid_CustomDrawCell);
            this.gv_list_C.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gv_list_C_CellValueChanged);
            // 
            // dxe_error_C
            // 
            this.dxe_error_C.ContainerControl = this;
            // 
            // grp_main_C
            // 
            this.grp_main_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grp_main_C.Controls.Add(this.gct_list_C);
            this.grp_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_main_C.Location = new System.Drawing.Point(0, 67);
            this.grp_main_C.Name = "grp_main_C";
            this.grp_main_C.Size = new System.Drawing.Size(764, 465);
            this.grp_main_C.TabIndex = 2;
            this.grp_main_C.Text = "Ngôn ngữ";
            // 
            // frmTranLate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 532);
            this.Controls.Add(this.grp_main_C);
            this.Controls.Add(this.gro_info_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTranLate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DỊCH NGÔN NGỮ";
            this.Load += new System.EventHandler(this.frmTranLate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gro_info_C)).EndInit();
            this.gro_info_C.ResumeLayout(false);
            this.gro_info_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_hoikhiluu_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_showall_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_langguage_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_field_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gct_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxe_error_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).EndInit();
            this.grp_main_C.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gro_info_C;
        private DevExpress.XtraGrid.GridControl gct_list_C;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_list_C;
        private DevExpress.XtraEditors.SimpleButton btn_import_S;
        private DevExpress.XtraEditors.SimpleButton btn_delete_S;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_field_I1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxe_error_C;
        private DevExpress.XtraEditors.SimpleButton btn_export_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.GroupControl grp_main_C;
        private DevExpress.XtraEditors.SimpleButton btn_restore_S;
        private DevExpress.XtraEditors.SimpleButton btn_backup_S;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_langguage_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chk_hoikhiluu_S;
        private DevExpress.XtraEditors.CheckEdit chk_showall_S;
    }
}