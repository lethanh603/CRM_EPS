using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM.Presentation
{
    public partial class frmConfirm : DevExpress.XtraEditors.XtraForm
    {
        public frmConfirm()
        {
            InitializeComponent();
        }
        public string iduser = "";
        public string IDUS = "";
        public string maxacthuc = "";
        public delegate void PassData(bool value);
        public PassData passData;
        public bool sua = true;
        private void frmXacNhan_Load(object sender, EventArgs e)
        {
            Function.clsFunction.TranslateForm(this,this.Name);
        }
         
        private void txt_password_S_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter )
            { 

                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where userid='"+Function.clsFunction._iduser+"' and password='" + Function.clsFunction.mahoapw(txt_password_S.Text).Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    passData(true);
                    this.Hide();
                }
                else {

                    MessageBox.Show("Chỉ admin mới đủ quyền hạn để xóa user","Thông báo");
                        
                     }
                this.Close();     
            }
        }
    }
}