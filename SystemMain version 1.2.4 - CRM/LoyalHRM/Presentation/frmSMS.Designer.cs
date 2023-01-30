namespace LoyalHRM.Presentation
{
    partial class frmSMS
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
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.btn_luu_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_thoat_S = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(1, 1);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(437, 96);
            this.memoEdit1.TabIndex = 1;
            this.memoEdit1.UseOptimizedRendering = true;
            // 
            // btn_luu_S
            // 
            this.btn_luu_S.Image = global::LoyalHRM.Properties.Resources.bbi_luu_S;
            this.btn_luu_S.Location = new System.Drawing.Point(282, 103);
            this.btn_luu_S.Name = "btn_luu_S";
            this.btn_luu_S.Size = new System.Drawing.Size(75, 23);
            this.btn_luu_S.TabIndex = 0;
            this.btn_luu_S.Text = "Lưu ";
            this.btn_luu_S.Click += new System.EventHandler(this.btn_luu_S_Click);
            // 
            // btn_thoat_S
            // 
            this.btn_thoat_S.Image = global::LoyalHRM.Properties.Resources.bbi_thoat_S;
            this.btn_thoat_S.Location = new System.Drawing.Point(363, 103);
            this.btn_thoat_S.Name = "btn_thoat_S";
            this.btn_thoat_S.Size = new System.Drawing.Size(75, 23);
            this.btn_thoat_S.TabIndex = 0;
            this.btn_thoat_S.Text = "Thoát";
            this.btn_thoat_S.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 129);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.btn_luu_S);
            this.Controls.Add(this.btn_thoat_S);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSMS";
            this.Text = ".:: NHỮNG THÔNG BÁO ĐẾN NHÂN VIÊN ::.";
            this.Load += new System.EventHandler(this.frmSMS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_thoat_S;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.SimpleButton btn_luu_S;
    }
}