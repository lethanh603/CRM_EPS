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
    public partial class frmSearchQuotation : DevExpress.XtraEditors.XtraForm
    {
        public frmSearchQuotation()
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
                loadGridLookupProvider();
                loadGridLookupEmployee();
                loadGridLookupMethod();
                loadGridLookupCommodity();
                Init();
                loadGrid();
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
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã phiếu", "Khách hàng", "Nhân viên", "Ngày nhập", "Tình trạng", "Loại báo giá", "Số lượng", "Thành tiền", "Tiền tệ", "Tỷ giá", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idexport_S", "col_idcustomer_S", "col_IDEMP_S", "col_dateimport_S", "col_idstatusquotation_S", "col_idquotationtype_S", "col_quantity_S", "col_total_S", "col_idcurrency_S", "col_exchangerate_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "GridLookupEditColumn", "SpinEditColumn", "SpinEditColumn", "GridLookupEditColumn", "SpinEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "250", "150", "80", "200", "100", "80", "100", "80", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation  ", "select idquotationtype, quotationtype frm dmquotationtype where status=1", "select idcurrency, currency from dmcurrency where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "status", "idquotationtype", "currency" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatusquotation", "idquotationtype", "idcurrency" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[5, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Loại báo giá" }, { "ID", "Tiền tệ" } };

            string[,] fieldname_glue_col = new string[5, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstatusquotation", "statusquotation" }, { "idquotationtype", "quotationtype" }, { "idcurrency", "currency" } };
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
            string[] gluenulltext = new string[] { "", "", "","" , ""};
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
                    string[] caption = new string[] { "Mã loại", "Loại báo giá" };
                    string[] fieldname = new string[] { "idquotationtype", "quotationtype" };
                    string[] col_visible = new string[] { "True", "True" };
                    ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idquotationtype_I1, "select '' as idquotationtype, N'" + clsFunction.transLateText("Tất cả") + "' as quotationtype union select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, glue_idquotationtype_I1.Width * 1, col_visible);


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

        private void loadGridLookupCommodity()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True","True" };
                string[] caption = new string[] { "Mã hàng", "Tên hàng","Tìm kiếm" };
                string[] fieldname = new string[] { "idcommodity", "commodity","tenkhongdau" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcommodity_S, "select  '' as idcommodity, N'" + clsFunction.transLateText("Tất cả") + "' as commodity, '' as tenkhongdau union select idcommodity, commodity, tenkhongdau from DMCOMMODITY ", "commodity", "idcommodity", caption, fieldname, this.Name, glue_idcommodity_S.Width*2, col_visible);

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
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union select idcustomer, customer, tel, fax, address from dmcustomers where status=1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);

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
                    string strDt = "";
                    string str = "";
                    if (glue_IDEMP_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.IDEMP='" + glue_IDEMP_I1.EditValue.ToString() + "'";
                    }
                    if (glue_idcustomer_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.idcustomer='" + glue_idcustomer_I1.EditValue.ToString() + "'";
                    }
                    if (glue_idquotationtype_I1.EditValue.ToString() != "")
                    {
                        str += " and  QO.idquotationtype='" + glue_idquotationtype_I1.EditValue.ToString() + "'";
                    }

                    if (txt_idpurchase_IK1.Text != "")
                    {
                        str += " and  QO.idexport='" + txt_idpurchase_IK1.Text + "'";
                    }

                    if (glue_idcommodity_S.EditValue.ToString() != "")
                    {
                        strDt = " where idcommodity like '%" + glue_idcommodity_S.EditValue.ToString() + "%' ";
                    }
                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT   QO.quotationno,  QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, SUM(dt.quantity) AS quantity, SUM(dt.amountvat) AS amountvat, SUM(dt.total) AS total FROM dbo.QUOTATION AS QO INNER JOIN (select * from dbo.QUOTATIONDETAIL "+strDt+") AS dt ON QO.idexport = dt.idexport INNER JOIN dbo.EMPLOYEES AS EM ON QO.IDEMP = EM.IDEMP WHERE     (QO.isdelete = 0) GROUP BY QO.quotationno, QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, EM.idrecursive HAVING (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime)) AND (CHARINDEX('" + clsFunction.GetIDEMPByUser() + "', EM.idrecursive) > 0) ");
                }
                gct_list_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {
            gv_EXPORTDETAIL_C.Columns["idcustomer"].FilterInfo = new ColumnFilterInfo("[idcustomer] LIKE '%" + glue_idcustomer_I1.EditValue.ToString() + "%'");
        }

        private void glue_IDEMP_I1_EditValueChanged(object sender, EventArgs e)
        {
            gv_EXPORTDETAIL_C.Columns["IDEMP"].FilterInfo = new ColumnFilterInfo("[IDEMP] LIKE '%" + glue_IDEMP_I1.EditValue.ToString() + "%'");
        }

        private void glue_idquotationtype_I1_EditValueChanged(object sender, EventArgs e)
        {
            gv_EXPORTDETAIL_C.Columns["idquotationtype"].FilterInfo = new ColumnFilterInfo("[idquotationtype] LIKE '%" + glue_idquotationtype_I1.EditValue.ToString() + "%'");
        }
    }
}