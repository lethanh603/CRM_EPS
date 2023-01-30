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
using DevExpress.XtraTreeList;


namespace SOURCE_FORM.Presentation
{
    public partial class frm_Grant_SH : DevExpress.XtraEditors.XtraForm
    {
        public frm_Grant_SH()
        {
            InitializeComponent();
        }

        #region Var

        public string langues = "_VI", mavaitro = "", ID = "";
        public bool statusForm = false;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DevExpress.XtraTreeList.TreeList tl = new DevExpress.XtraTreeList.TreeList();
        TreeListNode SavedFocused;
        PopupMenu menu = new PopupMenu();
        bool NeedRestoreFocused;
        bool isDeleteUser = false;
        public void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true && gv_user_C.FocusedRowHandle >= 0)
            {
                frm_sysUser_S frm = new frm_sysUser_S();
                frm.them = false;
                frm.ID = gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString();
                frm.index = Function.clsFunction.getIndexIDinTable(gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString(), "userid", APCoreProcess.APCoreProcess.Read("sysUser"));
                frm.passData = new frm_sysUser_S.PassDataUser(GetValueUser);
                frm.ShowDialog();
            }
        }
        public void GetValueX(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                string mavt = "";
                mavt = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();

                if (isDeleteUser == false)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from sysUser where mavaitro='" + mavt + "'").Rows.Count == 0)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysPower where mavaitro='" + mavt + "'");
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysRole where mavaitro='" + mavt + "'");
                        tl_containmenu_S.DeleteNode(tl_containmenu_S.FocusedNode);
                    }
                    else
                    {
                        MessageBox.Show("Vai trò này đang được sử dụng bởi user, không thể xóa", "Thông báo");
                    }
                }
                else
                {
                    if (!checkAdmin(gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString()))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysUser where userid='" + gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid") + "'");
                        gv_user_C.DeleteRow(gv_user_C.FocusedRowHandle);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn không thể xóa user này", "Thông báo");
            }
        }
        public void GetValueClockUser(bool value)
        {
            bool va_lue = value;
            if (va_lue == true && !checkAdmin(gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString()))
            {
                string mavt = "";
                mavt = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                APCoreProcess.APCoreProcess.ExcuteSQL("update  sysUser set status=0 where userid='" + gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString() + "'");
            }
            else
            {
                MessageBox.Show("Bạn không thể khóa user này", "Thông báo");
            }
        }

        public void GetValueUnClockUser(bool value)
        {
            bool va_lue = value;
            if (va_lue == true && !checkAdmin(gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString()))
            {
                string mavt = "";
                mavt = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                APCoreProcess.APCoreProcess.ExcuteSQL("update  sysUser set status=1 where userid='" + gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString() + "'");
            }
            else
            {
                MessageBox.Show("Bạn không thể khóa user này", "Thông báo");
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

        public void GetValueVaitro(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                Load_Tree(tl);
                loadRole();
                
            }
        }

        public void GetValueMenu(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                tl_containmenu_S.Nodes[0].Selected = true;
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
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("ALTER proc [dbo].[sysGrant_Role]  (@mavaitro as nvarchar(20), @makethua as nvarchar(20))   as begin select distinct sysRole.mavaitro,tenvaitro from sysRole left join sysUser on sysUser.mavaitro=sysRole.mavaitro where  (CHARINDEX(@makethua, idrecursive) > 0) or sysRole.mavaitro=@mavaitro end");
                Function.clsFunction.sysGrantUserByRole(bar_menu_C, this.Name);
                DataTable dtRoot = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dtRoot = Function.clsFunction.Excute_Proc("getRootUserID", new string[1, 2] { { "IdUser", Function.clsFunction._iduser } });
                }
                else
                {
                    dtRoot = APCoreProcess.APCoreProcess.Read("select sysUser.root  from sysUser where userid='" + Function.clsFunction._iduser + "' and root=1");
                }
                if (dtRoot.Rows.Count > 0)
                    bar_menuAdmin_C.Visible = true;
                else
                    bar_menuAdmin_C.Visible = false;
                Function.clsFunction._keylience = true;
                tl = new DevExpress.XtraTreeList.TreeList();
                langues = Function.clsFunction.langgues;
                //ConvertStringToArrayN("makhuvuc/tenkhuvuc/@makhuvuc1/tenkhuvuc1/@", "@", "/");
                ID = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + Function.clsFunction._iduser + "'").Rows[0][0].ToString();
                if (statusForm == true)
                {
                    Function.clsFunction.Save_sysControl(this, this);
                    SavetreeList();
                }
                else
                {
                    //bar button
                    tl.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(tlpq_CellValueChanged);
                    loadRole();
                    loadUser();
                    //Load_Tree(tl);
                    checkNodeCon();
                    //createpopupmenu();
                    Function.clsFunction.TranslateForm(this, this.Name);
                    Function.clsFunction.TranslateTreeColumn(tl);
                    Function.clsFunction.TranslateTreeColumn(tl_quyen_S);
                    Function.clsFunction.TranslateTreeColumn(tl_containmenu_S);

                }
            }
            catch { }
        }

        private void customPopupMenuAdmin()
        {
            // Bind the menu to a bar manager.
            menu.Manager = bar_contain_C;
            // Add two items that belong to the bar manager.
            for (int i = 0; i < bar_contain_C.Items.Count; i++)
            {
                if (bar_contain_C.Items[i].Name.Contains("_admin_S") || bar_contain_C.Items[i].Name.Contains("bbi_exit_S"))
                    menu.ItemLinks.Add(bar_contain_C.Items[i]);

            }
        }

        private void customPopupMenu()
        {

            // Bind the menu to a bar manager.
            menu.Manager = bar_contain_C;
            // Add two items that belong to the bar manager.
            for (int i = 0; i < bar_contain_C.Items.Count; i++)
            {
                if (!bar_contain_C.Items[i].Name.Contains("_admin_S") && bar_contain_C.Items[i].Name.Contains("_S"))
                    menu.ItemLinks.Add(bar_contain_C.Items[i]);

            }
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
            dt_button = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name, dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY dbo.sysControls.stt ");
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
            gctr.MainMenu = bar1;
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

        private void tl_containmenu_S_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                bool status = false;
                ID = (tl_containmenu_S.FocusedNode.GetValue(colID).ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select mavaitro, status from sysUser where userid='" + ID + "'");
                if (dt.Rows.Count > 0)
                {
                    ID = dt.Rows[0][0].ToString();
                    status = Convert.ToBoolean(dt.Rows[0][1]);
                    if (status == true)
                    {
                        bbi_clockuser_allow_edit.Caption = Function.clsFunction.transLateText("Khóa người dùng");
                    }
                    else
                    {
                        bbi_clockuser_allow_edit.Caption = Function.clsFunction.transLateText("Mở khóa người dùng");
                    }
                }
                if (tl_containmenu_S.FocusedNode.HasChildren != true || 1 == 1)
                {
                    //if (APCoreProcess.APCoreProcess.Read("select * from sysRole where mavaitro='" + tl_containmenu_S.FocusedNode.GetValue(colID) + "'").Rows.Count > 0)
                    //{
                    //    if (APCoreProcess.APCoreProcess.Read("select * from sysUser where mavaitro='" + tl_containmenu_S.FocusedNode.GetValue(colID) + "'").Rows.Count == 0)
                    //    {
                    //        return;
                    //    }

                    //}
                    tl = new DevExpress.XtraTreeList.TreeList();
                    addTreelistToPanel(CreateTableEdit());
                }
                reloadUser(ID);
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // return;
            }
        }

        private void reloadUser(String vaitro)
        {
            DataTable dtUser = APCoreProcess.APCoreProcess.Read("select userid, username from sysUser where mavaitro='" + vaitro + "'");
            gct_user_C.DataSource = dtUser;
        }

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

        private void tl_quyen_S_MouseUp(object sender, MouseEventArgs e)
        {
            menu.ItemLinks.Clear();
            TreeList tree = sender as TreeList;
            if (tree.Name != "tl_quyen")
            {
                customPopupMenu();
            }
            else
                customPopupMenuAdmin();

            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && tree.State == TreeListState.Regular)
            {
                Point pt = tree.PointToClient(MousePosition);
                TreeListHitInfo info = tree.CalcHitInfo(pt);
                if (info.HitInfoType == HitInfoType.Cell)
                {
                    SavedFocused = tree.FocusedNode;
                    int SavedTopIndex = tree.TopVisibleNodeIndex;
                    tree.FocusedNode = info.Node;
                    NeedRestoreFocused = true;
                    menu.ShowPopup(MousePosition);
                }
            }
        }

        private void popupMenu1_CloseUp(object sender, EventArgs e)
        {
            if (NeedRestoreFocused)
                tl_quyen_S.FocusedNode = SavedFocused;
        }

        private void bbi_clockuser_allow_edit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bbi_clockuser_allow_edit.Caption == Function.clsFunction.transLateText("Khóa người dùng"))
            {
                if (Function.clsFunction.MessageDelete("Thông báo xóa", "Bạn có muốn khóa user này không."))
                {
                    clockUser();
                }
            }
            else
            {
                if (Function.clsFunction.MessageDelete("Thông báo xóa", "Bạn có muốn mở khóa user này không."))
                {
                    UnclockUser();
                }
            }
        }

        private void clockUser()
        {
            try
            {
                ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                if (Function.clsFunction._iduser != ID)
                {

                    xacthuckhoa();

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

        private void UnclockUser()
        {
            try
            {
                ID = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
                if (Function.clsFunction._iduser != ID)
                {
                    xacthucUnkhoa();
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

        #endregion

        #region Button Event

        private void bbi_vaitro_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_sysRole_S frm = new frm_sysRole_S();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TopMost = true;
            frm.passData = new frm_sysRole_S.PassDataUser(GetValueVaitro);
            frm.them = true;
            frm.dauma = "VT";
            //frm.langues = langues;
            frm.statusForm = statusForm;
            frm.txt_mavaitro_IK1.Text = Function.clsFunction.layMa("VT", Function.clsFunction.getNameControls(frm.txt_mavaitro_IK1.Name), Function.clsFunction.getNameControls(frm.Name));
            frm.ShowDialog();
        }

        private void bbi_xoa_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                xacthuc();
            }
            catch { }
        }

        public void deleteUser()
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa mẫu tin này không?", "Thông báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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

        }

        private void bbi_sua_S_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                editVaiTro();
               
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
                    frm_sysRole_S frm = new frm_sysRole_S();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.TopMost = true;
                    frm.them = false;
                    frm.mavaitro = ID;
                    //frm.langues = langues;
                    frm.statusForm = statusForm;
                    frm.index = Function.clsFunction.getIndexIDinTable(ID, "mavaitro", APCoreProcess.APCoreProcess.Read("sysRole"));
                    frm.passData = new frm_sysRole_S.PassDataUser(GetValueVaitro);
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
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysRole where mavaitro='" + ID + "'");
                        loadRole();
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
                ID = gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString();
                if (Function.clsFunction._iduser != ID)
                {
                    xacthuc();
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
            frmConfirm frm = new frmConfirm();
            frm.passData = new frmConfirm.PassData(GetValueX);
            frm.IDUS = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
            frm.ShowDialog();
        }

        private void xacthuckhoa()
        {
            frmConfirm frm = new frmConfirm();
            frm.passData = new frmConfirm.PassData(GetValueClockUser);
            frm.IDUS = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
            frm.ShowDialog();
        }

        private void xacthucUnkhoa()
        {
            frmConfirm frm = new frmConfirm();
            frm.passData = new frmConfirm.PassData(GetValueUnClockUser);
            frm.IDUS = tl_containmenu_S.FocusedNode.GetValue(colID).ToString();
            frm.ShowDialog();
        }

        private void bbi_dong_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;

        }

        private void bbi_createmenu_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (tl.FocusedNode.Level == 0)
            {
                frm_sysMenu_S frm = new frm_sysMenu_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysMenu_S.PassData(GetValueMenu);
                frm.them = true;
                frm.dauma = "VT";
                frm.IDMenu = tl.FocusedNode.GetValue("ID").ToString();
                frm.IDMenu = "";
                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            if (tl.FocusedNode.Level == 1)
            {
                frm_sysGroupSubMenu frm = new frm_sysGroupSubMenu();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysGroupSubMenu.PassData(GetValueMenu);
                frm.them = true;
                frm.dauma = tl.FocusedNode.GetValue("ParentID").ToString() + ".";
                frm.IDGroupMenu = tl.FocusedNode.GetValue("ID").ToString();
                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            if (tl.FocusedNode.Level == 2)
            {
                frm_sysSubMenu_S frm = new frm_sysSubMenu_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysSubMenu_S.PassData(GetValueMenu);
                frm.them = true;
                frm.IDSubMenu = tl.FocusedNode.GetValue("ID").ToString();
                frm.dauma = tl.FocusedNode.GetValue("ParentID").ToString() + ".";
                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
        }

        private void bbi_editMenu_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            // MessageBox.Show(tl.FocusedNode.GetValue("ParentID").ToString());
            if (tl.FocusedNode.Level == 0)
            {
                frm_sysMenu_S frm = new frm_sysMenu_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysMenu_S.PassData(GetValueMenu);
                frm.them = false;
                frm.dauma = "VT";
                frm.IDMenu = tl.FocusedNode.GetValue("ID").ToString();

                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            if (tl.FocusedNode.Level == 1)
            {
                frm_sysGroupSubMenu frm = new frm_sysGroupSubMenu();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysGroupSubMenu.PassData(GetValueMenu);
                frm.them = false;

                frm.IDGroupMenu = tl.FocusedNode.GetValue("ID").ToString();
                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            if (tl.FocusedNode.Level == 2)
            {
                frm_sysSubMenu_S frm = new frm_sysSubMenu_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.passData = new frm_sysSubMenu_S.PassData(GetValueMenu);
                frm.them = false;
                frm.IDSubMenu = tl.FocusedNode.GetValue("ID").ToString();
                frm.IDMenu = tl.FocusedNode.GetValue("ParentID").ToString();
                frm.dauma = "VT";
                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
        }

        private void bbi_delete_menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (tl.FocusedNode.Level == 0)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select IDMenu from sysGroupSubMenu where IDMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                if (dt.Rows.Count > 0)
                    Function.clsFunction.MessageInfo("Thông báo", "Menu này đã được sử dụng không được xóa");
                else
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này không ?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete sysMenu where IDMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                        tl.DeleteNode(tl.FocusedNode);
                    }
                }
            }
            if (tl.FocusedNode.Level == 1)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select IDSubMenu from sysSubMenu where IDGroupMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                if (dt.Rows.Count > 0)
                    Function.clsFunction.MessageInfo("Thông báo", "Menu này đã được sử dụng không được xóa");
                else
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này không ?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete sysGroupSubMenu where IDGroupMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                        tl.DeleteNode(tl.FocusedNode);
                    }
                }
            }
            if (tl.FocusedNode.Level == 2)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select IDSubMenu from sysSubMenu where IDSubMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                if (dt.Rows.Count > 0)
                    Function.clsFunction.MessageInfo("Thông báo", "Menu này đã được sử dụng không được xóa");
                else
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này không ?"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete sysSubMenu where IDSubMenu='" + tl.FocusedNode.GetValue("ID").ToString() + "'");
                        DeleteSystem(tl.FocusedNode.GetValue("ID").ToString());
                        tl.DeleteNode(tl.FocusedNode);
                    }
                }
            }
        }
        #endregion

        #region GridEvent



        #endregion

        #region MeThods

        private void DeleteSystem(string idSubMenu)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from SysSubMenu where IDSubMenu='" + idSubMenu + "'");
            if (dt.Rows.Count > 0)
            {
                // delete table
                try
                {
                    string[] arrTable = dt.Rows[0]["tablePre"].ToString().Split('/');
                    for (int i = 0; i < arrTable.Length; i++)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("drop table " + arrTable[i].ToString());
                    }
                }
                catch { }
                // delete gridcolumn
                try
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysGridColumns where form_name='" + dt.Rows[0]["FormName"].ToString() + "'");
                }
                catch { }
                // delete sysControls
                try
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysControls where form_name='" + dt.Rows[0]["FormName"].ToString() + "'");
                }
                catch { }
            }
        }

        private void addTreelistToPanel(DataTable dt)
        {
            try
            {
                tl = new DevExpress.XtraTreeList.TreeList();
                tl.ClearNodes();
                panel_contain_C.Controls.Clear();
                Load_Tree(tl);
                tl.Name = "tl_quyen";
                tl.BeginUpdate();
                tl.DataSource = dt;
                //tl.Click += new EventHandler(tl_quyen_S_Click);
                tl.EndUpdate();
                tl.Dock = DockStyle.Fill;
                tl.MouseUp += new MouseEventHandler(tl_quyen_S_MouseUp);
                panel_contain_C.Controls.Add(tl);
            }
            catch { }
        }

        private void loadUserBK()
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
                //dt1 = APCoreProcess.APCoreProcess.Read("select distinct sysRole.mavaitro,tenvaitro from sysRole, sysUser where sysUser.mavaitro=sysRole.mavaitro and makethua='" + getVaiTroByUser(Function.clsFunction._iduser) + "' or sysRole.mavaitro='" + getVaiTroByUser(Function.clsFunction._iduser) + "'");
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt1 = APCoreProcess.APCoreProcess.Read("select distinct sysRole.mavaitro,tenvaitro from sysRole left join sysUser on sysUser.mavaitro=sysRole.mavaitro where (CHARINDEX(@makethua, idrecursive) > 0) or sysRole.mavaitro=@mavaitro", "sysGrant_Role", new string[2, 2] { { "makethua", getVaiTroByUser(Function.clsFunction._iduser) }, { "mavaitro", getVaiTroByUser(Function.clsFunction._iduser) } }, " (@mavaitro as nvarchar(20), @makethua as nvarchar(20)) ");
                }
                else
                {
                    dt1 = APCoreProcess.APCoreProcess.Read("select distinct sysRole.mavaitro,tenvaitro from sysRole left join sysUser where sysUser.mavaitro=sysRole.mavaitro where (CHARINDEX('" + getVaiTroByUser(Function.clsFunction._iduser) + "', idrecursive) > 0)  or sysRole.mavaitro='" + getVaiTroByUser(Function.clsFunction._iduser) + "'");
                }
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    IDRoot = (dt1.Rows[j][0].ToString());
                    sRoot = dt1.Rows[j][1].ToString();

                    TreeListNode rootNode = tl_containmenu_S.AppendNode(new object[] { IDRoot, sRoot }, parentNode);
                    //dt2 = APCoreProcess.APCoreProcess.Read("select distinct userid as mavaitro,username from sysUser where mavaitro='" + IDRoot + "'");
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        dt2 = APCoreProcess.APCoreProcess.Read("select distinct userid as mavaitro,username from sysUser where  (CHARINDEX(@makethua, idrecursive) > 0) ", "sysGrant_getUser", new string[1, 2] { { "mavaitro", IDRoot } }, " (@mavaitro as nvarchar(20)) ");
                    }
                    else
                    {
                        dt2 = APCoreProcess.APCoreProcess.Read("select distinct userid as mavaitro,username from sysUser where mavaitro='" + IDRoot + "'");
                    }
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

        private void loadUser()
        {
            try
            {

                ID = (tl_containmenu_S.FocusedNode.GetValue(colID).ToString());
                reloadUser(ID);
            }
            catch { }
        }

        private void loadRole()
        {
            try
            {
                //tl_containmenu_S = new DevExpress.XtraTreeList.TreeList();
                DataTable dt1 = new DataTable();
                //tl_containmenu_S.Nodes.Clear();                
                tl_containmenu_S.BeginUpdate();
                //tl_containmenu_S.UnlockReloadNodes();
                dt1 = APCoreProcess.APCoreProcess.Read("SELECT     mavaitro, tenvaitro, makethua FROM         dbo.sysRole ");
                tl_containmenu_S.KeyFieldName = "mavaitro";
                tl_containmenu_S.ParentFieldName = "makethua";
                tl_containmenu_S.DataSource = dt1;
                tl_containmenu_S.RefreshDataSource();                
                tl_containmenu_S.EndUpdate();
                //tl_containmenu_S.LockReloadNodes();
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
                dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                dt.Rows[0]["field_name"].ToString().Split('/'),
                dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["fieldname_glue_col"].ToString()),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["caption_glue_col"].ToString()),
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
                //dtMenu = APCoreProcess.APCoreProcess.Read("sysMenu");
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dtMenu = Function.clsFunction.Excute_Proc("GrantMenu", new string[0, 0]);
                }
                else
                {
                    dtMenu = APCoreProcess.APCoreProcess.Read("SELECT  IDmenu, menuname from sysMenu");
                }
                for (int i = 0; i < dtMenu.Rows.Count; i++)
                {
                    a = i;
                    tbl.Rows.Add(new object[] { dtMenu.Rows[i]["menuname"].ToString(), 0, dtMenu.Rows[i]["IDMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                    DataTable dtGroupMenu = new DataTable();
                    // dtGroupMenu = APCoreProcess.APCoreProcess.Read("sysGroupSubMenu where IDMenu='" + dtMenu.Rows[i]["IDMenu"].ToString() + "'");
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        dtGroupMenu = Function.clsFunction.Excute_Proc("GrantGroupMenu", new string[1, 2] { { "id", dtMenu.Rows[i]["IDMenu"].ToString() } });
                    }
                    else
                    {
                        dtGroupMenu = APCoreProcess.APCoreProcess.Read("SELECT  IDGroupMenu, GroupMenuName from sysGroupSubMenu where IDMenu= '" + dtMenu.Rows[i]["IDMenu"].ToString() + "'");
                    }
                    for (int j = 0; j < dtGroupMenu.Rows.Count; j++)
                    {
                        b = j;
                        tbl.Rows.Add(new object[] { dtGroupMenu.Rows[j]["GroupMenuName"].ToString(), dtMenu.Rows[i]["IDMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                        DataTable dtSubpMenu = new DataTable();
                        //dtSubpMenu = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT   dbo.sysPower.allow_access,dbo.sysPower.allow_all, dbo.sysPower.allow_insert, dbo.sysPower.allow_delete, dbo.sysPower.allow_edit, dbo.sysPower.allow_print,    dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.mavaitro, dbo.sysSubMenu.IDSubMenu, dbo.sysSubMenu.NameSubMenu,     dbo.sysSubMenu.IDGroupMenu FROM         dbo.sysSubMenu INNER JOIN        dbo.sysPower ON dbo.sysSubMenu.IDSubMenu = dbo.sysPower.IDSubMenu where IDGroupMenu='" + dtGroupMenu.Rows[j]["IDGroupMenu"].ToString() + "' and sysPower.mavaitro='" + ID + "'");
                        if (APCoreProcess.APCoreProcess.IssqlCe == false)
                        {
                            dtSubpMenu = Function.clsFunction.Excute_Proc("GrantSubMenu", new string[2, 2] { { "idGroup", dtGroupMenu.Rows[j]["IDGroupMenu"].ToString() }, { "vaitro", ID } });
                        }
                        else
                        {
                            dtSubpMenu = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT   sysPower.allow_access,sysPower.allow_all, sysPower.allow_insert, sysPower.allow_delete, sysPower.allow_edit,sysPower.allow_print,    sysPower.allow_import, sysPower.allow_export, sysPower.mavaitro, sysSubMenu.IDSubMenu, sysSubMenu.NameSubMenu,     sysSubMenu.IDGroupMenu FROM         sysSubMenu INNER JOIN        sysPower ON sysSubMenu.IDSubMenu = sysPower.IDSubMenu where IDGroupMenu='" + dtGroupMenu.Rows[j]["IDGroupMenu"].ToString() + "' and sysPower.mavaitro='" + ID + "'");
                        }
                        for (int k = 0; k < dtSubpMenu.Rows.Count; k++)
                        {
                            c = k;
                            tbl.Rows.Add(new object[] { dtSubpMenu.Rows[k]["NameSubMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), dtSubpMenu.Rows[k]["IDSubMenu"].ToString(), dtSubpMenu.Rows[k]["allow_all"].ToString(), dtSubpMenu.Rows[k]["allow_access"].ToString(), dtSubpMenu.Rows[k]["allow_insert"].ToString(), dtSubpMenu.Rows[k]["allow_edit"].ToString(), dtSubpMenu.Rows[k]["allow_delete"].ToString(), dtSubpMenu.Rows[k]["allow_print"].ToString(), dtSubpMenu.Rows[k]["allow_export"].ToString(), dtSubpMenu.Rows[k]["allow_import"].ToString(), "" });
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(a.ToString() + " - " + b.ToString() + " - " + c.ToString());
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

        private bool checkChildNodes(TreeListNode node, string column)
        {
            bool flag = false;
            if (node.HasChildren == true)
            {
                foreach (TreeListNode childnode in node.Nodes)
                {

                    checkChildNodes(childnode, column);

                }
            }
            else
            {
                if ((bool)node.GetValue(column) == false)
                    flag = false;
                else
                    flag = true;
            }
            return flag;
        }

        #endregion

        #region GridEvent
        private void bbi_allow_insert_config_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = "";
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Error " + ex.Message);
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

        #endregion

        private void bbi_deleteuser_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gv_user_C.FocusedRowHandle >= 0)
            {
                isDeleteUser = true;
                deleteUser();
            }
        }

        private void bbi_edituser_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmConfirm frm = new frmConfirm();
                frm.IDUS = gv_user_C.GetRowCellValue(gv_user_C.FocusedRowHandle, "userid").ToString();
                frm.passData = new frmConfirm.PassData(GetValue);
                frm.ShowDialog();
            }
            catch
            {
                Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn user cần sửa");
            }
        }
    }
}