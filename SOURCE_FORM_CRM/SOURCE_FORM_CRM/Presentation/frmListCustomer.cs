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
using System.IO;

////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frmListCustomer : DevExpress.XtraEditors.XtraForm
    {
        public frmListCustomer()
        {
            InitializeComponent();
        }

        #region Var

        public bool statusForm = false;
        public string _sign = "BD";    
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

        private void frmListCustomer_Load(object sender, EventArgs e)
        {
           //statusForm = true;
            Function.clsFunction._keylience = true;
           
            if (statusForm == true)
            {
                SaveGridControlsContactRent();
                SaveGridControlsPlan();
               
                //SaveGridControlsDevice();  
                //SaveGridControlsBinhdien();
                //SaveGridControlsVoxe();                              
                //SaveGridControls();
                //SaveGridControls_Campaign();
                //SaveGridControlsContact();
                //SaveGridControlsCusnote();
               
               
                
                //SaveGridControls_Quotation();             
                // clsFunction.Save_sysControl(this, this);           
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);               
                Load_Grid();// grid customer
                Load_Grid_Plan();
                Load_Grid_Contact();
                Load_Grid_Cusnote();
                Load_Grid_Survey();
                //Load_Grid_Campaign();
                Load_Grid_Quotation();
                Load_Grid_Device();
                Load_Grid_Voxe();
                Load_Grid_ContactRent();
                Load_Grid_Binhdien();
                gv_listcustomer_C.Columns["check"].OptionsColumn.AllowEdit = true;
                gv_listcustomer_C.Columns[1].OptionsColumn.AllowEdit = true;
                loadGrid();
                loadGridCampaign();
                Function.clsFunction.TranslateGridColumn(gv_listcustomer_C);
                loadArea();
                loadEmp();
                loadCustomerType();
                gv_listcustomer_C.FocusedRowHandle = 0;
                gv_listcustomer_C_Click(sender,e);
                bbi_check_S.EditValue = false ;
                ControlDev.FormatControls.setContainsFilter(gv_listcustomer_C);
                // Kiem tra quyen vao chuc nang phan bo set lại cho nut phan bo
                if (APCoreProcess.APCoreProcess.Read("select idemp from EMPLOYEES where idemp = '" + clsFunction.GetIDEMPByUser() + "' and ismanager=1").Rows.Count > 0)
                {
                    bbi_allow_access_phanbo_S.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    bbi_allow_access_phanbo_S.Visibility = BarItemVisibility.Never;
                }
                if (checkAdmin())
                {
                    xtap_cusdup_C.PageVisible = true;
                    loadCusDup();
                }
                else
                {
                    xtap_cusdup_C.PageVisible = false;
                    tap_quotation_C.PageVisible = false;
                }

            }
            bar7.Visible = false;
            bar6.Visible = false;
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

        private void frmListCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                bbi_allow_insert_S.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                bbi_allow_edit_S.PerformClick();
            }
     
            if (e.KeyCode == Keys.F9)
            {
                bbi_allow_access_S.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                bbi_allow_access_phanbo_S.PerformClick();
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_listcustomer_C.Name);
        }

        private void SaveGridControlsContact()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] {  "ID", "Ký hiệu", "Người liên hệ", "Điện thoại", "Chức vụ","Địa chỉ", "Email",  "Ghi chú"};

            // FieldName column
            string[] fieldname_col = new string[] { "idcontact", "sign","contactname", "tel", "idposition", "address", "email", "note"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "80", "200", "100", "100", "200", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] {"select idposition, position  from dmposition where status=1"};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"position" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {"idposition" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã", "Chức vụ" }};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] {{ "idposition", "position" } };
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

        private void SaveGridControlsPlan()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã KH", "Ký hiệu", "Ngày liên hệ", "Người liên hệ",  "Nội dung" ,"Tình trạng", "Chiến dịch", "Nhân viên liên hệ", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idplan", "sign", "datecontact", "idcontact", "description", "idstatus", "Chiến dịch", "idemp", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "DateColumn", "GridLookupEditColumn", "MemoColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "80", "80", "200", "300", "200", "200", "150", "200" };
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
            // so cot

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idcontact, (contactname + ' - ' + tel) as contact from cuscontact", "select idstatus, statusname from DMSTATUS ", "select idcampaign, campaign  from dmcampaign where status=1", "select idemp, staffname from employees" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"contact","statusname", "campaign", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcontact", "idstatus", "idcampaign", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[4, 2] { { "Mã NLH", "Người liên hệ" }, { "Mã", "Tình trạng" }, { "Mã CD", "Chiến dịch" }, { "Mã NV", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[4, 2] { { "idcontact", "contact" }, { "idstatus", "statusname" }, { "idcampaign", "campaign" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_plan_C.Name);
        }

        private void SaveGridControlsDevice()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã TB", "Hiệu(Brand)", "Model", "Số lượng", "Động cơ", "HT Phụ trợ kèm theo", "Số khung - Chiều cao", "Năm SX", "Xuất xứ",  "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "iddevice", "brand", "model", "quantity", "dongco", "hethongphutro", "sokhung", "namsx", "xuatxu",  "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "100", "120", "100", "180", "180", "180", "80","100",  "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False"};
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True"};
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
            string[] sql_glue = new string[] {  };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0,0] { };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0] {};
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_deviceinfo_C.Name);
        }

        private void SaveGridControlsBinhdien()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã TB", "Model xe", "Hiệu xe", "Model bình", "Hiệu bình", "Số lượng", "Thông số", "Kích thước",  "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idbinh", "modelxe", "iddevice", "modelbinh", "idbrandtype", "quantity", "thongso", "kichthuoc",  "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "GridLookupEditColumn", "SpinEditColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "150", "150", "150", "150", "100", "180", "100", "200" };
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
            string[] sql_glue = new string[] { "select idbrand as iddevice, brand from dmbrand where idbrandtype='BT000001' ", "select idbrand as idbrand, brand from dmbrand where idbrandtype='BT000003' " };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"brand", "brand" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {"iddevice","idbrand" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] {{"Mã hiệu","Hiệu xe"}, {"Mã hiệu","Mã Bình"} };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "iddevice", "brand" }, { "idbrand", "brand" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_binhdien_C.Name);
        }

        private void SaveGridControlsVoxe()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã VX", "Hiệu(Brand)","Thông số","Số lượng", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "iddevice", "brand", "thongso", "quantity",  "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80", "200", "220","100", "300"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0] { };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0] { };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_voxe_C.Name);
        }

        private void SaveGridControlsCusnote()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã","Khách hàng", "Ngày", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idcusnote","idcustomer", "datenote", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "DateColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "80","200", "80", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã", "Khách hàng" }};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "idcustomer", "customer" }};
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_cusnote_C.Name);
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;
            gv_listcustomer_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_listcustomer_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_listcustomer_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_listcustomer_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_listcustomer_C,
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
                gv_listcustomer_C.OptionsBehavior.Editable = true;
                arrFieldName = dt.Rows[0]["field_name"].ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Plan()
        {
            string text = Function.clsFunction.langgues;
            gv_plan_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_plan_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_plan_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_plan_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_plan_C,
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
              
                gv_plan_C.OptionsBehavior.Editable = true;
              
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Device()
        {
            string text = Function.clsFunction.langgues;
            gv_deviceinfo_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_deviceinfo_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_deviceinfo_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_deviceinfo_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_deviceinfo_C,
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

                gv_deviceinfo_C.OptionsBehavior.Editable = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Voxe()
        {
            string text = Function.clsFunction.langgues;
            gv_voxe_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_voxe_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_voxe_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_voxe_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_voxe_C,
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

                gv_voxe_C.OptionsBehavior.Editable = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Grid_Binhdien()
        {
            string text = Function.clsFunction.langgues;
            gv_binhdien_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_binhdien_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_binhdien_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_binhdien_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gc_binhdien_C,
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

                gv_binhdien_C.OptionsBehavior.Editable = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControlsContactRent()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "ID Thuê xe", "Tên người liên hệ", "Chức vụ", "Email", "Điện thoại", "Status", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idcontact", "idthuexe", "contactname", "idposition", "email", "tel", "status", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "CheckColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "150", "150", "100", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True" };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_contactrent_C.Name);
        }

        private void Load_Grid_ContactRent()
        {
            string text = Function.clsFunction.langgues;
            gv_contactrent_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_contactrent_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_contactrent_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_contactrent_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_contactRent_C,
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
                    string str = "";
                    if (txt_search_S.EditValue != null && txt_search_S.EditValue.ToString() != "")
                    {
                        str = " where C.idcustomer in (select idcustomer from CUSCONTACT where tel like '%" + txt_search_S.EditValue + "%' or email like '%" + txt_search_S.EditValue + "%' or contactname like '%" + txt_search_S.EditValue + "%')";
                    }
                    dts = APCoreProcess.APCoreProcess.Read("SELECT     C.idscale, C.idfields, C.idprovince, C.idtype,C.idregion, C.idcustomer, C.note, C.sign, C.nick, C.passport, C.bank, C.atm, C.website, C.email, C.mobile, C.tel, C.fax, C.station, C.tax, C.address, C.surrogate, C.customer,  CASE WHEN C.idgroup = 0 THEN N'Công ty' when C.idgroup=1 then N'Đại lý'  when C.idgroup=2 then N'Khách lẻ' END AS idgroup, C.status, C.date2, C.date1, C.userid2, C.userid1,  E.idemp, C.idarea as id FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer INNER JOIN  dbo.DMPROVINCE AS P ON C.idprovince = P.idprovince INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP  AND charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 AND E.status='True' " + str + " ORDER BY C.idcustomer ");
                }
                dts.Columns.Add("check", typeof(Boolean));
                gct_listcustomer_C.DataSource = dts;
                if (dts.Rows.Count > 0)
                {
                    gv_listcustomer_C.FocusedRowHandle = 0;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi "+ ex.Message);
            }
        }

        private void loadGridPlan()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from PLANCRM  WHERE idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_plan_C.DataSource = dts;
            }
        }

        private void loadGridRentContact()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select R.* from CUSCONTACTRENT R inner join DEVICEFORRENT D on R.idthuexe = D.idthuexe  where idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                gct_contactRent_C.DataSource = dt;
            }
        }

        private void loadGridDevice()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select D.*, B.brand from DEVICEINFO D INNER JOIN DMBRAND B on D.idbrand = B.idbrand  WHERE D.idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_deviceinfo_C.DataSource = dts;
            }
        }

        private void loadGridBinhDien()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from BINHDIENINFO  WHERE idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gc_binhdien_C.DataSource = dts;
            }
        }

        private void loadGridVoxe()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from VOXEINFO  WHERE idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_voxe_C.DataSource = dts;
            }
        }

        private void loadGridContact()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from CUSCONTACT  WHERE idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_contact_C.DataSource = dts;
            }
        }

        private void loadGridCusnote()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    dts = APCoreProcess.APCoreProcess.Read("select * from CUSNOTE  WHERE idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'");
                }

                gct_cusnote_C.DataSource = dts;
            }
        }

        private void loadGridQuotation()
        {
            if (gv_listcustomer_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    string str = "";          
              
                    str += " and  QO.idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle,"idcustomer").ToString() + "'"; 
                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT     QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status, QO.idcurrency, QO.exchangerate, QO.idstatusquotation, SUM(dt.quantity) AS quantity, SUM(dt.amountvat) AS amountvat, SUM(dt.total) AS total FROM dbo.QUOTATION AS QO INNER JOIN dbo.QUOTATIONDETAIL AS dt ON QO.idexport = dt.idexport WHERE     (QO.isdelete = 0) "+ str +" GROUP BY QO.vat, QO.dateimport, QO.idcustomer, QO.IDEMP, QO.idexport, QO.note, QO.discount, QO.amountdiscount, QO.surcharge, QO.amountsurcharge, QO.status,  QO.idcurrency, QO.exchangerate, QO.idstatusquotation  ");
                }

                gct_quotation_C.DataSource = dts;
            }
        }

        private void loadGridCampaign()
        {
            if (Function.clsFunction._pre == true)
            {
                dts = Function.clsFunction.dtTrace;
            }
            else
            {
                dts = APCoreProcess.APCoreProcess.Read("SELECT     CP.todate, CP.fromdate, CP.status, CP.idcampaign, CP.note, CP.sign, CP.campaign, CP.frequency, CP.finishdate, CP.remind, CP.dateremind, CP.idpriority,  CP.idemp FROM         dbo.DMCAMPAIGN AS CP INNER JOIN    dbo.CAMPAIGNCUS AS CS ON CP.idcampaign = CS.idcampaign GROUP BY CP.todate, CP.fromdate, CP.idcampaign, CP.note, CP.sign, CP.campaign, CP.frequency, CP.finishdate, CP.dateremind, CP.idpriority, CP.idemp, CP.status, CP.remind,   CS.idcustomer HAVING      (CS.idcustomer = '"+ gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle,"idcustomer") +"') ");
            }
            gct_campaign_C.DataSource = dts;
        }

        private void Load_Grid_Cusnote()
        {
            string text = Function.clsFunction.langgues;
            gv_cusnote_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_cusnote_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_cusnote_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_cusnote_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_cusnote_C,
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
               
                gv_cusnote_C.OptionsBehavior.Editable = true;
        
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            gv_listcustomer_C.OptionsView.ShowAutoFilterRow = true;
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

        // QUOTATION

        private void SaveGridControls_Quotation()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã phiếu", "Khách hàng", "Nhân viên", "Ngày nhập", "Tình trạng", "Số lượng", "Thành tiền", "Tiền tệ", "Tỷ giá", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idexport_S", "col_idcustomer_S", "col_IDEMP_S", "col_dateimport_S", "col_idstatusquotation_S", "col_quantity_S", "col_total_S", "col_idcurrency_S", "col_exchangerate_S", "col_note_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "GridLookupEditColumn", "SpinEditColumn", "SpinEditColumn", "GridLookupEditColumn", "SpinEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "250", "150", "80", "200", "80", "100", "80", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation", "select idcurrency, currency from dmcurrency where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "status", "currency" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatus", "idcurrency" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[4, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Tiền tệ" } };

            string[,] fieldname_glue_col = new string[4, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstus", "statusname" }, { "idcurrency", "currency" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_quotation_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

        private void Load_Grid_Quotation()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho", "Tiền tệ" };
            bool show_footer = true;
            // show filterRow
            gv_quotation_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_quotation_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_quotation_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_quotation_C,
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
                gv_quotation_C.OptionsBehavior.Editable = true;
                gv_quotation_C.OptionsView.ColumnAutoWidth = true;
                gv_quotation_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //CAMPAIGN

        private void SaveGridControls_Campaign()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã CD", "Ký hiệu", "Chiến dịch", "Từ ngày", "Đến ngày", "Tần số", "Nhắc nhỡ", "Ngày nhắc", "Ngày kết thúc", "Ưu tiên", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "idcampaign", "sign", "campaign", "fromdate", "todate", "frequency", "remind", "dateremind", "finishdate", "idpriority", "note", "status" };

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
            string[] fieldname_glue = new string[] { "idpriority" };
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
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_campaign_C.Name);
        }

        private void Load_Grid_Campaign()
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
            gv_campaign_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_campaign_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_campaign_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_campaign_C,
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
            loadGridDevice();
            loadGridVoxe();
            loadGridBinhDien();
            
        }

        private void gv_listcustomer_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_listcustomer_C.FocusedRowHandle >= 0)
                {
                    txt_address_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "address").ToString();
                    txt_atm_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "atm").ToString();
                    txt_bank_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "bank").ToString();
                    txt_customer_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "customer").ToString();
                    txt_email_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "email").ToString();
                    txt_passport_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "passport").ToString();
                    txt_surrogate_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "surrogate").ToString();
                    txt_tax_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "tax").ToString();
                    txt_tel_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "tel").ToString();
                    txt_website_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "website").ToString();
                    txt_fax_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "fax").ToString();
                    txt_station_S.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "station").ToString();
                    txt_national_S.Text = gv_listcustomer_C.GetRowCellDisplayText(gv_listcustomer_C.FocusedRowHandle, "id").ToString();
                    DataTable dtCus = new DataTable();
                    dtCus = APCoreProcess.APCoreProcess.Read("SELECT  CT.customertype, E.StaffName FROM  dbo.DMCUSTOMERS AS C INNER JOIN   dbo.DMCUSTOMERTYPE AS CT ON C.idtype = CT.idtype INNER JOIN  dbo.EMPCUS AS EC ON C.idcustomer = EC.idcustomer INNER JOIN   dbo.EMPLOYEES AS E ON EC.idemp = E.IDEMP GROUP BY CT.customertype, EC.idemp, EC.idcustomer, E.StaffName HAVING  (EC.idcustomer = '" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "') ORDER BY EC.idemp DESC");
                    if (dtCus.Rows.Count > 0)
                    {
                        txt_staffname_S.Text = dtCus.Rows[0]["staffname"].ToString();
                        txt_customertype_S.Text = dtCus.Rows[0]["customertype"].ToString();
                    }

                    loadGridPlan();
                    loadGridContact();
                    loadGridCusnote();
                    loadGridQuotation();
                    loadGridCampaign();
                    loadGridRentContact();
                    loadGridSurvey();

                    ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_plan_C.Columns["idcontact"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idcontact, (contactname + ' - ' + tel) as contact from cuscontact where idcustomer='"+ gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() +"' ");
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
                if (gv_listcustomer_C.FocusedRowHandle >= 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_menu_C, menu, gv_listcustomer_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void bar_device_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_deviceinfo_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_deviceinfo_C.Name + "'");
                            // Load_Grid_Plan();
                            Load_Grid_Device();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void bar_voxe_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_voxe_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_voxe_C.Name + "'");
                            
                            //Load_Grid_Device();
                            Load_Grid_Voxe();
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

        private void bar_cusnote_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gct_cusnote_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_cusnote_C.Name + "'");
                            Load_Grid_Cusnote();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void bar_campaign_C_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gct_campaign_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gct_campaign_C.Name + "'");
                            Load_Grid_Campaign();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
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
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_listcustomer_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_listcustomer_C.Name + "'");
                            Load_Grid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void Manager_Plan_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gct_plan_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gct_plan_C.Name + "'");
                            Load_Grid_Plan();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void gv_plan_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_plan_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_plan_C, menu, gv_plan_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_Plan_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch(Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: "+ex.Message);
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

        private void gv_cusnote_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_cusnote_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_cusnote_C, menu, gv_cusnote_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_cusnote_C_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
            }
        }

        private void gv_campaign_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_campaign_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_campaign_C, menu, gv_campaign_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_campaign_C_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
            }
        }

        private void gv_deviceinfo_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_deviceinfo_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_device_C, menu, gv_deviceinfo_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_device_C_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
            }
        }



        private void gv_voxeinfo_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_voxe_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_voxe_C, menu, gv_voxe_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_voxe_C_ItemPress);
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
        private void gv_plan_C_RowClick(object sender, RowClickEventArgs e)
        {
            
        }
        
        #endregion

        #region Event
        //customer
        private void bbi_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if(clsFunction.MessageDelete("Thông báo","Bạn có muốn xóa khách hàng này không")==true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from QUOTATION WHERE idcustomer ='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "'").Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Bạn có không thể xóa khách hàng này");
                    return;
                }
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from DMCUSTOMERS where idcustomer='"+ gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle,"idcustomer").ToString() +"'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from EMPCUS where idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSNOTE where idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSCONTACT where idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from PLANCRM where idcustomer='" + gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString() + "' ");
                gv_listcustomer_C.DeleteRow(gv_listcustomer_C.FocusedRowHandle);
            }
        }

        private void bbi_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_listcustomer_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S frm = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S();
                    frm._insert = false;
                    frm.statusForm = statusForm;
                    frm.ID = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
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
                frm._sign = "KH";
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S.PassData(getValueInsert);
                frm.ShowDialog();
           
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }
    
        private void bbi_check_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if ((bool)((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)(bbi_check_S.Edit)).ValueChecked == true)
            if ((bool)bbi_check_S.EditValue == false)
            {
                bbi_check_S.EditValue = true;
                for (int i = 0; i < gv_listcustomer_C.RowCount; i++)
                {
                    gv_listcustomer_C.SetRowCellValue(i, "check", true);
                }
            }
            else
            {
                bbi_check_S.EditValue = false;
                for (int i = 0; i < gv_listcustomer_C.RowCount; i++)
                {
                    gv_listcustomer_C.SetRowCellValue(i, "check", false);
                }
            }
        }
        
        private void bbi_allow_access_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void bbi_idarea_I1_EditValueChanged(object sender, EventArgs e)
        {
            gv_listcustomer_C.Columns["id"].FilterInfo = new ColumnFilterInfo("[id] LIKE '%" + bbi_idarea_I1.EditValue.ToString() + "%'");
        }

        private void bbi_idtype_I1_EditValueChanged(object sender, EventArgs e)
        {
            gv_listcustomer_C.Columns["idtype"].FilterInfo = new ColumnFilterInfo("[idtype] LIKE '%" + bbi_idtype_I1.EditValue.ToString() + "%'");
        }
        
        // Plan
        private void bbi_plan_allow_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_listcustomer_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    frm_PLANCRM_S frm = new frm_PLANCRM_S();
                    frm._insert = true;
                    frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                    frm._sign = "KH";
                    frm.statusForm = statusForm;
                    frm.passData = new frm_PLANCRM_S.PassData(getValueUpdatePlan);
                    frm.strpassData = new frm_PLANCRM_S.strPassData(getValueIdPlan);
                    frm.ShowDialog();
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo","Vui lòng chọn khách hàng");
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getValueUpdatePlan(bool value)
        {
            if (value == true)
            {
                loadGridPlan();              
                
            }
        }

        private void getValueIdPlan(string value)
        {
            if (value != "")
            {                
                
            }
        }

        private void getValueContact(bool value)
        {
            if (value == true)
            {
                loadGridContact();
            }
        }

        private void bbi_plan_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_listcustomer_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    frm_PLANCRM_S frm = new frm_PLANCRM_S();
                    frm._insert = false;
                    frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                    frm._sign = _sign;
                    frm.statusForm = statusForm;
                    frm.ID = gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idplan").ToString();
                    frm.passData = new frm_PLANCRM_S.PassData(getValueUpdatePlan);
                    frm.ShowDialog();
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Bạn phải chọn dòng cần sửa");
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error" + ex.Message);
            }
        }

        private void bbi_plan_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (gv_plan_C.FocusedRowHandle >= 0)
            {
                if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa dòng được chọn") == true)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete  from PLANCRM where idplan='" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idplan").ToString() + "' ");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from empmission where idplan='"+ gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle,"idplan") +"'");
                    gv_plan_C.DeleteRow(gv_plan_C.FocusedRowHandle);
                }
            }
            else
            {
                clsFunction.MessageInfo("Thông báo","Bạn phải chọn dòng cần xóa");
            }
        }
        
        private void gv_listcustomer_C_DoubleClick(object sender, EventArgs e)
        {
            bbi_allow_edit_S.PerformClick();
        }

        // CONTACT

        private void bbi_allow_insert_contact_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CUSCONTACT_S frm = new frm_CUSCONTACT_S();
                frm._insert = true;
                frm._sign = "KH";
                frm.statusForm = statusForm;
                frm.lbl_idcustomer_I1.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
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
                    frm.lbl_idcustomer_I1.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                    frm.passData = new frm_CUSCONTACT_S.PassData(getValueContact);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error" + ex.Message);
            }
        }

        private void bbi_note_allow_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CUSNOTE_S frm = new frm_CUSNOTE_S();
                frm._insert = true;
                frm._sign = _sign;
                frm.statusForm = statusForm;
                frm.lbl_idcustomer_I1.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                frm.passData = new frm_CUSNOTE_S.PassData(getValueCusnote);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_note_allow_delete_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (Function.clsFunction.MessageDelete("Thông báo","Bạn có chắc muốn xóa mẫu tin này không ?"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CUSNOTE where idcusnote='" + gv_cusnote_C.GetRowCellValue(gv_cusnote_C.FocusedRowHandle, "idcusnote").ToString() + "' ");
                gv_cusnote_C.DeleteRow(gv_cusnote_C.FocusedRowHandle);
            }
        }

        private void bbi_note_allow_edit_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_cusnote_C.FocusedRowHandle >= 0)
                {
                    menu.HidePopup();
                    frm_CUSNOTE_S frm = new frm_CUSNOTE_S();
                    frm._insert = false;
                    frm.statusForm = statusForm;
                    frm.ID = gv_cusnote_C.GetRowCellValue(gv_cusnote_C.FocusedRowHandle, "idcusnote").ToString();
                    frm.lbl_idcustomer_I1.Text = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                    frm.passData = new frm_CUSNOTE_S.PassData(getValueCusnote);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error" + ex.Message);
            }
        }

        private void getValueCusnote(bool value)
        {
            if (value == true)
            {
                loadGridCusnote();
            }
        }

        private void bbi_allow_import_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            return;
            menu.HidePopup();
            IMPORTEXCEL.frm_inPut_S frm = new IMPORTEXCEL.frm_inPut_S();
            frm.sDauma = _sign;
            frm.formNamePre = this.Name;
            frm.gridNamePre = gv_listcustomer_C.Name;
            frm.arrColumnCaption = arrCaption.Split('/');
            frm.arrColumnFieldName = arrFieldName.Split('/');
            frm.tbName = clsFunction.getNameControls(this.Name);
            frm.ShowDialog();
            APCoreProcess.APCoreProcess.ExcuteSQL("insert into EMPCUS (idemp, idcustomer, status, date1, userid1) (select '" + clsFunction.GetIDEMPByUser() + "' as idemp, idcustomer,'True' as status, getdate() as date1,'" + clsFunction._iduser + "' as userid1 from DMCUSTOMERS where userid1 IS NULL );update DMCUSTOMERS set userid1='" + clsFunction._iduser + "', date1 =getdate() where userid1 IS NULL");
            loadGrid();
        }

        private void bbi_emp_S_EditValueChanged(object sender, EventArgs e)
        {
            gv_listcustomer_C.Columns["idemp"].FilterInfo = new ColumnFilterInfo("[idemp] LIKE '%" + bbi_emp_S.EditValue.ToString() + "%'");
        }


        private void bbi_allow_insert_campaign_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    menu.HidePopup();
            //    SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S frm = new SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S();
            //    frm._insert = true;
            //    frm._sign = _sign;
            //    frm.statusForm = statusForm;
            //    frm.passData = new SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S.PassData(getValueUpdate);
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error :" + ex.Message);
            //}
        }

        private void bbi_allow_edit_campaign_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //menu.HidePopup();
                //SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S frm = new SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S();
                //frm._insert = false;
                //frm.statusForm = statusForm;
                //frm.ID = gv_campaign_C.GetRowCellValue(gv_campaign_C.FocusedRowHandle, "idcampaign").ToString();
                //frm.passData = new SOURCE_FORM_DMCAMPAIGN.Presentation.frm_DMCAMPAIGN_S.PassData(getValueUpdate);
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_delete_campaign_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {           
                APCoreProcess.APCoreProcess.ExcuteSQL("delete * from campaigncus where idcampaign='" + gv_campaign_C.GetRowCellValue(gv_campaign_C.FocusedRowHandle, "idcampaign").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete * from campaignmiss where idcampaign='" + gv_campaign_C.GetRowCellValue(gv_campaign_C.FocusedRowHandle, "idcampaign").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete * from dmcampaign where idcampaign='" + gv_campaign_C.GetRowCellValue(gv_campaign_C.FocusedRowHandle, "idcampaign").ToString() + "'");
                gv_campaign_C.DeleteRow(gv_campaign_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_refresh_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmListCustomer_Load(sender, e);
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

        // Device

        private void bbi_allow_insert_device_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICEINFO_S frm = new frm_DEVICEINFO_S();
                frm._insert = true;
                frm._sign = "DV";
                frm.statusForm = statusForm;
                frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                frm.passData = new frm_DEVICEINFO_S.PassData(getValueDeviceInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getValueDeviceInfo(bool value)
        {
            if (value == true)
            {
                loadGridDevice();
            }
        }
        private void bbi_allow_edit_device_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_DEVICEINFO_S frm = new frm_DEVICEINFO_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_deviceinfo_C.GetRowCellValue(gv_deviceinfo_C.FocusedRowHandle, "iddevice").ToString();
                frm.passData = new frm_DEVICEINFO_S.PassData(getValueDeviceInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }
        private void bbi_allow_delete_device_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from deviceinfo where iddevice='" + gv_deviceinfo_C.GetRowCellValue(gv_deviceinfo_C.FocusedRowHandle, "iddevice").ToString() + "'");
                gv_deviceinfo_C.DeleteRow(gv_deviceinfo_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        // Voxe

        private void bbi_allow_insert_voxe_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_VOXEINFO_S frm = new frm_VOXEINFO_S();
                frm._insert = true;
                frm._sign = "DV";
                frm.statusForm = statusForm;
                frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                frm.passData = new frm_VOXEINFO_S.PassData(getValueVoxeInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getValueVoxeInfo(bool value)
        {
            if (value == true)
            {
                loadGridVoxe();
            }
        }

        private void getValueBinhDienInfo(bool value)
        {
            if (value == true)
            {
                loadGridBinhDien();
            }
        }
        private void bbi_allow_edit_voxe_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_VOXEINFO_S frm = new frm_VOXEINFO_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_voxe_C.GetRowCellValue(gv_voxe_C.FocusedRowHandle, "iddevice").ToString();
                frm.passData = new frm_VOXEINFO_S.PassData(getValueVoxeInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }
        private void bbi_allow_delete_voxe_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from voxeinfo where iddevice='" + gv_voxe_C.GetRowCellValue(gv_voxe_C.FocusedRowHandle, "iddevice").ToString() + "'");
                gv_voxe_C.DeleteRow(gv_voxe_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_insert_binhdien_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_BINHDIENINFO_S frm = new frm_BINHDIENINFO_S();
                frm._insert = true;
                frm._sign = "DV";
                frm.statusForm = statusForm;
                frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                frm.passData = new frm_BINHDIENINFO_S.PassData(getValueBinhDienInfo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_binhdien_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from binhdieninfo where idbinh='" + gv_binhdien_C.GetRowCellValue(gv_binhdien_C.FocusedRowHandle, "idbinh").ToString() + "'");
                gv_binhdien_C.DeleteRow(gv_binhdien_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void bbi_allow_edit_binhdien_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_BINHDIENINFO_S frm = new frm_BINHDIENINFO_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_binhdien_C.GetRowCellValue(gv_binhdien_C.FocusedRowHandle, "idbinh").ToString();
                frm.passData = new frm_BINHDIENINFO_S.PassData(getValueBinhDienInfo);
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
                int index = gv_listcustomer_C.FocusedRowHandle;
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idprovince"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idprovince,province from dmprovince where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idtype"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idtype,customertype from dmcustomertype where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idfields"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfields,fieldname from dmfields where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idscale"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idscale,scale from dmscale where status=1");
                gv_listcustomer_C.FocusedRowHandle = index;
            }
        }

        private void getValueInsert(bool value)
        {
            if (value == true)
            {
                int index = gv_listcustomer_C.FocusedRowHandle;
                loadGrid();
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idprovince"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idprovince,province from dmprovince where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idtype"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idtype,customertype from dmcustomertype where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idfields"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idfields,fieldname from dmfields where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_listcustomer_C.Columns["idscale"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idscale,scale from dmscale where status=1");
                gv_listcustomer_C.FocusedRowHandle = gv_listcustomer_C.RowCount-1;
            }
        }

        private void loadArea()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select id,area from dmarea where status=1");
                DataRow dr = dt.NewRow();
                dr["id"] = "";
                dr["area"] = "ALL";              
                dt.Rows.Add(dr);
                DataView dv = dt.DefaultView;
                dv.Sort = "id";
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idarea_I1.Edit)).DataSource = dv.ToTable();
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idarea_I1.Edit)).ValueMember="id";
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idarea_I1.Edit)).DisplayMember = "area";
                if (dv.ToTable().Rows.Count > 0)
                {
                    //bbi_idarea_I1.EditValue = dv.ToTable().Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo","Error:"+ex.Message);
            }
        }

        private void loadEmp()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select IDEMP as idemp, StaffName as staffname from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%' and status=1");
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

        private void loadCustomerType()
        {
            try
            {
                DataTable dt=new DataTable();
                dt=APCoreProcess.APCoreProcess.Read("select idtype,customertype from dmcustomertype where status=1");
                DataRow dr = dt.NewRow();
                dr["idtype"] = "";
                dr["customertype"] = "ALL";
                dt.Rows.Add(dr);
                DataView dv = dt.DefaultView;
                dv.Sort = "idtype";
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idtype_I1.Edit)).DataSource = dv.ToTable();
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idtype_I1.Edit)).ValueMember = "idtype";
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(bbi_idtype_I1.Edit)).DisplayMember = "customertype";
                if (dv.ToTable().Rows.Count > 0)
                {
                    bbi_idtype_I1.EditValue = dv.ToTable().Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void loadCusDup()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT EMP1.StaffName AS staffname1, EMP2.StaffName AS staffname2, dbo.DMCUSTOMERS.customer,dbo.DMCUSTOMERS.idcustomer, cusdup.datedup, cusdup.id FROM  dbo.CustomerDuplicate AS cusdup INNER JOIN    dbo.EMPLOYEES AS EMP1 ON cusdup.idempfirst = EMP1.IDEMP INNER JOIN    dbo.EMPLOYEES AS EMP2 ON cusdup.idemplast = EMP2.IDEMP INNER JOIN   dbo.DMCUSTOMERS ON cusdup.idcustomer = dbo.DMCUSTOMERS.idcustomer ORDER BY cusdup.datedup DESC ");
                gct_cusdup_C.DataSource = dt;
            }
            catch (Exception ex)
            { 
            }
        }

       

        #endregion                           

        private void xtap_cus_C_Click(object sender, EventArgs e)
        {

        }

        private void bbi_allow_export_S_ItemClick(object sender, ItemClickEventArgs e)
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
                clsImportExcel.exportExcelTeamplate(dt, Application.StartupPath + @"\File\Template.xlt", gv_listcustomer_C, clsFunction.transLateText("Danh mục khách hàng"), this);
            }
            catch
            { }
        }

        private void gv_binhdien_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_binhdien_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_binhdien_C , menu, gv_binhdien_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(bar_voxe_C_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
            }
        }


        private void bar_binhdien_C_ItemPress(object sender, ItemClickEventArgs e)
        {

        }

        private void bbi_allow_insert_survey_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_listcustomer_C.FocusedRowHandle < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn khách hàng cần khảo sát");
                    return;
                }
                menu.HidePopup();
                SOURCE_FORM_CRM.Presentation.frm_survey_S frm = new SOURCE_FORM_CRM.Presentation.frm_survey_S();
                frm._insert = true;
                frm.ks_khachhang = true;
                frm._sign = "KS";
                frm.idquotation = "";
                frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle,"idcustomer").ToString();
                frm.passData = new SOURCE_FORM_CRM.Presentation.frm_survey_S.PassData(getValueSurvey);
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_delete_survey_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            menu.HidePopup();
            if (gv_survey_C.FocusedRowHandle < 0)
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn khảo sát cần xóa");
                return;
            }
            if (clsFunction.MessageDelete("Thông  báo", "Bạn có muốn xóa mẫu tin này không ?") == true)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from survey where idsurvey='" + gv_survey_C.GetRowCellValue(gv_survey_C.FocusedRowHandle, "idsurvey").ToString() + "'");
                gv_survey_C.DeleteRow(gv_survey_C.FocusedRowHandle);
            }
        }

        private void bbi_allow_edit_survey_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_survey_C.FocusedRowHandle < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn khảo sát cần sửa");
                    return;
                }
                menu.HidePopup();
                
                SOURCE_FORM_CRM.Presentation.frm_survey_S frm = new SOURCE_FORM_CRM.Presentation.frm_survey_S();
                frm.ks_khachhang = true;
                frm._insert = false;
                frm._sign = "KS";
                frm.idquotation ="";
                frm.idcustomer = gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle, "idcustomer").ToString();
                frm.statusForm = statusForm;
                frm.passData = new SOURCE_FORM_CRM.Presentation.frm_survey_S.PassData(getValueSurvey);
                frm.ID = gv_survey_C.GetRowCellValue(gv_survey_C.FocusedRowHandle, "idsurvey").ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getValueSurvey(bool value)
        {
            if (value == true)
            {
                loadGridSurvey();
            }
        }

        private void loadGridSurvey()
        {
            try
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    dts = APCoreProcess.APCoreProcess.Read("SELECT    S.idcustomer, S.idsurvey, S.surveyno, S.content as content, CT.contactname as contact, S.daterequest, S.dateaction, S.description, S.useraction, S.userrequest, S.spec, S.model, S.sign, S.address, S.idbrand, S.note, S.tel FROM  dbo.SURVEY S LEFT JOIN CUSCONTACT CT ON S.idcontact = CT.idcontact WHERE     (idquotation is null OR idquotation='') AND S.idcustomer = '"+ gv_listcustomer_C.GetRowCellValue(gv_listcustomer_C.FocusedRowHandle,"idcustomer").ToString() +"' ");
                }
                gct_survey_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gv_survey_C_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_survey_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_Survey_C, menu, gv_survey_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_Survey);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }
        private void Manager_ItemPress_Survey(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_survey_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gct_survey_C.Name + "'");
                            Load_Grid_Survey();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void SaveGridControlsSurvey()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã KS", "Số KS", "Nhãn hiệu", "Nội dung", "Liên hệ", "Ngày yêu cầu", "Ngày khảo sát", "Mô tả", "Người yêu cầu", "Người thực hiện", "Thông số", "Model",  "Địa chỉ", "Điện thoại", "Ghi chú" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "idsurvey", "surveyno", "idbrand", "content", "contact", "daterequest", "dateaction", "description", "userrequest", "useraction", "spec", "model", "address", "tel", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "DateColumn", "DateColumn", "MemoColumn", "TextColumn", "MemoColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "200", "200", "100", "80", "80", "200", "100", "100", "200", "100",  "200", "100", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False",  "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idbrand, brand  from dmbrand", };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "brand" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idbrand" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "Mã HH", "Nhãn hiệu" } };

            string[,] fieldname_glue_col = new string[1, 2] { { "idbrand", "brand" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_survey_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

        private void Load_Grid_Survey()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho", "Tiền tệ" };
            bool show_footer = true;
            // show filterRow
            gv_survey_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_survey_C.Name + "'");
            gv_survey_C.Columns.Clear();
            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_survey_C, read_Only, hien_Nav,
                              dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_survey_C,
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
                gv_survey_C.OptionsBehavior.Editable = true;
                gv_survey_C.OptionsView.ColumnAutoWidth = false;
                gv_survey_C.OptionsView.ShowAutoFilterRow = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_search_S_EditValueChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}