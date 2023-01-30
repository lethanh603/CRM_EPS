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

namespace SOURCE_FORM_DMCOMMODITY.Presentation
{
    public partial class frm_COMMODITYTOOL_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_COMMODITYTOOL_S()
        {
            InitializeComponent();
        }

        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        private void frm_COMMODITYTOOL_S_Load(object sender, EventArgs e)
        {
            loadCommodityDuplicate(false);
        }

        private void loadCommodityDuplicate(Boolean show)
        {
            DataTable dt = new DataTable();
            if (!show)
            {
                dt = APCoreProcess.APCoreProcess.Read("SELECT        commodity, COUNT(idcommodity) AS count_commodity FROM DMCOMMODITY GROUP BY commodity, status HAVING  (COUNT(idcommodity) > 1) AND (status = 1)");
            }
            else 
            {
                dt = APCoreProcess.APCoreProcess.Read("SELECT        commodity, COUNT(idcommodity) AS count_commodity FROM DMCOMMODITY GROUP BY commodity, status HAVING   (status = 1)");
            }
            gct_list_C.DataSource = dt;
            
        }

        private void detailCommodity(string name)
        {
            string sql = "SELECT        idcommodity, commodity FROM DMCOMMODITY WHERE        (status = 1) AND (commodity = N'"+ name +"')";
            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            gct_list_duplicate_C.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                txt_mahang_S.Text = gv_list_duplicate_C.GetRowCellValue(0, "idcommodity").ToString();
            }
        }

        private void gopMaHangTrung(string id, string name)
        {
            try
            {
                if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn gộp các mã hàng trùng về mã hàng " + id))
                {
                    string sqlEx = "SELECT        idcommodity FROM DMCOMMODITY WHERE        (status = 1) AND (commodity = N'" + name + "') and idcommodity <> '" + id + "'";
                    string sql = "update dmcommodity set status = 0 where idcommodity in (" + sqlEx + ")";
                    APCoreProcess.APCoreProcess.ExcuteSQL(sql);
                    Function.clsFunction.MessageInfo("Thông báo", "Gộp mã thành công");
                    loadCommodityDuplicate(chk_showall_S.Checked);
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Gộp mã thất bại: " + ex.Message);
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

        private void gv_list_C_Click(object sender, EventArgs e)
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                detailCommodity(gv_list_C.GetRowCellDisplayText(gv_list_C.FocusedRowHandle, "commodity"));
            }
        }

        private void gv_list_duplicate_C_Click(object sender, EventArgs e)
        {
            if (gv_list_duplicate_C.FocusedRowHandle >= 0)
            {
                txt_mahang_S.Text = gv_list_duplicate_C.GetRowCellValue(gv_list_duplicate_C.FocusedRowHandle, "idcommodity").ToString();
            }
        }

        private void btn_gopma_S_Click(object sender, EventArgs e)
        {
            if (txt_mahang_S.Text != "" && gv_list_C.GetRowCellDisplayText(gv_list_C.FocusedRowHandle, "commodity") != null)
            {
                gopMaHangTrung(txt_mahang_S.Text, gv_list_C.GetRowCellDisplayText(gv_list_C.FocusedRowHandle, "commodity"));

            }
        }

        private void chk_showall_S_CheckedChanged(object sender, EventArgs e)
        {
            loadCommodityDuplicate(chk_showall_S.Checked);
        }

    }
}