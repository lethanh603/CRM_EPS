namespace SOURCE_FORM_RETAIL.Presentation
{
    partial class frm_Reason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Reason));
            this.btn_accept_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.mmo_reason_S = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.mmo_reason_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_accept_S
            // 
            this.btn_accept_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_accept_S.Image")));
            this.btn_accept_S.Location = new System.Drawing.Point(201, 82);
            this.btn_accept_S.Name = "btn_accept_S";
            this.btn_accept_S.Size = new System.Drawing.Size(96, 36);
            this.btn_accept_S.TabIndex = 8;
            this.btn_accept_S.Text = "Đồng ý";
            this.btn_accept_S.Click += new System.EventHandler(this.btn_accept_S_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(303, 82);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(96, 36);
            this.btn_exit_S.TabIndex = 9;
            this.btn_exit_S.Text = "Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // mmo_reason_S
            // 
            this.mmo_reason_S.EditValue = "Nhập lý do cho hành động";
            this.mmo_reason_S.Location = new System.Drawing.Point(3, 3);
            this.mmo_reason_S.Name = "mmo_reason_S";
            this.mmo_reason_S.Properties.MaxLength = 200;
            this.mmo_reason_S.Size = new System.Drawing.Size(396, 75);
            this.mmo_reason_S.TabIndex = 10;
            this.mmo_reason_S.UseOptimizedRendering = true;
            this.mmo_reason_S.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mmo_reason_S_KeyDown);
            // 
            // frm_Reason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 122);
            this.Controls.Add(this.mmo_reason_S);
            this.Controls.Add(this.btn_accept_S);
            this.Controls.Add(this.btn_exit_S);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Reason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: LÝ DO ::.";
            this.Load += new System.EventHandler(this.frm_Reason_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mmo_reason_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_accept_S;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.MemoEdit mmo_reason_S;
    }
}