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
    public partial class frm_sysMenu_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysMenu_S()
        {
            InitializeComponent();
        }

        #region Var
      
        public bool them = false;
        public bool call = false;
        public int index;
        public bool statusForm;
        public string langues="_VI";
        public delegate void PassData(bool value);
        public PassData passData;
        public string IDMenu = "";
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
                catch  { }
            }
            LoadTT();
            Function.clsFunction.TranslateForm(this,this.Name);
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
                lbl_IDMenu_IK1.Text = Function.clsFunction.autonumber("IdMenu", "sysMenu").ToString();            
                txt_MenuName_I2.Text = "";
                chk_show_I6.Checked = true;
            } 
            else
            {
                lbl_IDMenu_IK1.Text = IDMenu;
                Function.clsFunction.Data_Binding1(this, lbl_IDMenu_IK1);
            }
            Function.clsFunction.TranslateForm(this, this.Name);
            this.Text = Function.clsFunction.transLateText(this.Text);
            txt_MenuName_I2.Focus();
        }

        #endregion

        #region Event


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
            if (txt_MenuName_I2.Text != "")
            {
                if (them == true)
                {

                    Function.clsFunction.Insert_data(this, (this.Name));

                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S.Text), lbl_IDMenu_IK1.Text, txt_MenuName_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    if (call == true)
                    {                        
                        strpassData(lbl_IDMenu_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, (this.Name), Function.clsFunction.getNameControls(lbl_IDMenu_IK1.Name), lbl_IDMenu_IK1.Text);


                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_S.Text), lbl_IDMenu_IK1.Text, txt_MenuName_I2.Text, SystemInformation.ComputerName.ToString(), "1");
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
                dxError.SetError(txt_MenuName_I2, caption);

            }
        }

        private bool checkInput()
        {
            if (txt_MenuName_I2.Text == "")
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_MenuName_I2.Focus();
                return false;
            } 
    
            return true;
        }

        #endregion


    }
}