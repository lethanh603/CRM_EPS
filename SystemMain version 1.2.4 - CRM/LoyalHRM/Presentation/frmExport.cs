using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using Function;

namespace LoyalHRM.Presentation
{
    public partial class frmExport : DevExpress.XtraEditors.XtraForm
    {
        public frmExport()
        {
            InitializeComponent();
        }
        DataTable dtExcel = new DataTable();
        //Microsoft.Office.Interop.Excel.Application oXL;
        //Microsoft.Office.Interop.Excel.Workbook oWB;
        //Microsoft.Office.Interop.Excel.Worksheet oSheet;
        //Microsoft.Office.Interop.Excel.Range oRange;
        private void frmExport_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("table");
            DataRow dr = dt.NewRow();
            dr["id"] = "ctr";
            dr["table"] = "sysControls";
            dt.Rows.Add(dr);
          
            DataRow dr1 = dt.NewRow();
            dr1["id"] = "grid";
            dr1["table"] = "  sysGridColumns";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["id"] = "mess";
            dr2["table"] = "sysMessage";
            dt.Rows.Add(dr2);

            lue_bang_S.Properties.DataSource = dt;
            lue_bang_S.Properties.ValueMember = "id";
            lue_bang_S.Properties.DisplayMember = "table";
            lue_bang_S.ItemIndex = 0;
            Function.clsFunction.TranslateForm(this, this.Name);
        }

        private void btn_xuat_S_Click(object sender, EventArgs e)
        {
            Function.clsImportExcel.exportDataToExcel("xxx",APCoreProcess.APCoreProcess.Read(lue_bang_S.Text));
            MessageBox.Show("thanh cong");
        }


        //public bool exportReport(int type, ReportDocument repd)
        //{
        //    SaveFileDialog f = new SaveFileDialog();
        //    bool result = false;
        //    switch (type)
        //    {
        //        case 1:

        //            f.Filter = "Word file(*.doc)|*.doc";
        //            if (f.ShowDialog() == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        case 2:

        //            f.Filter = "Pdf file(*.pdf)|*.pdf";
        //            if (f.ShowDialog() == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        case 3:

        //            f.Filter = "Excel file(*.xls)|*.xls";
        //            if (f.ShowDialog() == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        default:
        //            MessageBox.Show("Khong chon dung loai ");
        //            break;


        //    }
        //    return result;
        //}

       

        private void importMess()
        {
            System.Data.DataTable dt = new DataTable();

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName,0);
                        dt = excel.getDataFromExcel();
                        if (dt == null)
                        {
                            return;
                        }
                        int i = 0;
                        i = 0;
                        //xóa các dòng trống của file excel hoặc cột MaHocSinh ko có
                        while (i <= dt.Rows.Count - 1)
                        {
                            if (dt.Rows[i]["id"].ToString() == string.Empty)
                            {
                                dt.Rows.RemoveAt(i);
                                dt.AcceptChanges();
                            }
                            else
                            {
                                i += 1;
                            }
                        }

                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysMessage");
                        System.Data.DataTable dtpb = new System.Data.DataTable();
                        dtpb = APCoreProcess.APCoreProcess.Read("sysMessage");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow dr = dtpb.NewRow();
                            dr["title_VI"] = dt.Rows[j]["title_VI"].ToString().Trim();
                            dr["message_VI"] = dt.Rows[j]["message_VI"];
                            dr["title_EN"] = dt.Rows[j]["title_EN"];
                            dr["message_EN"] = dt.Rows[j]["message_EN"];
                      
             
                            dtpb.Rows.Add(dr);
                            APCoreProcess.APCoreProcess.Save(dr);
                        }
                        XtraMessageBox.Show("Cập nhật thành công");

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void importGrid()
        {
            System.Data.DataTable dt = new DataTable();

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName,1);
                        dt = excel.getDataFromExcel();
                        if (dt == null)
                        {
                            return;
                        }
                        int i = 0;
                        i = 0;
                        //xóa các dòng trống của file excel hoặc cột MaHocSinh ko có
                        while (i <= dt.Rows.Count - 1)
                        {
                            if (dt.Rows[i]["form_name"].ToString() == string.Empty)
                            {
                                dt.Rows.RemoveAt(i);
                                dt.AcceptChanges();
                            }
                            else
                            {
                                i += 1;
                            }
                        }

                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysGridColumns");
                        System.Data.DataTable dtpb = new System.Data.DataTable();
                        dtpb = APCoreProcess.APCoreProcess.Read("sysGridColumns");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow dr = dtpb.NewRow();
                            dr["caption_VI"] = dt.Rows[j]["caption_VI"].ToString().Trim();
                            dr["caption_lue_col_VI"] = dt.Rows[j]["caption_lue_col_VI"];
                            dr["caption_lue_col_EN"] = dt.Rows[j]["caption_lue_col_EN"];
                            dr["caption_EN"] = dt.Rows[j]["caption_EN"];


                            dtpb.Rows.Add(dr);
                            APCoreProcess.APCoreProcess.Save(dr);
                        }
                        XtraMessageBox.Show("Cập nhật thành công");

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void importCtr()
        {
            System.Data.DataTable dt = new DataTable();

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != "")
                    {
                        clsImportExcel excel = new clsImportExcel(openFileDialog1.FileName,0);
                        dt = excel.getDataFromExcel();
                        if (dt == null)
                        {
                            return;
                        }
                     
                        //xóa các dòng trống của file excel hoặc cột MaHocSinh ko có
                        //while (i <= dt.Rows.Count - 1)
                        //{
                        //    if (dt.Rows[i]["form_name"].ToString() == string.Empty)
                        //    {
                        //        dt.Rows.RemoveAt(i);
                        //        dt.AcceptChanges();
                        //    }
                        //    else
                        //    {
                        //        i += 1;
                        //    }
                        //}

                        //APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysControls");
                        System.Data.DataTable dtpb = new System.Data.DataTable();
                        dtpb = APCoreProcess.APCoreProcess.Read("sysControls");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow dr = dtpb.Rows[j];
                            dr["text_VI"] = dt.Rows[j]["text_VI"].ToString().Trim();
                            //dr["caption_col_lue_VI"] = dt.Rows[j]["caption_lue_col_VI"];
                            //dr["caption_col_lue_EN"] = dt.Rows[j]["caption_lue_col_EN"];
                            dr["text_EN"] = dt.Rows[j]["text_EN"];


                            //dtpb.Rows.Add(dr);
                            APCoreProcess.APCoreProcess.Save(dr);
                        }
                        XtraMessageBox.Show("Cập nhật thành công");

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btn_nhap_S_Click(object sender, EventArgs e)
        {
            if(lue_bang_S.EditValue.ToString()=="mess")
                importMess();
            if (lue_bang_S.EditValue.ToString() == "ctr")
                importCtr();
            if (lue_bang_S.EditValue.ToString() == "grid")
                importGrid();
        }
    }
}