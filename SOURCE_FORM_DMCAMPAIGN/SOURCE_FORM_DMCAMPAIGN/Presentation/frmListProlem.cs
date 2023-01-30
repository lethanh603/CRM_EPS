using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Function;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;

////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frmListProlem : DevExpress.XtraEditors.XtraForm
    {
        public frmListProlem()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "PL";    
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

        private void frmListProlem_Load(object sender, EventArgs e)
        {
           // statusForm = true;
            Function.clsFunction._keylience = true;
           
            if (statusForm == true)
            {
                SaveGridControls();
                SaveGridControlsContact();            
                clsFunction.Save_sysControl(this, this);           
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);               
                Load_Grid();// grid custome
                Load_Grid_Contact();
                gv_listProlem_C.Columns["check"].OptionsColumn.AllowEdit = true;
                gv_listProlem_C.Columns[1].OptionsColumn.AllowEdit = true;
                loadGrid();
                Function.clsFunction.TranslateGridColumn(gv_listProlem_C);
                loadEmp();
                gv_listProlem_C.FocusedRowHandle = 0;
                gv_listcustomer_C_Click(sender,e);
                ControlDev.FormatControls.setContainsFilter(gv_listProlem_C);
                // Kiem tra quyen vao chuc nang phan bo set lại cho nut phan bo
                if (APCoreProcess.APCoreProcess.Read("select idemp from EMPLOYEES where idemp = '" + clsFunction.GetIDEMPByUser() + "' and ismanager=1").Rows.Count > 0)
                {
                    
                }
                else
                {
                 
                }
                if (checkAdmin())
                {
                    
                }
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

        private void frmListProlem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
            }
            if (e.KeyCode == Keys.F2)
            {
            }
     
            if (e.KeyCode == Keys.F9)
            {
                bbi_allow_access_S.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
            }
        }

        #endregion

        #region ButtonEvent
    
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
            string[] caption_col = new string[] { "Chọn", "ID", "Ký hiệu", "Khách hàng", "Nhân viên", "Nhóm", "Tỉnh thành","Khu vực","Vùng","Loại KH", "Lĩnh vực", "Quy mô", "Địa chỉ", "Mã số thuế", "Điện thoại", "Fax", "Di động", "Website", "Email", "Nick", "Tài khoản", "Ngân hàng", "Trụ sở", "CMND - Hộ chiếu", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "check", "idcustomer", "sign", "customer","idemp", "idgroup", "idprovince", "id", "idregion", "idtype", "idfields", "idscale", "address", "tax", "tel", "fax", "mobile", "website", "email", "nick", "atm", "bank", "station", "passport", "note", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "CheckColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "80", "80", "200", "200", "100", "150", "150", "200", "150", "150", "100", "200", "80", "80", "80", "80", "100", "100", "100", "100", "100", "100", "100", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idemp, staffname  from employees where status=1", "select idprovince, province  from dmprovince where status=1", "select idregion, region  from dmregion where status=1", "select id, area from dmarea where status=1", "select idtype, customertype from DMCUSTOMERTYPE where status=1", "select idfields, fieldname from DMFIELDS where status=1", "select idscale, scale from DMSCALE where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "staffname","province","area","region", "customertype", "fieldname", "scale" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idemp","idprovince", "id","idtype", "idfields", "idscale" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[7, 2] { { "Mã NV", "Nhân viên" }, { "Mã", "Tỉnh thành" }, { "Mã", "Khu vực" }, { "Mã vùng", "Vùng" }, { "Mã", "Loại khách hàng" }, { "Mã ", "Lĩnh vực" }, { "Mã", "Quy mô" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[7, 2] { { "idemp", "province" }, { "idprovince", "province" }, { "id", "area" }, { "idregion", "region" }, { "idtype", "customertype" }, { "idfields", "fieldname" }, { "idscale", "scale" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_listProlem_C.Name);
        }

        private void SaveGridControlsContact()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] {  "ID", "Ký hiệu", "Khách hàng", "Người liên hệ", "Điện thoại", "Chức vụ","Địa chỉ", "Email",  "Ghi chú"};

            // FieldName column
            string[] fieldname_col = new string[] { "idcontact", "sign","idcustomer", "contactname", "tel", "idposition", "address", "email", "note"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "80","200", "200", "100", "100", "200", "100", "200" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idcustomer, customer from dmcustomers where status=1 ","select idposition, position  from dmposition where status=1"};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"customer", "position" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer","idposition" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "Mã", "Khách hàng" }, { "Mã", "Chức vụ" }};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "idcustomer", "customer" }, { "idposition", "position" } };
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

        
        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;
            gv_listProlem_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_listProlem_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_listProlem_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_listProlem_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_listProlem_C,
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
                gv_listProlem_C.OptionsBehavior.Editable = true;
                arrFieldName = dt.Rows[0]["field_name"].ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                dts = APCoreProcess.APCoreProcess.Read("SELECT     C.idscale, C.idfields, C.idprovince, C.idtype,C.idregion, C.idcustomer, C.note, C.sign, C.nick, C.passport, C.bank, C.atm, C.website, C.email, C.mobile, C.tel, C.fax, C.station, C.tax, C.address, C.surrogate, C.customer,  CASE WHEN C.idgroup = 0 THEN N'Công ty' when C.idgroup=1 then N'Đại lý'  when C.idgroup=2 then N'Khách lẻ' END AS idgroup, C.status, C.date2, C.date1, C.userid2, C.userid1, P.id, E.idemp FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer INNER JOIN  dbo.DMPROVINCE AS P ON C.idprovince = P.idprovince INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' ORDER BY C.idcustomer ");
            }
            dts.Columns.Add("check", typeof(Boolean));
            gct_listProlem_C.DataSource = dts;
            if (dts.Rows.Count > 0)
            {
                gv_listProlem_C.FocusedRowHandle = 0;
            }
        }       

        private void loadGridContact()
        {
            if (gv_listProlem_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from CUSCONTACT  WHERE idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_contact_C.DataSource = dts;
            }
        }

  
       

        private void Load_Grid_Contact()
        {
            string text = Function.clsFunction.langgues;
            gv_contact_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_listProlem_C.OptionsView.ShowAutoFilterRow = true;
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
     
                gv_contact_C.OptionsBehavior.Editable = true;
             
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void gv_EXPORTDETAIL_C_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.Name == "idstatus")
            {
                e.Column.AppearanceCell.Options.UseBackColor = true;
                string idstatus = View.GetRowCellValue(e.RowHandle, "idstatus").ToString();
                if (idstatus == "ST000001")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.White;
                }
                if (idstatus == "ST000002")
                {
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (idstatus == "ST000003")
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LimeGreen;
                }
            }
        }

        private void gv_listcustomer_C_Click(object sender, EventArgs e)
        {
           
        }

        private void gv_listcustomer_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_listProlem_C.FocusedRowHandle >= 0)
                {
                    
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void gv_listcustomer_C_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_listProlem_C.FocusedRowHandle >= 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_menu_C, menu, gv_listProlem_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress);
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
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='"+gv_listProlem_C.Name+"'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_listProlem_C.Name + "'");
                            Load_Grid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:"+ex.Message);
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

        
        private void gv_contact_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_contact_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_contact_C, menu, gv_contact_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_contact_C_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
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

        private int rowfocus = -1;
        
        
        #endregion

        #region Event
        //customer
        private void bbi_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if(clsFunction.MessageDelete("Thông báo","Bạn có muốn xóa khách hàng này không")==true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from QUOTATION WHERE idcustomer ='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "'").Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Bạn có không thể xóa khách hàng này");
                    return;
                }
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from DMCUSTOMERS where idcustomer='"+ gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle,"idcustomer").ToString() +"'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from EMPCUS where idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSNOTE where idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSCONTACT where idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from PLANCRM where idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSPLAN where idcustomer='" + gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                gv_listProlem_C.DeleteRow(gv_listProlem_C.FocusedRowHandle);
            }
        }

        private void bbi_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_listProlem_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S frm = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S();
                    frm._insert = false;
                    frm.statusForm = statusForm;
                    frm.ID = gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString();
                    frm.passData = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S.PassData(getValueUpdate);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error" + ex.Message);
            }
        }

        private void bbi_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S frm = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S();
                frm._insert = true;
                frm._sign = _sign;
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S.PassData(getValueInsert);
                frm.ShowDialog();
           
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }
    
        
        
        private void bbi_allow_access_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }       

        private void getValueContact(bool value)
        {
            if (value == true)
            {
                loadGridContact();
            }
        }

       
        // CONTACT

        private void bbi_allow_insert_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CUSCONTACT_S frm = new frm_CUSCONTACT_S();
                frm._insert = true;
                frm._sign = _sign;
                frm.statusForm = statusForm;
                frm.lbl_idcustomer_I1.Text = gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString();
                frm.passData = new frm_CUSCONTACT_S.PassData(getValueContact);
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
            if (Function.clsFunction.MessageDelete("Thông báo","Bạn có chắc muốn xóa mẫu tin này không?"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSCONTACT where idcontact='" + gv_contact_C.GetRowCellValue(gv_contact_C.FocusedRowHandle, "idcontact").ToString() + "' ");
                gv_contact_C.DeleteRow(gv_contact_C.FocusedRowHandle);
            }
        }

        private void bbi_allow_edit_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_contact_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    frm_CUSCONTACT_S frm = new frm_CUSCONTACT_S();
                    frm._insert = false;
                    frm.statusForm = statusForm;
                    frm.ID = gv_contact_C.GetRowCellValue(gv_contact_C.FocusedRowHandle, "idcontact").ToString();
                    frm.lbl_idcustomer_I1.Text = gv_listProlem_C.GetRowCellValue(gv_listProlem_C.FocusedRowHandle, "idcustomer").ToString();
                    frm.passData = new frm_CUSCONTACT_S.PassData(getValueContact);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error" + ex.Message);
            }
        }


        
        private void bbi_allow_import_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            return;
            menu.HidePopup();
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();
            frm.sDauma = _sign;
            frm.formNamePre = this.Name;
            frm.gridNamePre = gv_listProlem_C.Name;
            frm.arrColumnCaption = arrCaption.Split('/');
            frm.arrColumnFieldName = arrFieldName.Split('/');
            frm.tbName = clsFunction.getNameControls(this.Name);
            frm.ShowDialog();
            APCoreProcess.APCoreProcess.ExcuteSQL("insert into EMPCUS (idemp, idcustomer, status, date1, userid1) (select '" + clsFunction.GetIDEMPByUser() + "' as idemp, idcustomer,'True' as status, getdate() as date1,'" + clsFunction._iduser + "' as userid1 from DMCUSTOMERS where userid1 IS NULL );update DMCUSTOMERS set userid1='" + clsFunction._iduser + "', date1 =getdate() where userid1 IS NULL");
            loadGrid();
        }

        private void bbi_emp_S_EditValueChanged(object sender, EventArgs e)
        {
            gv_listProlem_C.Columns["idemp"].FilterInfo = new ColumnFilterInfo("[idemp] LIKE '%" + bbi_emp_S.EditValue.ToString() + "%'");
        }


        private void bbi_allow_insert_campaign_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                SOURCE_FORM_QUOTATION.Presentation.frm_DMCAMPAIGN_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_DMCAMPAIGN_S();
                frm._insert = true;
                frm._sign = _sign;
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_QUOTATION.Presentation.frm_DMCAMPAIGN_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
            }
        }

       

        private void bbi_refresh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmListProlem_Load(sender, e);
        }

        private void bbi_allow_access_phanbo_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_ShareCus frm = new frm_ShareCus();
                frm.passData = new frm_ShareCus.PassData(getValueInsert);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }  

        #endregion
        
        #region Methods

        private void getValueUpdate(bool value)
        {
            if (value == true)
            {
                int index = gv_listProlem_C.FocusedRowHandle;
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idprovince"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idprovince,province from dmprovince where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idtype"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idtype,customertype from dmcustomertype where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idfields"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfields,fieldname from dmfields where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idscale"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idscale,scale from dmscale where status=1");
                gv_listProlem_C.FocusedRowHandle = index;
            }
        }

        private void getValueInsert(bool value)
        {
            if (value == true)
            {
                int index = gv_listProlem_C.FocusedRowHandle;
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idprovince"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idprovince,province from dmprovince where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idtype"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idtype,customertype from dmcustomertype where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idfields"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfields,fieldname from dmfields where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listProlem_C.Columns["idscale"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idscale,scale from dmscale where status=1");
                gv_listProlem_C.FocusedRowHandle = gv_listProlem_C.RowCount-1;
            }
        }

       

        private void loadEmp()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select idemp,staffname from employees where status=1");
                DataRow dr = dt.NewRow();
                dr["idemp"] = "";
                dr["staffname"] = "ALL";
                dt.Rows.Add(dr);
                DataView dv = dt.DefaultView;
                dv.Sort = "idemp";
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(bbi_emp_S.Edit)).DataSource = dv.ToTable();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(bbi_emp_S.Edit)).ValueMember = "idemp";
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(bbi_emp_S.Edit)).DisplayMember = "staffname";
                if (dv.ToTable().Rows.Count > 0)
                {
                    bbi_emp_S.EditValue = dv.ToTable().Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }
        #endregion 

        private void barEditItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barEditItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}