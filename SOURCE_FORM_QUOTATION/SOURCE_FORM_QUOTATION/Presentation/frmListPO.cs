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
using GemBox.Spreadsheet;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frmListPO : DevExpress.XtraEditors.XtraForm
    {
        public frmListPO()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "PO";
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
                SaveGridControlsHistory();
                clsFunction.Save_sysControl(this, this);
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);

                Load_Grid();
                Load_Grid_Detail();
                Load_GridHistory();
                loadGridLookupProvider();
                loadGridLookupEmployee();
                loadGridLookupMethod();
                loadGridLookupStatus();
                Load_Grid_Procedure();
                loadProcedure();
                Init();
                // ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcustomer"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ORDER BY C.idcustomer");
            }
            if (checkAdmin() == true)
            {
                bbi_allow_print.Visibility = BarItemVisibility.Always;
                btn_allow_export_excel_S.Visibility = BarItemVisibility.Always;
            }
            else
            {
                bbi_survey_allow_delete_S.Visibility = BarItemVisibility.Never;
                bbi_allow_print.Visibility = BarItemVisibility.Never;
            }

            if (!clsFunction.checkAdmin())
            {
                tap_detail_C.PageVisible = true;
                tap_history_C.PageVisible = false;
                tap_invoice_C.PageVisible = false;
                xtap_info_C.PageVisible = false;
            }
        }

        private void frmSearchPurchar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                bbi_allow_access_detail_S.PerformClick();
            }
            if (e.KeyCode == Keys.F10)
            {
                btn_search_S.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                bbi_allow_exit_S.PerformClick();
            }
            if (e.KeyCode == Keys.F5)
            {
                btn_update_S.PerformClick();
            }
        }

        #endregion

        #region ButtonEvent

        private void gv_PURCHASEDETAIL_C_DoubleClick(object sender, EventArgs e)
        {
            bbi_allow_access_detail_S.PerformClick();
        }

        private void getValue(bool val)
        {
            if (val == true)
            {
                btn_search_S.PerformClick();
            }
        }

        private void getValueProcedure(bool val)
        {
            if (val == true)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from DMPROCEDURE where status=1 order by pos");
                gct_Procedure_C.DataSource = dt;
            }
        }

        private void getValuePO(bool val)
        {
            if (val == true)
            {
                int index1 = gv_EXPORTDETAIL_C.FocusedRowHandle;
                int index2 = gv_history_C.FocusedRowHandle;
                loadGridHistory(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());
                btn_search_S.PerformClick();
                gv_EXPORTDETAIL_C.FocusedRowHandle = index1;
                gv_history_C.FocusedRowHandle = index2;
            }
        }

        private void bbi_allow_access_search_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadGrid();
        }

        private void bbi_allow_access_detail_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //strpassData(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());             
                frm_QUOTATION_S frm = new frm_QUOTATION_S();
                frm.txt_idexport_IK1.Text = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
                frm.TopLevel = true;
                frm.calForm = true;
                frm.WindowState = FormWindowState.Maximized;
                frm.Dock = DockStyle.None;
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.passData = new frm_QUOTATION_S.PassData(getValue);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_allow_print_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                String str = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "quotationno").ToString();
                string dk = "(";

                if (str.Length > 0)
                {
                    String[] arr = str.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i < arr.Length - 1)
                        {
                            dk += "'" + arr[i].ToString().Trim() + "'" + ",";
                        }
                        else
                        {
                            dk += "'" + arr[i].ToString().Trim() + "'" + ")";
                        }
                    }

                }

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                String sql = "";
                sql = "SELECT dieukienthanhtoan, hieulucbaogia, ROW_NUMBER() over (order by (select QD.iddetail)) as _index, TT.currency, QO.chatluong, QO.phat, QO.dieukhoan,  QO.vuotdinhmuc, EE.StaffName,EE.tel as telnv ,   CU.customer, QO.invoiceeps quotationno, CC.contactname +' - '+ CC.tel as receiver, CU.tel, CU.fax, CU.idcustomer, QO.dateimport, QO.placedelivery, QO.prepaypercent, QO.postpaidpercent, QD._index, CC.email,  CO.commodity, UT.unit, QD.spec, QD.quantity,QD.cogs , QD.price, QD.amount, 	  QD.vat, QD.partnumber, QD.timedelivery, QD.note, QO.idexport, QD.description, QD.equipmentinfo, CU.address, CU.tax,(select sum(amount*d.vat/100) from quotationdetail d inner join quotation q on d.idexport = q.idexport  where q.quotationno in " + dk + "  and d.status =1) as tienvat, (select sum(amount*quotationdetail.vat/100+amount) from quotationdetail inner join quotation on quotationdetail.idexport = quotation.idexport  where quotation.quotationno in " + dk + " and quotationdetail.status =1) as tiensauvat, [dbo].[Num2Text]((select sum(amount*d.vat/100+amount) from quotationdetail d inner join quotation q on d.idexport = q.idexport  where q.quotationno in " + dk + " and d.status =1 )) tienchu FROM         dbo.QUOTATION AS QO LEFT JOIN    dbo.QUOTATIONDETAIL AS QD ON QO.idexport = QD.idexport LEFT JOIN      dbo.DMCUSTOMERS AS CU ON QO.idcustomer = CU.idcustomer LEFT JOIN    dbo.DMCOMMODITY AS CO ON QD.idcommodity = CO.idcommodity LEFT JOIN    dbo.DMUNIT AS UT ON QD.idunit = UT.idunit LEFT JOIN CUSCONTACT CC	  ON CC.idcontact=QO.receiver  LEFT JOIN EMPLOYEES EE ON EE.IDEMP = QO.IDEMP LEFT JOIN      dbo.DMCURRENCY AS TT ON QO.idcurrency = TT.idcurrency WHERE     (QO.quotationno in " + dk + " and QD.status =1)  ORDER BY QD._index";
                dt = APCoreProcess.APCoreProcess.Read(sql);

                XtraReport report = null;

                if (gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idquotationtype").ToString() == "QT000001")
                {
                    report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_PO_TB_S.repx", true);
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idquotationtype").ToString() == "QT000002")
                {
                    report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_PO_PhuTung_S.repx", true);
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idquotationtype").ToString() == "QT000003")
                {
                    report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_PO_ThueXe_S.repx", true);
                }
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);

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
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi " + ex.Message);
            }
        }



        private void bbi_allow_exit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbi_reload_exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSearchPurchar_Load(sender, e);
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
            string[] caption_col = new string[] { "Nhân viên", "Mã PO", "Ngày PO", "Loại PO (Quy trình)", "Mã KH", "Tên KH", "Tiền tạm ứng", "Chi phí PO", "Ngày GH (yêu cầu)", "Ngày GH (thực tế)", "Ngày nhận BBNT từ KH", "Cảnh báo", "Tình trạng PO", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_staffname_S", "col_invoiceeps_S", "col_datepo_S", "col_quotationtype_S", "col_idcustomer_S", "col_customer_S", "col_tamung_S", "col_chiphi_S", "col_daterequire_S", "col_datedelivery_S", "col_datebbnt_S", "col_warning_S", "col_idstatuspo_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "DateColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "SpinEditColumn", "SpinEditColumn", "DateColumn", "DateColumn", "DateColumn", "TextColumn", "GridLookupEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "150", "90", "100", "150", "100", "300", "100", "100", "100", "100", "100", "100", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "True", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idpotype, potype from dmpotype where status=1", "select idstatusprocedure, statusprocedure from statusprocedure where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "quotationtype", "statusprocedure" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idpotype", "idstatusprocedure" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "ID", "Loại PO" }, { "ID", "Tình trạng" } };

            string[,] fieldname_glue_col = new string[2, 2] { { "idntype", "potype" }, { "idstatusprocedure", "statusprocedure" } };
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
            string[] gluenulltext = new string[] { };
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
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;
                gv_EXPORTDETAIL_C.Columns["idpotype"].Group();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Detail()
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
            gv_detail_C.OptionsView.ShowAutoFilterRow = false;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_detail_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_detail_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_detail_C,
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
                gv_detail_C.OptionsBehavior.Editable = false;
                gv_detail_C.OptionsView.ColumnAutoWidth = false;
                gv_detail_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControlsHistory()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "idpohistory", "Loại PO", "Trình tự", "Nội dung công việc", "Người yêu cầu", "Ngày yêu cầu", "Người thực hiện", "Ngày kết thúc", "Chứng từ số", "Tạm ứng", "Chi phí PO", "Sự cố", "Nguyên nhân", "Hướng giải quyết", "Tình trạng", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idpohistory_S", "col_idpotype_S", "col_pos_S", "col_procedurename_S", "col_idemprequire_S", "col_daterequire_S", "col_idempaction_S", "col_dateend_S", "col_vouchers_S", "col_tamung_S", "col_chiphi_S", "col_proplem_S", "col_causal_S", "col_solution_S", "col_idstatusprocedure_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "TextColumn", "MemoColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "DateColumn", "TextColumn", "CalcEditColumn", "CalcEditColumn", "MemoColumn", "MemoColumn", "MemoColumn", "GridLookupEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "150", "100", "200", "150", "100", "150", "100", "100", "100", "100", "200", "200", "200", "200", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idpotype , potype  from dmpotype", "select idemp as idemprequire, staffname  from employees", "select idemp as idempaction, staffname  from EMPLOYEES", "select idstatusprocedure, statusprocedure from statusprocedure  " };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "potype", "staffname", "staffname", "statusprocedure" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idpotype", "idemprequire", "idempaction", "idstatusprocedure" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[4, 2] { { "ID", "Loại PO" }, { "Mã NV", "Người yêu cầu" }, { "Mã NV", "Người thực hiện" }, { "ID", "Tình trạng" } };

            string[,] fieldname_glue_col = new string[4, 2] { { "idpotype", "potype" }, { "idemprequire", "staffname" }, { "idempaction", "staffname" }, { "idstusprocedure", "statusprocedure" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_history_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

        private void Load_GridHistory()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_history_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_history_C, read_Only, hien_Nav,
                              dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_history_C,
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
                gv_history_C.OptionsBehavior.Editable = false;
                gv_history_C.OptionsView.ColumnAutoWidth = false;
                gv_history_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Load_Grid_Procedure()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_procedure_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_procedure_C, read_Only, hien_Nav,
                              dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_Procedure_C,
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
                gv_procedure_C.OptionsBehavior.Editable = true;
                gv_procedure_C.OptionsView.ColumnAutoWidth = false;
                gv_procedure_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gv_EXPORTDETAIL_C_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //if (e.Column.Name == "idstatusquotation")
                //{
                //    e.Column.AppearanceCell.Options.UseBackColor = true;
                //    string idstatus = View.GetRowCellValue(e.RowHandle, "idstatusquotation").ToString();
                //    if (idstatus == "ST000001")
                //    {
                //        e.Appearance.BackColor = Color.White;
                //        e.Appearance.BackColor2 = Color.White;
                //    }
                //    if (idstatus == "ST000002")
                //    {
                //        e.Appearance.BackColor = Color.PaleGoldenrod;
                //        e.Appearance.BackColor2 = Color.PaleGoldenrod;
                //    }
                //    if (idstatus == "ST000003")
                //    {
                //        e.Appearance.BackColor = Color.LimeGreen;
                //        e.Appearance.BackColor2 = Color.LimeGreen;
                //    }
                //}
            }
        }

        #endregion

        #region Methods

        private void loadGridLookupMethod()
        {
            try
            {
                string[] caption = new string[] { "Mã loại", "Loại báo giá" };
                string[] fieldname = new string[] { "idpotype", "potype" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idpotype_I1, " select '' as idpotype, N'" + clsFunction.transLateText("Tất cả") + "' as potype union select idpotype, potype from dmpotype where status=1 ", "potype", "idpotype", caption, fieldname, this.Name, glue_idpotype_I1.Width * 1, col_visible);

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
                if (clsFunction.checkAdmin() || clsFunction._user=="hchanh02")
                {
                    ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, " select IDEMP, StaffName from EMPLOYEES where idemp = '" + Function.clsFunction.GetIDEMPByUser() + "'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);

                }

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupProvider()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' AND C.status='True' ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        // Priority
        private void loadGridLookupStatus()
        {
            string[] caption = new string[] { "ID", "Tình trạng" };
            string[] fieldname = new string[] { "idstatusprocedure", "statusprocedure" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatuspo_S, "select idstatusprocedure, statusprocedure from statusprocedure", "statusprocedure", "idstatusprocedure", caption, fieldname, this.Name, glue_idstatuspo_S.Width);
        }
        private void Init()
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dte_todate_S.EditValue = DateTime.Now;
            btn_search_S.PerformClick();

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
                    if (glue_IDEMP_I1.EditValue.ToString() != "")
                    {
                        str += " and ( E.idemp = N'" + glue_IDEMP_I1.EditValue.ToString() + "' or idemppo='" + glue_IDEMP_I1.EditValue.ToString() + "' )";
                    }
                    if (glue_idcustomer_I1.EditValue.ToString() != "")
                    {
                        str += " and  QS.idcustomer='" + glue_idcustomer_I1.EditValue.ToString() + "'";
                    }
                    if (glue_idpotype_I1.EditValue.ToString() != "")
                    {
                        str += " and  QS.idpotype='" + glue_idpotype_I1.EditValue.ToString() + "'";
                    }

                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT   QS.quotationno + ','+QS.sobgnhap as quotationno ,  E.StaffName as staffname, QS.invoiceeps, QS.datepo, QS.idpotype, QS.idcustomer, CS.customer, QS.datebbnt, QS.datedelivery, QS.daterequire, QS.idstatusprocedure, QS.note, QS.idexport, QS.idquotationtype,  SUM(ISNULL(H.chiphi, 0)) AS chiphi, SUM(ISNULL(H.tamung, 0)) AS tamung FROM dbo.QUOTATION AS QS INNER JOIN dbo.DMCUSTOMERS AS CS ON QS.idcustomer = CS.idcustomer INNER JOIN dbo.EMPLOYEES AS E ON QS.IDEMP = E.IDEMP LEFT OUTER JOIN   dbo.POHISTORY AS H ON QS.idexport = H.idquotation GROUP BY E.StaffName, QS.quotationno, QS.invoiceeps, QS.datepo, QS.idpotype, QS.idcustomer, CS.customer, QS.datebbnt, QS.datedelivery, QS.idquotationtype, QS.daterequire, QS.idstatusprocedure, QS.note, QS.idexport, QS.sobgnhap, idstatusquotation,E.idrecursive, idemppo, E.idemp HAVING  (( " + checkQuyenPO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',E.idrecursive) >0 ) or idemppo='" + clsFunction.GetIDEMPByUser() + "') AND  (QS.idstatusquotation = N'ST000004') AND (CAST(DATEDIFF(d, 0, QS.datepo) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime)) " + str + " ORDER BY idstatusquotation  ");
                }
                gct_list_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private String checkQuyenPO()
        {
            string dk = "";
            DataTable dt = APCoreProcess.APCoreProcess.Read("select idemp from employees where idemp='" + clsFunction.GetIDEMPByUser() + "' and ismanagerpo =1 ");
            if (dt.Rows.Count > 0)
            {
                dk = " 1=1 OR ";
            }
            return dk;
        }

        private void loadGridHistory(String idquotation)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from POHISTORY WHERE idquotation='" + idquotation + "' and idpotype='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idpotype").ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    dt = APCoreProcess.APCoreProcess.Read("select idprocedure, procedurename, pos, idpotype from DMPROCEDURE where idpotype ='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idpotype").ToString() + "' order by idpotype, pos");
                    dt.Columns.Add("idpohistory", typeof(String));
                    dt.Columns.Add("idemprequire", typeof(String));
                    dt.Columns.Add("daterequire", typeof(DateTime));
                    dt.Columns.Add("idempaction", typeof(String));
                    dt.Columns.Add("dateend", typeof(DateTime));
                    dt.Columns.Add("invoice", typeof(String));
                    dt.Columns.Add("tamung", typeof(double));
                    dt.Columns.Add("chiphi", typeof(double));
                    dt.Columns.Add("proplem", typeof(String));
                    dt.Columns.Add("causal", typeof(String));
                    dt.Columns.Add("solution", typeof(String));
                    dt.Columns.Add("idstatusprocedure", typeof(String));
                    dt.Columns.Add("note", typeof(String));

                    DataTable dtup = new DataTable();
                    dtup = APCoreProcess.APCoreProcess.Read("POHISTORY where idquotation ='" + idquotation + "' and idpotype = '" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idpotype").ToString() + "'");
                    if (dtup.Rows.Count == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dtup.NewRow();
                            dr["idpohistory"] = clsFunction.layMa("HS", "idpohistory", "POHISTORY");
                            dr["idprocedure"] = dt.Rows[i]["idprocedure"];
                            dr["idquotation"] = idquotation;
                            dr["procedurename"] = dt.Rows[i]["procedurename"];
                            dr["idpotype"] = dt.Rows[i]["idpotype"];
                            dr["pos"] = dt.Rows[i]["pos"];
                            dtup.Rows.Add(dr);
                            APCoreProcess.APCoreProcess.Save(dr);

                        }
                    }
                    gct_history_C.DataSource = dtup;
                }
                else
                {
                    gct_history_C.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadProcedure()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from dmprocedure where status =1 order by idpotype, pos");
            gct_Procedure_C.DataSource = dt;
        }

        #endregion

        private byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }


        private void gv_history_C_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //strpassData(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());             
                frm_POHISTORY_S frm = new frm_POHISTORY_S();
                frm.ID = gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "idpohistory").ToString();
                frm.TopLevel = true;
                frm._insert = false;
                frm.procedure = gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "procedurename").ToString();
                frm.idquotation = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
                frm.idpotype = gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "idpotype").ToString();
                frm.idprocedure = gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "idprocedure").ToString();
                frm.pos = Convert.ToInt16(gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "pos"));
                frm.WindowState = FormWindowState.Normal;
                frm.Dock = DockStyle.None;
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.passData = new frm_POHISTORY_S.PassData(getValuePO);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }


        private int findIndexInArray(string value, string[] arr)
        {
            int index = -1;
            for (int i = 0; i < arr.Length; i++)
                if (value == arr[i])
                {
                    index = i;
                    break;
                }
            return index;
        }


        private void gv_EXPORTDETAIL_C_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                string status = gv_EXPORTDETAIL_C.GetRowCellValue(e.RowHandle, "idstatusquotation").ToString();
                if (status == "ST000006")
                {
                    e.Appearance.BackColor = Color.White;
                }
                if (status == "ST000001")
                {
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                }
                if (status == "ST000002")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                if (status == "ST000003")
                {
                    e.Appearance.BackColor = Color.LavenderBlush;
                }
                if (status == "ST000004")
                {
                    e.Appearance.BackColor = Color.PaleTurquoise;
                }
                if (status == "ST000005")
                {
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }
            catch { }
        }

        private void gv_EXPORTDETAIL_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                {
                    loadGridHistory(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());

                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("select * from quotation where idexport = '" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "' ");
                    if (dt.Rows.Count > 0)
                    {

                        txt_invoiceno_S.Text = dt.Rows[0]["invoice"].ToString();
                        dte_datebbnt_I5.EditValue = dt.Rows[0]["datebbnt"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["datebbnt"]) : new DateTime();
                        dte_datedelivery_I5.EditValue = dt.Rows[0]["datedelivery"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["datedelivery"]) : new DateTime();
                        //glue_idstatuspo_S.EditValue = dt.Rows[0]["idstatuspo"].ToString();
                        mmo_note_S.Text = dt.Rows[0]["note"].ToString();

                    }

                    loadPO(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "quotationno").ToString());
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            loadGrid();
            // gv_EXPORTDETAIL_C.Columns["idcustomer"].FilterInfo = new ColumnFilterInfo("[idcustomer] LIKE '%" + glue_idcustomer_I1.EditValue + "%'");
        }

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {
            // gv_EXPORTDETAIL_C.Columns["idcustomer"].FilterInfo = new ColumnFilterInfo("[idcustomer] LIKE '%" + glue_idcustomer_I1.EditValue + "%'");
        }

        private void glue_IDEMP_I1_EditValueChanged(object sender, EventArgs e)
        {
            //if (glue_IDEMP_I1.EditValue != null && glue_IDEMP_I1.EditValue != "")
            //gv_EXPORTDETAIL_C.Columns["staffname"].FilterInfo = new ColumnFilterInfo("[staffname] LIKE '%" + glue_IDEMP_I1.Text.Trim() + "%'");
        }

        private void glue_idpotype_I1_EditValueChanged(object sender, EventArgs e)
        {
            //if (glue_idpotype_I1.EditValue != null && glue_idpotype_I1.EditValue != "")
            //    gv_EXPORTDETAIL_C.Columns["idpotype"].FilterInfo = new ColumnFilterInfo("[idpotype] LIKE '%" + glue_idpotype_I1.EditValue + "%'");
        }

        private bool checkAdmin()
        {
            bool flag = false;
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where root = 1 AND userid = '" + clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        private void btn_update_S_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from quotation where invoiceeps='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "invoiceeps").ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dte_datedelivery_I5.EditValue != null && dte_datedelivery_I5.EditValue != "")
                        dr["datedelivery"] = dte_datedelivery_I5.EditValue;
                    if (dte_daterequire_S.EditValue != null && dte_daterequire_S.EditValue != "")
                        dr["daterequire"] = dte_daterequire_S.EditValue;
                    if (dte_datebbnt_I5.EditValue != null && dte_datebbnt_I5.EditValue != "")
                        dr["datebbnt"] = dte_datebbnt_I5.EditValue;
                    dr["invoice"] = txt_invoiceno_S.Text;
                    dr["note"] = mmo_note_S.Text;
                    dr["idstatusprocedure"] = glue_idstatuspo_S.EditValue.ToString();
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_survey_allow_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S frm = new SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S();
                frm._insert = true;
                frm._sign = "KS";
                frm.txt_idprocedure_IK1.Text = gv_procedure_C.GetRowCellValue(gv_procedure_C.FocusedRowHandle, "idprocedure").ToString();
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S.PassData(getValueProcedure);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_survey_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            // Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idprocedure", this, gv_list_C.Columns["idprocedure"], bbi_allow_delete.Name, "DMCUSTOMERS", "idprovince");
            clsFunction.Delete_M_Not_Pre("DMPROCEDURE", gv_procedure_C, "idprocedure", this, gv_procedure_C.Columns["idprocedure"], bbi_survey_allow_delete_S.Caption, "idprocedure");
        }

        private void bbi_survey_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S frm = new SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_procedure_C.GetRowCellValue(gv_procedure_C.FocusedRowHandle, "idprocedure").ToString();
                frm.passData = new SOURCE_FORM_DMPROCEDURE.Presentation.frm_DMPROCEDURE_S.PassData(getValueProcedure);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bar_menu_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "'");
                if (dt.Rows.Count > 0)
                {
                    strColName = dt.Rows[0][0].ToString();
                    strCol = dt.Rows[0][1].ToString();
                    string[] arrayColName = strColName.Split('/');
                    string[] arrCol = strCol.Split('/');
                    if (e.Item.Name.Contains("_allow_col_") && (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos = findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]))
                        {
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible = clsFunction.ConvertArrayToString(arrCol);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "'");
                            Load_Grid();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void btn_allow_export_excel_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportExcelTeamplate(Application.StartupPath + "\\File\\PO_KETOAN.xltx", gv_detail_C);
        }


        private void exportExcelTeamplate(string path, GridView gv)
        {
            try
            {
                if (gv_EXPORTDETAIL_C.FocusedRowHandle < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn PO");
                    return;
                }
                DataTable dtM = APCoreProcess.APCoreProcess.Read("select q.*, e.staffname from quotation q inner join employees e on q.idemp = e.idemp where idexport ='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "' ");

                if (dtM.Rows.Count == 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Thông tin người liên hệ của khách hàng chưa có");
                    return;
                }
                DataTable dtKH = APCoreProcess.APCoreProcess.Read("select * from dmcustomers where idcustomer = '" + dtM.Rows[0]["idcustomer"].ToString() + "'");
                DataTable dtct = APCoreProcess.APCoreProcess.Read("select idcontact, contactname as contact, tel from cuscontact  where idcontact='" + dtM.Rows[0]["receiver"].ToString() + "'");
                String nguoilienhe = "";
                if (dtct.Rows.Count > 0)
                {
                    nguoilienhe = dtct.Rows[0]["contact"].ToString();
                }
                int iCol = 0;
                double amount = 0;
                double amountvat = 0;
                double total = 0;
                iCol = gv.Columns.Count;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook;
                Microsoft.Office.Interop.Excel.Worksheet worksheet;
                workbook = xlApp.Workbooks.Open(path);
                
                xlApp.Visible = false;
                worksheet = workbook.Worksheets[1];
                workbook.Worksheets[1].Cells[3, 5] = "(Kèm " + dtM.Rows[0]["quotationno"].ToString() + ")";
                workbook.Worksheets[1].Cells[4, 7] = Convert.ToDateTime(dtM.Rows[0]["datepo"]).ToString("dd/MM/yyyy");
                workbook.Worksheets[1].Cells[5, 3] = nguoilienhe;
                workbook.Worksheets[1].Cells[6, 3] = dtKH.Rows[0]["customer"].ToString();
                if (dtM.Rows[0]["datedelivery"].ToString() != "")
                {
                    workbook.Worksheets[1].Cells[6, 11] = Convert.ToDateTime(dtM.Rows[0]["datedelivery"]).ToString("dd/MM/yyyy");
                }
                else 
                {
                    workbook.Worksheets[1].Cells[6, 11] = "";
                }
                workbook.Worksheets[1].Cells[7, 3] = dtKH.Rows[0]["address"].ToString();
                workbook.Worksheets[1].Cells[7, 11] = dtM.Rows[0]["limit"].ToString();
                workbook.Worksheets[1].Cells[8, 3] = APCoreProcess.APCoreProcess.Read("select tax from dmcustomers where idcustomer='" + dtM.Rows[0]["idcustomer"].ToString() + "' ").Rows[0][0].ToString();
                workbook.Worksheets[1].Cells[8, 11] = dtM.Rows[0]["quotationno"].ToString();
                workbook.Worksheets[1].Cells[9, 3] = dtM.Rows[0]["placedelivery"].ToString();
                workbook.Worksheets[1].Cells[9, 11] = dtM.Rows[0]["isdept"].ToString() == "0" ? "TM" : "CK";
                if (dtct.Rows.Count > 0)
                {
                    workbook.Worksheets[1].Cells[10, 3] = dtct.Rows[0]["tel"].ToString();
                }
                else
                {
                    workbook.Worksheets[1].Cells[10, 3] = dtKH.Rows[0]["tel"].ToString();
                }
                workbook.Worksheets[1].Cells[10, 3] = dtct.Rows[0]["tel"].ToString();
                workbook.Worksheets[1].Cells[10, 6] = dtKH.Rows[0]["fax"].ToString();
                workbook.Worksheets[1].Cells[10, 11] = dtM.Rows[0]["invoiceeps"].ToString();

                for (int i = 12; i < gv.RowCount + 12; i++)
                {
                    DataTable dtCommodity = new DataTable();
                    dtCommodity = APCoreProcess.APCoreProcess.Read("select * from dmcommodity where idcommodity = '" + gv_detail_C.GetRowCellValue(i - 12, "idcommodity").ToString() + "'");
                    // cot                   

                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 1], worksheet.Cells[i + 2, 1]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 1] = i - 11;


                    worksheet.Range[worksheet.Cells[i + 2, 2], worksheet.Cells[i + 2, 2]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 2], worksheet.Cells[i + 2, 2]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 2], worksheet.Cells[i + 2, 2]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 2], worksheet.Cells[i + 2, 2]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 2], worksheet.Cells[i + 2, 2]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 2] = dtCommodity.Rows[0]["idcommodity"].ToString().Trim();

                    worksheet.Range[worksheet.Cells[i + 2, 3], worksheet.Cells[i + 2, 3]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 3], worksheet.Cells[i + 2, 3]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 3], worksheet.Cells[i + 2, 3]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 3], worksheet.Cells[i + 2, 3]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 3], worksheet.Cells[i + 2, 3]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 3] = gv.GetRowCellDisplayText(i - 12, "partnumber").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 4], worksheet.Cells[i + 2, 4]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 4], worksheet.Cells[i + 2, 4]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 4], worksheet.Cells[i + 2, 4]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 4], worksheet.Cells[i + 2, 4]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 4], worksheet.Cells[i + 2, 4]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 4] = gv.GetRowCellDisplayText(i - 12, "spec").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 5], worksheet.Cells[i + 2, 5]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 5] = "";

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 6], worksheet.Cells[i + 2, 6]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 6], worksheet.Cells[i + 2, 6]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 6], worksheet.Cells[i + 2, 6]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 6], worksheet.Cells[i + 2, 6]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 6], worksheet.Cells[i + 2, 6]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 6] = gv.GetRowCellDisplayText(i - 12, "commodity").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 7], worksheet.Cells[i + 2, 7]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 7] = gv.GetRowCellDisplayText(i - 12, "quantity").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 8], worksheet.Cells[i + 2, 8]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 8], worksheet.Cells[i + 2, 8]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 8], worksheet.Cells[i + 2, 8]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 8], worksheet.Cells[i + 2, 8]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 8], worksheet.Cells[i + 2, 8]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 8] = gv.GetRowCellDisplayText(i - 12, "idunit").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 9], worksheet.Cells[i + 2, 9]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 9], worksheet.Cells[i + 2, 9]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 9], worksheet.Cells[i + 2, 9]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 9], worksheet.Cells[i + 2, 9]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 9], worksheet.Cells[i + 2, 9]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 9] = Convert.ToDouble(gv.GetRowCellValue(i - 12, "price")).ToString("N0").Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 10], worksheet.Cells[i + 2, 10]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    try
                    {
                        workbook.Worksheets[1].Cells[i + 2, 10] = Convert.ToDouble(gv.GetRowCellValue(i - 12, "cogs")).ToString("N0").Trim();
                    }
                    catch
                    {
                        workbook.Worksheets[1].Cells[i + 2, 10] = 0;
                    }
                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 11], worksheet.Cells[i + 2, 11]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 11], worksheet.Cells[i + 2, 11]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 11], worksheet.Cells[i + 2, 11]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 11], worksheet.Cells[i + 2, 11]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 11], worksheet.Cells[i + 2, 11]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 11] = Convert.ToDouble(gv.GetRowCellValue(i - 12, "amount")).ToString("N0");

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 12], worksheet.Cells[i + 2, 12]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 12], worksheet.Cells[i + 2, 12]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 12], worksheet.Cells[i + 2, 12]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 12], worksheet.Cells[i + 2, 12]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 12], worksheet.Cells[i + 2, 12]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 12] = gv.GetRowCellValue(i - 12, "equipmentinfo").ToString().Trim();

                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].Font.Size = 10;
                    worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].Font.Color = System.Drawing.Color.Black;
                    worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                    //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                    worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[i + 2, 13], worksheet.Cells[i + 2, 13]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[i + 2, 13] = gv.GetRowCellValue(i - 12, "note").ToString().Trim();
                    amount += Convert.ToDouble(gv.GetRowCellValue(i - 12, "amount"));
                    amountvat += Convert.ToDouble(gv.GetRowCellValue(i - 12, "amountvat"));
                    total += Convert.ToDouble(gv.GetRowCellValue(i - 12, "total"));
                }

                worksheet.Range[worksheet.Cells[gv.RowCount + 12 + 2, 1], worksheet.Cells[gv.RowCount + 12 + 2, 13]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                workbook.Worksheets[1].Cells[gv.RowCount + 12 + 2, 6] = "Cộng:";
                workbook.Worksheets[1].Cells[gv.RowCount + 12 + 2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 12 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 12 + 2, 11] = amount.ToString("N0");
                workbook.Worksheets[1].Cells[gv.RowCount + 12 + 2, 11].Font.FontStyle = "Bold";

                worksheet.Range[worksheet.Cells[gv.RowCount + 13 + 2, 1], worksheet.Cells[gv.RowCount + 13 + 2, 13]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 6] = "Thuế suất VAT:";
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 7] = dtM.Rows[0]["vat"].ToString() + " %";
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 11] = amountvat.ToString("N0");
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 11].Font.FontStyle = "Bold";

                worksheet.Range[worksheet.Cells[gv.RowCount + 14 + 2, 1], worksheet.Cells[gv.RowCount + 12 + 2, 13]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                workbook.Worksheets[1].Cells[gv.RowCount + 14 + 2, 6] = "Tổng thanh toán:";
                workbook.Worksheets[1].Cells[gv.RowCount + 14 + 2, 11] = total.ToString("N0");
                workbook.Worksheets[1].Cells[gv.RowCount + 14 + 2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 14 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 14 + 2, 11].Font.FontStyle = "Bold";

                workbook.Worksheets[1].Cells[gv.RowCount + 15 + 2, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                workbook.Worksheets[1].Cells[gv.RowCount + 15 + 2, 3].Font.FontStyle = "Bold Italic";
                workbook.Worksheets[1].Cells[gv.RowCount + 15 + 2, 3] = "(Bằng chữ: " + Function.ConvertNumToStr.So_chu(total) + ")";
                // chu ky
                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 2] = "Người đề nghị";

                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 5] = "TPKD";

                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 8] = "Quản lý đơn hàng";



                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workbook.Worksheets[1].Cells[gv.RowCount + 16 + 2, 11] = "Duyệt";

                workbook.Worksheets[1].Cells[gv.RowCount + 18 + 2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workbook.Worksheets[1].Cells[gv.RowCount + 18 + 2, 2] = dtM.Rows[0]["staffname"].ToString();

                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 10] = "HHNV: ";
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 10] = "HHKH: ";

                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 11] = dtM.Rows[0]["hhnv"].ToString() != "" ? (dtM.Rows[0]["hhnv"].ToString() + " %") : "";
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 11] = dtM.Rows[0]["hhkh"].ToString() != "" ? (dtM.Rows[0]["hhkh"].ToString() + " %") : "";

                xlApp.Visible = true;
                //worksheet.Columns.AutoFit();

                // If using Professional version, put your serial key below.
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void loadPO(string str)
        {
            try
            {
                if (str != "")
                {
                    DataTable dtD = new DataTable();
                    string dk = "(";


                    if (str.Length > 0)
                    {
                        String[] arr = str.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (i < arr.Length - 1)
                            {
                                dk += "'" + arr[i].ToString().Trim() + "'" + ",";
                            }
                            else
                            {
                                dk += "'" + arr[i].ToString().Trim() + "'" + ")";
                            }
                        }

                    }
                    //dtD = APCoreProcess.APCoreProcess.Read("SELECT   dt.status,  dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity,dt.price, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.sign, mh.commodity, dt.timedelivery, dt.spec, dt.partnumber, dt.cogs, dt.equipmentinfo, dt.description, Q.quotationno FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN QUOTATION Q on Q.idexport = dt.idexport where quotationno in " + dk + "  and dt.status=1 ORDER BY _index");
                    dtD = APCoreProcess.APCoreProcess.Read("SELECT   dt.status,  dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity,dt.price, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.sign, mh.commodity, dt.timedelivery, dt.spec, dt.partnumber, dt.cogs, dt.equipmentinfo, dt.description, Q.quotationno FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN QUOTATION Q on Q.idexport = dt.idexport where Q.idexport='"+ gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle,"idexport").ToString() +"' and dt.status=1 ORDER BY _index");
                    //if (dtD.Rows.Count > 0)
                    {
                        gct_detail_C.DataSource = dtD;
                    }
                }

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi: " + ex.Message);
            }
        }

        private void dte_todate_S_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbi_allow_print_po_list_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string str = "";
                /*if(glue_IDEMP_I1.EditValue.ToString() != "")
                {
                    str += " Staffname =" + glue_IDEMP_I1.Text + "";
                }
                if (glue_idcustomer_I1.EditValue.ToString() != "")
                {
                    str += " AND  Customer=" + glue_idcustomer_I1.Text+ "";
                }
                if (glue_idpotype_I1.Text!= "")
                {
                    str += " AND  PO TYPE=" + glue_idpotype_I1.Text + "";
                }*/
                str +="Từ ngày: " + Convert.ToDateTime( dte_fromdate_S.EditValue).ToString("dd/MM/yyyy") + " ~ " +  Convert.ToDateTime( dte_todate_S.EditValue).ToString("dd/MM/yyyy");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                String sql = "";
                //sql = "SELECT ROW_NUMBER() over (order by (select QD.iddetail)) as _index, TT.currency, QO.chatluong, QO.phat, QO.dieukhoan,  QO.vuotdinhmuc, EE.StaffName,EE.tel as telnv ,   CU.customer, QO.invoiceeps quotationno, CC.contactname +' - '+ CC.tel as receiver, CU.tel, CU.fax, CU.idcustomer, QO.dateimport, QO.placedelivery, QO.prepaypercent, QO.postpaidpercent, QD._index, CC.email,  CO.commodity, UT.unit, QD.spec, QD.quantity,QD.cogs , QD.price, QD.amount, 	  QD.vat, QD.partnumber, QD.timedelivery, QD.note, QO.idexport, QD.description, QD.equipmentinfo, CU.address, CU.tax,(select sum(amount*d.vat/100) from quotationdetail d inner join quotation q on d.idexport = q.idexport  where q.quotationno in " + dk + "  and d.status =1) as tienvat, (select sum(amount*quotationdetail.vat/100+amount) from quotationdetail inner join quotation on quotationdetail.idexport = quotation.idexport  where quotation.quotationno in " + dk + " and quotationdetail.status =1) as tiensauvat, [dbo].[Num2Text]((select sum(amount*d.vat/100+amount) from quotationdetail d inner join quotation q on d.idexport = q.idexport  where q.quotationno in " + dk + " and d.status =1 )) tienchu FROM         dbo.QUOTATION AS QO LEFT JOIN    dbo.QUOTATIONDETAIL AS QD ON QO.idexport = QD.idexport LEFT JOIN      dbo.DMCUSTOMERS AS CU ON QO.idcustomer = CU.idcustomer LEFT JOIN    dbo.DMCOMMODITY AS CO ON QD.idcommodity = CO.idcommodity LEFT JOIN    dbo.DMUNIT AS UT ON QD.idunit = UT.idunit LEFT JOIN CUSCONTACT CC	  ON CC.idcontact=QO.receiver  LEFT JOIN EMPLOYEES EE ON EE.IDEMP = QO.IDEMP LEFT JOIN      dbo.DMCURRENCY AS TT ON QO.idcurrency = TT.idcurrency WHERE     (QO.quotationno in " + dk + " and QD.status =1)  ORDER BY QD._index";
                sql = "SELECT getDate() as ngayin, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("dd/MM/yyyy") + " ~ " + Convert.ToDateTime(dte_todate_S.EditValue).ToString("dd/MM/yyyy") + "' as giaidoan, N'" + clsFunction.GetEmpNameByUser() + "' as tennv, '0908090809' as sdt, N'" + str + "' as dieukien, ";
                sql += " QS.quotationno + ','+QS.sobgnhap as quotationno ,  E.StaffName as staffname, QS.invoiceeps, QS.datepo, QS.idpotype, QS.idcustomer, CS.customer, QS.datebbnt, QS.datedelivery, QS.daterequire, ";
                sql += " QS.idstatusprocedure, QS.note, QS.idexport, QT.potype as idquotationtype,   sum(DT.amount) as amount FROM dbo.QUOTATION AS QS ";
                sql +=" INNER JOIN dbo.DMCUSTOMERS AS CS ";
                sql +=" ON QS.idcustomer = CS.idcustomer INNER JOIN dbo.EMPLOYEES AS E ON QS.IDEMP = E.IDEMP  ";
                sql += " INNER JOIN QUOTATIONDETAIL DT ON QS.idexport = DT.idexport  LEFT JOIN DMPOTYPE QT on QS.idpotype = QT.idpotype ";
                sql += " WHERE DT.status=1 ";
                sql += " GROUP BY E.StaffName, QS.quotationno,  DT.status,";
                sql += " QS.invoiceeps, QS.datepo, QS.idpotype, QS.idcustomer, CS.customer, QS.datebbnt, QS.datedelivery, QT.idpotype, QT.potype, QS.daterequire, QS.idstatusprocedure, QS.note, QS.idexport, QS.sobgnhap, ";
                sql += " idstatusquotation,E.idrecursive, QS.idemp, QS.idemppo HAVING (QS.IDEMP = '" + glue_IDEMP_I1.EditValue + "' or QS.idemppo = '" + glue_IDEMP_I1.EditValue + "' ) AND  (QS.idstatusquotation = N'ST000004')  AND  QS.invoiceeps <> ''  AND ";
                sql += " (CAST(DATEDIFF(d, 0, QS.datepo) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  ";
                sql += " ORDER BY QS.datepo ";
                dt = APCoreProcess.APCoreProcess.Read(sql);

                XtraReport report = null;

                report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frxReportPOByDate.repx", true);
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);

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
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi " + ex.Message);
            }
        }

        // Bao hành bảo trì


       
    }
}