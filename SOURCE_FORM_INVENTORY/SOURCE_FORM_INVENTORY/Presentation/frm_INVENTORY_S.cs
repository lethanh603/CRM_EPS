﻿using System;
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
namespace SOURCE_FORM_INVENTORY.Presentation
{
    public partial class frm_INVENTORY_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_INVENTORY_S()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "PX";
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
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);
       
                Init();
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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_list_C,clsFunction.transLateText( "TỒN KHO"), this);
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

        private void bbi_allow_insert_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
            
          
                    Init();
                
            }
            catch { }
        }



        private void bbi_allow_print_Search_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                loadGrid();
            }
            catch { }
        }
        
        
        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idtable").ToString();
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
            string[] column_summary = new string[] { "quantity", "amountpur" };
            // Caption column
            string[] caption_col = new string[] { "Mã hàng", "Ký hiệu", "Tên hàng", "Số lượng", "ĐVT", "TG Nhập", "Nhóm hàng", "Kho hàng", "Còn QL" };
         
            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idcommodity_S", "col_sign_S", "col_commodity_S", "col_quantity_S", "col_idunit_S", "col_amountpur_S", "col_idgroup_S", "col_idwarehouse_S", "col_status_S" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "CalcEditColumn", "GridLookupEditColumn", "CalcEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100","100", "200", "100", "60", "100", "150", "150", "100" };
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
            string[] sql_glue = new string[] { "select idunit, unit  from dmunit where status=1", "select idgroup, groupname  from dmgroup where status=1", "select idwarehouse, warehouse  from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"unit","groupname","warehouse"};
            
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idunit", "idgroup", "idwarehouse" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "Mã ĐVT", "ĐVT" }, { "Mã nhóm", "Nhóm" }, { "Mã kho", "Kho" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idunit", "unit" }, { "idgroup", "groupname" }, { "idwarehouse", "warehouse" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid,gv_list_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_list_C);
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
            gv_list_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='"+ gv_list_C.Name +"'");
            
            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_list_C, read_Only, hien_Nav,
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
                gv_list_C.OptionsBehavior.Editable = false;
                gv_list_C.OptionsView.ColumnAutoWidth = true;
                gv_list_C.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
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
                if (gv_list_C.FocusedRowHandle>=0)
                    loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
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
                if (gv_list_C.FocusedRowHandle >= 0)
                    loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "col_iddetail_IK1").ToString());
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
                if (gv_list_C.FocusedRowHandle>=0)
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


        private void gv_EXPORTDETAIL_C_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
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
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idfloors"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfloors,floors from dmfloors where status=1");
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
                dts = APCoreProcess.APCoreProcess.Read("select ctn.idcommodity, mh.sign,mh.commodity, ctn.idunit,ctn.idwarehouse, mh.idgroup, sum(ctn.quantity) - isnull((select SUM( tk.quantity)  from STOCKIO tk inner join PURCHASEDETAIL ctn1 on tk.iddetailpur=ctn1.iddetail where ctn1.idcommodity=ctn.idcommodity),0) as quantity, mh.status, sum(ctn.quantity* ctn.price) -isnull((select SUM( tk.quantity*ctn1.price)  from STOCKIO tk inner join PURCHASEDETAIL ctn1 on tk.iddetailpur=ctn1.iddetail where ctn1.idcommodity=ctn.idcommodity),0) as amountpur from dmcommodity mh inner join purchasedetail ctn on ctn.idcommodity=mh.idcommodity group by ctn.idcommodity, mh.sign,mh.commodity, ctn.idunit,ctn.idwarehouse,mh.idgroup,mh.status,mh.quantity having mh.quantity=1", this.Name + gv_list_C.Name, new string[0, 0], "");
            }
            gct_list_C.DataSource = dts;
        }

        private void loadStatus(string ID)
        {
            try
            {
                string status = "";
                DataTable dt = new DataTable();
                dt = Function.clsFunction.Excute_Proc("sysTraceByExpression", new string[5, 2] { { "userid", clsFunction._iduser }, { "idform", "%"+clsFunction.getNameControls( this.Name)+"%" }, { "object", ID }, { "fromdate", DateTime.Now.AddYears(-1).ToShortDateString() }, { "todate", DateTime.Now.ToShortDateString() } });
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
           

        private void Init()
        {
            try
            {
   
               
            }
            catch 
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
        }


        #endregion                   

        

    }
}