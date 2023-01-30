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
using Function;

namespace SOURCE_FORM_DEVICEFORRENT.Presentation
{
    public partial class frm_DEVICERENTDETAIL_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor

        public frm_DEVICERENTDETAIL_S()
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
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusxe_I1, "select  idcampaign, campaign from dmcampaign where  (convert(datetime, cast(datediff(d,0,getdate()) as datetime) ,103)   between convert(datetime, cast(datediff(d,0,fromdate) as datetime) ,103) and  convert(datetime, cast(datediff(d,0,todate) as datetime) ,103) ) or unlimited =1 ", "campaign", "idcampaign", caption, fieldname, this.Name, glue_idstatusxe_I1.Width, col_visible);
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
                    if (!checkAdmin())
                    {
                        clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                        return;
                    }
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idchothuexechitiet_IK1.Name) + " = '" + txt_idchothuexechitiet_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idchothuexechitiet_IK1.Text, txt_email_S.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    dxEp_error_S.ClearErrors();
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
                txt_idthuexe_I1.Text = idchothuexe;
                dte_createdate_I5.EditValue = Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
                dte_ngaythuchien_I5.EditValue = null;
                dte_ngayvietmail_I5.EditValue = null;
            } 
            else
            {
                txt_idchothuexechitiet_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idchothuexechitiet_IK1);
                           
            }                     
        }

        private bool checkInput()
        {
            if (APCoreProcess.APCoreProcess.Read("select * from STARTRENTDEVICE where cast('" + dte_createdate_I5.EditValue + "' as date) between DATEADD (day , -1 , ngaybatdau )    and  DATEADD (day , 1 , ngayketthuc )  and idthuexe = '" + txt_idthuexe_I1.Text + "'").Rows.Count == 0)
            {
                Function.clsFunction.MessageInfo("Thông báo","Bạn chưa thiết lập quản lý giờ hoạt động hàng tháng cho hợp đồng thuê xe "+ txt_idthuexe_I1.Text +", vui lòng kiểm tra lại");
                dxEp_error_S.SetError(dte_createdate_I5, Function.clsFunction.transLateText("Giờ hoạt động hàng tháng chưa được thiết lập"));
                dte_createdate_I5.Focus();
                return false;
            }
          
            if (glue_idstatusxe_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idstatusxe_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idstatusxe_I1.Focus();
                return false;
            }

            if (dte_createdate_I5.EditValue == null)
            {
                dxEp_error_S.SetError(dte_createdate_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_createdate_I5.Focus();
                return false;
            }

            if (dte_ngayvietmail_I5.EditValue != null && Convert.ToDateTime( Convert.ToDateTime( dte_ngayvietmail_I5.EditValue).ToString("yyyy-MM-dd")) < Convert.ToDateTime(Convert.ToDateTime( dte_createdate_I5.EditValue).ToString("yyyy-MM-dd")))
            {
                dxEp_error_S.SetError(dte_ngayvietmail_I5, Function.clsFunction.transLateText("Ngày kinh doanh viết mail sau ngày phát sinh sự cố"));
                dte_ngayvietmail_I5.Focus();
                return false;
            }

            if (dte_ngaythuchien_I5.EditValue != null && Convert.ToDateTime(Convert.ToDateTime( dte_ngayvietmail_I5.EditValue).ToString("yyyy-MM-dd")) > Convert.ToDateTime(Convert.ToDateTime(dte_ngaythuchien_I5.EditValue).ToString("yyyy-MM-dd")))
            {
                dxEp_error_S.SetError(dte_ngaythuchien_I5, Function.clsFunction.transLateText("Ngày kinh doanh viết mail sau ngày phát sinh sự cố"));
                dte_ngaythuchien_I5.Focus();
                return false;
            }

            if (_insert == true)
            {
               
            }

            return true;
        }
        
        private void loadGridLookupTask()
        {
            string[] caption = new string[] { "Mã TT", "Tình trạng" };
            string[] fieldname = new string[] { "idstatusxe", "statusxe" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatusxe_I1, "select idstatusxe, statusxe from DMTINHTRANGXE where status=1", "statusxe", "idstatusxe", caption, fieldname, this.Name, glue_idstatusxe_I1.Width);
        } 

        #endregion       
               
    }
}