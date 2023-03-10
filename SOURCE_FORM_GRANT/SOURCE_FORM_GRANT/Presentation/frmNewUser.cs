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


namespace SOURCE_FORM.Presentation
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
            dte_createddate_I5.EditValue = DateTime.Now;
            dte_enddate_I5.EditValue = DateTime.Now;
            dte_startdate_I5.EditValue = DateTime.Now;
            if (statusForm == true)
            {

                try
                {
                    Function.clsFunction.CreateTable(this, this);

                }
                catch (Exception ex)
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Error: " + ex.Message);
                }
            }
            loadNhanvien();
            loadManager();
            Function.clsFunction.TranslateForm(this, this.Name);
            loadVaiTro();
            LoadTT();

            this.Text = Function.clsFunction.transLateText(this.Name);

        }


        private void LoadTT()
        {

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
            loadUserDetail();

        }

        private void loadUserDetail()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     D.idemp, E.StaffName, U.username,  D.idmanager, D.userid, D.iduserdetail, D.createddate, M.username as manager FROM         dbo.sysUserDetail AS D INNER JOIN   dbo.EMPLOYEES AS E ON D.idemp = E.IDEMP INNER JOIN   dbo.sysUser AS U ON D.userid = U.userid INNER JOIN sysUser M ON U.idmanager = M.userid WHERE      (D.userid = N'" + txt_userid_IK1.Text + "') ");
                gct_user_C.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Event


        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT sysControls.control_name,  sysControls.type, sysControls.stt, sysControls.form_name, sysPower.allow_insert,   sysPower.allow_delete, sysPower.allow_import, sysPower.allow_export, sysPower.allow_print, sysControls.text_En, sysControls.text_Vi, sysPower.allow_edit, sysControls.stt FROM    sysControls INNER JOIN  sysPower ON sysControls.IDSubMenu = sysPower.IDSubMenu WHERE     (sysControls.form_name = N'" + this.Name + "') AND (sysControls.type = N'SimpleButton') AND (sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY sysControls.stt ");
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
                    updateUserDetail();
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_userid_IK1.Text, txt_username_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    LoadTT();
                    this.Close();
                    dxErrorProvider1.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_userid_IK1.Name), txt_userid_IK1.Text);
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysUser set password='" + Function.clsFunction.mahoapw(txt_password_I2.Text) + "' where userid='" + txt_userid_IK1.Text + "'");
                    updateUserDetail();
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_userid_IK1.Text, txt_username_I2.Text, SystemInformation.ComputerName.ToString(), "1");
                    this.Close();
                    Function.clsFunction.MessageInfo("Thông báo", "Cập nhật thành công");
                    dxErrorProvider1.ClearErrors();
                }

                if (passData != null)
                    passData(true);
            }
            else
            {
                dxErrorProvider1.SetError(txt_ghichu_I7, Function.clsFunction.transLateText("Không được rỗng"));
            }
        }

        private bool checkInput()
        {
            if (txt_username_I2.Text == "")
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                txt_username_I2.Focus();
                return false;
            }
            if (txt_password_I2.Text != txt_xacnhan_S.Text)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                txt_password_I2.Focus();
                return false;
            }
            if (txt_password_I2.Text == "")
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                txt_username_I2.Focus();
                return false;
            }
            if (glue_IDEMP_I1.EditValue == null)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                glue_IDEMP_I1.Focus();
                return false;
            }
            if (lue_mavaitro_I1.EditValue == null)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Không được rỗng");
                lue_mavaitro_I1.Focus();
                return false;
            }
            if (APCoreProcess.APCoreProcess.Read("select * from sysUser where IDEMP ='" + glue_IDEMP_I1.EditValue.ToString() + "'").Rows.Count > 0 && them == true)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Nhân viên này đã tồn tại tài khoản, vui lòng kiểm tra lại");
                glue_IDEMP_I1.Focus();
                return false;
            }
            if (them == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from sysUser where username='" + txt_username_I2.Text + "'").Rows.Count > 0)
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Trùng username, vui lòng kiểm tra lại");
                    txt_username_I2.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select username from  sysUser where username='" + txt_username_I2.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from  sysUser where username='" + txt_username_I2.Text + "' and username <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Trùng username, vui lòng kiểm tra lại");
                        txt_username_I2.Focus();
                        return false;
                    }
                }
            }


            return true;
        }

        private void loadNhanvien()
        {
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                ControlDev.FormatControls.LoadGridLookupEditParameter(glue_IDEMP_I1, "load_" + glue_IDEMP_I1.Name + "_" + this.Name, "create proc   " + "load_" + glue_IDEMP_I1.Name + "_" + this.Name + " (@IDEMP as nvarchar(10)) as begin  WITH temp(IDEMP, StaffName, alevel)   as (    Select IDEMP, StaffName, 0 as aLevel  From employees   Where idemp=@IDEMP and ( idmanager is null or idmanager=idemp or idemp=@IDEMP)                Union All                Select b.IDEMP, b.StaffName, a.alevel + 1                From temp as a, employees as b                Where a.idemp = b.idmanager and b.idemp <> b.idmanager        )        Select *        From temp  end", new string[1, 2] { { "IDEMP", Function.clsFunction.GetIDEMPByUser() } }, "IDEMP", "StaffName");
            }
            else
            {
                ControlDev.FormatControls.LoadGridLookupEditParameter(glue_IDEMP_I1, "load_" + glue_IDEMP_I1.Name + "_" + this.Name, "   Select idemp, staffname From employees where idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%' ", new string[1, 2] { { "IDEMP", Function.clsFunction.GetIDEMPByUser() } }, "IDEMP", "StaffName");

            }
        }

        private void loadManager()
        {
            //ControlDev.FormatControls.LoadGridLookupEditParameter(glue_idmanager_I2, "load_" + glue_idmanager_I2.Name + "_" + this.Name, "   Select userid, username From sysUser  ", new string[0, 0], "userid", "username");
     

            string[] caption = new string[] { "User ID", "User Name" };
            string[] fieldname = new string[] { "userid", "username" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idmanager_I2, "Select userid, username From sysUser", "username", "userid", caption, fieldname, this.Name, glue_idmanager_I2.Width);
        }

        private void loadVaiTro()
        {
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                ControlDev.FormatControls.LoadLookupEditParameter(lue_mavaitro_I1, "load_" + lue_mavaitro_I1.Name + "_" + this.Name, "create proc   " + "load_" + lue_mavaitro_I1.Name + "_" + this.Name + " (@IDRole as nvarchar(10)) as begin  WITH temp(mavaitro, tenvaitro, makethua)   as (    Select mavaitro, tenvaitro, makethua  From sysRole   Where mavaitro=@IDRole and ( makethua is null or makethua=mavaitro or mavaitro=@IDRole)                Union All                Select b.mavaitro, b.tenvaitro, b.makethua              From temp as a, sysRole as b                Where a.mavaitro = b.makethua and b.mavaitro <> b.makethua        )   Select temp.mavaitro, temp.tenvaitro, temp.makethua     From temp  end", new string[1, 2] { { "IDRole", Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) } }, "mavaitro", "tenvaitro");
            }
            else
            {
                ControlDev.FormatControls.LoadLookupEditParameter(lue_mavaitro_I1, "load_" + lue_mavaitro_I1.Name + "_" + this.Name, " SELECT DISTINCT sysRole.mavaitro, sysRole.tenvaitro, sysRole.makethua FROM         sysRole  WHERE     (sysRole.idrecursive like '%" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "%') ", new string[1, 2] { { "IDRole", Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) } }, "mavaitro", "tenvaitro");
            }
        }
        private void updateEmp()
        {
            String str = "";
            str += glue_IDEMP_I1.EditValue.ToString() + "@";
            for (int i = 0; i < gv_user_C.DataRowCount; i++)
            {
                if ((bool)gv_user_C.GetRowCellValue(i, "check"))
                {
                    str += gv_user_C.GetRowCellValue(i, "idemp").ToString() + "@";
                }
            }

            APCoreProcess.APCoreProcess.ExcuteSQL("update EMPLOYEES set idrecursive = '" + str + "' where idemp='" + glue_IDEMP_I1.EditValue.ToString() + "'");
        }

        private void updateUserDetail()
        {
            DataTable dt;
            DataRow dr = null;
            dt = APCoreProcess.APCoreProcess.Read("select * from sysUserDetail where userid='" + txt_userid_IK1.Text + "'");
            dr = dt.NewRow();
            dr["idmanager"] = glue_idmanager_I2.EditValue;
            dr["userid"] = txt_userid_IK1.Text;
            dr["idemp"] = glue_IDEMP_I1.EditValue;
            dr["createddate"] = DateTime.Now;
            dr["iduserdetail"] = Function.clsFunction.layMa("UD", "iduserdetail", "sysUserDetail");
            dt.Rows.Add(dr);

            APCoreProcess.APCoreProcess.Save(dr);

        }

        private bool checkUserManager(string idemp, bool isCurrent)
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read("select U.username from sysUserDetail D inner join sysUser on D.userid=U.userid where idemp='" + idemp + "' and iscurrent =" + isCurrent + "");
            if (dt.Rows.Count > 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Nhân viên này đã thuộc quản lý của user " + dt.Rows[0][0].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        private void chk_checkall_S_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_user_C.DataRowCount; i++)
            {
                if (chk_checkall_S.Checked)
                {
                    gv_user_C.SetRowCellValue(i, "check", true);
                }
                else
                {
                    gv_user_C.SetRowCellValue(i, "check", false);
                }
            }

        }

        private void chk_unlimited_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_unlimited_I6.Checked)
            {
                dte_enddate_I5.Enabled = false;
            }
            else
            {
                dte_enddate_I5.Enabled = true;
            }
        }

        #endregion


    }
}