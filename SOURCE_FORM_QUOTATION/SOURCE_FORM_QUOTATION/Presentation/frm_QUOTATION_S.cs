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
using System.Reflection;
using DevExpress.XtraGrid.Columns;
using System.Globalization;

////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
// Purchase trang thai xoa status =1
// export trang thai xoa isdelete=1

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_QUOTATION_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_QUOTATION_S()
        {
            InitializeComponent();
        }


        #endregion

        #region Var
        public bool statusForm = false;
        public string _sign = "QS";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public bool calForm = false;
        public PassData passData;
        public int stepquotation = 0;
        public bool viewHistory = false;
        public bool isFormChanged = false;
        public bool isImport = false;

        // Auto PO
        public string idCustomer = "";
        public string idCommodity ="";
        public string idPoOriginal = "";
        public List<string> items = new List<string>();
        public bool isload = true;

        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
            glue_idmanager_I1.EditValue = null;
            SetFormStateChangeHandlers(this);
            cal_vat_I4.Value = 0;
            Function.clsFunction._keylience = true;
            //statusForm = true;
            if (statusForm == true)
            {
                SaveGridControlsCSBG();
                SaveGridControlsDoiThu();
                SaveGridControls();
                clsFunction.Save_sysControl(this, this);
                clsFunction.CreateTable(this);
            }
            else
            {
                // ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcommodity"].ColumnEdit)).PopupFormMinSize= new Size(700,400);
                glue_idstatusquotation_I1.Properties.View.Columns.Clear();
                Function.clsFunction.TranslateForm(this, this.Name);
                Load_Grid();
                loadGrid();
                Load_Grid_Doithu();
                Load_Grid_CSBG();
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
                loadGridLookupCustomer();
                loadGridLookupEmployee();
                loadGridLookupEmployeePO();
                loadGridLookupManager();
                loadGridLookupMethod();
                loadGridLookupStatus();
                loadLookupCurrency();
                getGridCSBG(true);

                loadGridLookupCampaign();
                lue_idcurrency_I1.EditValue = "CC000003";
                loadGridLookupPOType();
                cal_vat_I4.Value = 10;
                glue_idmanager_I1.EditValue = "";
                if (idPoOriginal == "")
                {
                    if (calForm == true)
                    {
                        loadPurchase(txt_idexport_IK1.Text);
                    }
                    else
                    {
                        Init();
                    }
                }
                else
                {
                    Init();
                    txt_pokethua_I1.Text = idPoOriginal;
                    glue_idcustomer_I1.EditValue = idCustomer;
                   

                }
            }
            if (viewHistory == true)
            {
                bbi_allow_insert.Enabled = false;
                bbi_allow_insert_commodity_S.Enabled = false;
                bbi_allow_print_config_sub.Enabled = false;
                bbi_allow_print_Search.Enabled = false;
                btn_allows_delete_S.Enabled = false;
                bbi_allow_insert_save_exit.Enabled = false;
                bbi_allow_insert_deletecommodity.Enabled = false;
                bbi_allow_import_S.Enabled = false;
            }
            else
            {
                bbi_allow_insert.Enabled = true;
                bbi_allow_insert_commodity_S.Enabled = true;
                bbi_allow_print_config_sub.Enabled = true;
                bbi_allow_print_Search.Enabled = true;
                btn_allows_delete_S.Enabled = true;
                bbi_allow_insert_save_exit.Enabled = true;
            }
            chk_isvat_I6.Checked = true;
            txt_invoice_100_I2.Enabled = false;
            txt_invoiceeps_100_I2.Enabled = false;
            DataTable dtstep = APCoreProcess.APCoreProcess.Read("SELECT     idstatusquotation, statusquotation, step, allowedit, note, finish, hide FROM  dbo.DMSTATUSQUOTATION where idstatusquotation = '" + glue_idstatusquotation_I1.EditValue.ToString() + "' order by step");
            if (dtstep == null || dtstep.Rows.Count == 0)
                return;
            if (Convert.ToBoolean(dtstep.Rows[0]["hide"]) == true)
            {
                bbi_allow_print.Caption = clsFunction.transLateText("F6 Xem trước");
                glue_idcustomer_I1.Enabled = true;
                txt_quotationno_50_I2.Enabled = true;
            }
            else
            {
                bbi_allow_print.Caption = clsFunction.transLateText("F6 In");
                glue_idcustomer_I1.Enabled = false;
                txt_quotationno_50_I2.Enabled = false;
            }
            glue_idstatusquotation_I1_EditValueChanged(sender, e);
            if (txt_filename_I2.Text != "")
            {
                btn_openfile_S.Enabled = true;
                btn_deletefile_S.Enabled = true;
            }
            else
            {
                btn_openfile_S.Enabled = false;
                btn_deletefile_S.Enabled = false;
            }

            if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
            {
                btn_fileattack_S.Enabled = true;
            }
            else
            {
                btn_fileattack_S.Enabled = false;
            }

            glue_idstatusquotation_I1.Properties.View.Columns[0].Visible = false;
            glue_idstatusquotation_I1.Properties.View.Columns[2].Visible = false;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcommodity"].ColumnEdit)).View.Columns[0].Width = 60;


            loadStatusQuotation(dtstep);
            glue_idcustomer_I1.Properties.View.Columns[0].Width = 50;
            
            if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000006" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000005")
            {
                bbi_allow_print.Enabled = false;
            }
            else
            {
                bbi_allow_print.Enabled = true;
            }

            if (txt_pokethua_I1.Text != "")
            {
                glue_idstatusquotation_I1.EditValue = "ST000002";
                txt_quotationno_50_I2.Enabled = true;
                glue_idquotationtype_I1.Enabled = true;
                cal_limit_I4.Enabled = true;
                txt_invoice_100_I2.Enabled = true;
                glue_idpotype_I1.Enabled = true;
                txt_invoiceeps_100_I2.Enabled = true;
                dte_datedelivery_I5.Enabled = true;
                dte_datepo_I5.Enabled = true;
            }

            if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000004" )
            {
                txt_invoice_100_I2.Enabled = false;
                txt_invoiceeps_100_I2.Enabled = false;
                btn_exportinvoice_S.Enabled = false;
                dte_datepo_I5.Enabled = false;
                dte_datedelivery_I5.Enabled = false;
                glue_idpotype_I1.Enabled = false;
            }
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] != "")
                    {
                        gv_EXPORTDETAIL_C.AddNewRow();
                        int rowHandler = gv_EXPORTDETAIL_C.GetRowHandle(gv_EXPORTDETAIL_C.DataRowCount);
                        if (gv_EXPORTDETAIL_C.IsNewItemRow(rowHandler))
                        {
                            gv_EXPORTDETAIL_C.SetRowCellValue(rowHandler, gv_EXPORTDETAIL_C.Columns[2], items[i]);
                        }
                    }
                }
            }
            if (glue_idquotationtype_I1.EditValue.ToString() == "QT000001")
            {
                gv_EXPORTDETAIL_C.Columns["spec"].Visible = false;
            }
            else
            {
                gv_EXPORTDETAIL_C.Columns["spec"].Visible = true;
            }

            if (checkAdmin() && clsFunction._user == "administrator")
            {
                txt_quotationno_50_I2.Enabled = true;
            }
            
            isload = false;
        }

        private void frm_DMAREA_SH_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                for (int i = 0; i < bar_menu_C.Items.Count; i++)
                {
                    if (e.KeyCode.ToString() == bar_menu_C.Items[i].ShortcutKeyDisplayString && bar_menu_C.Items[i].Visibility != BarItemVisibility.Never)
                    {
                        bar_menu_C.PerformClick((bar_menu_C.Items[i]));
                        break;
                    }
                }
                if (e.KeyCode == Keys.Delete && gv_EXPORTDETAIL_C.Focus())
                {
                    lbl_delete_S_Click(sender, e);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region ButtonEvent

        private void bbi_allow_print_config_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                loadReport();
            }
            catch { }
        }

        private void bbi_exit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                passData(true);
                this.Close();
            }
            catch
            {
                this.Close();
            }
        }

        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                try
                {
                    if (checkInput() == true)
                    {
                        // hchanh02
                        if (checkHchanh02(glue_IDEMP_I1.EditValue.ToString()))
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set idmanager ='" + glue_idmanager_I1.EditValue + "', idstatusquotation='" + glue_idstatusquotation_I1.EditValue + "' where idexport='" + txt_idexport_IK1.Text + "'");
                            clsFunction.MessageInfo("Thông báo", "Đã duyệt báo giá thành công");
                            return;
                        }
                        if (saveMaster() == true)
                            saveDetai();
                        passData(true);
                        if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                        {
                            btn_fileattack_S.Enabled = true;
                        }
                        else
                        {
                            btn_fileattack_S.Enabled = false;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }
                XtraReport report = null;
                string fieldCommodity = "";

                if (rag_formreport_S.EditValue.ToString() == "0")
                {
                    if (glue_idquotationtype_I1.EditValue.ToString() == "QT000001")
                    {
                        report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_Quotation_TB_S.repx", true);
                    }
                    if (glue_idquotationtype_I1.EditValue.ToString() == "QT000002")
                    {
                        report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_Quotation_PhuTung_S.repx", true);
                    }
                    if (glue_idquotationtype_I1.EditValue.ToString() == "QT000003")
                    {
                        report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_Quotation_ThueXe_S.repx", true);
                    }
                    if (glue_idquotationtype_I1.EditValue.ToString() == "QT000004")
                    {
                        report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_Quotation_WareHouse_S.repx", true);
                    }
                    fieldCommodity = "commodity";
                }
                else
                {
                    fieldCommodity = "commodityen as commodity";
                    report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_Quotation_S_EN.repx", true);
                }
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT dieukienthanhtoan, hieulucbaogia,TT.currency, QO.chatluong, QO.phat, QO.dieukhoan,  QO.vuotdinhmuc, EE.StaffName,EE.tel as telnv ,   CU.customer, QO.quotationno, CC.contactname +' - '+ CC.tel as receiver, CU.tel, CU.fax, CU.idcustomer, QO.dateimport, QO.placedelivery, QO.prepaypercent, QO.postpaidpercent, QD._index, CC.email,  CO." + fieldCommodity + ", UT.unit, QD.spec, QD.quantity,QD.cogs , QD.price, QD.amount, 	  QD.vat, QD.partnumber, QD.timedelivery, QD.note, QO.idexport, QD.description, QD.equipmentinfo, CU.address, CU.tax,(select sum(amount*vat/100) from quotationdetail where idexport='" + txt_idexport_IK1.Text + "' ) as tienvat, (select sum(amount*vat/100+amount) from quotationdetail where idexport='" + txt_idexport_IK1.Text + "' ) as tiensauvat, [dbo].[Num2Text]((select sum(amount*vat/100+amount) from quotationdetail where idexport='" + txt_idexport_IK1.Text + "' )) tienchu FROM         dbo.QUOTATION AS QO INNER JOIN    dbo.QUOTATIONDETAIL AS QD ON QO.idexport = QD.idexport INNER JOIN      dbo.DMCUSTOMERS AS CU ON QO.idcustomer = CU.idcustomer INNER JOIN    dbo.DMCOMMODITY AS CO ON QD.idcommodity = CO.idcommodity LEFT JOIN    dbo.DMUNIT AS UT ON QD.idunit = UT.idunit LEFT JOIN CUSCONTACT CC	  ON CC.idcontact=QO.receiver  LEFT JOIN EMPLOYEES EE ON EE.IDEMP = QO.IDEMP LEFT JOIN      dbo.DMCURRENCY AS TT ON QO.idcurrency = TT.idcurrency WHERE     (QO.idexport = N'" + txt_idexport_IK1.Text + "') AND QO.idemp ='" + glue_IDEMP_I1.EditValue.ToString() + "' ORDER BY QD._index");
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
            catch { }
        }

        private void btn_exportinvoice_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {
                    if (saveMaster() == true)
                        saveDetai();
                    passData(true);
                    if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                    {
                        btn_fileattack_S.Enabled = true;
                    }
                    else
                    {
                        btn_fileattack_S.Enabled = false;
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
            try
            {
                if (gv_EXPORTDETAIL_C.DataRowCount > 0)
                {
                    exportExcelTeamplate(Application.StartupPath + "\\File\\PO_KETOAN.xltx", gv_EXPORTDETAIL_C);
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Chưa có dữ liệu, vui lòng kiểm tra lại");
                }
            }
            catch { };
        }

        private void bbi_allow_input_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();
            frm.sDauma = _sign;
            frm.formNamePre = this.Name;
            frm.gridNamePre = gv_EXPORTDETAIL_C.Name;
            frm.arrColumnCaption = arrCaption.Split('/');
            frm.arrColumnFieldName = arrFieldName.Split('/');
            frm.tbName = clsFunction.getNameControls(this.Name);
            frm.ShowDialog();
            loadGrid();
        }

        private void bbi_allow_export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                if (!Directory.Exists(Application.StartupPath + "\\File"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\File");
                }
                if (!File.Exists(Application.StartupPath + "\\File\\Template.xlt"))
                {
                    File.Create(Application.StartupPath + "\\File\\Template.xlt");
                }
                DataTable dt = new DataTable();
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_EXPORTDETAIL_C, clsFunction.transLateText("PHIẾU BÁO GIÁ"), this);
            }
            catch
            { }
        }

        private void bbi_exit_allow_access_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void bbi_allow_insert_ItemClick(object sender, ItemClickEventArgs e)
        {
            Init();

        }

        private void bbi_refresh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_DMAREA_SH_Load(sender, e);
        }

        private void bbi_allow_insert_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {
                    if (saveMaster() == true)
                        saveDetai();
                    Init();
                }
            }
            catch { }
        }

        private bool checkHchanh02(String idemp)
        {
            bool flag = false;
            if (clsFunction._user == "hchanh02" && idemp != clsFunction.GetIDEMPByUser())
            {
                flag = true;
            }
            return flag;
        }

        private void bbi_allow_insert_save_exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                
                if (checkInput() == true)
                {
                    // hchanh02
                    if (checkHchanh02(glue_IDEMP_I1.EditValue.ToString()))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set idmanager ='"+ glue_idmanager_I1.EditValue +"', idstatusquotation='"+ glue_idstatusquotation_I1.EditValue +"' where idexport='"+ txt_idexport_IK1.Text +"'");
                        clsFunction.MessageInfo("Thông báo", "Đã duyệt báo giá thành công");
                        return;
                    }
                    DataTable dt;
                    if (saveMaster() == true)
                        if (saveDetai() == true)
                        {
                            dt = APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'");
                            glue_idstatusquotation_I1.EditValue = dt.Rows[0]["idstatusquotation"].ToString();
                            clsFunction.MessageInfo("Thông báo", "Đã lưu báo giá thành công");
                        }
                    passData(true);
                    dt = APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        btn_fileattack_S.Enabled = true;
                    }
                    else
                    {
                        btn_fileattack_S.Enabled = false;
                    }
                }
                glue_idstatusquotation_I1_EditValueChanged(sender, e);
                if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000006" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000005")
                {
                    bbi_allow_print.Enabled = false;
                }
                else
                {
                    bbi_allow_print.Enabled = true;
                }

            }
            catch { }
        }

        private void bbi_allow_print_Search_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmSearchQuotation frm = new frmSearchQuotation();
                frm.Dock = DockStyle.Fill;
                frm.strpassData = new frmSearchQuotation.StrPassData(loadPurchase);

                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();

            }
            catch { }
            isFormChanged = false;
        }

        private void btn_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!clsFunction.GetIDEMPByUser().Equals(glue_IDEMP_I1.EditValue.ToString()))
                {
                    clsFunction.MessageInfo("Thông báo", "Chỉ có người tạo báo giá mới có thể xóa báo giá");
                    return;
                }
                if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa phiếu nhập này không?"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set isdelete=1 where idexport='" + txt_idexport_IK1.Text + "'");
                    //APCoreProcess.APCoreProcess.ExcuteSQL("delete from exportdetail where idexport='" + txt_idexport_IK1.Text + "'");
                    //APCoreProcess.APCoreProcess.ExcuteSQL("delete from export where idexport='" + txt_idexport_IK1.Text + "'");
                    //APCoreProcess.APCoreProcess.ExcuteSQL("delete from STOCKIO where iddetailex not in (select iddetail from exportdetail) ");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_allows_delete_S.Caption), txt_idexport_IK1.Text, txt_idexport_IK1.Text, SystemInformation.ComputerName.ToString(), "2", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    Init();
                }
                txt_address_S.Text = "";
                txt_fax_S.Text = "";
                txt_tel_S.Text = "";
                txt_invoice_100_I2.Text = "";
            }
            catch { }
        }

        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idtable").ToString();
            frm.idform = this.Name;
            frm.userid = clsFunction._iduser;
            frm.ShowDialog();
        }

        private void glue_idprovider_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                //callForm();
            }
            catch { }
        }

        private void glue_idprovider_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadInforProvider(glue_idcustomer_I1.Properties.View.FocusedRowHandle);
            loadContact();
        }

        private void com_vat_I4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void cal_limit_I4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cal_limit_I4.Value < 0)
                    cal_limit_I4.Value = 0;
                else
                {
                    if (cal_limit_I4.Value != Convert.ToDecimal(Convert.ToDateTime(dte_limitdept_I5.EditValue).Subtract(Convert.ToDateTime(dte_dateimport_I5.EditValue)).Days))
                        dte_limitdept_I5.EditValue = Convert.ToDateTime(dte_dateimport_I5.EditValue).AddDays(Convert.ToInt32(cal_limit_I4.Value));
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void dte_limitdept_I5_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dte_limitdept_I5.EditValue) < Convert.ToDateTime(dte_dateimport_I5.EditValue))
                {
                    dte_limitdept_I5.EditValue = dte_dateimport_I5.EditValue;
                    cal_limit_I4.Value = 0;
                }
                else
                {
                    cal_limit_I4.Value = Convert.ToDecimal(Convert.ToDateTime(dte_limitdept_I5.EditValue).Subtract(Convert.ToDateTime(dte_dateimport_I5.EditValue)).Days);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void lbl_delete_S_Click(object sender, EventArgs e)
        {
            deleteCom();
        }

        private void chk_isvat_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isvat_I6.Checked == true)
            {
                gv_EXPORTDETAIL_C.Columns["vat"].OptionsColumn.AllowEdit = false;
                setVatOrDiscountAll("vat", (decimal)cal_vat_I4.EditValue);
                cal_vat_I4.Properties.ReadOnly = false;
            }
            else
            {
                gv_EXPORTDETAIL_C.Columns["vat"].OptionsColumn.AllowEdit = true;
                cal_vat_I4.Properties.ReadOnly = true;
            }
        }

        private void chk_isdiscount_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isdiscount_I6.Checked == true)
            {
                gv_EXPORTDETAIL_C.Columns["discount"].OptionsColumn.AllowEdit = false;
                setVatOrDiscountAll("discount", (decimal)cal_vat_I4.EditValue);
            }
            else
            {
                gv_EXPORTDETAIL_C.Columns["discount"].OptionsColumn.AllowEdit = true;
            }
        }

        private void btn_file_S_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            /*if( e.Button.Caption=="Add")
             {
                 OpenFileDialog ofd = new OpenFileDialog();
                 ofd.ShowDialog();
                 btn_file_S.EditValue = ofd.FileName.Split('\\')[ofd.FileName.Split('\\').Length - 1];
                 File.Copy(ofd.FileName, Application.StartupPath + "\\File\\" + btn_file_S.EditValue, true);
             }
             else if(e.Button.Caption=="Remove")
             {
                 btn_file_S.EditValue = null;
                 if(File.Exists(Application.StartupPath+"\\File\\"+btn_file_S.EditValue.ToString()))
                 {
                     File.Delete(Application.StartupPath+"\\File\\"+btn_file_S.EditValue.ToString());
                 }
             }
             else 
             {
                 if (btn_file_S.EditValue != null)
                 {
                     try
                     {
                         if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                         {
                             DataTable dt = new DataTable();
                             dt = APCoreProcess.APCoreProcess.Read("select fileattack, filename from quotationhis where idquotation='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idquotation").ToString() + "'");
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
                     if(File.Exists(Application.StartupPath+"\\File\\"+btn_file_S.EditValue.ToString()))
                         Process.Start(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString());
                 }
             }*/
        }

        private void bbi_allow_insert_commodity_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                callFormCommodity();
            }
            catch { }
        }

        private void glue_idstatusquotation_I1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select step from dmstatusquotation where idstatusquotation='" + glue_idstatusquotation_I1.EditValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    lbl_index_S.Text = clsFunction.transLateText("Bước ") + " " + dt.Rows[0][0].ToString() + " : " + glue_idstatusquotation_I1.Text;
                }
                //if (APCoreProcess.APCoreProcess.Read("SELECT distinct S.* from DMSTATUSQUOTATION S INNER JOIN QUOTATION Q ON S.idstatusquotation = Q.idstatusquotation WHERE S.finish =1 and S.idstatusquotation='" + glue_idstatusquotation_I1.EditValue.ToString() + "' AND Q.quotationno='" + txt_quotationno_50_I2.Text + "' ").Rows.Count > 0)
                if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000004" && APCoreProcess.APCoreProcess.Read("select idstatusquotation from quotation where idexport='" + txt_idexport_IK1.Text + "' and idstatusquotation <> 'ST000004'").Rows.Count > 0)
                {
                    txt_invoice_100_I2.Enabled = true;
                    txt_invoiceeps_100_I2.Enabled = true;
                    btn_exportinvoice_S.Enabled = true;
                    dte_datepo_I5.Enabled = true;
                    dte_datedelivery_I5.Enabled = true;
                    glue_idpotype_I1.Enabled = true;
                }
                else
                {
                    txt_invoice_100_I2.Enabled = false;
                    txt_invoiceeps_100_I2.Enabled = false;
                    btn_exportinvoice_S.Enabled = false;
                    dte_datepo_I5.Enabled = false;
                    dte_datedelivery_I5.Enabled = false;
                    glue_idpotype_I1.Enabled = false;
                    //glue_idpotype_I1.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }
            if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
            {
                btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                glue_idstatusquotation_I1.Enabled = true;
            }
            else
            {
                btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                glue_idstatusquotation_I1.Enabled = false;
            }
        }

        private void cal_vat_I4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chk_isvat_I6.Checked = !chk_isvat_I6.Checked;
                chk_isvat_I6.Checked = !chk_isvat_I6.Checked;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { "quantity", "mount", "amountvat", "amountdiscount", "costs", "total" };
            // Caption column
            //col_iddetail_IK1/col_idcommodity_I1/col_partnumber_I2/col_spec_I2/col_commodity_S/col_idunit_I1/col_idwarehouse_I1/col_quantity_I4/col_price_I4/col_amount_I15/col_vat_I4/col_amountvat_I15/col_davat_I6/col_discount_I8/col_amountdiscount_I15/col_costs_I15/col_total_I15/col_timedelivery_I2/col_note_I3/col_idexport_I1/
            string[] caption_col = new string[] { "ID", "Mã hàng", "Ký hiệu", "Tên hàng", "ĐVT", "Kho hàng", "Số lượng", "Đơn giá", "Thành tiền", "VAT", "Tiền VAT", "CK Sau VAT", "CK", "Tiền CK", "Chi phí", "Tổng tiền", "Thời điểm GH", "Ghi chú", "ID" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddetail_IK1", "col_idcommodity_I1", "col_sign_20_I2", "col_commodity_S", "col_idunit_I1", "col_idwarehouse_I1", "col_quantity_I4", "col_price_I4", "col_amount_I15", "col_vat_I4", "col_amountvat_I15", "col_davat_I6", "col_discount_I8", "col_amountdiscount_I15", "col_costs_I15", "col_total_I15", "col_timedelivery_S", "col_note_I3", "col_idexport_I1" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CheckColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "MemoColumn", "MemoColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "200", "60", "200", "100", "100", "100", "60", "100", "100", "60", "100", "100", "100", "200", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "True", "True", "False", "True", "True", "True", "True", "False", "True", "False", "True", "True", "False", "True", "False", "True", "True", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "False" };
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
            string[] sql_glue = new string[] { "select idcommodity, commodity  from dmcommodity where status=1 and quantity=1", "select idunit, unit  from dmunit where status=1", "select idwarehouse, warehouse  from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "commodity", "unit", "warehouse" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcommodity", "idunit", "idwarehouse" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "Mã hàng", "Tên hàng" }, { "Mã ĐV", "ĐVT" }, { "Mã kho", "Kho" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcommodity", "commodity" }, { "idunit", "unit" }, { "idwarehouse", "warehouse" } };
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
            clsFunction.CreateTableGrid(fieldname_col, gv_EXPORTDETAIL_C);
        }

        private void SaveGridControlsDoiThu()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            //col_iddetail_IK1/col_idcommodity_I1/col_partnumber_I2/col_spec_I2/col_commodity_S/col_idunit_I1/col_idwarehouse_I1/col_quantity_I4/col_price_I4/col_amount_I15/col_vat_I4/col_amountvat_I15/col_davat_I6/col_discount_I8/col_amountdiscount_I15/col_costs_I15/col_total_I15/col_timedelivery_I2/col_note_I3/col_idexport_I1/
            string[] caption_col = new string[] { "ID", "Đối thủ", "Loại đối thủ", "Giá bán", "Ngày tham khảo", "Điện thoại", "Email", "Địa chỉ" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddoithu_IK1", "col_tendoithu_I1", "col_loaidoithu_I2", "col_giaban_I4", "col_ngaythamkhao_I5", "col_tel_I2", "col_email_I2", "col_address_I2" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "CalcEditColumn", "DateColumn", "TextColumn", "TextColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "100", "90", "100", "100", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcommodity, commodity  from dmcommodity where status=1 and quantity=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "commodity" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcommodity" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã hàng", "Tên hàng" } };
            // FieldName GridlookupEdit
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_doithu_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_doithu_C);
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
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho", "Loại SP" };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;
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
                gv_EXPORTDETAIL_C.OptionsBehavior.Editable = true;
                gv_EXPORTDETAIL_C.OptionsView.ColumnAutoWidth = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Doithu()
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
            gv_doithu_C.OptionsView.ShowAutoFilterRow = false;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_doithu_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_doithu_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_doithu_C,
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
                gv_doithu_C.OptionsBehavior.Editable = true;
                gv_doithu_C.OptionsView.ColumnAutoWidth = false;
                gv_doithu_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControlsCSBG()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            //col_iddetail_IK1/col_idcommodity_I1/col_partnumber_I2/col_spec_I2/col_commodity_S/col_idunit_I1/col_idwarehouse_I1/col_quantity_I4/col_price_I4/col_amount_I15/col_vat_I4/col_amountvat_I15/col_davat_I6/col_discount_I8/col_amountdiscount_I15/col_costs_I15/col_total_I15/col_timedelivery_I2/col_note_I3/col_idexport_I1/
            string[] caption_col = new string[] { "ID", "Ngày", "Nội dung"};

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_macs_IK1", "col_ngaytao_I5", "col_note_I2"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "DateColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "400"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True"};
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
            string[] sql_glue = new string[] {};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {};

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0];
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0];
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_CSBG_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_doithu_C);
        }

        private void Load_Grid_CSBG()
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
            gv_CSBG_C.OptionsView.ShowAutoFilterRow = false;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_CSBG_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_CSBG_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_CSBG_C,
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
                gv_CSBG_C.OptionsBehavior.Editable = true;
                gv_CSBG_C.OptionsView.ColumnAutoWidth = false;
                gv_CSBG_C.OptionsView.ShowAutoFilterRow = false;

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

        private void gct_list_C_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                    loadStatus(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Function.clsFunction.transLateText("Thông báo"));
            }
        }

        private void gv_list_C_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                    loadStatus(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Function.clsFunction.transLateText("Thông báo"));
            }
        }

        private void gv_list_C_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_menu_C, menu, gv_EXPORTDETAIL_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_CSBG_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_CSBG_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_csbg_C, menu, gv_CSBG_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_CSBG);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void Manager_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_EXPORTDETAIL_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_EXPORTDETAIL_C.Name + "'");
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

        private void Manager_ItemPress_DoiThu(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_doithu_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_doithu_C.Name + "'");
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

        private void Manager_ItemPress_CSBG(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_CSBG_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_CSBG_C.Name + "'");
                          
                            Load_Grid_CSBG();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
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

        private void gv_PURCHASEDETAIL_C_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void gv_PURCHASEDETAIL_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "col_idcommodity_I1")
                {
                    setRowByIDcommodity(e.RowHandle, e.Value.ToString());
                }
                if (e.Column.Name == "col_quantity_I4")
                {
                    setRowByquantity(e.RowHandle);
                }
                if (e.Column.Name == "col_price_I4")
                {
                    setRowByPrice(e.RowHandle);
                }
                if (e.Column.Name == "col_vat_I4")
                {
                    setRowByVat(e.RowHandle);
                }
                if (e.Column.Name == "col_davat_I6")
                {
                    setRowByDavat(e.RowHandle);
                }
                if (e.Column.Name == "col_discount_I8")
                {
                    setRowByDiscount(e.RowHandle);
                }
                if (e.Column.Name == "col_costs_I15")
                {
                    setRowByCost(e.RowHandle);
                }
                if (e.Column.Name == "col_amountdiscount_I15")
                {
                    setRowByAmountDiscount(e.RowHandle);
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu vượt quá số lượng cho phép, tối đa 1000 ký tự");
            }
        }

        private void gv_EXPORTDETAIL_C_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gv_PURCHASEDETAIL_C_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                // Kiểm tra trùng mã
                GridView view = sender as GridView;
                GridColumn inStockCol = view.Columns["idcommodity"];
                GridColumn intSl = view.Columns["quantity"];
                GridColumn intDG = view.Columns["price"];
                //Get the value of the first column
                String inSt = (String)view.GetRowCellValue(e.RowHandle, inStockCol);
                if (e.RowHandle < 0)
                {
                    //if (checkExitMaHangInGrid(inSt, (DataTable)gct_list_C.DataSource))
                    //{
                    //    e.Valid = false;
                    //    //Set errors with specific descriptions for the columns
                    //    view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");
                    //}
                }
                else
                {
                    //if (checkExitMaHangInGridSua(inSt, (DataTable)gct_list_C.DataSource))
                    //{
                    //    e.Valid = false;
                    //    //Set errors with specific descriptions for the columns
                    //    view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");

                    //}
                }
                // Giới hạn số lương
                if (view.GetRowCellValue(e.RowHandle, intSl).ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, intSl)) <= 0)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Số lượng không được nhỏ hơn 0");

                }
                // giới hạn vat
                if (view.GetRowCellValue(e.RowHandle, "vat").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) < 0 || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) > 100)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "VAT phải >=0 và <=100");

                }
                // giới hạn chiết khấu
                if (view.GetRowCellValue(e.RowHandle, "discount").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "discount")) < 0 || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "discount")) > 100)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Chiết khấu phải >=0 và <=100");

                }
                // giới hạn chi phí 0 âm
                if (view.GetRowCellValue(e.RowHandle, "costs").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "costs")) < 0)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Chi phí phải lớn hơn 0");

                }

                // kiểm tra đủ số lượng xuất
                //if (checkInsert(gv_EXPORTDETAIL_C.GetRowCellValue(e.RowHandle, "idcommodity").ToString(), Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(e.RowHandle, "quantity"))) == false)
                //{
                //    if (clsFunction.MessageDelete("Thông báo", "Số lượng tồn kho không đủ, bạn có muốn xuất âm mặt hàng này") == false)
                //    {
                //        e.Valid = false;
                //        //Set errors with specific descriptions for the columns
                //        view.SetColumnError(intSl, "Số lượng xuất không đủ");
                //    }

                //}
            }
            catch { }
        }
        #endregion

        #region Methods

        private void loadGridLookupMethod()
        {
            try
            {
                string[] caption = new string[] { "Mã loại", "Loại báo giá" };
                string[] fieldname = new string[] { "idquotationtype", "quotationtype" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idquotationtype_I1, "select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, 400, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadPurchase(string str)
        {
            try
            {
                if (str != "")
                {
                    DataTable dtM = new DataTable();
                    if (viewHistory == false)
                    {
                        dtM = APCoreProcess.APCoreProcess.Read("SELECT QO.hieulucbaogia, QO.dieukienthanhtoan, QO.filename,  ISNULL(QO.vat, 0) AS VAT, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, ISNULL(QO.discount, 0) AS discount, QO.idquotationtype,  QO.isvat, QO.idcurrency, ISNULL(QO.exchangerate, 0) AS exchangerate, QO.idstatusquotation, QO.isdept, QO.outlet, QO.limit,  QO.isdiscount,  QO.quotationno, QO.placedelivery, QO.receiver, QO.chatluong, QO.vuotdinhmuc, QO.invoice, QO.invoiceeps, QO.nguoichogia, QO.idmanager, QO.dieukhoan, hhnv, hhkh, thoigiantt, idemppo, idpotype, datedelivery, datepo, prepaypercent,postpaidpercent, sobgnhap, idcampaign FROM dbo.QUOTATION AS QO where idexport='" + str + "'");
                    }
                    else
                    {
                        dtM = APCoreProcess.APCoreProcess.Read("SELECT  QO.hieulucbaogia, QO.dieukienthanhtoan, QO.filename, ISNULL(QO.vat, 0) AS VAT, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP,  QO.note, ISNULL(QO.discount, 0) AS discount, QO.idquotationtype,  QO.isvat, QO.idcurrency, ISNULL(QO.exchangerate, 0) AS exchangerate, QO.idstatusquotation, QO.isdept, QO.outlet, QO.limit,  QO.isdiscount, QO.idquotationhis as idexport,  QO.quotationno, QO.placedelivery, QO.receiver, QO.chatluong, QO.vuotdinhmuc, QO.invoice, QO.invoiceeps, QO.nguoichogia, QO.idmanager, QO.dieukhoan, hhnv, hhkh, thoigiantt, idemppo, idpotype, datedelivery, datepo, prepaypercent,postpaidpercent, sobgnhap, idcampaign FROM dbo.QUOTATIONHIS AS QO where idquotationhis='" + str + "'");
                    }
                    if (dtM.Rows.Count > 0)
                    {
                        glue_IDEMP_I1.EditValue = null;
                        glue_idcustomer_I1.EditValue = null;
                        glue_idcustomer_I1.EditValue = dtM.Rows[0]["idcustomer"].ToString();
                        loadInfoProvider();
                        glue_idcustomer_I1.Properties.View.FocusedRowHandle = 1;
                        glue_IDEMP_I1.EditValue = dtM.Rows[0]["IDEMP"].ToString();
                        dte_dateimport_I5.EditValue = dtM.Rows[0]["dateimport"].ToString();
                        dte_limitdept_I5.EditValue = dtM.Rows[0]["limitdept"].ToString();
                        txt_idexport_IK1.Text = dtM.Rows[0]["idexport"].ToString();
                        txt_note_500_I2.Text = dtM.Rows[0]["note"].ToString();
                        cal_limit_I4.Value = Convert.ToInt32(dtM.Rows[0]["limit"]);
                        rad_isdept_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["isdept"]);
                        rad_outlet_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["outlet"]);
                        chk_isdiscount_I6.Checked = Convert.ToBoolean(dtM.Rows[0]["isdiscount"]);
                        chk_isvat_I6.Checked = Convert.ToBoolean(dtM.Rows[0]["isvat"]);
                        spe_exchangerate_I4.Value = Convert.ToDecimal(dtM.Rows[0]["exchangerate"]);
                        cal_vat_I4.Value = Convert.ToDecimal(dtM.Rows[0]["vat"]);
                        cal_discount_I4.Value = Convert.ToDecimal(dtM.Rows[0]["discount"]);
                        lue_idcurrency_I1.EditValue = dtM.Rows[0]["idcurrency"].ToString();
                        glue_idstatusquotation_I1.EditValue = dtM.Rows[0]["idstatusquotation"].ToString();
                        glue_idquotationtype_I1.EditValue = dtM.Rows[0]["idquotationtype"].ToString();
                        loadGridLookupPOType();
                        txt_quotationno_50_I2.Text = dtM.Rows[0]["quotationno"].ToString();
                        txt_placedelivery_500_I2.Text = dtM.Rows[0]["placedelivery"].ToString();
                        glue_receiver_I2.Text = dtM.Rows[0]["receiver"].ToString();
                        txt_filename_I2.Text = dtM.Rows[0]["filename"].ToString();
                        loadFile(dtM.Rows[0]["filename"].ToString());
                        txt_invoice_100_I2.Text = dtM.Rows[0]["invoice"].ToString();
                        txt_invoiceeps_100_I2.Text = dtM.Rows[0]["invoiceeps"].ToString();
                        mmo_chatluong_I3.Text = dtM.Rows[0]["chatluong"].ToString();
                        mmo_vuotdinhmuc_I3.Text = dtM.Rows[0]["vuotdinhmuc"].ToString();
                        mmo_dieukhoan_I3.Text = dtM.Rows[0]["dieukhoan"].ToString();
                        txt_nguoichogia_I2.Text = dtM.Rows[0]["nguoichogia"].ToString();
                        glue_idmanager_I1.EditValue = dtM.Rows[0]["idmanager"].ToString();
                        txt_hhnv_I2.Text = dtM.Rows[0]["hhnv"].ToString();
                        txt_hhkh_I2.Text = dtM.Rows[0]["hhkh"].ToString();
                        txt_thoigiantt_I2.Text = dtM.Rows[0]["thoigiantt"].ToString();
                        glue_idemppo_I1.EditValue = dtM.Rows[0]["idemppo"].ToString();
                        glue_idpotype_I1.EditValue = dtM.Rows[0]["idpotype"].ToString();
                        
                        glue_idcampaign_I1.EditValue = dtM.Rows[0]["idcampaign"].ToString();
                        mmo_hieulucbaogia_I3.Text = dtM.Rows[0]["hieulucbaogia"].ToString();
                        mmo_dieukienthanhtoan_I3.Text = dtM.Rows[0]["dieukienthanhtoan"].ToString();
                        if (dtM.Rows[0]["datedelivery"].ToString() != "")
                        {
                            dte_datedelivery_I5.EditValue = Convert.ToDateTime(dtM.Rows[0]["datedelivery"]);
                        }
                        else
                        {
                            dte_datedelivery_I5.EditValue = null;
                        }

                        if (dtM.Rows[0]["datepo"].ToString() != "")
                        {
                            dte_datepo_I5.EditValue = Convert.ToDateTime(dtM.Rows[0]["datepo"]);
                        }
                        else
                        {
                            dte_datepo_I5.EditValue = null;
                        }
                        cal_limit_I4.EditValue = dtM.Rows[0]["limit"] != null ? Convert.ToInt32(dtM.Rows[0]["limit"]) : 0;

                        spe_prepaypercent_I4.Value = dtM.Rows[0]["prepaypercent"] != null ? Convert.ToInt32(dtM.Rows[0]["prepaypercent"]) : 0;
                        spe_postpaidpercent_I4.Value = dtM.Rows[0]["postpaidpercent"] != null ? Convert.ToInt32(dtM.Rows[0]["postpaidpercent"]) : 0;
                        txt_sobgnhap_100_I2.Text = dtM.Rows[0]["sobgnhap"].ToString();
                        glue_idstatusquotation_I1.EditValue = dtM.Rows[0]["idstatusquotation"].ToString();
                        

                    }

                    DataTable dtD = new DataTable();
                    if (viewHistory == false)
                    {
                        dtD = APCoreProcess.APCoreProcess.Read("SELECT   dt.status, dt.baohanh , dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity,dt.price, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.sign, mh.commodity, dt.timedelivery, dt.spec, dt.partnumber, dt.cogs, dt.equipmentinfo, dt.description, dt.idgrouptk, dt._index as [index] FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport='" + str + "'  ORDER BY _index");
                    }
                    else
                    {
                        dtD = APCoreProcess.APCoreProcess.Read("SELECT   dt.status, dt.baohanh,  dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity,dt.price, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note,  mh.sign, mh.commodity, dt.timedelivery, dt.spec, dt.partnumber, dt.cogs, dt.equipmentinfo, dt.description, dt.idgrouptk, dt._index as [index] FROM         QUOTATIONHISDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idquotationhis='" + str + "'  ORDER BY _index");
                    }

                    //if (dtD.Rows.Count > 0)
                    {
                        gct_list_C.DataSource = dtD;
                    }

                    if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                        loadStatus(txt_idexport_IK1.Text);

                    if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                    {
                        btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                        glue_idstatusquotation_I1.Enabled = true;
                    }
                    else
                    {
                        btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                        glue_idstatusquotation_I1.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi: " + ex.Message);
            }
            isFormChanged = false;
            if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
            {
                btn_fileattack_S.Enabled = true;
            }
            else
            {
                btn_fileattack_S.Enabled = false;
            }

            DataTable dtstep = APCoreProcess.APCoreProcess.Read("SELECT     idstatusquotation, statusquotation, step, allowedit, note, finish, hide FROM  dbo.DMSTATUSQUOTATION where idstatusquotation = '" + glue_idstatusquotation_I1.EditValue.ToString() + "' order by step");
            loadStatusQuotation(dtstep);

        }

        private bool checkAdmin()
        {
            bool flag = false;
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where root = 1 AND userid = '" + clsFunction._iduser + "' ");
            if (dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        private void loadStatusQuotation(DataTable dtstep)
        {
            stepquotation = Convert.ToInt16(dtstep.Rows[0]["step"]);
            loadGridLookupStatus();
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'");
            if (Convert.ToBoolean(dtstep.Rows[0]["hide"]) == true)
            {

                glue_idcustomer_I1.Enabled = true;

                if (dt.Rows.Count > 0)
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                    glue_idstatusquotation_I1.Enabled = true;
                    txt_quotationno_50_I2.Enabled = false;
                    if (isImport == true)
                    {
                        glue_idstatusquotation_I1.EditValue = dt.Rows[0]["idstatusquotation"].ToString();
                    }
                }
                else
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                    glue_idstatusquotation_I1.Enabled = false;
                    txt_quotationno_50_I2.Enabled = true;
                }
                bbi_allow_insert_deletecommodity.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                glue_idstatusquotation_I1.Enabled = true;
                txt_quotationno_50_I2.Enabled = false;
            }


            if (Convert.ToBoolean(dtstep.Rows[0]["finish"]) == true)
            {
                glue_idcustomer_I1.Enabled = false;
                txt_quotationno_50_I2.Enabled = false;
                bbi_allow_insert_deletecommodity.Visibility = BarItemVisibility.Never;
            }

            if (checkAdmin() == true)
            {
                btn_allows_delete_S.Visibility = BarItemVisibility.Always;
            }
            if (APCoreProcess.APCoreProcess.Read("select * from EMPLOYEES WHERE IDEMP = '" + clsFunction.GetIDEMPByUser() + "' AND ismanager =1").Rows.Count > 0)
            {
                glue_idmanager_I1.Enabled = true;
            }
            else
            {
                glue_idmanager_I1.Enabled = false;
            }
        }

        private void customPopupMenu()
        {
            // Bind the menu to a bar manager.
            menu.Manager = bar_menu_C;
            // Add two items that belong to the bar manager.
            for (int i = 0; i < bar_menu_C.Items.Count; i++)
            {
                if (!bar_menu_C.Items[i].Name.Contains("_S"))
                {
                    bar_menu_C.Items[i].Caption = clsFunction.transLateText(bar_menu_C.Items[i].Caption);
                    menu.ItemLinks.Add(bar_menu_C.Items[i]);
                }
            }
        }

        private void getValueUpdate(bool value)
        {
            if (value == true)
            {
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idfloors"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfloors,floors from dmfloors where status=1");
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
                dts = APCoreProcess.APCoreProcess.Read("SELECT  dt.status,   dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, dt.spec, mh.commodity, dt.timedelivery, dt.partnumber, dt.description, dt.equipmentinfo, dt.cogs, dt.baohanh, dt.idgrouptk FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport='" + txt_idexport_IK1.Text + "'");
            }
            gct_list_C.DataSource = dts;
        }

        private void loadStatus(string ID)
        {
            try
            {
                string status = "";
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Function.clsFunction.ExcuteProc("sysTraceByExpression", new string[5, 2] { { "userid", clsFunction._iduser }, { "idform", "%" + clsFunction.getNameControls(this.Name) + "%" }, { "object", ID }, { "fromdate", DateTime.Now.AddYears(-1).ToShortDateString() }, { "todate", DateTime.Now.ToShortDateString() } });
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read("	SELECT     sysTrace.ID, sysTrace.userid, sysTrace.date, sysTrace.idform, sysTrace.status, sysTrace.object, sysTrace.caption,   sysTrace.action, sysTrace.computer, sysTrace.tableName,sysTrace.datapre,sysTrace.namespace, EMPLOYEES.IDEMP, EMPLOYEES.StaffName	FROM         sysTrace INNER JOIN  sysUser ON sysTrace.userid = sysUser.userid INNER JOIN  EMPLOYEES ON sysUser.IDEMP = EMPLOYEES.IDEMP	  where (sysTrace.idform like '%" + clsFunction.getNameControls(this.Name) + "%')  and  sysTrace.object='" + ID + "' and CONVERT(datetime, sysTrace.date,103) between convert(datetime,'" + DateTime.Now.AddYears(-1).ToString("yyyyMMdd") + "',103) and convert(datetime,'" + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "')	  and sysTrace.userid  in (select us.userid from sysUser us inner join employees e on e.IDEMP=us.IDEMP inner join employees e1 on e1.idemp=e.idmanager where e.idmanager in (select idemp from sysUser u1 where userid='" + clsFunction._iduser + "') or e.idemp in (select idemp from sysUser u1 where userid='" + clsFunction._iduser + "'))");
                }
                if (dt.Rows.Count > 0)
                {
                    bbi_status_S.Caption = "";
                    status += clsFunction.transLateText("Người tạo: ") + dt.Rows[0]["StaffName"].ToString() + " - " + clsFunction.transLateText(" Ngày tạo: ") + Convert.ToDateTime(dt.Rows[0]["date"]).ToString("dd/MM/yyyy HH:mm:ss");
                    if (dt.Rows.Count > 1)
                    {
                        status += " - " + clsFunction.transLateText("Người sửa: ") + dt.Rows[dt.Rows.Count - 1]["StaffName"].ToString() + " - " + clsFunction.transLateText(" Ngày sửa: ") + Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["date"]).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    bbi_status_S.Caption = status;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Function.clsFunction.transLateText("Thông báo"));
            }
        }

        private void loadReport()
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = this.Name;
                frm.ShowDialog();
            }
            catch { }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("dmtable");
            ds.Tables.Add(dt);
            //Instantiating your DataSet instance here
            //.....
            //Pass the created DataSet to your report:
            ((XtraReport)sender).DataSource = ds;
            ((XtraReport)sender).DataMember = ds.Tables[0].TableName;
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Nhân viên", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "staffname", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "True", "False", "False", "False" };
                String dkManager = "";
                if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
                {
                    dkManager = " or 1=1 ";
                }

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 " + dkManager + ") AND E.status='True' AND C.status='True' ORDER BY C.idcustomer", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }

        // PO
        private void loadGridLookupPOType()
        {
            string[] caption = new string[] { "Mã loại PO", "Loại PO" };
            string[] fieldname = new string[] { "idpotype", "potype" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idpotype_I1, "select idpotype, potype from dmpotype where idquotationtype = '" + glue_idquotationtype_I1.EditValue.ToString() + "' AND status =1 ", "potype", "idpotype", caption, fieldname, this.Name, glue_idpotype_I1.Width);
        }

        private void loadGridLookupCampaign()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã CD", "Chiến dịch" };
                string[] fieldname = new string[] { "idcampaign", "campaign" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcampaign_I1, "select  idcampaign, campaign from dmcampaign where  (convert(datetime, cast(datediff(d,0,getdate()) as datetime) ,103)   between convert(datetime, cast(datediff(d,0,fromdate) as datetime) ,103) and  convert(datetime, cast(datediff(d,0,todate) as datetime) ,103) ) or unlimited =1 ", "campaign", "idcampaign", caption, fieldname, this.Name, glue_idcampaign_I1.Width, col_visible);
            }
            catch { }
        }

        private void loadInfoProvider()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from dmcustomers where idcustomer='" + glue_idcustomer_I1.EditValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txt_address_S.Text = dt.Rows[0]["address"].ToString();
                    txt_fax_S.Text = dt.Rows[0]["fax"].ToString();
                    txt_tel_S.Text = dt.Rows[0]["tel"].ToString();
                }
            }
            catch { }
        }

        private void callForm()
        {
            try
            {
                SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S frm = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S();
                frm.strpassData = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S.strPassData(getStringValue);
                frm._insert = true;
                frm.call = true;
                frm._sign = "KH";
                frm.ShowDialog();
            }
            catch { }
        }

        private void callFormCommodity()
        {
            try
            {
                SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S frm = new SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S();
                frm.strpassData = new SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S.strPassData(getStringValueCommodity);
                frm._insert = true;
                frm.call = true;
                frm._sign = "HH";
                frm.ShowDialog();
            }
            catch { }
        }

        private void getStringValue(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idcustomer_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idcustomer,customer from dmcustomers where status=1");
                    glue_idcustomer_I1.EditValue = value;
                }
            }
            catch { }
        }

        private void getStringValueCommodity(string value)
        {
            try
            {
                if (value != "")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcommodity"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select  idcommodity, commodity, equipmentinfo,spec from dmcommodity where status=1 and quantity=1");

                }
            }
            catch { }
        }

        private void getStringValueUnit(string value)
        {
            try
            {
                if (value != "")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idunit"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select  idunit, unit from dmunit where status=1");

                }
            }
            catch { }
        }

        private void loadInforProvider(int index)
        {
            try
            {
                if (index >= 0)
                {
                    txt_address_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcDiachi).ToString();
                    txt_fax_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcFax).ToString();
                    txt_tel_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcSodt).ToString();
                }
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo","Lỗi: " + ex.Message);
            }
        }

        private void Init()
        {
            try
            {
               
                isImport = false;
                loadGridLookupStatus();
                dte_datedelivery_I5.EditValue = DateTime.Now;
                dte_datepo_I5.EditValue = DateTime.Now;
                txt_invoice_100_I2.Text = "";
                txt_quotationno_50_I2.Text = "";
                mmo_chatluong_I3.Text = "Hàng mới 100%, bảo hành 24 tháng theo tiêu chuẩn của Nhà sản xuất.";
                mmo_vuotdinhmuc_I3.Text = "Sử dụng vượt quá định mức 300giờ/tháng (option) sẽ tính thêm chi phí của số giờ vượt quá định mức";
                glue_IDEMP_I1.Properties.ReadOnly = true;
                glue_IDEMP_I1.EditValue = clsFunction.GetIDEMPByUser();
                glue_idcustomer_I1.EditValue = null;
                dte_dateimport_I5.EditValue = DateTime.Now;
                dte_limitdept_I5.EditValue = DateTime.Now;
                rad_isdept_I6.EditValue = true;
                rad_outlet_I6.EditValue = true;
                cal_limit_I4.Value = 0;
                txt_idexport_IK1.Text = clsFunction.layMa("QS", "idexport", "Quotation");
                mmo_dieukhoan_I3.Text = "";
                txt_placedelivery_500_I2.Text = "";
                glue_idquotationtype_I1.EditValue = "";
                cal_limit_I4.Value = 0;
                txt_invoice_100_I2.Text = "";
                glue_idpotype_I1.EditValue = "";
                txt_invoiceeps_100_I2.Text = "";
                dte_datedelivery_I5.EditValue = "";
                dte_datepo_I5.EditValue = "";
                txt_hhkh_I2.Text = "";
                txt_hhnv_I2.Text = "";
                while (gv_EXPORTDETAIL_C.RowCount > 0)
                {
                    gv_EXPORTDETAIL_C.DeleteRow(0);
                }

                

                if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                    glue_idstatusquotation_I1.Enabled = false;
                }
                else
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                    glue_idstatusquotation_I1.Enabled = true;
                }

                glue_idquotationtype_I1.EditValue = glue_idquotationtype_I1.Properties.GetKeyValue(0);
                if (glue_idquotationtype_I1.EditValue.ToString() == "QT000003")
                {
                    mmo_chatluong_I3.Text = "Hàng mới 100%, bảo hành 6 tháng theo tiêu chuẩn của Nhà sản xuất.";
                    mmo_vuotdinhmuc_I3.Text = "Sử dụng vượt quá định mức 300giờ/tháng (option) sẽ tính thêm chi phí của số giờ vượt quá định mức";
                }
                else
                {
                    mmo_chatluong_I3.Text = "Hàng mới 100%, bảo hành 6 tháng theo tiêu chuẩn của Nhà sản xuất.";
                    mmo_vuotdinhmuc_I3.Text = "";
                }

                if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                    glue_idstatusquotation_I1.Enabled = false;
                }
                else
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                    glue_idstatusquotation_I1.Enabled = true;
                }
                for (int i = gv_file_C.DataRowCount - 1; i >= 0; i--)
                {
                    gv_file_C.DeleteRow(i);
                }

                txt_invoice_100_I2.Text = "";
                txt_invoiceeps_100_I2.Text = "";
                txt_quotationno_50_I2.Text = createCodePO(1);

                stepquotation = 0;
                DataTable dtstep = APCoreProcess.APCoreProcess.Read("SELECT     idstatusquotation, statusquotation, step, allowedit, note, finish, hide FROM  dbo.DMSTATUSQUOTATION  order by step");
                glue_idstatusquotation_I1.Properties.DataSource = dtstep;
                loadStatusQuotation(dtstep);
                txt_address_S.Text = "";
                txt_fax_S.Text = "";
                txt_tel_S.Text = "";
            }
            catch(Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

           
        }

        private String createCodePO(int num)
        {
            String sign = "";
            sign = APCoreProcess.APCoreProcess.Read("select  sign from employees where idemp ='" + clsFunction.GetIDEMPByUser() + "'").Rows[0][0].ToString().Trim();
            String code = "";
            DataTable dt = new DataTable();
            //dt = APCoreProcess.APCoreProcess.Read("select top 1  quotationno from QUOTATION where  quotationno like '%" + "-" + Convert.ToDateTime(dte_dateimport_I5.EditValue).ToString("MMyy") + "/EPS-BG" + "' order by quotationno desc");
            dt = APCoreProcess.APCoreProcess.Read("select top 1  quotationno from QUOTATION  order by idexport desc");
            int index = 0;
            String sIndex = "0";
            try
            {
                sIndex = dt.Rows[0][0].ToString().Substring(sign.Length, 3);
                index = Convert.ToInt32(sIndex);
              
            }
            catch
            {
            }
            code = sign + (index + num).ToString().PadLeft(3, '0') + "-" + DateTime.Now.ToString("MMyy") + "/EPS-BG";
            if (APCoreProcess.APCoreProcess.Read("select quotationno from quotation where quotationno='" + code + "'").Rows.Count > 0)
            {
                num = num + 1;
                createCodePO(num);
            }
            return code;
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES where status=1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
            }
            catch { }
        }

        private void loadGridLookupEmployeePO()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idemppo_I1, "select IDEMP, StaffName from EMPLOYEES where status=1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemppo_I1.Width);
                glue_idemppo_I1.EditValue = clsFunction.GetIDEMPByUser();
            }
            catch { }
        }

        private void loadGridLookupManager()
        {
            try
            {
                string[] caption = new string[] { "Mã QL", "Tên QL" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idmanager_I1, "select IDEMP, StaffName from EMPLOYEES where ismanager =1 and status=1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idmanager_I1.Width);
            }
            catch { }
        }

        private void loadGridLookupStatus()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Tình trạng", "step" };
                string[] fieldname = new string[] { "idstatusquotation", "statusquotation", "step" };
                string[] col_visible = new string[] { "False", "True", "False" };
                glue_idstatusquotation_I1.Properties.View.Columns.Clear();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select finish from dmstatusquotation where idstatusquotation='" + glue_idstatusquotation_I1.EditValue.ToString() + "' and finish =1");
                if (isImport == false)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusquotation_I1, "select idstatusquotation, statusquotation, step from DMSTATUSQUOTATION where step>=" + stepquotation + " and step <= " + (stepquotation) + "  ORDER BY step", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, 300);
                    }
                    else
                    {
                        ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusquotation_I1, "select idstatusquotation, statusquotation, step from DMSTATUSQUOTATION where step>=" + stepquotation + " and step <= " + (stepquotation + 2) + "  ORDER BY step", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, 300);
                    }
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusquotation_I1, "select idstatusquotation, statusquotation, step from DMSTATUSQUOTATION   ORDER BY step", "statusquotation", "idstatusquotation", caption, fieldname, this.Name, 300);
                }
                glue_idstatusquotation_I1.Properties.View.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo","Lỗi : " + ex.Message);
            }
        }

        private void loadLookupCurrency()
        {
            try
            {
                string[] caption = new string[] { "Mã TT", "Tiền Tệ" };
                string[] fieldname = new string[] { "idcurrency", "currency" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadLookupEdit(lue_idcurrency_I1, "select idcurrency, currency from DMCURRENCY", "currency", "idcurrency", caption, fieldname, statusForm, this.Name, this.Name);
            }
            catch { }
        }

        private void setRowByIDcommodity(int index, string idcommodity)
        {
            try
            {
                // set default
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "status", true);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "spec", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "commodity", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "iddetail", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "idunit", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "idwarehouse", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "quantity", 1);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "price", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "costs", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "status", false);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "davat", false);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "description", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "equipmentinfo", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "index", gv_EXPORTDETAIL_C.DataRowCount + 1);

                if (chk_isvat_I6.Checked == true)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "vat", cal_vat_I4.EditValue);
                }
                else
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "vat", 0);
                }
                if (chk_isdiscount_I6.Checked == true)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", cal_discount_I4.EditValue);
                }
                else
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", 0);
                }
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select idcommodity,isnull(baohanh,0) as baohanh, model, description, equipmentinfo, spec, commodity, idunit, idwarehouse, price, cogs from dmcommodity where idcommodity='" + idcommodity + "' ");
                if (dt.Rows.Count > 0)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "spec", dt.Rows[0]["spec"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "partnumber", dt.Rows[0]["model"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "commodity", dt.Rows[0]["commodity"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "description", dt.Rows[0]["commodity"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "equipmentinfo", dt.Rows[0]["equipmentinfo"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "iddetail", dt.Rows[0]["idcommodity"].ToString() + "_" + txt_idexport_IK1.Text);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idunit", dt.Rows[0]["idunit"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idwarehouse", dt.Rows[0]["idwarehouse"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "quantity", 1);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "price", Convert.ToDouble(dt.Rows[0]["price"].ToString()));
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price")));
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "status", true);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "davat", false);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", 0);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "baohanh", Convert.ToDouble(dt.Rows[0]["baohanh"].ToString()));
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idexport", txt_idexport_IK1.Text);
                }
                //gv_EXPORTDETAIL_C.SetRowCellValue(index, "idgrouptk", "GP000001");
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
            }
        }

        private void setRowByquantity(int index)
        {
            try
            {
                double price = 0;
                double vat = 0;
                double discount = 0;
                double costs = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "price").ToString() != "")
                {
                    price = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs"));
                }
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price * (vat / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price * (discount / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByPrice(int index)
        {
            try
            {
                double quantity = 0;
                double price = 0;
                double vat = 0;
                double discount = 0;
                double costs = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity").ToString() != "")
                {
                    quantity = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "price").ToString() != "")
                {
                    price = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs"));
                }
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", quantity * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price")));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price * (vat / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price * (discount / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByVat(int index)
        {
            try
            {
                double amount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount"));
                }
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(amount * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat")) / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", amount * (Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat")) / 100 + 1));
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByDavat(int index)
        {
            try
            {
                double amount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double discount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount"));
                }
                double vat = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"));
                }
                bool davat = false;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat"));
                }
                if (davat == false)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * discount / 100);
                }
                else
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * (1 + vat / 100) * discount / 100);
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByDiscount(int index)
        {
            try
            {
                bool davat = false;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat"));
                }
                double amount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double amountvat = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat").ToString() != "")
                {
                    amountvat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat"));
                }
                double amountdiscount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount").ToString() != "")
                {
                    amountdiscount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount"));
                }
                if (davat == false) // chiết khấu trước thuế
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount")) / 100);
                    //gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                }
                else
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * (Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat")) / 100 + 1) * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount")) / 100);
                    //gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByAmountDiscount(int index)
        {
            try
            {
                bool davat = false;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(index, "davat"));
                }
                double amount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double amountvat = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat").ToString() != "")
                {
                    amountvat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat"));
                }
                double amountdiscount = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount").ToString() != "")
                {
                    amountdiscount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount"));
                }

                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);


            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private void setRowByCost(int index)
        {
            try
            {
                double total = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "total").ToString() != "")
                {
                    total = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "total"));
                }
                double costs = 0;
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs"));
                }
                double price = 0;
                double vat = 0;
                double discount = 0;

                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "price").ToString() != "")
                {
                    price = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "costs"));
                }

                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            }
        }

        private bool saveMaster()
        {
            bool flag = false;
            if (checkPermissionSaveQuotation() == false)
            {
                clsFunction.MessageInfo("Thông báo", "Bạn không đủ quyền để sửa báo giá này");
                txt_quotationno_50_I2.Focus();
                return false;
            }

         

            if ( APCoreProcess.APCoreProcess.Read("select * from quotation where idexport ='" +txt_idexport_IK1.Text+"'").Rows.Count==0 && checkCallPurchase() == false)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from quotation where quotationno ='" + txt_quotationno_50_I2.Text + "'").Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Thông báo","Trùng số báo giá");
                    return false;
                }
                String code = "";
                txt_idexport_IK1.Text = clsFunction.Insert_data(this, this.Name, code);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update Quotation  set phat=" + spe_phat_I4.Value + ", status='0', isdelete=0,vat=" + Convert.ToDouble(cal_vat_I4.Value) + ", discount=" + Convert.ToDouble(cal_discount_I4.Value) + ",isvat='" + chk_isvat_I6.EditValue + "',isdiscount='" + chk_isdiscount_I6.EditValue + "',    dateimport='" + dte_dateimport_I5.EditValue.ToString() + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "', idemppo='" + glue_idemppo_I1.EditValue.ToString() + "', limit = " + cal_limit_I4.Value + " where idexport='" + txt_idexport_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert.Caption), txt_idexport_IK1.Text, txt_idexport_IK1.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            }
            else
            {
                clsFunction.Edit_data(this, this.Name, "idexport", txt_idexport_IK1.Text);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update quotation set phat=" + spe_phat_I4.Value + ",  status='0',  dateimport='" + dte_dateimport_I5.EditValue.ToString() + "',vat=" + Convert.ToDouble(cal_vat_I4.Value) + ", discount=" + Convert.ToDouble(cal_discount_I4.Value) + ",isvat='" + chk_isvat_I6.EditValue + "',isdiscount='" + chk_isdiscount_I6.EditValue + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "', idemppo='" + glue_idemppo_I1.EditValue.ToString() + "', limit = " + cal_limit_I4.Value + " where idexport='" + txt_idexport_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
            }

            insertFile();
            flag = true;
            return flag;
        }

        private bool checkPermissionSaveQuotation()
        {
            bool flag = false;
            if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
            {
                if (APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 ) AND E.status='True' ORDER BY C.idcustomer").Rows.Count == 0)
                {
                    if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000001" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000006" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000002")
                        flag = true;
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                return true;
            }

            return flag;
        }

        private void saveLogQuotation(string quotation)
        {
            try
            {
                if (gv_EXPORTDETAIL_C.DataRowCount == 0)
                {
                    return;
                }
                string idquotationhis = "";
                idquotationhis = clsFunction.layMa("QH", "idquotationhis", "quotationhis");
                APCoreProcess.APCoreProcess.ExcuteSQL("INSERT INTO QUOTATIONHIS(limit, vat, dateimport, limitdept, idcustomer, IDEMP, "
                + " isdept, outlet, idexport, note, idtable, discount,amountdiscount, surcharge, amountsurcharge, invoice, complet,  retail,"
                + " status, cancel, isdelete, tableunion, isdiscount, issurcharge,  reasondiscount, reasonsurcharge, isvat, isbrowse, reasonbrowse,"
                + " idstatusquotation, idcurrency, exchangerate, fileattack, filename, prepaypercent, postpaidpercent, idquotationtype,quotationno,placedelivery,receiver,invoiceeps, idquotationhis, chatluong, vuotdinhmuc, phat, dieukhoan, nguoichogia, idmanager, hhnv, hhkh, thoigiantt, idcampaign, pokethua, dieukienthanhtoan, hieulucbaogia)"
                + " SELECT limit, vat, dateimport, limitdept, idcustomer, IDEMP, isdept, outlet, idexport, note, idtable, discount,"
                + " amountdiscount, surcharge, amountsurcharge, invoice, complet,  retail, status, cancel, isdelete, tableunion, isdiscount,"
                + " issurcharge, reasondiscount, reasonsurcharge, isvat, isbrowse, reasonbrowse, idstatusquotation, idcurrency, "
                + " exchangerate, fileattack, filename, prepaypercent, postpaidpercent, idquotationtype,quotationno,placedelivery,receiver,invoiceeps,'" + idquotationhis + "', chatluong, vuotdinhmuc, phat, dieukhoan, nguoichogia, idmanager, hhnv, hhkh, thoigiantt, idcampaign, pokethua, dieukienthanhtoan, hieulucbaogia FROM QUOTATION "
                + " WHERE idexport='" + quotation + "';"
                + " INSERT INTO QUOTATIONHISDETAIL (iddetail, idcommodity, idunit, idwarehouse, quantity, price, amount, vat, amountvat, davat,"
                + " discount, amountdiscount, costs, total, note, idquotationhis, status, pending, "
                + " amountsurcharge, surcharge, strquantity, give, isdiscount, reasondiscount, issurcharge, reasonsurcharge, reasongive, "
                + " idpromotion, _index, partnumber,cogs, baohanh, idgrouptk) select iddetail, idcommodity, idunit, idwarehouse, quantity, price, amount, vat, amountvat, davat, "
                + " discount, amountdiscount, costs, total, note, '" + idquotationhis + "', status, pending, amountsurcharge, surcharge, strquantity,"
                + " give, isdiscount, reasondiscount, issurcharge, reasonsurcharge, reasongive, idpromotion, _index, partnumber,cogs, baohanh, idgrouptk from QUOTATIONDETAIL "
                + " where idexport='" + quotation + "'");
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }
        }

        private void insertFile()
        {
            /*string path = "";
            if (btn_file_S.EditValue != null)
            {
                if (File.Exists(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString()))
                    path=(Application.StartupPath + "\\File\\" + btn_file_S.EditValue.ToString());
            }
            if (path != "")
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                byte[] MyData = new byte[fs.Length];
                fs.Read(MyData, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("Select * from QUOTATION where idexport='" + txt_idexport_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    dr["fileattack"] = MyData;
                    dr["filename"] = btn_file_S.EditValue.ToString();
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }*/
        }

        private bool saveDetai()
        {
            if ((glue_idstatusquotation_I1.EditValue.ToString() == "ST000002" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000003" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000004" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000005") && clsFunction.checkAdmin() ==false)
            {
                //clsFunction.MessageInfo("Thông báo", "Sau khi duyệt giá, chỉ admin mới đủ quyền chỉnh sửa báo giá");
               //return true;
            }
            bool flag = false;
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from quotationdetail where idexport='" + txt_idexport_IK1.Text + "'  ");
                DataTable dt = new DataTable();
                for (int i = 0; i < gv_EXPORTDETAIL_C.DataRowCount; i++)
                {
                    dt = APCoreProcess.APCoreProcess.Read("QUOTATIONDETAIL where idexport='" + txt_idexport_IK1.Text + "' and idcommodity='" + gv_EXPORTDETAIL_C.GetRowCellValue(i, "idcommodity").ToString() + "' ");
                    if (dt.Rows.Count == 0 || 1 == 1)// thêm mới
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < gv_EXPORTDETAIL_C.Columns.Count; j++)
                        {
                            if (gv_EXPORTDETAIL_C.Columns[j].Name.Contains("_S") == false)
                            {
                                try
                                {
                                    dr[gv_EXPORTDETAIL_C.Columns[j].FieldName] = gv_EXPORTDETAIL_C.GetRowCellValue(i, gv_EXPORTDETAIL_C.Columns[j].FieldName);
                                }
                                catch { }
                            }
                        }
                        dr["iddetail"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "idcommodity").ToString() + "_" + txt_idexport_IK1.Text;
                        dr["spec"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "spec").ToString();
                        dr["idexport"] = txt_idexport_IK1.Text;

                        dr["status"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "status") != null ? Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(i, "status")) : false;

                        dr["price"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "price").ToString() == "" ? "0" : gv_EXPORTDETAIL_C.GetRowCellValue(i, "price").ToString();
                        dr["idgrouptk"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "idgrouptk").ToString();
                        dr["cogs"] = gv_EXPORTDETAIL_C.GetRowCellValue(i, "cogs").ToString() == "" ? "0" : gv_EXPORTDETAIL_C.GetRowCellValue(i, "cogs").ToString();
                        dr["pending"] = false;
                        dr["give"] = false;
                        dr["_index"] = i + 1;

                        try
                        {
                            dr["cogs"] = Convert.ToInt32(gv_EXPORTDETAIL_C.GetRowCellValue(i, "cogs"));
                        }
                        catch
                        {
                            dr["cogs"] = 0;
                        }

                        dt.Rows.Add(dr);

                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }

                saveLogQuotation(txt_idexport_IK1.Text);

                DataTable dtstep = APCoreProcess.APCoreProcess.Read("SELECT     idstatusquotation, statusquotation, step, allowedit, note, finish, hide FROM  dbo.DMSTATUSQUOTATION where idstatusquotation = '" + glue_idstatusquotation_I1.EditValue.ToString() + "' order by step");
                loadStatusQuotation(dtstep);
                if (APCoreProcess.APCoreProcess.Read("select * from quotation where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Never;
                    glue_idstatusquotation_I1.Enabled = true;
                }
                else
                {
                    btn_allows_delete_S.Visibility = BarItemVisibility.Always;
                    glue_idstatusquotation_I1.Enabled = false;
                }
                flag = true;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Dữ liệu không hợp lệ vui lòng kiểm tra lại" + ex.Message);
                flag = false;
            }
            return flag;
        }

        private bool checkExitsDetail(string idcommodity)
        {
            bool flag = false;
            if (APCoreProcess.APCoreProcess.Read("select * from QUOTATIONEDETAIL where idexport='" + txt_idexport_IK1.Text + "' and idcommodity='" + idcommodity + "'").Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        private bool checkInput()
        {
            bool flag = true;
            if (glue_IDEMP_I1.EditValue == null)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa chọn nhân viên");
                glue_IDEMP_I1.Focus();
                return false;
            }
            if (txt_note_500_I2.Text == "" && 1 == 2)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa nhập nội dung báo giá");
                txt_note_500_I2.Focus();
                return false;
            }
            if (glue_idcustomer_I1.EditValue.ToString() == "")
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa chọn khách hàng");
                glue_idcustomer_I1.Focus();
                return false;
            }
            if (txt_quotationno_50_I2.Text == "")
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa nhập số hiệu báo giá");
                txt_quotationno_50_I2.Focus();
                return false;
            }

            if (dte_datepo_I5.Enabled == true && (dte_datepo_I5.EditValue == null || dte_datepo_I5.EditValue == ""))
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn phải nhập ngày ra PO");
                dte_datepo_I5.Focus();
                return false;
            }

            if (dte_datedelivery_I5.Enabled == true && (dte_datedelivery_I5.EditValue == null || dte_datedelivery_I5.EditValue == ""))
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn phải nhập ngày kết thúc dự kiến của kinh doanh");
                dte_datedelivery_I5.Focus();
                return false;
            }

            if (glue_idpotype_I1.Enabled == true && (glue_idpotype_I1.EditValue == null || glue_idpotype_I1.EditValue == ""))
            {                
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn phải nhập loại PO");
                glue_idpotype_I1.Focus();
                return false;
            }

            int priceTemp = 0;

            if (glue_idstatusquotation_I1.EditValue.ToString() == "ST000002" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000003" || glue_idstatusquotation_I1.EditValue.ToString() == "ST000004")
            {
                for (int i = 0; i < gv_EXPORTDETAIL_C.DataRowCount; i++)
                {

                    if (gv_EXPORTDETAIL_C.GetRowCellValue(i, "idgrouptk") == null || gv_EXPORTDETAIL_C.GetRowCellValue(i, "idgrouptk") == "")
                    {
                        clsFunction.MessageInfo("Thông báo", "Vui lòng chọn loại sản phẩm");
                        return false;
                    }

                    priceTemp += Convert.ToInt32(gv_EXPORTDETAIL_C.GetRowCellValue(i, "price"));                    
                }
            }

            if (priceTemp == 0)
            {
                if (!clsFunction.MessageDelete("Thông báo", "Sản phầm chưa có giá, bạn có muốn tiếp tục lưu dữ liệu không ?"))
                {
                    return false;
                }
            }

            if ((glue_idmanager_I1.EditValue == null || glue_idmanager_I1.EditValue == "") && Convert.ToInt16(APCoreProcess.APCoreProcess.Read("select * from dmstatusquotation where idstatusquotation='" + glue_idstatusquotation_I1.EditValue.ToString() + "'").Rows[0]["step"]) > 2)
            {
                clsFunction.MessageInfo("Thông báo", "Báo giá này chưa được duyệt");
                return false;
            }

            if (txt_invoiceeps_100_I2.Enabled && APCoreProcess.APCoreProcess.Read("select ideport from quotation where invoiceeps ='" + txt_invoiceeps_100_I2.Text + "'").Rows.Count > 0)
            {
                clsFunction.MessageInfo("Thông báo", "Số PO trùng, vui lòng kiểm tra lại");
                txt_invoiceeps_100_I2.Focus();
                return false;
            }

            return flag;
        }

        private bool checkInsert(string idcommodity, double quantity)
        {
            bool flag = false;
            DataTable dtI = new DataTable();
            dtI = APCoreProcess.APCoreProcess.Read("SELECT     dt.quantity - SUM(ISNULL(stk.quantity, 0)) AS quantity, dt.iddetail FROM         PURCHASEDETAIL AS dt INNER JOIN   PURCHASE AS mt ON dt.idpurchase = mt.idpurchase LEFT OUTER JOIN   STOCKIO AS stk ON dt.iddetail = stk.iddetailpur GROUP BY dt.idcommodity, dt.status, dt.iddetail, dt.quantity HAVING      (dt.idcommodity = N'" + idcommodity + "') AND (dt.status = 0) AND (dt.quantity - SUM(ISNULL(stk.quantity, 0)) > " + quantity + ") ");
            if (dtI.Rows.Count > 0)// tồn kho đủ
            {
                flag = true;
            }

            flag = true;
            return flag;
        }

        private bool checkCallPurchase()
        {
            bool flag = false;
      
            DataTable dt = APCoreProcess.APCoreProcess.Read(" SELECT     quotationno, idexport from QUOTATION where quotationno=N'" + txt_quotationno_50_I2.Text + "' and idexport='"+ txt_idexport_IK1.Text +"' ");
            if (dt.Rows.Count > 0)
            {
                flag = true;
                txt_idexport_IK1.Text = dt.Rows[0][1].ToString();
            }
            else
            {
                txt_idexport_IK1.Text = clsFunction.layMa("QS", "idexport", "Quotation");
            }

            return flag;
        }

        private bool checkExitMaHangInGrid(string mahang, DataTable dt)
        {
            int a = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                    break;
                }
            }

            if (a > 0)
                return true;
            return false;
        }

        private bool checkExitMaHangInGridSua(string mahang, DataTable dt)
        {
            int a = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                }
            }
            if (a > 1)
                return true;
            return false;
        }

        private void setVatOrDiscountAll(string fieldName, Decimal value)
        {
            try
            {
                for (int i = 0; i < gv_EXPORTDETAIL_C.RowCount; i++)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(i, fieldName, value);
                }
            }
            catch { }
        }

        private void loadContact()// Nguoi nhan hang
        {
            try
            {
                glue_receiver_I2.Properties.View.Columns.Clear();
                string[] caption = new string[] { "ID", "Liên hệ" };
                string[] fieldname = new string[] { "idcontact", "contact" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_receiver_I2, "select idcontact, contactname + ' - ' + tel as contact from cuscontact  where idcustomer='" + glue_idcustomer_I1.EditValue + "' ", "contact", "idcontact", caption, fieldname, this.Name, glue_receiver_I2.Width, col_visible);

            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void exportExcelTeamplate(string path, GridView gv)
        {
            try
            {
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
                workbook.Worksheets[1].Cells[3, 5] = "(Kèm " + txt_quotationno_50_I2.Text + ")";
                workbook.Worksheets[1].Cells[4, 7] = Convert.ToDateTime(dte_datepo_I5.EditValue).ToString("dd/MM/yyyy");
                workbook.Worksheets[1].Cells[5, 3] = glue_receiver_I2.Text.Split('-')[0];
                workbook.Worksheets[1].Cells[6, 3] = glue_idcustomer_I1.Text;
                workbook.Worksheets[1].Cells[6, 11] = Convert.ToDateTime(dte_datedelivery_I5.EditValue).ToString("dd/MM/yyyy");
                workbook.Worksheets[1].Cells[7, 3] = txt_address_S.Text;
                workbook.Worksheets[1].Cells[7, 11] = cal_limit_I4.Value;
                workbook.Worksheets[1].Cells[8, 3] = APCoreProcess.APCoreProcess.Read("select tax from dmcustomers where idcustomer='" + glue_idcustomer_I1.EditValue + "' ").Rows[0][0].ToString();
                workbook.Worksheets[1].Cells[8, 11] = txt_quotationno_50_I2.Text;
                workbook.Worksheets[1].Cells[9, 3] = txt_placedelivery_500_I2.Text;
                workbook.Worksheets[1].Cells[9, 11] = rad_isdept_I6.EditValue == "0" ? "TM" : "CK";
                workbook.Worksheets[1].Cells[10, 3] = txt_tel_S.Text;
                workbook.Worksheets[1].Cells[10, 6] = txt_fax_S.Text;
                workbook.Worksheets[1].Cells[10, 11] = txt_invoiceeps_100_I2.Text;

                for (int i = 12; i < gv.RowCount + 12; i++)
                {
                    DataTable dtCommodity = new DataTable();
                    dtCommodity = APCoreProcess.APCoreProcess.Read("select * from dmcommodity where idcommodity = '" + gv_EXPORTDETAIL_C.GetRowCellValue(i - 12, "idcommodity").ToString() + "'");
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
                    workbook.Worksheets[1].Cells[i + 2, 11] = Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(i - 12, "amount")).ToString("N0");

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
                workbook.Worksheets[1].Cells[gv.RowCount + 13 + 2, 7] = cal_vat_I4.Value.ToString() + " %";
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
                workbook.Worksheets[1].Cells[gv.RowCount + 18 + 2, 2] = glue_IDEMP_I1.Text;

                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 10] = "HHNV: ";
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 10] = "HHKH: ";

                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 22 + 2, 11] = txt_hhnv_I2.Text != "" ? (txt_hhnv_I2.Text + " %") : "";
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                workbook.Worksheets[1].Cells[gv.RowCount + 23 + 2, 11] = txt_hhkh_I2.Text != "" ? (txt_hhkh_I2.Text + " %") : "";

                xlApp.Visible = true;
                //worksheet.Columns.AutoFit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void frm_QUOTATION_S_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (isFormChanged == true && 1 == 2)
                {
                    if (!clsFunction.MessageDelete("Thông báo", "Dữ liệu có thay đỗi, bạn có muốn thoát form không ?"))
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch { }
        }

        private void SetFormStateChangeHandlers(Control parent)
        {
            isFormChanged = false;

            foreach (Control control in parent.Controls)
            {
                if (control.GetType().Name.Contains("Button"))
                {
                    isFormChanged = false;
                }
                // Attach to text changed event
                EventInfo eventInfo = control.GetType().GetEvent("TextChanged",
                        BindingFlags.Instance | BindingFlags.Public);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(control, new EventHandler(ControlStateChanged));
                }

                //// Attach to value changed event
                eventInfo = control.GetType().GetEvent("ValueChanged",
                        BindingFlags.Instance | BindingFlags.Public);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(control, new EventHandler(ControlStateChanged));
                }

                // handle container controls which might have child controls
                if (control.Controls.Count > 0)
                {
                    SetFormStateChangeHandlers(control);
                }
            }
        }

        /// <summary>
        /// Controls the state changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ControlStateChanged(object sender, EventArgs e)
        {
            isFormChanged = true;
        }

        private void btn_fileattack_S_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DataTable dtcogfig = new DataTable();
            String pathRoot = Application.StartupPath + "\\File";
            dtcogfig = APCoreProcess.APCoreProcess.Read("sysconfig");
            if (dtcogfig.Rows.Count > 0)
            {
                if (clsFunction.checkFolderExist(dtcogfig.Rows[0]["config"].ToString()))
                {
                    pathRoot = dtcogfig.Rows[0]["config"].ToString() + "\\File";
                }
            }

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Pdf files (*.pdf)|*.Pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            String filename = pathRoot + "\\" + clsFunction._iduser + txt_idexport_IK1.Text + System.IO.Path.GetFileName(openFileDialog1.FileName);
                            clsFunction.Copy_File(openFileDialog1.FileName, filename);
                            txt_filename_I2.Text = txt_filename_I2.Text + System.IO.Path.GetFileName(openFileDialog1.FileName) + "/";
                            loadFile(txt_filename_I2.Text);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set filename='" + txt_filename_I2.Text + "' where idexport='" + txt_idexport_IK1.Text + "'");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            if (txt_filename_I2.Text != "")
            {
                btn_openfile_S.Enabled = true;
                btn_deletefile_S.Enabled = true;
            }
            else
            {
                btn_openfile_S.Enabled = false;
                btn_deletefile_S.Enabled = false;
            }
        }

        private String getStringFile()
        {
            String str = "";
            for (int i = 0; i < gv_file_C.DataRowCount; i++)
            {
                str += gv_file_C.GetRowCellValue(i, "filename").ToString() + "/";
            }
            return str;
        }

        private void addFiles(String filename)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)gct_file_C.DataSource;
            if (dt == null)
            {
                dt.Columns.Add("filename", typeof(String));
                dt.Columns.Add("option", typeof(String));
            }
            int index = checkFileName(filename);
            if (index != -1)
            {
                DataRow dr = dt.NewRow();
                dr["filename"] = filename;
                dr["option"] = clsFunction.transLateText("Xóa");
                dt.Rows.Add(dr);
            }
            else
            {
                DataRow dr = dt.Rows[index];
                dr["filename"] = filename;
                dr["option"] = clsFunction.transLateText("Xóa");
                dt.Rows.Add(dr);
            }
            gct_file_C.DataSource = dt;
        }

        private void loadFile(String filenames)
        {
            String[] arr = filenames.Split('/');
            DataTable dt = new DataTable();

            DataColumn workCol = dt.Columns.Add("filename", typeof(String));
            workCol.AllowDBNull = false;
            workCol.Unique = true;

            dt.Columns.Add("option", typeof(String));
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int index = checkFileName(arr[i]);
                if (index >= -1)
                {
                    DataRow dr = dt.NewRow();
                    dr["filename"] = arr[i];
                    dr["option"] = clsFunction.transLateText("Xóa");
                    dt.Rows.Add(dr);
                }
                //else
                //{
                //    DataRow dr = dt.Rows[index];
                //    dr["filename"] = arr[i];
                //    dr["option"] = clsFunction.transLateText("Xóa");
                //    dt.Rows.Add(dr);
                //}
            }
            gct_file_C.DataSource = dt;
        }

        private int checkFileName(String filename)
        {
            int index = -1;
            for (int i = 0; i < gv_file_C.RowCount; i++)
            {
                if (gv_file_C.GetRowCellValue(i, "filename").ToString() == filename)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void btn_deletefile_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txt_filename_I2.Text))
                {
                    File.Delete(txt_filename_I2.Text);
                    txt_filename_I2.Text = "";
                    if (txt_filename_I2.Text != "")
                    {
                        btn_openfile_S.Enabled = true;
                        btn_deletefile_S.Enabled = true;
                    }
                    else
                    {
                        btn_openfile_S.Enabled = false;
                        btn_deletefile_S.Enabled = false;
                    }
                }
            }
            catch { }
        }

        private void btn_openfile_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txt_filename_I2.Text))
                {
                    Process.Start(txt_filename_I2.Text);
                }
            }
            catch { }
        }

        private void gv_file_C_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            String path = Application.StartupPath + "\\File" + "\\" + clsFunction._iduser + txt_idexport_IK1.Text;
            if (e.Column == gcFileName)
            {

                try
                {
                    if (File.Exists(path + gv_file_C.GetRowCellValue(e.RowHandle, "filename").ToString()))
                    {
                        Process.Start(path + gv_file_C.GetRowCellValue(e.RowHandle, "filename").ToString());
                    }
                }
                catch
                {
                    clsFunction.MessageInfo("Thông báo", "File không tìm thấy");
                }
            }
            if (e.Column == gcDelete)
            {
                if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa file này không"))
                {
                    File.Delete(path + gv_file_C.GetRowCellValue(e.RowHandle, "filename").ToString());
                    gv_file_C.DeleteRow(e.RowHandle);
                    txt_filename_I2.Text = getStringFile();
                    APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set filename='" + txt_filename_I2.Text + "' where idexport='" + txt_idexport_IK1.Text + "'");
                }
            }
        }

        private DataTable loadFileExcel()
        {
            DataTable dtExCel = new DataTable();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            try
            {
                String version = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {

                        if (Path.GetExtension(openFileDialog1.FileName) == "xls")
                            version = "2003";
                        else
                            version = "2007";

                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName, Convert.ToInt16(version));
                        dtExCel = excel.getDataFromExcel(0);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dtExCel;
        }

        private void bbi_allow_import_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            int dongloi = 0;
            cal_vat_I4.Value = 0;
            cal_discount_I4.Value = 0;
            Init();
            isImport = true;
            DataTable dtD = new DataTable();
            try
            {
                DataTable dtx = loadFileExcel();

                String idcommodity = "";
                String commodity = "";
                String partnumber = "";
                String spec = "";
                String info = "";
                Int32 quantity = 0;
                Int32 price = 0;
                Int32 giagoc = 0;
                String deliveryTime = "";
                String note = "";
                String TenDVT = "";
                bool type_9 = true;

                dtD = APCoreProcess.APCoreProcess.Read("SELECT   dt.status, dt.baohanh, dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity,dt.price, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.sign, mh.commodity, dt.timedelivery, mh.spec, dt.partnumber, dt.cogs, dt.equipmentinfo, dt.description, dt.idgrouptk FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport=''");

                String quotationno = dtx.Rows[4][8].ToString().Trim();
                String tienTe = "";
                DateTime NgayBG = new DateTime();
                try
                {
                    if (quotationno == "" || quotationno.Length < 5)
                    {
                        quotationno = dtx.Rows[4][9].ToString().Trim();
                        type_9 = false;
                        tienTe = dtx.Rows[11][10].ToString().Trim();
                        NgayBG = DateTime.ParseExact(dtx.Rows[5][9].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        tienTe = dtx.Rows[11][8].ToString().Trim();
                        NgayBG = DateTime.ParseExact(dtx.Rows[5][8].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (NgayBG >= new DateTime(2017, 05, 01))
                    {
                        clsFunction.MessageInfo("Thông báo", "Bạn không thể import báo giá sau ngày 01/05/2015");
                        return;
                    }
                    DataTable dtcheckDuplicate = new DataTable();
                    dtcheckDuplicate = APCoreProcess.APCoreProcess.Read("Select idexport from quotation where quotationno = '" + quotationno + "'");
                    if (dtcheckDuplicate.Rows.Count > 0)
                    {
                        if (!clsFunction.MessageDelete("Thông báo", "Mã báo giá này đã tồn tại, Bạn có muốn cập nhật đè báo giá này không ?"))
                        {
                            Init();
                            return;
                        }
                    }
                }

                catch { }
                txt_quotationno_50_I2.Text = quotationno;
                dte_dateimport_I5.EditValue = NgayBG;
                DataTable dtTT = APCoreProcess.APCoreProcess.Read("select idcurrency from DMCURRENCY where currency =N'" + tienTe + "'");
                if (dtTT.Rows.Count > 0)
                {
                    String idTienTe = dtTT.Rows[0][0].ToString();
                    lue_idcurrency_I1.EditValue = idTienTe;
                }
                else
                {
                    lue_idcurrency_I1.EditValue = null;
                }
                String cusName = dtx.Rows[8][2].ToString().Trim();
                DataTable dtkh = APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' AND C.customer=N'" + cusName + "' ORDER BY C.idcustomer");
                if (dtkh.Rows.Count == 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Không tồn tại khách hàng  " + cusName + " trong dữ liệu, vui lòng kiểm tra lại");
                    return;
                }
                else
                {
                    glue_idcustomer_I1.EditValue = dtkh.Rows[0][0].ToString();
                    txt_address_S.Text = dtkh.Rows[0][4].ToString();
                    txt_fax_S.Text = dtkh.Rows[0][3].ToString();
                    txt_tel_S.Text = dtkh.Rows[0][2].ToString();

                    cal_discount_I4.Value = 0;

                    cal_vat_I4.Value = 0;

                    loadContact();

                }
                int countRow = 14;
                for (int i = 14; i < dtx.Rows.Count; i++)
                {
                    if (!clsFunction.checkDigit(dtx.Rows[i][0].ToString()))
                    {
                        break;
                    }
                    dongloi = i;

                    commodity = dtx.Rows[i][1].ToString();
                    if (type_9 == true)
                    {
                        TenDVT = dtx.Rows[i][3].ToString();
                    }
                    else
                    {
                        TenDVT = dtx.Rows[i][5].ToString();
                    }
                    String MaDVT = "";
                    DataTable dtDVT = APCoreProcess.APCoreProcess.Read("select idunit from dmunit where unit = N'" + TenDVT + "'");
                    if (dtDVT.Rows.Count > 0)
                    {
                        MaDVT = dtDVT.Rows[0][0].ToString();
                    }
                    if (commodity != "" && (countRow + 2) >= i)
                    {
                        countRow = i;
                        if (type_9 == true)
                        {

                            partnumber = "";
                            spec = dtx.Rows[i][2].ToString();
                            info = "";

                            try
                            {
                                quantity = Convert.ToInt32(dtx.Rows[i][4].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                            }
                            catch
                            {
                                quantity = 0;
                            }
                            try
                            {
                                price = Convert.ToInt32(dtx.Rows[i][5].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                            }
                            catch
                            {
                                price = 0;
                            }

                            giagoc = 0;
                            deliveryTime = dtx.Rows[i][7].ToString();
                            note = dtx.Rows[i][8].ToString();
                        }
                        else
                        {
                            partnumber = dtx.Rows[i][2].ToString();
                            spec = dtx.Rows[i][3].ToString();
                            info = dtx.Rows[i][4].ToString();
                            try
                            {
                                quantity = Convert.ToInt32(dtx.Rows[i][6].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                            }
                            catch
                            {
                                quantity = 0;
                            }
                            giagoc = 0;
                            try
                            {
                                price = Convert.ToInt32(dtx.Rows[i][7].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                            }
                            catch
                            {
                                price = 0;
                            }
                            deliveryTime = dtx.Rows[i][9].ToString();
                            note = dtx.Rows[i][10].ToString();
                        }

                        DataTable dt = APCoreProcess.APCoreProcess.Read("select * from dmcommodity where commodity = N'" + commodity.Trim() + "' and spec = N'" + spec + "' ");
                        if (dt.Rows.Count == 0 || commodity == "")
                        {
                            if (commodity != "")
                            {
                                insertCommodityTemp(dt, commodity, spec, info, price, MaDVT);
                                idcommodity = dt.Rows[0]["idcommodity"].ToString();
                                getStringValueCommodity(idcommodity);
                            }
                        }
                        else
                        {
                            idcommodity = dt.Rows[0]["idcommodity"].ToString();
                        }

                        DataRow dr = dtD.NewRow();
                        dr["idcommodity"] = idcommodity;
                        dr["commodity"] = commodity;
                        dr["partnumber"] = partnumber;
                        dr["spec"] = spec;
                        dr["equipmentinfo"] = info;
                        dr["quantity"] = quantity;
                        dr["cogs"] = giagoc;
                        dr["costs"] = 0;
                        dr["idunit"] = MaDVT;
                        dr["vat"] = cal_vat_I4.Value;
                        dr["discount"] = 0;
                        dr["amount"] = quantity * price;
                        dr["status"] = true;
                        dr["baohanh"] = 0;
                        dr["price"] = price;
                        dr["timedelivery"] = deliveryTime;
                        dr["note"] = note;
                        dr["idgrouptk"] = "";
                        String idexport = clsFunction.layMa("QS", "idexport", "quotation");
                        dr["iddetail"] = idcommodity + "_" + idexport;
                        dtD.Rows.Add(dr);
                    }
                }

                if (dtD.Rows.Count > 0)
                {
                    gct_list_C.DataSource = dtD;
                    cal_vat_I4.Value = 10;
                    cal_vat_I4.Value = 0;
                }

                Int32 amountVat = 0;
                Int32 total = 0;
                if (type_9)
                {
                    total = Convert.ToInt32(dtx.Rows[countRow + 1][6].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                    amountVat = Convert.ToInt32(dtx.Rows[countRow + 2][6].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                }
                else
                {
                    total = Convert.ToInt32(dtx.Rows[countRow + 1][8].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                    amountVat = Convert.ToInt32(dtx.Rows[countRow + 2][8].ToString().Replace(" ", "").Replace(".", "").Replace(",", ""));
                }
                double vat = 0;
                vat = Convert.ToDouble(amountVat) * 100 / total;
                cal_vat_I4.Value = Convert.ToInt16(vat);

                txt_placedelivery_500_I2.Text = dtx.Rows[countRow + 6][2].ToString();
                mmo_chatluong_I3.Text = dtx.Rows[countRow + 7][2].ToString();
            }
            catch (Exception ex)
            {
                isImport = false;
                clsFunction.MessageInfo("Thông báo", "Import không thành công " + dongloi);
            }
        }

        private void insertCommodityTemp(DataTable dt, String commodity, String spec, String info, int price, String MaDVT)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dr["idcommodity"] = clsFunction.layMa("HH", "idcommodity", "DMCOMMODITY");
                dr["commodity"] = commodity;
                dr["spec"] = spec;
                dr["price"] = price;
                dr["equipmentinfo"] = info;
                dr["idunit"] = MaDVT;
                dr["status"] = true;
                dr["quantity"] = true;
                dr["mininventory"] = 0;
                dr["cogs"] = 0;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void gv_EXPORTDETAIL_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcommodity"].ColumnEdit)).View.ActiveFilter.Clear();
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idcommodity"].ColumnEdit)).View.LayoutChanged();
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_EXPORTDETAIL_C.Columns["idunit"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idunit, unit from dmunit");
            loadGridDoiThu();
        }

        private void loadGridDoiThu()
        {
            try
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select iddoithu, tendoithu, loaidoithu,giaban, ngaythamkhao, tel, email, address from doithu d inner join dmloaidoithu l on d.idloaidoithu = l.idloaidoithu where idquotationdetail ='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "' ");
                gct_doithu_C.DataSource = dt;
            }
            catch { }
        }

        private void getGridDoiThu(bool val)
        {
            try
            {
                if (val == false)
                    return;
                DataTable dt = APCoreProcess.APCoreProcess.Read("select iddoithu, tendoithu, loaidoithu,giaban, ngaythamkhao, tel, email, address from doithu d inner join dmloaidoithu l on d.idloaidoithu = l.idloaidoithu where idquotationdetail ='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "' ");
                gct_doithu_C.DataSource = dt;
            }
            catch { }
        }


        private void getGridCSBG(bool val)
        {
            try
            {
                if (val == false)
                    return;
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from CSBG where idexport='"+ txt_idexport_IK1.Text +"'");
               
                gct_CSBG_C.DataSource = dt;
            }
            catch { }
        }

        private void gv_EXPORTDETAIL_C_ShownEditor(object sender, EventArgs e)
        {
            GridLookUpEdit editor = gv_EXPORTDETAIL_C.ActiveEditor as GridLookUpEdit;
            if (editor != null)
            {
                editor.Properties.PopupFormMinSize = new Size(800, 500);
            }
        }

        private void glue_idquotationtype_I1_EditValueChanged(object sender, EventArgs e)
        {
            if (isload == true)
            {
                return;
            }
            if (glue_idquotationtype_I1.EditValue.ToString() != "QT000003")
            {
                lbl_phat_S.Visible = false;
                spe_phat_I4.Enabled = false;
                mmo_dieukhoan_I3.Text = "";
            }
            else
            {
                lbl_phat_S.Visible = false;
                spe_phat_I4.Enabled = false;
                mmo_dieukhoan_I3.Text = " - Những hư hỏng do lỗi kỹ thuật , hao mòn công ty EPS chịu phí sửa chữa.\n"
               + "- Công ty EPS sẽ bảo trì định kỳ xe, thay nhớt, lọc…\n"
               + "- Những hư hỏng do tác động bên ngoài như va chạm, ..do người vận hành không đúng thì BÊN THUÊ chịu trách nhiệm sửa chữa\n"
               + "- Một năm thay 2 vỏ xe trước và 2 vỏ xe sau.\n"
               + "- Sửa chữa đại tu động cơ trong vòng 5 ngày.\n"
               + "- Sửa chữa đại tu hộp số trong vòng 3 ngày.\n"
               + "- Sửa chữa nhỏ trong vòng 2 ngày.\n"
               + "- Xe ngưng hoạt động để sửa chửa 6 ngày phải đổi xe khác.\n"
               + "-  Thời gian kỹ thuật có mặt kiểm tra sửa chữa: 24-36h kể từ lúc nhận thông tin không tính ngày lễ và chủ nhật tính từ thời điểm tiếp nhận thông tin.\n"
               + "+ 8h-12h tính trong ngày.\n"
               + "+ Sau 12h tính ngày hôm sau.\n"
               + "- Xe ngừng hoạt động từ ngày thứ 2 mà kỹ thuật không đến kiểm tra sửa chữa không tính phí.\n"
               + "- Khi bàn giao xe, kiểm tra định kỳ số giờ hoạt động của xe phải lập biên bản.";
            }
            if (glue_idquotationtype_I1.EditValue.ToString() != "QT000004")
            {
                mmo_chatluong_I3.Text = " Hàng mới 100%, bảo hành 6 tháng theo tiêu chuẩn của Nhà sản xuất.";
                mmo_dieukienthanhtoan_I3.Text = " - Hàng có sẵn thanh toán 100% giá trị ngay sau khi nhận được hàng, thời hạn không quá 7 ngày kề từ ngày nghiệm thu hàng hóa.\n"
                +" - Hàng đặt phải kí quỹ 30% giá trị đơn hàng ngay sau khi xác nhận đặt hàng. Thanh toán 70% còn lại của đơn hàng trong vòng trong vòng 7 ngày kể từ ngày nghiệm thu hàng hóa.";
                mmo_hieulucbaogia_I3.Text = "báo giá trên có hiệu lực trong vòng 15 ngày kể từ ngày báo giá.";
            }
            else
            {
                mmo_chatluong_I3.Text = "- Mới 100% \n"
                + "- Nguyên liệu thép: Theo tiêu chuẩn Nhật Jis 3101.\n"
                +"- Khung cột  FΩ90*2.0 được thiết kế định hình, có dạng Ω , mỗi khung gồm 2 cột và các thanh giằng ngang và chéo đựơc liên kết bằng các bulon.\n"
                +"- Cột và thanh giằng được sơn tĩnh điện màu xanh.  \n"
                +"- Thanh beam, cột bảo vệ, chân đế : sơn tĩnh điện màu cam.\n"
                +"- Tòan bộ kệ được sơn tĩnh điện theo màu tiêu chuẩn. \n"
                +"- Sơn: Toàn bộ hệ thống kệ selective đựơc xử lý bề mặt theo công nghệ Châu Âu trước khi sơn tĩnh điện.\n"
                +"- Tải trọng thiết kế: 700 Kg/ Pallet \n"
                +"- Độ võng cho phép( Max): +- 0.5%L. Dung sai vật liệu +-5%.\n"
                +"- Hệ số an toàn: K=1,2 \n"
                +"- Bảo hành 36 tháng.";
                mmo_dieukienthanhtoan_I3.Text = " - Thanh toán chia làm 2 đợt: Đợt 1: 50% ngay sau khi ký hợp đồng. Đợt 2 ngay sau khi ký biên bản bàn giao và nghiệm thu hàng hóa.";
                mmo_hieulucbaogia_I3.Text = "báo giá trên có hiệu lực trong vòng 45 ngày kể từ ngày báo giá.";
            }

            if (glue_idquotationtype_I1.EditValue.ToString() == "QT000001")
            {
                gv_EXPORTDETAIL_C.Columns["spec"].Visible = false;
            }
            else {
                gv_EXPORTDETAIL_C.Columns["spec"].Visible = true;
            }
        }

        private void bbi_deletecommodity_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!clsFunction.GetIDEMPByUser().Equals(glue_IDEMP_I1.EditValue.ToString()))
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có người tạo báo giá mới có thể xóa hoặc chỉnh sửa báo giá");
                return;
            }
            deleteCom();
        }

        private void deleteCom()
        {
            try
            {
                if (glue_idstatusquotation_I1.EditValue == "ST000005" || glue_idstatusquotation_I1.EditValue == "ST000004")
                {
                    clsFunction.MessageInfo("Thông báo", "Báo giá đã chốt hoặc hủy bạn không thể xóa");
                    return;
                }
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from quotationdetail where iddetail='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "'");

                        gv_EXPORTDETAIL_C.DeleteRow(gv_EXPORTDETAIL_C.FocusedRowHandle);
                        //if (gv_EXPORTDETAIL_C.DataRowCount == 0)
                        //{
                        //    APCoreProcess.APCoreProcess.ExcuteSQL("delete quotation where idexport='" + txt_idexport_IK1.Text + "'");
                        //}
                    }
                }
            }
            catch { }
        }

        private void btn_up_S_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gv_EXPORTDETAIL_C;
                view.GridControl.Focus();
                int index = view.FocusedRowHandle;
                if (index <= 0) return;
                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index - 1);
                for (int i = 0; i < gv_EXPORTDETAIL_C.Columns.Count; i++)
                {
                    object val1 = row1[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)];
                    object val2 = row2[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)];
                    row1[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)] = val2;
                    row2[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)] = val1;
                }
                view.FocusedRowHandle = index - 1;
            }
            catch { }
        }

        private void btn_down_S_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gv_EXPORTDETAIL_C;
                view.GridControl.Focus();
                int index = view.FocusedRowHandle;
                if (index >= view.DataRowCount - 1) return;

                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index + 1);

                for (int i = 0; i < gv_EXPORTDETAIL_C.Columns.Count; i++)
                {
                    object val1 = row1[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)];
                    object val2 = row2[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)];
                    row1[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)] = val2;
                    row2[clsFunction.getNameControls(gv_EXPORTDETAIL_C.Columns[i].Name)] = val1;
                }

                view.FocusedRowHandle = index + 1;
            }
            catch { }
        }

        private void txt_hhnv_I2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_allow_insert_unit_S_Click(object sender, EventArgs e)
        {
            try
            {
                SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S frm = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S();
                frm.strpassData = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S.strPassData(getStringValueUnit);
                frm._insert = true;
                frm.call = true;
                frm._sign = "DV";
                frm.ShowDialog();
            }
            catch { }
        }

        private void glue_idpotype_I1_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (glue_idpotype_I1.EditValue != null)
            {
                dt = APCoreProcess.APCoreProcess.Read("select sign from dmpotype where idpotype ='" + glue_idpotype_I1.EditValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    String code = createIDPO(1, dt.Rows[0][0].ToString());
                    if (APCoreProcess.APCoreProcess.Read("select invoiceeps from quotation where (invoiceeps like '%" + dt.Rows[0][0].ToString() + "' or invoiceeps is null) and idexport = '" + txt_idexport_IK1.Text + "' ").Rows.Count == 0)
                    {
                        //txt_invoiceeps_100_I2.Text = code;
                    }
                }
            }
        }

        private String createIDPO(int num, String sign)
        {
            String code = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1  invoiceeps from QUOTATION where invoiceeps like 'PO%' and invoiceeps like '%" + "-" + Convert.ToDateTime(dte_dateimport_I5.EditValue).ToString("MMyy") + "/"+ sign + "' order by invoiceeps desc");
            int index = 0;
            String sIndex = "0";
            try
            {
                sIndex = dt.Rows[0][0].ToString().Substring(2, 3);
                index = Convert.ToInt32(sIndex);
            }
            catch
            {
            }
            code = "PO" + (index + num).ToString().PadLeft(3, '0') + "-" + DateTime.Now.ToString("MMyy") + "/" + sign;
            if (APCoreProcess.APCoreProcess.Read("select invoiceeps from quotation where invoiceeps='" + code + "'").Rows.Count > 0)
            {
                num = num + 1;
                createIDPO(num, sign);
            }
            return code;
        }

        private void btn_allow_survey_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (APCoreProcess.APCoreProcess.Read("select idexport from quotation where quotationno ='" + txt_quotationno_50_I2.Text + "'").Rows.Count == 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Báo giá chưa được khởi tạo");
                    return;
                }

                menu.HidePopup();
                SOURCE_FORM_CRM.Presentation.frm_survey_S frm = new SOURCE_FORM_CRM.Presentation.frm_survey_S();
                frm._insert = true;
                frm._sign = "KS";
                frm.idquotation = txt_idexport_IK1.Text;
                frm.idcustomer = glue_idcustomer_I1.EditValue.ToString();
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_plan_allow_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S();
                frm._insert = true;
                frm.passData = new SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S.PassData(getGridDoiThu);
                frm._sign = "DT";
                frm.txt_commodity_S.Text = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "commodity");
                frm.idcommodity = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcommodity").ToString();
                frm.idquotationdetail = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString();
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Sản phầm chưa được chọn, vui lòng kiểm tra lại");
            }
        }

        private void bbi_plan_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete DOITHU where iddoithu = '" + gv_doithu_C.GetRowCellValue(gv_doithu_C.FocusedRowHandle, "iddoithu").ToString() + "'");
                gv_doithu_C.DeleteRow(gv_doithu_C.FocusedRowHandle);
            }
        }

        private void bbi_plan_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S();
                frm._insert = false;
                frm.txt_iddoithu_IK1.Text = gv_doithu_C.GetRowCellValue(gv_doithu_C.FocusedRowHandle, "iddoithu").ToString();
                frm.ID = gv_doithu_C.GetRowCellValue(gv_doithu_C.FocusedRowHandle, "iddoithu").ToString();
                frm.passData = new SOURCE_FORM_QUOTATION.Presentation.frm_DOITHU_S.PassData(getGridDoiThu);
                frm._sign = "DT";
                //frm.txt_commodity_S.Text = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcommodity");
                //frm.txt_idcommodity_I2.Text = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcommodity").ToString();
                //frm.txt_idquotationdetail_I2.Text = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString();
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void gv_doithu_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_doithu_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_doithu_C, menu, gv_doithu_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void bbi_allow_insert_copy_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gv_EXPORTDETAIL_C.DataRowCount == 0)
            {
                clsFunction.MessageInfo("Thông báo", "Không thể tách báo giá không có sản phẩm nào");
                return;
            }

            int rowAdd = 0;
            for (int i = 0; i < gv_EXPORTDETAIL_C.DataRowCount; i++)
            {
                if ((Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(i, "status").ToString() == "" ? false : gv_EXPORTDETAIL_C.GetRowCellValue(i, "status"))) == true)
                {
                    rowAdd++;
                }
            }
            if (rowAdd < 1)
            {
                clsFunction.MessageInfo("Thông báo", "Bạn phải chọn ít nhất 1 sản phẩm");
                return;
            }

            String idexport = txt_idexport_IK1.Text;

            String idexportNew = copyQuotation(idexport, idexport, true, false);
            copyQuotationDetail(idexport, idexportNew);
            // delete các dong khong dc chọn
            for (int i = 0; i < gv_EXPORTDETAIL_C.DataRowCount; i++)
            {
                if ((Convert.ToBoolean(gv_EXPORTDETAIL_C.GetRowCellValue(i, "status").ToString() == "" ? false : gv_EXPORTDETAIL_C.GetRowCellValue(i, "status"))) != true)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from quotationdetail where idexport ='" + idexportNew + "' and idcommodity ='" + gv_EXPORTDETAIL_C.GetRowCellValue(i, "idcommodity").ToString() + "'");
                }
            }

            saveLogQuotation(idexportNew, idexport);

            loadPurchase(idexportNew);
            txt_idexport_IK1.Text = idexportNew;
            clsFunction.MessageInfo("Thông báo", "Tách báo giá thành công, mã báo giá mới : " + idexportNew);
            txt_quotationno_50_I2.Enabled = true;

        }

        private void saveLogQuotation(string quotationNew, String quotationOld)
        {
            try
            {
                string idquotationhis = "";
                idquotationhis = clsFunction.layMa("QH", "idquotationhis", "quotationhis");
                APCoreProcess.APCoreProcess.ExcuteSQL("INSERT INTO QUOTATIONHIS(limit, vat, dateimport, limitdept, idcustomer, IDEMP, "
                + " isdept, outlet, idexport, note, idtable, discount,amountdiscount, surcharge, amountsurcharge, invoice, complet,  retail,"
                + " status, cancel, isdelete, tableunion, isdiscount, issurcharge,  reasondiscount, reasonsurcharge, isvat, isbrowse, reasonbrowse,"
                + " idstatusquotation, idcurrency, exchangerate, fileattack, filename, prepaypercent, postpaidpercent, idquotationtype,quotationno,placedelivery,receiver,invoiceeps, idquotationhis, chatluong, vuotdinhmuc, phat, dieukhoan, nguoichogia, idmanager, hhnv, hhkh, thoigiantt, idcampaign)"
                + " SELECT limit, vat, dateimport, limitdept, idcustomer, IDEMP, isdept, outlet, '" + quotationNew + "' as idexport, note, idtable, discount,"
                + " amountdiscount, surcharge, amountsurcharge, invoice, complet,  retail, status, cancel, isdelete, tableunion, isdiscount,"
                + " issurcharge, reasondiscount, reasonsurcharge, isvat, isbrowse, reasonbrowse, idstatusquotation, idcurrency, "
                + " exchangerate, fileattack, filename, prepaypercent, postpaidpercent, idquotationtype,quotationno,placedelivery,receiver,invoiceeps,'" + idquotationhis + "', chatluong, vuotdinhmuc, phat, dieukhoan, nguoichogia, idmanager, hhnv, hhkh, thoigiantt, idcampaign FROM QUOTATION "
                + " WHERE idexport='" + quotationOld + "';"
                + " INSERT INTO QUOTATIONHISDETAIL (iddetail, idcommodity, idunit, idwarehouse, quantity, price, amount, vat, amountvat, davat,"
                + " discount, amountdiscount, costs, total, note, idquotationhis, status, pending, "
                + " amountsurcharge, surcharge, strquantity, give, isdiscount, reasondiscount, issurcharge, reasonsurcharge, reasongive, "
                + " idpromotion, _index, partnumber,cogs, baohanh, idgrouptk) select iddetail, idcommodity, idunit, idwarehouse, quantity, price, amount, vat, amountvat, davat, "
                + " discount, amountdiscount, costs, total, note, '" + idquotationhis + "', status, pending, amountsurcharge, surcharge, strquantity,"
                + " give, isdiscount, reasondiscount, issurcharge, reasonsurcharge, reasongive, idpromotion, _index, partnumber,cogs, baohanh, idgrouptk from QUOTATIONDETAIL "
                + " where idexport='" + quotationOld + "'");
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }
        }

        private String copyQuotation(String idexportOld, String quotationOrginal, bool tachBG, bool gopBG)
        {

            String idexportNew = "";
            try
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from quotation where idexport = '" + idexportOld + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    idexportNew = clsFunction.layMa("QS", "idexport", "quotation");
                    dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "idexport")
                        {
                            dr["idexport"] = idexportNew;
                        }
                        else if (dt.Columns[i].ColumnName == "quotationno")
                        {
                            String idquotationNew = createCodePO(1, Convert.ToDateTime(dr["dateimport"].ToString()));
                            dr["quotationno"] = idquotationNew;
                        }
                        else
                        {
                            dr[i] = dt.Rows[0][i];
                        }
                    }

                    dr["quotationorginal"] = quotationOrginal;
                    dr["gopbg"] = gopBG;
                    dr["tachbg"] = tachBG;
                    dr["invoiceeps"] = "";
                    dr["dateimport"] = DateTime.Now;
                    dr["idstatusquotation"] = "ST000006";
                    dr["quotationno"] = createCodePO(1);
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }

            return idexportNew;
        }

        private String createCodePO(int num, DateTime dte)
        {
            String sign = "";
            sign = APCoreProcess.APCoreProcess.Read("select  sign from employees where idemp ='" + clsFunction.GetIDEMPByUser() + "'").Rows[0][0].ToString().Trim();
            String code = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1  quotationno from QUOTATION where quotationno like '%" + "-" + Convert.ToDateTime(dte).ToString("MMyy") + "/EPS-BG" + "' order by quotationno desc");
            int index = 0;
            String sIndex = "0";
            try
            {
                sIndex = dt.Rows[0][0].ToString().Substring(sign.Length, 3);
                index = Convert.ToInt32(sIndex);
            }
            catch
            {
            }
            code = sign + (index + num).ToString().PadLeft(3, '0') + "-" + DateTime.Now.ToString("MMyy") + "/EPS-BG";
            if (APCoreProcess.APCoreProcess.Read("select quotationno from quotation where quotationno='" + code + "'").Rows.Count > 0)
            {
                num = num + 1;
                createCodePO(num, dte);
            }
            return code;
        }

        private void copyQuotationDetail(String idexportOld, String idexportNew)
        {
            try
            {
                string sql = "";
                sql = " INSERT INTO QUOTATIONDETAIL SELECT idcommodity + '_' + '" + idexportNew + "' as iddetail, idcommodity, idunit, idwarehouse, quantity, price, amount, vat, amountvat, davat, discount, amountdiscount, costs, total, note, '" + idexportNew + "' as idexport, status, pending, amountsurcharge, surcharge, strquantity, give, isdiscount, reasondiscount, issurcharge, reasonsurcharge, reasongive, idpromotion,   _index, spec, timedelivery, partnumber, cogs, equipmentinfo, description, baohanh, idgrouptk FROM QUOTATIONDETAIL WHERE idexport='" + idexportOld + "' and status =1 ";
                APCoreProcess.APCoreProcess.ExcuteSQL(sql);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error " + ex.Message);
            }
        }

        private void copyQuotationDetailOld(String idexportOld, String idexportNew)
        {

            //delete quotationdetail where iddetail in (select iddetail from QUOTATIONDETAIL group by iddetail, _index having count(iddetail+'_'+ CONVERT(varchar(10), _index))>1)
            try
            {
                DataRow drOld;
                DataTable dtQuotationDetail = APCoreProcess.APCoreProcess.Read("select * from quotationdetail where idexport='" + idexportOld + "'");
                DataTable dtQuotationDetailNew = new DataTable();
                dtQuotationDetailNew = APCoreProcess.APCoreProcess.Read("select * from quotationdetail where idexport='" + idexportNew + "'");
                for (int i = 0; i < dtQuotationDetail.Rows.Count; i++)
                {
                    DataRow drNew;
                    drOld = dtQuotationDetail.Rows[i];

                    String iddetail = drOld["idcommodity"].ToString() + "_" + idexportNew;

                    int index = findIndexRowExist(iddetail, dtQuotationDetailNew);
                    if (index != -1 && 1==2)// ko bao gio xay ra
                    {
                        drNew = dtQuotationDetailNew.Rows[index];
                        drNew["quantity"] = Convert.ToInt32(drOld["quantity"]) + Convert.ToInt32(drNew["quantity"]);
                        drNew["amount"] = Convert.ToInt32(drNew["quantity"]) * Convert.ToInt32(drNew["price"]);
                        drNew["amountvat"] = Convert.ToInt32(drNew["quantity"]) * Convert.ToInt32(drNew["price"]) * Convert.ToInt32(drNew["vat"]) / 100;
                        drNew["total"] = Convert.ToInt32(drNew["quantity"]) * Convert.ToInt32(drNew["price"]) * (100 + Convert.ToInt32(drNew["vat"])) / 100;
                    }
                    else
                    {
                        drNew = dtQuotationDetailNew.NewRow();
                    }
                    for (int j = 0; j < drOld.Table.Columns.Count; j++)
                    {
                        if (index == -1)
                        {
                            if (drOld.Table.Columns[j].ColumnName == "iddetail")
                            {
                                drNew["iddetail"] = iddetail;
                            }
                            else if (drOld.Table.Columns[j].ColumnName == "idexport")
                            {
                                drNew["idexport"] = idexportNew;
                            }
                            else
                            {
                                drNew[j] = drOld[j];
                            }
                        }
                    }
                    if (index == -1)
                    {
                        dtQuotationDetailNew.Rows.Add(drNew);
                    }
                    else
                    {
                        drNew.EndEdit();
                    }
                    APCoreProcess.APCoreProcess.Save(drNew);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
            }
        }

        private int findIndexRowExist(String iddetail, DataTable dt)
        {
            int index = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["iddetail"].ToString() == iddetail)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }

        private void bbi_allow_insert_csbg_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (APCoreProcess.APCoreProcess.Read("select idexport from quotation where idexport ='" + txt_idexport_IK1.Text + "'").Rows.Count == 0)
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Báo giá chưa được khởi tạo");
                    return;
                }
                menu.HidePopup();
                SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S();
                frm._insert = true;
                frm.passData = new SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S.PassData(getGridCSBG);
                frm._sign = "DT";
                frm.txt_macs_IK1.Text = clsFunction.layMa("CS", "macs", "CSBG");
                frm.idquotation = txt_idexport_IK1.Text;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi" + ex.Message);
            }
        }

        private void bbi_allow_delete_csbg_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete CSBG where macs = '" + gv_CSBG_C.GetRowCellValue(gv_CSBG_C.FocusedRowHandle, "macs").ToString() + "'");
                gv_CSBG_C.DeleteRow(gv_CSBG_C.FocusedRowHandle);
            }
        }

        private void bbi_allow_edit_csbg_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_CSBG_C.FocusedRowHandle < 0)
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn mẫu tin cần sửa ");
                    return;
                }
                menu.HidePopup();
                SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S();
                frm._insert = false;
                frm.txt_macs_IK1.Text = gv_CSBG_C.GetRowCellValue(gv_CSBG_C.FocusedRowHandle, "macs").ToString();
                frm.idquotation = txt_idexport_IK1.Text;
                frm.passData = new SOURCE_FORM_QUOTATION.Presentation.frm_CSBG_S.PassData(getGridCSBG);
                frm._sign = "DT";
                //frm.txt_commodity_S.Text = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcommodity");
                //frm.txt_idcommodity_I2.Text = gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idcommodity").ToString();
                //frm.txt_idquotationdetail_I2.Text = gv_EXPORTDETAIL_C.GetRowCellDisplayText(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString();
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }
    }
}