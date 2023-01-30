namespace LoyalHRM.Presentation
{
    partial class frm_liences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_liences));
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.txt_madangky_S = new DevExpress.XtraEditors.TextEdit();
            this.txt_masudung_S = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btn_register_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_createcode_S = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_madangky_S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_masudung_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(350, 65);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(83, 32);
            this.btn_exit_S.TabIndex = 0;
            this.btn_exit_S.Text = "Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // txt_madangky_S
            // 
            this.txt_madangky_S.Location = new System.Drawing.Point(90, 13);
            this.txt_madangky_S.Name = "txt_madangky_S";
            this.txt_madangky_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_madangky_S.Properties.Appearance.Options.UseFont = true;
            this.txt_madangky_S.Properties.ReadOnly = true;
            this.txt_madangky_S.Size = new System.Drawing.Size(343, 20);
            this.txt_madangky_S.TabIndex = 1;
            // 
            // txt_masudung_S
            // 
            this.txt_masudung_S.Location = new System.Drawing.Point(90, 39);
            this.txt_masudung_S.Name = "txt_masudung_S";
            this.txt_masudung_S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_masudung_S.Properties.Appearance.Options.UseFont = true;
            this.txt_masudung_S.Properties.MaxLength = 200;
            this.txt_masudung_S.Size = new System.Drawing.Size(343, 20);
            this.txt_masudung_S.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Mã đăng ký:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Mã sử dụng";
            // 
            // btn_register_S
            // 
            this.btn_register_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_register_S.Image")));
            this.btn_register_S.Location = new System.Drawing.Point(261, 65);
            this.btn_register_S.Name = "btn_register_S";
            this.btn_register_S.Size = new System.Drawing.Size(83, 32);
            this.btn_register_S.TabIndex = 0;
            this.btn_register_S.Text = "Đăng ký";
            this.btn_register_S.Click += new System.EventHandler(this.btn_register_S_Click);
            // 
            // btn_createcode_S
            // 
            this.btn_createcode_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_createcode_S.Image")));
            this.btn_createcode_S.Location = new System.Drawing.Point(172, 65);
            this.btn_createcode_S.Name = "btn_createcode_S";
            this.btn_createcode_S.Size = new System.Drawing.Size(83, 32);
            this.btn_createcode_S.TabIndex = 0;
            this.btn_createcode_S.Text = "Tạo mã";
            this.btn_createcode_S.Click += new System.EventHandler(this.btn_createcode_S_Click);
            // 
            // frm_liences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 101);
            this.ControlBox = false;
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_masudung_S);
            this.Controls.Add(this.txt_madangky_S);
            this.Controls.Add(this.btn_createcode_S);
            this.Controls.Add(this.btn_register_S);
            this.Controls.Add(this.btn_exit_S);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_liences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký sử dụng";
            this.Load += new System.EventHandler(this.frm_liences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_madangky_S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_masudung_S.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.TextEdit txt_madangky_S;
        private DevExpress.XtraEditors.TextEdit txt_masudung_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_register_S;
        private DevExpress.XtraEditors.SimpleButton btn_createcode_S;
    }
}