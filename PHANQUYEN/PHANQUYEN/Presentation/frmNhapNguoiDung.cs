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


namespace PHANQUYEN.Presentation
{
    public partial class frm_sysUser_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysUser_S()
        {
            InitializeComponent();
        }

        #region Var

        public bool them = false;
        public int index;
        public bool statusForm;
        public string langues = "_VI";
        public delegate void PassDataUser(bool value);
        public PassDataUser passData;
        public string dauma = "US";
        public string ID = "";
        System.Data.DataTable dtMessage = new System.Data.DataTable();
        public void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {

            }
        }
        #endregion

        #region Load
        private void frmNhapDVT_Load(object sender, EventArgs e)
        {
            dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");

            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    //Function.clsFunction.CreateTable(this, this);
                    setNhanVien();
                    setVaiTro();
                }
                catch (Exception ex) { }
            }
            loadNhanvien();
            loadVaiTro();
            LoadTT();
            //System.Data.DataTable dt_textForm = new System.Data.DataTable();
            //dt_textForm = APCoreProcess.APCoreProcess.Read("select * from sysSubmenu where form_name='" + this.Name.ToString().Trim() + "" + "'");

            //this.Text =  dt_textForm.Rows[0]["text_form" + langues].ToString();

        }


        private void LoadTT()
        {
            Function.clsFunction.Text_Control(this, langues);
            if (them == true)
            {
                txt_userid_IK1.Text = Function.clsFunction.layMa(dauma, Function.clsFunction.getNameControls(txt_userid_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                them = true;
                txt_username_I2.Text = "";
                chk_status_I6.Checked = true;

            }
            else
            {
                txt_userid_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_userid_IK1);
                txt_password_I2.Text = Function.clsFunction.giaima(txt_password_I2.Text);
                txt_xacnhan_S.Text = txt_password_I2.Text;
            }

        }

        #endregion

        #region Event


        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT dbo.sysControls.control_name,  dbo.sysControls.type, dbo.sysControls.stt, dbo.sysControls.form_name, dbo.sysPower.allow_insert,   dbo.sysPower.allow_delete, dbo.sysPower.allow_import, dbo.sysPower.allow_export, dbo.sysPower.allow_print, dbo.sysControls.text_En, dbo.sysControls.text_Vi, dbo.sysPower.allow_edit, dbo.sysControls.stt FROM         dbo.sysControls INNER JOIN  dbo.sysPower ON dbo.sysControls.ma_sub_menu = dbo.sysPower.ma_sub_menu WHERE     (dbo.sysControls.form_name = N'" + this.Name + "') AND (dbo.sysControls.type = N'SimpleButton') AND (dbo.sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY dbo.sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
                {
                    if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
                    {

                        {
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("luu"))
                            {
                                btnLuu0_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("thoat"))
                            {
                                btnThoat0_Click(sender, e);
                            }

                        }
                    }
                }
            }
        }

        private void grp_ttbb_C_Paint(object sender, PaintEventArgs e)
        {

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

        #endregion

        #region Methods

        private void insert()
        {
            if (!checkInput()) return;
            if (txt_userid_IK1.Text != "")
            {
                if (them == true)
                {

                    Function.clsFunction.Insert_data(this, (this.Name));
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysUser set password='" + Function.clsFunction.mahoapw(txt_password_I2.Text) + "' where userid='" + txt_userid_IK1.Text + "'");
                    Function.clsFunction.NhatKyHeThong(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), (txt_userid_IK1.Text), SystemInformation.ComputerName.ToString(), "0");
                    LoadTT();
                    this.Close();
                    dxErrorProvider1.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_userid_IK1.Name), txt_userid_IK1.Text);

                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysUser set password='" + Function.clsFunction.mahoapw(txt_password_I2.Text) + "' where userid='" + txt_userid_IK1.Text + "'");
                    Function.clsFunction.NhatKyHeThong(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), (txt_userid_IK1.Text), SystemInformation.ComputerName.ToString(), "0");
                    this.Close();
                    DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[9]["message" + langues].ToString(), dtMessage.Rows[9]["title" + langues].ToString());
                    dxErrorProvider1.ClearErrors();
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
                dxErrorProvider1.SetError(txt_ghichu_I7, caption);

            }
        }

        private bool checkInput()
        {
            if (txt_username_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_username_I2.Focus();
                return false;
            }
            if (txt_password_I2.Text != txt_xacnhan_S.Text)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_password_I2.Focus();
                return false;
            }
            if (txt_password_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                txt_username_I2.Focus();
                return false;
            }
            if (glue_manhanvien_I1.EditValue == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                glue_manhanvien_I1.Focus();
                return false;
            }
            if (lue_mavaitro_I1.EditValue == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[3]["message" + langues].ToString(), dtMessage.Rows[3]["title" + langues].ToString());
                lue_mavaitro_I1.Focus();
                return false;
            }
            return true;
        }

        private void setNhanVien()
        {
            string[] fieldname = new string[] { "manhanvien", "tennhanvien" };
            string[] caption = new string[] { "Mã NV", "Tên NV" };
            ControlDev.FormatControls.LoadGridLookupEdit3(glue_manhanvien_I1, "nhapnhanvien", "tennhanvien", "manhanvien", caption, fieldname, statusForm, "", this.Name);
        }
        private void loadNhanvien()
        {
            DataTable dt_sysControls = new DataTable();
            dt_sysControls = APCoreProcess.APCoreProcess.Read("sysControls where form_name='" + this.Name + "' and control_name='" + glue_manhanvien_I1.Name + "'");
            string[] fieldname = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["fieldname_col_lue"].ToString(), "/");
            string[] caption = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["caption_col_lue" + langues].ToString(), "/");
            string sql = dt_sysControls.Rows[0]["sql_lue"].ToString();
            //string scaption = dt_sysControls.Rows[0]["caption" + langues].ToString();
            string sfieldname = dt_sysControls.Rows[0]["field_name"].ToString();
            string image = dt_sysControls.Rows[0]["image"].ToString();
            ControlDev.FormatControls.LoadGridLookupEdit3(glue_manhanvien_I1,  "nhapnhanvien", "tennhanvien", "manhanvien", caption, fieldname, statusForm, image, this.Name);

        }

        private void setVaiTro()
        {
            string[] fieldname = new string[] { "mavaitro", "tenvaitro" };
            string[] caption = new string[] { "Mã VT", "Tên VT" };
            ControlDev.FormatControls.LoadLookupEdit(lue_mavaitro_I1, "(SELECT DISTINCT dbo.sysVaiTro.mavaitro, dbo.sysVaiTro.tenvaitro, dbo.sysVaiTro.makethua, dbo.sysUser.root FROM         dbo.sysVaiTro LEFT OUTER JOIN                       dbo.sysUser ON dbo.sysVaiTro.mavaitro = dbo.sysUser.mavaitro WHERE     (dbo.sysVaiTro.makethua)='" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') union (SELECT DISTINCT dbo.sysVaiTro.mavaitro, dbo.sysVaiTro.tenvaitro, dbo.sysVaiTro.makethua, dbo.sysUser.root FROM         dbo.sysVaiTro LEFT OUTER JOIN                      dbo.sysUser ON dbo.sysVaiTro.mavaitro = dbo.sysUser.mavaitro WHERE     (dbo.sysVaiTro.mavaitro = '" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "'))", "tenvaitro", "mavaitro", caption, fieldname, statusForm, "", this.Name);
        }
        private void loadVaiTro()
        {
            DataTable dt_sysControls = new DataTable();
            dt_sysControls = APCoreProcess.APCoreProcess.Read("sysControls where form_name='" + this.Name + "' and control_name='" + lue_mavaitro_I1.Name + "'");
            string[] fieldname = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["fieldname_col_lue"].ToString(), "/");
            string[] caption = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["caption_col_lue" + langues].ToString(), "/");
            string sql = dt_sysControls.Rows[0]["sql_lue"].ToString();
            //string scaption = dt_sysControls.Rows[0]["caption" + langues].ToString();
            string sfieldname = dt_sysControls.Rows[0]["field_name"].ToString();
            string image = dt_sysControls.Rows[0]["image"].ToString();
            ControlDev.FormatControls.LoadLookupEdit(lue_mavaitro_I1, "(SELECT DISTINCT dbo.sysVaiTro.mavaitro, dbo.sysVaiTro.tenvaitro, dbo.sysVaiTro.makethua, dbo.sysUser.root FROM         dbo.sysVaiTro LEFT OUTER JOIN                       dbo.sysUser ON dbo.sysVaiTro.mavaitro = dbo.sysUser.mavaitro WHERE     (dbo.sysVaiTro.makethua)='" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') union (SELECT DISTINCT dbo.sysVaiTro.mavaitro, dbo.sysVaiTro.tenvaitro, dbo.sysVaiTro.makethua, dbo.sysUser.root FROM         dbo.sysVaiTro LEFT OUTER JOIN                      dbo.sysUser ON dbo.sysVaiTro.mavaitro = dbo.sysUser.mavaitro WHERE     (dbo.sysVaiTro.mavaitro = '" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "'))", "tenvaitro", "mavaitro", caption, fieldname, statusForm, image, this.Name);
        }   
        #endregion



    }
}