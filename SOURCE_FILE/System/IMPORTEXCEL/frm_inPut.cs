using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Function;
using System.Reflection;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;


namespace IMPORTEXCEL
{
    public partial class frm_inPut_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_inPut_S()
        {
            InitializeComponent();
        }

        #region  Var

        public bool autocode = false;
        public string tbName = "nhapduan",sDauma="DA";
        bool checkAllow = false;
        private DataTable dtExCel = new DataTable();
        public string[] arrColumnCaption;
        public string[] arrColumnFieldName;
        private DevExpress.Utils.WaitDialogForm waitDialog = null;
        ProgressBarControl progressBarControl1 = new ProgressBarControl();
        public string gridNamePre = "";
        public string formNamePre = "";
        DataTable dtConfig = new DataTable();
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        int countE = 0;
        int countS = 0;
        string colCompair = "";
        public bool statusForm = false;
        private int step = -1;

        #endregion

        #region  Form_Event

        private void frm_inPut_Load(object sender, EventArgs e)
        {
            if (statusForm == true)
            {
                SaveGridControls();
            }
            else
            {
                //lockButton(true);
               
                //setDataGrid();
   
                clsFunction.TranslateForm(this, this.Name);
                clsFunction.TranslateGridColumn(gv_import_C);

                setEnableButton();
            }
        }

        private void ColumnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        #endregion
                
        #region  GridEvent

        private void gv_import_C_Click(object sender, EventArgs e)
        {


        }

        private void spe_index_S_EditValueChanged(object sender, EventArgs e)
        {
            if (spe_index_S.Value < 0)
            {
                spe_index_S.Value = 1;
            }
        }

        private void gv_import_C_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn inStockCol = view.Columns["col_valuedefault"];

            //Get the value of the first column

            //Get the value of the second column

            //Validity criterion

            try
            {
                object inSt = view.GetRowCellValue(e.RowHandle, inStockCol);
                if (inSt != null)
                {
                    inSt = Convert.ChangeType(inSt, System.Type.GetType("System." + view.GetRowCellValue(e.RowHandle, "col_type").ToString()));
                }
            }
            catch
            {
                view.SetColumnError(inStockCol, "The value not default");
                e.Valid = false;
            }
        }

        #endregion

        #region  Methods

        private void lockButton(bool islock)
        {
            btn_browser_S.Enabled = islock;       

            btn_exit_S.Enabled = islock;
            btn_cancel_S.Enabled = !islock;
        }

        private void import()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName,Convert.ToInt16(cbo_version_S.Text));
                        dt = excel.getDataFromExcel();
                        if (dt == null)
                        {
                            return;
                        }
                        int i = 0;
                        i = 0;
                        //xóa các dòng trống của file excel hoặc cột MaHocSinh ko có
                        while (i <= dt.Rows.Count - 1)
                        {
                            if (dt.Rows[i]["tenduan"].ToString() == string.Empty)
                            {
                                dt.Rows.RemoveAt(i);
                                dt.AcceptChanges();
                            }
                            else
                            {
                                i += 1;
                            }
                        }

                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from nhapnhanvien");
                        System.Data.DataTable dtpb = new System.Data.DataTable();
                        dtpb = APCoreProcess.APCoreProcess.Read("nhapduan");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            try
                            {
                                DataRow dr = dtpb.NewRow();
                                dr["maduan"] = clsFunction.layMa("DA", "maduan", "nhapduan");
                                dr["maloaiduan"] = dt.Rows[j]["maloaiduan"].ToString().Trim();
                                //dr = dt.Rows[j];
                                dr["tenduan"] = dt.Rows[j]["tenduan"].ToString().Trim();
                                dr["manhanvien"] = "NVKH000001";
                                dr["ngayketthuc"] = clsFunction.ConVertDatetimeToNumeric(DateTime.Now);
                                dr["ngaybatdau"] = clsFunction.ConVertDatetimeToNumeric(DateTime.Now);
                                dr["status"] = 1;
                                dr["trigia"] = dt.Rows[j]["trigia"].ToString().Trim();
                                dr["dientichthat"] = dt.Rows[j]["dientichthat"].ToString().Trim();
                                dr["sokihieu"] = dt.Rows[j]["sokihieu"].ToString().Trim();
                                dr["coquanquanlyduan"] = "Bất động sản Trần Anh";
                                dr["quymonangluc"] = "Chưa xác định";
                                dr["diadiem"] = dt.Rows[j]["diadiem"].ToString().Trim();
                                dr["muctieudautu"] = "";
                                dr["ghichu"] = dt.Rows[j]["ghichu"];
                                dr["dagiaodich"] = 0;
                                dr["malo"] = dt.Rows[j]["malo"].ToString().Trim();
                                dtpb.Rows.Add(dr);
                                APCoreProcess.APCoreProcess.Save(dr);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        XtraMessageBox.Show("Cập nhật thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void loadFileExcel()
        {            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        if (openFileDialog1.FileName.PadRight(3).ToString() == "xls")
                            cbo_version_S.Text = "2003";
                        else
                            cbo_version_S.Text = "2007";
                        waitDialog = new DevExpress.Utils.WaitDialogForm("Processing data ...", "Please wait");
                        waitDialog.SetCaption("Please wait few second ...");
                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName, Convert.ToInt16(cbo_version_S.Text));
                        dtExCel = excel.getDataFromExcel((int)(spe_index_S.Value-1));
                        txt_input_S.Text = openFileDialog1.FileName;
                        waitDialog.Close();
                        lockButton(false);
                    }
                }
            }
            catch (Exception ex)
            {
                waitDialog.Close();
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void loadCboColumn()
        {
            cbo_checkColumn_S.Properties.Items.Clear();
            for (int i = 0; i <= dtExCel.Columns.Count-1; i++)
            {
                cbo_checkColumn_S.Properties.Items.Add(dtExCel.Rows[0][i].ToString());
            }
            cbo_checkColumn_S.SelectedIndex = 0;
        }

        private void setDataGrid()
        {
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("col_database",typeof(string));
            dtGrid.Columns.Add("col_excel", typeof(string));
            dtGrid.Columns.Add("col_type", typeof(string));
            dtGrid.Columns.Add("col_valuedefault", typeof(string));
            DataTable dtTable = new DataTable();
            dtTable = APCoreProcess.APCoreProcess.Read(tbName);

            for (int i = 0; i < dtTable.Columns.Count; i++)
            {
                DataRow dr = dtGrid.NewRow();
                dr["col_database"] = dtTable.Columns[i].ColumnName;
                dr["col_excel"] =getColExcel( dtTable.Columns[i].ColumnName) ;
                dr["col_type"] = dtTable.Columns[i].DataType.Name;
                dr["col_valuedefault"] =  clsFunction.GetDefault(dtTable.Columns[i].DataType);
                dtGrid.Rows.Add(dr);
            }
            gct_import_C.DataSource = dtGrid;
            //gv_import_C.SetRowCellValue(0, "col_valuedefault", "Boolean");
                
        }

        private object setDaufault(string Datatype)
        {
            object value = null;
            try
            {
                switch (Datatype.ToUpper())
                {
                    case "STRING":
                        value = "";
                        break;
                    case "NUMERIC":
                        value = 0;
                        break;
                    case "BOOLEAN":
                        value = false;
                        break;
                    case "BOOL":
                        value = false;
                        break;
                }
            }
            catch { }
            return value;
        }

        private string getColExcel(string value)
        {
            string str="None";
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_import_C.Columns[1].ColumnEdit).DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["col_excel"].ToString() == ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_import_C.Columns[0].ColumnEdit).GetDisplayText(value))
                    {
                        str = ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_import_C.Columns[0].ColumnEdit).GetDisplayText(value);
                        break;
                    }
                }
            }
            catch
            { }
            return str;
        }

        private void loadLueColumnExcel()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("col_excel", typeof(string));        
            DataRow drNone = dt.NewRow();
            drNone["col_excel"] = "None";
            dt.Rows.Add(drNone);
            DataRow drAuto = dt.NewRow();
            drAuto["col_excel"] = "Auto";
            dt.Rows.Add(drAuto);
            for (int i = 0; i < dtExCel.Columns.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["col_excel"] = dtExCel.Rows[(int)spe_rowstart_S.Value-1][i].ToString();
                dt.Rows.Add(dr);
            }
            resColExcel.DataSource = dt;
            resColExcel.ValueMember = "col_excel";
            resColExcel.DisplayMember = "col_excel";            
        }

        private void loadLueColumnExcel(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit resColExcel)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("col_excel", typeof(string));
            DataRow drNone = dt.NewRow();
            drNone["col_excel"] = "None";
            dt.Rows.Add(drNone);
            DataRow drAuto = dt.NewRow();
            drAuto["col_excel"] = "Auto";
            dt.Rows.Add(drAuto);
            for (int i = 0; i < dtExCel.Columns.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["col_excel"] = dtExCel.Rows[(int)spe_rowstart_S.Value - 1][i].ToString();
                dt.Rows.Add(dr);
            }
            resColExcel.DataSource = dt;
            resColExcel.ValueMember = "col_excel";
            resColExcel.DisplayMember = "col_excel";
        }

        private void loadLueColumnDatabase()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("caption", typeof(string));
            dt.Columns.Add("fieldname", typeof(string));
            for (int i = 0; i < arrColumnFieldName.Length; i++)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr["caption"] = arrColumnCaption[i];
                dr["fieldname"] = arrColumnFieldName[i];
                dt.Rows.Add(dr);
            }
            rescol_database.DataSource = dt;
            rescol_database.ValueMember = "fieldname";
            rescol_database.DisplayMember = "caption";
        }

        private void loadLueColumnDatabase(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rescol_database)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("caption", typeof(string));
            dt.Columns.Add("fieldname", typeof(string));
            for (int i = 0; i < arrColumnFieldName.Length; i++)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr["caption"] = arrColumnCaption[i];
                dr["fieldname"] = arrColumnFieldName[i];
                dt.Rows.Add(dr);
            }
            rescol_database.DataSource = dt;
            rescol_database.ValueMember = "fieldname";
            rescol_database.DisplayMember = "caption";
        }

        private void insert()
        {
            int Mode = -1;
            Mode = Convert.ToInt16(rg_option_S.EditValue);
            switch (Mode)
            {
                case 4:
                saveAndBoth();
                break;
                case 1:
                saveAndReplace();
                break;
                case 2:             
                saveAndSkip();
                break;
                case 3:
                saveAndStop();
                break;
            }   
        }

        private string setValueColumn(string col, DataRow drExcel, int index, int indexCol, string type)
        {           
            string str = "";
            if (col!="None")
            {
                if (col != "Auto")
                {
                    try
                    {
                        Convert.ChangeType(drExcel[col].ToString(), Type.GetType("System."+type));
                        str = drExcel[col].ToString();
                    }
                    catch
                    {
                        str = "NULL";
                    }                    
                }
                else
                {
                    str = clsFunction.autonumber(gv_import_C.GetRowCellValue(indexCol, "col_database").ToString(), tbName).ToString();
                }
            }
            else
            {
                if (checkPrimaryColumn(gv_import_C.GetRowCellValue(indexCol, "col_database").ToString()))
                {
                    str = clsFunction.layMa(sDauma, gv_import_C.GetRowCellValue(indexCol, "col_database").ToString(), tbName);
                }
                else
                {
                    str = gv_import_C.GetRowCellValue(indexCol,"col_valuedefault").ToString();
                }
            }
            return str;
        }

        private void saveMode1()     // giử cả 2
        {
            try
            {
                DataTable dtGrid = new DataTable();
                dtGrid = (DataTable)gct_import_C.DataSource;
                waitDialog = new DevExpress.Utils.WaitDialogForm("Processing data ...", "Please wait");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                string value = "";
                for (int i = 0; i < dtExCel.Rows.Count; i++)
                {
                    waitDialog.SetCaption("Processing " + ((float)i / dtExCel.Rows.Count * 100).ToString("F2") + " % ");
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < gv_import_C.RowCount; j++)
                    {
                        value=setValueColumn(gv_import_C.GetRowCellValue(j, "col_excel").ToString(), dtExCel.Rows[i], i, j, gv_import_C.GetRowCellValue(j,"col_type").ToString());
                        if (value != "NULL")
                            dr[gv_import_C.GetRowCellValue(j, "col_database").ToString()] = value;                 
                    }
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
            catch
            {                
                waitDialog.Close();
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
            waitDialog.Close();
        }

        private void saveMode2()     // ghi de
        {
            try
            {
                DataTable dtGrid = new DataTable();
                dtGrid = (DataTable)gct_import_C.DataSource;
                waitDialog = new DevExpress.Utils.WaitDialogForm("Processing data ...", "Please wait");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                string value = "";
                for (int i = 0; i < dtExCel.Rows.Count; i++)
                {
                    DataRow dr;
                    waitDialog.SetCaption("Processing " + ((float)i / dtExCel.Rows.Count * 100).ToString("F2") + " % ");
                    if (checkExitData(dtExCel.Rows[i][cbo_checkColumn_S.Text].ToString()))
                    {                    
                        dr = dt.Rows[clsFunction.getIndexIDinTable(dtExCel.Rows[i][cbo_checkColumn_S.Text].ToString(), colCompair, dt)];
                        for (int j = 0; j < gv_import_C.RowCount; j++)
                        {
                            if (gv_import_C.GetRowCellValue(j, "col_database").ToString() != "ID")
                            {
                                value = setValueColumn(gv_import_C.GetRowCellValue(j, "col_excel").ToString(), dtExCel.Rows[i], i, j, gv_import_C.GetRowCellValue(j, "col_type").ToString());
                                if (value != "NULL")
                                    dr[gv_import_C.GetRowCellValue(j, "col_database").ToString()] = value;
                            }
                        }
                    }
                    else
                    {
                        dr = dt.NewRow();
                        for (int j = 0; j < gv_import_C.RowCount; j++)
                        {
                            value = setValueColumn(gv_import_C.GetRowCellValue(j, "col_excel").ToString(), dtExCel.Rows[i], i, j, gv_import_C.GetRowCellValue(j, "col_type").ToString());
                            if (value != "NULL")
                                dr[gv_import_C.GetRowCellValue(j, "col_database").ToString()] = value;
                        }
                        dt.Rows.Add(dr);
                    }
                    APCoreProcess.APCoreProcess.Save(dr);
                    waitDialog.Close();
                }
            }
            catch
            {
                waitDialog.Close();
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
        }

        private bool checkPrimaryColumn(string col)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT   column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tbName + "' and column_name='"+col+"'");
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }

        private bool checkExitData(string keyValue)
        {
            bool flag = false;
            for (int j = 0; j < gv_import_C.RowCount; j++)
            {
                if (gv_import_C.GetRowCellValue(j, "col_excel").ToString() == cbo_checkColumn_S.Text)
                {
                    colCompair = gv_import_C.GetRowCellValue(j, "col_database").ToString();
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("declare @value nvarchar(50) set @value=N'" + keyValue + "' select " + gv_import_C.GetRowCellValue(j, "col_database").ToString() + " from " + tbName + " where " + gv_import_C.GetRowCellValue(j, "col_database").ToString() + " = @value  ");
                    if (dt.Rows.Count > 0)
                        flag= true;
                }
            }  
            return flag;
        }

        private void saveMode3()     // bỏ qua
        {
            try
            {
                DataTable dtGrid = new DataTable();
                dtGrid = (DataTable)gct_import_C.DataSource;
                waitDialog = new DevExpress.Utils.WaitDialogForm("Processing data ...", "Please wait");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                string value = "";
                for (int i = 0; i < dtExCel.Rows.Count; i++)
                {
                    waitDialog.SetCaption("Processing " + ((float)i / dtExCel.Rows.Count * 100).ToString("F2") + " % ");
                    DataRow dr=dt.NewRow();;
                    if (!checkExitData(dtExCel.Rows[i][cbo_checkColumn_S.Text].ToString()))
                    {               
                        for (int j = 0; j < gv_import_C.RowCount; j++)
                        {
                            value = setValueColumn(gv_import_C.GetRowCellValue(j, "col_excel").ToString(), dtExCel.Rows[i], i, j, gv_import_C.GetRowCellValue(j, "col_type").ToString());
                            if (value != "NULL")
                                dr[gv_import_C.GetRowCellValue(j, "col_database").ToString()] = value;
                            //else
                            //    dr[gv_import_C.GetRowCellValue(j, col_database).ToString()] = clsFunction.GetDefault(dtExCel.Columns[i].DataType);
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }                      
                    waitDialog.Close();
                }
            }
            catch
            {
                waitDialog.Close();
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
        }

        private void setGridColumns()
        {
            dtConfig = (DataTable)gct_import_C.DataSource;
            Load_Grid();
            gct_import_C.DataSource = null;
            setDataSourceGrid();
        }

        private void setDataSourceGrid()
        {
            try
            {
                DataTable dt = new DataTable();

                for (int i = 0; i < dtExCel.Columns.Count; i++)
                {
                    if (sysNameCol(dtExCel.Rows[0][i].ToString())!="")
                    {
                        dtExCel.Columns[i].ColumnName = sysNameCol(dtExCel.Rows[0][i].ToString());
                    }
                }
                dtExCel.Rows.RemoveAt(0);
                gct_import_C.DataSource = dtExCel;
            }
            catch { }
        }

        private string sysNameCol(string ColExcel)
        {
            string colName = "";
            for (int i = 0; i < dtConfig.Rows.Count; i++)
            {
                if (ColExcel == dtConfig.Rows[i]["col_excel"].ToString())
                {
                    colName = dtConfig.Rows[i]["col_database"].ToString();
                    break;
                }
            }
            return colName;
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;
            gv_import_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = true;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_import_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (formNamePre) + "' and grid_name='" + gridNamePre + "'");
            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_import_C, read_Only, hien_Nav,
                dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_import_C,
                dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                dt.Rows[0]["field_name"].ToString().Split('/'),
                dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["fieldname_glue_col"].ToString()),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["caption_glue_col"].ToString()),
                dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
     
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gv_import_C_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
             
                GridView view = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    string strErr = view.GetRowCellValue(e.RowHandle, sysNameCol(cbo_checkColumn_S.EditValue.ToString())).ToString();
                    if (strErr != "")
                    {
                        e.Appearance.BackColor = Color.White;
                        e.Appearance.BackColor2 = Color.White;
                        countS++;                        
                    }
                    else
                    {
                        countE++;
                        e.Appearance.BackColor = Color.FromArgb(0xFF, 0x66, 0x00);
                        e.Appearance.BackColor2 = Color.FromArgb(0xFF, 0x66, 0x00);
                    }     
                }
                lbl_status_S.Text = clsFunction.transLateText("Số dòng thành công: ") + countS + clsFunction.transLateText(" - Số dòng lỗi: "+countE);
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

        private void gv_import_C_RowClick(object sender, RowClickEventArgs e)
        {
            gv_import_C.Appearance.FocusedRow.BackColor = Color.Aquamarine;
        }

        private void gv_import_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "col_excel")
            {
                if (e.Value.ToString() == cbo_checkColumn_S.Text)
                {
                    checkAllow = true;
                }
            }
        }

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = "";//Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Cột nguồn", "Cột Excel", "Kiểu", "Giá trị mặc định" };

            // FieldName column
            string[] fieldname_col = new string[] { "col_database", "col_excel", "col_type", "col_valuedefault"};

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "MemoColumn", };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "100", "300", "300"};
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "True", "False", "True"};
            // Cac cot an
            string[] Column_Visible = new string[] { };
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
            int so_cot_lue = 0;
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
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                  caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                  fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                  fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_import_C.Name);
        }

        private void Load_Grid_Excel()
        {
            string text = Function.clsFunction.langgues;
            gv_import_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_import_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_import_C.Name + "'");
            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_import_C, read_Only, hien_Nav,
       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_import_C,
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
         
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setEnableButton()
        {
            try
            {
                switch (step)
                {
                    case -1:
                        btn_browser_S.Enabled = true;
                        btn_cancel_S.Enabled = false;
                        btn_exit_S.Enabled = true;
                        btn_forward_S.Enabled = false;
                        btn_import_S.Enabled = false;
                        // Form Load
                        Load_Grid_Excel();
                        //setDataGrid();
                        gct_import_C.DataSource = null;
                       
                        btn_import_S.Text = clsFunction.transLateText("Tiếp tục");
                        break;
                    case 0:
                        dtExCel = null;
                        btn_browser_S.Enabled = false;
                        btn_cancel_S.Enabled = true;
                        btn_exit_S.Enabled = true;
                        btn_forward_S.Enabled = true;
                        btn_import_S.Enabled = true;
                        // Form Load
                        Load_Grid_Excel();
                        //setDataGrid();
                        process();
                        //gct_import_C.DataSource = dtExCel;
                        btn_import_S.Text = clsFunction.transLateText("Tiếp tục");
                        break;
                    case 1:
                        btn_browser_S.Enabled = false;
                        btn_cancel_S.Enabled = true;
                        btn_exit_S.Enabled = true;
                        btn_forward_S.Enabled = true;
                        btn_import_S.Enabled = true;
                        // duyet loi               
                        setGridColumns();
                        checkErrorGrid();                        
                        btn_import_S.Text = clsFunction.transLateText("Hoàn tất");
                        gv_import_C.OptionsBehavior.ReadOnly = false;
                        //gv_import_C.Editable = true;                        
                        for (int i = 0; i < gv_import_C.Columns.Count; i++)
                        {
                            gv_import_C.Columns[i].OptionsColumn.AllowFocus = true;
                            gv_import_C.Columns[i].OptionsColumn.AllowEdit = true;
                        }
                            break;
                    case 2:
                        btn_browser_S.Enabled = false;
                        btn_cancel_S.Enabled = true;
                        btn_exit_S.Enabled = true;
                        btn_forward_S.Enabled = true;
                        btn_import_S.Enabled = true;
                        insert();
                        lockButton(true);
                        step = -1;
                        clsFunction.MessageInfo("Thông báo", "Đã nhập dữ liệu hoàn tất");
                        this.Close();
                        break;
                }
            }
            catch { }
        }

        private bool CheckKeyExits(string fieldName, string keyvalue)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read( tbName+" where "+fieldName+ " = '"+keyvalue+"'  ");
            if (dt.Rows.Count > 0)
                flag = true;
            return flag;
        }

        private void saveAndSkip()
        {
            try
            {
                string keyfield = "";
                keyfield = sysNameCol(cbo_checkColumn_S.EditValue.ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                DataRow dr;
                for (int i = 0; i < gv_import_C.DataRowCount; i++)
                {
                    if (gv_import_C.GetRowCellValue(i, keyfield).ToString() != "" && !CheckKeyExits(keyfield, gv_import_C.GetRowCellValue(i, keyfield).ToString()))
                    {
                        dr = dt.NewRow();
                        for (int j = 0; j < dtConfig.Rows.Count; j++)
                        {
                            try
                            {
                                if (gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString() != "")
                                {
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString();
                                }
                                else
                                {
                                    // getvaluedefault
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = dtConfig.Rows[j]["col_valuedefault"].ToString();
                                }
                            }
                            catch { }
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng kiểm tra lại dữ liệu excel");
            }
        }

        private void saveAndReplace()
        {
            try
            {
                string keyfield = "";
                keyfield = sysNameCol(cbo_checkColumn_S.EditValue.ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                DataRow dr;
                for (int i = 0; i < gv_import_C.DataRowCount; i++)
                {
                    if (gv_import_C.GetRowCellValue(i, keyfield).ToString() != "" )
                    {
                        if (CheckKeyExits(keyfield, gv_import_C.GetRowCellValue(i, keyfield).ToString()))
                        {
                           dr=dt.Rows[clsFunction.getIndexIDinTable(gv_import_C.GetRowCellValue(i, keyfield).ToString(),keyfield,dt)];
                            dr.Delete();
                            APCoreProcess.APCoreProcess.Save(dr);
                        }
                        dr = dt.NewRow();
                        for (int j = 0; j < dtConfig.Rows.Count; j++)
                        {
                            try
                            {
                                if (gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString() != "")
                                {
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString();
                                }
                                else
                                {
                                    // getvaluedefault
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = dtConfig.Rows[j]["col_valuedefault"].ToString();
                                }
                            }
                            catch { }
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng kiểm tra lại dữ liệu excel");
            }
        }

        private void saveAndStop()
        {
            try
            {
                string keyfield = "";
                keyfield = sysNameCol(cbo_checkColumn_S.EditValue.ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                DataRow dr;
                for (int i = 0; i < gv_import_C.DataRowCount; i++)
                {
                    if (gv_import_C.GetRowCellValue(i, keyfield).ToString() != "")
                    {
                        if (CheckKeyExits(keyfield, gv_import_C.GetRowCellValue(i, keyfield).ToString()))
                        {
                            break;
                        }
                        dr = dt.NewRow();
                        for (int j = 0; j < dtConfig.Rows.Count; j++)
                        {
                            try
                            {
                                if (gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString() != "")
                                {
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString();
                                }
                                else
                                {
                                    // getvaluedefault
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = dtConfig.Rows[j]["col_valuedefault"].ToString();
                                }
                            }
                            catch { }
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng kiểm tra lại dữ liệu excel");
            }
        }

        private void saveAndBoth()
        {
            try
            {
                string keyfield = "";
                keyfield = sysNameCol(cbo_checkColumn_S.EditValue.ToString());
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(tbName);
                DataRow dr;
                for (int i = 0; i < gv_import_C.DataRowCount; i++)
                {
                    if (gv_import_C.GetRowCellValue(i, keyfield).ToString() != "")
                    {
                        if (CheckKeyExits(keyfield, gv_import_C.GetRowCellValue(i, keyfield).ToString()))
                        {
                            dr = dt.NewRow();
                            for (int j = 0; j < dtConfig.Rows.Count; j++)
                            {
                                try
                                {
                                    if (gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString() != "")
                                    {
                                        if (checkPrimaryKey() == dtConfig.Rows[j]["col_database"].ToString())
                                        {
                                            dr[dtConfig.Rows[j]["col_database"].ToString()] = Function.clsFunction.layMa(sDauma, checkPrimaryKey(), tbName);
                                        }
                                        else
                                        {
                                            dr[dtConfig.Rows[j]["col_database"].ToString()] = gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString();
                                        }
                                    }
                                    else
                                    {
                                        // getvaluedefault
                                        dr[dtConfig.Rows[j]["col_database"].ToString()] = dtConfig.Rows[j]["col_valuedefault"].ToString();
                                    }

                                }
                                catch { }
                            }
                        }
                        else
                        {
                            dr = dt.NewRow();
                            for (int j = 0; j < dtConfig.Rows.Count; j++)
                            {
                                if (gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString() != "")
                                {
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = gv_import_C.GetRowCellValue(i, dtConfig.Rows[j]["col_database"].ToString()).ToString();
                                }
                                else
                                {
                                    // getvaluedefault
                                    dr[dtConfig.Rows[j]["col_database"].ToString()] = dtConfig.Rows[j]["col_valuedefault"].ToString();
                                }
                            }
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng kiểm tra lại dữ liệu excel");
            }
        }
        
        private string checkPrimaryKey()
        {
            string primarykey = "";
            DataTable dt = new DataTable();
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
                dt = APCoreProcess.APCoreProcess.Read("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tbName + "'");
            else
                dt = APCoreProcess.APCoreProcess.Read("SELECT     COLUMN_NAME FROM         INFORMATION_SCHEMA.INDEXES WHERE     (PRIMARY_KEY = 1) AND (TABLE_NAME = '" + tbName + "')");
            if (dt.Rows.Count > 0)
            {
                primarykey = dt.Rows[0][0].ToString();
            }
            return primarykey;
        }
             
        #endregion

        #region  Button Event
        
        private void btn_browser_S_Click(object sender, EventArgs e)
        {
            step = -1;
            try
            {
                gct_import_C.DataSource = null;
                countE = 0;
                countS = 0;
                dtExCel = null;
                loadFileExcel();
                loadCboColumn();
                btn_import_S.Enabled = false;
                //process();
                step++;
                setEnableButton();
                
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Kết nối thất bại, vui lòng kiểm tra lại phiên bản");
            }
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_import_S_Click(object sender, EventArgs e)
        {
            if (step<2)
                step++;
            setEnableButton();    
        }

        private void btn_forward_S_Click(object sender, EventArgs e)
        {            
            if (step > -1)
                step--;
            setEnableButton();          
        }

        private void checkErrorGrid()
        {
            for (int i = 0; i < gv_import_C.DataRowCount; i++)
            {
                if (gv_import_C.GetRowCellValue(i, gv_import_C.Columns[sysNameCol(cbo_checkColumn_S.EditValue.ToString())].Name.ToString()).ToString() == "")
                {
                    gv_import_C.SetColumnError(gv_import_C.Columns[sysNameCol(cbo_checkColumn_S.EditValue.ToString())], clsFunction.transLateText("Trường khóa không được rỗng"));                                   
                }
            }
        }

        private void process()
        {
            try
            {
                if (dtExCel == null)
                {
                    clsImportExcel excel = new clsImportExcel(txt_input_S.Text   , Convert.ToInt16(cbo_version_S.Text));
                    dtExCel = excel.getDataFromExcel((int)(spe_index_S.Value - 1));
                }
                gct_import_C.DataSource = null;
                int i = Convert.ToInt16(spe_rowstart_S.Value);
                //xóa các dòng trống của file excel theo điều kiện cột rỗng
                //while (i <= dtExCel.Rows.Count)
                //{
                //    if (dtExCel.Rows[i][0].ToString() == string.Empty)
                //    {
                //        dtExCel.Rows.RemoveAt(i);
                //        dtExCel.AcceptChanges();
                //    }
                //    else
                //    {
                //        i += 1;
                //    }
                //}
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpCrt1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                rpCrt1.ShowHeader = false;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpCrt2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                rpCrt2.ShowHeader = false;
                gv_import_C.Columns[0].ColumnEdit = rpCrt1;
                gv_import_C.Columns[1].ColumnEdit = rpCrt2;

                loadLueColumnExcel(((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_import_C.Columns[1].ColumnEdit));
                loadLueColumnDatabase(((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_import_C.Columns[0].ColumnEdit));

                setDataGrid();
                gv_import_C.Columns[1].OptionsColumn.AllowEdit = true;
                gv_import_C.Columns[3].OptionsColumn.AllowEdit = true;
                gv_import_C.Columns[1].OptionsColumn.ReadOnly = false;
                gv_import_C.Columns[3].OptionsColumn.ReadOnly = false;
                //Load_Grid();  
            
               
            }
            catch { }
        }

        private void cbo_checkColumn_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkAllow = false;
        }

        private void btn_cancel_S_Click(object sender, EventArgs e)
        {
            checkAllow = false;
            lockButton(true);       
        }
        
        #endregion
            
    }
}