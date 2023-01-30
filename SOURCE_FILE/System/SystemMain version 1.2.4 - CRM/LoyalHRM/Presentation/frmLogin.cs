using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;

namespace LoyalHRM.Presentation
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor

        public frmLogin()
        {
            InitializeComponent();
        }
        public struct SystemTime
        {
            public int Year;
            public int Month;
            public int DayOfWeek;
            public int Day;
            public int Hour;
            public int Minute;
            public int Second;
            public int Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
        public extern static void Win32GetSystemTime(ref SystemTime sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);
        #endregion

        #region Var
    
        public string langues = "_VI";
        bool load = false;
        #endregion

        #region ButtonEvent

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (APCoreProcess.APCoreProcess.Read("select * from sysUser where username='" + cboTaikhoan_I2.Text + "' and password='" + Function.clsFunction.mahoapw(txtMatkhau_I2.Text) + "' and status=1").Rows.Count > 0)
            {
                Function.clsFunction.langgues = lue_ngonngu_I2.EditValue.ToString();
                Function.clsFunction._iduser = APCoreProcess.APCoreProcess.Read("select userid from sysUser where username='" + cboTaikhoan_I2.EditValue.ToString() + "'").Rows[0]["userid"].ToString();
                if (chk_stylemenu_S.Checked == false)
                {
                    frmMain1 frm = new frmMain1();
                    frm.id_user = APCoreProcess.APCoreProcess.Read("select * from sysUser where username='" + cboTaikhoan_I2.Text + "' and password='" + Function.clsFunction.mahoapw(txtMatkhau_I2.Text) + "'").Rows[0]["userid"].ToString();
                    frm.langues = Function.clsFunction.langgues;
                    frm.Show();
                }
                else
                {
                    frmMain frm = new frmMain();
                    frm.id_user = APCoreProcess.APCoreProcess.Read("select * from sysUser where username='" + cboTaikhoan_I2.Text + "' and password='" + Function.clsFunction.mahoapw(txtMatkhau_I2.Text) + "'").Rows[0]["userid"].ToString();
                    frm.langues = Function.clsFunction.langgues;
                    frm.Show();
                }
                Function.clsFunction._user = cboTaikhoan_I2.Text;
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btnDangnhap.Text), Function.clsFunction._iduser, cboTaikhoan_I2.Text, SystemInformation.ComputerName.ToString(), "4");
                this.Hide();
                //SECURITY.clsSecurity.sysLMT();
                checkSysActive();
                checkInfo();
            }
            else
            {
                Function.clsFunction.MessageInfo("Thông báo", "Sai thông tin đăng nhập");
            }
        }

        private void btnKetthuc_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Methods

        private void SetDateTime()
        {
            try
            {
                // Set system date and time
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss ";
                culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(1053);
                string swedishTime = DateTime.Now.ToShortTimeString();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select CONVERT(DATETIME, getdate(),103)");
                DateTime dte = new DateTime();
                dte = Convert.ToDateTime(dt.Rows[0][0]);
                SystemTime updatedTime = new SystemTime();
                updatedTime.Year = (int)dte.Year;
                updatedTime.Month = (int)dte.Month;
                updatedTime.Day = (int)dte.Day;
                updatedTime.Hour = (int)(dte.Hour);
                updatedTime.Minute = (int)dte.Minute;
                updatedTime.Second = (int)dte.Second;
                // Call the unmanaged function that sets the new date and time instantly
                Win32SetSystemTime(ref updatedTime);
            }
            catch { }
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {  
            
            Function.clsFunction.langgues = "_VI";
            CultureInfo objCI = new CultureInfo("en-US");
            //CultureInfo objCI = new CultureInfo("vi-VN");
            Application.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;
            try
            {
                if (checkConfig())
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml("config.xml");
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["issqlserver"]) == true)
                    {
                        Function.clsFunction.DataBaseName = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["DBName"].ToString());
                        Data.APCoreData.chuoiKetNoi = "Data Source=" + (ds.Tables[0].Rows[0]["server"].ToString()) + ";Database=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["DBName"].ToString()) + "; User ID=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["account"].ToString()) + ";Password=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["password"].ToString()) + ";";
                        //Data.APCoreData.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0][0].ToString() + ";Initial Catalog=" + ds.Tables[0].Rows[0][3].ToString() + ";Integrated Security=True";
                        //Data.APCoreData.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0][0].ToString() + ",1433;Network Library=DBMSSOCN;Initial Catalog=" + ds.Tables[0].Rows[0][3].ToString() + ";User ID=" + ds.Tables[0].Rows[0][1].ToString() + ";Password=" + ds.Tables[0].Rows[0][2].ToString() + ";";
                        APCoreProcess.APCoreProcess.IssqlCe = false;
                    }
                    else
                    {
                        Function.clsFunction.DataBaseName = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["dbnameCE"].ToString());
                        Data.APCoreDataSQLCE.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0]["path"].ToString() + ";Password='" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["passCE"].ToString()) + "';";
                        //Data Source=E:\Libs\Drive\Libs\Data\Data\system.sdf;Password=***********
                        APCoreProcess.APCoreProcess.IssqlCe = true;
                    }
                    System.Data.DataTable dtConfig = new System.Data.DataTable();
                    dtConfig = APCoreProcess.APCoreProcess.Read("sysConfig");
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = dtConfig.Rows[0]["skin"].ToString();
                    Function.clsFunction.langgues = dtConfig.Rows[0]["langues"].ToString();
                    ControlDev.FormatControls.LoadComBoBoxEdit(cboTaikhoan_I2, APCoreProcess.APCoreProcess.Read("sysUser"));
                    Function.clsFunction.dateFormat = dtConfig.Rows[0]["typedate"].ToString();
                    loadNgonNgu();
                    load = true;
                    //Function.clsFunction.Save_sysControl(this, this);
                    Function.clsFunction.Text_Control(this, langues);
                    chk_stylemenu_S.Checked = Convert.ToBoolean(dtConfig.Rows[0]["style"]);
                    setLockUser();
                    Function.clsFunction.TranslateForm(this, this.Name);
                    this.Text = (dtConfig.Rows[0]["softname"].ToString());
                    SetDateTime();
                }
                else
                {
                    frmConfigDatabase frc = new frmConfigDatabase();
                    frc.ShowDialog();
                }     
            }
            catch ( Exception ex)
            {
                frmConfigDatabase frc = new frmConfigDatabase();
                frc.ShowDialog();
                MessageBox.Show(ex.Message);
            }
            //SECURITY.clsSecurity.setLience(120);
        }

        private void setLockUser()
        {
            int day_limt=0;
            day_limt=Convert.ToInt16( APCoreProcess.APCoreProcess.Read("select day_limit from sysConfig").Rows[0][0]);      
            if (day_limt > 0)
            {
                DataTable dt = new DataTable();
                string[,] arr = new string[2, 2] { { "tungay", Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now.AddDays(-day_limt)).ToString() }, { "denngay", Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now).ToString() } };
                //dt = Function.clsFunction.ExcuteProc("checkLockUser", arr);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update sysUser set status=0 where userid='" + dt.Rows[i]["userid"].ToString() + "'");
                    }
                }
            }
        }    

        private bool checkConfig()
        {
            DataSet ds = new DataSet();
            bool flag = false;
            try
            {
                ds.ReadXml("config.xml");
                if (ds.Tables[0].Rows.Count > 0)
                    flag= true;
            }
            catch 
            {
 
            }
            return flag;
        }
     
        private void checkInfo()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1 * from sysINFO");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["MS"]) == 603)
                {
                    frm_sysINFO_S frm = new frm_sysINFO_S();
                    frm.them = false;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                    string str = "";
                    str = SECURITY.clsSecurity.GetHDDSerialNumber("C") + "-" + Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now) + "-" + "0030YUYH" + "-" + "0000UHVF-" + Function.clsFunction.ConVertDatetimeToNumeric(DateTime.Now);
                    createKey(str);
                }
            }
        }

        private void createKey(string str)
        {
            try
            {
                string[] arr = str.Split('-');
                if (arr.Length == 5)
                {
                    string A1 = SECURITY.clsSecurity.EC_CL((arr[0]));
                    string A2 = SECURITY.clsSecurity.EC_CL(arr[1]);
                    string A3 = SECURITY.clsSecurity.EC_CL(arr[2]);
                    string A4 = SECURITY.clsSecurity.EC_CL(arr[3]);
                    string A5 = SECURITY.clsSecurity.EC_CL(arr[4]);
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
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Key không hợp lệ", "Thông báo");
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

        public  void checkSysActive()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysA where A1='" + SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")) + "'");
            if (dt.Rows.Count == 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Thông tin đăng ký không hợp lệ");
                frm_liences frm = new frm_liences();
                frm.ShowDialog();                
            }
            else
            {
                if (dt.Rows[0][0].ToString() != SECURITY.clsSecurity.EC_CL(SECURITY.clsSecurity.GetHDDSerialNumber("C")))
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Thông tin đăng ký không hợp lệ");
                    frm_liences frm = new frm_liences();
                    frm.ShowDialog();
                }
                else
                {
                    try
                    {
                        Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][1].ToString())));
                    }
                    catch
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Thông tin đăng ký không hợp lệ");
                        frm_liences frm = new frm_liences();
                        frm.ShowDialog();
                    }
                    try
                    {                
                        Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][4].ToString())));
                    }
                    catch
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Thông tin đăng ký không hợp lệ");
                        frm_liences frm = new frm_liences();
                        frm.ShowDialog();
                    }
                    try
                    {
                        if (Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][2].ToString()).ToString().Substring(0, 4)) < Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][3].ToString()).ToString().Substring(0, 4)))
                        {
                            Function.clsFunction.MessageInfo( "Thông báo","Thông tin đăng ký không hợp lệ");
                            frm_liences frm = new frm_liences();
                            frm.ShowDialog();
                        }
                        else
                        {
                            if (Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][2].ToString()).ToString().Substring(0, 4)) - Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][3].ToString()).ToString().Substring(0, 4)) < 10)
                            {
                                XtraMessageBox.Show(Function.clsFunction.transLateText( "Thông báo"),Function.clsFunction.transLateText( "Hạn sử dụng còn dưới ")+(Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][2].ToString()).ToString().Substring(0, 4)) - Convert.ToInt32(SECURITY.clsSecurity.DC_CL(dt.Rows[0][3].ToString()).ToString().Substring(0, 4))).ToString()+Function.clsFunction.transLateText(" ngày, vui lòng liên hệ nhà sản xuất") );
                            }
                        }
                    }
                    catch
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Thông tin đăng ký không hợp lệ");
                        frm_liences frm = new frm_liences();
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loadNgonNgu()
        {
            string[]fieldname=new string[]{"kyhieu","ngonngu"};
            string[] caption = new string[] { "Ký hiệu", "Ngôn Ngữ" };
            ControlDev.FormatControls.LoadLookupEdit(lue_ngonngu_I2, "sysLanggues", "ngonngu", "kyhieu", caption, fieldname, false, "", this.Name);
            DataTable conFig = new DataTable();
            conFig = APCoreProcess.APCoreProcess.Read("sysConfig");
            lue_ngonngu_I2.EditValue = conFig.Rows[0]["langues"].ToString();         
            lue_ngonngu_I2.DataBindings.Clear();
            lue_ngonngu_I2.DataBindings.Add("EditValue",conFig,"langues");         
        }

        private void lue_ngonngu_EditValueChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                if(Function.clsFunction.MessageDelete("Thông báo","Bạn có muốn thay đỗi ngôn ngữ ?"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update sysConfig set langues='" + lue_ngonngu_I2.EditValue.ToString() + "'");
                    Function.clsFunction.langgues =Function.clsFunction.giaima( lue_ngonngu_I2.EditValue.ToString());
                }
            }
        }

        private void chk_stylemenu_S_CheckedChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update sysConfig set style='" + chk_stylemenu_S.Checked+ "'");               
            }
        }

        private void btnTuychon_Click(object sender, EventArgs e)
        {
            frmConfigDatabase frc = new frmConfigDatabase();
            frc.TopMost = true;
            frc.TopLevel = true;
            frc.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmExport fr = new frmExport();
            fr.ShowDialog();
        }      

        private void txtMatkhau_KeyDown(object sender, KeyEventArgs e)        
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangnhap_Click(sender, e);
            }
        }

        #endregion

    }
}