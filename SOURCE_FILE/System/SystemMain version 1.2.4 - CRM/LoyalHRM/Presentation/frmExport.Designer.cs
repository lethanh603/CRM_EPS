namespace LoyalHRM.Presentation
{
    partial class frmExport
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
            this.btn_nhap_S = new DevExpress.XtraEditors.SimpleButton();
            this.lue_bang_S = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_xuat_S = new DevExpress.XtraEditors.SimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.lue_bang_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_nhap_S
            // 
            this.btn_nhap_S.Location = new System.Drawing.Point(126, 48);
            this.btn_nhap_S.Name = "btn_nhap_S";
            this.btn_nhap_S.Size = new System.Drawing.Size(75, 23);
            this.btn_nhap_S.TabIndex = 0;
            this.btn_nhap_S.Text = "Nhập";
            this.btn_nhap_S.Click += new System.EventHandler(this.btn_nhap_S_Click);
            // 
            // lue_bang_S
            // 
            this.lue_bang_S.Location = new System.Drawing.Point(126, 22);
            this.lue_bang_S.Name = "lue_bang_S";
            this.lue_bang_S.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_bang_S.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("table", "Tên bảng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Name3", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lue_bang_S.Size = new System.Drawing.Size(202, 20);
            this.lue_bang_S.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Chọn bảng:";
            // 
            // btn_xuat_S
            // 
            this.btn_xuat_S.Location = new System.Drawing.Point(253, 48);
            this.btn_xuat_S.Name = "btn_xuat_S";
            this.btn_xuat_S.Size = new System.Drawing.Size(75, 23);
            this.btn_xuat_S.TabIndex = 0;
            this.btn_xuat_S.Text = "Xuất";
            this.btn_xuat_S.Click += new System.EventHandler(this.btn_xuat_S_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 81);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_xuat_S);
            this.Controls.Add(this.lue_bang_S);
            this.Controls.Add(this.btn_nhap_S);
            this.Name = "frmExport";
            this.Text = "Export and Import";
            this.Load += new System.EventHandler(this.frmExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lue_bang_S.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_nhap_S;
        private DevExpress.XtraEditors.LookUpEdit lue_bang_S;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_xuat_S;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}