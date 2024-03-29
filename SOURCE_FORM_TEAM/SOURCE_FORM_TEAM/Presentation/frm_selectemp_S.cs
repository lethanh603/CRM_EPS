﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using Function;
using DevExpress.XtraGrid.Views.Grid;

namespace SOURCE_FORM_TEAM.Presentation
{
    public partial class frm_selectemp_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_selectemp_S()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "KH";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void StrPassData(string value);
        public StrPassData strpassData;
        public string sIdCus = "";

        #endregion

        #region FormEvent

        private void frm_selectcus_Load(object sender, EventArgs e)
        {
            //statusForm = true;
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {
                SaveGridControls();   
                clsFunction.Save_sysControl(this, this);
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGrid();
                Load_Grid();// grid customer

                gv_listcustomer_C.Columns["check"].OptionsColumn.AllowEdit = true;
                gv_listcustomer_C.Columns[1].OptionsColumn.AllowEdit = true;
                
                Function.clsFunction.TranslateGridColumn(gv_listcustomer_C); 
                gv_listcustomer_C.FocusedRowHandle = 0;
                loadCusSelect();
            
                
            }
        }

        private void loadGrid()
        {
            if (Function.clsFunction._pre == true)
            {
                dts = Function.clsFunction.dtTrace;
            }
            else
            {
                dts = APCoreProcess.APCoreProcess.Read("SELECT   idemp , sign, staffname, iddepartment from employees ORDER BY idemp ");
            }
            dts.Columns.Add("check", typeof(Boolean));
            gct_listcustomer_C.DataSource = dts;
            if (dts.Rows.Count > 0)
            {
                gv_listcustomer_C.FocusedRowHandle = 0;
            }
        }


        private void frm_selectemp_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_select_S.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.Close();
            }
        }


        #endregion

        #region GridEvent

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Chọn", "ID", "Ký hiệu",  "Nhân viên", "Bộ phận" };

            // FieldName column
            string[] fieldname_col = new string[] { "check", "idemp", "sign", "staffname", "iddepartment" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "CheckColumn", "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "100", "100", "200", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False"};
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select iddepartment, department  from dmdepartment where status=1", };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "department"};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "iddepartment" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã BP", "Bộ phận" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "iddepartment", "department" }};
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_listcustomer_C.Name);
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;
            gv_listcustomer_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_listcustomer_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_listcustomer_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_listcustomer_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_listcustomer_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                gv_listcustomer_C.OptionsBehavior.Editable = true;
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_listcustomer_C.Columns["check"].OptionsColumn.AllowEdit = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ButtonEvent

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_select_S_Click(object sender, EventArgs e)
        {
            try
            {
                sIdCus = "";
                for (int i = 0; i < gv_listcustomer_C.RowCount; i++)
                {
                    if ((bool)(gv_listcustomer_C.GetRowCellValue(i, "check")) == true)
                    {
                        sIdCus += gv_listcustomer_C.GetRowCellValue(i,"idemp").ToString() + "@";
                    }                
                }
                strpassData(sIdCus);
            }
            catch (Exception ex)
            {
            }
            this.Close();
        }

        #endregion

        #region Methods

        private void loadCusSelect()
        {
            try
            {
                for (int i = 0; i < gv_listcustomer_C.RowCount; i++)
                {
                    if (sIdCus.Contains(gv_listcustomer_C.GetRowCellValue(i, "idemp").ToString()))
                    {
                        gv_listcustomer_C.SetRowCellValue(i, "check", true);
                    }
                    else
                    {
                        gv_listcustomer_C.SetRowCellValue(i, "check", false);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion




    }
}