using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frm_Reason : DevExpress.XtraEditors.XtraForm
    {
        public frm_Reason()
        {
            InitializeComponent();
        }
        public delegate void PassData(bool value);
        public PassData passData;
        public string idexport = "";
         public string idcommodity = "";
        public int status = -1;//0: is discount, 1 is surchage; 2 is give commodity; 3 discount commodity
        private void frm_Reason_Load(object sender, EventArgs e)
        {
            mmo_reason_S.Focus();
        }

        private void mmo_reason_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_accept_S.Focus();
            }
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            passData(false);
            this.Close();
        }

        private void btn_accept_S_Click(object sender, EventArgs e)
        {
            if (mmo_reason_S.Text != "" )
            {
                if (status == 0)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set isdiscount=1, reasondiscount=N'" + mmo_reason_S.Text + "' where idexport='" + idexport + "'");
                }
                if (status == 1)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set issurcharge=1, reasonsurcharge=N'" + mmo_reason_S.Text + "' where idexport='" + idexport + "'");
                }
                if (status == 2) // set give commodity
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set give=1, reasongive=N'" + mmo_reason_S.Text + "' where idexport='" + idexport + "' and idcommodity='" + idcommodity + "' ");
                }
                if (status == 3) // set discount commodity
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set isdiscount=1, reasondiscount=N'" + mmo_reason_S.Text + "' where idexport='" + idexport + "' and idcommodity='" + idcommodity + "' ");
                }
                passData(true);
                this.Close();
            }
            else
            {
                Function.clsFunction.MessageInfo("Thông báo","Bạn phải nhập lý do");
                mmo_reason_S.Text = "";
                mmo_reason_S.Focus();
            }

        }
    }
}