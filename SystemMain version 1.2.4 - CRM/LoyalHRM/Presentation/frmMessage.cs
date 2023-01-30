using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace LoyalHRM.Presentation
{
    public partial class frmMessage : DevExpress.XtraEditors.XtraForm
    {
        public frmMessage()
        {
            InitializeComponent();
        }
        bool statusForm = false;
        public string langues = "_VI";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
 

        private void frmMessage_Load(object sender, EventArgs e)
        {
            if (statusForm == true)
            {
                SaveGridControls();
            }
            else
            {
         
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     NHAPNHANVIEN.tennhanvien, NHAPPHONGBAN.tenphongban, NHAPCHUCVU.tenchucvu, ngaysinh, CONVERT(int, DATEDIFF(day, GETDATE(), DATEADD(year, DATEPART(year, GETDATE()) - DATEPART(year, CONVERT(datetime,  NHAPNHANVIEN.ngaysinh, 103)), CONVERT(datetime, NHAPNHANVIEN.ngaysinh, 103)))) AS songay FROM         NHAPNHANVIEN INNER JOIN       NHAPCHUCVU ON NHAPNHANVIEN.machucvu = NHAPCHUCVU.machucvu INNER JOIN       NHAPPHONGBAN ON NHAPNHANVIEN.maphongban = NHAPPHONGBAN.maphongban WHERE     (CONVERT(int, DATEDIFF(day, GETDATE(), DATEADD(year, DATEPART(year, GETDATE()) - DATEPART(year, CONVERT(datetime, NHAPNHANVIEN.ngaysinh, 103)),     CONVERT(datetime, NHAPNHANVIEN.ngaysinh, 103)))) >= 0) AND (CONVERT(int, DATEDIFF(day, GETDATE(), DATEADD(year, DATEPART(year, GETDATE())   - DATEPART(year, CONVERT(datetime, NHAPNHANVIEN.ngaysinh, 103)), CONVERT(datetime, NHAPNHANVIEN.ngaysinh, 103)))) <= 10)");
                gctr_danhmuc_C.DataSource = dt;
                Load_Grid();
                Function.clsFunction.setDateNumToDate(gv_danhmuc_C,"ngaysinh");
                Function.clsFunction.TranslateForm(this, this.Name);
            }

        }
        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] {  "Nhân Viên", "Phòng Ban", "Chức vụ", "Ngày Sinh", "Số Ngày" };
            // FieldName column
            string[] fieldname_col = new string[] {  "tennhanvien", "tenphongban", "tenchucvu", "ngaysinh", "songay" };
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "CalcEditColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] {  "200", "150", "150", "100", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { };
            // datasource lookupEdit
            string[] sql_lue = new string[] {};
            // Caption lookupEdit
            string[] caption_lue = new string[] {};
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] {  };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[0, 0];
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[0, 0] ;
            //so cot
            //int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {  };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {  };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0,0] ;
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0] ;
            //so cot
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] {};
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
     caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
     fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
     fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gctr_danhmuc_C.Name);
        }

        private void Load_Grid()
        {
            string text = langues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;

            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "'");
            //try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_danhmuc_C, read_Only, hien_Nav,
                        dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gctr_danhmuc_C,
                        dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption" + text].ToString().Split('/'),
                        dt.Rows[0]["field_name"].ToString().Split('/'),
                        dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                        dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                        dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue" + text].ToString().Split('/'),
                        dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                        Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col" + text].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                        int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                        dt.Rows[0]["caption_glue_VI"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                        Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                        Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col" + text].ToString()), "@", "/"),
                        dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 


            }
            //catch (Exception ex)
            {
                //MessageBox.Show("co loi");
            }
        }
        private void gv_danhmuc_C_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                try
                {
                    string category = View.GetRowCellValue(e.RowHandle, View.Columns["songay"]).ToString();
                    if (category == "0")
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.BackColor2 = Color.Red;
                    }
                    // tre som

                    if (Convert.ToInt16(category) > 0 && Convert.ToInt16(category) <= 3)
                    {
                        e.Appearance.BackColor = Color.YellowGreen;
                        e.Appearance.BackColor2 = Color.YellowGreen;
                    }
                
                }
                catch 
                {
                }
            }
        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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


    }
}