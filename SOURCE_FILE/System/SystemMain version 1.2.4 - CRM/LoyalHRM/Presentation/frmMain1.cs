using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Localization;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Reflection;
using System.IO;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using ControlDev;
using System.Data.SqlClient;
using DevExpress.XtraNavBar;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using DevExpress.Utils.Drawing;
using DevExpress.XtraNavBar.ViewInfo;
using System.Net;
using System.Diagnostics;

namespace LoyalHRM.Presentation
{
    public partial class frmMain1 : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frmMain1()
        {
            InitializeComponent();
        }
        #endregion

        #region Var
        double second = 0;
        string text = "";
        int timer1_dem = 0;
        int kitu = 0;
      
        string x, y;
        public string id_user = "";     

        System.Data.DataTable dtConfig = new System.Data.DataTable();
        public string langues = "_VI";
        private DevExpress.Utils.WaitDialogForm waitDialog = null;
        ProgressBarControl progressBarControl1 = new ProgressBarControl();
        #endregion

        #region FormEvent

        private void frmMain_Load(object sender, EventArgs e)
        {
            bts_DangXuat_S.Caption = Function.clsFunction.transLateText(bts_DangXuat_S.Caption);
            bts_Thoat_S.Caption = Function.clsFunction.transLateText(bts_Thoat_S.Caption);
            bts_Skin_S.Caption = Function.clsFunction.transLateText(bts_Skin_S.Caption);       
            loadImage();
            //dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
            dtConfig = APCoreProcess.APCoreProcess.Read("sysConfig");
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = dtConfig.Rows[0]["skin"].ToString();
            AddSkin();
            Load_StatusBar();
            text = Function.clsFunction.transLate(dtConfig.Rows[0]["message"].ToString());
            bstcongty1.Caption = (dtConfig.Rows[0]["company"].ToString());
            text = text.ToString().ToUpper();
         
            LoadMenu();   
            Function.clsFunction.TranslateForm(this, this.Name);
            //SECURITY.clsSecurity.setStart();
            this.Text = (dtConfig.Rows[0]["softname"].ToString());
            foreach (var process in Process.GetProcessesByName("UPDATE_ONLINE.exe"))
            {
                process.Kill();
            }
       
        }
        
        #endregion

        #region Function

        private bool Quyen(string sql)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
                flag = true;
            return flag;
        }

        private void RunText()
        {
            x = "";
            for (int i = kitu; i < text.Length; i++)
            {
                x += text[i];
                if (i == kitu)
                    y += text[i];
            }
            kitu++;
            bstThongbao1.Caption = x + " " + y;
            if (kitu == text.Length)
            {
                timer2.Stop();
                kitu = 0;
                y = "";
            }
        }

        private void Call_Form(string dll, string fname)
        {         
            try
            {
                if (dll != "")
                {
                    string serverPath = Application.StartupPath;
                    //if (APCoreProcess.APCoreProcess.Read("sysConfig").Rows.Count > 0)
                    //    serverPath = APCoreProcess.APCoreProcess.Read("sysConfig").Rows[0]["serverPath"].ToString();
                    //else
                    //    serverPath = Application.StartupPath;
                    Assembly assembly = Assembly.LoadFile(serverPath + @"\" + dll + ".dll");
                  
                    Type type = assembly.GetType(dll + ".Presentation." + fname);
                    object obj = Activator.CreateInstance(type);
                    Form frm = obj as Form;
                    if (frm != null)
                    {
                        DataTable dt = new DataTable();
                        dt = APCoreProcess.APCoreProcess.Read("select * from syssubmenu where formname='" + fname + "'");
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["addtappage"].ToString() == "False")
                            {
                        
                                frm.StartPosition = FormStartPosition.CenterScreen;
                                frm.ShowDialog();
                            }
                            else
                            {
                                frm.TopLevel = false;                            
                                frm.Dock = DockStyle.Fill;
                                frm.FormBorderStyle = FormBorderStyle.None;
                                Function.clsFunction.langgues = langues;
                                Function.clsFunction._iduser = id_user;
                                DevExpress.XtraTab.XtraTabPage page = new DevExpress.XtraTab.XtraTabPage();
                                frm.WindowState = FormWindowState.Maximized;
                                page.Controls.Add(frm);                             
                                page.Text =Function.clsFunction.transLateText( dt.Rows[0]["NameSubMenu"].ToString());
                                page.Name = frm.Name;
                                page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                                page.MouseMove += new MouseEventHandler(TabControl_MouseMove);
                                xtraTabmain.TabPages.Add(page);                                
                                xtraTabmain.SelectedTabPageIndex = xtraTabmain.TabPages.Count;
                                frm.Show();
                            }
                        }
                    }
                }
                else
                {
                    if (fname == "logout")
                    {
                        this.Hide();
                        frmLogin frm = new frmLogin();
                        frm.ShowDialog();

                    }

                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo( "Thông báo","Chương trình chưa hỗ trợ tính năng này, vui lòng liên hệ nhà cung cấp \n"+ex.Message);

            }
        }

        private void TabControl_MouseMove(object sender, MouseEventArgs e)
        {
            xtraTabmain.TabPages.RemoveAt(xtraTabmain.SelectedTabPageIndex);
        }

        private void AddSkin()
        {
            // add barlistItem vao ribbon
            DevExpress.XtraBars.BarListItem list = new DevExpress.XtraBars.BarListItem();
            list.Caption = "sKin";
            list.Name = "sKin";
            // add item vao list
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                list.Strings.Add(cnt.SkinName);
            }
            list.ShowChecks = true;
            Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
            if (System.IO.File.Exists(Application.StartupPath + "\\Images\\paint.png"))
            {
                image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\paint.png").Clone();
                image.MakeTransparent(Color.Fuchsia);
                list.Glyph = image;
            }
            list.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(Skin_ItemClick);
            //ribbon.Toolbar.ItemLinks.Add(list);
            bts_Skin_S.ItemLinks.Add(list);

        }

        void itemFrwNav_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                xtraTabmain.SelectedTabPageIndex = Function.clsFunction.sotap + 1;
            }
            catch { }
        }
        void itemBackNav_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                xtraTabmain.SelectedTabPageIndex = Function.clsFunction.sotap - 1;
            }
            catch (Exception ex) { }
        }
        private void Load_StatusBar()
        {
            bstNgay1.Caption = DateTime.Now.ToString("dd/MM/yyyy");
            bstTime1.Caption = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            second = Convert.ToDouble(DateTime.Now.Hour) * 60 * 60 + Convert.ToDouble(DateTime.Now.Minute) * 60 + Convert.ToDouble(DateTime.Now.Second);
            bstUser1.Caption = Function.clsFunction._user;
        }
               
        
        private void LoadMenu()
        {
            string _mamenu = "";
            int  iGroup = -1;
            nav_menu_C.Groups.Clear();
            nav_menu_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;            
            //DataTable dt_menu = APCoreProcess.APCoreProcess.Read("SELECT  DISTINCT    TOP (100) PERCENT sysMenu.IDMenu, sysMenu.MenuName,  sysMenu.show,  sysGroupSubMenu.IDGroupMenu, sysGroupSubMenu.GroupMenuName, sysSubMenu.IDSubMenu, sysSubMenu.NameSubMenu,  sysSubMenu.formname, sysSubMenu.pos, sysSubMenu.showmenu, sysSubMenu.image FROM         sysSubMenu INNER JOIN  sysPower ON sysSubMenu.IDSubMenu = sysPower.IDSubMenu INNER JOIN   sysGroupSubMenu ON sysSubMenu.IDGroupMenu = sysGroupSubMenu.IDGroupMenu INNER JOIN  sysMenu ON sysGroupSubMenu.IDMenu = sysMenu.IDMenu WHERE     (sysPower.allow_access = 1) AND (sysSubMenu.showmenu = 1) AND (sysMenu.show = 1) and sysPower.mavaitro='" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "' ORDER BY sysMenu.IDMenu, sysGroupSubMenu.IDGroupMenu, sysSubMenu.IDSubMenu, sysSubMenu.pos");
            string[,] arr=new string[1,2] {{"id",Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser)}};
            DataTable dt_menu = new DataTable();
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                dt_menu = Function.clsFunction.Excute_Proc("loadMenu", arr);
            }
            else
            {
                dt_menu = APCoreProcess.APCoreProcess.Read("SELECT   sysMenu.IDMenu, sysMenu.MenuName,  sysMenu.show,  sysGroupSubMenu.IDGroupMenu, sysGroupSubMenu.GroupMenuName, sysSubMenu.IDSubMenu, sysSubMenu.NameSubMenu,  sysSubMenu.formname, sysSubMenu.pos,  sysSubMenu.showmenu, sysSubMenu.image, sysSubMenu.icon FROM         sysSubMenu INNER JOIN  sysPower ON sysSubMenu.IDSubMenu = sysPower.IDSubMenu  INNER JOIN   sysGroupSubMenu ON sysSubMenu.IDGroupMenu = sysGroupSubMenu.IDGroupMenu INNER JOIN  sysMenu ON  sysGroupSubMenu.IDMenu = sysMenu.IDMenu WHERE     (sysPower.allow_access = 1) AND (sysSubMenu.showmenu = 1) AND (sysMenu.show = 1)  and sysPower.mavaitro='" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "' ORDER BY sysMenu.IDMenu,  sysGroupSubMenu.IDGroupMenu, sysSubMenu.IDSubMenu, sysSubMenu.pos");
            }
            DevExpress.XtraNavBar.NavBarGroup navg = new DevExpress.XtraNavBar.NavBarGroup();
            for (int i = 0; i < dt_menu.Rows.Count; i++)
            {
               
                if (_mamenu != dt_menu.Rows[i]["IDMenu"].ToString())
                {
                    //add menu
                    navg = new DevExpress.XtraNavBar.NavBarGroup();
                    navg.Name = dt_menu.Rows[i]["IDMenu"].ToString();
                    nav_menu_C.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;              
                    
                        navg.Caption= Function.clsFunction.transLateText(dt_menu.Rows[i]["MenuName"].ToString());                   
                                      // add group
                    DevExpress.XtraNavBar.NavBarItem nbi = new DevExpress.XtraNavBar.NavBarItem();
                    nbi.Name = dt_menu.Rows[i]["formname"].ToString();                  
                    {
                        nbi.Caption = Function.clsFunction.transLateText(dt_menu.Rows[i]["NameSubMenu"].ToString());
                    }

                    Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
                    if (Function.clsFunction.checkFileExist(Application.StartupPath + "\\Images\\" + nbi.Name + ".png"))
                        image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\" + nbi.Name + ".png");
                    else
                        image = (Bitmap)LoyalHRM.Properties.Resources.page_blank.Clone();
                    //image = (Bitmap)Image.FromFile(picData.ToString(),false);
                    if (image != null)
                    {
                        //using (MemoryStream ms = new MemoryStream(picData))
                        {
                            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            //image = bmp;

                            image.MakeTransparent(Color.Transparent);
                            Size s = new Size(20, 20);
                            nbi.SmallImage = ControlDev.FormatControls.resizeImage(image, s);

                        }
                    }
                
                    nbi.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(Menu_ItemClick);
                    nbi.Appearance.BackColor = Color.FromArgb(0x66, 0xFF, 0x00);
                    nbi.Appearance.Options.UseBackColor = true;
                    nbi.Appearance.BorderColor=Color.Black;
                    nbi.Appearance.Options.UseBorderColor = true;                      
                    navg.ItemLinks.Add(nbi);      
                    nav_menu_C.Groups.Add(navg);
                    iGroup = i;
                    _mamenu = dt_menu.Rows[i]["IDMenu"].ToString();
                }
                else
                {
                    DevExpress.XtraNavBar.NavBarItem nbi = new DevExpress.XtraNavBar.NavBarItem();
                    nbi.Name = dt_menu.Rows[i]["formname"].ToString();                      
                    nbi.Caption = Function.clsFunction.transLateText(dt_menu.Rows[i]["NameSubMenu"].ToString());                    
                    Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
                    if (Function.clsFunction.checkFileExist(Application.StartupPath + "\\Images\\" + nbi.Name + ".png"))
                        image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\" + nbi.Name + ".png");
                    else
                        image = (Bitmap)LoyalHRM.Properties.Resources.page_blank.Clone();
                    //image = (Bitmap)Image.FromFile(picData.ToString(),false);
                    if (image != null)
                    {
                        //using (MemoryStream ms = new MemoryStream(picData))
                        {                       
                            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            //image = bmp;
                         
                            image.MakeTransparent(Color.Transparent);
                            Size s = new Size(20, 20);
                            nbi.SmallImage = ControlDev.FormatControls.resizeImage(image, s);
                            
                        }
                    }
                    nbi.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(Menu_ItemClick);
                    navg.ItemLinks.Add(nbi);
                    nav_menu_C.Groups.Add(navg);
                }
                    nav_menu_C.Groups.Add(navg);
            }
            
        }

        #endregion

        #region Event
       
        private void bts_Thoat_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void bts_DangXuat_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void nav_menu_C_CustomDrawLink1(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawNavBarElementEventArgs e)
        {
            DevExpress.XtraNavBar.ViewInfo.NavLinkInfoArgs info = e.ObjectInfo as DevExpress.XtraNavBar.ViewInfo.NavLinkInfoArgs;
            if (info.State == DevExpress.Utils.Drawing.ObjectState.Selected | info.State == DevExpress.Utils.Drawing.ObjectState.Pressed)
            {

                e.Graphics.FillRectangle(Brushes.Red, e.ObjectInfo.Bounds);
                e.Graphics.DrawImage(e.Image, info.ImageRectangle);
                e.Appearance.DrawString(e.Cache, info.Link.Caption, info.CaptionRectangle);
                e.Appearance.BorderColor = Color.LimeGreen;

                e.Appearance.Options.UseBorderColor = true;
                e.Handled = true;
            }
            else
            {
                if (info.State == DevExpress.Utils.Drawing.ObjectState.Disabled)
                {
                    e.Graphics.FillRectangle(Brushes.Transparent, e.ObjectInfo.Bounds);
                    e.Graphics.DrawImage(e.Image, info.ImageRectangle);
                    e.Appearance.DrawString(e.Cache, info.Link.Caption, info.CaptionRectangle);
                    e.Appearance.BackColor = Color.AliceBlue;
                    e.Appearance.Options.UseBackColor = true;
                    e.Handled = true;
                }
            }
        }
        private void nav_menu_C_CustomDrawLink(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawNavBarElementEventArgs e)
        {
            if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
            {
                LinearGradientBrush brush;
                NavLinkInfoArgs linkInfo = e.ObjectInfo as NavLinkInfoArgs;
                if (e.ObjectInfo.State == ObjectState.Hot)
                {
                    brush = new LinearGradientBrush(e.RealBounds, Color.Orange, Color.PeachPuff,
                        LinearGradientMode.Horizontal);
                }
                else
                    brush = new LinearGradientBrush(e.RealBounds, Color.PeachPuff, Color.Orange,
                        LinearGradientMode.Horizontal);
                if (e.ObjectInfo.State == ObjectState.Normal)
                {
                    //Rectangle rtg = new Rectangle();

                    brush = new LinearGradientBrush(e.RealBounds, Color.MistyRose, Color.LightGoldenrodYellow,
                       LinearGradientMode.Horizontal);
                }
                e.Graphics.FillRectangle(Brushes.OrangeRed, e.RealBounds);
                Rectangle rect = e.RealBounds;
                rect.Inflate(1, -1);

                e.Graphics.FillRectangle(brush, rect);
                if (e.Image != null)
                {
                    Rectangle imageRect = linkInfo.ImageRectangle;
                    imageRect.X += (imageRect.Width - e.Image.Width) / 2;
                    imageRect.Y += (imageRect.Height - e.Image.Height) / 2;
                    imageRect.Size = e.Image.Size;
                    e.Graphics.DrawImageUnscaled(e.Image, imageRect);
                }
                e.Appearance.BorderColor = Color.Black;
                e.Appearance.Options.UseBorderColor = true;
                e.Appearance.DrawString(e.Cache, e.Caption, linkInfo.RealCaptionRectangle, Brushes.Black);
                e.Handled = true;
            }
        }

        private void bbi_update_S_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        bool update = false;
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("File downloaded");
            update = true;
            Application.Exit();

        }

        private void frmMain1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                updateDateUsed();
                if (update == true)
                {
                    //File.Copy(@"C:\test\LoyalHRM.exe", Application.StartupPath + "\\LoyalHRM.exe");   
                    DataTable dt = new DataTable();
                    dt.TableName = "tbPath";
                    dt.Columns.Add("path", typeof(string));
                    dt.Columns.Add("connection", typeof(string));
                    DataRow dr = dt.NewRow();
                    dr["path"] = Application.StartupPath;
                    dr["connection"] = Data.APCoreData.chuoiKetNoi;
                    dt.Rows.Add(dr);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.WriteXml("path.xml");

                    Process.Start(Application.StartupPath + "\\UPDATE_ONLINE.exe", Application.StartupPath);
                }
                else
                {
                    Application.Exit();
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        private void updateDateUsed()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysA where A1='"+ SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")) +"'");
            if(dt.Rows.Count>0 )
            {
                string A4 = "";
                A4 = SECURITY.clsSecurity.DC_CL( dt.Rows[0]["A4"].ToString());
                int date = -1;
                date = Convert.ToInt16(A4.Substring(0,4));
                date += 1;
                string Sdate = "";
                Sdate = date.ToString().PadLeft(4, '0');
                A4 = Sdate + A4.Substring(4, A4.Length - 4);
                if (Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now) != Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0]["A5"].ToString())))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysA set A5='" + SECURITY.clsSecurity.EC_CL( Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now).ToString()) + "', A4='" +SECURITY.clsSecurity.EC_CL(  A4) + "' where A1='" + SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")) + "'");
                }
            }
        }
    
        private void nav_menu_C_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            foreach (DevExpress.XtraNavBar.NavBarGroup navg in nav_menu_C.Groups)
            {
                if (e.Group.Name != navg.Name)
                {
                    navg.Expanded = false;
                }
            }
        }

        private void frmMain1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                checkBackupTrace();
                Application.Exit();
            }
            catch
            {
                Application.Exit();
            }
       
        }
        private void Skin_ItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarListItem list = new DevExpress.XtraBars.BarListItem();
            string[] newSkin = new string[] { "" };
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                list.Strings.Add(cnt.SkinName);
            }
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = list.Strings[e.Index];
            APCoreProcess.APCoreProcess.ExcuteSQL("update sysConfig set skin='" + list.Strings[e.Index] + "'");
        }
        //Menu Click
        private void Menu_ItemClick(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                nav_menu_C.Refresh();
                DevExpress.XtraNavBar.NavBarItemLink link = e.Link;
                for (int i = 0; i < link.Group.ItemLinks.Count; i++)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysSubmenu set pos=" + i + " where formname='" + link.Group.ItemLinks[i].ItemName + "'");
                }
            }
            catch
            {

            }

            bool formx = false;
            if (e.Link.ItemName.ToString() != "")
            {
                switch (e.Link.ItemName.ToString())
                {
                    case "backup":
                        backup();
                        formx = true;                  
                        break;
                    case "restore":
                        restore();
                        formx = true;
     
                        break;
               
                    case "frm_doimatkhau_S":
                        DoiMatKhau();
                        formx = true;
     
                        break;
                    case "frm_thongtin_S":
                        ThongTin();
                        formx = true;
                       
                        break;
                    case "frmMessage":
                        ThongBao();
                        formx = true;
                   
                        break;
                    case "frmSMS":
                        tinNhan();
              
                        formx = true;
                        break;
                    case "frmUpdate":
                        upDate();
             
                        formx = true;
                        break;
                    case "exit":
                        Application.Exit();
                        break;
                    case "frmTranLate":
                        if (checkFormOpen1(e.Link.ItemName.ToString()) == -1 && formx == false)
                        {
                            sysLanguages();
                            Function.clsFunction.sotap++;
                            formx = true;
                        }
                        break;
                    case "frm_liences":
                        if (checkFormOpen1(e.Link.ItemName.ToString()) == -1 && formx == false)
                        {
                            sysLiences();
                            Function.clsFunction.sotap++;
                            formx = true;
                        }
                        break;
                    case "frmConfigFTP":
                        if (checkFormOpen1(e.Link.ItemName.ToString()) == -1 && formx == false)
                        {
                            sysConfigFTP();
                    
                            formx = true;
                        }
                        break;
                }
                try
                {
                    if (checkFormOpen1(e.Link.ItemName.ToString()) == -1 && formx == false)
                    {
                        DataTable dt = new DataTable();
                        dt = APCoreProcess.APCoreProcess.Read("select formname,namespace from sysSubMenu where formname='" + e.Link.ItemName + "'");

                        Call_Form(dt.Rows[0]["namespace"].ToString().Trim(), e.Link.ItemName);
                        //DevExpress.XtraEditors.XtraMessageBox.Show(e.Item.Caption);    
                        Function.clsFunction.sotap++;
                    }
                    else
                    {
                        xtraTabmain.SelectedTabPageIndex = checkFormOpen1(e.Link.ItemName.ToString());
                    }
                }
                catch (Exception) { }
            }
            else
            {
                if (e.Link.Caption.ToString() == "Kết Thúc")
                    Application.Exit();

            }
        }

        private int checkFormOpen1(string formname)
        {
            int pos = -1;
            try
            {

                for (int i = 0; i <xtraTabmain.TabPages.Count; i++)
                {
                    if (formname.Trim() == xtraTabmain.TabPages[i].Name.Trim() || formname.Trim() == xtraTabmain.TabPages[i].Name.Trim()+"H")
                    {
                        if (xtraTabmain.TabPages[i].PageVisible != false)
                            pos = i;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return pos;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1_dem++;
            if (timer1_dem % 2 == 0)
            {
                timer2_Tick(sender, e);
                timer2.Start();
            }

            //second += 1;
            //string time = "";
            //double h = (int)(second / 3600);
            //double m = (int)(second % 3600) / 60;
            //double s = (int)((second % 3600) % 60);
            //string hh = "";
            //if ((int)h / 10 == 0)
            //    hh = "0" + h.ToString();
            //else
            //    hh = h.ToString();
            //string mm = "";
            //if ((int)m / 10 == 0)
            //    mm = "0" + m.ToString();
            //else
            //    mm = m.ToString();
            //string ss = "";
            //if ((int)s / 10 == 0)
            //    ss = "0" + s.ToString();
            //else
            //    ss = s.ToString();
            //time = hh + ":" + mm + ":" + ss;
            bstTime1.Caption = DateTime.Now.ToString("HH:mm:ss");
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            RunText();
        }
        private void xtraTabmain_CloseButtonClick(object sender, EventArgs e)
        {
            XtraTabControl tabControl = sender as XtraTabControl;
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
            Function.clsFunction.sotap--;
        }
        
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }

        #endregion#endregion

        #region Methods

        private bool checkBackupTrace()
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from sysTrace where  convert(datetime, sysTrace.date,103) between  convert(datetime,'" + Function.clsFunction.ConVertDatetimeToNumeric(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1)) + "',103) and  convert(datetime,'" + Function.clsFunction.ConVertDatetimeToNumeric(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) + "',103)");
            if (dt.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.WriteXml(Application.StartupPath + "\\Trace\\" + DateTime.Now.AddMonths(-1).ToString("MMyyyy"));
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysTrace where convert(datetime, sysTrace.date,103) <  convert(datetime,'" + Function.clsFunction.ConVertDatetimeToNumeric(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) + "',103)");
                flag = true;
            }
            else
                flag = false;
            return flag;
        }

        private void upDate()
        {
            try
            {
                waitDialog = new DevExpress.Utils.WaitDialogForm("Downloading...", Function.clsFunction.transLateText("Vui lòng chờ trong giây lát..."));


                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysVar");
                string pathftp = "", username = "", pass = "", pathout = "";
                pathftp = SECURITY.clsSecurity.DC_CL(dt.Rows[2]["value"].ToString());
                username = SECURITY.clsSecurity.DC_CL(dt.Rows[0]["value"].ToString());
                pass = SECURITY.clsSecurity.DC_CL(dt.Rows[1]["value"].ToString());
                pathout = dt.Rows[3]["value"].ToString();
                //pathftp = "ftp://23.94.223.203//UPDATE";
                //username = "anhthanh";
                //pass = "anhthanh@123";
                //pathout = "C:\\temp\\";
                string[] files = GetFileList(pathftp, username, pass);
                int i = 0;
                foreach (string file in files)
                {
                    i++;
                    Download(file, pathftp, username, pass, pathout);
                    waitDialog.SetCaption(Math.Round((float)i / files.Length * 100, 0) + " %");
                }
                update = true;
                Application.Exit();
                waitDialog.Close();
            }
            catch
            {

                waitDialog.Close();
                Function.clsFunction.MessageInfo("Thông báo", "Đường truyền internet gặp sự cố, vui lòng kiểm tra lại đường truyền");
              
            }
        }
        public string[] GetFileList(string path, string username, string pass)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"" + path));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(username, pass);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;      
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null )
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
              
                
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    return result.ToString().Split('\n');
                
                
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
                Function.clsFunction.MessageInfo("Thông báo", "Đường truyền internet gặp sự cố, vui lòng kiểm tra lại đường truyền");
            }
        }

        private void Download(string file, string pathftp, string username, string pass, string pathOut)
        {            
            try
            {
                if (!File.Exists(pathOut))
                {
                    System.IO.Directory.CreateDirectory(@"" + pathOut);
                }
                string uri = @"" + pathftp +"/"+ file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp  || file.Length <3)
                {
                    return;
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"" + pathftp +"/"+ file));
                reqFTP.Credentials = new NetworkCredential((username), (pass));
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(@"" + pathOut + file, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();

            }
            catch (WebException wEx)
            {
                //MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message, "Download Error");
            }
        }

        private void khoitaovaitro()
        {
            DataTable dt_form = new DataTable();
            dt_form = Data.APCoreData.read("select IDSubMenu from sysSubMenu");
            DataTable dt_Phanquyen = new DataTable();
            dt_Phanquyen = Data.APCoreData.readStructure("SysPhanquyen");
            DataRow dr;
            for (int i = 0; i < dt_form.Rows.Count; i++)
            {
                dr = dt_Phanquyen.NewRow();
                dr["id_quyen"] = Function.clsFunction.autonumber("id_quyen", "sysPhanquyen").ToString();
                dr["IDSubMenu"] = dt_form.Rows[i]["IDSubMenu"].ToString();
                dr["them"] = "True";
                dr["xoa"] = "True";
                dr["sua"] = "True";
                dr["nhap"] = "True";
                dr["xuat"] = "True";
                dr["xembaocao"] = "True";
                dr["truycap"] = "True";
                MessageBox.Show("");
                dr["userid"] = "True";
                Data.APCoreData.write(dr);
            }
        }

        private void loadImage()
        {
                Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
            if (System.IO.File.Exists(Application.StartupPath + "\\Images\\paint.png"))
            {
            
                image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\paint.png").Clone();
                image.MakeTransparent(Color.Fuchsia);
                bts_Skin_S.Glyph = FormatControls.resizeImage(image, 16, 16);
            }
            if (System.IO.File.Exists(Application.StartupPath + "\\Images\\logout.png"))
            {
                image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\logout.png").Clone();
                image.MakeTransparent(Color.Fuchsia);
                bts_DangXuat_S.Glyph = FormatControls.resizeImage(image, 16, 16);
                bts_DangXuat_S.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            }
            if (System.IO.File.Exists(Application.StartupPath + "\\Images\\exit.png"))
            {
                image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\exit.png").Clone();
                image.MakeTransparent(Color.Fuchsia);
                bts_Thoat_S.Glyph = FormatControls.resizeImage(image, 16, 16);
                bts_Thoat_S.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            }
        }

        private void backup()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sqlStmt = String.Format("BACKUP DATABASE " + Function.clsFunction.DataBaseName + " TO DISK='{0}'", saveFileDialog1.FileName);
                try
                {
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                        APCoreProcess.APCoreProcess.ExcuteSQL(sqlStmt);
                        waitDialog.Close();
                        Function.clsFunction.MessageInfo("Thông báo", "Sao lưu thành công");
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml("config.xml");
                        waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                        File.Copy(ds.Tables[0].Rows[0]["path"].ToString(), saveFileDialog1.FileName, true);
                        waitDialog.Close();
                        Function.clsFunction.MessageInfo("Thông báo", "Sao lưu thành công");

                    }
                }
                catch (Exception ex)
                {
                    waitDialog.Close();
                    Function.clsFunction.MessageInfo("Thông báo", "Sao lưu thất bại ");
                }
            }
        }

        private void restore()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            try
                            {
                                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                                {
                                    waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                                    string path = openFileDialog1.FileName;
                                    string sqlRestore = "Use master Restore Database [UITGPP] from disk='" + path + "'";
                                    APCoreProcess.APCoreProcess.ExcuteSQL(sqlRestore);
                                    waitDialog.Close();
                                    Function.clsFunction.MessageInfo("Thông báo", "Phục hồi thành công ");
                                }
                                else
                                {

                                    DataSet ds = new DataSet();
                                    ds.ReadXml("config.xml");
                                    waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                                    //File.Delete(ds.Tables[0].Rows[0]["path"].ToString());
                                    File.Copy(openFileDialog1.FileName, ds.Tables[0].Rows[0]["path"].ToString(), true);
                                    waitDialog.Close();
                                    Function.clsFunction.MessageInfo("Thông báo", "Sao lưu thành công");
                                }

                            }
                            catch (SqlException ex)
                            {
                                waitDialog.Close();
                                Function.clsFunction.MessageInfo("Thông báo", "Phục hồi thất bại ");

                                return;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void restore1()
        {
            frm_BKRT_S frm=new frm_BKRT_S();
            frm.ShowDialog();
        }

        private void DoiMatKhau()
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            
            frm.ShowDialog();
        }
        private void ThongTin()
        {
            frm_sysINFO_S frm = new frm_sysINFO_S();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.langues = langues;
          
            frm.ShowDialog();
        }
        private void ThongBao()
        {
            frmMessage frm = new frmMessage();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.langues = langues;
       
            frm.ShowDialog();
        }
        private void tinNhan()
        {
            frmSMS frm = new frmSMS();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.passData = new frmSMS.PassData(getValueSMS);
            frm.langues = langues;
        
            frm.ShowDialog();
        }
 
        private void sysLanguages()
        {
            frmTranLate frm = new frmTranLate();
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from syssubmenu where formname='" + frm.Name + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["addtappage"].ToString() == "False")
                {
                    
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                else
                {
                 
                    frm.TopLevel = false;
                    frm.TopMost = true;
                    frm.Dock = DockStyle.Fill;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    Function.clsFunction.langgues = langues;
                    Function.clsFunction._iduser = id_user;
                    DevExpress.XtraTab.XtraTabPage page = new DevExpress.XtraTab.XtraTabPage();
                    page.Controls.Add(frm);
                    page.Text = Function.clsFunction.transLateText(dt.Rows[0]["NameSubMenu"].ToString());


                    page.Name = frm.Name;
                    page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                    page.MouseMove += new MouseEventHandler(TabControl_MouseMove);
                    xtraTabmain.TabPages.Add(page);

                    xtraTabmain.SelectedTabPageIndex = xtraTabmain.TabPages.Count;
                    frm.Show();
                }
            }
   
        }

        private void sysLiences()
        {
            frm_liences frm = new frm_liences();
            frm.StartPosition = FormStartPosition.CenterParent; 
            

            frm.ShowDialog();

        }

        //sysConfigFTP
        private void sysConfigFTP()
        {
            frnConfigFTP frm = new frnConfigFTP();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

        }
        public void getValueSMS(string value)
        {
            if (value !="")
            {
                bstThongbao1.Caption = value;
            }
        }
        #endregion
          
    }
}