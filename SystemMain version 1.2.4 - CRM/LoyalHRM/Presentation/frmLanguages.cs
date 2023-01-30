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
using System.Text.RegularExpressions;


namespace LoyalHRM.Presentation
{
    public partial class frm_sysLanggues_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysLanggues_S()
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
        public string dauma = "LA";
        public delegate void strPassData(string value);
        public strPassData strpassData;
        string old_code = "";

        #endregion

        #region Load
        private void frmNhapDVT_Load(object sender, EventArgs e)
        {
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {       
                try
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysControls where form_name='" + this.Name + "'");
                    Function.clsFunction.Save_sysControl(this, this);
                    //APCoreProcess.APCoreProcess.ExcuteSQL("drop table sysLanggues");
                    //Function.clsFunction.CreateTable(this);
                    Function.clsFunction.setLanguageForm(this, this);
                }
                catch { }
            }
            else
            {
                //DataTable dt = new DataTable();
                //dt = APCoreProcess.APCoreProcess.Read("select * from sysLanggues");
                //if (dt.Rows.Count > 0)
                //{
                //    them = false;
                //    txt_id_IK1.Text = dt.Rows[0]["id"].ToString();
                //}
                //else
                //    them = true;e 
                this.Text = Function.clsFunction.transLateText(this.Text);
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadTT();
                lbl_title_I2.Focus();
            }
        }


        private void LoadTT()
        {
            if (them == false && txt_id_IK1.Text == "" || them == true)
            {
                txt_id_IK1.Text = Function.clsFunction.layMa(dauma, Function.clsFunction.getNameControls(txt_id_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                them = true;
                old_code = "";
                
            } if (them == false)
            {
                Function.clsFunction.Data_Binding1(this, txt_id_IK1);
                old_code = txt_kyhieu_I2.Text;
            }
            //Function.clsFunction.Text_Control(this, langues);
            txt_ngonngu_I2.Focus();

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
            if (txt_ngonngu_I2.Text != "")
            {
                if (them == true)
                {
                    txt_kyhieu_I2.Text = txt_kyhieu_I2.Text.ToString().Replace("_", "");
                    txt_kyhieu_I2.Text = txt_kyhieu_I2.Text.ToString().Replace(" ", "");
                    txt_kyhieu_I2.Text = Function.clsFunction.RemoveSign4VietnameseString(txt_kyhieu_I2.Text);
                    txt_kyhieu_I2.Text = "_" + txt_kyhieu_I2.Text;
                    Function.clsFunction.Insert_data(this, (this.Name));
                    
                    alterColumns();
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sys_Language set language" + txt_kyhieu_I2.Text + "=language_VI+ '" + txt_kyhieu_I2.Text + " '");
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_id_IK1.Text, txt_ngonngu_I2.Text, SystemInformation.ComputerName.ToString(), "1");
                    LoadTT();
                    if (passData != null)
                        passData(true);
                    dxe_err_C.ClearErrors();
                    this.Close();
                }
                else
                {
                    if (APCoreProcess.APCoreProcess.Read("select kyhieu from sysLanggues where id= '" + txt_id_IK1.Text + "'").Rows[0][0].ToString() != Function.clsFunction.langgues)
                    {
                        txt_kyhieu_I2.Text = txt_kyhieu_I2.Text.ToString().Replace("_", "");
                        txt_kyhieu_I2.Text = txt_kyhieu_I2.Text.ToString().Replace(" ", "");
                        txt_kyhieu_I2.Text = Function.clsFunction.RemoveSign4VietnameseString(txt_kyhieu_I2.Text);
                        txt_kyhieu_I2.Text = "_" + txt_kyhieu_I2.Text;
                        Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_id_IK1.Name), txt_id_IK1.Text);
                        alterRenameColumns();
                        if (passData != null)
                            passData(true);
                        Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_id_IK1.Text, txt_ngonngu_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                        this.Close();
                        dxe_err_C.ClearErrors();
                        this.Close();
                        DevExpress.XtraEditors.XtraMessageBox.Show(Function.clsFunction.transLate("Lưu thành công"), Function.clsFunction.transLate("Thông báo"));
                    }
                    else
                        Function.clsFunction.MessageInfo("Thông báo", "Bạn không thể xóa ngôn ngữ hiện đang sử dụng,\nVui lòng chuyển sang ngôn ngữ khác để sửa ngôn ngữ này");
                }          
            }     
        }

        private void alterColumns()
        {
            APCoreProcess.APCoreProcess.ExcuteSQL("ALTER TABLE sys_Language ADD language" + ( txt_kyhieu_I2.Text) + " nvarchar(200)");
        }
        private void alterRenameColumns()
        {
            APCoreProcess.APCoreProcess.ExcuteSQL("EXEC sp_rename 'sys_Language.language" + old_code + "', 'language" + txt_kyhieu_I2.Text + "'");
        }
        private bool checkInput()
        {
            if (lbl_title_I2.Text == "")
            {

                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                lbl_title_I2.Focus();
                dxe_err_C.SetError(lbl_title_I2,Function.clsFunction.transLateText("Nhập tên lĩnh vực"));
                return false;
            }
           
            

                if (old_code == "")
                {
                    if (APCoreProcess.APCoreProcess.Read("select kyhieu from sysLanggues where kyhieu = '_" + txt_kyhieu_I2.Text + "' ").Rows.Count > 0 || (txt_kyhieu_I2.Text == ""))
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Ký hiệu trùng");
                        txt_kyhieu_I2.Focus();
                        return false;
                    }
                }
                else
                {
                    if (APCoreProcess.APCoreProcess.Read("select kyhieu from sysLanggues where kyhieu = '" + txt_kyhieu_I2.Text + "' and kyhieu <> '" + old_code + "' ").Rows.Count > 0 || (txt_kyhieu_I2.Text == ""))
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Ký hiệu trùng");
                        txt_kyhieu_I2.Focus();
                        return false;
                    }
                }
    
            
            if (txt_ngonngu_I2.Text == "")
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                txt_ngonngu_I2.Focus();
                dxe_err_C.SetError(txt_ngonngu_I2, Function.clsFunction.transLateText("Nhập ngôn ngữ"));
                return false;
            }    
            return true;
        }
        private void txt_kyhieu_I2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar)||  char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        #endregion

 

    


        
    }
}