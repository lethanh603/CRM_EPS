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
    public partial class frm_DEVICEFORRENT_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DEVICEFORRENT_S()
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
                loadGridLookupXe();
                loadGridLookupEmployee();
                LoadInfo();
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
                    txt_idthuexe_IK1.Focus();
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
                    APCoreProcess.APCoreProcess.ExcuteSQL("update dmcommodity set tenkhongdau = dbo.fChuyenCoDauThanhKhongDauNew(commodity) where idcommodity ='" + txt_idthuexe_IK1.Text + "'");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idthuexe_IK1.Name) + " = '" + txt_idthuexe_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idthuexe_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_idthuexe_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idthuexe_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idthuexe_IK1.Name) + " = '" + txt_idthuexe_IK1.Text + "'"));
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
                txt_idthuexe_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idthuexe_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idthuexe_IK1);
                chk_status_I6.Checked = true;
                glue_idcommodity_I1.EditValue = null;
                glue_idemp_I1.EditValue = clsFunction.GetIDEMPByUser();
            } 
            else
            {
                txt_idthuexe_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idthuexe_IK1);
                             
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
            if (glue_idcommodity_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idcommodity_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idcommodity_I1.Focus();
                return false;
            }
            if (glue_idemp_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idemp_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idemp_I1.Focus();
                return false;
            }
            if (dte_ngaybatdau_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaybatdau_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaybatdau_I5.Focus();
                return false;
            }
            if (dte_ngayketthuc_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngayketthuc_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngayketthuc_I5.Focus();
                return false;
            }

            if (Convert.ToDateTime(Convert.ToDateTime(dte_ngaybatdau_I5.EditValue).ToString("yyyy-MM-dd")) > Convert.ToDateTime(Convert.ToDateTime(dte_ngayketthuc_I5.EditValue).ToString("yyyy-MM-dd")))
            {
                dxEp_error_S.SetError(dte_ngaybatdau_I5, Function.clsFunction.transLateText("Ngày kết thúc hợp đồng phải lớn hơn ngày bắt đầu hợp đồng"));
                dte_ngaybatdau_I5.Focus();
                return false;
            }

            DataTable dtCheckExits = APCoreProcess.APCoreProcess.Read("select idthuexe, ngaybatdau, ngayketthuc from DEVICEFORRENT WHERE idcommodity = '"+ glue_idcommodity_I1.EditValue.ToString() +"' AND cast( ngayketthuc, as date) > cast( getDate(), as date)");
            if (dtCheckExits.Rows.Count > 0)
            {
                dxEp_error_S.SetError(glue_idcommodity_I1, Function.clsFunction.transLateText("Xe này đang được cho thuê từ ngày : " + Convert.ToDateTime(dtCheckExits.Rows[0]["ngaybatdau"]).ToString("dd/MM/yyyy") + " đến ngày: " + Convert.ToDateTime(dtCheckExits.Rows[0]["ngayketthuc"]).ToString("dd/MM/yyyy") + ", vui lòng kiểm tra lại thông tin"));
                glue_idcommodity_I1.Focus();
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

        private void loadGridLookupXe()
        {
            try
            {
                string[] caption = new string[] { "Mã Xe", "Tên Xe", "Số xe" };
                string[] fieldname = new string[] { "idcommodity", "commodity", "sign" };
                string[] col_visible = new string[] { "True", "True", "True"};

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcommodity_I1, "SELECT    C.idcommodity, C.commodity, C.sign FROM   dbo.DMRENTDEVICE AS C WHERE status =1", "commodity", "idcommodity", caption, fieldname, this.Name, glue_idcommodity_I1.Width * 1, col_visible);

            }
            catch { }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_I1.Width);
            }
            catch { }
        }

        #endregion   

        
    }
}