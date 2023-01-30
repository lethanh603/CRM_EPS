using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using APCoreProcess;
using Function;
using System.IO;
using System.Globalization;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using System.Reflection;
using System.Drawing;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;

namespace Function
{

   public class clsFunction
    {

        #region Var
        static string column = "";
        static string key = "";
        public static string _user="";
        public static string _iduser = "US000001";
        public static string langgues="_VI";
        public static int img_logo_height=133;
        public static int img_logo_width=133;
        public static int sotap=0; 
        public static bool _keylience=false;
        public static string DataBaseName = "";
        public static string dateFormat = "dd/MM/yyyy";
        public static bool _pre=false;
        public static DataTable dtTrace = new DataTable();
        public static bool showhideCol = false;
    
        #endregion

        #region Update

        public static DataTable Excute_Proc(string namePro, string[,] arrParameters)
        {
            DataSet dt = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(namePro);
                SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
                {
                    cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
                }
                sqlConnection1.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                //adapt.SelectCommand = cmd;
              
                adapt.Fill(dt);
                sqlConnection1.Close();
                
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
            if (dt.Tables.Count>0 )
                return dt.Tables[0];
            else
                return null;
        }

        public static void sysGrantUserByRole(DevExpress.XtraBars.Bar ctr, string FormName)
        {
            try
            {
                string option="";
                string[] arr = new string[] { };
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Excute_Proc("getRoleUserByRole", new string[2, 2] { { "IDUser", _iduser }, { "FormName", FormName } });
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read("SELECT     sysUser.userid, sysPower.allow_access, sysPower.allow_insert, sysPower.allow_delete, sysPower.allow_edit, sysPower.allow_print,  sysPower.allow_import, sysPower.allow_export, sysSubMenu.FormName FROM         sysPower INNER JOIN sysRole ON sysPower.mavaitro = sysRole.mavaitro INNER JOIN sysUser ON sysRole.mavaitro = sysUser.mavaitro INNER JOIN   sysSubMenu ON sysPower.IDSubMenu = sysSubMenu.IDSubMenu WHERE     (sysUser.userid = '"+ clsFunction._iduser +"') AND (sysSubMenu.FormName = '"+ FormName +"')");
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < ctr.Manager.Items.Count; i++)
                    {
                        arr=ctr.Manager.Items[i].Name.Split('_');
                        option = arr.Length > 1 ? arr[arr.Length - 2] + "_" + arr[arr.Length - 1] : "";
                        switch (option)
                        {
                            case "allow_insert":
                                if (dt.Rows[0]["allow_insert"].ToString() == "True")
                                {
                                    ctr.Manager.Items[i].Enabled = true;
                                    ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                                }
                                else
                                    ctr.Manager.Items[i].Enabled = false;                                                            
                               break;
                            case "allow_edit":
                               if (dt.Rows[0]["allow_edit"].ToString() == "True")
                               {
                                   ctr.Manager.Items[i].Enabled = true;
                                   ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                               }
                               else
                                   ctr.Manager.Items[i].Enabled = false;
                               break;
                            case "allow_delete":
                               if (dt.Rows[0]["allow_delete"].ToString() == "True")
                               {
                                   ctr.Manager.Items[i].Enabled = true;
                                   ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                               }
                               else
                                   ctr.Manager.Items[i].Enabled = false;
                               break;
                            case "allow_import":
                               if (dt.Rows[0]["allow_import"].ToString() == "True")
                               {
                                   ctr.Manager.Items[i].Enabled = true;
                                   ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                               }
                               else
                                   ctr.Manager.Items[i].Enabled = false;
                               break;
                            case "allow_export":
                               if (dt.Rows[0]["allow_export"].ToString() == "True")
                               {
                                   ctr.Manager.Items[i].Enabled = true;
                                   ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                               }
                               else
                                   ctr.Manager.Items[i].Enabled = false;
                               break;
                            case "allow_print":
                               if (dt.Rows[0]["allow_print"].ToString() == "True")
                               {
                                   ctr.Manager.Items[i].Enabled = true;
                                   ctr.Manager.Items[i].Caption = clsFunction.transLateText(ctr.Manager.Items[i].Caption);
                               }
                               else
                                   ctr.Manager.Items[i].Enabled = false;
                               break;
                            default :
                               ctr.Manager.Items[i].Enabled = true;
                               break;
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {

            }
            

        }

        public static void sysGrantUserByRole(PanelControl ctr, string FormName)
        {
            try
            {
                string option = "";
                string[] arr = new string[] { };
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = Excute_Proc("getRoleUserByRole", new string[2, 2] { { "IDUser", _iduser }, { "FormName", FormName } });
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read("SELECT     sysUser.userid, sysPower.allow_access, sysPower.allow_insert, sysPower.allow_delete, sysPower.allow_edit, sysPower.allow_print,  sysPower.allow_import, sysPower.allow_export, sysSubMenu.FormName FROM         sysPower INNER JOIN sysRole ON sysPower.mavaitro = sysRole.mavaitro INNER JOIN sysUser ON sysRole.mavaitro = sysUser.mavaitro INNER JOIN   sysSubMenu ON sysPower.IDSubMenu = sysSubMenu.IDSubMenu WHERE     (sysUser.userid = '" + clsFunction._iduser + "') AND (sysSubMenu.FormName = '" + FormName + "')");
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < ctr.Controls.Count; i++)
                    {
                        arr = ctr.Controls[i].Name.Split('_');
                        option = arr.Length > 1 ? arr[arr.Length - 2] + "_" + arr[arr.Length - 1] : "";
                        switch (option)
                        {
                            case "allow_insert":
                                if (dt.Rows[0]["allow_insert"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            case "allow_edit":
                                if (dt.Rows[0]["allow_edit"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            case "allow_delete":
                                if (dt.Rows[0]["allow_delete"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            case "allow_import":
                                if (dt.Rows[0]["allow_import"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            case "allow_export":
                                if (dt.Rows[0]["allow_export"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            case "allow_print":
                                if (dt.Rows[0]["allow_print"].ToString() == "True")
                                    ctr.Controls[i].Enabled = true;
                                else
                                    ctr.Controls[i].Enabled = false;
                                break;
                            default:

                                ctr.Controls[i].Enabled = true;
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }


        }

        public static void Delete(Form F, DevExpress.XtraGrid.Views.Grid.GridView GvDM)
        {
            DataTable dtsysMessage = new DataTable();
            dtsysMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
            DialogResult dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtsysMessage.Rows[6]["message" + langgues].ToString(), dtsysMessage.Rows[6]["title" + langgues].ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dl == DialogResult.OK)
            {
                DataTable dt = new DataTable();                
                dt = APCoreProcess.APCoreProcess.Read(getNameControls(F.Name));
                DataRow dr;
                int a = GvDM.FocusedRowHandle;
                //dr = dt.Rows[GvDM.FocusedRowHandle];
                dr = dt.Rows.Find(GvDM.GetRowCellValue(GvDM.FocusedRowHandle,GvDM.Columns[0].Name));
                dr.Delete();
                APCoreProcess.APCoreProcess.Save(dr);
                if (Function.clsFunction._keylience != true)
                    Application.Exit();
            }
        }

        public static void Delete(Form F, DevExpress.XtraGrid.Views.Grid.GridView GvDM,int index)
        {
            DataTable dtsysMessage = new DataTable();
            dtsysMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
            DialogResult dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtsysMessage.Rows[6]["message" + langgues].ToString(), dtsysMessage.Rows[6]["title" + langgues].ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dl == DialogResult.OK)
            {
                DataTable dt = new DataTable();

                dt = APCoreProcess.APCoreProcess.Read(getNameControls(F.Name));
                DataRow dr;
                int a = GvDM.FocusedRowHandle;
                //dr = dt.Rows[GvDM.FocusedRowHandle];
                dr = dt.Rows.Find(GvDM.GetRowCellValue(GvDM.FocusedRowHandle, GvDM.Columns[index].Name));
                dr.Delete();
                APCoreProcess.APCoreProcess.Save(dr);
                if (Function.clsFunction._keylience != true)
                    Application.Exit();
            }
        }

        public static void Delete_M(string table, DevExpress.XtraGrid.Views.Grid.GridView GvDM,string colName, Form F, GridColumn gc,string btn, string tablePre, string field)
        {
            try
            {
                if (GvDM.FocusedRowHandle >= 0)
                {

                    if (tablePre !="" ? APCoreProcess.APCoreProcess.Read("select " + field + " from " + tablePre + " where " + field + "='" + GvDM.GetRowCellValue(GvDM.FocusedRowHandle, gc.Name).ToString() + "'").Rows.Count == 0 : 1==1)
                    {
                        if (MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này không?"))
                        {
                            DataTable dt = new DataTable();
                            dt = APCoreProcess.APCoreProcess.Read(table);
                            DataRow dr;
                            int a = GvDM.FocusedRowHandle;
                            dr = dt.Rows.Find(GvDM.GetRowCellValue(GvDM.FocusedRowHandle, GvDM.Columns[colName].Name));
                            DataSet ds = new DataSet();
                            ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(F.Name) + " where " + (gc.Name) + " = '" + GvDM.GetRowCellValue(GvDM.FocusedRowHandle, gc.Name).ToString() + "'"));
                            Function.clsFunction.sysTrace(F.Name, Function.clsFunction.transLate(btn), GvDM.GetRowCellValue(GvDM.FocusedRowHandle, gc.Name).ToString(), GvDM.GetRowCellValue(GvDM.FocusedRowHandle, gc.Name).ToString(), SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(F.GetType()).ToString().Split(',')[0]);
                            dr.Delete();
                            APCoreProcess.APCoreProcess.Save(dr);
                            GvDM.DeleteRow(GvDM.FocusedRowHandle);
                        }
                    }
                    else
                    {
                        MessageInfo("Thông báo", "Bạn không thể xóa dữ liệu này vì nó đã được sử dụng");
                    }
                }
            }
            catch { }
        }        

        public static void NhatKyHeThong(string idform, string action, string _object, string computer,string status)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysTrace");
                DataRow dr;
                dr = dt.NewRow();
                dr["idform"] = idform;
                dr["date"] = DateTime.Now;
                dr["userid"] = _iduser;
                dr["action"] = action;
                dr["computer"] = computer;
                dr["object"] = _object;
                dr["status"] = status;
                // dr["id"] = autonumber("id", "NhatKyHeThong");
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch { }
        }

       //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_userid_IK1.Text, txt_username_I2.Text, SystemInformation.ComputerName.ToString(), "1",""); status 0: insert,1: edit,2: delete,3:view:4: nothing
        
       public static void sysTrace(string idform, string action, string _object,string _caption ,string computer, string status)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysTrace");
                DataRow dr;
                dr = dt.NewRow();
                dr["idform"] = idform;
                dr["date"] = DateTime.Now;
                dr["userid"] = _iduser;
                dr["action"] = action;
                dr["computer"] = computer;
                dr["object"] = _object;
                dr["caption"] = _caption;
                dr["status"] = status;
                dr["tableName"] = idform.Split('_').Length > 1 ? idform.Split('_')[1] : "";              
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch { }
        }

       public static void sysTrace(string idform, string action, string _object, string _caption, string computer, string status, string datapre, string Namespace)
       {
           try
           {
               DataTable dt = new DataTable();
               dt = APCoreProcess.APCoreProcess.Read("sysTrace");
               DataRow dr;
               dr = dt.NewRow();
               dr["ID"] = clsFunction.autonumber( "ID","sysTrace");
               dr["idform"] = idform;
               dr["date"] = DateTime.Now;
               dr["userid"] = _iduser;
               dr["action"] = action;
               dr["computer"] = computer;
               dr["object"] = _object;
               dr["caption"] = _caption;
               dr["status"] = status;
               dr["tableName"] = idform.Split('_').Length > 1 ? idform.Split('_')[1] : "";
               dr["datapre"] = datapre;
               dr["namespace"] = Namespace;
               dt.Rows.Add(dr);
               APCoreProcess.APCoreProcess.Save(dr);
           }
           catch { }
       }
        
       public static void Insert_data(Control root, string form_name)
        {
            DataTable dt, dt_save = new DataTable();         
            dt = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + form_name + "' and status='True'");
            dt_save = APCoreProcess.APCoreProcess.Read(getNameControls(form_name));
            DataRow dr = dt_save.NewRow();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (dt.Rows[i]["type"].ToString() == "PictureEdit")
                    {
                        if (((PictureEdit)Function.clsFunction.FindControl(root, dt.Rows[i]["control_name"].ToString())).Image != null)
                        {
                            MemoryStream ms = new MemoryStream();
                            ((PictureEdit)Function.clsFunction.FindControl(root, dt.Rows[i]["control_name"].ToString())).Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            Byte[] bytBLOBData = new Byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                            dr[dt.Rows[i]["field_name"].ToString()] = bytBLOBData;
                        }
                    }
                    else
                    {
                        dr[dt.Rows[i]["field_name"].ToString()] = getDataControl(FindControl(root, dt.Rows[i]["control_name"].ToString()));
                        //MessageBox.Show(i.ToString());                   
                    }
                }
                catch (Exception)
                {
                
                }
            }
            dt_save.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
            if (Function.clsFunction._keylience != true)
                Application.Exit();

        }

        public static void Edit_data(Control root, string form_name, string key, string  id)
        {
            bool _picture = false;
            try
            {
                DataTable dt, dt_save = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + form_name + "' and status='True'");
                dt_save = APCoreProcess.APCoreProcess.Read(getNameControls(form_name) + "  where "+key+"='"+id+"'");
                DataRow dr = dt_save.Rows[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (FindControl(root, dt.Rows[i]["control_name"].ToString()).Name.Contains("IK1") == false)
                    {
                        try
                        {
                            if (dt.Rows[i]["type"].ToString() == "PictureEdit")
                            {
                                _picture = true;
                                if (((PictureEdit)Function.clsFunction.FindControl(root, dt.Rows[i]["control_name"].ToString())).Image != null)
                                {
                                    MemoryStream ms = new MemoryStream();
                                    ((PictureEdit)Function.clsFunction.FindControl(root, dt.Rows[i]["control_name"].ToString())).Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byte[] bytBLOBData = new byte[ms.Length];
                                    ms.Position = 0;
                                    ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                                    dr[dt.Rows[i]["field_name"].ToString()] = bytBLOBData;
                                }
                            }
                            else
                            {
                                dr[dt.Rows[i]["field_name"].ToString()] = getDataControl(FindControl(root, dt.Rows[i]["control_name"].ToString()));
                            }
                        }
                        catch (Exception ex)
                        {
                            if (_picture==true)
                            clsFunction.MessageInfo("Thông báo", "Vui lòng sử dụng hình ảnh dạng jpg, bmp, ico");
                        }
                    }
                    
                }
                APCoreProcess.APCoreProcess.Save(dr);
         
            }
            catch (Exception )
            {
               
            }
        }

        public static void Save_sysControl(Control ctl, Form F)
        {
            DataTable dt = new DataTable();
            
            dt = APCoreProcess.APCoreProcess.Read("sysControls");
            try
            {
                for (int i = 0; i < ctl.Controls.Count; i++)
                {                    
                    if (ctl.Controls[i].Name.Contains("C") != false)
                    {
                        Save_sysControl(ctl.Controls[i], F);
                    }
                    if (ctl.Controls[i].Name.Contains("_I") == true)
                    {
                        DataRow dr = dt.NewRow();
                        dr["form_name"] = F.Name;
                        dr["control_name"] = ctl.Controls[i].Name;
                        dr["type"] = ctl.Controls[i].GetType().Name;
                        dr["text_En"] = ctl.Controls[i].Text + "1";
                        dr["text_Vi"] = ctl.Controls[i].Text;
                        if (ctl.Controls[i].Name.Contains("_S") == true || ctl.Controls[i].Name.Contains("_C") == true)
                            dr["status"] = "False";
                        else
                        {
                            if (ctl.Controls[i].Name.Contains("_I") == true)
                                dr["status"] = "True";
                        }
                        dr["field_name"] = getNameControls(ctl.Controls[i].Name);
                        int n = 0;
                        try
                        {
                            if (ctl.Controls[i].Text.ToString().Length > 1 && int.TryParse(ctl.Controls[i].Text.ToString().Substring(1, 1), out n))
                                dr["stt"] = ctl.Controls[i].Text.ToString().Substring(1, 1);
                        }
                        catch
                        {
                            dr["stt"] = "NULL";
                        }
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
            }
        }

        private static readonly string[] VietnameseSigns = new string[]

        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };


        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }

        public static void saveControlReport(DevExpress.XtraReports.UI.XtraReport report)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = APCoreProcess.APCoreProcess.Read("sysReportControls");
                foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
                {
                    foreach (DevExpress.XtraReports.UI.XRControl control in band)
                    {
                        
                        DataRow dr = dt.NewRow();
                        string[] name1 = report.ToString().Split('_');
                        dr["report_name"] = name1[1].ToString();
                        dr["control_name"] = control.Name;
                        dr["type"] = control.GetType().Name;
                        dr["text_En"] = (control.Text) + "1";
                        dr["text_Vi"] = (control.Text);
                        if (control.Name.Contains("S") == true || control.Name.Contains("C") == true)
                            dr["status"] = "False";
                        else
                            dr["status"] = "True";
                        if (control.Name.Contains("I") == true)

                            dr["field_name"] = Function.clsFunction.getNameControls(control.Name);
                        dr["name_file_En"] = RemoveSign4VietnameseString(control.Text) + "1";
                        dr["name_file_Vi"] = RemoveSign4VietnameseString(control.Text);

                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);

                        if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                        {
                            DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                            {
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                                {
                                    DataRow dr1 = dt.NewRow();
                                    string[] name2 = report.ToString().Split('_');
                                    dr1["report_name"] = name2[1].ToString();
                                    dr1["control_name"] = cell.Name;
                                    dr1["type"] = cell.GetType().Name;
                                    dr1["text_En"] = cell.Text + "1";
                                    dr1["text_Vi"] = cell.Text;
                                    if (cell.Name.Contains("S") == true || cell.Name.Contains("C") == true)
                                        dr1["status"] = "False";
                                    else
                                        dr1["status"] = "True";
                                    if (cell.Name.Contains("I") == true)
                                        dr1["field_name"] = Function.clsFunction.getNameControls(cell.Name);

                                    dt.Rows.Add(dr1);
                                    APCoreProcess.APCoreProcess.Save(dr1);
                                }
                            }
                        }

                        else
                        {
                            ;// translation processing here
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        public static void saveSysConfig(DevExpress.XtraReports.UI.XtraReport rpt, string _object, string config)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysConfig");
            DataRow dr = dt.NewRow();
            string[] name1 = rpt.ToString().Split('.');
            dr["form_name"] = name1[2].ToString();
            dr["object"] = _object;
            dr["config"] = config;
            dr["status"] = "True";
            dt.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
        }
        public static void UpdateSysConfig(DevExpress.XtraReports.UI.XtraReport rpt, string _object, string config)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysConfig");

            string[] name1 = rpt.ToString().Split('.');
            //dr["form_name"] = name1[2].ToString();
            DataRow dr = dt.Rows[index(dt, name1[2].ToString(), _object)];
            //dr["object"] = _object;
            dr["config"] = config;
            //dr["status"] = "True";
            //dt.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
        }
        private static int index(DataTable dt, string form_name, string _object)
        {
            int _index = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["form_name"].ToString().Trim() == form_name.Trim() && dt.Rows[i]["object"].ToString().Trim() == _object.Trim())
                    _index = i;
            }
            return _index;
        }

        public static void Save_sysGridColumns(Form F,
     string[] caption_VI, string[] field_name, string[] style_column, string[] column_width,
     string[] column_visible, string[] sql_lue, string[] allow_focus, string[] caption_lue_VI, string[] fieldname_lue,
     string[,] caption_lue_col_VI, string[,] fieldname_lue_col, string[] fieldname_lue_visible,
     int cot_lue_search, string[] sql_glue, string[] fieldname_glue, string[,] caption_glue_col_VI,
     string[,] fieldname_glue_col, string[] fieldname_glue_visible, int cot_glue_search,
     string[] column_summary, string sql_grid)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns");
            try
            {
                DataRow dr = dt.NewRow();
                dr["form_name"] = F.Name;
                dr["column_name"] = ConvertArrayToString(field_name);
                dr["caption_VI"] = ConvertArrayToString(caption_VI);
                dr["caption_EN"] = ConvertArrayToString(caption_VI);
                dr["field_name"] = ConvertArrayToString(field_name);
                dr["style_column"] = ConvertArrayToString(style_column);
                dr["column_width"] = ConvertArrayToString(column_width);
                dr["column_visible"] = ConvertArrayToString(column_visible);
                dr["sql_lue"] = ConvertArrayToString(sql_lue);
                dr["allow_focus"] = ConvertArrayToString(allow_focus);
                dr["caption_lue_VI"] = ConvertArrayToString(caption_lue_VI);
                dr["caption_lue_EN"] = ConvertArrayToString(caption_lue_VI);
                dr["fieldname_lue"] = ConvertArrayToString(fieldname_lue);
                //MessageBox.Show("loi");
                dr["caption_lue_col_VI"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["caption_lue_col_EN"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["fieldname_lue_col"] = ConvertArray2ToString(fieldname_lue_col);
                dr["fieldname_lue_visible"] = ConvertArrayToString(fieldname_lue_visible);
                dr["cot_lue_search"] = cot_lue_search;
                dr["sql_glue"] = ConvertArrayToString(sql_glue);
                dr["fieldname_glue"] = ConvertArrayToString(fieldname_glue);
                dr["caption_glue_col_VI"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["caption_glue_col_EN"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["fieldname_glue_col"] = ConvertArray2ToString(fieldname_glue_col);
                dr["fieldname_glue_visible"] = ConvertArrayToString(fieldname_glue_visible);
                dr["cot_glue_search"] = cot_glue_search;
                dr["column_summary"] = ConvertArrayToString(column_summary);
                dr["sql_grid"] = sql_grid;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
            }
        }

       //them cot grid_name 

        public static void Save_sysGridColumns(Form F,
string[] caption_VI, string[] field_name, string[] style_column, string[] column_width,
string[] column_visible, string[] sql_lue, string[] allow_focus, string[] caption_lue_VI, string[] fieldname_lue,
string[,] caption_lue_col_VI, string[,] fieldname_lue_col, string[] fieldname_lue_visible,
int cot_lue_search, string[] sql_glue, string[] caption_glue, string[] fieldname_glue, string[,] caption_glue_col_VI,
string[,] fieldname_glue_col, string[] fieldname_glue_visible, int cot_glue_search,
string[] column_summary, string sql_grid,string grid_name)
        {
            DataTable dt = new DataTable();
            APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysGridColumns where form_name='"+ F.Name +"'");
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns");
            try
            {
                DataRow dr = dt.NewRow();
                dr["form_name"] = F.Name;
                dr["column_name"] = ConvertArrayToString(field_name);
                dr["caption"] = ConvertArrayToString(caption_VI);
                //dr["caption_EN"] = ConvertArrayToString(caption_VI);
                dr["field_name"] = ConvertArrayToString(field_name);
                dr["style_column"] = ConvertArrayToString(style_column);
                dr["column_width"] = ConvertArrayToString(column_width);
                dr["column_visible"] = ConvertArrayToString(column_visible);
                dr["sql_lue"] = ConvertArrayToString(sql_lue);
                dr["allow_focus"] = ConvertArrayToString(allow_focus);
                dr["caption_lue"] = ConvertArrayToString(caption_lue_VI);
                //dr["caption_lue_EN"] = ConvertArrayToString(caption_lue_VI);
                dr["fieldname_lue"] = ConvertArrayToString(fieldname_lue);
                //MessageBox.Show("loi");
                dr["caption_lue_col"] = ConvertArray2ToString(caption_lue_col_VI);
                //dr["caption_lue_col_EN"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["fieldname_lue_col"] = ConvertArray2ToString(fieldname_lue_col);
                dr["fieldname_lue_visible"] = ConvertArrayToString(fieldname_lue_visible);
                dr["cot_lue_search"] = cot_lue_search;
                dr["sql_glue"] = ConvertArrayToString(sql_glue);
                dr["fieldname_glue"] = ConvertArrayToString(fieldname_glue);
                dr["caption_glue"] = ConvertArrayToString(caption_glue);
                dr["caption_glue_col"] = ConvertArray2ToString(caption_glue_col_VI);
                //dr["caption_glue_col_EN"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["fieldname_glue_col"] = ConvertArray2ToString(fieldname_glue_col);
                dr["fieldname_glue_visible"] = ConvertArrayToString(fieldname_glue_visible);
                dr["cot_glue_search"] = cot_glue_search;
                dr["column_summary"] = ConvertArrayToString(column_summary);
                dr["sql_grid"] = sql_grid;
                dr["grid_name"] = grid_name;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
            }
        }

        public static void Save_sysGridColumns_Edit(Form F,
string[] caption_VI, string[] field_name, string[] style_column, string[] column_width,
string[] column_visible, string[] sql_lue, string[] allow_focus, string[] caption_lue_VI, string[] fieldname_lue,
string[,] caption_lue_col_VI, string[,] fieldname_lue_col, string[] fieldname_lue_visible,
int cot_lue_search, string[] sql_glue, string[] caption_glue, string[] fieldname_glue, string[,] caption_glue_col_VI,
string[,] fieldname_glue_col, string[] fieldname_glue_visible, int cot_glue_search,
string[] column_summary, string sql_grid, string grid_name)
        {
            DataTable dt = new DataTable();
            APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysGridColumns where form_name='" + F.Name + "'");
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns");
            try
            {
                DataRow dr = dt.NewRow();
                dr["form_name"] = F.Name;
                dr["column_name"] = ConvertArrayToString(field_name);
                dr["caption"] = ConvertArrayToString(caption_VI);
                //dr["caption_EN"] = ConvertArrayToString(caption_VI);
                dr["field_name"] = getFieldNameFromCol(field_name);
                dr["style_column"] = ConvertArrayToString(style_column);
                dr["column_width"] = ConvertArrayToString(column_width);
                dr["column_visible"] = ConvertArrayToString(column_visible);
                dr["sql_lue"] = ConvertArrayToString(sql_lue);
                dr["allow_focus"] = ConvertArrayToString(allow_focus);
                dr["caption_lue"] = ConvertArrayToString(caption_lue_VI);
                //dr["caption_lue_EN"] = ConvertArrayToString(caption_lue_VI);
                dr["fieldname_lue"] = ConvertArrayToString(fieldname_lue);
                //MessageBox.Show("loi");
                dr["caption_lue_col"] = ConvertArray2ToString(caption_lue_col_VI);
                //dr["caption_lue_col_EN"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["fieldname_lue_col"] = ConvertArray2ToString(fieldname_lue_col);
                dr["fieldname_lue_visible"] = ConvertArrayToString(fieldname_lue_visible);
                dr["cot_lue_search"] = cot_lue_search;
                dr["sql_glue"] = ConvertArrayToString(sql_glue);
                dr["fieldname_glue"] = ConvertArrayToString(fieldname_glue);
                dr["caption_glue"] = ConvertArrayToString(caption_glue);
                dr["caption_glue_col"] = ConvertArray2ToString(caption_glue_col_VI);
                //dr["caption_glue_col_EN"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["fieldname_glue_col"] = ConvertArray2ToString(fieldname_glue_col);
                dr["fieldname_glue_visible"] = ConvertArrayToString(fieldname_glue_visible);
                dr["cot_glue_search"] = cot_glue_search;
                dr["column_summary"] = ConvertArrayToString(column_summary);
                dr["sql_grid"] = sql_grid;
                dr["grid_name"] = grid_name;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
            }
        }

        private static string getFieldNameFromCol(string[] fieldCol)
        {
            string feild = "";
            for (int i = 0; i < fieldCol.Length; i++)
            {
                feild += getNameControls(fieldCol[i].ToString()) + "/";
            }
            return feild;
        }

        public static void Save_sysTreeColumns(Form F,
string[] caption_VI, string[] field_name, string[] style_column, string[] column_width,
string[] column_visible, string[] sql_lue, string[] allow_focus, string[] caption_lue_VI, string[] fieldname_lue,
string[,] caption_lue_col_VI, string[,] fieldname_lue_col, string[] fieldname_lue_visible,
int cot_lue_search, string[] sql_glue, string[] fieldname_glue, string[,] caption_glue_col_VI,
string[,] fieldname_glue_col, string[] fieldname_glue_visible, int cot_glue_search,
string[] column_summary, string sql_grid, string tree_name)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns");
            try
            {
                DataRow dr = dt.NewRow();
                dr["form_name"] = F.Name;
                dr["column_name"] = ConvertArrayToString(field_name);
                dr["caption_VI"] = ConvertArrayToString(caption_VI);
                dr["caption_EN"] = ConvertArrayToString(caption_VI);
                dr["field_name"] = ConvertArrayToString(field_name);
                dr["style_column"] = ConvertArrayToString(style_column);
                dr["column_width"] = ConvertArrayToString(column_width);
                dr["column_visible"] = ConvertArrayToString(column_visible);
                dr["sql_lue"] = ConvertArrayToString(sql_lue);
                dr["allow_focus"] = ConvertArrayToString(allow_focus);
                dr["caption_lue_VI"] = ConvertArrayToString(caption_lue_VI);
                dr["caption_lue_EN"] = ConvertArrayToString(caption_lue_VI);
                dr["fieldname_lue"] = ConvertArrayToString(fieldname_lue);
                //MessageBox.Show("loi");
                dr["caption_lue_col_VI"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["caption_lue_col_EN"] = ConvertArray2ToString(caption_lue_col_VI);
                dr["fieldname_lue_col"] = ConvertArray2ToString(fieldname_lue_col);
                dr["fieldname_lue_visible"] = ConvertArrayToString(fieldname_lue_visible);
                dr["cot_lue_search"] = cot_lue_search;
                dr["sql_glue"] = ConvertArrayToString(sql_glue);
                dr["fieldname_glue"] = ConvertArrayToString(fieldname_glue);
                dr["caption_glue_col_VI"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["caption_glue_col_EN"] = ConvertArray2ToString(caption_glue_col_VI);
                dr["fieldname_glue_col"] = ConvertArray2ToString(fieldname_glue_col);
                dr["fieldname_glue_visible"] = ConvertArrayToString(fieldname_glue_visible);
                dr["cot_glue_search"] = cot_glue_search;
                dr["column_summary"] = ConvertArrayToString(column_summary);
                dr["sql_grid"] = sql_grid;
                dr["grid_name"] = tree_name;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Có lỗi xảy ra\r\n" + ex.Message);
            }
        }
        #endregion

        #region Function

        private void ExportFile(GridView gv_list_C)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            bool status = false;
            saveFileDialog1.Filter = "Excel 97 - 2003 (*.xls)|*.xls |Excel 2007(*.xlsx)|*.xlsx |Pdf (*.pdf)|*.pdf |Webpage (*.html)|*.html |Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog1.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog1.FileName);
                    if (f.Extension.Equals(".xls"))
                    {
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file?"))
                        {
                            gv_list_C.ExportToXls(f.FullName);
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
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file?"))
                        {
                            gv_list_C.ExportToXlsx(f.FullName);
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
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file?"))
                        {
                            gv_list_C.ExportToPdf(f.FullName);
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
                    if (f.Extension.Equals(".rtf"))
                    {
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file?"))
                        {
                            gv_list_C.ExportToRtf(f.FullName);
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
                        if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở file?"))
                        {
                            gv_list_C.ExportToHtml(f.FullName);
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

        public static void CopyFile(string FileCopy, string FileSave)
        {
            //string path = @"c:\temp\MyTest.txt";
            //string path2 = @"c:\tempz\MyTest.txt";

            try 
            {
                // Create the file and clean up handles.
                using (FileStream fs = File.Create(FileCopy)) { }

                // Ensure that the target does not exist.
                File.Delete(FileSave);

                // Copy the file.
                File.Copy(FileCopy, FileSave);
                Console.WriteLine("{0} copied to {1}", FileCopy, FileSave);

                // Try to copy the same file again, which should succeed.
                File.Copy(FileCopy, FileSave, true);
                Console.WriteLine("The second Copy operation succeeded, which was expected.");
            } 

            catch 
            {
                Console.WriteLine("Double copy is not allowed, which was not expected.");
            }          

        }

        public static void Copy_File(string FileCopy, string FileSave)
        {
            //string path = @"c:\temp\MyTest.txt";
            //string path2 = @"c:\tempz\MyTest.txt";

            try
            {
                File.Copy(FileCopy, FileSave);
            }

            catch
            {
                Console.WriteLine("Double copy is not allowed, which was not expected.");
            }

        }

        public static bool checkFileExist(string path)
        {
            bool flag=false;   
            if (File.Exists(path))
                flag = true;
            else
                flag = false;
            return flag;
        }
       
        public static string transLate(string str)
       {
           string s="";
           DataTable dt=new DataTable();
           dt = APCoreProcess.APCoreProcess.Read("select language"+ Function.clsFunction.langgues +" from sys_Language where language_VI='"+str+"'");
           if (dt.Rows.Count == 0)
           {
               s = str;
           }
           else
           {
               if (dt.Rows[0][0].ToString() != "")
                   s = dt.Rows[0][0].ToString();
               else
                   s = str;
           }
           return s;
       }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                if (type == DateTime.Now.GetType())
                {
                    return DateTime.Now;
                }               
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static int LaySoNgayCN(int thang, int nam)
        {
            int soNgayCN = 0;
            for (int i = 0; i <= 31; i++)
            {
                try
                {
                    DateTime dt = new DateTime(nam, thang, i);
                    if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        soNgayCN++;
                    }
                }
                catch (Exception)
                {

                }
            }
            return soNgayCN;
        }

        public static string getVaiTroByUser(string US)
        {
            string vaitro = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + US + "'");
            if (dt.Rows.Count > 0)
            {
                vaitro = dt.Rows[0][0].ToString();
            }
            return vaitro;
        }

        public static double tinhGioCong(string giovao,string giora,int gionghitrua)
       {
           double sogio = 0;
           try
           {
               if (TimeSpan.Parse(giovao) < TimeSpan.Parse(giora))//ca sang
               {
                   sogio = Convert.ToDouble(Convert.ToDouble((TimeSpan.Parse(giora) - TimeSpan.Parse(giovao)).Hours) + Convert.ToDouble((TimeSpan.Parse(giora) - TimeSpan.Parse(giovao)).Minutes) / 60);
               }
               else
               {
                   sogio = Convert.ToDouble((TimeSpan.Parse(giora) + TimeSpan.Parse("23:00") - TimeSpan.Parse(giovao) + TimeSpan.Parse("01:00")).Hours + Convert.ToDouble((TimeSpan.Parse(giora) + TimeSpan.Parse("23:00") - TimeSpan.Parse(giovao) + TimeSpan.Parse("01:00")).Minutes) / 60);
               }
               if(sogio>5)
               sogio = sogio - gionghitrua;
           }
           catch (Exception ex)
           {
               //MessageBox.Show(ex.Message);
           }

           return lamTronWeiTai(sogio);
       }

        public static int nghigiuaca(string macakip)
       {
           int sogio = 0;
           try
           {
               DataTable dt = new DataTable();
               dt = APCoreProcess.APCoreProcess.Read("select OnLunch, OffLunch from shift where ShiftID='"+macakip+"'");
               if(dt.Rows.Count>0)
               if (dt.Rows[0]["OnLunch"].ToString()!="" &&  dt.Rows[0]["OffLunch"].ToString()!="" )//ca sang
               {
                   sogio = (TimeSpan.Parse(dt.Rows[0]["OffLunch"].ToString()) - TimeSpan.Parse(dt.Rows[0]["OnLunch"].ToString())).Hours ;
               }
           }
           catch (Exception ex)
           {
               //MessageBox.Show(ex.Message);
           }

           return (sogio);
       }

        public static double lamTronWeiTai(double sogio)
        {
            double sogiolamtron = 0;
            if ((sogio - (int)(sogio)) >= 0 && (sogio - (int)(sogio)) < 0.5)
            {
                sogiolamtron = (int)(sogio);
            }
            else
            {
                if ((sogio - (int)(sogio)) >= 0.95 && (sogio - (int)(sogio)) <= 0.99)
                    sogiolamtron = (int)(sogio) + 1;
                else
                    sogiolamtron = (Convert.ToDouble((int)(sogio)) + 0.5);
            }
            return sogiolamtron;
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string[] ConvertDatatableToArray(DataTable dt, string fieldname)
        {
            string[] arr = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] = dt.Rows[i][fieldname].ToString().Trim();
            }
            return arr;
        }

        public static void customPopupMenu(BarManager bar_menu_C, PopupMenu menu, GridView gv, Form F)
        {
            // Bind the menu to a bar manager.         
            menu.Manager = bar_menu_C;
            menu.ItemLinks.Clear();            
            // Add two items that belong to the bar manager.            
            for (int i = 0; i < bar_menu_C.Items.Count; i++)
            {
                if (bar_menu_C.Items[i].Name.Contains("_allow_") && bar_menu_C.Items[i].Name.Contains("_sub")==false)
                {
                    bar_menu_C.Items[i].Caption = transLateText(bar_menu_C.Items[i].Caption);
                    menu.ItemLinks.Add(bar_menu_C.Items[i]);
                }
            }
            BarSubItem bit = new BarSubItem();
            bit.Caption = transLateText("Ẩn/Hiện cột");
            bit.Name = "_allow_" + "column";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select  column_visible from sysGridColumns where form_name='" + F.Name + "'");
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                BarCheckItem barItem = new BarCheckItem();
                barItem.Enabled = true;
                barItem.Checked = Convert.ToBoolean( dt.Rows[0][0].ToString().Split('/')[i]);
                barItem.Name = "_allow_col_" + gv.Columns[i].FieldName+"_sub";
                barItem.Caption = gv.Columns[i].Caption;            
                if (checkExitsBarItemInBarManager(barItem.Name, bar_menu_C) == false)
                {
                    //barItem.ItemClick+=new ItemClickEventHandler(barItem_ItemClick);
                    bit.ItemLinks.Add(barItem);
                    bit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(barItem) });
                    bar_menu_C.Items.Add(barItem);   
                }                                    
            }      
            if (checkExitsBarItemInBarManager(bit.Name, bar_menu_C) == false)
            {
                menu.ItemLinks.Add(bit);                
            }
        }

        private static bool checkExitsBarItemInBarManager(string barItemName, BarManager bar)
        {
            bool flag = false;
            for (int i = 0; i < bar.Items.Count; i++)
            {
                if (bar.Items[i].Name == barItemName)
                {
                    flag = true;
                    break;
                }
            }
                return flag;
        }

 

        public static void Export(GridView gv_danhmuc_C)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            bool status = false;
            saveFileDialog1.Filter = "Excel 97 - 2003 (*.xls)|*.xls |Excel 2007(*.xlsx)|*.xlsx |Pdf (*.pdf)|*.pdf |Webpage (*.html)|*.html |Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog1.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog1.FileName);
                    if (f.Extension.Equals(".xls"))
                    {


                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở tập tin"))
                        {

                            gv_danhmuc_C.ExportToXls(f.FullName);
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
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở tập tin"))
                        {

                            gv_danhmuc_C.ExportToXlsx(f.FullName);

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
                    if (f.Extension.Equals(".pdf"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở tập tin"))
                        {

                            gv_danhmuc_C.ExportToPdf(f.FullName);

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
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở tập tin"))
                        {

                            gv_danhmuc_C.ExportToRtf(f.FullName);

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
                    if (f.Extension.Equals(".html"))
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn mở tập tin"))
                        {

                            gv_danhmuc_C.ExportToHtml(f.FullName);

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
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("You did pick a location " + "to save file to");
                }
            }
        }

        public static void setDateNumToDate(DevExpress.XtraGrid.Views.Grid.GridView gv_danhmuc_C,string columnName)
        {
            try
            {
                int ngay = 0;
                for (int i = 0; i < gv_danhmuc_C.DataRowCount; i++)
                {
                    if (gv_danhmuc_C.GetRowCellValue(i, columnName).ToString()!="")
                    {
                        ngay = Convert.ToInt32(gv_danhmuc_C.GetRowCellValue(i, columnName));
                        gv_danhmuc_C.SetRowCellValue(i, columnName, Function.clsFunction.convertNumtoDate(ngay));
                    }
                }
            }
            catch (Exception ex)
            { };
        }

        public static void setIndexAuto(DevExpress.XtraGrid.Views.Grid.GridView gv_danhmuc_C, string columnName)
        {
            try
            {
                int ngay = 0;
                for (int i = 0; i < gv_danhmuc_C.DataRowCount; i++)
                {
                    //if (gv_danhmuc_C.GetRowCellValue(i, columnName).ToString() != "")
                    {
                        ngay = i;
                        gv_danhmuc_C.SetRowCellValue(i,columnName, i+1);
                    }
                }
            }
            catch (Exception ex)
            { };
        }

        public static void setIndexAuto(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gv_danhmuc_C, string columnName)
        {
            try
            {
                int ngay = 0;
                for (int i = 0; i < gv_danhmuc_C.DataRowCount; i++)
                {
                    //if (gv_danhmuc_C.GetRowCellValue(i, columnName).ToString() != "")
                    {
                        ngay = i;
                        gv_danhmuc_C.SetRowCellValue(i, columnName, i + 1);
                    }
                }
            }
            catch (Exception ex)
            { };
        }

        public static int GetBarItemComboBoxIndex(BarEditItem item)
        {
            RepositoryItemComboBox comboBoxItem = item.Edit as RepositoryItemComboBox;
            if (comboBoxItem == null) return -1;
            return comboBoxItem.Items.IndexOf(item.EditValue);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }

        public static int getIndexIDinTable(string id,string fiedname, DataTable dt)
        {
            int index = 0;
  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (id == dt.Rows[i][fiedname].ToString())
                    index = i;
            }
            return index;
        }

        public static bool CheckDelete(string filename,string table, string value)
        {
            if (APCoreProcess.APCoreProcess.Read("select " + filename + " from " + table + " where " + filename + " ='" + value + "'").Rows.Count > 0)
                return false;
            return true;
        }

        public static string GetManvByUser()
        {
            string manv = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select manhanvien from sysUser where userid='" + Function.clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
                manv = dt.Rows[0][0].ToString();
            return manv;
        }

        public static string GetIDEMPByUser()
        {
            string manv = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select IDEMP from sysUser where userid='" + Function.clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
                manv = dt.Rows[0][0].ToString();
            return manv;
        }

        public static int ConVertStringDateToInt(string sDate)
        {
            if (sDate == "")
                sDate = "01/01/2011";
            int date=0;
            string stam = sDate.Substring(6, 4) + sDate.Substring(3, 2) + sDate.Substring(0, 2);
            date = Convert.ToInt32(stam);
            return date;
        }

        public static int[] getDayMonthYear(int num)
        {
            int[] arrDate = new int[3];            // Declare int array of zeros
            arrDate[0] = num % 10000 % 100;
            arrDate[1] = num % 10000 / 100;
            arrDate[2] = num / 10000;
            return arrDate;
        }
       // xuat ra thu
        private static bool KiemTraNamNhuan(int yy)
        {
            return ((yy % 4 == 0 && yy % 100 != 0) || yy % 400 == 0);
        }

        private static int NgayToiDaCuaThang(int mm, int yy)
        {
            int[] MangThang = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (KiemTraNamNhuan(yy))
                MangThang[2] = 29;
            return MangThang[mm];
        }

        private static bool KTNHAP(int ngay, int thang, int nam)
        {
            return (nam > 0 && thang > 0 && thang < 13 && ngay > 0 && ngay <= NgayToiDaCuaThang(thang, nam));
        }

        public static string convertNumtoDateX(int num)
        {
            string date = "";
            if ((num % 10000 % 100) < 10)
                date = "0" + (num % 10000 % 100) + "/" + (num % 10000 / 100) + "/" + (num / 10000);
            else
                date = (num % 10000 % 100) + "/" + (num % 10000 / 100) + "/" + (num / 10000);
            return date;
        }

        public static int ConVertDatetimeToNumeric(DateTime dte)
        {
            int date=0;
            date=dte.Year*10000 + dte.Month*100 + dte.Day  ;
            return date;
        }

        public static string convertNumtoDate(int num)
        {
            string date = "";
   
            string day = "", month = "";
            if ((num % 10000 % 100) < 10)
            {
                day = "0" + num % 10000 % 100;
            }
            else
            {
                day =  (num % 10000 % 100).ToString();
            }
            if ((num % 10000 / 100) < 10)
            {
                month = "0" + num % 10000 / 100;
            }
            else
            {
                month = (num % 10000 / 100).ToString();
            }
            date = day + "/" + month + "/" + (num / 10000).ToString();
            return date;
        }

        public static int ConVertNumericToDateTime(DateTime dte)
        {
            int date = 0;
            date = dte.Year * 10000 + dte.Month * 100 + dte.Day;
            return date;
        }
       
        private static int STTNgay(int ngay, int thang, int nam)
        {
            int STT = ngay;
            for (int i = 1; i < thang; i++)
                STT += NgayToiDaCuaThang(i, nam);
            return STT;
        }

        public static int ThuTuongUng(int ngay, int thang, int nam)
        {
            int tongngay = STTNgay(ngay, thang, nam) + (nam - 1) * 365 + (nam - 1) / 4 + (nam - 1) / 400 - (nam - 1) / 100;
            int thu = tongngay % 7;
            return thu;
        } 

        public static int ThuTuongUng(string date)
        {
            int ngay=-1, thang=-1, nam=-1;
            ngay= Convert.ToInt32(date.Substring(6,2));
            thang= Convert.ToInt32(date.Substring(4,2));
            nam= Convert.ToInt32(date.Substring(0,4));
            int tongngay = STTNgay(ngay, thang, nam) + (nam - 1) * 365 + (nam - 1) / 4 + (nam - 1) / 400 - (nam - 1) / 100;
            int thu = tongngay % 7;
            return thu;
        }

        public static int ThuTuongUng(DateTime date)
        {
            int ngay = -1, thang = -1, nam = -1;
            ngay = date.Day;
            thang = date.Month;
            nam = date.Year;
            int tongngay = STTNgay(ngay, thang, nam) + (nam - 1) * 365 + (nam - 1) / 4 + (nam - 1) / 400 - (nam - 1) / 100;
            int thu = tongngay % 7;
            return thu;
        } 

        public static DateTime ConVertNumericToDatetime(int num)
        {
            DateTime date = new DateTime(num / 10000, num % 10000 / 100, num % 10000 % 100);

            return date;
        }

        public static string GetDayOfWeek(DateTime dte)
        {
            string sThu = "";
            sThu = dte.DayOfWeek.ToString();           
            return sThu;
        }

        public static string ConvertDateOfWeekVi(string s)
        {
            string sThu = "";
            switch (s)
            {
                case "Monday":
                    {
                        sThu = "Thứ hai";
                        break;
                    }
                case "Tuesday":
                    sThu = "Thứ ba";
                    break;
                case "Wednesday":
                    sThu = "Thứ tư";
                    break;
                case "Thursday":
                    sThu = "Thứ năm";
                    break;
                case "Friday":
                    sThu = "Thứ sáu";
                    break;
                case "Saturday":
                    sThu = "Thứ bảy";
                    break;
                case "Sunday":
                    sThu = "Chủ nhật";
                    break;
           
            }
            return sThu;
        }

        public static bool checkDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) == true)
                    return true;
            }
            return false;
        }

        public static int GetDaysInMonth(int year, int month)
        {
            DateTime dt1 = new DateTime(year, month, 1);
            DateTime dt2 = dt1.AddMonths(1);
            TimeSpan ts = dt2 - dt1;
            return (int)ts.TotalDays;
        }

        public static string layMa(string sDauMa, string field, string table)// autocode 
        {
      
            DataTable danhSach = APCoreProcess.APCoreProcess.Read("select " + field + " from " + table + " ORDER BY " + field + " ");
            string XuatMa = sDauMa;
            int ma = 0;
            string maCuoi = danhSach.Rows.Count - 1 >= 0 ? danhSach.Rows[danhSach.Rows.Count - 1][field].ToString() : "0";//KD11
            try
            {
                maCuoi = maCuoi.Replace(sDauMa, "");//11
                ma = int.Parse(maCuoi) + 1;
            }
            catch  { }
            
            for (int i = 0; i < 5 - (ma.ToString().Length - 1); i++)
            {
                XuatMa += "0";
            }
            if (Function.clsFunction._keylience != true)
                Application.Exit();
            XuatMa += ma.ToString();
            return XuatMa;
        }

        public static string layMa(string sDauMa,string sPhuDe, string field, string table)// autocode 
        {

            DataTable danhSach = APCoreProcess.APCoreProcess.Read("select " + field + " from " + table + " ORDER BY " + field + " ");
            string XuatMa = sDauMa;
            int ma = 0;
            string maCuoi = danhSach.Rows.Count - 1 >= 0 ? danhSach.Rows[danhSach.Rows.Count - 1][field].ToString() : "0";//KD11
            try
            {
                maCuoi = maCuoi.Replace(sDauMa, "");//11
                ma = int.Parse(maCuoi) + 1;
            }
            catch (Exception ex) { }

            for (int i = 0; i < 5 - (ma.ToString().Length - 1); i++)
            {
                XuatMa += "0";
            }
            if (Function.clsFunction._keylience != true)
                Application.Exit();
            XuatMa =sPhuDe+ ma.ToString();
            return XuatMa;
        }

        public static int autonumber(string field, string table)// autocode 
        {
             int num = 0;
             DataTable danhSach = APCoreProcess.APCoreProcess.Read("select case when  max(convert(int," + field + ")) is null then 0 else max(convert(int," + field + ")) end from " + table + "  ");
            if (danhSach.Rows.Count>0)
            {
           
            int maCuoi = Convert.ToInt16(danhSach.Rows.Count - 1 >= 0 ? danhSach.Rows[danhSach.Rows.Count - 1][0].ToString() : "0");
            num = maCuoi + 1;
            }
            else
            {
                num=1;
            }
            return num;
        }

        public static string autonumber(string sDauma,string field, string table)// autocode 
        {
            //select top 1 IDGroupMenu from SysGroupSubMenu group by IDGroupMenu having IDGroupMenu like '1.%' order by CONVERT(int, replace(IDGroupMenu,'1.','')) desc
            DataTable danhSach = APCoreProcess.APCoreProcess.Read("select top 1 " + field + " from " + table + " group by  " + field + " having " + field + " like '" + sDauma + "%' order by convert(int,replace(" + field + ",'" + sDauma + "','')) desc");
            int num = 0;
            int maCuoi = Convert.ToInt16(danhSach.Rows.Count - 1 >= 0 ? danhSach.Rows[danhSach.Rows.Count - 1][0].ToString().Replace(sDauma,"") : "0");
            num = maCuoi + 1;
            return sDauma+ num;
        }

        public static string mahoapw(string password)
        {
            string pwmahoa = "";
            int makt, maao;
            int n = password.Length;
            Encoding ascii = Encoding.ASCII;
            Byte[] a = ascii.GetBytes(password);
            for (int i = 0; i < n; i++)
            {
                makt = Convert.ToInt32(a[i]) + i - 2;
                maao = Convert.ToInt32(a[i]) + i - 7;
                pwmahoa = pwmahoa + Convert.ToChar(makt).ToString() + Convert.ToChar(maao).ToString();
            }
            return pwmahoa;
        }

        public static string giaima(string password)
        {
            string pwmahoa = "";
            int makt;
            int n = password.Length;
            Encoding ascii = Encoding.ASCII;
            Byte[] a = ascii.GetBytes(password);
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    makt = Convert.ToInt32(a[i]) - i / 2 + 2;
                    pwmahoa = pwmahoa + Convert.ToChar(makt).ToString();
                }
            }
            return pwmahoa;
        }

        public static string ConvertArrayToString(string[] array)
        {
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i].ToString() + "/";
            }
            return str;
        }

        public static string ConvertArrayIntToString(int[] array)
        {
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i].ToString() + "/";
            }
            return str;
        }

        public static string ConvertArrayStringToArrayInt(int[] array)
        {
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i].ToString() + "/";
            }
            return str;
        }

        public static string ConvertArray2ToString(string[,] array)
        {
            string str = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    str += array[i, j].ToString() + "/";
                str += "@";
            }
            if (str == "")
                str = "@@";
            return str;
        }

        public static string[,] ConVertStringToArray2(string str)
        {
            string[,] str2 = new string[0, 0];
            int rank = str.Split('@').Length;
            return str2;
        }

        public static string[,] ConvertStringToArrayN(string str, string separator1, string separator2)
        {
            String[] array1 = ConvertToArray(str, "@");
            int a = array1.Length;
            int b = CountSeparator(array1[0].ToString(), separator2);
            int k = 0;
            string[,] Array2 = new string[a, b];
            try
            {
  
                for (int i = 0; i < a; i++)
                {
                    for (int j = 0; j < b; j++)
                    {
                        Array2[i, j] = array1[i].Split(separator2.ToCharArray())[j];
                        k++;
                    }
                }
            }
            catch (Exception ex)
            { }
            return Array2;

        }

        private static int CountSeparator(string str, string separator)
        {
            int dem = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToString().Trim() == separator.Trim())
                    dem++;
            }
            return dem;
        }

        public static string[] ConvertToArray(string strIds, string separator)
        {
            int size = (strIds != null && strIds.Length > 0)
  ? 1 : 0;
            //Determine how many semi-colon separated values are in string
            string temp = strIds;
            int pos;
            while (temp.IndexOf(separator) > -1)
            {
                size++;
                pos = temp.IndexOf(separator);
                temp = temp.Substring(pos + 1).Trim();
            }
            string[] strArray;
            if(size>0)
                strArray = new string[size - 1];
            else
                strArray = new string[0];
            for (int i = 0; i < size - 1; i++)
            {

                pos = (strIds.Trim().IndexOf(separator) == -1)
                        ? strIds.Trim().Length
                        : strIds.IndexOf(separator);
                //Now get the string within the single quotes; trimming them off
                string val = strIds.Trim().Substring(0, pos).Trim();
                strArray[i] = val;
                if (strIds.Length > (pos + 1))
                {
                    strIds = strIds.Substring(pos + 1).Trim();
                }
            }
            return strArray;
        }

        public static Control FindControl(Control root, string target)
        {
            try
            {
            if (root.Name.Equals(target))
                return root;
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                if (root.Controls[i].Name.Equals(target))
                    return root.Controls[i];
            }
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                Control result;
                for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                {
                    result = FindControl(root.Controls[i].Controls[k], target);
                    if (result != null)
                        return result;
                }
            }
            }
            catch
            {
                MessageBox.Show("");
            }
            return null;
        }

        public static void upDateLanguage(string text)
        {
            try
            {
           
                if (APCoreProcess.APCoreProcess.Read("select language_VI from sys_Language where language_VI =N'" + text + "'").Rows.Count == 0 && text != " or language <>N'"+ text +"' " )
                {
                    //APCoreProcess.APCoreProcess.ExcuteSQL("insert into sys_language (language_VI,userid1,date1) values (N'" + text + "','" + clsFunction._iduser + "','" + DateTime.Now + "')");
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("select * from sys_language");
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["ID"] = autonumber("ID", "sys_Language");
                    dr["language_VI"] = text;
                    dr["userid1"] = clsFunction._iduser;
                    dr["date1"] = DateTime.Now;
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
            catch { }
        }

        public static void TranslateGridColumn(GridView gv)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                gv.Columns[i].Caption = transLateText(gv.Columns[i].Caption.ToString());
            }
        }

        public static void TranslateTreeColumn(TreeList gv)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                gv.Columns[i].Caption = transLateText(gv.Columns[i].Caption.ToString());
            }
        }

       public static string getIDfromIndex(int index, string table, string field)
        {
            string id = "";
            DataTable dt=new DataTable();
            dt = APCoreProcess.APCoreProcess.Read(table);
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[index][field].ToString();
            }
            return id;
        }

       public  static void MessageInfo(string title, string text)
       {        
           XtraMessageBox.Show(transLateText(text),transLateText(title),MessageBoxButtons.OK,MessageBoxIcon.Information);                
       }

       public static bool MessageDelete(string title, string text)
       {
           bool flag = false;
           DialogResult dl=  XtraMessageBox.Show(transLateText(text), transLateText(title), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (dl==DialogResult.Yes )
               flag=true;
           else
                   flag=false;
           return flag;
       }

        public static Control TranslateForm(Control root, string target)
        {
            try
            {                                       
                if (!((root.Name.Contains("_I")) || (root.GetType().Name=="CheckEdit" ) || target.Contains("_I")))
                {
                    transLateConTrols(root);
                }                            
                for (var i = 0; i < root.Controls.Count; ++i)
                {                                     
                    if (root.Controls[i].Name.Equals(target))
                    {
                        // code here
                        if (!((root.Name.Contains("_I")) || (root.GetType().Name == "CheckEdit") || target.Contains("_I")))
                        {
                            transLateConTrols(root.Controls[i]);
                            return root.Controls[i];
                        }
                    }
                    else
                    {
                        Control result;
                        if (!((root.Name.Contains("_I")) || (root.GetType().Name == "CheckEdit") || target.Contains("_I")))
                        {
                            if (!((root.Name.Contains("_I")) || (root.GetType().Name == "CheckEdit") || target.Contains("_I")))
                            {
                                transLateConTrols(root.Controls[i]);
                            }
                            for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                            {
                                result = TranslateForm(root.Controls[i].Controls[k], root.Controls[i].Name);
                                if (result != null)
                                    return result;
                            }
                        }
                    }                   
                }
            }
            catch
            {
                
            }
            return null;
        }

        public static void setLanguageForm(Control ctl, Form F)
        {        
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysControls where form_name='" + F.Name + "'");
            for (int i = 0; i < ctl.Controls.Count; i++)
            {
                if (ctl.Controls[i].Name.Contains("C") != false)
                {
                    upDateLanguage(ctl.Controls[i].Text);
                    setLanguageForm(ctl.Controls[i], F);
                    if (ctl.Controls[i].GetType().ToString() == "GridView")
                    {
                        setLanguageGrid(((DevExpress.XtraGrid.GridControl)ctl.Controls[i])); 
                    }
                }
                else
                {
                    upDateLanguage(ctl.Controls[i].Text);
                }            
            }
        }

        private static void setLanguageGrid(DevExpress.XtraGrid.GridControl gv)
        {
            for (int i = 0; i < ((GridView)gv.ViewCollection[0]).Columns.Count; i++)
            {
                upDateLanguage(((GridView)gv.ViewCollection[0]).Columns[i].Caption);
            }
        }

        public static void LoadLanguageGrid(DevExpress.XtraGrid.GridControl gv)
        {
            for (int i = 0; i < ((GridView)gv.ViewCollection[0]).Columns.Count; i++)
            {
                ((GridView)gv.ViewCollection[0]).Columns[i].Caption =transLateText( ((GridView)gv.ViewCollection[0]).Columns[i].Caption ) ;
            }
        }

        public static Control setLanguageForm1(Control root, string target)
        {
            try
            {
                for (var i = 0; i < root.Controls.Count; ++i)
                {
                    upDateLanguage(root.Controls[i].Text);
                    if (root.Controls[i].Name.Equals(target))
                    {
                        // code here
                        return root.Controls[i];
                    }
                    else
                    {
                        Control result;
                        for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                        {
                            result = TranslateForm(root.Controls[i].Controls[k], target);
                            upDateLanguage(root.Controls[i].Text);
                            if (result != null)
                                return result;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("");
            }
            return null;
        }

        private static void transLateConTrols(Control ctr)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select language" + langgues + " from sys_language where language_VI=N'" + ctr.Text + "' or language" + langgues + "=N'" + ctr.Text + "'");
                if (dt.Rows.Count > 0)
                    if (dt.Rows[0][0].ToString() != "")
                        ctr.Text = dt.Rows[0][0].ToString();
                    else
                        ctr.Text = ctr.Text;
                else
                {
                    ctr.Text = ctr.Text;

                    upDateLanguage(ctr.Text);
                }
            }
            catch { }
        }
       
        public static string transLateText(string text)
        {
            string str = "";
            if (text !="")
            {
                DataTable dt = new DataTable();
                //dt = APCoreProcess.APCoreProcess.Read("select language" + langgues + " from sys_language where language_VI=N'" + text + "'");
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = clsFunction.Excute_Proc("Tranlate", new string[2, 2] { { "type", Function.clsFunction.langgues }, { "str", text } });
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read("select language" + clsFunction.langgues + " from sys_language where language_VI='"+ text +"'");
                }
                if (dt.Rows.Count > 0)
                    if (dt.Rows[0][0].ToString() != "")
                        str = dt.Rows[0][0].ToString();
                    else
                    {
                        str = text;
                    }
                else
                {
                    str = text;
                    upDateLanguage(text);
                }
            }
            return str;
        }

        public static string getDataControl(Control ctl)
        {                                                 
                string data = "";
                if (ctl != null)
                {
                   
                    {
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "TextEdit".ToUpper().Trim() || ctl.GetType().Name.ToString().Trim().ToUpper() == "MemoEdit".ToUpper().Trim())
                        {
                            if (ctl.Name.Contains("_I4") || ctl.Name.Contains("_I8") || ctl.Name.Contains("_I15"))
                            {
                                data += Convert.ToDouble( ctl.Text).ToString().Trim() + "/";
                            }
                            else
                            {
                                data += ctl.Text.Trim() + "/";
                            }
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "PictureEdit".ToUpper().Trim())
                        {
                            if (ctl != null)
                            {
                                try
                                {
                                    MemoryStream ms = new MemoryStream();
                                    ((PictureEdit)ctl).Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);  
                                    Byte[] bytBLOBData = new Byte[ms.Length];
                                    ms.Position = 0;
                                    ms.Read(bytBLOBData, 0, Convert.ToInt32(ms.Length));
                                    data += bytBLOBData + "/";
                                }
                                catch (Exception)
                                {
                                }
                            }
                            else
                                data +=  "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "MRUEdit".ToUpper().Trim())
                        {
                            data += ((MRUEdit)ctl).Text + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "ListBoxControl".ToUpper().Trim())
                        {
                            data += ((ListBoxControl)ctl).Items[0] + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "CalcEdit".ToUpper().Trim())
                        {
                            data += ((CalcEdit)ctl).Value + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "DateEdit".ToUpper().Trim())
                        {
                            if ((((DateEdit)ctl).Text) != "")
                            {
                                if (ctl.Name.Contains("_I5"))
                                {
                                    data += Convert.ToDateTime(((DateEdit)ctl).EditValue).ToString() + "/";
                                }
                                else
                                {
                                    data += Function.clsFunction.ConVertDatetimeToNumeric(Convert.ToDateTime(((DateEdit)ctl).EditValue)) + "/";
                                }
                            }
                            else
                            {                              
                                data += "/";
                            }
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "TimeEdit".ToUpper().Trim())
                        {
                            data += ctl.Text.Trim() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "LabelControl".ToUpper().Trim())
                        {
                            data += ctl.Text.Trim() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "MemoExEdit".ToUpper().Trim())
                        {
                            data += ctl.Text.Trim() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "CheckEdit".ToUpper().Trim())
                        {
                            data += ((CheckEdit)ctl).EditValue.ToString() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "ComboBoxEdit".ToUpper().Trim())
                        {
                            data += ((ComboBoxEdit)ctl).Text.ToString() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "RadioGroup".ToUpper().Trim())
                        {
                            data += ((RadioGroup)ctl).EditValue.ToString() + "/";
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "LookupEdit".ToUpper())
                        {
                            try
                            {
                                data += ((LookUpEdit)ctl).EditValue.ToString().Trim() + "/";
                            }
                            catch (Exception ex)
                            {
                               // DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Thông báo");
                            }
                        }
                        if (ctl.GetType().Name.ToString().Trim().ToUpper() == "GridLookupEdit".ToUpper())
                        {
                            try
                            {
                                data += ((GridLookUpEdit)ctl).EditValue.ToString().Trim() + "/";
                            }
                            catch (Exception ex)
                            {
                               // DevExpress.XtraEditors.XtraMessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Thông báo1");
                            }
                        }
                    }
                    //if(data !="")
                }
                else
                {
                    data += "/";
                }
    
            return data.Substring(0, data.Length - 1);
        }

        private static string DataTypeColumn(string fieldname)
        {
            string col = "";
            if (fieldname.Contains("K"))
            {
                col += getNameControls(fieldname) + " " + DataType(GetNumberInString(fieldname)) + " NOT NULL,";
                key = getNameControls(fieldname);
            }
            else
            {
                col += getNameControls(fieldname) + " " + DataType(GetNumberInString(fieldname)) + " NULL,";
            }
            return col;
        }

        public static int GetNumberInStringBK(string pValue)
        {
            int number = 0;
            foreach (Char c in pValue)
            {
                if (Char.IsDigit(c))
                    number = int.Parse(c.ToString());
            }
            if (number == 0)
                number =Convert.ToInt16(pValue.Substring(pValue.Length-2, 2));
            return number;
        }

        public static int GetNumberInString(string pValue)
        {
            // lấy phần tử cuối cùng tách lấy số col_name_Ik12 ==>12
            int number = 0;
            string temp = "";
            temp = pValue.Split('_')[pValue.Split('_').Length-1];
            temp = temp.Replace("IK", "");
            temp = temp.Replace("I", "");
       
                    number = int.Parse(temp);
            
            if (number == 0)
                number = Convert.ToInt16(pValue.Substring(pValue.Length - 2, 2));
            return number;
        }

        public static string getNameControls(string name)
        {
            string fielname = "";
            try
            {
                string[] ctl_name = name.Split('_');
                fielname = ctl_name[1];
            }
            catch (Exception ex) { }
            return fielname;

        }

        public static string getLastNumControls(string name)
        {
            string fielname = "";
            try
            {
                string[] ctl_name = name.Split('_');
                fielname = ctl_name[2];
            }
            catch (Exception ex) { }
            return fielname;

        }

        private static string DataType(int num)
        {
            string type = "";
            switch (num)
            {
     
                case 1:
                    type = "[nvarchar](20)";
                    break;
                case 2:
                    type = "[nvarchar](100)";
                    break;
                case 3:
                    type = "[nvarchar](MAX)";
                    break;
                case 4:
                    type = "[int]";
                    break;
                case 5:
                    type = "[datetime]";
                    break;
                case 6:
                    type = "[bit]";
                    break;
                case 7:
                    type = "[ntext]";
                    break;
                case 8:
                    type = "[float]";
                    break;
                case 9:
                    type = "[double]";
                    break;
                case 10:
                    type = "[image]";
                    break;
                case 11:
                    type = "[nvarchar](4000)";
                    break;
                case 12:
                    type = "[nvarchar](500)";
                    break;
                case 13:
                    type = "[nvarchar](200)";
                    break;
                case 14:
                    type = "[nchar](1)";
                    break;
                case 15:
                    type = "[money]";
                    break;
                default:
                    {
                        type = "[nvarchar]("+ num +")";
                    }
                    break;
            }          


            return type;
        }

        public static void CreateTable(Control ctl, Form F)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysControls where form_name='"+ F.Name +"'");
            for (int i = 0; i < ctl.Controls.Count; i++)
            {
                if (ctl.Controls[i].Name.Contains("C") != false)
                {
                    CreateTable(ctl.Controls[i], F);
                }
                else
                {
                    if (ctl.Controls[i].Name.Contains("I") != false)
                    {
                        column += DataTypeColumn(ctl.Controls[i].Name);
                    }
                }
                if (i == ctl.Controls.Count-1)
                    flag = true;
            }
            string sql = "CREATE TABLE [dbo].[" + getNameControls(F.Name).ToUpper() + "](";
            sql += column;
            sql += " CONSTRAINT [PK_" + getNameControls(F.Name) + "] PRIMARY KEY CLUSTERED ";
            sql += " (";
            sql += " [" + key + "] ASC";
            sql += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
            sql += " ) ON [PRIMARY]";
            if (flag == true)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(column);
                APCoreProcess.APCoreProcess.ExcuteSQL("drop table "+Function.clsFunction.getNameControls(F.Name));
                APCoreProcess.APCoreProcess.ExcuteSQL(sql);
            }
        }

        public static void setMaxLength(Control ctl, Form F)
        {
            for (int i = 0; i < ctl.Controls.Count; i++)
            {
                if (ctl.Controls[i].Name.Contains("C") != false)
                {
                    setMaxLength(ctl.Controls[i], F);
                }
                else
                {
                    if (ctl.Controls[i].Name.Contains("I") != false)
                    {
                        if (ctl.Controls[i].GetType().Name.ToString().Trim().ToUpper() == "TextEdit".ToUpper().Trim() )
                        {
                            ((TextEdit)ctl.Controls[i]).Properties.MaxLength = Convert.ToInt16(getLastNumControls(ctl.Controls[i].Name));
                        }
                    }
                }      
            }      
        }

        public static void CreateTable( Form F)
        {
            column = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysControls where form_name='" + F.Name + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {               
                {
                    if (i < dt.Rows.Count - 1)
                    {
                        column += DataTypeColumn(dt.Rows[i]["control_name"].ToString()) + "";
                    }
                    else
                    {
                        column += DataTypeColumn(dt.Rows[i]["control_name"].ToString()) +"";
                    }
                }               
            }
            string sql = "CREATE TABLE [dbo].[" + getNameControls(F.Name).ToUpper() + "](";
            sql += column;
            sql += " CONSTRAINT [PK_" + getNameControls(F.Name) + "] PRIMARY KEY CLUSTERED ";
            sql += " (";
            sql += " [" + key + "] ASC";
            sql += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
            sql += " ) ON [PRIMARY]";
           
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(column);
                APCoreProcess.APCoreProcess.ExcuteSQL("drop table " + Function.clsFunction.getNameControls(F.Name));
                APCoreProcess.APCoreProcess.ExcuteSQL(sql);
            }
        }

        public static void CreateTableGrid(string[] fieldName,GridView gv)
        {
            column = "";

            for (int i = 0; i < fieldName.Length;i++ )
            {
                if (fieldName[i].Contains("_S")==false)
                {
                    if (i < fieldName.Length - 1)
                    {
                        column += DataTypeColumn(fieldName[i]) + "";
                    }
                    else
                    {
                        column += DataTypeColumn(fieldName[i]) + "";
                    }
                }
            }
            string sql = "CREATE TABLE [dbo].[" + getNameControls(gv.Name).ToUpper() + "](";
            sql += column;
            sql += " CONSTRAINT [PK_" + getNameControls(gv.Name) + "] PRIMARY KEY CLUSTERED ";
            sql += " (";
            sql += " [" + key + "] ASC";
            sql += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
            sql += " ) ON [PRIMARY]";

            {
                DevExpress.XtraEditors.XtraMessageBox.Show(column);
                APCoreProcess.APCoreProcess.ExcuteSQL("drop table " + Function.clsFunction.getNameControls(gv.Name));
                APCoreProcess.APCoreProcess.ExcuteSQL(sql);
            }
        }


        public static void BindDataControlReport(DevExpress.XtraReports.UI.XtraReport report, DataTable data, string langues)
        {
            try
            {
                string Text = "";
                Text = "text" + langgues;
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysReportControls WHERE report_name='"+getNameControls(report.ToString())+"'");
                foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
                {
                    foreach (DevExpress.XtraReports.UI.XRControl control in band)
                    {
                        if (APCoreProcess.APCoreProcess.Read("select text_VI,text_EN from sysReportControls where control_name='" + (control.Name) + "' AND REPORT_NAME='"+report.Name+"'").Rows.Count>0)
                            control.Text = APCoreProcess.APCoreProcess.Read("select text_VI,text_EN from sysReportControls where control_name='" + (control.Name) + "'").Rows[0][Text].ToString();
                        if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                        {
                            DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                            {
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                                {
                                    if (cell.Name.Contains("I") == true)
                                    {
                                        cell.DataBindings.Add("Text", data, Function.clsFunction.getNameControls(cell.Name), "{0:n0}");
                                        if (cell.GetType().Name == "LookUpEdit")
                                        {
                                            cell.DataBindings.Add("EditValue", data, Function.clsFunction.getNameControls(cell.Name));
                                        }
                                    }
                           
                                    else
                                    {
                                        cell.Text = APCoreProcess.APCoreProcess.Read("select text_VI,text_EN from sysReportControls where control_name='" + (cell.Name) + "'").Rows[0][Text].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ;// translation processing here
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public static void BindDataControlReport(DevExpress.XtraReports.UI.XtraReport report)
        {
            try
            {
                foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
                {
                    foreach (DevExpress.XtraReports.UI.XRControl control in band)
                    {                       
                        if (control.Name.Contains("_S")) //nếu controls có name là _S
                            control.Text = clsFunction.transLateText(control.Text);
                        if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                        {
                            DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                            {
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                                {
                                    if (cell.Name.Contains("_S") == true)
                                    {
                                        cell.Text = clsFunction.transLateText(cell.Text);                                      
                                    }
                                }
                            }
                        }                     
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void Data_Binding(Form F, LabelControl lbl)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(F.Name) + " where " + Function.clsFunction.getNameControls(lbl.Name) + " ='" + lbl.Text + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + F.Name + "' and status='True'");

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim()))
                    {
                        //kiem tra control                        
                        try
                        {
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TextEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoExEdit")
                            {
                                Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TimeEdit")
                            {
                                ((TimeEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LookUpEdit")
                            {
                                ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                            {
                                ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                            {
                                ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToInt16(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ListBoxControl") 
                            {
                                ((ListBoxControl)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Items[0] = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                            }
                            //if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "PictureEdit")
                            //{
                            //    ((PictureEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString())];
                            //}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public static void Data_XoaText(Form F, TextEdit lbl)
        {
           
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(F.Name) + " where " + Function.clsFunction.getNameControls(lbl.Name) + " ='" + lbl.Text + "'");
           // if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + F.Name + "' and status='True'  AND (control_name not like '%IK1');");

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim()))
                    {
                        //kiem tra control                        
                        try
                        {
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TextEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoExEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MRUEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                            {
                                Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = "";
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ComboBoxEdit" )
                            {
                                Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = "";
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TimeEdit")
                            {
                                ((TimeEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = null;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CalcEdit")
                            {
                                ((CalcEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Value =0;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "DateEdit")
                            {
                                ((DateEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue =DateTime.Now;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LookUpEdit")
                            {
                                ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = null ;
               
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "GridLookUpEdit")
                            {
                                ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Properties.GetKeyValue(0);
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                            {
                                ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = false;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                            {
                                ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = false;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "PictureEdit")
                            {
                                 ((PictureEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Image =null;
                            }
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ListBoxControl")
                            {
                                //string [] arr=(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString()).Split('@');
                                ((ListBoxControl)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Items.Clear();
                        
                            }
                        }
                        catch (Exception ex) 
                        {
                            //MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //MessageBox.Show("select * from sysControls where form_name='" + F.Name + "' and status='True'  AND (control_name not like '%IK1');");
                    }
                }
            }
        }

        public static void Data_Binding1(Form F, TextEdit lbl)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(F.Name) + " where " + Function.clsFunction.getNameControls(lbl.Name) + " ='" + lbl.Text + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name=@formname and status='True'", "read_syscontrol", new string[1, 2] { { "formname", F.Name } }, "@formname as nvarchar(20)");
                }
                else
                {
                    dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='"+F.Name+"' and status='True'");
                }
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim()))
                        {
                            //kiem tra control                        
                            try
                            {
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TextEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoExEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MRUEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ComboBoxEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TimeEdit")
                                {
                                    ((TimeEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CalcEdit")
                                {
                                    ((CalcEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Value = Convert.ToDecimal(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "DateEdit")
                                {
                                    try
                                    {
                                        if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                            ((DateEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToDateTime (((dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString())));
                                        else
                                            ((DateEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = null;
                                    }
                                    catch { }
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LookUpEdit")
                                {
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "GridLookUpEdit")
                                {
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                                {
                                    if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                        ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    else
                                        ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = false;
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                                {
                                    try
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToInt16(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "PictureEdit")
                                {
                                    ((PictureEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())]);
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ListBoxControl")
                                {
                                    string[] arr = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString()).Split('@');
                                    ((ListBoxControl)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Items.AddRange(arr);
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public static void Data_BindingTrace(Form F, DataTable dt)
        {
            //DataTable dt = new DataTable();
            //dt = APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(F.Name) + " where " + Function.clsFunction.getNameControls(lbl.Name) + " ='" + lbl.Text + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name=@formname and status='True'", "read_syscontrol", new string[1, 2] { { "formname", F.Name } }, "@formname as nvarchar(20)");
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim()))
                        {
                            //kiem tra control                        
                            try
                            {
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TextEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoExEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MRUEdit" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ComboBoxEdit")
                                {
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TimeEdit")
                                {
                                    ((TimeEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CalcEdit")
                                {
                                    ((CalcEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Value = Convert.ToDecimal(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "DateEdit")
                                {
                                    try
                                    {
                                        if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                            ((DateEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Function.clsFunction.ConVertNumericToDatetime(Convert.ToInt32((dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString())));
                                        else
                                            ((DateEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = null;
                                    }
                                    catch { }
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LookUpEdit")
                                {
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((LookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "GridLookUpEdit")
                                {
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                                {
                                    if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                        ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    else
                                        ((CheckEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Checked = false;
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                                {
                                    try
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToInt16(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "PictureEdit")
                                {
                                    ((PictureEdit)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).EditValue = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())]);
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ListBoxControl")
                                {
                                    string[] arr = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString()).Split('@');
                                    ((ListBoxControl)Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString())).Items.AddRange(arr);
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public static void Data_BindingOject(Form F,Control Ctr,  TextEdit lbl)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(F.Name) + " where " + Function.clsFunction.getNameControls(lbl.Name) + " ='" + lbl.Text + "'");
            if (dt.Rows.Count > 0)
            {
                string sCtr=null; 
                DataTable dt1 = new DataTable();
                dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + F.Name + "' and status='True'");
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        sCtr = Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim());
                        if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == sCtr )
                        {
                            //kiem tra control                        
                            try
                            {
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TextEdit" || Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoExEdit" || Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MRUEdit" || Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "MemoEdit")
                                {
                                    Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ComboBoxEdit")
                                {
                                    Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Text = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "TimeEdit")
                                {
                                    ((TimeEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CalcEdit")
                                {
                                    ((CalcEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).Value = Convert.ToDecimal(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "DateEdit")
                                {
                                    if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                        ((DateEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = ConVertNumericToDatetime(Convert.ToInt32(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString()));
                                    else
                                        ((DateEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = null;
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LookUpEdit")
                                {
                                    ((LookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((LookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((LookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "GridLookUpEdit")
                                {
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).DataBindings.Clear();
                                    ((GridLookUpEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).DataBindings.Add("EditValue", dt, Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToLower().Trim()));
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                                {
                                    if (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString() != "")
                                        ((CheckEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).Checked = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    else
                                        ((CheckEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).Checked = false;
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                                {
                                    try
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToBoolean(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        ((RadioGroup)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = Convert.ToInt16(dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString());
                                    }
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "PictureEdit")
                                {
                                    ((PictureEdit)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).EditValue = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())]);
                                    continue;
                                }
                                if (Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "ListBoxControl")
                                {
                                    string[] arr = (dt.Rows[0][Function.clsFunction.getNameControls(Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim())].ToString()).Split('@');
                                    ((ListBoxControl)Function.clsFunction.FindControl(Ctr, dt1.Rows[i]["control_name"].ToString())).Items.AddRange(arr);
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public static void Text_Control(Form F, string langues)
        {
            string text = "";          
            text = "text"+langgues;        
            DataTable dt1 = new DataTable();
            dt1 = APCoreProcess.APCoreProcess.Read("select * from sysControls where form_name='" + F.Name + "' ");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                try
                {
                    if (dt1.Rows[i]["field_name"].ToString().ToUpper().Trim() == Function.clsFunction.getNameControls(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Name.ToString().ToUpper().Trim()))
                    {
                        //kiem tra control                                    
                        //if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "LabelControl" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "GroupControl")
                        try
                        {
                            if (Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "RadioGroup")
                            {
                                for (int j = 0; j < ConvertToArray(dt1.Rows[i][text].ToString(), "/").Length; j++)
                                {
                                    ((RadioGroup)(Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()))).Properties.Items[j].Description = ConvertToArray(dt1.Rows[i][text].ToString(), "/")[j].ToString();
                                }
                            }
                            else
                            {
                                if (dt1.Rows[i]["status"].ToString() == "False" || Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).GetType().Name == "CheckEdit")
                                    Function.clsFunction.FindControl(F, dt1.Rows[i]["control_name"].ToString()).Text = dt1.Rows[i][text].ToString();
                            }
                        }
                        catch { };
                    }
                }
                catch 
                {
                  
                };          
            }
        }

        #endregion
    }
}
