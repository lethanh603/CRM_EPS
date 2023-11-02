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
using DevExpress.XtraGrid.Columns;
using Function;

namespace SOURCE_FORM_REPORT.Presentation
{
    public partial class frm_REPORT_DEVICE : DevExpress.XtraEditors.XtraForm
    {
        public frm_REPORT_DEVICE()
        {
            InitializeComponent();
        }
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0) // thiet bị
            {
                xtabBacDan.PageVisible = false;
                xtabBinhDien.PageVisible = false;
                xtabThietBi.PageVisible = true;
            }
            else if (cboType.SelectedIndex == 1) // bac dạn
            {
                xtabBacDan.PageVisible = true;
                xtabBinhDien.PageVisible = false;
                xtabThietBi.PageVisible = false;
            }
            else if (cboType.SelectedIndex == 2) //  vo xe
            {
                xtabBacDan.PageVisible = true;
                xtabBinhDien.PageVisible = false;
                xtabThietBi.PageVisible = false;
            }
            else { 
                // bình
                xtabBacDan.PageVisible = false;
                xtabBinhDien.PageVisible = true;
                xtabThietBi.PageVisible = false;
            }
        }

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            string sql_tb = @"
                select iddevice, B.brand, DV.model, DV.createdDate,DV.sokhung, DV.namsx, DV.xuatxu, DV.quantity, DV.dongco
                ,''  Attackment , DV.note, C.idcustomer, C.customer , E.IDEMP, E.StaffName
                from DEVICEINFO  DV
                inner join DMBRAND B ON DV.idbrand=B.idbrand
                INNER JOIN DMCUSTOMERS C ON C.idcustomer = DV.idcustomer
				LEFT JOIN EMPCUS EC with(nolock) ON EC.idcustomer = C.idcustomer and ec.status='True'
				LEFT join EMPLOYEES E ON EC.idemp = E.IDEMP where EC.idemp like 
                " + " '%"+glue_IDEMP_I1.EditValue.ToString()+"%'";
            string sql_bd = @"

                select BD.idbinh, BD.modelbinh, BD.thongso, BD.kichthuoc, BD.modelxe, 
                BD.createdDate, BD.quantity, B.brand,C.idcustomer , C.customer , BD.note,
				E.IDEMP, E.StaffName
                from BInhdieninfo BD with(nolock)
                inner join DMBRAND B ON BD.idbrandtype=B.idbrand
                INNER JOIN DMCUSTOMERS C ON C.idcustomer = BD.idcustomer
				LEFT JOIN EMPCUS EC with(nolock) ON EC.idcustomer = C.idcustomer and ec.status='True'
				LEFT join EMPLOYEES E ON EC.idemp = E.IDEMP
                WHERE EC.idemp like 
                " + " '%"+glue_IDEMP_I1.EditValue.ToString()+"%'";
            string sql_vx = @"
               select
                case when bacdanvoxe=1 then 'BD' else 'VX' end type,
                VX.iddevice, thongso, xuatxu, 
                case when bacdanvoxe=1 then VX.brand else B.brand end brand,
                VX.createdDate,quantity, VX.note, C.idcustomer, C.customer , E.IDEMP, E.StaffName
				from VOXEINFO VX with(nolock)
                inner join DMBRAND B ON VX.idbrand=B.idbrand
                INNER JOIN DMCUSTOMERS C ON C.idcustomer = VX.idcustomer
				LEFT JOIN EMPCUS EC with(nolock) ON EC.idcustomer = C.idcustomer and ec.status='True'
				LEFT join EMPLOYEES E ON EC.idemp = E.IDEMP
                where bacdanvoxe=0 and EC.idemp like 
                " + " '%" + glue_IDEMP_I1.EditValue.ToString() + "%' ";
            string sql_bdn = @"

                select
                case when bacdanvoxe=1 then 'BD' else 'VX' end type,
                VX.iddevice, thongso, xuatxu, 
                case when bacdanvoxe=1 then VX.brand else B.brand end brand,
                VX.createdDate,quantity, VX.note,C.idcustomer, C.customer, E.IDEMP, E.StaffName from VOXEINFO VX with(nolock)
                inner join DMBRAND B ON VX.idbrand=B.idbrand
                INNER JOIN DMCUSTOMERS C ON C.idcustomer = VX.idcustomer
				LEFT JOIN EMPCUS EC with(nolock) ON EC.idcustomer = C.idcustomer and ec.status='True'
				LEFT join EMPLOYEES E ON EC.idemp = E.IDEMP
                where bacdanvoxe=1 and EC.idemp like 
                " + " '%" + glue_IDEMP_I1.EditValue.ToString() + "%' "; 

            if (cboType.SelectedIndex == 0) // thiet bị
            {
                sql_tb = sql_tb + " and createdDate between '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd")+"'" + " and C.idcustomer like '%"+ glue_idcustomer_I1.EditValue.ToString() +"%'";
                gct_thietbi_C.DataSource = APCoreProcess.APCoreProcess.Read(sql_tb);
            }
            else if (cboType.SelectedIndex == 1) // bac dạn
            {
                sql_bdn = sql_bdn + " and createdDate between '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd") + "'" + " and C.idcustomer like '%" + glue_idcustomer_I1.EditValue.ToString() + "%'";
                gct_bacdanvoxe_C.DataSource = APCoreProcess.APCoreProcess.Read(sql_bdn);
            }
            else if (cboType.SelectedIndex == 2) //  vo xe
            {
                sql_vx = sql_vx + " and createdDate between '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd") + "'" + " and C.idcustomer like '%" + glue_idcustomer_I1.EditValue.ToString() + "%'";
                gct_bacdanvoxe_C.DataSource = APCoreProcess.APCoreProcess.Read(sql_vx);
            }
            else
            {
                // bình
                sql_bd = sql_bd + " and createdDate between '" + Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd") + "'" + " and C.idcustomer like '%" + glue_idcustomer_I1.EditValue.ToString() + "'";
                gc_binhdien_C.DataSource = APCoreProcess.APCoreProcess.Read(sql_bd);
            }
        }

        private void frm_REPORT_DEVICE_Load(object sender, EventArgs e)
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dte_todate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            loadGridLookupCustomer();
            cboType_SelectedIndexChanged(sender, e);
            loadGridLookupEmp();
            // Binh dien
            
        }
        private void loadGridLookupEmp()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên Nhân Viên" };
                string[] fieldname = new string[] { "idemp", "staffname" };

                if (clsFunction.checkAdmin() && clsFunction._iduser.ToString() != "US000022")
                {
                    string s = clsFunction._iduser.ToString();
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select '' idemp, 'All' staffname union select idemp,staffname from employees  where status=1 ", "staffname", "idemp", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select idemp,staffname from employees where status=1 and CHARINDEX('" + clsFunction.GetIDEMPByUser() + "', idrecursive) >0 ", "staffname", "idemp", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
                }
                glue_IDEMP_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }
        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH" };
                string[] fieldname = new string[] { "idcustomer", "customer" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "select '' idcustomer, 'All' customer union select idcustomer,customer from dmcustomers where status=1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width);
       

            }
            catch { }
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
    }
}