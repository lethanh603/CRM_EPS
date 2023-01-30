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

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_ShareCus : DevExpress.XtraEditors.XtraForm
    {
        public frm_ShareCus()
        {
            InitializeComponent();
        }

        #region Var

        public delegate void PassData(bool value);
        public PassData passData;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        #endregion

        #region FormEvent

        private void frm_ShareCus_Load(object sender, EventArgs e)
        {
            loadEmp();

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

        private void gv_employees_C_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (gv_employees_C.FocusedRowHandle >= 0)
                {
                    loadCus();
                }
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
        }


        #endregion

        #region Method

        private void loadCusByEmp()
        {
            try
            {
                DataTable dt = new DataTable();

                for (int i = 0; i < gv_customer_C.RowCount; i++)
                {
                    dt = APCoreProcess.APCoreProcess.Read("SELECT idcustomer from EMPCUS where idemp='" + gv_employees_C.GetRowCellValue(gv_employees_C.FocusedRowHandle, "idemp").ToString() + "' and idcustomer='" + gv_customer_C.GetRowCellValue(i, "idcustomer").ToString() + "' ");
                    if (dt.Rows.Count > 0)
                    {
                        gv_customer_C.SetRowCellValue(i, "check", true);
                    }
                    else
                    {
                        gv_customer_C.SetRowCellValue(i, "check", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
        }


        private void loadEmp()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     e.IDEMP AS idemp, e.StaffName AS staffname, d.department FROM dbo.EMPLOYEES AS e INNER JOIN   dbo.DMDEPARTMENT AS d ON e.iddepartment = d.iddepartment WHERE     (e.status = 1)");
                DataColumn dtc = new DataColumn("check", typeof(bool));
                dtc.DefaultValue = false;
                dt.Columns.Add(dtc);
                gct_employees_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
        }

        private void loadCus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT     c.idcustomer, c.customer, t.customertype FROM  dbo.DMCUSTOMERS AS c INNER JOIN dbo.DMCUSTOMERTYPE AS t ON c.idtype = t.idtype INNER JOIN    dbo.EMPCUS AS EC ON c.idcustomer = EC.idcustomer WHERE     (c.status = 1) AND (EC.idemp = '" + gv_employees_C.GetRowCellValue(gv_employees_C.FocusedRowHandle, "idemp").ToString() + "')");
                DataColumn dtc = new DataColumn("check", typeof(bool));
                dtc.DefaultValue = false;
                dt.Columns.Add(dtc);
                gct_customer_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo x", "Lỗi :" + ex.Message);
            }
        }

        private bool checkInput()
        {
            bool flag = false;
            try
            {
                for (int i = 0; i < gv_employees_C.RowCount; i++)
                {
                    if ((bool)gv_employees_C.GetRowCellValue(i, "check") == true)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
            return flag;
        }

        private bool checkExist(string idemp, string idcustomer)
        {
            bool flag = false;
            try
            {
                if (APCoreProcess.APCoreProcess.Read("select * from EMPCUS where idemp='" + idemp + "' and idcustomer='" + idcustomer + "'").Rows.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
            return flag;
        }

        #endregion

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            frm_DisCus frm = new frm_DisCus();
            frm.strpassData = new frm_DisCus.strPassData(getValueEmp);

            string sCus = "";
            for (int i = 0; i < gv_customer_C.RowCount; i++)
            {
                if ((bool)gv_customer_C.GetRowCellValue(i, "check"))
                {
                    sCus += gv_customer_C.GetRowCellValue(i, "idcustomer").ToString() + "@";
                }
            }
            if (sCus.Length == 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Bạn chưa chọn khách hàng");
                return;
            }
            frm.idcustomer = sCus;
            frm.idempdis = gv_employees_C.GetRowCellValue(gv_employees_C.FocusedRowHandle, "idemp").ToString();
            frm.ShowDialog();
            try
            {
                if (gv_employees_C.FocusedRowHandle >= 0)
                {
                    loadCus();
                    passData(true);
                }
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi :" + ex.Message);
            }
        }

        private void getValueEmp(string val)
        {
            if (val != "")
            {
                gv_employees_C.SetFocusedRowModified();
            }
        }

        private void frm_ShareCus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                bbi_allow_insert.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F9)
                {
                    btn_exit_S.PerformClick();
                }
            }
        }

        private void chk_checkall_S_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_checkall_S.Checked == true)
            {
                for (int i = 0; i < gv_customer_C.RowCount; i++)
                {
                    gv_customer_C.SetRowCellValue(i, "check", true);
                }
            }
            else
            {
                for (int i = 0; i < gv_customer_C.RowCount; i++)
                {
                    gv_customer_C.SetRowCellValue(i, "check", false);
                }
            }
        }

    }
}