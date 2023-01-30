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
using System.Diagnostics;
using System.IO;
using DevExpress.XtraReports.UI;
using Function;

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_survey_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_survey_S()
        {
            InitializeComponent();
        }

        #region Var

        public bool _insert = true;
        public bool call = false;
        public bool statusForm;
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "KS";
        public string ID = "";
        public string idcustomer = "";
        public string idquotation = "";

        #endregion

        #region FormEvent

        private void frm_survey_Load(object sender, EventArgs e)
        {
            //statusForm = true;
            Function.clsFunction._keylience = true;
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
                loadGridLookupCustomer();
                loadGridLookupCommodity();
                LoadInfo();
            }           
            txt_surveyno_20_I2.Focus();
        }

        private void frm_survey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btn_insert_allow_insert.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                bbi_allow_insert.PerformClick();
            }
            else if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                bbi_allow_insert_S.PerformClick();
            }
           
        }

        #endregion

        #region ButtonEvent

        private void bbi_allow_insert_S_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\File\\template_survey.dotx");
            }
            catch
            {
                Function.clsFunction.MessageInfo("Thông báo","File template không tồn tại");
            }
        }

        private void btn_insert_allow_insert_Click(object sender, EventArgs e)
        {
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                // print
                Print();
                txt_surveyno_20_I2.Focus();
            }
        }

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

        #endregion

        #region Event

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadInfoCustomer();
        }

        private void glue_idcustomer_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callForm();
            }
            catch { }
        }

        private void glue_idcommodity_S_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callFormCommodity();
            }
            catch { }
        }

        #endregion

        #region Method

        private void LoadInfo()
        {
            this.Focus();
            if (_insert == true)
            {
                txt_idsurvey_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idsurvey_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idsurvey_IK1);
                rg_sex_I14.EditValue = 0;
                txt_surveyno_20_I2.Text = txt_idsurvey_IK1.Text;
                dte_dateend_I5.EditValue = null;
            }
            else
            {
                txt_idsurvey_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idsurvey_IK1);
            }
            txt_idcustomer_S.Text = idcustomer;
            glue_idcustomer_S.EditValue = idcustomer;
            txt_idquotation_I1.Text = idquotation;
        }

        // Customer

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_S, "select idcustomer, customer, tel, fax, address from dmcustomers where status=1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_S.Width , col_visible);

            }
            catch { }
        }

        private void loadInfoCustomer()
        {
            try
            {
                if (_insert == true)
                {
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("select * from dmcustomers where idcustomer='" + glue_idcustomer_S.EditValue.ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        txt_address_I2.Text = dt.Rows[0]["address"].ToString();
                        txt_tel_12_I2.Text = dt.Rows[0]["tel"].ToString();
                    }
                }
            }
            catch { }
        }

        private void callForm()
        {
            try
            {
                SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S frm = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S();
                frm.strpassData = new SOURCE_FORM_DMCUSTOMER.Presentation.frm_DMCUSTOMERS_S.strPassData(getStringValueKh);
                frm._insert = true;
                frm.call = true;
                frm._sign = "KH";
                frm.ShowDialog();
            }
            catch { }
        }

        private void getStringValueKh(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idcustomer_S.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idcustomer, customer, tel, fax, address  from dmcustomers where status=1");
                    glue_idcustomer_S.EditValue = value;
                }
            }
            catch { }
        }

        // Commodity
        private void loadGridLookupCommodity()
        {
            try
            {
                string[] caption = new string[] { "Mã TB", "Tên thiết bị",};
                string[] fieldname = new string[] { "idcommodity", "commodity"};
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcommodity_I1, "select idcommodity, commodity from dmcommodity where status=1", "commodity", "idcommodity", caption, fieldname, this.Name, glue_idcommodity_I1.Width , col_visible);

            }
            catch { }
        }

        private void loadInfoCommodity()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select idcommodity, sign,model,spec, commodity from dmcommodity where idcommodity='" + glue_idcommodity_I1.EditValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txt_model_200_I2.Text = dt.Rows[0]["model"].ToString();
                    txt_sign_200_I2.Text = dt.Rows[0]["sign"].ToString();
                    mmo_spec_I3.Text = dt.Rows[0]["spec"].ToString();
                }
            }
            catch { }
        }

        private void callFormCommodity()
        {
            try
            {
                SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S frm = new SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S();
                frm.strpassData = new SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_S.strPassData(getStringValueCommodity);
                frm._insert = true;
                frm.call = true;
                frm._sign = "HH";
                frm.ShowDialog();
            }
            catch { }
        }

        private void getStringValueCommodity(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idcommodity_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity, sign,model, commodity from dmcommodity where idcommodity='" + glue_idcommodity_I1.EditValue.ToString() + "'");
                    glue_idcommodity_I1.EditValue = value;
                }
            }
            catch { }
        }

        private void save()
        {
            try
            {
                if (!checkInput()) return;
                if (_insert == true)
                {
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idsurvey_IK1.Name) + " = '" + txt_idsurvey_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idsurvey_IK1.Text, txt_idsurvey_IK1.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                  
                    if (call == true)
                    {
                        strpassData(txt_idsurvey_IK1.Text);
                    }
               
                    passData(true);
                    //this.Hide();  
                    dxEp_error_S.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idsurvey_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idsurvey_IK1.Name) + " = '" + txt_idsurvey_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idsurvey_IK1.Text, txt_idsurvey_IK1.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
               
                    passData(true);
                }
            }
            catch(Exception ex)
            {
                clsFunction.MessageInfo("Thông báo","Lỗi hệ thống "+ ex.Message);
            }
        }

        private bool checkInput()
        {
            if (txt_surveyno_20_I2.Text == "")
            {
                txt_surveyno_20_I2.Focus();
                dxEp_error_S.SetError(txt_surveyno_20_I2, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (glue_idcustomer_S.Text == "")
            {
                dxEp_error_S.SetError(glue_idcustomer_S, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idcustomer_S.Focus();
                return false;
            }
            if (txt_contact_50_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_contact_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_contact_50_I2.Focus();
                return false;
            }
            if (glue_idcommodity_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idcommodity_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idcommodity_I1.Focus();
                return false;
            }           

            return true;
        }

        private void Print()
        {
            try
            {
                XtraReport report = XtraReport.FromFile(Application.StartupPath+"\\Report\\"+ "frx_survey_S.repx", true);
                //report.FindControl("xxx", true).Text="alo";
                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //dt = APCoreProcess.APCoreProcess.Read(dtRP.Rows[0]["query"].ToString());
                dt = APCoreProcess.APCoreProcess.Read("SELECT     S.surveyno, C.customer, S.contact, S.sex, S.address, S.tel, S.model, S.[content], S.spec, CO.commodity, S.sign, S.description, S.note, S.useraction, S.userrequest,     S.daterequest, S.dateaction, S.idsurvey FROM         dbo.QUOTATION AS Q INNER JOIN dbo.DMCUSTOMERS AS C ON Q.idcustomer = C.idcustomer INNER JOIN  dbo.SURVEY AS S INNER JOIN    dbo.DMCOMMODITY AS CO ON S.idcommodity = CO.idcommodity ON Q.idexport = S.idquotation WHERE     (S.idsurvey = N'" + txt_idsurvey_IK1.Text + "')");
                ds.Tables.Add(dt);
                report.DataSource = ds;
                ReportPrintTool tool = new ReportPrintTool(report);
                for (int i = 0; i < report.Parameters.Count; i++)
                {
                    report.Parameters[i].Visible = false;
                }
                tool.ShowPreviewDialog();
                
            }
            catch { }
        }

        #endregion


               
    }
}