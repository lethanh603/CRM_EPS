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

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_NHIEMVUKYHAN_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_NHIEMVUKYHAN_S()
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
        public string iddetail = "";
        public string idplan = "";
        public string plan_type = "";

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
                LoadInfo();
                
            }
            txt_sign_20_I1.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
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
            try
            {
                save();
                if (dxEp_error_S.HasErrors == false)
                {
                    this.Close();
                }
            }
            catch{}
            this.Close();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idkyhan_IK1.Name) + " = '" + txt_idkyhan_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idkyhan_IK1.Text, txt_kyhan_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idkyhan_IK1.Text);                    
                }
                else
                {
                    //strpassData("new");  
                    LoadInfo();
                    passData(true);
                }
                
                //this.Hide();  
                //dxEp_error_S.ClearErrors();
            }
            else
            {
                Function.clsFunction.Edit_data(this, this.Name,"idkyhan",ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idkyhan_IK1.Name) + " = '" + txt_idkyhan_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idkyhan_IK1.Text, txt_kyhan_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                //strpassData("edit");  
                passData(true);
            }
            
        }

        private void LoadInfo()
        {
            loadGridLookupThucHien();
            this.Focus();
            if (_insert==true)
            {
                txt_idkyhan_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idkyhan_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idkyhan_IK1);
                chk_tinhtrang_I6.Checked = false;
                txt_sign_20_I1.Text = txt_idkyhan_IK1.Text;
                string sql = "";
                if(plan_type=="GiaoHang")
                    sql = "select count(*) from nhiemvukyhan where idplan='" + idplan + "'";
                else if(plan_type=="BaoTri")
                    sql = "select count(*) from nhiemvukyhan where idplan='" + idplan + "' and iddetail='"+ iddetail +"'";
                DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
                txt_kyhan_50_I2.Text = "Lần " + (Convert.ToInt16( dt.Rows[0][0]) +1);
                txt_idplan_I1.Text = idplan;
                txt_iddetail_I1.Text = iddetail;
                dte_ngayketthuc_I5.EditValue = null;
                dte_ngaythuchien_I5.EditValue = null;
            } 
            else
            {
                txt_idkyhan_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idkyhan_IK1);                             
            }                     
        }

        private void loadGridLookupThucHien()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienphutrach_I1, "select IDEMP, StaffName from EMPLOYEES where status =1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienphutrach_I1.Width);
                glue_manhanvienphutrach_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }

        private bool checkInput()
        {
            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (txt_kyhan_50_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_kyhan_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_kyhan_50_I2.Focus();
                return false;
            }
            

            if ( dte_ngayketthuc_I5.EditValue != null && dte_ngaythuchien_I5.EditValue != null)
            {
                if (Convert.ToDateTime( dte_ngaythuchien_I5.EditValue).Date > Convert.ToDateTime( dte_ngayketthuc_I5.EditValue).Date)
                {
                    dxEp_error_S.SetError(txt_kyhan_50_I2, Function.clsFunction.transLateText("Ngày kết thúc phải lớn hơn ngày thực hiện"));
                    dte_ngayketthuc_I5.Focus();
                    return false;
                }
            }

            string sql = "select cast( coalesce((select max(thoidiem) from NHIEMVUKYHAN where idplan='" + idplan + "'), (select dateadd(day,-1,ngaynhanviec) from PLANGIAOHANG where manhiemvu='" + idplan + "')) as date)";
            //MessageBox.Show(sql);
            DataTable dtc = APCoreProcess.APCoreProcess.Read(sql);
           
            if (dtc.Rows.Count > 0 && plan_type =="GiaoHang")
            {
                if(Convert.ToDateTime(dtc.Rows[0][0]).Date >  Convert.ToDateTime(dte_thoidiem_I5.EditValue).Date)
                {
                    //dxEp_error_S.SetError(dte_thoidiem_I5, Function.clsFunction.transLateText("Thời điểm giao hàng không hợp lệ"));
                    Function.clsFunction.MessageInfo("Thông báo", "Thời điểm giao hàng không hợp lệ");
                    dte_thoidiem_I5.Focus();
                    return false;
                }
            }

            if(dte_ngaythuchien_I5.EditValue != null)
            {
                if (Convert.ToDateTime(dte_ngaythuchien_I5.EditValue).Date < Convert.ToDateTime(dte_thoidiem_I5.EditValue).Date)
                {
                    //dxEp_error_S.SetError(dte_thoidiem_I5, Function.clsFunction.transLateText("Thời điểm giao hàng không hợp lệ"));
                    Function.clsFunction.MessageInfo("Thông báo", "Ngày thực hiện không hợp lệ");
                    dte_ngaythuchien_I5.Focus();
                    return false;
                }
            }

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from NHIEMVUKYHAN where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from NHIEMVUKYHAN where id='"+ txt_idkyhan_IK1.Text +"'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from NHIEMVUKYHAN where sign='" + txt_sign_20_I1.Text + "' and sign <>'"+ dt.Rows[0][0].ToString() +"'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }

        #endregion        
               

    }
}