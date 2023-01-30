//Thanh 
//Danh mục cakip
//15/5/2013
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;
using System.Reflection;

namespace SOURCE_FORM.Presentation
{
    public partial class frm_sysRole_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_sysRole_S()
        {
            InitializeComponent();
        }

        #region Var
      
        public bool them = true;
        public int index;
        public bool statusForm=false;
        public string langues1="_VI",maUser="";
        public delegate void PassDataUser(bool value);
        public PassDataUser passData;
        public string dauma = "VT", mavaitro = "VT000001";
      
        public  void GetValue(bool value)
        {
            bool va_lue = value;
            if (va_lue == true)
            {                
                
            }
        }
        #endregion

        #region Load
        private void frmNhapDVT_Load(object sender, EventArgs e)
        {
            try
            {
                if (statusForm == true)
                {
                    Function.clsFunction.Save_sysControl(this, this);
                    try
                    {
                        SavetreeList();
                        setVaiTro();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    loadVaiTro();
                    LoadTT();
                    System.Data.DataTable dt_textForm = new System.Data.DataTable();
                    //dt_textForm = APCoreProcess.APCoreProcess.Read("select * from sysSubmenu where form_name='" + this.Name.ToString().Trim() + "" + "'");
                    //this.Text =  dt_textForm.Rows[0]["text_form" + langues].ToString() + dtMessage.Rows[20]["message"+langues].ToString()+ Function.clsFunction._user;
                    chk_status_I6.Checked = true;
                    
                    Function.clsFunction.TranslateForm(this, this.Name);
                    Function.clsFunction.TranslateTreeColumn(tl_vaitro_S);
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_all"].ColumnEdit).CheckedChanged += chk_allow_all_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_access"].ColumnEdit).CheckedChanged += chk_allow_access_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_insert"].ColumnEdit).CheckedChanged += chk_allow_insert_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_edit"].ColumnEdit).CheckedChanged += chk_allow_edit_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_delete"].ColumnEdit).CheckedChanged += chk_allow_delete_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_print"].ColumnEdit).CheckedChanged += chk_allow_print_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_import"].ColumnEdit).CheckedChanged += chk_allow_import_CheckedChanged;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)tl_vaitro_S.Columns["allow_export"].ColumnEdit).CheckedChanged += chk_allow_export_CheckedChanged;
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        
        private DataTable CreateTableEdit()
        {            
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ParentID", typeof(string));
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("allow_all", typeof(bool));
            tbl.Columns.Add("allow_access", typeof(bool));
            tbl.Columns.Add("allow_insert", typeof(bool));
            tbl.Columns.Add("allow_edit", typeof(bool));
            tbl.Columns.Add("allow_delete", typeof(bool));
            tbl.Columns.Add("allow_print", typeof(bool));
            tbl.Columns.Add("allow_export", typeof(bool));
            tbl.Columns.Add("allow_import", typeof(bool));
            tbl.Columns.Add("mavaitro", typeof(string));
            DataTable dtMenu = new DataTable();
            dtMenu = APCoreProcess.APCoreProcess.Read("sysMenu");
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                tbl.Rows.Add(new object[] { dtMenu.Rows[i]["menuname"].ToString(), 0, dtMenu.Rows[i]["IDMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                DataTable dtGroupMenu = new DataTable();
                dtGroupMenu = APCoreProcess.APCoreProcess.Read("select * from sysGroupSubMenu where IDMenu='" + dtMenu.Rows[i]["IDMenu"].ToString() + "' and IDGroupMenu IN (select IDGroupMenu from sysSubMenu)");
                for (int j = 0; j < dtGroupMenu.Rows.Count; j++)
                {
                    tbl.Rows.Add(new object[] { dtGroupMenu.Rows[j]["GroupMenuName"].ToString(), dtMenu.Rows[i]["IDMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                    DataTable dtSubpMenu = new DataTable();
                    dtSubpMenu = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT   sysPower.allow_access,sysPower.allow_all, sysPower.allow_insert, sysPower.allow_delete, sysPower.allow_edit, sysPower.allow_print,    sysPower.allow_import, sysPower.allow_export, sysPower.mavaitro, sysSubMenu.IDSubMenu, sysSubMenu.NameSubMenu,     sysSubMenu.IDGroupMenu FROM         sysSubMenu INNER JOIN        sysPower ON sysSubMenu.IDSubMenu = sysPower.IDSubMenu where IDGroupMenu='" + dtGroupMenu.Rows[j]["IDGroupMenu"].ToString() + "' and sysPower.mavaitro='" + mavaitro + "'");
                    for (int k = 0; k < dtSubpMenu.Rows.Count; k++)
                    {
                        tbl.Rows.Add(new object[] { dtSubpMenu.Rows[k]["NameSubMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), dtSubpMenu.Rows[k]["IDSubMenu"].ToString(), dtSubpMenu.Rows[k]["allow_all"].ToString(), dtSubpMenu.Rows[k]["allow_access"].ToString(), dtSubpMenu.Rows[k]["allow_insert"].ToString(), dtSubpMenu.Rows[k]["allow_edit"].ToString(), dtSubpMenu.Rows[k]["allow_delete"].ToString(), dtSubpMenu.Rows[k]["allow_print"].ToString(), dtSubpMenu.Rows[k]["allow_export"].ToString(), dtSubpMenu.Rows[k]["allow_import"].ToString(), "" });
                    }
                }
            }
            return tbl;
        }
        private DataTable CreateTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ParentID", typeof(string));
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("allow_all", typeof(bool));
            tbl.Columns.Add("allow_access", typeof(bool));
            tbl.Columns.Add("allow_insert", typeof(bool));
            tbl.Columns.Add("allow_edit", typeof(bool));
            tbl.Columns.Add("allow_delete", typeof(bool));
            tbl.Columns.Add("allow_print", typeof(bool));
            tbl.Columns.Add("allow_export", typeof(bool));
            tbl.Columns.Add("allow_import", typeof(bool));
            tbl.Columns.Add("mavaitro", typeof(string));
            DataTable dtMenu = new DataTable();
            lue_mavaitro_I1.EditValue = "VT000001";
            dtMenu = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT M.IDMenu, M.MenuName FROM sysMenu M INNER JOIN sysGroupSubmenu G ON M.IDMenu=G.IDMenu INNER JOIN sysSubMenu S ON G.IDGroupMenu = S.IDGroupMenu INNER JOIN sysPower P ON S.IDSubMenu = P.IDSubMenu Where P.mavaitro = '"+ lue_mavaitro_I1.EditValue +"' and allow_access=1 ");
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                tbl.Rows.Add(new object[] { dtMenu.Rows[i]["menuname" ].ToString(), 0, dtMenu.Rows[i]["IDMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                DataTable dtGroupMenu = new DataTable();
                dtGroupMenu = APCoreProcess.APCoreProcess.Read("select DISTINCT G.* from sysGroupSubMenu G  INNER JOIN sysSubMenu S ON G.IDGroupMenu = S.IDGroupMenu INNER JOIN sysPower P ON S.IDSubMenu = P.IDSubMenu Where P.mavaitro = '" + lue_mavaitro_I1.EditValue + "' and allow_access=1  and G.IDMenu='" + dtMenu.Rows[i]["IDMenu"].ToString() + "'");
                for (int j = 0; j < dtGroupMenu.Rows.Count; j++)
                {
                    tbl.Rows.Add(new object[] { dtGroupMenu.Rows[j]["GroupMenuName"].ToString(), dtMenu.Rows[i]["IDMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                    DataTable dtSubpMenu = new DataTable();
                    dtSubpMenu = APCoreProcess.APCoreProcess.Read(" select DISTINCT S.* From syssubmenu S INNER JOIN sysPower P ON S.IDSubMenu = P.IDSubMenu Where P.mavaitro = '" + lue_mavaitro_I1.EditValue + "' and allow_access=1 and IDGroupMenu='" + dtGroupMenu.Rows[j]["IDGroupMenu"].ToString() + "' ");
                    for (int k = 0; k < dtSubpMenu.Rows.Count; k++)
                    {
                        tbl.Rows.Add(new object[] { dtSubpMenu.Rows[k]["NameSubMenu"].ToString(), dtGroupMenu.Rows[j]["IDGroupMenu"].ToString(), dtSubpMenu.Rows[k]["IDSubMenu"].ToString(), true, true, true, true, true, true, true, true, "" });
                    }
                }
            }
            return tbl;
        }
        private void LoadTT()
        {
            //tl_vaitro_S = new DevExpress.XtraTreeList.TreeList();
            if (them == true)
            {
                tl_vaitro_S.DataMember.Clone();
                tl_vaitro_S.Nodes.Clear();
                tl_vaitro_S.DataSource = null;
                txt_mavaitro_IK1.Text = Function.clsFunction.layMa(dauma, "mavaitro", "sysRole");
                if (APCoreProcess.APCoreProcess.Read("syspower where mavaitro='" + txt_mavaitro_IK1.Text + "'").Rows.Count == 0)
                    tl_vaitro_S.DataSource = CreateTable();
                else
                    tl_vaitro_S.DataSource = CreateTableEdit();
                tl_vaitro_S.RefreshDataSource();
                Load_Tree();
            }
            else
            {
                txt_mavaitro_IK1.Text = mavaitro;
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysRole where mavaitro='"+mavaitro+"'");
                if (dt.Rows.Count > 0)
                {
                    txt_tenvaitro_I2.Text = dt.Rows[0]["tenvaitro"].ToString();
                    lue_mavaitro_I1.EditValue = dt.Rows[0]["makethua"].ToString();
                    txt_ghichu_I7.Text = dt.Rows[0]["ghichu"].ToString();        
                }
                tl_vaitro_S.DataSource = CreateTableEdit();
                Load_Tree();
            }
        
            tl_vaitro_S.Columns[1].Visible = false;
            tl_vaitro_S.Columns[2].Visible = false;
        }

        #endregion

        #region Event

        private bool checkTrueNodeCha(TreeListNode tn)
        {
            bool flag = true;
            if (tn.ParentNode.Nodes.Count>0)// ton tai nodes cha
            {
                for (int i = 0; i < tn.ParentNode.Nodes.Count; i++)
                {
                    for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                        if (tn.ParentNode.Nodes[i].GetValue(tl_vaitro_S.Columns[j].Name).ToString() != "True" && tl_vaitro_S.Columns[j].Name != "ParentID" && tl_vaitro_S.Columns[j].Name != "ID" && tl_vaitro_S.Columns[j].Name != "mavaitro" && tl_vaitro_S.Columns[j].Name!="Name")
                        {
                            flag = false;
                            break;
                        }
                }
            }
            return flag;
        }
        private bool checkTrueNodeChaColumn(TreeListNode tn,string column)
        {
            bool flag = true;
            if (tn.ParentNode.Nodes.Count > 0)// ton tai nodes cha
            {
                for (int i = 0; i < tn.ParentNode.Nodes.Count; i++)
                {

                    if (tn.ParentNode.Nodes[i].GetValue(column).ToString() != "True" && column != "ParentID" && column != "ID" && column != "mavaitro" && column != "Name")
                        {
                            flag = false;
                            break;
                        }
                }
            }
            return flag;
        }
        private bool checkTrueFalseNodeCon(TreeListNode tn,bool check)
        {
            bool flag = true;
            if (tn.Nodes.Count > 0)// ton tai nodes con
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                        if (tl_vaitro_S.Columns[j].Name != "ParentID" && tl_vaitro_S.Columns[j].Name != "ID" && tl_vaitro_S.Columns[j].Name != "mavaitro" && tl_vaitro_S.Columns[j].Name != "Name")
                        {
                            try
                            {
                                tn.Nodes[i].ParentNode.ParentNode.SetValue(tl_vaitro_S.Columns[j].Name, check);
                            }
                            catch { }
                            tn.Nodes[i].ParentNode.SetValue(tl_vaitro_S.Columns[j].Name, check);
                            tn.Nodes[i].SetValue(tl_vaitro_S.Columns[j].Name, check);
                        }
                    if (tn.Nodes[i].Nodes.Count > 0)
                    {
                        checkTrueFalseNodeCon(tn.Nodes[i], check);
                    }
                }
            }
            else
            {
                for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                    if (tl_vaitro_S.Columns[j].Name != "ParentID" && tl_vaitro_S.Columns[j].Name != "ID" && tl_vaitro_S.Columns[j].Name != "mavaitro" && tl_vaitro_S.Columns[j].Name != "Name")
                    {
                        tn.ParentNode.SetValue(tl_vaitro_S.Columns[j].Name, check);
                        tn.SetValue(tl_vaitro_S.Columns[j].Name, check);
                    }
            }
            return flag;
        }
        private void checkParentCheck(TreeListNode tln)
        {
            try
            {                
                if (checkTrueNodeCha(tln) != true )
                {
                    tln.ParentNode.SetValue(tl_vaitro_S.FocusedColumn, false);
                    tln.ParentNode.SetValue(tl_vaitro_S.Columns["allow_all"], false);
                    tl_vaitro_S.RefreshNode(tln.ParentNode);
                    if (tl_vaitro_S.FocusedNode.Nodes.Count > 0)
                        for (int i = 0; i < tln.Nodes.Count; i++)
                        {
                            for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                                if (tl_vaitro_S.Columns[j].Name ==tl_vaitro_S.FocusedColumn.Name)
                                {
                                    tln.Nodes[i].SetValue(tl_vaitro_S.Columns[j].Name, false);                            
                                }
                        }
                }
                else
                {
                    tln.ParentNode.SetValue(tl_vaitro_S.FocusedColumn, true);
                    tln.ParentNode.SetValue(tl_vaitro_S.Columns["allow_all"], true);
                    tl_vaitro_S.RefreshNode(tln.ParentNode);
                    if (tl_vaitro_S.FocusedNode.Nodes.Count > 0)
                        for (int i = 0; i < tln.Nodes.Count; i++)
                        {
                            for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                                if (tl_vaitro_S.Columns[j].Name == tl_vaitro_S.FocusedColumn.Name)
                                {
                                    tln.Nodes[i].SetValue(tl_vaitro_S.Columns[j].Name, true);
                                }
                        }
                }
                if (tln.ParentNode.Nodes.Count > 0)
                    checkParentCheck(tln.ParentNode);
            }
            catch { }
        }
        private void checkParentCheck(TreeListNode tln,string column)
        {
            try
            {
                if (checkTrueNodeChaColumn(tln,column) != true)
                {
                    tln.ParentNode.SetValue(tl_vaitro_S.FocusedColumn, false);
                    tln.ParentNode.SetValue(tl_vaitro_S.Columns["allow_all"], false);
                    tl_vaitro_S.RefreshNode(tln.ParentNode);
                    if (tl_vaitro_S.FocusedNode.Nodes.Count > 0)
                        for (int i = 0; i < tln.Nodes.Count; i++)
                        {
                            for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                                if (tl_vaitro_S.Columns[j].Name == tl_vaitro_S.FocusedColumn.Name)
                                {
                                    tln.Nodes[i].SetValue(tl_vaitro_S.Columns[j].Name, false);
                                }
                        }
                }
                else
                {
                    tln.ParentNode.SetValue(tl_vaitro_S.FocusedColumn, true);
                    tln.ParentNode.SetValue(tl_vaitro_S.Columns["allow_all"], true);
                    tl_vaitro_S.RefreshNode(tln.ParentNode);
                    if (tl_vaitro_S.FocusedNode.Nodes.Count > 0)
                        for (int i = 0; i < tln.Nodes.Count; i++)
                        {
                            for (int j = 0; j < tl_vaitro_S.Columns.Count; j++)
                                if (tl_vaitro_S.Columns[j].Name == tl_vaitro_S.FocusedColumn.Name)
                                {
                                    tln.Nodes[i].SetValue(tl_vaitro_S.Columns[j].Name, true);
                                }
                        }
                }
                if (tln.ParentNode.Nodes.Count > 0)
                    checkParentCheck(tln.ParentNode);
            }
            catch { }
        }
        private void checkChildCheck(TreeListNode tln)
        {
            try
            {
                checkTrueFalseNodeCon(tln, !(bool)tln.GetValue(tl_vaitro_S.FocusedColumn.Name));
    
            }
            catch { }
        }

        private void chk_allow_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();              
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT   sysPower.IDSubMenu, sysPower.allow_access, sysPower.allow_insert, sysPower.allow_delete, sysPower.allow_edit,  sysPower.allow_print, sysPower.allow_import, sysPower.allow_export, sysPower.mavaitro, sysPower.allow_all, sysPower.id,   sysSubMenu.IDGroupMenu FROM         sysPower INNER JOIN                      sysSubMenu ON sysPower.IDSubMenu = sysSubMenu.IDSubMenu WHERE     (sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') AND (sysSubMenu.IDGroupMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_all"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkTrueFalseNodeCon(tl_vaitro_S.FocusedNode, false);
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkTrueFalseNodeCon(tl_vaitro_S.FocusedNode, (bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                    }
                }
                else
                {
                    if (!((bool)tl_vaitro_S.FocusedNode.GetValue("allow_all")))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkTrueFalseNodeCon(tl_vaitro_S.FocusedNode, !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkTrueFalseNodeCon(tl_vaitro_S.FocusedNode, (bool)tl_vaitro_S.FocusedNode.GetValue("allow_all"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_access_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_access"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_access", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_access");
             
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));

                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_access", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_access");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_access")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_access", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_access", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_access");
           
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_access", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_access", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_access");
            
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_access"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_insert_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();                
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_insert"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_insert", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_insert");
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_insert", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_insert");
                        if(checkTrueNodeCha(tl_vaitro_S.FocusedNode)==false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_insert", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_insert", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_insert");
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_insert", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_insert", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode, "allow_insert");
                        tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                    }
                }
            }
            catch { }
            
        }
        private void chk_allow_edit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_edit"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_edit", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_edit");
              
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_edit", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_edit");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_edit", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_edit", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_edit");
                
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_edit", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_edit", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_edit");
            
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_edit"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_delete_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_delete"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_delete", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_delete");               
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_delete", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_delete");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_deletet"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_delete", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_delete", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_delete");             
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_delete", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_delete", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_delete");
                   
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_delete"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_print_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_print"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_print", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_print");
            
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_insert"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_print", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_print");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_print")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_print", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_print", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_print");
      
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_print", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_print", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_print");
                      
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_print"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_import_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_import"]))
                    {

                        tl_vaitro_S.FocusedNode.SetValue("allow_import", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_import");
          
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_import", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_import");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_import")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_import", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_import", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"import");
              
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_import", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_import", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_import");
    
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_import"));
                    }
                }
            }
            catch { }
        }
        private void chk_allow_export_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("SELECT DISTINCT IDSubMenu, allow_access, allow_insert, allow_delete, allow_edit, allow_print, allow_import, allow_export, mavaitro, allow_all, id FROM         sysPower WHERE     (IDSubMenu = N'" + tl_vaitro_S.FocusedNode.GetValue("ID").ToString() + "') AND (mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "')");
                if (dt.Rows.Count > 0)
                {
                    if (!((bool)dt.Rows[0]["allow_export"]))
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_export", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_export");
              
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                    }
                    else
                    {
                        tl_vaitro_S.FocusedNode.SetValue("allow_export", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_export");
                        if (checkTrueNodeCha(tl_vaitro_S.FocusedNode) == false)
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                    }
                }
                else
                {
                    if (!((bool)(bool)tl_vaitro_S.FocusedNode.GetValue("allow_export")))
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_export", true);
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_export", true);
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_export");
                 
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                    }
                    else
                    {
                        if (tl_vaitro_S.FocusedNode.ParentNode != null)
                        {
                            tl_vaitro_S.FocusedNode.ParentNode.SetValue("allow_export", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                        }
                        tl_vaitro_S.FocusedNode.SetValue("allow_export", !(bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                        tl_vaitro_S.RefreshNode(tl_vaitro_S.FocusedNode);
                        checkParentCheck(tl_vaitro_S.FocusedNode,"allow_export");
                   
                            tl_vaitro_S.FocusedNode.SetValue("allow_all", (bool)tl_vaitro_S.FocusedNode.GetValue("allow_export"));
                    }
                }
            }
            catch { }
        }
    
        private void tl_vaitro_S_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
 
            }
            catch
            {

            }
        }
        //bool main = false;
        private void SetValueToNode(TreeListNode e, DevExpress.XtraTreeList.CellValueChangedEventArgs c)
        {
            if (e.Nodes != null)
            {
                int index = 0, sonode = 0;
                if (e.Nodes.Count > 0)
                {
                    foreach (TreeListNode childnode in e.Nodes)
                    {                      
                        SetValueToNode(childnode, c);
                        e.SetValue(c.Column.Name, !(bool)e.GetValue(c.Column.Name));                   
                        if ((bool)e.GetValue(c.Column.Name) == false)
                        {
                            if (e.ParentNode.HasChildren == true)
                                e.ParentNode.SetValue(c.Column.Name, false);
                        }
                        else
                        {
                            sonode++;
                            if (e.Nodes.Count == sonode)
                                e.ParentNode.SetValue(c.Column.Name, true);
                        }

                    }
                }
                else
                {
                    //if(index==0)
                    e.SetValue(c.Column.Name, !(bool)e.GetValue(c.Column.Name));
                    if ((bool)e.GetValue(c.Column.Name) == false)
                    {
                        if (e.ParentNode.HasChildren == true)
                            e.ParentNode.SetValue(c.Column.Name, false);
                    }
                    else
                    {
                        sonode++;
                        if (e.ParentNode.Nodes.Count == sonode)
                            e.ParentNode.SetValue(c.Column.Name, true);
                    }
                }
            }
        }
        bool main1 = false;
        bool main1_2 = false;
        private void SetValueToNodeAll(TreeListNode e)
        {
            if (e.Nodes != null)
            { 
                if (e.Nodes.Count > 0)
                {
                 if(e.ParentNode.Nodes.Count>0)
                    e.SetValue(tl_vaitro_S.Columns["allow_all"], !main1);
                    SetValueToNodeAll(e.Nodes[0]);
                }
                else
                {
                    //if (index == 0)
                    {
                        for (int i = 0; i < tl_vaitro_S.Columns.Count; i++)
                        {
                            if (tl_vaitro_S.Columns[i].Name != "ID" && tl_vaitro_S.Columns[i].Name != "ParentID" && tl_vaitro_S.Columns[i].Name != "Name" && tl_vaitro_S.Columns[i].Name != "mavaitro")
                            {
                                e.SetValue(tl_vaitro_S.Columns[i].Name, (bool)e.GetValue("allow_all"));
                           
                            }

                        }
                    }           
                }
            }
        }
        private void SetValueToNodeAll1(TreeListNode e)
        {

            if (e.Nodes != null)
            {
                int index = 0;
                if (e.Nodes.Count > 0)
                {
                    foreach (TreeListNode childnode in e.Nodes)
                    {
                        if (index == 0)
                            main1 = (bool)e.GetValue("allow_all");
                        for (int i = 0; i < tl_vaitro_S.Columns.Count; i++)
                        {
                            if (tl_vaitro_S.Columns[i].Name != "ID" && tl_vaitro_S.Columns[i].Name != "ParentID" && tl_vaitro_S.Columns[i].Name != "Name")
                                e.SetValue(tl_vaitro_S.Columns[i].Name, !main1);
                        }
                        main1_2 = true;
                        SetValueToNodeAll(childnode);

                        index++;
                    }

                }
                else
                {
                    //if (index == 0)
                    {
                        for (int i = 0; i < tl_vaitro_S.Columns.Count; i++)
                        {
                            if (tl_vaitro_S.Columns[i].Name != "ID" && tl_vaitro_S.Columns[i].Name != "ParentID" && tl_vaitro_S.Columns[i].Name != "Name")
                            {
                                if (main1_2 == false && index < 1)
                                    main1 = (bool)e.GetValue("allow_all");
                                e.SetValue(tl_vaitro_S.Columns[i].Name, !main1);

                                index++;
                            }

                        }
                    }
                    main1_2 = false;
                }
            }
        }

        private void frm_nhapkhuvuc_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("SELECT     TOP (100) PERCENT sysControls.control_name,  sysControls.type, sysControls.stt, sysControls.form_name, sysPower.allow_insert,   sysPower.allow_delete, sysPower.allow_import, sysPower.allow_export, sysPower.allow_print, sysControls.text_En, sysControls.text_Vi, sysPower.allow_edit, sysControls.stt FROM         sysControls INNER JOIN  sysPower ON sysControls.IDSubMenu = sysPower.IDSubMenu WHERE     (sysControls.form_name = N'" + this.Name + "') AND (sysControls.type = N'SimpleButton') AND (sysPower.mavaitro = N'" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "') ORDER BY sysControls.stt ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (KiemTraQuyen(dt.Rows[i]["control_name"].ToString().Trim(), dt.Rows[i]) || dt.Rows[i]["control_name"].ToString().Trim().Contains("_thoat"))
                {
                    if (e.KeyChar == char.Parse(dt.Rows[i]["stt"].ToString().Trim()))
                    {

                        {
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("luu"))
                            {
                                btnLuu0_Click(sender, e);
                            }
                            if (dt.Rows[i]["control_name"].ToString().Trim().Contains("thoat"))
                            {
                                btnThoat0_Click(sender, e);
                            }

                        }
                    }
                }
            }
        }

        private void grp_ttbb_C_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tl_vaitro_S_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                string mamenu = "";

                mamenu = e.Node.GetValue("ID").ToString();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysPower where IDSubMenu='" + mamenu + "' and " + e.Column.FieldName + "='True' and mavaitro='" + getVaiTroByUser() + "' ");
                if (dt.Rows.Count == 0)
                {
                    e.Node.SetValue(e.Column.Name, false);
                }

            }
            catch { }
        }
        #endregion

        #region ButtonEvent

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnThoat0_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu0_Click(object sender, EventArgs e)
        {
            insert();

        }   

        #endregion

        #region Methods

        private void insert()
        {
            if (!checkInput()) return;
            if (txt_tenvaitro_I2.Text != "")
            {
                if (them == true)
                {
                    Save();
                    //Function.clsFunction.Insert_data(this, (this.Name));

                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_mavaitro_IK1.Text, txt_tenvaitro_I2.Text, SystemInformation.ComputerName.ToString(), "0");
                    LoadTT();
                    //this.Hide();  
                    dxErrorProvider1.ClearErrors();
                    this.Close();
                }
                else
                {
                    //Function.clsFunction.Edit_data(this, this.Name, index);
                    Edit();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_mavaitro_IK1.Name) + " = '" + txt_mavaitro_IK1.Text + "'"));
                    Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_luu_S.Text), txt_mavaitro_IK1.Text, txt_tenvaitro_I2.Text, SystemInformation.ComputerName.ToString(), "1", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    //Function.clsFunction.NhatKyHeg(this.Name, Function.clsFunction.transLate(this.ActiveControl.Text), Function.clsFunction._iduser, Function.clsFunction._iduser, SystemInformation.ComputerName.ToString());
                    //this.Close();\
                    Function.clsFunction.MessageInfo("Lưu thành công","Thông báo");
                    dxErrorProvider1.ClearErrors();
                }
                if (passData != null)
                    passData(true);
            }
            else
            {
                string caption = "";
                DataTable dtMessage = new DataTable();
                dtMessage = APCoreProcess.APCoreProcess.Read("sysMessage");
                //caption = dtMessage.Rows[0]["title" + langues].ToString();
                dxErrorProvider1.SetError(txt_tenvaitro_I2, caption);

            }                
            
        }



        private bool checkInput()
        {
            if (txt_tenvaitro_I2.Text == "")
            {
                Function.clsFunction.MessageInfo("Không được rỗng", "Thông báo");
                txt_tenvaitro_I2.Focus();
                return false;
            }     
            return true;
        }
        private void Save()
        {
            try
            {
                if (lue_mavaitro_I1.EditValue == null)
                {
                    lue_mavaitro_I1.EditValue = txt_mavaitro_IK1.Text;
                }
                APCoreProcess.APCoreProcess.ExcuteSQL("insert into sysRole(mavaitro,tenvaitro,ghichu,makethua,idrecursive) values('" + txt_mavaitro_IK1.Text.Trim() + "',N'" + txt_tenvaitro_I2.Text.Trim() + "',N'" + txt_ghichu_I7.Text.Trim() + "','" + lue_mavaitro_I1.EditValue.ToString().Trim() + "','" + (((DataRowView)lue_mavaitro_I1.Properties.GetDataSourceRowByKeyValue(lue_mavaitro_I1.EditValue))["idrecursive"].ToString().Trim() + txt_mavaitro_IK1.Text + "@") + "')");
                save_tree();
            }
            catch { }
        }
        private void save_tree()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt1 = APCoreProcess.APCoreProcess.Read("sysPower where id=0");
                DataRow dr;
                dt = (DataTable)tl_vaitro_S.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!CheckID_ParentID(dt.Rows[i]["ID"].ToString(), dt))
                    {
                        dr = dt1.NewRow();
                        dr["IDSubMenu"] = dt.Rows[i]["ID"];
                        dr["mavaitro"] = txt_mavaitro_IK1.Text;
                        dr["allow_all"] = (bool)dt.Rows[i]["allow_all"];
                        dr["allow_access"] = (bool)dt.Rows[i]["allow_access"];
                        dr["allow_insert"] = (bool)dt.Rows[i]["allow_insert"];
                        dr["allow_edit"] = (bool)dt.Rows[i]["allow_edit"];
                        dr["allow_delete"] = (bool)dt.Rows[i]["allow_delete"];
                        dr["allow_print"] = (bool)dt.Rows[i]["allow_print"];
                        dr["allow_import"] = (bool)dt.Rows[i]["allow_import"];
                        dr["allow_export"] = (bool)dt.Rows[i]["allow_export"];
                        dr["id"] = Function.clsFunction.autonumber("id","sysPower");
                        dt1.Rows.Add(dr);
                        APCoreProcess.APCoreProcess.Save(dr);
                    }
                }
            }
            catch { }            
        }

        private void Edit()
        {
            APCoreProcess.APCoreProcess.ExcuteSQL("update  sysRole set tenvaitro= N'" + txt_tenvaitro_I2.Text + "',ghichu= N'" + txt_ghichu_I7.Text + "',makethua='" + lue_mavaitro_I1.EditValue.ToString() + "', idrecursive='" + (((DataRowView)lue_mavaitro_I1.Properties.GetDataSourceRowByKeyValue(lue_mavaitro_I1.EditValue))["idrecursive"].ToString().Trim() + txt_mavaitro_IK1.Text + "@") + "' where mavaitro='" + txt_mavaitro_IK1.Text + "'");
           save_edit();
        }
        private void save_edit()
        {
            APCoreProcess.APCoreProcess.ExcuteSQL("delete from sysPower where mavaitro='" + txt_mavaitro_IK1.Text + "'");
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt1 = APCoreProcess.APCoreProcess.Read("sysPower where id=0");
            DataRow dr;
            dt = (DataTable)tl_vaitro_S.DataSource;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!CheckID_ParentID(dt.Rows[i]["ID"].ToString(), dt))
                {
                    dr = dt1.NewRow();
                    dr["id"] = Function.clsFunction.autonumber("id", "sysPower");
                    dr["IDSubMenu"] = dt.Rows[i]["ID"];
                    dr["mavaitro"] = txt_mavaitro_IK1.Text;
                    dr["allow_all"] = (bool)dt.Rows[i]["allow_all"];
                    dr["allow_access"] = (bool)dt.Rows[i]["allow_access"];
                    dr["allow_insert"] = (bool)dt.Rows[i]["allow_insert"];
                    dr["allow_edit"] = (bool)dt.Rows[i]["allow_edit"];
                    dr["allow_delete"] = (bool)dt.Rows[i]["allow_delete"];
                    dr["allow_print"] = (bool)dt.Rows[i]["allow_print"];
                    dr["allow_import"] = (bool)dt.Rows[i]["allow_import"];
                    dr["allow_export"] = (bool)dt.Rows[i]["allow_export"];
                    dt1.Rows.Add(dr);
                    APCoreProcess.APCoreProcess.Save(dr);
                }
            }


        }

        private bool CheckID_ParentID(string ID, DataTable dt)
        {
            bool flag = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ID == dt.Rows[i]["ParentID"].ToString())
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void SavetreeList()
        {           
            //datasỏuce
            string sql_grid = "";
            //côt tổng
            string[] column_summary = new string[] { };
            // Caption column
            string[] caption_col = new string[] { "Chức năng","ID","ParentID","Tất Cả","Truy Cập","Thêm","Sửa","Xóa","In","Nhập","Xuất","mavaitro"};
            // FieldName column,
            string[] fieldname_col = new string[] {"Name","ID","ParentID","allow_all","allow_access","allow_insert","allow_edit","allow_delete","allow_print","allow_import","allow_export","mavaitro" };
            // TextColumn, MemoColumn, LookUpEditColumn, CheckColumn, SpinEditColumn, CalcEditColumn, TimeEditColumn, DateColumn, GridLookupEditColumn
            string[] Style_Column = new string[] { "TextColumn", "TextColumn", "TextColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "CheckColumn", "TextColumn" };
            // Chieu rong column
            string[] Column_Width = new string[] { "200", "10", "10", "80", "80", "80", "80", "80", "80", "80", "80", "80" };
            //AllowFocus
            string[] AllowFocus = new string[] { "False", "False", "False", "True", "True", "True", "True", "True", "True", "True", "True", "False" };
            // Cac cot an, "False"
            string[] Column_Visible = new string[] { "ID","ParentID","mavaitro"};
            // datasource lookupEdit
            string[] sql_lue = new string[] { };
            // Caption lookupEdit
            string[] caption_lue = new string[] { };
            // FieldName lookupEdit
            string[] fieldname_lue = new string[] { };
            // Caption lookupEdit column
            string[,] caption_lue_col = new string[0, 0] ;
            // FieldName lookupEdit column
            string[,] fieldname_lue_col = new string[0, 0];
            //so cot
            int so_cot_lue = 0;
            // FieldName lookupEdit column an
            string[] fieldname_lue_visible = new string[] { };
            // Chi so column lookupEdit search
            int cot_lue_search = 0;

            // datasource GridlookupEdit
            string[] sql_glue = new string[] { };
            // Caption GridlookupEdit
            string[] caption_glue = new string[] { };
            // FieldName GridlookupEdit
            string[] fieldname_glue = new string[] { };
            // Caption GridlookupEdit
            string[,] caption_glue_col = new string[0, 0];
            // FieldName GridlookupEdit
            string[,] fieldname_glue_col = new string[0, 0];
            //so cot
            int so_cot_glue = 2;
            // FieldName GridlookupEdit column an
            string[] fieldname_glue_visible = new string[] { };
            // Chi so column GridlookupEdit search
            int cot_glue_search = 0;

            //save sysGridColumns
            if (statusForm == true)
                Function.clsFunction.Save_sysTreeColumns(this,
                caption_col, fieldname_col, Style_Column, Column_Width, Column_Visible, sql_lue, AllowFocus, caption_lue,
                fieldname_lue, caption_lue_col, fieldname_lue_col, fieldname_lue_visible, cot_lue_search, sql_glue,
                fieldname_glue, caption_glue_col, fieldname_glue_col, fieldname_glue_visible, cot_glue_search, column_summary, sql_grid, tl_vaitro_S.Name);
        }

        private void Load_Tree()
        {
            
            //string text = langues;
            // tl_vaitro_S.Nodes.Clear();
            //SaveGridControls();
            // Datasource
            bool read_Only = false;
            //Hien Navigator
            bool hien_Nav = true;
            // show footer
            bool show_footer = true;

            // Hien thị Gridview
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysGridColumns where form_name ='" + (this.Name) + "'");
            //try
            {
                ControlDev.FormatControls.Controls_in_Tree(tl_vaitro_S, read_Only, hien_Nav,
                dt.Rows[0]["column_summary"].ToString().Split('/'), show_footer,
                dt.Rows[0]["sql_grid"].ToString(), dt.Rows[0]["caption"].ToString().Split('/'),
                dt.Rows[0]["field_name"].ToString().Split('/'),
                dt.Rows[0]["Column_Width"].ToString().Split('/'), dt.Rows[0]["Allow_Focus"].ToString().Split('/'),
                dt.Rows[0]["Style_Column"].ToString().Split('/'), dt.Rows[0]["Column_Visible"].ToString().Split('/'),
                dt.Rows[0]["sql_lue"].ToString().Split('/'), dt.Rows[0]["caption_lue"].ToString().Split('/'),
                dt.Rows[0]["fieldname_lue"].ToString().Split('/'), Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["fieldname_lue_col"].ToString(), "@", "/"),
                Function.clsFunction.ConvertStringToArrayN(dt.Rows[0]["caption_lue_col"].ToString(), "@", "/"), dt.Rows[0]["fieldname_lue_visible"].ToString().Split('/'),
                int.Parse(dt.Rows[0]["cot_lue_search"].ToString()), dt.Rows[0]["sql_glue"].ToString().Split('/'),
                dt.Rows[0]["caption_glue"].ToString().Split('/'), dt.Rows[0]["fieldname_glue"].ToString().Split('/'),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["fieldname_glue_col"].ToString()),
                Function.clsFunction.ConVertStringToArray2(dt.Rows[0]["caption_glue_col" ].ToString()),
                dt.Rows[0]["fieldname_glue_visible"].ToString().Split('/'), int.Parse(dt.Rows[0]["cot_glue_search"].ToString()));
                //Hien Navigator      

            }
            tl_vaitro_S.KeyFieldName = "ID";
            tl_vaitro_S.ParentFieldName = "ParentID";
            tl_vaitro_S.EndUnboundLoad();
            tl_vaitro_S.RefreshDataSource();
            tl_vaitro_S.EndUpdate();
            tl_vaitro_S.CollapseAll();
            //tl_vaitro_S.PopulateColumns();
            tl_vaitro_S.ExpandAll();
            //catch (Exception ex)
            {
                //MessageBox.Show("co loi");
            }
        }

        private void loadTreePhanQuyen()
        {
            try
            {
                tl_vaitro_S.DataSource = CreateTableEdit();              
                tl_vaitro_S.EndUnboundLoad();
                tl_vaitro_S.CollapseAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void setVaiTro()
        {
            string[] fieldname = new string[] { "mavaitro", "tenvaitro" };
            string[] caption = new string[] { "Mã VT", "Tên VT" };
            ControlDev.FormatControls.LoadLookupEdit(lue_mavaitro_I1, "select sysRole.mavaitro,tenvaitro, idrecursive from sysRole, sysUser where sysRole.mavaitro=sysUser.mavaitro and sysUser.userid='" + Function.clsFunction._iduser + "'", "tenvaitro", "mavaitro", caption, fieldname, statusForm, "", this.Name);
        }
        private void loadVaiTro()
        {
            //DataTable dt_sysControls = new DataTable();
            //dt_sysControls = APCoreProcess.APCoreProcess.Read("sysControls where form_name='" + this.Name + "' and control_name='" + lue_mavaitro_I1.Name + "'");
            //string[] fieldname = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["fieldname_col_lue"].ToString(), "/");
            //string[] caption = Function.clsFunction.ConvertToArray(dt_sysControls.Rows[0]["caption_col_lue" + langues].ToString(), "/");
            //string sql = dt_sysControls.Rows[0]["sql_lue"].ToString();
            //string scaption = dt_sysControls.Rows[0]["caption" + langues].ToString();
            //string sfieldname = dt_sysControls.Rows[0]["field_name"].ToString();
            //string image = dt_sysControls.Rows[0]["image"].ToString();
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                //ControlDev.FormatControls.LoadLookupEditNoneParameter(lue_mavaitro_I1, "load_" + lue_mavaitro_I1.Name + "_" + this.Name, "create proc   " + "load_" + lue_mavaitro_I1.Name + "_" + this.Name + " as begin select mavaitro,tenvaitro,idrecursive from sysRole where mavaitro like '%" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "%'  end", "mavaitro", "tenvaitro");
                ControlDev.FormatControls.LoadLookupEditParameter(lue_mavaitro_I1, "load_" + lue_mavaitro_I1.Name + "_" + this.Name, "create proc   " + "load_" + lue_mavaitro_I1.Name + "_" + this.Name + " (@IDRole as nvarchar(10)) as begin     Select mavaitro, tenvaitro, makethua, idrecursive  From sysRole   Where idrecursive like  @IDRole   end", new string[1, 2] { { "IDRole", "%" + Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) + "%" } }, "mavaitro", "tenvaitro");
            }
            else
            {
                ControlDev.FormatControls.LoadLookupEditNoneParameter(lue_mavaitro_I1, "load_" + lue_mavaitro_I1.Name + "_" + this.Name, "  select mavaitro,tenvaitro,idrecursive from sysRole where idrecursive like '%"+ Function.clsFunction.getVaiTroByUser(Function.clsFunction._iduser) +"%'  ", "mavaitro", "tenvaitro");
            }
            lue_mavaitro_I1.EditValue = "VT000001";
        }
        private string getVaiTroByUser()
        {
            string vaitro = "";
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("select mavaitro from sysUser where userid='" + Function.clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
            {
                vaitro = dt.Rows[0][0].ToString();
            }
            return vaitro;
        }

        #endregion

        private void lue_mavaitro_I1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
               //LoadTT();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }                            

    }
}