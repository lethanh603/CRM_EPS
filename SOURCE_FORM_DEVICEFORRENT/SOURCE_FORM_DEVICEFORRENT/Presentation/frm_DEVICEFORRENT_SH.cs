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
namespace SOURCE_FORM_DEVICEFORRENT.Presentation
{
    public partial class frm_DEVICEFORRENT_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_DEVICEFORRENT_SH()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "HH";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();

        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
            Function.clsFunction._keylience = true;
            //statusForm = true;
            if (statusForm == true)
            {
                SaveGridControlsPhatSinh();
                SaveGridControls();
                SaveGridControlsContact();
                SaveGridControlsHis();
                SaveGridControlsInfo();
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGrid();
                Load_Grid();
                Load_Grid_Contact();
                Load_Grid_His();
                Load_Grid_Info();
                Load_Grid_PhatSinh();
                
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
                ControlDev.FormatControls.setContainsFilter(gv_list_C);
                loadResponsitory();
                const int index = 2;
                object val = resStatusDeivce.Items[index];
                resStatusDeivce.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                bbi_statusdevice_S.EditValue = val;
            }
        }

        private void frm_DMAREA_SH_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                for (int i = 0; i < bar_menu_C.Items.Count; i++)
                {
                    if (e.KeyCode.ToString() == bar_menu_C.Items[i].ShortcutKeyDisplayString)
                    {
                        bar_menu_C.PerformClick((bar_menu_C.Items[i]));
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        
        #endregion

        #region ButtonEvent

        private void bbi_allow_insert_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICEFORRENT_S frm=new frm_DEVICEFORRENT_S();
                frm._insert = true;
                frm._sign = _sign;       
                frm.statusForm = statusForm;
                frm.passData = new frm_DEVICEFORRENT_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo( "Thông báo","Lỗi thực thi");
            }
        }

        private void bbi_allow_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICEFORRENT_S frm = new frm_DEVICEFORRENT_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, Function.clsFunction.getNameControls(frm.txt_idthuexe_IK1.Name)).ToString();
                frm.passData = new frm_DEVICEFORRENT_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo","Lỗi, vui lòng chọn dòng cần sửa");
            }
        }

        private void bbi_allow_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (checkAdmin())
            {
                string idthuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                if (Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idthuexe", this, gv_list_C.Columns["idthuexe"], bbi_allow_delete.Name, "DEVICERENTDETAIL", "idthuexe"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from CUSCONTACTRENT where idthuexe='" + idthuexe + "'");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from DEVICERENTDETAIL where idthuexe='" + idthuexe + "'");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from DEVICERENTINFO where idthuexe='" + idthuexe + "'");
                }
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa mẫu tin này");
            }
        }

        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            DataTable dtRP = new DataTable();
            dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='"+ this.Name +"' and iscurrent=1");
            if (dtRP.Rows.Count > 0)
            {
                XtraReport report = XtraReport.FromFile(Application.StartupPath +"\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString()+".repx", true);
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(dtRP.Rows[0]["query"].ToString());            
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

        private void bbi_allow_input_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                
                XtraReport report = null;

                report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frxDeviceForRentDetail.repx", true);
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT        dbo.DMCUSTOMERS.customer, dbo.DMCUSTOMERS.tel, dbo.DMCUSTOMERS.fax, dbo.DMCUSTOMERS.tax, dbo.DMCUSTOMERS.address,  dbo.DEVICEFORRENT.ngayketthuc, dbo.DEVICEFORRENT.ngaybatdau, dbo.DEVICEFORRENT.giatrihopdong, dbo.DEVICEFORRENT.sogiodinhmuc,  dbo.DEVICEFORRENT.phivuotdinhmuc, dbo.DEVICEFORRENT.phivanchuyen, dbo.DEVICEFORRENT.datcoc,  dbo.DEVICEFORRENT.diachidangky, dbo.DEVICEFORRENT.ghichu, dbo.DEVICEFORRENT.idquotation, dbo.DEVICEFORRENT.status, dbo.EMPLOYEES.StaffName,  dbo.DEVICERENTINFO.quantity, dbo.DEVICERENTINFO.serial, dbo.DEVICERENTINFO.devicename, dbo.DEVICERENTINFO.original,  dbo.DEVICERENTINFO.model AS modeldetail, dbo.DEVICERENTINFO.statusdevice, dbo.DMRENTDEVICE.commodity, dbo.DMRENTDEVICE.spec diachigiaodich , dbo.DEVICEFORRENT.idcustomer,  dbo.DEVICEFORRENT.idthuexe, dbo.DMRENTDEVICE.label, dbo.DMRENTDEVICE.sign, dbo.DMRENTDEVICE.model AS model, dbo.DMRENTDEVICE.spec, dbo.DMRENTDEVICE.equipmentinfo, dbo.EMPLOYEES.tel AS telnv FROM            dbo.DEVICEFORRENT INNER JOIN         dbo.DMCUSTOMERS ON dbo.DEVICEFORRENT.idcustomer = dbo.DMCUSTOMERS.idcustomer INNER JOIN   dbo.EMPLOYEES ON dbo.DEVICEFORRENT.idemp = dbo.EMPLOYEES.IDEMP INNER JOIN     dbo.DMRENTDEVICE ON dbo.DEVICEFORRENT.idcommodity = dbo.DMRENTDEVICE.idcommodity INNER JOIN    dbo.DEVICERENTINFO ON dbo.DEVICEFORRENT.idthuexe = dbo.DEVICERENTINFO.idthuexe WHERE        (dbo.DEVICEFORRENT.idthuexe = N'" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "') AND DEVICERENTINFO.Status =0");
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
                    clsFunction.MessageInfo("Thông báo", "Không tìm thấy dữ liệu, vui lòng kiểm tra lại.");
                }
            }
            catch { }
        }
        
        private void bbi_allow_export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICERENTDETAIL_S frm = new frm_DEVICERENTDETAIL_S();
                frm._insert = true;
                frm._sign = "HIS";
                frm.statusForm = statusForm;
                frm.idchothuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                frm.passData = new frm_DEVICERENTDETAIL_S.PassData(getValueRentHis);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_exit_allow_access_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void bbi_refresh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_DMAREA_SH_Load(sender, e);
        }

        private void bbi_allow_insert_his_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICERENTDETAIL_S frm = new frm_DEVICERENTDETAIL_S();
                frm._insert = true;
                frm._sign = "HIS";
                frm.statusForm = statusForm;
                frm.idchothuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                frm.passData = new frm_DEVICERENTDETAIL_S.PassData(getValueRentHis);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_his_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (!checkAdmin())
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from devicerentdetail where idchothuexechitiet='" + gv_his_C.GetRowCellValue(gv_his_C.FocusedRowHandle, "idchothuexechitiet").ToString() + "'");
                gv_his_C.DeleteRow(gv_his_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_his_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
               

                frm_DEVICERENTDETAIL_S frm = new frm_DEVICERENTDETAIL_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_his_C.GetRowCellValue(gv_his_C.FocusedRowHandle, "idchothuexechitiet").ToString();
                frm.passData = new frm_DEVICERENTDETAIL_S.PassData(getValueRentHis);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_insert_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CUSCONTACTRENT_S frm = new frm_CUSCONTACTRENT_S();
                frm._insert = true;
                frm._sign = "CT";
                frm.statusForm = statusForm;
                frm.idThuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                frm.passData = new frm_CUSCONTACTRENT_S.PassData(getValueRentContact);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (!checkAdmin())
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSCONTACTRENT where idcontact='" + gv_contact_C.GetRowCellValue(gv_contact_C.FocusedRowHandle, "idcontact").ToString() + "'");
                gv_contact_C.DeleteRow(gv_contact_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                

                frm_CUSCONTACTRENT_S frm = new frm_CUSCONTACTRENT_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_contact_C.GetRowCellValue(gv_contact_C.FocusedRowHandle, "idcontact").ToString();
                frm.passData = new frm_CUSCONTACTRENT_S.PassData(getValueRentContact);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_insert_info_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICERENTINFO_S frm = new frm_DEVICERENTINFO_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                frm.idThuexe= gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                frm.passData = new frm_DEVICERENTINFO_S.PassData(getValueRentInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_info_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (!checkAdmin())
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from DEVICEINFO where iddevice='" + gv_info_C.GetRowCellValue(gv_info_C.FocusedRowHandle, "iddevice").ToString() + "'");
                gv_info_C.DeleteRow(gv_info_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_info_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                

                frm_DEVICERENTINFO_S frm = new frm_DEVICERENTINFO_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_info_C.GetRowCellValue(gv_info_C.FocusedRowHandle, "iddevice").ToString();
                frm.passData = new frm_DEVICERENTINFO_S.PassData(getValueRentInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_search_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadGrid();
        }

        private void bbi_allow_insert_phatsinh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_STARTRENTDEVICE_S frm = new frm_STARTRENTDEVICE_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                frm.idThuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
                frm.passData = new frm_STARTRENTDEVICE_S.PassData(getValueRentPhatSinh);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_phatsinh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (!checkAdmin())
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from STARTRENTDEVICE where id='" + gv_phatsinh_C.GetRowCellValue(gv_phatsinh_C.FocusedRowHandle, "id").ToString() + "'");
                gv_phatsinh_C.DeleteRow(gv_phatsinh_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_phatsinh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_STARTRENTDEVICE_S frm = new frm_STARTRENTDEVICE_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_phatsinh_C.GetRowCellValue(gv_phatsinh_C.FocusedRowHandle, "id").ToString();
                frm.passData = new frm_STARTRENTDEVICE_S.PassData(getValueRentPhatSinh);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_print_phatsinh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DateTime ngaybatdau = Convert.ToDateTime(gv_phatsinh_C.GetRowCellValue(gv_phatsinh_C.FocusedRowHandle,"ngaybatdau"));
                DateTime ngayketthuc = Convert.ToDateTime(gv_phatsinh_C.GetRowCellValue(gv_phatsinh_C.FocusedRowHandle, "ngayketthuc"));

                XtraReport report = null;

                report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frxDeviceForRentHis.repx", true);
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
               
                dt = APCoreProcess.APCoreProcess.Read("SELECT        dbo.DMCUSTOMERS.customer, dbo.DMCUSTOMERS.tel, dbo.DMCUSTOMERS.fax, dbo.DMCUSTOMERS.tax, dbo.DMCUSTOMERS.address, " 
                    +" dbo.DEVICEFORRENT.ngayketthuc, dbo.DEVICEFORRENT.ngaybatdau, dbo.DEVICEFORRENT.giatrihopdong, dbo.DEVICEFORRENT.sogiodinhmuc, "
                    +" dbo.DEVICEFORRENT.phivuotdinhmuc, dbo.DEVICEFORRENT.phivanchuyen, dbo.DEVICEFORRENT.datcoc, dbo.DEVICEFORRENT.diachigiaodich, "
                    + " dbo.DEVICEFORRENT.diachidangky, dbo.DEVICEFORRENT.ghichu, dbo.DEVICEFORRENT.idquotation, dbo.DEVICEFORRENT.status, dbo.EMPLOYEES.StaffName, "
                     + " dbo.DMRENTDEVICE.commodity, dbo.DEVICEFORRENT.idcustomer, dbo.DEVICEFORRENT.idthuexe, dbo.DMRENTDEVICE.label, dbo.DMRENTDEVICE.sign, "
                      + " dbo.DMRENTDEVICE.model AS modeldevice, dbo.DMRENTDEVICE.spec, dbo.DMRENTDEVICE.equipmentinfo, dbo.EMPLOYEES.tel AS telnv,  DEVICERENTDETAIL.idpo, "
	                  + " DEVICERENTDETAIL.sokhaosat, DEVICERENTDETAIL.ketquathuchien, DEVICERENTDETAIL.ngaythuchien,  DEVICERENTDETAIL.createdate, "
	                  + " DEVICERENTDETAIL.ngayvietmail, DEVICERENTDETAIL.ngayphanhoi, DEVICERENTDETAIL.note,  DEVICERENTDETAIL.idchothuexechitiet, "
	                  + " DEVICERENTDETAIL.sogiongunghoatdong, dbo.DMTINHTRANGXE.statusxe, derivedtbl_1.sogioketthuc,  derivedtbl_1.sogiobatdau, derivedtbl_1.ngaybatdau AS ngaydauthang,"
	                   + " derivedtbl_1.ngayketthuc AS ngaycuoithang  FROM            dbo.DEVICEFORRENT INNER JOIN  dbo.DMCUSTOMERS ON dbo.DEVICEFORRENT.idcustomer = dbo.DMCUSTOMERS.idcustomer "
	                   + " INNER JOIN  dbo.EMPLOYEES ON dbo.DEVICEFORRENT.idemp = dbo.EMPLOYEES.IDEMP INNER JOIN  dbo.DMRENTDEVICE ON dbo.DEVICEFORRENT.idcommodity = dbo.DMRENTDEVICE.idcommodity "
                        + " LEFT JOIN  (Select * from dbo.DEVICERENTDETAIL where  CAST( DEVICERENTDETAIL.createdate as date) BETWEEN   CAST('" + ngaybatdau.ToString("yyyy/MM/dd") + "' AS date) AND  CAST('" + ngayketthuc.ToString("yyyy/MM/dd") + "' AS date)) as DEVICERENTDETAIL ON dbo.DEVICEFORRENT.idthuexe = DEVICERENTDETAIL.idthuexe "
	                   + " INNER JOIN  (SELECT        TOP (1) sogioketthuc, sogiobatdau, ngaybatdau, ngayketthuc, status, idthuexe, id, note, sign  FROM     "
                             + " dbo.STARTRENTDEVICE  WHERE        (idthuexe = '" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "') AND (CAST(ngaybatdau AS date) = CAST('" + ngaybatdau.ToString("yyyy/MM/dd") + "' AS date)) "
                              + " AND (CAST(ngayketthuc AS date) = CAST('" + ngayketthuc.ToString("yyyy/MM/dd") + "' AS date)))   AS derivedtbl_1 ON dbo.DEVICEFORRENT.idthuexe = derivedtbl_1.idthuexe LEFT OUTER JOIN  "
                               + " dbo.DMTINHTRANGXE ON DEVICERENTDETAIL.idstatusxe = dbo.DMTINHTRANGXE.idstatusxe  WHERE        (dbo.DEVICEFORRENT.idthuexe = N'" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "') ");
                
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
                    clsFunction.MessageInfo("Thông báo", "Không tìm thấy dữ liệu, vui lòng kiểm tra lại.");
                }
            }
            catch { }
        }

        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString();
            frm.idform = this.Name;
            frm.userid = clsFunction._iduser;
            frm.ShowDialog();
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] {"giatrihopdong","datcoc", "phivanchuyen" };
            // Caption column
            string[] caption_col = new string[] { "ID Thuê xe", "Số PO", "Khách hàng", "Xe", "Nhân viên phụ trách", "GT hợp đồng", "Đặt cọc", "Ngày bắt đầu", "Ngày kết thúc", "Số giờ ĐM", "Phí vượt định mức","Phí vận chuyển", "Địa chỉ đăng ký", "Địa chỉ giao dịch", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "idthuexe", "idquotation", "idcustomer", "idcommodity", "idemp", "giatrihopdong", "datcoc", "ngaybatdau", "ngayketthuc", "sogiodinhmuc", "phivuotdinhmuc", "phivanchuyen", "diachidangky", "diachigiaodich", "ghichu", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "SpinEditColumn", "SpinEditColumn", "DateColumn", "DateColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "MemoColumn", "MemoColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "250", "200", "200", "100", "100", "100", "100", "100", "100","100", "200", "200", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer , customer from dmcustomer where status=1", "select idcommodity , commodity from dmdivice where status=1", "select idemp , staffname from employees where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "commodity", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "idcommodity", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "ID", "Khách hàng" }, { "ID", "Xe" }, { "ID", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcustomer", "customer" }, { "idcommodity", "commodity" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_list_C.Name);
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;
            gv_list_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_list_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='"+ gv_list_C.Name +"'");
            
            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_list_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue" ].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col" ].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
            }
          
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        // his

        private void SaveGridControlsHis()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Tháng", "ID", "ID Thuê xe", "Ngày tạo", "Số khảo sát", "Số po", "Tình trạng hoạt động", "Ngày KH phản hồi", "Ngày KD viết mail", "Ngày KT thực hiện","Số giờ ngừng hoạt động", "Kết quả thực hiện", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "thang","idchothuexechitiet", "idthuexe", "createdate", "sokhaosat", "idpo", "idstatusxe", "ngayphanhoi", "ngayvietmail", "ngaythuchien","sogiongunghoatdong", "ketquathuchien", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "DateColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "DateColumn", "DateColumn", "DateColumn", "SpinEditColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "100", "100", "100", "100", "250", "100", "100", "100", "100", "200", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idstatusxe , statusxe from DMTINHTRANGXE where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "statusxe"};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idstatusxe"};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "ID", "Tình trạng" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "idstatusxe", "statusxe" }};
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_his_C.Name);
        }

        private void Load_Grid_His()
        {
            string text = Function.clsFunction.langgues;
            gv_his_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_his_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_his_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_his_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_his_C,
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // contact

        private void SaveGridControlsContact()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "ID Thuê xe", "Tên người liên hệ", "Chức vụ", "Email", "Điện thoại", "Status",  "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idcontact", "idthuexe", "contactname", "idposition", "email", "tel", "status", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "CheckColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "150", "150", "100", "100", "200"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False"};
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True"};
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
            string[] sql_glue = new string[] { "select idstatusactive , statusactive from dmstatusactive where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "statusactive" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idstatusactive" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "ID", "Tình trạng" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "idstatusactive", "statusactive" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_contact_C.Name);
        }

        private void Load_Grid_Contact()
        {
            string text = Function.clsFunction.langgues;
            gv_contact_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_contact_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_contact_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_contact_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_contact_C,
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // info

        private void SaveGridControlsInfo()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] {"ID", "Id thuê xe", "Tên thiết bị", "Model", "Serial","Số lượng", "Ngày cập nhật","Tình trạng", "Xuất xứ", "Ghi chú" ,"Status"};

            // FieldName column
            string[] fieldname_col = new string[] { "iddevice", "idthuexe", "devicename", "model", "serial", "quantity", "updatedat", "statusdevice", "original", "note", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "DateColumn", "TextColumn", "TextColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "250", "200", "200", "100", "100", "200", "200", "200", "100"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False"};
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True"};
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
            string[] sql_glue = new string[] { "select idcustomer , customer from dmcustomer where status=1", "select idcommodity , commodity from dmdivice where status=1", "select idemp , staffname from employees where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "commodity", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "idcommodity", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "ID", "Khách hàng" }, { "ID", "Xe" }, { "ID", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcustomer", "customer" }, { "idcommodity", "commodity" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_info_C.Name);
        }

        private void Load_Grid_Info()
        {
            string text = Function.clsFunction.langgues;
            gv_info_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_info_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_info_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_info_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_info_C,
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // phat ssinh
        private void SaveGridControlsPhatSinh()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Id thuê xe", "Ngày bắt đầu", "Giờ bắt đầu", "Giờ kết thúc", "Chênh lệch", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "id", "idthuexe", "ngaybatdau","ngayketthuc", "sogiobatdau", "sogioketthuc", "sogiochenhlech","note", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "DateColumn", "DateColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "100", "100", "100", "100", "100", "200", "100" };
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
            string[] sql_glue = new string[] { "select idcustomer , customer from dmcustomer where status=1", "select idcommodity , commodity from dmdivice where status=1", "select idemp , staffname from employees where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "commodity", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "idcommodity", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "ID", "Khách hàng" }, { "ID", "Xe" }, { "ID", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcustomer", "customer" }, { "idcommodity", "commodity" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_phatsinh_C.Name);
        }

        private void Load_Grid_PhatSinh()
        {
            string text = Function.clsFunction.langgues;
            gv_phatsinh_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_phatsinh_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_phatsinh_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_phatsinh_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_phatsinh_C,
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

        private void gct_list_C_DoubleClick(object sender, EventArgs e)
        {
            bbi_allow_edit.PerformClick();
        }
   
        private void gv_list_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_list_C.FocusedRowHandle >= 0)
                {
                    loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString());
                    loadGridRentContact();                   
                    loadGridRentInfo();
                    loadGridRentPhatSinh();
                    loadGridRentHist();
                    loadThueXeChitiet();
                }
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
                bool flag =false;
                if (gv_list_C.FocusedRowHandle>=0 || 1==1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag==true )
                {
                    clsFunction.customPopupMenu(bar_menu_C,menu, gv_list_C, this); 
                    menu.Manager.ItemClick+=new ItemClickEventHandler(Manager_ItemPress);
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
            int pos=-1;
            try
            {              
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_list_C.Name + "'");
                if (dt.Rows.Count > 0)
                {
                    strColName = dt.Rows[0][0].ToString();
                    strCol = dt.Rows[0][1].ToString();
                    string[] arrayColName = strColName.Split('/');
                    string[] arrCol = strCol.Split('/');
                    if (e.Item.Name.Contains("_allow_col_") && (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos=findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]))
                        {                     
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible= clsFunction.ConvertArrayToString(arrCol);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='"+ strColVisible +"' where form_name='"+this.Name+"' and grid_name ='"+ gv_list_C.Name +"'");
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

        private void gv_info_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_info_C.FocusedRowHandle >= 0 || 1 == 1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_info_C, menu, gv_info_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_Info);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_his_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_his_C.FocusedRowHandle >= 0 || 1==1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_history_C, menu, gv_his_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_His);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_contact_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_contact_C.FocusedRowHandle >= 0 || 1 == 1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_contact_C, menu, gv_contact_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_Contact);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        

        private void Manager_ItemPress_Info(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_info_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_info_C.Name + "'");
                            Load_Grid_Info();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void Manager_ItemPress_Contact(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_contact_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_contact_C.Name + "'");
                            Load_Grid_Contact();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void Manager_ItemPress_His(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_his_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_his_C.Name + "'");
                            Load_Grid_His();
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
          
        #endregion

        #region Methods

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
              
            }
        }

        private void loadResponsitory()
        {
            String dkManager = "";
            if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
            {
                dkManager = " or 1=1 ";
            }
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idcustomer"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 " + dkManager + ") AND E.status='True' ORDER BY C.idcustomer");
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idcommodity"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcommodity, C.commodity, C.sign FROM   dbo.DMRENTDEVICE AS C WHERE status =1");
        }

      private void loadGrid()
        {
            if (Function.clsFunction._pre == true)
            {
                dts = Function.clsFunction.dtTrace;
            }
            else
            {
                string sdk = "";
                if (bbi_fromdate_S.EditValue != null)
                {
                    sdk += " cast (ngaybatdau as date) >=cast('" + Convert.ToDateTime(bbi_fromdate_S.EditValue) + "' as date)";
                }
                if (bbi_todate_S.EditValue != null)
                {
                    if (sdk != "")
                    {
                        sdk += " AND cast (ngayketthuc as date) <=cast('" + Convert.ToDateTime(bbi_todate_S.EditValue) + "' as date)";
                    }
                    else
                    {
                        sdk += " cast (ngayketthuc as date) <=cast('" + Convert.ToDateTime(bbi_todate_S.EditValue) + " as date')";
                    }
                   
                }
                object val = bbi_statusdevice_S.EditValue;
                // dang cho thue
                if (resStatusDeivce.Items.IndexOf(val) == 0)
                {
                    if (sdk != "")
                    {
                        sdk += " AND  cast (ngayketthuc as date) <= cast (getDate() as date)";
                    }
                    else
                    {
                        sdk += "  cast (ngayketthuc as date) <=  cast (getDate() as date)";
                    }
                }
                    // het hạn hợp đồng
                else if (resStatusDeivce.Items.IndexOf(val) == 1)
                {
                    if (sdk != "")
                    {
                        sdk += " AND  cast (ngayketthuc as date) >= cast (getDate() as date)";
                    }
                    else
                    {
                        sdk += "  cast (ngayketthuc as date) >= cast (getDate() as date)";
                    }
                }
                    // tất cả
                else if (resStatusDeivce.Items.IndexOf(val) == 2)
                {
                    sdk += "";
                }
                if (sdk != "")
                {
                    sdk = " WHERE " + sdk;
                }
                dts = APCoreProcess.APCoreProcess.Read("select D.* from DEVICEFORRENT D INNER JOIN EMPLOYEES EM on D.IDEMP=EM.IDEMP  AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0   " + sdk);                
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
                    dt = Function.clsFunction.Excute_Proc("sysTraceByExpression", new string[5, 2] { { "userid", clsFunction._iduser }, { "idform", "%" + clsFunction.getNameControls(this.Name) + "%" }, { "object", ID }, { "fromdate", DateTime.Now.AddYears(-1).ToShortDateString() }, { "todate", DateTime.Now.ToShortDateString() } });
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read("	SELECT     sysTrace.ID, sysTrace.userid, sysTrace.date, sysTrace.idform, sysTrace.status, sysTrace.object, sysTrace.caption,   sysTrace.action, sysTrace.computer, sysTrace.tableName,sysTrace.datapre,sysTrace.namespace, EMPLOYEES.IDEMP, EMPLOYEES.StaffName	FROM         sysTrace INNER JOIN  sysUser ON sysTrace.userid = sysUser.userid INNER JOIN  EMPLOYEES ON sysUser.IDEMP = EMPLOYEES.IDEMP	  where (sysTrace.idform like '%" + clsFunction.getNameControls(this.Name) + "%')  and  sysTrace.object='" + ID + "' and CONVERT(datetime, sysTrace.date,103) between convert(datetime,'" + DateTime.Now.AddYears(-1).ToString("yyyyMMdd") + "',103) and convert(datetime,'" + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "')	  and sysTrace.userid  in (select us.userid from sysUser us inner join employees e on e.IDEMP=us.IDEMP inner join employees e1 on e1.idemp=e.idmanager where e.idmanager in (select idemp from sysUser u1 where userid='" + clsFunction._iduser + "') or e.idemp in (select idemp from sysUser u1 where userid='" + clsFunction._iduser + "'))");
                }
                if (dt.Rows.Count > 0)
                {
                    bbi_status_S.Caption = "";
                    status += clsFunction.transLateText("Người tạo: ") + dt.Rows[0]["StaffName"].ToString() +" - "+ clsFunction.transLateText(" Ngày tạo: ")+Convert.ToDateTime( dt.Rows[0]["date"]).ToString("dd/MM/yyyy HH:mm:ss");
                    if (dt.Rows.Count > 1)
                    {
                        status +=" - "+ clsFunction.transLateText("Người sửa: ") + dt.Rows[dt.Rows.Count-1]["StaffName"].ToString()+" - " + clsFunction.transLateText(" Ngày sửa: " )+ Convert.ToDateTime(dt.Rows[dt.Rows.Count-1]["date"]).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    bbi_status_S.Caption = status;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Function.clsFunction.transLateText("Thông báo"));
            }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e) {
              DataSet ds = new DataSet();
              DataTable dt = new DataTable();
              dt = APCoreProcess.APCoreProcess.Read(clsFunction.getNameControls(this.Name));
              ds.Tables.Add(dt);
            //Instantiating your DataSet instance here
            //.....
            //Pass the created DataSet to your report:
            ((XtraReport)sender).DataSource = ds;
            ((XtraReport)sender).DataMember = ds.Tables[0].TableName;     
        }

        private void getValueRentHis(bool value)
        {
            if (value == true)
            {
                loadGridRentHist();
            }
        }

        private void getValueRentContact(bool value)
        {
            if (value == true)
            {
                loadGridRentContact();
            }
        }

        private void getValueRentInfo(bool value)
        {
            if (value == true)
            {
                loadGridRentInfo();
            }
        }

        private void loadGridRentHist()
        {
            try
            {
                if (gv_list_C.FocusedRowHandle >=0)
                {
                    DataTable dt = APCoreProcess.APCoreProcess.Read("select D.*, convert(nvarchar,year(ngaythuchien))+'/'+convert(nvarchar,month(ngaythuchien)) as thang from devicerentdetail AS D INNER JOIN DEVICEFORRENT R ON R.idthuexe = D.idthuexe INNER JOIN STARTRENTDEVICE S ON S.idthuexe = R.idthuexe  where D.idthuexe='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "' AND D.createdate BETWEEN  DATEADD (day , -1 , S.ngaybatdau )  AND  DATEADD (day , 1 , S.ngayketthuc)  ");
                    gct_his_C.DataSource = dt;
                    gv_his_C.Columns["thang"].Group();
                    gv_his_C.CollapseAllGroups();
                }
            }
            catch (Exception ex) { };
        }

        private void loadGridRentContact()
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from CUSCONTACTRENT where idthuexe='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "'");
                gct_contact_C.DataSource = dt;
            }
        }

        private void loadGridRentInfo()
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from DEVICERENTINFO where idthuexe='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "'");
                gct_info_C.DataSource = dt;
                
            }
        }

        private void getValueRentPhatSinh(bool value)
        {
            if (value == true)
            {
                loadGridRentPhatSinh();
            }
        }

        private void loadGridRentPhatSinh()
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select D.*, CASE WHEN (sogioketthuc - sogiobatdau) <0 THEN 0 Else (sogioketthuc - sogiobatdau) END as sogiochenhlech from STARTRENTDEVICE as D where idthuexe='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "'");
                gct_phatsinh_C.DataSource = dt;
                
            }
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

        private void loadThueXeChitiet()
        {
            try
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select C.customer, C.surrogate, C.tel, C.fax, C.tax, C.email, DR.diachidangky, DR.diachigiaodich, C.surrogate, T.customertype, E.staffname, D.sign, D.label, D.commodity, D.model, D.spec from deviceforrent DR left join DMRENTDEVICE  D ON DR.idcommodity = D.idcommodity left join DEVICERENTINFO I on I.idthuexe = DR.idthuexe inner join  DMCUSTOMERS C on C.idcustomer = DR.idcustomer inner join employees E on E.idemp = DR.idemp inner join DMCUSTOMERTYPE T on C.idtype = T.idtype where DR.idthuexe = '" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idthuexe").ToString() + "' ");
                if (dt.Rows.Count > 0)
                {
                    txt_address_S.Text = dt.Rows[0]["diachigiaodich"].ToString();
                    txt_station_S.Text = dt.Rows[0]["diachidangky"].ToString();
                    txt_model_S.Text = dt.Rows[0]["model"].ToString();
                    txt_customertype_S.Text = dt.Rows[0]["customertype"].ToString();
                    txt_commodity_S.Text = dt.Rows[0]["commodity"].ToString();
                    txt_customer_S.Text = dt.Rows[0]["customer"].ToString();
                    txt_email_S.Text = dt.Rows[0]["email"].ToString();
                    txt_tel_S.Text = dt.Rows[0]["tel"].ToString();
                    txt_tax_S.Text = dt.Rows[0]["tax"].ToString();
                    txt_fax_S.Text = dt.Rows[0]["fax"].ToString();
                    txt_label_S.Text = dt.Rows[0]["label"].ToString();
                    txt_sign_S.Text = dt.Rows[0]["sign"].ToString();
                    txt_spec_S.Text = dt.Rows[0]["spec"].ToString();
                    txt_staffname_S.Text = dt.Rows[0]["staffname"].ToString();
                    txt_surrogate_S.Text = dt.Rows[0]["surrogate"].ToString();      
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

       
            
        #endregion                   

        private void bar_info_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_info_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_info_C.Name + "'");
                            Load_Grid_Info();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void bar_contact_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_contact_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_contact_C.Name + "'");
                            Load_Grid_Contact();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void bar_history_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_his_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_his_C.Name + "'");
                            Load_Grid_His();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void gv_phatsinh_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_phatsinh_C.FocusedRowHandle >= 0 || 1 == 1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_phatsinh_C, menu, gv_phatsinh_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_His);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_phatsinh_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadGridRentHist();
        }

        
    }
}