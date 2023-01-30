using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraPrinting;
using System.Globalization;
using System.Drawing.Imaging;
using System.IO;

namespace DEBTRECEIVABLE.Presentation
{
    public partial class report : DevExpress.XtraEditors.XtraForm
    {
        public report()
        {
            InitializeComponent();
        }

        #region Var

          
                public string table = "";
                public bool statusForm = false;
                public string langues = "_VI";
                public string sql_report="";
                public string maphieunhap = "";    
                public string report_name = "";
                public string img_logo = "logo.png";
                public double sotien = 0,tienlai=0;          
                public string nguoilapphieu = "";
                public string maphieuchi = "";
                public string lydo = "";
                public int img_logo_width;
                public int img_logo_height;
   
        #endregion

        #region Load

        private void report_Load(object sender, EventArgs e)
        {
            //statusForm = true;
      
            xrp_phieuthu_S rpt = new xrp_phieuthu_S();
          
            rpt.langues = langues;
            rpt.statusForm = statusForm;
            rpt.img_logo_height = img_logo_height;
            rpt.img_logo_width = img_logo_width;
            rpt.maphieunhap = maphieunhap;
            rpt.maphieuchi = maphieuchi;
            rpt.sotien = sotien;
            rpt.nguoilapphieu = nguoilapphieu;
            rpt.lydo = lydo;
            rpt.DataSource = ShowData();
            rpt.img_logo = "logo.png";
            print_Control_1.PrintingSystem = rpt.PrintingSystem;
            rpt.BindData();
            report_name = rpt.report_name;
            rpt.CreateDocument();
            Format(rpt);          
        }
        private void Format(DevExpress.XtraReports.UI.XtraReport report)
        {
            PrintingSystemBase printingSystem1 = report.PrintingSystem;
            // Obtain the current export options.
            ExportOptions options = printingSystem1.ExportOptions;
            // Set Print Preview options.
            options.PrintPreview.ActionAfterExport = ActionAfterExport.AskUser;
            string form_name=report.ToString();
            string[]mang=form_name.Split('.');
            DataTable dt = new DataTable();
            
            dt = APCoreProcess.APCoreProcess.Read("select * from sysConfig");
            if (dt.Rows.Count > 0)
            {
                options.PrintPreview.DefaultDirectory = dt.Rows[0]["config"].ToString();
                if (DirExists(dt.Rows[0]["config"].ToString().Trim())==false)                
                {                    
                    saveFileDialog1.FileName = dt.Rows[0]["config"].ToString();                    
              
                    //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (!saveFileDialog1.FileName.Equals(String.Empty))
                        {
                            FileInfo f = new FileInfo(saveFileDialog1.FileName);
                            if (saveFileDialog1.FileName.Trim() != dt.Rows[0]["config"].ToString().Trim())
                                Function.clsFunction.UpdateSysConfig(report, "path", f.FullName);
                            options.PrintPreview.DefaultDirectory = f.FullName;
                        }
                    }
                }                
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!saveFileDialog1.FileName.Equals(String.Empty))
                    {
                        FileInfo f = new FileInfo(saveFileDialog1.FileName);
                        saveFileDialog1.Title = "Report";
                        saveFileDialog1.FileName = "Report";
                        saveFileDialog1.InitialDirectory = Application.StartupPath + "\\LoyaltySM";
                        Function.clsFunction.saveSysConfig(report, "path", f.FullName);
                        options.PrintPreview.DefaultDirectory = f.FullName;
                    }
                }
            }            
            options.PrintPreview.DefaultFileName = report_name;         
            options.PrintPreview.SaveMode = SaveMode.UsingDefaultPath;
            options.PrintPreview.ShowOptionsBeforeExport = false;

            // Set E-mail options.
            options.Email.RecipientAddress = "someone@somewhere.com";
            options.Email.RecipientName = "Someone";
            options.Email.Subject = "Test";
            options.Email.Body = "Test";

            // Set CSV-specific export options.
            options.Csv.Encoding = Encoding.Unicode;
            options.Csv.Separator =
            CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();

            // Set HTML-specific export options.
            options.Html.CharacterSet = "UTF-8";
            options.Html.RemoveSecondarySymbols = false;
            options.Html.Title = "Test Title";

            // Set Image-specific export options.
            options.Image.Format = ImageFormat.Jpeg;

            // Set MHT-specific export options.
            options.Mht.CharacterSet = "UTF-8";
            options.Mht.RemoveSecondarySymbols = false;
            options.Mht.Title = "Test Title";

            // Set PDF-specific export options.
            options.Pdf.Compressed = true;
            options.Pdf.ImageQuality = PdfJpegImageQuality.High;
            options.Pdf.NeverEmbeddedFonts = "Tahoma;Courier New";
            options.Pdf.DocumentOptions.Application = "Test Application";
            options.Pdf.DocumentOptions.Author = "Test Team";
            options.Pdf.DocumentOptions.Keywords = "Test1, Test2";
            options.Pdf.DocumentOptions.Subject = "Test Subject";
            options.Pdf.DocumentOptions.Title = "Test Title";

            // Set Text-specific export options.
            options.Text.Encoding = Encoding.Unicode;
            options.Text.Separator =
            CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();

            // Set XLS-specific export options.
            options.Xls.ShowGridLines = true;
            options.Xls.SheetName = "Page 1";
            options.Xls.TextExportMode = TextExportMode.Value;

            // Set XLSX-specific export options.
            options.Xlsx.ShowGridLines = true;
            options.Xlsx.SheetName = "Page 1";
            options.Xlsx.TextExportMode = TextExportMode.Value;
            options.PrintPreview.ShowOptionsBeforeExport=true;
    }
        private static bool DirExists(string sDirName)

        {
            try
            {
                return (System.IO.Directory.Exists(Application.StartupPath+"\\LoyaltyHRM"));    //Check for file
            }
            catch (Exception)
            {
                return (false);                                 //Exception occured, return False
            }
        }
       private DataTable ShowData()
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read(sql_report);
            return dt;
        } 
        private static bool FileExists(string sPathName)
        {
            try
            {
                return (System.IO.Directory.Exists(sPathName));  //Exception for folder
            }
            catch (Exception)
            {
                return (false);                                   //Error occured, return False
            }
        }

        #endregion
    }
}