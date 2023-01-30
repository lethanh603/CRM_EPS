using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using APCoreProcess;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using System.Reflection;

namespace ControlDev
{
    public class FormatControls
    {

        #region Var
    
        #endregion

        #region ComBoBoxEdit
        public static void LoadComBoBoxEdit(ComboBoxEdit cbo, DataTable dt)
        {
            cbo.Properties.Items.Clear();
            cbo.Properties.Buttons.Clear();
            cbo.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            string[] arr = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] =  dt.Rows[i]["username"].ToString();
            }            
       
            cbo.Properties.Items.AddRange(arr);
            cbo.SelectedIndex = 0;
        }
        public static void LoadComBoBoxEdit1(ComboBoxEdit cbo, DataTable dt, string image_name,string feild_name)
        {
            string[] arr = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] = Function.clsFunction.transLateText(dt.Rows[i][feild_name].ToString());
                    
            }
            if (image_name != "")
            {
                cbo.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                cbo.Properties.Buttons.Add(ebtn);
            }
            cbo.Properties.Items.AddRange(arr);
            cbo.SelectedIndex = 0;
        }

        public static void LoadResComBoBoxEdit1(DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbo, DataTable dt, string image_name, string feild_name)
        {
            cbo.Items.Clear();
            string[] arr = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] = dt.Rows[i][feild_name].ToString();
            }
   
            if (image_name != "")
            {         
                cbo.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                cbo.Buttons.Add(ebtn);
            }
            cbo.Items.AddRange(arr);
            
        }
        #endregion

        #region Grid

        public static void setColorColumn(BandedGridView bandedGridView1, DateTime ngay, int thu, Color mau)
        {
            int day = 0, month = 0, year = 0;
            month = ngay.Month;
            year = ngay.Year;
            int ngaytrongthang = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < ngaytrongthang; i++)
            {
                day = i + 1;
                if (Function.clsFunction.ThuTuongUng(day, month, year) == thu)
                {
                    bandedGridView1.Columns[i].AppearanceCell.BackColor = mau;
                    bandedGridView1.Columns[i].AppearanceCell.Options.UseBackColor = true;
                }              
            }
            bandedGridView1.Columns[ngaytrongthang].AppearanceCell.BackColor = mau;
            bandedGridView1.Columns[ngaytrongthang].AppearanceCell.Options.UseBackColor = true;
        }
        public static void setColorColumn(BandedGridView bandedGridView1, DateTime tungay, DateTime denngay, int thu, Color mau)
        {
            int day = 0, month = 0, year = 0;      
        
            for (DateTime   i = tungay; i <= denngay;i= i.AddDays(1))
            {
                day = i.Day;
                month=i.Month;
                year=i.Year;
                if (Function.clsFunction.ThuTuongUng(day, month, year) == thu)
                {
                    bandedGridView1.Columns["gc"+i.Day].AppearanceCell.BackColor = mau;
                    bandedGridView1.Columns["gc"+i.Day].AppearanceCell.Options.UseBackColor = true;
                }
            }
            bandedGridView1.Columns[denngay.Day].AppearanceCell.BackColor = mau;
            bandedGridView1.Columns[denngay.Day].AppearanceCell.Options.UseBackColor = true;
        }

        public static void setColorColumn(BandedGridView bandedGridView1, string[] columnName , Color mau)
        {  
            for (int i = 0; i < bandedGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < columnName.Length; j++)
                {
                    if (columnName[j]==bandedGridView1.Columns[i].Name)
                    {
                        bandedGridView1.Columns[i].AppearanceCell.BackColor = mau;
                        bandedGridView1.Columns[i].AppearanceCell.Options.UseBackColor = true;
                    }
                }
            }

        }
        public static void setColorColumn(BandedGridView bandedGridView1, DateTime ngay, int thu, Color mau, string str)
        {
            int day = 0, month = 0, year = 0;
            month = ngay.Month;
            year = ngay.Year;
            int ngaytrongthang = DateTime.DaysInMonth(year, month);

            for (int i = 0; i < ngaytrongthang; i++)
            {
                day = i + 1;
                if (Function.clsFunction.ThuTuongUng(day, month, year) == thu)
                {
                    bandedGridView1.Columns[i].AppearanceCell.BackColor = mau;
                    bandedGridView1.Columns[i].AppearanceCell.Options.UseBackColor = true;
                    if (bandedGridView1.GetRowCellValue(bandedGridView1.FocusedRowHandle, bandedGridView1.Columns[i].Name).ToString() == "0")
                        bandedGridView1.SetRowCellValue(bandedGridView1.FocusedRowHandle, bandedGridView1.Columns[i].Name, "");
                }

            }
            bandedGridView1.Columns[ngaytrongthang].AppearanceCell.BackColor = mau;
            bandedGridView1.Columns[ngaytrongthang].AppearanceCell.Options.UseBackColor = true;
        }
        public static void FormatGridControl(GridView gv, bool hien_Nav, DevExpress.XtraGrid.GridControl gctrDM, bool show_footer)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowFooter = show_footer;
            //////////
            if (hien_Nav == true)
            {
                ControlNavigator navigator = gctrDM.EmbeddedNavigator;
                navigator.Buttons.BeginUpdate();
                try
                {
                    navigator.Buttons.Append.Visible = false;
                    navigator.Buttons.Remove.Visible = false;
                    navigator.Buttons.CancelEdit.Visible = false;
                    navigator.Buttons.Edit.Visible = false;
                    navigator.Buttons.EndEdit.Visible = false;
                }
                finally
                {
                    navigator.Buttons.EndUpdate();
                }
                gctrDM.UseEmbeddedNavigator = true;
            }

        }
        public static void LoadGridView(GridView gv, bool hien_Nav, string[] column_summary, bool show_footer, GridControl gct, string sql, string[] caption_col, string[] fieldname_col, string[] Column_Width, string[] AllowFocus)
        {  
            if (gct.DataSource != null )
            {
                gv.Columns.Clear();
                //gct.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            }
            FormatGridControl(gv, hien_Nav, gct, show_footer);   
            for (int i = 0; i < fieldname_col.Length-1; i++)
            {
                GridColumn gc = new GridColumn();
                gc.Caption = caption_col[i];
                gc.FieldName = fieldname_col[i];
                gc.Name = fieldname_col[i];
                gc.Width = int.Parse(Column_Width[i]);
                gc.OptionsColumn.AllowFocus = bool.Parse(AllowFocus[i]);
              
                for (int j = 0; j < column_summary.Length; j++)
                {
                    if (gc.FieldName.ToString() == column_summary[j])
                    {
                        gc.SummaryItem.FieldName = column_summary[j];
                        gc.SummaryItem.Tag = j;
                        gc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gc.SummaryItem.DisplayFormat= "{0:n0}";
                    }
                }
                gc.VisibleIndex = i;
                gv.Columns.Add(gc);
            }
            if (gct.DataSource == null && sql!="")
            {
                //gv.Columns.Clear();
                gct.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            }
      
        }
        public static void LoadGridView_Edit(GridView gv, bool hien_Nav, string[] column_summary, bool show_footer, GridControl gct, string sql, string[] caption_col, string[] fieldname_col, string[] fieldname, string[] Column_Width, string[] AllowFocus)
        {
            if (gct.DataSource != null)
            {
                gv.Columns.Clear();
                //gct.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            }
            FormatGridControl(gv, hien_Nav, gct, show_footer);
            for (int i = 0; i < fieldname_col.Length - 1; i++)
            {

                GridColumn gc = new GridColumn();
                gc.Caption = caption_col[i];
                gc.FieldName = fieldname[i];
                gc.Name = fieldname_col[i];
                gc.Width = int.Parse(Column_Width[i]);
                gc.OptionsColumn.AllowFocus = bool.Parse(AllowFocus[i]);

                for (int j = 0; j < column_summary.Length; j++)
                {
                    if (gc.FieldName.ToString() == column_summary[j])
                    {
                        gc.SummaryItem.FieldName = column_summary[j];
                        gc.SummaryItem.Tag = j;
                        gc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gc.SummaryItem.DisplayFormat = "{0:n0}";
                        break;
                    }
                }
                gc.VisibleIndex = i;
                gv.Columns.Add(gc);
            }
            if (gct.DataSource == null && sql != "")
            {
                //gv.Columns.Clear();
                //gct.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            }
        }

        public static int GiaiMaMang(String sChuoi)
        {
            int i = 0;
            if (sChuoi.ToLower() == "TextColumn".ToLower())          
                i = 1;   
            if (sChuoi.ToLower() == "CheckColumn".ToLower())
                i = 2;
            if (sChuoi.ToLower() == "LookUpEditColumn".ToLower())
                i = 3;
            if (sChuoi.ToLower() == "SpinEditColumn".ToLower())
                i = 4;
            if (sChuoi.ToLower() == "CalcEditColumn".ToLower())
                i = 5;
            if (sChuoi.ToLower() == "TimeEditColumn".ToLower())
                i = 6;
            if (sChuoi.ToLower() == "MemoColumn".ToLower())
                i = 7;
            if (sChuoi.ToLower() == "DateColumn".ToLower())
                i = 8;
            if (sChuoi.ToLower() == "GridLookupEditColumn".ToLower())
                i = 9;           
            if (sChuoi.ToLower() == "Percent".ToLower())
                i = 11;
            if (sChuoi.ToLower() == "HyperLinkEditColumn".ToLower())
                i = 12;
            if (sChuoi.ToLower() == "Percent2".ToLower())
            {
                i = 13;
            }
            if (sChuoi.ToLower() == "F1Column".ToLower())
            {
                i = 14;
            }
            if (sChuoi.ToLower() == "F2Column".ToLower())
            {
                i = 15;
            }


            return i;
        }

        public static void Controls_in_Grid(GridView gv, bool read_Only, bool hien_Nav, string[] column_summary, bool show_footer, GridControl gct, string sql, string[] caption_col, string[] fieldname_col, string[] Column_Width, string[] AllowFocus, string[] styleColumn,
            string[] Column_Visible, string[] sql_lue, string[] caption_lue, string[] fieldname_lue, string[,] fieldname_lue_col, string[,] caption_lue_col,
            string[] fieldname_lue_visible, int cot_lue_search, string[] sql_glue, string[] caption_glue, string[] fieldname_glue, string[,] fieldname_glue_col, string[,] caption_glue_col,
            string[] fieldname_glue_visible, int cot_glue_search)
        {
            gv.OptionsBehavior.ReadOnly = read_Only;
            LoadGridView(gv, hien_Nav, column_summary, show_footer, gct, sql, caption_col, fieldname_col, Column_Width, AllowFocus);
            int lan_glue = 0;
            int lan_lue = 0;
            for (int i = 0; i < fieldname_col.Length-1; i++)
            {
                switch (GiaiMaMang(styleColumn[i]))
                {
                    case 1://textColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 2://CheckColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 3://LookupEditColumn
                        {                        
                            if (sql_lue[lan_lue] != "")
                            {
                                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                rpCrt.DataSource = APCoreProcess.APCoreProcess.Read(sql_lue[lan_lue]);
                                rpCrt.DisplayMember = caption_lue[lan_lue];
                                rpCrt.ValueMember = fieldname_lue[lan_lue];
                                for (int j = 0; j < fieldname_lue_col.GetLength(0); j++)
                                {
                                    try
                                    {
                                        for (int h = 0; h < fieldname_lue_col.GetLength(1); h++)
                                        {
                                            DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                                            col.FieldName = fieldname_lue_col[lan_lue, h];
                                            col.Caption = caption_lue_col[lan_lue, h];
                                          
                                            rpCrt.Columns.Add(col);
                                            for (int k = 0; k < fieldname_lue_visible.Length; k++)
                                                if (col.FieldName.ToString() == fieldname_lue_visible[k])
                                                    col.Visible = false;
                                            gv.Columns[i].ColumnEdit = rpCrt;
                                            rpCrt.AutoSearchColumnIndex = cot_lue_search;
                                            rpCrt.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                                            rpCrt.NullValuePrompt = "";
                                            rpCrt.NullText = "";
                                        }
                                        break;
                                    }
                                    catch (Exception ex) 
                                    {
                                        MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    };
                                }
                            }
                                lan_lue++;
                                break;                            
                        }
                    case 4://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask="n0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 5://CalcEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "N0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;                                             
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 6://TimeEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 7://MemoColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
                            rpCrt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                            rpCrt.ShowIcon = false;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 8://DateColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                         
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;                      
                            rpCrt.Mask.EditMask = APCoreProcess.APCoreProcess.Read("sysConfig").Rows[0]["typedate"].ToString();
                            rpCrt.EditFormat.FormatType=FormatType.DateTime;
                            rpCrt.EditFormat.FormatString = APCoreProcess.APCoreProcess.Read("sysConfig").Rows[0]["typedate"].ToString();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 9://GridLookupEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
                            DataTable dt = new DataTable();
                            dt = APCoreProcess.APCoreProcess.Read(sql_glue[lan_glue]);                       
                            rpCrt.DataSource = dt;
                            rpCrt.DisplayMember = caption_glue[lan_glue];
                            rpCrt.ValueMember = fieldname_glue[lan_glue];
                            rpCrt.NullValuePrompt = "";
                            rpCrt.NullText = "";
                            for (int j = 0; j < fieldname_glue_col.GetLength(0); j++)
                            {
                                for (int h = 0; h < fieldname_glue_col.GetLength(1); h++)
                                {
                                    DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                                    col.Caption = caption_glue_col[lan_glue, h];
                                    col.FieldName = fieldname_glue_col[lan_glue, h];                                  
                                    col.VisibleIndex = h;
                                    if (h == 0)
                                    {
                                        col.Width = 40;
                                    }
                                    col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                                    for (int k = 0; k < fieldname_glue_visible.Length; k++)
                                        if (col.FieldName.ToString() == fieldname_glue_visible[k])
                                            col.Visible = false;
                                    rpCrt.View.Columns.Add(col);
                                    rpCrt.View.ActiveFilter.Clear();
                                    
                                }
                                break;
                            }
                            //rpCrt.View.OptionsView.ShowAutoFilterRow = true;   
                            rpCrt.View.OptionsView.ShowAutoFilterRow = true;
                            gv.Columns[i].ColumnEdit = rpCrt;                           
                            lan_glue++;
                            break;
                        }

                    case 11://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 12://HyperLinkEdit
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();                        
                            rpCrt.OpenLink+=new OpenLinkEventHandler(rpCrt_OpenLink);                           
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 13://Percent2
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P2";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 14://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "F1";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 15://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "F2";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                }
                //for (int j = 0; j < Column_Visible.Length; j++)
                //{
                //    if (gv.Columns[i].FieldName.ToString() == Column_Visible[j])
                //        gv.Columns[i].Visible = false;
                //}
                gv.Columns[i].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                gv.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                gv.Columns[i].Visible = Convert.ToBoolean( Column_Visible[i]);
                gv.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gv.Columns[i].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gv.Columns[i].AppearanceHeader.Options.UseFont = true; 
                gv.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        public static void Controls_in_Grid_Edit(GridView gv, bool read_Only, bool hien_Nav, string[] column_summary, bool show_footer, GridControl gct, string sql, string[] caption_col, string[] fieldname_col, string[] fieldname, string[] Column_Width, string[] AllowFocus, string[] styleColumn,
          string[] Column_Visible, string[] sql_lue, string[] caption_lue, string[] fieldname_lue, string[,] fieldname_lue_col, string[,] caption_lue_col,
          string[] fieldname_lue_visible, int cot_lue_search, string[] sql_glue, string[] caption_glue, string[] fieldname_glue, string[,] fieldname_glue_col, string[,] caption_glue_col,
          string[] fieldname_glue_visible, int cot_glue_search, string[] gluenulltext)
        {
            gv.OptionsBehavior.ReadOnly = read_Only;
            LoadGridView_Edit(gv, hien_Nav, column_summary, show_footer, gct, sql, caption_col, fieldname_col,fieldname, Column_Width, AllowFocus);
            int lan_glue = 0;
            int lan_lue = 0;
            for (int i = 0; i < fieldname_col.Length - 1; i++)
            {
                switch (GiaiMaMang(styleColumn[i]))
                {
                    case 1://textColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();

                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 2://CheckColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 3://LookupEditColumn
                        {
                            if (sql_lue[lan_lue] != "")
                            {
                                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                rpCrt.DataSource = APCoreProcess.APCoreProcess.Read(sql_lue[lan_lue]);
                                rpCrt.DisplayMember = caption_lue[lan_lue];
                                rpCrt.ValueMember = fieldname_lue[lan_lue];
                                for (int j = 0; j < fieldname_lue_col.GetLength(0); j++)
                                {
                                    try
                                    {
                                        for (int h = 0; h < fieldname_lue_col.GetLength(1); h++)
                                        {
                                            DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                                            col.FieldName = fieldname_lue_col[lan_lue, h];
                                            col.Caption = caption_lue_col[lan_lue, h];
                                            rpCrt.Columns.Add(col);
                                            for (int k = 0; k < fieldname_lue_visible.Length; k++)
                                                if (col.FieldName.ToString() == fieldname_lue_visible[k])
                                                    col.Visible = false;
                                            gv.Columns[i].ColumnEdit = rpCrt;
                                            rpCrt.AutoSearchColumnIndex = cot_lue_search;
                                            rpCrt.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                                            rpCrt.NullValuePrompt = "";
                                            rpCrt.NullText = "";
                                        }
                                        break;
                                    }
                                    catch (Exception ex) 
                                    {
                                        MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    };
                                }
                            }
                            lan_lue++;
                            break;
                        }
                    case 4://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "N0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 5://CalcEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "N0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 6://TimeEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 7://MemoColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
                            rpCrt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                            rpCrt.ShowIcon = false;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 8://DateColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();

                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            //rpCrt.Mask.EditMask = APCoreProcess.APCoreProcess.Read("sysConfig").Rows[0]["typedate"].ToString();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 9://GridLookupEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
                            rpCrt.DataSource = APCoreProcess.APCoreProcess.Read(sql_glue[lan_glue]);
                            rpCrt.DisplayMember = caption_glue[lan_glue];
                            rpCrt.ValueMember = fieldname_glue[lan_glue];
                            for (int j = 0; j < fieldname_glue_col.GetLength(0); j++)
                            {
                                for (int h = 0; h < fieldname_glue_col.GetLength(1); h++)
                                {
                                    DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                                    col.Caption = caption_glue_col[lan_glue, h];
                                    col.FieldName = fieldname_glue_col[lan_glue, h];
                                    col.VisibleIndex = h;
                                    if (h == 0)
                                    {
                                        col.Width = 40;
                                    }
                                    else
                                    {
                                        col.Width = 200;
                                    }
                                    col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                                    for (int k = 0; k < fieldname_glue_visible.Length; k++)
                                        if (col.FieldName.ToString() == fieldname_glue_visible[k])
                                            col.Visible = false;
                                    rpCrt.View.Columns.Add(col);
                                    rpCrt.View.ActiveFilter.Clear();
                                }
                                break;
                            }
                            rpCrt.View.OptionsView.ShowAutoFilterRow = true;
                            if (gluenulltext.Length > 0)
                            {
                                rpCrt.NullText = Function.clsFunction.transLateText(gluenulltext[lan_glue]);
                            }
                            gv.Columns[i].ColumnEdit = rpCrt;
                            lan_glue++;
                            break;
                        }

                    case 11://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 12://HyperLinkEdit
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
                            rpCrt.OpenLink += new OpenLinkEventHandler(rpCrt_OpenLink);
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 13://Percent2
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P2";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 14://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "F1";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 15://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "F2";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                }
                //for (int j = 0; j < Column_Visible.Length; j++)
                //{
                //    if (gv.Columns[i].FieldName.ToString() == Column_Visible[j])
                //        gv.Columns[i].Visible = false;
                //}
                gv.Columns[i].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                gv.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                gv.Columns[i].Visible = Convert.ToBoolean(Column_Visible[i]);
                gv.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gv.Columns[i].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gv.Columns[i].AppearanceHeader.Options.UseFont = true;
                gv.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }


        private static void rpCrt_OpenLink(object sender, OpenLinkEventArgs e)
        {
            e.EditValue = e.EditValue.ToString();
        }

        public static void setContainsFilter(GridView view)
        {
            foreach (GridColumn col in view.Columns)
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
        }

        public static void setContainsFilterGridLookupEdit(GridLookUpEdit view)
        {
            foreach (GridColumn col in view.Properties.View.Columns)
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
        }

        #endregion

        #region gridLookUpEdit

        private void gridLookUpEdit1_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e, DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rpCrt)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            string filter = "1,2,4";
            edit.Properties.View.ActiveFilter.Clear();
            edit.Properties.View.ActiveFilter.Add(rpCrt.View.Columns["CategoryID"], new ColumnFilterInfo("CategoryID IN (" + filter.ToString() + ")"));
            edit.Properties.View.ApplyColumnsFilter();
            edit.Properties.View.LayoutChanged();
        }

        public static void LoadGridLookupEdit(GridLookUpEdit glue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            glue.Properties.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            glue.Properties.DisplayMember = caption;
            glue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = glue.Properties.View.Columns.AddField(fieldname_lue_col[i]);
                col.Caption = caption_lue_col[i];
                col.Name = fieldname_lue_col[i];
                col.VisibleIndex = i;
                if (i == 0)
                {
                    col.Width = 30;
                    col.Visible = false;
                }
                //glue.Properties.Columns.Add(col);
                //glue.ItemIndex = 0;
            }
            glue.Properties.PopupFormWidth = 500;  
            glue.Properties.NullText = "";            
            //Add button
            if (image_name != "")
            {
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                glue.Properties.Buttons.Add(ebtn);
            }
            //lue.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(Properties_ButtonClick);
            // Save lookupEdit
            SaveGridLookupEdit(glue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name,formname);
        }
        
        public static void LoadGridLookupEdit(GridLookUpEdit glue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col,  string formname, int width,string[] col_visible)
        {
            glue.Properties.DataSource = null;
            glue.Properties.View.Columns.Clear();
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            
            glue.Properties.DisplayMember = caption;
            glue.Properties.ValueMember = value;
  
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = glue.Properties.View.Columns.AddField(fieldname_lue_col[i]);
                col.Caption = caption_lue_col[i];
                col.Name = fieldname_lue_col[i];

                col.VisibleIndex = i;
                if (i == 0)
                {
                    col.Width = 30;
                }
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                glue.Properties.View.Columns[i].Visible = Convert.ToBoolean(col_visible[i]);
                //glue.Properties.Columns.Add(col);
                //glue.ItemIndex = 0;
            }
            glue.Properties.DataSource = dt;
            glue.Properties.PopupFormWidth = width;
            glue.Properties.NullText = "";
            glue.EditValue = glue.Properties.GetKeyValue(0);
            glue.Properties.View.OptionsView.ShowAutoFilterRow = true;
   
        }

        public static void LoadGridLookupEditSearch(GridLookUpEdit glue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, string formname, int width, string[] col_visible)
        {
            glue.Properties.View.Columns.Clear();
            DataTable dtS = new DataTable();
            
            
            glue.Properties.DisplayMember = caption;
            glue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = glue.Properties.View.Columns.AddField(fieldname_lue_col[i]);
                col.Caption = caption_lue_col[i];
                col.Name = fieldname_lue_col[i];
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                if (i == 0)
                {
                    col.Width = 30;
                    col.Visible = false;
                }
                col.VisibleIndex = i;
                glue.Properties.View.Columns[i].Visible = Convert.ToBoolean(col_visible[i]);
                //glue.Properties.Columns.Add(col);
                //glue.ItemIndex = 0;
            }
            glue.Properties.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            glue.Properties.PopupFormWidth = width;
            glue.Properties.NullText = Function.clsFunction.transLateText( "Tất cả");
            glue.EditValue = glue.Properties.GetKeyValue(0);
        }

        public static void LoadGridLookupEdit(GridLookUpEdit glue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, string formname, int width)
        {
            glue.Properties.View.Columns.Clear();
            
            glue.Properties.DisplayMember = caption;
            glue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = glue.Properties.View.Columns.AddField(fieldname_lue_col[i]);
                col.Caption = caption_lue_col[i];
                col.Name = fieldname_lue_col[i];
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                col.VisibleIndex = i;
                if (i == 0)
                {
                    col.Width = 30;
                    col.Visible = false;
                }
                //glue.Properties.Columns.Add(col);
                //glue.ItemIndex = 0;
            }
            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            glue.Properties.DataSource = dt;
            glue.Properties.PopupFormWidth = width;
            glue.Properties.NullText = "";
            if (dt.Rows.Count > 0)
            {
                glue.EditValue = glue.Properties.GetKeyValue(0);
            }
           
        }

        public static void LoadGridLookupEdit1(GridLookUpEdit glue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            glue.Properties.View.Columns.Clear();
            
            glue.Properties.DisplayMember = caption;
            glue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = glue.Properties.View.Columns.AddField(fieldname_lue_col[i]);
                col.Caption = caption_lue_col[i];
                col.Name = fieldname_lue_col[i];
                col.VisibleIndex = i;
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                if (i == 0)
                {
                    col.Width = 30;
                    col.Visible = false;
                }
                //glue.Properties.Columns.Add(col);
                //glue.ItemIndex = 0;
            }
            glue.Properties.PopupFormWidth = 500;
            glue.Properties.NullText = "";

            //Add button
            if (image_name != "")
            {
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                glue.Properties.Buttons.Add(ebtn);
            }
            glue.Properties.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            //lue.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(Properties_ButtonClick);
            // Save lookupEdit
            SaveGridLookupEdit(glue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name,formname);
        }

        private static void SaveGridLookupEdit(GridLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            if (status_Form == true)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysControls");
                try
                {
                    DataRow dr = dt.Rows[index_edit(lue.Name,formname)];
                    dr["sql_lue"] = sql;
                    dr["displaymember_lue"] = caption;
                    dr["valuemember_lue"] = value;
                    dr["caption_col_lue_VI"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["caption_col_lue_EN"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["fieldname_col_lue"] = Function.clsFunction.ConvertArrayToString(fieldname_lue_col);
                    dr["image"] = (image_name);
                    APCoreProcess.APCoreProcess.Save(dr);
                    MessageBox.Show("save thanh cong");
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
                }
            }
        }
        
        public static void LoadGridLookupEditNull(GridLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            lue.Properties.View.Columns.Clear();
            DataTable dt=new DataTable();
            dt=APCoreProcess.APCoreProcess.Read(sql);
            DataRow dr=dt.NewRow();
            dr[caption]="Không chọn";
            dr[value] = "NULL";
            dt.Rows.Add(dr);
            DataView dv=dt.DefaultView;
            dv.Sort=value;
            lue.Properties.DataSource = dv.ToTable();
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;        
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = new GridColumn();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                col.VisibleIndex = 1;
                lue.Properties.View.Columns.Add(col);
            }
            lue.Properties.View.Columns[0].Width = 25;
            lue.Properties.PopupFormWidth = 300;
            lue.Properties.NullText = "";
            if (Function.clsFunction._keylience != true)
                Application.Exit();
            //Add button
            if (image_name != "")
            {
                lue.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Properties.Buttons.Add(ebtn);
            }
            SaveGridLookupEdit3(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name,formname);
        }

        public static void LoadGridLookupEdit3(GridLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name, string formname)
        {
            lue.Properties.View.Columns.Clear();
            lue.Properties.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = new GridColumn();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                col.VisibleIndex = 1;
                lue.Properties.View.Columns.Add(col);
            }
            lue.Properties.View.Columns[0].Width = 25;
            lue.Properties.PopupFormWidth = 300;
            lue.Properties.NullText = "";
            if (Function.clsFunction._keylience != true)
                Application.Exit();
            //Add button
            if (image_name != "")
            {
                lue.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Properties.Buttons.Add(ebtn);
            }
            SaveGridLookupEdit3(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name, formname);
        }

        public static void LoadGridLookupEditALL(GridLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name, string formname)
        {
            lue.Properties.View.OptionsView.ShowAutoFilterRow = true;
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(sql);
            DataRow dr = dt.NewRow();
            dr[value] = "";
            dr[caption] = "ALL";
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                dr[fieldname_lue_col[i]] = "ALL";
            }
            dt.Rows.Add(dr);
            DataView dv = dt.DefaultView;
            dv.Sort = value;
            lue.Properties.DataSource = dv.ToTable();
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;
            lue.Properties.View.Columns.Clear();       
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                GridColumn col = new GridColumn();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                col.VisibleIndex = 1;
                lue.Properties.View.Columns.Add(col);
            }
            lue.Properties.View.Columns[0].Width = 35;
            lue.Properties.PopupFormWidth = 400;
            lue.Properties.NullText = "";
            if (Function.clsFunction._keylience != true)
                Application.Exit();
            //Add button
            if (image_name != "")
            {
                lue.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Properties.Buttons.Add(ebtn);
            }

            SaveGridLookupEdit3(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name, formname);
        }

        private static void SaveGridLookupEdit3(GridLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            if (status_Form == true)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysControls");
                try
                {
                    DataRow dr = dt.Rows[index_edit(lue.Name,formname)];
                    dr["sql_lue"] = sql;
                    dr["displaymember_lue"] = caption;
                    dr["valuemember_lue"] = value;
                    dr["caption_col_lue_VI"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["caption_col_lue_EN"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["fieldname_col_lue"] = Function.clsFunction.ConvertArrayToString(fieldname_lue_col);
                    dr["image"] = (image_name);
                    APCoreProcess.APCoreProcess.Save(dr);
                    MessageBox.Show("save thanh cong");
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
                }
            }
        }

        public static void LoadGridLookupEditNoneParameter(GridLookUpEdit lue, string storename, string sqlStore, string valuemember, string displaymember)
        {
            try
            {
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Function.clsFunction.ExcuteProc(storename, new string[0, 0]);
                }
                else
                {


                    dt = APCoreProcess.APCoreProcess.Read(sqlStore);
                }
                //dt = APCoreProcess.APCoreProcess.Read(sqlStore, storename, new string[0, 0], "");
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = displaymember;
                lue.Properties.ValueMember = valuemember;
                lue.Properties.NullText = "";
                for (int i = 0; i < lue.Properties.View.Columns.Count; i++)
                {
                    lue.Properties.View.Columns[i].Caption = Function.clsFunction.transLateText(lue.Properties.View.Columns[i].Caption);
                }
                //Add button
                lue.EditValue = lue.Properties.GetKeyValue(0);
            }
            catch
            {
                APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
            }
        }
       
        public static void LoadGridLookupEditParameter(GridLookUpEdit lue, string storename, string sqlStore,string[,] arr, string valuemember, string displaymember)
        {
            try
            {
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Function.clsFunction.ExcuteProc(storename, arr);
                    if (dt==null)
                        APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read(sqlStore);
                }
                //dt = APCoreProcess.APCoreProcess.Read(sqlStore, storename, new string[0, 0], "");
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = displaymember;
                lue.Properties.ValueMember = valuemember;
                lue.Properties.NullText = "";
                for (int i = 0; i < lue.Properties.View.Columns.Count; i++)
                {
                    lue.Properties.View.Columns[i].Caption = Function.clsFunction.transLateText(lue.Properties.View.Columns[i].Caption);
                }
                //Add button
                lue.EditValue = lue.Properties.GetKeyValue(0);
            }
            catch
            {
                APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
            }
        }

        #endregion

        #region Button

        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static void FormatButton(SimpleButton btn, string image_name)
        {
            Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
            image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Image_Form\\" + image_name).Clone();
            image.MakeTransparent(Color.Fuchsia);
            btn.Image = resizeImage(image, 32, 32);
            btn.ImageLocation = ImageLocation.TopCenter;
            if (Function.clsFunction._keylience != true)
                Application.Exit();
        }

        public static void FormatButtonImage(SimpleButton btn, string image_name, int height, int width)
        {
            Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
            image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Image_Form\\" + image_name).Clone();
            image.MakeTransparent(Color.Fuchsia);
            btn.Image = resizeImage(image, width, height);
            btn.ImageLocation = ImageLocation.TopCenter;
        }
        public static void FormatBarButtonImage(BarButtonItem btn, Image image_name, int height, int width)
        {
         
            btn.Glyph = resizeImage(image_name, width, height);
            btn.PaintStyle=BarItemPaintStyle.CaptionGlyph;
        }
        public static void FormatBarButtonImage(BarSubItem btn, string image_name, int height, int width)
        {
            Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
            image = (Bitmap)Image.FromFile(Application.StartupPath + "\\Image_Form\\" + image_name).Clone();
            image.MakeTransparent(Color.Fuchsia);
            btn.Glyph = resizeImage(image, width, height);
            btn.PaintStyle=BarItemPaintStyle.CaptionGlyph;
        }
        public static Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();
            return (Image)b;
        }

        public static Image Resize(Image img, float percentage,int height,int width)
        {
            //lấy kích thước ban đầu của bức ảnh
            int originalW = img.Width;
            int originalH = img.Height;

            //tính kích thước cho ảnh mới theo tỷ lệ đưa vào
            //int resizedW = (int)(originalW * percentage);
            //int resizedH = (int)(originalH * percentage);
            int resizedW = height;
            int resizedH = width;
            //tạo 1 ảnh Bitmap mới theo kích thước trên
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //tạo 1 graphic mới từ Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            //vẽ lại ảnh ban đầu lên bmp theo kích thước mới
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //giải phóng tài nguyên mà graphic đang giữ
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }

        public static Image ResizePercent(Image img, float percentage)
        {
            //lấy kích thước ban đầu của bức ảnh
            int originalW = img.Width;
            int originalH = img.Height;

            //tính kích thước cho ảnh mới theo tỷ lệ đưa vào
            int resizedW = (int)(originalW * percentage/100);
            int resizedH = (int)(originalH * percentage/100);
  
            //tạo 1 ảnh Bitmap mới theo kích thước trên
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //tạo 1 graphic mới từ Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            //vẽ lại ảnh ban đầu lên bmp theo kích thước mới
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //giải phóng tài nguyên mà graphic đang giữ
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }
#endregion

        #region LookupEdit

        public static void LoadLookupEditNoneParameter(LookUpEdit lue, string storename, string sqlStore, string valuemember, string displaymember)
        {
            try
            {
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Function.clsFunction.ExcuteProc(storename, new string[0, 0]);
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read(sqlStore);
                }
                //dt = APCoreProcess.APCoreProcess.Read(sqlStore, storename, new string[0, 0], "");
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = displaymember;
                lue.Properties.ValueMember = valuemember;
                lue.Properties.NullText = "";
                for (int i = 0; i < lue.Properties.Columns.Count; i++)
                {
                    lue.Properties.Columns[i].Caption = Function.clsFunction.transLateText(lue.Properties.Columns[i].Caption);
                }
                //Add button
                lue.ItemIndex = 0;
            }
            catch
            {
                APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
            }

        }

        public static void LoadLookupEditParameter(LookUpEdit lue, string storename, string sqlStore, string[,] Param, string valuemember, string displaymember)
        {
            try
            {
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    
                    dt = Function.clsFunction.ExcuteProc(storename, Param);
                    if (dt==null)
                        APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read(sqlStore);
                }
                //dt = APCoreProcess.APCoreProcess.Read(sqlStore, storename, new string[0, 0], "");
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = displaymember;
                lue.Properties.ValueMember = valuemember;
                lue.Properties.NullText = "";
                for (int i = 0; i < lue.Properties.Columns.Count; i++)
                {
                    lue.Properties.Columns[i].Caption = Function.clsFunction.transLateText(lue.Properties.Columns[i].Caption);
                }
                //Add button
                lue.ItemIndex = 0;
            }
            catch
            {
                APCoreProcess.APCoreProcess.ExcuteSQL(sqlStore);
            }

        }

        public static void LoadLookupEdit(LookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string form_name)
        {
            lue.Properties.Columns.Clear();
            DataTable dt=new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(sql);
    
            lue.Properties.DataSource = dt;
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;
            lue.Properties.Columns.Clear();
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                lue.Properties.Columns.Add(col);
                lue.ItemIndex = 0;
            }
            lue.Properties.Columns[0].Width = 12;
            lue.Properties.NullText = "";
            //Add button
            if (image_name != "")
            {
                lue.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
              
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Properties.Buttons.Add(ebtn);
            }
            //lue.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(Properties_ButtonClick);
            // Save lookupEdit
            SaveLookupEdit(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form,image_name,form_name);
        }
        
        private static void SaveLookupEdit(LookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name,string formname)
        {
            if (status_Form == true)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysControls");
                try
                {                  
                        DataRow dr = dt.Rows[index_edit(lue.Name,formname)];
                        dr["sql_lue"] = sql;
                        dr["displaymember_lue"] = caption;
                        dr["valuemember_lue"] = value;
                        dr["caption_col_lue_VI"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                        dr["caption_col_lue_EN"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                        dr["fieldname_col_lue"] = Function.clsFunction.ConvertArrayToString(fieldname_lue_col);
                        dr["image"] = (image_name);
                        APCoreProcess.APCoreProcess.Save(dr);
                        MessageBox.Show("Save thành công");
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
                }
            }            
        }

        private static int index_edit(string control_name,string formname)
        {
            int index=-1;
              DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysControls");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                if (dt.Rows[i]["control_name"].ToString().Trim() == control_name.Trim() && dt.Rows[i]["form_name"].ToString().Trim() == formname.Trim())
                {
                    index=i;
                    break;
                }
            }
            return index;
        }

        public static void LoadLookupEditAll(LookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name, string form_name)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(sql);
            DataRow dr = dt.NewRow();
            dr[value] = "";
            dr[caption] = "ALL";
            dt.Rows.Add(dr);
            DataView dv=dt.DefaultView;
            dv.Sort = value;
            lue.Properties.DataSource =   dv.ToTable();
            lue.Properties.DisplayMember = caption;
            lue.Properties.ValueMember = value;
            lue.Properties.Columns.Clear();
            lue.Properties.PopupFormMinSize=new Size(150,300);
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                lue.Properties.Columns.Add(col);
                lue.ItemIndex = 0;
            }
            //lue.Properties.Columns[0].Width = 12;
            lue.Properties.Columns[0].Visible=false;
            lue.Properties.NullText = "";
            //Add button
            if (image_name != "" && 1==2)
            {
                lue.Properties.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile("\\..\\..\\" + Application.StartupPath + "\\Resources\\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Properties.Buttons.Add(ebtn);
            }
            //lue.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(Properties_ButtonClick);
            // Save lookupEdit
            SaveLookupEdit(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name, form_name);
        }

        public static void setOption(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit res)
        {
            res.DataSource = APCoreProcess.APCoreProcess.Read("select id,_option from sysOption", "frm_Trace_SHbbi_tuychon_S", new string[0, 0], "");
            res.ValueMember = "id";
            res.DisplayMember = "_option";
            res.ShowHeader = false;
            LookUpColumnInfo col = new LookUpColumnInfo();
            col.Caption = "ID";
            col.FieldName = "id";
            col.Visible = false;
            res.Columns.Add(col);
            LookUpColumnInfo col1 = new LookUpColumnInfo();
            col1.Caption =Function.clsFunction.transLateText( "Tùy chọn");
            col1.FieldName = "_option";
            col1.Visible = true;
            res.Columns.Add(col1);
            res.AutoSearchColumnIndex = 1;

            
            
            
        }
        public static void res_EditValueChanged(int _case, BarEditItem fromdate,BarEditItem todate )
        {
            int caseSwitch = _case;
            
       
            switch (caseSwitch)
            {
                case 1:
                    fromdate.EditValue = DateTime.Now;
                    todate.EditValue = DateTime.Now;
                    break;
                case 2:
                                        fromdate.EditValue = DateTime.Now.AddDays(-7);
                    todate.EditValue = DateTime.Now;
                    break;
                case 3:
                    fromdate.EditValue = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                    todate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                    break;
                case 4:
      fromdate.EditValue = new DateTime(DateTime.Now.Year,1,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 1, 1).AddMonths(1).AddDays(-1);
                    break;
                case 5:
                         fromdate.EditValue = new DateTime(DateTime.Now.Year,2,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 2, 1).AddMonths(1).AddDays(-1);
                    break;
                case 6:
                        fromdate.EditValue = new DateTime(DateTime.Now.Year,3,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 3, 1).AddMonths(1).AddDays(-1);
                    break;
                case 7:
                      fromdate.EditValue = new DateTime(DateTime.Now.Year,4,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 4, 1).AddMonths(1).AddDays(-1);
                    break;
                case 8:
                         fromdate.EditValue = new DateTime(DateTime.Now.Year,5,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 5, 1).AddMonths(1).AddDays(-1);
                    break;
                case 9:
                    fromdate.EditValue = new DateTime(DateTime.Now.Year,6,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 6, 1).AddMonths(1).AddDays(-1);
                    break;
                case 10:
      fromdate.EditValue = new DateTime(DateTime.Now.Year,7,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 7, 1).AddMonths(1).AddDays(-1);
                    break;
                case 11:
      fromdate.EditValue = new DateTime(DateTime.Now.Year,8,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 8, 1).AddMonths(1).AddDays(-1);
                    break;
                case 12:
                        fromdate.EditValue = new DateTime(DateTime.Now.Year,9,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 9, 1).AddMonths(1).AddDays(-1);
                    break;
                case 13:
                      fromdate.EditValue = new DateTime(DateTime.Now.Year,10,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 10, 1).AddMonths(1).AddDays(-1);
                    break;
                case 14:
                      fromdate.EditValue = new DateTime(DateTime.Now.Year,11,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 11, 1).AddMonths(1).AddDays(-1);
                    break;
                case 15:
      fromdate.EditValue = new DateTime(DateTime.Now.Year,12,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 12, 1).AddMonths(1).AddDays(-1);
                    break;
                case 16:
                       fromdate.EditValue = new DateTime(DateTime.Now.Year,1,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 1, 1).AddMonths(3).AddDays(-1);
                    break;
                case 17:
                 fromdate.EditValue = new DateTime(DateTime.Now.Year,4,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 4, 1).AddMonths(3).AddDays(-1);
                    break;
                case 18:
                               fromdate.EditValue = new DateTime(DateTime.Now.Year,7,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 7, 1).AddMonths(3).AddDays(-1);
                    break;
                case 19:
                          fromdate.EditValue = new DateTime(DateTime.Now.Year,10,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 10, 1).AddMonths(3).AddDays(-1);
                    break;
                case 20:
                                     fromdate.EditValue = new DateTime(DateTime.Now.Year,1,1);
      todate.EditValue = new DateTime(DateTime.Now.Year, 1, 1).AddMonths(12).AddDays(-1);
                    break;
            }

        }
        #endregion

        #region Responsitory

        public static void LoadRepositoryItemLookUpEdit(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name, string form_name)
        {
            lue.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            lue.DisplayMember = caption;
            lue.ValueMember = value;
            lue.Columns.Clear();
            for (int i = 0; i < fieldname_lue_col.Length; i++)
            {
                DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                col.Caption = caption_lue_col[i];
                col.FieldName = fieldname_lue_col[i];
                lue.Columns.Add(col);   
                
            }
            lue.NullText = "";
            //Add button
            if (image_name != "")
            {
                lue.Buttons.Clear();
                DevExpress.XtraEditors.Controls.EditorButton ebtn = new DevExpress.XtraEditors.Controls.EditorButton();
                Bitmap image;//= (Bitmap)DMDVT.Properties._32px_Crystal_Clear_action_exit.Clone();
                image = (Bitmap)Image.FromFile(Application.StartupPath + @"\..\..\Resources\" + image_name).Clone();
                image.MakeTransparent(Color.Fuchsia);
                ebtn.Image = resizeImage(image, 16, 16);
                ebtn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                lue.Buttons.Add(ebtn);
            }
            //lue.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(Properties_ButtonClick);
            // Save lookupEdit
            SaveRepositoryItemLookUpEdit(lue, sql, caption, value, caption_lue_col, fieldname_lue_col, status_Form, image_name, form_name);
        }

        private static void SaveRepositoryItemLookUpEdit(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lue, string sql, string caption, string value, string[] caption_lue_col, string[] fieldname_lue_col, bool status_Form, string image_name, string formname)
        {
            if (status_Form == true)
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysControls");
                try
                {
                    DataRow dr = dt.Rows[index_edit(lue.Name, formname)];
                    dr["sql_lue"] = sql;
                    dr["displaymember_lue"] = caption;
                    dr["valuemember_lue"] = value;
                    dr["caption_col_lue_VI"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["caption_col_lue_EN"] = Function.clsFunction.ConvertArrayToString(caption_lue_col);
                    dr["fieldname_col_lue"] = Function.clsFunction.ConvertArrayToString(fieldname_lue_col);
                    dr["image"] = (image_name);
                    APCoreProcess.APCoreProcess.Save(dr);
                    MessageBox.Show("save thanh cong");
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
                }
            }
        }



        #endregion

        #region Treelist

        public static void Controls_in_Tree(TreeList gv, bool read_Only, bool hien_Nav, string[] column_summary, bool show_footer, string sql, string[] caption_col, string[] fieldname_col, string[] Column_Width, string[] AllowFocus, string[] styleColumn, string[] Column_Visible, string[] sql_lue, string[] caption_lue, string[] fieldname_lue, string[,] fieldname_lue_col, string[,] caption_lue_col, string[] fieldname_lue_visible, int cot_lue_search, string[] sql_glue, string[] caption_glue, string[] fieldname_glue, string[,] fieldname_glue_col, string[,] caption_glue_col, string[] fieldname_glue_visible, int cot_glue_search)
        {
            LoadTreeView(gv, hien_Nav, column_summary, show_footer, sql, caption_col, fieldname_col, Column_Width, AllowFocus);
            int lan_glue = 0;
            int lan_lue = 0;
            for (int i = 0; i < (fieldname_col.Length - 1); i++)
            {
           
                switch (GiaiMaMang(styleColumn[i]))
                {
                    case 1:
                        {
                            RepositoryItemTextEdit rpCrt = new RepositoryItemTextEdit();
                            gv.RepositoryItems.Add(rpCrt);
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 2:
                        {
                            RepositoryItemCheckEdit rpCrt = new RepositoryItemCheckEdit();
                            gv.RepositoryItems.Add(rpCrt);
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 3:                      
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                            rpCrt.DataSource = APCoreProcess.APCoreProcess.Read(sql_lue[lan_lue]);
                            rpCrt.DisplayMember = caption_lue[lan_lue];
                            rpCrt.ValueMember = fieldname_lue[lan_lue];
                            for (int j = 0; j < fieldname_lue_col.GetLength(0); j++)
                            {
                                try
                                {
                                    for (int h = 0; h < fieldname_lue_col.GetLength(1); h++)
                                    {
                                        DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                                        col.FieldName = fieldname_lue_col[lan_lue, h];
                                        col.Caption = caption_lue_col[lan_lue, h];
                                        rpCrt.Columns.Add(col);
                                        for (int k = 0; k < fieldname_lue_visible.Length; k++)
                                            if (col.FieldName.ToString() == fieldname_lue_visible[k])
                                                col.Visible = false;
                                        gv.Columns[i].ColumnEdit = rpCrt;
                                        rpCrt.AutoSearchColumnIndex = cot_lue_search;
                                        rpCrt.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                                        rpCrt.NullValuePrompt = "";
                                        rpCrt.NullText = "";

                                    }
                                    break;
                                }
                                catch (Exception ex) 
                                {
                                    MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                };

                            }

                            lan_lue++;
                            break;
                        };

                    case 4:
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "n0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 5:
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "n0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 6://TimeEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 7://MemoColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
                            rpCrt.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                            rpCrt.ShowIcon = false;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 8://DateColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            rpCrt.Mask.EditMask = "dd/MM/yyyy";
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 9://GridLookupEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
                            rpCrt.DataSource = APCoreProcess.APCoreProcess.Read(sql_glue[lan_glue]);
                            rpCrt.DisplayMember = caption_glue[lan_glue];
                            rpCrt.ValueMember = fieldname_glue[lan_glue];
                            for (int j = 0; j < fieldname_glue_col.GetLength(0); j++)
                            {
                                for (int h = 0; h < fieldname_glue_col.GetLength(1); h++)
                                {
                                    DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                                    col.Caption = caption_glue_col[lan_glue, h];
                                    col.FieldName = fieldname_glue_col[lan_glue, h];

                                    col.VisibleIndex = h;
                                    for (int k = 0; k < fieldname_glue_visible.Length; k++)
                                        if (col.FieldName.ToString() == fieldname_glue_visible[k])
                                            col.Visible = false;
                                    rpCrt.View.Columns.Add(col);
                                    rpCrt.View.ActiveFilter.Clear();
                                }

                                break;
                            }
                            //rpCrt.View.OptionsView.ShowAutoFilterRow = true;

                            gv.Columns[i].ColumnEdit = rpCrt;

                            lan_glue++;
                            break;
                        }

                    case 11://SpinEditColumn
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }

                    case 12://HyperLinkEdit
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
                            rpCrt.OpenLink += new OpenLinkEventHandler(rpCrt_OpenLink);

                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    case 13:
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rpCrt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                            rpCrt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                            rpCrt.Mask.EditMask = "P0";
                            rpCrt.Mask.UseMaskAsDisplayFormat = true;
                            gv.Columns[i].ColumnEdit = rpCrt;
                            break;
                        }
                    default:
                        break;
                }

        
                gv.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                gv.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gv.Columns[i].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gv.Columns[i].AppearanceHeader.Options.UseFont = true;
                gv.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                gv.Columns[i].Visible = Convert.ToBoolean(Column_Visible[i]);
                gv.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                gv.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gv.Columns[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gv.Columns[i].AppearanceHeader.Options.UseFont = true;
                gv.Columns[i].AppearanceHeader.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        public static void LoadTreeView(TreeList gv, bool hien_Nav, string[] column_summary, bool show_footer, string sql, string[] caption_col, string[] fieldname_col, string[] Column_Width, string[] AllowFocus)
        {
            if (gv.DataSource != null)
            {
                gv.Columns.Clear();
            }
            gv.BeginUpdate();
            for (int i = 0; i < (fieldname_col.Length - 1); i++)
            {
                TreeListColumn gc = gv.Columns.Add();
                gc.Caption = caption_col[i];
                gc.FieldName = fieldname_col[i];
                gc.Name = fieldname_col[i];
                gc.Width = int.Parse(Column_Width[i]);
                gc.OptionsColumn.AllowFocus = bool.Parse(AllowFocus[i]);
                gc.VisibleIndex = i;
                gc.Visible = true;
            }
            gv.EndUpdate();
            if ((gv.DataSource == null) && (sql != ""))
            {
                gv.DataSource = APCoreProcess.APCoreProcess.Read(sql);
            }
        }

        #endregion
    }
}
