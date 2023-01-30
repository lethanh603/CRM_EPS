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


namespace SOURCE_FORM.Presentation
{
    public partial class frm_sysGroupSubMenu : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysGroupSubMenu()
        {
            InitializeComponent();
        }

        #region Var
      
        public bool them = false;
        public bool call = false;
        public int index;
        public bool statusForm;
        public string langues="_VI";
        public string IDGroupMenu = "";
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string dauma = "";

        public  void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {                
                
            }
        }
        #endregion

        #region Load

        private void frmMenu_Load(object sender, EventArgs e)
        {
        
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    //Function.clsFunction.CreateTable(this, this);
                }
                catch { }
            }
            else
            {
                ControlDev.FormatControls.LoadLookupEditNoneParameter(lue_IDMenu_I1, "load_" + lue_IDMenu_I1.Name + "_" + this.Name, "create proc   " + "load_" + lue_IDMenu_I1.Name + "_" + this.Name+ " as begin select IDmenu,MenuName from sysMenu end","IDMenu","MenuName");
                LoadTT();
                Function.clsFunction.TranslateForm(this, this.Name);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SOURCE_FORM.Presentation.frm_sysSubMenu_S frm = new SOURCE_FORM.Presentation.frm_sysSubMenu_S();
            frm.passData = new SOURCE_FORM.Presentation.frm_sysSubMenu_S.PassData(GetValue);
            frm.TopMost = true;
            this.TopMost = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void LoadTT()
        {
            if ( them == true)
            {
                lbl_IDGroupMenu_IK1.Text = Function.clsFunction.autonumber(dauma,"IDGroupMenu","SysGroupSubMenu");              
                txt_GroupMenuName_I2.Text = "";
                chk_show_I6.Checked = true;
            } 
            else
            {
                lbl_IDGroupMenu_IK1.Text = IDGroupMenu;
                Function.clsFunction.Data_Binding1(this, lbl_IDGroupMenu_IK1);
            }
            Function.clsFunction.TranslateForm(this, this.Name);
            txt_GroupMenuName_I2.Focus();
        }

        #endregion

        #region Event

        private void lue_IDMenu_I1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name,  dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.IDSubMenu = dbo.sysPower.IDSubMenu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._user) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
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

        private void grp_ttbb_C_Paint(object sender, PaintEventArgs e)
        {

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
            if (txt_GroupMenuName_I2.Text != "")
            {
                if (them == true)
                {

                    Function.clsFunction.Insert_data(this, (this.Name));

                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S.Text), lbl_IDGroupMenu_IK1.Text, txt_GroupMenuName_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    if (call == true)
                    {                        
                        strpassData(lbl_IDGroupMenu_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, (this.Name), Function.clsFunction.getNameControls(lbl_IDGroupMenu_IK1.Name), lbl_IDGroupMenu_IK1.Text);


                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S.Text), lbl_IDGroupMenu_IK1.Text, txt_GroupMenuName_I2.Text, SystemInformation.ComputerName.ToString(), "1");
                    this.Close();
                    //DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[9]["message" + langues].ToString(), dtMessage.Rows[9]["title" + langues].ToString());
                    dxError.ClearErrors();

                }
                if (passData != null)
                    passData(true);
             
            }
            else
            {
                string caption = "";
                DataTable dtMessage = new DataTable();
                dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                caption = dtMessage.Rows[0]["title" + langues].ToString();
                dxError.SetError(txt_GroupMenuName_I2, caption);

            }
        }

        private bool checkInput()
        {
            if (txt_GroupMenuName_I2.Text == "")
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_GroupMenuName_I2.Focus();
                return false;
            } 
    
            return true;
        }

        #endregion

 


    }
}