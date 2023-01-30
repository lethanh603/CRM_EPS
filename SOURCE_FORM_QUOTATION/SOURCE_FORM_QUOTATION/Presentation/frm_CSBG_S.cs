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
    public partial class frm_CSBG_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_CSBG_S()
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
        public string idquotation = "";  

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
                dte_ngaytao_I5.EditValue = DateTime.Now;
                LoadInfo();
                txt_idexport_I1.Text = idquotation;
            }

            mmo_note_I3.Focus();
        }

        private void frm_DMCAMPAIGN_S_Activated(object sender, EventArgs e)
        {
            mmo_note_I3.Focus();
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
                mmo_note_I3.Focus();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_macs_IK1.Name) + " = '" + txt_macs_IK1.Text + "'"));
                    
                if (call == true)
                {
                    strpassData(txt_macs_IK1.Text);                    
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
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete baotridetail where mabaotri='" + txt_macs_IK1.Text + "'");
                }
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_macs_IK1.Name),txt_macs_IK1.Text);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_macs_IK1.Name) + " = '" + txt_macs_IK1.Text + "'"));
                 
                passData(true);
            }
            this.Close();
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_macs_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_macs_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                //Function.clsFunction.Data_XoaText(this, txt_mabaotri_IK1);
            } 
            else
            {
                Function.clsFunction.Data_Binding1(this, txt_macs_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            if (mmo_note_I3.Text == "")
            {
                mmo_note_I3.Focus();
                dxEp_error_S.SetError(mmo_note_I3, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            return true;
        }
        
        #endregion      
        
    }
}