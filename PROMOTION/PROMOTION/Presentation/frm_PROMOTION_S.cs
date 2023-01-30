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
using DevExpress.XtraGrid.Views.Grid;

namespace SOURCE_FORM_PROMOTION.Presentation
{
    public partial class frm_PROMOTION_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_PROMOTION_S()
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
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
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
                chk_isunlimit_I6.Checked = true;
                chk_isunlimit_I6.Checked = false;
                loadGridLookupEmployees();                
                LoadInfo();
                loadGridLeft();
            }
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(gv_list_C.Columns["idcommodity"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity,commodity from dmcommodity ");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(gv_listadd_C.Columns["idcommodity"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idcommodity,commodity from dmcommodity ");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(gv_list_C.Columns["idkind"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idkind,kind from dmkind ");
            ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)(gv_listadd_C.Columns["idkind"].ColumnEdit)).DataSource = APCoreProcess.APCoreProcess.Read("select idkind,kind from dmkind ");
            chk_status_I6_CheckedChanged( sender,  e);
            chk_istimegold_I6_CheckedChanged(sender, e);
            chk_amountdiscount_I6_CheckedChanged(sender, e);
            chk_percentdiscount_I4_CheckedChanged(sender, e);
            txt_sign_20_I1.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            try
            {
                //txt_sign_20_I1.Focus();
            }
            catch { }
        }

        private void frm_DMAREA_S_KeyDown(object sender, KeyEventArgs e)
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

        private void btn_removeall_S_Click(object sender, EventArgs e)
        {
            removeAll();
        }

        private void btn_remove_S_Click(object sender, EventArgs e)
        {
            if (gv_listadd_C.FocusedRowHandle >= 0)
            {
                removeOne();
            }
            else
            {
                removeGroup(gv_listadd_C.GetGroupRowValue(gv_listadd_C.FocusedRowHandle, gcGroupR).ToString());
            }
        }

        private void btn_add_S_Click(object sender, EventArgs e)
        {
            if (gv_list_C.FocusedRowHandle >= 0)
            {
                addOne();
            }
            else
            {
                addGroup(gv_list_C.GetGroupRowValue(gv_list_C.FocusedRowHandle, gcGroupA).ToString());
            }
        }

        private void btn_addall_S_Click(object sender, EventArgs e)
        {
            addAll();
        }        

        #endregion
        
        #region Event

        // enter next control
        
        private void txt_sign_I2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void chk_isunlimit_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isunlimit_I6.Checked == false)
            {
                dte_todate_I5.Enabled = true;
            }
            else
            {
                dte_todate_I5.Enabled = false;
            }
        }

        private void chk_percentdiscount_I4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ispercentdiscount_I6.Checked == true)
            {
                txt_amoundiscount_I4.Enabled = false;
                txt_percentdiscount_I4.Enabled = true;
                chk_isamountdiscount_I6.Checked = false;
                chk_ispercentdiscount_I6.Checked = true;
                txt_percentdiscount_I4.Focus();
            }
            else
            {
                txt_amoundiscount_I4.Enabled = true;
                txt_percentdiscount_I4.Enabled = true;
                chk_ispercentdiscount_I6.Checked = false;
                chk_isamountdiscount_I6.Checked = true;
                txt_amoundiscount_I4.Focus();
            }
        }

        private void chk_amountdiscount_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_isamountdiscount_I6.Checked == false)
            {
                txt_amoundiscount_I4.Enabled = false;
                txt_percentdiscount_I4.Enabled = true;
                chk_ispercentdiscount_I6.Checked = true;
                chk_isamountdiscount_I6.Checked = false;
                txt_percentdiscount_I4.Focus();
            }
            else
            {
                txt_amoundiscount_I4.Enabled = true;
                txt_percentdiscount_I4.Enabled = false;
                chk_ispercentdiscount_I6.Checked = false;
                chk_isamountdiscount_I6.Checked = true;
                txt_amoundiscount_I4.Focus();
            }
        }

        private void chk_istimegold_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_istimegold_I6.Checked == true)
            {
                txt_fromhour_5_I2.Enabled = true;
                txt_tohour_5_I2.Enabled = true;
                txt_fromhour_5_I2.Focus();
            }
            else
            {
                txt_fromhour_5_I2.Enabled = false;
                txt_tohour_5_I2.Enabled = false;
            }
        }

        private void btn_accept_S_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ((DataTable)gct_listadd_C.DataSource);
                dt.Columns["newprice"].ReadOnly = false;
                gct_listadd_C.DataSource = dt;
                for (int i = 0; i < gv_listadd_C.DataRowCount; i++)
                {
                    if (chk_ispercentdiscount_I6.Checked == true)
                    {
                        if ((Convert.ToInt16(txt_percentdiscount_I4.Text) < 0) || (Convert.ToInt16(txt_percentdiscount_I4.Text) > 100))
                        {
                            Function.clsFunction.MessageInfo("Thông báo", "Sai giá trị cập nhật");
                            txt_percentdiscount_I4.Focus();
                        }
                        else
                        {
                            gv_listadd_C.SetRowCellValue(i, "newprice", Convert.ToInt32(gv_listadd_C.GetRowCellValue(i, "oldprice")) * (1 - Convert.ToDouble(txt_percentdiscount_I4.Text) / 100));
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(txt_amoundiscount_I4.Text) < 0)
                        {
                            Function.clsFunction.MessageInfo("Thông báo", "Sai giá trị cập nhật");
                            txt_amoundiscount_I4.Focus();
                        }
                        else
                        {
                            gv_listadd_C.SetRowCellValue(i, "newprice", Convert.ToInt32(gv_listadd_C.GetRowCellValue(i, "oldprice")) - Convert.ToDouble(txt_amoundiscount_I4.Text));
                        }
                    }
                }
            }
            catch { }
        }

        private void chk_status_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_status_I6.Checked == true)
            {
                chk_status_I6.Text = Function.clsFunction.transLateText("Đang khuyến mãi");
            }
            else
            {
                chk_status_I6.Text = Function.clsFunction.transLateText("Ngừng khuyến mãi");
            }
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


        private void gv_list_C_DoubleClick(object sender, EventArgs e)
        {
            btn_add_S.PerformClick();
        }

        private void gv_listadd_C_DoubleClick(object sender, EventArgs e)
        {
            btn_remove_S.PerformClick();
        }

        #endregion

        #region Methods

        private void addGroup(string group)
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (A.Rows.Count > 0)
                {
                    for (int i = 0; i < A.Rows.Count; i++)
                    {
                        object[] keys = new object[2];
                        keys[0] = gv_list_C.GetRowCellValue(i, "idcommodity").ToString();
                        keys[1] = txt_idpromotion_IK1.Text + gv_list_C.GetRowCellValue(i, "idcommodity").ToString();
                        DataColumn[] keyColumns = new DataColumn[2];
                        keyColumns[0] = B.Columns["iddetail"];
                        keyColumns[1] = B.Columns["idcommodity"];
                        B.PrimaryKey = keyColumns;
                        if (B.Rows.Find(keys) == null && gv_list_C.GetRowCellValue(i, "idkind").ToString() == group)
                        {
                            DataSet ds = new DataSet();
                            ds = Add(A, B, gv_list_C.GetRowCellValue(i, "idcommodity").ToString());
                            i -= 1;
                        }    
                 
                    }
                }
            }
            catch { }
        }

        private void removeGroup(string group)
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (B.Rows.Count > 0)
                {
                    for (int i = 0; i < B.Rows.Count; i++)
                    {
                        object[] keys = new object[1];
                        keys[0] = gv_listadd_C.GetRowCellValue(i, "idcommodity").ToString();                 
                        DataColumn[] keyColumns = new DataColumn[1];              
                        keyColumns[0] = A.Columns["idcommodity"];
                        A.PrimaryKey = keyColumns;
                        if (A.Rows.Find(keys) == null && gv_listadd_C.GetRowCellValue(i, "idkind").ToString() == group)
                        {
                            DataSet ds = new DataSet();
                            ds = Remove(A, B, gv_listadd_C.GetRowCellValue(i, "idcommodity").ToString());
                            i -= 1;
                        }
              
                    }
                }
            }
            catch { }
        }

        private void addOne()
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (A.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds = Add(A, B, gv_list_C.GetRowCellValue(gv_list_C.FocusedRowHandle, "idcommodity").ToString());
                }
            }
            catch { }
        }

        private void removeOne()
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (B.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds = Remove(A, B, gv_listadd_C.GetRowCellValue(gv_listadd_C.FocusedRowHandle, "idcommodity").ToString());
                }
            }
            catch { }
        }

        private void addAll()
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (A.Rows.Count > 0)
                {
                    for (int i = 0; i < A.Rows.Count; i++)
                    {
                        object[] keys = new object[2];
                        keys[0] = gv_list_C.GetRowCellValue(i, "idcommodity").ToString();
                        keys[1] = txt_idpromotion_IK1.Text + gv_list_C.GetRowCellValue(i, "idcommodity").ToString();
                        DataColumn[] keyColumns = new DataColumn[2];
                        keyColumns[0] = B.Columns["iddetail"];
                        keyColumns[1] = B.Columns["idcommodity"];
                        B.PrimaryKey = keyColumns;
                
                        {
                            if (B.Rows.Find(keys) == null)
                            {
                                DataSet ds = new DataSet();
                                ds = Add(A, B, gv_list_C.GetRowCellValue(i, "idcommodity").ToString());
                                i -= 1;
                            }
                        }
               
                    }
                }
            }
            catch { }
        }

        private void removeAll()
        {
            try
            {
                DataTable A = new DataTable();
                DataTable B = new DataTable();
                A = (DataTable)gct_list_C.DataSource;
                B = (DataTable)gct_listadd_C.DataSource;
                if (B.Rows.Count > 0)
                {
                    for (int i = 0; i < B.Rows.Count; i++)
                    {
                        object[] keys = new object[1];
                        keys[0] = gv_listadd_C.GetRowCellValue(i, "idcommodity").ToString();          
                        DataColumn[] keyColumns = new DataColumn[1];                    
                        keyColumns[0] = B.Columns["idcommodity"];
                        B.PrimaryKey = keyColumns;
                        if (A.Rows.Find(keys) == null)
                        {
                            DataSet ds = new DataSet();
                            ds = Remove(A, B, gv_listadd_C.GetRowCellValue(gv_listadd_C.FocusedRowHandle, "idcommodity").ToString());
                            i -= 1;
                        }
                    }
                }
            }
            catch { }
        }

        private DataSet Add(DataTable dtA, DataTable dtR, string idcommodity)
        {
            DataSet ds = new DataSet();
            try
            {

                DataRow drR;
                DataRow drA;
                drR = dtR.NewRow();
                drA = dtA.Rows.Find(idcommodity);
                drR["iddetail"] = txt_idpromotion_IK1.Text + drA["idcommodity"].ToString();
                drR["idcommodity"] = drA["idcommodity"];
                drR["sign"] = drA["sign"];
                drR["oldprice"] = drA["oldprice"];
                drR["newprice"] = drA["oldprice"];
                drR["idkind"] = drA["idkind"];
                drR["status"] = true;
                dtR.Rows.Add(drR);
                dtA.Rows.RemoveAt(dtA.Rows.IndexOf(drA));
                DataSet u = dtA.DataSet;
                if (u != null)
                {
                    if (u.Tables.Count > 0)
                        u.Tables.Remove(dtA.TableName);
                }
                DataSet u1 = dtR.DataSet;
                if (u1 != null)
                {
                    if (u1.Tables.Count > 0)
                        u1.Tables.Remove(dtR.TableName);
                }
                ds.Tables.Add(dtA);
                ds.Tables.Add(dtR);
            }
            catch { }
            return ds;
        }

        private DataSet Remove(DataTable dtA, DataTable dtR, string idcommodity)
        {
            DataSet ds = new DataSet();
            try
            {
                object[] keys = new object[1];
                keys[0] = idcommodity;
                //keys[1] = txt_idpromotion_IK1.Text + idcommodity;
                DataRow drR;
                DataRow drA;
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dtR.Columns["idcommodity"];
                //keyColumns[0] = dtR.Columns["iddetail"];
                
                dtR.PrimaryKey = keyColumns;
                drA = dtA.NewRow();

                drR = dtR.Rows.Find(idcommodity);
                drA["idcommodity"] = drR["idcommodity"];
                drA["sign"] = drR["sign"];
                drA["oldprice"] = drR["oldprice"];
                drA["idkind"] = drR["idkind"];
                dtA.Rows.Add(drA);
                dtR.Rows.RemoveAt(dtR.Rows.IndexOf(drR));
                DataSet u = dtA.DataSet;
                if (u != null)
                {
                    if (u.Tables.Count > 0)
                        u.Tables.Remove(dtA.TableName);
                }
                DataSet u1 = dtR.DataSet;
                if (u1 != null)
                {
                    if (u1.Tables.Count > 0)
                        u1.Tables.Remove(dtR.TableName);
                }
                ds.Tables.Add(dtA);
                ds.Tables.Add(dtR);
            }
            catch { }
            return ds;
        }

        private void save()
        {
            if (!checkInput()) return;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                saveDetail();
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpromotion_IK1.Name) + " = '" + txt_idpromotion_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert.Text), txt_idpromotion_IK1.Text, txt_promotion_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idpromotion_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_idpromotion_IK1.Name),ID);
                saveDetail();
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpromotion_IK1.Name) + " = '" + txt_idpromotion_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(bbi_allow_insert.Text), txt_idpromotion_IK1.Text, txt_promotion_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
       
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idpromotion_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idpromotion_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idpromotion_IK1);
                chk_status_I6.Checked = true;
                chk_ismo_I6.Checked = true;
                chk_istu_I6.Checked = true;
                chk_iswe_I6.Checked = true;
                chk_isth_I6.Checked = true;
                chk_isfr_I6.Checked = true;
                chk_issa_I6.Checked = true;
                chk_issu_I6.Checked = true;
              

            } 
            else
            {
                txt_idpromotion_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idpromotion_IK1);
                loadGridLeft();             
            }                     
        }

        private bool checkInput()
        {
            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (txt_promotion_100_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_promotion_100_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_promotion_100_I2.Focus();
                return false;
            }

            if (glue_IDEMP_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_IDEMP_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_IDEMP_I1.Focus();
                return false;
            }

            if (dte_dateimport_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_dateimport_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_dateimport_I5.Focus();
                return false;
            }
            if (dte_fromdate_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_fromdate_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_fromdate_I5.Focus();
                return false;
            }

            if (dte_todate_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_todate_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_todate_I5.Focus();
                return false;
            }

            if (gv_listadd_C.DataRowCount==0)
            {
                Function.clsFunction.MessageInfo("Thông báo","Vui lòng chọn mặt hàng cần cập nhật lại giá");
                gv_listadd_C.Focus();
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
                dt = APCoreProcess.APCoreProcess.Read("select sign from " + Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idpromotion_IK1.Name) + "='" + txt_idpromotion_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(this.Name) + " where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }
        
        private void loadGridLookupEmployees()
        {
            string[] caption = new string[] { "Mã NV", "Nhân viên" };
            string[] fieldname = new string[] { "IDEMP", "StaffName" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES where status=1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
        }

        private void loadGridLeft()
        {
            DataTable dtA = new DataTable();
            DataTable dtR = new DataTable();
            dtA = APCoreProcess.APCoreProcess.Read(" select  hh.sign, hh.idcommodity,hh.price as oldprice, hh.idkind from DMCOMMODITY hh  where hh.idcommodity not in (select idcommodity from promotiondetail dt where idpromotion='" + txt_idpromotion_IK1.Text + "') order by idkind  ");
            dtR = APCoreProcess.APCoreProcess.Read(" select  hh.sign, hh.idcommodity,dt.oldprice as oldprice, hh.idkind, dt.newprice,dt.iddetail,dt.status from DMCOMMODITY hh  inner join promotiondetail dt on hh.idcommodity=dt.idcommodity where dt.idpromotion='" + txt_idpromotion_IK1.Text + "' order by idkind ");
            dtA.Columns.Add("chooseA", typeof(bool));
            dtR.Columns.Add("chooseR", typeof(bool));
                 gct_list_C.DataSource = dtA;
             gct_listadd_C.DataSource = dtR;
        }

        private void saveDetail()
        {
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete promotiondetail where idpromotion='" + txt_idpromotion_IK1.Text + "'");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("promotiondetail where idpromotion='"+ txt_idpromotion_IK1.Text +"'");           
                DataRow dr;
                for (int i = 0; i < gv_listadd_C.DataRowCount; i++)
                {
                    dr = dt.NewRow();
                    dr["idpromotion"] = txt_idpromotion_IK1.Text;
                    dr["iddetail"] = txt_idpromotion_IK1.Text + gv_listadd_C.GetRowCellValue(i, "idcommodity").ToString(); ;
                    dr["idcommodity"] = gv_listadd_C.GetRowCellValue(i, "idcommodity").ToString();
                    dr["oldprice"] = Convert.ToDouble(gv_listadd_C.GetRowCellValue(i, "oldprice").ToString());
                    dr["newprice"] = Convert.ToDouble(gv_listadd_C.GetRowCellValue(i, "newprice").ToString());
                    dr["status"] = gv_listadd_C.GetRowCellValue(i, "status").ToString() != "" ? Convert.ToBoolean(gv_listadd_C.GetRowCellValue(i, "status").ToString()) : true;
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }                 
               
            }
            catch { }
        }

        private void updatePrice() // hàm này bỏ
        {
            try
            {
                // thực hiện cập nhật lại giá các đợt giảm giá đã hết hạn và status=true, nếu status= false đợt khuyên mãi đã kết thúc hoàn toàn và đã cập nhật lại giá
                DataTable dtProOld = new DataTable();
                dtProOld = APCoreProcess.APCoreProcess.Read("select * from promotion where status=1 and convert(datetime,'" + DateTime.Now.ToString("yyyyMMdd") + "',103) > CAST(datediff(d,0,todate) as datetime)");
                for (int i = 0; i < dtProOld.Rows.Count; i++)
                {
                    DataTable dtDT = new DataTable();
                    dtDT = APCoreProcess.APCoreProcess.Read("select * from promotiondetail where idpromotion='"+ dtProOld.Rows[i]["idpromotion"].ToString() +"'");
                    for (int j = 0; j < dtDT.Rows.Count; j++)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update dmcommodity set price=" + Convert.ToDouble(dtDT.Rows[j]["oldprice"]) + " where idcommodity='" + dtDT.Rows[j]["idcommodity"].ToString() + "'");
                    }
                }
                // lấy các đợt giảm giá đang trong giai doan activend 
                DataTable dtProCurrent = new DataTable();
                dtProCurrent = APCoreProcess.APCoreProcess.Read("select * from promotion where status=1 and convert(datetime,'" + DateTime.Now.ToString("yyyyMMdd") + "',103) between CAST(datediff(d,0,fromdate) as datetime) and CAST(datediff(d,0,todate) as datetime)");
                // thực hiện cập nhật lại giá vào danh muc mat hang
                for (int i = 0; i < dtProCurrent.Rows.Count; i++)
                {
                    DataTable dtDT = new DataTable();
                    dtDT = APCoreProcess.APCoreProcess.Read("select * from promotiondetail where idpromotion='" + dtProCurrent.Rows[i]["idpromotion"].ToString() + "'");
                    for (int j = 0; j < dtDT.Rows.Count; j++)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update dmcommodity set price=" + Convert.ToDouble(dtDT.Rows[j]["newprice"]) + " where idcommodity='" + dtDT.Rows[j]["idcommodity"].ToString() + "'");
                    }
                }

            }
            catch { }
        }


        #endregion        

 



    }
}