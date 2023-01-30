using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_PURCHASE.Presentation
{
    public partial class frm_panel_SH : DevExpress.XtraEditors.XtraForm
    {
        public frm_panel_SH()
        {
            InitializeComponent();
        }

        #region FormEvent
        bool _load = false;
        private void frm_panel_Load(object sender, EventArgs e)
        {
             loadPurchase();            
        }

        #endregion

        #region Methods



        private void getValue(bool value)
        {
            if (value == true)
            {             
                this.Close();
            }
        }

        private void loadPurchase()
        {
            try
            {
                pn_main_C.Controls.Clear();
                pn_main_C.Dock = DockStyle.Fill;
                SOURCE_FORM_PURCHASE.Presentation.frm_PURCHASE_S frm = new SOURCE_FORM_PURCHASE.Presentation.frm_PURCHASE_S();
                frm.passData = new frm_PURCHASE_S.PassData(getValue);
                frm.TopLevel = false;                
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                pn_main_C.Controls.Add(frm);
                frm.Show();                
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadProvider()
        {
            try
            {
                pn_main_C.Controls.Clear();
                SOURCE_FORM_DMPROVIDERS.Presentation.frm_DMPROVIDER_SH frm = new SOURCE_FORM_DMPROVIDERS.Presentation.frm_DMPROVIDER_SH();
                frm.TopLevel = false;               
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                pn_main_C.Controls.Add(frm);
                frm.Show();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadWarehouse()
        {
            try
            {
                pn_main_C.Controls.Clear();
                SOURCE_FORM_DMWAREHOUSE.Presentation.frm_DMWAREHOUSE_SH frm = new SOURCE_FORM_DMWAREHOUSE.Presentation.frm_DMWAREHOUSE_SH();
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                pn_main_C.Controls.Add(frm);
                frm.Show();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }

        private void loadUnit()
        {
            try
            {                         
                pn_main_C.Controls.Clear();
                SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_SH frm = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_SH();
                frm.TopLevel = false;             
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                pn_main_C.Controls.Add(frm);
                frm.Show();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        private void loadCommodity()
        {
            try
            {                           
                pn_main_C.Controls.Clear();
                SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_SH frm = new SOURCE_FORM_DMCOMMODITY.Presentation.frm_DMCOMMODITY_SH();
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                pn_main_C.Controls.Add(frm);
                frm.Show();
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }

        }

        #endregion

        #region Button

        private void nav_nhaphang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadPurchase();
        }

        private void nav_dmnhacungcap_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadProvider();
        }

        private void nav_kho_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadWarehouse();
        }

        private void nav_donvitinh_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadUnit();
        }

        private void nav_mathang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadCommodity();
        }

        #endregion

        private void frm_panel_SH_Resize(object sender, EventArgs e)
        {
            pn_main_C.Dock = DockStyle.Fill;
        }

 



    }
}