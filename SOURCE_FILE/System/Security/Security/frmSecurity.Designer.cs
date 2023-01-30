namespace SECURITY
{
    partial class frmSecurity
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
            this.btn_xacnhan_S = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_maxacnhan_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_thoihan_S = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_maxacnhan_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_thoihan_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_xacnhan_S
            // 
            this.btn_xacnhan_S.Location = new System.Drawing.Point(189, 73);
            this.btn_xacnhan_S.Name = "btn_xacnhan_S";
            this.btn_xacnhan_S.Size = new System.Drawing.Size(75, 23);
            this.btn_xacnhan_S.TabIndex = 0;
            this.btn_xacnhan_S.Text = "Xác nhận";
            this.btn_xacnhan_S.Click += new System.EventHandler(this.btn_xacnhan_S_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Mã xác nhận:";
            // 
            // txt_maxacnhan_S
            // 
            this.txt_maxacnhan_S.Location = new System.Drawing.Point(92, 21);
            this.txt_maxacnhan_S.Name = "txt_maxacnhan_S";
            this.txt_maxacnhan_S.Size = new System.Drawing.Size(172, 20);
            this.txt_maxacnhan_S.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 50);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Thời hạn:";
            // 
            // txt_thoihan_S
            // 
            this.txt_thoihan_S.EditValue = "30";
            this.txt_thoihan_S.Location = new System.Drawing.Point(92, 47);
            this.txt_thoihan_S.Name = "txt_thoihan_S";
            this.txt_thoihan_S.Properties.DisplayFormat.FormatString = "d";
            this.txt_thoihan_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txt_thoihan_S.Size = new System.Drawing.Size(172, 20);
            this.txt_thoihan_S.TabIndex = 2;
            // 
            // frmSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 100);
            this.Controls.Add(this.txt_thoihan_S);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_maxacnhan_S);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_xacnhan_S);
            this.Name = "frmSecurity";
            this.Text = "SECURITY";
            ((System.ComponentModel.ISupportInitialize)(this.txt_maxacnhan_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_thoihan_S.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_xacnhan_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_maxacnhan_S;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_thoihan_S;
    }
}