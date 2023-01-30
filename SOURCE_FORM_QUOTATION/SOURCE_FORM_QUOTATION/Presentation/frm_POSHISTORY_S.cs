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
    public partial class frm_POHISTORY_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_POHISTORY_S()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        public bool _insert = true;
        public bool call = false;     
        public bool statusForm;    
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "HS";
        public string ID = "";
        public string procedure = "";
        public string idprocedure = "";
        public string idpotype = "";
        public string idquotation = "";
        public int pos = 0;

        #endregion

        #region FormEvent

        private void frm_POHISTORY_S_Load(object sender, EventArgs e)
        {
            
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    //Function.clsFunction.CreateTable(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGridLookupPOType();
                loadGridLookupEmployeeRequire();
                loadGridLookupEmployeeAction();
                LoadInfo();
            }
            txt_procedurename_100_I2.Text = procedure;
            txt_procedurename_100_I2.Focus();
            txt_idquotation_I2.Text = idquotation;
            txt_idpotype_I2.Text = idpotype;
            txt_idprocedure_I2.Text = idprocedure;
            spe_pos_I4.Value = pos;
        }

        private void frm_POHISTORY_S_Activated(object sender, EventArgs e)
        {
            txt_procedurename_100_I2.Focus();
        }

        private void frm_POHISTORY_S_KeyDown(object sender, KeyEventArgs e)
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
                txt_procedurename_100_I2.Focus();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpohistory_IK1.Name) + " = '" + txt_idpohistory_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idpohistory_IK1.Text, txt_procedurename_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idpohistory_IK1.Text);                    
                }
                else
                {
                    LoadInfo();
                    passData(true);
                }
                
                //this.Hide();  
                dxEp_error_S.ClearErrors();
            }
            else
            {
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_idpohistory_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpohistory_IK1.Name) + " = '" + txt_idpohistory_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idpohistory_IK1.Text, txt_procedurename_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idpohistory_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idpohistory_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idpohistory_IK1);
                chk_status_I6.Checked = true;
            } 
            else
            {
                txt_idpohistory_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idpohistory_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
         
            if (txt_procedurename_100_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_procedurename_100_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_procedurename_100_I2.Focus();
                return false;
            }   

            if (_insert == true)
            {        

                if (APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(this.Name) + " where pos=" + spe_tamung_I4.Value + " and idpotype ='"+ glue_idemprequire_I1.EditValue.ToString() +"' ").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(spe_tamung_I4, Function.clsFunction.transLateText("Không được trùng"));
                    spe_tamung_I4.Focus();
                    return false;
                }
            }
            else
            {
           
            }     

            return true;
        }

        // PO
        private void loadGridLookupPOType()
        {
            string[] caption = new string[] { "Mã TT", "Tình trạng" };
            string[] fieldname = new string[] { "idstatusprocedure", "statusprocedure" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusprocedure_I1, "select idstatusprocedure, statusprocedure from statusprocedure", "statusprocedure", "idstatusprocedure", caption, fieldname, this.Name, glue_idstatusprocedure_I1.Width);
        }

        private void loadGridLookupEmployeeRequire()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idemprequire_I1, " select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemprequire_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupEmployeeAction()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idempaction_I1, " select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idempaction_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion

        private void glue_idemprequire_I1_EditValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}