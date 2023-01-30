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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Menu;
using DevExpress.Utils.Menu;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
namespace SOURCE_FORM_DMNHANVIEN.Presentation
{
    public partial class frm_DMNHANVIEN_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_DMNHANVIEN_SH()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public bool _insert = false;
        public bool _edit = false;
        public string _sign = "NV";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        DevExpress.XtraTreeList.TreeList tl = new DevExpress.XtraTreeList.TreeList();
        TreeListNode SavedFocused;
        bool NeedRestoreFocused;
        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
            setClockButton(true);
            gct_list_C.Enabled = true;
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {      
                SaveGridControls();
            }
            else
            {
                loadUser();
                Function.clsFunction.TranslateForm(this, this.Name);                
                Load_Grid();
                loadGrid();
                loadGridLookupKV();
                Function.clsFunction.TranslateGridColumn(gv_list_C);
                Function.clsFunction.sysGrantUserByRole(bar_menu, this.Name);             
            }
            gct_list_C_Click(sender, e);
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
            loadReport();
        }  

        private void bbi_allow_insert_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                setClockButton(false);
                txt_MANV_20_I1.Focus();
                _insert = true;
                _edit = false;
                gct_list_C.Enabled = false;
                //menu.HidePopup();
                //frm_DMNHANVIEN_S frm=new frm_DMNHANVIEN_S();
                //frm._insert = true;
                //frm._sign = _sign;       
                //frm.statusForm = statusForm;
                //frm.passData = new frm_DMNHANVIEN_S.PassData(getValueUpdate);
                //frm.ShowDialog();
                clsFunction.Data_XoaText(this, txt_MANV_20_I1);
                LoadInfo();
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
                setClockButton(false);
                txt_MANV_20_I1.Focus();
                gct_list_C.Enabled = false;
                _insert = false;
                _edit = true;
                LoadInfo();
                //menu.HidePopup();
                //frm_DMNHANVIEN_S frm = new frm_DMNHANVIEN_S();
                //frm._insert = false;
                //frm.statusForm = statusForm;
                //frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "NVID").ToString();
                //frm.passData = new frm_DMNHANVIEN_S.PassData(getValueUpdate);
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo","Lỗi, vui lòng chọn dòng cần sửa");
            }
        }

        private void bbi_allow_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gct_list_C.Enabled = true;
            menu.HidePopup();
            Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "NVID", this, gv_list_C.Columns["NVID"], bbi_allow_delete.Name, "BoPhan", "BPID");
        }

        private void bbi_allow_print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menu.HidePopup();
            DataTable dtRP = new DataTable();
            dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='"+ this.Name +"' and iscurrent=1");
            if (dtRP.Rows.Count > 0)
            {
                XtraReport report = XtraReport.FromFile(Application.StartupPath +"\\Report" +"\\"+ dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
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
                save();
                bbi_allow_cancel.PerformClick();
                getValueUpdate(true);
                setClockButton(true);
                gv_list_C.FocusedRowHandle = 0;
                //menu.HidePopup();
                //if (!Directory.Exists(Application.StartupPath + "\\File"))
                //{
                //    Directory.CreateDirectory(Application.StartupPath + "\\File");
                //}
                //if (!File.Exists(Application.StartupPath + "\\File\\Template.xlt"))
                //{
                //    File.Create(Application.StartupPath + "\\File\\Template.xlt");
                //}
                //DataTable dt = new DataTable();
                //clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_list_C,clsFunction.transLateText( "Danh mục nhân viên"), this);
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

        private void bbi_allow_cancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            gct_list_C.Enabled = true;
            _insert = false;
            _edit = false;
            setClockButton(true);
            gv_list_C.FocusedRowHandle = 0;
        }

        #endregion

        #region Event

        private void bbi_status_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "NVID").ToString();
            frm.idform = this.Name;
            frm.userid = clsFunction._iduser;
            frm.ShowDialog();
        }

        private void tl_containmenu_S_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                string ID = "";
                bool status = false;
                ID = (tl_containmenu_S.FocusedNode.GetValue("BPID").ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("WITH temp(BPID, TENBP, BPCHA) as (    Select BPID, TENBP, BPCHA  From BoPhan   Where  ( BPCHA ='" + ID + "' or BPID='" + ID + "' )     Union All                Select b.BPID, b.TENBP, b.BPCHA               From temp as a, BoPhan as b      Where a.BPID = b.BPCHA and b.BPID <> b.BPCHA        )   Select distinct NVID,MANV,TENNV,CHUCVU,nv.BPID   From dmnhanvien nv inner join  temp on nv.BPID=temp.BPID");
                gct_list_C.DataSource = dt;
                gct_list_C_Click(sender, e);
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tl_containmenu_S_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void deleteNodeMenuItemClick(object sender, EventArgs e)
        {
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa bộ phận này không?"))
            {
                if (APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN where BPID='" + tl_containmenu_S.FocusedNode.GetValue("BPID").ToString() + "'").Rows.Count == 0)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete bophan where BPID='" + tl_containmenu_S.FocusedNode.GetValue("BPID").ToString() + "'");
                    DXMenuItem item = sender as DXMenuItem;
                    if (item == null) return;
                    TreeListNode node = item.Tag as TreeListNode;
                    if (node == null) return;
                    node.TreeList.DeleteNode(node);
                }
                else
                    clsFunction.MessageInfo("Thông báo", "Bộ phận này đã được sử dụng không thể xóa");
            }

        }
        private void insertNodeMenuItemClick(object sender, EventArgs e)
        {


        }

        private void editNodeMenuItemClick(object sender, EventArgs e)
        {


        }

        private void tl_containmenu_S_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Check if a node's indicator cell is clicked.
                TreeListHitInfo hitInfo = tl_containmenu_S.CalcHitInfo(e.Location);
                TreeListNode node = null;
                if (hitInfo.HitInfoType == HitInfoType.RowIndicator)
                {
                    node = hitInfo.Node;
                }
                // if (node == null) return;
                // Create a menu with a 'Delete Node' item.
                TreeListMenu menu = new TreeListMenu(sender as TreeList);
                DXMenuItem menuItem = new DXMenuItem("Xóa bộ phận", new EventHandler(deleteNodeMenuItemClick));
                menuItem.Tag = node;
                menu.Items.Add(menuItem);

                DXMenuItem menuItemThem = new DXMenuItem("Thêm bộ phận", new EventHandler(insertNodeMenuItemClick));
                menuItemThem.Tag = node;
                menu.Items.Add(menuItemThem);

                DXMenuItem menuItemSua = new DXMenuItem("Sửa bộ phận", new EventHandler(editNodeMenuItemClick));
                menuItemSua.Tag = node;
                menu.Items.Add(menuItemSua);

                // Show the menu.
                menu.Show(e.Location);
            }
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
            string[] caption_col = new string[] { "NVID", "Mã NV", "Tên NV", "Chức vụ", "Bộ phận" };
         
            // FieldName column
            string[] fieldname_col = new string[] { "NVID", "MANV", "TENNV", "CHUCVU", "BPID" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "200", "200"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "False", "True" };
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
            string[] sql_glue = new string[] {"select BPID, TENBP  from BoPhan "};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"TENBP"};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "BPID"};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2]{{"BPID","TENBP"}};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2]{{"BPID","TENBP"}};
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

        private void gct_list_C_DoubleClick(object sender, EventArgs e)
        {
            bbi_allow_edit.PerformClick();
        }

        private void gct_list_C_Click(object sender, EventArgs e)
        {
            try
            {
                _insert = false;
                _edit = false;
                if (gv_list_C.FocusedRowHandle>=0)
                loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"NVID").ToString());
                if (gv_list_C.FocusedRowHandle >= 0)
                {
                    txt_NVID_IK1.Text = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "NVID").ToString();
                    LoadInfo();
                }
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
                    loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "NVID").ToString());
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

        private void loadUser()
        {
            try
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                tl_containmenu_S.Nodes.Clear();
                string IDRoot = "", ID1 = "";
                string sRoot = "", sUser = "";
                tl_containmenu_S.BeginUnboundLoad();
                TreeListNode parentNode = null;
                tl_containmenu_S.DataSource = Function.clsFunction.Excute_Proc("load_BoPhan_S", new string[0, 0]);
                tl_containmenu_S.EndUnboundLoad();
                tl_containmenu_S.ExpandAll();
            }
            catch { }
        }
        private string getVaiTroByUser(string US)
        {
            string vaitro = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + US + "'");
            if (dt.Rows.Count > 0)
            {
                vaitro = dt.Rows[0][0].ToString();
            }
            return vaitro;
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
               // ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["BPID"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select BPID,TENBP from BoPhan");
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
                dts = APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN",this.Name+gv_list_C.Name,new string[0,0],"");                
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

        private void loadReport()
        {
            try
            {
                //ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                //frm.formname = this.Name;
                //frm.ShowDialog();
            }
            catch { }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e) {
              DataSet ds = new DataSet();
              DataTable dt = new DataTable();
              dt = APCoreProcess.APCoreProcess.Read("dmnhanvien");
              ds.Tables.Add(dt);
            //Instantiating your DataSet instance here
            //.....
            //Pass the created DataSet to your report:
            ((XtraReport)sender).DataSource = ds;
            ((XtraReport)sender).DataMember = ds.Tables[0].TableName;     
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert == true)
            {
                txt_NVID_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_NVID_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_NVID_IK1);
            }
            else
                if (_edit==false)
                {                
                    Function.clsFunction.Data_Binding1(this, txt_NVID_IK1);
                }
        }

        private void loadGridLookupKV()
        {
            string[] caption = new string[] { "Mã BP", "Tên BP" };
            string[] fieldname = new string[] { "BPID", "TENBP" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_BPID_I1, "select BPID, TENBP from BOPHAN", "TENBP", "BPID", caption, fieldname, this.Name, glue_BPID_I1.Width);
        }

        private void callForm()
        {
            SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S frm = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S();
            frm.passData = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S.PassData(getValue);
            frm.strpassData = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "KV";
            frm.ShowDialog();
        }

        private void getValue(bool value)
        {
            if (value == true)
            {

            }
        }

        private void getStringValue(string value)
        {
            if (value != "")
            {
                glue_BPID_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select BPID,TENBP from BOPHAN");
                glue_BPID_I1.EditValue = value;
            }
        }

        private void save()
        {
            if (!checkInput()) return;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_NVID_IK1.Name) + " = '" + txt_NVID_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_save.Caption), txt_NVID_IK1.Text, txt_TENNV_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

            
                {
                    LoadInfo();
                }
              
                //this.Hide();  
                dxEp_error_S.ClearErrors();
            }
            else
            {
                Function.clsFunction.Edit_data(this, this.Name, "NVID", txt_NVID_IK1.Text);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_NVID_IK1.Name) + " = '" + txt_NVID_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_save.Caption), txt_NVID_IK1.Text, txt_TENNV_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                
            }
        }
        private bool checkInput()
        {
            if (txt_MANV_20_I1.Text == "")
            {
                txt_MANV_20_I1.Focus();
                dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (txt_TENNV_100_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_TENNV_100_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_TENNV_100_I2.Focus();
                return false;
            }
            if (glue_BPID_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_BPID_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_BPID_I1.Focus();
                return false;
            }


            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN where MANV='" + txt_MANV_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_MANV_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select MANV from DMNHANVIEN where NVID='" + txt_NVID_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN where MANV='" + txt_MANV_20_I1.Text + "' and MANV <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_MANV_20_I1.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
        
        private void setClockButton(bool clock)
        {
            bbi_allow_cancel.Enabled = !clock;
            bbi_allow_edit.Enabled = clock;
            bbi_allow_insert_S.Enabled = clock;
            bbi_allow_save.Enabled = !clock;
            bbi_allow_delete.Enabled = clock;
            bbi_exit_allow_access.Enabled = clock;
        }
            
        #endregion                   

    }
}