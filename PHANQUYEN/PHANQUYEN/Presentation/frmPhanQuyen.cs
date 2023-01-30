//Thanh : 20131305
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Reflection;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;


namespace PHANQUYEN.Presentation
{
    public partial class frm_phanquyen_SH : DevExpress.XtraEditors.XtraForm
    {
        public frm_phanquyen_SH()
        {
            InitializeComponent();
        }

        #region Var

        public string langues = "_VI", mavaitro = "", ID = "";
        public bool statusForm = false;   
        private int row_focus = -1;    
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
         DevExpress.XtraTreeList.TreeList tl = new DevExpress.XtraTreeList.TreeList();
        DataTable dtMessage = new DataTable();

        public void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                frm_sysUser_S frm = new frm_sysUser_S();
                frm.them = false;
                frm.ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                frm.index = Function.clsFunction.getIndexIDinTable(tl_containmenu_S.FocusedNode.GetValue(colID).ToString(),"userid",APCoreProcess.APCoreProcess.Read("sysUser"));
                frm.passData = new frm_sysUser_S.PassDataUser(GetValueUser);
                frm.ShowDialog();

            }
        }
        public void GetValueX(bool value)
        {
            bool va_lue = value;
            if (va_lue == true && !checkAdmin(tl_containmenu_S.FocusedNode.GetValue(colID).ToString()))
            {
                string mavt = "";
                mavt = getVaiTroByUser(tl_containmenu_S.FocusedNode.GetValue(colID).ToString());
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysUser where userid='" + tl_containmenu_S.FocusedNode.GetValue(colID).ToString() + "'");
                if (APCoreProcess.APCoreProcess.Read("select * from sysUser where mavaitro='" + mavt + "'").Rows.Count == 0)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysPower where mavaitro='" + mavt + "'");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysvaitro where mavaitro='" + mavt + "'");
                }
            }
            else
            {
                MessageBox.Show("Bạn không thể xóa user này","Thông báo");
            }
        }
        public void GetValueUser(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                loadUser();
            }
        }

        private bool checkAdmin(string maUser)
        {
            if (APCoreProcess.APCoreProcess.Read("select * from sysUser where userid='" + maUser + "' and root=1").Rows.Count > 0)
                return true;
            return false;
        }
        #endregion

        #region Load
  


        private void frm_nhapdvt_SH_Load(object sender, EventArgs e)
        {
           
            Function.clsFunction._keylience = true;
            tl = new DevExpress.XtraTreeList.TreeList();
            langues = Function.clsFunction.langgues;
            //ConvertStringToArrayN("makhuvuc/tenkhuvuc/@makhuvuc1/tenkhuvuc1/@", "@", "/");
            ID = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='"+Function.clsFunction._iduser+"'").Rows[0][0].ToString();
            dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                SavetreeList();
            }
            else
            {                
                //bar button
                tl.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(tlpq_CellValueChanged);
                loadUser();
                Load_Tree(tl);

                //tl_quyen_S.KeyFieldName = "ID";
                //tl_quyen_S.ParentFieldName = "ParentID";
                //
                Function.clsFunction.Text_Control(this, langues);
             
                //DataTable dt_textForm = new DataTable();
                //dt_textForm = APCoreProcess.APCoreProcess.Read("select * from sysSubmenu where form_name='" + this.Name.ToString().Trim() + "'");

                //this.Text = dt_textForm.Rows[0]["text_form" + langues].ToString();
                //bar_contain_C.Items.Clear();
                //load_button(this, bar_contain_C, Function.clsFunction._iduser);
                checkNodeCon();
                //createpopupmenu();
            }
        }

        private void createpopupmenu()
        {
            int index = 0;
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name, dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.ma_sub_menu = dbo.sysPower.ma_sub_menu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
                {
                    Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
                    image = (Bitmap)Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\Image_Form\\" + dt.Rows[i]["control_name"].ToString().Trim() + ".png").Clone();

                    image.MakeTransparent(Color.Fuchsia);

                    contextMenuStrip.Items.Add(dt.Rows[i]["text" + langues].ToString());

                    contextMenuStrip.Items[index].Name = dt.Rows[i]["control_name"].ToString();
                    contextMenuStrip.Items[index].Image = image;
                    index++;

                    //((ToolStripMenuItem)contextMenuStrip.Items[0]).ShortcutKeys = (Keys)Type.GetType("Keys.S"); 

                }

            }
         
            contextMenuStrip.Show(this, PointToClient(MousePosition));
           
        }

        private void load_button(Form form_name, BarManager gctr, string userid)
        {

            gctr.BeginUpdate();
            // Create two bars and dock them to the top of the form.
            // Bar1 - is a main menu, which is stretched to match the form's width.
            // Bar2 - is a regular bar.
            Bar bar1 = new Bar();
            bar1.BarName = "menu";
            bar1.DockStyle = BarDockStyle.Top;
     
            // Position the bar1 above the bar2
            bar1.DockRow = 0;
            // The bar1 must act as the main menu.
   
            DataTable dt_button = new DataTable();
            dt_button = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name, dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.ma_sub_menu = dbo.sysPower.ma_sub_menu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt_button.Rows.Count; i++)
            {
                if (KiemTraQuyen(dt_button.Rows[i]["control_name"].ToString().Trim(), dt_button.Rows[i]) || dt_button.Rows[i]["control_name"].ToString().Trim().Contains("_thoat") || dt_button.Rows[i]["control_name"].ToString().Trim().Contains("_naplai"))
                {
                    BarSubItem btn = new BarSubItem();
                    btn.Caption = dt_button.Rows[i]["text" + langues].ToString().Trim();
                    btn.Name = dt_button.Rows[i]["control_name"].ToString().Trim();
                    ControlDev.FormatControls.FormatBarButtonImage(btn, btn.Name + ".png", 20, 20);
                   
                   
                    bar1.AddItem(btn);
                }
            }
            gctr.Bars.Add(bar1);
            gctr.MainMenu=bar1;
            gctr.ItemClick += new ItemClickEventHandler(btn_Click);
            gctr.EndUpdate();


        }

        private bool KiemTraQuyen(string control_name, DataRow dr)
        {
            bool flag = false;
            if (control_name.Contains("_in") == true)
            {
                if ((bool)dr["allow_print"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_them") == true)
            {
                if ((bool)dr["allow_insert"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_xoa") == true)
            {
                if ((bool)dr["allow_delete"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_sua") == true)
            {
                if ((bool)dr["allow_edit"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_nhap") == true)
            {
                if ((bool)dr["allow_import"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_xuat") == true)
            {
                if ((bool)dr["allow_export"] == true)
                {
                    flag = true;
                }
            }

            return flag;
        }
        
        #endregion

        #region Event
          

        private void btn_Click(object sender, ItemClickEventArgs e)
        {
            try
            {
                BarSubItem subMenu = e.Item as BarSubItem;
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     field_name, type, form_name FROM         dbo.sysControls WHERE     (type = N'BarButtonItem') AND (form_name = N'" + this.Name + "')");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (subMenu.Name.Contains(dt.Rows[i]["field_name"].ToString().Trim()))
                    {
                        if (subMenu.Name.Contains("xoa"))
                        {

                        }
                        if (subMenu.Name.Contains("themvaitro"))
                        {
                            bbi_vaitro_S_ItemClick(sender, e);
                        }
                        if (subMenu.Name.Contains("themnguoidung"))
                        {
                            bbi_themnguoidung_S_ItemClick_1(sender, e);
                        }
                        if (subMenu.Name.Contains("sua"))
                        {
                            bbi_sua_S_ItemClick_1(sender, e);
                        }

                        if (subMenu.Name.Contains("thoat"))
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        } 

        #endregion      

        #region Button Event

        private void bbi_vaitro_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_sysVaiTro_S frm = new frm_sysVaiTro_S();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TopMost = true;
            frm.passData = new frm_sysVaiTro_S.PassDataUser(GetValueUser);
            frm.them = true;
            frm.dauma = "VT";
            frm.langues = langues;
            frm.statusForm = statusForm;
            frm.txt_mavaitro_IK1.Text = Function.clsFunction.layMa("VT", Function.clsFunction.getNameControls(frm.txt_mavaitro_IK1.Name), Function.clsFunction.getNameControls(frm.Name));
            frm.ShowDialog();
            row_focus = -1;
        }

        private void bbi_xoa_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (tl_containmenu_S.FocusedNode.HasChildren == false)

                {
                   
                    xoa();
                }
                else
                {
                    frmXacNhan frm = new frmXacNhan();
                    //frm.passData = new frmXacNhan.PassData(GetValue);
                    frm.ShowDialog();

                }
            }
            catch { }
        }


        public void xoa()
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa user này không.", "Thông báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {

                xoaUser();
            }
            else if (result == DialogResult.No)
            {
                //MessageBox.Show("You chose No.");
            }

        }
        private void bbi_themnguoidung_S_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frm_sysUser_S frm = new frm_sysUser_S();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TopMost = true;
            frm.passData = new frm_sysUser_S.PassDataUser(GetValueUser);
            frm.them = true;
            frm.dauma = "US";
            frm.langues = langues;
            frm.statusForm = statusForm;
            //frm.txt_mavaitro_IK1.Text = Function.clsFunction.layMa("VT", Function.clsFunction.getNameControls(frm.txt_mavaitro_IK1.Name), Function.clsFunction.getNameControls(frm.Name));
            frm.ShowDialog();
            row_focus = -1;
        }
        private void bbi_sua_S_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try               
            {
                if (tl_containmenu_S.FocusedNode.HasChildren == true)
                {                   
                    editVaiTro();                    
                }
                else
                {
                    frmXacNhan frm = new frmXacNhan();
                    frm.IDUS = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();                    
                    frm.passData = new frmXacNhan.PassData(GetValue);
                    frm.ShowDialog();                   
                }
            }
            catch { }
        }

        private void editVaiTro()
        {
            try
            {
                string ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                if (getVaiTroByUser(Function.clsFunction._iduser) != ID)
                {
                    frm_sysVaiTro_S frm = new frm_sysVaiTro_S();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.TopMost = true;
                    frm.them = false;
                    frm.mavaitro = ID;
                    frm.langues = langues;
                    frm.statusForm = statusForm;
                    frm.index = Function.clsFunction.getIndexIDinTable(ID,"mavaitro",APCoreProcess.APCoreProcess.Read("sysVaiTro"));
                    frm.passData = new frm_sysVaiTro_S.PassDataUser(GetValueUser);
                    //frm.txt_mavaitro_IK1.Text = gv_danhmuc_C.GetRowCellValue(gv_danhmuc_C.FocusedRowHandle, Function.clsFunction.getNameControls(frm.lbl_macakip_IK1.Name)).ToString();
                    frm.ShowDialog();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Bạn không đủ quyền để thay đỗi vai trò này", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Chọn dòng cần sửa", "Thông báo");
            }
        }

        private void editUser()
        {
            try
            {
                frm_sysUser_S frm = new frm_sysUser_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.TopMost = true;
                frm.langues = langues;
                frm.statusForm = statusForm;
                //frm.index = gv_danhmuc_C.FocusedRowHandle;
                frm.passData = new frm_sysUser_S.PassDataUser(GetValueUser);
                            
                frm.ID = (tl_containmenu_S.FocusedNode.GetValue(colID).ToString());

                //frm.txt_mavaitro_IK1.Text = gv_danhmuc_C.GetRowCellValue(gv_danhmuc_C.FocusedRowHandle, Function.clsFunction.getNameControls(frm.lbl_macakip_IK1.Name)).ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Chọn dòng cần sửa", "Thông báo");
            }
        }

        private void xoaVaiTro()
        {
            try
            {
                string ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                if (getVaiTroByUser(Function.clsFunction._iduser) != ID)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from sysUser where mavaitro='" + ID + "'").Rows.Count == 0)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysVaitro where mavaitro='" + ID + "'");
                        loadUser();
                    }
                    else
                        DevExpress.XtraEditors.XtraMessageBox.Show("Bạn không thể xóa vai trò này vì đã có user sử dụng ", "Thông báo");
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Bạn không đủ quyền để xóa vai trò này", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Chọn dòng cần sửa", "Thông báo");
            }
        }

        private void xoaUser()
        {
            try
            {
                ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                if (Function.clsFunction._iduser != ID )
                {
                  
                    xacthuc();
                  
                        loadUser();
                    
     
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Bạn không đủ quyền để xóa user này", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Chọn dòng cần sửa", "Thông báo");
            }
        }
        private void xacthuc()
        {
            frmXacNhan frm = new frmXacNhan();
            frm.passData = new frmXacNhan.PassData(GetValueX);
            frm.IDUS = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
            frm.ShowDialog();
        }
        #endregion
        
        #region GridEvent



        #endregion

        #region MeThods

        private void loadUser()
        {
            try
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                tl_containmenu_S.Nodes.Clear();
                string IDRoot = "", ID1 ="", ID2 ="";
                string sRoot = "", sVaiTro = "", sUser = "";
                tl_containmenu_S.BeginUnboundLoad();
                TreeListNode parentNode = null;
                dt1 = APCoreProcess.APCoreProcess.Read("select distinct sysVaiTro.mavaitro,tenvaitro from sysVaitro, sysUser where sysUser.mavaitro=sysVaitro.mavaitro and makethua='" + getVaiTroByUser(Function.clsFunction._iduser) + "' or sysVaitro.mavaitro='" + getVaiTroByUser(Function.clsFunction._iduser) + "'");
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    IDRoot = (dt1.Rows[j][0].ToString());
                    sRoot = dt1.Rows[j][1].ToString();

                    TreeListNode rootNode = tl_containmenu_S.AppendNode(new object[] { IDRoot, sRoot }, parentNode);
                    dt2 = APCoreProcess.APCoreProcess.Read("select distinct userid as mavaitro,username from sysUser where mavaitro='" + IDRoot + "'");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ID1 = (dt2.Rows[i][0].ToString());
                        sUser = dt2.Rows[i][1].ToString();
                        TreeListNode Node1 = tl_containmenu_S.AppendNode(new object[] { ID1, sUser }, rootNode);

                    }
                }
                tl_containmenu_S.EndUnboundLoad();
                tl_containmenu_S.ExpandAll();
            }
            catch { }
        }
        private string getVaiTroByUser(string US)
        {
            string vaitro = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='"+US+"'");
            if (dt.Rows.Count > 0)
            {
                vaitro = dt.Rows[0][0].ToString();
            }
            return vaitro;
        }
        private void SavetreeList()
        {

            //datasỏuce
            string sql_grid = "";
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Chức năng", "ID", "ParentID", "Tất Cả", "Truy Cập", "Thêm", "Sửa", "Xóa", "In", "Nhập", "Xuất", "mavaitro" };
            // FieldName column,
            string[] fieldname_col = new string[] { "Name", "ID", "ParentID", "allow_all", "allow_access", "allow_insert", "allow_edit", "allow_delete", "allow_print", "allow_import", "allow_export", "mavaitro" };
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "200", "10", "10", "80", "80", "80", "80", "80", "80", "80", "80", "80" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an, "False"
            string[] Column_Visible = new string[] { "ID", "ParentID", "mavaitro" };
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
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0];
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0];
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysTreeColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, tl_quyen_S.Name);
        }

        private void Load_Tree(DevExpress.XtraTreeList.TreeList tl_quyen_S1)
        {
          
            string text = langues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;

            // Hien thị Gridview
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "'");
            //try
            {
                ControlDev.FormatControls.Controls_in_Tree(tl_quyen_S1, read_Only, hien_Nav,
                dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer,
                dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption" + text].ToString().Split('/'),
                dt.Rows[0]["field_name"].ToString().Split('/'),
                dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue" + text].ToString().Split('/'),
                dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col" + text].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                dt.Rows[0]["caption_glue_VI"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["fieldname_glue_col"].ToString()),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["caption_glue_col" + text].ToString()),
                dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 

            }
            tl_quyen_S1.KeyFieldName = "ID";
            tl_quyen_S1.ParentFieldName = "ParentID";
            //tl_quyen_S1.EndUnboundLoad();
            tl_quyen_S1.CollapseAll();
            //catch (Exception ex)
            {
                //MessageBox.Show("co loi");
            }
        }
        private DataTable CreateTableEdit()
        {
        
            DataTable tbl = new DataTable();
            int a = 0, b = 0, c = 0;
            try
            {
                tbl.Columns.Add("Name", typeof(string));
                tbl.Columns.Add("ParentID", typeof(string));
                tbl.Columns.Add("ID", typeof(string));
                tbl.Columns.Add("allow_all", typeof(bool));
                tbl.Columns.Add("allow_access", typeof(bool));
                tbl.Columns.Add("allow_insert", typeof(bool));
                tbl.Columns.Add("allow_edit", typeof(bool));
                tbl.Columns.Add("allow_delete", typeof(bool));
                tbl.Columns.Add("allow_print", typeof(bool));
                tbl.Columns.Add("allow_export", typeof(bool));
                tbl.Columns.Add("allow_import", typeof(bool));
                tbl.Columns.Add("mavaitro", typeof(string));
                DataTable dtMenu = new DataTable();
                dtMenu = APCoreProcess.APCoreProcess.Read("sysMenu");
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    a = i;
                    tbl.Rows.Add(new object[] { dtMenu.Rows[i]["ten_menu" + langues].ToString(), 0, dtMenu.Rows[i]["ma_menu"].ToString(), true, true, true, true, true, true, true, true, "" });
                    DataTable dtGroupMenu = new DataTable();
                    dtGroupMenu = APCoreProcess.APCoreProcess.Read("sysGroupSubMenu where ma_menu='" + dtMenu.Rows[i]["ma_menu"].ToString() + "'");
                    for (int j = 0; j < dtGroupMenu.Rows.Count; j++)
                    {
                        b = j;
                        tbl.Rows.Add(new object[] { dtGroupMenu.Rows[j]["ten_group_menu" + langues].ToString(), dtMenu.Rows[i]["ma_menu"].ToString(), dtGroupMenu.Rows[j]["ma_group_menu"].ToString(), true, true, true, true, true, true, true, true, "" });
                        DataTable dtSubpMenu = new DataTable();
                        dtSubpMenu = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT   dbo.sysPower.allow_access,dbo.sysPower.allow_all, dbo.sysPower.allow_insert, dbo.sysPower.allow_delete, dbo.sysPower.allow_edit, dbo.sysPower.allow_print,    dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.mavaitro, dbo.sysSubMenu.ma_sub_menu, dbo.sysSubMenu.ten_sub_menu_VI,     dbo.sysSubMenu.ma_group_menu, dbo.sysSubMenu.ten_sub_menu_EN FROM         dbo.sysSubMenu INNER JOIN        dbo.sysPower ON dbo.sysSubMenu.ma_sub_menu = dbo.sysPower.ma_sub_menu where ma_group_menu='" + dtGroupMenu.Rows[j]["ma_group_menu"].ToString() + "' and sysPower.mavaitro='" + ID + "'");
                        for (int k = 0; k < dtSubpMenu.Rows.Count; k++)
                        {
                            c = k;
                            tbl.Rows.Add(new object[] { dtSubpMenu.Rows[k]["ten_sub_menu" + langues].ToString(), dtGroupMenu.Rows[j]["ma_group_menu"].ToString(), dtSubpMenu.Rows[k]["ma_sub_menu"].ToString(), dtSubpMenu.Rows[k]["allow_all"].ToString(), dtSubpMenu.Rows[k]["allow_access"].ToString(), dtSubpMenu.Rows[k]["allow_insert"].ToString(), dtSubpMenu.Rows[k]["allow_edit"].ToString(), dtSubpMenu.Rows[k]["allow_delete"].ToString(), dtSubpMenu.Rows[k]["allow_print"].ToString(), dtSubpMenu.Rows[k]["allow_export"].ToString(), dtSubpMenu.Rows[k]["allow_import"].ToString(), "" });
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show(a.ToString()+" - "+ b.ToString()+" - "+ c.ToString());
            }
            return tbl;
        }
        private void tlpq_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Node.HasAsParent(e.Node) == false)
                {
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                    {
                        if (e.Node.Nodes[i].GetValue(e.Column.Name).ToString() == "false")
                        {
                            e.Node.SetValue(e.Column.Name, false);
                            break;
                        }
                    }
                }

            }
            catch { }
        }
        private void checkNodeCon()
        {
            for (int i = 0; i < tl.Nodes.Count; i++)
            {
                for (int j = 0; j < tl.Columns.Count; j++)
                {
                    if (tl.Columns[j].Name != "ID" && tl.Columns[j].Name != "ParentID" && tl.Columns[j].Name != "Name")
                        tl.Nodes[i].SetValue(tl.Columns[j], checkChildNodes(tl.Nodes[i], tl.Columns[j].Name));
                }
            }
        }
        private bool checkChildNodes(TreeListNode node,string column)
        {
            bool flag=false;
            if (node.HasChildren == true)
            {
                foreach (TreeListNode childnode in node.Nodes)
                {
             
                      checkChildNodes(childnode,column);                   

                }
            }
            else
            {
                if ((bool)node.GetValue(column) == false)
                    flag= false;
                else
                    flag = true;
            }
            return flag;
        }
        #endregion

        private void tl_containmenu_S_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {            
           
            try
            {
                ID = (tl_containmenu_S.FocusedNode.GetValue(colID).ToString());
                if (APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + ID + "'").Rows.Count>0)
                    ID = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + ID + "'").Rows[0][0].ToString();
             
                if (tl_containmenu_S.FocusedNode.HasChildren != true)
                {
                    tl = new DevExpress.XtraTreeList.TreeList();
                   
                    addTreelistToPanel(CreateTableEdit());
             
                    //tl_quyen_S.BeginUpdate();
                    
                    //tl_quyen_S.DataSource = CreateTableEdit();
                    //tl_quyen_S.RefreshDataSource();



                    //tl_quyen_S.EndUpdate();
                   
                    //Load_Tree(tl);
                  

                }
                //else
                //{
                //    tl_quyen_S.DataSource = CreateTableEdit();
                //    tl_quyen_S.KeyFieldName = "ID";
                //    tl_quyen_S.ParentFieldName = "ParentID";
                //    tl_quyen_S.EndUnboundLoad();
                //    tl_quyen_S.CollapseAll();
                //    //Load_Tree();
                //}
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void addTreelistToPanel(DataTable dt)
        {
            tl = new DevExpress.XtraTreeList.TreeList();
            tl.ClearNodes();
            panel_contain_C.Controls.Clear();
            Load_Tree(tl);
            tl.Name = "tl_quyen";
            tl.BeginUpdate();
            tl.DataSource = dt;
       
            tl.EndUpdate();
            tl.Dock = DockStyle.Fill;
           
            panel_contain_C.Controls.Add(tl);
        }

        private void bbi_dong_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

    }
}