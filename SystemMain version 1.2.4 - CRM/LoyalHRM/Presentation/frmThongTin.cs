//Thanh 
//Danh mục cakip
//15/5/2013
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.IO;
using System.Reflection;


namespace LoyalHRM.Presentation
{
    public partial class frm_sysINFO_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysINFO_S()
        {
            InitializeComponent();
        }

        #region Var      
        public bool them = false;
        public int index;
        public bool call = false;
        public bool statusForm=false;
        public string langues="_VI";
        public delegate void PassData(bool value);
        public PassData passData;
        public string dauma = "CTY";
        public delegate void strPassData(string value);
        public strPassData strpassData;


        #endregion

        #region Load
        private void frmNhapDVT_Load(object sender, EventArgs e)
        {  
            if (statusForm == true)
            {       
                try
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysControls where form_name='" + this.Name + "'");
                    Function.clsFunction.Save_sysControl(this, this);
                    APCoreProcess.APCoreProcess.ExcuteSQL("drop table sysInfo");
                    Function.clsFunction.CreateTable(this);
                    Function.clsFunction.setLanguageForm(this, this);
                }
                catch { }
            }
            else
            {
                string xml = "<NewDataSet>  <sysINFO>    <field>1 - Kinh Doanh</field>    <logo>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</logo>    <company>Công ty TNHH MTV THẢO NGUYÊN XANH</company>    <address>32- Lý Chính Thắng</address>    <companyid>CTY1409070001</companyid>    <email>mar@thaonguyenxanh.com.vn</email>    <fax>083.8771212</fax>    <Licence>1103TTCP_110</Licence>    <rep>Trần Long Biên</rep>    <sign>THAO NGUYEN XANH</sign>    <tax>1298345677</tax>    <tel>0907012207</tel>    <website>thaonguyenxanh.com.vn</website>  </sysINFO></NewDataSet>";
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.LoadXml(xml);

                DataSet ds = new DataSet();
                ds.ReadXml(XmlReader.Create(new StringReader(xml)));
                loadCbo();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysInfo");
                if (dt.Rows.Count > 0)
                {
                    them = false;
                    txt_companyid_IK1.Text = dt.Rows[0]["companyid"].ToString();
                }
                else
                    them = true;
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadTT();
                lbl_company_I2.Focus();
            }
        }


        private void LoadTT()
        {
            if (them == false && txt_companyid_IK1.Text == "" || them == true)
            {
                txt_companyid_IK1.Text = Function.clsFunction.layMa(dauma, Function.clsFunction.getNameControls(txt_companyid_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                them = true;   
                
            } if (them == false)
            {
                Function.clsFunction.Data_Binding1(this, txt_companyid_IK1);
                img_logo_I10.EditValue = APCoreProcess.APCoreProcess.Read("sysInfo where companyid='" + txt_companyid_IK1.Text + "'").Rows[0]["logo"];
            }
            //Function.clsFunction.Text_Control(this, langues);
            txt_address_I2.Focus();
        }

        private void GetVal(bool flag)
        {
            if (flag == true)
            {
                cbo_field_I1.Properties.Items.Clear();
                loadCbo();
            }
        }

        #endregion

        #region Event

        private void frm_sysINFO_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && btn_luu_S.Enabled == true)
                btnLuu0_Click(sender, e);
            if (e.KeyCode == Keys.F9 && btn_thoat_S.Enabled==true)
                btnThoat0_Click(sender, e);
        }

        private void img_logo_I10_Click(object sender, EventArgs e)
        {
            img_logo_I10.LoadImage();
        }
        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT sysControls.control_name,  sysControls.type, sysControls.stt, sysControls.form_name, sysPower.allow_insert,   sysPower.allow_delete, sysPower.allow_import, sysPower.allow_export, sysPower.allow_print, sysControls.text_En, sysControls.text_Vi, sysPower.allow_edit, sysControls.stt FROM         sysControls INNER JOIN  sysPower ON sysControls.IDSubMenu = sysPower.IDSubMenu WHERE     (sysControls.form_name = N'" + this.Name + "') AND (sysControls.type = N'SimpleButton') AND (sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY sysControls.stt ");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
            //    {
            //        if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
            //        {
            //            {
            //                if (dt.Rows[i]["control_name"].ToString().Trim().Contains("luu"))
            //                {
            //                    btnLuu0_Click(sender, e);
            //                }
            //                if (dt.Rows[i]["control_name"].ToString().Trim().Contains("thoat"))
            //                {
            //                    btnThoat0_Click(sender, e);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void cbo_field_I1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                if (e.Button.ToolTip == "Insert")
                {
                    frm_sysField_S frm = new frm_sysField_S();
                    frm.passData = new frm_sysField_S.PassData(GetVal);
                    frm.ShowDialog();
                }
                else
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa mẫu tin này không"))
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete sysField where fieldid='"+ Function.clsFunction.getIDfromIndex(cbo_field_I1.SelectedIndex,"sysField","fieldid") +"'");
                    GetVal(true);
                }
            }
        }

        #endregion

        #region ButtonEvent

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnThoat0_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu0_Click(object sender, EventArgs e)
        {      
            insert();
        }

        private void btn_nhatky_S_Click(object sender, EventArgs e)
        {
            SOURCE_FORM_TRACE.Presentation.frm_Trace_SH frm = new SOURCE_FORM_TRACE.Presentation.frm_Trace_SH();
            frm._object = txt_companyid_IK1.Text;
            frm.idform = this.Name;
            frm.userid = Function.clsFunction._iduser;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

        #endregion

        #region Methods

        private void loadCbo()
        {
            try
            {
                if (APCoreProcess.APCoreProcess.Read("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'sysField'").Rows.Count>0)
                {
                    ControlDev.FormatControls.LoadComBoBoxEdit1(cbo_field_I1, APCoreProcess.APCoreProcess.Read("sysField"), "", "fieldname");
                }
                else
                {
                    frm_sysField_S frm = new frm_sysField_S();
                    frm.passData = new frm_sysField_S.PassData(GetVal);
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void insert()
        {
            if (!checkInput()) return;
            if (txt_address_I2.Text != "")
            {
                if (them == true)
                {

                    Function.clsFunction.Insert_data(this, (this.Name));
                    if (img_logo_I10.Image != null)
                    {

                        MemoryStream ms = new MemoryStream();
                        img_logo_I10.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                       byte[] bytBLOBData = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                        //APCoreProcess.APCoreProcess.ExcuteSQL("update nhaphanghoa set hinhanh=" + imageData + " where mahanghoa='" + txt_mahanghoa_IK1.Text + "'");
                        DataTable dt = new DataTable();
                        dt = APCoreProcess.APCoreProcess.Read("sysInfo where companyid='" + txt_companyid_IK1.Text + "'");
                        DataRow dr = dt.Rows[0];
                        if (bytBLOBData != null)
                            dr["logo"] = bytBLOBData;
                        else
                            dr["logo"] = null;
                        APCoreProcess.APCoreProcess.Save(dr);

                    }
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_companyid_IK1.Text, lbl_company_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    LoadTT();
                   
                    dxe_err_C.ClearErrors();
                    this.Close();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_companyid_IK1.Name), txt_companyid_IK1.Text);
                    if (img_logo_I10.Image != null)
                    {

                        MemoryStream ms = new MemoryStream();
                        img_logo_I10.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                        byte[] bytBLOBData = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                        //APCoreProcess.APCoreProcess.ExcuteSQL("update nhaphanghoa set hinhanh=" + imageData + " where mahanghoa='" + txt_mahanghoa_IK1.Text + "'");
                        DataTable dt = new DataTable();
                        dt = APCoreProcess.APCoreProcess.Read("sysInfo where companyid='" + txt_companyid_IK1.Text + "'");
                        DataRow dr = dt.Rows[0];
                        if (bytBLOBData != null)
                            dr["logo"] = bytBLOBData;
                        else
                            dr["logo"] = null;
                        APCoreProcess.APCoreProcess.Save(dr);

                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_companyid_IK1.Name) + " = '" + txt_companyid_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_companyid_IK1.Text, txt_sign_I2.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    this.Close();  
                    dxe_err_C.ClearErrors();
                    this.Close();
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysInfo set MS=0 ");
                    DevExpress.XtraEditors.XtraMessageBox.Show(Function.clsFunction.transLate("Lưu thành công"), Function.clsFunction.transLate("Thông báo"));
                }
                if (passData != null)
                    passData(true);
            }
            else
            {
                string caption = "";
                DataTable dtMessage = new DataTable();
                dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                caption = dtMessage.Rows[0]["title" + langues].ToString();
                dxe_err_C.SetError(txt_address_I2, caption);

            }
        }

        private bool checkInput()
        {


            if (lbl_company_I2.Text == "")
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(Function.clsFunction.transLate("Lưu thành công"), Function.clsFunction.transLate("Thông báo"));
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                txt_address_I2.Focus();
                dxe_err_C.SetError(lbl_company_I2,Function.clsFunction.transLateText("Nhập tên đơn vị"));
                return false;
            }

    
            return true;
        }

        #endregion

 

   



      
        
    }
}