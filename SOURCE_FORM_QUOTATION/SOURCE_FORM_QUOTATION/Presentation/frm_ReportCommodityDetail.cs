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
using DevExpress.XtraGrid.Columns;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_ReportCommodityDetail: DevExpress.XtraEditors.XtraForm
    {
        public frm_ReportCommodityDetail()
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
                Load_Grid();
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                loadGridLookupEmployee();
                loadGridLookupCommodity();
                loadGridLookupProvider();
                loadGridLookupStatus();
                loadGridLookupMethod();
                loadGridLookupGroupTk();
                Init();
                loadGrid();
            }
        }

        private void loadGridLookupProvider()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
            if (e.KeyCode == Keys.F11)
            {
                btn_allow_print_detail_S.PerformClick();
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
            gv_EXPORTDETAIL_C.Columns["idcustomer"].FilterInfo = new ColumnFilterInfo("[idcustomer] LIKE '%" + glue_idcustomer_I1.EditValue.ToString() + "%'");
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[4] {"quantity","amount","amountvat","total" };
            // Caption column
            string[] caption_col = new string[] { "ID","ID ", "Số BG", "Khách hàng", "Nhân viên", "Ngày BG", "Tình trạng", "Loại báo giá","Mã hàng","Tên hàng","Nhóm hàng","Model","Thông số","Thông tin thiết bị", "Số lượng", "Đơn giá", "Thành tiền","VAT","Thành tiền sau VAT", "Tiền tệ", "Loại SP", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddetail_S","col_idexport_S", "col_quotationno_S", "col_idcustomer_S", "col_IDEMP_S", "col_dateimport_S", "col_idstatusquotation_S", "col_idquotationtype_S","col_idcommodity_S","col_commodity_S","col_idgroup_S","col_model_S","col_spec_S","col_equipmentinfo_S", "col_quantity_S","col_price_S", "col_amount_S","col_amountvat","col_total_S", "col_idcurrency_S","col_idgrouptk", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "80", "120", "300", "150", "80", "120", "200", "100","250","200","200","200", "200", "100","100", "100","100","120", "60","150", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "False", "True", "True", "True", "True", "True", "True", "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "False", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation  ", "select idquotationtype, quotationtype from dmquotationtype where status=1", "select idgroup, groupname from dmgroup where status=1", "select idcurrency, currency from dmcurrency where status=1", "select idgrouptk, grouptk from dmgrouptk where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "statusquotation", "quotationtype", "groupname", "currency","grouptk" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatusquotation", "idquotationtype","idgroup", "idcurrency","idgrouptk" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[7, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Loại báo giá" }, { "Mã nhóm", "Tên nhóm" }, { "ID", "Tiền tệ" }, { "ID", "Loại SP" } };

            string[,] fieldname_glue_col = new string[7, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstusquotation", "statusquotation" }, { "idquotationtype", "quotationtype" }, { "idgroup", "groupname" }, { "idcurrency", "currency" }, { "idgrouptk", "grouptk" } };
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

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho", "Tiền tệ" };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_EXPORTDETAIL_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_EXPORTDETAIL_C, read_Only, hien_Nav,
                              dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
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
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_EXPORTDETAIL_C.OptionsBehavior.Editable = true;
                gv_EXPORTDETAIL_C.OptionsView.ColumnAutoWidth = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        #endregion

        #region Methods

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

        private void loadGridLookupStatus()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Tình trạng" };
                string[] fieldname = new string[] { "idstatusquotation", "statusquotation" };
                string[] col_visible = new string[] { "True", "True" };

                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idstatusquotation_S, "select '' as idstatusquotation, N'" + clsFunction.transLateText("Tất cả") + "' as statusquotation union select idstatusquotation, statusquotation from dmstatusquotation ", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, glue_idstatusquotation_S.Width * 1, col_visible);
                glue_idstatusquotation_S.EditValue = "";

                //ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupGroupTk()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Loại SP" };
                string[] fieldname = new string[] { "idgrouptk", "grouptk" };
                string[] col_visible = new string[] { "True", "True" };

                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idgrouptk_S, "select '' as idgrouptk, N'" + clsFunction.transLateText("Tất cả") + "' as grouptk union select idgrouptk, grouptk from dmgrouptk ", "grouptk", "idgrouptk", caption, fieldname, this.Name, glue_idgrouptk_S.Width * 1, col_visible);
                glue_idgrouptk_S.EditValue = "";

                //ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupMethod()
        {
            try
            {
                string[] caption = new string[] { "Mã loại", "Loại báo giá" };
                string[] fieldname = new string[] { "idquotationtype", "quotationtype" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idquotationtype_I1, "select '' as idquotationtype, N'" + clsFunction.transLateText("Tất cả") + "' as quotationtype union select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, glue_idquotationtype_I1.Width * 1, col_visible);
                //ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idstatusquotation_S, "select '' as idquotationtype, N'" + clsFunction.transLateText("Tất cả") + "' as quotationtype union select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, glue_idstatusquotation_S.Width * 1, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }


        private void loadGridLookupCommodity()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True", "True" };
                string[] caption = new string[] { "Mã hàng", "Tên hàng", "Tìm kiếm" };
                string[] fieldname = new string[] { "idcommodity", "commodity", "tenkhongdau" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcommodity_S, "select  '' as idcommodity, N'" + clsFunction.transLateText("Tất cả") + "' as commodity, '' as tenkhongdau union select idcommodity, commodity, tenkhongdau from DMCOMMODITY ", "commodity", "idcommodity", caption, fieldname, this.Name, glue_idcommodity_S.Width * 2, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
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
                    string str = "";
                    string strDt = "";
                    if (glue_IDEMP_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.IDEMP like '%" + glue_IDEMP_I1.EditValue.ToString() + "%'";
                    }
                    if (glue_idcustomer_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.idcustomer='" + glue_idcustomer_I1.EditValue.ToString() + "'";
                    }
                    if (glue_idquotationtype_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.idquotationtype='" + glue_idquotationtype_I1.EditValue.ToString() + "'";
                    }

                    if (glue_idcommodity_S.Text != "")
                    {
                        strDt += " where D.idcommodity like N'%" + glue_idcommodity_S.EditValue.ToString() + "%' ";
                    }
                    if (glue_idgrouptk_S.Text != "")
                    {
                        if(strDt == "")
                            strDt += " where D.idgrouptk like N'%" + glue_idgrouptk_S.EditValue.ToString() + "%' ";
                        else
                            strDt += " and D.idgrouptk like N'%" + glue_idgrouptk_S.EditValue.ToString() + "%' ";
                    }

                    if (glue_idstatusquotation_S.Text != "")
                    {
                        str += " and QO.idstatusquotation like N'%" + glue_idstatusquotation_S.EditValue.ToString() + "%' ";
                    }

                    String date_po = "QO.dateimport";
                    if (glue_idstatusquotation_S.EditValue.ToString() == "ST000004")
                    {
                        date_po = "QO.datepo";
                    }

                    // dts = APCoreProcess.APCoreProcess.Read("SELECT  QO.quotationno,  QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, SUM(dt.quantity) AS quantity, SUM(dt.amount) as amount ,SUM(dt.amountvat) AS amountvat, SUM(dt.total) AS total FROM dbo.QUOTATION AS QO INNER JOIN (select D.idexport,  D.status, D.quantity, D.amount, D.amountvat, D.total from dbo.QUOTATIONDETAIL D  " + strDt + ") AS dt ON QO.idexport = dt.idexport INNER JOIN (SELECT idemp, idcustomer from EMPCUS) EC ON QO.idcustomer = EC.idcustomer  INNER JOIN EMPLOYEES EM ON EC.idemp=EM.idemp  WHERE     (QO.isdelete = 0) AND dt.status = 1 " +str+ " AND ( " + checkQuyenQO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 ) GROUP BY QO.quotationno, QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation HAVING (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime)) ORDER BY idstatusquotation  ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT QO.idexport, QO.quotationno, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idquotationtype, QO.idcurrency, QO.idstatusquotation, CM.commodity, CM.spec, CM.model, CM.equipmentinfo, CM.description, DT.iddetail,DT.idgrouptk, DT.quantity, DT.price, DT.amount, DT.vat, DT.amountvat, DT.total, DT.note, CM.idgroup, DT.idcommodity FROM  dbo.QUOTATION AS QO INNER JOIN  (SELECT        idemp, idcustomer  FROM   dbo.EMPCUS) AS EC ON QO.idcustomer = EC.idcustomer INNER JOIN dbo.EMPLOYEES AS EM ON EC.idemp = EM.IDEMP INNER JOIN (SELECT * FROM dbo.QUOTATIONDETAIL D " + strDt + ") AS DT ON QO.idexport = DT.idexport INNER JOIN dbo.DMCOMMODITY AS CM ON DT.idcommodity = CM.idcommodity WHERE  (QO.isdelete = 0) " + str + "  AND (CAST(DATEDIFF(d, 0, " + date_po + ") AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND ( " + checkQuyenQO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 ) ORDER BY QO.idstatusquotation");

                }

                gct_list_C.DataSource = dts;
                gv_EXPORTDETAIL_C.ClearGrouping();
                if (rad_group_S.EditValue.ToString() == "1")
                {
                    gv_EXPORTDETAIL_C.Columns[9].Group();
                }
                else if (rad_group_S.EditValue.ToString() == "2")
                {
                    gv_EXPORTDETAIL_C.Columns[10].Group();
                }
                else if (rad_group_S.EditValue.ToString() == "3")
                {
                    gv_EXPORTDETAIL_C.Columns[3].Group();
                }
                else if (rad_group_S.EditValue.ToString() == "4")
                {
                    gv_EXPORTDETAIL_C.Columns[2].Group();
                }
                gv_EXPORTDETAIL_C.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        private String checkQuyenQO()
        {
            string dk = "";
            DataTable dt = APCoreProcess.APCoreProcess.Read("select idemp from employees where idemp='" + clsFunction.GetIDEMPByUser() + "' and ismanager =1 ");
            if (dt.Rows.Count > 0)
            {
                dk = " 1=1 OR ";
            }
            return dk;
        }

        #endregion

        private void btn_allow_print_detail_S_Click(object sender, EventArgs e)
        {
            if (gv_EXPORTDETAIL_C.FocusedRowHandle < 0)
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn báo giá cần xem");
                return;
            }
            frm_ReportQuotationDetail frm = new frm_ReportQuotationDetail();
            frm.idexport = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void gv_EXPORTDETAIL_C_DoubleClick(object sender, EventArgs e)
        {
            btn_allow_print_detail_S_Click(sender, e);
        }

        private void btn_print_S_Click(object sender, EventArgs e)
        {
            XtraReport report = null;

            report = XtraReport.FromFile(Application.StartupPath + "\\Report\\xrp_quotation_detail.repx", true);
            //report.FindControl("xxx", true).Text="alo";
            clsFunction.BindDataControlReport(report);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string str = "";
            string strDt = "";
            if (glue_IDEMP_I1.EditValue.ToString() != "")
            {
                str += " and  QO.IDEMP like '%" + glue_IDEMP_I1.EditValue.ToString() + "%'";
            }
            if (glue_idcustomer_I1.EditValue.ToString() != "")
            {
                str += " and  QO.idcustomer='" + glue_idcustomer_I1.EditValue.ToString() + "'";
            }
            if (glue_idquotationtype_I1.EditValue.ToString() != "")
            {
                str += " and  QO.idquotationtype='" + glue_idquotationtype_I1.EditValue.ToString() + "'";
            }

            if (glue_idcommodity_S.Text != "")
            {
                strDt += " where D.idcommodity like N'%" + glue_idcommodity_S.EditValue.ToString() + "%' ";
            }
            if (glue_idgrouptk_S.Text != "")
            {
                if (strDt == "")
                    strDt += " where D.idgrouptk like N'%" + glue_idgrouptk_S.EditValue.ToString() + "%' ";
                else
                    strDt += " and D.idgrouptk like N'%" + glue_idgrouptk_S.EditValue.ToString() + "%' ";
            }

            if (glue_idstatusquotation_S.Text != "")
            {
                str += " and QO.idstatusquotation like N'%" + glue_idstatusquotation_S.EditValue.ToString() + "%' ";
            }
            // dt = APCoreProcess.APCoreProcess.Read("SELECT QO.idexport, QO.quotationno, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idquotationtype, QO.idcurrency, QO.idstatusquotation, CM.commodity, CM.spec, CM.model, CM.equipmentinfo, CM.description, DT.iddetail,DT.idgrouptk, DT.quantity, DT.price, DT.amount, DT.vat, DT.amountvat, DT.total, DT.note, CM.idgroup, DT.idcommodity , CS.customer, EM.StaffName, QT.quotationtype, QS.statusquotation, GT.grouptk INNER JOIN dbo.DMCUSTOMERS AS CS ON QO.idcustomer = CS.idcustomer INNER JOIN dbo.DMSTATUSQUOTATION AS QS ON QO.idstatusquotation = QS.idstatusquotation INNER JOIN    dbo.DMQUOTATIONTYPE AS QT ON QO.idquotationtype = QT.idquotationtype LEFT OUTER JOIN  dbo.DMGROUPTK AS GT ON DT.idgrouptk = GT.idgrouptk FROM  dbo.QUOTATION AS QO INNER JOIN  (SELECT        idemp, idcustomer  FROM   dbo.EMPCUS) AS EC ON QO.idcustomer = EC.idcustomer INNER JOIN dbo.EMPLOYEES AS EM ON EC.idemp = EM.IDEMP INNER JOIN (SELECT * FROM dbo.QUOTATIONDETAIL D " + strDt + ") AS DT ON QO.idexport = DT.idexport INNER JOIN dbo.DMCOMMODITY AS CM ON DT.idcommodity = CM.idcommodity WHERE  (QO.isdelete = 0) " + str + "  AND (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND ( " + checkQuyenQO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 ) ORDER BY QO.idstatusquotation");
            String sql = "SELECT  QO.idexport, QO.quotationno, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idquotationtype, QO.idcurrency, QO.idstatusquotation, ";
            sql += " CM.commodity, N'Từ ngày: "  + Convert.ToDateTime( dte_fromdate_S.EditValue).ToString("dd/MM/yyyy") +" Đến ngày: "+ Convert.ToDateTime( dte_todate_S.EditValue).ToString("dd/MM/yyyy") +"' as spec, CM.model, CM.equipmentinfo, CM.description, DT.iddetail, DT.idgrouptk, DT.quantity, DT.price, DT.amount, DT.vat, DT.amountvat, DT.total, ";
            sql +=" DT.note, CM.idgroup, DT.idcommodity, CS.customer, '"+ clsFunction.GetEmpNameByUser() +"' as StaffName, QT.quotationtype, QS.statusquotation, GT.grouptk";
            sql +=" FROM            dbo.QUOTATION AS QO INNER JOIN";
            sql +=" (SELECT        idemp, idcustomer";
            sql +=" FROM            dbo.EMPCUS) AS EC ON QO.idcustomer = EC.idcustomer INNER JOIN";
            sql +=" dbo.EMPLOYEES AS EM ON EC.idemp = EM.IDEMP INNER JOIN";
            sql +=" (SELECT  * ";
            sql +=" FROM            dbo.QUOTATIONDETAIL AS D";
            sql += strDt +" ) AS DT ON QO.idexport = DT.idexport INNER JOIN";
            sql +=" dbo.DMCOMMODITY AS CM ON DT.idcommodity = CM.idcommodity INNER JOIN";
            sql +=" dbo.DMCUSTOMERS AS CS ON QO.idcustomer = CS.idcustomer INNER JOIN";
            sql +=" dbo.DMSTATUSQUOTATION AS QS ON QO.idstatusquotation = QS.idstatusquotation INNER JOIN";
            sql +=" dbo.DMQUOTATIONTYPE AS QT ON QO.idquotationtype = QT.idquotationtype LEFT OUTER JOIN";
            sql +=" dbo.DMGROUPTK AS GT ON DT.idgrouptk = GT.idgrouptk";
            sql += " WHERE        (QO.isdelete = 0) " + str + "  AND (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND ( " + checkQuyenQO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 ) ORDER BY QO.idstatusquotation";
            dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
            {
                ds.Tables.Add(dt);
                report.DataSource = ds;
                ReportPrintTool tool = new ReportPrintTool(report);
                for (int i = 0; i < report.Parameters.Count; i++)
                {
                    report.Parameters[i].Visible = false;
                }

                tool.ShowPreviewDialog();
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Không tìm thấy dữ liệu hoặc chưa nhập thông tin hàng hóa, vui lòng kiểm tra lại.");
            }
        }
    }
}