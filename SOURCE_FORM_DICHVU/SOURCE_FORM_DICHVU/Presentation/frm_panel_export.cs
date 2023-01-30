using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_DICHVU.Presentation
{
    public partial class frm_panel_export : DevExpress.XtraEditors.XtraForm
    {
        public frm_panel_export()
        {
            InitializeComponent();
        }

        #region FormEvent

        private void frm_panel_Load(object sender, EventArgs e)
        {
            loadForm();

        }

        #endregion

        #region Methods
        private void loadForm()
        {
            try
            {
                pn_main_C.Controls.Clear();
                frm_EXPORT_S frm = new frm_EXPORT_S();
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.passData = new frm_EXPORT_S.PassData(getValue);
                pn_main_C.Controls.Add(frm);
                frm.Show();
            }
            catch { }
        }

        private void getValue(bool value)
        {
            if (value == true)
            {
                this.Close();
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
            catch { }

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
            catch { }

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
            catch { }

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
            catch { }

        }
        #endregion

        #region Button

        private void nav_nhaphang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            loadForm();
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



    }
}