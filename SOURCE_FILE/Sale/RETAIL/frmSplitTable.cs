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
using DevExpress.XtraGrid;

namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frmSplitTable : DevExpress.XtraEditors.XtraForm
    {
        public frmSplitTable()
        {
            InitializeComponent();
        }

        #region Var

        public string idtablechoose = "";
        public string idtablemove = "";
        public string nametablechoose = "";
        public string nametablemove = "";
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void PassDataDT(DataTable dtvalue);
        public PassDataDT passDataDT;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        #endregion
        
        #region FormEvent

        private void frmSplitTable_Load(object sender, EventArgs e)
        {
            Init();         
        }

        #endregion

        #region ButtonEvent

        private void btn_right_S_Click(object sender, EventArgs e)
        {
            move(gv_menu_C, gv_right_C, gct_menu_C, gct_right_C);
        }

        private void btn_left_S_Click(object sender, EventArgs e)
        {
            move(gv_right_C, gv_menu_C,  gct_right_C,gct_menu_C);
        }

        private void btn_agree_S_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void btn_cancel_S_Click(object sender, EventArgs e)
        {
            passData(false);
            this.Close();
        }

        private void btn_refresh_S_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        private int checkExitsIdcommodity(GridView gv, string idcommodtity)
        {
            int index = -1;
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (gv.GetRowCellValue(i, "idcommodity").ToString() == idcommodtity)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void OK()
        {
            try
            {
                if (gv_right_C.DataRowCount>0)
                {
                    string idexport = "";
                    idexport = getInvoiceByIDTable(idtablechoose);
                    double quantity = 0;
                    for (int i = 0; i < gv_right_C.RowCount; i++)
                    {
                        quantity = Convert.ToDouble(APCoreProcess.APCoreProcess.Read("select quantity from exportdetail where idexport='" + idexport + "' and idcommodity='" + gv_right_C.GetRowCellValue(i, "idcommodity").ToString() + "'").Rows[0][0]);
                        if (quantity == Convert.ToDouble(gv_right_C.GetRowCellValue(i, "quantity").ToString()))
                            APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport='" + idexport + "' and idcommodity='" + gv_right_C.GetRowCellValue(i, "idcommodity").ToString() + "' ");
                        else
                            APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set quantity=" + (quantity - Convert.ToDouble(gv_right_C.GetRowCellValue(i, "quantity").ToString())) + ", strquantity=convert(nvarchar(20), " + (quantity - Convert.ToDouble(gv_right_C.GetRowCellValue(i, "quantity").ToString())) + ") where  idexport='" + idexport + "' and idcommodity='" + gv_right_C.GetRowCellValue(i, "idcommodity").ToString() + "'");
                    }
                    passData(true);
                    passDataDT((DataTable)gct_right_C.DataSource);
                }
                APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where quantity=0");
                this.Close();
            }
            catch { }
        }

        private void move(GridView gvchoose, GridView gvmove, GridControl gctchoose, GridControl gctmove)
        {
            try
            {
                if (gvchoose.DataRowCount > 0)
                {
                    int index = -1;
                    index = checkExitsIdcommodity(gvmove, gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "idcommodity").ToString());
                    if (index != -1) // đã có cập nhật số lượng gvmove và update lại số lương gvchoose
                    {
                        if (Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text) >= 0) // số lượng đủ cung ứng
                        {
                            gvmove.SetRowCellValue(index, "strquantity", Convert.ToInt16(gvmove.GetRowCellValue(index, "quantity").ToString()) + Convert.ToInt16(txt_quantity_S.Text));
                            gvmove.SetRowCellValue(index, "quantity", Convert.ToInt16(gvmove.GetRowCellValue(index, "quantity").ToString()) + Convert.ToInt16(txt_quantity_S.Text));
                     
                            if (Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text) > 0)
                            {
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "strquantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "quantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));
                            }
                            else
                            {
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "quantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));
                                gvchoose.DeleteRow(gvchoose.FocusedRowHandle);
                            }
                        }
                        else// số lượng không đủ không cho phép thực hiện
                        {
                            Function.clsFunction.MessageInfo("Thông báo", "Số lượng không đủ");
                        }
                    }
                    else// chưa có
                    {
                        if (Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text) >= 0) // số lượng đủ cung ứng
                        {
                            if (Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text) > 0)
                            {
                                DataTable dt = new DataTable();
                                dt = (DataTable)gctmove.DataSource;
                                DataRow dr;
                                dr = dt.NewRow();
                                dr["idcommodity"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "idcommodity").ToString();
                                dr["strquantity"] = Convert.ToInt16(txt_quantity_S.Text);
                                dr["quantity"] = Convert.ToInt16(txt_quantity_S.Text);
                                dr["price"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "price").ToString();
                                dr["total"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "total").ToString();
                                dr["iddetail"] = Function.clsFunction.layMa("PX", "idexport", "export").ToString() + gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "idcommodity").ToString();
                                dt.Rows.Add(dr);
                                dt.Columns["total"].Expression = "quantity*price";
                                gctmove.DataSource = dt;
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "strquantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "quantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));

                            }
                            else
                            {
                                DataTable dt = new DataTable();
                                dt = (DataTable)gctmove.DataSource;
                                DataRow dr;
                                dr = dt.NewRow();
                                dr["idcommodity"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "idcommodity").ToString();
                                dr["strquantity"] = Convert.ToInt16(txt_quantity_S.Text);
                                dr["quantity"] = Convert.ToInt16(txt_quantity_S.Text);
                                dr["price"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "price").ToString();
                                dr["total"] = gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "total").ToString();
                                dr["iddetail"] = Function.clsFunction.layMa("PX", "idexport", "export").ToString() + gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "idcommodity").ToString();
                                dt.Rows.Add(dr);                             
                                gctmove.DataSource = dt;
                                gvchoose.SetRowCellValue(gvchoose.FocusedRowHandle, "quantity", Convert.ToInt16(gvchoose.GetRowCellValue(gvchoose.FocusedRowHandle, "quantity").ToString()) - Convert.ToInt16(txt_quantity_S.Text));
                                gvchoose.DeleteRow(gvchoose.FocusedRowHandle);
                            }
                        }
                        else// số lượng không đủ không cho phép thực hiện
                        {
                            Function.clsFunction.MessageInfo("Thông báo", "Số lượng không đủ");
                        }
                    }
                }
                gctmove.Refresh();
                gctchoose.Refresh();                
                gctmove.Focus();
                gctchoose.Focus();
                gctchoose.DataSource = (DataTable)gctchoose.DataSource;
                gctmove.DataSource = (DataTable)gctmove.DataSource;
                btn_refresh_S.PerformClick();
            }
            catch { }
        }

        private void Init()
        {
            lbl_tablechoose_S.Text = nametablechoose;
            lbl_tablemove_S.Text = nametablemove;
            Function.clsFunction.TranslateForm(this, this.Name);
            loadInfoInvoice(getInvoiceByIDTable(idtablechoose), (GridControl)gct_menu_C);
            loadInfoInvoiceGvMove(getInvoiceByIDTable(idtablemove), (GridControl)gct_right_C);
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_menu_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_right_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
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

        private void loadInfoInvoice(string idexport, GridControl gct)
        {
            try
            {
                DataTable dtM = new DataTable();
                dtM = APCoreProcess.APCoreProcess.Read("select * from export where idexport='" + idexport + "' and cancel  =0 and complet = 0");
                if (dtM.Rows.Count > 0)
                {                                    
                    // load detail
                    DataTable dtD = new DataTable();
                    dtD = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + idexport + "'");
                    //dtD.Columns.Add("total", typeof(System.Double));
                    dtD.Columns["total"].Expression = "quantity*price";
                    if (dtD.Rows.Count > 0)
                    {
                        gct.DataSource = dtD;                      
                    }
                    else
                    {
                        gct.DataSource = null;

                    }
                }
                else
                {       
                    gct.DataSource = null;
                }
            }
            catch { }
        }

        private void loadInfoInvoiceGvMove(string idexport, GridControl gct)
        {
            try
            {
           
                    DataTable dtD = new DataTable();
                    dtD = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + idexport + "'");
                    //dtD.Columns.Add("total", typeof(System.Double));
                    dtD.Columns["total"].Expression = "quantity*price";
                    //if (dtD.Rows.Count > 0)
                    {
                        gct.DataSource = dtD;
                    }
               
                
            }
            catch { }
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

        #endregion

        #region Event

        private void txt_quantity_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(txt_quantity_S.Text) < 1)
            {
                txt_quantity_S.Text = "1";
            }
        }

        #endregion  

 

    }
}