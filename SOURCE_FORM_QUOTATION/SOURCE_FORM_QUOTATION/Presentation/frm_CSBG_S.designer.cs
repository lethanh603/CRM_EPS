namespace SOURCE_FORM_QUOTATION.Presentation
{
    partial class frm_CSBG_S
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
            this.grp_footer_C = new DevExpress.XtraEditors.GroupControl();
            this.btn_insert_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.grp_main_C = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dte_ngaytao_I5 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_macs_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mmo_note_I3 = new DevExpress.XtraEditors.MemoEdit();
            this.dxEp_error_S = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.txt_idexport_I1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).BeginInit();
            this.grp_footer_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).BeginInit();
            this.grp_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_ngaytao_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_ngaytao_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_macs_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idexport_I1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_footer_C
            // 
            this.grp_footer_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grp_footer_C.Controls.Add(this.btn_insert_allow_insert);
            this.grp_footer_C.Controls.Add(this.bbi_allow_insert);
            this.grp_footer_C.Controls.Add(this.btn_exit_S);
            this.grp_footer_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_footer_C.Location = new System.Drawing.Point(0, 213);
            this.grp_footer_C.Name = "grp_footer_C";
            this.grp_footer_C.Size = new System.Drawing.Size(595, 39);
            this.grp_footer_C.TabIndex = 0;
            // 
            // btn_insert_allow_insert
            // 
            this.btn_insert_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_insert_allow_insert.Image = global::SOURCE_FORM_QUOTATION.Properties.Resources.save_16x16;
            this.btn_insert_allow_insert.Location = new System.Drawing.Point(271, 3);
            this.btn_insert_allow_insert.Name = "btn_insert_allow_insert";
            this.btn_insert_allow_insert.Size = new System.Drawing.Size(117, 31);
            this.btn_insert_allow_insert.TabIndex = 6;
            this.btn_insert_allow_insert.Text = "F4 Lưu - Thêm";
            this.btn_insert_allow_insert.Visible = false;
            this.btn_insert_allow_insert.Click += new System.EventHandler(this.btn_insert_allow_insert_Click);
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbi_allow_insert.Image = global::SOURCE_FORM_QUOTATION.Properties.Resources.save_16x16;
            this.bbi_allow_insert.Location = new System.Drawing.Point(389, 3);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(120, 31);
            this.bbi_allow_insert.TabIndex = 4;
            this.bbi_allow_insert.Text = "F5 Lưu - Thoát";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit_S.Image = global::SOURCE_FORM_QUOTATION.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(510, 3);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 5;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // grp_main_C
            // 
            this.grp_main_C.Controls.Add(this.groupControl2);
            this.grp_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_main_C.Location = new System.Drawing.Point(0, 0);
            this.grp_main_C.Name = "grp_main_C";
            this.grp_main_C.Size = new System.Drawing.Size(595, 213);
            this.grp_main_C.TabIndex = 1;
            this.grp_main_C.Text = "Thông tin";
            // 
            // groupControl2
            // 
            this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.dte_ngaytao_I5);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.txt_idexport_I1);
            this.groupControl2.Controls.Add(this.txt_macs_IK1);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.mmo_note_I3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 21);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(591, 190);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "groupControl2";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl2.Location = new System.Drawing.Point(5, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Nội dung(*):";
            // 
            // dte_ngaytao_I5
            // 
            this.dte_ngaytao_I5.EditValue = null;
            this.dte_ngaytao_I5.Enabled = false;
            this.dte_ngaytao_I5.Location = new System.Drawing.Point(78, 40);
            this.dte_ngaytao_I5.Name = "dte_ngaytao_I5";
            this.dte_ngaytao_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_ngaytao_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_ngaytao_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_ngaytao_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_ngaytao_I5.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dte_ngaytao_I5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_ngaytao_I5.Size = new System.Drawing.Size(113, 20);
            this.dte_ngaytao_I5.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 43);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "Ngày tạo:";
            // 
            // txt_macs_IK1
            // 
            this.txt_macs_IK1.Enabled = false;
            this.txt_macs_IK1.Location = new System.Drawing.Point(78, 17);
            this.txt_macs_IK1.Name = "txt_macs_IK1";
            this.txt_macs_IK1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_macs_IK1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_macs_IK1.Properties.MaxLength = 20;
            this.txt_macs_IK1.Size = new System.Drawing.Size(113, 20);
            this.txt_macs_IK1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl1.Location = new System.Drawing.Point(5, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã CS(*):";
            // 
            // mmo_note_I3
            // 
            this.mmo_note_I3.Location = new System.Drawing.Point(78, 66);
            this.mmo_note_I3.Name = "mmo_note_I3";
            this.mmo_note_I3.Properties.MaxLength = 500;
            this.mmo_note_I3.Size = new System.Drawing.Size(505, 112);
            this.mmo_note_I3.TabIndex = 3;
            this.mmo_note_I3.UseOptimizedRendering = true;
            this.mmo_note_I3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // dxEp_error_S
            // 
            this.dxEp_error_S.ContainerControl = this;
            // 
            // txt_idexport_I1
            // 
            this.txt_idexport_I1.Enabled = false;
            this.txt_idexport_I1.Location = new System.Drawing.Point(197, 17);
            this.txt_idexport_I1.Name = "txt_idexport_I1";
            this.txt_idexport_I1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_idexport_I1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_idexport_I1.Properties.MaxLength = 20;
            this.txt_idexport_I1.Size = new System.Drawing.Size(113, 20);
            this.txt_idexport_I1.TabIndex = 0;
            this.txt_idexport_I1.Visible = false;
            // 
            // frm_CSBG_S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 252);
            this.ControlBox = false;
            this.Controls.Add(this.grp_main_C);
            this.Controls.Add(this.grp_footer_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_CSBG_S";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CHĂM SÓC BÁO GIÁ ::.";
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
            ((System.ComponentModel.ISupportInitialize)(this.dte_ngaytao_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_ngaytao_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_macs_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_note_I3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_idexport_I1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_footer_C;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.GroupControl grp_main_C;
        private DevExpress.XtraEditors.MemoEdit mmo_note_I3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxEp_error_S;
        private DevExpress.XtraEditors.SimpleButton btn_insert_allow_insert;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.DateEdit dte_ngaytao_I5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txt_macs_IK1;
        public DevExpress.XtraEditors.TextEdit txt_idexport_I1;
    }
}