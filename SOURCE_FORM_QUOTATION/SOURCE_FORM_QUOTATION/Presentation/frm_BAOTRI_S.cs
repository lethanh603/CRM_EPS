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

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_BAOTRI_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_BAOTRI_S()
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
        public string _sign = "CD";
        public string ID = "";  

        #endregion

        #region FormEvent

        private void frm_DMCAMPAIGN_S_Load(object sender, EventArgs e)
        {
            statusForm = false;
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
                loadGridLookupEmployee();
                loadGridLookupCustomer();
                loadGridLookupLoaiBaoTri();
                rad_loaibaohanh_I6_SelectedIndexChanged(sender,e);
                dte_ngaytao_I5.EditValue = DateTime.Now;
                dte_ngaybatdau_I5.EditValue = DateTime.Now;
                LoadInfo();
                
            }

            txt_idquotation_I1.Focus();
        }

        private void frm_DMCAMPAIGN_S_Activated(object sender, EventArgs e)
        {
            txt_idquotation_I1.Focus();
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
                txt_idquotation_I1.Focus();
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

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_mabaotri_IK1.Name) + " = '" + txt_mabaotri_IK1.Text + "'"));
                    
                if (call == true)
                {
                    strpassData(txt_mabaotri_IK1.Text);                    
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
                if (!clsFunction.MessageDelete("Thông báo", "Lịch trình bảo trì/ bảo hành đã được thiết lập, nếu bạn sửa đỗi sẽ phải thiết lập lại lịch trình bảo trì/bảo hành"))
                {
                    return;
                }
                else
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete baotridetail where mabaotri='" + txt_mabaotri_IK1.Text + "'");
                }
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_mabaotri_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_mabaotri_IK1.Name) + " = '" + txt_mabaotri_IK1.Text + "'"));
                 
                passData(true);
            }
            this.Close();
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                glue_idemp_I1.EditValue = Function.clsFunction.GetIDEMPByUser();
                txt_mabaotri_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_mabaotri_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                //Function.clsFunction.Data_XoaText(this, txt_mabaotri_IK1);
            } 
            else
            {
                txt_mabaotri_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_mabaotri_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            if (Convert.ToBoolean(rad_khachhang_I6.EditValue))
            {
                if (txt_idquotation_I1.Text == "")
                {
                    txt_idquotation_I1.Focus();
                    dxEp_error_S.SetError(txt_idquotation_I1, Function.clsFunction.transLateText("Không được rỗng"));
                    return false;
                }
            }            

            return true;
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1, "select IDEMP, StaffName from EMPLOYEES where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_idemp_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupLoaiBaoTri()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã Loại", "Tên Loại" };
                string[] fieldname = new string[] { "idloaibaotri", "loaibaotri" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idloaibaotri_I1, "select idloaibaotri, loaibaotri from DMLOAIBAOTRI where status =1", "loaibaotri", "idloaibaotri", caption, fieldname, this.Name, glue_idloaibaotri_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
        
        #endregion      

        private void rad_khachhang_I6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_idquotation_I1.Text = "";
            txt_idcustomer_I1.Text = "";
            glue_idcustomer_S.Text = "";
            if (Convert.ToBoolean(rad_khachhang_I6.EditValue) == false)
            {
                txt_idquotation_I1.Enabled = true;
                txt_idcustomer_I1.Text = "EPS";
                glue_idcustomer_S.Text = "EPS Việt Nam";
            }
            else
            {
                txt_idquotation_I1.Enabled = false;
            }
        }

        private void txt_idquotation_I1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadQuotationInfo();
            }
        }

        private void loadQuotationInfo()
        {
            if (txt_idquotation_I1.Text == "")
            {
                return;
            }
            string sql = "SELECT TOP 1 Q.idcustomer, Q.idemp, placedelivery from QUOTATION Q ";
            sql += " INNER JOIN EMPLOYEES E ON Q.IDEMP=E.IDEMP where  invoiceeps ='" + txt_idquotation_I1.Text + "' OR quotationno='"+ txt_idquotation_I1.Text +"' AND idstatusquotation='ST000004'";

            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
            {
                txt_idcustomer_I1.Text = dt.Rows[0]["idcustomer"].ToString();
                glue_idemp_I1.EditValue = dt.Rows[0]["idemp"].ToString();
                glue_idcustomer_S.EditValue = dt.Rows[0]["idcustomer"].ToString();
                txt_diachibaotri_500_I2.Text = dt.Rows[0]["placedelivery"].ToString();
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Số PO/ BG không tồn tại");
            }
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH" };
                string[] fieldname = new string[] { "idcustomer", "customer"};
                string[] col_visible = new string[] { "True", "True"};

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_S, "SELECT    C.idcustomer, C.customer FROM   dbo.DMCUSTOMERS AS C ", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_S.Width * 2, col_visible);

            }
            catch { }
        }

        private void rad_loaibaohanh_I6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(rad_loaibaohanh_I6.EditValue))
            {
                lblSogio.Visible = false;
                spe_sogio_I4.Visible = false;
                spe_solanbaotritheogio_I4.Visible = false;

                lblSothang.Visible = true;
                spe_sothang_I4.Visible = true;
                dte_ngaybatdau_I5.Visible = true;
                lblNgaybatdau.Text = "Ngày bắt đầu:";
            }
            else
            {
                lblSogio.Visible = true;
                spe_sogio_I4.Visible = true;
                spe_solanbaotritheogio_I4.Visible = true;
                lblNgaybatdau.Text = "Số lần bảo trì:";
                lblSothang.Visible = false;
                spe_sothang_I4.Visible = false;
                dte_ngaybatdau_I5.Visible = false;
            }
        }
        
    }
}