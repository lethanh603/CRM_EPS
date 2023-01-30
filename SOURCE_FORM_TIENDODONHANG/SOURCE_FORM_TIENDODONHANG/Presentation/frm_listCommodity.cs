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
    public partial class frm_listCommodity : DevExpress.XtraEditors.XtraForm
    {
        public frm_listCommodity()
        {
            InitializeComponent();
        }

        public string idexport = "";
        public string manhiemvu = "";
        public string idplantype = "";
        public delegate void StrPassData(string item, string iddetail, int soluong);
        public StrPassData strpassData;
        private void btn_cancel_detail_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_listCommodity_Load(object sender, EventArgs e)
        {
            if (idplantype == "PT000001")
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("with tem as (select d.iddetail, d.idcommodity, sum( d.quantity) as quantity from QUOTATIONDETAIL d where d.idexport = '"+ idexport +"' group by d.iddetail, d.idcommodity ) select tem.iddetail, tem.idcommodity, tem.quantity - isnull((select sum(soluong) from NHIEMVU_DETAIL where manhiemvu = '"+manhiemvu+"' and iddetail = tem.iddetail group by iddetail),0) as quantity from tem where tem.quantity - isnull((select sum(soluong) from NHIEMVU_DETAIL where manhiemvu = '"+manhiemvu+"' and iddetail = tem.iddetail group by iddetail),0) >0");
                gct_detail_C.DataSource = dt;
            }
            else if (idplantype == "PT000003")
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select d.iddetail, d.idcommodity, d.quantity  as quantity, C.commodity from quotationdetail d inner join dmCommodity C on d.idcommodity = c.idcommodity  where idexport = '" + idexport + "' and d.status = 1 ");
                gct_detail_C.DataSource = dt;
            }
        }

        private void btn_detail_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_detail_C.FocusedRowHandle >= 0)
                {
                    int soluonggoc = Convert.ToInt16(gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle,"quantity"));
                    strpassData(gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "idcommodity").ToString(), gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "iddetail").ToString(), soluonggoc);
                    this.Hide();
                }
                else
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn 1 sản phẩm");
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Error " + ex.Message);
            }
        }

        private void gv_detail_C_DoubleClick(object sender, EventArgs e)
        {
            btn_detail_insert_Click(sender, e);
        }
    }
}