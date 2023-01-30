using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using Function;
using DevExpress.XtraGrid.Views.Grid;

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_NHIEMVU_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_NHIEMVU_S()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        public bool _insert = false;
        public bool call = false;     
        public bool statusForm;    
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "HH";
        public string ID = "";
        public string idquotation = "";
        DateTime ngaytaoPO = new DateTime();

        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            try
            {
                // statusForm = true;
                if (statusForm == true)
                {
                    Function.clsFunction.Save_sysControl(this, this);
                    try
                    {
                        Function.clsFunction.CreateTable(this, this);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Function.clsFunction.TranslateForm(this, this.Name);
                    loadGridLookupCustomer();
                    loadGridLookupThucHien();
                    loadGridLookupQuanly();
                    loadGridLookupPriority();
                    loadGridLookupPlanType();
                    loadGridLookupLoaiBaoTri();
                    dte_ngaytao_I5.EditValue = DateTime.Now;
                    LoadInfo();
                    if (txt_idquotation_I1.Text != "")
                    {
                        loadQuotationInfo();
                    }
                    else
                    {
                        glue_idcustomer_I1.EditValue = "";
                        glue_manhanvienphutrach_I1.EditValue = "";
                    }
                }

                loadCommodity();
                loadChothue();
                chk_status_I6.Enabled = false;

                if (clsFunction.checkIsmanagerPo(clsFunction.GetManvByUser()))
                {
                    if (!chk_duyet_I6.Checked)
                    {
                        chk_duyet_I6.Enabled = true;
                        glue_manhanvienthuchien_I1.Enabled = true;
                    }
                    else
                    {
                        chk_duyet_I6.Enabled = false;
                        glue_manhanvienthuchien_I1.Enabled = false;
                    }
                    dte_ngaygiaohang_I5.Enabled = true;
                    dte_ngaynghiemthunoibo_I5.Enabled = true;
                }
                else
                {
                    chk_duyet_I6.EditValue = false;
                    dte_ngaygiaohang_I5.Enabled = true;
                    dte_ngaynghiemthunoibo_I5.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo","Error " + ex.Message);
            }
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            try
            {
                glue_idcustomer_I1.Focus();
            }
            catch { }
        }

        private void frm_DMAREA_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btn_insert_allow_insert.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F5)
                {
                    bbi_allow_insert.PerformClick();
                }
                else
                {
                    if (e.KeyCode == Keys.F9)
                    {
                        btn_exit_S.PerformClick();
                    }
                }
            }
        }


        #endregion

        #region ButtonEvent

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            string str = "";
            bool flagChoose = false;
            if (txt_idquotation_I1.Text.Contains("TTM") || txt_idquotation_I1.Text.Contains("TTC") || txt_idquotation_I1.Text.Contains("BTM") || txt_idquotation_I1.Text.Contains("BTC"))
            {
                txt_idcommodity_I1.Text = "";
                for (int i = 0; i < gv_plan_C.DataRowCount; i++)
                {
                    if (_insert)
                    {
                        if (Convert.ToBoolean(gv_plan_C.GetRowCellValue(i, "choose").ToString() == "" ? false : gv_plan_C.GetRowCellValue(i, "choose")))
                        {
                            txt_idcommodity_I1.Text = "'" + gv_plan_C.GetRowCellValue(i, "idcommodity").ToString() + "'" + "";
                            flagChoose = true;
                        }
                    }
                    else
                    {
                        txt_idcommodity_I1.Text = "'" + gv_plan_C.GetRowCellValue(i, "idcommodity").ToString() + "'" + "";
                    }
                }
            }
            else
            {
                txt_idcommodity_I1.Text = "";
               
                for (int i = 0; i < gv_plan_C.DataRowCount; i++)
                {
                    if (_insert)
                    {
                        if (Convert.ToBoolean(gv_plan_C.GetRowCellValue(i, "choose").ToString() == "" ? false : gv_plan_C.GetRowCellValue(i, "choose")))
                        {
                            str += "'" + gv_plan_C.GetRowCellValue(i, "idcommodity").ToString() + "'" + ",";
                            flagChoose = true;
                        }
                    }
                    else
                    {
                        str += "'" + gv_plan_C.GetRowCellValue(i, "idcommodity").ToString() + "'" + ",";
                    }
                }
                txt_idcommodity_I1.Text = str.Substring(0, str.Length - 1);
            }
            if (flagChoose == false && _insert)
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn ít nhất 1 sản phẩm");
                return;
            }
           
            if (!checkInput()) return;
            if (chk_duyet_I6.Checked)
            {
                if (clsFunction.MessageDelete("Thông báo", "Quản lý đơn hàng đã duyệt, không thể chỉnh sửa thông tin kế hoạch \n Bạn chỉ có thể thay đỗi tình trạng kế hoạch \n Bạn có chắc muốn cập nhật tình trạng kế hoạch không ?"))
                {
                    // update status
                    APCoreProcess.APCoreProcess.ExcuteSQL("update nhiemvu set idplantype='"+ glue_idplantype_I1.EditValue.ToString() +"' where manhiemvu='"+ txt_manhiemvu_IK1.Text +"'");
                    clsFunction.MessageInfo("Thông báo", "Cập nhật trạng thái thành công");
                }

                return;
            }
            save();
            
            if (dxEp_error_S.HasErrors == false)
            {
                glue_idloaibaotri_I1.Enabled = true;
                gct_hangmuc_baotri_C.Enabled = true;
                //this.Close();
            }            
            // 0317
            clsFunction.MessageInfo("Thông báo","Lưu thành công !!!");
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_insert_allow_insert_Click(object sender, EventArgs e)
        {
            try
            {
                save();
                if (dxEp_error_S.HasErrors == false)
                {
                    InitData();
                    txt_manhiemvu_IK1.Focus();
                    passData(true);
                }
            }
            catch { }
        }

        private void InitData()
        {
            _insert = true;
            LoadInfo();
        }

        #endregion
        
        #region Event

        // enter next control
        
        private void txt_sign_I2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ! sender.GetType().ToString().Contains("MemoEdit"))
                SendKeys.Send("{Tab}");
        }

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glue_idkind_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glue_idgroup_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }


        #endregion

        #region GridEvent

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

        #endregion

        #region Methods

        private void save()
        {
            try
            {
                
                if (_insert == true)
                {
                    Function.clsFunction.Insert_data(this, this.Name);
                    //APCoreProcess.APCoreProcess.ExcuteSQL("update dmcommodity set tenkhongdau = dbo.fChuyenCoDauThanhKhongDauNew(commodity) where idcommodity ='" + txt_manhiemvu_IK1.Text + "'");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name) + " = '" + txt_manhiemvu_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idthuexe_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    saveDetail();
                    if (call == true)
                    {
                        strpassData(txt_manhiemvu_IK1.Text);
                    }
                    else
                    {
                        //LoadInfo();
                    }
                    passData(true);
                    //this.Hide();  
                    dxEp_error_S.ClearErrors();
                    _insert = false;
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name), ID);
                    DataTable dt = APCoreProcess.APCoreProcess.Read("select * from NHIEMVU where manhiemvu = '"+ txt_manhiemvu_IK1.Text +"'");
                    //if ( (Convert.ToInt16( spe_sothangbaotri_I4.Value) !=  Convert.ToInt16( dt.Rows[0]["sothangbaotri"].ToString()) || Convert.ToInt16( spe_sothangchothue_I4.Value) != Convert.ToInt16( dt.Rows[0]["sothangchothue"].ToString()) || Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue).ToString("yyyy-MM-dd") != Convert.ToDateTime(dt.Rows[0]["ngaynhanviec"]).ToString("yyyy-MM-dd")))
                    {
                        saveDetail();
                    }
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idthuexe_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                   // update baotri detail
                    // luu bao tri detail
                    if (gv_baotri_C.FocusedRowHandle >= 0)
                    {
                        int iddetail = Convert.ToInt32(gv_baotri_C.GetRowCellValue(gv_baotri_C.FocusedRowHandle,"iddetail"));
                        if (iddetail != null && iddetail > 0 )
                        {
                            DataSet ds = new DataSet();
                            ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name) + " = '" + txt_manhiemvu_IK1.Text + "'"));

                            APCoreProcess.APCoreProcess.ExcuteSQL("delete from nhiemvu_baotri_detail where idbaotridetail = "+ iddetail +"");
                            DataTable dtbaotridetail = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_baotri_detail where idbaotridetail= "+ iddetail +"");
                            for (int i = 0; i < gv_hangmuc_baotri_C.RowCount; i++)
                            {
                                if (Convert.ToBoolean(gv_hangmuc_baotri_C.GetRowCellValue(i, "check")))
                                {
                                    DataRow dr;
                                    dr = dtbaotridetail.NewRow();
                                    dr["idbaotridetail"] = iddetail;
                                    dr["idbaotri"] = gv_hangmuc_baotri_C.GetRowCellValue(i, "id").ToString();
                                    dtbaotridetail.Rows.Add(dr);
                                    APCoreProcess.APCoreProcess.Save(dr);
                                }
                            }
                        }
                    }
                    dxEp_error_S.ClearErrors();
                    passData(true);
                }
                
            }
            catch
            {
            }
        }

        private void saveDetail()
        {
            try
            {
                if (dte_ngaygiaohang_I5.EditValue == null)
                {
                    return;
                }
                // luu bao tri va cho thue
                DataTable dtdetail = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where idplan='" + txt_manhiemvu_IK1.Text + "'");
                if (gv_baotri_C.DataRowCount > 0 && tab_baotri_C.PageVisible == true)
                {
                    // delete old data
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete nhiemvu_detail where idplan ='" + txt_manhiemvu_IK1.Text + "'  ");
                    DataRow dr = null;
                    for (int i = 0; i < gv_baotri_C.DataRowCount; i++)
                    {
                        dr = dtdetail.NewRow();
                        dr["idplan"] = txt_manhiemvu_IK1.Text;
                        dr["kybaotri"] = gv_baotri_C.GetRowCellValue(i, "kybaotri").ToString();
                        dr["ngaybaotri"] = Convert.ToDateTime(gv_baotri_C.GetRowCellValue(i, "ngaybaotri"));
                        dr["baotrighichu"] = gv_baotri_C.GetRowCellValue(i, "baotrighichu").ToString();
                        dtdetail.Rows.Add(dr);
                    }
                    APCoreProcess.APCoreProcess.Save(dr);
                    dtdetail = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where idplan='" + txt_manhiemvu_IK1.Text + "'");
                    gct_baotri_C.DataSource = dtdetail;
                }

                // luu bao tri va baotri
                DataTable dtdetailbaotri = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where idplan='" + txt_manhiemvu_IK1.Text + "'");
                if (gv_chothue_C.DataRowCount > 0 && tab_chothue_C.PageVisible == true)
                {
                    // delete old data
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete nhiemvu_detail where idplan ='" + txt_manhiemvu_IK1.Text + "'  ");
                    DataRow dr = null;
                    for (int i = 0; i < gv_chothue_C.DataRowCount; i++)
                    {
                        dr = dtdetailbaotri.NewRow();
                        dr["idplan"] = txt_manhiemvu_IK1.Text;
                        dr["kyhanchothue"] = gv_chothue_C.GetRowCellValue(i, "kyhanchothue").ToString();
                        dr["ngaybatdauchothue"] = Convert.ToDateTime(gv_chothue_C.GetRowCellValue(i, "ngaybatdauchothue"));
                        dr["ngayketthucchothue"] = Convert.ToDateTime(gv_chothue_C.GetRowCellValue(i, "ngayketthucchothue"));
                        dr["chothueghichu"] = gv_chothue_C.GetRowCellValue(i, "chothueghichu").ToString();
                        dtdetailbaotri.Rows.Add(dr);
                    }
                    APCoreProcess.APCoreProcess.Save(dr);
                    dtdetailbaotri = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where idplan='" + txt_manhiemvu_IK1.Text + "'");
                    gct_chothue_C.DataSource = dtdetailbaotri;
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void LoadInfo()
        {
            try
            {
                this.Focus();
                if (_insert == true)
                {
                    txt_manhiemvu_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_manhiemvu_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                    dte_ngaygiaohang_I5.EditValue = null;
                    dte_ngaygiaohangdudinh_I5.EditValue = null;
                    dte_ngaynghiemthunoibo_I5.EditValue = null;

                    //Function.clsFunction.Data_XoaText(this, txt_manhiemvu_IK1);
                    
                    chk_status_I6.Checked = true;
                    txt_sogiodudinhthuchien_I8.Text = "0";
                    txt_songaythuctethuchien_I8.Text = "0";
                    glue_idloaibaotri_I1.Enabled = false;
                    gct_hangmuc_baotri_C.Enabled = false;
                }
                else
                {
                    txt_manhiemvu_IK1.Text = ID;
                    Function.clsFunction.Data_Binding1(this, txt_manhiemvu_IK1);
                    glue_idloaibaotri_I1.Enabled = true;
                    gct_hangmuc_baotri_C.Enabled = true;
                }
                txt_idquotation_I1.Text = idquotation;

            }
            catch (Exception ex)

            { }
        }

        private bool checkInput()
        {
            dxEp_error_S.ClearErrors();

            if (!chk_duyet_I6.Checked && glue_idplanstatus_I1.EditValue.ToString() != "PS000001")
            {
                glue_idplanstatus_I1.Focus();
                dxEp_error_S.SetError(glue_idplanstatus_I1, Function.clsFunction.transLateText("Quản lý đơn hàng chưa xét duyệt"));
                return false;
            }

            if (glue_idcustomer_I1.Text == "")
            {
                glue_idcustomer_I1.Focus();
                dxEp_error_S.SetError(glue_idcustomer_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }

            if (txt_idcommodity_I1.Text == "")
            {
                glue_idcustomer_I1.Focus();
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn 1 sản phẩm");
                return false;
            }
            
            if (dte_ngaytao_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaytao_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaytao_I5.Focus();
                return false;
            }
            if (dte_ngaygiaohangdudinh_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaygiaohangdudinh_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaygiaohangdudinh_I5.Focus();
                return false;
            }

           

            if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
            {
                if (dte_ngaynghiemthunoibo_I5.Text == "")
                {
                    dxEp_error_S.SetError(dte_ngaynghiemthunoibo_I5, Function.clsFunction.transLateText("Không được rỗng"));
                    dte_ngaynghiemthunoibo_I5.Focus();
                    return false;
                }

                if (dte_ngaygiaohang_I5.Text == "")
                {
                    dxEp_error_S.SetError(dte_ngaygiaohang_I5, Function.clsFunction.transLateText("Không được rỗng"));
                    dte_ngaygiaohang_I5.Focus();
                    return false;
                }

                if (clsFunction.ConVertDatetimeToNumeric((DateTime)(dte_ngaygiaohang_I5.EditValue)) < clsFunction.ConVertDatetimeToNumeric((DateTime)Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)))
                {
                    dxEp_error_S.SetError(dte_ngaynhanviec_I5, Function.clsFunction.transLateText("Ngày nghiệm thu nội bộ phải lớn hơn ngày bắt đầu"));
                    dte_ngaynhanviec_I5.Focus();
                    return false;
                }

                if (clsFunction.ConVertDatetimeToNumeric((DateTime)(dte_ngaynghiemthunoibo_I5.EditValue)) > clsFunction.ConVertDatetimeToNumeric((DateTime)Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue)))
                {
                    dxEp_error_S.SetError(dte_ngaynghiemthunoibo_I5, Function.clsFunction.transLateText("Ngày nghiệm thu nội bộ phải nhỏ hơn hoặc bằng ngày biên bản bàn giao khách hàng"));
                    dte_ngaynghiemthunoibo_I5.Focus();
                    return false;
                }
            }
            else
            {

                if (dte_ngaynhanviec_I5.Text != "")
                {
                    dxEp_error_S.SetError(dte_ngaynhanviec_I5, Function.clsFunction.transLateText("Chỉ dành cho quản lý đơn hàng"));
                    dte_ngaynhanviec_I5.Focus();
                    return false;
                }

               

                if (dte_ngaygiaohang_I5.Text != "")
                {
                    dxEp_error_S.SetError(dte_ngaygiaohang_I5, Function.clsFunction.transLateText("Chỉ dành cho quản lý đơn hàng"));
                    dte_ngaygiaohang_I5.Focus();
                    return false;
                }


            }
            if (txt_idquotation_I1.Text == "")
            {
                dxEp_error_S.SetError(txt_idquotation_I1, Function.clsFunction.transLateText("Không được rỗng"));
                txt_idquotation_I1.Focus();
                return false;
            }
            
            return true;
        }

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Nhân viên", "Tel", "Fax", "Địa chỉ" };
                string[] fieldname = new string[] { "idcustomer", "customer", "staffname", "tel", "fax", "address" };
                string[] col_visible = new string[] { "True", "True", "True", "False", "False", "False" };
                String dkManager = "";
                if (clsFunction.checkIsmanager(clsFunction.GetIDEMPByUser()))
                {
                    dkManager = " or 1=1 ";
                }

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, EM.StaffName as staffname, C.tel, C.fax, C.address FROM   dbo.DMCUSTOMERS AS C INNER JOIN  dbo.EMPCUS AS E ON C.idcustomer = E.idcustomer  INNER JOIN EMPLOYEES EM on EM.IDEMP=E.IDEMP AND (charindex('" + clsFunction.GetIDEMPByUser() + "',EM.idrecursive) >0 " + dkManager + ") AND E.status='True' ORDER BY C.idcustomer", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 2, col_visible);

            }
            catch { }
        }

        private void loadGridLookupPlanType()
        {
            string[] caption = new string[] { "Mã", "Loại kế hoạch" };
            string[] fieldname = new string[] { "idplantype", "plantype" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idplantype_I1, "select idplantype, plantype from dmplantype", "plantype", "idplantype", caption, fieldname, this.Name, glue_idplantype_I1.Width);
        }

        private void loadGridLookupPlanStatus()
        {
            string[] caption = new string[] { "Mã", "Tình trạng" };
            string[] fieldname = new string[] { "idplanstatus", "planstatus" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idplanstatus_I1, "select idplanstatus, planstatus from dmplanstatus where CHARINDEX ('"+ glue_idplantype_I1.EditValue.ToString() +"' ,idplantype) >0 ", "planstatus", "idplanstatus", caption, fieldname, this.Name, glue_idplanstatus_I1.Width);
        }

        private void loadGridLookupKhachHang()
        {
            try
            {
                string[] caption = new string[] { "Mã KH", "Tên KH", "Mã số thuế" };
                string[] fieldname = new string[] { "idcustomer", "customer", "tax" };
                string[] col_visible = new string[] { "True", "True", "True"};

                ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "SELECT    C.idcustomer, C.customer, C.tax FROM   dbo.NHIEMVU AS C WHERE status =1", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width * 1, col_visible);

            }
            catch { }
        }

        

        private void loadGridLookupQuanly()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienphutrach_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienphutrach_I1.Width);
                glue_manhanvienphutrach_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }

        private void loadGridLookupThucHien()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienthuchien_I1, "select IDEMP, StaffName from EMPLOYEES where status =1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienthuchien_I1.Width);
                glue_manhanvienthuchien_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }

        private void loadGridLookupLoaiBaoTri()
        {
            try
            {
                string[] caption = new string[] { "Mã Loại", "Loại bảo trì" };
                string[] fieldname = new string[] { "idloaibaotri", "loaibaotri" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idloaibaotri_I1, "select idloaibaotri, loaibaotri from DMLOAIBAOTRI", "loaibaotri", "idloaibaotri", caption, fieldname, this.Name, glue_idloaibaotri_I1.Width);
            }
            catch { }
        }

        private void loadGridLookupPriority()
        {
            try
            {
                string[] caption = new string[] { "Mã UT", "Ưu tiên" };
                string[] fieldname = new string[] { "idpriority", "priority" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idpriority_I1, "select idpriority, priority from DMPRIORITY", "priority", "idpriority", caption, fieldname, this.Name, glue_idpriority_I1.Width);
            }
            catch { }
        }

        #endregion   

        private void dte_ngaynhanviec_I5_EditValueChanged(object sender, EventArgs e)
        {
            checkNgayDuDinh();
        }

        private void dte_ngaygiaohangdudinh_I5_EditValueChanged(object sender, EventArgs e)
        {
            if (spe_sothangchothue_I4.Value != 0)
            {
                txt_sothangchothue_I4_EditValueChanged(sender,e);
            }

            if (spe_solanbaotri_I4.Value != 0 && spe_sothangbaotri_I4.Value !=0)
            {
                txt_solanbaotri_I4_EditValueChanged(sender, e);
            }
        }

        private void checkNgayDuDinh()
        {
            //if (dte_ngaytao_I5.EditValue != null && dte_ngaygiaohangdudinh_I5.EditValue != null)
            //{
            //    if (Convert.ToDateTime(dte_ngaytao_I5.EditValue) > Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))
            //    {
            //        clsFunction.MessageInfo("Thông báo", "Ngày dự định phải lớn hơn ngày nhận việc");
            //    }
            //    else {
            //        cal_sogiodudinhthuchien_10_I4.Value = ((TimeSpan)(Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue) - Convert.ToDateTime(dte_ngaytao_I5.EditValue))).Days;
            //    }
               
            //}
        }

        private void dte_ngaynghiemthunoibo_I5_EditValueChanged(object sender, EventArgs e)
        {
            //checkNgayThucte();
        }

        private void dte_ngaygiaohang_I5_EditValueChanged(object sender, EventArgs e)
        {
            dte_ngaynghiemthunoibo_I5.EditValue = dte_ngaygiaohang_I5.EditValue;
            //try
            //{
            //    checkNgayThucte();
            //    int hours = 0;
            //    int day = 0;
            //    int minute = 0;
            //    float dayDiff = 0;
            //    checkNgayDuDinh();
            //    if (dte_ngaygiaohang_I5.EditValue.ToString() != "" && dte_ngaynhanviec_I5.EditValue.ToString() != "")
            //    {
            //        hours = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Hours;
            //        day = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Days;
            //        minute = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).Subtract(Convert.ToDateTime(dte_ngaynhanviec_I5.EditValue)).Minutes;
            //        dayDiff = day * 8 + ((hours > 4) ? hours - 1 : hours) + (float)minute / 60;
            //        txt_songaythuctethuchien_I8.Text = dayDiff.ToString("F1");
            //    }
            //}
            //catch (Exception ex) { }
        }

        private void checkNgayThucte()
        {
            //if (dte_ngaygiaohangdudinh_I5.EditValue != null && dte_ngaygiaohang_I5.EditValue != null)
            //{
            //    if (Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue) > Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))
            //    {
            //        clsFunction.MessageInfo("Thông báo", "Ngày giao hàng phải lớn hơn ngày nghiệm thu");
            //    }
            //    else
            //    {
            //        cal_songaythuctethuchien_10_I4.Value = ((TimeSpan)(Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue) - Convert.ToDateTime(dte_ngaygiaohangdudinh_I5.EditValue))).Days;
            //    }
            //}
        }

        private void loadQuotationInfo()
        {
            if (txt_idquotation_I1.Text == "")
            {
                return;
            }
            string sql = " SELECT T.* FROM (SELECT Q.idcustomer, Q.idemp, Q.datepo,Q.datedelivery from QUOTATION Q where invoiceeps ='" + txt_idquotation_I1.Text + "'";
            sql += " UNION";
            sql += " SELECT S.idcustomer, S.idemp as idemp, S.daterequest as datepo, NULL as datedelivery from SURVEY S where S.idsurvey='" + txt_idquotation_I1.Text + "')  T";
            sql += " INNER JOIN EMPLOYEES E ON T.IDEMP=E.IDEMP where  idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'";

            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
            {
                glue_idcustomer_I1.EditValue = dt.Rows[0]["idcustomer"].ToString();
                glue_manhanvienphutrach_I1.EditValue = dt.Rows[0]["idemp"].ToString();
                ngaytaoPO = Convert.ToDateTime(dt.Rows[0]["datepo"]);
                try
                {
                    dte_ngaygiaohangdudinh_I5.EditValue = Convert.ToDateTime(dt.Rows[0]["datedelivery"]);
                }
                catch {
                    clsFunction.MessageInfo("Thông báo", "Ngày kết thúc dự kiến chưa được nhập, vui lòng bổ sung thêm");
                    dte_ngaygiaohangdudinh_I5.Focus();
                }
                dte_ngaytao_I5.EditValue = ngaytaoPO;
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Số PO/ KS không tồn tại");
            }
        }

        private void txt_idquotation_I1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadQuotationInfo();
            }
        }

        private void glue_idplantype_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadGridLookupPlanStatus();
            if (glue_idplantype_I1.EditValue.ToString() == "PT000002") // cho thue
            {
                tab_chothue_C.PageVisible = true;
                tab_baotri_C.PageVisible = false;
                tab_baohanh_C.PageVisible = false;
            }
            else if (glue_idplantype_I1.EditValue.ToString() == "PT000003")// bao tri
            {
                tab_chothue_C.PageVisible = false;
                tab_baotri_C.PageVisible = true;
                tab_baohanh_C.PageVisible = false;
            }
            else if (glue_idplantype_I1.EditValue.ToString() == "PT000004") // bao hanh
            {
                tab_chothue_C.PageVisible = false;
                tab_baotri_C.PageVisible = false;
                tab_baohanh_C.PageVisible = true;
            }
            else
            {
                tab_chothue_C.PageVisible = false;
                tab_baotri_C.PageVisible = false;
                tab_baohanh_C.PageVisible = false;
            }
        }

        private void loadCommodity()
        {
            try
            {
                DataTable dt = new DataTable();
                if (_insert)
                {
                    dt = APCoreProcess.APCoreProcess.Read(" select D.idcommodity, D.quantity, C.commodity from (select top 1 invoiceeps, idexport from quotation where invoiceeps ='" + txt_idquotation_I1.Text + "' order by idexport desc) As Q INNER JOIN QUOTATIONDETAIL D on Q.idexport = D.idexport INNER JOIN DMCOMMODITY C ON C.idcommodity = D.idcommodity ");
                    dt.Columns.Add("choose", typeof(System.Boolean));
                }
                else
                {
                    dt = APCoreProcess.APCoreProcess.Read(" select D.idcommodity, D.quantity, C.commodity from (select top 1 invoiceeps, idexport from quotation where invoiceeps ='" + txt_idquotation_I1.Text + "' order by idexport desc) As Q INNER JOIN QUOTATIONDETAIL D on Q.idexport = D.idexport INNER JOIN DMCOMMODITY C ON C.idcommodity = D.idcommodity  where D.idcommodity IN ( "+ txt_idcommodity_I1.Text +" )");
                    gv_plan_C.OptionsBehavior.Editable = false;
                    dt.Columns.Add("choose", typeof(System.Boolean));
                    gv_plan_C.Columns["choose"].Visible = false;
                }
                gct_plan_C.DataSource = dt;
                
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void loadChothue()
        {
            try
            {
                DataTable dt = new DataTable();
                if (!_insert)
                {
                    dt = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where idplan ='"+ txt_manhiemvu_IK1.Text +"' ");
                }
                gct_chothue_C.DataSource = dt;
                gct_baotri_C.DataSource = dt;

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void txt_sothangchothue_I4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dte_ngaygiaohang_I5.EditValue == null)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng nhập ngày giao hàng (chỉ dành cho quản lý đơn hàng)");
                    dte_ngaygiaohang_I5.Focus();
                    return;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("idplan", typeof(System.String));
                dt.Columns.Add("kyhanchothue", typeof(System.String));
                dt.Columns.Add("ngaybatdauchothue", typeof(System.DateTime));
                dt.Columns.Add("ngayketthucchothue", typeof(System.DateTime));
                dt.Columns.Add("chothueghichu", typeof(System.String));
                DataRow dr;
                int kyhan = Convert.ToInt32(spe_sothangchothue_I4.Value);
                for (int i = 1; i <= kyhan; i++)
                {
                    dr = dt.NewRow();
                    dr["idplan"] = txt_manhiemvu_IK1.Text;
                    dr["kyhanchothue"] = "Kỳ "+ i;
                    dr["ngaybatdauchothue"] = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).AddMonths(i-1);
                    dr["ngayketthucchothue"] = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).AddMonths(i);
                    dr["chothueghichu"] = "";
                    dt.Rows.Add(dr);
                }
                gct_chothue_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void txt_solanbaotri_I4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dte_ngaygiaohang_I5.EditValue == null)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng nhập ngày giao hàng (chỉ dành cho quản lý đơn hàng)");
                    dte_ngaygiaohang_I5.Focus();
                    return;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("idplan", typeof(System.String));
                dt.Columns.Add("kybaotri", typeof(System.String));
                dt.Columns.Add("ngaybaotri", typeof(System.DateTime));
                dt.Columns.Add("baotrighichu", typeof(System.String));
                DataRow dr;
                int solanbaotri = Convert.ToInt32(spe_solanbaotri_I4.Value);
                int sothang = Convert.ToInt32(spe_sothangbaotri_I4.Value);

                int songaybaotri = 0;
                if (Convert.ToBoolean(rad_optionbaotri_I6.EditValue))
                {
                    songaybaotri = Convert.ToInt32(Convert.ToDouble(sothang) / Convert.ToDouble(((solanbaotri -1 ) + 2)) * 30);
                }
                else
                {
                    songaybaotri = Convert.ToInt32(Convert.ToDouble(sothang) / Convert.ToDouble((solanbaotri + 2)) * 30);
                    solanbaotri -= 1;
                }

                for (int i = 1; i <= solanbaotri; i++)
                {
                    dr = dt.NewRow();
                    dr["idplan"] = txt_manhiemvu_IK1.Text;
                    dr["kybaotri"] = "Kỳ " + i;
                    dr["ngaybaotri"] = Convert.ToDateTime(dte_ngaygiaohang_I5.EditValue).AddDays(i * songaybaotri);
                    dr["baotrighichu"] = "";
                    dt.Rows.Add(dr);
                }
                gct_baotri_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void gv_plan_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // chỉ chọn 1 sản phầm duy nhất
            // TTM, TTC, BTM, BTC
            if (txt_idquotation_I1.Text.Contains("TTM") || txt_idquotation_I1.Text.Contains("TTC") || txt_idquotation_I1.Text.Contains("BTM") || txt_idquotation_I1.Text.Contains("BTC"))
            {
                if (e.Column.FieldName == "choose")
                {
                    if (Convert.ToBoolean(e.Value) == true)
                    {
                        txt_idcommodity_I1.Text = gv_plan_C.GetRowCellValue(e.RowHandle, "idcommodity").ToString();
                        for (int i = 0; i < gv_plan_C.DataRowCount; i++)
                        {
                            if (i != e.RowHandle)
                            {
                                gv_plan_C.SetRowCellValue(i, "choose", false);
                            }
                        }
                    }
                    else
                    {
                        txt_idcommodity_I1.Text = "";
                    }
                }
            }
            else
            { 

            }
        }

        private List< string> getItem()
        {
            List<string>  item = null;
            string str = "";
            for (int i = 0; i < gv_hangmuc_baotri_C.DataRowCount; i++)
            {
                if (Convert.ToBoolean(gv_hangmuc_baotri_C.GetRowCellValue(i, "check")))
                {
                    item.Add( gv_hangmuc_baotri_C.GetRowCellValue(i, "idcommodity").ToString()) ;
                }
            }

            
            return item;
        }

        private void btn_phatsinhPO_S_Click(object sender, EventArgs e)
        {
            SOURCE_FORM_QUOTATION.Presentation.frm_QUOTATION_S frm = new SOURCE_FORM_QUOTATION.Presentation.frm_QUOTATION_S();
            frm.items = getItem();
            frm.idPoOriginal = txt_idquotation_I1.Text;
            frm.idCustomer = glue_idcustomer_I1.EditValue.ToString();
            
            frm.Show();
        }

        private void btn_phatsinh_suvu_S_Click(object sender, EventArgs e)
        {
            SOURCE_FORM_CRM.Presentation.frm_DMPROBLEM_S frm = new SOURCE_FORM_CRM.Presentation.frm_DMPROBLEM_S();
            frm.idquotation = txt_idquotation_I1.Text;
            frm.idplan = txt_manhiemvu_IK1.Text;
            frm._insert = true;
            frm.ShowDialog();
        }

        private void btn_chitiet_baotri_S_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from ";
                DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idloaibaotri_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadBaoTriDetail();
        }

        private void gv_baotri_C_Click(object sender, EventArgs e)
        {
            //loadBaoTriDetail();
        }

        private void loadBaoTriDetail()
        {
            try
            {
                if (gv_baotri_C.FocusedRowHandle < 0)
                {
                    return;
                }
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select B.*,C.commodity,isnull((select 1 from nhiemvu_baotri_detail where idbaotridetail = " + gv_baotri_C.GetRowCellValue(gv_baotri_C.FocusedRowHandle, "iddetail").ToString() + " and idbaotri = B.id),0) as check1 from DMBAOHANH B INNER JOIN DMCOMMODITY C ON B.idcommodity = C.idcommodity  where idloaibaotri = '" + glue_idloaibaotri_I1.EditValue.ToString() + "'");
                dt.Columns.Add("check", typeof(Boolean));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["check1"].ToString() == "1")
                    {
                        dt.Rows[i]["check"] = true;
                    }
                    else
                    {
                        dt.Rows[i]["check"] = false;
                    }
                }
                gct_hangmuc_baotri_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_idquotation_I1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}