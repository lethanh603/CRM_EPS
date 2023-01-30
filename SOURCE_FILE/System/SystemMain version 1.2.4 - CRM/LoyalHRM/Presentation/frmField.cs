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


namespace LoyalHRM.Presentation
{
    public partial class frm_sysField_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysField_S()
        {
            InitializeComponent();
        }

        #region Var      
        public bool them = false;
        public int index;
        public bool call = false;
        public bool statusForm=false;
        public string langues="_VI";
        public delegate void PassData(bool value);
        public PassData passData;
        public string dauma = "CTY";
        public delegate void strPassData(string value);
        public strPassData strpassData;


        #endregion

        #region Load
        private void frmNhapDVT_Load(object sender, EventArgs e)
        {  
            if (statusForm == true)
            {       
                try
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysControls where form_name='" + this.Name + "'");
                    Function.clsFunction.Save_sysControl(this, this);
                    APCoreProcess.APCoreProcess.ExcuteSQL("drop table sysField");
                    Function.clsFunction.CreateTable(this);
                    Function.clsFunction.setLanguageForm(this, this);
                }
                catch { }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysInfo");
                if (dt.Rows.Count > 0)
                {
                    them = false;
                    txt_fieldid_IK1.Text = dt.Rows[0]["companyid"].ToString();
                }
                else
                    them = true;
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadTT();
                lbl_fieldname_I2.Focus();
            }
        }


        private void LoadTT()
        {
            if (them == false && txt_fieldid_IK1.Text == "" || them == true)
            {
                txt_fieldid_IK1.Text = Function.clsFunction.layMa(dauma, Function.clsFunction.getNameControls(txt_fieldid_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                them = true;                   
            } 
            if (them == false)
            {
                Function.clsFunction.Data_Binding1(this, txt_fieldid_IK1);
            }
            //Function.clsFunction.Text_Control(this, langues);
            txt_notes_I2.Focus();
        }

        #endregion

        #region Event

        private void frm_sysField_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && btn_luu_S.Enabled == true)
                btnLuu0_Click(sender, e);
            if (e.KeyCode == Keys.F2 && btn_thoat_S.Enabled==true)
                btnThoat0_Click(sender, e);
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
            if (txt_notes_I2.Text != "")
            {
                if (them == true)
                {

                    Function.clsFunction.Insert_data(this, (this.Name));

                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_fieldid_IK1.Text, txt_sign_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    LoadTT();
                    if (passData != null)
                        passData(true);
                    dxe_err_C.ClearErrors();
                    this.Close();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_fieldid_IK1.Name), txt_fieldid_IK1.Text);
                    if (passData != null)
                        passData(true);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name)+" where " + Function.clsFunction.getNameControls( txt_fieldid_IK1.Name)+" = '"+txt_fieldid_IK1.Text+"'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_fieldid_IK1.Text, txt_sign_I2.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    this.Close();  
                    dxe_err_C.ClearErrors();
                    this.Close();
                    DevExpress.XtraEditors.XtraMessageBox.Show(Function.clsFunction.transLate("Lưu thành công"), Function.clsFunction.transLate("Thông báo"));
                }          
            }
            else
            {
                string caption = "";
                DataTable dtMessage = new DataTable();
                dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                caption = dtMessage.Rows[0]["title" + langues].ToString();
                dxe_err_C.SetError(txt_notes_I2, caption);

            }
        }

        private bool checkInput()
        {
            if (lbl_fieldname_I2.Text == "")
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(Function.clsFunction.transLate("Lưu thành công"), Function.clsFunction.transLate("Thông báo"));
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                lbl_fieldname_I2.Focus();
                dxe_err_C.SetError(lbl_fieldname_I2,Function.clsFunction.transLateText("Nhập tên lĩnh vực"));
                return false;
            }

    
            return true;
        }

        #endregion





      
        
    }
}