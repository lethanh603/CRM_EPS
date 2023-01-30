using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Extensions;
using System.IO;
using DevExpress.XtraReports.Parameters;

namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frm_checkbill : DevExpress.XtraEditors.XtraForm
    {
        public frm_checkbill()
        {
            InitializeComponent();
        }
        public string idexport = "";
        public string IDEMP = "";
        public double amount = 0;
        public double surcharge = 0;
        public double discount = 0;
        public string idtable = "";
        public delegate void PassData(bool value);
        public PassData passData;
        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cal_khachdua_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cal_khachdua_S.Value <= 0)
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Số tiền nhập phải lớn hơn 0");
                    cal_khachdua_S.Focus();
                }
                else
                {
                    cal_duthoi_S.Value = cal_khachdua_S.Value - cal_sotien_S.Value;
                }
            }
            catch { }
        }

        private void btn_accept_S_Click(object sender, EventArgs e)
        {
            try
            {
                if (cal_duthoi_S.Value >= 0)
                {
                    DataTable dt = new DataTable();             
                    dt = APCoreProcess.APCoreProcess.Read("SELECT idreceipt, idexport, datereceipt, IDEMP, amount, note, proceeds, surplus, idtype,surcharge, discount,vat, idtable, undo,del FROM RECEIPT where idreceipt=''");
                    DataRow dr = dt.NewRow();
                    dr["idreceipt"] = Function.clsFunction.layMa("PT", "idreceipt", "receipt");
                    dr["idexport"] = idexport;
                    dr["datereceipt"] = DateTime.Now;
                    dr["IDEMP"] = IDEMP;
                    dr["amount"] = amount;
                    dr["note"] = mmo_note_S.Text;
                    dr["proceeds"] = cal_khachdua_S.Value;
                    dr["surplus"] = cal_duthoi_S.Value;
                    dr["idtype"] = glue_idtype_S.EditValue.ToString();
                    dr["surcharge"] = surcharge;
                    dr["discount"] = discount;
                    dr["vat"] = 0;
                    dr["undo"] = false;
                    dr["del"] = false;
                    dr["idtable"] = idtable;
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set complet=1, cancel=0 where idexport='" + idexport + "'");
                    passData(true);
                    setDefaultTableUnion();

                    if (chk_inhoadon.Checked == true)
                    {
                        print(idexport);
                    }
                    this.Close();
                }
                else
                {
                    Function.clsFunction.MessageInfo("Thông báo","Số tiền thanh toán chưa đủ" );
                }
            }
            catch 
            { }
        }        

        private void setDefaultTableUnion()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select tableunion from export where idexport='" + idexport + "' and tableunion is not null");
                string str = "";
                if (dt.Rows.Count > 0)
                {
                    str = dt.Rows[0][0].ToString();
                    for (int i = 0; i < str.Split('@').Length; i++)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='0', complet=1 where idexport='" + getInvoiceByIDTable(str.Split('@')[i].ToString()) + "'");
                    }
                }
            }
            catch { }
        }

        private string getInvoiceByIDTable(string idtable)
        {
            string invoice = "";
            // lấy số hóa đơn đang tồn tại trong phiên làm việc
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1 idexport  from export where idtable='" + idtable + "' and complet=0 order by dateimport desc");
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0][0].ToString();
            }
            return invoice;
        }

        private void loadGridLookupType()
        {
            try
            {
                string[] caption = new string[] { "Mã HT", "Hình thức"};
                string[] fieldname = new string[] { "idtype", "typename"};
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idtype_S, "select idtype, typename from dmtype where status=1", "typename", "idtype", caption, fieldname, this.Name, glue_idtype_S.Width * 2, col_visible);
               //glue_idtype_S.EditValue = glue_idtype_S.Properties.GetKeyValue(0);
            }
            catch { }
        }

        private void frm_checkbill_Load(object sender, EventArgs e)
        {
            //Function.clsFunction.TranslateForm(this, this.Name);
            cal_khachdua_S.Focus();
            cal_khachdua_S.Value = cal_sotien_S.Value;
            
            loadGridLookupType();
        }

        private void print(string idexport)
        {
            try
            {
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='" + this.Name + "' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath +"\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    Function.clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        dt = APCoreProcess.APCoreProcess.Read("with temp as ( SELECT     mt.dateimport, ISNULL(mt.vat, 0) AS vat, mt.idexport, ISNULL(pt.surcharge, 0) AS amountsurcharge, mt.invoice, pt.datereceipt, ISNULL(pt.amount, 0) AS amount, pt.note, ISNULL(pt.surplus, 0) AS surplus, ISNULL(pt.discount, 0) AS amountdiscount, tb.tableNO, mh.idcommodity,  mh.commodity  as commodity, kh.customer, dt.idunit, ISNULL(dt.quantity, 0) AS quantity, ISNULL(dt.price, 0) AS price, ISNULL(dt.amountvat, 0) AS amountvat,  dt.davat, dt.costs, ISNULL(dt.total, 0) AS total,  convert(varchar(3), ISNULL(dt.quantity, 0)) + ' '+ lower( left( U.Sign,2)) AS  strquantity,  pt.proceeds, convert(int, pt.surcharge/pt.amount *100) as surcharge, convert(int, pt.discount/pt.amount*100) as discount, pt.amount + pt.surcharge - pt.discount AS amount1, case when dt.give=0 then N'Thực đơn' else N'Tặng kèm' end as give, dt.give as give1 FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN  DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN DMUNIT U ON U.idunit=dt.idunit WHERE     (mt.idexport = N'" + idexport + "')  and ISNULL(dt.quantity, 0)>0   union SELECT     mt.dateimport, ISNULL(mt.vat, 0) AS vat, mt.idexport, ISNULL(pt.surcharge, 0) AS amountsurcharge, mt.invoice, pt.datereceipt, ISNULL(pt.amount, 0) AS amount, pt.note, ISNULL(pt.surplus, 0) AS surplus, ISNULL(pt.discount, 0) AS amountdiscount, tb.tableNO, mh.idcommodity ,  N'-> Giảm ~ ' + convert(nvarchar(3),(select convert(nvarchar(10), round((dt1.amountdiscount/dt1.price*100),0)) from exportdetail dt1 where dt1.give=0 and dt1.idcommodity=dt.idcommodity and dt1.idexport=dt.idexport)) + ' %'   as commodity, kh.customer, dt.idunit, ISNULL(dt.quantity, 0) AS quantity,  ISNULL(- dt.amountdiscount, 0) AS price,   ISNULL(dt.amountvat, 0) AS amountvat, dt.davat, dt.costs, ISNULL(dt.total, 0) AS total,  convert(varchar(3), ISNULL(dt.quantity, 0)) + ' '+ lower( left( U.Sign,1)) AS  strquantity,  pt.proceeds, convert(int, pt.surcharge/pt.amount *100) as surcharge, convert(int, pt.discount/pt.amount*100) as discount, pt.amount + pt.surcharge - pt.discount AS amount1, case when dt.give=0 then N'Thực đơn' else N'Tặng kèm' end as give, dt.give as give1 FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN  DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN DMUNIT U ON U.idunit=dt.idunit WHERE     (mt.idexport = N'" + idexport + "')  and ISNULL(dt.quantity, 0)>0 and dt.give=0 and dt.amountdiscount>0 ) select temp.* , case when give1=1 then 0 else price end as price2 from temp order by give desc ");
                    }
                    else
                    {
                        dt = APCoreProcess.APCoreProcess.Read("SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL  THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,   CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, mh.commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END AS quantity,  CASE WHEN dt.price IS NULL THEN 0 ELSE dt.price END AS price, CASE WHEN dt.amountvat IS NULL THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat,  dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3), CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END)  + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100) AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount,  pt.amount + pt.surcharge - pt.discount AS amount1, CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1,   CASE WHEN dt.give = 1 THEN 0 ELSE dt.price END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN   DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN    DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN   DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) UNION SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,  CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, N'-> Giảm giá ' + CONVERT(nvarchar(3), dt.discount) + ' %' AS commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL  THEN 0 ELSE dt.quantity END AS quantity, CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END AS price, CASE WHEN dt.amountvat IS NULL    THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat, dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3),   CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END) + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100)    AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount, pt.amount + pt.surcharge - pt.discount AS amount1,     CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1, CASE WHEN dt.give = 1 THEN 0 ELSE CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN    RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN      DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN      DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN    DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) AND (dt.give = 0) AND (dt.amountdiscount > 0)");
                    }
                    //dt.DefaultView.Sort = "give1 desc,_index , price desc";
                    dt.DefaultView.Sort = " give1 desc,idcommodity , price desc";
                    DataTable dtO = new DataTable();
                    dtO = dt.DefaultView.ToTable();
                    ds.Tables.Add(dtO);
                    report.DataSource = ds;
                    report.DataMember = "Data";// phair co datamember moi len dc group
                    report.DataAdapter=ds;
                    LaunchCommandLineOpen(report);
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }
                    //tool.ShowPreviewDialog();                
                    tool.Print(APCoreProcess.APCoreProcess.Read("select * from sysPrintConfig where status=1").Rows[0]["printname"].ToString());                   
                }
            }
            catch
            {
                Function.clsFunction.MessageInfo( "Thông báo","Không kết nối được máy in");
            }
        }

        private DataTable convertDatatableReadOnly(DataTable dt)
        {
            
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;

            }
            return dt;
        }

        private  void LaunchCommandLineOpen(XtraReport xrp)
        {
            // Create a parameter and specify its name
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysINFO");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "logo")
                    {
                        ReportDesignExtension.AssociateReportWithExtension(xrp, "SomeName");
                        MemoryStream ms;
                        byte[] pic = (byte[])dt.Rows[0][i];
                        ms = new MemoryStream(pic);
                        ms.Seek(0, SeekOrigin.Begin);
                        Image image = Image.FromStream(ms);
                        Parameter param1 = new Parameter();
                        param1.Name = dt.Columns[i].ColumnName;
                        // Specify other parameter properties.
                        param1.Type = typeof(System.Drawing.Image);
                        param1.Value = image;
                        param1.Description = dt.Columns[i].ColumnName;
                        param1.Visible = true;
                        // Add the parameter to the report.
                        xrp.Parameters.Add(param1);
                    }
                    else
                    {
                        Parameter param1 = new Parameter();
                        param1.Name = dt.Columns[i].ColumnName;
                        // Specify other parameter properties.
                        param1.Type = typeof(System.String);
                        param1.Value = dt.Rows[0][i].ToString();
                        param1.Description = dt.Columns[i].ColumnName;
                        param1.Visible = true;
                        // Add the parameter to the report.
                        xrp.Parameters.Add(param1);
                    }
                }
            }
            
        }

        private void loadReport()
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = this.Name;
                frm.ShowDialog();
            }
            catch { }
        }

        private void btn_config_S_Click(object sender, EventArgs e)
        {
            loadReport();
        }

        private void frm_checkbill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btn_config_S.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                btn_accept_S.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
        }




    
    }
}