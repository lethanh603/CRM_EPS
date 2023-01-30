namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    partial class frmChiTietGiaohang
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
            this.btn_cancel_detail = new DevExpress.XtraEditors.SimpleButton();
            this.btn_detail_insert = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dte_denngay_I5 = new DevExpress.XtraEditors.DateEdit();
            this.dte_tungay_I5 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.soluong = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_denngay_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_denngay_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_tungay_I5.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_tungay_I5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cancel_detail
            // 
            this.btn_cancel_detail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel_detail.Image = global::SOURCE_FORM_TIENDODONHANG.Properties.Resources.bbi_xoa_S;
            this.btn_cancel_detail.Location = new System.Drawing.Point(114, 5);
            this.btn_cancel_detail.Name = "btn_cancel_detail";
            this.btn_cancel_detail.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel_detail.TabIndex = 18;
            this.btn_cancel_detail.Text = "Hủy";
            this.btn_cancel_detail.Click += new System.EventHandler(this.btn_cancel_detail_Click);
            // 
            // btn_detail_insert
            // 
            this.btn_detail_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_detail_insert.Image = global::SOURCE_FORM_TIENDODONHANG.Properties.Resources.add;
            this.btn_detail_insert.Location = new System.Drawing.Point(33, 5);
            this.btn_detail_insert.Name = "btn_detail_insert";
            this.btn_detail_insert.Size = new System.Drawing.Size(75, 23);
            this.btn_detail_insert.TabIndex = 17;
            this.btn_detail_insert.Text = "Lưu";
            this.btn_detail_insert.Click += new System.EventHandler(this.btn_detail_insert_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_cancel_detail);
            this.panelControl1.Controls.Add(this.btn_detail_insert);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 95);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(194, 33);
            this.panelControl1.TabIndex = 19;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.dte_denngay_I5);
            this.panelControl2.Controls.Add(this.dte_tungay_I5);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.soluong);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(194, 95);
            this.panelControl2.TabIndex = 20;
            // 
            // dte_denngay_I5
            // 
            this.dte_denngay_I5.EditValue = null;
            this.dte_denngay_I5.Location = new System.Drawing.Point(92, 38);
            this.dte_denngay_I5.Name = "dte_denngay_I5";
            this.dte_denngay_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_denngay_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_denngay_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_denngay_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_denngay_I5.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dte_denngay_I5.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dte_denngay_I5.Size = new System.Drawing.Size(85, 20);
            this.dte_denngay_I5.TabIndex = 3;
            // 
            // dte_tungay_I5
            // 
            this.dte_tungay_I5.EditValue = null;
            this.dte_tungay_I5.Location = new System.Drawing.Point(92, 12);
            this.dte_tungay_I5.Name = "dte_tungay_I5";
            this.dte_tungay_I5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_tungay_I5.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dte_tungay_I5.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dte_tungay_I5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dte_tungay_I5.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dte_tungay_I5.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dte_tungay_I5.Size = new System.Drawing.Size(85, 20);
            this.dte_tungay_I5.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Số lượng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // soluong
            // 
            this.soluong.Location = new System.Drawing.Point(92, 64);
            this.soluong.Name = "soluong";
            this.soluong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.soluong.Size = new System.Drawing.Size(85, 20);
            this.soluong.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // frmChiTietGiaohang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 128);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmChiTietGiaohang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: CHI TIẾT GIAO HÀNG ::.";
            this.Load += new System.EventHandler(this.frmChiTietGiaohang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dte_denngay_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_denngay_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_tungay_I5.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dte_tungay_I5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluong.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_cancel_detail;
        private DevExpress.XtraEditors.SimpleButton btn_detail_insert;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CalcEdit soluong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dte_denngay_I5;
        private DevExpress.XtraEditors.DateEdit dte_tungay_I5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}