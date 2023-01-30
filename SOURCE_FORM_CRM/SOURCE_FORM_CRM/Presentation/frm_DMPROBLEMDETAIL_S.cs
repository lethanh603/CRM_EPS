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

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_DMPROBLEMDETAIL_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMPROBLEMDETAIL_S()
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
        public string _sign = "PL";
        public string ID = "";
        public string idproblem = "";
        public string idsettingdetail="";
        #endregion

        #region FormEvent

        private void frm_DMCAMPAIGN_S_Load(object sender, EventArgs e)
        {
            //statusForm = true;
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
                LoadInfo();
            }
            
            mmo_solution_I3.Focus();
        }

        private void frm_DMCAMPAIGN_S_Activated(object sender, EventArgs e)
        {
            txt_idproblem_20_I1.Focus();
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
                txt_idproblem_20_I1.Focus();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblemdetail_IK1.Name) + " = '" + txt_idproblemdetail_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idproblemdetail_IK1.Text, mmo_solution_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idproblemdetail_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_idproblemdetail_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblemdetail_IK1.Name) + " = '" + txt_idproblemdetail_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idproblemdetail_IK1.Text, mmo_solution_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idproblemdetail_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idproblemdetail_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idproblemdetail_IK1);
                txt_idproblem_20_I1.Text = idproblem;
                txt_idemp_I2.Text = Function.clsFunction.GetIDEMPByUser();
                rg_issupport_I14.EditValue = 0;
            } 
            else
            {              

                txt_idproblemdetail_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idproblemdetail_IK1);
                DataTable dt1 = APCoreProcess.APCoreProcess.Read("select * FROM DMPROBLEMDETAIL  where idproblem = '" + txt_idproblem_20_I1.Text + "' and  idemp='" + Function.clsFunction.GetIDEMPByUser() + "' and idproblemdetail='" + txt_idproblemdetail_IK1.Text + "' ");
                if (dt1.Rows.Count == 0)
                {
                    btn_insert_allow_insert.Enabled = false;
                    bbi_allow_insert.Enabled = false;
                }       
            }                     
        }

        private bool checkInput()
        {
            if (txt_idproblem_20_I1.Text == "")
            {
                txt_idproblem_20_I1.Focus();
                dxEp_error_S.SetError(txt_idproblem_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (mmo_solution_I3.Text == "")
            {
                dxEp_error_S.SetError(mmo_solution_I3, Function.clsFunction.transLateText("Không được rỗng"));
                mmo_solution_I3.Focus();
                return false;
            }
            return true;
        }
        
        #endregion
    }
}