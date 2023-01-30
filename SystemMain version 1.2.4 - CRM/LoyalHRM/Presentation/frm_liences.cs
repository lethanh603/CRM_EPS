//-HDD-SNSD-SNDSD-NHH
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LoyalHRM.Presentation
{
    public partial class frm_liences : DevExpress.XtraEditors.XtraForm
    {
        public frm_liences()
        {
            InitializeComponent();
        }

        private void frm_liences_Load(object sender, EventArgs e)
        {
            Function.clsFunction.TranslateForm(this, this.Name);
            txt_madangky_S.Text = getStringSerialNumber();
            checkSysActive();
        }
        private string getStringSerialNumber()
        {
            string sr = "";
            sr = SECURITY.clsSecurity.GetHDDSerialNumber("C");        
            sr += "-" + (Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now)-1) * 7;
            sr += "-" + (Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now)-5) * 9;
            sr += "-" + Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now) * 5;
            return sr;
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_createcode_S_Click(object sender, EventArgs e)
        {
            createKey(txt_masudung_S.Text);
        }
        public void checkSysActive()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysA where A1='" + SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")) + "'");
            if (dt.Rows.Count == 0)
            {           
                return;      
            }
            else
            {
                if (dt.Rows[0][0].ToString() != SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")))
                {             
                    return;            
                }
                else
                {
                    try
                    {
                        Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][1].ToString())));
                    }
                    catch
                    {                
                        return;                  
                    }
                    try
                    {
                        Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][4].ToString())));
                    }
                    catch
                    {                 
                        return;
                    }
                    try
                    {
                        if (Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][2].ToString()).ToString().Substring(0, 4)) < Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][3].ToString()).ToString().Substring(0, 4)))
                        {                     
                            return;
                        }
                        else
                        {
                            btn_register_S.Enabled = false;
                        }
                    }
                    catch
                    {             
                        return;
                    }
                }
            }
        }
        private void createKey(string str)
        {
            try
            {
                str = SECURITY.clsSecurity.convertStringToNumberOrChar(str);
               
                string[] arr = str.Split('-');
                if (arr.Length == 5)
                {
                    string A1 = SECURITY.clsSecurity.EC_CL( (arr[0]));//HDd
                    string A2 = SECURITY.clsSecurity.EC_CL(arr[1]); // ngay cài đặt 
                    string A3 = SECURITY.clsSecurity.EC_CL(arr[2]); // sô ngay su dung 
                    string A4 = SECURITY.clsSecurity.EC_CL(arr[3]);// so ngay da su dung
                    string A5 = SECURITY.clsSecurity.EC_CL(arr[4]); // ngayhien hanh
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("sysA");
                    if (!checkExitsKey(A1))
                    {
                        DataRow dr = dt.NewRow();
                        dr["A1"] = A1;
                        dr["A2"] = A2;
                        dr["A3"] = A3;
                        dr["A4"] = A4;
                        dr["A5"] = A5;
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    else
                    {
                        DataRow dr = dt.Rows[Function.clsFunction.getIndexIDinTable(A1, "A1", dt)];
                        dr["A1"] = A1;
                        dr["A2"] = A2;
                        dr["A3"] = A3;
                        dr["A4"] = A4;
                        dr["A5"] = A5;
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    Function.clsFunction.MessageInfo("Thông báo", "Đăng ký thành công, chương trình sẽ khởi động lại ");
                        Application.Restart();
                }
            }
            catch
            {
                Function.clsFunction.MessageInfo("Key không hợp lệ","Thông báo");
                Application.Restart();
            }            
        }

        private bool checkExitsKey(string A)
        {
            bool a = false;
            if (APCoreProcess.APCoreProcess.Read("sysA where A1='" + A + "'").Rows.Count > 0)
            {
                a = true;
            }
            return a;
        }

        private void btn_register_S_Click(object sender, EventArgs e)
        {
            createKey(txt_masudung_S.Text);
        }

        private void frm_liences_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}