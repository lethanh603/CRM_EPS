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
namespace SOURCE_FORM_REPORT_RETAIL.Presentation
{
    public partial class frm_REPORTRETAIL_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_REPORTRETAIL_SH()
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
            if (APCoreProcess.APCoreProcess.Read("select * from sysReportDesigns where reportname='PrintSum'").Rows.Count == 0)
                APCoreProcess.APCoreProcess.ExcuteSQL("insert into sysReportDesigns (formname,reportname,[path],iscurrent,caption,query,template,[group],source,customSource) values('frm_checkbill','PrintSum','',0,'PrintSum','','',0,null,1) ");
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {      
                SaveGridControls();
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                //Load_Grid();
                //loadGrid();
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                dte_tungay_S.EditValue = DateTime.Now;
                dte_denngay_S.EditValue = DateTime.Now;
                loadGridLookupShift();
                rad_option_C.EditValue = 0;
            }
        }
                
        #endregion

        #region ButtonEvent

        private void bbi_allow_print_config_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadReport();
        }  

        
        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            DataTable dtRP = new DataTable();
            dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='"+ this.Name +"' and iscurrent=1");
            if (dtRP.Rows.Count > 0)
            {
                XtraReport report = XtraReport.FromFile(dtRP.Rows[0]["path"].ToString() + "\\" + dtRP.Rows[0]["reportname"].ToString()+".repx", true);
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
            menu.HidePopup();
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();
            frm.sDauma = _sign;
            frm.formNamePre = this.Name;
            frm.gridNamePre = gv_list_C.Name;
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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_list_C,clsFunction.transLateText( "BÁO CÁO BÁN HÀNG"), this);
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

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dtIn = new DataTable();
                if (rad_option_C.EditValue.ToString() == "0")
                {
                    DataTable dtTime = new DataTable();
                    dtTime = APCoreProcess.APCoreProcess.Read("select * from sysconfigBill");
                    DateTime fromdate;
                    DateTime todate;
                    if (dtTime.Rows.Count > 0)
                    {
                         fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, Convert.ToInt16(dtTime.Rows[0]["timestart"].ToString().Split(':')[0]), Convert.ToInt16(dtTime.Rows[0]["timestart"].ToString().Split(':')[1]), 0);
                         todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, Convert.ToInt16(dtTime.Rows[0]["timeend"].ToString().Split(':')[0]), Convert.ToInt16(dtTime.Rows[0]["timeend"].ToString().Split(':')[1]), 0);
                    }
                    else
                    {
                         fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, 9, 0, 0);
                         todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, 9, 0, 0);
                    }
                    //dt = APCoreProcess.APCoreProcess.Read("SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del having  CAST(datediff(d,0,datereceipt) as datetime)  between convert(datetime,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(dte_tungay_S.EditValue)) + "',103) and convert(datetime,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(dte_denngay_S.EditValue)) + "',103) and hd.del <> 1");
                    //dtIn = APCoreProcess.APCoreProcess.Read("SELECT     kh.customer, ex.invoice, nv.StaffName, ban.tableNO AS tableno, ex.dateimport,CONVERT(varchar(8),ex.dateimport,108) as hours, ex.isdelete, SUM(dt.quantity * dt.price) AS amount FROM   DMTABLE AS ban INNER JOIN  DMCUSTOMERS AS kh INNER JOIN  EXPORT AS ex ON kh.idcustomer = ex.idcustomer ON ban.idtable = ex.idtable INNER JOIN   EMPLOYEES AS nv ON ex.IDEMP = nv.IDEMP INNER JOIN   EXPORTDETAIL AS dt ON ex.idexport = dt.idexport GROUP BY kh.customer, ex.invoice, nv.StaffName, ban.tableNO, ex.dateimport, ex.isdelete, ex.complet HAVING      (ex.isdelete = 0) and ex.complet <>1 and   CAST(datediff(d,0,dateimport) as datetime)  between convert(datetime,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(dte_tungay_S.EditValue)) + "',103) and convert(datetime,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(dte_denngay_S.EditValue)) + "',103)");
                    dt = APCoreProcess.APCoreProcess.Read(" set dateformat DMY SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del having  convert(datetime,hd.datereceipt,103)  between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime,'" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and hd.del <> 1");
                    dtIn = APCoreProcess.APCoreProcess.Read("set dateformat DMY SELECT     kh.customer, ex.invoice, nv.StaffName, ban.tableNO AS tableno, ex.dateimport,CONVERT(varchar(8),ex.dateimport,108) as hours, ex.isdelete, SUM(dt.quantity * dt.price) AS amount FROM   DMTABLE AS ban INNER JOIN  DMCUSTOMERS AS kh INNER JOIN  EXPORT AS ex ON kh.idcustomer = ex.idcustomer ON ban.idtable = ex.idtable INNER JOIN   EMPLOYEES AS nv ON ex.IDEMP = nv.IDEMP INNER JOIN   EXPORTDETAIL AS dt ON ex.idexport = dt.idexport GROUP BY kh.customer, ex.invoice, nv.StaffName, ban.tableNO, ex.dateimport, ex.isdelete, ex.complet HAVING      (ex.isdelete = 0) and ex.complet <>1 and   convert(datetime,dateimport,103)   between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime,'" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103)");
                }
                else
                {
                    if (rad_option_C.EditValue.ToString() == "1")
                    {
                        DateTime fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, Convert.ToDateTime(txt_fromhour_S.EditValue).Hour, Convert.ToDateTime(txt_fromhour_S.EditValue).Minute, 0);
                        DateTime todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, Convert.ToDateTime(txt_tohour_S.EditValue).Hour, Convert.ToDateTime(txt_tohour_S.EditValue).Minute, 0);                    
                        dt = APCoreProcess.APCoreProcess.Read("set dateformat DMY SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del  having convert(datetime, datereceipt) between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime,'" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and hd.del <> 1");
                        dtIn = APCoreProcess.APCoreProcess.Read("set dateformat DMY SELECT     kh.customer, ex.invoice, nv.StaffName, ban.tableNO AS tableno, ex.dateimport, CONVERT(varchar(8),ex.dateimport,108) as hours,ex.isdelete, SUM(dt.quantity * dt.price) AS amount FROM   DMTABLE AS ban INNER JOIN  DMCUSTOMERS AS kh INNER JOIN  EXPORT AS ex ON kh.idcustomer = ex.idcustomer ON ban.idtable = ex.idtable INNER JOIN   EMPLOYEES AS nv ON ex.IDEMP = nv.IDEMP INNER JOIN   EXPORTDETAIL AS dt ON ex.idexport = dt.idexport GROUP BY kh.customer, ex.invoice, nv.StaffName, ban.tableNO, ex.dateimport, ex.isdelete, ex.complet HAVING      (ex.isdelete = 0) and ex.complet <>1 and   convert(datetime,dateimport,103)  between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime, '" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) ");
                    }
                    else
                    {
                        if (rad_option_C.EditValue.ToString() == "2")
                        {
                            DateTime fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, Convert.ToDateTime(txt_fromhour_S.EditValue).Hour, Convert.ToDateTime(txt_fromhour_S.EditValue).Minute, 0);
                            DateTime todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, Convert.ToDateTime(txt_tohour_S.EditValue).Hour, Convert.ToDateTime(txt_tohour_S.EditValue).Minute, 0);
                            dt = APCoreProcess.APCoreProcess.Read("set dateformat DMY SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del  having convert(datetime, datereceipt) between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime,'" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and hd.del <> 1");
                            dtIn = APCoreProcess.APCoreProcess.Read("set dateformat DMY SELECT     kh.customer, ex.invoice, nv.StaffName, ban.tableNO AS tableno, ex.dateimport, CONVERT(varchar(8),ex.dateimport,108) as hours,ex.isdelete, SUM(dt.quantity * dt.price) AS amount FROM   DMTABLE AS ban INNER JOIN  DMCUSTOMERS AS kh INNER JOIN  EXPORT AS ex ON kh.idcustomer = ex.idcustomer ON ban.idtable = ex.idtable INNER JOIN   EMPLOYEES AS nv ON ex.IDEMP = nv.IDEMP INNER JOIN   EXPORTDETAIL AS dt ON ex.idexport = dt.idexport GROUP BY kh.customer, ex.invoice, nv.StaffName, ban.tableNO, ex.dateimport, ex.isdelete, ex.complet HAVING      (ex.isdelete = 0) and ex.complet <>1 and   convert(datetime,dateimport,103)  between convert(datetime,'" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) and convert(datetime, '" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "',103) ");
                        }
                    }
                }
                gct_list_C.DataSource = dt;
                // load những hóa đơn chưa thanh toán  
                gct_Invoice_C.DataSource = dtIn;
            }
            catch { }
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_in_S_Click(object sender, EventArgs e)
        {
            print();
        }

        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcommodity").ToString();
            frm.idform = this.Name;
            frm.userid = clsFunction._iduser;
            frm.ShowDialog();
        }
        
        private void rad_option_C_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rad_option_C.EditValue.ToString() == "0")
                {
                    gc_hour_C.Enabled = false;
                    gc_shift_C.Enabled = false;
                }
                else
                {
                    if (rad_option_C.EditValue.ToString() == "1")
                    {
                        glue_idshift_S_EditValueChanged(sender, e);
                        gc_hour_C.Enabled = false;
                        gc_shift_C.Enabled = true;
                    }
                    else
                    {
                        gc_hour_C.Enabled = true;
                        gc_shift_C.Enabled = false;
                    }
                }
            }
            catch { }
        }

        private void glue_idshift_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from dmshift where idshift='"+ glue_idshift_S.EditValue.ToString() +"' and status=1");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["isNight"]) == false)
                    {
                        txt_fromhour_S.Text = dt.Rows[0]["fromhour"].ToString();
                        txt_tohour_S.Text = dt.Rows[0]["tohour"].ToString();
                    }
                    else
                    {
                        dte_denngay_S.EditValue = Convert.ToDateTime(dte_denngay_S.EditValue).AddDays(1);
                        txt_fromhour_S.EditValue = dt.Rows[0]["fromhour"].ToString();
                        txt_tohour_S.EditValue = dt.Rows[0]["tohour"].ToString();
                    }
                }

            }
            catch { }
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
            string[] caption_col = new string[] { "Số PT", "Số HĐ", "Ngày", "Giờ", "Nhân viên", "Khách hàng","Bàn", "Số tiền ","Phụ phí", "Chiết khấu", "Tổng tiền"};
         
            // FieldName column
            string[] fieldname_col = new string[] { "idreceipt", "invoice", "dateimport","hour", "idgroup", "idkind", "idwarehouse", "cogs", "price", "quantity", "mininventory", "note", "status" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CalcEditColumn", "CalcEditColumn", "CheckColumn", "CalcEditColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "80", "150", "150", "150", "100", "100", "100", "100","200","100" };
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
            string[] sql_glue = new string[] { "select idunit , unit from dmunit where status=1", "select idgroup , groupname from dmgroup where status=1", "select idkind , kind from dmkind where status=1", "select idwarehouse , warehouse from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"unit","groupname","kind","warehouse"};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idunit","idgroup","idkind","idwarehouse"};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[4, 2] { { "Mã ĐVT", "ĐVT" }, { "Mã nhóm", "Nhóm hàng" }, { "Mã Loại", "Loại hàng" }, { "Mã kho", "Kho hàng" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[4, 2] { { "idunit", "unit" }, { "idgroup", "groupname" }, { "idkind", "kind" }, { "idwarehouse", "warehouse" } };
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
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid,gv_list_C.Name);
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
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

        }

        private void gv_list_C_RowClick(object sender, RowClickEventArgs e)
        {

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
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='"+ strColVisible +"' where form_name='"+this.Name+"'");
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

        private void loadGridLookupShift()
        {
            try
            {
                string[] caption = new string[] { "Mã ca", "Ký hiệu", "Ca", "Từ giờ", "Đến giờ" };
                string[] fieldname = new string[] { "idshift", "sign", "shiftname", "fromhour", "tohour" };
                string[] col_visible = new string[] { "false", "True", "true", "true", "true" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idshift_S, "select idshift, sign, shiftname, fromhour, tohour from dmshift where status=1", "shiftname", "idshift", caption, fieldname, this.Name, glue_idshift_S.Width * 2, col_visible);

            }
            catch { }
        }

        private void getValueUpdate(bool value)
        {
            if (value == true)
            {
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idunit"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idunit,unit from dmunit where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idgroup"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup,groupname from dmgroup where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idkind"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idkind,kind from dmkind where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idwarehouse"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idwarehouse,warehouse from dmwarehouse where status=1");
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
                dts = APCoreProcess.APCoreProcess.Read("select * from dmcommodity",this.Name+gv_list_C.Name,new string[0,0],"");                
            }
            gct_list_C.DataSource = dts;
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

        private void print()
        {
            try
            {
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='frm_checkbill' and reportname='PrintSum'");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath + "\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    Function.clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataTable dtTime = new DataTable();
                    dtTime = APCoreProcess.APCoreProcess.Read("select * from sysconfigBill");
                    DateTime fromdate,todate;
                    if (dtTime.Rows.Count > 0)
                    {
                        fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, Convert.ToInt16(dtTime.Rows[0]["timestart"].ToString().Split(':')[0]), Convert.ToInt16(dtTime.Rows[0]["timestart"].ToString().Split(':')[1]), 0);
                        todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, Convert.ToInt16(dtTime.Rows[0]["timeend"].ToString().Split(':')[0]), Convert.ToInt16(dtTime.Rows[0]["timeend"].ToString().Split(':')[1]), 0);
                    }
                    else
                    {
                        fromdate = new DateTime(Convert.ToDateTime(dte_tungay_S.EditValue).Year, Convert.ToDateTime(dte_tungay_S.EditValue).Month, Convert.ToDateTime(dte_tungay_S.EditValue).Day, 9, 0, 0);
                        todate = new DateTime(Convert.ToDateTime(dte_denngay_S.EditValue).Year, Convert.ToDateTime(dte_denngay_S.EditValue).Month, Convert.ToDateTime(dte_denngay_S.EditValue).Day, 9, 0, 0);
                    }
                    dt = APCoreProcess.APCoreProcess.Read("declare @fromdate as varchar(20) set @fromdate= '" + fromdate.ToString("dd/MM/yyyy HH:mm:ss") + "' declare @todate as varchar(20) set @todate='" + todate.ToString("dd/MM/yyyy HH:mm:ss") + "' set dateformat DMY  SELECT     mh.idcommodity, mh.commodity  as commodity , DMUNIT.sign,  SUM(dt.quantity) AS quantity, fnPadRight(convert(varchar(3), SUM(dt.quantity)),' ',4) + ''+ DMUNIT.Sign AS  strquantity, dt.price,   (SELECT     SUM(discount) AS discount FROM         RECEIPT AS pt WHERE     (pt.datereceipt BETWEEN CONVERT(datetime, @fromdate) AND CONVERT(datetime, @todate)) and del<>1 ) as amountdiscout, 0 as amountvat,   (SELECT     SUM(surcharge) AS discount FROM         RECEIPT AS pt WHERE     (pt.datereceipt BETWEEN CONVERT(datetime, @fromdate) AND CONVERT(datetime, @todate)) and del<>1 ) as amountsurchage, '" + Convert.ToDateTime(dte_tungay_S.EditValue).ToString("dd/MM/yyyy") + "' as ngay,   (SELECT     count(pt.idreceipt) AS discount FROM         RECEIPT AS pt WHERE     (pt.datereceipt BETWEEN CONVERT(datetime, @fromdate) AND CONVERT(datetime, @todate)) and del<>1 ) as tonghd FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN    RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN  DMUNIT  ON mh.idunit = DMUNIT.idunit WHERE     (pt.datereceipt BETWEEN CONVERT(datetime, @fromdate) AND CONVERT(datetime, @todate)) and del<>1  GROUP BY mh.idcommodity, mh.commodity, DMUNIT.sign, dt.price");
                    if (dt.Rows.Count == 0)
                    {
                        clsFunction.MessageInfo("Thông báo","Chưa có dữ liệu thống kê");
                        return;
                    }
                    //dt = APCoreProcess.APCoreProcess.Read("set dateformat DMY  SELECT     mh.idcommodity, mh.commodity, DMUNIT.sign, SUM(dt.quantity) AS quantity, dt.price FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN   RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN  DMUNIT ON mh.idunit = DMUNIT.idunit WHERE     (pt.datereceipt BETWEEN CONVERT(datetime, '" + fromdate.ToString() + "') AND CONVERT(datetime, '" + todate.ToString() + "')) GROUP BY mh.idcommodity, mh.commodity, DMUNIT.sign, dt.price");
                    //dt.Columns.Add("amountsurchage", Type.GetType("System.Double"));
                    //dt.Columns.Add("amountdiscout", Type.GetType("System.Double"));
                    //dt.Columns.Add("amountvat", Type.GetType("System.Double"));
                    //dt.Columns.Add("ngay", Type.GetType("System.String"));
                    //dt.Columns.Add("tongHD", Type.GetType("System.Double"));
                    DataTable dtc = new DataTable();
                    dtc = dt.Copy();
                    ds.Tables.Add(dtc);
                    report.DataSource = ds;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }
                    //tool.ShowPreviewDialog();

                    //tool.Print();
                    tool.Print(APCoreProcess.APCoreProcess.Read("select * from sysPrintConfig where status=1").Rows[0]["printname"].ToString());
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
                    
        #endregion                   



                
    }
}