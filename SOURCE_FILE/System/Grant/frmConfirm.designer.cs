namespace SOURCE_FORM.Presentation
{
    partial class frmConfirm
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
            this.txt_password_S = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_password_S
            // 
            this.txt_password_S.Location = new System.Drawing.Point(13, 10);
            this.txt_password_S.Name = "txt_password_S";
            this.txt_password_S.Properties.PasswordChar = '*';
            this.txt_password_S.Size = new System.Drawing.Size(198, 20);
            this.txt_password_S.TabIndex = 0;
            this.txt_password_S.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_password_S_KeyDown);
          
            // 
            // frmXacNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 34);
            this.Controls.Add(this.txt_password_S);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmXacNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: XÁC NHẬN MẬT KHẨU::.";
            this.Load += new System.EventHandler(this.frmXacNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_password_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txt_password_S;
    }
}