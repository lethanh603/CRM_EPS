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
using DevExpress.XtraGrid.Views.Grid;

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_ReportQuotationDetail : DevExpress.XtraEditors.XtraForm
    {
        public frm_ReportQuotationDetail()
        {
            InitializeComponent();
        }
        #region Var
        public bool statusForm = false;
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        public string idexport = "";
        #endregion
        private void frm_ReportQuotationDetail_Load(object sender, EventArgs e)
        {
            //statusForm = true;
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
                loadGrid();
            }
        }

        #region Method

        private void loadGrid()
        {
            try
            {
                dts = APCoreProcess.APCoreProcess.Read("SELECT  dt.status,  mh.sign, dt.iddetail, dt.idcommodity, dt.idunit, dt.idwarehouse, dt.quantity, dt.price, dt.amount, dt.vat, dt.amountvat, dt.davat, dt.discount, dt.amountdiscount, dt.costs, dt.total,  dt.note, dt.idexport, dt.spec, mh.commodity, dt.timedelivery, dt.partnumber, dt.description, dt.equipmentinfo, dt.cogs, dt.baohanh, dt.idgrouptk FROM         QUOTATIONDETAIL AS dt INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity where idexport='" + idexport + "'");
                gct_list_C.DataSource = dts;
            }
            catch (Exception ex) 
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { "quantity", "mount", "amountvat", "amountdiscount", "costs", "total" };
            // Caption column
            //col_iddetail_IK1/col_idcommodity_I1/col_partnumber_I2/col_spec_I2/col_commodity_S/col_idunit_I1/col_idwarehouse_I1/col_quantity_I4/col_price_I4/col_amount_I15/col_vat_I4/col_amountvat_I15/col_davat_I6/col_discount_I8/col_amountdiscount_I15/col_costs_I15/col_total_I15/col_timedelivery_I2/col_note_I3/col_idexport_I1/
            string[] caption_col = new string[] { "ID", "Mã hàng","Ký hiệu", "Tên hàng", "ĐVT", "Kho hàng", "Số lượng", "Đơn giá", "Thành tiền", "VAT", "Tiền VAT", "CK Sau VAT", "CK", "Tiền CK", "Chi phí", "Tổng tiền", "Thời điểm GH", "Ghi chú", "ID" };

            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "iddetail", "idcommodity", "sign", "commodity", "idunit", "idwarehouse", "quantity", "price", "amount", "vat", "amountvat", "davat", "discount", "amountdiscount", "costs", "total", "timedelivery", "note", "idexport" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CheckColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "MemoColumn", "MemoColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "250", "200", "60", "200", "100", "100", "100", "60", "100", "100", "60", "100", "100", "100", "200", "200", "100" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "False", "True", "True", "False", "True", "True", "True", "True", "True", "False", "False", "False", "False", "True", "False", "True", "False" };
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
            string[] sql_glue = new string[] {  "select idunit, unit  from dmunit where status=1", "select idwarehouse, warehouse  from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "unit", "warehouse" };

            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] {"idunit", "idwarehouse" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] {{ "Mã ĐV", "ĐVT" }, { "Mã kho", "Kho" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] {{ "idunit", "unit" }, { "idwarehouse", "warehouse" } };
            //so cot
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_EXPORTDETAIL_C.Name);
            clsFunction.CreateTableGrid(fieldname_col, gv_EXPORTDETAIL_C);
            gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;
            gv_EXPORTDETAIL_C.OptionsBehavior.Editable = false;
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
            string[] gluenulltext = new string[] { };
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
                gv_EXPORTDETAIL_C.OptionsView.ColumnAutoWidth = false;
                gv_EXPORTDETAIL_C.OptionsView.ShowAutoFilterRow = true;


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        #endregion

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ReportQuotationDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
        }
    }
}