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
    public partial class frm_ChoThueXeChiTiet_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor

        public frm_ChoThueXeChiTiet_S()
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
        public string idchothuexe = "";

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            statusForm = true;
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
                LoadInfo();
            }           
        }
        private void loadGridLookupCampaign()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã CD", "Chiến dịch" };
                string[] fieldname = new string[] { "idcampaign", "campaign" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusactive_I1, "select  idcampaign, campaign from dmcampaign where  (convert(datetime, cast(datediff(d,0,getdate()) as datetime) ,103)   between convert(datetime, cast(datediff(d,0,fromdate) as datetime) ,103) and  convert(datetime, cast(datediff(d,0,todate) as datetime) ,103) ) or unlimited =1 ", "campaign", "idcampaign", caption, fieldname, this.Name, glue_idstatusactive_I1.Width, col_visible);
            }
            catch { }
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
                  
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name) + " = '" + txt_idchothuexechitiet_IK1.Text + "'"));
                   //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcammiss_IK1.Text, txt_email_S.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        //strpassData(txt_idcammiss_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name) + " = '" + txt_idchothuexechitiet_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idchothuexechitiet_IK1.Text, txt_email_S.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

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
                txt_idchothuexechitiet_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idchothuexechitiet_IK1);
            } 
            else
            {
                txt_idchothuexechitiet_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idchothuexechitiet_IK1);
                           
            }                     
        }

        private bool checkInput()
        {
          
            if (glue_idstatusxe_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idstatusxe_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idstatusxe_I1.Focus();
                return false;
            }

            if (_insert == true)
            {
               
            }

            return true;
        }
        
        private void loadGridLookupTask()
        {
            string[] caption = new string[] { "Mã HĐ", "Hành động" };
            string[] fieldname = new string[] { "idtask", "taskname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusxe_I1, "select idtask, taskname from dmtask where status=1", "taskname", "idtask", caption, fieldname, this.Name, glue_idstatusxe_I1.Width);
        } 

        #endregion       

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