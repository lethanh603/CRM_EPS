using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Views.Grid;


namespace Function
{
    public class clsImportExcel
    {
        #region Variables
        private OleDbConnection conn;
        private String sConnectionString;
        #endregion

        #region Constructor

        public clsImportExcel(String file_readed, int versionExcel)
        {
            if (versionExcel == 2003)
            {
                sConnectionString = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source = " + file_readed + "; Extended Properties=Excel 8.0;;IMEX=1";
            }
            if (versionExcel == 2007)
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_readed + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";              
            }
            if (versionExcel == 2010)
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_readed + ";    Extended Properties='Excel 12.0 Xml;HDR=YES;;IMEX=1'";
            }
            if (versionExcel == 2013)
            {
                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_readed + ";    Extended Properties='Excel 12.0 Xml;HDR=YES;;IMEX=1'";
            }
        }

        public clsImportExcel(String file_readed)
        {          
             sConnectionString = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source = " + file_readed + "; Extended Properties=Excel 8.0;";
        }

        #endregion

        #region Functions

        public System.Data.DataTable getDataFromExcel()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                conn = new System.Data.OleDb.OleDbConnection(sConnectionString);
                conn.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand("SELECT * FROM [Sheet1$]", conn);
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                conn.Close();
            }
            catch 
            {
                MessageBox.Show("Đặt tên sheet trong file excel là Sheet1 và đóng file sau khi đổi tên sheet", ".: Thông Báo :.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;                
            }
            return dt;
        }

        public System.Data.DataTable getDataFromExcel(int sheetIndex)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dtSheet = new DataTable();
            try
            {
                conn = new System.Data.OleDb.OleDbConnection(sConnectionString);
                conn.Open();
                dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);   
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand("SELECT * FROM [" + dtSheet.Rows[sheetIndex]["TABLE_NAME"].ToString() + "]", conn);
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                conn.Close();
            }
            catch
            {
                Function.clsFunction.MessageInfo( ".: Thông Báo :.","Vui lòng kiểm tra lại kết nối");
                dt = null;
            }
            return dt;
        }

        #endregion

        #region Export

        static public bool exportDataToExcel(string tieude, DataTable dt)
        {
            bool result = false;
            //khoi tao cac doi tuong Com Excel de lam viec
            Excel.Application xlApp;
            Excel.Worksheet xlSheet;
            Excel.Workbook xlBook;
            //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
            object missValue = System.Reflection.Missing.Value;
            //khoi tao doi tuong Com Excel moi
            xlApp = new Excel.Application();
 
            xlBook = xlApp.Workbooks.Add(missValue);
            //su dung Sheet dau tien de thao tac
            xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
            //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
            xlApp.Visible = false;
            int socot = dt.Columns.Count;
            int sohang = dt.Rows.Count;
            int i, j;

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                for (i = 0; i < socot; i++)
                    xlSheet.Cells[2, i + 2] = dt.Columns[i].ColumnName;
                //dien cot stt
                xlSheet.Cells[2, 1] = "STT";
                for (i = 0; i < sohang; i++)
                    xlSheet.Cells[i + 3, 1] = i + 1;
                //dien du lieu vao sheet


                for (i = 0; i < sohang; i++)
                    for (j = 0; j < socot; j++)
                    {
                        xlSheet.Cells[i + 3, j + 2] = dt.Rows[i][j];

                    }
                //autofit độ rộng cho các cột 

                //for (i = 0; i < sohang; i++)
                //    ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

                //save file
                xlBook.SaveAs(f.FileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlBook.Close(true, missValue, missValue);
                xlApp.Quit();

                // release cac doi tuong COM
                releaseObject(xlSheet);
                releaseObject(xlBook);
                releaseObject(xlApp);
                result = true;
            }
            return result;
        }

        static public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static void exportExcelTeamplate(DataTable dt, string path, GridView gv, string title, Form F)
        {
            try
            {
                int iCol = 0;
                iCol = gv.Columns.Count ;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook;
                Microsoft.Office.Interop.Excel.Worksheet worksheet;
                xlApp.Visible = true;
                workbook = xlApp.Workbooks.Open(path);        
                worksheet = workbook.Worksheets[1];
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Merge();
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Font.Bold = true;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Font.Size = 15;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Font.Color = System.Drawing.Color.DarkRed;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].HorizontalAlignment = -4108;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, iCol]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                workbook.Worksheets[1].Cells[1, 1] = title.ToUpper();
                for (int i = 1; i <= gv.Columns.Count ; i++)
                {
                    worksheet = workbook.Worksheets[1];
                    worksheet.Range[worksheet.Cells[2, i], worksheet.Cells[2, i]].Font.Size = 11;
                    worksheet.Range[worksheet.Cells[2, i], worksheet.Cells[2, i]].Font.Color = System.Drawing.Color.Blue;
                    worksheet.Range[worksheet.Cells[2, i], worksheet.Cells[2, i]].HorizontalAlignment = -4108;
                    worksheet.Range[worksheet.Cells[2, i], worksheet.Cells[2, i]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                    worksheet.Range[worksheet.Cells[2, i], worksheet.Cells[2, i]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    workbook.Worksheets[1].Cells[2, i] = gv.Columns[i-1].Caption;
                }
                for (int i = 1; i <= gv.RowCount; i++)
                {                   
                    // cot
                    for (int j = 1; j <= gv.Columns.Count; j++)
                    {
                        worksheet = workbook.Worksheets[1];
                        worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i+2, j]].Font.Size = 10;
                        worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i+2, j]].Font.Color = System.Drawing.Color.Black;
                        worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        //worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i + 2, j]].Style.NumberFormat = "";
                        //MessageBox.Show( gv.Columns[j].DisplayFormat.FormatString);
                        worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i+2, j]].Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                        worksheet.Range[worksheet.Cells[i + 2, j], worksheet.Cells[i+2, j]].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        workbook.Worksheets[1].Cells[i+2, j] = gv.GetRowCellValue(i-1, gv.Columns[j-1].FieldName).ToString();
                    }
                }
                worksheet.Columns.AutoFit();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion
    }
}
