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
    public partial class frmListQuotation : DevExpress.XtraEditors.XtraForm
    {
        public frmListQuotation()
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
                SaveGridControlsSurvey();
                SaveGridControls();
                SaveGridControlsHistory();
                SaveGridControlsBrowse();
                clsFunction.Save_sysControl(this, this);
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGridLookupDepartment();
                Load_Grid();
                Load_GridHistory();
                Load_Grid_Survey();
                //Load_Grid_Browse();
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                loadGridLookupProvider();
                loadGridLookupEmployee();
               
                // glue_IDEMP_I1.EditValue = clsFunction.GetIDEMPByUser();
                loadGridLookupMethod();
                Init();
                bbi_allow_access_search.PerformClick();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcustomer"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ORDER BY C.idcustomer");
                ControlDev.FormatControls.setContainsFilter(gv_EXPORTDETAIL_C);
                ControlDev.FormatControls.setContainsFilter(gv_history_C);
                ControlDev.FormatControls.setContainsFilter(gv_survey_C);
                glue_IDEMP_I1.Properties.View.Columns[0].Visible = false;
                glue_idquotationtype_I1.Properties.View.Columns[0].Visible = false;
            }
            if (checkAdmin() == true)
            {
                bbi_survey_allow_delete_S.Visibility = BarItemVisibility.Always;
            }
            else
            {
                bbi_survey_allow_delete_S.Visibility = BarItemVisibility.Never;
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
                bbi_allow_access_search.PerformClick();
            }
        }

        private void btn_file_S_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Add")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                btn_file_S.EditValue = ofd.FileName.Split('\\')[ofd.FileName.Split('\\').Length - 1];
                File.Copy(ofd.FileName, Application.StartupPath + "\\File\\" + btn_file_S.EditValue, true);
            }
            else if (e.Button.Caption == "Remove")
            {
                btn_file_S.EditValue = null;
                if (File.Exists(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString()))
                {
                    File.Delete(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString());
                }
            }
            else
            {
                if (btn_file_S.EditValue != null)
                {
                    try
                    {
                        if (gv_history_C.FocusedRowHandle >= 0)
                        {
                            DataTable dt = new DataTable();
                            dt = APCoreProcess.APCoreProcess.Read("select fileattack, filename from quotationhis where idquotation='" + gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "idquotation").ToString() + "'");
                            if (dt.Rows.Count > 0)
                            {
                                string ToSaveFileTo = Application.StartupPath + "\\File\\" + dt.Rows[0]["filename"].ToString();
                                byte[] fileData = (byte[])dt.Rows[0]["fileattack"];
                                using (System.IO.FileStream fs = new System.IO.FileStream(ToSaveFileTo, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                                {
                                    using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                                    {
                                        bw.Write(fileData);
                                        bw.Close();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsFunction.MessageInfo("Thông báo", ex.Message);
                    }
                    if (File.Exists(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString()))
                        Process.Start(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString());
                }
            }
        }

        private void btn_browse_S_Click(object sender, EventArgs e)
        {
            if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
            {
                //saveQuotationHis(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString(), glue_iddepartment_I1.EditValue.ToString());
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

                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='frm_QuotationTB_S' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath + "\\Report\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = APCoreProcess.APCoreProcess.Read(dtRP.Rows[0]["query"].ToString());
                    dt = APCoreProcess.APCoreProcess.Read("SELECT     CU.customer, QO.quotationno, QO.receiver, CU.tel, CU.fax, CU.idcustomer, QO.dateimport, QO.placedelivery, QO.prepaypercent, QO.postpaidpercent, QD._index, CO.commodity, UT.unit, CO.spec, QD.quantity, QD.price, QD.amount,  QD.vat,  QD.timedelivery, QD.note, QO.idexport, CU.address, CU.tax,  (select sum(amount) from quotationdetail where idexport='" + txt_idpurchase_IK1.Text + "' ) as tongtien, (select sum(amount*vat/100) from quotationdetail where idexport='" + txt_idpurchase_IK1.Text + "' ) as tienvat, (select sum(amount*vat/100+amount) from quotationdetail where idexport='" + txt_idpurchase_IK1.Text + "' ) as tiensauvat, [dbo].[Num2Text]((select sum(amount*vat/100+amount) from quotationdetail where idexport='" + txt_idpurchase_IK1.Text + "' )) tienchu FROM         dbo.QUOTATION AS QO INNER JOIN  dbo.QUOTATIONDETAIL AS QD ON QO.idexport = QD.idexport INNER JOIN    dbo.DMCUSTOMERS AS CU ON QO.idcustomer = CU.idcustomer INNER JOIN    dbo.DMCOMMODITY AS CO ON QD.idcommodity = CO.idcommodity INNER JOIN   dbo.DMUNIT AS UT ON QD.idunit = UT.idunit WHERE     (QO.idexport = N'" + txt_idpurchase_IK1.Text + "')");
                    ds.Tables.Add(dt);
                    report.DataSource = ds;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }
                    tool.ShowPreviewDialog();
                }
            }
            catch { }
        }

        private void bbi_allow_print_config_sub_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbi_allow_exit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbi_survey_allow_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_CRM.Presentation.frm_survey_S frm = new SOURCE_FORM_CRM.Presentation.frm_survey_S();
                frm._insert = true;
                frm._sign = "KS";
                frm.idquotation = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
                frm.idcustomer = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcustomer").ToString();
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_CRM.Presentation.frm_survey_S.PassData(getValueSurvey);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_survey_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông  báo", "Bạn có muốn xóa mẫu tin này không ?") == true)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from survey where idsurvey='" + gv_survey_C.GetRowCellValue(gv_survey_C.FocusedRowHandle, "idsurvey").ToString() + "'");
                gv_survey_C.DeleteRow(gv_survey_C.FocusedRowHandle);
            }
        }

        private void bbi_survey_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_CRM.Presentation.frm_survey_S frm = new SOURCE_FORM_CRM.Presentation.frm_survey_S();
                frm._insert = false;
                frm._sign = "KS";
                frm.idquotation = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
                frm.idcustomer = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcustomer").ToString();
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_CRM.Presentation.frm_survey_S.PassData(getValueSurvey);
                frm.ID = gv_survey_C.GetRowCellValue(gv_survey_C.FocusedRowHandle, "idsurvey").ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
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
            string[] caption_col = new string[] { "Mã phiếu", "Số hiệu", "Khách hàng", "Nhân viên", "Ngày nhập", "Tình trạng", "Loại báo giá", "Số lượng", "Thành tiền", "Tiền tệ", "Tỷ giá", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idexport_S", "col_quotationno_S", "col_idcustomer_S", "col_IDEMP_S", "col_dateimport_S", "col_idstatusquotation_S", "col_idquotationtype_S", "col_quantity_S", "col_total_S", "col_idcurrency_S", "col_exchangerate_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "GridLookupEditColumn", "SpinEditColumn", "SpinEditColumn", "GridLookupEditColumn", "SpinEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "250", "150", "80", "200", "100", "80", "100", "80", "100", "200" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation  ", "select idquotationtype, quotationtype frm dmquotationtype where status=1", "select idcurrency, currency from dmcurrency where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "status", "idquotationtype", "currency" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatusquotation", "idquotationtype", "idcurrency" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[5, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Loại báo giá" }, { "ID", "Tiền tệ" } };

            string[,] fieldname_glue_col = new string[5, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstusquotation", "statusquotation" }, { "idquotationtype", "quotationtype" }, { "idcurrency", "currency" } };
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
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;

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
            string[] caption_col = new string[] { "Mã LSBG", "Mã phiếu", "Khách hàng", "Nhân viên", "Ngày nhập", "Tình trạng", "Loại báo giá", "Số lượng", "Thành tiền", "Tiền tệ", "Tỷ giá", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idquotation_S", "col_idexport_S", "col_idcustomer_S", "col_IDEMP_S", "col_dateimport_S", "col_idstatusquotation_S", "col_idquotationtype_S", "col_quantity_S", "col_total_S", "col_idcurrency_S", "col_exchangerate_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "GridLookupEditColumn", "SpinEditColumn", "SpinEditColumn", "GridLookupEditColumn", "SpinEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "100", "250", "150", "80", "200", "100", "80", "100", "80", "100", "200" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation  ", "select idquotationtype, quotationtype frm dmquotationtype where status=1", "select idcurrency, currency from dmcurrency where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "status", "idquotationtype", "currency" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatusquotation", "idquotationtype", "idcurrency" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[5, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Loại báo giá" }, { "ID", "Tiền tệ" } };

            string[,] fieldname_glue_col = new string[5, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstusquotation", "statusquotation" }, { "idquotationtype", "quotationtype" }, { "idcurrency", "currency" } };
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

        private void SaveGridControlsBrowse()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Phòng ban", "Ngày", "Nhân viên", "Đính kèm" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idquotation_S", "col_iddepartment_S", "col_qdate_S", "col_idemp_S", "col_attack_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "150", "150", "80", "150", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select iddepartment, department  from dmdepartment", "select IDEMP, StaffName  from EMPLOYEES" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "department", "StaffName" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "iddepartment", "IDEMP" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "Mã PB", "Phòng ban" }, { "Mã NV", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "iddepartment", "department" }, { "IDEMP", "StaffName" } };
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

        private void Load_Grid_Browse()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer

            string[] gluenulltext = new string[] { };

            bool show_footer = false;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_history_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_history_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_history_C,
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
                gv_history_C.OptionsBehavior.Editable = false;
                gv_history_C.OptionsView.ColumnAutoWidth = true;
                gv_history_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControlsSurvey()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã KS", "Số KS", "Hàng hóa", "Nội dung", "Liên hệ", "Ngày yêu cầu", "Ngày khảo sát", "Mô tả", "Người yêu cầu", "Người thực hiện", "Thông số", "Model", "Số hiệu", "Địa chỉ", "Điện thoại", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "idsurvey", "surveyno", "idcommodity", "content", "contact", "daterequest", "dateaction", "description", "userrequest", "useraction", "spec", "model", "sign", "address", "tel", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "DateColumn", "DateColumn", "MemoColumn", "TextColumn", "TextColumn", "MemoColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "200", "100", "80", "80", "200", "100", "100", "200", "100", "100", "200", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcommodity, commodity  from dmcommodity", };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "commodity" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcommodity" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã HH", "Sản phầm" } };

            string[,] fieldname_glue_col = new string[1, 2] { { "idcommodity", "commodity" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_survey_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

        private void Load_Grid_Survey()
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
            gv_survey_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_survey_C.Name + "'");
            gv_survey_C.Columns.Clear();
            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_survey_C, read_Only, hien_Nav,
                              dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_survey_C,
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
                gv_survey_C.OptionsBehavior.Editable = true;
                gv_survey_C.OptionsView.ColumnAutoWidth = false;
                gv_survey_C.OptionsView.ShowAutoFilterRow = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gv_EXPORTDETAIL_C_Click(object sender, EventArgs e)
        {
            if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
            {
                quotationClick();
                loadGridHistory();
                loadGridSurvey();
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

        private void Manager_Survey_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_survey_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_survey_C.Name + "'");
                            Load_Grid_Survey();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void gv_survey_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_survey_C.FocusedRowHandle >= 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                flag = true;
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_survey_C, menu, gv_survey_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_Survey_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
            }
        }

        #endregion

        #region Methods

        private void saveQuotationHis(string idQuotation, string idDepartment)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from QuotationHis where idquotation='" + idQuotation + "' and iddepartment='" + idDepartment + "' ");
                DataRow dr;
                if (dt.Rows.Count == 0)
                {
                    // Insert
                    dr = dt.NewRow();
                    dr["idquotation"] = idQuotation;
                    dr["iddepartment"] = idDepartment;
                    dr["filename"] = btn_file_S.EditValue.ToString();
                    dr["sign"] = idQuotation + "_" + idDepartment;
                    dr["qdate"] = clsFunction.getDateServer();
                    dr["reasonquotation"] = txt_note_100_I2.Text;
                    dr["idemp"] = clsFunction.GetIDEMPByUser();
                    dr["status"] = "1";// 0: khởi tạo, 1 duyệt, -1 từ chối, 2 finish
                    dr["step"] = 1;
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                else
                {
                    // Edit
                    dr = dt.Rows[0];
                    dr["qdate"] = clsFunction.getDateServer();
                    dr["reasonquotation"] = txt_note_100_I2.Text;
                    dr["filename"] = btn_file_S.EditValue.ToString();
                    dr["idemp"] = clsFunction.GetIDEMPByUser();

                    if (dr["step"].ToString() != "")
                    {
                        dr["step"] = (int)dr["step"] + 1;
                    }
                    else
                    {
                        dr["step"] = 1;
                    }
                    dt.Rows[0].EndEdit();
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                insertFile();
                //string status = "";


                clsFunction.MessageInfo("Thông báo", "Chuyển thành công");
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void insertFile()
        {
            string path = "";
            if (btn_file_S.EditValue != null)
            {
                if (File.Exists(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString()))
                    path = (Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString());
            }
            if (path != "")
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                byte[] MyData = new byte[fs.Length];
                fs.Read(MyData, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("Select * from QUOTATIONHIS where idquotation='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    dr["fileattack"] = MyData;
                    dr["filename"] = btn_file_S.EditValue.ToString();
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
        }

        private void loadGridLookupDepartment()
        {
            try
            {
                string[] caption = new string[] { "Mã PB", "Tên Phòng Ban" };
                string[] fieldname = new string[] { "iddepartment", "department" };

            }
            catch { }
        }

        private void quotationClick()
        {
            try
            {
                DataTable dtQuotation = new DataTable();
                dtQuotation = APCoreProcess.APCoreProcess.Read("select TOP 1 QO.idstatusquotation, QO.idemp, QO.note , ST.step, ST.statusquotation from QUOTATION QO inner join DMSTATUSQUOTATION ST on QO.idstatusquotation=ST.idstatusquotation and QO.idexport='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport") + "' ORDER BY idstatusquotation ");
                if (dtQuotation.Rows.Count > 0)
                {
                    glue_idemp_S.EditValue = dtQuotation.Rows[0]["idemp"].ToString();
                    glue_idstatusquotation_S.EditValue = dtQuotation.Rows[0]["idstatusquotation"].ToString();
                    mmp_note_S.Text = dtQuotation.Rows[0]["note"].ToString();
                    lbl_step_S.Text = clsFunction.transLateText("Bước") + " " + dtQuotation.Rows[0]["step"].ToString() + " : " + dtQuotation.Rows[0]["statusquotation"].ToString();
                }

            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
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
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idstatusquotation_S, "select '' as idquotationtype, N'" + clsFunction.transLateText("Tất cả") + "' as quotationtype union select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, glue_idstatusquotation_S.Width * 1, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupStatus()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Tình trạng" };
                string[] fieldname = new string[] { "idstatusquotation", "statusquotation" };
                string[] col_visible = new string[] { "True", "True" };

                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idstatusquotation_S, "select idstatusquotation, statusquotation from dmstatusquotation ", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, glue_idstatusquotation_S.Width * 1, col_visible);

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
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%"+ Function.clsFunction.GetIDEMPByUser() +"%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idemp_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%"+ Function.clsFunction.GetIDEMPByUser() +"%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_S.Width, col_visible);
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
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "select '' as idcustomer, N'" + clsFunction.transLateText("Tất cả") + "' as customer,'' as tel, '' as fax, '' as address union SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width, col_visible);

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
            loadGridLookupStatus();
            bbi_allow_access_search.PerformClick();

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

                    if (txt_idpurchase_IK1.Text != "")
                    {
                        str += " and  QO.idexport='" + txt_idpurchase_IK1.Text + "'";
                    }

                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT  QO.quotationno,  QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, SUM(dt.quantity) AS quantity, SUM(dt.amountvat) AS amountvat, SUM(dt.total) AS total FROM dbo.QUOTATION AS QO LEFT JOIN dbo.QUOTATIONDETAIL AS dt ON QO.idexport = dt.idexport INNER JOIN EMPLOYEES EM ON QO.idemp=EM.idemp  WHERE     (QO.isdelete = 0) AND dt.status = 1 AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 GROUP BY QO.quotationno, QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation HAVING (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime)) ORDER BY idstatusquotation  ");
                }
                gct_list_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridHistory()
        {
            try
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, SUM(dt.quantity) AS quantity, SUM(dt.amountvat) AS amountvat, SUM(dt.total)  AS total, dbo.DMSTATUSQUOTATION.step AS [index], QO.idquotationhis FROM dbo.QUOTATIONHIS AS QO INNER JOIN  dbo.QUOTATIONHISDETAIL AS dt ON QO.idquotationhis = dt.idquotationhis INNER JOIN dbo.DMSTATUSQUOTATION ON QO.idstatusquotation = dbo.DMSTATUSQUOTATION.idstatusquotation INNER JOIN DMSTATUSQUOTATION ST  ON ST.idstatusquotation=QO.idstatusquotation WHERE     (QO.isdelete = 0) GROUP BY QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idquotationtype, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, dbo.DMSTATUSQUOTATION.step, QO.idquotationhis, ST.hide HAVING  (QO.idexport = '" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport") + "') AND ST.hide=0 ORDER BY QO.idstatusquotation  ");
                }
                gct_history_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridSurvey()
        {
            try
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT     idsurvey, surveyno, [content], contact, daterequest, dateaction, description, useraction, userrequest, spec, model, sign, address, idcommodity, note, tel FROM  dbo.SURVEY WHERE     (idquotation = N'" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "') ");
                }
                gct_survey_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion

        private void btn_attackfile_S_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Add")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = "Browse Files";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "xlsx";
                //openFileDialog1.Filter = "Document files (*.xls)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    btn_attackfile_S.Text = openFileDialog1.FileName;
                    lbl_extension_S.Text = Path.GetExtension(openFileDialog1.FileName);
                }
            }
            else if (e.Button.Caption == "Remove")
            {
                btn_attackfile_S.Text = "";
            }
            else if (e.Button.Caption == "Open")
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select attackfile, extension from SURVEYQUOTATION  where idquotation='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "'");
                if (dt.Rows.Count == 0)
                    return;
                string filename = "";
                byte[] picData = dt.Rows[0]["attackfile"] as byte[] ?? null;

                if (picData != null)
                {
                    using (MemoryStream ms = new MemoryStream(picData))
                    {

                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);

                        bmp.Save(Application.StartupPath + "\\attackfile." + dt.Rows[0]["extension"].ToString());

                        Process p = new Process();
                        p.StartInfo.FileName = "attackfile." + dt.Rows[0]["extension"].ToString();

                        //MessageBox.Show(Application.StartupPath + "\\img.jpeg");

                        p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        this.TopMost = false;
                        p.Start();
                    }
                }
            }
        }

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

        private void btn_savesurvey_S_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select idsurvey, idquotation, attackfile, extension, note from SURVEYQUOTATION WHERE idquotation='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                //
                DataRow dr = dt.Rows[0];
                dr["attackfile"] = ReadFile(btn_attackfile_S.Text);
                dr["extension"] = lbl_extension_S.Text;
                dr["note"] = txt_note_S.Text;
                APCoreProcess.APCoreProcess.Save(dr);
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["idsurvey"] = clsFunction.layMa("SV", "idsurvey", "SURVEYQUOTATION");
                dr["idquotation"] = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString();
                dr["attackfile"] = ReadFile(btn_attackfile_S.Text);
                dr["extension"] = lbl_extension_S.Text;
                dr["note"] = txt_note_S.Text;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
        }

        private void gv_history_C_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //strpassData(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());             
                frm_QUOTATION_S frm = new frm_QUOTATION_S();
                frm.txt_idexport_IK1.Text = gv_history_C.GetRowCellValue(gv_history_C.FocusedRowHandle, "idquotationhis").ToString();
                frm.TopLevel = true;
                frm.calForm = true;
                frm.viewHistory = true;
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

        private void bar_survey_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_survey_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_survey_C.Name + "'");
                            Load_Grid_Survey();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
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

        private void getValueSurvey(bool value)
        {
            if (value == true)
            {
                loadGridSurvey();
            }
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
            quotationClick();
            loadGridHistory();
            loadGridSurvey();
        }

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            loadGrid();
            gv_EXPORTDETAIL_C.Columns["idcustomer"].FilterInfo = new ColumnFilterInfo("[idcustomer] LIKE '%" + glue_idcustomer_I1.EditValue.ToString() + "%'");
        }

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
    }
}