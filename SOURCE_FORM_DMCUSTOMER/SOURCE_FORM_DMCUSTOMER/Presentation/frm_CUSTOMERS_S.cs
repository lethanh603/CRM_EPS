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

namespace SOURCE_FORM_DMCUSTOMER.Presentation
{
    public partial class frm_DMCUSTOMERS_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMCUSTOMERS_S()
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
        public string _sign = "KH";
        public string ID = "";
        private bool load = false;

        #endregion

        #region FormEvent

        private void frm_DMCUSTOMERS_S_Load(object sender, EventArgs e)
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
                loadGridLookupKV();
                loadGridLookupCT();
                loadGridLookupFields();
                loadGridLookupProvince();
                loadGridLookupScale();
                loadGridLookupQG();
                LoadInfo();
                glue_idprovince_I1_EditValueChanged(sender, e);
                ContextMenu _blankContextMenu = new ContextMenu();
                txt_tax_50_I2.ContextMenu = _blankContextMenu;
                Function.clsFunction.transLate("F4 Lưu - Thêm");
                Function.clsFunction.transLate("F5 Lưu - Thoát");
                if (!checkAdmin())
                {
                    chk_status_I6.Enabled = false;
                }
            }
            if (rg_idgroup_I14.EditValue.ToString() == "2")
            {
                setKhachLe(false);
            }
            else
            {
                setKhachLe(true);
            }

            load = true;
            txt_sign_20_I2.Focus();
        }

        private bool checkAdmin()
        {
            bool flag = false;
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where root = 1 AND userid = '" + Function.clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        private void frm_DMCUSTOMERS_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I2.Focus();
            
        }

        private void frm_DMCUSTOMERS_S_KeyDown(object sender, KeyEventArgs e)
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
                txt_sign_20_I2.Focus();
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

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callForm();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        private void glue_idtype_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callFormTC();
            }
            catch(Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }

        private void glue_idprovince_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callFormProvince();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idfields_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callFormFields();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idscale_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callFormScale();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idprovince_I1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //DataRow dr = (glue_idprovince_I1.Properties.GetRowByKeyValue(glue_idprovince_I1.EditValue) as DataRowView).Row;
                //glue_idregion_I1.EditValue = dr[2].ToString();
            }
            catch { }
        }
        #endregion

        #region GridEvent



        #endregion

        #region Methods

        private void save()
        {
            dxEp_error_S.ClearErrors();
            if (!checkInput()) return;
            DataTable dt = new DataTable();
            DataRow dr;
            if (_insert == true)
            {
                txt_idcustomer_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idcustomer_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                txt_sign_20_I2.Text = txt_idcustomer_IK1.Text;
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcustomer_IK1.Name) + " = '" + txt_idcustomer_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcustomer_IK1.Text, txt_customer_300_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                dt = APCoreProcess.APCoreProcess.Read("SELECT * from EMPCUS");
                dr = dt.NewRow();
                dr["idemp"] = Function.clsFunction.GetIDEMPByUser();
                dr["idcustomer"] = txt_idcustomer_IK1.Text;
                dr["status"] = true;
                dr["userid1"] = Function.clsFunction._iduser;
                dr["date1"] = Function.clsFunction.getDateServer();
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
                if (call == true)
                {
                    strpassData(txt_idcustomer_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,"idcustomer",ID);
                //dt = APCoreProcess.APCoreProcess.Read("SELECT * from DMCUSTOMERS where idcustomer='" + txt_idcustomer_IK1.Text + "'");
                //if (dt.Rows.Count > 0)
                //{
                //    dr = dt.Rows[0];
                //    dr["userid2"] = Function.clsFunction._iduser;
                //    dr["date2"] = Function.clsFunction.getDateServer();
                //    dt.EndInit();
                //    APCoreProcess.APCoreProcess.Save(dr);
                //}
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idcustomer_IK1.Name) + " = '" + txt_idcustomer_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcustomer_IK1.Text, txt_customer_300_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
          

            if (_insert==true)
            {
                txt_idcustomer_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idcustomer_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idcustomer_IK1);
                txt_sign_20_I2.Text = txt_idcustomer_IK1.Text;
                rg_idgroup_I14.EditValue = 0;
                chk_status_I6.Checked = true;
                lbl_userid1_10_I2.Text = Function.clsFunction._iduser;
                dte_date1_I5.EditValue = Function.clsFunction.getDateServer();
            } 
            else
            {
                txt_idcustomer_IK1.Text = ID;              
                lbl_userid2_10_I2.Text = Function.clsFunction._iduser;
                dte_date2_I5.EditValue = Function.clsFunction.getDateServer();
                Function.clsFunction.Data_Binding1(this, txt_idcustomer_IK1);     
       
            }                     
        }

        private bool checkInput()
        {
            // check trùng khách hàng

            if (txt_sign_20_I2.Text == "")
            {
                txt_sign_20_I2.Focus();
                dxEp_error_S.SetError(txt_sign_20_I2, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }



            if (txt_customer_300_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_customer_300_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_customer_300_I2.Focus();
                return false;
            }
            if (glue_idregion_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idregion_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idregion_I1.Focus();
                return false;
            }
            if (txt_tax_50_I2.Text == ""  && rg_idgroup_I14.EditValue.ToString() !="2")
            {
                dxEp_error_S.SetError(txt_tax_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_tax_50_I2.Focus();
                return false;
            }

            if (txt_surrogate_50_I2.Text == "" && rg_idgroup_I14.EditValue.ToString() != "2")
            {
                dxEp_error_S.SetError(txt_surrogate_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_surrogate_50_I2.Focus();
                return false;
            }
            if (rg_idgroup_I14.EditValue.ToString() == "2" && txt_tel_50_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_tel_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_tel_50_I2.Focus();
                return false;
            }

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from DMCUSTOMERS where sign='" + txt_sign_20_I2.Text + "'").Rows.Count > 0 && 1==2)
                {
                    dxEp_error_S.SetError(txt_sign_20_I2, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I2.Focus();
                    return false;
                }

                if (APCoreProcess.APCoreProcess.Read("select * from DMCUSTOMERS where customer='" + txt_customer_300_I2.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_customer_300_I2, Function.clsFunction.transLateText("Không được trùng"));
                    txt_customer_300_I2.Focus();
                    return false;
                }
                
                if (txt_tax_50_I2.Text != "")
                {
                    DataTable dtTax = APCoreProcess.APCoreProcess.Read("select top 1 K.customer, EM.staffname  from DMCUSTOMERS K inner join EMPCUS E ON K.idcustomer = E.idcustomer inner join EMPLOYEES EM on E.idemp = EM.idemp and tax='" + txt_tax_50_I2.Text + "' and E.status ='true' order by E.date1 desc");
                    if (dtTax.Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_tax_50_I2, Function.clsFunction.transLateText("Không được trùng"));
                        Function.clsFunction.MessageInfo("Thông báo", "Khách hàng " + dtTax.Rows[0]["customer"].ToString() + " đã tồn tại trong hệ thống, \nKhách hàng này của nhân viên " + dtTax.Rows[0]["staffname"].ToString() + " đã nhập");
                        txt_tax_50_I2.Focus();
                        checkCustomerDuplicate();
                        return false;
                    }
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign, customer, tax from DMCUSTOMERS where idcustomer='" + txt_idcustomer_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from DMCUSTOMERS where sign='" + txt_sign_20_I2.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I2, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I2.Focus();
                        return false;
                    }

                    if (APCoreProcess.APCoreProcess.Read("select * from DMCUSTOMERS where customer='" + txt_customer_300_I2.Text + "' and customer <>'" + dt.Rows[0]["customer"].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_customer_300_I2, Function.clsFunction.transLateText("Không được trùng"));
                        txt_customer_300_I2.Focus();
                        return false;
                    }

                    if (APCoreProcess.APCoreProcess.Read("select * from DMCUSTOMERS where tax='" + txt_tax_50_I2.Text + "' and tax <>'" + dt.Rows[0]["tax"].ToString() + "'").Rows.Count > 0 && txt_tax_50_I2.Text !="")
                    {
                        dxEp_error_S.SetError(txt_tax_50_I2, Function.clsFunction.transLateText("Không được trùng"));
                        txt_tax_50_I2.Focus();
                        checkCustomerDuplicate();
                        return false;
                    }
                } 
            }     

            return true;
        }

        private void checkCustomerDuplicate()
        {
            try
            {
                DataTable dtcus = APCoreProcess.APCoreProcess.Read("select idcustomer from dmcustomers where tax= N'" + txt_tax_50_I2.Text + "'");
                if (dtcus.Rows.Count > 0)
                {
                    String idcus = dtcus.Rows[0][0].ToString();
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("CustomerDuplicate");            
                    DataTable dtEmcus = APCoreProcess.APCoreProcess.Read("select idemp from EMPCUS where idcustomer=N'" + idcus + "'");
                    if (dtEmcus.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["idempfirst"] = dtEmcus.Rows[0][0].ToString();
                        dr["idemplast"] = Function.clsFunction.GetIDEMPByUser();
                        dr["idcustomer"] = idcus;
                        dr["datedup"] = DateTime.Now;
                        dr["id"] = Function.clsFunction.layMa("DP","id","CustomerDuplicate");
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                // Function.clsFunction.MessageInfo("Thông báo","Lỗi hệ thống");
            }
        }
        
        private void loadGridLookupCT()
        {
            string[] caption = new string[] { "Mã loại", "Loại khách hàng" };
            string[] fieldname = new string[] { "idtype", "customertype" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idtype_I1,"select idtype, customertype from dmcustomertype","customertype","idtype",caption,fieldname, this.Name, glue_idtype_I1.Width);
        }

        private void callFormTC()
        {
            //TODO
            SOURCE_FORM_DMCUSTOMERTYPE.Presentation.frm_DMCUSTOMERTYPE_S frm = new SOURCE_FORM_DMCUSTOMERTYPE.Presentation.frm_DMCUSTOMERTYPE_S();
            frm.strpassData = new SOURCE_FORM_DMCUSTOMERTYPE.Presentation.frm_DMCUSTOMERTYPE_S.strPassData(getStringValueTC);
            frm._insert = true;
            frm.call = true;
            frm._sign = "TC";
            frm.ShowDialog();
        }

        private void getStringValueTC(string value)
        {
            if (value != "")
            {
                glue_idtype_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idtype, customertype from dmcustomertype where status=1");
                glue_idtype_I1.EditValue = value;
            }
        }

        // KHU VUC
        private void loadGridLookupKV()
        {
            string[] caption = new string[] { "Mã Vùng", "Tên Vùng" };
            string[] fieldname = new string[] { "idregion", "region" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idregion_I1, "select idregion, region from dmregion", "region", "idregion", caption, fieldname, this.Name, glue_idregion_I1.Width);
        }

        private void loadGridLookupQG()
        {
            string[] caption = new string[] { "Mã QG", "Quốc gia" };
            string[] fieldname = new string[] { "idarea", "area" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idarea_I1, "select id as idarea, area from dmarea", "area", "idarea", caption, fieldname, this.Name, glue_idarea_I1.Width);
        }

        private void callForm()
        {
            SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S frm = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S();
            frm.strpassData = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "KV";
            frm.ShowDialog();
        }

        private void getStringValue(string value)
        {
            if (value != "")
            {
                glue_idregion_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select id,area from dmarea where status=1");
                glue_idregion_I1.EditValue = value;
            }
        }

        // TINH THANH
        private void loadGridLookupProvince()
        {
            string[] caption = new string[] { "Mã tỉnh", "Tên tỉnh", "Mã KV" };
            string[] fieldname = new string[] { "idprovince", "province", "id" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idprovince_I1, "select idprovince, province,id from dmprovince", "province", "idprovince", caption, fieldname, this.Name, glue_idprovince_I1.Width);
            glue_idprovince_I1.Properties.View.Columns[2].Visible = false;
        }

        private void callFormProvince()
        {
            SOURCE_FORM_DMPROVINCE.Presentation.frm_DMPROVINCE_S frm = new SOURCE_FORM_DMPROVINCE.Presentation.frm_DMPROVINCE_S();
            frm.strpassData = new SOURCE_FORM_DMPROVINCE.Presentation.frm_DMPROVINCE_S.strPassData(getStringValueProvince);
            frm._insert = true;
            frm.call = true;
            frm._sign = "TT";
            frm.ShowDialog();
        }

        private void getStringValueProvince(string value)
        {
            if (value != "")
            {
                glue_idprovince_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idprovince,province from dmprovince where status=1");
                glue_idprovince_I1.EditValue = value;
            }
        }

        // FIELDS
        private void loadGridLookupFields()
        {
            string[] caption = new string[] { "Mã", "Lĩnh vực" };
            string[] fieldname = new string[] { "idfields", "fieldname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idfields_I1, "select idfields, fieldname from dmfields", "fieldname", "idfields", caption, fieldname, this.Name, glue_idfields_I1.Width);
        }

        private void callFormFields()
        {
            SOURCE_FORM_DMFIELDS.Presentation.frm_DMFIELDS_S frm = new SOURCE_FORM_DMFIELDS.Presentation.frm_DMFIELDS_S();
            frm.strpassData = new SOURCE_FORM_DMFIELDS.Presentation.frm_DMFIELDS_S.strPassData(getStringValueFields);
            frm._insert = true;
            frm.call = true;
            frm._sign = "LV";
            frm.ShowDialog();
        }

        private void getStringValueFields(string value)
        {
            if (value != "")
            {
                glue_idfields_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idfields,fieldname from dmfields where status=1");
                glue_idfields_I1.EditValue = value;
            }
        }


        // SCALE
        private void loadGridLookupScale()
        {
            string[] caption = new string[] { "Mã", "Quy mô" };
            string[] fieldname = new string[] { "idscale", "scale" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idscale_I1, "select idscale, scale from dmscale", "scale", "idscale", caption, fieldname, this.Name, glue_idscale_I1.Width);
        }

        private void callFormScale()
        {
            SOURCE_FORM_DMSCALE.Presentation.frm_DMSCALE_S frm = new SOURCE_FORM_DMSCALE.Presentation.frm_DMSCALE_S();
            frm.strpassData = new SOURCE_FORM_DMSCALE.Presentation.frm_DMSCALE_S.strPassData(getStringValueScale);
            frm._insert = true;
            frm.call = true;
            frm._sign = "QM";
            frm.ShowDialog();
        }

        private void getStringValueScale(string value)
        {
            if (value != "")
            {
                glue_idscale_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idscale,scale from dmscale where status=1");
                glue_idscale_I1.EditValue = value;
            }
        }

        #endregion        

        private void txt_tax_20_I1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar.ToString() !="-")
            {
                e.Handled = true;
            }
            //Clipboard.Clear();
        }

        private void txt_bank_50_I2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_tax_20_I1_MouseClick(object sender, MouseEventArgs e)
        {
            //Clipboard.Clear();
        }

        private void txt_tax_50_I2_EditValueChanged(object sender, EventArgs e)
        {
            if (txt_tax_50_I2.Text != "" && txt_tax_50_I2.Text.Length >= 8 && rg_idgroup_I14.EditValue.ToString() !="2" && load != false)
            {
                DataTable dtTax = APCoreProcess.APCoreProcess.Read("select top 1 K.customer, EM.staffname  from DMCUSTOMERS K inner join EMPCUS E ON K.idcustomer = E.idcustomer inner join EMPLOYEES EM on E.idemp = EM.idemp and tax='" + txt_tax_50_I2.Text + "' and E.status ='true' order by E.date1 desc");
                if (dtTax.Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_tax_50_I2, Function.clsFunction.transLateText("Không được trùng"));
                    Function.clsFunction.MessageInfo("Thông báo", "Khách hàng " + dtTax.Rows[0]["customer"].ToString() + " đã tồn tại trong hệ thống, \nKhách hàng này của nhân viên " + dtTax.Rows[0]["staffname"].ToString() + " đã nhập");
                    txt_tax_50_I2.Focus();
          
                }
            }
        }

        private void rg_idgroup_I14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_idgroup_I14.EditValue.ToString() == "2")
            {
                setKhachLe(false);
            }
            else
            {
                setKhachLe(true);
            }
        }

        private void setKhachLe(bool flag)
        {
            glue_idarea_I1.Enabled = flag;
            glue_idfields_I1.Enabled = flag;
            glue_idregion_I1.Enabled = flag;
            glue_idtype_I1.Enabled = flag;
            txt_station_300_I2.Enabled = flag;
            txt_surrogate_50_I2.Enabled = flag;
            txt_tax_50_I2.Enabled = flag;
            txt_passport_50_I2.Enabled = flag;
            txt_atm_50_I2.Enabled = flag;
            txt_bank_50_I2.Enabled = flag;
            txt_website_50_I2.Enabled = flag;
            txt_nick_50_I2.Enabled = flag;
            cal_doanhso_10_I4.Enabled = flag;
            cal_solaodong_6_I4.Enabled = flag;
            cal_sothietbi_10_I4.Enabled = flag;
            txt_fax_50_I2.Enabled = flag;
            txt_mobile_50_I2.Enabled = flag;
            glue_idscale_I1.Enabled = flag;
            cal_nguonvon_10_I4.Enabled = flag;
            if (flag == false)
            {
                labelControl20.ForeColor = Color.Black;
                labelControl7.ForeColor = Color.Black;
                labelControl8.ForeColor = Color.Red;
            }
            else
            {
                labelControl20.ForeColor = Color.Red;
                labelControl7.ForeColor = Color.Red;
                labelControl8.ForeColor = Color.Black;
            }
        }
    }
}