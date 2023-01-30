using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using Function;
using DevExpress.XtraBars;

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_PLANCRM_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_PLANCRM_S()
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
        public string _sign = "KH";
        public string ID = "";
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();
        public string idcustomer="";

        #endregion

        #region FormEvent

        private void frm_PLANCRM_S_Load(object sender, EventArgs e)
        {
            // statusForm = true;
            if (statusForm == true)
            {                
                try
                {
                    SaveGridControlsMission();
                    Function.clsFunction.CreateTable(this, this);
                    Function.clsFunction.Save_sysControl(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                loadGridLookupCus();                
                loadGridLookupEmp();
                loadGridLookupStatus();
                loadGridLookupCampaign();
                loadGridLookupContact();
                LoadInfo();
                
                Load_Grid_Mission();
                loadGridMission();
            }
            txt_sign_20_I1.Focus();
        }

        private void frm_PLANCRM_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I1.Focus();
        }

        private void frm_PLANCRM_S_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void bbi_plan_allow_insert_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv_mission_C.OptionsBehavior.Editable = true;
            gv_mission_C.OptionsView.ShowAutoFilterRow = false;
            gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private void bbi_plan_allow_delete_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv_mission_C.FocusedRowHandle >= 0)
                {
                    if (Function.clsFunction.MessageDelete("Thông báo", "Bạn có muốn xóa mẫu tin này"))
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("delete from EMPMISSION where idmission='" + gv_mission_C.GetRowCellValue(gv_mission_C.FocusedRowHandle, "idmission") + "'");
                        gv_mission_C.DeleteRow(gv_mission_C.FocusedRowHandle);
                        gv_mission_C.OptionsBehavior.Editable = false;
                        gv_mission_C.OptionsView.ShowAutoFilterRow = true;
                        gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                    }
                }
                else
                {
                    Function.clsFunction.MessageInfo("Thông báo", "Vui lòng chọn dòng cần xóa");
                }
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Lỗi Error :" + ex.Message);
            }
        }

        private void bbi_plan_allow_edit_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv_mission_C.OptionsBehavior.Editable = true;
            gv_mission_C.OptionsView.ShowAutoFilterRow = false;
            gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
  
        #endregion

        #region GridEvent

        private void SaveGridControlsMission()
        {
            //datasỏuce

            string sql_grid = Function.clsFunction.getNameControls(this.Name);
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "ID", "Tác vụ", "Nội dung", "Tình trạng", "Tiến độ", "Ghi chú"};

            // FieldName column
            string[] fieldname_col = new string[] { "idmission", "idtask", "contents", "idstatus", "progress", "note" };

            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "GridLookupEditColumn", "TextColumn", "GridLookupEditColumn", "SpinEditColumn", "TextColumn", "MemoColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "100", "200", "200", "200", "80", "200" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "False", "False", "False" };
            // Cac cot an
            string[] Column_Visible = new string[] { "True", "True", "True", "True", "True", "True" };
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

            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { "select idtask, taskname  from dmtask where status=1", "select idstatus, statusname from DMSTATUS " };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { "taskname", "statusname" };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { "idtask", "idstatus" };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[2, 2] { { "Mã tác vụ", "Tác vụ" }, { "Mã TT", "Tình trạng" } };
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[2, 2] { { "idtask", "taskname" }, { "idstatus", "statusname" } };
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
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, gv_mission_C.Name);
        }       

        private void Load_Grid_Mission()
        {
            string text = Function.clsFunction.langgues;
            gv_mission_C.Columns.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = false;
            // show filterRow
            gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "' and grid_name='" + gv_mission_C.Name + "'");

            try
            {
                ControlDev.FormatControls.Controls_in_Grid(gv_mission_C, read_Only, hien_Nav,
                       dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer, gct_mission_C,
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
                gv_mission_C.OptionsBehavior.Editable = true;
                gv_mission_C.OptionsView.ShowAutoFilterRow = true;
                arrFieldName = dt.Rows[0]["field_name"].ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadGridMission()
        {
            if (Function.clsFunction._pre == true)
            {
                dts = Function.clsFunction.dtTrace;
            }
            else
            {
              
                dts = APCoreProcess.APCoreProcess.Read("SELECT  idmission,idplan, idstatus, idtask, contents, note, progress FROM EMPMISSION  WHERE  idplan = '" + txt_idplan_IK1.Text + "'");
            }          
            gct_mission_C.DataSource = dts;
            gv_mission_C.OptionsBehavior.Editable = false;
            gv_mission_C.OptionsView.ShowAutoFilterRow = true;
            gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
    
        private void Manager_Mission_ItemPress(object sender, ItemClickEventArgs e)
        {
            string strCol = "";
            string strColName = "";
            string strColVisible = "";
            int pos = -1;
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select column_name,  column_visible from sysGridColumns where form_name='" + this.Name + "' and grid_name='" + gv_mission_C.Name + "'");
                if (dt.Rows.Count > 0)
                {
                    strColName = dt.Rows[0][0].ToString();
                    strCol = dt.Rows[0][1].ToString();
                    string[] arrayColName = strColName.Split('/');
                    string[] arrCol = strCol.Split('/');
                    if (e.Item.Name.Contains("_allow_col_") && (e.Item.GetType().Name == "BarCheckItem"))
                    {
                        pos = findIndexInArray(e.Item.Name.Split('_')[3].ToString(), arrayColName);
                        if (((BarCheckItem)e.Item).Checked != Convert.ToBoolean(arrCol[pos]))
                        {
                            arrCol[pos] = ((BarCheckItem)e.Item).Checked.ToString();
                            strColVisible = clsFunction.ConvertArrayToString(arrCol);
                            APCoreProcess.APCoreProcess.ExcuteSQL("update sysGridColumns set column_visible='" + strColVisible + "' where form_name='" + this.Name + "' and grid_name='" + gv_mission_C.Name + "'");
                            loadGridMission();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Error:" + ex.Message);
            }
        }

        private void gv_mission_C_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                menu.ItemLinks.Clear();
                bool flag = false;
                if (gv_mission_C.FocusedRowHandle >= 0)
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
                    clsFunction.customPopupMenu(bar_mission_C, menu, gv_mission_C, this);
                    menu.Manager.ItemClick += new ItemClickEventHandler(Manager_Mission_ItemPress);
                    menu.ShowPopup(MousePosition);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo", "Lỗi Error: " + ex.Message);
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
        
        private void gv_mission_C_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (gv_mission_C.RowCount <= 1)
            {
                gv_mission_C.SetRowCellValue(e.RowHandle, "idmission", clsFunction.layMa("MS", "idmission", "EMPMISSION"));
            }
            else
            {
                String sId = "";
                sId = gv_mission_C.GetRowCellValue(gv_mission_C.RowCount - 2, "idmission").ToString().Replace("MS", "");
                int iId = Convert.ToInt16(sId);
                iId += 1;
                gv_mission_C.SetRowCellValue(e.RowHandle, "idmission", ("MS" + iId.ToString().PadLeft(6, '0')));
            }
        }

        private void gct_mission_C_Leave(object sender, EventArgs e)
        {
            gv_mission_C.OptionsBehavior.Editable = false;
            gv_mission_C.OptionsView.ShowAutoFilterRow = true;
            gv_mission_C.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }  

        #endregion

        #region Methods

        private void save()
        {
            if (!checkInput()) return;
            DataTable dt = new DataTable();
            DataRow dr;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idplan_IK1.Name) + " = '" + txt_idplan_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idplan_IK1.Text, mmo_description_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                saveMission();
                strpassData(txt_idplan_IK1.Text);
                if (call == true)
                {
                    strpassData(txt_idplan_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,"idplan",ID);
       
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idplan_IK1.Name) + " = '" + txt_idplan_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idplan_IK1.Text, mmo_description_I3.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                saveMission();
                passData(true);
            }
        }

        private void saveMission()
        {
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("delete from EMPMISSION WHERE idplan='"+txt_idplan_IK1.Text+"'");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT  idmission, idplan, idstatus, idtask, contents, note, progress FROM  dbo.EMPMISSION WHERE idplan='"+ txt_idplan_IK1.Text +"'");
                DataRow dr;
                for (int i = 0; i < gv_mission_C.RowCount; i++)
                {
                    dr = dt.NewRow();
                    dr["idmission"] = gv_mission_C.GetRowCellValue(i,"idmission").ToString();
                    dr["idplan"] = txt_idplan_IK1.Text;
                    dr["idstatus"] = gv_mission_C.GetRowCellValue(i, "idstatus").ToString();
                    dr["idtask"] = gv_mission_C.GetRowCellValue(i, "idtask").ToString();
                    dr["contents"] = gv_mission_C.GetRowCellValue(i, "contents").ToString();
                    dr["note"] = gv_mission_C.GetRowCellValue(i, "note").ToString();
                    dr["progress"] = gv_mission_C.GetRowCellValue(i, "progress").ToString() == "" ? "0" : gv_mission_C.GetRowCellValue(i, "progress").ToString();
                    dt.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }
            catch (Exception ex)
            {
                clsFunction.MessageInfo("Thông báo"," Vui lòng nhập hoàn tất nhiệm vụ. Error  : "+ex.Message);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idplan_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idplan_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idplan_IK1);
                txt_sign_20_I1.Text = txt_idplan_IK1.Text;

                glue_idcustomer_I1.EditValue = idcustomer;
            } 
            else
            {
                txt_idplan_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idplan_IK1);            
            }
            //gv_mission_C.Columns[0].OptionsColumn.AllowEdit = false;     
        }

        private bool checkInput()
        {
            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (glue_idcontact_I1.Text == "")
            {
                glue_idcontact_I1.Focus();
                dxEp_error_S.SetError(glue_idcontact_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (mmo_description_I3.Text == "")
            {
                dxEp_error_S.SetError(mmo_description_I3, Function.clsFunction.transLateText("Không được rỗng"));
                mmo_description_I3.Focus();
                return false;
            }
            if (glue_idemp_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idemp_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idemp_I1.Focus();
                return false;
            }
            if (dte_datecontact_I5.EditValue==null)
            {
                dxEp_error_S.SetError(dte_datecontact_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_datecontact_I5.Focus();
                return false;
            }

            

            if (glue_idcampaign_I1.EditValue == null)
            {
                dxEp_error_S.SetError(glue_idcampaign_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idcampaign_I1.Focus();
                return false;
            }


            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from PLANCRM where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from PLANCRM where idplan='" + txt_idplan_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from PLANCRM where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }
        
        private void loadGridLookupEmp()
        {
            string[] caption = new string[] { "Mã nhân viên", "Tên nhân viên" };
            string[] fieldname = new string[] { "idemp", "staffname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1,"select idemp, staffname from employees where idemp =N'"+ clsFunction.GetIDEMPByUser() +"'","staffname","idemp",caption,fieldname, this.Name, glue_idemp_I1.Width);
        }

        private void loadGridLookupContact()
        {
            string[] caption = new string[] { "Mã NLH", "Người liên hệ" };
            string[] fieldname = new string[] { "idcontact", "contact" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idcontact_I1, "select idcontact, (contactname + ' - ' + tel) as contact from cuscontact where idcustomer = '"+ idcustomer +"'", "contact", "idcontact", caption, fieldname, this.Name, glue_idcontact_I1.Width);
        }

        // CUSTOMERTYPE
        private void loadGridLookupCus()
        {
            string[] caption = new string[] { "Mã KH", "Tên khách hàng" };
            string[] fieldname = new string[] { "idcustomer", "customer" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idcustomer_I1, "select idcustomer, customer from dmcustomers", "customer", "idcustomer", caption, fieldname, this.Name, glue_idcustomer_I1.Width);
        }

        // Status
        private void loadGridLookupStatus()
        {
            string[] caption = new string[] { "ID", "Tình trạng" };
            string[] fieldname = new string[] { "idstatus", "statusname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idstatus_I1, "select idstatus, statusname from dmstatus", "statusname", "idstatus", caption, fieldname, this.Name, glue_idstatus_I1.Width);
        }        

        // Campaign
        private void loadGridLookupCampaign()
        {
            string[] caption = new string[] { "ID", "Chiến dịch" };
            string[] fieldname = new string[] { "idcampaign", "campaign" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idcampaign_I1, "select idcampaign, campaign from  DMCAMPAIGN where  todate > fromdate and ( todate<=getDate() and fromdate >=getDate()  ) or unlimited = 1", "campaign", "idcampaign", caption, fieldname, this.Name, glue_idcampaign_I1.Width);
        }
        #endregion 
        
    }
}