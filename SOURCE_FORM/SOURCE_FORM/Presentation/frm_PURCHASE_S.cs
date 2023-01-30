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
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
// Purchase trang thai xoa status =1
// export trang thai xoa isdelete=1
namespace SOURCE_FORM_PURCHASE.Presentation
{
    public partial class frm_PURCHASE_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_PURCHASE_S()
        {
            InitializeComponent();
        }        

        
    #endregion
        
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
        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
            try
            {
                clsFunction.AddKeyDownEvent(this);
                Function.clsFunction._keylience = true;
                if (statusForm == true)
                {
                    SaveGridControls();
                    clsFunction.Save_sysControl(this, this);
                    clsFunction.CreateTable(this);

                }
                else
                {
                    Function.clsFunction.TranslateForm(this, this.Name);
                    Load_Grid();
                    loadGrid();
                    Function.clsFunction.TranslateGridColumn(gv_PURCHASEDETAIL_C);
                    Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
                    loadGridLookupProvider();
                    loadGridLookupEmployee();
                    loadGridLookupMethod();
                    Init();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
                if (e.KeyCode == Keys.Delete)
                {
                    lbl_delete_S_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_exit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
            passData(true);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='" + this.Name + "' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(dtRP.Rows[0]["path"].ToString() + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("SELECT     mth.MeThodName, mt.dateimport, mt.limitdept, mt.note, mt.idpurchase, mt.status, dt.idcommodity, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat,  dt.discount, dt.amountdiscount, dt.costs, dt.total, com.commodity, e.StaffName, pro.provider, pro.address AS addressPro, pro.note AS notepro, pro.tel AS telpro,  dbo.DMUNIT.unit FROM         dbo.PURCHASE AS mt INNER JOIN  dbo.PURCHASEDETAIL AS dt ON mt.idpurchase = dt.idpurchase INNER JOIN   dbo.DMMETHOD AS mth ON mt.IDMETHOD = mth.IDMETHOD INNER JOIN    dbo.DMCOMMODITY AS com ON dt.idcommodity = com.idcommodity INNER JOIN  dbo.EMPLOYEES AS e ON mt.IDEMP = e.IDEMP INNER JOIN   dbo.DMPROVIDER AS pro ON mt.idprovider = pro.idprovider INNER JOIN   dbo.DMUNIT ON dt.idunit = dbo.DMUNIT.idunit WHERE     (mt.idpurchase = N'" + txt_idpurchase_IK1.Text + "')");
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
                        clsFunction.MessageInfo("Thông báo","Không tìm thấy dữ liệu");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_allow_input_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
            menu.HidePopup();
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();
            frm.sDauma = _sign;
            frm.formNamePre = this.Name;
            frm.gridNamePre = gv_PURCHASEDETAIL_C.Name;
            frm.arrColumnCaption = arrCaption.Split('/');
            frm.arrColumnFieldName = arrFieldName.Split('/');
            frm.tbName = clsFunction.getNameControls(this.Name);
            frm.ShowDialog();
            loadGrid();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_PURCHASEDETAIL_C,clsFunction.transLateText( "Danh mục bàn"), this);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_exit_allow_access_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
            menu.HidePopup();
            this.Close();
            Function.clsFunction.sotap--;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void bbi_allow_insert_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
            Init();
            clockButton(false);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void bbi_allow_insert_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {
                    saveMaster();
                    saveDetai();
                    clockButton(true);
                    Init();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_allow_insert_save_exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {
                    saveMaster();
                    saveDetai();
                    passData(true);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void bbi_allow_print_Search_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmSearchPurchar frm = new frmSearchPurchar();
                frm.Dock = DockStyle.Fill;
                frm.strpassData = new frmSearchPurchar.StrPassData(loadPurchase);
                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
              
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void btn_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (APCoreProcess.APCoreProcess.Read("select mt.idpurchase from STOCKIO stk inner join purchasedetail dt on dt.iddetail=stk.iddetailpur inner join purchase mt on mt.idpurchase=dt.idpurchase where mt.idpurchase='" + txt_idpurchase_IK1.Text + "'").Rows.Count == 0)
                if(Convert.ToDateTime( dte_dateimport_I5.EditValue) >= new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(-1))
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa phiếu nhập này không?"))
                    {
                        // XOA STATUS =TRUE
                        APCoreProcess.APCoreProcess.ExcuteSQL("UPDATE PURCHASE SET STATUS=1 WHERE IDPURCHASE='"+ txt_idpurchase_IK1.Text +"'");
                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from purchasedetail where idpurchase='" + txt_idpurchase_IK1.Text + "'");
                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from purchase where idpurchase='" + txt_idpurchase_IK1.Text + "'");
                        DataSet ds = new DataSet();
                        ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpurchase_IK1.Name) + " = '" + txt_idpurchase_IK1.Text + "'"));
                        Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_allow_delete_S.Caption), txt_idpurchase_IK1.Text, txt_idpurchase_IK1.Text, SystemInformation.ComputerName.ToString(), "2", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                        Init();
                    }
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Phiếu nhập này đã được kết chuyển, không thể xóa");
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        
        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
                frm._object = gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "idtable").ToString();
                frm.idform = this.Name;
                frm.userid = clsFunction._iduser;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void glue_idprovider_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callForm();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idprovider_I1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                loadInforProvider(glue_idprovider_I1.Properties.View.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void com_vat_I4_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
            if (!(char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
                        dte_limitdept_I5.EditValue = Convert.ToDateTime(dte_dateimport_I5.EditValue).AddDays(Convert.ToInt32( cal_limit_I4.Value));
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
                    cal_limit_I4.Value =Convert.ToDecimal(Convert.ToDateTime(dte_limitdept_I5.EditValue).Subtract(Convert.ToDateTime( dte_dateimport_I5.EditValue)).Days);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void lbl_delete_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_PURCHASEDETAIL_C.FocusedRowHandle >= 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select iddetailpur from STOCKIO where iddetailpur='" + gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "'").Rows.Count == 0)
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không?"))
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("delete purchasedetail where iddetail='" + gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "'");
                            gv_PURCHASEDETAIL_C.DeleteRow(gv_PURCHASEDETAIL_C.FocusedRowHandle);
                            if (gv_PURCHASEDETAIL_C.DataRowCount == 0)
                            {
                                APCoreProcess.APCoreProcess.ExcuteSQL("delete purchase where idpurchase='" + txt_idpurchase_IK1.Text + "'");
                            }
                        }
                    }
                    else
                    {
                        clsFunction.MessageInfo("Thông báo", "Mã hàng này đã được kết chuyển, không thể xóa");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            try
            {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { "quantity",  "mount",  "amountvat", "amountdiscount", "costs", "total" };
            // Caption column
            string[] caption_col = new string[] {"ID", "Mã hàng", "Ký hiệu", "Tên hàng", "ĐVT", "Kho hàng", "Số lượng", "Đơn giá","Thành tiền", "VAT", "Tiền VAT", "CK Sau VAT", "CK", "Tiền CK","Chi phí","Tổng tiền", "Ghi chú","ID" };
         
            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddetail_IK1", "col_idcommodity_I1", "col_sign_20_S", "col_commodity_S", "col_idunit_I1", "col_idwarehouse_I1", "col_quantity_I4", "col_price_I4", "col_amount_I15", "col_vat_I4", "col_amountvat_I15", "col_davat_I6", "col_discount_I8", "col_amountdiscount_I15", "col_costs_I15", "col_total_I15", "col_note_I3", "col_idpurchase_I1" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CheckColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "MemoColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100","100", "100", "200", "60", "200", "100", "100", "100", "60", "100", "100", "60", "100", "100", "100", "200","100" };
            //AllowFocus
            string[] AllowFocus = new string[] {"False", "True", "False", "False", "True", "True", "True", "True", "False", "True", "False", "True", "True", "False", "True", "False","True","False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "False" };
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
            string[] sql_glue = new string[] { "select idcommodity, commodity  from dmcommodity where status=1 and quantity=1", "select idunit, unit  from dmunit where status=1", "select idwarehouse, warehouse  from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"commodity","unit","warehouse"};
            
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcommodity", "idunit", "idwarehouse" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "Mã hàng", "Tên hàng" }, { "Mã ĐV", "ĐVT" }, { "Mã kho", "Kho" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcommodity", "commodity" },  { "idunit", "unit" }, { "idwarehouse", "warehouse" } };
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
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid,gv_PURCHASEDETAIL_C.Name);
            clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

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
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='"+ gv_PURCHASEDETAIL_C.Name +"'");
            
            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_PURCHASEDETAIL_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["column_name"].ToString().Split('/'), dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue" ].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col" ].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()),gluenulltext);
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_PURCHASEDETAIL_C.OptionsBehavior.Editable = true;
                gv_PURCHASEDETAIL_C.OptionsView.ColumnAutoWidth = false;
                gv_PURCHASEDETAIL_C.OptionsView.ShowAutoFilterRow = false;
            }

            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
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
            catch ( Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
         
        private void gct_list_C_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_PURCHASEDETAIL_C.FocusedRowHandle>=0)
                    loadStatus(gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
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
                if (gv_PURCHASEDETAIL_C.FocusedRowHandle >= 0)
                    loadStatus(gv_PURCHASEDETAIL_C.GetRowCellValue(gv_PURCHASEDETAIL_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
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
                if (gv_PURCHASEDETAIL_C.FocusedRowHandle>=0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag==true )
                {
                    clsFunction.customPopupMenu(bar_menu_C,menu, gv_PURCHASEDETAIL_C, this); 
                    menu.Manager.ItemClick+=new ItemClickEventHandler(Manager_ItemPress);
                    menu.ShowPopup(MousePosition);         
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
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
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "'");
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
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private int findIndexInArray(string value, string[] arr)
        {
     
            int index = -1;
            try
            {
            for (int i = 0; i < arr.Length; i++)
                if (value == arr[i])
                {
                    index = i;
                    break;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

                return index;
        }

        private void gv_PURCHASEDETAIL_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "col_idcommodity_I1")
                {                                        
                    setRowByIDcommodity(e.RowHandle,e.Value.ToString());
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
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
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
                    if (checkExitMaHangInGrid(inSt, (DataTable)gct_list_C.DataSource))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");
                    }
                }
                else
                {
                    if (checkExitIDcommdityInStock(view.GetRowCellValue(e.RowHandle, inStockCol).ToString()))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Không được sửa mã hàng, vui lòng xóa đi nhập lại");

                    }
                }
                if (e.RowHandle >=0)// Nếu mặt hàng đã tồn tại trong tồn kho thì không được chọn lại mã hàng chỉ được xóa đi tạo lại
                {
                    if (checkExitMaHangInGrid(inSt, (DataTable)gct_list_C.DataSource))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");
                    }
                }
          
                // Giới hạn số lương
                
                if (view.GetRowCellValue(e.RowHandle, intSl).ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, intSl))<=0 )
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Số lượng không được nhỏ hơn 0");

                }
                // Giới hạn số lương
                if (checkChangeQuantity(gv_PURCHASEDETAIL_C.GetRowCellValue(e.RowHandle, "iddetail").ToString(), Convert.ToDouble(view.GetRowCellValue(e.RowHandle, intSl)))==false)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Số lượng không được nhỏ hơn 0");

                }
                // giới hạn vat
                if (view.GetRowCellValue(e.RowHandle, "vat").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) < 0 || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) >100)
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
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gv_PURCHASEDETAIL_C_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
                e.WindowCaption = "Error";
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }
        
        private void gv_PURCHASEDETAIL_C_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }
        
        #endregion

        #region Methods

        private bool checkExitIDcommdityInStock(string iddetailpur)
        {
            bool flag = false;
            try
            {
                if (APCoreProcess.APCoreProcess.Read("select iddetailpur from STOCKIO where iddetailpur='" + iddetailpur + "'").Rows.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return flag;

        }

        private bool checkChangeQuantity(string iddetail, double quantity)
        {
            bool flag = false;
            try
            {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select isnull(sum(quantity),0) as quantity from STOCKIO where iddetailpur='"+ iddetail +"'");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt.Rows[0][0]) <= quantity)
                    flag = true;
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return flag;
        }

        private void loadPurchase(string str)
        {
            try
            {
                if (str != "")
                {
                    DataTable dtM = new DataTable();
                    dtM = APCoreProcess.APCoreProcess.Read("select * from purchase where idpurchase='"+ str +"'");
                    if (dtM.Rows.Count > 0)
                    {
                        glue_IDEMP_I1.EditValue = null;
                        glue_idprovider_I1.EditValue = null;
                        glue_idprovider_I1.EditValue = dtM.Rows[0]["idprovider"].ToString();
                        loadInfoProvider();
                        glue_idprovider_I1.Properties.View.FocusedRowHandle = 1;
                        glue_IDEMP_I1.EditValue = dtM.Rows[0]["IDEMP"].ToString();
                        dte_dateimport_I5.EditValue = dtM.Rows[0]["dateimport"].ToString();
                        dte_limitdept_I5.EditValue = dtM.Rows[0]["limitdept"].ToString();
                        txt_idpurchase_IK1.Text = dtM.Rows[0]["idpurchase"].ToString();
                        txt_note_100_I2.Text = dtM.Rows[0]["note"].ToString();
                        cal_limit_I4.Value =Convert.ToInt32(dtM.Rows[0]["limit"]);
                        rad_isdept_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["isdept"]);
                        rad_outlet_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["outlet"]);
                        glue_method_I1.EditValue = dtM.Rows[0]["IDMETHOD"].ToString();
                        
                     }
                    DataTable dtD = new DataTable();
                    dtD = APCoreProcess.APCoreProcess.Read("SELECT     dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idpurchase, mh.sign, mh.commodity FROM         PURCHASEDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idpurchase='" + str + "'");
                    if (dtD.Rows.Count > 0)
                    {
                        gct_list_C.DataSource = dtD;
                    }

                    if (gv_PURCHASEDETAIL_C.FocusedRowHandle >= 0)
                        loadStatus(txt_idpurchase_IK1.Text);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void customPopupMenu()
        {
            try
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
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }  
        
        private void getValueUpdate(bool value)
        {
            try
            {
            if (value == true)
            {
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_PURCHASEDETAIL_C.Columns["idfloors"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfloors,floors from dmfloors where status=1");
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

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
                dts = APCoreProcess.APCoreProcess.Read("SELECT     dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idpurchase, mh.sign, mh.commodity FROM         PURCHASEDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idpurchase='" + txt_idpurchase_IK1.Text + "'", this.Name + gv_PURCHASEDETAIL_C.Name, new string[0, 0], "");
            }
            gct_list_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

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

        private void loadReport()
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = this.Name;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e) {
            try
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
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void loadGridLookupProvider()
        {
            try
            {
                string[] caption = new string[] { "Mã NCC", "Tên NCC","Tel","Fax","Địa chỉ" };
                string[] fieldname = new string[] { "idprovider", "provider","tel","fax","address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idprovider_I1, "select idprovider, provider, tel, fax, address from dmprovider where status=1", "provider", "idprovider", caption, fieldname, this.Name, glue_idprovider_I1.Width*2,col_visible);
                
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadInfoProvider()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from dmprovider where idprovider='"+ glue_idprovider_I1.EditValue.ToString() +"'");
                if (dt.Rows.Count > 0)
                {
                    txt_address_S.Text = dt.Rows[0]["address"].ToString();
                    txt_fax_S.Text = dt.Rows[0]["fax"].ToString();
                    txt_tel_S.Text = dt.Rows[0]["tel"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void callForm()
        {
            try
            {
                SOURCE_FORM_DMPROVIDERS.Presentation.frm_DMPROVIDER_S frm = new SOURCE_FORM_DMPROVIDERS.Presentation.frm_DMPROVIDER_S();
                frm.strpassData = new SOURCE_FORM_DMPROVIDERS.Presentation.frm_DMPROVIDER_S.strPassData(getStringValue);
                frm._insert = true;
                frm.call = true;
                frm._sign = "NCC";
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
         
        private void getStringValue(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idprovider_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idprovider,provider from dmprovider where status=1");
                    glue_idprovider_I1.EditValue = value;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadInforProvider(int index)
        {
            try
            {
                if (index >= 0)
                {
                    txt_address_S.Text = glue_idprovider_I1.Properties.View.GetRowCellValue(index, gcDiachi).ToString();
                    txt_fax_S.Text = glue_idprovider_I1.Properties.View.GetRowCellValue(index, gcFax).ToString();
                    txt_tel_S.Text = glue_idprovider_I1.Properties.View.GetRowCellValue(index, gcSodt).ToString();
                }
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
                string[] caption = new string[] { "Mã PT", "Phương thức" };
                string[] fieldname = new string[] { "idmethod", "MeThodName" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_method_I1, "select idmethod, MeThodName from dmmethod where status=1 and kind ='IN'", "MeThodName", "idmethod", caption, fieldname, this.Name, glue_method_I1.Width * 2, col_visible);

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void Init()
        {
            try
            {
                glue_idprovider_I1.EditValue = null;
                glue_IDEMP_I1.EditValue = null;
                dte_dateimport_I5.EditValue = DateTime.Now;
                dte_limitdept_I5.EditValue = DateTime.Now;
                rad_isdept_I6.EditValue = true;
                rad_outlet_I6.EditValue = true;
                cal_limit_I4.Value = 0;
            
                txt_idpurchase_IK1.Text = clsFunction.layMa("PN","idpurchase","purchase");
                while (gv_PURCHASEDETAIL_C.RowCount > 0)
                {
                    gv_PURCHASEDETAIL_C.DeleteRow(0);
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
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByIDcommodity(int index, string idcommodity)
        {
            try
            {
                // set default
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "sign", "");
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "commodity", "");
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "iddetail", "");
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "idunit", "");
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "idwarehouse","");
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "quantity", 1);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "price", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amount", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "vat", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountvat", 0);                
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "discount", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "costs", 0);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "davat", false);
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select idcommodity, sign, commodity, idunit, idwarehouse, price from dmcommodity where idcommodity='"+ idcommodity +"' ");
                if (dt.Rows.Count > 0)
                {
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "sign", dt.Rows[0]["sign"].ToString());
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "commodity", dt.Rows[0]["commodity"].ToString());
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "iddetail", dt.Rows[0]["idcommodity"].ToString() + "_" + txt_idpurchase_IK1.Text);
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "idunit", dt.Rows[0]["idunit"].ToString());
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "idwarehouse", dt.Rows[0]["idwarehouse"].ToString());
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "quantity", 1);
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "price", Convert.ToDouble(dt.Rows[0]["price"].ToString()));
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amount", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price")));
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "vat", 0);
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "davat", false);
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "discount", 0);
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "idpurchase", txt_idpurchase_IK1.Text);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price").ToString()!="")
                {
                    price = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs"));
                }
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amount", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * price);
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * price*(vat/100));
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * price * (discount / 100));
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
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
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity").ToString() != "")
                {
                    quantity = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity"));
                }      
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price").ToString() != "")
                {
                    price = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs"));
                }
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amount", quantity * Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price")));     
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * price * (vat / 100));
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "quantity")) * price * (discount / 100));
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByVat(int index)
        {
            try
            {
                double amount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount"));
                }
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(amount * Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"))/100));
                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount * (Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"))/100 +1));
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByDavat(int index)
        {
            try
            {
                double amount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double discount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount"));
                }
                double vat = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"));
                }
                bool davat = false;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat"));
                }
                if (davat == false)
                {
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * discount / 100);
                }
                else
                {
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", amount*(1+vat/100) * discount / 100);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByDiscount(int index)
        {
            try
            {
                bool davat = false;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat"));
                }
                double amount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double amountvat = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat").ToString() != "")
                {
                    amountvat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat"));
                }
                double amountdiscount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount").ToString() != "")
                {
                    amountdiscount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount"));
                }
                if (davat == false) // chiết khấu trước thuế
                {
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount")) / 100);
                    //gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                }
                else
                {
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * (Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat")) / 100+1)  * Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount")) / 100);
                    //gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByAmountDiscount(int index)
        {
            try
            {
                bool davat = false;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat").ToString() != "")
                {
                    davat = Convert.ToBoolean(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "davat"));
                }
                double amount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount").ToString() != "")
                {
                    amount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount"));
                }
                double amountvat = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat").ToString() != "")
                {
                    amountvat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat"));
                }
                double amountdiscount = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount").ToString() != "")
                {
                    amountdiscount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount"));
                }
            
                    gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                
           
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void setRowByCost(int index)
        {
            try
            {
                double total = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "total").ToString() != "")
                {
                    total = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "total"));
                }
                double costs = 0;
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs"));
                }
                double price = 0;
                double vat = 0;
                double discount = 0;

                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price").ToString() != "")
                {
                    price = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "price"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat").ToString() != "")
                {
                    vat = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "vat"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount").ToString() != "")
                {
                    discount = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "discount"));
                }
                if (gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs").ToString() != "")
                {
                    costs = Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "costs"));
                }

                gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_PURCHASEDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void saveMaster()
        {
            try
            {
            if (checkCallPurchase() == false)
            {
                clsFunction.Insert_data(this, this.Name);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update PURCHASE set  STATUS=0 , dateimport='" + dte_dateimport_I5.EditValue.ToString() + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "' where idpurchase='" + txt_idpurchase_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpurchase_IK1.Name) + " = '" + txt_idpurchase_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert.Caption), txt_idpurchase_IK1.Text, txt_idpurchase_IK1.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            }
            else
            {
                clsFunction.Edit_data(this, this.Name,"idpurchase",txt_idpurchase_IK1.Text);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update PURCHASE set dateimport='" + dte_dateimport_I5.EditValue.ToString() + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "' where idpurchase='" + txt_idpurchase_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpurchase_IK1.Name) + " = '" + txt_idpurchase_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert_save.Caption), txt_idpurchase_IK1.Text, txt_idpurchase_IK1.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void saveDetai()
        {
            try
            {
                string iddetailex  = "";
                DataTable dt = new DataTable();
                
                for (int i = 0; i < gv_PURCHASEDETAIL_C.DataRowCount; i++)
                {
                    
                    dt = APCoreProcess.APCoreProcess.Read("PURCHASEDETAIL where idpurchase='" + txt_idpurchase_IK1.Text + "' and idcommodity='"+ gv_PURCHASEDETAIL_C.GetRowCellValue(i, "idcommodity").ToString() +"'");
                    if (dt.Rows.Count==0)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < gv_PURCHASEDETAIL_C.Columns.Count; j++)
                        {
                            if (gv_PURCHASEDETAIL_C.Columns[j].Name.Contains("_S") == false)
                            {
                                dr[gv_PURCHASEDETAIL_C.Columns[j].FieldName] = gv_PURCHASEDETAIL_C.GetRowCellValue(i, gv_PURCHASEDETAIL_C.Columns[j].FieldName);
                            }
                        }
                        dr["status"] = false;
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    else
                    {
                        DataRow dr = dt.Rows[0];
                        for (int j = 0; j < gv_PURCHASEDETAIL_C.Columns.Count; j++)
                        {
                            if (gv_PURCHASEDETAIL_C.Columns[j].Name.Contains("_S") == false)
                            {
                                dr[gv_PURCHASEDETAIL_C.Columns[j].FieldName] = gv_PURCHASEDETAIL_C.GetRowCellValue(i, gv_PURCHASEDETAIL_C.Columns[j].FieldName);
                            }
                        }                      
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    iddetailex = getIddetailExByExitIdcommodityPending(gv_PURCHASEDETAIL_C.GetRowCellValue(i, "idcommodity").ToString());
                    //lấy số lượng xuất                     
                    if (iddetailex != "") // đồng bộ tồn kho
                    {
                        updateStockOut(gv_PURCHASEDETAIL_C.GetRowCellValue(i, "idcommodity").ToString(), getQuantityExByExitIdcommodityPending(gv_PURCHASEDETAIL_C.GetRowCellValue(i, "idcommodity").ToString()), iddetailex, gv_PURCHASEDETAIL_C.GetRowCellValue(i, "idunit").ToString());
                    }
                }             
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private string getIddetailExByExitIdcommodityPending(string idcommodity)
        {
            string iddetailex ="";
            try
            {
            DataTable dt = new DataTable();
            dt=APCoreProcess.APCoreProcess.Read("select iddetail from exportdetail where pending=1 and idcommodity='" + idcommodity + "'");
            if (dt.Rows.Count > 0)
                iddetailex = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return iddetailex;
        }

        private double getQuantityExByExitIdcommodityPending(string idcommodity)
        {
            double quantity = 0;
            try
            {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select quantity from exportdetail where pending=1 and idcommodity='" + idcommodity + "'");
            if (dt.Rows.Count > 0)
                quantity = Convert.ToDouble( dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return quantity;
        }

        private void updateStockOut(string idcommodity, double quantity, string iddetailex, string idunit)
        {
            try
            {
            // kiểm tra tồn kho
            DataTable dtI = new DataTable();
            dtI = APCoreProcess.APCoreProcess.Read("SELECT     dt.quantity - SUM(ISNULL(stk.quantity, 0)) AS quantity, dt.iddetail FROM         PURCHASEDETAIL AS dt INNER JOIN  PURCHASE AS mt ON dt.idpurchase = mt.idpurchase LEFT OUTER JOIN   STOCKIO AS stk ON dt.iddetail = stk.iddetailpur GROUP BY dt.idcommodity, dt.status, dt.iddetail, dt.quantity HAVING      (dt.status = 0) AND (dt.idcommodity = N'" + idcommodity + "') AND (dt.quantity - SUM(ISNULL(stk.quantity, 0)) > 0) ");
            double quantitytemp = 0;// số lượng còn lại tạm
            if (dtI.Rows.Count > 0)// tồn kho đủ
            {
                for (int i = 0; i < dtI.Rows.Count; i++)
                {
                    string iddetailpur = "";
                    iddetailpur = dtI.Rows[i]["iddetail"].ToString();
                    double quantityrest = 0;
                    DataTable dtQuanDT = new DataTable();
                    dtQuanDT = APCoreProcess.APCoreProcess.Read("select dt.quantity -sum(isnull(stk.quantity,0)) as quantity from purchasedetail dt left join STOCKIO stk on stk.iddetailpur=dt.iddetail group by dt.quantity,dt.iddetail  having dt.iddetail='" + iddetailpur + "'");
                    if (dtQuanDT.Rows.Count > 0)
                    {
                        quantityrest = Convert.ToDouble(dtQuanDT.Rows[0]["quantity"]);
                    }
                    if (quantityrest < quantity && dtI.Rows.Count == 1)
                    {
                        // cho xuất hàng nhưng không cập nhật vào tồn. cập nhật pending =true
                        // Khi nhập hàng sẽ phát sinh tự động nhập vào tồn
                        APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=1 where iddetail='" + iddetailex + "'");
                        break;
                    }
                    DataTable dtSTK = new DataTable();
                    dtSTK = APCoreProcess.APCoreProcess.Read("SELECT     iddetailpur, iddetailex, idunit, quantity, datein, dateup,quantityrest FROM  STOCKIO WHERE     (iddetailpur = N'" + iddetailpur + "') AND (iddetailex = N'" + iddetailex + "')");
                    DataRow dr;
                    if (dtSTK.Rows.Count == 0)
                    {
                        dr = dtSTK.NewRow();
                        dr["iddetailpur"] = iddetailpur;
                        dr["iddetailex"] = iddetailex;
                        dr["idunit"] = idunit;
                        dr["quantity"] = (quantity - quantitytemp) - quantityrest > 0 ? quantityrest : (quantity - quantitytemp);
                        dr["quantityrest"] = (quantity - quantitytemp) - quantityrest > 0 ? 0 : quantityrest - (quantity - quantitytemp);
                        dr["datein"] = DateTime.Now;
                        dtSTK.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                        APCoreProcess.APCoreProcess.ExcuteSQL("update STOCKIO set quantityrest=" + ((quantity - quantitytemp) - quantityrest > 0 ? 0 : quantityrest - (quantity - quantitytemp)) + " where iddetailpur='" + iddetailpur + "' and quantityrest <> 0 ");
                        // Cập nhât lại trạng thái chi tiết phiếu xuất (đã xuất âm )
                        APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=0 where iddetail='" + iddetailex + "'");
                    }
                    if (quantity - quantityrest <= 0)
                    {
                        break;
                    }
                    else
                    {
                        quantitytemp += quantityrest;
                    }
                }
            }
            else
            {
                // cho xuất hàng nhưng không cập nhật vào tồn. cập nhật pending =true
                // Khi nhập hàng sẽ phát sinh tự động nhập vào tồn
                APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=1 where iddetail='" + iddetailex + "'");
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private bool checkExitsDetail(string idcommodity)
        {
            bool flag = false;
            try
            {
            if (APCoreProcess.APCoreProcess.Read("select * from PURCHASEDETAIL where idpurchar='" + txt_idpurchase_IK1.Text + "' and idcommodity='"+ idcommodity +"'").Rows.Count > 0)
            {
                flag = true;
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return flag;
        }

        private bool checkInput()
        {
            bool flag = true;
            try
            {
                if (Convert.ToDateTime(dte_dateimport_I5.EditValue) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1))
                {
                    flag = false;
                    clsFunction.MessageInfo("Thông báo", "Phiếu nhập hàng này đã kết chuyển không thể chỉnh sửa");
                    dte_dateimport_I5.Focus();
                }
            if (glue_IDEMP_I1.EditValue == null)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo","Bạn chưa chọn nhân viên");
                glue_IDEMP_I1.Focus();
            }
            if (glue_idprovider_I1.EditValue == null)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa chọn nhà cung cấp");
                glue_idprovider_I1.Focus();
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return flag;
        }

        private void clockButton(bool clock)
        {
            //bbi_allow_insert.Enabled = clock;
            //bbi_allow_insert_save.Enabled = !clock;
            //bbi_allow_insert_save_exit.Enabled = !clock;        
            //bbi_exit_S.Enabled = clock;
            //bbi_allow_print_Search.Enabled = clock;
            //if (checkCallPurchase())
            //{
            //    bbi_allow_print_config_sub.Enabled = clock;
            //    bbi_allow_print.Enabled = clock;
            //}
            //else
            //{
            //    bbi_allow_print_config_sub.Enabled = !clock;
            //    bbi_allow_print.Enabled = !clock;
            //}
        }

        private bool checkCallPurchase()
        {
            bool flag = false;
            try
            {
                if (APCoreProcess.APCoreProcess.Read(" SELECT     idpurchase from purchase where idpurchase='" + txt_idpurchase_IK1.Text + "'").Rows.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            return flag;
        }

        private bool checkExitMaHangInGrid(string mahang, DataTable dt)
        {
            int a = 0;
            try
            {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                    break;
                }
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            if (a > 0)
                return true;
            return false;
        }

        private bool checkExitMaHangInGridSua(string mahang, DataTable dt)
        {
            int a = 0;
            try
            {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                }
            }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

            if (a > 1)
                return true;
            return false;
        }

        private void SyncInventory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion                   



    }
}