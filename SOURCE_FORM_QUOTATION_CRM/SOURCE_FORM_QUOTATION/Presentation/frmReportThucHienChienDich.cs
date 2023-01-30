using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using Function;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frmReportThucHienChienDich: DevExpress.XtraEditors.XtraForm
    {
        public frmReportThucHienChienDich()
        {
            InitializeComponent();
        }

        #region Var
        public bool statusForm = false;
        public string _sign = "QS";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void StrPassData(string value);
        public StrPassData strpassData;
        #endregion

        #region FormEvent

        private void frmSearchPurchar_Load(object sender, EventArgs e)
        {
            // statusForm = true;
            Function.clsFunction._keylience = true;
            if (statusForm == true)
            {
                SaveGridControls();
                clsFunction.Save_sysControl(this, this);
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                Load_Grid();
                Function.clsFunction.TranslateGridColumn(gv_EXPORTDETAIL_C);
                loadGridLookupGroup();
                loadGridLookupEmployee();
                loadGridLookupMethod();
                loadGridLookupCampaign();
                Init();
                loadGrid();
            }
        }

        private void frmSearchPurchar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F10)
            {
                btn_tim_S.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
        }


        #endregion

        #region ButtonEvent

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_view_S_Click(object sender, EventArgs e)
        {
            try
            {
                strpassData(gv_EXPORTDETAIL_C.GetRowCellValue(gv_EXPORTDETAIL_C.FocusedRowHandle, "idexport").ToString());
            }
            catch
            { }
            this.Close();
        }

        private void btn_tim_S_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Ký hiệu NV", "Nhân viên", "Đội", "Chiến dịch", "Tổng số KH", "Khách được CS", "Khách có NC", "Khách đang HG", "Khách nhận BG", "Khách hủy BG", "Khách đặt ĐH", "Tình trạng nhân viên" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_idemp_S", "col_staffname_S", "col_teamname_S", "col_campaign_S", "col_cuscount_S",  "col_cuscampaigncount_S", "col_sonhucau_S", "col_sohoigia_S", "col_sobaogia_S", "col_sobaogiahuy_S", "col_sodonhang_S", "col_status_S" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn",  "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "SpinEditColumn", "CheckColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "150", "150", "100", "100", "100", "100", "100", "100", "100", "100",  "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True", "True", "True", "True",  "True", "True", "True", "True" };
            // datasource lookupEdit
            string[] sql_lue = new string[] { };
            // Caption lookupEdit
            string[] caption_lue = new string[] { };
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] { };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[0, 0];
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[0, 0];
            //so cot
            //int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers", "select IDEMP, StaffName  from EMPLOYEES", "select idstatusquotation, statusquotation from dmstatusquotation  ", "select idquotationtype, quotationtype frm dmquotationtype where status=1", "select idcurrency, currency from dmcurrency where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "StaffName", "status", "idquotationtype", "currency" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "IDEMP", "idstatusquotation", "idquotationtype", "idcurrency" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[5, 2] { { "Mã KH", "Khách hàng" }, { "Mã NV", "Nhân viên" }, { "ID", "Tình trạng" }, { "ID", "Loại báo giá" }, { "ID", "Tiền tệ" } };

            string[,] fieldname_glue_col = new string[5, 2] { { "idcustomer", "customer" }, { "IDEMP", "StaffName" }, { "idstatusquotation", "statusquotation" }, { "idquotationtype", "quotationtype" }, { "idcurrency", "currency" } };
            //so cot
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns_Edit(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_EXPORTDETAIL_C.Name);
            //clsFunction.CreateTableGrid(fieldname_col, gv_PURCHASEDETAIL_C);
        }

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


        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "", "", "", "", "" };
            bool show_footer = true;
            // show filterRow
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_EXPORTDETAIL_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_EXPORTDETAIL_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["column_name"].ToString().Split('/'), dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()), gluenulltext);
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_EXPORTDETAIL_C.OptionsBehavior.Editable = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #endregion

        #region Methods

        private void loadGridLookupMethod()
        {
            try
            {
                string[] caption = new string[] { "Mã loại", "Loại báo giá" };
                string[] fieldname = new string[] { "idquotationtype", "quotationtype" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcampaign_I1, "select '' as idquotationtype, N'" + clsFunction.transLateText("Tất cả") + "' as quotationtype union select idquotationtype, quotationtype from dmquotationtype where status=1 ", "quotationtype", "idquotationtype", caption, fieldname, this.Name, glue_idcampaign_I1.Width * 1, col_visible);


            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_IDEMP_I1, "select '' as IDEMP, N'" + clsFunction.transLateText("Tất cả") + "' as StaffName union select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width, col_visible);
            }
            catch { }
        }

        private void loadGridLookupCampaign()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã CD", "Chiến dịch" };
                string[] fieldname = new string[] { "idcampaign", "campaign" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idcampaign_I1, "select '' as idcampaign, N'" + clsFunction.transLateText("Tất cả") + "' as campaign union select idcampaign, campaign from dmcampaign", "campaign", "idcampaign", caption, fieldname, this.Name, glue_idcampaign_I1.Width, col_visible);
            }
            catch { }
        }

        private void loadGridLookupGroup()
        {
            try
            {
                string[] caption = new string[] { "Mã đội", "Tên đội" };
                string[] fieldname = new string[] { "idgroup", "groupname" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEditSearch(glue_idgroup_I1, "select '' as idgroup, N'" + clsFunction.transLateText("Tất cả") + "' as groupname union select idgroup, groupname from dmgroupemp where status=1", "groupname", "idgroup", caption, fieldname, this.Name, glue_idgroup_I1.Width, col_visible);

            }
            catch { }
        }

        private void Init()
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dte_todate_S.EditValue = DateTime.Now;
            btn_tim_S.PerformClick();
        }

        private void loadGrid()
        {
            try
            {
                if (Function.clsFunction._pre == true)
                {
                    dts = Function.clsFunction.dtTrace;
                }
                else
                {
          

                    //dts = APCoreProcess.APCoreProcess.Read("SELECT QO.limit, QO.vat, QO.dateimport, QO.limitdept, QO.idcustomer, QO.IDEMP, QO.isdept, QO.outlet, QO.idexport, QO.note, QO.idtable, QO.discount, QO.amountdiscount,   QO.surcharge, QO.amountsurcharge, QO.invoice, QO.complet, QO.retail, QO.status, QO.cancel, QO.isdelete, QO.tableunion, QO.isdiscount, QO.issurcharge,    QO.reasondiscount, QO.reasonsurcharge, QO.IDMETHOD, QO.isvat, QO.isbrowse, QO.reasonbrowse, QO.idcurrency, QO.exchangerate, QO.idstatus FROM            dbo.QUOTATION AS QO  WHERE        (CAST(DATEDIFF(d, 0, QO.dateimport) AS datetime) BETWEEN CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToShortDateString() + "') AS datetime) AND CAST(DATEDIFF(d, 0, '" + Convert.ToDateTime(dte_todate_S.EditValue).ToShortDateString() + "') AS datetime))  AND (QO.isdelete = 0) ");
                    String sql = "SELECT     EMP.StaffName AS staffname, TEAM.groupname As teamname, EMP.sign as idemp, CA.campaign, isnull( count(EC.idcustomer),0) cuscount , ";
sql += " isnull(C1.count,0) as cusnewcount, isnull( C2.cuscampaigncount,0) as cuscampaigncount, isnull(C3.sonhucau,0) sonhucau, ";
sql += " isnull(C4.sokhaosat,0) sokhaosat, isnull(C5.sohoigia,0) sohoigia ,isnull(C6.sobaogia,0) sobaogia ,isnull(C7.sobaogiahuy,0) sobaogiahuy,isnull(C11.sodonhang,0) sodonhang,";
sql += " isnull(C8.tonggiatri,0) tonggiatri,EMP.status, isnull(C9.chuyendi,0) chuyendi, isnull(C10.nhanve,0) nhanve";
sql += " FROM         dbo.EMPCUS AS EC INNER JOIN";
sql +=                       " dbo.EMPLOYEES AS EMP ON EC.idemp = EMP.IDEMP LEFT JOIN";
sql +=                       " dbo.DMGROUPEMP AS TEAM ON EMP.idgroup = TEAM.idgroup INNER JOIN DMCAMPAIGN CA ON EMP.idemp=CA.idemp ";
sql += 					  " LEFT JOIN";
sql += 					  " (SELECT     count(EC.idcustomer) AS count, EMP.IDEMP";
sql += " FROM         dbo.EMPCUS AS EC INNER JOIN";
sql +=                       " dbo.EMPLOYEES AS EMP ON EC.idemp = EMP.IDEMP ";
sql += 					  " WHERE  EC.date1 between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') ";
sql += 					  " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"') ";
sql += " GROUP BY EMP.StaffName,  EMP.IDEMP )  C1 ";
sql += " ON C1.IDEMP = EMP.IDEMP";
sql += " Left join (SELECT     COUNT(MISS.idcustomer) AS cuscampaigncount, CP.idemp";
sql += " FROM         dbo.MISSION AS MISS INNER JOIN";
sql += "                      dbo.DMCAMPAIGN AS CP ON MISS.idcampaign = CP.idcampaign";
sql += " WHERE     (CP.todate BETWEEN CONVERT(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"')";
sql += " AND CONVERT(datetime, '"+ dte_todate_S.EditValue.ToString() +"')) AND (CP.fromdate BETWEEN ";
sql += " CONVERT(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') AND ";
sql +=  "                     CONVERT(datetime, '"+ dte_todate_S.EditValue.ToString() +"'))";
sql += " GROUP BY CP.idemp ) C2 ON EMP.IDEMP = C2.IDEMP";
sql += " LEFT JOIN (select Q.idemp, count(DISTINCT Q.idcustomer) sonhucau from QUOTATION Q where Q.dateimport between ";
sql += " convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"') ";
sql += " group by Q.idemp) C3 ON EMP.IDEMP = C3.IDEMP";

sql += " LEFT JOIN (select Q.idemp, count(DISTINCT Q.idcustomer) sokhaosat from QUOTATION Q INNER JOIN SURVEY S ON Q.idexport = S.idquotation ";
sql += " where Q.dateimport between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') ";
sql += " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"')  group by idemp) C4 ON EMP.IDEMP = C4.IDEMP";

sql += " LEFT JOIN (select Q.idemp, count(DISTINCT  Q.idcustomer) sohoigia from QUOTATION Q where Q.idstatusquotation = 'ST000001'";
 sql += " and Q.dateimport between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') ";
 sql += " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"')  group by Q.idemp) C5 ON EMP.IDEMP = C5.IDEMP";

sql += " LEFT JOIN (select Q.idemp, count( DISTINCT  Q.idcustomer) sobaogia from QUOTATION Q ";
sql += " where (Q.idstatusquotation = 'ST000002' or Q.idstatusquotation = 'ST000003' or Q.idstatusquotation = 'ST000004'  ) ";
sql += " and Q.dateimport between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') ";
sql += " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"')  group by Q.idemp) C6 ON EMP.IDEMP = C6.IDEMP";

sql += " LEFT JOIN (select Q.idemp, count( DISTINCT  Q.idcustomer) sobaogiahuy from QUOTATION Q ";
sql += " where Q.idstatusquotation = 'ST000005' and Q.dateimport between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"') ";
sql += " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"')  group by Q.idemp) C7 ON EMP.IDEMP = C7.IDEMP";

sql += " LEFT JOIN (select Q.idemp, count( DISTINCT  Q.idcustomer) sodonhang from QUOTATION Q ";
sql += " where Q.idstatusquotation = 'ST000004' and Q.dateimport between convert(datetime, '" + dte_fromdate_S.EditValue.ToString() + "') ";
sql += " and convert(datetime, '" + dte_todate_S.EditValue.ToString() + "')  group by Q.idemp) C11 ON EMP.IDEMP = C11.IDEMP";

sql += " LEFT JOIN (select Q.idemp, sum(D.total) tonggiatri from QUOTATION Q INNER JOIN QUOTATIONDETAIL D ";
sql += " ON Q.idexport = D.idexport where D.status=1 AND  Q.idstatusquotation <> 'ST000005' and Q.dateimport ";
sql += " between convert(datetime, '"+ dte_fromdate_S.EditValue.ToString() +"')";
sql += " and convert(datetime, '"+ dte_todate_S.EditValue.ToString() +"')  group by Q.idemp) C8 ON EMP.IDEMP = C8.IDEMP";


sql += " left join (SELECT     count(EC.idcustomer) AS chuyendi, EC.IDEMP FROM         dbo.EMPCUSHIS AS EC  ";
sql += " WHERE  EC.datecn between convert(datetime, '" + dte_fromdate_S.EditValue.ToString() + "')  and convert(datetime, '" + dte_todate_S.EditValue.ToString() + "')  ";
sql += " and EC.status=1 GROUP BY EC.IDEMP,EC.status )  C9 ON C9.idemp = EMP.IDEMP ";

sql += " left join (SELECT     count(EC.idcustomer) AS nhanve, EC.IDEMP FROM         dbo.EMPCUSHIS AS EC  ";
sql += " WHERE  EC.datecn between convert(datetime, '" + dte_fromdate_S.EditValue.ToString() + "')  and convert(datetime, '" + dte_todate_S.EditValue.ToString() + "') and  EC.status=0 GROUP BY  ";
sql += " EC.IDEMP, EC.status )  C10 ON C10.idemp = EMP.IDEMP ";


sql += " GROUP BY EMP.StaffName, TEAM.groupname, CA.campaign, EMP.sign,C1.count,C2.cuscampaigncount, C3.sonhucau,C4.sokhaosat,";
sql += " C5.sohoigia, C6.sobaogia, C7.sobaogiahuy,C8.tonggiatri,EMP.status, C9.chuyendi, C10.nhanve, C11.sodonhang";

string str = " having 1=1  ";
if (glue_IDEMP_I1.EditValue.ToString() != "")
{
    str += " and   EMP.StaffName='" + glue_IDEMP_I1.Text+ "'";
}
if (glue_idgroup_I1.EditValue.ToString() != "")
{
    str += " and  TEAM.groupname='" + glue_idgroup_I1.Text + "'";
}

if (glue_idcampaign_I1.EditValue.ToString() != "")
{
    str += " and  CAM.campaign='" + glue_idcampaign_I1.Text + "'";
}
sql = sql + str;
sql = "with temp as (" + sql + ") select sign, staffname, teamname, cuscount,cusnewcount,(cuscount-cuscampaigncount) as khachhangchuachamsoc, sonhucau, (cuscount-sonhucau) as chuanhucau, chuyendi as chuyendi,nhanve as nhanve, sodonhang  from temp";
dts = APCoreProcess.APCoreProcess.Read(sql);

                }
                gct_list_C.DataSource = dts;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        #endregion
    }
}