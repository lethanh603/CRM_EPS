using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Function;

namespace SOURCE_FORM_DMCOMMODITY.Presentation
{
    public partial class frm_itemPriceHistory : DevExpress.XtraEditors.XtraForm
    {
        public frm_itemPriceHistory()
        {
            InitializeComponent();
        }

        public string idcommodity = "";

        private void frm_itemPriceHistory_Load(object sender, EventArgs e)
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dte_todate_S.EditValue = DateTime.Now;
            search();
        }

        private void search()
        {
           
            string sql = "select Q.quotationno, dateimport, Q.invoiceeps, D.price, D.idcommodity, E.StaffName, customer,D.note, D.partnumber, D.spec, D.equipmentinfo from QUOTATION Q INNER JOIN  QUOTATIONDETAIL D  ON Q.idexport=d.idexport INNER JOIN EMPLOYEES E  ON Q.IDEMP = E.IDEMP  INNER JOIN DMCUSTOMERS C  ON Q.idcustomer = C.idcustomer   where idcommodity='" + idcommodity + "' and   (idstatusquotation ='ST000002' or idstatusquotation ='ST000003' or idstatusquotation ='ST000004' ) and (( " + checkQuyenPO() + " charindex('" + clsFunction.GetIDEMPByUser() + "',E.idrecursive) >0 ) or E.idemp='" + clsFunction.GetIDEMPByUser() + "') and  cast(dateimport as date) between cast( '" + Convert.ToDateTime( dte_fromdate_S.EditValue).ToString("yyyy-MM-dd") + "' as date) and cast( '" + Convert.ToDateTime( dte_todate_S.EditValue).ToString("yyyy-MM-dd") + "' as date) order by dateimport";
            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            gc_list_C.DataSource = dt;
        }

        private String checkQuyenPO()
        {
            string dk = "";
            DataTable dt = APCoreProcess.APCoreProcess.Read("select idemp from employees where idemp='" + clsFunction.GetIDEMPByUser() + "' and ismanagerpo = 1 ");
            if (dt.Rows.Count > 0)
            {
                dk = " 1=1 OR ";
            }
            return dk;
        }

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}