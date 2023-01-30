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
namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_NHIEMVU_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_NHIEMVU_SH()
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
                SaveGridControls_Ngay();
                SaveGridControls_TienDo();
                SaveGridControls_Ngay();
                SaveGridControls();
                SaveGridControls_His();
                SaveGridControls_Chitiet();
            }
            else
            {
                try
                {
                    // tinhBaoHiem();
                    dte_kehoachtungay_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dte_congviec_tungay_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    
                    Function.clsFunction.TranslateForm(this, this.Name);
                    loadGridLookupEmployeeKeHoach();
                    Load_Grid();
                    Load_Grid_Ngay();
                    Load_Grid_TienDo();
                    Load_Grid_Chitiet();
                    Load_Grid_His();
                    dte_kehoachdenngay_S.EditValue = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1);
                    dte_congviec_denngay_S.EditValue = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1);
                    
                    Function.clsFunction.TranslateGridColumn(gv_list_C);
                    ControlDev.FormatControls.setContainsFilter(gv_list_C);
                    loadResponsitory();
                    gct_tiendo_C.DataSource = null;
                    btn_kehoach_tim_S_Click(sender,e);
                    btn_congviec_search_S_Click(sender,e);
                }
                catch (Exception ex)
                {
                    clsFunction.MessageInfo("Thông báo", ex.Message);
                }
            }
        }


        #endregion

        #region ButtonEvent

    

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

        private void bbi_allow_insert_info_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CHITIETCONGVIECHANGNGAY_S frm = new frm_CHITIETCONGVIECHANGNGAY_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                frm.idngay = gv_chamcong_C.GetRowCellValue(gv_chamcong_C.FocusedRowHandle, "idngay").ToString();
                frm.passData = new frm_CHITIETCONGVIECHANGNGAY_S.PassData(getValueChiTiet);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_search_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadGrid();
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
            string[] column_summary = new string[] {};
            // Caption column
            string[] caption_col = new string[] {"Tình trạng", "ID", "Số PO", "Khách hàng", "Nhân viên quản lý","Độ ưu tiên", "Công việc được giao", "Ngày nhận việc", "Ngày dự định", "Số giờ TH", "Ngày BBNT nội bộ", "Ngày BBNT bàn giao KH","Số giờ thực tế thực hiện", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] {"matinhtrangcongviec", "manhiemvu", "idquotation", "idcustomer", "manhanvienphutrach", "idpriority", "congviecduocgiao", "ngaynhanviec", "ngaygiaohangdudinh", "sogiodudinhthuchien", "ngaynghiemthunoibo", "ngaygiaohang", "songaythuctethuchien", "ghichu", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "MemoColumn", "DateColumn", "DateColumn", "SpinEditColumn", "DateColumn", "DateColumn", "SpinEditColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "100", "250",  "120", "200", "250", "100", "100", "100", "100", "100", "100", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False",  "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "FALSE", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] {  "select idemp , staffname from employees where status=1", "select idpriority , priority from dmpriority where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"staffname", "priority" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idemp", "idpriority" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "ID", "Nhân viên" },  { "ID", "Ưu tiên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] {  { "idemp", "staffname" },{ "idpriority", "priority" } };
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
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveGridControls_His()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID HIS","ID", "Số PO", "Khách hàng", "Nhân viên quản lý", "Nhân viên kỹ thuật", "Độ ưu tiên", "Công việc được giao", "Ngày nhận việc", "Ngày dự định", "Số ngày TH", "Ngày BBNT nội bộ", "Ngày BBNT bàn giao KH", "Số ngày thực tế thực hiện", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] {"idhis", "manhiemvu", "idquotation", "idcustomer", "manhanvienphutrach", "manhanvienthuchien", "ididpriority", "congviecduocgiao", "ngaynhanviec", "ngaygiaohangdudinh", "sogiodudinhthuchien", "ngaynghiemthunoibo", "ngaygiaohang", "songaythuctethuchien", "ghichu", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "MemoColumn", "DateColumn", "DateColumn", "SpinEditColumn", "DateColumn", "DateColumn", "SpinEditColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "100", "250", "200", "120", "200", "250", "100", "100", "100", "100", "100", "100", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select idcustomer , customer from dmcustomer where status=1", "select idemp , staffname from employees where status=1", "select idemp , staffname from employees where status=1", "select idpriority , priority from dmpriority where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "staffname", "staffname", "priority" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "idemp", "idemp", "idpriority" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[4, 2] { { "ID", "Khách hàng" }, { "ID", "Nhân viên" }, { "ID", "Nhân viên" }, { "ID", "Ưu tiên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[4, 2] { { "idcustomer", "customer" }, { "idemp", "staffname" }, { "idemp", "staffname" }, { "idpriority", "priority" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_detail_C.Name);
        }

        private void Load_Grid_His()
        {
            string text = Function.clsFunction.langgues;
            gv_detail_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_detail_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_list_C.Name + "'");

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

        private void SaveGridControls_Ngay()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Ngày", "Nhiệm vụ",  "Nhân viên quản lý", "Nhân viên kỹ thuật", "Giờ bắt đầu", "Giờ kết thúc", "Tính công", "Lý do", "Ghi chú",  "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "idngay", "ngay", "manhiemvu", "manhanvienphutrach", "manhanvienthuchien", "giobatdau", "gioketthuc", "ngaycong", "diengiai",  "note", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "DateColumn", "GridLookupEditColumn", "GridLookupEditColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "SpinEditColumn", "MemoColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "250", "150", "150", "100", "100", "100", "200",  "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False",  "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True"};
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
            string[] sql_glue = new string[] { "select manhiemvu , congviecduocgiao from nhiemvu where status=1", "select idemp , staffname from employees where status=1", "select idemp , staffname from employees where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "congviecduocgiao", "staffname", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {"manhiemvu",  "idemp", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "ID", "Nhiệm vụ" },  { "ID", "Nhân viên" }, { "ID", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "manhiemvu", "congviecduocgiao" }, { "idemp", "staffname" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_chamcong_C.Name);
        }

        private void Load_Grid_Ngay()
        {
            string text = Function.clsFunction.langgues;
            gv_chamcong_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_chamcong_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_chamcong_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_chamcong_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_chamcong_C,
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

        private void SaveGridControls_TienDo()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID",  "Tình trạng", "Bộ phận", "Ngày tạo","Ngày dự định","Số giờ thực hiện", "Nội dung yêu cầu", "Ghi chú"};

            // FieldName column
            string[] fieldname_col = new string[] { "matiendo", "matinhtrangcongviec", "manhanvienthuchien", "ngaytao","ngaydudinh","sogiodudinh", "lydo", "note"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "DateColumn", "DateColumn", "TextColumn", "MemoColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "250", "100","100","100", "250", "250"};
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
            string[] sql_glue = new string[] {  "select matinhtrangcongviec , tinhtrangcongviec from dmtinhtrangcongviec where status=1", "select idemp , staffname from employees where status=1"};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {  "tinhtrangcongviec", "staffname"};
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "matinhtrangcongviec", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "ID", "Kết quả công việc" }, { "ID", "Nhân viên" }};
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] {  { "matinhtrangcongviec", "tinhtrangcongviec" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_tiendo_C.Name);
        }

        private void Load_Grid_TienDo()
        {
            string text = Function.clsFunction.langgues;
            gv_tiendo_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_tiendo_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_tiendo_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_tiendo_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_tiendo_C,
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

        private void SaveGridControls_Chitiet()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Nhiệm vụ", "Kết quả công việc", "Giờ đi", "Giờ bắt đầu", "Giờ kết thúc", "Cần hỗ trợ", "Người đề xuất","Nội dung", "Ghi chú", "Status" };

            // FieldName column
            string[] fieldname_col = new string[] { "machitietcongviechangngay", "congviecduocgiao", "matinhtrangcongviec", "giodi", "giobatdau", "gioketthuc","canhotro", "manhanviendexuat", "noidungyeucau", "ghichu", "status" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "MemoColumn", "GridLookupEditColumn", "TimeEditColumn", "TimeEditColumn", "TimeEditColumn",  "CheckColumn", "GridLookupEditColumn", "MemoColumn", "MemoColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "300",  "200", "100", "100", "100", "100", "150", "200", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True" };
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
            string[] sql_glue = new string[] { "select matinhtrangcongviec , tinhtrangcongviec from dmtinhtrangcongviec where status=1", "select idemp , staffname from employees where status=1"};
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "tinhtrangcongviec", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "matinhtrangcongviec", "idemp"};
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "ID", "Kết quả công việc" }, { "ID", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "matinhtrangcongviec", "tinhtrangcongviec" }, { "idemp", "staffname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_chitiethangngay_C.Name);
        }

        private void Load_Grid_Chitiet()
        {
            string text = Function.clsFunction.langgues;
            gv_chitiethangngay_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;
            // show filterRow
            gv_chamcong_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_chitiethangngay_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_chitiethangngay_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_chitiethangngay_C,
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


   
        private void gv_list_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_list_C.FocusedRowHandle >= 0)
                {
                    loadStatus(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString());
                    loadGridNgay();
                    loadGridTienDo();
                    loadNhiemVu();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Function.clsFunction.transLateText("Thông báo"));
            }
        }

       

        private void Manager_ItemPress_Ngay(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos=-1;
            try
            {              
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_chamcong_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='"+ strColVisible +"' where form_name='"+this.Name+"' and grid_name ='"+ gv_chamcong_C.Name +"'");
                            Load_Grid_Ngay();
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void Manager_ItemPress_Tiendo(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_tiendo_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_tiendo_C.Name + "'");
                            Load_Grid_TienDo();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void Manager_ItemPress_ChiTiet(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name ='" + gv_chitiethangngay_C.Name + "'");
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
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name ='" + gv_chitiethangngay_C.Name + "'");
                            Load_Grid_Chitiet();
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
            // ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idcustomer"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 " + dkManager + ") AND E.status='True' ORDER BY C.idcustomer");
            //((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)(gv_list_C.Columns["idcommodity"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("SELECT    C.idcommodity, C.commodity, C.sign FROM   dbo.DMRENTDEVICE AS C WHERE status =1");
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
                if (glue_matinhtrangcongviec_S.EditValue.ToString() != "")
                {
                    sdk += " AND matinhtrangcongviec='" + glue_matinhtrangcongviec_S.EditValue.ToString() + "'";
                }

                if (glue_kehoach_manhanvienphutrach_S.EditValue != "")
                {
                    sdk += " AND manhanvienphutrach='" + glue_kehoach_manhanvienphutrach_S.EditValue + "'";
                }

                //cast('" + Convert.ToDateTime(bbi_todate_S.EditValue) + "' as date)
                if (dte_kehoachtungay_S.EditValue != "" && dte_kehoachdenngay_S.EditValue == "")
                {
                    sdk += " AND cast (ngaytao as date) >= cast ('" + dte_kehoachtungay_S.EditValue + "' as date)";
                }

                if (dte_kehoachtungay_S.EditValue == "" && dte_kehoachdenngay_S.EditValue != "")
                {
                    sdk += " AND cast (ngaytao as date) <= cast ('" + dte_kehoachdenngay_S.EditValue + "' as date)";
                }
                if (dte_kehoachtungay_S.EditValue != "" && dte_kehoachdenngay_S.EditValue != "")
                {
                    sdk += " AND cast (ngaytao as date) BETWEEN cast ('" + dte_kehoachtungay_S.EditValue + "' as date) AND cast ('" + dte_kehoachdenngay_S.EditValue + "' as date)";
                }


                dts = APCoreProcess.APCoreProcess.Read("SELECT     TD.matinhtrangcongviec  , D.idpriority, D.ngaygiaohang, D.ngaynghiemthunoibo, D.ngaygiaohangdudinh, D.ngaytao, D.manhanvienphutrach,  C.customer,  D.songaythuctethuchien, D.ngaynhanviec, D.sogiodudinhthuchien, D.status, D.idquotation, D.manhiemvu, D.ghichu, D.congviecduocgiao FROM            dbo.NHIEMVU AS D INNER JOIN DMCUSTOMERS C ON D.idcustomer = C.idcustomer INNER JOIN    dbo.EMPLOYEES AS EM ON D.manhanvienphutrach = EM.IDEMP LEFT  JOIN   (SELECT        TOP (10) manhiemvu, MAX(matinhtrangcongviec) AS matinhtrangcongviec FROM            dbo.TIENDOCONGVIEC    GROUP BY manhiemvu order by matinhtrangcongviec desc) AS TD ON D.manhiemvu = TD.manhiemvu WHERE (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0   OR '" + clsFunction.GetIDEMPByUser() + "' IN (select manhanvienthuchien from TIENDOCONGVIEC where manhiemvu=D.manhiemvu) )   " + sdk);
            }

            gct_list_C.DataSource = dts;
        }



        private void loadStatus(string ID)
        {
           
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


        private void getValueNgay(bool value)
        {
            if (value == true)
            {
                loadGridNgay();
            }
        }

        private void getValueTienDo(bool value)
        {
            if (value == true)
            {
                loadGridTienDo();
            }
        }

        private void loadGridTienDo()
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from TIENDOCONGVIEC where manhiemvu='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString() + "'");
                gct_tiendo_C.DataSource = dt;
            }
        }

        private void loadGridNgay()
        {
          
                string sdk = "";
                if (glue_congviec_manhanvienthuchien_S.EditValue.ToString() != "")
                {
                    sdk += " AND manhanvienthuchien='" + glue_congviec_manhanvienthuchien_S.EditValue.ToString() + "'";
                }

                if (glue_congviec_manhanvienphutrach_S.EditValue != "")
                {
                    sdk += " AND manhanvienphutrach='" + glue_congviec_manhanvienphutrach_S.EditValue + "'";
                }
                //cast('" + Convert.ToDateTime(bbi_todate_S.EditValue) + "' as date)
                if (dte_congviec_tungay_S.EditValue != "" && dte_congviec_denngay_S.EditValue == "")
                {
                    sdk += " AND cast (ngay as date) >= cast ('" + dte_congviec_tungay_S.EditValue + "' as date)";
                }

                if (dte_congviec_tungay_S.EditValue == "" && dte_congviec_denngay_S.EditValue != "")
                {
                    sdk += " AND cast (ngay as date) <= cast ('" + dte_congviec_denngay_S.EditValue + "' as date)";
                }
                if (dte_congviec_tungay_S.EditValue != "" && dte_congviec_denngay_S.EditValue != "")
                {
                    sdk += " AND cast (ngay as date) BETWEEN cast ('" + dte_congviec_tungay_S.EditValue + "' as date) AND cast ('" + dte_congviec_denngay_S.EditValue + "' as date)";
                }

                DataTable dt = APCoreProcess.APCoreProcess.Read("select C.* from CONGVIECHANGNGAY C inner join EMPLOYEES E on C.manhanvienphutrach = E.idemp where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'" + sdk);
                gct_chamcong_C.DataSource = dt;
                gv_chamcong_C.Columns["ngay"].Group();
                gv_chamcong_C.ExpandAllGroups();
            
        }

        private void getValueChiTiet(bool value)
        {
            if (value == true)
            {
                loadGridChiTiet();
            }
        }

        private void loadGridChiTiet()
        {
            if (gv_chamcong_C.FocusedRowHandle >= 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from CHITIETCONGVIECHANGNGAY where idngay='" + gv_chamcong_C.GetRowCellValue(gv_chamcong_C.FocusedRowHandle, "idngay").ToString() + "'");
                gct_detail_C.DataSource = dt;
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
            
        #endregion                   


        private void btn_allow_insert_kehoach_S_Click(object sender, EventArgs e)
        {
            try
            {
                frm_NHIEMVU_S frm = new frm_NHIEMVU_S();
                frm._insert = true;
                frm._sign = _sign;
                frm.statusForm = statusForm;
                frm.passData = new frm_NHIEMVU_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi");
            }
        }

        private void btn_kehoach_tim_S_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void btn_allow_delete_kehoach_S_Click(object sender, EventArgs e)
        {
            if (!checkAdmin())
            {
                if (APCoreProcess.APCoreProcess.Read("select * from TIENDOCONGVIEC where manhiemvu='"+ gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"manhiemvu").ToString() +"'").Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Kế hoạch đã được triển khai, chỉ có admin mới có quyền sửa nội dung kế hoạch");
                    return;
                }
                
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from TIENDOCONGVIEC where manhiemvu='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString() + "'");
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from NHIEMVU where manhiemvu='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString() + "'");
                gv_list_C.DeleteRow(gv_list_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

       

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadGridLookupEmployeeKeHoach()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_kehoach_manhanvienphutrach_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_kehoach_manhanvienphutrach_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_kehoach_manhanvienthuchien_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_kehoach_manhanvienthuchien_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_congviec_manhanvienphutrach_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_congviec_manhanvienphutrach_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_congviec_manhanvienthuchien_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_congviec_manhanvienthuchien_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_manhanvien_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvien_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idemp_baohanh_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_baohanh_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idemp_baotri_S, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_baotri_S.Width, col_visible);

                col_visible = new string[] { "True", "True" };
                caption = new string[] { "Mã TT", "Tình trạng" };
                fieldname = new string[] { "matinhtrangcongviec", "tinhtrangcongviec" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_matinhtrangcongviec_S, "select '' as matinhtrangcongviec, N'" + clsFunction.transLateText("Tất cả") + "' as tinhtrangcongviec union select matinhtrangcongviec, tinhtrangcongviec from DMTINHTRANGCONGVIEC ", "tinhtrangcongviec", "matinhtrangcongviec", caption, fieldname, this.Name, glue_matinhtrangcongviec_S.Width, col_visible);
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_kehoach_matinhtrangcongviec_S, "select  matinhtrangcongviec, tinhtrangcongviec from DMTINHTRANGCONGVIEC ", "tinhtrangcongviec", "matinhtrangcongviec", caption, fieldname, this.Name, glue_kehoach_matinhtrangcongviec_S.Width, col_visible);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gv_chamcong_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_chamcong_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_chitiet_C, menu, gv_chamcong_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_ChiTiet);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_chitiethangngay_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_chitiethangngay_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_chitiet_C, menu, gv_chitiethangngay_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_ChiTiet);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void bbi_allow_insert_his_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CONGVIECHANGNGAY_S frm = new frm_CONGVIECHANGNGAY_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                frm.manhiemvu = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString();
                frm.passData = new frm_CONGVIECHANGNGAY_S.PassData(getValueNgay);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_insert_tiendocongviec_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_TIENDOCONGVIEC_S frm = new frm_TIENDOCONGVIEC_S();
                frm._insert = true;
                frm._sign = "TD";
                frm.statusForm = statusForm;
                frm.manhiemvu = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString();
                frm.congviecduocgiao = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "congviecduocgiao").ToString();
                frm.passData = new frm_TIENDOCONGVIEC_S.PassData(getValueTienDo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void gct_tiendo_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (gv_list_C.FocusedRowHandle < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn nhiệm vụ");
                    return;
                }
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_tiendo_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_tiendo_C, menu, gv_tiendo_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_Tiendo);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void gv_list_C_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
        }

        private void gv_list_C_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (gv_list_C.FocusedRowHandle >= 0)
                {
                    string status = gv_list_C.GetRowCellValue(e.RowHandle, "matinhtrangcongviec").ToString();
                    if (status == "TT000002" || status == "TT000003")
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    if (status == "TT000004")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                    if (status == "TT000005")
                    {
                        e.Appearance.BackColor = Color.Maroon;
                    }
                }
            }
            catch { }
        }

        private void btn_allow_edit_kehoach_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkAdmin())
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from TIENDOCONGVIEC where manhiemvu='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString() + "'").Rows.Count > 0)
                    {
                        clsFunction.MessageInfo("Thông báo", "Kế hoạch đã được triển khai, chỉ có admin mới có quyền sửa nội dung kế hoạch");
                        return;
                    }

                }
                menu.HidePopup();
                frm_NHIEMVU_S frm = new frm_NHIEMVU_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, Function.clsFunction.getNameControls(frm.txt_manhiemvu_IK1.Name)).ToString();
                frm.passData = new frm_NHIEMVU_S.PassData(getValueUpdate);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa");
            }
        }

        private void loadNhiemVu()
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read("SELECT     TD.matinhtrangcongviec, CS.surrogate , CS.tel, CS.fax, CS.tax, CS.email, CS.address , D.idpriority, D.ngaygiaohang, D.ngaynghiemthunoibo, D.ngaygiaohangdudinh, D.ngaytao, EM.staffname,  D.idcustomer,  D.songaythuctethuchien, D.manhanvienphutrach,  D.status, D.idquotation, D.manhiemvu, D.ghichu, D.congviecduocgiao, CS.idcustomer, CS.customer FROM    dbo.NHIEMVU AS D INNER JOIN    dbo.EMPLOYEES AS EM ON D.manhanvienphutrach = EM.IDEMP LEFT  JOIN   (SELECT        TOP (20) manhiemvu, MAX(matinhtrangcongviec) AS matinhtrangcongviec FROM      dbo.TIENDOCONGVIEC    GROUP BY manhiemvu order by matinhtrangcongviec desc) AS TD ON D.manhiemvu = TD.manhiemvu  INNER JOIN DMCUSTOMERS CS ON D.idcustomer=CS.idcustomer WHERE D.manhiemvu='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                txt_customer_S.Text = "";
                txt_surrogate_S.Text = "";
                txt_tel_S.Text = "";
                txt_tax_S.Text = "";
                txt_fax_S.Text = "";
                txt_email_S.Text = "";
                mmo_address_I3.Text = "";
                txt_kethoach_manhanvienphutrach_S.EditValue = "";
                dte_kehoach_ngaybatdau_S.EditValue = null;
                dte_kehoach_ngaydudinh_I5.EditValue = null;
                cal_kehoach_songaythuchien_S.Value = 0;
                dte_kehoach_ngaynghiemthunoibo_S.EditValue = null;
                dte_kehoach_ngaybangiaokh_S.EditValue = null;
                glue_kehoach_matinhtrangcongviec_S = null;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                txt_customer_S.Text = dr["customer"].ToString();
                txt_surrogate_S.Text = dr["surrogate"].ToString();
                txt_tel_S.Text = dr["tel"].ToString();
                txt_tax_S.Text = dr["tax"].ToString();
                txt_fax_S.Text = dr["fax"].ToString();
                txt_email_S.Text = dr["email"].ToString();
                mmo_address_I3.Text = dr["address"].ToString();
                txt_kethoach_manhanvienphutrach_S.EditValue = dr["staffname"].ToString();
                // txt_kehoach_manhanvienkythuat_S.Text = dr["manhanvienthuchien"].ToString();
                mmo_kehoach_noidungcongviec_S.Text = dr["congviecduocgiao"].ToString();
                dte_kehoach_ngaybatdau_S.EditValue = dr["ngaytao"];
                dte_kehoach_ngaydudinh_I5.EditValue = dr["ngaygiaohangdudinh"];
                // cal_kehoach_songaythuchien_S.Value = Convert.ToInt16( dr["sogiodudinhthuchien"]);
                dte_kehoach_ngaynghiemthunoibo_S.EditValue = dr["ngaynghiemthunoibo"];

                dte_kehoach_ngaybangiaokh_S.EditValue = dr["ngaygiaohang"];
                glue_kehoach_matinhtrangcongviec_S.EditValue = dr["matinhtrangcongviec"].ToString(); 
            }
        }

        private void bbi_allow_edit_tiendocongviec_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_TIENDOCONGVIEC_S frm = new frm_TIENDOCONGVIEC_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_tiendo_C.GetRowCellValue(gv_tiendo_C.FocusedRowHandle, "matiendo").ToString();
                frm.passData = new frm_TIENDOCONGVIEC_S.PassData(getValueTienDo);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_delete_tiendocongviec_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!checkAdmin())
            {
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from TIENDOCONGVIEC where matiendo='" + gv_tiendo_C.GetRowCellValue(gv_tiendo_C.FocusedRowHandle, "matiendo").ToString() + "'");
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
                loadGridTienDo();
            }
        }

        private void bbi_allow_insert_ngay_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            btn_allow_insert_congviec_S.PerformClick();
        }

        private void bbi_allow_insert_chitiet_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CONGVIECHANGNGAY_S frm = new frm_CONGVIECHANGNGAY_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                //frm.manhiemvu = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString();
                frm.passData = new frm_CONGVIECHANGNGAY_S.PassData(getValueNgay);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void btn_congviec_search_S_Click(object sender, EventArgs e)
        {
            loadGridNgay();
        }

        private void btn_allow_insert_congviec_S_Click(object sender, EventArgs e)
        {
            try
            {
                menu.HidePopup();
                frm_CONGVIECHANGNGAY_S frm = new frm_CONGVIECHANGNGAY_S();
                frm._insert = true;
                frm._sign = "IF";
                frm.statusForm = statusForm;
                frm.manhiemvu = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "manhiemvu").ToString();
                frm.passData = new frm_CONGVIECHANGNGAY_S.PassData(getValueNgay);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_edit_ngay_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            btn_allow_edit_congviec_S.PerformClick();
        }

        private void btn_allow_edit_congviec_S_Click(object sender, EventArgs e)
        {
            try
            {
                menu.HidePopup();
                if (!checkAdmin())
                {
                    clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                    return;
                }

                frm_CONGVIECHANGNGAY_S frm = new frm_CONGVIECHANGNGAY_S();
                frm._insert = false;
                frm.statusForm = statusForm;
                frm.ID = gv_chamcong_C.GetRowCellValue(gv_chamcong_C.FocusedRowHandle, "idngay").ToString();
                frm.passData = new frm_CONGVIECHANGNGAY_S.PassData(getValueNgay);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi, vui lòng chọn dòng cần sửa Error:" + ex.Message);
            }
        }

        private void bbi_allow_delete_ngay_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            btn_allow_delete_congviec_S.PerformClick();
        }

        private void btn_allow_delete_congviec_S_Click(object sender, EventArgs e)
        {
            menu.HidePopup();
            if (!checkAdmin())
            {
                clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                return;
            }

            if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không"))
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete  from CONGVIECHANGNGAY where idngay='" +  gv_chamcong_C.GetRowCellValue(gv_chamcong_C.FocusedRowHandle, "idngay").ToString() + "'");
                gv_chamcong_C.DeleteRow(gv_chamcong_C.FocusedRowHandle);
                //Function.clsFunction.Delete_M(Function.clsFunction.getNameControls(this.Name), gv_list_C, "idcampaign", this, gv_list_C.Columns["idcampaign"], bbi_allow_delete.Name, "PLANCRM", "idcampaign");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}