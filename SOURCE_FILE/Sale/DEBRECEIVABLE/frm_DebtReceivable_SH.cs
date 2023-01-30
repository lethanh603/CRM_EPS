//Thanh : 20131305
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Reflection;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Ribbon;

using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;
using Function;

namespace DEBTRECEIVABLE.Presentation
{
    public partial class frm_DebtReceivable_SH : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DebtReceivable_SH()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        public string langues = "_VI";
        public bool statusForm = false;
        public string dauma = "PT";
        private int row_focus = -1;
        public int img_logo_height = 160;
        public int img_logo_width = 160;
        public delegate void PassData(bool value);
        public PassData passData;
        public bool them = false;
        private string arrCaption;
        private string arrFieldName;
        private bool SubMenuClick = false;
        PopupMenu menu = new PopupMenu();
        System.Data.DataTable dtChange = new System.Data.DataTable();
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        public void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {
                //Load_Grid();    
                FillGrid_Master();
                if (row_focus != -1)
                    gv_grid_C.FocusedRowHandle = row_focus;
                else
                    gv_grid_C.FocusedRowHandle = gv_grid_C.RowCount - 1;
                row_focus = -1;

            }
        }
        #endregion

        #region FormEvent

        private void frm_nhapdvt_SH_Load(object sender, EventArgs e)
        {
            bbi_fromdate_S.EditValue = DateTime.Now;
            bbi_todate_S.EditValue = DateTime.Now;
            langues = Function.clsFunction.langgues;
            //statusForm = true;
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                SaveGridControlMaster();
                SaveGridControlDetail();
            }
            else
            {
                bbi_checkall_s.Checked = true;     
                bbi_checkall_s.PerformClick();
                Function.clsFunction.Text_Control(this, langues);
                FillGrid_Master();
                Load_GridMaster();
                Load_GridDetail();
                ((GridView)gctr_grid_C.ViewCollection[1]).OptionsView.ShowGroupPanel = false;
                DevExpress.XtraGrid.GridLevelNode gridLevelNodeDetail = new DevExpress.XtraGrid.GridLevelNode();
                gridLevelNodeDetail.LevelTemplate = ((GridView)gctr_grid_C.ViewCollection[1]);
                gridLevelNodeDetail.RelationName = "Chi Tiết";
                gctr_grid_C.LevelTree.Nodes.Add(gridLevelNodeDetail);
                //this.Text = dt_textForm.Rows[0]["text_form" + langues].ToString();
                gv_grid_C.OptionsView.ShowAutoFilterRow = true;
                bbi_fromdate_S.Edit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                bbi_fromdate_S.Edit.EditFormat.FormatString =clsFunction.dateFormat;
                bbi_todate_S.Edit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                bbi_todate_S.Edit.EditFormat.FormatString = clsFunction.dateFormat;
                summaryGroup();
                ((GridView)gctr_grid_C.ViewCollection[1]).MouseUp += showSubMenu;
            }
        }

        public DataSet MasterDetail()
        {
            DataSet dts = new DataSet();
            DataTable dtPN = new DataTable();
            dtPN = APCoreProcess.APCoreProcess.Read("SELECT     ex.idexport, ex.IDEMP AS idemp, ex.idcustomer, ex.dateimport, ex.note,"
            +" SUM((dt.quantity * dt.price) * (1 + dt.vat / 100)) AS amount, SUM(ISNULL(pt.moneydebt, 0)) "
            +" AS amountdebt, SUM(ISNULL((dt.quantity * dt.price) * (1 + dt.vat / 100) - ISNULL(pt.moneydebt, 0), 0)) AS rest, ISNULL(ex.rate, 0) AS rate, ex.limitdept, "
            + " CONVERT( bit, CASE WHEN (DATEDIFF(day, ex.limitdept, GETDATE())) > 0 THEN 'true' ELSE 'false' END) AS overdue, DATEDIFF(day, ex.limitdept, GETDATE()) AS numdate," 
            +" ISNULL((ex.rate / 100) * (ISNULL((dt.quantity * dt.price) * (1 + dt.vat / 100) - pt.moneydebt, 0) / 30) * DATEDIFF(day, ex.limitdept, GETDATE()), 0) AS amountrate"
            +" FROM         dbo.EXPORT AS ex INNER JOIN  dbo.EXPORTDETAIL AS dt ON ex.idexport = dt.idexport LEFT OUTER JOIN"
            +" dbo.DEBTRECEIVABLE AS pt ON ex.idexport = pt.idexport"
            +" GROUP BY ex.idexport, ex.IDEMP, ex.idcustomer, ex.dateimport, ex.note, ISNULL(ex.rate, 0), CAST(DATEDIFF(d, 0, ex.dateimport) AS datetime), ex.limitdept, "
            +" CASE WHEN (DATEDIFF(day, ex.limitdept, GETDATE())) > 0 THEN 1 ELSE 0 END, DATEDIFF(day, ex.limitdept, GETDATE()), ex.rate, ISNULL((ex.rate / 100) "
            +" * (ISNULL((dt.quantity * dt.price) * (1 + dt.vat / 100) - pt.moneydebt, 0) / 30) * DATEDIFF(day, ex.limitdept, GETDATE()), 0) HAVING     ( (CAST(DATEDIFF(d, 0, ex.dateimport) AS datetime) "
            + " BETWEEN CONVERT(datetime, '" + Convert.ToDateTime(bbi_fromdate_S.EditValue).ToString("yyyyMMdd") + "', 103) AND CONVERT(datetime, '" + Convert.ToDateTime(bbi_todate_S.EditValue).ToString("yyyyMMdd") + "', 103)) or '"+ bbi_checkall_s.Checked +"'='true' ) AND "
            +" (SUM((dt.quantity * dt.price) * (1 + dt.vat / 100) - ISNULL(pt.moneydebt, 0)) > 0)");

            DataTable dtDetail = new DataTable();
            dtDetail = APCoreProcess.APCoreProcess.Read("SELECT     pt.iddebtreceivable, pt.datedebt, pt.idexport, ISNULL(pt.moneydebt, 0) AS moneydebt,"
            + " ISNULL(pt.reason, 0) AS reason, ISNULL(pt.amountrate, 0) AS amountrate, "
            + " pt.statusdel, pt.statusreceivable, emp.StaffName AS staffname FROM         dbo.DEBTRECEIVABLE AS pt INNER JOIN "
            + " dbo.EMPLOYEES AS emp ON pt.idemp = emp.IDEMP WHERE     (pt.statusdel = 0) AND (pt.idexport IN"
            + " (SELECT     ex.idexport FROM          dbo.EXPORT AS ex INNER JOIN  dbo.EXPORTDETAIL AS dt ON ex.idexport = dt.idexport LEFT OUTER JOIN"
            + " dbo.DEBTRECEIVABLE AS pt ON ex.idexport = pt.idexport"
            + " WHERE      ((dt.quantity * dt.price) * (1 + dt.vat / 100) - ISNULL(pt.moneydebt, 0) > 0) AND (CAST(DATEDIFF(d, 0, ex.dateimport) AS datetime) BETWEEN "
            + " CONVERT(datetime, '" + Convert.ToDateTime(bbi_fromdate_S.EditValue).ToString("yyyyMMdd") + "', 103) AND CONVERT(datetime, '" + Convert.ToDateTime(bbi_todate_S.EditValue).ToString("yyyyMMdd") + "', 103))))  ");
            dts.Tables.Add(dtPN);
            dts.Tables.Add(dtDetail);
            dts.Relations.Add(new DataRelation(("Chi Tiết"), dtPN.Columns["idexport"], dtDetail.Columns["idexport"]));
            return dts;
        }

        private void CustomRepositoryItemGridLookUpEdit()
        {
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.OptionsView.ShowAutoFilterRow = true;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).PopupFormSize = new Size(700, 300);
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[1].Width = 100;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[2].Width = 200;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[3].Width = 100;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[4].Width = 100;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[5].Width = 100;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[0].ColumnEdit).View.Columns[6].Width = 100;
        }

        private void CustomRepositoryItemGridLookUpEditDVT()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rpcolum = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();

            rpcolum.DataSource = APCoreProcess.APCoreProcess.Read("dmunit where status=1");
            rpcolum.PopupFormSize = new Size(250, 300);
            rpcolum.DisplayMember = "unit";
            rpcolum.ValueMember = "idunit";
            GridColumn gc = new GridColumn();
            gc.Caption = "Mã DVT";
            gc.FieldName = "idunit";
            gc.VisibleIndex = 0;
            rpcolum.View.Columns.Add(gc);
            GridColumn gc1 = new GridColumn();
            gc1.Caption = "Tên DVT";
            gc1.FieldName = "unit";
            gc1.VisibleIndex = 1;
            rpcolum.View.Columns.Add(gc1);
            rpcolum.View.Columns[0].Width = 100;
            rpcolum.View.Columns[1].Width = 150;
            rpcolum.View.Columns[0].Caption = "Mã DVT";
            rpcolum.View.Columns[1].Caption = "Tên DVT";
            gv_grid_C.Columns[2].ColumnEdit = rpcolum;
            ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_grid_C.Columns[2].ColumnEdit).View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
        }

        #endregion

        #region Event

        private void bbi_checkall_s_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (bbi_checkall_s.Checked == true)
            {
                bbi_fromdate_S.Enabled = false;
                bbi_todate_S.Enabled = false;
            }
            else
            {
                bbi_fromdate_S.Enabled = true;
                bbi_todate_S.Enabled = true;
            }
        }

        #endregion

        #region ButtonEvent

        private bool checkGridviewHaschange()
        {
            DataTable changes = dtChange.GetChanges(DataRowState.Added | DataRowState.Modified);
            bool isModified = (changes != null);
            return isModified;
        }


        private void bbi_thoat_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void bbi_thoat_S_Click(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Function.clsFunction.sotap--;
        }

        private void btn_Click(object sender, ItemClickEventArgs e)
        {

            BarSubItem subMenu = e.Item as BarSubItem;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     field_name, type, form_name FROM         dbo.sysControls WHERE     (type = N'BarButtonItem') AND (form_name = N'" + this.Name + "')");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (subMenu.Name.Contains(dt.Rows[i]["field_name"].ToString().Trim()))
                {

                    if (subMenu.Name.Contains("thoat"))
                    {
                        bbi_thoat_S_ItemClick(sender, e);
                    }
                }
            }
        }

        private void bbi_xoa_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            gv_grid_C.DeleteRow(gv_grid_C.FocusedRowHandle);
        }
        private void bbi_lapphieu_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frm_DeptReceivable_S frm = new frm_DeptReceivable_S();
                //frm.lbl_iddebtreceivable_IK1.Text = gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "idexport").ToString();
                //frm.them = false;
                //frm.dauma = dauma;
                //frm.passData = new frm_DebtReceivable_S1.PassData(GetValue);
                //frm.ngay = Convert.ToDateTime(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "dateimport").ToString());
                //frm.conno = Convert.ToDouble(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "rest"));
                //frm.sotien = Convert.ToDouble(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "amount"));
                //frm.tienlai = Convert.ToDouble(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "amountrate"));
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch
            {
            }
        }

        private void bbi_xem_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(bbi_todate_S.EditValue) >= Convert.ToDateTime(bbi_fromdate_S.EditValue))
                {
                    gctr_grid_C.DataSource = MasterDetail().Tables[0];
                }
                else
                    clsFunction.MessageInfo("Thông báo", "Lỗi chọn sai giá trị ngày");
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }


        #endregion

        #region GridEvent


        private void gv_danhmuc_C_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frm_DeptReceivable_S frm = new frm_DeptReceivable_S();
                frm.lbl_iddebtreceivable_IK1.Text = gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "idexport").ToString();
                frm.dauma = dauma;
                frm.amountrate = 0;

                frm.passData = new frm_DeptReceivable_S.PassData(GetValue);
                frm.datedebt = Convert.ToDateTime(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "dateimport").ToString());
                frm.rest = Convert.ToDecimal(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "rest"));
                frm.amount = Convert.ToDecimal(gv_grid_C.GetRowCellValue(gv_grid_C.FocusedRowHandle, "amount"));
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch { }
        }
        private void gv_danhmuc_C_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void gv_danhmuc_C_MouseUp(object sender, MouseEventArgs e)
        {
            if (gv_grid_C.FocusedRowHandle >= 0 && SubMenuClick==false)
            {       
                try
                {
                    menu.ItemLinks.Clear();
                    bool flag = false;
                    if (gv_grid_C.FocusedRowHandle >= 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }

                    if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                    {
                        clsFunction.customPopupMenu(bar_menu_C, menu, gv_grid_C, this);
                        menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress);
                        menu.ShowPopup(MousePosition);
                    }
                }
                catch
                { }
            }
            SubMenuClick = false;
        }

        private void showSubMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)          
            try
            {
                SubMenuClick = true;
                createSubMenu();
            }
            catch
            { }

        }

        private void gv_danhmuc_C_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gctr_danhmuc_C_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the end-user has right clicked the grid control. 
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gv_grid_C.CalcHitInfo(new Point(e.X, e.Y)));
        }

        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            // Create the menu. 
            DevExpress.XtraGrid.Menu.GridViewMenu menu = null;
            // Check whether the header panel button has been clicked. 
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton)
            {

                // menu = new GridViewColumnButtonMenu(gv_danhmuc_C);
                menu.Init(hi);
                // Display the menu. 
                menu.Show(hi.HitPoint);
            }
        }

        private void gv_danhmuc_C_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                menu.Items.Clear();
                if (menu.Column != null)
                {
                    menu.Items.Add(CreateCheckItem("Can Moved", menu.Column, null));
                }
            }
        }

        DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, Image image)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption,
              column.OptionsColumn.AllowMove, image, new EventHandler(OnCanMovedItemClick));
            item.Tag = new MenuColumnInfo(column);
            return item;
        }
        void OnCanMovedItemClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            MenuColumnInfo info = item.Tag as MenuColumnInfo;
            if (info == null) return;
            info.Column.OptionsColumn.AllowMove = item.Checked;
        }

        // The class that stores menu specific information.
        class MenuColumnInfo
        {
            public MenuColumnInfo(GridColumn column)
            {
                this.Column = column;
            }
            public GridColumn Column;
        }


        private void gv_danhmuc_C_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DataRow row = view.GetDataRow(e.RowHandle);
            }
            catch (Exception) { }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            //e.Handled = true;
            //
            GridCellInfo cell = e.Cell as GridCellInfo;

            ObjectPainter p = cell.RowInfo.ViewInfo.Painter.ElementsPainter.DetailButton;

            if (!cell.CellButtonRect.IsEmpty)
            {

                ObjectPainter.DrawObject(e.Cache, p,

                    new DevExpress.XtraGrid.Drawing.DetailButtonObjectInfoArgs(cell.CellButtonRect,

                        view.GetMasterRowExpanded(cell.RowHandle), cell.RowInfo.IsMasterRowEmpty));
            }


            e.Handled = true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                e.Info.DisplayText = clsFunction.transLateText("STT");
            }

            e.Painter.DrawObject(e.Info);
            clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
            e.Handled = true;
        }


        #endregion

        #region Methods

        private void FillGrid_Master()
        {
            gctr_grid_C.DataSource = MasterDetail();
            gctr_grid_C.DataMember = MasterDetail().Tables[0].TableName;
            gctr_grid_C.ForceInitialize();
        }

        private void Xuat()
        {
            bool status = false;
            saveFileDialog1.Filter = "Excel 97 - 2003 (*.xls)|*.xls |Excel 2007(*.xlsx)|*.xlsx |Pdf (*.pdf)|*.pdf |Webpage (*.html)|*.html |Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog1.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog1.FileName);
                    if (f.Extension.Equals(".xls"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file") == true)
                        {
                            gv_grid_C.ExportToXls(f.FullName);
                            Process.Start(f.FullName);
                        }
                        status = true;
                    }
                    else
                    {
                        if (status == true)
                            DevExpress.XtraEditors.XtraMessageBox.Show("Invalid file type");
                    }
                    status = false;
                    if (f.Extension.Equals(".xlsx"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file") == true)
                        {
                            gv_grid_C.ExportToXlsx(f.FullName);
                            status = true;
                            Process.Start(f.FullName);
                        }

                    }
                    else
                    {
                        if (status == true)
                            DevExpress.XtraEditors.XtraMessageBox.Show("Invalid file type");
                    }
                    status = false;
                    if (f.Extension.Equals(".pdf"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file") == true)
                        {
                            gv_grid_C.ExportToPdf(f.FullName);

                            status = true;
                            Process.Start(f.FullName);
                            //report.ShowPreviewDialog();
                        }
                    }
                    else
                    {
                        if (status == true)
                            DevExpress.XtraEditors.XtraMessageBox.Show("Invalid file type");
                    }
                    status = false;
                    if (f.Extension.Equals(".rtf"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file") == true)
                        {
                            gv_grid_C.ExportToRtf(f.FullName);
                            status = true;
                            Process.Start(f.FullName);
                        }
                    }
                    else
                    {
                        if (status == true)
                            DevExpress.XtraEditors.XtraMessageBox.Show("Invalid file type");
                    }
                    status = false;
                    if (f.Extension.Equals(".html"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file") == true)
                        {
                            gv_grid_C.ExportToHtml(f.FullName);
                            status = true;
                            Process.Start(f.FullName);
                        }
                    }
                    else
                    {
                        if (status == true)
                            DevExpress.XtraEditors.XtraMessageBox.Show("Invalid file type");
                    }
                    status = false;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("You did pick a location " + "to save file to");
                }
            }
        }

        private void createSubMenu()
        {
          
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            Bitmap image;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();                     

            contextMenuStrip.Items.Add(clsFunction.transLateText("Sửa phiếu"));
            image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\Table\\bbi_sua_S.png");
            image.MakeTransparent(Color.Fuchsia);
            contextMenuStrip.Items[0].Name = "Edit";
            contextMenuStrip.Items[0].Image = image;

            contextMenuStrip.Items.Add(clsFunction.transLateText("Xóa phiếu"));
            image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Images\\Table\\bbi_xoa_S.png");
            image.MakeTransparent(Color.Fuchsia);
            contextMenuStrip.Items[1].Name = "Delete";
            contextMenuStrip.Items[1].Image = image;
          
            contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
            contextMenuStrip.Show(this, PointToClient(MousePosition));
            gctr_grid_C.ContextMenuStrip = contextMenuStrip;
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            gctr_grid_C.ContextMenuStrip = null;
            if (e.ClickedItem.Name.Contains("Delete"))
            {
                MessageBox.Show("Delete");
            }
            if (e.ClickedItem.Name.Contains("Edit"))
            {
                MessageBox.Show("Edit");
            }
        }

        private bool KiemTraQuyen(string control_name, DataRow dr)
        {
            bool flag = false;
            if (control_name.Contains("_in") == true)
            {
                if ((bool)dr["allow_print"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_luu") == true)
            {
                if ((bool)dr["allow_edit"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_xem") == true)
            {
                if ((bool)dr["allow_insert"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_xoa") == true)
            {
                if ((bool)dr["allow_delete"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_sua") == true)
            {
                if ((bool)dr["allow_edit"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_nhap") == true)
            {
                if ((bool)dr["allow_import"] == true)
                {
                    flag = true;
                }
            }
            if (control_name.Contains("_xuat") == true)
            {
                if ((bool)dr["allow_export"] == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        private void summaryGroup()
        {
            gv_grid_C.OptionsView.ShowFooter = true;
            gv_grid_C.Columns["amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv_grid_C.Columns["amount"].SummaryItem.FieldName = "amount";
            gv_grid_C.Columns["amount"].SummaryItem.DisplayFormat = "{0:N0}";
            

            gv_grid_C.OptionsView.ShowFooter = true;
            gv_grid_C.Columns["amountdebt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv_grid_C.Columns["amountdebt"].SummaryItem.FieldName = "amountdebt";
            gv_grid_C.Columns["amountdebt"].SummaryItem.DisplayFormat = "{0:N0}";

            gv_grid_C.OptionsView.ShowFooter = true;
            gv_grid_C.Columns["rest"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv_grid_C.Columns["rest"].SummaryItem.FieldName = "rest";
            gv_grid_C.Columns["rest"].SummaryItem.DisplayFormat = "{0:N0}";

            gv_grid_C.OptionsView.ShowFooter = true;
            gv_grid_C.Columns["amountrate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv_grid_C.Columns["amountrate"].SummaryItem.FieldName = "amountrate";
            gv_grid_C.Columns["amountrate"].SummaryItem.DisplayFormat = "{0:N0}";

    
        }

        private void customPopupMenu()
        {
            // Bind the menu to a bar manager.
            menu.Manager = bar_menu_C;
            // Add two items that belong to the bar manager.
            for (int i = 0; i < bar_menu_C.Items.Count; i++)
            {
                if (!bar_menu_C.Items[i].Name.Contains("_S"))
                {
                    bar_menu_C.Items[i].Caption = clsFunction.transLateText(bar_menu_C.Items[i].Caption);
                    bar_menu_C.Items.Add(bar_menu_C.Items[i]);
                }
            }
        }

        private void Manager_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='"+ gv_grid_C.Name +"'");
                if (dt.Rows.Count > 0)
                {
                    strColName = dt.Rows[0][0].ToString();
                    strCol = dt.Rows[0][1].ToString();
                    string[] arrayColName = strColName.Split('/');
                    string[] arrCol = strCol.Split('/');
                    if (e.Item.Name.Contains("_allow_col_") && (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos = findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (pos == -1)
                            return;
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]) )
                        {
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible = clsFunction.ConvertArrayToString(arrCol);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='"+ gv_grid_C.Name +"'");
                            DataTable dtC = new DataTable();
                            dtC = APCoreProcess.APCoreProcess.Read("select column_visible from sysGridcolumns where form_name='" + this.Name + "' and grid_name='" + gv_grid_C.Name + "'");
                            if (dtC.Rows.Count > 0)
                            {
                                string[] arr = dtC.Rows[0][0].ToString().Split('/');
                                for (int i = 0; i < arr.Length-1; i++)
                                {
                                    gv_grid_C.Columns[i].Visible = Convert.ToBoolean(arr[i]);
                                }
                            }
                        }
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private int findIndexInArray(string value, string[] arr)
        {
            int index = -1;
            for (int i = 0; i < arr.Length; i++)
                if (value == arr[i])
                {
                    index = i;
                    break;
                }
            return index;
        }

        #region GridContructor

        private void SaveGridControlMaster()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã Phiếu Xuất", "Khách Hàng", "Ngày Xuất", "Tổng Số Tiền ", "Đã Trả", "Còn Lại", "Hạn TT", "Trễ hạn", "Quá hạn", "Lãi Suất", "Tiền Lãi", "Nhân viên", "Ghi Chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "idexport", "idcustomer", "dateimport", "amount", "amountdebt", "rest", "limitdebt", "overdue", "numdate", "rate", "amountrate", "idemp", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "DateColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "DateColumn", "CheckColumn", "CalcEditColumn", "Percent2", "CalcEditColumn", "GridLookupEditColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "80", "80", "80", "80", "80", "100", "100", "100", "80", "150", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "true", "true", "true", "true", "true", "true", "true", "true", "true", "true", "true", "true", "true" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idcustomer, customer  from dmcustomers where status=1", "select idemp, staffname  from employees where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "customer", "staffname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcustomer", "idemp" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "Mã KH", "Tên KH" }, { "Mã NV", "Nhân viên" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "idcustomer", "customer" }, { "idemp", "staffname" } };

            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_grid_C.Name);
        }

        private void SaveGridControlDetail()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Mã PT", "Ngày thu", "Số tiền", "Tiền lãi", "Nhân viên", "Ghi chú" };

            // FieldName column
            string[] fieldname_col = new string[] { "iddebtreceivable", "datedebt", "moneydebt", "amountrate", "staffname", "reason" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "DateColumn", "CalcEditColumn", "CalcEditColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "100", "100", "200", "250" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "true", "true", "true", "true", "true", "true" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0];
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0];

            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, ((GridView)gctr_grid_C.ViewCollection[1]).Name);
        }

        private void Load_GridMaster()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_grid_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_grid_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_grid_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gctr_grid_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_GridDetail()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_grid_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + ((GridView)gctr_grid_C.ViewCollection[1]).Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid((GridView)gctr_grid_C.ViewCollection[1], read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gctr_grid_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                ((GridView)gctr_grid_C.ViewCollection[1]).IndicatorWidth = 35;
                ((GridView)gctr_grid_C.ViewCollection[1]).CustomDrawCell += new RowCellCustomDrawEventHandler(gridView1_CustomDrawCell);//gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e);
                ((GridView)gctr_grid_C.ViewCollection[1]).CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator);//gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #endregion

    }
}