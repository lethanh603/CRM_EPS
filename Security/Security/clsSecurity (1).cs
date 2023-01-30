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
using System.Globalization;
using System.Management;

namespace SECURITY
{
    public class clsSecurity
    {
        //A001 
        #region Security

        public static bool checkLience(string str)
        {
            bool flag=false;
            flag=true;
            return flag;
        }

        public static string EC_CL(string str)
        {
            string ECL = "";
            int iC, sC;
            int n = str.Length;
            Encoding ascii = Encoding.ASCII;
            Byte[] a = ascii.GetBytes(str);
            for (int i = 0; i < n; i++)
            {
                iC = Convert.ToInt32(a[i]) + i - 2;
                sC = Convert.ToInt32(a[i]) + i - 7;
                ECL = ECL + Convert.ToChar(iC).ToString() + Convert.ToChar(sC).ToString();
            }
            return ECL;
        }
        public static string DC_CL(string str)
        {
            string DCL = "";
            int iC;
            int n = str.Length;
            Encoding ascii = Encoding.ASCII;
            Byte[] a = ascii.GetBytes(str);
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    iC = Convert.ToInt32(a[i]) - i / 2 + 2;
                    DCL = DCL + Convert.ToChar(iC).ToString();
                }
            }
            return DCL;
        }
        // A001 key A002 xn A003 day cai dat A004 30 A005 count A006 ngay hientai 16112013
        public static void sysLMT()
        {
            try
            {
                int songay=0;
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("AAA");
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa đăng ký bản quyền cho phần mềm, vui lòng liên hệ nhà sản xuất", "Thông báo");
                    Application.Exit();
                }
                else
                {
                    if (dt.Rows[0][1].ToString() != "")
                    {
                        if (Function.clsFunction.mahoapw(dt.Rows[0][0].ToString()) != dt.Rows[0][1].ToString())
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa đăng ký bản quyền cho phần mềm, vui lòng liên hệ nhà sản xuất", "Thông báo");
                            Application.Exit();
                        }
                        else
                        {
                            if (Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][4].ToString()!="" ? dt.Rows[0][4].ToString():".)")) >= Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][3].ToString()!=""?dt.Rows[0][3].ToString():".)")))
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa đăng ký bản quyền cho phần mềm, vui lòng liên hệ nhà sản xuất", "Thông báo");
                                Application.Exit();
                            }
                            else
                            {
                                songay= (-Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][4].ToString()!="" ? dt.Rows[0][4].ToString():".)")) + Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][3].ToString()!=""?dt.Rows[0][3].ToString():".)")));
                                if(songay<=30)
                                {
                                         DevExpress.XtraEditors.XtraMessageBox.Show("Hệ thống sẽ ngừng hoạt động trong "+ songay.ToString() + " nữa. Vui lòng liên hệ nhà sản xuất" , "Thông báo");
                                }
                                Function.clsFunction._keylience=true;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][4].ToString() != "" ? dt.Rows[0][4].ToString() : ".)")) >= Convert.ToInt16(Function.clsFunction.giaima(dt.Rows[0][3].ToString() != "" ? dt.Rows[0][3].ToString() : ".)")))
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa đăng ký bản quyền cho phần mềm, vui lòng liên hệ nhà sản xuất", "Thông báo");
                            Application.Exit();
                        }
                    }
                }
                
            }
            catch
            {
                XtraMessageBox.Show("Bạn chưa đăng ký bản quyền cho phần mềm, vui lòng liên hệ nhà sản xuất", "Thông báo");
            }
        }

        public static void setStart()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("AAA");
            DateTime dte = new DateTime();
            dte = DateTime.Now;
            int a = 0;


            if (dt.Rows.Count == 0)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("insert into AAA (A001,A003,A004) values('" + GetHDDSerialNumber("") + "','" + Function.clsFunction.mahoapw(Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now).ToString()) + "','" + EC_CL(".)".ToString()) + "')");
            }
            else
            {
                a = Convert.ToInt16(Function.clsFunction.giaima( dt.Rows[0][4].ToString()!="" ? dt.Rows[0][4].ToString():".)"));
                if (dte.ToShortDateString() != Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32(Function.clsFunction.giaima(dt.Rows[0][5].ToString()!="" ? dt.Rows[0][5].ToString():".)" ))).ToShortDateString())
                {
                    a++;
                    APCoreProcess.APCoreProcess.ExcuteSQL("update  AAA set A005 =('" + EC_CL(a.ToString()) + "'), A006 ='"+EC_CL(Function.clsFunction.ConVertDatetimeToNumeric(dte).ToString())+"'");
                }
                
            }
        }
        public static void setLience(int day)
        {
            DataTable dt=new DataTable();
            dt=APCoreProcess.APCoreProcess.Read("AAA");         
            APCoreProcess.APCoreProcess.ExcuteSQL("update AAA set A002='"+  SECURITY.clsSecurity.EC_CL(dt.Rows[0][0].ToString())+"', A004='"+EC_CL( day.ToString())+"'");
        }
        public static string GetHDDSerialNumber(string drive)
        {
            //check to see if the user provided a drive letter
            //if not default it to "C"
            if (drive == "" || drive == null)
            {
                drive = "C";
            }
            //create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive + ":\"");
            //bind our management object
            disk.Get();
            //return the serial number

            return disk["VolumeSerialNumber"].ToString();
        }
        #endregion
    
    }
}
