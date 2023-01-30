using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace LoyalHRM.Presentation
{
    public partial class frmTranLate : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frmTranLate()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        public bool statusForm = false;
        #endregion

        #region FormEvent
        private void frmTranLate_Load(object sender, EventArgs e)
        {
            if (statusForm == true)
            {
                Function.clsFunction.setLanguageForm(this, this);
            }
            loadCboChoseLanguage();
            loadCboLanguage();
            Load_Grid();
            this.Text = Function.clsFunction.transLateText(this.Text);
            Function.clsFunction.TranslateForm(this, this.Name);
            Function.clsFunction.LoadLanguageGrid(gct_list_C);
            chk_hoikhiluu_S.Text = Function.clsFunction.transLateText(chk_hoikhiluu_S.Text);
            chk_showall_S.Text = Function.clsFunction.transLateText(chk_showall_S.Text);
        }
        #endregion

        #region Event

        private void cbo_langguage_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chk_showall_S.Checked == true)
                chk_showall_S_CheckedChanged(sender, e);
        }

        private void chk_showall_S_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_showall_S.Checked == true)
            {
                for (int i = 0; i < gv_list_C.Columns.Count; i++)
                {
                    if (gv_list_C.Columns[i].Name.Contains("language") )
                    {
                    if (gv_list_C.Columns[i].Name.Contains(APCoreProcess.APCoreProcess.Read("select kyhieu from sysLanggues where id='" + Function.clsFunction.getIDfromIndex(cbo_langguage_S.SelectedIndex, "sysLanggues", "id") + "'").Rows[0][0].ToString()))
                        gv_list_C.Columns[i].Visible = true;
                    else
                        if (!gv_list_C.Columns[i].Name.Contains("language_VI"))
                        gv_list_C.Columns[i].Visible = false;
                    }
                }
            }
            else
            {
                Load_Grid();
            }
        
        }
        #endregion

        #region ButtonEvent


        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void btn_delete_S_Click(object sender, EventArgs e)
        {
            if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc là muốn xóa mẫu tin này"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from sys_Language where id='"+gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"id").ToString() +"'");
                gv_list_C.DeleteSelectedRows();
            }
        }
        
        private void btn_backup_S_Click(object sender, EventArgs e)
        {
            backupFile();
        }

        private void btn_restore_S_Click(object sender, EventArgs e)
        {
            restoreFile();
        }

        private void btn_import_S_Click(object sender, EventArgs e)
        {
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();           
            frm.sDauma = "";
            frm.tbName = "sys_Language";
            frm.ShowDialog();
        }

        private void btn_export_S_Click(object sender, EventArgs e)
        {
            exportExcel();
        }
        private void cbo_field_I1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                if (e.Button.ToolTip == "Insert")
                {
                    frm_sysLanggues_S frm = new frm_sysLanggues_S();
                    frm.passData = new frm_sysLanggues_S.PassData(GetVal);
                    frm.ShowDialog();
                }
                if (e.Button.ToolTip == "Delete")
                {
                    if (APCoreProcess.APCoreProcess.Read("select kyhieu from sysLanggues where id= '" + Function.clsFunction.getIDfromIndex(cbo_field_I1.SelectedIndex, "sysLanggues", "id") + "'").Rows[0][0].ToString() != Function.clsFunction.langgues)
                    {
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này không"))
                        {
                            if (cbo_field_I1.SelectedIndex == 0)
                                Function.clsFunction.MessageInfo("Thông báo", "Bạn không thể xóa ngôn ngữ gốc của hệ thống");
                            else
                            {
                                alterColumns();
                                APCoreProcess.APCoreProcess.ExcuteSQL("delete sysLanggues where id='" + Function.clsFunction.getIDfromIndex(cbo_field_I1.SelectedIndex, "sysLanggues", "id") + "'");
                            }
                        }
                        GetVal(true); ;
                    }
                    else
                        Function.clsFunction.MessageInfo("Thông báo","Bạn không thể xóa ngôn ngữ hiện đang sử dụng,\nVui lòng chuyển sang ngôn ngữ khác để xóa ngôn ngữ này");
                }
                if (e.Button.ToolTip == "edit")
                {
                    if (cbo_field_I1.SelectedIndex == 0)
                        Function.clsFunction.MessageInfo("Thông báo", "Bạn không thể sửa ngôn ngữ gốc của hệ thống");
                    else
                    {
                        frm_sysLanggues_S frm = new frm_sysLanggues_S();
                        frm.them = false;
                        frm.txt_id_IK1.Text = APCoreProcess.APCoreProcess.Read("select id from syslanggues where id='" + Function.clsFunction.getIDfromIndex(cbo_field_I1.SelectedIndex, "sysLanggues", "id") + "'").Rows[0][0].ToString();

                        frm.passData = new frm_sysLanggues_S.PassData(GetVal);
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void alterColumns()
        {
             string kyhieu = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select kyhieu from syslanggues where id='" + Function.clsFunction.getIDfromIndex(cbo_field_I1.SelectedIndex, "syslanggues", "id").ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                kyhieu = dt.Rows[0][0].ToString();
              
            }
            APCoreProcess.APCoreProcess.ExcuteSQL("ALTER TABLE sys_Language drop column language" + kyhieu  + " ");
        }

        #endregion

        #region GridEvent


        private void grid_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }
        private void grid_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            bool indicatorIcon = false;
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = Convert.ToString(int.Parse(e.RowHandle.ToString()) + 1);

                if (!indicatorIcon)
                    e.Info.ImageIndex = -1;
            }
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = Function.clsFunction.transLateText("STT");
            }

            e.Painter.DrawObject(e.Info);
            clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
            e.Handled = true;
        }

      

        private void Load_Grid()
        {            //datasỏuce
            string sql_grid = "select id,";
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { };
            string scaption_h = "";
            string scaption_f = "";
            string sfieldname_h = "";
            string sfieldname_f = "";
            string Style_Column_h = "";
            string Style_Column_f = "";
            string Column_Width_h = "";
            string Column_Width_f = "";
            string AllowFocus_h = "";
            string AllowFocus_f = "";
            string strVisible = "False/";

            scaption_h = Function.clsFunction.transLateText("Mã")+"/";
            sfieldname_h = "ID/";
            Style_Column_h = "TextColumn/";
            Column_Width_h = "100/";
            AllowFocus_h = "False/";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select kyhieu, ngonngu from sysLanggues");
            if (dt.Rows.Count > 0)
            {
                for( int i =0 ;i< dt.Rows.Count;i++)
                {
                    scaption_h +=  Function.clsFunction.transLateText(dt.Rows[i][1].ToString())+"/";
                    sfieldname_h += "language" + dt.Rows[i][0].ToString() + "/";
                    Style_Column_h += "TextColumn" + "/";
                    Column_Width_h += "200/";
                    if (i==0)
                        AllowFocus_h += "False/";
                    else
                    AllowFocus_h += "True/";
                    sql_grid += "language" + dt.Rows[i][0].ToString() + ",";
                    strVisible += "True/";
                }
            }
            sql_grid += "note,code,userid1,date1,userid2,date2 from sys_Language ";
            scaption_f = Function.clsFunction.transLateText("Ghi chú") + "/" + Function.clsFunction.transLateText("Code") + "/" + Function.clsFunction.transLateText("Người tạo") + "/" + Function.clsFunction.transLateText("Ngày tạo") + "/" + Function.clsFunction.transLateText("Người sửa") + "/" + Function.clsFunction.transLateText("Ngày sửa") + "/";           
            scaption_h += scaption_f;
            sfieldname_f = "note/code/userid1/date1/userid2/date2/";
            sfieldname_h += sfieldname_f;
            Style_Column_f = "MemoColumn/TextColumn/TextColumn/DateColumn/TextColumn/DateColumn/";
            Style_Column_h+=Style_Column_f;
            Column_Width_f = "200/100/80/80/80/80/";           
            Column_Width_h += Column_Width_f;
            AllowFocus_f = "True/True/False/False/False/False/";
            AllowFocus_h += AllowFocus_f;
            // FieldName column
            string[] fieldname_col = new string[] { };
            caption_col = scaption_h.Split('/');
            fieldname_col = sfieldname_h.Split('/');
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] {  };
            Style_Column = Style_Column_h.Split('/');
            // Chieu rong column
            string[] Column_Width = new string[] {};
            Column_Width = Column_Width_h.Split('/');
            //AllowFocus
            string[] AllowFocus = new string[] {  };
            AllowFocus = AllowFocus_h.Split('/');
            // Cac cot an"
            strVisible += "False/False/False/False/False/False/";
            string[] Column_Visible = strVisible.Split('/');
            // datasource lookupEdit
            string[] sql_lue = new string[] { };
            // Caption lookupEdit
            string[] caption_lue = new string[] { };
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] { };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[0, 0];
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[0, 0];
            //so cot
            int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0];
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0];
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            string text = Function.clsFunction.langgues;
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false ;
            // Hien thị Gridview
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "'");
            //try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_list_C, read_Only, hien_Nav,
                column_summary, show_footer, gct_list_C,
                sql_grid, caption_col,
                fieldname_col,
                Column_Width, AllowFocus,
                Style_Column, Column_Visible,
                sql_lue, caption_lue,
                fieldname_lue, fieldname_lue_col,
                caption_lue_col, fieldname_lue_visible,
                cot_lue_search, sql_glue,
                caption_glue, fieldname_glue,
                caption_glue_col,
                fieldname_glue_col,
                fieldname_glue_visible, cot_glue_search);
                //Hien Navigator 
            }
            //catch (Exception ex)
            {
                //MessageBox.Show("co loi");
            }
        }

        private void gv_list_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (chk_hoikhiluu_S.Checked == true)
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn lưu thay đỗi"))
                        APCoreProcess.APCoreProcess.ExcuteSQL("update sys_Language set " + e.Column.Name + "=N'" + e.Value.ToString() + "' where id='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "id").ToString() + "'");
                }
                else
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sys_Language set " + e.Column.Name + "=N'" + e.Value.ToString() + "' where id='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "id").ToString() + "'");
                }
            }
            catch { }
        }

        private void gct_list_C_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (gv_list_C.FocusedColumn.VisibleIndex == gv_list_C.VisibleColumns.Count - 1)
                gv_list_C.FocusedRowHandle++;
                // gv_list_C.FocusedColumn = gv_list_C.GetNearestCanFocusedColumn(gv_list_C.Columns);
            }
        }

        #endregion

        #region Methods
        private void GetVal(bool flag)
        {
            if (flag == true)
            {
                cbo_field_I1.Properties.Items.Clear();
                loadCboChoseLanguage();
                loadCboLanguage();
                Load_Grid();
            }
        }
        private void loadCboChoseLanguage()
        {
            try
            {
                cbo_langguage_S.Properties.Items.Clear();
                ControlDev.FormatControls.LoadComBoBoxEdit1(cbo_langguage_S, APCoreProcess.APCoreProcess.Read("sysLanggues"), "", "ngonngu");
                
             
            }
            catch { }
        }
        private void loadCboLanguage()
        {
            try
            {
                cbo_field_I1.Properties.Items.Clear();
                ControlDev.FormatControls.LoadComBoBoxEdit1(cbo_field_I1, APCoreProcess.APCoreProcess.Read("sysLanggues"), "", "ngonngu");


            }
            catch { }
        }

        private void backupFile()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select ";
            for (int i = 1; i < gv_list_C.Columns.Count; i++)
            {
                if (i<gv_list_C.Columns.Count-1)
                sql += " isnull(" + gv_list_C.Columns[i].Name + ",'') as " + gv_list_C.Columns[i].Name + ", ";
                else
                    sql += " isnull(" + gv_list_C.Columns[i].Name + ",'') as " + gv_list_C.Columns[i].Name + " ";
            }
            sql += " from sys_Language ";
                dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt != null)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Lang Files|*.lang| All file|*.*";
                sf.Title = Function.clsFunction.transLateText("Lưu file ngôn ngữ");                
                sf.ShowDialog();
                if (sf.FileName != "")
                {
                    ds.Tables.Add(dt);
                    ds.WriteXml(sf.FileName);
                }
            }
        }

        private void restoreFile()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            APCoreProcess.APCoreProcess.ExcuteSQL("truncate table sys_Language");
            {
                OpenFileDialog sf = new OpenFileDialog();
                sf.Filter = "Lang Files|*.lang| All file|*.*";
                sf.Title = Function.clsFunction.transLateText("Mở file ngôn ngữ");
                sf.ShowDialog();
                if (sf.FileName != "")
                {
                    ds.ReadXml(sf.FileName);
                    if (ds != null)
                    {
             
                        gct_list_C.DataSource = ds.Tables[0];
                        SaveGridInDatabase();
                    }
                }

            }
        }

        private void SaveGridInDatabase()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sys_Language");
            DataRow dr;
            for (int i = 0; i < gv_list_C.DataRowCount; i++)
            {
                dr = dt.NewRow();
                for (int j =1; j < gv_list_C.Columns.Count; j++)
                {
                    dr[gv_list_C.Columns[j].Name] = gv_list_C.GetRowCellValue(i, gv_list_C.Columns[j].Name).ToString();
                }
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
        }

        private void exportExcel()
        {
            if (gv_list_C.DataRowCount > 0)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Lang Files|*.xls";
                sf.Title = Function.clsFunction.transLateText("Xuất excel");
                sf.ShowDialog();
                if (sf.FileName != "")
                {
                    gct_list_C.ExportToXls(sf.FileName);
                }
            }
        }

        #endregion
             

    }
}