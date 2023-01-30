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
using DevExpress.XtraGrid;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_BaoTri_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_BaoTri_SH()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "BT";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();

        #endregion

        #region FormEvent

        private void frm_BaoTri_SH_Load(object sender, EventArgs e)
        {
            statusForm = false;
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {      
                SaveGridControls();
                SaveGridControlsChiTiet();
            }
            else
            {
                bbi_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                bbi_todate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                Function.clsFunction.TranslateForm(this, this.Name);                
                Load_Grid();
                Load_Grid_Chitiet();
                loadGrid();
                loadGridLookupLoaiBaoTri();
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
            }
        }

        private void loadGridLookupLoaiBaoTri()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã Loại", "Tên Loại" };
                string[] fieldname = new string[] { "idloaibaotri", "loaibaotri" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idloaibaotri_I1, "select idloaibaotri, loaibaotri from DMLOAIBAOTRI where status =1", "loaibaotri", "idloaibaotri", caption, fieldname, this.Name, glue_idloaibaotri_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void frm_BaoTri_SH_KeyDown(object sender, KeyEventArgs e)
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
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
            }
        }
        
        #endregion

        #region ButtonEvent

        private void bbi_allow_print_config_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadReport();
        }  

        private void bbi_allow_insert_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_BAOTRI_S frm = new frm_BAOTRI_S();
                frm._insert = true;
                frm._sign = _sign;       
                frm.statusForm = statusForm;
                frm.passData = new frm_BAOTRI_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo( "Thông báo","Lỗi thực thi Error :"+ex.Message);
            }
        }

        private void bbi_allow_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            try
            {
                menu.HidePopup();
                frm_BAOTRI_S frm = new frm_BAOTRI_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString();
                frm.passData = new frm_BAOTRI_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo","Lỗi, vui lòng chọn dòng cần sửa Error:"+ex.Message);
            }
        }

        private void bbi_allow_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from baotridetail where mabaotri='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from baotri where id='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString() + "'");
                gv_list_C.DeleteRow(gv_list_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            DataTable dtRP = new DataTable();
            dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='"+ this.Name +"' and iscurrent=1");
            if (dtRP.Rows.Count > 0)
            {
                XtraReport report = XtraReport.FromFile(Application.StartupPath +"\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_list_C,clsFunction.transLateText( "Danh mục chiến dịch"), this);
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
            }
        }

        private void bbi_exit_allow_access_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            this.Close();
            Function.clsFunction.sotap--;
        }

        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString();
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
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Số PO",  "Ngày tạo","Mã KH","Khách hàng","Mã NV", "Nhân viên", "Loại bảo trì","Địa chỉ bảo trì", "Khách hàng/ Công ty","Loại", "Ghi chú" };
         
            // FieldName column
            string[] fieldname_col = new string[] { "mabaotri", "idquotation", "ngaytao","idcustomer", "customer","idemp","staffname","loaibaotri", "diachibaotri",  "khachhang" ,"loai","note" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "DateColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "110", "100","100",  "300", "100","150","150", "250",  "100","120", "300" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False","False", "False", "False", "False","False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True","True", "True", "True", "True","True", "True", "True", "True", "True", "True"};
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
            string[] sql_glue = new string[] { "select idpriority, priority  from dmpriority where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "priority" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {"idpriority" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã UT", "Ưu tiên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "idpriority", "priority" } };
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
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControlsChiTiet()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Chọn","id","Mã SP", "Sản phẩm", "Số lượng", "Loại BH" };

            // FieldName column
            string[] fieldname_col = new string[] { "check","id", "idcommodity", "commodity", "quantity","idloaibaotri"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "CheckColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100","100","100", "350", "100","100"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "False", "True", "True", "True", "False" };
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
            string[] sql_glue = new string[] {  };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0] { };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0] {};
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_detail_C.Name);
        }

        private void Load_Grid_Chitiet()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_detail_C.OptionsView.ShowAutoFilterRow = true;
            gv_detail_C.OptionsBehavior.Editable = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_detail_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_detail_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_detail_C,
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

        private void gv_list_C_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_ChoThueXe_S frm = new frm_ChoThueXe_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString();
                frm.passData = new frm_ChoThueXe_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
               // Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void gv_list_C_MouseUp(object sender, MouseEventArgs e)
        {
            
            try
            {
                menu.ItemLinks.Clear();
                bool flag =false;
                if (gv_list_C.FocusedRowHandle>=0)
                {
                    flag = true;
                }
                else
                {
                    flag = true;
                }
                
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag==true )
                {
                    clsFunction.customPopupMenu(bar_menu_C,menu, gv_list_C, this); 
                    menu.Manager.ItemClick+=new ItemClickEventHandler(Manager_ItemPress);
                    menu.ShowPopup(MousePosition);         
                }
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
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
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='"+ gv_list_C.Name +"' ");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='"+ strColVisible +"' where form_name='"+this.Name+"' and grid_name='"+gv_list_C.Name+"'");
                            Load_Grid();
                        }         
                    }
                }                
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
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

        

        private void loadGrid()
        {
            if (Function.clsFunction._pre == true)
            {
                dts = Function.clsFunction.dtTrace;
            }
            else
            {
                String dkManager = "";
                if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
                {
                    dkManager = " or 1=1 ";
                }

                dts = APCoreProcess.APCoreProcess.Read("SELECT        B.idquotation, B.ngaytao, CASE WHEN B.khachhang = 1 THEN N'Khách hàng' ELSE N'Công ty' END AS khachhang,  CASE WHEN B.loai = 1 THEN N'Bảo hành' ELSE N'Bảo trì' END AS loai, B.idemp, B.mabaotri, B.note, B.idcustomer, B.diachibaotri, E.StaffName, isnull(C.customer, N'EPS Việt Nam') as customer, L.loaibaotri FROM   dbo.BAOTRI AS B INNER JOIN   dbo.DMCUSTOMERS AS C ON B.idcustomer = C.idcustomer INNER JOIN   dbo.EMPLOYEES AS E ON B.idemp = E.IDEMP INNER JOIN DMLOAIBAOTRI L ON B.idloaibaotri = L.idloaibaotri where (charindex('" + clsFunction.GetIDEMPByUser() + "',E.idrecursive) >0 " + dkManager + ") AND E.status='True' ");
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
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
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
              dt = APCoreProcess.APCoreProcess.Read( Function.clsFunction.getNameControls(this.Name));
              ds.Tables.Add(dt);
            //Instantiating your DataSet instance here
            //.....
            //Pass the created DataSet to your report:
            ((XtraReport)sender).DataSource = ds;
            ((XtraReport)sender).DataMember = ds.Tables[0].TableName;     
        }

        private void bbi_search_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadGrid();
        }

        private void bbi_option_S_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dtn = DateTime.Now;
            if (bbi_option_S.EditValue.ToString() == "Tuần này")
            {
                bbi_fromdate_S.EditValue = clsFunction.GetFirstDayOfWeek(dtn).AddDays(1);
                bbi_todate_S.EditValue = Convert.ToDateTime(bbi_fromdate_S.EditValue).AddDays(6);
            }
        }

        private void bar_menudetail_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_list_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_list_C.Name + "'");
                            Load_Grid_Chitiet();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void bbi_insert_chitiet_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_ChoThueXeChiTiet_S frm = new frm_ChoThueXeChiTiet_S();
                frm._insert = true;
                frm._sign = "DV";
                frm.statusForm = statusForm;
                frm.idchothuexe = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idchothuexe").ToString();
                frm.passData = new frm_ChoThueXeChiTiet_S.PassData(getValueChoThueXeDetail);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_detail_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from ChoThueXeChiTiet where idchothuexechitiet='" + gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idchothuexechitiet").ToString() + "'");
                gv_detail_C.DeleteRow(gv_detail_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_chitiet_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_ChoThueXeChiTiet_S frm = new frm_ChoThueXeChiTiet_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idchothuexechitiet").ToString();
                frm.passData = new frm_ChoThueXeChiTiet_S.PassData(getValueChoThueXeDetail);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void getValueChoThueXeDetail(bool value)
        {
            if (value == true)
            {
                loadChoThueDetail();
            }
        }

        private void loadChoThueDetail()
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from ChoThueXeChiTiet where idchothuexe ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idchothuexe") + "'");
            gct_detail_C.DataSource = dt;
        }

        #endregion

        private void gv_list_C_Click(object sender, EventArgs e)
        {
            loadDataByPO();
        }

        private void loadDataByPO()
        {
            
            DataTable dtDetail = null;
            if (gv_list_C.GetRowCellDisplayText(gv_list_C.FocusedRowHandle, "khachhang").ToString() == "Khách hàng")
            {
                dtDetail = APCoreProcess.APCoreProcess.Read("SELECT     [check], idloaibaotri,   D.idcommodity, C.commodity, D.quantity FROM  dbo.QUOTATIONDETAIL AS D INNER JOIN   dbo.DMCOMMODITY AS C ON D.idcommodity = C.idcommodity INNER JOIN  dbo.QUOTATION AS Q ON D.idexport = Q.idexport LEFT JOIN BAOTRI_SP BS ON D.idcommodity=BS.idcommodity AND BS.mabaotri='"+ gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"mabaotri") +"' WHERE        (Q.invoiceeps = '" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idquotation").ToString() + "') GROUP BY D.idcommodity, C.commodity, D.quantity, [check], idloaibaotri");
            }
            else
            {
                dtDetail = APCoreProcess.APCoreProcess.Read("SELECT      [check],  idloaibaotri,  mininventory AS quantity, idcommodity, commodity, idcommodity AS id FROM   dbo.DMRENTDEVICE AS D LEFT JOIN BAOTRI_SP BS ON D.idcommodity=BS.idcommodity AND BS.mabaotri='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri") + "'");
            }
            gct_detail_C.DataSource = dtDetail;

            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from baotri where mabaotri ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                rad_loaibaohanh_S.EditValue = Convert.ToBoolean(dt.Rows[0]["loaibaohanh"]);
                spe_sogiochay_I4.Value = Convert.ToDecimal(dt.Rows[0]["sogiochay"]);
                spe_sogiobatdau_S.Value = Convert.ToDecimal(dt.Rows[0]["sogio"]);
            }
        }

        private void loadBaoTriBaoHanh(string idcommodity, string invoiceeps)
        {
            glue_idloaibaotri_I1.EditValue = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idloaibaotri").ToString();
            if (Convert.ToBoolean(rad_loaibaohanh_S.EditValue) == true)
            {
                gcBaotriGio.Visible = false;
                gcBaotriNgay.Visible = true;
                spe_sogiochay_I4.Enabled = false;
            }
            else
            {
                gcBaotriGio.Visible = true;
                gcBaotriNgay.Visible = false;
                spe_sogiochay_I4.Enabled = true;
            }

            if (idcommodity == string.Empty)
            {
                // load data default
                loadDefault();
            }
            else
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from BAOHANH where idcommodity='" + idcommodity + "' AND invoiceeps='" + invoiceeps + "'");
                if (dt.Rows.Count > 0)
                {
                    gct_baohanh_kehoach_C.DataSource = dt;
                }
                else
                {
                    loadDefault();
                }
            }

        }

        private void loadDefault()
        {
            try
            {
                string sql = "";
                sql = "select * from DMBAOHANH where idloaibaotri='" +glue_idloaibaotri_I1.EditValue.ToString() + "'";

                DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
                gct_baohanh_kehoach_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void btn_allow_insert_baohanh_S_Click(object sender, EventArgs e)
        {
            saveBaoHanh();
        }

        private void saveBaoHanh()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < gv_baohanh_kehoach_C.DataRowCount; i++)
            {
                if (Convert.ToBoolean(rad_loaibaohanh_S.EditValue))
                {
                    if (gv_baotri_C.GetRowCellValue(i, gcBaotriNgay).ToString() == "")
                    {
                        clsFunction.MessageInfo("Thông báo", "Vui lòng nhập đầy đủ thông tin ngày tháng bảo hành/ bảo trì");
                        return;
                    }
                }
                else
                {
                    if (gv_baotri_C.GetRowCellValue(i, gcBaotriGio) == null || gv_baotri_C.GetRowCellValue(i, gcBaotriGio).ToString() == "0")
                    {
                        clsFunction.MessageInfo("Thông báo", "Vui lòng nhập đầy đủ thông tin số giờ bảo hành/ bảo trì");
                        return;
                    }
                }

                dt = APCoreProcess.APCoreProcess.Read(" BAOHANH where id='" + gv_baohanh_kehoach_C.GetRowCellValue(i,gcId).ToString() + "'");
                DataRow dr;
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    dr["id"] = Convert.ToInt32(gv_baohanh_kehoach_C.GetRowCellValue(i, gcId));
                    dr["idbaohanh"] = gv_baohanh_kehoach_C.GetRowCellValue(i, gcId).ToString();
                    dr["idcommodity"] = gv_detail_C.GetRowCellValue(i, "idcommodity").ToString();
                    dr["invoiceeps"] = gv_list_C.GetRowCellValue(i, "invoiceeps").ToString();
                    dr["sogio"] = Convert.ToInt32(gv_baotri_C.GetRowCellValue(i, gcBaotriGio));
                    dr["ghichu"] = gv_baotri_C.GetRowCellValue(i, gcBaotriGhiChu).ToString();
                   
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["id"] = Convert.ToInt32(gv_baohanh_kehoach_C.GetRowCellValue(i, gcId));
                    dr["idbaohanh"] = gv_baohanh_kehoach_C.GetRowCellValue(i, gcId).ToString();
                    dr["idcommodity"] = gv_detail_C.GetRowCellValue(i, "idcommodity").ToString();
                    dr["invoiceeps"] = gv_list_C.GetRowCellValue(i, "invoiceeps").ToString();
                    dr["sogio"] = Convert.ToInt32(gv_baotri_C.GetRowCellValue(i, gcBaotriGio));
                    dr["ghichu"] = gv_baotri_C.GetRowCellValue(i, gcBaotriGhiChu).ToString();

                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }

                clsFunction.MessageInfo("Thông báo", "Lưu thành công");
            }
        }


        private void rad_loaibaohanh_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBaoTriBaoHanh(gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString(), gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idquotation").ToString());
        }

        private void gv_detail_C_Click(object sender, EventArgs e)
        {
            try
            {
                loadBaoTriBaoHanh(gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString(), gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idquotation").ToString());
                saveScheduleDefault();
            }
            catch(Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gv_baohanh_kehoach_C_Click(object sender, EventArgs e)
        {
            try
            {
                string mabaotri = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString();
                string idcommodity = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString();
                DataTable dtBaotri = APCoreProcess.APCoreProcess.Read("select * from baotri where mabaotri ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString() + "'");
                if (dtBaotri.Rows.Count > 0)
                {
                    DataTable dt = APCoreProcess.APCoreProcess.Read("select * from dmbaohanh where id='" + gv_baohanh_kehoach_C.GetRowCellValue(gv_baohanh_kehoach_C.FocusedRowHandle, "id") + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string mabaohanh = dt.Rows[0]["id"].ToString();
                         DataTable dtBaotriDetail = APCoreProcess.APCoreProcess.Read("select * from baotridetail where mabaotri='" + mabaotri + "' and mabaohanh='" + mabaohanh + "' and idcommodity ='"+ idcommodity +"'");
                         if (dtBaotriDetail.Rows.Count > 0)
                         {
                             gct_baotri_C.DataSource = dtBaotriDetail;
                         }
                         else
                         {
                             gct_baotri_C.DataSource = dtBaotriDetail;
                         }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void saveScheduleDefault()
        {
            try
            {
                string mabaotri = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString();
                string idcommodity = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle,"idcommodity").ToString();
                bool theothang_theogio = false;// theo thang true
                theothang_theogio = Convert.ToBoolean(rad_loaibaohanh_S.EditValue);
                DataTable dtBaotri = APCoreProcess.APCoreProcess.Read("select * from baotri where mabaotri ='" + mabaotri + "'");
                if (dtBaotri.Rows.Count > 0)
                {
                    int sothangbaotri = Convert.ToInt32(dtBaotri.Rows[0]["sothang"]);
                    int solanbaotritheogio = Convert.ToInt32(dtBaotri.Rows[0]["solanbaotritheogio"]);
                    DateTime ngaybatdau = Convert.ToDateTime(dtBaotri.Rows[0]["ngaybatdau"]);
                    for (int i = 0; i < gv_baohanh_kehoach_C.DataRowCount; i++)
                    {
                        DataTable dt = APCoreProcess.APCoreProcess.Read("select * from dmbaohanh where id='" + gv_baohanh_kehoach_C.GetRowCellValue(i, "id") + "'");
                        if (dt.Rows.Count > 0)
                        {
                            string mabaohanh = dt.Rows[0]["id"].ToString();
                            int sogio = Convert.ToInt32(dt.Rows[0]["sogio"]);
                            DataTable dtBaotriDetail = APCoreProcess.APCoreProcess.Read("select * from baotridetail where mabaotri='" + mabaotri + "' and mabaohanh='" + mabaohanh + "' and idcommodity ='"+ idcommodity +"'");
                            if (dtBaotriDetail.Rows.Count == 0)
                            {
                                
                                if (theothang_theogio)
                                {
                                    int sothangbaotri1lan = Convert.ToInt32(dt.Rows[0]["sothang"]);
                                    int sogiobaotri1lan = Convert.ToInt32(dt.Rows[0]["sogio"]);
                                    int solanbaotri = sothangbaotri / sothangbaotri1lan;

                                    for (int j = 0; j < solanbaotri; j++)
                                    {
                                        DataRow dr = dtBaotriDetail.NewRow();
                                        dr["id"] = clsFunction.layMa("BT","id","baotridetail");
                                        dr["idcommodity"] = idcommodity;
                                        dr["mabaotri"] = mabaotri;
                                        dr["ghichu"] = "";
                                        dr["mabaohanh"] = mabaohanh;
                                        dr["sogiochay"] = -1;
                                        dr["ngaybh"] = ngaybatdau.AddMonths((j+1)* sothangbaotri1lan);
                                        dtBaotriDetail.Rows.Add(dr);
                                        APCoreProcess.APCoreProcess.Save(dr);
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtBaotriDetail.NewRow();
                                    dr["id"] = clsFunction.layMa("BT", "id", "baotridetail");
                                    dr["idcommodity"] = idcommodity;
                                    dr["mabaotri"] = mabaotri;
                                    dr["ghichu"] = "";
                                    dr["mabaohanh"] = mabaohanh;
                                    dr["sogiochay"] = sogio;
                                    dtBaotriDetail.Rows.Add(dr);
                                    APCoreProcess.APCoreProcess.Save(dr);
                                }
                            }
                        }                        
                    }                   
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void btn_allow_insert_baohanh_S_Click_1(object sender, EventArgs e)
        {
            try
            {

                DataTable dtBS = new DataTable();
                dtBS = APCoreProcess.APCoreProcess.Read("select * from BAOTRI_SP where mabaotri='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString() + "' AND idcommodity ='" + gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString() + "'");
                DataRow dr;
                if (dtBS.Rows.Count == 0)
                {
                    dr = dtBS.NewRow();
                    dr["mabaotri"] = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString();
                    dr["idcommodity"] = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString();
                    dr["idloaibaotri"] = glue_idloaibaotri_I1.EditValue.ToString();
                    dr["check"] = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "check").ToString();
                    dtBS.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                else
                {
                    dr = dtBS.Rows[0];
                    dr["mabaotri"] = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "mabaotri").ToString();
                    dr["idcommodity"] = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString();
                    dr["idloaibaotri"] = glue_idloaibaotri_I1.EditValue.ToString();
                    dr["check"] = gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "check").ToString();
                    APCoreProcess.APCoreProcess.Save(dr);
                }

                DataTable dt = (DataTable)gct_baotri_C.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dt.Rows[i];
                    APCoreProcess.APCoreProcess.Save(dr1);
                }
                APCoreProcess.APCoreProcess.ExcuteSQL("update baotri set sogiochay="+ spe_sogiochay_I4.Value +" where mabaotri='"+ gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"mabaotri").ToString() +"'");
                clsFunction.MessageInfo("Thông báo", "Lưu thành công");
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idloaibaotri_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadDefault();
        }
    }
}