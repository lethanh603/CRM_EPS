using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Net.Mail;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using System.IO;

namespace DMNHANVIENKH.Presentation
{
    public partial class frmMailServer : DevExpress.XtraEditors.XtraForm
    {
        public frmMailServer()
        {
            InitializeComponent();
        }

        #region Var
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        public string langues = "]XUPID";
        System.Data.DataTable dtMessage = new System.Data.DataTable();
        #endregion

        #region FormEvent

        private void frmMailServer_Load(object sender, EventArgs e)
        {
            loadMail();
            loadFile();
            chk_all_S.Checked = true;
            chk_all_S_CheckedChanged(sender, e);
        }

        #endregion

        #region GridEvent


        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            bool indicatorIcon = false;
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = Convert.ToString(int.Parse(e.RowHandle.ToString()) + 1);

                if (!indicatorIcon)
                    e.Info.ImageIndex = -1;
            }
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Appearance.DrawString(e.Cache, e.RowHandle.ToString(), e.Bounds);
                e.Info.DisplayText = "No";
            }

            e.Painter.DrawObject(e.Info);
            clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
            e.Handled = true;
        }
        #endregion

        #region Event

        #endregion

        #region Methods

        private void Xuat()
        {
            bool status = false;
            saveFileDialog1.Filter = "Excel 97 - 2003 (*.xls)|*.xls |Excel 2007(*.xlsx)|*.xlsx |Pdf (*.pdf)|*.pdf |Webpage (*.html)|*.html |Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog1.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog1.FileName);
                    if (f.Extension.Equals(".xls"))
                    {
                        DialogResult dl;

                        System.Data.DataTable dtMessage = new System.Data.DataTable();
                        dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                        dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[2]["message" + langues].ToString(), dtMessage.Rows[2]["title" + langues].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dl == DialogResult.Yes)
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
                        DialogResult dl;


                        dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[2]["message" + langues].ToString(), dtMessage.Rows[2]["title" + langues].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dl == DialogResult.Yes)
                        {

                            gv_list_C.ExportToXlsx(f.FullName);

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
                        DialogResult dl;

                        System.Data.DataTable dtMessage = new System.Data.DataTable();
                        dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                        dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[2]["message" + langues].ToString(), dtMessage.Rows[2]["title" + langues].ToString());
                        if (dl == DialogResult.Yes)
                        {

                            gv_list_C.ExportToPdf(f.FullName);

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
                        DialogResult dl;

                        System.Data.DataTable dtMessage = new System.Data.DataTable();
                        dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                        dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[2]["message" + langues].ToString(), dtMessage.Rows[2]["title" + langues].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dl == DialogResult.Yes)
                        {

                            gv_list_C.ExportToRtf(f.FullName);

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
                        DialogResult dl;



                        dl = DevExpress.XtraEditors.XtraMessageBox.Show(dtMessage.Rows[2]["message" + langues].ToString(), dtMessage.Rows[2]["title" + langues].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dl == DialogResult.Yes)
                        {

                            gv_list_C.ExportToHtml(f.FullName);

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

        private void loadMail()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select email, tenkhachhang as name from nhapkhachhangkh union select email, name from LISTMAIL ");
            dt.Columns.Add("check", typeof(bool));
            dt.Columns["check"].DefaultValue = true;
            gct_list_C.DataSource = dt;

 
        }
        #endregion
        private void loadFile()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            gct_file_C.DataSource = dt;
        }
        private void chk_all_S_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all_S.Checked == true)
            {
                for (int i = 0; i < gv_list_C.DataRowCount; i++)
                {
                    gv_list_C.SetRowCellValue(i, gcCheck, true);
                }

            }
            else
            {
                for (int i = 0; i < gv_list_C.DataRowCount; i++)
                {
                    gv_list_C.SetRowCellValue(i, gcCheck, false);
                }
            }
        }

        private void bbi_import_C_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IMPORTEXCEL.frm_inPut frm = new IMPORTEXCEL.frm_inPut();
            frm.tbName = "LISTMAIL";
            frm.sDauma = "";
            
            frm.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("Configmail");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];                

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(dr["smtp"].ToString());
                    mail.From = new MailAddress(dr["emailaddress"].ToString());
                    for (int i = 0; i < gv_list_C.DataRowCount; i++)
                    {
                        if (gv_list_C.GetRowCellValue(i, gcMail).ToString() != "" && gv_list_C.GetRowCellValue(i, gcCheck).ToString() != "False")
                        {
                            mail.To.Add(gv_list_C.GetRowCellValue(i, gcMail).ToString());
                        }
                    }
                    mail.Subject = mmo_subject_C.Text;
                    mail.Body = rich_Text_C.Text;
                    if (gv_file_C.DataRowCount > 0)
                    {
                        for (int j = 0; j < gv_file_C.DataRowCount; j++)
                        {
                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(gv_file_C.GetRowCellValue(j, gcPath).ToString());
                            mail.Attachments.Add(attachment);
                        }
                    }
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Port = Convert.ToInt32(dr["port"]);
                    SmtpServer.Credentials = new System.Net.NetworkCredential(dr["username"].ToString(), Function.clsFunction.giaima(dr["password"].ToString()));
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);                        
                    
                    XtraMessageBox.Show("Mail Send");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void rich_Text_C_Click(object sender, EventArgs e)
        {

        }

        private void btn_attack_S_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        DataTable dt = new DataTable();
                        dt = (DataTable)gct_file_C.DataSource;
                       
                        {
                            DataRow dr = dt.NewRow();
                            dr["File"] = System.IO.Path.GetFileName(openFileDialog1.FileName);
                            dr["Path"] = openFileDialog1.FileName;
                            dt.Rows.Add(dr);
                            gct_file_C.DataSource = dt;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
               
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btn_delete_S_Click(object sender, EventArgs e)
        {
            gv_file_C.DeleteRow(gv_file_C.FocusedRowHandle);
        }

        private void bbi_config_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void bbi_exit_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbi_export_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xuat();
        }

   



    }
}