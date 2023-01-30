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

namespace SOURCE_FORM_DMBAOHANH.Presentation
{
    public partial class frm_DMBAOHANH_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMBAOHANH_S()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        public bool _insert = false;
        public bool call = false;     
        public bool statusForm = false;    
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "BH";
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
                    //Function.clsFunction.CreateTable(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGridLookupBrand();
                loadGridLookupIdcommodity();
                LoadInfo();
                
            }
            txt_phutung_500_I2.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            txt_phutung_500_I2.Focus();
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
                txt_phutung_500_I2.Focus();
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

        private void loadGridLookupIdcommodity()
        {
            string[] caption = new string[] { "ID", "Sản phẩm" };
            string[] fieldname = new string[] { "idcommodity", "commodity" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idcommodity_I1, "select idcommodity, commodity from dmcommodity where status =1", "commodity", "idcommodity", caption, fieldname, this.Name, glue_idcommodity_I1.Width);
        }

        private void loadGridLookupBrand()
        {
            string[] caption = new string[] { "Mã loại", "Loại bảo trì" };
            string[] fieldname = new string[] { "idloaibaotri", "loaibaotri" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idloaibaotri_I1, "select idloaibaotri, loaibaotri from dmloaibaotri where status=1", "loaibaotri", "idloaibaotri", caption, fieldname, this.Name, glue_idloaibaotri_I1.Width);
        }

        private void save()
        {
            if (!checkInput()) return;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_id_IK1.Name) + " = '" + txt_id_IK1.Text + "'"));
                    
                if (call == true)
                {
                    strpassData(txt_id_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_id_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_id_IK1.Name) + " = '" + txt_id_IK1.Text + "'"));
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_id_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_id_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_id_IK1);
                chk_status_I6.Checked = true;
                
            } 
            else
            {
                txt_id_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_id_IK1);
                             
            }
            txt_phutung_500_I2.Text = glue_idcommodity_I1.Text;        
        }

        private bool checkInput()
        {
            if (glue_idcommodity_I1.Text == "")
            {
                glue_idcommodity_I1.Focus();
                dxEp_error_S.SetError(txt_phutung_500_I2, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            txt_phutung_500_I2.Text = glue_idcommodity_I1.Text;
            return true;
        }
        


        #endregion        




    }
}