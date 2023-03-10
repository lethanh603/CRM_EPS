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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_PLANGIAOHANG_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_PLANGIAOHANG_S()
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
        PopupMenu menu = new PopupMenu();
        public string _sign = "HH";
        public string ID = "";
        public string idquotation = "";
        DateTime ngaytaoPO = new DateTime();
        public string idplanType = "";
        private int row_focus = -1;
        public string idexport = "";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            try
            {
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
                    loadGridLookupPlanStatus();
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

                glue_idplantype_I1.EditValue = idplanType;
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
                loadDetail();

                if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
                {
                    bbi_allow_insert.Enabled = true;
                    btn_delete_detail.Enabled = true;
                    gv_detail_C.OptionsBehavior.Editable = true;
                }
                else
                {
                    bbi_allow_insert.Enabled = false;
                    gv_detail_C.OptionsBehavior.Editable = false;
                    btn_delete_detail.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo","Error " + ex.Message);
            }
        }

        private void loadDetail()
        {
            // load kyhan
            DataTable dtkh = APCoreProcess.APCoreProcess.Read("select * from nhiemvukyhan where idplan='" + txt_manhiemvu_IK1.Text + "'");


            gct_plan_C.DataSource = dtkh;
            if (gv_plan_C.FocusedRowHandle >= 0)
            {
                {
                    gv_plan_C.FocusedRowHandle = 0;
                }
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
            savePlan();
            if(!dxEp_error_S.HasErrors)
                clsFunction.MessageInfo("Thông báo", "Lưu thành công !!!");
            
        }

        private bool savePlan()
        {
            if (!checkInput()) return false;
            if (chk_duyet_I6.Checked)
            {
                if (clsFunction.MessageDelete("Thông báo", "Quản lý đơn hàng đã duyệt, không thể chỉnh sửa thông tin kế hoạch \n Bạn chỉ có thể thay đỗi tình trạng kế hoạch \n Bạn có chắc muốn cập nhật tình trạng kế hoạch không ?"))
                {
                    // update status
                    APCoreProcess.APCoreProcess.ExcuteSQL("update nhiemvu set idplantype='" + glue_idplantype_I1.EditValue.ToString() + "' where manhiemvu='" + txt_manhiemvu_IK1.Text + "'");
                    clsFunction.MessageInfo("Thông báo", "Cập nhật trạng thái thành công");
                }

                return false;
            }
            save();
          
            
            if (dxEp_error_S.HasErrors == false)
            {
                //glue_idloaibaotri_I1.Enabled = true;
                //gct_hangmuc_baotri_C.Enabled = true;
                //this.Close();
            }
            return true;
            // 0317
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
                   
                    
                    dxEp_error_S.ClearErrors();
                    passData(true);
                }
                
            }
            catch
            {
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
                }
                else
                {
                    txt_manhiemvu_IK1.Text = ID;
                    Function.clsFunction.Data_Binding1(this, txt_manhiemvu_IK1);
                }
                txt_idquotation_I1.Text = idquotation;

            }
            catch (Exception ex)

            { }
        }

        private bool checkInput()
        {
            dxEp_error_S.ClearErrors();
            if (glue_idcustomer_I1.Text == "")
            {
                glue_idcustomer_I1.Focus();
                dxEp_error_S.SetError(glue_idcustomer_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }

            if (dte_ngaytao_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaytao_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaytao_I5.Focus();
                return false;
            }

            if (dte_ngaynhanviec_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaynhanviec_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaynhanviec_I5.Focus();
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
                /*if (dte_ngaynghiemthunoibo_I5.Text == "")
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
                }*/
                if (clsFunction.ConVertDatetimeToNumeric((DateTime)(dte_ngaynhanviec_I5.EditValue)) < clsFunction.ConVertDatetimeToNumeric((DateTime)Convert.ToDateTime(dte_ngaytao_I5.EditValue)))
                {
                    dxEp_error_S.SetError(dte_ngaynhanviec_I5, Function.clsFunction.transLateText("Ngày ra xác nhận công việc phải lớn hơn ngày ra PO"));
                    dte_ngaynhanviec_I5.Focus();
                    return false;
                }
                if (dte_ngaygiaohang_I5.Text != "" && dte_ngaynhanviec_I5.Text != "" && dte_ngaynghiemthunoibo_I5.Text != "")
                {
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
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idplantype_I1, "select idplantype, plantype from dmplantype where status=1", "plantype", "idplantype", caption, fieldname, this.Name, glue_idplantype_I1.Width);
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
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienthuchien_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienthuchien_I1.Width);
                glue_manhanvienthuchien_I1.EditValue = clsFunction.GetIDEMPByUser();

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
            string sql = " SELECT T.* FROM (SELECT Q.idcustomer, Q.idemp, Q.datepo,Q.datedelivery from QUOTATION Q where idexport ='" + idexport + "'";
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
                //clsFunction.MessageInfo("Thông báo", "Số PO/ KS không tồn tại");
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

            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", ex.Message);
            }

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
            
        }

        private void glue_idcustomer_I1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_idquotation_I1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gv_plan_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_plan_C.FocusedRowHandle >= 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                flag = true;
                if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && flag == true)
                {
                    clsFunction.customPopupMenu(bar_kehoach_C, menu, gv_plan_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_ItemPress_Plan);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch
            { }
        }

        private void Manager_ItemPress_Plan(object sender, ItemClickEventArgs e)
        {
            string strColVisible = "";
            int pos = -1;
            try
            {
                if (gv_plan_C.DataRowCount > 0 || 1==1)
                {
                    string[] arrayColName = new string[gv_plan_C.Columns.Count];
                    string[] arrCol = { };
                    int i = 0;
                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((ColumnView)gv_plan_C).Columns)
                    {
                        arrayColName[i] = col.Name;
                        i++;
                    }

                    if ( (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos = clsFunction.findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]))
                        {
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible = clsFunction.ConvertArrayToString(arrCol);
                            //APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_listProlem_C.Name + "'");
                            gv_plan_C.RefreshData();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(strColVisible);
            }
        }

        private void bar_kehoach_C_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void bbi_allow_insert_kehoach_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
                {
                    
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo","Chỉ dành cho quản lý đơn hàng");
                    return;
                }

                if (glue_idplanstatus_I1.EditValue.ToString().Equals("PS000001"))
                {
                    clsFunction.MessageInfo("Thông báo", "Kế hoạch đang ở tình trạng khởi tạo");
                    glue_idplanstatus_I1.Focus();
                    return;
                }

                if (dte_ngaynhanviec_I5.EditValue == null)
                {
                    clsFunction.MessageInfo("Thông báo", "Phải nhập ngày xác nhận công việc");
                    dte_ngaynhanviec_I5.Focus();
                    return;
                }

                if (savePlan() == false)
                {
                    clsFunction.MessageInfo("Thông báo", "Lưu không thành công");
                    return;
                }

                if (txt_idquotation_I1.Text =="")
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn PO");
                    return;
                }
                string idexport = "";
                DataTable dt = APCoreProcess.APCoreProcess.Read("select top 1 manhiemvu from plangiaohang order by manhiemvu desc");
                if (dt.Rows.Count > 0)
                {
                    txt_manhiemvu_IK1.Text = dt.Rows[0][0].ToString();
                    DataTable dt1 = APCoreProcess.APCoreProcess.Read("select idexport from quotation where invoiceeps='"+ idquotation +"'");
                    if (dt1.Rows.Count > 0)
                        idexport = dt1.Rows[0][0].ToString();
                    else
                    {
                        clsFunction.MessageInfo("Thông báo","Lỗi không tìm thấy báo giá");
                        return;
                    }
                }



                DataTable dtcheck = APCoreProcess.APCoreProcess.Read("with tem as (select d.iddetail, d.idcommodity, sum( d.quantity) as quantity from QUOTATIONDETAIL d where d.idexport = '" + idexport + "' group by d.iddetail, d.idcommodity ) select tem.iddetail, tem.idcommodity, tem.quantity - isnull((select sum(soluong) from NHIEMVU_DETAIL where manhiemvu = '" + txt_manhiemvu_IK1.Text + "' and iddetail = tem.iddetail group by iddetail),0) as quantity from tem where tem.quantity - isnull((select sum(soluong) from NHIEMVU_DETAIL where manhiemvu = '" + txt_manhiemvu_IK1.Text + "' and iddetail = tem.iddetail group by iddetail),0) >0 ");
                if (dtcheck.Rows.Count==0)
                {
                    clsFunction.MessageInfo("Thông báo", "Số lượng đã giao hết");
                    return;
                }

                DataTable dtcheck2 = APCoreProcess.APCoreProcess.Read(" select * from NHIEMVUKYHAN where idplan = '"+ txt_manhiemvu_IK1.Text +"' and idkyhan not in (select idkyhan from NHIEMVU_DETAIL)");
                if (dtcheck2.Rows.Count >0)
                {
                    clsFunction.MessageInfo("Thông báo", "Kỳ hạn " + dtcheck2.Rows[0]["kyhan"].ToString() + " đã chưa được thiết lập");
                    return;
                }

                menu.HidePopup();
                frm_NHIEMVUKYHAN_S frm = new frm_NHIEMVUKYHAN_S();
                frm._insert = true;
                frm._sign = "KH";
                frm.idplan = txt_manhiemvu_IK1.Text;
                frm.plan_type = "GiaoHang";
                frm.statusForm = statusForm;
                frm.passData = new frm_NHIEMVUKYHAN_S.PassData(getKyhan);
                frm.strpassData = new frm_NHIEMVUKYHAN_S.strPassData(getKyhanByval);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void bbi_allow_edit_kehoach_S_ItemClick(object sender, ItemClickEventArgs e)
        {
             try
            {
                if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
                {

                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Chỉ dành cho quản lý đơn hàng");
                    return;
                }
                if (dte_ngaynhanviec_I5.EditValue == null)
                {
                    clsFunction.MessageInfo("Thông báo", "Phải nhập ngày xác nhận công việc");
                    dte_ngaynhanviec_I5.Focus();
                    return;
                }
                if (gv_plan_C.FocusedRowHandle < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Vui lòng chọn plan");
                    return;
                }
                menu.HidePopup();
                frm_NHIEMVUKYHAN_S frm = new frm_NHIEMVUKYHAN_S();
                frm._insert = false;
                frm.plan_type = "GiaoHang";
                frm._sign = "KH";
                frm.idplan = txt_manhiemvu_IK1.Text;
                frm.ID = gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString();

                frm.statusForm = statusForm;
                //frm.passData = new frm_NHIEMVUKYHAN_S.PassData(getKyhan);
                frm.strpassData = new frm_NHIEMVUKYHAN_S.strPassData(getKyhanByval);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //Function.clsFunction.MessageInfo("Thông báo", "Lỗi thực thi Error: " + ex.Message);
            }
        }

        private void getKyhan(bool val)
        {
            if (val == true)
            {
                loadDetail();
                gv_plan_C.LocateByValue("idkyhan", 1);
            }
        }
        private void getKyhanByval(string val)
        {
            if (val != "")
            {
                loadDetail();
                //gv_plan_C.LocateByValue("idkyhan", val);
                // gv_plan_C.SelectRow(1);
                //gv_plan_C.SetFocusedRowCellValue(gv_plan_C.Columns["idkyhan"], val);
                if(val == "new")
                    gv_plan_C.FocusedRowHandle = gv_plan_C.DataRowCount - 1;
            }
        }

        private void bbi_allow_delete_kehoach_S_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gv_plan_C.FocusedRowHandle >= 0)
            {
                if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
                {

                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Chỉ dành cho quản lý đơn hàng");
                    return;
                }
                if (APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where manhiemvu ='" + txt_manhiemvu_IK1.Text + "' and idkyhan='"+ gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle,"idkyhan").ToString() +"'").Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Thông báo","Kỳ hạn này đã sản phẩm, không được xóa");
                    return;
                }

                if (clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa kỳ hạn này"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from nhiemvukyhan where idkyhan='" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "'");
                    loadDetail();
                }
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn 1 kỳ");
            }
        }

        private void gv_plan_C_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_delete_detail_Click(object sender, EventArgs e)
        {
            if (gv_detail_C.FocusedRowHandle >= 0)
            {
                if (clsFunction.checkIsmanagerPo(clsFunction.GetIDEMPByUser()))
                {

                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Chỉ dành cho quản lý đơn hàng");
                    return;
                }
                if (clsFunction.MessageDelete("Thông báo", "Bạn có chắc muốn xóa sản phầm này"))
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("delete from nhiemvu_detail where idkyhan = '" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "' and iddetail = '" + gv_detail_C.GetRowCellValue(gv_detail_C.FocusedRowHandle, "iddetail").ToString() + "'");


                    try
                    {
                        //DataTable dt = APCoreProcess.APCoreProcess.Read("select mahang, soluong, commodity, id from nhiemvu_detail d inner join dmcommodity c on d.mahang=c.idcommodity where idkyhan='" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "' ");
                        //gct_detail_C.DataSource = dt;
                        gv_plan_C_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        clsFunction.MessageInfo("Thông báo", "Error " + ex.Message);
                    }
                }
            }
            else
            {
                clsFunction.MessageInfo("Thông báo", "Vui lòng chọn 1 sản phẩm");
            }
        }

        private void btn_detail_insert_Click(object sender, EventArgs e)
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read("select idexport from quotation where invoiceeps = '"+ idquotation +"'");
            if (dt.Rows.Count > 0)
            {
                frm_listCommodity frm = new frm_listCommodity();
                frm.idexport = dt.Rows[0][0].ToString();
                frm.idplantype = idplanType;
                frm.manhiemvu = txt_manhiemvu_IK1.Text;
                frm.strpassData = new frm_listCommodity.StrPassData(getItem);
                frm.ShowDialog();
            }
        }

        private void getItem(string item, string iddetail, int soluong)
        {
            try
            {
                if (item != "")
                {
                    frmChiTietGiaohang frm = new frmChiTietGiaohang();
                    frm.soluonggoc = soluong;
                    frm.item = item;
                    frm.iddetail = iddetail;
                    frm.arrpassData = new frmChiTietGiaohang.arrPassData(getValueQty);
                    frm.ShowDialog();

                    
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error "+ ex.Message);
            }
        }

        private void getValueQty(string item, string iddetail,int soluong,DateTime tungay, DateTime denngay)
        {
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from nhiemvu_detail where manhiemvu='" + txt_manhiemvu_IK1.Text + "'");
            DataRow dr = dt.NewRow();
            dr["manhiemvu"] = txt_manhiemvu_IK1.Text;
            dr["idkyhan"] = gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString();
            dr["mahang"] = item;
            dr["iddetail"] = iddetail;
            dr["tungay"] = tungay;
            dr["denngay"] = denngay;
            dr["soluong"] = soluong;
            dt.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
            loadDetail();
            if (gv_plan_C.FocusedRowHandle > 0)
            {
                gv_plan_C.FocusedRowHandle = gv_plan_C.FocusedRowHandle - 1;
                gv_plan_C.FocusedRowHandle = gv_plan_C.FocusedRowHandle + 1;
            }
            else
            {
                gv_plan_C.FocusedRowHandle = 0;
            }
        }

        private void gv_plan_C_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //gv_plan_C_Click(sender,e);
        }

        private void gv_detail_C_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "dagiao")
            {
                if (Convert.ToInt16( e.Value) < 1)
                {
                    clsFunction.MessageInfo("Thông báo","Số lượng giao phải >0");
                    return;
                }
                else if (Convert.ToInt16(e.Value) > Convert.ToInt16(gv_detail_C.GetRowCellValue(e.RowHandle, "conlai1")))
                {
                    clsFunction.MessageInfo("Thông báo", "Số lượng giao phải <= số lượng còn lại");
                    return;
                }
                // insert or update
                string sql = "if (select count(*) from NHIEMVU_DETAIL where mahang='' and manhiemvu='' and idkyhan = '') > 0 ";
                        sql += " begin";
                        sql += "    update NHIEMVU_DETAIL set soluong = " + Convert.ToInt16(gv_detail_C.GetRowCellValue(e.RowHandle, "conlai")) + " where mahang='" + gv_detail_C.GetRowCellValue(e.RowHandle, "mahang").ToString() + "' and manhiemvu='" + txt_manhiemvu_IK1.Text + "' and idkyhan = '" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "'";
                        sql += " end";
                        sql += " begin";
	                    sql += "    insert into NHIEMVU_DETAIL (mahang, manhiemvu, iddetail, soluong, idloaibaotri, idkyhan)";
                        sql += "    values ('" + gv_detail_C.GetRowCellValue(e.RowHandle, "mahang").ToString() + "', '" + txt_manhiemvu_IK1.Text + "', '" + gv_detail_C.GetRowCellValue(e.RowHandle, "iddetail").ToString() + "', "+ e.Value +",'','"+ gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle,"idkyhan").ToString() +"')";
                        sql += " end";
                APCoreProcess.APCoreProcess.ExcuteSQL(sql);
                gv_detail_C.SetRowCellValue(gv_detail_C.FocusedRowHandle, "conlai1", (Convert.ToInt16(gv_detail_C.GetRowCellValue(e.RowHandle, "conlai")) - Convert.ToInt16(e.Value)));
            }

        }

        private void gv_plan_C_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_plan_C.FocusedRowHandle >= 0)
                {
                    DataTable dt = APCoreProcess.APCoreProcess.Read("select iddetail, D.idcommodity mahang,C.commodity, D.quantity soluong,  isnull(( select sum(isnull(soluong,0)) from NHIEMVU_DETAIL where idkyhan='" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "' and  manhiemvu = '" + txt_manhiemvu_IK1.Text + "' and iddetail=D.iddetail ),0) as dagiao1, D.quantity - isnull(( select sum(isnull(soluong,0)) from NHIEMVU_DETAIL where  manhiemvu = '" + txt_manhiemvu_IK1.Text + "' and iddetail=D.iddetail and idkyhan  < '" + gv_plan_C.GetRowCellValue(gv_plan_C.FocusedRowHandle, "idkyhan").ToString() + "' ),0) as conlai from ((SELECT iddetail, idcommodity, idexport, status, sum(quantity) as quantity FROM QUOTATIONDETAIL GROUP BY iddetail, idcommodity, idexport, status )) D INNER JOIN DMCOMMODITY C ON D.idcommodity = C.idcommodity INNER JOIN QUOTATION Q ON Q.idexport = D.idexport   WHERE Q.idexport  = '" + idexport + "' and D.status = 1 ");
                    dt.Columns.Add("dagiao", typeof(int));
                    dt.Columns.Add("conlai1", typeof(int));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["dagiao"] = Convert.ToInt16(dt.Rows[i]["dagiao1"]);
                        dt.Rows[i]["conlai1"] = Convert.ToInt16(dt.Rows[i]["conlai"]) - Convert.ToInt16(dt.Rows[i]["dagiao1"]);

                    }
                    gct_detail_C.DataSource = dt;
                    if (gv_plan_C.FocusedRowHandle == gv_plan_C.RowCount - 1)
                    {
                        gv_detail_C.OptionsBehavior.Editable = true;
                        gv_detail_C.Columns["dagiao"].AppearanceCell.BackColor = Color.White;
                        btn_delete_detail.Enabled = true;
                    }
                    else
                    {
                        gv_detail_C.OptionsBehavior.Editable = false;
                        gv_detail_C.Columns["dagiao"].AppearanceCell.BackColor = Color.Gray;
                        btn_delete_detail.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                //clsFunction.MessageInfo("Thông báo", "Error " + ex.Message);
            }
        }

        private void glue_idplanstatus_I1_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}