using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.IO;
using DevExpress.XtraReports.UI;

namespace ReportControls.Presentation
{
    public partial class frmConfigRePort : DevExpress.XtraEditors.XtraForm
    {

        #region Var

        public frmConfigRePort()
        {
            InitializeComponent();
        }
        public string sql = "", formname="";
        public int group = 0;
        bool them = false;
        public string langues = "";
        public string[,] arrParam;
        #endregion 

        #region FormEvent

        private void frmConfigRePort_Load(object sender, EventArgs e)
        {
            clockButton(true);
            khoaText(true);
            chk_custom_S_CheckedChanged(sender,e);
            loadGlueReport();
            glue_template_IS.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select '' as id, N'" + Function.clsFunction.transLateText("Không chọn") + "' as reportname, N'" + Function.clsFunction.transLateText("Không chọn") + "' as caption, '' as query union select id,reportname,caption, query from sysReportDesigns");         
            string strfolder = Application.StartupPath + "\\Report";
            if (!Directory.Exists(strfolder))
            {
                Directory.CreateDirectory(strfolder);
            }
            txt_path_IS.Text = strfolder;
            Function.clsFunction.TranslateForm(this,this.Name);
        }

        #endregion    

        #region Methods

        private void loadGlueReport()
        {
            glue_report_IS.Properties.DataSource = APCoreProcess.APCoreProcess.Read("sysReportDesigns  where formname like '%" + (formname) + "%'");
            glue_report_IS.Properties.ValueMember = "id";
            glue_report_IS.Properties.DisplayMember = "caption";
        }

        private void clockButton(bool khoa)
        {
            btn_new_S.Enabled = khoa;
            btn_edit_S.Enabled = khoa;
            btn_delete_S.Enabled = khoa;
            btn_cancel_S.Enabled = ! khoa;
            btn_saveinfo_S.Enabled = ! khoa;        
            gc_top_C.Enabled =  khoa;
        }

        private void khoaText(bool khoa)
        {
            them = true;
            txt_caption_IS.Properties.ReadOnly=khoa;
            txt_name_IS.Properties.ReadOnly = khoa;
            txt_path_IS.Properties.ReadOnly = khoa;
            txt_query_IS.Properties.ReadOnly = khoa;
            glue_template_IS.Properties.ReadOnly = khoa;
            chk_customSource_I6.Properties.ReadOnly = khoa;
            btn_browser_S.Enabled = !khoa;
        }

        private bool checkInput()
        {
            if (txt_name_IS.Text == "")
            {
                Function.clsFunction.MessageInfo("Thông báo", "Tên không được rỗng");
                txt_name_IS.Focus();
                return false;
            }
            if (them == true && APCoreProcess.APCoreProcess.Read("select reportname from sysReportDesigns where reportname='" + txt_name_IS.Text + "'").Rows.Count > 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Tên không được trùng");
                txt_name_IS.Focus();
                return false;
            }
            if (them == false && APCoreProcess.APCoreProcess.Read("select reportname from sysReportDesigns where reportname='" + txt_name_IS.Text + "' and id <> '"+ txt_id_IK1.Text +"'").Rows.Count > 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Tên không được trùng");
                txt_name_IS.Focus();
                return false;
            }
            if (txt_caption_IS.Text == "")
            {
                Function.clsFunction.MessageInfo("Thông báo", "Tiêu đề không được rỗng");
                txt_caption_IS.Focus();
                return false;
            }
     
                return true;
        }

        private void inSert()
        {         
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysReportDesigns where id=0");
            DataRow dr = dt.NewRow();
            dr["id"] = Function.clsFunction.layMa("", "id", "sysReportDesigns");
            dr["formname"] = formname;
            dr["reportname"] = txt_name_IS.Text;
            dr["path"] = txt_path_IS.Text;
            dr["iscurrent"] = false;
            dr["caption"] = txt_caption_IS.Text;
            dr["query"] = txt_query_IS.Text;
            dr["template"] = "";
            dr["_group"] = group;
            dr["customSource"] = chk_customSource_I6.Checked;
            dt.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
            Function.clsFunction.MessageInfo("Thông báo", "Lưu thành công");
        }

        private void eDit()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysReportDesigns where id="+glue_report_IS.Properties.View.GetRowCellValue(glue_report_IS.Properties.View.FocusedRowHandle,"id").ToString());
            //DataRow dr = dt.Rows[Function.clsFunction.getIndexIDinTable(glue_report_IS.EditValue.ToString(),"id",APCoreProcess.APCoreProcess.Read("sysReportDesigns"))];
            DataRow dr = dt.Rows[0];
            dr["formname"] = formname;
            dr["reportname"] = txt_name_IS.Text;
            dr["path"] = txt_path_IS.Text;
            dr["iscurrent"] = chk_customSource_I6.Checked;
            dr["caption"] = txt_caption_IS.Text;
            dr["query"] = txt_query_IS.Text;
            dr["template"] = "";
            dr["_group"] = group;
            dr["customSource"] = chk_customSource_I6.Checked;
            dr.EndEdit();
            APCoreProcess.APCoreProcess.Save(dr);
            Function.clsFunction.MessageInfo("Thông báo", "Cập nhật thành công");
        }

        void ThreadMethod()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = "";
                path = dlg.SelectedPath.ToString();
                txt_path_IS.Text = path;
            }
        }

        #endregion

        #region ButtonEvent

        private void chk_custom_S_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_customSource_I6.Checked == true)
                txt_query_IS.Properties.ReadOnly = false;
            else
                txt_query_IS.Properties.ReadOnly = true;
        }

        private void btn_allow_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }           

        private void btn_save_S_Click(object sender, EventArgs e)
        {
            if (chk_iscurrent_IS.Checked == true)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update sysReportDesigns set iscurrent =0 where formname='" + formname + "' and _group=" + group);
                APCoreProcess.APCoreProcess.ExcuteSQL("update sysReportDesigns set iscurrent =1 where id=" + glue_report_IS.EditValue.ToString() + "");
                Function.clsFunction.MessageInfo("Thông báo", "Lưu thành công");
            }
            else
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lưu không thành công");
            }
        }

        private void btn_new_S_Click(object sender, EventArgs e)
        {
            them = true;
            txt_caption_IS.Text = "";
            txt_name_IS.Text = "";   
            txt_query_IS.Text = "";
            glue_template_IS.EditValue = null;
            chk_customSource_I6.Checked = false;
            clockButton(false);
            khoaText(false);
        }

        private void btn_edit_S_Click(object sender, EventArgs e)
        {
            if (glue_report_IS.EditValue != null)
            {
                clockButton(false);
                khoaText(false);
                them = false;
            }
        }

        private void btn_delete_S_Click(object sender, EventArgs e)
        {
            if (glue_report_IS.EditValue != null)
            {
                khoaText(true);
                DialogResult dl = XtraMessageBox.Show("Do you want delete record", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysReportDesigns where id=" + glue_report_IS.EditValue.ToString());
                    frmConfigRePort_Load(sender, e);
                }
            }
        }

        private void btn_cancel_S_Click(object sender, EventArgs e)
        {
            clockButton(true);
            khoaText(true);
        }

        private void btn_saveinfo_S_Click(object sender, EventArgs e)
        {
            if (!checkInput())
                return;
            if (them == true)
            {
                inSert();
            }
            else
            {
                eDit();
            }
            clockButton(true);
            khoaText(true);
            frmConfigRePort_Load(sender, e);
            inhericReport();
        }

        private void inhericReport()
        {
            try
            {
                if (glue_template_IS.EditValue.ToString() !="")
                    File.Copy(Application.StartupPath + "\\Report"+"\\"+ glue_template_IS.Properties.View.GetRowCellValue(glue_template_IS.Properties.View.FocusedRowHandle,"reportname").ToString(),Application.StartupPath + "\\Report"+"\\"+txt_name_IS.Text,true);
            }
            catch 
            {
                Function.clsFunction.MessageInfo("Thông báo","Không tìm thấy file");
            }
        }

        private void glue_report_S_EditValueChanged(object sender, EventArgs e)
        {
            if (glue_report_IS.EditValue.ToString() != "")
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysReportDesigns where id=" + glue_report_IS.Properties.View.GetRowCellValue(glue_report_IS.Properties.View.FocusedRowHandle,"id").ToString());
                if (dt.Rows.Count > 0)
                {
                    txt_caption_IS.Text = dt.Rows[0]["caption"].ToString();
                    txt_name_IS.Text = dt.Rows[0]["reportname"].ToString();
                    txt_query_IS.Text = dt.Rows[0]["query"].ToString(); ;
                    glue_template_IS.EditValue = dt.Rows[0]["template"].ToString(); ;
                    chk_iscurrent_IS.Checked = (bool)dt.Rows[0]["iscurrent"];
                    chk_customSource_I6.Checked = (bool)dt.Rows[0]["customSource"];
                    txt_id_IK1.Text= dt.Rows[0]["id"].ToString();
                }
            }
        }

        private void btn_run_S_Click(object sender, EventArgs e)
        {
            try
            {
                string ex1 = txt_path_IS.Text + "\\" + txt_name_IS.Text + ".repx";
                if (!File.Exists(ex1))
                {
                    createReport();
                }
                else
                {
                    openReport();
                }
            }
            catch(Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }

        private void createReport()
        {
            string sqle = "select * from " + Function.clsFunction.getNameControls(formname);
            if (chk_customSource_I6.Checked == true)
                sqle = txt_query_IS.Text;
            XtraReport xrp;
            if (chk_pro_S.Checked == true)
            {
                string ex1 = txt_path_IS.Text + "\\" + txt_name_IS.Text + ".repx";
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    xrp = new XR_Report(txt_query_IS.Text,arrParam, Data.APCoreData.chuoiKetNoi);
                else
                    xrp = new XR_Report(txt_query_IS.Text, Data.APCoreDataSQLCE.chuoiKetNoi); 
            }
            else
            {
                string ex1 = txt_path_IS.Text + "\\" + txt_name_IS.Text + ".repx";
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    xrp = new XR_Report(txt_query_IS.Text, Data.APCoreData.chuoiKetNoi);
                else
                    xrp = new XR_Report(txt_query_IS.Text, Data.APCoreDataSQLCE.chuoiKetNoi); 
            }
  
            clsReportDesign.LaunchCommandLineOpen(sqle, txt_path_IS.Text, txt_name_IS.Text, Convert.ToInt32(glue_report_IS.EditValue), xrp, (bool) rad_option_C.EditValue);                          
        }

        private void openReport()
        {
            string sqle = "select * from " + Function.clsFunction.getNameControls(formname);
            if (chk_customSource_I6.Checked == true)
                sqle = txt_query_IS.Text;
            XtraReport xrp=new XtraReport();
            string ex1 = txt_path_IS.Text + "\\" + txt_name_IS.Text + ".repx";
            //xrp =  XR_Report.FromFile(ex1,true);
            xrp.LoadLayout(ex1);
            clsReportDesign.LaunchCommandLineAppEdit(sqle, txt_path_IS.Text, txt_name_IS.Text, Convert.ToInt32(glue_report_IS.EditValue), xrp, (bool)rad_option_C.EditValue);

        }

        private void btn_browser_S_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(ThreadMethod));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                XtraReport report = new XtraReport();// = CreateReport();
                report.LoadLayout("C:\\LoyaltyData\\ddd.repx");
                DataSet ds = new DataSet();
                DataTable dtthong = new DataTable();
                dtthong = APCoreProcess.APCoreProcess.Read("thongtin");
                ds.Tables.Add(dtthong);
                DataTable dtPhongban = new DataTable();
                dtPhongban = APCoreProcess.APCoreProcess.Read("nhapphongban");
                ds.Tables.Add(dtPhongban);
                DataTable dtNhanvien = new DataTable();
                dtNhanvien = APCoreProcess.APCoreProcess.Read("nhapnhanvien");
                ds.Tables.Add(dtNhanvien);
                ds.Relations.Add("BrandNameStr", dtPhongban.Columns["maphongban"], dtNhanvien.Columns["maphongban"]);
                report.DataSource = ds;
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo(ex.Message,"Thông báo");
            }
        }

        private void glue_template_IS_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txt_query_IS.Text = glue_template_IS.Properties.View.GetRowCellValue(glue_template_IS.Properties.View.FocusedRowHandle, "query").ToString();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }


        #endregion              
               
    }
}