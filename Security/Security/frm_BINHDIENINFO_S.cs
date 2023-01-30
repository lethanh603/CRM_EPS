using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using Function;
using DevExpress.XtraBars;

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_BINHDIENINFO_S : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_BINHDIENINFO_S()
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
        public string _sign = "DV";
        public string ID = "";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public string idcustomer="";

        #endregion

        #region FormEvent

        private void frm_PLANCRM_S_Load(object sender, EventArgs e)
        {
            // statusForm = true;
            if (statusForm == true)
            {                
                
                try
                {
                    Function.clsFunction.Save_sysControl(this, this);
                    Function.clsFunction.CreateTable(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                spe_namsx_I4.EditValue = DateTime.Now.Year;
                loadGridLookupBrand();
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadInfo();
               
               
            }
            glue_idbrand_I1.Focus();
        }

        private void frm_PLANCRM_S_Activated(object sender, EventArgs e)
        {
            glue_idbrand_I1.Focus();
        }

        private void frm_PLANCRM_S_KeyDown(object sender, KeyEventArgs e)
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
                glue_idbrand_I1.Focus();
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

       
        private int findIndexInArray(string value, string[] arr)
        {
            int index = -1;
            for (int i = 0; i < arr.Length; i++)
                if (value == arr[i])
                {
                    index = i;
                    break;
                }
            return index;
        }

        #endregion

        #region Methods

        private void save()
        {
            if (!checkInput()) return;
            DataTable dt = new DataTable();
            DataRow dr;
            if (_insert == true)
            {
                spe_namsx_I4.EditValue = DateTime.Now.Year;
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_iddevice_IK1.Name) + " = '" + txt_iddevice_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_iddevice_IK1.Text, txt_dongco_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
            
                //strpassData(txt_iddevice_IK1.Text);
                if (call == true)
                {
                    //strpassData(txt_iddevice_IK1.Text);                    
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
                dte_upadatedat_I5.EditValue = DateTime.Now;
                Function.clsFunction.Edit_data(this, this.Name,"iddevice",ID);
                
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_iddevice_IK1.Name) + " = '" + txt_iddevice_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_iddevice_IK1.Text, txt_dongco_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                
                passData(true);
            }
        }

       

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_iddevice_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_iddevice_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_iddevice_IK1);
                txt_idcustomer_I2.Text = idcustomer;
            } 
            else
            {
                txt_iddevice_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_iddevice_IK1);            
            }
            //gv_mission_C.Columns[0].OptionsColumn.AllowEdit = false;     
            
        }
        private void loadGridLookupBrand()
        {
            string[] caption = new string[] { "Mã Hiệu", "Nhãn hiệu" };
            string[] fieldname = new string[] { "idbrand", "brand" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idbrand_I1, "select idbrand, brand from dmbrand where idbrandtype='BT000001'  and status =1", "brand", "idbrand", caption, fieldname, this.Name, glue_idbrand_I1.Width);
        }
        private bool checkInput()
        {
            if (glue_idbrand_I1.EditValue == "")
            {
                glue_idbrand_I1.Focus();
                dxEp_error_S.SetError(glue_idbrand_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            

            return true;
        }
       
        #endregion 
        
    }
}