using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frmChiTietGiaohang : DevExpress.XtraEditors.XtraForm
    {
        public frmChiTietGiaohang()
        {
            InitializeComponent();
        }

        public delegate void arrPassData(string item, string iddetail ,int soluong, DateTime tungay, DateTime denngay);
        public arrPassData arrpassData;

        public int soluonggoc = 0;
        public string item = "";
        public string iddetail = "";

        private void btn_cancel_detail_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChiTietGiaohang_Load(object sender, EventArgs e)
        {
            dte_tungay_I5.EditValue = DateTime.Now;
            dte_denngay_I5.EditValue = DateTime.Now;            
            soluong.Value = 0;
        }

        private void btn_detail_insert_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime( dte_tungay_I5.EditValue) > Convert.ToDateTime( dte_denngay_I5.EditValue)  )
            {
                Function.clsFunction.MessageInfo("Thông báo","Thời gian không hợp lệ");
                return;
            }

            if (soluong.Value == 0 || soluong.Value > soluonggoc)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Số lượng phải >0 và nhỏ hơn số lượng còn lại");
                return;
            }

            arrpassData(item, iddetail, Convert.ToInt16(soluong.Value), Convert.ToDateTime(dte_tungay_I5.EditValue), Convert.ToDateTime(dte_denngay_I5.EditValue));
            this.Hide();
        }
    }
}