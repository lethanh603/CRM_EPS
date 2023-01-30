using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using Function;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using System.Reflection;
using DevExpress.XtraGrid.Columns;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frm_RETAIL_S : DevExpress.XtraEditors.XtraForm
    {
        
        #region Contructor
        public frm_RETAIL_S()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "PX";
        //private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public delegate void PassData(bool value);
        public PassData passData;

        private bool changeTable = false;
        private string idTableChoose = "";
        private string TableChoose = "";

        private bool MergeTable = false;
        private bool UnionTable = false;
        private bool SplitTable = false;
        private bool give = false;

        private double calprice = 0;
        private double calamountdiscount = 0;
        private double caldiscount = 0;
        private double calquantity = 0;
        private bool bvalue = false;
        private string idComCurrent = "";
        private DateTime dateExport;

        #endregion

        #region FormEvent

        private void frm_DMAREA_SH_Load(object sender, EventArgs e)
        {
            try
            {
                //updateSQL();
                Function.clsFunction._keylience = true;
                if (statusForm == true)
                {
                    SaveGridControls();
                    clsFunction.Save_sysControl(this, this);
                    clsFunction.CreateTable(this);
                }
                else
                {
                    Function.clsFunction.TranslateForm(this, this.Name);
                    //Load_Grid();
                    loadGridtable();
                    loadGridGroup();
                    Function.clsFunction.TranslateGridColumn(gv_list_C);
                    Init();
                    InitInvoice();
                    loadInvoice();
      


                    gv_table_C.RefreshRowCell(0,gv_table_C.VisibleColumns[0]);
                }
            }
            catch { }
        }

        private void frm_DMAREA_SH_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    btn_enter_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F2)
                {
                    btn_callmenu_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F3)
                {
                    btn_info_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F4)
                {
                    lbl_refresh_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F5)
                {
                    btn_change_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F6)
                {
                    btn_input_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F7)
                {
                    btn_union_S_Click(sender, e);
                }
                if (e.KeyCode == Keys.F8)
                {
                    btn_div_S_Click(sender, e);
                }
            }
            catch
            {
            }
        }

        private void frm_RETAIL_S_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn thoát chương trình"))
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //catch { this.Close(); }
        }
                      
        #endregion

        #region ButtonEvent
        

        private void btn_enter_S_Click(object sender, EventArgs e)
        {
           
            setClickButton(btn_enter_S.Text);
        }

        private void btn_callmenu_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_callmenu_S.Text);
        }

        private void btn_info_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_info_S.Text);
        }

        private void btn_change_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_change_S.Text);
        }

        private void btn_input_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_input_S.Text);
        }

        private void btn_union_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_union_S.Text);
        }

        private void btn_div_S_Click(object sender, EventArgs e)
        {
            setClickButton(btn_div_S.Text);
        }

        private void btn_thoat_S_Click(object sender, EventArgs e)
        {
            //if(clsFunction.MessageDelete("Thông báo","Bạn có chắc muốn thoát khỏi chương trình không ?"))
            //    this.Close();
            split_main_C.CollapsePanel = SplitCollapsePanel.Panel1;
            split_main_C.Collapsed = true;
        }
        
        #endregion

        #region Event

        private void chk_issurcharge_I6_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (checkIssurcharge() == true)
            {
                if (Convert.ToBoolean(e.OldValue) == true && Convert.ToBoolean(e.NewValue) == false)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn hủy phụ thu cho hóa đơn này") == false)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void chk_isdiscount_I6_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (checkIsdiscount() == true)
            {
                if (Convert.ToBoolean(e.OldValue) == true && Convert.ToBoolean(e.NewValue) == false)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Bạn có muốn hủy giảm giá cho hóa đơn này") == false)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void cal_costs_S_EditValueChanged(object sender, EventArgs e)
        {
            if (cal_discount_I4.Value > 0)
            {
                cal_amountdiscount_I4.Value = Convert.ToDecimal(Convert.ToInt32(cal_discount_I4.Value * cal_costs_I1.Value / 100));
            }
        }

        private void gct_list_C_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                createpopupmenu();
        }

        private void txt_search_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (APCoreProcess.APCoreProcess.IssqlCe == false)
                {
                    dt = APCoreProcess.APCoreProcess.Read("declare @date1 nvarchar(15), @date2 nvarchar(15) set @date1='" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "' set @date2='" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "' select hh.idcommodity, hh.sign,hh.commodity, hh.price, ISNULL( (select top 1 dt.newprice from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion where 1=1 and  dt.idcommodity=hh.idcommodity and  CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case when  mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1 and  CONVERT(datetime,getdate(),103) between CONVERT(datetime,@date1,103) and case when mt.istimegold=1 then    CONVERT(datetime,@date2,103) else CONVERT(datetime,getdate(),103) end  order by mt.fromdate desc ),0) as amountdiscount, case when ISNULL( (select top 1 dt.newprice from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion where 1=1 and  dt.idcommodity=hh.idcommodity and CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case  when mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1 and CONVERT(datetime,getdate(),103) between CONVERT(datetime,@date1,103) and case when mt.istimegold=1 then CONVERT(datetime,@date2,103) else CONVERT(datetime,getdate(),103) end order by mt.fromdate desc ),0) >0 then  hh.price - ISNULL( (select top 1 dt.newprice from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion where 1=1 and dt.idcommodity=hh.idcommodity and  CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case when  mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1   and CONVERT(datetime,getdate(),103) between CONVERT(datetime,@date1,103) and  case when mt.istimegold=1 then CONVERT(datetime,@date2,103) else CONVERT(datetime,getdate(),103) end order by mt.fromdate desc ),0) else 0 end   as discount,          isnull( (select top 1 dt.idpromotion from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion where 1=1 and  dt.idcommodity=hh.idcommodity and  CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case when  mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1 and   CONVERT(datetime,getdate(),103) between CONVERT(datetime,@date1,103) and case when mt.istimegold=1 then    CONVERT(datetime,@date2,103) else CONVERT(datetime,getdate(),103) end  order by mt.fromdate desc ),'') as idpromotion from dmcommodity hh where hh.status=1  and sign like '" + txt_search_I4.Text + "%'");
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read(" select hh.idcommodity, hh.sign,hh.commodity, hh.price,    case when ( P1.newprice) is null then 0 else P1.newprice end  as amountdiscount,   hh.price -   case when ( P1.newprice) is null then 0 else P1.newprice end  as discount,       case when   (P1.idpromotion)  is null then '' else   (P1.idpromotion) end    as idpromotion from dmcommodity as hh   left join (select  dt.newprice, dt.idcommodity, mt.idpromotion from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion inner join dmcommodity h1 on h1.idcommodity=dt.idcommodity where 1=1 and h1.sign like '" + txt_search_I4.Text + "%' and CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case when  mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1 and  CONVERT(datetime,getdate(),103) between CONVERT(datetime,'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "',103) and case when mt.istimegold=1 then    CONVERT(datetime,'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "',103) else CONVERT(datetime,getdate(),103) end  ) as P1 on hh.idcommodity=P1.idcommodity   where hh.status=1  and hh.sign like '" + txt_search_I4.Text + "%'");
                }
                    if (dt.Rows.Count > 0)
                {
                    gct_listmenu_C.DataSource = dt;
                }
                else
                {
                    gct_listmenu_C.DataSource = null;
                }
            }
            catch { }
        }

        private void createpopupmenu()
        {
            try
            {
                int index = 0;
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                System.Data.DataTable dt = new System.Data.DataTable();
                string[,] arr = new string[4, 3] { { "Hoàn tác", "", "bbi_hoantac_S" }, { "In lại hóa đơn", "", "bbi_inlai_S" }, { "Xóa hóa đơn", "", "bbi_delete_S" }, { "Sửa hóa đơn", "", "bbi_edit_S" } };
                for (int i = 0; i < arr.Length/3; i++)
                {
                    Bitmap image = null;//= (Bitmap)QLBH.Properties._32px_Crystal_Clear_action_exit.Clone();
                    if (arr[i, 1].ToString() != "")
                    {
                        image = (Bitmap)Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\Image_Form\\" + arr[i, 1].ToString() + ".png").Clone();
                        image.MakeTransparent(Color.Fuchsia);
                        contextMenuStrip.Items[index].Image = image;
                    }
                    contextMenuStrip.Items.Add(clsFunction.transLateText(arr[i, 0].ToString()));
                    contextMenuStrip.Items[index].Name = arr[i, 2].ToString();

                    index++;
                }
                contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
                contextMenuStrip.Show(this, PointToClient(MousePosition));
                gct_list_C.ContextMenuStrip = contextMenuStrip;
            }
            catch { }
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name.Contains("bbi_hoantac_S"))
                {
                    string idreceipt = "";
                    idreceipt = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idreceipt").ToString();
                    cancelInvoice(idreceipt);
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("Hoàn tác"), "", "1");
                }
                if (e.ClickedItem.Name.Contains("bbi_inlai_S"))
                {
                    print(getIdexportByIdreceipt(gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idreceipt").ToString()));
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("In lại hóa đơn"), "", "1");
                }
                if (e.ClickedItem.Name.Contains("bbi_delete_S"))
                {
                    if (gv_list_C.RowCount > 0)
                    {
                        if (clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa hóa đơn đã thanh toán này không ?"))
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("update receipt set del=1 where idexport ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idexport").ToString() + "'");
                            //APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idexport").ToString() + "'");
                            //APCoreProcess.APCoreProcess.ExcuteSQL("delete export where idexport ='" + gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idexport").ToString() + "'");
                            insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("Xóa hóa đơn"), gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idexport").ToString(), "1");
                            loadInvoice();
                        }
                       
                    }
                }
                if (e.ClickedItem.Name.Contains("bbi_edit_S"))
                {
                    if (gv_list_C.RowCount > 0)
                    {
                        frmEditInvoice frm = new frmEditInvoice();
                        frm.idtable = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"idtable").ToString();
                        frm.passData = new frmEditInvoice.PassData(getValueEditInvoice);
                        frm.idinvoice = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle,"idexport").ToString();
                        frm.ShowDialog();
                    }
                }
                gv_table_C.LayoutChanged();
            }
            catch { }
        }

        private void getValueEditInvoice(bool val)
        {
            if (val == true)
            {
                loadInvoice();
            }
        }

        private string getIdexportByIdreceipt(string Idreceipt)
        {
            string invoice = "";
            // lấy số hóa đơn đang tồn tại trong phiên làm việc
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1 idexport  from receipt where idreceipt='" + Idreceipt + "' ");
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0][0].ToString();
            }
            return invoice;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lbl_time_S.Text = DateTime.Now.ToString("HH:mm:ss");
                if (lbl_start_S.Text != "00:00:00")
                {
                    lbl_time1_S.Text = (DateTime.Now.Subtract(dateExport).Days*24 + DateTime.Now.Subtract(dateExport).Hours).ToString().PadLeft(2,'0').ToString() + ":" +DateTime.Now.Subtract(dateExport).Minutes.ToString().PadLeft(2,'0') + ":" + DateTime.Now.Subtract(dateExport).Seconds.ToString().PadLeft(2,'0');
                    //lbl_time1_S.Text = DateTime.Now.Subtract(dateExport).ToString("HH:mm:ss");
                }
            }
            catch { }
        }

        private void split_main_C_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (split_main_C.CollapsePanel.ToString() != "Panel1")
            {
                btn_info_S.Text = clsFunction.transLateText("F3 << Menu");
            }
            else
            {
                btn_info_S.Text = clsFunction.transLateText("F3 Thông tin");
            }
        }

        private void lbl_change_S_Click(object sender, EventArgs e)
        {
            idTableChoose = "";
            TableChoose = "";
            changeTable = false;
            UnionTable = false;
            MergeTable = false;
            SplitTable = false;
            lbl_change_S.Text = "";
        }
    
        private void cal_surcharge_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16( cal_surcharge_I4.Value) !=  Convert.ToInt16 (cal_amountsurcharge_I4.Value * 100 / cal_costs_I1.Value))
                {
                    cal_amountsurcharge_I4.Value = (cal_surcharge_I4.Value / 100 * cal_costs_I1.Value);
                }
            }
            catch { }
        }

        private void cal_discount_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cal_discount_I4.Value) != Convert.ToInt16(cal_amountdiscount_I4.Value * 100 / cal_costs_I1.Value))
                {
                    cal_amountdiscount_I4.Value = (cal_discount_I4.Value / 100 * cal_costs_I1.Value);
                }
            }
            catch { }
        }

        private void cal_amountsurcharge_S_EditValueChanged(object sender, EventArgs e)
        {
            loadTotal();
            try
            {
                if (Math.Round(cal_amountsurcharge_I4.Value / cal_costs_I1.Value * 100, 1) != Math.Round(cal_surcharge_I4.Value, 1))
                {
                    cal_surcharge_I4.Value = Math.Round(cal_amountsurcharge_I4.Value / cal_costs_I1.Value * 100, 0);
                }
                savesurcharge(chk_issurcharge_I6.Checked,Convert.ToDouble(cal_surcharge_I4.Value),Convert.ToDouble(cal_amountsurcharge_I4.Value));
            }
            catch { }
        }

        private void cal_amountdiscount_S_EditValueChanged(object sender, EventArgs e)
        {
            loadTotal();
            try
            {
                if (Math.Round(cal_amountdiscount_I4.Value / cal_costs_I1.Value * 100, 1) != Math.Round(cal_discount_I4.Value, 1))
                {
                    cal_discount_I4.Value = Math.Round(cal_amountdiscount_I4.Value / cal_costs_I1.Value * 100, 0);
                }
                savediscount(chk_isdiscount_I6.Checked,Convert.ToDouble(cal_discount_I4.Value),Convert.ToDouble(cal_amountdiscount_I4.Value));
            }
            catch { }
        }

        private void lbl_refresh_S_Click(object sender, EventArgs e)
        {
            txt_search_I4.Text = "";
        }

        private void lbl_give_S_Click(object sender, EventArgs e)
        {
            split_menu_C.CollapsePanel = SplitCollapsePanel.Panel2;
            split_menu_C.Collapsed = true;
        }

        private void chk_issurchage_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_issurcharge_I6.Checked == true && checkIssurcharge() == false)
            {
                frm_Reason frm = new frm_Reason();
                frm.idexport = lbl_idpurchase_S.Text;
                frm.idcommodity = "";
                frm.status = 1;
                frm.passData = new frm_Reason.PassData(getValueSurcharge);
                frm.ShowDialog();
            }
            else
            {
                if (chk_issurcharge_I6.Checked == false)
                {
                    savesurcharge(false, 0, 0);
                    cal_amountsurcharge_I4.Value = 0;
                    cal_surcharge_I4.Value = 0;
                }
            }
            cal_surcharge_I4.Properties.ReadOnly = ! Convert.ToBoolean(chk_issurcharge_I6.Checked);
            cal_amountsurcharge_I4.Properties.ReadOnly = ! Convert.ToBoolean(chk_issurcharge_I6.Checked);
        }

        private bool checkIssurcharge()
        {
            bool flag = false;
            if (APCoreProcess.APCoreProcess.Read("select issurcharge from export where idexport='" + lbl_idpurchase_S.Text + "' and issurcharge =1").Rows.Count > 0)
                flag = true;
            return flag;
        }

        private bool checkIsdiscount()
        {
            bool flag = false;
            if (APCoreProcess.APCoreProcess.Read("select isdiscount from export where idexport='" + lbl_idpurchase_S.Text + "' and isdiscount =1").Rows.Count > 0)
                flag = true;
            return flag;
        }

        private void getValueSurcharge(bool val)
        {
            if (val == true)
            {
                chk_issurcharge_I6.Checked = true;
                cal_surcharge_I4.Properties.ReadOnly = false;
                cal_amountsurcharge_I4.Properties.ReadOnly = false;
            }
            else
            {
                chk_issurcharge_I6.Checked = false;
                cal_surcharge_I4.Properties.ReadOnly = true;
                cal_amountsurcharge_I4.Properties.ReadOnly = true;
            }
        }

        private void chk_isdiscount_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isdiscount_I6.Checked == true && checkIsdiscount() == false)
            {
                frm_Reason frm = new frm_Reason();
                frm.idexport = lbl_idpurchase_S.Text;
                frm.idcommodity = "";
                frm.status = 0;
                frm.passData = new frm_Reason.PassData(getValueDiscount);
                frm.ShowDialog();
            }
            else
            {
                if (chk_isdiscount_I6.Checked == false)
                {
                    savediscount(false, 0, 0);
                    cal_discount_I4.Value = 0;
                    cal_amountdiscount_I4.Value = 0;
                }
            }
            cal_discount_I4.Properties.ReadOnly = !Convert.ToBoolean(chk_isdiscount_I6.Checked);
            cal_amountdiscount_I4.Properties.ReadOnly = !Convert.ToBoolean(chk_isdiscount_I6.Checked);
        }

        private void getValueDiscount(bool val)
        {
            if (val == true)
            {
                chk_isdiscount_I6.Checked = true;
                cal_discount_I4.Properties.ReadOnly = false;
                cal_amountdiscount_I4.Properties.ReadOnly = false;
            }
            else
            {
                chk_isdiscount_I6.Checked = false;
                cal_discount_I4.Properties.ReadOnly = true;
                cal_amountdiscount_I4.Properties.ReadOnly = true;
            }
        }
        #endregion

        #region GridEvent

        private void SaveGridControls()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { "quantity",  "mount",  "amountvat", "amountdiscount", "costs", "total" };
            // Caption column
            string[] caption_col = new string[] {"ID", "Mã hàng", "Ký hiệu", "Tên hàng", "ĐVT", "Kho hàng", "Số lượng", "Đơn giá","Thành tiền", "VAT", "Tiền VAT", "CK Sau VAT", "CK", "Tiền CK","Chi phí","Tổng tiền", "Ghi chú","ID" };
         
            // FieldName column từ khóa column không được viết in hoa trừ từ khóa quy định kiểu
            string[] fieldname_col = new string[] { "col_iddetail_IK1", "col_idcommodity_I1", "col_sign_20_S", "col_commodity_S", "col_idunit_I1", "col_idwarehouse_I1", "col_quantity_I4", "col_price_I4", "col_amount_I15", "col_vat_I4", "col_amountvat_I15", "col_davat_I6", "col_discount_I8", "col_amountdiscount_I15", "col_costs_I15", "col_total_I15", "col_note_I3", "col_idexport_I1" };
           
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "TextColumn", "TextColumn", "GridLookupEditColumn", "GridLookupEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CheckColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "CalcEditColumn", "MemoColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100","100", "100", "200", "60", "200", "100", "100", "100", "60", "100", "100", "60", "100", "100", "100", "200","100" };
            //AllowFocus
            string[] AllowFocus = new string[] {"False", "True", "False", "False", "True", "True", "True", "True", "False", "True", "False", "True", "True", "False", "True", "False","True","False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "False", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "True", "False" };
            // datasource lookupEdit
            string[] sql_lue = new string[] { };
            // Caption lookupEdit
            string[] caption_lue = new string[] { };
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] { };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[0, 0];
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[0, 0];
            //so cot
            //int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idcommodity, commodity  from dmcommodity where status=1", "select idunit, unit  from dmunit where status=1", "select idwarehouse, warehouse  from dmwarehouse where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] {"commodity","unit","warehouse"};
            
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idcommodity", "idunit", "idwarehouse" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[3, 2] { { "Mã hàng", "Tên hàng" }, { "Mã ĐV", "ĐVT" }, { "Mã kho", "Kho" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[3, 2] { { "idcommodity", "commodity" },  { "idunit", "unit" }, { "idwarehouse", "warehouse" } };
            //so cot
            //int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns_Edit(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid,gv_list_C.Name);
            clsFunction.CreateTableGrid(fieldname_col, gv_list_C);
        }

        private void Load_Grid()
        {
            string text = Function.clsFunction.langgues;

            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            string[] gluenulltext = new string[] { "Nhập mã", "Nhập ĐVT", "Nhập kho" };
            bool show_footer = true;
            // show filterRow
            gv_list_C.OptionsView.ShowAutoFilterRow = true;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='"+ gv_list_C.Name +"'");
            
            try
            {
                ControlDev.FormatControls.Controls_in_Grid_Edit(gv_list_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_list_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["column_name"].ToString().Split('/'), dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue" ].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col" ].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()),gluenulltext);
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_list_C.OptionsBehavior.Editable = true;
                gv_list_C.OptionsView.ColumnAutoWidth = false;
                gv_list_C.OptionsView.ShowAutoFilterRow = false;

            }
          
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            clsG.DoDefaultDrawCell(view, e);
            clsG.DrawCellBorder(e.RowHandle, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).RowInfo.DataBounds, e.Graphics);
            e.Handled = true;
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                e.Info.DisplayText = Function.clsFunction.transLateText("STT");
            }

            e.Painter.DrawObject(e.Info);
            clsG.DrawCellBorder(e.RowHandle, e.Bounds, e.Graphics);
            e.Handled = true;
        }

        private void gv_table_C_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
    
                if (e.RowHandle >= 0)
                {
                    int state = -1;
                    state = checkStatusTable(e.CellValue.ToString());
                
                    if (state == 0)
                    {
                        e.Appearance.BackColor = Color.Azure;
                        //e.Appearance.Image = imageCollection1.Images[0];
                    }
                    else 
                    {
                        if (state == 1)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                            //e.Appearance.Image = imageCollection1.Images[1];
                           
                        }
                        else
                        {
                            if (state == 2)
                            {
                                e.Appearance.BackColor = Color.Gray;
                            }
                            else
                            {
                                if(state==3)
                                    e.Appearance.BackColor = Color.Red;
                                else
                                    e.Appearance.BackColor = Color.Silver;
                            }
                        }                        
                    }
                }
            }
            catch 
            { }
        }
        
        private void gv_table_C_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = (GridView)sender;
            int state = -1;
            state = checkStatusTable(e.CellValue.ToString());
            if (state == 0)
            {
                Image img = null;
                try
                {
                    img = Image.FromFile(Application.StartupPath + "\\Images\\Table\\Ban2.gif");
                }
                catch
                { ;}
                e.Appearance.ForeColor = Color.White;
                e.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Graphics.DrawImage(img, e.Bounds);
                e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                e.Handled = true;
            }
            else
            {
                if (state == 1)
                {
                    Image img = null;
                    try
                    {
                        img = Image.FromFile(Application.StartupPath + "\\Images\\Table\\Ban3.gif");
                    }
                    catch
                    { ;}
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.Options.UseTextOptions = true;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Graphics.DrawImage(img, e.Bounds);
                    e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                    e.Handled = true;
                }
                else
                {
                    if (state == 2)
                    {
                        Image img = null;
                        try
                        {
                            img = Image.FromFile(Application.StartupPath + "\\Images\\Table\\Ban5.gif");
                        }
                        catch
                        { ;}
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        e.Appearance.Options.UseTextOptions = true;
                        e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        e.Graphics.DrawImage(img, e.Bounds);
                        e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                        e.Handled = true;
                    }
                    else
                    {
                        if (state == 3)
                        {
                            Image img = null;
                            try
                            {
                                img = Image.FromFile(Application.StartupPath + "\\Images\\Table\\Ban4.gif");
                            }
                            catch
                            { ;}
                            e.Appearance.ForeColor = Color.White;
                            e.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
                            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            e.Appearance.Options.UseTextOptions = true;
                            e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            e.Graphics.DrawImage(img, e.Bounds);
                            e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                            e.Handled = true;
                        }
                        else
                        {
                            e.Appearance.BackColor = Color.Silver;
                        }
                    }
                }
            }
        }
        
        private void Manager_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos=-1;
            try
            {              
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select field_name,  column_visible from sysGridColumns where form_name='" + this.Name + "'");
                if (dt.Rows.Count > 0)
                {
                    strColName = dt.Rows[0][0].ToString();
                    strCol = dt.Rows[0][1].ToString();
                    string[] arrayColName = strColName.Split('/');
                    string[] arrCol = strCol.Split('/');
                    if (e.Item.Name.Contains("_allow_col_") && (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos=findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]))
                        {                     
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible= clsFunction.ConvertArrayToString(arrCol);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='"+ strColVisible +"' where form_name='"+this.Name+"'");
                            Load_Grid();
                        }         
                    }
                }                
            }
            catch 
            {
                MessageBox.Show(strColVisible);
            }
        }

        private int findIndexInArray(string value, string[] arr)
        {
            int index = -1;
            for (int i = 0; i < arr.Length; i++)
                if (value == arr[i])
                {
                    index = i;
                    break;
                }
                return index;
        }
        
        private void gv_EXPORTDETAIL_C_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gv_PURCHASEDETAIL_C_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                // Kiểm tra trùng mã
                GridView view = sender as GridView;
                GridColumn inStockCol = view.Columns["idcommodity"];
                GridColumn intSl = view.Columns["quantity"];
                GridColumn intDG = view.Columns["price"];
                //Get the value of the first column
                String inSt = (String)view.GetRowCellValue(e.RowHandle, inStockCol);
                if (e.RowHandle < 0)
                {
                    if (checkExitMaHangInGrid(inSt, (DataTable)gct_list_C.DataSource))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");
                    }
                }
                else
                {
                    if (checkExitMaHangInGridSua(inSt, (DataTable)gct_list_C.DataSource))
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(inStockCol, "Mã hàng này đã tồn tại");

                    }
                }
                // Giới hạn số lương
                if (view.GetRowCellValue(e.RowHandle, intSl).ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, intSl))<=0 )
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Số lượng không được nhỏ hơn 0");

                }
                // giới hạn vat
                if (view.GetRowCellValue(e.RowHandle, "vat").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) < 0 || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "vat")) >100)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "VAT phải >=0 và <=100");

                }
                // giới hạn chiết khấu
                if (view.GetRowCellValue(e.RowHandle, "discount").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "discount")) < 0 || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "discount")) > 100)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Chiết khấu phải >=0 và <=100");

                }
                // giới hạn chi phí 0 âm
                if (view.GetRowCellValue(e.RowHandle, "costs").ToString() == "" || Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "costs")) < 0)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    view.SetColumnError(intSl, "Chi phí phải lớn hơn 0");

                }

                // kiểm tra đủ số lượng xuất
                if (checkInsert(gv_list_C.GetRowCellValue(e.RowHandle, "idcommodity").ToString(), Convert.ToDouble(gv_list_C.GetRowCellValue(e.RowHandle, "quantity")))==false)
                {
                    if (clsFunction.MessageDelete("Thông báo", "Số lượng tồn kho không đủ, bạn có muốn xuất âm mặt hàng này") == false)
                    {
                        e.Valid = false;
                        //Set errors with specific descriptions for the columns
                        view.SetColumnError(intSl, "Số lượng xuất không đủ");
                    }

                }
            }
            catch { }
        }

        private void gv_table_C_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {


                //chk_isdiscount_I6.Checked = checkIsdiscount();
                //chk_issurcharge_I6.Checked = checkIssurcharge();

                chageTable();
                mergeTable();
                unionTable();
                splitTable();
                setButton(e.CellValue.ToString());
                loadInfoInvoice(getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()));
                loadTotal();               
            }
            catch { }
        }

        private void gv_group_C_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if(e.CellValue !=null)
                loadListMenu(e.CellValue.ToString());
        }

        private void gv_list_C_DoubleClick(object sender, EventArgs e)
        {
            string idreceipt = "";
            idreceipt = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idreceipt").ToString();
            if (gv_list_C.RowCount > 0)
            {
                frmEditInvoice frm = new frmEditInvoice();
                frm.idtable = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idtable").ToString();
                frm.passData = new frmEditInvoice.PassData(getValueEditInvoice);
                frm.idinvoice = gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idexport").ToString();
                frm.ShowDialog();
            }
        }

        private void gv_listmenu_C_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    getValueAD(Convert.ToDouble(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle,"discount")));// amount discount
                    getValueD(Convert.ToInt16 (Convert.ToDouble(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "amountdiscount")) / Convert.ToDouble(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "price")) * 100)); // discount
                    getValueP(0);
                    getValueQ(1);
                    getValue(true);
                    updateExportDetail(gv_listmenu_C, true);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    gv_table_C.LayoutChanged();
                    txt_search_I4.Text = "";
                    txt_search_I4.Focus();
                    loadTotal();
                    return;
                }

                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    getValueAD(Convert.ToDouble(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "discount")));// amount discount
                    getValueD(0);
                    getValueP(0);
                    getValueQ(-1);
                    getValue(true);
                    updateExportDetail(gv_listmenu_C, true);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    gv_table_C.LayoutChanged();
                    txt_search_I4.Text = "";
                    loadTotal();
                    txt_search_I4.Focus();

                    return;
                }

                if (e.RowHandle >= 0 && gv_listmenu_C.SelectedRowsCount > 0 && gv_listmenu_C.FocusedRowHandle >= 0)
                {
                    frmCal frm = new frmCal();
                    frm.idexport = lbl_idpurchase_S.Text;
                    frm.idcommodity = "";
                    frm.chk_tang_I6.Checked = false;
                    frm.chk_tang_I6.Enabled = true;
                    frm.amoundiscount = Convert.ToDecimal (Convert.ToDouble(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "discount")));// amount discount;//Convert.ToDecimal(getAmountDiscountCurrent(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "idcommodity").ToString(), gv_menu_C));
                    frm.quantity = Convert.ToDecimal(getQuantityCurrent(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "idcommodity").ToString(),gv_menu_C));
                    frm.price = Convert.ToDecimal(gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "price"));
                    frm.commodity = gv_listmenu_C.GetRowCellValue(gv_listmenu_C.FocusedRowHandle, "commodity").ToString();
                    frm.table = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    frm.passDataad = new frmCal.PassDataAD(getValueAD);
                    frm.passDatad = new frmCal.PassDataD(getValueD);
                    frm.passDatap = new frmCal.PassDataP(getValueP);
                    frm.passDataq = new frmCal.PassDataQ(getValueQ);
                    frm.passData = new frmCal.PassData(getValue);
                    frm.passDataGive = new frmCal.PassDataGive(getValueGive);
                    frm.ShowDialog();
                    updateExportDetail(gv_listmenu_C,false);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    //loadTotal();
                    gv_table_C.LayoutChanged();
                    txt_search_I4.Text = "";
                    loadTotal();
                    txt_search_I4.Focus();
                }
   
            }
            catch { }
        }

        private void gv_menu_C_RowClick(object sender, RowClickEventArgs e)
        {
            idComCurrent = gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString();
            try
            {
                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    getValueAD(Convert.ToDouble(getAmountDiscountCurrent(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString(), gv_menu_C)));
                    getValueD(0);
                    getValueP(0);
                    getValueQ(1);
                    getValue(true);

                    updateExportDetail(gv_menu_C,true);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    loadTotal();
                    return;
                }

                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    getValueAD(Convert.ToDouble(getAmountDiscountCurrent(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString(), gv_menu_C)));
                    getValueD(0);
                    getValueP(0);
                    getValueQ(-1);
                    getValue(true);
                    updateExportDetail(gv_menu_C,true);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    loadTotal();
                    return;
                }

                if (e.RowHandle >= 0 && gv_menu_C.SelectedRowsCount > 0 && gv_menu_C.FocusedRowHandle >= 0)
                {
                    frmCal frm = new frmCal();
                    frm.idcommodity = "";
                    frm.idexport = lbl_idpurchase_S.Text;                    
                    frm.chk_tang_I6.Checked = false;
                    frm.chk_tang_I6.Enabled = true;
                    frm.amoundiscount = Convert.ToDecimal(getAmountDiscountCurrent(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString(), gv_menu_C));
                    frm.quantity = Convert.ToDecimal(getQuantityCurrent(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString(),gv_menu_C));
                    frm.price = Convert.ToDecimal(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "price"));
                    frm.commodity = gv_menu_C.GetRowCellDisplayText(gv_menu_C.FocusedRowHandle, "idcommodity").ToString();
                    frm.table = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    frm.passDataad = new frmCal.PassDataAD(getValueAD);
                    frm.passDatad = new frmCal.PassDataD(getValueD);
                    frm.passDatap = new frmCal.PassDataP(getValueP);
                    frm.passDataq = new frmCal.PassDataQ(getValueQ);
                    frm.passData = new frmCal.PassData(getValue);
                    frm.passDataGive = new frmCal.PassDataGive(getValueGive);
                    frm.ShowDialog();
                    updateExportDetail(gv_menu_C,false);
                    setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    loadTotal();
                    return;
                }
                gv_table_C.LayoutChanged();
            }
            catch { }
        }

        private void gv_give_C_RowClick(object sender, RowClickEventArgs e)
        {
            idComCurrent = gv_give_C.GetRowCellValue(gv_give_C.FocusedRowHandle, "idcommodity").ToString();
            try
            {
                give = true;
                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    getValueAD(0);
                    getValueD(0);
                    getValueP(0);
                    getValueQ(1);
                    getValue(true);
                    updateExportDetail(gv_give_C, true);
                    //setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    //loadTotal();
                    return;
                }

                if (ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    getValueAD(0);
                    getValueD(0);
                    getValueP(0);
                    getValueQ(-1);
                    getValue(true);
                    updateExportDetail(gv_give_C, true);
                    //setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    //loadTotal();
                    return;
                }

                if (e.RowHandle >= 0 && gv_give_C.SelectedRowsCount > 0 && gv_give_C.FocusedRowHandle >= 0)
                {
                    frmCal frm = new frmCal();
                    frm.chk_tang_I6.Checked = true;
                    frm.chk_tang_I6.Enabled = false;
                    frm.cal_amountdiscount_S.Enabled = false;
                    frm.cal_discount_S.Enabled = false;
                    frm.quantity = Convert.ToDecimal(getQuantityCurrent(gv_give_C.GetRowCellValue(gv_give_C.FocusedRowHandle, "idcommodity").ToString(), gv_give_C));
                    frm.price = Convert.ToDecimal(gv_give_C.GetRowCellValue(gv_give_C.FocusedRowHandle, "price"));
                    frm.commodity = gv_give_C.GetRowCellDisplayText(gv_give_C.FocusedRowHandle, "idcommodity").ToString();
                    frm.table = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    frm.passDataad = new frmCal.PassDataAD(getValueAD);
                    frm.passDatad = new frmCal.PassDataD(getValueD);
                    frm.passDatap = new frmCal.PassDataP(getValueP);
                    frm.passDataq = new frmCal.PassDataQ(getValueQ);
                    frm.passData = new frmCal.PassData(getValue);
                    frm.ShowDialog();
                    updateExportDetail(gv_give_C, false);
                    //setStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                    //loadTotal();
                    return;
                }
                gv_table_C.LayoutChanged();
            }
            catch { }
        }
        //getAmountDiscountCurrent
        private double getAmountDiscountCurrent(string idcommodity, GridView gv)
        {
            double quantity = 0;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                if (idcommodity == gv.GetRowCellValue(i, "idcommodity").ToString())
                {
                    quantity = Convert.ToDouble(gv.GetRowCellValue(i, "amountdiscount")) ;
                    break;
                }
            }

            return quantity;
        }

        private double getQuantityCurrent(string idcommodity, GridView gv)
        {
            double quantity = 1;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                if (idcommodity == gv.GetRowCellValue(i, "idcommodity").ToString())
                {
                    quantity = Convert.ToDouble(gv.GetRowCellValue(i, "quantity"));
                    break;
                }
            }
     
            return quantity;
        }
        
        private void getValueAD(double value)
        {
            if (value >= 0)
            {
                calamountdiscount = value;
            }
            
        }

        private void getValueD(double value)
        {
            if (value >= 0)
            {
                caldiscount = value;
            }
  
        }

        private void getValueP(double value)
        {
            if (value >= 0)
            {
                calprice = value;
            }
      
        }

        private void getValueQ(double value)
        {
            //if (value != 0)
            {
                calquantity = value;
            }
   
        }

        private void getValue(bool _value)
        {
            if (_value != false)
            {
                bvalue = _value;       
               
            }
            else
            {
                bvalue = false;
            }

        }

        private void getValueGive(bool _value)
        {
            if (_value != false)
            {
                give = true;
            }
            else
            {
                give = false;
            }  

        }

        private void gv_group_C_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = (GridView)sender;
            int state = -1;
            state = checkStatusTable(e.CellValue.ToString());
            if (state == 0)
            {
                Image img = null;
                try
                {
                    img = Image.FromFile(Application.StartupPath + "\\Images\\Table\\wood.png");
                }
                catch
                { ;}
                e.Appearance.ForeColor = Color.White;
                e.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Graphics.DrawImage(img, e.Bounds);
                e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                e.Handled = true;
            }
        }

        #endregion

        #region Methods
        
        private void insertStraceRetail( string _object, string action, string des,string status)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from traceretail where idtrace='1'");
                DataRow dr = dt.NewRow();
                dr["idtrace"] = clsFunction.layMa("TA", "idtrace", "traceretail");
                dr["dateaction"] = DateTime.Now;
                dr["object"] = _object;
                dr["action"] = action;
                dr["descriprion"] = des;
                dr["userid"] = clsFunction._iduser;
                dr["status"] = status;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
                gct_trace_C.DataSource = APCoreProcess.APCoreProcess.Read("select * from traceretail where CAST(datediff(d,0,dateaction) as datetime) = CAST(datediff(d,0,getdate()) as datetime) ");
            }
            catch { }
        }

        private void print(string idexport)
        {
            try
            {
                DataTable dtRP = new DataTable();
                dtRP = APCoreProcess.APCoreProcess.Read("select reportname, path, query from sysReportDesigns where formname='frm_checkbill' and iscurrent=1");
                if (dtRP.Rows.Count > 0)
                {
                    XtraReport report = XtraReport.FromFile(Application.StartupPath + "\\Report" + "\\" + dtRP.Rows[0]["reportname"].ToString() + ".repx", true);
                    //report.FindControl("xxx", true).Text="alo";
                    Function.clsFunction.BindDataControlReport(report);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    if (APCoreProcess.APCoreProcess.IssqlCe == false)
                    {
                        dt = APCoreProcess.APCoreProcess.Read("with temp as ( SELECT     mt.dateimport, ISNULL(mt.vat, 0) AS vat, mt.idexport, ISNULL(pt.surcharge, 0) AS amountsurcharge, mt.invoice, pt.datereceipt, ISNULL(pt.amount, 0) AS amount, pt.note, ISNULL(pt.surplus, 0) AS surplus, ISNULL(pt.discount, 0) AS amountdiscount, tb.tableNO, mh.idcommodity,  mh.commodity  as commodity, kh.customer, dt.idunit, ISNULL(dt.quantity, 0) AS quantity, ISNULL(dt.price, 0) AS price, ISNULL(dt.amountvat, 0) AS amountvat,  dt.davat, dt.costs, ISNULL(dt.total, 0) AS total,  convert(varchar(3), ISNULL(dt.quantity, 0)) + ' '+ lower( left( U.Sign,2)) AS  strquantity,  pt.proceeds, convert(int, pt.surcharge/pt.amount *100) as surcharge, convert(int, pt.discount/pt.amount*100) as discount, pt.amount + pt.surcharge - pt.discount AS amount1, case when dt.give=0 then N'Thực đơn' else N'Tặng kèm' end as give, dt.give as give1 FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN  DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN DMUNIT U ON U.idunit=dt.idunit WHERE     (mt.idexport = N'" + idexport + "')  and ISNULL(dt.quantity, 0)>0   union SELECT     mt.dateimport, ISNULL(mt.vat, 0) AS vat, mt.idexport, ISNULL(pt.surcharge, 0) AS amountsurcharge, mt.invoice, pt.datereceipt, ISNULL(pt.amount, 0) AS amount, pt.note, ISNULL(pt.surplus, 0) AS surplus, ISNULL(pt.discount, 0) AS amountdiscount, tb.tableNO, mh.idcommodity ,  N'-> Giảm ~ ' + convert(nvarchar(3),(select convert(nvarchar(10), round((dt1.amountdiscount/dt1.price*100),0)) from exportdetail dt1 where dt1.give=0 and dt1.idcommodity=dt.idcommodity and dt1.idexport=dt.idexport)) + ' %'   as commodity, kh.customer, dt.idunit, ISNULL(dt.quantity, 0) AS quantity,  ISNULL(- dt.amountdiscount, 0) AS price,   ISNULL(dt.amountvat, 0) AS amountvat, dt.davat, dt.costs, ISNULL(dt.total, 0) AS total,  convert(varchar(3), ISNULL(dt.quantity, 0)) + ' '+ lower( left( U.Sign,1)) AS  strquantity,  pt.proceeds, convert(int, pt.surcharge/pt.amount *100) as surcharge, convert(int, pt.discount/pt.amount*100) as discount, pt.amount + pt.surcharge - pt.discount AS amount1, case when dt.give=0 then N'Thực đơn' else N'Tặng kèm' end as give, dt.give as give1 FROM         EXPORTDETAIL AS dt INNER JOIN  EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN  DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN  DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN DMUNIT U ON U.idunit=dt.idunit WHERE     (mt.idexport = N'" + idexport + "')  and ISNULL(dt.quantity, 0)>0 and dt.give=0 and dt.amountdiscount>0 ) select temp.* , case when give1=1 then 0 else price end as price2 from temp order by give desc ");
                    }
                    else
                    {
                        dt = APCoreProcess.APCoreProcess.Read("SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL  THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,   CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, mh.commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END AS quantity,  CASE WHEN dt.price IS NULL THEN 0 ELSE dt.price END AS price, CASE WHEN dt.amountvat IS NULL THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat,  dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3), CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END)  + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100) AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount,  pt.amount + pt.surcharge - pt.discount AS amount1, CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1,   CASE WHEN dt.give = 1 THEN 0 ELSE dt.price END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN   DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN    DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN   DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) UNION SELECT  dt._index,   mt.dateimport, CASE WHEN mt.vat IS NULL THEN 0 ELSE mt.vat END AS vat, mt.idexport, CASE WHEN pt.surcharge IS NULL THEN 0 ELSE pt.surcharge END AS amountsurcharge, mt.invoice, pt.datereceipt, CASE WHEN pt.amount IS NULL THEN 0 ELSE pt.amount END AS amount, pt.note,  CASE WHEN pt.surplus IS NULL THEN 0 ELSE pt.surplus END AS surplus, CASE WHEN pt.discount IS NULL THEN 0 ELSE pt.discount END AS amountdiscount,   tb.tableNO, mh.idcommodity, N'-> Giảm giá ' + CONVERT(nvarchar(3), dt.discount) + ' %' AS commodity, kh.customer, dt.idunit, CASE WHEN dt.quantity IS NULL  THEN 0 ELSE dt.quantity END AS quantity, CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END AS price, CASE WHEN dt.amountvat IS NULL    THEN 0 ELSE dt.amountvat END AS amountvat, dt.davat, dt.costs, CASE WHEN dt.total IS NULL THEN 0 ELSE dt.total END AS total, CONVERT(nvarchar(3),   CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END) + ' ' + LOWER(U.sign) AS strquantity, pt.proceeds, CONVERT(int, pt.surcharge / pt.amount * 100)    AS surcharge, CONVERT(int, pt.discount / pt.amount * 100) AS discount, pt.amount + pt.surcharge - pt.discount AS amount1,     CASE WHEN dt.give = 0 THEN N'Thực đơn' ELSE N'Tặng kèm' END AS give, dt.give AS give1, CASE WHEN dt.give = 1 THEN 0 ELSE CASE WHEN dt.amountdiscount IS NULL THEN 0 ELSE - dt.amountdiscount END END AS price2 FROM         EXPORTDETAIL AS dt INNER JOIN     EXPORT AS mt ON dt.idexport = mt.idexport INNER JOIN    RECEIPT AS pt ON mt.idexport = pt.idexport INNER JOIN   DMCOMMODITY AS mh ON dt.idcommodity = mh.idcommodity INNER JOIN      DMTABLE AS tb ON mt.idtable = tb.idtable INNER JOIN      DMCUSTOMERS AS kh ON mt.idcustomer = kh.idcustomer INNER JOIN    DMUNIT AS U ON U.idunit = dt.idunit WHERE     (mt.idexport = N'" + idexport + "') AND (CASE WHEN dt.quantity IS NULL THEN 0 ELSE dt.quantity END > 0) AND (dt.give = 0) AND (dt.amountdiscount > 0)");
                    }
                    //dt.DefaultView.Sort = "give1 desc,_index , price desc";
                    dt.DefaultView.Sort = " give1 desc,idcommodity , price desc";
                    DataTable dtO = new DataTable();
                    dtO = dt.DefaultView.ToTable();
                    ds.Tables.Add(dtO);
                    report.DataSource = ds;
                    report.DataMember = "Data";// phair co datamember moi len dc group
                    report.DataAdapter = ds;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }
                    //tool.ShowPreviewDialog();

                    //tool.Print();
                    tool.Print(APCoreProcess.APCoreProcess.Read("select * from sysPrintConfig where status=1").Rows[0]["printname"].ToString());
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo",ex.Message);
            }
        }

        private void loadGridtable()
        {
            DataTable dtF = new DataTable();
            dtF = APCoreProcess.APCoreProcess.Read("select distinct idfloors from dmtable order by idfloors");
            DataTable dtTable = new DataTable();
            DataTable dtPro = new DataTable();
            dtPro.Columns.Add("idtable1", Type.GetType("System.String"));
            dtPro.Columns.Add("idtable2", Type.GetType("System.String"));
            dtPro.Columns.Add("idtable3", Type.GetType("System.String"));
            dtPro.Columns.Add("idtable4", Type.GetType("System.String"));
            dtPro.Columns.Add("idtable5", Type.GetType("System.String"));
            dtPro.Columns.Add("idfloors", Type.GetType("System.String"));
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable as idtable1 , tableNO from dmtable");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[1].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable as idtable2 , tableNO from dmtable");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[2].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable as idtable3 , tableNO from dmtable");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[3].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable as idtable4 , tableNO from dmtable");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[4].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable as idtable5 , tableNO from dmtable"); 
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_table_C.Columns[5].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idfloors , floors from dmfloors");
            

            if (dtF.Rows.Count > 0)
            {
                
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    int k = 0;
                    dtTable = APCoreProcess.APCoreProcess.Read("select idtable from dmtable where idfloors='" +    dtF.Rows[i]["idfloors"].ToString() + "'");
                    if (dtTable.Rows.Count > 0)
                    {
                        while (k < (dtTable.Rows.Count-1)/5+1)
                        {
                            DataRow dr;
                            dr = dtPro.NewRow();
                            for (int j = 1 + k * 5; j <= 5 + k * 5; j++)
                            {
                                if (dtTable.Rows.Count > j - 1)
                                {
                                    dr["idtable" + (j-(5*k)).ToString()] = dtTable.Rows[(j) - 1][0].ToString();
                                }
                                else
                                {
                                    dr["idtable" + (j-5*k).ToString()] = null;
                                }
                            }
                            dr["idfloors"] = dtF.Rows[i]["idfloors"].ToString();
                            dtPro.Rows.Add(dr);
                            k++;
                        }
                    }

                }
            }
            gct_table_C.DataSource = dtPro;
            gv_table_C.ExpandAllGroups();
        }

        private void loadGridGroup()
        {
            DataTable dtF = new DataTable();
            dtF = APCoreProcess.APCoreProcess.Read("select distinct idsector from dmgroup order by idsector");
            DataTable dtTable = new DataTable();
            DataTable dtPro = new DataTable();
            dtPro.Columns.Add("idgroup1", Type.GetType("System.String"));
            dtPro.Columns.Add("idgroup2", Type.GetType("System.String"));
            dtPro.Columns.Add("idgroup3", Type.GetType("System.String"));
            dtPro.Columns.Add("idgroup4", Type.GetType("System.String"));
            dtPro.Columns.Add("idgroup5", Type.GetType("System.String"));
            dtPro.Columns.Add("idsector", Type.GetType("System.String"));
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup as idgroup1 , groupname from dmgroup");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[1].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup as idgroup2 , groupname from dmgroup");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[2].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup as idgroup3 , groupname from dmgroup");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[3].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup as idgroup4 , groupname from dmgroup");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[4].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idgroup as idgroup5 , groupname from dmgroup");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_group_C.Columns[5].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idsector , sector from dmsectors");
            if (dtF.Rows.Count > 0)
            {

                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    int k = 0;
                    dtTable = APCoreProcess.APCoreProcess.Read("select idgroup from dmgroup where idsector='" + dtF.Rows[i]["idsector"].ToString() + "'");
                    if (dtTable.Rows.Count > 0)
                    {
                        while (k < (dtTable.Rows.Count - 1) / 5 + 1)
                        {
                            DataRow dr;
                            dr = dtPro.NewRow();
                            for (int j = 1 + k * 5; j <= 5 + k * 5; j++)
                            {
                                if (dtTable.Rows.Count > j - 1)
                                {
                                    dr["idgroup" + (j - (5 * k)).ToString()] = dtTable.Rows[(j) - 1][0].ToString();
                                }
                                else
                                {
                                    dr["idgroup" + (j - 5 * k).ToString()] = null;
                                }
                            }
                            dr["idsector"] = dtF.Rows[i]["idsector"].ToString();
                            dtPro.Rows.Add(dr);
                            k++;
                        }
                    }

                }
            }
            gct_group_C.DataSource = dtPro;
            gv_group_C.ExpandAllGroups();
        }

        private void loadReport()
        {
            try
            {
                ReportControls.Presentation.frmConfigRePort frm = new ReportControls.Presentation.frmConfigRePort();
                frm.formname = this.Name;
                frm.ShowDialog();
            }
            catch { }
        }

        private void MyDataSourceDemandedHandler(object sender, EventArgs e) {
              DataSet ds = new DataSet();
              DataTable dt = new DataTable();
              dt = APCoreProcess.APCoreProcess.Read("dmtable");
              ds.Tables.Add(dt);
            //Instantiating your DataSet instance here
            //.....
            //Pass the created DataSet to your report:
            ((XtraReport)sender).DataSource = ds;
            ((XtraReport)sender).DataMember = ds.Tables[0].TableName;     
        }

        private void Init()
        {
            try
            {
                loadGridLookupCustomer();
                loadGridLookupEmployee();
                split_main_C.CollapsePanel = SplitCollapsePanel.Panel1;
                split_main_C.Collapsed = true;
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_give_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_menu_C.Columns[0].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity as idcommodity , commodity from dmcommodity");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_trace_C.Columns[5].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select userid  , username from sysUser");
                gct_trace_C.DataSource = APCoreProcess.APCoreProcess.Read("select * from traceretail where CAST(datediff(d,0,dateaction) as datetime) = CAST(datediff(d,0,getdate()) as datetime) ");
           
            }
            catch 
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi");
            }
        }

        private bool checkInput()
        {
            bool flag = true;

            return flag;
        }

        private bool checkInsert(string idcommodity, double quantity)
        {
            bool flag = false;
            DataTable dtI = new DataTable();
            dtI = APCoreProcess.APCoreProcess.Read("SELECT     dt.quantity - SUM(ISNULL(stk.quantity, 0)) AS quantity, dt.iddetail FROM         PURCHASEDETAIL AS dt INNER JOIN   PURCHASE AS mt ON dt.idpurchase = mt.idpurchase LEFT OUTER JOIN   STOCKIO AS stk ON dt.iddetail = stk.iddetailpur GROUP BY dt.idcommodity, dt.status, dt.iddetail, dt.quantity HAVING      (dt.idcommodity = N'"+ idcommodity +"') AND (dt.status = 0) AND (dt.quantity - SUM(ISNULL(stk.quantity, 0)) > "+ quantity +") ");
            if (dtI.Rows.Count > 0)// tồn kho đủ
            {
                flag = true;
            }
            return flag;
        }

        private void clockButton(bool clock)
        {
  
        }

        private bool checkExitMaHangInGrid(string mahang, DataTable dt)
        {
            int a = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                    break;
                }
            }
            if (a > 0)
                return true;
            return false;
        }

        private bool checkExitMaHangInGridSua(string mahang, DataTable dt)
        {
            int a = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idcommodity"].ToString() == mahang)
                {
                    a++;
                }
            }
            if (a > 1)
                return true;
            return false;
        }
                
        private int checkStatusTable(string idtable)
        {
            int state = -1;// 0: Bàn trống; 1 chờ, 2 bàn đặt , 3 thanhtoan, -1 Chua co ban
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select case when status='0' then 0 when status='1' then 1  when status ='2'  then 2 when status='3' then 3 when status='-1' then '-1' else 0  end as _state from EXPORT where idtable='" + idtable + "' and cancel=0 and complet=0");
            if (dt.Rows.Count > 0)
            {
                state = Convert.ToInt16(dt.Rows[0][0]);
            }
            else
            {
                if (idtable != "")
                    state = 0;
                else
                    state = -1;
            }
            return state;
        }

        private void setButton(string idtable)
        {
            try
            {
                int state = -1;
                state = checkStatusTable(idtable);
                if (state == -1)
                {
                    //Bàn chưa có
                    btn_callmenu_S.Text = clsFunction.transLateText("");
                    btn_enter_S.Text = clsFunction.transLateText("");
                    btn_info_S.Text = clsFunction.transLateText("");
                    btn_input_S.Text = clsFunction.transLateText("");
                    btn_change_S.Text = clsFunction.transLateText("");
                    btn_union_S.Text = clsFunction.transLateText("");
                    btn_div_S.Text = clsFunction.transLateText("");           
                    lbl_tableno_S.Text = clsFunction.transLateText("Chưa có bàn");                   
                }
                if (state == 0)
                {
                    //Bàn trống
                    btn_callmenu_S.Text = clsFunction.transLateText("F2 Mở bàn");
                    btn_enter_S.Text = clsFunction.transLateText("F1 Gọi món");
                    btn_info_S.Text = clsFunction.transLateText("");
                    btn_input_S.Text = clsFunction.transLateText("");
                    btn_change_S.Text = clsFunction.transLateText("F5 Đặt chỗ");
                    btn_union_S.Text = clsFunction.transLateText("");
                    btn_div_S.Text = clsFunction.transLateText("");
        
                    //
                    DataTable dtTable=new DataTable();
                    dtTable=APCoreProcess.APCoreProcess.Read("select idtable, tableNo, floors from dmtable inner join dmfloors on dmfloors.idfloors=dmtable.idfloors where idtable='"+ idtable +"'");
                    if (dtTable.Rows.Count > 0)
                    {
                        lbl_tableno_S.Text = dtTable.Rows[0]["tableno"].ToString() + " -  " + dtTable.Rows[0]["floors"].ToString();
                    }
                    else
                    {
                        lbl_tableno_S.Text = clsFunction.transLateText("Chưa có bàn");
                    }
                    // bàn chưa mở thì ẩn danh sách món
                    split_main_C.CollapsePanel = SplitCollapsePanel.Panel1;
                    split_main_C.Collapsed = true;
                }
                else
                {
                    if (state == 3)
                    {
                        //bàn đang có khách
                        btn_callmenu_S.Text = clsFunction.transLateText("F2 Thanh toán");
                        btn_enter_S.Text = clsFunction.transLateText("F1 Gọi món");
                        btn_info_S.Text = clsFunction.transLateText("F3 Thông tin");
                        btn_input_S.Text = clsFunction.transLateText("F6 Nhập");
                        btn_change_S.Text = clsFunction.transLateText("F5 Chuyển");
                        btn_union_S.Text = clsFunction.transLateText("F7 Ghép");
                        btn_div_S.Text = clsFunction.transLateText("F8 Tách");
                        DataTable dtTable = new DataTable();
                        dtTable = APCoreProcess.APCoreProcess.Read("select idtable, tableNo, floors from dmtable inner join dmfloors on dmfloors.idfloors=dmtable.idfloors where idtable='" + idtable + "'");
                        if (dtTable.Rows.Count > 0)
                        {
                            lbl_tableno_S.Text = dtTable.Rows[0]["tableno"].ToString() + " -  " + dtTable.Rows[0]["floors"].ToString();
                        }
                        else
                        {
                            lbl_tableno_S.Text = clsFunction.transLateText("Chưa có bàn");
                        }
                    }
                    else
                    {
                        if (state == 2)
                        {
                            // Mở bàn nhưng chưa gọi expect
                            // Đặt chỗ 
                            btn_callmenu_S.Text = clsFunction.transLateText("F2 Mở bàn");
                            btn_enter_S.Text = clsFunction.transLateText("F1 Gọi món");
                            btn_info_S.Text = clsFunction.transLateText("F3 Hủy đặt");
                            btn_input_S.Text = clsFunction.transLateText("");
                            btn_change_S.Text = clsFunction.transLateText("");
                            btn_union_S.Text = clsFunction.transLateText("");
                            btn_div_S.Text = clsFunction.transLateText("");
                            DataTable dtTable = new DataTable();
                            dtTable = APCoreProcess.APCoreProcess.Read("select idtable, tableNo, floors from dmtable inner join dmfloors on dmfloors.idfloors=dmtable.idfloors where idtable='" + idtable + "'");
                            if (dtTable.Rows.Count > 0)
                            {
                                lbl_tableno_S.Text = dtTable.Rows[0]["tableno"].ToString() + " -  " + dtTable.Rows[0]["floors"].ToString();
                            }
                            else
                            {
                                lbl_tableno_S.Text = clsFunction.transLateText("Chưa có bàn");
                            }
                        }
                        else
                        {
                            if (state == 1)
                            {
                                // Chờ
                                btn_callmenu_S.Text = clsFunction.transLateText("F2 Hủy bàn");
                                btn_enter_S.Text = clsFunction.transLateText("F1 Gọi món");
                                btn_info_S.Text = clsFunction.transLateText("");
                                btn_input_S.Text = clsFunction.transLateText("");
                                btn_change_S.Text = clsFunction.transLateText("");
                                btn_union_S.Text = clsFunction.transLateText("");
                                btn_div_S.Text = clsFunction.transLateText("");
          
                                DataTable dtTable = new DataTable();
                                dtTable = APCoreProcess.APCoreProcess.Read("select idtable, tableNo, floors from dmtable inner join dmfloors on dmfloors.idfloors=dmtable.idfloors where idtable='" + idtable + "'");
                                if (dtTable.Rows.Count > 0)
                                {
                                    lbl_tableno_S.Text = dtTable.Rows[0]["tableno"].ToString() + " -  " + dtTable.Rows[0]["floors"].ToString();
                                }
                                else
                                {
                                    lbl_tableno_S.Text = clsFunction.transLateText("Chưa có bàn");
                                }
                            }
                        }
                    }
                }
                if (split_main_C.CollapsePanel.ToString() != "Panel1")
                {
                    btn_info_S.Text = clsFunction.transLateText("F3 << Menu");
                }
                else
                {
                    btn_info_S.Text = clsFunction.transLateText("F3 Thông tin");
                }
            }
            catch
            { }
        }

        private void setStatusInvoice()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select count(*) from exportdetail where idexport='"+ lbl_idpurchase_S.Text +"'");
            if (dt.Rows.Count > 0)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='3' where idexport='"+ lbl_idpurchase_S.Text +"'");
            }
            else
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='1' where idexport='" + lbl_idpurchase_S.Text + "'");
            }
            setButton(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
        }

        private void setClickButton(string btnText)
        {
            try
            {
                lbl_time1_S.Text = "00:00:00";
                
                if (glue_idcustomer_I1.EditValue == null)
                {
                    glue_idcustomer_I1.EditValue = glue_idcustomer_I1.Properties.GetKeyValue(0);
                }
                if (glue_IDEMP_I1.EditValue == null)
                {
                    glue_IDEMP_I1.EditValue = glue_IDEMP_I1.Properties.GetKeyValue(0);
                }
                DataTable dtHD = new DataTable();
                dtHD = APCoreProcess.APCoreProcess.Read("select * from export where idexport='" + getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName).ToString()) + "'");
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F1 Gọi món"))
                {
                    // gọi món
                    if (dtHD.Rows.Count == 0)
                    {
                        DataRow dr = dtHD.NewRow();
                        dr["dateimport"] = DateTime.Now.ToString();
                        dr["idcustomer"] = glue_idcustomer_I1.EditValue.ToString();
                        dr["IDEMP"] = glue_IDEMP_I1.EditValue.ToString();
                        dr["idexport"] = clsFunction.layMa("PX", "idexport", "export");
                        dr["idtable"] = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString());
                        dr["complet"] = "False";
                        dr["cancel"] = "false";
                        dr["retail"] = "True";
                        dr["status"] = "1";
                        dr["isdelete"] = "False";
                        dr["invoice"] = clsFunction.layMa("HD", "invoice", "export");
                        dtHD.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);                       
                    }
                
                 // mở danh sách món
                    split_main_C.CollapsePanel = SplitCollapsePanel.Panel2;
                    split_main_C.Collapsed = true;
                    txt_search_I4.Focus();
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F2 Thanh toán"))
                {
                    try
                    {
                        frm_checkbill frm = new frm_checkbill();
                        frm.amount = Convert.ToDouble(cal_costs_I1.Value);
                        frm.cal_sotien_S.Value = cal_amount_I4.Value;
                        frm.lbl_banso_S.Text =clsFunction.transLateText("Bàn số : ") + lbl_tableno_S.Text;
                        frm.idtable=gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                        frm.surcharge = Convert.ToDouble( cal_amountsurcharge_I4.Value);
                        frm.discount = Convert.ToDouble( cal_amountdiscount_I4.Value);
                        frm.IDEMP = glue_IDEMP_I1.EditValue.ToString();
                        frm.passData = new frm_checkbill.PassData(getValueInvoice);
                        frm.idexport = getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                        frm.lbl_table_S.Text = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                        frm.ShowDialog();
                    }
                    catch 
                    {
                    }                    
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F3 << Menu"))
                {
                    split_main_C.CollapsePanel = SplitCollapsePanel.Panel1;
                    split_main_C.Collapsed = true;
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F2 Hủy bàn"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set cancel=1, complet=0 where idexport='" + getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()) + "'");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete export where cancel=1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F3 Thông tin"))
                {

                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F2 Hủy đặt"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set cancel=1, complet=0 where idexport='" + getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()) + "'");
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete export where cancel=1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F5 Chuyển"))
                {
                    lbl_change_S.Text = clsFunction.transLateText("Hãy chọn bàn cần chuyển - X để hủy thao tác");
                    changeTable = true;
                    idTableChoose = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    TableChoose = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("F5 Chuyển"),"","1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F6 Nhập"))
                {
                    lbl_change_S.Text = clsFunction.transLateText("Hãy chọn bàn cần nhập - X để hủy thao tác");
                    MergeTable = true;
                    idTableChoose = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    TableChoose = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("F6 Nhập"), "", "1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F7 Ghép"))
                {
                    lbl_change_S.Text = clsFunction.transLateText("Hãy chọn bàn cần ghép - X để hủy thao tác");
                    UnionTable = true;
                    idTableChoose = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    TableChoose = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("F7 Ghép"), "", "1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F8 Tách"))
                {
                    lbl_change_S.Text = clsFunction.transLateText("Hãy chọn bàn cần tách - X để hủy thao tác");
                    SplitTable = true;
                    idTableChoose = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    TableChoose = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    insertStraceRetail(gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()), clsFunction.transLateText("F8 Tách"), "", "1");
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F5 Đặt chỗ"))
                {
                    DataRow dr = dtHD.NewRow();
                    dr["dateimport"] = DateTime.Now.ToString();
                    dr["idcustomer"] = glue_idcustomer_I1.EditValue.ToString();
                    dr["IDEMP"] = glue_IDEMP_I1.EditValue.ToString();
                    dr["idexport"] = clsFunction.layMa("PX", "idexport", "export");
                    dr["idtable"] = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString());
                    dr["complet"] = "False";
                    dr["cancel"] = "false";
                    dr["retail"] = "True";
                    dr["status"] = "2";
                    dr["isdelete"] = "False";
                    dr["invoice"] = clsFunction.layMa("HD", "invoice", "export");
                    dtHD.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
                if (clsFunction.transLateText(btnText) == clsFunction.transLateText("F2 Mở bàn"))
                {
                    if (dtHD.Rows.Count == 0)
                    {
                        DataRow dr = dtHD.NewRow();
                        dr["dateimport"] = DateTime.Now.ToString();
                        dr["idcustomer"] = glue_idcustomer_I1.EditValue.ToString();
                        dr["IDEMP"] = glue_IDEMP_I1.EditValue.ToString();
                        dr["idexport"] = clsFunction.layMa("PX", "idexport", "export");
                        dr["idtable"] = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString());
                        dr["complet"] = "False";
                        dr["cancel"] = "false";
                        dr["retail"] = "True";
                        dr["status"] = "1";
                        dr["isdelete"] = "False";
                        dr["invoice"] = clsFunction.layMa("HD", "invoice", "export");
                        dtHD.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    else// đã đặt chỗ
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='1' where idexport='" + lbl_idpurchase_S.Text + "'");
                    }
                }
                gv_table_C.LayoutChanged();
                setButton(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                loadInfoInvoice(getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()));
                if (split_main_C.CollapsePanel.ToString() != "Panel1")
                {
                    btn_info_S.Text = clsFunction.transLateText("F3 << Menu");
                }
                else
                {
                    btn_info_S.Text = clsFunction.transLateText("F3 Thông tin");
                }
            }
            catch { }
        }

        private string getInvoiceByIDTable( string idtable )
        {
            string invoice = "";
            // lấy số hóa đơn đang tồn tại trong phiên làm việc
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select top 1 idexport  from export where idtable='" + idtable + "' and complet=0 order by dateimport desc");
            if (dt.Rows.Count > 0)
            {
                invoice = dt.Rows[0][0].ToString();
            }
            return invoice;
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "False", "False", "False" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "select idcustomer, customer, tel, fax, address from dmcustomers where status=1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }
        
        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
            }
            catch { }
        }

        private void loadInfoInvoice(string idexport)
        {
            try
            {
                DataTable dtM = new DataTable();
                dtM = APCoreProcess.APCoreProcess.Read("select * from export where idexport='" + idexport + "' and cancel  =0 and complet = 0 ");
                if (dtM.Rows.Count > 0)
                {
                    lbl_start_S.Text = Convert.ToDateTime(dtM.Rows[0]["dateimport"]).ToString("HH:mm:ss");
                    lbl_ngay_S.Text = Convert.ToDateTime(dtM.Rows[0]["dateimport"]).ToString("dd/MM/yyyy");
                    dateExport = Convert.ToDateTime(dtM.Rows[0]["dateimport"]);
                    lbl_status_S.Text = "";
                    lbl_idpurchase_S.Text = dtM.Rows[0]["idexport"].ToString();
                    glue_idcustomer_I1.EditValue = dtM.Rows[0]["idcustomer"].ToString();
                    glue_IDEMP_I1.EditValue = dtM.Rows[0]["IDEMP"].ToString();
                    chk_isdiscount_I6.Checked = dtM.Rows[0]["isdiscount"].ToString() == "" ? false : Convert.ToBoolean(dtM.Rows[0]["isdiscount"]);
                    chk_issurcharge_I6.Checked = dtM.Rows[0]["issurcharge"].ToString() == "" ? false : Convert.ToBoolean(dtM.Rows[0]["issurcharge"]);
                    cal_amountdiscount_I4.Value = dtM.Rows[0]["amountdiscount"].ToString() == "" ? 0 : Convert.ToDecimal(dtM.Rows[0]["amountdiscount"]);
                    cal_amountsurcharge_I4.Value = dtM.Rows[0]["amountsurcharge"].ToString() == "" ? 0 : Convert.ToDecimal(dtM.Rows[0]["amountsurcharge"]);
            
                    // load detail
                    DataTable dtD = new DataTable();
                    dtD = APCoreProcess.APCoreProcess.Read("select dt.*, u.sign from exportdetail dt, dmunit u where u.idunit=dt.idunit and dt.idexport='" + idexport + "' and dt.give <> 1");
                    if ( dtD.Columns.Contains("total")==false)
                    {
                        dtD.Columns.Add("total", typeof(System.Double));                        
                    }
                    if (dtD.Columns.Contains("discount1") == false)
                    {
                        dtD.Columns.Add("discount1", typeof(System.Double));
                    }
                    dtD.Columns["total"].Expression = "quantity*price -quantity*amountdiscount";
                    dtD.Columns["discount1"].Expression = "quantity*amountdiscount";
                    if (dtD.Rows.Count > 0)
                    {
                        gct_menu_C.DataSource = dtD;
                        int index=0;
                        if (idComCurrent == "")
                        {
                            index = 0;
                        }
                        else
                        {
                            DataRow dr;
                            dr = gv_menu_C.GetDataRow(gv_menu_C.FocusedRowHandle);
                            index = findRowIndexIndata(dtD, idComCurrent);
                        }
                        gv_menu_C.FocusedRowHandle = gv_menu_C.GetRowHandle(index);
                        lbl_status_S.Text = clsFunction.transLateText("Đã gọi") +" ("+dtD.Rows.Count.ToString()+") "+ clsFunction.transLateText(" món");
                        loadTotal();
                        if (cal_amountdiscount_I4.Value > 0)
                        {
                            cal_discount_I4.Value = Convert.ToDecimal(Convert.ToInt16(cal_amountdiscount_I4.Value / cal_costs_I1.Value * 100));
                        }
                        if (cal_amountsurcharge_I4.Value > 0)
                        {
                            cal_surcharge_I4.Value = Convert.ToDecimal(Convert.ToInt16(cal_amountsurcharge_I4.Value / cal_costs_I1.Value * 100));
                        }
                    }
                    else
                    {
                        gct_menu_C.DataSource = null;                    
                    }

                    // load detail give
                    DataTable dtG = new DataTable();
                    dtG = APCoreProcess.APCoreProcess.Read("select dt.*, u.sign from exportdetail dt, dmunit u where u.idunit=dt.idunit and dt.idexport='" + idexport + "' and give = 1");
                    if (dtG.Columns.Contains("total") == false)
                    {
                        dtG.Columns.Add("total", typeof(System.Double));
                    }
                    dtG.Columns["total"].Expression = "quantity*price";
                    if (dtG.Rows.Count > 0)
                    {
         
                        gct_give_C.DataSource = dtG;
                        int index = 0;
                        if (idComCurrent == "")
                        {
                            index = 0;
                        }
                        else
                        {
                            DataRow dr;
                            dr = gv_give_C.GetDataRow(gv_menu_C.FocusedRowHandle);
                            index = findRowIndexIndata(dtG, idComCurrent);
                        }
                        gv_give_C.FocusedRowHandle = gv_give_C.GetRowHandle(index);
                        //lbl_status_S.Text = clsFunction.transLateText("Đã gọi") + " (" + dtD.Rows.Count.ToString() + ") " + clsFunction.transLateText(" món");
                    }
                    else
                    {
                  
                        gct_give_C.DataSource = null;
                    }
                }
                else
                {

                    lbl_idpurchase_S.Text = "";
                    lbl_start_S.Text = "00:00:00";
                    lbl_status_S.Text = clsFunction.transLateText( "Chưa gọi món");
                    glue_idcustomer_I1.EditValue = null;
                    glue_IDEMP_I1.EditValue = null;
                    glue_IDEMP_I1.Properties.NullText = "";
                    glue_idcustomer_I1.Properties.NullText = "";
                    gct_menu_C.DataSource = null;
                    gct_give_C.DataSource = null;
                    chk_isdiscount_I6.Checked = false;
                    chk_issurcharge_I6.Checked = false;
                }
            }
            catch { }
        }

        private int findRowIndexIndata(DataTable dt, string key)
        {
            int index = 0;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (key == dt.Rows[i]["idcommodity"].ToString())
                    {
                        index = i;
                        break;
                    }
                }
            }
            catch { }
            return index;
        }

        private void callMenu()
        {
            try
            {
                frm_Commodity_S frm = new frm_Commodity_S();
                frm.ShowDialog();
            }
            catch { }
        }

        private void loadListMenu(string idgroup)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read(" select hh.idcommodity, hh.sign,hh.commodity, hh.price,    case when ( P1.newprice) is null then 0 else P1.newprice end  as amountdiscount,   hh.price -  case when case when ( P1.newprice) is null then 0 else P1.newprice end >0 then  case when ( P1.newprice) is null then 0 else P1.newprice end else hh.price end  as discount,       case when   (P1.idpromotion)  is null then '' else   (P1.idpromotion) end    as idpromotion from dmcommodity as hh   left join (select  dt.newprice, dt.idcommodity, mt.idpromotion from PromotionDetail dt inner join Promotion mt on mt.idpromotion=dt.idpromotion inner join dmcommodity h1 on h1.idcommodity=dt.idcommodity where 1=1 and h1.idgroup= '" + idgroup + "' and CAST(datediff(d,0,GETDATE()) as datetime) between CAST(datediff(d,0,mt.fromdate) as datetime) and case when  mt.isunlimit =0 then CAST(datediff(d,0,mt.todate) as datetime) else CAST(datediff(d,0,GETDATE()) as datetime) end and mt.status=1 and dt.status=1 and  CONVERT(datetime,getdate(),103) between CONVERT(datetime,'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "',103) and case when mt.istimegold=1 then    CONVERT(datetime,'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "',103) else CONVERT(datetime,getdate(),103) end  ) as P1 on hh.idcommodity=P1.idcommodity   where hh.status=1  and hh.idgroup= '" + idgroup + "'");
                if (dt.Rows.Count > 0)
                {
                    gct_listmenu_C.DataSource = dt;
                }
                else
                {
                    gct_listmenu_C.DataSource = null;
                }
            }
            catch 
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi truy xuất hiển thị món");
            }
        }

        private void updateExportDetail(GridView gv, bool hotkey)
        {
            try
            {
                if (bvalue == true)
                {
                    DataTable dt = new DataTable();
                    dt = APCoreProcess.APCoreProcess.Read("select * from exportdetail where  idexport='" + lbl_idpurchase_S.Text + "' and idcommodity='" + gv.GetRowCellValue(gv.FocusedRowHandle, "idcommodity").ToString() + "' and give='"+ give +"'");
                    DataTable dtHH = new DataTable();
                    dtHH = APCoreProcess.APCoreProcess.Read("select price, idunit, idwarehouse,idcommodity from dmcommodity where idcommodity='" + gv.GetRowCellValue(gv.FocusedRowHandle, "idcommodity").ToString() + "'");
                    if (dt.Rows.Count > 0)// có rồi cập nhật
                    {
                        DataRow dr = dt.Rows[0];
                        dr["idcommodity"] = gv.GetRowCellValue(gv.FocusedRowHandle, "idcommodity").ToString();
                        dr["idunit"] = dtHH.Rows[0]["idunit"].ToString();// không cần thiết
                        dr["idwarehouse"] = dtHH.Rows[0]["idwarehouse"].ToString();// không cần thiết
                        dr["idpromotion"] = gv.GetRowCellValue(gv.FocusedRowHandle, "idpromotion").ToString();
                        if (hotkey == false)// khong hotkey
                        {
                            if (Convert.ToDouble(dr["quantity"]) >= calquantity)
                            {
                                if (calquantity > 0)
                                {
                                    dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString() + " - " + (Convert.ToDouble(dr["quantity"]) - calquantity).ToString();// hiển thị
                                }
                                else
                                {
                                    dr["strquantity"] = 0;
                                }                                
                            }
                            else
                            {
                                if (Convert.ToDouble(dr["quantity"]) < calquantity)
                                {
                                    dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString() + " + " + (calquantity - Convert.ToDouble(dr["quantity"])).ToString();// hiển thị
                                }
                                else
                                {
                                    dr["strquantity"] = calquantity.ToString();
                                }
                            }
                            dr["quantity"] = calquantity;
                        }
                        else// hotkey
                        {
                            if (Convert.ToDouble(dr["quantity"]) + calquantity <= 0)
                            {
                                // 05/04/2015 sua
                                APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport= '"+ lbl_idpurchase_S.Text +"' and idcommodity='"+ dtHH.Rows[0]["idcommodity"].ToString() +"' and give='"+ give +"'");
                                loadInfoInvoice(getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()));
                                setStatusInvoice();
                                return;
                            }
                            if (calquantity > 0)
                            {
                                dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString()+  " + " + calquantity.ToString();// hiển thị
                            }
                            else
                            {
                                dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString() + ""+  calquantity.ToString();// hiển thị
                            }
                            dr["quantity"] = Convert.ToDouble(dr["quantity"])+calquantity;
                        }
                    
                        dr["price"] = Convert.ToDouble(dtHH.Rows[0]["price"].ToString());
                        dr["davat"] = "False";
                        dr["discount"] = 0;
                        dr["costs"] = 0;
                        dr["idexport"] = lbl_idpurchase_S.Text;
                        dr["note"] = "Bán lẻ";
                        dr["status"] = "True";
                        dr["iddetail"] = lbl_idpurchase_S.Text + dr["idcommodity"].ToString();
                        dr["amount"] = 0;
                        dr["vat"] = 0;
                        dr["amountvat"] = 0;
                        dr["amountdiscount"] = calamountdiscount;
                        dr["total"] = 0;
                        dr["pending"] = "False";
                        if (give==true) // neu la hang tang
                        dr["give"] = true;
                        else
                            dr["give"] = false;
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    else // chưa có thêm vào
                    {
                        if ( calquantity <= 0)
                            return;
                        DataRow dr = dt.NewRow();
                        dr["idpromotion"] = gv.GetRowCellValue(gv.FocusedRowHandle, "idpromotion").ToString();
                        if (gv.GetRowCellValue(gv.FocusedRowHandle, "idpromotion").ToString()!="")
                            dr["isdiscount"] = "True";
                        else
                            dr["davat"] = "False";
                        dr["_index"] = clsFunction.autonumber("_index","exportdetail");
                        dr["idcommodity"] = gv.GetRowCellValue(gv.FocusedRowHandle, "idcommodity").ToString();      
                        dr["idunit"] = dtHH.Rows[0]["idunit"].ToString();// không cần thiết
                        dr["idwarehouse"] = dtHH.Rows[0]["idwarehouse"].ToString();// không cần thiết
                        dr["quantity"] = calquantity;
                        dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString();
                        dr["price"] = Convert.ToDouble(dtHH.Rows[0]["price"].ToString());
                        dr["davat"] = "False";
                        dr["discount"] = 0;
                        dr["costs"] = 0;
                        dr["idexport"] = lbl_idpurchase_S.Text;
                        dr["note"] = "Bán lẻ";
                        dr["status"] = "True";                  
                        dr["iddetail"] = lbl_idpurchase_S.Text + dr["idcommodity"].ToString();
                        dr["amount"] = 0;
                        dr["vat"] = 0;
                        dr["amountvat"] = 0;
                        dr["amountdiscount"] = calamountdiscount; 
                        dr["total"] = 0;
                        dr["pending"] = "False";
                        if (give==true) // neu la hang tang
                            dr["give"] = true;
                        else
                            dr["give"] = false;
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where quantity <=0 ");
                    loadInfoInvoice(getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()));
                    setStatusInvoice();
                    gv_table_C.LayoutChanged();
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi cập nhật món ăn");
            }
            give = false;
        }

        private void updateExportDetailFromTableSplit(DataTable dts)
        {
            try
            {
                if (dts != null)
                {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        DataTable dt = new DataTable();
                        dt = APCoreProcess.APCoreProcess.Read("select * from exportdetail where  idexport='" + lbl_idpurchase_S.Text + "' and idcommodity='" + dts.Rows[i]["idcommodity"].ToString() + "'");
                        DataTable dtHH = new DataTable();
                        dtHH = APCoreProcess.APCoreProcess.Read("select price, idunit, idwarehouse from dmcommodity where idcommodity='" + dts.Rows[i]["idcommodity"].ToString() + "'");
                        DataRow dr = dt.NewRow();
                        dr["idcommodity"] = dts.Rows[i]["idcommodity"].ToString();
                        dr["idunit"] = dtHH.Rows[0]["idunit"].ToString();// không cần thiết
                        dr["idwarehouse"] = dtHH.Rows[0]["idwarehouse"].ToString();// không cần thiết
                        dr["quantity"] = Convert.ToDouble( dts.Rows[i]["quantity"].ToString());
                        dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString();
                        dr["price"] = Convert.ToDouble(dtHH.Rows[0]["price"].ToString());
                        dr["davat"] = "False";
                        dr["discount"] = 0;
                        dr["costs"] = 0;
                        dr["idexport"] = lbl_idpurchase_S.Text;
                        dr["note"] = "Bán lẻ";
                        dr["status"] = "True";
                        dr["iddetail"] = lbl_idpurchase_S.Text + dr["idcommodity"].ToString();
                        dr["amount"] = 0;
                        dr["vat"] = 0;
                        dr["amountvat"] = 0;
                        dr["amountdiscount"] = 0;
                        dr["total"] = 0;
                        dr["pending"] = "False";
                        dt.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                        loadInfoInvoice(getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString()));
                        setStatusInvoice();
                    }
                }
            }
            catch
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi cập nhật món ăn");
            }
        }

        private void deleteMenu()
        {
    
            {
                if (Convert.ToDouble(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "quantity")) <= 1)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idcommodity='" + gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString() + "' and idexport='" + lbl_idpurchase_S.Text + "' ");
                    gv_menu_C.DeleteRow(gv_listmenu_C.FocusedRowHandle);
                }
                else
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set quantity=quantity-1 where idcommodity='" + gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "idcommodity").ToString() + "' and idexport='" + lbl_idpurchase_S.Text + "' ");
                    gv_menu_C.SetRowCellValue(gv_menu_C.FocusedRowHandle, "quantity", Convert.ToDouble(gv_menu_C.GetRowCellValue(gv_menu_C.FocusedRowHandle, "quantity")) - 1);
                }
                setStatusInvoice();
                setButton(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
            }
        }

        private void loadTotal()
        {
            try
            {
                decimal total = 0;
                decimal quantity = 0;
                for (int i = 0; i < gv_menu_C.DataRowCount; i++)
                {
                    total += Convert.ToDecimal(gv_menu_C.GetRowCellValue(i, "total"));
                    quantity += Convert.ToDecimal(gv_menu_C.GetRowCellValue(i, "quantity"));
                }
                cal_costs_I1.Value = total;
                cal_amount_I4.Value = cal_costs_I1.Value + cal_amountsurcharge_I4.Value - cal_amountdiscount_I4.Value;
            }
            catch { }
        }

        private void chageTable()
        {
            int state = -1;
            state = checkStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
            if (state == 0 && idTableChoose!="" && changeTable==true)
            {
                DialogResult dl = XtraMessageBox.Show(clsFunction.transLateText("Bạn có muốn chuyển ") + TableChoose + clsFunction.transLateText(" sang ") + gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString(), clsFunction.transLateText("Thông báo"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dl == DialogResult.OK)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set idtable='" + gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString() + "' where idexport='" + lbl_idpurchase_S.Text + "'");
                }
                else
                {
                    idTableChoose = "";
                    TableChoose = "";
                    changeTable = false;
                    lbl_change_S.Text = "";
                }
                idTableChoose = "";
                TableChoose = "";
                changeTable = false;
                lbl_change_S.Text = "";
            }
            else
            {
                if(changeTable==true)
                clsFunction.MessageInfo("Thông báo","Vui lòng chọn bàn trống");
            }
        }

        private void mergeTable()
        {
            int state = -1;
            string idexport = "";
            state = checkStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
            if (state == 3 && idTableChoose != "" && MergeTable == true && idTableChoose != gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString())
            {
                DialogResult dl = XtraMessageBox.Show(clsFunction.transLateText("Bạn có muốn nhập")+"  " + TableChoose+ " " + clsFunction.transLateText("vào")+" " + gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString(), clsFunction.transLateText("Thông báo"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dl == DialogResult.OK)
                {
                    // lấy thông tin hóa đơn bàn cần nhập vào bàn nhập
                    DataTable dtCanNhap = new DataTable();
                    dtCanNhap = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                    if (dtCanNhap.Rows.Count > 0)
                    {
                        DataTable dtNhap = new DataTable();
                        DataRow dr;
                        for (int i = 0; i < dtCanNhap.Rows.Count; i++)
                        {
                            idexport = getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                            dtNhap = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + idexport + "' and idcommodity='" + dtCanNhap.Rows[i]["idcommodity"].ToString() + "'");
                            if (dtNhap.Rows.Count == 0)// chưa có insert
                            {
                                dr = dtNhap.NewRow();
                                dr["iddetail"] = idexport + dtCanNhap.Rows[i]["idcommodity"].ToString();
                                dr["idcommodity"] = dtCanNhap.Rows[i]["idcommodity"].ToString();
                                dr["idunit"] = dtCanNhap.Rows[i]["idunit"].ToString();
                                dr["idwarehouse"] = dtCanNhap.Rows[i]["idwarehouse"].ToString();
                                dr["quantity"] = Convert.ToDouble( dtCanNhap.Rows[i]["quantity"]);
                                dr["strquantity"] = Convert.ToDouble(dtCanNhap.Rows[i]["quantity"]).ToString();
                                dr["price"] = Convert.ToDouble(dtCanNhap.Rows[i]["price"]);
                                dr["amount"] = Convert.ToDouble(dtCanNhap.Rows[i]["amount"]);
                                dr["vat"] = Convert.ToDouble(dtCanNhap.Rows[i]["vat"]);
                                dr["amountvat"] = Convert.ToDouble(dtCanNhap.Rows[i]["amountvat"]);
                                dr["davat"] = dtCanNhap.Rows[i]["davat"].ToString();
                                dr["discount"] = Convert.ToDouble(dtCanNhap.Rows[i]["discount"]);
                                dr["amountdiscount"] = Convert.ToDouble(dtCanNhap.Rows[i]["amountdiscount"]);
                                dr["costs"] = Convert.ToDouble(dtCanNhap.Rows[i]["costs"]);
                                dr["total"] = Convert.ToDouble(dtCanNhap.Rows[i]["total"]);
                                dr["note"] = dtCanNhap.Rows[i]["note"].ToString();
                                dr["idexport"] = idexport;
                                dr["status"] = dtCanNhap.Rows[i]["status"].ToString();
                                dr["pending"] = dtCanNhap.Rows[i]["pending"].ToString();
                                dtNhap.Rows.Add(dr);
                                APCoreProcess.APCoreProcess.Save(dr);
                            }
                            else// có rồi update
                            {
                                dr = dtNhap.Rows[0];                              
                                dr["quantity"] =Convert.ToDouble( dr["quantity"]) + Convert.ToDouble( dtCanNhap.Rows[i]["quantity"]);
                                dr["strquantity"] = (Convert.ToDouble( dr["quantity"])).ToString();                 
                                APCoreProcess.APCoreProcess.Save(dr);
                            }                            
                        }
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='0' where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                    }
                }
                else
                {
                    idTableChoose = "";
                    TableChoose = "";
                    MergeTable = false;
                    lbl_change_S.Text = "";
                }
                idTableChoose = "";
                TableChoose = "";
                MergeTable = false;
                lbl_change_S.Text = "";
            }
            else
            {
                if (MergeTable == true)
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn bàn cần nhập");
            }
        }

        private void unionTable()
        {
            int state = -1;
            string idexport = "";
            state = checkStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
            if ((state == 3 || state == 1) && idTableChoose != "" && UnionTable == true && idTableChoose != gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString())
            {
                DialogResult dl = XtraMessageBox.Show(clsFunction.transLateText("Bạn có muốn ghép") + "  " + TableChoose + " " + clsFunction.transLateText("vào") + " " + gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString(), clsFunction.transLateText("Thông báo"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dl == DialogResult.OK)
                {
             
                    // lấy thông tin hóa đơn bàn cần nhập vào bàn nhập
                    DataTable dtCanNhap = new DataTable();
                    dtCanNhap = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                    if (dtCanNhap.Rows.Count > 0)
                    {
                        DataTable dtNhap = new DataTable();
                        DataRow dr;
                        for (int i = 0; i < dtCanNhap.Rows.Count; i++)
                        {
                            idexport = getInvoiceByIDTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
                            dtNhap = APCoreProcess.APCoreProcess.Read("select * from exportdetail where idexport='" + idexport + "' and idcommodity='" + dtCanNhap.Rows[i]["idcommodity"].ToString() + "'");
                            if (dtNhap.Rows.Count == 0)// chưa có insert
                            {
                                dr = dtNhap.NewRow();
                                dr["iddetail"] = idexport + dtCanNhap.Rows[i]["idcommodity"].ToString();
                                dr["idcommodity"] = dtCanNhap.Rows[i]["idcommodity"].ToString();
                                dr["idunit"] = dtCanNhap.Rows[i]["idunit"].ToString();
                                dr["idwarehouse"] = dtCanNhap.Rows[i]["idwarehouse"].ToString();
                                dr["quantity"] = Convert.ToDouble(dtCanNhap.Rows[i]["quantity"]);
                                dr["strquantity"] = Convert.ToDouble(dtCanNhap.Rows[i]["quantity"]).ToString();
                                dr["price"] = Convert.ToDouble(dtCanNhap.Rows[i]["price"]);
                                dr["amount"] = Convert.ToDouble(dtCanNhap.Rows[i]["amount"]);
                                dr["vat"] = Convert.ToDouble(dtCanNhap.Rows[i]["vat"]);
                                dr["amountvat"] = Convert.ToDouble(dtCanNhap.Rows[i]["amountvat"]);
                                dr["davat"] = dtCanNhap.Rows[i]["davat"].ToString();
                                dr["discount"] = Convert.ToDouble(dtCanNhap.Rows[i]["discount"]);
                                dr["amountdiscount"] = Convert.ToDouble(dtCanNhap.Rows[i]["amountdiscount"]);
                                dr["costs"] = Convert.ToDouble(dtCanNhap.Rows[i]["costs"]);
                                dr["total"] = Convert.ToDouble(dtCanNhap.Rows[i]["total"]);
                                dr["note"] = dtCanNhap.Rows[i]["note"].ToString();
                                dr["idexport"] = idexport;
                                dr["status"] = dtCanNhap.Rows[i]["status"].ToString();
                                dr["pending"] = dtCanNhap.Rows[i]["pending"].ToString();
                                dtNhap.Rows.Add(dr);
                                APCoreProcess.APCoreProcess.Save(dr);
                            }
                            else// có rồi update
                            {
                                dr = dtNhap.Rows[0];
                                dr["quantity"] = Convert.ToDouble(dr["quantity"]) + Convert.ToDouble(dtCanNhap.Rows[i]["quantity"]);
                                dr["strquantity"] = Convert.ToDouble(dr["quantity"]).ToString()+"+" + Convert.ToDouble(dtCanNhap.Rows[i]["quantity"]).ToString();
                                APCoreProcess.APCoreProcess.Save(dr);
                            }
                        }
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete exportdetail where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='-1' where idexport='" + getInvoiceByIDTable(idTableChoose) + "'");
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set tableunion= '"+ getTablePreUnion(idTableChoose) + idTableChoose + "@"+ "' where idexport='" + idexport + "'");
                    }
                }
                else
                {
                    idTableChoose = "";
                    TableChoose = "";
                    UnionTable = false;
                    lbl_change_S.Text = "";
                }
                idTableChoose = "";
                TableChoose = "";
                UnionTable = false;
                lbl_change_S.Text = "";
            }
            else
            {
                if (UnionTable == true)
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn bàn cần ghép");
            }
        }

        private void splitTable()
        {
            int state = -1;
            //string idexport = "";
            state = checkStatusTable(gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString());
            if ((state == 3 || state == 0) && idTableChoose != "" && SplitTable == true && idTableChoose != gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString())
            {
                DialogResult dl = XtraMessageBox.Show(clsFunction.transLateText("Bạn có muốn tách") + "  " + TableChoose + " " + clsFunction.transLateText("vào") + " " + gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString(), clsFunction.transLateText("Thông báo"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dl == DialogResult.OK)
                {
                    frmSplitTable frm = new frmSplitTable();
                    frm.idtablechoose = idTableChoose;
                    frm.idtablemove = gv_table_C.GetRowCellValue(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    frm.nametablechoose = TableChoose;
                    frm.nametablemove = gv_table_C.GetRowCellDisplayText(gv_table_C.FocusedRowHandle, gv_table_C.FocusedColumn.FieldName.ToString()).ToString();
                    frm.passData = new frmSplitTable.PassData(getValueSplit);
                    frm.passDataDT = new frmSplitTable.PassDataDT(getValueSplitDT);
                    frm.ShowDialog();
                    setStatusTable(idTableChoose);
                }
            }
        }

        private void getValueSplit(bool value)
        {
            SplitTable = false;
            lbl_change_S.Text = "";
            if (value == true)
            {            
                //MessageBox.Show("Thực thi tách");
            }
        }

        private void getValueSplitDT(DataTable value)
        {        
            if (value !=null)
            {
                setClickButton(clsFunction.transLateText( "F1 Gọi món"));
                updateExportDetailFromTableSplit(value);
            }
        }

        private string getTablePreUnion(string idtable)
        { 
            string tablepre = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select tableunion from export where idexport='"+ getInvoiceByIDTable(idtable) +"'");
            if (dt.Rows.Count > 0)
            {
                tablepre = dt.Rows[0][0].ToString();
            }
            return tablepre.Trim();
        }

        private void loadInvoice()
        {
            DataTable dtTime = new DataTable();
            int hh, mm;
            hh = 0;
            mm = 0;
            dtTime = APCoreProcess.APCoreProcess.Read("select * from sysConfigBill");
            if (dtTime.Rows.Count > 0)
            {
                hh = TimeSpan.Parse(dtTime.Rows[0]["timestart"].ToString()).Hours;
                mm = TimeSpan.Parse(dtTime.Rows[0]["timestart"].ToString()).Minutes;
            }
            DataTable dt = new DataTable();
            DateTime dtestart;
            if (DateTime.Now.Hour <= hh)
            {
                dtestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,hh,mm,0).AddDays(-1);
            }
            else
            {
                dtestart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh,mm,0);
            }
            //dt = APCoreProcess.APCoreProcess.Read("SELECT     pt.idreceipt, pt.idexport, pt.datereceipt, pt.datereceipt AS timereceipt, pt.IDEMP, pt.amount , pt.note, pt.proceeds, pt.surplus, pt.idtype, pt.surcharge, pt.discount, pt.vat,pt.amount - pt.discount + pt.surcharge as total ,  pt.idtable, ba.idfloors, px.invoice, pt.undo FROM RECEIPT AS pt INNER JOIN  DMTABLE AS ba ON pt.idtable = ba.idtable INNER JOIN  EXPORT AS px ON pt.idexport = px.idexport WHERE     (pt.undo <> 1) and CAST(datediff(d,0,datereceipt) as datetime)  =  CAST(datediff(d,0,getdate()) as datetime)  ");
            dt = APCoreProcess.APCoreProcess.Read("SELECT     pt.idreceipt, pt.idexport, pt.datereceipt, pt.datereceipt AS timereceipt, pt.IDEMP, pt.amount , pt.note, pt.proceeds, pt.surplus, pt.idtype, pt.surcharge, pt.discount, pt.vat,pt.amount - pt.discount + pt.surcharge as total ,  pt.idtable, ba.idfloors, px.invoice, pt.undo FROM RECEIPT AS pt INNER JOIN  DMTABLE AS ba ON pt.idtable = ba.idtable INNER JOIN  EXPORT AS px ON pt.idexport = px.idexport WHERE     (pt.undo <> 1) and datereceipt between  convert(datetime, '" + dtestart.ToString("yyyyMMdd HH:mm:ss") + "',103) and getdate() and del <> 1 ");
            if (dt.Rows.Count > 0)
            {
                gct_list_C.DataSource = dt;
            }
            else
            {
                gct_list_C.DataSource = null;
            }
        }

        private void InitInvoice()
        {
            try
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit) gv_list_C.Columns["idtable"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtable  , tableNO from dmtable ");
                ((DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit)gv_list_C.Columns["IDEMP"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select IDEMP  , StaffName from EMPLOYEES ");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_list_C.Columns["idfloors"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idfloors  , floors from dmfloors ");
                ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)gv_list_C.Columns["idtype"].ColumnEdit).DataSource = APCoreProcess.APCoreProcess.Read("select idtype  , typename from dmtype ");
            }
            catch { }
        }

        private void getValueInvoice(bool value)
        {
            if (value == true)
            {
                lbl_idpurchase_S.Text = "";
                for(int i=0;i<gv_menu_C.DataRowCount;i++)
                {
                    updateStockOut(gv_menu_C.GetRowCellValue(i, "idcommodity").ToString(), Convert.ToDouble(gv_menu_C.GetRowCellValue(i, "quantity")), (lbl_idpurchase_S.Text + gv_menu_C.GetRowCellValue(i, "idcommodity").ToString()), getIdunitByIdCommodity(gv_menu_C.GetRowCellValue(i, "idcommodity").ToString()));
                    loadInvoice();
                    cal_discount_I4.EditValue = 0;
                    cal_surcharge_I4.EditValue = 0;
                    cal_amount_I4.EditValue = 0;
                    cal_costs_I1.EditValue = 0;
                }
            }
        }

        private string getIdunitByIdCommodity(string idcommodity)
        {
            string idunit = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select idunit from dmcommodity where idcommodity='"+ idcommodity +"'");
            if (dt.Rows.Count > 0)
            {
                idunit=dt.Rows[0][0].ToString();
            }
            return idunit;
        }

        private void updateStockOut(string idcommodity, double quantity, string iddetailex, string idunit)
        {
            // kiểm tra tồn kho
            DataTable dtI = new DataTable();
            dtI = APCoreProcess.APCoreProcess.Read("SELECT     dt.quantity  - CASE WHEN SUM(stk.quantity) IS NULL THEN 0 ELSE SUM(stk.quantity) END  AS quantity, dt.iddetail FROM         PURCHASEDETAIL AS dt INNER JOIN  PURCHASE AS mt ON dt.idpurchase = mt.idpurchase LEFT OUTER JOIN   STOCKIO AS stk ON dt.iddetail = stk.iddetailpur GROUP BY dt.idcommodity, dt.status, dt.iddetail, dt.quantity HAVING      (dt.status = 0) AND (dt.idcommodity = N'" + idcommodity + "') AND (dt.quantity  - CASE WHEN SUM(stk.quantity) IS NULL THEN 0 ELSE SUM(stk.quantity) END  > 0) ");
            double quantitytemp = 0;// số lượng còn lại tạm
            if (dtI.Rows.Count > 0)// tồn kho đủ
            {
                for (int i = 0; i < dtI.Rows.Count; i++)
                {
                    string iddetailpur = "";
                    iddetailpur = dtI.Rows[i]["iddetail"].ToString();
                    double quantityrest = 0;
                    DataTable dtQuanDT = new DataTable();
                    dtQuanDT = APCoreProcess.APCoreProcess.Read("select dt.quantity - case when sum(stk.quantity) is null then 0 else sum(stk.quantity) end as quantity from purchasedetail dt left join STOCKIO stk on stk.iddetailpur=dt.iddetail group by dt.quantity,dt.iddetail  having dt.iddetail='" + iddetailpur + "'");
                    if (dtQuanDT.Rows.Count > 0)
                    {
                        quantityrest = Convert.ToDouble(dtQuanDT.Rows[0]["quantity"]);
                    }
                    if (quantityrest < quantity && dtI.Rows.Count <= 1)
                    {
                        // cho xuất hàng nhưng không cập nhật vào tồn. cập nhật pending =true
                        // Khi nhập hàng sẽ phát sinh tự động nhập vào tồn
                        APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=1 where iddetail='" + iddetailex + "'");
                        break;
                    }
                    DataTable dtSTK = new DataTable();
                    dtSTK = APCoreProcess.APCoreProcess.Read("SELECT     iddetailpur, iddetailex, idunit, quantity, datein, dateup,quantityrest FROM  STOCKIO WHERE     (iddetailpur = N'" + iddetailpur + "') AND (iddetailex = N'" + iddetailex + "')");
                    DataRow dr;
                    if (dtSTK.Rows.Count == 0)
                    {
                        dr = dtSTK.NewRow();
                        dr["iddetailpur"] = iddetailpur;
                        dr["iddetailex"] = iddetailex;
                        dr["idunit"] = idunit;
                        dr["quantity"] = (quantity - quantitytemp) - quantityrest > 0 ? quantityrest : (quantity - quantitytemp);
                        dr["quantityrest"] = (quantity - quantitytemp) - quantityrest > 0 ? 0 : quantityrest - (quantity - quantitytemp);
                        dr["datein"] = DateTime.Now;
                        dtSTK.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                        APCoreProcess.APCoreProcess.ExcuteSQL("update STOCKIO set quantityrest=" + ((quantity - quantitytemp) - quantityrest > 0 ? 0 : quantityrest - (quantity - quantitytemp)) + " where iddetailpur='" + iddetailpur + "' and quantityrest <> 0 ");
                        // Cập nhât lại trạng thái chi tiết phiếu xuất (đã xuất âm )
                        APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=0 where iddetail='" + iddetailex + "'");
                    }
                    if (quantity - quantityrest <= 0)
                    {
                        break;
                    }
                    else
                    {
                        quantitytemp += quantityrest;
                    }
                }
            }
            else
            {
                // cho xuất hàng nhưng không cập nhật vào tồn. cập nhật pending =true
                // Khi nhập hàng sẽ phát sinh tự động nhập vào tồn
                APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set pending=1 where iddetail='" + iddetailex + "'");
            }
        }

        private void cancelInvoice(string idreceipt)
        {
            if(clsFunction.MessageDelete("Thông báo","Bạn có muốn hoàn tác"))
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select idtable, idexport from receipt where idreceipt='" + idreceipt + "'");
                if (dt.Rows.Count > 0)
                {
                    int state = -1;
                    string idexport = "";
                    idexport = dt.Rows[0]["idexport"].ToString();
                    state = checkStatusTable(dt.Rows[0]["idtable"].ToString());
                    if (state == 0)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete receipt where idexport='"+ idexport +"'");
                        APCoreProcess.APCoreProcess.ExcuteSQL("update export set complet=0 where idexport='" + idexport + "'");
                        gv_list_C.DeleteRow(gv_list_C.FocusedRowHandle);
                        setButton(dt.Rows[0]["idtable"].ToString());
                    }
                }
            }
        }

        private void setStatusTable(string idtable)// khi tong so luong tren hoa don =0 thi chuyen ve trang thai ban cho
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     idexport,SUM(quantity)  as qty FROM  EXPORTDETAIL GROUP BY idexport HAVING (idexport = N'" + getInvoiceByIDTable(idtable) + "') ");
            if (dt.Rows.Count >0 )
            {
                if(Convert.ToInt16(dt.Rows[0][1])==0)
                    APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='1' where idexport='" + getInvoiceByIDTable(idtable) + "'");
            }
            else
                APCoreProcess.APCoreProcess.ExcuteSQL("update export set status='1' where idexport='" + getInvoiceByIDTable(idtable) + "'");
        }

        private void updateSQL()
        {

            APCoreProcess.APCoreProcess.ExcuteSQL("ALTER proc [dbo].[getInvoiceByDateTime] (@fromdate  datetime, @todate  datetime) as begin SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del  having convert(datetime, datereceipt) between convert(datetime,@fromdate,103) and convert(datetime,@todate,103) and hd.del <> 1  end ");
            APCoreProcess.APCoreProcess.ExcuteSQL("if not exists(select * from sys.columns   where Name = N'del' and Object_ID = Object_ID(N'receipt')) begin    ALTER TABLE receipt ADD del BIT CONSTRAINT Constraint_del DEFAULT 0 WITH VALUES end");
            APCoreProcess.APCoreProcess.ExcuteSQL("if not exists(select * from sys.columns   where Name = N'give' and Object_ID = Object_ID(N'exportdetail')) begin    ALTER TABLE exportdetail ADD give BIT CONSTRAINT Constraint_give DEFAULT 0 WITH VALUES end");
            APCoreProcess.APCoreProcess.ExcuteSQL("ALTER proc [dbo].[getInvoiceByDateTime] (@fromdate  datetime, @todate  datetime) as begin SELECT     kh.customer, hd.idreceipt, ex.invoice ,hd.idexport, hd.datereceipt, hd.datereceipt AS hours, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName,  ban.tableNO AS tableno, hd.amount + hd.surcharge - hd.discount AS total FROM         DMTABLE AS ban INNER JOIN   RECEIPT AS hd ON ban.idtable = hd.idtable INNER JOIN  EMPLOYEES AS nv ON hd.IDEMP = nv.IDEMP inner join EXPORT as ex on ex.idexport=hd.idexport INNER JOIN   DMCUSTOMERS AS kh on kh.idcustomer=ex.idcustomer GROUP BY kh.customer, hd.idreceipt,ex.invoice, hd.idexport, hd.datereceipt, hd.amount, hd.note, hd.surcharge, hd.discount, hd.vat, nv.StaffName, ban.tableNO, hd.del  having convert(datetime, datereceipt) between convert(datetime,@fromdate,103) and convert(datetime,@todate,103) and hd.del <> 1  end ");
        }

        private void savesurcharge(bool isSurcharge, double surcharge,double amountsurcharge)
        {
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update export set surcharge="+ surcharge+", amountsurcharge ="+ amountsurcharge +", issurcharge='"+ isSurcharge +"', reasonsurcharge= case when 'False'='"+ isSurcharge.ToString() +"' then '' else reasonsurcharge end  where idexport='"+ lbl_idpurchase_S.Text  +"' ");
            }
            catch { }
        }

        private void savediscount(bool isdiscount, double discount, double amountdiscount)
        {
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update export set discount=" + discount + ", amountdiscount =" + amountdiscount + ", isdiscount='" + isdiscount + "', reasondiscount = case when 'False'='" + isdiscount.ToString() + "' then '' else reasondiscount end where idexport='" + lbl_idpurchase_S.Text + "' ");
            }
            catch { }
        }
        
        #endregion               

        }  
   }