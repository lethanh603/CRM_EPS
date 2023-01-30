namespace ReportControls.Presentation
{
    partial class frmConfigRePort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigRePort));
            this.chk_iscurrent_IS = new DevExpress.XtraEditors.CheckEdit();
            this.btn_save_S = new DevExpress.XtraEditors.SimpleButton();
            this.glue_report_IS = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_main_C = new DevExpress.XtraEditors.GroupControl();
            this.btn_allow_exit_S = new DevExpress.XtraEditors.SimpleButton();
            this.rad_option_C = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.glue_template_IS = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btn_delete_S = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.chk_customSource_I6 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_path_IS = new DevExpress.XtraEditors.TextEdit();
            this.txt_caption_IS = new DevExpress.XtraEditors.TextEdit();
            this.txt_id_IK1 = new DevExpress.XtraEditors.TextEdit();
            this.txt_name_IS = new DevExpress.XtraEditors.TextEdit();
            this.btn_browser_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_saveinfo_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cancel_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_edit_S = new DevExpress.XtraEditors.SimpleButton();
            this.btn_new_S = new DevExpress.XtraEditors.SimpleButton();
            this.txt_query_IS = new DevExpress.XtraEditors.MemoEdit();
            this.gc_top_C = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_run_S = new DevExpress.XtraEditors.SimpleButton();
            this.gc_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_reportname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_caption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReportname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcQuery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chk_pro_S = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_iscurrent_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_report_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_main_C)).BeginInit();
            this.gc_main_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rad_option_C.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_template_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_customSource_I6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_path_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_caption_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_id_IK1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_query_IS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_top_C)).BeginInit();
            this.gc_top_C.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_pro_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chk_iscurrent_IS
            // 
            this.chk_iscurrent_IS.Location = new System.Drawing.Point(117, 60);
            this.chk_iscurrent_IS.Name = "chk_iscurrent_IS";
            this.chk_iscurrent_IS.Properties.Caption = "Current";
            this.chk_iscurrent_IS.Size = new System.Drawing.Size(75, 19);
            this.chk_iscurrent_IS.TabIndex = 12;
            // 
            // btn_save_S
            // 
            this.btn_save_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_save_S.Image")));
            this.btn_save_S.Location = new System.Drawing.Point(217, 57);
            this.btn_save_S.Name = "btn_save_S";
            this.btn_save_S.Size = new System.Drawing.Size(75, 26);
            this.btn_save_S.TabIndex = 13;
            this.btn_save_S.Text = "Save";
            this.btn_save_S.Click += new System.EventHandler(this.btn_save_S_Click);
            // 
            // glue_report_IS
            // 
            this.glue_report_IS.EditValue = "Choose report";
            this.glue_report_IS.Location = new System.Drawing.Point(119, 32);
            this.glue_report_IS.Name = "glue_report_IS";
            this.glue_report_IS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_report_IS.Properties.DisplayMember = "caption";
            this.glue_report_IS.Properties.NullText = "Choose report";
            this.glue_report_IS.Properties.ValueMember = "id";
            this.glue_report_IS.Properties.View = this.gridLookUpEdit1View;
            this.glue_report_IS.Size = new System.Drawing.Size(382, 20);
            this.glue_report_IS.TabIndex = 11;
            this.glue_report_IS.EditValueChanged += new System.EventHandler(this.glue_report_S_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_ID,
            this.gc_reportname,
            this.gc_caption});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gc_main_C
            // 
            this.gc_main_C.Controls.Add(this.btn_allow_exit_S);
            this.gc_main_C.Controls.Add(this.rad_option_C);
            this.gc_main_C.Controls.Add(this.labelControl6);
            this.gc_main_C.Controls.Add(this.glue_template_IS);
            this.gc_main_C.Controls.Add(this.labelControl5);
            this.gc_main_C.Controls.Add(this.btn_delete_S);
            this.gc_main_C.Controls.Add(this.labelControl4);
            this.gc_main_C.Controls.Add(this.labelControl3);
            this.gc_main_C.Controls.Add(this.chk_pro_S);
            this.gc_main_C.Controls.Add(this.chk_customSource_I6);
            this.gc_main_C.Controls.Add(this.labelControl2);
            this.gc_main_C.Controls.Add(this.txt_path_IS);
            this.gc_main_C.Controls.Add(this.txt_caption_IS);
            this.gc_main_C.Controls.Add(this.txt_id_IK1);
            this.gc_main_C.Controls.Add(this.txt_name_IS);
            this.gc_main_C.Controls.Add(this.btn_browser_S);
            this.gc_main_C.Controls.Add(this.btn_saveinfo_S);
            this.gc_main_C.Controls.Add(this.btn_cancel_S);
            this.gc_main_C.Controls.Add(this.btn_edit_S);
            this.gc_main_C.Controls.Add(this.btn_new_S);
            this.gc_main_C.Controls.Add(this.txt_query_IS);
            this.gc_main_C.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gc_main_C.Location = new System.Drawing.Point(0, 88);
            this.gc_main_C.Name = "gc_main_C";
            this.gc_main_C.Size = new System.Drawing.Size(507, 245);
            this.gc_main_C.TabIndex = 3;
            this.gc_main_C.Text = "Edit Or New Report Designs";
            // 
            // btn_allow_exit_S
            // 
            this.btn_allow_exit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_allow_exit_S.Image")));
            this.btn_allow_exit_S.Location = new System.Drawing.Point(427, 203);
            this.btn_allow_exit_S.Name = "btn_allow_exit_S";
            this.btn_allow_exit_S.Size = new System.Drawing.Size(74, 37);
            this.btn_allow_exit_S.TabIndex = 12;
            this.btn_allow_exit_S.Text = "Thoát";
            this.btn_allow_exit_S.Click += new System.EventHandler(this.btn_allow_exit_S_Click);
            // 
            // rad_option_C
            // 
            this.rad_option_C.EditValue = true;
            this.rad_option_C.Location = new System.Drawing.Point(309, 109);
            this.rad_option_C.Name = "rad_option_C";
            this.rad_option_C.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Rinbon"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Standard")});
            this.rad_option_C.Size = new System.Drawing.Size(192, 23);
            this.rad_option_C.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(14, 90);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 13);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "Choose Report:";
            // 
            // glue_template_IS
            // 
            this.glue_template_IS.EditValue = "Chooose report";
            this.glue_template_IS.Location = new System.Drawing.Point(120, 88);
            this.glue_template_IS.Name = "glue_template_IS";
            this.glue_template_IS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_template_IS.Properties.DisplayMember = "caption";
            this.glue_template_IS.Properties.NullText = "Choose report";
            this.glue_template_IS.Properties.ValueMember = "id";
            this.glue_template_IS.Properties.View = this.gridView1;
            this.glue_template_IS.Size = new System.Drawing.Size(381, 20);
            this.glue_template_IS.TabIndex = 3;
            this.glue_template_IS.EditValueChanged += new System.EventHandler(this.glue_template_IS_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcID,
            this.gcReportname,
            this.gcCaption,
            this.gcQuery});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(14, 135);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(68, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Source query:";
            // 
            // btn_delete_S
            // 
            this.btn_delete_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete_S.Image")));
            this.btn_delete_S.Location = new System.Drawing.Point(194, 203);
            this.btn_delete_S.Name = "btn_delete_S";
            this.btn_delete_S.Size = new System.Drawing.Size(75, 37);
            this.btn_delete_S.TabIndex = 8;
            this.btn_delete_S.Text = "Delete";
            this.btn_delete_S.Click += new System.EventHandler(this.btn_delete_S_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 69);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Path:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 49);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Caption:";
            // 
            // chk_customSource_I6
            // 
            this.chk_customSource_I6.Location = new System.Drawing.Point(120, 109);
            this.chk_customSource_I6.Name = "chk_customSource_I6";
            this.chk_customSource_I6.Properties.Caption = "Custom source";
            this.chk_customSource_I6.Size = new System.Drawing.Size(94, 19);
            this.chk_customSource_I6.TabIndex = 4;
            this.chk_customSource_I6.CheckedChanged += new System.EventHandler(this.chk_custom_S_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Name:";
            // 
            // txt_path_IS
            // 
            this.txt_path_IS.EditValue = "E:\\Report";
            this.txt_path_IS.Enabled = false;
            this.txt_path_IS.Location = new System.Drawing.Point(120, 67);
            this.txt_path_IS.Name = "txt_path_IS";
            this.txt_path_IS.Properties.ReadOnly = true;
            this.txt_path_IS.Size = new System.Drawing.Size(381, 20);
            this.txt_path_IS.TabIndex = 2;
            // 
            // txt_caption_IS
            // 
            this.txt_caption_IS.Location = new System.Drawing.Point(120, 46);
            this.txt_caption_IS.Name = "txt_caption_IS";
            this.txt_caption_IS.Size = new System.Drawing.Size(381, 20);
            this.txt_caption_IS.TabIndex = 1;
            // 
            // txt_id_IK1
            // 
            this.txt_id_IK1.Location = new System.Drawing.Point(120, 25);
            this.txt_id_IK1.Name = "txt_id_IK1";
            this.txt_id_IK1.Size = new System.Drawing.Size(138, 20);
            this.txt_id_IK1.TabIndex = 0;
            this.txt_id_IK1.Visible = false;
            // 
            // txt_name_IS
            // 
            this.txt_name_IS.Location = new System.Drawing.Point(120, 25);
            this.txt_name_IS.Name = "txt_name_IS";
            this.txt_name_IS.Size = new System.Drawing.Size(381, 20);
            this.txt_name_IS.TabIndex = 0;
            // 
            // btn_browser_S
            // 
            this.btn_browser_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_browser_S.Image")));
            this.btn_browser_S.Location = new System.Drawing.Point(428, 66);
            this.btn_browser_S.Name = "btn_browser_S";
            this.btn_browser_S.Size = new System.Drawing.Size(73, 22);
            this.btn_browser_S.TabIndex = 10;
            this.btn_browser_S.Text = "Browser";
            this.btn_browser_S.Visible = false;
            this.btn_browser_S.Click += new System.EventHandler(this.btn_browser_S_Click);
            // 
            // btn_saveinfo_S
            // 
            this.btn_saveinfo_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_saveinfo_S.Image")));
            this.btn_saveinfo_S.Location = new System.Drawing.Point(349, 203);
            this.btn_saveinfo_S.Name = "btn_saveinfo_S";
            this.btn_saveinfo_S.Size = new System.Drawing.Size(74, 37);
            this.btn_saveinfo_S.TabIndex = 6;
            this.btn_saveinfo_S.Text = "Save";
            this.btn_saveinfo_S.Click += new System.EventHandler(this.btn_saveinfo_S_Click);
            // 
            // btn_cancel_S
            // 
            this.btn_cancel_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel_S.Image")));
            this.btn_cancel_S.Location = new System.Drawing.Point(272, 203);
            this.btn_cancel_S.Name = "btn_cancel_S";
            this.btn_cancel_S.Size = new System.Drawing.Size(74, 37);
            this.btn_cancel_S.TabIndex = 9;
            this.btn_cancel_S.Text = "Cancel";
            this.btn_cancel_S.Click += new System.EventHandler(this.btn_cancel_S_Click);
            // 
            // btn_edit_S
            // 
            this.btn_edit_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit_S.Image")));
            this.btn_edit_S.Location = new System.Drawing.Point(117, 203);
            this.btn_edit_S.Name = "btn_edit_S";
            this.btn_edit_S.Size = new System.Drawing.Size(74, 37);
            this.btn_edit_S.TabIndex = 7;
            this.btn_edit_S.Text = "Edit";
            this.btn_edit_S.Click += new System.EventHandler(this.btn_edit_S_Click);
            // 
            // btn_new_S
            // 
            this.btn_new_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_new_S.Image")));
            this.btn_new_S.Location = new System.Drawing.Point(40, 203);
            this.btn_new_S.Name = "btn_new_S";
            this.btn_new_S.Size = new System.Drawing.Size(74, 37);
            this.btn_new_S.TabIndex = 6;
            this.btn_new_S.Text = "New";
            this.btn_new_S.Click += new System.EventHandler(this.btn_new_S_Click);
            // 
            // txt_query_IS
            // 
            this.txt_query_IS.Location = new System.Drawing.Point(120, 133);
            this.txt_query_IS.Name = "txt_query_IS";
            this.txt_query_IS.Size = new System.Drawing.Size(381, 64);
            this.txt_query_IS.TabIndex = 5;
            this.txt_query_IS.UseOptimizedRendering = true;
            // 
            // gc_top_C
            // 
            this.gc_top_C.Controls.Add(this.labelControl1);
            this.gc_top_C.Controls.Add(this.glue_report_IS);
            this.gc_top_C.Controls.Add(this.chk_iscurrent_IS);
            this.gc_top_C.Controls.Add(this.btn_run_S);
            this.gc_top_C.Controls.Add(this.btn_save_S);
            this.gc_top_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_top_C.Location = new System.Drawing.Point(0, 0);
            this.gc_top_C.Name = "gc_top_C";
            this.gc_top_C.Size = new System.Drawing.Size(507, 88);
            this.gc_top_C.TabIndex = 3;
            this.gc_top_C.Text = "Option";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Choose Report:";
            // 
            // btn_run_S
            // 
            this.btn_run_S.Image = ((System.Drawing.Image)(resources.GetObject("btn_run_S.Image")));
            this.btn_run_S.Location = new System.Drawing.Point(413, 57);
            this.btn_run_S.Name = "btn_run_S";
            this.btn_run_S.Size = new System.Drawing.Size(88, 26);
            this.btn_run_S.TabIndex = 14;
            this.btn_run_S.Text = "Run Designs";
            this.btn_run_S.Click += new System.EventHandler(this.btn_run_S_Click);
            // 
            // gc_ID
            // 
            this.gc_ID.Caption = "ID";
            this.gc_ID.FieldName = "id";
            this.gc_ID.Name = "gc_ID";
            // 
            // gc_reportname
            // 
            this.gc_reportname.Caption = "Report name";
            this.gc_reportname.FieldName = "reportname";
            this.gc_reportname.Name = "gc_reportname";
            this.gc_reportname.Visible = true;
            this.gc_reportname.VisibleIndex = 1;
            // 
            // gc_caption
            // 
            this.gc_caption.Caption = "Caption";
            this.gc_caption.FieldName = "caption";
            this.gc_caption.Name = "gc_caption";
            this.gc_caption.Visible = true;
            this.gc_caption.VisibleIndex = 0;
            // 
            // gcID
            // 
            this.gcID.Caption = "ID";
            this.gcID.FieldName = "id";
            this.gcID.Name = "gcID";
            // 
            // gcReportname
            // 
            this.gcReportname.Caption = "Report name";
            this.gcReportname.FieldName = "reportname";
            this.gcReportname.Name = "gcReportname";
            this.gcReportname.Visible = true;
            this.gcReportname.VisibleIndex = 1;
            // 
            // gcCaption
            // 
            this.gcCaption.Caption = "Caption";
            this.gcCaption.FieldName = "caption";
            this.gcCaption.Name = "gcCaption";
            this.gcCaption.Visible = true;
            this.gcCaption.VisibleIndex = 0;
            // 
            // gcQuery
            // 
            this.gcQuery.Caption = "Query";
            this.gcQuery.FieldName = "query";
            this.gcQuery.Name = "gcQuery";
            // 
            // chk_pro_S
            // 
            this.chk_pro_S.Location = new System.Drawing.Point(220, 109);
            this.chk_pro_S.Name = "chk_pro_S";
            this.chk_pro_S.Properties.Caption = "Procedures";
            this.chk_pro_S.Size = new System.Drawing.Size(94, 19);
            this.chk_pro_S.TabIndex = 4;
            this.chk_pro_S.CheckedChanged += new System.EventHandler(this.chk_custom_S_CheckedChanged);
            // 
            // frmConfigRePort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 333);
            this.Controls.Add(this.gc_top_C);
            this.Controls.Add(this.gc_main_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigRePort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config Report";
            this.Load += new System.EventHandler(this.frmConfigRePort_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chk_iscurrent_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_report_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_main_C)).EndInit();
            this.gc_main_C.ResumeLayout(false);
            this.gc_main_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rad_option_C.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_template_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_customSource_I6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_path_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_caption_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_id_IK1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_query_IS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_top_C)).EndInit();
            this.gc_top_C.ResumeLayout(false);
            this.gc_top_C.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_pro_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chk_iscurrent_IS;
        private DevExpress.XtraEditors.SimpleButton btn_save_S;
        private DevExpress.XtraEditors.GridLookUpEdit glue_report_IS;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.GroupControl gc_main_C;
        private DevExpress.XtraEditors.GroupControl gc_top_C;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_caption_IS;
        private DevExpress.XtraEditors.TextEdit txt_name_IS;
        private DevExpress.XtraEditors.SimpleButton btn_new_S;
        private DevExpress.XtraEditors.SimpleButton btn_saveinfo_S;
        private DevExpress.XtraEditors.SimpleButton btn_cancel_S;
        private DevExpress.XtraEditors.SimpleButton btn_edit_S;
        private DevExpress.XtraEditors.SimpleButton btn_run_S;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gc_reportname;
        private DevExpress.XtraGrid.Columns.GridColumn gc_caption;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.GridLookUpEdit glue_template_IS;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcID;
        private DevExpress.XtraGrid.Columns.GridColumn gcReportname;
        private DevExpress.XtraGrid.Columns.GridColumn gcCaption;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btn_delete_S;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chk_customSource_I6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_path_IS;
        private DevExpress.XtraEditors.SimpleButton btn_browser_S;
        private DevExpress.XtraEditors.MemoEdit txt_query_IS;
        private DevExpress.XtraEditors.RadioGroup rad_option_C;
        private DevExpress.XtraEditors.TextEdit txt_id_IK1;
        private DevExpress.XtraEditors.SimpleButton btn_allow_exit_S;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuery;
        private DevExpress.XtraEditors.CheckEdit chk_pro_S;
    }
}