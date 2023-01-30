namespace LoyalHRM.Presentation
{
    partial class frmDoiMatKhau
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
            this.pn_main_C = new DevExpress.XtraEditors.PanelControl();
            this.btn_insert_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.txt_reminder_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_matkhaumoi_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_matkhau_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_tendangnhap_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_matkhaucu_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pn_main_C)).BeginInit();
            this.pn_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_reminder_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhaumoi_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhau_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tendangnhap_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhaucu_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_main_C
            // 
            this.pn_main_C.Controls.Add(this.btn_insert_S);
            this.pn_main_C.Controls.Add(this.btn_exit_S);
            this.pn_main_C.Controls.Add(this.txt_reminder_I2);
            this.pn_main_C.Controls.Add(this.labelControl5);
            this.pn_main_C.Controls.Add(this.txt_matkhaumoi_S);
            this.pn_main_C.Controls.Add(this.labelControl3);
            this.pn_main_C.Controls.Add(this.txt_matkhau_S);
            this.pn_main_C.Controls.Add(this.labelControl2);
            this.pn_main_C.Controls.Add(this.txt_tendangnhap_I2);
            this.pn_main_C.Controls.Add(this.labelControl4);
            this.pn_main_C.Controls.Add(this.txt_matkhaucu_S);
            this.pn_main_C.Controls.Add(this.labelControl1);
            this.pn_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_main_C.Location = new System.Drawing.Point(0, 0);
            this.pn_main_C.Name = "pn_main_C";
            this.pn_main_C.Size = new System.Drawing.Size(304, 157);
            this.pn_main_C.TabIndex = 0;
            // 
            // btn_insert_S
            // 
            this.btn_insert_S.Image = global::LoyalHRM.Properties.Resources.bbi_luu_S;
            this.btn_insert_S.Location = new System.Drawing.Point(124, 122);
            this.btn_insert_S.Name = "btn_insert_S";
            this.btn_insert_S.Size = new System.Drawing.Size(80, 27);
            this.btn_insert_S.TabIndex = 5;
            this.btn_insert_S.Text = "&F1 Lưu";
            this.btn_insert_S.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = global::LoyalHRM.Properties.Resources.bbi_thoat_S;
            this.btn_exit_S.Location = new System.Drawing.Point(210, 122);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(85, 27);
            this.btn_exit_S.TabIndex = 6;
            this.btn_exit_S.Text = "&F2 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // txt_reminder_I2
            // 
            this.txt_reminder_I2.Location = new System.Drawing.Point(124, 96);
            this.txt_reminder_I2.Name = "txt_reminder_I2";
            this.txt_reminder_I2.Size = new System.Drawing.Size(171, 20);
            this.txt_reminder_I2.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 96);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Từ gợi nhớ:";
            // 
            // txt_matkhaumoi_S
            // 
            this.txt_matkhaumoi_S.Location = new System.Drawing.Point(124, 74);
            this.txt_matkhaumoi_S.Name = "txt_matkhaumoi_S";
            this.txt_matkhaumoi_S.Properties.PasswordChar = '*';
            this.txt_matkhaumoi_S.Size = new System.Drawing.Size(171, 20);
            this.txt_matkhaumoi_S.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Lặp lại :";
            // 
            // txt_matkhau_S
            // 
            this.txt_matkhau_S.Location = new System.Drawing.Point(124, 52);
            this.txt_matkhau_S.Name = "txt_matkhau_S";
            this.txt_matkhau_S.Properties.PasswordChar = '*';
            this.txt_matkhau_S.Size = new System.Drawing.Size(171, 20);
            this.txt_matkhau_S.TabIndex = 2;
            this.txt_matkhau_S.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_matkhau_S_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Mật khẩu mới:";
            // 
            // txt_tendangnhap_I2
            // 
            this.txt_tendangnhap_I2.Location = new System.Drawing.Point(124, 8);
            this.txt_tendangnhap_I2.Name = "txt_tendangnhap_I2";
            this.txt_tendangnhap_I2.Size = new System.Drawing.Size(171, 20);
            this.txt_tendangnhap_I2.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(76, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Tên đăng nhập:";
            // 
            // txt_matkhaucu_S
            // 
            this.txt_matkhaucu_S.Location = new System.Drawing.Point(124, 30);
            this.txt_matkhaucu_S.Name = "txt_matkhaucu_S";
            this.txt_matkhaucu_S.Properties.PasswordChar = '*';
            this.txt_matkhaucu_S.Size = new System.Drawing.Size(171, 20);
            this.txt_matkhaucu_S.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Nhập mật khẩu:";
            // 
            // frmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 157);
            this.Controls.Add(this.pn_main_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: ĐỖI MẬT KHẨU ::.";
            this.Load += new System.EventHandler(this.frmDoiMatKhau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pn_main_C)).EndInit();
            this.pn_main_C.ResumeLayout(false);
            this.pn_main_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_reminder_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhaumoi_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhau_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tendangnhap_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_matkhaucu_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pn_main_C;
        private DevExpress.XtraEditors.SimpleButton btn_insert_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.TextEdit txt_reminder_I2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txt_matkhaumoi_S;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_matkhau_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_tendangnhap_I2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_matkhaucu_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}