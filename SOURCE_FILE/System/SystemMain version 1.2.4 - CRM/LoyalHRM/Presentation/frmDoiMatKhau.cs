using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Resources;
using LoyalHRM.Properties;

namespace LoyalHRM.Presentation
{
    public partial class frmDoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
         
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txt_matkhaucu_S.Text == "" || txt_matkhaumoi_S.Text == "" || txt_matkhau_S.Text == "" || txt_tendangnhap_I2.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ", "Thông báo");              
            }
            else
            {
                if (txt_matkhaucu_S.Text.ToUpper().Trim() == Function.clsFunction.giaima( APCoreProcess.APCoreProcess.Read("select password from sysUser where userid='" + Function.clsFunction._iduser + "'").Rows[0][0].ToString()).Trim().ToUpper())
                {
                    if (txt_matkhau_S.Text == txt_matkhaumoi_S.Text)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update sysUser set password = '" + Function.clsFunction.mahoapw(txt_matkhaumoi_S.Text) + "', username='"+txt_tendangnhap_I2.Text+"' where userid='" + Function.clsFunction._iduser + "'");
                        MessageBox.Show("Cập nhật thành công", "Thông báo");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu lặp lại không đúng", "Thông báo");
                    }
                }
                else
                    MessageBox.Show("Mật khẩu xác thực không đúng", "Thông báo");

            }

        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            this.Text = Function.clsFunction.transLateText(this.Text);
            Function.clsFunction.TranslateForm(this, this.Name);
            txt_tendangnhap_I2.Text = APCoreProcess.APCoreProcess.Read("select username from sysuser where userid='"+Function.clsFunction._iduser+"'").Rows[0][0].ToString();
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_matkhau_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

    }
}