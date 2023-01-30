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
    public partial class frmCSKH_DETAIL : DevExpress.XtraEditors.XtraForm
    {
        public frmCSKH_DETAIL()
        {
            InitializeComponent();
        }
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        public string fromDate = "";
        public string toDate = "";
        public string idCustomer = "";

        private void frmCSHK_DETAIL_Load(object sender, EventArgs e)
        {
            loadCSKH();
            ControlDev.FormatControls.setContainsFilter(gv_device_C);
        }

        private void loadCSKH()
        {
            string sql = @"
                            SELECT E.StaffName, PC.datecontact,  CC.contactname, S.statusname, 
                            PC.description, PC.description2, PC.note
                            FROM PLANCRM PC with(nolock)
                            INNER JOIN DMCUSTOMERS C with(nolock) ON PC.idcustomer = C.idcustomer
                            INNER JOIN CUSCONTACT CC with(nolock) ON CC.idcontact = PC.idcontact
                            INNER JOIN DMSTATUS S with(nolock) ON S.idstatus= PC.idstatus
							INNER JOIN EMPLOYEES E with(nolock) ON E.IDEMP = PC.idemp

                            WHERE cast( PC.datecontact as date) between '{0}' and '{1}' {2}
                            ";

            string expEmp = " AND PC.idcustomer like '%" + idCustomer + "%'";

            sql = string.Format(sql, fromDate, toDate, expEmp);
           
            gct_device_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }


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

    }
}