namespace SOURCE_FORM_DMNHANVIEN.Presentation
{
    partial class frm_BOPHAN_S
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BOPHAN_S));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.grp_footer_C = new DevExpress.XtraEditors.GroupControl();
            this.btn_insert_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.bbi_allow_insert = new DevExpress.XtraEditors.SimpleButton();
            this.btn_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.grp_main_C = new DevExpress.XtraEditors.GroupControl();
            this.grop_Main_C = new DevExpress.XtraEditors.GroupControl();
            this.glue_BPID_I1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txt_NVID_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_MANV_20_I1 = new DevExpress.XtraEditors.TextEdit();
            this.txt_CHUCVU_200_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txt_TENNV_100_I2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dxEp_error_S = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).BeginInit();
            this.grp_footer_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).BeginInit();
            this.grp_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grop_Main_C)).BeginInit();
            this.grop_Main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_BPID_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NVID_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_MANV_20_I1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CHUCVU_200_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_TENNV_100_I2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_footer_C
            // 
            this.grp_footer_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grp_footer_C.Controls.Add(this.btn_insert_allow_insert);
            this.grp_footer_C.Controls.Add(this.bbi_allow_insert);
            this.grp_footer_C.Controls.Add(this.btn_exit_S);
            this.grp_footer_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_footer_C.Location = new System.Drawing.Point(0, 171);
            this.grp_footer_C.Name = "grp_footer_C";
            this.grp_footer_C.Size = new System.Drawing.Size(382, 39);
            this.grp_footer_C.TabIndex = 0;
            // 
            // btn_insert_allow_insert
            // 
            this.btn_insert_allow_insert.Image = ((System.Drawing.Image)(resources.GetObject("btn_insert_allow_insert.Image")));
            this.btn_insert_allow_insert.Location = new System.Drawing.Point(51, 4);
            this.btn_insert_allow_insert.Name = "btn_insert_allow_insert";
            this.btn_insert_allow_insert.Size = new System.Drawing.Size(117, 31);
            this.btn_insert_allow_insert.TabIndex = 20;
            this.btn_insert_allow_insert.Text = "F4 Lưu - Thêm";
            this.btn_insert_allow_insert.Click += new System.EventHandler(this.btn_insert_allow_insert_Click);
            // 
            // bbi_allow_insert
            // 
            this.bbi_allow_insert.Image = ((System.Drawing.Image)(resources.GetObject("bbi_allow_insert.Image")));
            this.bbi_allow_insert.Location = new System.Drawing.Point(169, 4);
            this.bbi_allow_insert.Name = "bbi_allow_insert";
            this.bbi_allow_insert.Size = new System.Drawing.Size(120, 31);
            this.bbi_allow_insert.TabIndex = 21;
            this.bbi_allow_insert.Text = "F5 Lưu - Thoát";
            this.bbi_allow_insert.Click += new System.EventHandler(this.bbi_allow_insert_Click);
            // 
            // btn_exit_S
            // 
            this.btn_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit_S.Image")));
            this.btn_exit_S.Location = new System.Drawing.Point(290, 4);
            this.btn_exit_S.Name = "btn_exit_S";
            this.btn_exit_S.Size = new System.Drawing.Size(82, 31);
            this.btn_exit_S.TabIndex = 22;
            this.btn_exit_S.Text = "F9 Thoát";
            this.btn_exit_S.Click += new System.EventHandler(this.btn_exit_S_Click);
            // 
            // grp_main_C
            // 
            this.grp_main_C.Controls.Add(this.grop_Main_C);
            this.grp_main_C.Controls.Add(this.groupControl1);
            this.grp_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_main_C.Location = new System.Drawing.Point(0, 0);
            this.grp_main_C.Name = "grp_main_C";
            this.grp_main_C.Size = new System.Drawing.Size(382, 171);
            this.grp_main_C.TabIndex = 1;
            this.grp_main_C.Text = "Thông tin";
            // 
            // grop_Main_C
            // 
            this.grop_Main_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grop_Main_C.Controls.Add(this.glue_BPID_I1);
            this.grop_Main_C.Controls.Add(this.txt_NVID_IK1);
            this.grop_Main_C.Controls.Add(this.labelControl5);
            this.grop_Main_C.Controls.Add(this.labelControl1);
            this.grop_Main_C.Controls.Add(this.labelControl2);
            this.grop_Main_C.Controls.Add(this.txt_MANV_20_I1);
            this.grop_Main_C.Controls.Add(this.txt_CHUCVU_200_I2);
            this.grop_Main_C.Controls.Add(this.labelControl6);
            this.grop_Main_C.Controls.Add(this.txt_TENNV_100_I2);
            this.grop_Main_C.Controls.Add(this.labelControl3);
            this.grop_Main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grop_Main_C.Location = new System.Drawing.Point(2, 31);
            this.grop_Main_C.Name = "grop_Main_C";
            this.grop_Main_C.Size = new System.Drawing.Size(378, 138);
            this.grop_Main_C.TabIndex = 10;
            this.grop_Main_C.Text = "groupControl2";
            // 
            // glue_BPID_I1
            // 
            this.glue_BPID_I1.Location = new System.Drawing.Point(123, 80);
            this.glue_BPID_I1.Name = "glue_BPID_I1";
            this.glue_BPID_I1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("glue_BPID_I1.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.glue_BPID_I1.Properties.View = this.gridLookUpEdit1View;
            this.glue_BPID_I1.Size = new System.Drawing.Size(247, 22);
            this.glue_BPID_I1.TabIndex = 12;
            this.glue_BPID_I1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glue_id_I1_ButtonClick);
            this.glue_BPID_I1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txt_NVID_IK1
            // 
            this.txt_NVID_IK1.Enabled = false;
            this.txt_NVID_IK1.Location = new System.Drawing.Point(123, 17);
            this.txt_NVID_IK1.Name = "txt_NVID_IK1";
            this.txt_NVID_IK1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txt_NVID_IK1.Properties.Appearance.Options.UseBackColor = true;
            this.txt_NVID_IK1.Properties.MaxLength = 20;
            this.txt_NVID_IK1.Size = new System.Drawing.Size(159, 20);
            this.txt_NVID_IK1.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl5.Location = new System.Drawing.Point(10, 81);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Bộ phận (*):";
          
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl1.Location = new System.Drawing.Point(10, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "BPID (*):";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl2.Location = new System.Drawing.Point(10, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ký hiệu (*):";
            // 
            // txt_MANV_20_I1
            // 
            this.txt_MANV_20_I1.Location = new System.Drawing.Point(123, 38);
            this.txt_MANV_20_I1.Name = "txt_MANV_20_I1";
            this.txt_MANV_20_I1.Properties.MaxLength = 20;
            this.txt_MANV_20_I1.Size = new System.Drawing.Size(159, 20);
            this.txt_MANV_20_I1.TabIndex = 3;
            this.txt_MANV_20_I1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // txt_CHUCVU_200_I2
            // 
            this.txt_CHUCVU_200_I2.Location = new System.Drawing.Point(123, 102);
            this.txt_CHUCVU_200_I2.Name = "txt_CHUCVU_200_I2";
            this.txt_CHUCVU_200_I2.Properties.MaxLength = 100;
            this.txt_CHUCVU_200_I2.Size = new System.Drawing.Size(247, 20);
            this.txt_CHUCVU_200_I2.TabIndex = 5;
            this.txt_CHUCVU_200_I2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Location = new System.Drawing.Point(10, 105);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Chức vụ:";
            // 
            // txt_TENNV_100_I2
            // 
            this.txt_TENNV_100_I2.Location = new System.Drawing.Point(123, 59);
            this.txt_TENNV_100_I2.Name = "txt_TENNV_100_I2";
            this.txt_TENNV_100_I2.Properties.MaxLength = 100;
            this.txt_TENNV_100_I2.Size = new System.Drawing.Size(247, 20);
            this.txt_TENNV_100_I2.TabIndex = 4;
            this.txt_TENNV_100_I2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sign_I2_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelControl3.Location = new System.Drawing.Point(10, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Bộ phận (*):";
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(2, 21);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(378, 10);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "groupControl1";
            // 
            // dxEp_error_S
            // 
            this.dxEp_error_S.ContainerControl = this;
            // 
            // frm_BOPHAN_S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 210);
            this.ControlBox = false;
            this.Controls.Add(this.grp_main_C);
            this.Controls.Add(this.grp_footer_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frm_BOPHAN_S";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: BỘ PHẬN ::.";
            this.Activated += new System.EventHandler(this.frm_DMAREA_S_Activated);
            this.Load += new System.EventHandler(this.frm_AREA_S_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DMAREA_S_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grp_footer_C)).EndInit();
            this.grp_footer_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grp_main_C)).EndInit();
            this.grp_main_C.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grop_Main_C)).EndInit();
            this.grop_Main_C.ResumeLayout(false);
            this.grop_Main_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_BPID_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NVID_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_MANV_20_I1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CHUCVU_200_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_TENNV_100_I2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxEp_error_S)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_footer_C;
        private DevExpress.XtraEditors.SimpleButton bbi_allow_insert;
        private DevExpress.XtraEditors.SimpleButton btn_exit_S;
        private DevExpress.XtraEditors.GroupControl grp_main_C;
        private DevExpress.XtraEditors.TextEdit txt_TENNV_100_I2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_MANV_20_I1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_NVID_IK1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxEp_error_S;
        private DevExpress.XtraEditors.SimpleButton btn_insert_allow_insert;
        private DevExpress.XtraEditors.GroupControl grop_Main_C;
        private DevExpress.XtraEditors.GridLookUpEdit glue_BPID_I1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txt_CHUCVU_200_I2;
    }
}