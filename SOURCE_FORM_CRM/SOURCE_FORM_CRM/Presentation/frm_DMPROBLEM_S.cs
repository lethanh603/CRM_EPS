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

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_DMPROBLEM_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMPROBLEM_S()
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
        public string _sign = "PL";
        public string ID = "";
        public string idpre = "";
        public string idquotation = "";
        public string idplan = "";

        #endregion

        #region FormEvent

        private void frm_DMCAMPAIGN_S_Load(object sender, EventArgs e)
        {
            _sign = "PL";
            // statusForm = true;
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    SaveGridControls_SettingDetail();
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
                loadGridLookupPriority();
                loadGridLookupGroupSetting();
                loadGridLookupSettingDetail();
                Load_Grid_SettingDetail();               
                LoadInfo();
                glue_idsettingdetail_I1_EditValueChanged(sender,e);
               
            }

            mmo_problem_I3.Focus();
        }

        private void frm_DMCAMPAIGN_S_Activated(object sender, EventArgs e)
        {
            txt_idpre_20_I1.Focus();
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
                txt_idpre_20_I1.Focus();
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
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
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
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblem_IK1.Name) + " = '" + txt_idproblem_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idproblem_IK1.Text, mmo_problem_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    savePhatSinh();
                    if (call == true)
                    {
                        strpassData(txt_idproblem_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idproblem_IK1.Name), ID);
                    savePhatSinh();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblem_IK1.Name) + " = '" + txt_idproblem_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idproblem_IK1.Text, mmo_problem_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    passData(true);
                }
            }
            catch { }
        }

        private void savePhatSinh()
        {
            try
            {
                string idproblem = txt_idproblem_IK1.Text;
                for (int i = 0; i < gv_problem_C.DataRowCount; i++)
                {
                    DataTable dtCheck = new DataTable();
                    dtCheck = APCoreProcess.APCoreProcess.Read("select * from suvuphatsinh where idproblem ='" + idproblem + "' and idinvolve = '"+ gv_problem_C.GetRowCellValue(i,"idinvolve").ToString() +"'");

                    if (dtCheck.Rows.Count > 0)
                    {
                        // update
                        DataRow dr = dtCheck.Rows[0];
                        dr["updateddate"] = Function.clsFunction.getDateServer();
                        dr["status"] = Convert.ToBoolean(gv_problem_C.GetRowCellValue(i, "status"));
                        dr["idproblemdetail"] = glue_idsettingdetail_I1.EditValue.ToString();
                        dr["idinvolve"] = gv_problem_C.GetRowCellValue(i, "idinvolve").ToString();
                        dr["hide"] = false;
                        APCoreProcess.APCoreProcess.Save(dr);
                        savePhatSinhAdmin(dtCheck,idproblem, true, true);
                    }
                    else
                    {
                        // insert
                        DataRow dr = dtCheck.NewRow();
                        dr["idphatsinh"] = Function.clsFunction.layMa("PS", "idphatsinh", "suvuphatsinh");
                        dr["idproblem"] = txt_idproblem_IK1.Text;
                        dr["createddate"] = Function.clsFunction.getDateServer();
                        dr["status"] = Convert.ToBoolean( gv_problem_C.GetRowCellValue(i, "status"));
                        dr["idproblemdetail"] = glue_idsettingdetail_I1.EditValue.ToString();
                        dr["idinvolve"] = gv_problem_C.GetRowCellValue(i,"idinvolve").ToString();
                        dr["hide"] = false;
                        dtCheck.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                        savePhatSinhAdmin(dtCheck, idproblem, true, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi hệ thống "+ ex.Message);
            }
        }

        private void savePhatSinhAdmin(DataTable dtCheck , string idproblem, bool status, bool hide)
        {
            DataTable dt1 = APCoreProcess.APCoreProcess.Read("select * from SETTINGINVOLVE where idsettingdetail='" + glue_idsettingdetail_I1.EditValue + "' and idemp='" + getEMPAdmin() + "' ");
            
            if (dt1.Rows.Count > 0)
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select * from suvuphatsinh PS inner join SETTINGINVOLVE DT on PS.idinvolve = DT.idinvolve  where PS.idproblem='"+ idproblem +"' AND PS.idinvolve ='" + dt1.Rows[0]["idinvolve"].ToString() + "' and DT.idemp='" + getEMPAdmin() + "' ");
                if (dt.Rows.Count == 0)
                {

                    DataRow dr = dtCheck.NewRow();
                    dr["idphatsinh"] = Function.clsFunction.layMa("PS", "idphatsinh", "suvuphatsinh");
                    dr["idproblem"] = idproblem;
                    dr["createddate"] = Function.clsFunction.getDateServer();
                    dr["status"] = status;
                    dr["idproblemdetail"] = glue_idsettingdetail_I1.EditValue.ToString();
                    dr["idinvolve"] = dt1.Rows[0]["idinvolve"].ToString();
                    dr["hide"] = hide;
                    dtCheck.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr); APCoreProcess.APCoreProcess.Save(dr);
                }
            }
        }
        
        private string getEMPAdmin()
        {
            string id = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where root=1");
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["IDEMP"].ToString();
            }
            return id;
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert == true)
            {
                txt_idproblem_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idproblem_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idproblem_IK1);
                chk_status_I6.Checked = true;
                txt_idpre_20_I1.Text = idpre;
                txt_idemp_I2.Text = Function.clsFunction.GetIDEMPByUser();
                txt_quotationno_I2.Text = idquotation;
                txt_idplan_I1.Text = idplan;
            }
            else
            {
                txt_idproblem_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idproblem_IK1);
            }
            
        }

        private bool checkInput()
        {
            if (mmo_problem_I3.Text == "")
            {
                dxEp_error_S.SetError(mmo_problem_I3, Function.clsFunction.transLateText("Không được rỗng"));
                mmo_problem_I3.Focus();
                return false;
            }
            if (!loadQuotationInfo())
            {
                dxEp_error_S.SetError(txt_quotationno_I2, Function.clsFunction.transLateText("Số PO/ KS không tồn tại"));
                txt_quotationno_I2.Focus();
                return false;
            }
            return true;
        }

        // Priority
        private void loadGridLookupPriority()
        {
            string[] caption = new string[] { "ID", "Ưu tiên" };
            string[] fieldname = new string[] { "idpriority", "priority" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idpriority_I1, "select idpriority, priority from dmpriority", "priority", "idpriority", caption, fieldname, this.Name, glue_idpriority_I1.Width);
        }

        private void loadGridLookupGroupSetting()
        {
            string[] caption = new string[] { "ID", "Nhóm sự vụ" };
            string[] fieldname = new string[] { "idgroupsetting", "groupsetting" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idgroupsetting_I1, "select idgroupsetting, groupsetting from settingproblem", "groupsetting", "idgroupsetting", caption, fieldname, this.Name, glue_idgroupsetting_I1.Width);
        }

        private void loadGridLookupSettingDetail()
        {
            try
            {
                string[] col_visible = new string[] { "True", "True" };
                string[] caption = new string[] { "Mã vụ việc", "Tên vụ việc" };
                string[] fieldname = new string[] { "idsettingdetail", "settingdetail" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idsettingdetail_I1, "select idsettingdetail, settingdetail from settingproblemdetail where idgroupsetting = '" + glue_idgroupsetting_I1.EditValue.ToString() + "'", "settingdetail", "idsettingdetail", caption, fieldname, this.Name, glue_idsettingdetail_I1.Width, col_visible);

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        // setting problem detail
        private void SaveGridControls_SettingDetail()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Status", "ID", "Đối tượng", "Nhân viên" };

            // FieldName column
            string[] fieldname_col = new string[] { "status", "idinvolve","involve", "StaffName" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "CheckColumn", "TextColumn", "TextColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "200", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "True", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "False", "True", "True" };
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
            int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select id, area  from  dmarea  where status=1" };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "area" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "id" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[1, 2] { { "ID", "Khu vực" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[1, 2] { { "id", "area" } };
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysGridColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue, caption_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_problem_C.Name);
        }

        private void Load_Grid_SettingDetail()
        {
            string text = Function.clsFunction.langgues;
            string arrCaption;
            string arrFieldName;
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_problem_C.OptionsView.ShowAutoFilterRow = false;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_problem_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_problem_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_problem_C,
                       dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                       dt.Rows[0]["field_name"].ToString().Split('/'),
                       dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                       dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                       dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                       dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                       int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                       dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                       Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_glue_col"].ToString(), "@", "/"),
                       Function.clsFunction.ConvertStringToArrayN((dt.Rows[0]["caption_glue_col"].ToString()), "@", "/"),
                       dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator 
                arrCaption = dt.Rows[0]["caption"].ToString();
                arrFieldName = dt.Rows[0]["field_name"].ToString();
                gv_problem_C.OptionsBehavior.Editable = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void glue_idgroupsetting_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadGridLookupSettingDetail();
        }

        private void glue_idsettingdetail_I1_EditValueChanged(object sender, EventArgs e)
        {
            loadInvolve(glue_idsettingdetail_I1.EditValue.ToString());
        }

        private void loadInvolve(string idinvolve)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT   DT.idinvolve, DT.involve, E.StaffName, cast( case when (select count(*) from SUVUPHATSINH where idinvolve=DT.idinvolve and status =1 and idproblem='" + txt_idproblem_IK1.Text + "' ) >0 then 1 else 0 end as bit) as status FROM  dbo.SETTINGINVOLVE AS DT INNER JOIN  dbo.EMPLOYEES AS E ON DT.idemp = E.IDEMP where DT.idsettingdetail= '" + idinvolve + "' AND  isnull( DT.hide,0) = 0  ");
                dt.Columns[3].ReadOnly = false;
                gct_problem_C.DataSource = dt;
            }
            catch (Exception ex) {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi hệ thống " + ex.Message );
            }
        }

        private Boolean loadQuotationInfo()
        {
            if (txt_quotationno_I2.Text == "")
            {
                return false;
            }
            string sql = " SELECT T.* FROM (SELECT Q.idcustomer, Q.idemp from QUOTATION Q where invoiceeps ='" + txt_quotationno_I2.Text + "'";
            sql += " UNION";
            sql += " SELECT S.idcustomer, S.idemp as idemp from SURVEY S where S.idsurvey='" + txt_quotationno_I2.Text + "')  T";
            sql += " INNER JOIN EMPLOYEES E ON T.IDEMP=E.IDEMP where  idrecursive like '%" + Function.clsFunction.GetIDEMPByUser() + "%'";

            DataTable dt = APCoreProcess.APCoreProcess.Read(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void txt_idquotation_I1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!loadQuotationInfo())
                {
                    clsFunction.MessageInfo("Thông báo", "Số PO/ KS không tồn tại");
                }
            }
        }

        #endregion
    }
}