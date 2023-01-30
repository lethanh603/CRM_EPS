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
    public partial class frm_TIENDOCONGVIEC_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_TIENDOCONGVIEC_S()
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
        public string _sign = "TD";
        public string ID = "";
        public string manhiemvu = "";
        public string congviecduocgiao = "";

        #endregion

        #region FormEvent

        private void frm_CUSCONTACT_S_Load(object sender, EventArgs e)
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
                loadGridLookupTinhTrang();
                loadGridLookupEmployee();
                loadPercent();
                LoadInfo();
                loadCommodity();
                
            }
            txt_sign_20_I1.Focus();
        }

        private void loadCommodity()
        {
            string sql = "SELECT distinct   Qd.iddetail, QD.idcommodity, C.commodity, QD.quantity,  Q.invoiceeps, isnull((select sum (isnull(soluong,0)) from TIENDOCONGVIECHITIET where matiendo = TD.matiendo and QD.idcommodity = idcommodity),0) AS dagiao, QD.quantity - isnull((select sum (isnull(soluong,0)) from TIENDOCONGVIECHITIET where matiendo = TD.matiendo and QD.idcommodity = idcommodity),0) AS conlai, NHIEMVU.idcommodity FROM       dbo.QUOTATION AS Q INNER JOIN      dbo.QUOTATIONDETAIL AS QD ON Q.idexport = QD.idexport INNER JOIN  dbo.DMCOMMODITY AS C ON QD.idcommodity = C.idcommodity INNER JOIN        dbo.NHIEMVU ON Q.invoiceeps = dbo.NHIEMVU.idquotation LEFT  JOIN  dbo.TIENDOCONGVIEC AS TD ON dbo.NHIEMVU.manhiemvu = TD.manhiemvu  WHERE      (dbo.NHIEMVU.manhiemvu = N'" + txt_manhiemvu_I1.Text + "')";
            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            dt.Columns.Add("qty", typeof(Int16));
            gct_commodity_C.DataSource = dt;
        }

        private void frm_CUSCONTACT_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I1.Focus();
        }

        private void frm_CUSCONTACT_S_KeyDown(object sender, KeyEventArgs e)
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
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                this.Close();
            }
            
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_insert_allow_insert_Click(object sender, EventArgs e)
        {
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                InitData();
                txt_sign_20_I1.Focus();
            }
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
            if (e.KeyCode == Keys.Enter && !sender.GetType().ToString().Contains("MemoEdit")) 
                SendKeys.Send("{Tab}");
        }

        private void glue_idstatustype_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        #endregion

        #region GridEvent

        #endregion

        #region Methods

        private void save()
        {
            try
            {
                if (!checkInput()) return;
                if (_insert == true)
                {
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_matiendo_IK1.Name) + " = '" + txt_matiendo_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_matiendo_IK1.Text, txt_contactname_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_matiendo_IK1.Text);
                    }
                    else
                    {
                        LoadInfo();
                    }
                    passData(true);
                    //this.Hide();  
                    dxEp_error_S.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_matiendo_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_matiendo_IK1.Name) + " = '" + txt_matiendo_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_matiendo_IK1.Text, txt_contactname_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    passData(true);
                }
                // 
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from TIENDOCONGVIECHITIET where matiendo='"+ txt_matiendo_IK1.Text +"'");
                for (int i = 0; i < gv_commodity_C.DataRowCount; i++)
                {
                    int sl = Convert.ToInt16(gv_commodity_C.GetRowCellValue(i, "qty"));
                    if (sl >= 0)
                    {
                        if (APCoreProcess.APCoreProcess.Read("select * from TIENDOCONGVIECHITIET WHERE matiendo=" + txt_matiendo_IK1.Text + " and idcommodity ='" + gv_commodity_C.GetRowCellValue(i, "idcommodity").ToString() + "'").Rows.Count == 0)// insert
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("insert into TIENDOCONGVIECHITIET (matiendo, idcommodity, soluong) values ('" + txt_matiendo_IK1.Text + "', '" + gv_commodity_C.GetRowCellValue(i, "idcommodity").ToString() + "', " + sl + ")");
                        }
                        else
                        {
                            APCoreProcess.APCoreProcess.ExcuteSQL("update TIENDOCONGVIECHITIET set soluong=" + sl + " where matiendo=" + txt_matiendo_IK1.Text + " and idcommodity ='" + gv_commodity_C.GetRowCellValue(i, "idcommodity").ToString() + "'");
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadInfo()
        {
            
            txt_nguoitao_I2.Text = clsFunction.GetIDEMPByUser();
            this.Focus();
            if (_insert==true)
            {
                txt_matiendo_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_matiendo_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_matiendo_IK1);
                chk_status_I6.Checked = true;
                txt_sign_20_I1.Text = txt_matiendo_IK1.Text;
                mmo_lydo_I3.Text = congviecduocgiao;
                txt_sogiothuchien_I8.Text = "0";
                txt_manhiemvu_I1.Text = manhiemvu;
                lue_tiendo_I1.EditValue = 0.1 ;
                glue_manhanvienthuchien_I1.EditValue = clsFunction.GetIDEMPByUser();
            } 
            else
            {
                txt_matiendo_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_matiendo_IK1);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            DataTable dtcheck = APCoreProcess.APCoreProcess.Read("select * from tiendocongviec where manhiemvu='"+ txt_manhiemvu_I1.Text +"' and cast( ngaydudinh as date) >  cast('" + Convert.ToDateTime(dte_ngaytao_I5.EditValue) + "' as date) ");
            if (dtcheck.Rows.Count > 0)
            {
                clsFunction.MessageInfo("Thông báo", "Bạn không thể lùi lại ngày xử lý");
                dxEp_error_S.SetError(dte_ngaytao_I5, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (glue_matinhtrangcongviec_I1.EditValue.ToString() == "TT000005")
            {
                if (!clsFunction.MessageDelete("Thông báo", "Bạn có muốn hủy kế hoạch này"))
                {
                    return false;
                }
            }

            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Bạn không thể lùi lại ngày xử lý"));
                return false;
            }

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from "+ Function.clsFunction.getNameControls(this.Name) +" where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {               

                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from  " + Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_matiendo_IK1.Name) + "='" + txt_matiendo_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from  " + Function.clsFunction.getNameControls(this.Name) + " where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }

                    if (!Function.clsFunction.checkAdmin())
                    {
                        if (txt_nguoitao_I2.Text != clsFunction.GetIDEMPByUser())
                        {
                            clsFunction.MessageInfo("Thông báo","Bạn không thể sửa đỗi dữ liệu của người khác");
                            return false;
                        }
                    }
                }
            }

            if (APCoreProcess.APCoreProcess.Read("select matinhtrangcongviec from tiendocongviec where manhiemvu='"+ txt_manhiemvu_I1.Text +"' and matinhtrangcongviec > '" + glue_matinhtrangcongviec_I1.EditValue.ToString() + "'").Rows.Count > 0)
            {
                glue_matinhtrangcongviec_I1.Focus();
                dxEp_error_S.SetError(glue_matinhtrangcongviec_I1, Function.clsFunction.transLateText("Không thể quay lại tiến trình của kế hoạch"));
                Function.clsFunction.MessageInfo("Thông báo", "Không thể quay lại tiến trình của kế hoạch");
                return false;
            }

            if (txt_sogiothuchien_I8.Text =="")
            {
                dxEp_error_S.SetError(txt_sogiothuchien_I8, Function.clsFunction.transLateText("Không được rỗng"));
                txt_sogiothuchien_I8.Focus();
                return false;
            }

            // check list commodity
            int sl = 0;
            int cl = 0;
            for (int i = 0; i < gv_commodity_C.DataRowCount; i++)
            {
                if (gv_commodity_C.GetRowCellValue(gv_commodity_C.FocusedRowHandle, "qty") == null)
                {
                    sl = 0;
                }
                else
                {
                    sl = Convert.ToInt16(gv_commodity_C.GetRowCellValue(gv_commodity_C.FocusedRowHandle, "qty"));
                }
                cl = Convert.ToInt16(gv_commodity_C.GetRowCellValue(gv_commodity_C.FocusedRowHandle, "conlai"));
                if (sl < 0)
                {
                    clsFunction.MessageInfo("Thông báo","Số lượng phải >0");
                    return false;
                }
                if (sl > cl)
                {
                    clsFunction.MessageInfo("Thông báo", "Số lượng phải < còn lại");
                    return false;
                }
            }

                return true;
        }

        private void loadGridLookupTinhTrang()
        {
            string[] caption = new string[] { "Mã", "Tình trạng" };
            string[] fieldname = new string[] { "matinhtrangcongviec", "tinhtrangcongviec" };
            if (APCoreProcess.APCoreProcess.Read("select matiendo from tiendocongviec where manhiemvu='"+ txt_manhiemvu_I1.Text +"'").Rows.Count==0 || Function.clsFunction.checkAdmin())
            {
                ControlDev.FormatControls.LoadGridLookupEdit(glue_matinhtrangcongviec_I1, "select matinhtrangcongviec, tinhtrangcongviec from dmtinhtrangcongviec order by _index", "tinhtrangcongviec", "matinhtrangcongviec", caption, fieldname, this.Name, glue_matinhtrangcongviec_I1.Width);
            }
            else
            {
                ControlDev.FormatControls.LoadGridLookupEdit(glue_matinhtrangcongviec_I1, "select matinhtrangcongviec, tinhtrangcongviec from dmtinhtrangcongviec where matinhtrangcongviec <> 'TT000001' order by _index", "tinhtrangcongviec", "matinhtrangcongviec", caption, fieldname, this.Name, glue_matinhtrangcongviec_I1.Width);
            }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienthuchien_I1, "select IDEMP, StaffName from EMPLOYEES WHERE STATUS=1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienthuchien_I1.Width);
                glue_manhanvienthuchien_I1.EditValue = Function.clsFunction.GetIDEMPByUser();
            }
            catch { }
        }

        private void loadPercent()
        {
            lue_tiendo_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("SELECT value, display from sysPercent order by value");
        }

        private void callForm()
        {
            SOURCE_FORM_DMPOSITION.Presentation.frm_DMPOSITION_S frm = new SOURCE_FORM_DMPOSITION.Presentation.frm_DMPOSITION_S();
            frm.passData = new SOURCE_FORM_DMPOSITION.Presentation.frm_DMPOSITION_S.PassData(getValue);
            frm.strpassData = new SOURCE_FORM_DMPOSITION.Presentation.frm_DMPOSITION_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "CV";
            frm.ShowDialog();
        }

        private void getValue(bool value)
        {
            if (value == true)
            {

            }
        }

        private void getStringValue(string value)
        {
            if (value != "")
            {
                glue_matinhtrangcongviec_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idposition,position from dmposition where status=1");
                glue_matinhtrangcongviec_I1.EditValue = value;
            }
        }   
        
        #endregion    

        private void glue_matinhtrangcongviec_I1_EditValueChanged(object sender, EventArgs e)
        {
            if (glue_matinhtrangcongviec_I1.EditValue.ToString() == "TT000004")
            {
                lue_tiendo_I1.EditValue = 1;
            }
        }

        private void gv_commodity_C_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void gv_commodity_C_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        private void gv_commodity_C_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            object sl = e.Value;
            int conlai = 0;
            conlai = Convert.ToInt16(gv_commodity_C.GetRowCellValue(gv_commodity_C.FocusedRowHandle, "conlai"));

            if (view.FocusedColumn.FieldName == "qty")
            {
                if (sl == null || string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "Vui lòng nhập số";
                }
                if (Convert.ToInt16( sl) < 0)
                {
                    clsFunction.MessageInfo("Thông báo", "Số lượng giao hàng phải lớn hơn 0");

                }
                if (Convert.ToInt16( sl) > conlai)
                {
                    clsFunction.MessageInfo("Thông báo", "Số lượng giao hàng lớn hơn số lượng còn lại");
                }
            }
 
        }

        private void gv_commodity_C_ShownEditor(object sender, EventArgs e)
        {
            GridView view = sender as GridView;

            view.ActiveEditor.IsModified = true;
        }
        
    }
}