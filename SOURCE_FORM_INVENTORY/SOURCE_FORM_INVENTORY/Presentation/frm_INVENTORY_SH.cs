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
using DevExpress.XtraReports.UI;

namespace SOURCE_FORM_INVENTORY.Presentation
{
    public partial class frm_INVENTORY_SH : DevExpress.XtraEditors.XtraForm
    {

        #region Var
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        #endregion

        #region Contructor

        public frm_INVENTORY_SH()
        {
            InitializeComponent();
        }

        #endregion

        #region FormEvent

        private void frm_INVENTORY_SH_Load(object sender, EventArgs e)
        {
 
            _InitializeComponent();
        }

        #endregion

        
        #region ButtonEvent
        private void btn_search_Click(object sender, EventArgs e)
        {
            loadInventory();
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            print("");
        }
        private void btn_config_S_Click(object sender, EventArgs e)
        {
            loadReport();
        }
        #endregion

        #region Event

        private void loadProcess_DoWork(object sender, DoWorkEventArgs e)
        {
          
            Function.frmProcess frm = new Function.frmProcess();
            frm.Show();
            _InitializeComponent();
            frm.Dispose();
        }

        private void loadProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        
        }

        private void runProcess_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void runProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void com_month_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int year = 0;
                int month = 0;
                int day = 1;
                year = Convert.ToInt16(com_year.Text);
                month = com_month.SelectedIndex + 1;
                dte_fromdate.EditValue = new DateTime(year, month, day);
                dte_todate.EditValue = Convert.ToDateTime(dte_fromdate.EditValue).AddMonths(1).AddDays(-1);
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void com_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) || !Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void com_month_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void com_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComMonth();
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_INVENTORY_SH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                btn_search.PerformClick();
            }
            if (e.KeyCode == Keys.F6)
            {
                btn_print.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                this.Dispose();
            }
        } 

        #endregion

        #region Grid

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


        #endregion

        #region Methods

        private void _InitializeComponent()
        {
            loadYear();
            loadSector();
            loadGroup();
            loadWarehouse();
            loadComGrid();
        }

        private void loadComMonth()
        {
            try
            {
                if (com_month.Properties.Items.Count > 0)
                    com_month.Properties.Items.Clear();
                //com_month.Properties.ReadOnly = true;
                for (int i = 1; i <= 12; i++)
                {
                    com_month.Properties.Items.Add(i.ToString().PadLeft(2,'0')+"/"+com_year.Text);
                }
                com_month.SelectedIndex = DateTime.Now.Month-1;
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadYear()
        {
            try
            {
                if(com_year.Properties.Items.Count>0)
                    com_year.Properties.Items.Clear();
                //com_year.Properties.ReadOnly = true;
                for (int i = 10; i >= 1; i--)
                {
                    com_year.Properties.Items.Add((DateTime.Now.Year+i-10).ToString());
                }
                com_year.SelectedIndex=0;
                loadComMonth();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadSector()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select 'All'  idsector, 'All' sector union select idsector, sector from dmsectors");
                glue_idsector_I1.Properties.DataSource = dt;
                glue_idsector_I1.EditValue = glue_idsector_I1.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGroup()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select 'All'  idgroup, 'All' groupname union select idgroup, groupname from dmgroup");
                glue_idgroup_I1.Properties.DataSource = dt;
                glue_idgroup_I1.EditValue = glue_idgroup_I1.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadWarehouse()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select 'All'  idwarehouse, 'All' warehouse union select idwarehouse, warehouse from dmwarehouse");
                glue_idwarehouse_I1.Properties.DataSource = dt;
                glue_idwarehouse_I1.EditValue = glue_idwarehouse_I1.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadComGrid()
        {
            try
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_inventory_C.Columns["IDSECTOR"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idsector, sector from dmsectors where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_inventory_C.Columns["IDGROUP"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup, groupname from dmgroup where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_inventory_C.Columns["IDWAREHOUSE"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idwarehouse, warehouse from dmwarehouse where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_inventory_C.Columns["IDUNIT"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idunit, unit from dmunit where status=1");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_inventory_C.Columns["IDCOMMODITY"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity, commodity from dmcommodity where status=1");
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadInventory()
        {
            try
            {
                DataTable dt = new DataTable();
                string idsector = string.Empty;
                string idgroup = string.Empty;
                string idkind = string.Empty;
                string idcommodity = string.Empty;
                string idwarehouse = string.Empty;
                if (glue_idsector_I1.EditValue.ToString() != "All")
                {
                    idsector = glue_idsector_I1.EditValue.ToString();
                }
                else
                {
                    idsector = "";
                }
                if (glue_idgroup_I1.EditValue.ToString() != "All")
                {
                    idgroup = glue_idgroup_I1.EditValue.ToString();
                }
                else
                {
                    idgroup = "";
                }
                if (glue_idwarehouse_I1.EditValue.ToString() != "All")
                {
                    idwarehouse = glue_idwarehouse_I1.EditValue.ToString();
                }
                else
                {
                    idwarehouse = "";
                }
                if (txt_idcommodity_S.Text != "")
                {
                    idcommodity = txt_idcommodity_S.Text;
                }
                else
                {
                    idcommodity = "";
                }
                string[,] param = new string[6, 2] { { "date", Convert.ToDateTime(dte_todate.EditValue).ToString("yyyyMMdd") }, { "idsector", idsector }, { "idgroup", idgroup }, { "idkind", "" }, { "idcom", idcommodity },{"idware",idwarehouse} };
                dt = Function.clsFunction.Excute_Proc("getInventory", param);
                gct_inventory_C.DataSource = dt;
                gv_inventory_C.ExpandAllGroups();
            }
            catch(Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void print(string idexport)
        {
            try
            {
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='frm_INVENTORY_SH' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath + "\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    Function.clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        string idsector = string.Empty;
                        string idgroup = string.Empty;
                        string idkind = string.Empty;
                        string idcommodity = string.Empty;
                        string idwarehouse = string.Empty;
                        if (glue_idsector_I1.EditValue.ToString() != "All")
                        {
                            idsector = glue_idsector_I1.EditValue.ToString();
                        }
                        else
                        {
                            idsector = "";
                        }
                        if (glue_idgroup_I1.EditValue.ToString() != "All")
                        {
                            idgroup = glue_idgroup_I1.EditValue.ToString();
                        }
                        else
                        {
                            idgroup = "";
                        }
                        if (glue_idwarehouse_I1.EditValue.ToString() != "All")
                        {
                            idwarehouse = glue_idwarehouse_I1.EditValue.ToString();
                        }
                        else
                        {
                            idwarehouse = "";
                        }
                        if (txt_idcommodity_S.Text != "")
                        {
                            idcommodity = txt_idcommodity_S.Text;
                        }
                        else
                        {
                            idcommodity = "";
                        }
                        string[,] param = new string[6, 2] { { "date", Convert.ToDateTime(dte_todate.EditValue).ToString("yyyyMMdd") }, { "idsector", idsector }, { "idgroup", idgroup }, { "idkind", "" }, { "idcom", idcommodity }, { "idware", idwarehouse } };
                        dt = Function.clsFunction.Excute_Proc("getInventory", param);
                    }
                    else
                    {
                       // dt = APCoreProcess.APCoreProcess.Read("SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL  THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,   CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, mh.commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END AS quantity,  CASE WHEN dt.price IS NULL THEN 0 ELSE dt.price END AS price, CASE WHEN dt.amountvat IS NULL THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat,  dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3), CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END)  + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100) AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount,  pt.amount + pt.surcharge - pt.discount AS amount1, CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1,   CASE WHEN dt.give = 1 THEN 0 ELSE dt.price END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN   DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN    DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN   DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) UNION SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,  CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, N'-> Giảm giá ' + CONVERT(nvarchar(3), dt.discount) + ' %' AS commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL  THEN 0 ELSE dt.quantity END AS quantity, CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END AS price, CASE WHEN dt.amountvat IS NULL    THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat, dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3),   CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END) + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100)    AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount, pt.amount + pt.surcharge - pt.discount AS amount1,     CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1, CASE WHEN dt.give = 1 THEN 0 ELSE CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN    RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN      DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN      DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN    DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) AND (dt.give = 0) AND (dt.amountdiscount > 0)");
                    }
              
                    DataTable dtO = new DataTable();
                    dtO = dt.DefaultView.ToTable();
                    ds.Tables.Add(dtO);
                    report.DataSource = ds;
                    report.DataMember = "Data";// phair co datamember moi len dc group
                    report.DataAdapter = ds;
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
        private void loadReport()
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = this.Name;
                string idsector = string.Empty;
                string idgroup = string.Empty;
                string idkind = string.Empty;
                string idcommodity = string.Empty;
                string idwarehouse = string.Empty;
                if (glue_idsector_I1.EditValue.ToString() != "All")
                {
                    idsector = glue_idsector_I1.EditValue.ToString();
                }
                else
                {
                    idsector = "";
                }
                if (glue_idgroup_I1.EditValue.ToString() != "All")
                {
                    idgroup = glue_idgroup_I1.EditValue.ToString();
                }
                else
                {
                    idgroup = "";
                }
                if (glue_idwarehouse_I1.EditValue.ToString() != "All")
                {
                    idwarehouse = glue_idwarehouse_I1.EditValue.ToString();
                }
                else
                {
                    idwarehouse = "";
                }
                if (txt_idcommodity_S.Text != "")
                {
                    idcommodity = txt_idcommodity_S.Text;
                }
                else
                {
                    idcommodity = "";
                }
                string[,] param = new string[6, 2] { { "date", Convert.ToDateTime(dte_todate.EditValue).ToString("yyyyMMdd") }, { "idsector", idsector }, { "idgroup", idgroup }, { "idkind", "" }, { "idcom", idcommodity }, { "idware", idwarehouse } };

                frm.arrParam = param;
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

 




        

    }
}