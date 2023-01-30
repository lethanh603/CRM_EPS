namespace SOURCE_FORM_RETAIL.Presentation
{
    partial class frm_configBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_configBill));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gct_top_C = new DevExpress.XtraEditors.GroupControl();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.gct_bottom_C = new DevExpress.XtraEditors.GroupControl();
            this.chk_isNight_I6 = new DevExpress.XtraEditors.CheckEdit();
            this.tm_start_I2 = new DevExpress.XtraEditors.TextEdit();
            this.tm_end_I2 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gct_top_C)).BeginInit();
            this.gct_top_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gct_bottom_C)).BeginInit();
            this.gct_bottom_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_isNight_I6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tm_start_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tm_end_I2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(129, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Giờ bắt đầu phiên làm việc:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(134, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Giờ kết thúc phiên làm việc :";
            // 
            // gct_top_C
            // 
            this.gct_top_C.Controls.Add(this.bbi_allow_insert);
            this.gct_top_C.Controls.Add(this.btn_exit_S);
            this.gct_top_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gct_top_C.Location = new System.Drawing.Point(0, 97);
            this.gct_top_C.Name = "gct_top_C";
            this.gct_top_C.ShowCaption = false;
            this.gct_top_C.Size = new System.Drawing.Size(281, 46);
            this.gct_top_C.TabIndex = 2;
            this.gct_top_C.Text = "groupControl1";
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Image = ((System.Drawing.Image)(resources.GetObject("bbi_allow_insert.Image")));
            this.bbi_allow_insert.Location = new System.Drawing.Point(95, 6);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(98, 35);
            this.bbi_allow_insert.TabIndex = 12;
            this.bbi_allow_insert.Text = "F5 Lưu";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(194, 6);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 35);
            this.btn_exit_S.TabIndex = 13;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // gct_bottom_C
            // 
            this.gct_bottom_C.Controls.Add(this.tm_end_I2);
            this.gct_bottom_C.Controls.Add(this.tm_start_I2);
            this.gct_bottom_C.Controls.Add(this.chk_isNight_I6);
            this.gct_bottom_C.Controls.Add(this.labelControl2);
            this.gct_bottom_C.Controls.Add(this.labelControl1);
            this.gct_bottom_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gct_bottom_C.Location = new System.Drawing.Point(0, 0);
            this.gct_bottom_C.Name = "gct_bottom_C";
            this.gct_bottom_C.ShowCaption = false;
            this.gct_bottom_C.Size = new System.Drawing.Size(281, 97);
            this.gct_bottom_C.TabIndex = 3;
            // 
            // chk_isNight_I6
            // 
            this.chk_isNight_I6.EditValue = true;
            this.chk_isNight_I6.Location = new System.Drawing.Point(157, 64);
            this.chk_isNight_I6.Name = "chk_isNight_I6";
            this.chk_isNight_I6.Properties.Caption = "Qua đêm";
            this.chk_isNight_I6.Size = new System.Drawing.Size(75, 19);
            this.chk_isNight_I6.TabIndex = 2;
            // 
            // tm_start_I2
            // 
            this.tm_start_I2.EditValue = "00:00";
            this.tm_start_I2.Location = new System.Drawing.Point(159, 15);
            this.tm_start_I2.Name = "tm_start_I2";
            this.tm_start_I2.Properties.DisplayFormat.FormatString = "HH:mm";
            this.tm_start_I2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tm_start_I2.Properties.EditFormat.FormatString = "HH:mm";
            this.tm_start_I2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tm_start_I2.Properties.Mask.EditMask = "HH:mm";
            this.tm_start_I2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.tm_start_I2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.tm_start_I2.Size = new System.Drawing.Size(41, 20);
            this.tm_start_I2.TabIndex = 3;
            // 
            // tm_end_I2
            // 
            this.tm_end_I2.EditValue = "00:00";
            this.tm_end_I2.Location = new System.Drawing.Point(159, 37);
            this.tm_end_I2.Name = "tm_end_I2";
            this.tm_end_I2.Properties.DisplayFormat.FormatString = "HH:mm";
            this.tm_end_I2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tm_end_I2.Properties.EditFormat.FormatString = "HH:mm";
            this.tm_end_I2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tm_end_I2.Properties.Mask.EditMask = "HH:mm";
            this.tm_end_I2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.tm_end_I2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.tm_end_I2.Size = new System.Drawing.Size(41, 20);
            this.tm_end_I2.TabIndex = 3;
            // 
            // frm_configBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 143);
            this.ControlBox = false;
            this.Controls.Add(this.gct_bottom_C);
            this.Controls.Add(this.gct_top_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_configBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CẤU HÌNH ::.";
            this.Load += new System.EventHandler(this.frm_configBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gct_top_C)).EndInit();
            this.gct_top_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gct_bottom_C)).EndInit();
            this.gct_bottom_C.ResumeLayout(false);
            this.gct_bottom_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_isNight_I6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tm_start_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tm_end_I2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl gct_top_C;
        private DevExpress.XtraEditors.GroupControl gct_bottom_C;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.CheckEdit chk_isNight_I6;
        private DevExpress.XtraEditors.TextEdit tm_end_I2;
        private DevExpress.XtraEditors.TextEdit tm_start_I2;
    }
}