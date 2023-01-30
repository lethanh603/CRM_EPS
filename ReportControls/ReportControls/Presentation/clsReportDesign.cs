using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.Drawing.Imaging;
using DevExpress.XtraReports.Extensions;

namespace ReportControls.Presentation
{
    public class clsReportDesign
    {
        static clsReportDesign()
        {
            ReportDesignExtension.RegisterExtension(new CustomReportDesignExtension(), "SomeName");
        }

        #region Var
        static Byte[] bytBLOBData = null;
        #endregion
        
        public static MemoryStream StoreReportToStreamOpen(XtraReport report,string path,  string name,int  id, string sql, bool styleRinbon)
        {         
            MemoryStream stream = new MemoryStream();
            {               
                string strfolder =path;
                if (!Directory.Exists(strfolder))
                {
                    Directory.CreateDirectory(strfolder);
                }                
                    report.SaveLayout(strfolder + "\\" + name + ".repx");                 
                    MemoryStream ms = new MemoryStream();
                    bytBLOBData = FileToByteArray(strfolder + "\\" + name + ".repx");
                    ms.Position = 0;
                    ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("sysReportDesigns where id="+id+"");
                    DataRow dr = dt.Rows[0];
                    if (bytBLOBData != null)
                        dr["source"] = bytBLOBData;
                    else
                        dr["source"] = null;
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    XtraReport xr = new XtraReport();
                    xr.LoadLayout(strfolder + "\\" + name + ".repx");
                    //xr.DataSource = APCoreProcess.APCoreProcess.Read(sql);
                    //xr = report;
                    ReportDesignTool designTool = new ReportDesignTool(xr);
                    if (styleRinbon == true)
                    {
                        designTool.ShowRibbonDesignerDialog();
                    }
                    else
                    {
                        designTool.ShowDesignerDialog();
                    }

            }
            return stream;
        }

        public static MemoryStream StoreReportToStreamEdit(XtraReport report, string path, string name, int id, string sql, bool styleRinbon)
        {
            MemoryStream stream = new MemoryStream();
            {
                string strfolder = path;
                if (!Directory.Exists(strfolder))
                {
                    Directory.CreateDirectory(strfolder);
                }
                //report.DataSource = APCoreProcess.APCoreProcess.Read(sql);
                report.SaveLayout(strfolder + "\\" + name + ".repx");
                MemoryStream ms = new MemoryStream();
                bytBLOBData = FileToByteArray(strfolder + "\\" + name + ".repx");
                ms.Position = 0;
                ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                //APCoreProcess.APCoreProcess.ExcuteSQL("update nhaphanghoa set hinhanh=" + imageData + " where mahanghoa='" + txt_mahanghoa_IK1.Text + "'");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysReportDesigns where id=" + id + "");
                DataRow dr = dt.Rows[0];
                if (bytBLOBData != null)
                    dr["source"] = bytBLOBData;
                else
                    dr["source"] = null;
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                XtraReport xr = new XtraReport();
                xr.LoadLayout(strfolder + "\\" + name + ".repx");
                
                //xr.DataAdapter = APCoreProcess.APCoreProcess.Read(sql);//18/04/2015

                ReportDesignTool designTool = new ReportDesignTool(xr);
                if (styleRinbon == true)
                {
                    designTool.ShowRibbonDesignerDialog();
                }
                else
                {
                    designTool.ShowDesignerDialog();
                }
            }
            return stream;
        }

        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] buffer = null;
            try
            {
                // Open file for reading 
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                // attach filestream to binary reader 
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                // get total byte length of the file 
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                // read entire file into buffer 
                buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                // close file reader 
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error 
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            return buffer;
        }

        public static void LaunchCommandLineOpen(string arrSQL, string path, string name, int id, XtraReport xrp, bool styleRinbon)
        {
            // Create a parameter and specify its name
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysINFO");
            if (dt.Rows.Count > 0 )
            {              
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "logo")
                    {
                        try
                        {
                            ReportDesignExtension.AssociateReportWithExtension(xrp, "SomeName");
                            MemoryStream ms;
                            if (dt.Rows[0][i] != null)
                            {
                                byte[] pic = (byte[])dt.Rows[0][i];
                                ms = new MemoryStream(pic);
                                ms.Seek(0, SeekOrigin.Begin);
                                Image image = Image.FromStream(ms);
                                Parameter param1 = new Parameter();
                                param1.Name = dt.Columns[i].ColumnName;
                                // Specify other parameter properties.
                                param1.Type = typeof(System.Drawing.Image);
                                param1.Value = image;
                                param1.Description = dt.Columns[i].ColumnName;
                                param1.Visible = true;
                                // Add the parameter to the report.
                                xrp.Parameters.Add(param1);
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        Parameter param1 = new Parameter();
                        param1.Name = dt.Columns[i].ColumnName;
                        // Specify other parameter properties.
                        param1.Type = typeof(System.String);
                        param1.Value = dt.Rows[0][i].ToString();
                        param1.Description = dt.Columns[i].ColumnName;
                        param1.Visible = true;
                        // Add the parameter to the report.
                        xrp.Parameters.Add(param1);
                    }
                }
            }
            StoreReportToStreamOpen(xrp, path, name, id, arrSQL, styleRinbon);      
        }

        public static void LaunchCommandLineAppEdit(string arrSQL, string path, string name, int id, XtraReport xrp, bool styleRinbon)
        {         
            // Create a parameter and specify its name
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysINFO");
            if (dt.Rows.Count > 0 && 1==2)
            {                
                xrp.Parameters.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "logo")
                    {
                        ReportDesignExtension.AssociateReportWithExtension(xrp, "SomeName");
                        MemoryStream ms;
                        byte[] pic = (byte[])dt.Rows[0][i];
                        ms = new MemoryStream(pic);
                        ms.Seek(0,SeekOrigin.Begin);
                        Image image = Image.FromStream(ms); 
                        Parameter param1 = new Parameter();
                        param1.Name = dt.Columns[i].ColumnName;
                        // Specify other parameter properties.
                        param1.Type = typeof(System.Drawing.Image);
                        param1.Value = image;
                        param1.Description = dt.Columns[i].ColumnName;
                        param1.Visible = true;
                        // Add the parameter to the report.
                        xrp.Parameters.Add(param1);                
                    }
                    else
                    {
                        Parameter param1 = new Parameter();
                        param1.Name = dt.Columns[i].ColumnName;
                        // Specify other parameter properties.
                        param1.Type = typeof(System.String);
                        param1.Value = dt.Rows[0][i].ToString();
                        param1.Description = dt.Columns[i].ColumnName;
                        param1.Visible = true;
                        // Add the parameter to the report.
                        xrp.Parameters.Add(param1);
                    }
                }
            }     
         
            StoreReportToStreamEdit(xrp, path, name, id, arrSQL,  styleRinbon);    
        }
    }

    class CustomReportDesignExtension : ReportDesignExtension
    {
        protected override bool CanSerialize(object data)
        {
            return base.CanSerialize(data) || data is Image;
        }
        protected override string SerializeData(object data, XtraReport report)
        {
            if (data is Image)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    ((Image)data).Save(stream, ImageFormat.Png);
                    byte[] buffer = stream.GetBuffer();
                    return Convert.ToBase64String(buffer);
                }
            }
            return base.SerializeData(data, report);
        }
        protected override bool CanDeserialize(string value, string typeName)
        {
            return base.CanDeserialize(value, typeName) || string.Equals(typeName, typeof(Image).FullName);
        }
        protected override object DeserializeData(string value, string typeName, XtraReport report)
        {
            if (string.Equals(typeName, typeof(Image).FullName))
            {
                byte[] buffer = Convert.FromBase64String(value);
                return Image.FromStream(new MemoryStream(buffer));
            }
            return base.DeserializeData(value, typeName, report);
        }
    }
}
