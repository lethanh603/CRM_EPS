//Thanh : 20131305
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraBars;
using Function;
using System.Resources;
using SOURCE_FORM.Properties;


namespace SOURCE_FORM.Presentation
{
    public partial class frm_NewFormList_SH : DevExpress.XtraEditors.XtraForm
    {
        public frm_NewFormList_SH()
        {
            InitializeComponent();
        }

        #region Var

        public string langues = "_VI";
        public bool statusForm = true;
        public string dauma = "";
        private string sql = "select * from NewFormAdd";
        private int row_focus = -1;  
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        public void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                //Load_Grid();    
                gctr_danhmuc_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
                if (row_focus != -1)
                    gv_danhmuc_C.FocusedRowHandle = row_focus;
                else
                    gv_danhmuc_C.FocusedRowHandle = gv_danhmuc_C.RowCount - 1;
                row_focus = -1;
            }
        }
        #endregion

        #region Load

        private void frm_nhapdvt_SH_Load(object sender, EventArgs e)
        {
            langues = Function.clsFunction.langgues;           
                   //bbi_delete_S.Glyph = GetImageByName("add");

                   if (statusForm == true)
                   {
                       Function.clsFunction.Save_sysControl(this, this);
                       SaveGridControls();
                   }
                   else
                   {
                       //bar button

                       //
                       Function.clsFunction.Text_Control(this, langues);
                       Load_Grid();                    
                       gv_danhmuc_C.OptionsView.ShowAutoFilterRow = true;
                       //createpopupmenu();

                   }

        }

        private void createpopupmenu()
        {
            int index = 0;
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name,  dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.userid = N'" + Function.clsFunction._iduser + "') ORDER BY dbo.sysControls.stt ");
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
            contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
            contextMenuStrip.Show(this, PointToClient(MousePosition));
            gctr_danhmuc_C.ContextMenuStrip = contextMenuStrip;
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Contains("xoa"))
            {
                btn_xoa_S_Click(sender, e);
            }
            if (e.ClickedItem.Name.Contains("them"))
            {
                btn_them_S_Click(sender, e);
            }
            if (e.ClickedItem.Name.Contains("sua"))
            {
                btn_sua_S_Click(sender, e);
            }
            if (e.ClickedItem.Name.Contains("in"))
            {
                btn_in_S_Click(sender, e);
            }
            if (e.ClickedItem.Name.Contains("thoat"))
            {
                btn_thoat_S_Click(sender, e);
            }
            if (e.ClickedItem.Name.Contains("xuat"))
            {
                btn_xuat_S_Click(sender, e);
            }
        }

        private void load_button(Form form_name, GroupControl gctr, string userid)
        {
            gctr.Controls.Clear();
            int step = -1;
            int index = 0;
            int pos = 0;
            string masubmenu = "";
            System.Data.DataTable dt_button = new System.Data.DataTable();
            dt_button = APCoreProcess.APCoreProcess.Read("SELECT  distinct dbo.sysControls.control_name, dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + form_name.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(userid) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt_button.Rows.Count; i++)
            {
                System.Data.DataTable dtMaMeNu = new System.Data.DataTable();
                dtMaMeNu = APCoreProcess.APCoreProcess.Read("select top 1 IDSubMenu from sysSubMenu where form_name='" + (this.Name) + "'");
                if (dtMaMeNu.Rows.Count > 0)
                    masubmenu = dtMaMeNu.Rows[0][0].ToString();
                System.Data.DataTable dtPw = new System.Data.DataTable();
                dtPw = APCoreProcess.APCoreProcess.Read("select * from sysPower where IDSubMenu ='" + masubmenu + "' and mavaitro='" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "'");
                if (KiemTraQuyen(dt_button.Rows[i]["control_name"].ToString().Trim(), dtPw.Rows[0]))
                {
                    step++;
                    index++;

                    SimpleButton btn = new SimpleButton();

                    btn.Text = dt_button.Rows[i]["text" + langues].ToString().Trim();

                    btn.Name = dt_button.Rows[i]["control_name"].ToString().Trim();
                    ControlDev.FormatControls.FormatButtonImage(btn, btn.Name + ".png", 20, 20);
                    Size size = btn.CalcBestSize();
                    size.Width = Math.Max(size.Width, btn.Width);
                    size.Height = Math.Max(size.Height, btn.Height);
                    btn.Size = size;
                    btn.SetBounds(pos, 0, size.Width - 30, size.Height + 0);
                    pos += size.Width - 29;

                    btn.Click += new EventHandler(btn_Click);
                    gctr.Controls.Add(btn);
                    gctr.Height = size.Height + 0;
                }
            }
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
            if (control_name.Contains("_thoat") == true)
            {
                flag = true;
            }
            if (control_name.Contains("_huy") == true)
            {
                if ((bool)dr["allow_insert"] == true || (bool)dr["allow_edit"] == true)
                    flag = true;
                else
                    flag = false;
            }

            if (control_name.Contains("_naplai") == true)
            {
                flag = true;
            }

            if (control_name.Contains("_luu") == true)
            {
                if ((bool)dr["allow_insert"] == true || (bool)dr["allow_edit"] == true)
                    flag = true;
                else
                    flag = false;
            }

            return flag;
        }

        #endregion

        #region Event

        private void bbi_insert_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("");
        }

        private void btn_xoa_S_Click(object sender, EventArgs e)
        {
  
        }

        private void btn_sua_S_Click(object sender, EventArgs e)
        {
            try
            {
                frm_sysSubMenu_S frm = new frm_sysSubMenu_S();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.TopMost = true;

                frm.langues = langues;
                frm.statusForm = statusForm;
                frm.index = gv_danhmuc_C.FocusedRowHandle;
                frm.passData = new frm_sysSubMenu_S.PassData(GetValue);
                frm.lbl_IDSubMenu_IK1.Text = gv_danhmuc_C.GetRowCellValue(gv_danhmuc_C.FocusedRowHandle, Function.clsFunction.getNameControls(frm.lbl_IDSubMenu_IK1.Name)).ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Chọn dòng cần sửa", "Thông báo");
            }
        }

        private void btn_them_S_Click(object sender, EventArgs e)
        {
            frm_sysSubMenu_S frm = new frm_sysSubMenu_S();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.TopMost = true;

            frm.passData = new frm_sysSubMenu_S.PassData(GetValue);
            frm.them = true;
            frm.dauma = dauma;
            frm.langues = langues;
            frm.statusForm = statusForm;
            frm.lbl_IDSubMenu_IK1.Text = Function.clsFunction.layMa(dauma, Function.clsFunction.getNameControls(frm.lbl_IDSubMenu_IK1.Name), Function.clsFunction.getNameControls(frm.Name));
            frm.ShowDialog();
            row_focus = -1;
        }

        private void btn_in_S_Click(object sender, EventArgs e)
        {  



        }

        private void btn_thoat_S_Click(object sender, EventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void btn_naplai_S_Click(object sender, EventArgs e)
        {
    
        }

        private void btn_Click(object sender, EventArgs e)
        {
            
            SimpleButton btn = (SimpleButton)sender;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     field_name, type, form_name FROM         dbo.sysControls WHERE     (type = N'SimpleButton') AND (form_name = N'" + this.Name + "')");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (btn.Name.Contains(dt.Rows[i]["field_name"].ToString().Trim()))
                {
                    if (btn.Name.Contains("xoa"))
                    {
                        btn_xoa_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("them"))
                    {
                        btn_them_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("naplai"))
                    {
                        btn_naplai_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("sua"))
                    {
                        btn_sua_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("in"))
                    {
                        btn_in_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("thoat"))
                    {
                        btn_thoat_S_Click(sender, e);
                    }
                    if (btn.Name.Contains("xuat"))
                    {
                        btn_xuat_S_Click(sender, e);
                    }
                }
            }

        }

        private void btn_xuat_S_Click(object sender, EventArgs e)
        {
            Xuat();
            //export();

        }

        private void Xuat()
        {
           
        }

        #endregion      

        #region GridEvent

        private void gctr_danhmuc_C_Click(object sender, EventArgs e)
        {
            row_focus = gv_danhmuc_C.FocusedRowHandle;
        }

        private void gv_danhmuc_C_DoubleClick(object sender, EventArgs e)
        {
            btn_sua_S_Click(sender, e);
        }

        private void gctr_danhmuc_C_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name,  dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.userid = N'" + Function.clsFunction._iduser + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
                {
                    if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
                    {

                        {
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("xoa"))
                            {
                                btn_xoa_S_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("them"))
                            {
                                btn_them_S_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("sua"))
                            {
                                btn_sua_S_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("in"))
                            {
                                btn_in_S_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("thoat"))
                            {
                                btn_thoat_S_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("xuat"))
                            {
                                btn_xuat_S_Click(sender, e);
                            }
                        }
                    }
                }
            }
        }

        private void gctr_danhmuc_C_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button==System.Windows.Forms.MouseButtons.Right)
                createpopupmenu();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
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
                    e.Info.DisplayText = clsFunction.transLateText("STT");
                }

                e.Painter.DrawObject(e.Info);
                clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
                e.Handled = true;
            }
            catch { }
        }


        #endregion

        #region Methods

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Main Menu", "Group Menu", "Mã Menu","Menu", "Form Name","Namespace","Show","Add tap","Index","Image Name" };
            // FieldName column
            string[] fieldname_col = new string[] { "IDMenu", "IDGroupMenu", "IDSubMenu","ten_sub_menu_VI", "form_name","namespace","Showmenu","addtappage","index","image" };
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "LookUpEditColumn", "LookUpEditColumn", "TextColumn", "TextColumn", "TextColumn", "TextColumn","CheckColumn","CheckColumn", "TextColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "200", "200", "100", "200","100","100","80","80", "50","80"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
      
            string[] Column_Visible = new string[] { };
            // datasource lookupEdit
            string[] sql_lue = new string[] {"sysMenu","sysGroupSubMenu" };
            // Caption lookupEdit
            string[] caption_lue = new string[] {"ten_menu","GroupMenuName" };
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] { "IDMenu", "IDGroupMenu" };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[2, 2]{{"Mã Menu","Menu"},{"Mã Group","Group"}};
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[2, 2] { { "IDMenu", "MenuName" }, { "IDGroupMenu", "GroupMenuName" } };
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
                Function.clsFunction.Save_sysGridColumns(this,
               caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
               fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
               fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_danhmuc_C.Name);
        }

        private void Load_Grid()
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
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "'");
            //try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_danhmuc_C, read_Only, hien_Nav,
                dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gctr_danhmuc_C,
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
            //catch (Exception ex)
            {
                //MessageBox.Show("co loi");
            }
        }

        public Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }



        #endregion

  
    }
}