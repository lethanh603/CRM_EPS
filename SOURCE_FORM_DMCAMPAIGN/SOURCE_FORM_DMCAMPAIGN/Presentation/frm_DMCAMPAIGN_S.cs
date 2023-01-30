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

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_DMCAMPAIGN_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMCAMPAIGN_S()
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
        public string _sign = "CD";
        public string ID = "";  

        #endregion

        #region FormEvent

        private void frm_DMCAMPAIGN_S_Load(object sender, EventArgs e)
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
                loadGridLookupPriority();
                chk_remind_I6.Checked = false;
                chk_remind_I6_CheckedChanged(sender, e);
                loadGridLookupEmployee();
                LoadInfo();
                glue_idemp_I1.EditValue = Function.clsFunction.GetIDEMPByUser();
            }
            txt_sign_20_I1.Focus();
        }

        private void frm_DMCAMPAIGN_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I1.Focus();
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
                txt_sign_20_I1.Focus();
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
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void chk_remind_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_remind_I6.Checked == true)
            {
                dte_dateremind_I5.Properties.ReadOnly = false;
                dte_dateremind_I5.EditValue = DateTime.Now;
            }
            else
            {
                dte_dateremind_I5.Properties.ReadOnly = true;
                dte_dateremind_I5.EditValue = null;
            }
        }

        #endregion

        #region GridEvent

        #endregion

        #region Methods
        private void save()
        {
            if (!checkInput()) return;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcampaign_IK1.Name) + " = '" + txt_idcampaign_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcampaign_IK1.Text, txt_campaign_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idcampaign_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_idcampaign_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcampaign_IK1.Name) + " = '" + txt_idcampaign_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcampaign_IK1.Text, txt_campaign_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idcampaign_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idcampaign_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idcampaign_IK1);
                chk_status_I6.Checked = true;
                dte_finishdate_I5.EditValue = null;
            } 
            else
            {
                txt_idcampaign_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idcampaign_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (txt_campaign_100_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_campaign_100_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_campaign_100_I2.Focus();
                return false;
            }
        


            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from "+ Function.clsFunction.getNameControls(this.Name) +" where sign='" + txt_sign_20_I1.Text + "' and idemp = '"+ Function.clsFunction.GetIDEMPByUser() +"' ").Rows.Count > 0 )
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from  " + Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcampaign_IK1.Name) + "='" + txt_idcampaign_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from  " + Function.clsFunction.getNameControls(this.Name) + " where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "' and and idemp = '" + Function.clsFunction.GetIDEMPByUser() + "' ").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }

        // Priority
        private void loadGridLookupPriority()
        {
            string[] caption = new string[] { "ID", "Ưu tiên" };
            string[] fieldname = new string[] { "idpriority", "priority" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idpriority_I1, "select idpriority, priority from dmpriority", "priority", "idpriority", caption, fieldname, this.Name, glue_idpriority_I1.Width);
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1, "select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        
        #endregion      

        private void chk_unlimited_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_unlimited_I6.Checked == true)
            {
                dte_fromdate_I5.Enabled = false;
                dte_todate_I5.Enabled = false;
                dte_finishdate_I5.Enabled = false;
                dte_dateremind_I5.Enabled = false;
            }
            else {
                dte_fromdate_I5.Enabled = true;
                dte_todate_I5.Enabled = true;
                dte_finishdate_I5.Enabled = true;
                dte_dateremind_I5.Enabled = true;
            }
        }


        
    }
}