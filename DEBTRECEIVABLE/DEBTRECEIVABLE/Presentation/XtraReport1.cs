using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using DevExpress.XtraReports.UI;
using System.IO;
using ControlDev;
namespace DEBTRECEIVABLE.Presentation
{
    public partial class xrp_phieuthu_S : DevExpress.XtraReports.UI.XtraReport
    {
        public xrp_phieuthu_S()
        {
            InitializeComponent();            
        }
        public string langues = "_VI";
        public bool statusForm = false;
        public string report_name = "";
        public string img_logo = "logo.png";
        public double sotien = 0,tienlai=0;
        public int img_logo_height;
        public string maphieunhap = "";
        public string nguoilapphieu = "";
        public string maphieuchi = "";
        public string lydo = "";
        public int img_logo_width;
        int stt = 0;
        public void BindData()
        {
            string[] name1 = this.ToString().Split('_');
            string text="";
            //statusForm = true;
                text="text"+langues;            
            // insert system report
            if (statusForm == true)
                Function.clsFunction.saveControlReport(this);
            else
            {
                // bindData                 
                Function.clsFunction.BindDataControlReport(this, (DataTable)DataSource, langues);
                //report_name = APCoreProcess.APCoreProcess.Read("select name_file_VI,name_file_EN from sysReportControls where report_name='" + name1[2] + "' and control_name='" + lbl_diachi_S.Name.Trim() + "'").Rows[0][text].ToString();
            }
        }      
        
        private void celStt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    
        private void celSoDM_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {            
  
        }

        private void xrp_khuvuc_S_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from thongtin ");
            DataTable dtPN = new DataTable();
            dtPN = APCoreProcess.APCoreProcess.Read("select * from phieuxuathang,nhapkhachhang where phieuxuathang.makhachhang=nhapkhachhang.makhachhang and maphieuxuat='" + maphieunhap + "' ");
            stt++;
            lbl_tencongty_S.Text = dt.Rows[0]["tendonvi"].ToString();
            lbl_ngaychitien_S.Text = " Ngày  " + DateTime.Now.Day + "  Tháng  " + DateTime.Now.Month + "  Năm  " + DateTime.Now.Year;
            lbl_ngaylapphieu_S.Text = " Ngày  " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;
            lbl_diachi_S.Text = dt.Rows[0]["diachi"].ToString();
            lbl_nguoinhantien_S.Text = dtPN.Rows[0]["tenkhachhang"].ToString();
            lbl_diachinguoinhan_S.Text = dtPN.Rows[0]["diachi"].ToString();
            lbl_tel_S.Text = dt.Rows[0]["dienthoai"].ToString();
            lbl_phieu_S.Text = maphieunhap;
            lbl_phieuchi_S.Text=maphieuchi;
            lbl_sotien_S.Text = sotien.ToString("N0");
            lbl_tienlai_S.Text = tienlai.ToString("N0");
            lbl_tongthu_S.Text = (sotien+tienlai).ToString("N0");
            lbl_tennguoilap_S.Text = nguoilapphieu;
            lbl_sotienchu_S.Text = Function.ConvertNumToStr.So_chu(sotien + tienlai);
            lbl_lydo_S.Text = lydo;
            Byte[] image = (Byte[])(dt.Rows[0]["logo"]);
            Image img = byteArrayToImage(image);
            Size s = new System.Drawing.Size();
            s.Height = img_logo_height;
            s.Width = img_logo_width;
            img = FormatControls.resizeImage(img, s);
            xpt_logo_S.Image = img;

        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
