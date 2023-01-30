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
    public partial class frm_DOITHU_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DOITHU_S()
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
        public string _sign = "DT";
        public string ID = "";
        public string idcommodity, idquotationdetail;

        #endregion

        #region FormEvent

        private void frm_DOITHU_S_Load(object sender, EventArgs e)
        {
            // statusForm = true;
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
                loadGridLookupPosition();
                loadGridLookupCustomer();
                LoadInfo();
                
            }
            txt_sign_20_I1.Focus();
        }

        private void frm_DOITHU_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I1.Focus();
        }

        private void frm_DOITHU_S_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Enter && !sender.GetType().ToString().Contains("MemoEdit")) 
                SendKeys.Send("{Tab}");
        }

        private void glue_idstatustype_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

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
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_iddoithu_IK1.Name) + " = '" + txt_iddoithu_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_iddoithu_IK1.Text, txt_idcustomer_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_iddoithu_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_iddoithu_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_iddoithu_IK1.Name) + " = '" + txt_iddoithu_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_iddoithu_IK1.Text, txt_idcustomer_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    passData(true);
                }
            }
            catch { }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_iddoithu_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_iddoithu_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_iddoithu_IK1);
                txt_idcommodity_I2.Text = idcommodity;
                txt_idquotationdetail_I2.Text = idquotationdetail;
                chk_status_I6.Checked = true;
                txt_sign_20_I1.Text = txt_iddoithu_IK1.Text;
            } 
            else
            {
                txt_iddoithu_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_iddoithu_IK1);
                DataTable dt = APCoreProcess.APCoreProcess.Read("select commodity from dmcommodity where idcommodity ='"+ txt_idcommodity_I2.Text +"'");
                if (dt.Rows.Count > 0)
                {
                    txt_commodity_S.Text = dt.Rows[0][0].ToString();
                }      
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

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from "+ Function.clsFunction.getNameControls(this.Name) +" where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from  " + Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_iddoithu_IK1.Name) + "='" + txt_iddoithu_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from  " + Function.clsFunction.getNameControls(this.Name) + " where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }

        private void loadGridLookupPosition()
        {
            string[] caption = new string[] { "Mã", "Loại đối thủ" };
            string[] fieldname = new string[] { "idloaidoithu", "loaidoithu" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idloaidoithu_I1, "select idloaidoithu, loaidoithu from dmloaidoithu", "loaidoithu", "idloaidoithu", caption, fieldname, this.Name, glue_idloaidoithu_I1.Width);
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH",  "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer",  "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "True",  "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS C ORDER BY C.idcustomer", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }
        
        #endregion    

        private void txt_idcustomer_100_I2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !sender.GetType().ToString().Contains("MemoEdit"))
                SendKeys.Send("{Tab}");
            DataTable dt = APCoreProcess.APCoreProcess.Read("select customer, tel, email, address from dmcustomers where idcustomer = '"+ txt_idcustomer_100_I2.Text +"'");
            if (dt.Rows.Count > 0)
            {
                txt_address_100_I2.Text = dt.Rows[0]["address"].ToString();
                txt_email_50_I2.Text = dt.Rows[0]["email"].ToString();
                txt_tel_20_I2.Text = dt.Rows[0]["tel"].ToString();
                txt_tendoithu_I2.Text = dt.Rows[0]["customer"].ToString();
            }
            else
            {
                txt_address_100_I2.Text = "";
                txt_email_50_I2.Text = "";
                txt_tel_20_I2.Text = "";
                txt_tendoithu_I2.Text = "";
                glue_idcustomer_I1.EditValue = null;
            }
        }

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {
            txt_idcustomer_100_I2.Text = glue_idcustomer_I1.EditValue.ToString();
        }

        private void txt_idcustomer_100_I2_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}