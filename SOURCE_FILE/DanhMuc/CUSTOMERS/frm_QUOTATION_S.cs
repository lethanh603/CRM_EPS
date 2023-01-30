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

namespace SOURCE_FORM_CRM.Presentation
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
        //private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public bool calForm = false;
        public PassData passData;
        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
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
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
                loadGridLookupCustomer();
                loadGridLookupEmployee();
                loadGridLookupMethod();
                loadGridLookupDepartment();
                loadLookupCurrency();
                //glue_idcustomer_I1.PopupFilterMode(Default, Contains, StartsWith);
                glue_idcustomer_I1.Properties.PopupFilterMode = PopupFilterMode.Contains;
                ControlDev.FormatControls.setContainsFilterGridLookupEdit(glue_idcustomer_I1);

                if (calForm == true)
                {
                    loadPurchase(txt_idexport_IK1.Text);
                }
                else
                {
                    Init();
                }
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
            passData(true);
            this.Close();
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
            catch { }
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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_EXPORTDETAIL_C,clsFunction.transLateText( "PHIẾU BÁO GIÁ"), this);
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
            clockButton(false);
        }

        private void bbi_allow_insert_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {     
                     if (saveMaster()==true)
                        saveDetai();                    
                    clockButton(true);
                    Init();
                }
            }
            catch { }
        }

        private void bbi_allow_insert_save_exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {       
                    if (saveMaster()==true)
                        saveDetai();
                    passData(true);
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
        }

        private void btn_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dte_dateimport_I5.EditValue) >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1))
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa phiếu nhập này không?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update quotation set isdelete=1 where idexport='"+ txt_idexport_IK1.Text +"'");
                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from exportdetail where idexport='" + txt_idexport_IK1.Text + "'");
                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from export where idexport='" + txt_idexport_IK1.Text + "'");
                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from STOCKIO where iddetailex not in (select iddetail from exportdetail) ");
                        DataSet ds = new DataSet();
                        ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
                        Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_allow_delete_S.Caption), txt_idexport_IK1.Text, txt_idexport_IK1.Text, SystemInformation.ComputerName.ToString(), "2", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                        Init();
                    }
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Phiếu xuất này đã được kết chuyển, không thể xóa");
                }
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
                callForm();
            }
            catch { }
        }

        private void glue_idprovider_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadInforProvider(glue_idcustomer_I1.Properties.View.FocusedRowHandle);
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
            try
            {
                if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from quotationdetail where iddetail='" + gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "iddetail").ToString() + "'");
            
                        gv_EXPORTDETAIL_C.DeleteRow(gv_EXPORTDETAIL_C.FocusedRowHandle);
                        if (gv_EXPORTDETAIL_C.DataRowCount == 0)
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("delete quotation where idexport='" + txt_idexport_IK1.Text + "'");
                        }
                    }
                }
            }
            catch { }
        }

        private void chk_isvat_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isvat_I6.Checked == true)
            {
                gv_EXPORTDETAIL_C.Columns["vat"].OptionsColumn.AllowEdit = false;
                setVatOrDiscountAll("vat",  (decimal)cal_vat_I4.EditValue);
            }
            else
            {
                gv_EXPORTDETAIL_C.Columns["vat"].OptionsColumn.AllowEdit = true;
            }
        }

        private void chk_isdiscount_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isdiscount_I6.Checked == true)
            {
                gv_EXPORTDETAIL_C.Columns["discount"].OptionsColumn.AllowEdit = false;
                setVatOrDiscountAll("discount", (decimal) cal_vat_I4.EditValue);
            }
            else
            {
                gv_EXPORTDETAIL_C.Columns["discount"].OptionsColumn.AllowEdit = true;
            }
        }

        private void btn_file_S_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           if( e.Button.Caption=="Add")
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
            }
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { "quantity",  "mount",  "amountvat", "amountdiscount", "costs", "total" };
            // Caption column
            string[] caption_col = new string[] {"ID", "Mã hàng", "Ký hiệu", "Tên hàng", "ĐVT", "Kho hàng", "Số lượng", "Đơn giá","Thành tiền", "VAT", "Tiền VAT", "CK Sau VAT", "CK", "Tiền CK","Chi phí","Tổng tiền", "Ghi chú","ID" };
         
            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddetail_IK1", "col_idcommodity_I1", "col_spec_20_S", "col_commodity_S", "col_idunit_I1", "col_idwarehouse_I1", "col_quantity_I4", "col_price_I4", "col_amount_I15", "col_vat_I4", "col_amountvat_I15", "col_davat_I6", "col_discount_I8", "col_amountdiscount_I15", "col_costs_I15", "col_total_I15", "col_note_I3", "col_idexport_I1" };
           
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
            //int so_cot_lue = 0;
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
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;
          
            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns_Edit(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid,gv_EXPORTDETAIL_C.Name);
            clsFunction.CreateTableGrid(fieldname_col, gv_EXPORTDETAIL_C);
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
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='"+ gv_EXPORTDETAIL_C.Name +"'");
            
            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_EXPORTDETAIL_C, read_Only, hien_Nav,
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
                gv_EXPORTDETAIL_C.OptionsBehavior.Editable = true;
                gv_EXPORTDETAIL_C.OptionsView.ColumnAutoWidth = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;

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
                if (gv_EXPORTDETAIL_C.FocusedRowHandle>=0)
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
                bool flag =false;
                if (gv_EXPORTDETAIL_C.FocusedRowHandle>=0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag==true )
                {
                    clsFunction.customPopupMenu(bar_menu_C,menu, gv_EXPORTDETAIL_C, this); 
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
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                    if (checkExitMaHangInGrid(inSt, (DataTable)gct_list_C.DataSource))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");
                    }
                }
                else
                {
                    if (checkExitMaHangInGridSua(inSt, (DataTable)gct_list_C.DataSource))
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

                // kiểm tra đủ số lượng xuất
                if (checkInsert(gv_EXPORTDETAIL_C.GetRowCellValue(e.RowHandle, "idcommodity").ToString(), Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(e.RowHandle, "quantity")))==false)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Số lượng tồn kho không đủ, bạn có muốn xuất âm mặt hàng này") == false)
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(intSl, "Số lượng xuất không đủ");
                    }

                }
            }
            catch { }
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
                ControlDev.FormatControls.LoadGridLookupEdit(glue_method_I1, "select idmethod, MeThodName from dmmethod where status=1 and kind ='OU'", "MeThodName", "idmethod", caption, fieldname, this.Name, glue_method_I1.Width * 2, col_visible);

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
                    dtM = APCoreProcess.APCoreProcess.Read("SELECT        ISNULL(QO.vat, 0) AS VAT, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, ISNULL(QO.discount, 0) AS discount, QO.IDMETHOD,  QO.isvat, QO.idcurrency, ISNULL(QO.exchangerate, 0) AS exchangerate, HIS.reasonquotation, HIS.step, HIS.iddepartment, QO.isdept, QO.outlet, QO.limit,  QO.isdiscount FROM      dbo.QUOTATION AS QO INNER JOIN      dbo.QUOTATIONHIS AS HIS ON QO.idexport = HIS.idquotation and idexport='"+ str +"'");
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
                        txt_note_100_I2.Text = dtM.Rows[0]["note"].ToString();
                        cal_limit_I4.Value =Convert.ToInt32(dtM.Rows[0]["limit"]);
                        rad_isdept_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["isdept"]);
                        rad_outlet_I6.EditValue = Convert.ToBoolean(dtM.Rows[0]["outlet"]);
                        chk_isdiscount_I6.Checked = Convert.ToBoolean(dtM.Rows[0]["isdiscount"]);
                        chk_isvat_I6.Checked = Convert.ToBoolean(dtM.Rows[0]["isvat"]);
                        spe_exchangerate_I4.Value = Convert.ToDecimal(dtM.Rows[0]["exchangerate"]);
                        cal_vat_I4.Value = Convert.ToDecimal(dtM.Rows[0]["vat"]);
                        cal_discount_I4.Value = Convert.ToDecimal(dtM.Rows[0]["discount"]);
                        lue_idcurrency_I1.EditValue = dtM.Rows[0]["idcurrency"].ToString();
                        
                        glue_iddepartment_I1.EditValue = dtM.Rows[0]["iddepartment"].ToString();
                                       
                     }
                    DataTable dtD = new DataTable();
                    dtD = APCoreProcess.APCoreProcess.Read("SELECT     dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.spec, mh.commodity FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport='" + str + "'");
                    if (dtD.Rows.Count > 0)
                    {
                        gct_list_C.DataSource = dtD;
                    }

                    if (gv_EXPORTDETAIL_C.FocusedRowHandle >= 0)
                        loadStatus(txt_idexport_IK1.Text);
                }
            }
            catch { }
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
                dts = APCoreProcess.APCoreProcess.Read("SELECT     dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, mh.spec, mh.commodity FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport='" + txt_idexport_IK1.Text + "'", this.Name + gv_EXPORTDETAIL_C.Name, new string[0, 0], "");
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
            catch { }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e) {
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
                string[] caption = new string[] { "Mã KH", "Tên KH","Tel","Fax","Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer","tel","fax","address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT C.idcustomer, C.customer, CASE WHEN C.idgroup = 0 THEN N'Công ty' ELSE N'Khách lẻ' END AS idgroup, C.tel, C.fax, C.address, C.status FROM   dbo.DMCUSTOMERS AS C INNER JOIN   dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer INNER JOIN  dbo.DMPROVINCE AS P ON C.idprovince = P.idprovince INNER JOIN dbo.EMPLOYEES AS EM ON EM.IDEMP = E.idemp AND CHARINDEX('"+clsFunction.GetIDEMPByUser()+"', EM.idrecursive) > 0 AND E.status = 'True' WHERE     (C.status = 1)", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width*2,col_visible);
                
            }
            catch { }
        }

        private void loadInfoProvider()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from dmcustomers where idcustomer='"+ glue_idcustomer_I1.EditValue.ToString() +"'");
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

        private void loadInforProvider(int index)
        {
            try
            {
                txt_address_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcDiachi).ToString();
                txt_fax_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcFax).ToString();
                txt_tel_S.Text = glue_idcustomer_I1.Properties.View.GetRowCellValue(index, gcSodt).ToString();
            }
            catch { }
        }

        private void Init()
        {
            try
            {
                glue_idcustomer_I1.EditValue = null;
                glue_IDEMP_I1.EditValue = null;
                dte_dateimport_I5.EditValue = DateTime.Now;
                dte_limitdept_I5.EditValue = DateTime.Now;
                rad_isdept_I6.EditValue = true;
                rad_outlet_I6.EditValue = true;
                cal_limit_I4.Value = 0;      
                txt_idexport_IK1.Text = clsFunction.layMa("QS","idexport","Quotation");
                while (gv_EXPORTDETAIL_C.RowCount > 0)
                {
                    gv_EXPORTDETAIL_C.DeleteRow(0);
                }
               
            }
            catch 
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
            catch { }
        }

        private void loadGridLookupDepartment()
        {
            try
            {
                string[] caption = new string[] { "Mã PB", "Tên Phòng Ban" };
                string[] fieldname = new string[] { "iddepartment", "department" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_iddepartment_I1, "select iddepartment, department from DMDEPARTMENT", "department", "iddepartment", caption, fieldname, this.Name, glue_iddepartment_I1.Width);
            }
            catch { }
        }

        private void loadLookupCurrency()
        {
            try
            {
                string[] caption = new string[] { "Mã TT", "Tiền Tệ" };
                string[] fieldname = new string[] { "idcurrency", "currency" };
                ControlDev.FormatControls.LoadLookupEdit(lue_idcurrency_I1, "select idcurrency, currency from DMCURRENCY", "currency", "idcurrency", caption, fieldname, statusForm, this.Name,this.Name);
            }
            catch { }
        }

        private void setRowByIDcommodity(int index, string idcommodity)
        {
            try
            {
                // set default
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "spec", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "commodity", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "iddetail", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "idunit", "");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "idwarehouse","");
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "quantity", 1);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "price", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", 0);        
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", 0);                
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "costs", 0);
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "davat", false);
                if (chk_isvat_I6.Checked == true)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index,"vat",cal_vat_I4.EditValue);
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
                dt = APCoreProcess.APCoreProcess.Read("select idcommodity, spec, commodity, idunit, idwarehouse, price from dmcommodity where idcommodity='"+ idcommodity +"' ");
                if (dt.Rows.Count > 0)
                {
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "spec", dt.Rows[0]["spec"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "commodity", dt.Rows[0]["commodity"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "iddetail", dt.Rows[0]["idcommodity"].ToString() + "_" + txt_idexport_IK1.Text);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idunit", dt.Rows[0]["idunit"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idwarehouse", dt.Rows[0]["idwarehouse"].ToString());
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "quantity", 1);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "price", Convert.ToDouble(dt.Rows[0]["price"].ToString()));
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "price")));
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "vat", 0);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "davat", false);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "discount", 0);
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "idexport", txt_idexport_IK1.Text);
                }
            }
            catch             
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                if (gv_EXPORTDETAIL_C.GetRowCellValue(index, "price").ToString()!="")
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
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price*(vat/100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "quantity")) * price * (discount / 100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amount")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountvat")) + Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "amountdiscount")) + costs);
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountvat", Convert.ToDouble(amount * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"))/100));
                gv_EXPORTDETAIL_C.SetRowCellValue(index, "total", amount * (Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat"))/100 +1));
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount*(1+vat/100) * discount / 100);
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                    gv_EXPORTDETAIL_C.SetRowCellValue(index, "amountdiscount", amount * (Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "vat")) / 100+1)  * Convert.ToDouble(gv_EXPORTDETAIL_C.GetRowCellValue(index, "discount")) / 100);
                    //gv_PURCHASEDETAIL_C.SetRowCellValue(index, "total", amount + amountvat - amountdiscount);
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                clsFunction.MessageInfo("Thông báo", "Lỗi");
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
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
        }

        private bool saveMaster()
        {
            bool flag = false;
            if (checkCallPurchase() == false)
            {
                clsFunction.Insert_data(this, this.Name);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update Quotation  set status='0', isdelete=0,vat=" + Convert.ToDouble(cal_vat_I4.Value) + ", discount=" + Convert.ToDouble(cal_discount_I4.Value) + ",isvat='" + chk_isvat_I6.EditValue + "',isdiscount='" + chk_isdiscount_I6.EditValue + "',    dateimport='" + dte_dateimport_I5.EditValue.ToString() + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "' where idexport='" + txt_idexport_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert.Caption), txt_idexport_IK1.Text, txt_idexport_IK1.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            }
            else
            {   
                clsFunction.Edit_data(this, this.Name,"idexport",txt_idexport_IK1.Text);
                APCoreProcess.APCoreProcess.ExcuteSQL(" update quotation set status='0',  dateimport='" + dte_dateimport_I5.EditValue.ToString() + "',vat=" + Convert.ToDouble(cal_vat_I4.Value) + ", discount=" + Convert.ToDouble(cal_discount_I4.Value) + ",isvat='" + chk_isvat_I6.EditValue + "',isdiscount='" + chk_isdiscount_I6.EditValue + "', limitdept='" + dte_limitdept_I5.EditValue.ToString() + "' where idexport='" + txt_idexport_IK1.Text + "' ");
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idexport_IK1.Name) + " = '" + txt_idexport_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert_save.Caption), txt_idexport_IK1.Text, txt_idexport_IK1.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            }
            insertFile();
            flag = true;
            return flag;
        }

        private void insertFile()
        {
            string path = "";
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
            }
        }

        private void saveDetai()
        {
            try
            {  
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from quotationdetail where idexport='"+ txt_idexport_IK1.Text +"'");
                DataTable dt = new DataTable();                
                for (int i = 0; i < gv_EXPORTDETAIL_C.DataRowCount; i++)
                {
                    dt = APCoreProcess.APCoreProcess.Read("QUOTATIONDETAIL where idexport='" + txt_idexport_IK1.Text + "' and idcommodity='"+ gv_EXPORTDETAIL_C.GetRowCellValue(i, "idcommodity").ToString() +"'");
                    if (dt.Rows.Count==0)// thêm mới
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < gv_EXPORTDETAIL_C.Columns.Count; j++)
                        {
                            if (gv_EXPORTDETAIL_C.Columns[j].Name.Contains("_S") == false)
                            {
                                dr[gv_EXPORTDETAIL_C.Columns[j].FieldName] = gv_EXPORTDETAIL_C.GetRowCellValue(i, gv_EXPORTDETAIL_C.Columns[j].FieldName);
                            }
                        }
                        dr["status"] = false;
                        dr["pending"] = false;
                        dr["give"] = false;
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }       
                }
                
            }
            catch 
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
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
            if (Convert.ToDateTime(dte_dateimport_I5.EditValue) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1))
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Phiếu xuất hàng này đã kết chuyển không thể chỉnh sửa");
                dte_dateimport_I5.Focus();
                return false;
            }
            if (glue_IDEMP_I1.EditValue == null)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo","Bạn chưa chọn nhân viên");
                glue_IDEMP_I1.Focus();
                return false;
            }
            if (glue_idcustomer_I1.EditValue == null)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Bạn chưa chọn khách hàng");
                glue_idcustomer_I1.Focus();
                return false;
            }
            if (APCoreProcess.APCoreProcess.Read("select idquotation from QUOTATIONHIS where idquotation='"+txt_idexport_IK1.Text+"' and iddepartment='"+glue_iddepartment_I1.EditValue.ToString() +"' and status='1' ").Rows.Count>0)
            {
                flag = false;
                clsFunction.MessageInfo("Thông báo", "Phiếu báo giá này đã được duyệt, không thể chỉnh sửa");
                txt_idexport_IK1.Focus();
                return false;
            }
            return flag;
        }

        private bool checkInsert(string idcommodity, double quantity)
        {
            bool flag = false;
            DataTable dtI = new DataTable();
            dtI = APCoreProcess.APCoreProcess.Read("SELECT     dt.quantity - SUM(ISNULL(stk.quantity, 0)) AS quantity, dt.iddetail FROM         PURCHASEDETAIL AS dt INNER JOIN   PURCHASE AS mt ON dt.idpurchase = mt.idpurchase LEFT OUTER JOIN   STOCKIO AS stk ON dt.iddetail = stk.iddetailpur GROUP BY dt.idcommodity, dt.status, dt.iddetail, dt.quantity HAVING      (dt.idcommodity = N'"+ idcommodity +"') AND (dt.status = 0) AND (dt.quantity - SUM(ISNULL(stk.quantity, 0)) > "+ quantity +") ");
            if (dtI.Rows.Count > 0)// tồn kho đủ
            {
                flag = true;
            }
            flag = true;
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
            if (APCoreProcess.APCoreProcess.Read(" SELECT     idexport from QUOTATION where idexport='" + txt_idexport_IK1.Text + "'").Rows.Count > 0)
            {
                flag = true;
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

        private void setVatOrDiscountAll(string fieldName ,Decimal value)
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
        #endregion              


    }
}