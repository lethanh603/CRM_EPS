using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Extensions;
using System.IO;
using DevExpress.XtraReports.Parameters;

namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frmEditInvoice : DevExpress.XtraEditors.XtraForm
    {
        public frmEditInvoice()
        {
            InitializeComponent();
        }

        #region Var

        public bool statusForm = false;
        public string idtable = "";
        public string idinvoice = "";
        public string _sign = "PX";
        //private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        //private string arrCaption;
        //private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public PassData passData;
        private bool _load = false;
        private DateTime dateExport  ;
        #endregion

        #region FormEvent

        private void frmEditInvoice_Load(object sender, EventArgs e)
        {
            Init();
            loadTotal();
        }

        private void frmEditInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                bbi_allow_insert.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F9)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region GridEvent

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            bool indicatorIcon = false;
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = Convert.ToString(int.Parse(e.RowHandle.ToString()) + 1);

                if (!indicatorIcon)
                    e.Info.ImageIndex = -1;
            }
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = Function.clsFunction.transLateText("STT");
            }

            e.Painter.DrawObject(e.Info);
            clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
            e.Handled = true;
        }

        private void gv_menu_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "gcQuantity")
                {
                    loadTotal();
                }
            }
            catch { }

        }
        #endregion

        #region Event
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lbl_time_S.Text = DateTime.Now.ToString("HH:mm:ss");
                if (lbl_start_S.Text != "00:00:00")
                {
                    //lbl_time1_S.Text = (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) - TimeSpan.Parse(lbl_start_S.Text)).ToString();
                    lbl_time1_S.Text = (DateTime.Now.Subtract(dateExport).Days * 24 + DateTime.Now.Subtract(dateExport).Hours).ToString().PadLeft(2, '0').ToString() + ":" + DateTime.Now.Subtract(dateExport).Minutes.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Subtract(dateExport).Seconds.ToString().PadLeft(2, '0');
                }
            }
            catch { }
        }

        private void cal_amountsurcharge_S_EditValueChanged(object sender, EventArgs e)
        {
            loadTotal();
            try
            {
                if (Math.Round(cal_amountsurcharge_S.Value / cal_costs_S.Value * 100, 0) != Math.Round(cal_surcharge_S.Value, 0))
                {
                    cal_surcharge_S.Value = Math.Round(cal_amountsurcharge_S.Value / cal_costs_S.Value * 100, 0);
                }
            }
            catch { }
        }

        private void cal_amountdiscount_S_EditValueChanged(object sender, EventArgs e)
        {
            loadTotal();
            try
            {
                if (Math.Round(cal_amountdiscount_S.Value / cal_costs_S.Value * 100, 1) != Math.Round(cal_discount_S.Value, 1))
                {
                    cal_discount_S.Value = Math.Round(cal_amountdiscount_S.Value / cal_costs_S.Value * 100, 1);
                }
            }
            catch { }
        }

        private void cal_khachdua_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cal_khachdua_S.Value < 0)
                {
                    //Function.clsFunction.MessageInfo("Thông báo", "Số tiền nhập phải lớn hơn 0");
                    cal_khachdua_S.Focus();
                }
                else
                {
                    cal_duthoi_S.Value = cal_khachdua_S.Value - cal_amount_S.Value;
                }
            }
            catch { }
        }

        private void cal_surcharge_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16( cal_surcharge_S.Value)!= Convert.ToInt16( (cal_amountsurcharge_S.Value / cal_costs_S.Value * 100)))
                    
                {
                    cal_amountsurcharge_S.Value = (cal_surcharge_S.Value / 100 * cal_costs_S.Value);
                }
            }
            catch { }
        }

        private void cal_discount_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cal_amountdiscount_S.Value != (cal_discount_S.Value / 100 * cal_costs_S.Value))
                {
                    cal_amountdiscount_S.Value = (cal_discount_S.Value / 100 * cal_costs_S.Value);
                }
            }
            catch { }
        }

        private void cal_costs_S_EditValueChanged(object sender, EventArgs e)
        {
            loadTotal();
        }

        #endregion

        #region ButtonEvent

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            save();
        }

        private void btn_insert_S_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_S_Click(object sender, EventArgs e)
        {
            deleteCommodity();
        }
        private void btn_delete1_S_Click(object sender, EventArgs e)
        {
            deleteCommodityGive();
        }
        #endregion

        #region Methods

        private void save()
        {
            try
            {
                // cap nhat exportdetail
                for (int i = 0; i < gv_menu_C.DataRowCount; i++)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set quantity=" + Convert.ToDouble(gv_menu_C.GetRowCellValue(i, "quantity")) + ", strquantity='" + gv_menu_C.GetRowCellValue(i, "quantity").ToString() + "' where idexport='" + idinvoice + "' and idcommodity='" + gv_menu_C.GetRowCellValue(i, "idcommodity").ToString() + "' and give <> 1 ");
                }
                for (int i = 0; i < gv_give_C.DataRowCount; i++)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set quantity=" + Convert.ToDouble(gv_give_C.GetRowCellValue(i, "quantity")) + ", strquantity='" + gv_give_C.GetRowCellValue(i, "quantity").ToString() + "' where idexport='" + idinvoice + "' and idcommodity='" + gv_give_C.GetRowCellValue(i, "idcommodity").ToString() + "' and give = 1 ");
                }   
                //them lại phieu thu moi
                if (cal_duthoi_S.Value >= 0)
                {
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("SELECT idreceipt, idexport, datereceipt, IDEMP, amount, note, proceeds, surplus, idtype,surcharge, discount,vat, idtable, undo,del FROM RECEIPT where idexport='"+ idinvoice +"'");
                    DataRow dr = dt.Rows[0];                   
                    dr["idexport"] = idinvoice;
                    dr["datereceipt"] = DateTime.Now;
                    dr["IDEMP"] = glue_IDEMP_I1.EditValue.ToString();
                    dr["amount"] = cal_costs_S.Value;                
                    dr["proceeds"] = cal_khachdua_S.Value;
                    dr["surplus"] = cal_duthoi_S.Value;                
                    dr["surcharge"] = cal_amountsurcharge_S.Value;
                    dr["discount"] = cal_amountdiscount_S.Value;
                    dr["vat"] = 0;
                    dr["undo"] = false;
                    dr["del"] = false;
                    dr.EndEdit();
                    APCoreProcess.APCoreProcess.Save(dr);
                    passData(true);                    

                    if (chk_inhoadon.Checked == true)
                    {
                        print(idinvoice);
                        insertStraceRetail(idtable, Function.clsFunction.transLateText("Sửa hóa đơn"), idinvoice, "1");
                    }
                    this.Close();
                }
                else
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Số tiền thanh toán chưa đủ");
                }
            }
            catch { }
        }

        private void insertStraceRetail(string _object, string action, string des, string status)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from traceretail where idtrace='1'");
                DataRow dr = dt.NewRow();
                dr["idtrace"] = Function.clsFunction.layMa("TA", "idtrace", "traceretail");
                dr["dateaction"] = DateTime.Now;
                dr["object"] = _object;
                dr["action"] = action;
                dr["descriprion"] = des;
                dr["userid"] = Function.clsFunction._iduser;
                dr["status"] = status;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
                //gct_trace_C.DataSource = APCoreProcess.APCoreProcess.Read("select * from traceretail where CAST(datediff(d,0,dateaction) as datetime) = CAST(datediff(d,0,getdate()) as datetime) ");
            }
            catch { }
        }

        private string getIdexportByIdreceipt(string Idreceipt)
        {
            string invoice = "";
            // lấy số hóa đơn đang tồn tại trong phiên làm việc
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1 idexport  from receipt where idreceipt='" + Idreceipt + "' ");
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0][0].ToString();
            }
            return invoice;
        }

        private void print(string idexport)
        {
            try
            {
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='frm_checkbill' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath + "\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
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
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }
                    //tool.ShowPreviewDialog();

                    //tool.Print();
                    tool.Print(APCoreProcess.APCoreProcess.Read("select * from sysPrintConfig where status=1").Rows[0]["printname"].ToString());
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void LaunchCommandLineOpen(XtraReport xrp)
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

        private void loadTotal()
        {
            try
            {
                decimal total = 0;
                decimal quantity = 0;
                for (int i = 0; i < gv_menu_C.DataRowCount; i++)
                {
                    total += Convert.ToDecimal(gv_menu_C.GetRowCellValue(i, "amount"));
                    quantity += Convert.ToDecimal(gv_menu_C.GetRowCellValue(i, "quantity"));
                }
                cal_costs_S.Value = total;
                cal_amount_S.Value = cal_costs_S.Value + cal_amountsurcharge_S.Value - cal_amountdiscount_S.Value;
                cal_khachdua_S.Value = cal_amount_S.Value;
                if (_load == false)
                {
                    cal_discount_S.Value = Convert.ToDecimal(Math.Round(Convert.ToDouble(cal_amountdiscount_S.Value / cal_costs_S.Value * 100), 1));
                    cal_surcharge_S.Value = Convert.ToDecimal(Math.Round(Convert.ToDouble(cal_amountsurcharge_S.Value / cal_costs_S.Value * 100), 1));
                }
                else
                {
                    //cal_amountdiscount_S.Value = Convert.ToDecimal(Math.Round(Convert.ToDouble(cal_discount_S.Value * cal_costs_S.Value / 100), 0));
                    //cal_amountsurcharge_S.Value = Convert.ToDecimal(Math.Round(Convert.ToDouble(cal_surcharge_S.Value * cal_costs_S.Value / 100), 0));
                }
                _load = true;
            }
            catch { }
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "select idcustomer, customer, tel, fax, address from dmcustomers where status=1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
            }
            catch { }
        }

        private void Init()
        {
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_menu_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_give_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
            loadGridLookupCustomer();
            loadGridLookupEmployee();
            lbl_status_S.Text = Function.clsFunction.transLateText("Đã thanh toán");
            loadMaster();
            loadDetail();
            loadGive();
        }

        private void loadMaster()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     case when ep.vat is null then 0 else ep.vat end as vat, ep.dateimport, ep.idcustomer, ep.IDEMP, ep.idexport, ep.invoice, tb.tableNO, rc.amount, rc.proceeds, rc.surplus, rc.surcharge, rc.discount, rc.del FROM  DMTABLE AS tb INNER JOIN    EXPORT AS ep ON tb.idtable = ep.idtable INNER JOIN RECEIPT AS rc ON ep.idexport = rc.idexport WHERE     (ep.idexport = N'" + idinvoice + "')");
            if (dt.Rows.Count > 0)
            {
                lbl_tableno_S.Text = dt.Rows[0]["tableno"].ToString();
                lbl_thoidiem_S.Text = Convert.ToDateTime(dt.Rows[0]["dateimport"]).ToString("dd/MM/yyyy");
                dateExport=Convert.ToDateTime(dt.Rows[0]["dateimport"]);
                lbl_start_S.Text = Convert.ToDateTime(dt.Rows[0]["dateimport"]).ToString("HH:mm:ss");
                lbl_idpurchase_S.Text = dt.Rows[0]["invoice"].ToString();
                glue_idcustomer_I1.EditValue = dt.Rows[0]["idcustomer"].ToString();
                glue_IDEMP_I1.EditValue = dt.Rows[0]["IDEMP"].ToString();
                cal_costs_S.Value = Convert.ToDecimal(dt.Rows[0]["amount"]);
                cal_amountdiscount_S.Value = Convert.ToDecimal(dt.Rows[0]["discount"]);
                cal_amountsurcharge_S.Value = Convert.ToDecimal(dt.Rows[0]["surcharge"]);
                cal_amount_S.Value = Convert.ToDecimal(dt.Rows[0]["surcharge"]) - Convert.ToDecimal(dt.Rows[0]["discount"]) + Convert.ToDecimal(dt.Rows[0]["amount"]);
            }
        }

        private void loadDetail()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT  idcommodity, quantity, price, quantity* (price-amountdiscount) as amount, amountdiscount as discount,  strquantity FROM  EXPORTDETAIL WHERE     (idexport = N'"+ idinvoice +"') and give <> 1");
                if (dt.Rows.Count > 0)
                {
                    gct_menu_C.DataSource = dt;
                }
            }
            catch { }
        }

        private void loadGive()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT  idcommodity, quantity, price, amount, strquantity FROM  EXPORTDETAIL WHERE     (idexport = N'" + idinvoice + "') and give = 1");
                if (dt.Rows.Count > 0)
                {
                    gct_give_C.DataSource = dt;
                }
            }
            catch { }
        }

        private void deleteCommodity()
        {
            try
            {
                if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa món này không ?"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport='" + idinvoice +"' and idcommodity='"+ gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle,"idcommodity").ToString() +"' and give <> 1 ");
                    gv_menu_C.DeleteRow(gv_menu_C.FocusedRowHandle);
                    loadTotal();
                }
            }
            catch
            {
                Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn món cần xóa");
            }
        }

        private void deleteCommodityGive()
        {
            try
            {
                if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa món này không ?"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport='" + idinvoice + "' and idcommodity='" + gv_give_C.GetRowCellValue(gv_give_C.FocusedRowHandle, "idcommodity").ToString() + "' and give = 1 ");
                    gv_give_C.DeleteRow(gv_give_C.FocusedRowHandle);
                    //loadTotal();
                }
            }
            catch
            {
                Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn món cần xóa");
            }
        }

        private void insertCommodity()
        {
            try
            {

            }
            catch { }
        }

        #endregion

 
        
    }
}