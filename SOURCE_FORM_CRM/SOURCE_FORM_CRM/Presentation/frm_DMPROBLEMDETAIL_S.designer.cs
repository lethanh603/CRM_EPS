namespace SOURCE_FORM_CRM.Presentation
{
    partial class frm_DMPROBLEMDETAIL_S
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
            DevExpress.XtraEditors.DateEdit dte_dateprocess_I5;
            this.grp_footer_C = new DevExpress.XtraEditors.GroupControl();
            this.btn_insert_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.grp_main_C = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.rg_issupport_I14 = new DevExpress.XtraEditors.RadioGroup();
            this.cbo_process_I4 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txt_idproblemdetail_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mmo_solution_I3 = new DevExpress.XtraEditors.MemoEdit();
            this.mmo_note_I3 = new DevExpress.XtraEditors.MemoEdit();
            this.txt_idemp_I2 = new DevExpress.XtraEditors.TextEdit();
            this.txt_idproblem_20_I1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dxEp_error_S = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txt_idphatsinh_I2 = new DevExpress.XtraEditors.TextEdit();
            dte_dateprocess_I5 = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(dte_dateprocess_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dte_dateprocess_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).BeginInit();
            this.grp_footer_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).BeginInit();
            this.grp_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_issupport_I14.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_process_I4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idproblemdetail_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_solution_I3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idemp_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idproblem_20_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idphatsinh_I2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dte_dateprocess_I5
            // 
            dte_dateprocess_I5.EditValue = null;
            dte_dateprocess_I5.Location = new System.Drawing.Point(105, 97);
            dte_dateprocess_I5.Name = "dte_dateprocess_I5";
            dte_dateprocess_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            dte_dateprocess_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            dte_dateprocess_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            dte_dateprocess_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dte_dateprocess_I5.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            dte_dateprocess_I5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dte_dateprocess_I5.Properties.Mask.EditMask = "dd/MM/yyyy";
            dte_dateprocess_I5.Size = new System.Drawing.Size(95, 20);
            dte_dateprocess_I5.TabIndex = 2;
            // 
            // grp_footer_C
            // 
            this.grp_footer_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grp_footer_C.Controls.Add(this.btn_insert_allow_insert);
            this.grp_footer_C.Controls.Add(this.bbi_allow_insert);
            this.grp_footer_C.Controls.Add(this.btn_exit_S);
            this.grp_footer_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_footer_C.Location = new System.Drawing.Point(0, 236);
            this.grp_footer_C.Name = "grp_footer_C";
            this.grp_footer_C.Size = new System.Drawing.Size(474, 39);
            this.grp_footer_C.TabIndex = 0;
            // 
            // btn_insert_allow_insert
            // 
            this.btn_insert_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_insert_allow_insert.Image = global::SOURCE_FORM_CRM.Properties.Resources.save_16x16;
            this.btn_insert_allow_insert.Location = new System.Drawing.Point(150, 3);
            this.btn_insert_allow_insert.Name = "btn_insert_allow_insert";
            this.btn_insert_allow_insert.Size = new System.Drawing.Size(117, 31);
            this.btn_insert_allow_insert.TabIndex = 8;
            this.btn_insert_allow_insert.Text = "F4 Lưu - Thêm";
            this.btn_insert_allow_insert.Visible = false;
            this.btn_insert_allow_insert.Click += new System.EventHandler(this.btn_insert_allow_insert_Click);
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbi_allow_insert.Image = global::SOURCE_FORM_CRM.Properties.Resources.save_16x16;
            this.bbi_allow_insert.Location = new System.Drawing.Point(268, 3);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(120, 31);
            this.bbi_allow_insert.TabIndex = 6;
            this.bbi_allow_insert.Text = "F5 Lưu - Thoát";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_CRM.Properties.Resources.bbi_thoat_s;
            this.btn_exit_S.Location = new System.Drawing.Point(389, 3);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 7;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // grp_main_C
            // 
            this.grp_main_C.Controls.Add(this.groupControl2);
            this.grp_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_main_C.Location = new System.Drawing.Point(0, 0);
            this.grp_main_C.Name = "grp_main_C";
            this.grp_main_C.Size = new System.Drawing.Size(474, 236);
            this.grp_main_C.TabIndex = 1;
            this.grp_main_C.Text = "Thông tin";
            // 
            // groupControl2
            // 
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl2.Controls.Add(this.rg_issupport_I14);
            this.groupControl2.Controls.Add(this.cbo_process_I4);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(dte_dateprocess_I5);
            this.groupControl2.Controls.Add(this.txt_idphatsinh_I2);
            this.groupControl2.Controls.Add(this.txt_idproblemdetail_IK1);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.mmo_solution_I3);
            this.groupControl2.Controls.Add(this.mmo_note_I3);
            this.groupControl2.Controls.Add(this.txt_idemp_I2);
            this.groupControl2.Controls.Add(this.txt_idproblem_20_I1);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 21);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(470, 213);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "groupControl2";
            // 
            // rg_issupport_I14
            // 
            this.rg_issupport_I14.EditValue = 0;
            this.rg_issupport_I14.Location = new System.Drawing.Point(244, 96);
            this.rg_issupport_I14.Name = "rg_issupport_I14";
            this.rg_issupport_I14.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tự giải quyết"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Cần hỗ trợ")});
            this.rg_issupport_I14.Size = new System.Drawing.Size(218, 47);
            this.rg_issupport_I14.TabIndex = 4;
            // 
            // cbo_process_I4
            // 
            this.cbo_process_I4.EditValue = "0";
            this.cbo_process_I4.Location = new System.Drawing.Point(105, 120);
            this.cbo_process_I4.Name = "cbo_process_I4";
            this.cbo_process_I4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_process_I4.Properties.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cbo_process_I4.Properties.NullText = "0";
            this.cbo_process_I4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_process_I4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbo_process_I4.Size = new System.Drawing.Size(70, 20);
            this.cbo_process_I4.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(189, 124);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(11, 13);
            this.labelControl6.TabIndex = 63;
            this.labelControl6.Text = "%";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl8.Location = new System.Drawing.Point(9, 126);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(42, 13);
            this.labelControl8.TabIndex = 62;
            this.labelControl8.Text = "Tiến độ :";
            // 
            // txt_idproblemdetail_IK1
            // 
            this.txt_idproblemdetail_IK1.Enabled = false;
            this.txt_idproblemdetail_IK1.Location = new System.Drawing.Point(105, 17);
            this.txt_idproblemdetail_IK1.Name = "txt_idproblemdetail_IK1";
            this.txt_idproblemdetail_IK1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_idproblemdetail_IK1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_idproblemdetail_IK1.Properties.MaxLength = 20;
            this.txt_idproblemdetail_IK1.Size = new System.Drawing.Size(95, 20);
            this.txt_idproblemdetail_IK1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl1.Location = new System.Drawing.Point(9, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã(*):";
            // 
            // mmo_solution_I3
            // 
            this.mmo_solution_I3.Location = new System.Drawing.Point(105, 39);
            this.mmo_solution_I3.Name = "mmo_solution_I3";
            this.mmo_solution_I3.Properties.MaxLength = 500;
            this.mmo_solution_I3.Size = new System.Drawing.Size(357, 55);
            this.mmo_solution_I3.TabIndex = 1;
            this.mmo_solution_I3.UseOptimizedRendering = true;
            this.mmo_solution_I3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // mmo_note_I3
            // 
            this.mmo_note_I3.Location = new System.Drawing.Point(105, 145);
            this.mmo_note_I3.Name = "mmo_note_I3";
            this.mmo_note_I3.Properties.MaxLength = 500;
            this.mmo_note_I3.Size = new System.Drawing.Size(357, 60);
            this.mmo_note_I3.TabIndex = 5;
            this.mmo_note_I3.UseOptimizedRendering = true;
            this.mmo_note_I3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // txt_idemp_I2
            // 
            this.txt_idemp_I2.Location = new System.Drawing.Point(244, 174);
            this.txt_idemp_I2.Name = "txt_idemp_I2";
            this.txt_idemp_I2.Properties.MaxLength = 20;
            this.txt_idemp_I2.Size = new System.Drawing.Size(159, 20);
            this.txt_idemp_I2.TabIndex = 2;
            this.txt_idemp_I2.Visible = false;
            this.txt_idemp_I2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // txt_idproblem_20_I1
            // 
            this.txt_idproblem_20_I1.Location = new System.Drawing.Point(244, 153);
            this.txt_idproblem_20_I1.Name = "txt_idproblem_20_I1";
            this.txt_idproblem_20_I1.Properties.MaxLength = 20;
            this.txt_idproblem_20_I1.Size = new System.Drawing.Size(159, 20);
            this.txt_idproblem_20_I1.TabIndex = 2;
            this.txt_idproblem_20_I1.Visible = false;
            this.txt_idproblem_20_I1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl3.Location = new System.Drawing.Point(8, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Sự vụ:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 103);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Ngày xử lý:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 156);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Ghi chú:";
            // 
            // dxEp_error_S
            // 
            this.dxEp_error_S.ContainerControl = this;
            // 
            // txt_idphatsinh_I2
            // 
            this.txt_idphatsinh_I2.Enabled = false;
            this.txt_idphatsinh_I2.Location = new System.Drawing.Point(206, 17);
            this.txt_idphatsinh_I2.Name = "txt_idphatsinh_I2";
            this.txt_idphatsinh_I2.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_idphatsinh_I2.Properties.Appearance.Options.UseBackColor = true;
            this.txt_idphatsinh_I2.Properties.MaxLength = 20;
            this.txt_idphatsinh_I2.Size = new System.Drawing.Size(95, 20);
            this.txt_idphatsinh_I2.TabIndex = 0;
            this.txt_idphatsinh_I2.Visible = false;
            // 
            // frm_DMPROBLEMDETAIL_S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 275);
            this.ControlBox = false;
            this.Controls.Add(this.grp_main_C);
            this.Controls.Add(this.grp_footer_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_DMPROBLEMDETAIL_S";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: GIẢI QUYẾT SỰ VỤ ::.";
            this.Activated += new System.EventHandler(this.frm_DMCAMPAIGN_S_Activated);
            this.Load += new System.EventHandler(this.frm_DMCAMPAIGN_S_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DMAREA_S_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(dte_dateprocess_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dte_dateprocess_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).EndInit();
            this.grp_footer_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).EndInit();
            this.grp_main_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_issupport_I14.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_process_I4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idproblemdetail_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_solution_I3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idemp_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idproblem_20_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idphatsinh_I2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_footer_C;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.GroupControl grp_main_C;
        private DevExpress.XtraEditors.MemoEdit mmo_note_I3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_idproblemdetail_IK1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxEp_error_S;
        private DevExpress.XtraEditors.SimpleButton btn_insert_allow_insert;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.MemoEdit mmo_solution_I3;
        public DevExpress.XtraEditors.TextEdit txt_idproblem_20_I1;
        public DevExpress.XtraEditors.TextEdit txt_idemp_I2;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_process_I4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.RadioGroup rg_issupport_I14;
        private DevExpress.XtraEditors.TextEdit txt_idphatsinh_I2;
    }
}