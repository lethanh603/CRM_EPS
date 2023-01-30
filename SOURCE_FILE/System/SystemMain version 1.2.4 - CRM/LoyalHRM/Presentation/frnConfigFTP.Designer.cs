namespace LoyalHRM.Presentation
{
    partial class frnConfigFTP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frnConfigFTP));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_address_I2 = new DevExpress.XtraEditors.TextEdit();
            this.txt_account_I2 = new DevExpress.XtraEditors.TextEdit();
            this.txt_password_I2 = new DevExpress.XtraEditors.TextEdit();
            this.txt_path_I2 = new DevExpress.XtraEditors.TextEdit();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save_S = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_address_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_account_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_path_I2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Địa chỉ FTP:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Tài khoản:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 54);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(79, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Đường dẫn tạm:";
            // 
            // txt_address_I2
            // 
            this.txt_address_I2.Location = new System.Drawing.Point(121, 9);
            this.txt_address_I2.Name = "txt_address_I2";
            this.txt_address_I2.Properties.MaxLength = 100;
            this.txt_address_I2.Size = new System.Drawing.Size(218, 20);
            this.txt_address_I2.TabIndex = 0;
            // 
            // txt_account_I2
            // 
            this.txt_account_I2.Location = new System.Drawing.Point(121, 30);
            this.txt_account_I2.Name = "txt_account_I2";
            this.txt_account_I2.Properties.MaxLength = 100;
            this.txt_account_I2.Size = new System.Drawing.Size(218, 20);
            this.txt_account_I2.TabIndex = 1;
            // 
            // txt_password_I2
            // 
            this.txt_password_I2.Location = new System.Drawing.Point(121, 51);
            this.txt_password_I2.Name = "txt_password_I2";
            this.txt_password_I2.Properties.MaxLength = 100;
            this.txt_password_I2.Properties.PasswordChar = '*';
            this.txt_password_I2.Size = new System.Drawing.Size(218, 20);
            this.txt_password_I2.TabIndex = 2;
            // 
            // txt_path_I2
            // 
            this.txt_path_I2.Location = new System.Drawing.Point(121, 72);
            this.txt_path_I2.Name = "txt_path_I2";
            this.txt_path_I2.Properties.MaxLength = 100;
            this.txt_path_I2.Size = new System.Drawing.Size(218, 20);
            this.txt_path_I2.TabIndex = 3;
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(236, 98);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(103, 33);
            this.btn_exit_S.TabIndex = 5;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // btn_save_S
            // 
            this.btn_save_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_save_S.Image")));
            this.btn_save_S.Location = new System.Drawing.Point(121, 98);
            this.btn_save_S.Name = "btn_save_S";
            this.btn_save_S.Size = new System.Drawing.Size(103, 33);
            this.btn_save_S.TabIndex = 4;
            this.btn_save_S.Text = "F5 Lưu";
            this.btn_save_S.ToolTip = "Lưu";
            this.btn_save_S.Click += new System.EventHandler(this.btn_save_S_Click);
            // 
            // frnConfigFTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 133);
            this.Controls.Add(this.btn_save_S);
            this.Controls.Add(this.btn_exit_S);
            this.Controls.Add(this.txt_path_I2);
            this.Controls.Add(this.txt_password_I2);
            this.Controls.Add(this.txt_account_I2);
            this.Controls.Add(this.txt_address_I2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frnConfigFTP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CẤU HÌNH FTP SERVER ::.";
            this.Load += new System.EventHandler(this.frnConfigFTP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frnConfigFTP_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txt_address_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_account_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_path_I2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_address_I2;
        private DevExpress.XtraEditors.TextEdit txt_account_I2;
        private DevExpress.XtraEditors.TextEdit txt_password_I2;
        private DevExpress.XtraEditors.TextEdit txt_path_I2;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.SimpleButton btn_save_S;
    }
}