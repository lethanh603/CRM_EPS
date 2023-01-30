//Thanh 
//Danh mục cakip
//15/5/2013
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.IO;
using System.Reflection;
using Function;


namespace SOURCE_FORM.Presentation
{
    public partial class frm_sysSubMenu_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysSubMenu_S()
        {
            InitializeComponent();
        }

        #region Var
        bool loadPic = false;
        public bool them = false;
        public bool call = false;
        public int index;
        public bool statusForm;
        public string langues="_VI";
        public string IDSubMenu = "";
        public string IDMenu = "";
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string dauma = "";
        System.Data.DataTable dtMessage = new System.Data.DataTable();
        public  void GetValueMenu(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    ControlDev.FormatControls.LoadGridLookupEditNoneParameter(lue_IDMenu_S, "load_" + lue_IDMenu_S.Name + "_" + this.Name, "create proc   " + "load_" + lue_IDMenu_S.Name + "_" + this.Name + " as begin select IDMenu,MenuName from sysMenu  end", "IDMenu", "MenuName");
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEditNoneParameter(lue_IDMenu_S, "load_" + lue_IDMenu_S.Name + "_" + this.Name, "  select IDMenu,MenuName from sysMenu  ", "IDMenu", "MenuName");
                }
            }
        }
        public void GetValueGroupMenu(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    ControlDev.FormatControls.LoadGridLookupEditNoneParameter(glue_IDGroupMenu_I1, "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name, "create proc   " + "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name + " as begin select IDGroupMenu,GroupMenuName from sysGroupSubMenu  end", "IDGroupMenu", "GroupMenuName");
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEditNoneParameter(glue_IDGroupMenu_I1, "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name,  " select IDGroupMenu,GroupMenuName from sysGroupSubMenu  ", "IDGroupMenu", "GroupMenuName");
                }
                    glue_IDGroupMenu_I1.Properties.View.SelectRow(((DataTable)glue_IDGroupMenu_I1.Properties.DataSource).Rows.Count-1);
            }
        }
        #endregion

        #region Load

        private void frmNhapDVT_Load(object sender, EventArgs e)
        {
            //statusForm = true;
            Function.clsFunction.sysGrantUserByRole(grp_ttlq_C, this.Name);
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);   
            }
            LoadTT();      

        }  

        private void LoadTT()
        {
            Function.clsFunction.TranslateForm(this, this.Name);
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                ControlDev.FormatControls.LoadGridLookupEditNoneParameter(lue_IDMenu_S, "load_" + lue_IDMenu_S.Name + "_" + this.Name, "create proc   " + "load_" + lue_IDMenu_S.Name + "_" + this.Name + " as begin select IDMenu,MenuName from sysMenu  end", "IDMenu", "MenuName");
                ControlDev.FormatControls.LoadGridLookupEditNoneParameter(glue_IDGroupMenu_I1, "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name, "create proc   " + "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name + " as begin select IDGroupMenu,GroupMenuName from sysGroupSubMenu  end", "IDGroupMenu", "GroupMenuName");
            }
            else
            {
                ControlDev.FormatControls.LoadGridLookupEditNoneParameter(lue_IDMenu_S, "load_" + lue_IDMenu_S.Name + "_" + this.Name, " select IDMenu,MenuName from sysMenu  ", "IDMenu", "MenuName");
                ControlDev.FormatControls.LoadGridLookupEditNoneParameter(glue_IDGroupMenu_I1, "load_" + glue_IDGroupMenu_I1.Name + "_" + this.Name," select IDGroupMenu,GroupMenuName from sysGroupSubMenu  ", "IDGroupMenu", "GroupMenuName");
            }
                
            if ( them == true)
            {
                lbl_IDSubMenu_I1.Text = Function.clsFunction.autonumber(dauma, "IDSubMenu", "SysSubMenu");                 
                txt_NameSubMenu_I2.Text = "";
                chk_AddTappage_I6.Checked = true;
                lue_IDMenu_S.EditValue = lue_IDMenu_S.Properties.GetKeyValue(0);
            } 
            else
            {
                glue_IDGroupMenu_I1.EditValue = IDMenu;
                lbl_IDSubMenu_I1.Text = IDSubMenu;
                if (clsFunction._pre == false)
                {
                    Function.clsFunction.Data_Binding1(this, lbl_IDSubMenu_I1);
                }
                else 
                {
                    Function.clsFunction.Data_BindingTrace(this,clsFunction.dtTrace);
                    btn_insert_S_allow_insert.Enabled = false;
                }
            }
            
            txt_NameSubMenu_I2.Focus();
            Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();     
            if (Function.clsFunction.checkFileExist(Application.StartupPath + "\\Images\\" + txt_FormName_I2.Text + ".png"))
                image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\" + txt_FormName_I2.Text + ".png");
            else
                image = null;
            if (image != null)
            {
                image.MakeTransparent(Color.Fuchsia);
                Size s = new Size(32, 32);
                img_icon_S.Image = ControlDev.FormatControls.resizeImage(image, s);
            }
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     m.IDSubMenu, m.NameSubMenu, m.IDGroupMenu, m.FormName, g.IDMenu FROM  sysGroupSubMenu AS g INNER JOIN   sysSubMenu AS m ON g.IDGroupMenu = m.IDGroupMenu where m.IDSubMenu='"+ lbl_IDSubMenu_I1.Text +"'");
            if (dt.Rows.Count > 0)
            {
                cbo_list_S.Text = dt.Rows[0]["FormName"].ToString();
                lue_IDMenu_S.EditValue =dt.Rows[0]["IDMenu"].ToString(); 
                glue_IDGroupMenu_I1.EditValue = dt.Rows[0]["IDGroupMenu"].ToString(); ;
            }
        }

        #endregion

        #region Event

        private void lue_IDMenu_S_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void lue_IDMenu_I1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_sysMenu_S frm = new frm_sysMenu_S();
            frm.passData = new frm_sysMenu_S.PassData(GetValueMenu);
            frm.them = true;
            frm.langues = langues;
            frm.ShowDialog();
        }
        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name,  dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._user) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
                {
                    if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
                    {

                        {
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("luu"))
                            {
                                btnLuu0_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("thoat"))
                            {
                                btnThoat0_Click(sender, e);
                            }

                        }
                    }
                }
            }
        }

    

        #endregion

        #region ButtonEvent

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnThoat0_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu0_Click(object sender, EventArgs e)
        {            
            insert();      
        }   

        #endregion

        #region Methods

        private void insert()
        {
            if (!checkInput()) return;
            if (txt_NameSubMenu_I2.Text != "")
            {
                if (them == true)
                {
                    Function.clsFunction.Insert_data(this, (this.Name));
                    APCoreProcess.APCoreProcess.ExcuteSQL("insert into sysPower (IDSubMenu,mavaitro,allow_all,allow_insert,allow_edit,allow_delete,allow_import,allow_export,allow_access,allow_print,id) values ('" + lbl_IDSubMenu_I1.Text + "','" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "','True','True','True','True','True','True','True','True','"+ clsFunction.autonumber("id","sysPower") +"')");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(lbl_IDSubMenu_I1.Name) + " = '" + lbl_IDSubMenu_I1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S_allow_insert.Text), lbl_IDSubMenu_I1.Text, txt_NameSubMenu_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                    if (call == true)
                    {                        
                        strpassData(lbl_IDSubMenu_I1.Text);
                        this.Close();
                    }
                    else
                    {
                        LoadTT();
                    }                  
                    dxError.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, (this.Name), Function.clsFunction.getNameControls(lbl_IDSubMenu_I1.Name), lbl_IDSubMenu_I1.Text);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(lbl_IDSubMenu_I1.Name) + " = '" + lbl_IDSubMenu_I1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S_allow_insert.Text), lbl_IDSubMenu_I1.Text, txt_NameSubMenu_I2.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                    this.Close();
                    //Function.clsFunction.MessageInfo("Thông báo","Cập nhật thành công");
                    dxError.ClearErrors();
                }
                if (passData != null)
                    passData(true);             
            }
            else
            {         
                dxError.SetError(txt_NameSubMenu_I2, Function.clsFunction.transLateText("Không được rỗng"));
            }

            if (loadPic == true && img_icon_S.EditValue !=null)
            {
                try
                {
                    img_icon_S.Image.Save(Application.StartupPath + "\\Images\\" + txt_FormName_I2.Text + ".png");
                }
                catch { }
            }
       
        }

        private bool checkInput()
        {
            if (txt_NameSubMenu_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_NameSubMenu_I2.Focus();
                return false;
            } 
    
            return true;
        }  

        #endregion

        private void glue_IDGroupMenu_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_sysGroupSubMenu frm = new frm_sysGroupSubMenu();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.passData = new frm_sysGroupSubMenu.PassData(GetValueMenu);
            frm.them = true;
            frm.dauma = lue_IDMenu_S.EditValue.ToString()+".";
    
            frm.langues = langues;
            frm.statusForm = statusForm;
            frm.ShowDialog();
        }

        private void img_icon_S_Click(object sender, EventArgs e)
        {
            img_icon_S.LoadImage();
        }

        private void img_icon_S_EditValueChanged(object sender, EventArgs e)
        {
            loadPic = true;
        }

        private void bbi_browser_Click(object sender, EventArgs e)
        {            
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.dll)|*.dll|All files (*.*)|*.*";
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
                            myStream.Close();
                            // Insert code to read the stream here.               
                            string filename="";
                            filename=openFileDialog1.FileName.Split('\\')[openFileDialog1.FileName.Split('\\').Length-1];
                            Function.clsFunction.Copy_File(openFileDialog1.FileName, Application.StartupPath+"\\" + filename);
                            string serverPath = System.Windows.Forms.Application.StartupPath;
                            //Assembly assembly = Assembly.LoadFile(serverPath + @"\" + filename);
                            Call_Form(filename.Replace(".dll",""));     
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Call_Form(string dll)
        {
            try
            {
                if (dll != "")
                {
                    string serverPath = System.Windows.Forms.Application.StartupPath;
                    Assembly assembly = Assembly.LoadFile(serverPath + @"\" + dll + ".dll");// cung phien ban devexpress moi dc
                    txt_Namespace_I2.Text =  assembly.ManifestModule.Name.Replace(".dll","");
                    for (int i = 0; i < assembly.GetTypes().Length; i++)
                    {
                        cbo_list_S.Properties.Items.Add(assembly.GetTypes()[i].Name);
                    }
                    cbo_list_S.SelectedIndex = 0;
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Truy tìm thất bại");
                    clsFunction._pre = false;
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Chương trình chưa hỗ trợ tính năng này, vui lòng liên hệ nhà cung cấp \n" + ex.Message);
                clsFunction._pre = false;
            }
        }

        private void cbo_list_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_FormName_I2.Text = cbo_list_S.Text;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }




    }
}