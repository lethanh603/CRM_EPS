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
    public partial class frm_BAOHANH_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_BAOHANH_S()
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
        public string _sign = "HH";
        public string ID = "";  

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
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
                loadGridLookupCustomer();
                loadGridLookupQuanly();
                loadGridLookupPriority();
                dte_ngaytao_I5.EditValue = DateTime.Now;
                LoadInfo();
                if (txt_idquotation_I1.Text != "")
                {
                    loadQuotationInfo();
                }
                else
                {
                    glue_idcustomer_I1.EditValue = "";
                    glue_manhanvienphutrach_I1.EditValue = "";
                }
            }
            chk_status_I6.Enabled = false;
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            try
            {
                glue_idcustomer_I1.Focus();
            }
            catch { }
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
            try
            {
                save();
                if (dxEp_error_S.HasErrors == false)
                {
                    InitData();
                    txt_manhiemvu_IK1.Focus();
                    passData(true);
                }
            }
            catch { }
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

        }

        private void glue_idkind_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glue_idgroup_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
                    APCoreProcess.APCoreProcess.ExcuteSQL("update dmcommodity set tenkhongdau = dbo.fChuyenCoDauThanhKhongDauNew(commodity) where idcommodity ='" + txt_manhiemvu_IK1.Text + "'");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name) + " = '" + txt_manhiemvu_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idthuexe_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_manhiemvu_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name) + " = '" + txt_manhiemvu_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idthuexe_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
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
                txt_manhiemvu_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name), Function.clsFunction.getNameControls(this.Name));
              
                dte_ngaygiaohangdudinh_I5.EditValue = null;
                dte_ngaytao_I5.EditValue = null;
                
                Function.clsFunction.Data_XoaText(this, txt_manhiemvu_IK1);
                chk_status_I6.Checked = true;
                txt_sogiodudinhthuchien_I8.Text = "0";
            } 
            else
            {
                txt_manhiemvu_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_manhiemvu_IK1);
            }
        }

        private bool checkInput()
        {
            dxEp_error_S.ClearErrors();
            if (glue_idcustomer_I1.Text == "")
            {
                glue_idcustomer_I1.Focus();
                dxEp_error_S.SetError(glue_idcustomer_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            
            if (dte_ngaytao_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaytao_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaytao_I5.Focus();
                return false;
            }
            if (dte_ngaygiaohangdudinh_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaygiaohangdudinh_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaygiaohangdudinh_I5.Focus();
                return false;
            }
            if (Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue) > Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))
            {
                dxEp_error_S.SetError(dte_ngaynhanviec_I5, Function.clsFunction.transLateText("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc"));
                dte_ngaynhanviec_I5.Focus();
                return false;
            }
            if (txt_idquotation_I1.Text == "")
            {
                dxEp_error_S.SetError(txt_idquotation_I1, Function.clsFunction.transLateText("Không được rỗng"));
                txt_idquotation_I1.Focus();
                return false;
            }
            
            return true;
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Nhân viên", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "staffname", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "True", "False", "False", "False" };
                String dkManager = "";
                if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
                {
                    dkManager = " or 1=1 ";
                }

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 " + dkManager + ") AND E.status='True' ORDER BY C.idcustomer", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }

        private void loadGridLookupKhachHang()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Mã số thuế" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tax" };
                string[] col_visible = new string[] { "True", "True", "True"};

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, C.tax FROM   dbo.NHIEMVU AS C WHERE status =1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 1, col_visible);

            }
            catch { }
        }

        

        private void loadGridLookupQuanly()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienphutrach_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienphutrach_I1.Width);
                glue_manhanvienphutrach_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }

        private void loadGridLookupPriority()
        {
            try
            {
                string[] caption = new string[] { "Mã UT", "Ưu tiên" };
                string[] fieldname = new string[] { "idpriority", "priority" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idpriority_I1, "select idpriority, priority from DMPRIORITY", "priority", "idpriority", caption, fieldname, this.Name, glue_idpriority_I1.Width);
            }
            catch { }
        }

        #endregion   

        private void dte_ngaynhanviec_I5_EditValueChanged(object sender, EventArgs e)
        {
            checkNgayDuDinh();
        }

        private void dte_ngaygiaohangdudinh_I5_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void checkNgayDuDinh()
        {
            //if (dte_ngaytao_I5.EditValue != null && dte_ngaygiaohangdudinh_I5.EditValue != null)
            //{
            //    if (Convert.ToDateTime(dte_ngaytao_I5.EditValue) > Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))
            //    {
            //        clsFunction.MessageInfo("Thông báo", "Ngày dự định phải lớn hơn ngày nhận việc");
            //    }
            //    else {
            //        cal_sogiodudinhthuchien_10_I4.Value = ((TimeSpan)(Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue) - Convert.ToDateTime(dte_ngaytao_I5.EditValue))).Days;
            //    }
               
            //}
        }

        private void dte_ngaynghiemthunoibo_I5_EditValueChanged(object sender, EventArgs e)
        {
            //checkNgayThucte();
        }

        private void dte_ngaygiaohang_I5_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    checkNgayThucte();
            //    int hours = 0;
            //    int day = 0;
            //    int minute = 0;
            //    float dayDiff = 0;
            //    checkNgayDuDinh();
            //    if (dte_ngaygiaohang_I5.EditValue.ToString() != "" && dte_ngaynhanviec_I5.EditValue.ToString() != "")
            //    {
            //        hours = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Hours;
            //        day = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Days;
            //        minute = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Minutes;
            //        dayDiff = day * 8 + ((hours > 4) ? hours - 1 : hours) + (float)minute / 60;
            //        txt_songaythuctethuchien_I8.Text = dayDiff.ToString("F1");
            //    }
            //}
            //catch (Exception ex) { }
        }

        private void checkNgayThucte()
        {
            //if (dte_ngaygiaohangdudinh_I5.EditValue != null && dte_ngaygiaohang_I5.EditValue != null)
            //{
            //    if (Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue) > Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))
            //    {
            //        clsFunction.MessageInfo("Thông báo", "Ngày giao hàng phải lớn hơn ngày nghiệm thu");
            //    }
            //    else
            //    {
            //        cal_songaythuctethuchien_10_I4.Value = ((TimeSpan)(Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue) - Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))).Days;
            //    }
            //}
        }

        private void loadQuotationInfo()
        {
            if (txt_idquotation_I1.Text == "")
            {
                return;
            }
            string sql = " SELECT T.* FROM (SELECT Q.idcustomer, Q.idemp from QUOTATION Q where invoiceeps ='" + txt_idquotation_I1.Text + "'";
            sql += " UNION";
            sql += " SELECT S.idcustomer, S.idemp as idemp from SURVEY S where S.idsurvey='" + txt_idquotation_I1.Text + "')  T";
            sql += " INNER JOIN EMPLOYEES E ON T.IDEMP=E.IDEMP where  idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'";

            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
            {
                glue_idcustomer_I1.EditValue = dt.Rows[0]["idcustomer"].ToString();
                glue_manhanvienphutrach_I1.EditValue = dt.Rows[0]["idemp"].ToString();
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Số PO/ KS không tồn tại");
            }
        }

        private void txt_idquotation_I1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadQuotationInfo();
            }
        }
    }
}