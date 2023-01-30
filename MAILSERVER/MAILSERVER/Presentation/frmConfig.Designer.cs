namespace DMNHANVIENKH.Presentation
{
    partial class frmConfig
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
            this.txt_smtp_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_mailEddress_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_username_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_pass_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save_S = new DevExpress.XtraEditors.SimpleButton();
            this.txt_port_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_smtp_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailEddress_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_pass_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_smtp_I2
            // 
            this.txt_smtp_I2.Location = new System.Drawing.Point(98, 10);
            this.txt_smtp_I2.Name = "txt_smtp_I2";
            this.txt_smtp_I2.Size = new System.Drawing.Size(271, 20);
            this.txt_smtp_I2.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(55, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "SmtpClient:";
            // 
            // txt_mailEddress_I2
            // 
            this.txt_mailEddress_I2.Location = new System.Drawing.Point(98, 32);
            this.txt_mailEddress_I2.Name = "txt_mailEddress_I2";
            this.txt_mailEddress_I2.Size = new System.Drawing.Size(271, 20);
            this.txt_mailEddress_I2.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "MailAddress:";
            // 
            // txt_username_I2
            // 
            this.txt_username_I2.Location = new System.Drawing.Point(98, 54);
            this.txt_username_I2.Name = "txt_username_I2";
            this.txt_username_I2.Size = new System.Drawing.Size(271, 20);
            this.txt_username_I2.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Email:";
            // 
            // txt_pass_I2
            // 
            this.txt_pass_I2.Location = new System.Drawing.Point(98, 76);
            this.txt_pass_I2.Name = "txt_pass_I2";
            this.txt_pass_I2.Properties.PasswordChar = '*';
            this.txt_pass_I2.Size = new System.Drawing.Size(271, 20);
            this.txt_pass_I2.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 79);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Password:";
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = global::DMNHANVIENKH.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(213, 122);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(75, 23);
            this.btn_exit_S.TabIndex = 5;
            this.btn_exit_S.Text = "Exit";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // btn_Save_S
            // 
            this.btn_Save_S.Image = global::DMNHANVIENKH.Properties.Resources.bbi_luu_S;
            this.btn_Save_S.Location = new System.Drawing.Point(294, 122);
            this.btn_Save_S.Name = "btn_Save_S";
            this.btn_Save_S.Size = new System.Drawing.Size(75, 23);
            this.btn_Save_S.TabIndex = 6;
            this.btn_Save_S.Text = "Save";
            this.btn_Save_S.Click += new System.EventHandler(this.btn_Save_S_Click);
            // 
            // txt_port_S
            // 
            this.txt_port_S.Location = new System.Drawing.Point(98, 98);
            this.txt_port_S.Name = "txt_port_S";
            this.txt_port_S.Properties.DisplayFormat.FormatString = "d";
            this.txt_port_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txt_port_S.Size = new System.Drawing.Size(271, 20);
            this.txt_port_S.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(13, 101);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "Port:";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 149);
            this.Controls.Add(this.btn_exit_S);
            this.Controls.Add(this.btn_Save_S);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_pass_I2);
            this.Controls.Add(this.txt_port_S);
            this.Controls.Add(this.txt_username_I2);
            this.Controls.Add(this.txt_mailEddress_I2);
            this.Controls.Add(this.txt_smtp_I2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmConfig";
            this.Text = ".:: Config Mail ::.";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_smtp_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailEddress_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_pass_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port_S.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txt_smtp_I2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_mailEddress_I2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_username_I2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_pass_I2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btn_Save_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.TextEdit txt_port_S;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}