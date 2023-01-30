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

namespace SOURCE_FORM_REPORT.Presentation
{
    public partial class frm_Device_DETAIL : DevExpress.XtraEditors.XtraForm
    {
        public frm_Device_DETAIL()
        {
            InitializeComponent();
        }
        public string fromDate = "";
        public string toDate = "";
        public string idEmp = "";
        public string idStatus = "";
        public string idCustomer = "";

        private void frm_Device_DETAIL_Load(object sender, EventArgs e)
        {
            loadDataDevice();
            loadDataVoXe();
            loadDataBacDan();
            loadBinhDien();

            ControlDev.FormatControls.setContainsFilter(gv_bacdan_C);
            ControlDev.FormatControls.setContainsFilter(gv_binhdien_C);
            ControlDev.FormatControls.setContainsFilter(gv_device_C);
            ControlDev.FormatControls.setContainsFilter(gv_voxe_C);
        }

        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

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
        #endregion

        private void loadDataDevice()
        {
            string sql = @"
            SELECT DV.iddevice, DV.createdDate ngay_tao, DV.upadatedat,B.brand, DV.model,
            isnull((DV.quantity),0) soluong, DV.dongco, DV.hethongphutro, dv.sokhung,DV.namsx,dv.xuatxu				
            FROM 					
             DEVICEINFO DV with(nolock)
 
            INNER JOIN DMBRAND B with(nolock) ON B.idbrand = DV.idbrand
            WHERE 
            cast( DV.createdDate as date) between '{0}' and '{1}' {2}
            ";

            string expCustomer = " AND idcustomer like '%" + idCustomer + "%'";

            sql = string.Format(sql, fromDate, toDate, expCustomer);
       
            gct_device_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }

        private void loadDataBacDan()
        {
            string sql = @"
            SELECT DV.sign iddevice, DV.createdDate ngay_tao, DV.upadatedat,DV.brand	,DV.thongso,
            isnull((DV.quantity),0) soluong, dv.xuatxu, dv.note	, DV.brand			
            FROM  VOXEINFO DV with(nolock)
            WHERE  cast( DV.createdDate as date) between '{0}' and '{1}' and DV.bacdanvoxe=1  {2}
            ";
            string expCus = " AND idcustomer like '%" + idCustomer + "%'";

            sql = string.Format(sql, fromDate, toDate,expCus);
            //MessageBox.Show(sql);
            gct_bacdan_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }
        private void loadDataVoXe()
        {
            string sql = @"
            SELECT DV.iddevice, DV.createdDate ngay_tao, DV.upadatedat,B.brand,DV.thongso,
            isnull((DV.quantity),0) soluong, dv.xuatxu, dv.note				
            FROM  VOXEINFO DV with(nolock)
            INNER JOIN DMBRAND B with(nolock) ON B.idbrand = DV.idbrand
            WHERE  cast( DV.createdDate as date) between '{0}' and '{1}' and DV.bacdanvoxe=0  {2} 
            ";
            string expCus = " AND idcustomer like '%" + idCustomer + "%'";

            sql = string.Format(sql, fromDate, toDate, expCus);
            //MessageBox.Show(sql);
            gct_voxe_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }

        private void loadBinhDien()
        {
            string sql = @"
            SELECT DV.iddevice, DV.createdDate ngay_tao, DV.upadatedat , BT.brand brand_xe , BT2.brand brand_binh,DV.modelxe, isnull((DV.quantity),0) soluong,DV.modelbinh,DV.thongso,DV.kichthuoc,
             dv.note				
            FROM  BINHDIENINFO DV with(nolock)
			LEFT JOIN DMBRAND BT with(nolock)
			ON DV.iddevice = BT.idbrand			LEFT JOIN DMBRAND BT2 with(nolock)
			ON DV.idbrandtype = BT2.idbrand
            WHERE  cast( DV.createdDate as date) between '{0}' and '{1}' {2}
            ";
            string expCus = " AND DV.idcustomer like '%" + idCustomer + "%'";

            sql = string.Format(sql, fromDate, toDate, expCus);
            //MessageBox.Show(sql);
            gct_binhdien_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }
    }
}