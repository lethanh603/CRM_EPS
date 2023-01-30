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

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frmReportDoanhSoBaoGiaChiTiet : DevExpress.XtraEditors.XtraForm
    {
        public frmReportDoanhSoBaoGiaChiTiet()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "QS";
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
            // statusForm = true;
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
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                loadGridLookupGroup();
                loadGridLookupEmployee();
                loadGridLookupMethod();
                loadGridLookupGroupTK();
                Init();
                loadGrid();
            }
        }

        private void frmSearchPurchar_KeyDown(object sender, KeyEventArgs e)
        {

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

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_S_Click(object sender, EventArgs e)
        {
            try
            {
                strpassData(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());
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
            string[] column_summary = new string[] { "quantity", "amount", "amountvat", "total" };
            // Caption column
            string[] caption_col = new string[] { "Mã HH", "Tên sản phầm", "Loại", "Nhóm", "ĐVT", "Số lượng", "Thành tiền", "Tiền VAT", "Tổng cộng", "Loại BG", "Tình trạng báo giá"};

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idcommodity_S", "col_commodity_S","col_unit_S", "col_kind_S", "col_groupname_S","col_quantity_S", "col_amount_S", "col_amountvat_S", "col_total_S", "col_quotationtype_S", "col_statusquotation_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "TextColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "250", "150", "150", "100", "150", "130", "130", "130", "130", "250", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            //int so_cot_lue = 0;
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
            string[,] caption_glue_col = new string[0, 0] { };

            string[,] fieldname_glue_col = new string[0, 0] { };
            //so cot
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns_Edit(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_EXPORTDETAIL_C.Name);
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
            string[] gluenulltext = new string[] { "", "", "", "", "" };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_EXPORTDETAIL_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_EXPORTDETAIL_C, read_Only, hien_Nav,
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
                gv_EXPORTDETAIL_C.OptionsBehavior.Editable = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;

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
                try
                {
                    string[] caption = new string[] { "Mã TT", "Tình trạng" };
                    string[] fieldname = new string[] { "idstatusquotation", "statusquotation" };
                    string[] col_visible = new string[] { "True", "True" };
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusquotation_I1, "select idstatusquotation, statusquotation from dmstatusquotation  ", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, glue_idstatusquotation_I1.Width * 1, col_visible);


                }
                catch (Exception ex)
                {
                    clsFunction.MessageInfo("Thông báo", ex.Message);
                }

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
                if (Function.clsFunction.checkAdmin())
                {
                    ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%' and status =1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
                }
            }
            catch { }
        }

        private void loadGridLookupGroup()
        {
            try
            {
                string[] caption = new string[] { "Mã BP", "Bộ phận" };
                string[] fieldname = new string[] { "iddepartment", "department" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idquotationtype_I1, "select '' as iddepartment, N'" + clsFunction.transLateText("Tất cả") + "' as department union select iddepartment, department from dmdepartment where status=1", "department", "iddepartment", caption, fieldname, this.Name, glue_idquotationtype_I1.Width, col_visible);

            }
            catch { }
        }

        private void loadGridLookupGroupTK()
        {
            try
            {
                string[] caption = new string[] { "Mã SP", "Loại SP" };
                string[] fieldname = new string[] { "idgrouptk", "grouptk" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idgrouptk_I1, "select '' as idgrouptk, N'" + clsFunction.transLateText("Tất cả") + "' as grouptk union select idgrouptk, grouptk from dmgrouptk where status=1", "grouptk", "idgrouptk", caption, fieldname, this.Name, glue_idgrouptk_I1.Width, col_visible);

            }
            catch { }
        }

        private void Init()
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
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
                    String date_po = "dateimport";
                    if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000004")
                    {
                        date_po = "datepo";
                    }

                    string sql = "SELECT        QD.idcommodity, C.commodity, SUM(QD.quantity) AS quantity, SUM(QD.amount) AS amount, SUM(QD.amountvat) AS amountvat, SUM(QD.total) AS total, U.unit, ";
                        sql +=" QT.quotationtype, ST.statusquotation, G.groupname, K.kind ";
                        sql +=" FROM            dbo.DMSTATUSQUOTATION AS ST INNER JOIN ";

                        sql += "  ( SELECT * FROM dbo.QUOTATION WHERE " + date_po + " between Cast('" + Convert.ToDateTime(dte_fromdate_S.EditValue) + "'as Date) AND Cast('" + dte_todate_S.EditValue + "'as Date) ";
                        sql +="  AND idemp like '%"+ (glue_IDEMP_I1.EditValue != "" ? glue_IDEMP_I1.EditValue : "") +"%')";

						sql +="  AS Q ON ST.idstatusquotation = Q.idstatusquotation INNER JOIN ";
                        sql +=" dbo.QUOTATIONDETAIL AS QD ON Q.idexport = QD.idexport INNER JOIN ";
                        sql +=" dbo.DMCOMMODITY AS C ON QD.idcommodity = C.idcommodity INNER JOIN ";
                        sql +=" dbo.DMUNIT AS U ON QD.idunit = U.idunit LEFT OUTER JOIN ";
                        sql +=" dbo.DMGROUP AS G ON C.idgroup = G.idgroup LEFT OUTER JOIN ";
                        sql +=" dbo.DMKIND AS K ON C.idkind = K.idkind LEFT OUTER JOIN ";
                        sql +=" dbo.DMQUOTATIONTYPE AS QT ON Q.idquotationtype = QT.idquotationtype ";
                        sql +=" GROUP BY QD.idcommodity, C.commodity, U.unit, QT.quotationtype, ST.statusquotation, G.groupname, K.kind, Q.idquotationtype, Q.idstatusquotation ";
                        sql +=" HAVING 1=1 ";
                        if (glue_idquotationtype_I1.EditValue != "")
                        {
                            sql += " AND Q.idquotationtype ='"+ glue_idquotationtype_I1.EditValue +"' ";
                        }
                        if (glue_idstatusquotation_I1.EditValue != "")
                        {
                            sql += " AND Q.idstatusquotation ='" + glue_idstatusquotation_I1.EditValue + "' ";
                        }

                    dts = APCoreProcess.APCoreProcess.Read(sql);

                }
                
                gct_list_C.DataSource = dts;
                if (rad_phanloai_S.EditValue.ToString() == "True")
                {
                    gv_EXPORTDETAIL_C.Columns["quotationtype"].Group();
                }
                else
                {
                    gv_EXPORTDETAIL_C.Columns["statusquotation"].Group();
                }
                gv_EXPORTDETAIL_C.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void rad_phanloai_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rad_phanloai_S.EditValue.ToString() == "True")
            {
                lbl_loaisp_S.Visible = false;
                glue_idgrouptk_I1.Visible = false;
                glue_idgrouptk_I1.EditValue = "";

                lbl_nhanvien_S.Visible = true;
                glue_IDEMP_I1.Visible = true;
                lbl_bophan_S.Visible = true;
                glue_idquotationtype_I1.Visible = true;
            }
            else
            {
                lbl_nhanvien_S.Visible = false;
                glue_IDEMP_I1.Visible = false;
                lbl_bophan_S.Visible = false;
                glue_idquotationtype_I1.Visible = false;
                glue_IDEMP_I1.EditValue = "";
                glue_idquotationtype_I1.EditValue = "";

                lbl_loaisp_S.Visible = true;
                glue_idgrouptk_I1.Visible = true;
            }
        }
        #endregion
    }
}