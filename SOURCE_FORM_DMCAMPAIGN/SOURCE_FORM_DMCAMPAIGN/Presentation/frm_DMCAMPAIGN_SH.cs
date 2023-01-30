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
    public partial class frm_DMCAMPAIGN_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_DMCAMPAIGN_SH()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "CD";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();

        #endregion

        #region FormEvent

        private void frm_DMCAMPAIGN_SH_Load(object sender, EventArgs e)
        {
            //statusForm = true;
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {      
                SaveGridControls();
            }
            else
            {
                bbi_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                bbi_todate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                Function.clsFunction.TranslateForm(this, this.Name);                
                Load_Grid();
                loadGrid();
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
                

            }
        }

        private void frm_DMCAMPAIGN_SH_KeyDown(object sender, KeyEventArgs e)
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
                frm_DMCAMPAIGN_S frm = new frm_DMCAMPAIGN_S();
                frm._insert = true;
                frm._sign = _sign;       
                frm.statusForm = statusForm;
                frm.passData = new frm_DMCAMPAIGN_S.PassData(getValueUpdate);
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
                frm_DMCAMPAIGN_S frm = new frm_DMCAMPAIGN_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString();
                frm.passData = new frm_DMCAMPAIGN_S.PassData(getValueUpdate);
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
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from campaigncus where idcampaign='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from campaignmiss where idcampaign='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from dmcampaign where idcampaign='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString() + "'");
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
            string[] caption_col = new string[] { "Mã CD", "Ký hiệu", "Chiến dịch","Từ ngày","Đến ngày","Tần số", "Nhắc nhỡ", "Ngày nhắc","Ngày kết thúc", "Ưu tiên" , "Ghi chú", "Status" };
         
            // FieldName column
            string[] fieldname_col = new string[] { "idcampaign", "sign", "campaign","fromdate", "todate", "frequency","remind", "dateremind","finishdate", "idpriority" ,"note", "status" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "DateColumn", "DateColumn", "TextColumn", "CheckColumn", "DateColumn", "DateColumn", "GridLookupEditColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "100", "100", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True", "True", "True" };
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
                frm_DMCAMPAIGN_S frm = new frm_DMCAMPAIGN_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcampaign").ToString();
                frm.passData = new frm_DMCAMPAIGN_S.PassData(getValueUpdate);
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
                //dts = APCoreProcess.APCoreProcess.Read("select CD.* from  " + Function.clsFunction.getNameControls(this.Name) + " CD  where   (convert(datetime, cast(datediff(d,0,CD.fromdate) as datetime) ,103)   between convert(datetime, cast(datediff(d,0,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "') as datetime) ,103)  and convert(datetime, cast(datediff(d,0,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "') as datetime) ,103)  and convert(datetime, cast(datediff(d,0,CD.todate) as datetime) ,103)  between  convert(datetime, cast(datediff(d,0,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "') as datetime),103) and   convert(datetime, cast(datediff(d,0,'" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "') as datetime) ,103)) or CD.unlimited =1 order by fromdate ");

                // dts = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT CD.todate, CD.fromdate, CD.status, case when CD.unlimited = 1 then CD.campaign + N' Không giới hạn'  else CD.campaign + N' - Từ ngày '+ convert(varchar(10), CD.fromdate,103) +N' - Đến ngày '+ convert(varchar(108),CD.todate,103) end as campaign, CD.note, CD.sign, CD.idcampaign, CD.frequency, CD.finishdate, CD.remind, CD.dateremind,  CD.idpriority, CD.idemp, Q.idemp as idempsale, CD.chitieuds, CD.chitieukh, CD.unlimited,sum(case when (step =1 or step=2 or step=3 or step=4 or step=5  or step=6) then 1 else 0 end) as nhucau,sum(case when (step=2 or step=3 or step=4 or step=5  or step=6) then 1 else 0 end) as hoigia,sum(case when (step=3 or step=4 or step=5  or step=6) then 1 else 0 end) as baogia,sum(case when (step=4 or step=5  or step=6) then 1 else 0 end) as thuongluong,sum(case when (step=5  or step=6) then 1 else 0 end) as chotbaogia, sum(case when (step=5  or step=6) then dt.quantity else 0 end) as soluong,  sum(case when (step=5  or step=6) then dt.total else 0 end) as tongtien,sum(case when step =6 then 1 else 0 end) as huy FROM  dbo.DMCAMPAIGN AS CD LEFT JOIN (SELECT     ISNULL(idcampaign,(SELECT     TOP (1) idcampaign FROM   dbo.DMCAMPAIGN WHERE      (unlimited = 1))) AS idcampaign, QUOTATION.idstatusquotation, DMSTATUSQUOTATION.step, QUOTATION.idemp , QUOTATION.idexport FROM    dbo.QUOTATION INNER JOIN DMSTATUSQUOTATION ON DMSTATUSQUOTATION.idstatusquotation = QUOTATION.idstatusquotation INNER JOIN EMPLOYEES EM ON EM.IDEMP = QUOTATION.idemp Where charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive)>0 AND EM.status='True' AND QUOTATION.dateimport BETWEEN CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) AND CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) ) AS Q ON Q.idcampaign = CD.idcampaign LEFT JOIN QUOTATIONDETAIL DT ON Q.idexport=DT.idexport GROUP BY CD.todate, CD.fromdate, CD.status, CD.idcampaign, CD.note, CD.sign, CD.campaign, CD.frequency, CD.finishdate, CD.remind, CD.dateremind, CD.idpriority, CD.idemp, CD.chitieuds, CD.chitieukh, CD.unlimited, Q.IDEMP HAVING      ((CONVERT(datetime, CAST(DATEDIFF(d, 0, CD.fromdate) AS datetime), 103) BETWEEN CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) AND CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "', 102)) AS datetime), 103))   AND (CONVERT(datetime, CAST(DATEDIFF(d, 0, CD.todate) AS datetime), 103) BETWEEN CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME,   '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) AND CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "', 102)) AS datetime), 103)) OR CD.unlimited=1) ORDER BY CD.fromdate");
                dts = APCoreProcess.APCoreProcess.Read("select * from DMCAMPAIGN where  ((CONVERT(datetime, CAST(DATEDIFF(d, 0, CD.fromdate) AS datetime), 103) BETWEEN CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) AND CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "', 102)) AS datetime), 103))   AND (CONVERT(datetime, CAST(DATEDIFF(d, 0, CD.todate) AS datetime), 103) BETWEEN CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME,   '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_fromdate_S.EditValue)).ToString() + "', 102)) AS datetime), 103) AND CONVERT(datetime, CAST(DATEDIFF(d, 0, CONVERT(DATETIME, '" + clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(bbi_todate_S.EditValue)).ToString() + "', 102)) AS datetime), 103)) OR CD.unlimited=1)");
            }
            gct_list_C.DataSource = dts;
   
            //gv_list_C.ExpandAllGroups();

            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "nhucau";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item1.DisplayFormat = "{0:N0}";
            item1.ShowInGroupColumnFooter = gv_list_C.Columns["nhucau"];
            gv_list_C.GroupSummary.Add(item1);

            GridGroupSummaryItem item2 = new GridGroupSummaryItem();
            item2.FieldName = "hoigia";
            item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item2.DisplayFormat = "{0:N0}";
            item2.ShowInGroupColumnFooter = gv_list_C.Columns["hoigia"];
            gv_list_C.GroupSummary.Add(item2);

            GridGroupSummaryItem item3 = new GridGroupSummaryItem();
            item3.FieldName = "baogia";
            item3.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item3.DisplayFormat = "{0:N0}";
            item3.ShowInGroupColumnFooter = gv_list_C.Columns["baogia"];
            gv_list_C.GroupSummary.Add(item3);

            GridGroupSummaryItem item4 = new GridGroupSummaryItem();
            item4.FieldName = "thuongluong";
            item4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item4.DisplayFormat = "{0:N0}";
            item4.ShowInGroupColumnFooter = gv_list_C.Columns["thuongluong"];
            gv_list_C.GroupSummary.Add(item4);

            GridGroupSummaryItem item5 = new GridGroupSummaryItem();
            item5.FieldName = "chotbaogia";
            item5.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item5.DisplayFormat = "{0:N0}";
            item5.ShowInGroupColumnFooter = gv_list_C.Columns["chotbaogia"];
            gv_list_C.GroupSummary.Add(item5);

            GridGroupSummaryItem item6 = new GridGroupSummaryItem();
            item6.FieldName = "huy";
            item6.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item6.DisplayFormat = "{0:N0}";
            item6.ShowInGroupColumnFooter = gv_list_C.Columns["huy"];
            gv_list_C.GroupSummary.Add(item6);
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

        #endregion

       
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

        
       
    }
}