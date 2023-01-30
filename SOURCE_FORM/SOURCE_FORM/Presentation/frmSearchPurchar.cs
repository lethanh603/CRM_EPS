using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using Function;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_PURCHASE.Presentation
{
    public partial class frmSearchPurchar : DevExpress.XtraEditors.XtraForm
    {
        public frmSearchPurchar()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "PN";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void StrPassData(string value);
        public StrPassData strpassData;
        #endregion

        #region FormEvent

        private void frmSearchPurchar_Load(object sender, EventArgs e)
        {
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {
                SaveGridControls();            
                clsFunction.Save_sysControl(this, this);           

            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                Load_Grid();           
                loadGrid();
                Function.clsFunction.TranslateGridColumn(gv_PURCHASEDETAIL_C);    
                loadGridLookupProvider();
                loadGridLookupEmployee();
                loadGridLookupMethod();

                Init();
            }
        }

        private void frmSearchPurchar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_view_S.PerformClick();
            }
            if (e.KeyCode == Keys.F10)
            {
                btn_tim_S.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
        }


        #endregion

        #region ButtonEvent

        private void gv_PURCHASEDETAIL_C_DoubleClick(object sender, EventArgs e)
        {
            btn_view_S.PerformClick();
        }


        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_S_Click(object sender, EventArgs e)
        {
            try
            {
                strpassData(gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "idpurchase").ToString());
            }
            catch
            { }
            this.Close();
        }

        private void btn_tim_S_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] {  };
            // Caption column
            string[] caption_col = new string[] { "Mã phiếu", "Nhà cung cấp", "Nhân viên", "Ngày nhập", "Ngày nợ", "Ngày trả","Công nợ", "HTTT", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idpurchase_S", "col_idprovider_S", "col_IDEMP_S", "col_dateimport_S", "col_limitdept_S", "col_limit_S", "col_isdept_S", "col_outlet_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "DateColumn", "SpinEditColumn", "CheckColumn", "CheckColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "200", "100", "100", "100", "100", "100", "200"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idprovider, provider  from dmprovider", "select IDEMP, StaffName  from EMPLOYEES"  };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "provider", "StaffName" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idprovider", "IDEMP" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "Mã NCC", "Nhà cung cấp" }, { "Mã NV", "Nhân viên" }};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "idprovider", "provider" }, { "IDEMP", "StaffName" }};
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns_Edit(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_PURCHASEDETAIL_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

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


        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho" };
            bool show_footer = true;
            // show filterRow
            gv_PURCHASEDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_PURCHASEDETAIL_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_PURCHASEDETAIL_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["column_name"].ToString().Split('/'), dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()), gluenulltext);
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_PURCHASEDETAIL_C.OptionsBehavior.Editable = true;
                gv_PURCHASEDETAIL_C.OptionsView.ColumnAutoWidth = true;
                gv_PURCHASEDETAIL_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    

        #endregion

        #region Methods

        private void loadGridLookupMethod()
        {
            try
            {
                string[] caption = new string[] { "Mã PT", "Phương thức" };
                string[] fieldname = new string[] { "idmethod", "MeThodName" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_method_I1, "select '' as idmethod, N'"+ clsFunction.transLateText("Tất cả") +"' as MeThodName union select idmethod, MeThodName from dmmethod where status=1 and kind ='IN'", "MeThodName", "idmethod", caption, fieldname, this.Name, glue_method_I1.Width * 1, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
            }
            catch { }
        }

        private void loadGridLookupProvider()
        {
            try
            {
                string[] caption = new string[] { "Mã NCC", "Tên NCC", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idprovider", "provider", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idprovider_I1, "select '' as idprovider, N'" + clsFunction.transLateText("Tất cả") + "' as provider,'' as tel, '' as fax, '' as address union select idprovider, provider, tel, fax, address from dmprovider where status=1", "provider", "idprovider", caption, fieldname, this.Name, glue_idprovider_I1.Width, col_visible);

            }
            catch { }
        }

        private void Init()
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1);
            dte_todate_S.EditValue = DateTime.Now;
            btn_tim_S.PerformClick();
        }

        private void loadGrid()
        {
            try
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {                
                    string str = "";
                    if (glue_IDEMP_I1.EditValue.ToString()!="")
                    {
                        str += " and IDEMP='" + glue_IDEMP_I1.EditValue.ToString() + "'";
                    }
                    if (glue_idprovider_I1.EditValue.ToString()!="")
                    {
                        str += " and idprovider='" + glue_idprovider_I1.EditValue.ToString() + "'";
                    }
                    if (glue_method_I1.EditValue.ToString()!="")
                    {
                        str += " and IDMETHOD='" + glue_method_I1.EditValue.ToString() + "'";
                    }
                    str += " and outlet='" + rad_outlet_I6.EditValue.ToString() + "'";
                    if (txt_idpurchase_IK1.Text != "")
                    {
                        str += " and idpurchase='" + txt_idpurchase_IK1.Text + "'";
                    }
                    dts = APCoreProcess.APCoreProcess.Read("select * from purchase where CAST(datediff(d,0,dateimport) as datetime)  between CAST(datediff(d,0,'" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') as datetime)  and CAST(datediff(d,0,'" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') as datetime) " + str + " and status=0 ");
                }
                gct_list_C.DataSource = dts;
            }
            catch { }
        }

        #endregion



    }
}