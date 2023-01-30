namespace SOURCE_FORM_TEAM.Presentation
{
    partial class frm_DMTEAM_S
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
            this.grp_footer_C = new DevExpress.XtraEditors.GroupControl();
            this.btn_insert_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.grp_main_C = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.glue_idemp_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.dte_todate_I5 = new DevExpress.XtraEditors.DateEdit();
            this.dte_fromdate_I5 = new DevExpress.XtraEditors.DateEdit();
            this.chk_status_I6 = new DevExpress.XtraEditors.CheckEdit();
            this.txt_idteam_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.mmo_note_I3 = new DevExpress.XtraEditors.MemoEdit();
            this.txt_sign_20_I1 = new DevExpress.XtraEditors.TextEdit();
            this.txt_teamname_100_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dxEp_error_S = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).BeginInit();
            this.grp_footer_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).BeginInit();
            this.grp_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idemp_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_status_I6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idteam_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_sign_20_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_teamname_100_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_footer_C
            // 
            this.grp_footer_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grp_footer_C.Controls.Add(this.btn_insert_allow_insert);
            this.grp_footer_C.Controls.Add(this.bbi_allow_insert);
            this.grp_footer_C.Controls.Add(this.btn_exit_S);
            this.grp_footer_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_footer_C.Location = new System.Drawing.Point(0, 216);
            this.grp_footer_C.Name = "grp_footer_C";
            this.grp_footer_C.Size = new System.Drawing.Size(433, 39);
            this.grp_footer_C.TabIndex = 0;
            // 
            // btn_insert_allow_insert
            // 
            this.btn_insert_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_insert_allow_insert.Image = global::SOURCE_FORM_TEAM.Properties.Resources.save_16x16;
            this.btn_insert_allow_insert.Location = new System.Drawing.Point(109, 3);
            this.btn_insert_allow_insert.Name = "btn_insert_allow_insert";
            this.btn_insert_allow_insert.Size = new System.Drawing.Size(117, 31);
            this.btn_insert_allow_insert.TabIndex = 6;
            this.btn_insert_allow_insert.Text = "F4 Lưu - Thêm";
            this.btn_insert_allow_insert.Click += new System.EventHandler(this.btn_insert_allow_insert_Click);
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbi_allow_insert.Image = global::SOURCE_FORM_TEAM.Properties.Resources.save_16x16;
            this.bbi_allow_insert.Location = new System.Drawing.Point(227, 3);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(120, 31);
            this.bbi_allow_insert.TabIndex = 7;
            this.bbi_allow_insert.Text = "F5 Lưu - Thoát";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_TEAM.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(348, 3);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 8;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // grp_main_C
            // 
            this.grp_main_C.Controls.Add(this.groupControl2);
            this.grp_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_main_C.Location = new System.Drawing.Point(0, 0);
            this.grp_main_C.Name = "grp_main_C";
            this.grp_main_C.Size = new System.Drawing.Size(433, 216);
            this.grp_main_C.TabIndex = 1;
            this.grp_main_C.Text = "Thông tin";
            // 
            // groupControl2
            // 
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl2.Controls.Add(this.glue_idemp_I1);
            this.groupControl2.Controls.Add(this.labelControl23);
            this.groupControl2.Controls.Add(this.dte_todate_I5);
            this.groupControl2.Controls.Add(this.dte_fromdate_I5);
            this.groupControl2.Controls.Add(this.chk_status_I6);
            this.groupControl2.Controls.Add(this.txt_idteam_IK1);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.mmo_note_I3);
            this.groupControl2.Controls.Add(this.txt_sign_20_I1);
            this.groupControl2.Controls.Add(this.txt_teamname_100_I2);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 21);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(429, 193);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "groupControl2";
            // 
            // glue_idemp_I1
            // 
            this.glue_idemp_I1.EditValue = "";
            this.glue_idemp_I1.Location = new System.Drawing.Point(105, 102);
            this.glue_idemp_I1.Name = "glue_idemp_I1";
            this.glue_idemp_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_idemp_I1.Properties.NullText = "";
            this.glue_idemp_I1.Properties.View = this.gridView3;
            this.glue_idemp_I1.Size = new System.Drawing.Size(319, 20);
            this.glue_idemp_I1.TabIndex = 4;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl23.Location = new System.Drawing.Point(8, 105);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(73, 13);
            this.labelControl23.TabIndex = 17;
            this.labelControl23.Text = "Đội trưởng (*):";
            // 
            // dte_todate_I5
            // 
            this.dte_todate_I5.EditValue = null;
            this.dte_todate_I5.Location = new System.Drawing.Point(324, 80);
            this.dte_todate_I5.Name = "dte_todate_I5";
            this.dte_todate_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_todate_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_todate_I5.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_todate_I5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_todate_I5.Size = new System.Drawing.Size(100, 20);
            this.dte_todate_I5.TabIndex = 3;
            // 
            // dte_fromdate_I5
            // 
            this.dte_fromdate_I5.EditValue = null;
            this.dte_fromdate_I5.Location = new System.Drawing.Point(105, 80);
            this.dte_fromdate_I5.Name = "dte_fromdate_I5";
            this.dte_fromdate_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_fromdate_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_fromdate_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_fromdate_I5.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_fromdate_I5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_fromdate_I5.Size = new System.Drawing.Size(100, 20);
            this.dte_fromdate_I5.TabIndex = 2;
            // 
            // chk_status_I6
            // 
            this.chk_status_I6.EditValue = true;
            this.chk_status_I6.Location = new System.Drawing.Point(270, 17);
            this.chk_status_I6.Name = "chk_status_I6";
            this.chk_status_I6.Properties.Caption = "Status";
            this.chk_status_I6.Size = new System.Drawing.Size(154, 19);
            this.chk_status_I6.TabIndex = 99;
            // 
            // txt_idteam_IK1
            // 
            this.txt_idteam_IK1.Enabled = false;
            this.txt_idteam_IK1.Location = new System.Drawing.Point(105, 17);
            this.txt_idteam_IK1.Name = "txt_idteam_IK1";
            this.txt_idteam_IK1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_idteam_IK1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_idteam_IK1.Properties.MaxLength = 20;
            this.txt_idteam_IK1.Size = new System.Drawing.Size(159, 20);
            this.txt_idteam_IK1.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl1.Location = new System.Drawing.Point(8, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã đội(*):";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl2.Location = new System.Drawing.Point(8, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ký hiệu (*):";
            // 
            // mmo_note_I3
            // 
            this.mmo_note_I3.Location = new System.Drawing.Point(105, 125);
            this.mmo_note_I3.Name = "mmo_note_I3";
            this.mmo_note_I3.Properties.MaxLength = 500;
            this.mmo_note_I3.Size = new System.Drawing.Size(319, 60);
            this.mmo_note_I3.TabIndex = 5;
            this.mmo_note_I3.UseOptimizedRendering = true;
            this.mmo_note_I3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // txt_sign_20_I1
            // 
            this.txt_sign_20_I1.Location = new System.Drawing.Point(105, 38);
            this.txt_sign_20_I1.Name = "txt_sign_20_I1";
            this.txt_sign_20_I1.Properties.MaxLength = 20;
            this.txt_sign_20_I1.Size = new System.Drawing.Size(159, 20);
            this.txt_sign_20_I1.TabIndex = 0;
            this.txt_sign_20_I1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // txt_teamname_100_I2
            // 
            this.txt_teamname_100_I2.Location = new System.Drawing.Point(105, 59);
            this.txt_teamname_100_I2.Name = "txt_teamname_100_I2";
            this.txt_teamname_100_I2.Properties.MaxLength = 100;
            this.txt_teamname_100_I2.Size = new System.Drawing.Size(319, 20);
            this.txt_teamname_100_I2.TabIndex = 1;
            this.txt_teamname_100_I2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(211, 86);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl3.Location = new System.Drawing.Point(8, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Tên đội:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 86);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(44, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Từ ngày:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 138);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Ghi chú:";
            // 
            // dxEp_error_S
            // 
            this.dxEp_error_S.ContainerControl = this;
            // 
            // frm_DMTEAM_S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 255);
            this.ControlBox = false;
            this.Controls.Add(this.grp_main_C);
            this.Controls.Add(this.grp_footer_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_DMTEAM_S";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: ĐỘI TÁC NGHIỆP  ::.";
            this.Activated += new System.EventHandler(this.frm_DMCAMPAIGN_S_Activated);
            this.Load += new System.EventHandler(this.frm_DMCAMPAIGN_S_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DMAREA_S_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).EndInit();
            this.grp_footer_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).EndInit();
            this.grp_main_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_idemp_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_todate_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_fromdate_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_status_I6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idteam_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_sign_20_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_teamname_100_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_footer_C;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.GroupControl grp_main_C;
        private DevExpress.XtraEditors.CheckEdit chk_status_I6;
        private DevExpress.XtraEditors.MemoEdit mmo_note_I3;
        private DevExpress.XtraEditors.TextEdit txt_teamname_100_I2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_sign_20_I1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_idteam_IK1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxEp_error_S;
        private DevExpress.XtraEditors.SimpleButton btn_insert_allow_insert;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.DateEdit dte_todate_I5;
        private DevExpress.XtraEditors.DateEdit dte_fromdate_I5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.GridLookUpEdit glue_idemp_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelControl23;
    }
}