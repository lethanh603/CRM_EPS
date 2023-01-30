using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace SOURCE_FORM_DMCAMPAIGN.Presentation
{
    public partial class frm_MISSION_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor

        public frm_MISSION_S()
        {
            InitializeComponent();
        }

        #endregion

        #region Var

        public bool _insert = false;
        public bool call = false;     
        public bool statusForm;    
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "CM";
        public string ID = "";
        public string idcustomer = "";
        public string idcampaign = "";

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    Function.clsFunction.CreateTable(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGridLookupTask();
                loadGridLookupContact();
                loadGridLookupStatus();     
                LoadInfo();
            }           
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
           
        }

        private void frm_DMAREA_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btn_insert_allow_insert.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F5)
                {
                    bbi_allow_insert.PerformClick();
                }
                else
                {
                    if (e.KeyCode == Keys.F9)
                    {
                        btn_exit_S.PerformClick();
                    }
                    if (e.KeyCode == Keys.F2)
                    {
                        btn_allow_insertBG_S.PerformClick();
                    }
                }
            }
        }

        #endregion

        #region ButtonEvent

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                this.Close();
            }            
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_insert_allow_insert_Click(object sender, EventArgs e)
        {
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                InitData();
                passData(true);
            }
        }

        private void InitData()
        {
            _insert = true;
            LoadInfo();
        }

        #endregion
        
        #region Event

        // enter next control
        
        private void txt_sign_I2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ! sender.GetType().ToString().Contains("MemoEdit"))
                SendKeys.Send("{Tab}");
        }


        #endregion

        #region GridEvent

        #endregion

        #region Methods

        private void save()
        {
            try
            {
                if (!checkInput()) return;
                if (_insert == true)
                {
                    if (glue_idcontact_I1.EditValue == null)
                    {
                        glue_idcontact_I1.EditValue = "";
                    }
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcammiss_IK1.Name) + " = '" + txt_idcammiss_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcammiss_IK1.Text, txt_email_S.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_idcammiss_IK1.Text);
                    }
                    else
                    {
                        LoadInfo();
                    }
                    passData(true);
                    //this.Hide();  
                    dxEp_error_S.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idcammiss_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcammiss_IK1.Name) + " = '" + txt_idcammiss_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcammiss_IK1.Text, txt_email_S.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    passData(true);
                }
            }
            catch
            {
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idcammiss_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idcammiss_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idcammiss_IK1);
                txt_idcampaign_I1.Text = idcampaign;
                txt_idcustomer_I1.Text = idcustomer; 
            } 
            else
            {
                txt_idcammiss_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idcammiss_IK1);
                           
            }                     
        }

        private bool checkInput()
        {
          
            if (glue_idtask_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idtask_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idtask_I1.Focus();
                return false;
            }
            
            if (glue_idstatus_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idstatus_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idstatus_I1.Focus();
                return false;
            }

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from MISSION where idcustomer='" + txt_idcustomer_I1.Text + "' AND idcampaign ='" + txt_idcampaign_I1.Text + "' AND idtask='"+ glue_idtask_I1.EditValue.ToString() +"' ").Rows.Count > 0 && 1==2)
                {
                    Function.clsFunction.MessageInfo("Thông báo","Thiết lập trùng khách hàng và chiến dịch, vui lòng kiểm tra lại");
                    dxEp_error_S.SetError(glue_idtask_I1, Function.clsFunction.transLateText("Error"));
                    return false;
                }
            }

            return true;
        }
        
        private void loadGridLookupTask()
        {
            string[] caption = new string[] { "Mã HĐ", "Hành động" };
            string[] fieldname = new string[] { "idtask", "taskname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idtask_I1, "select idtask, taskname from dmtask where status=1", "taskname", "idtask", caption, fieldname, this.Name, glue_idtask_I1.Width);
        }      

        private void loadGridLookupContact()
        {
            string[] caption = new string[] { "Mã NLH", "Người liên hệ" };
            string[] fieldname = new string[] { "idcontact", "contactname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idcontact_I1, "select idcontact, contactname from cuscontact where status=1 and idcustomer ='"+ idcustomer +"'", "contactname", "idcontact", caption, fieldname, this.Name, glue_idcontact_I1.Width);
        }

        private void loadGridLookupStatus()
        {
            string[] caption = new string[] { "Mã TT", "Tình trạng" };
            string[] fieldname = new string[] { "idstatus", "statusname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatus_I1, "select idstatus, statusname from dmstatus where status=1", "statusname", "idstatus", caption, fieldname, this.Name, glue_idstatus_I1.Width);
        }

        #endregion       

        private void glue_idcontact_I1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select tel, email from cuscontact where idcontact='" + glue_idcontact_I1.EditValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txt_tel_S.Text = dt.Rows[0]["tel"].ToString();
                    txt_email_S.Text = dt.Rows[0]["email"].ToString();
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Khách hàng này chưa có người liên hệ, vui lòng kiểm tra lại");
             
            }
        }

        private void btn_addcontact_S_Click(object sender, EventArgs e)
        {
            try
            {
                SOURCE_FORM_CRM.Presentation.frm_CUSCONTACT_S frm = new SOURCE_FORM_CRM.Presentation.frm_CUSCONTACT_S();
                frm._insert = true;
                frm._sign = "KH";
                frm.statusForm = statusForm;
                frm.lbl_idcustomer_I1.Text = txt_idcustomer_I1.Text;
                frm.passData = new SOURCE_FORM_CRM.Presentation.frm_CUSCONTACT_S.PassData(getValueContact);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getValueContact(bool value)
        {
            if (value == true)
            {
                loadGridLookupContact();
            }
        }

        private void btn_allow_insertBG_S_Click(object sender, EventArgs e)
        {
            try
            {
                SOURCE_FORM_QUOTATION.Presentation.frm_QUOTATION_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_QUOTATION_S();
                frm._sign = "QS";
                frm.statusForm = statusForm;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }
    }
}