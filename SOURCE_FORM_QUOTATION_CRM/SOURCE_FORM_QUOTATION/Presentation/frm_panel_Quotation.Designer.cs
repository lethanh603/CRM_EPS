namespace SOURCE_FORM_QUOTATION.Presentation
{
    partial class frm_panel_Quotation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_panel_Quotation));
            this.dock_main_C = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dock_left_C = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nav_nhaphang_C = new DevExpress.XtraNavBar.NavBarGroup();
            this.nav_list_S = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_nhaphang = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_thongke_C = new DevExpress.XtraNavBar.NavBarGroup();
            this.nav_chungtu_C = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_hanghoa = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_nhacungcap = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_danhmuc = new DevExpress.XtraNavBar.NavBarGroup();
            this.nav_mathang = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_dmnhacungcap = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_kho = new DevExpress.XtraNavBar.NavBarItem();
            this.nav_donvitinh = new DevExpress.XtraNavBar.NavBarItem();
            this.pn_main_C = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dock_main_C)).BeginInit();
            this.dock_left_C.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // dock_main_C
            // 
            this.dock_main_C.DockMode = DevExpress.XtraBars.Docking.Helpers.DockMode.Standard;
            this.dock_main_C.Form = this;
            this.dock_main_C.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dock_left_C});
            this.dock_main_C.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dock_left_C
            // 
            this.dock_left_C.Controls.Add(this.dockPanel1_Container);
            this.dock_left_C.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dock_left_C.FloatSize = new System.Drawing.Size(200, 198);
            this.dock_left_C.ID = new System.Guid("4c3f7297-0be7-4c89-ba45-1759b2b92997");
            this.dock_left_C.Location = new System.Drawing.Point(0, 0);
            this.dock_left_C.Margin = new System.Windows.Forms.Padding(0);
            this.dock_left_C.Name = "dock_left_C";
            this.dock_left_C.OriginalSize = new System.Drawing.Size(148, 200);
            this.dock_left_C.Size = new System.Drawing.Size(148, 490);
            this.dock_left_C.Text = "Chức năng";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(140, 463);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nav_nhaphang_C;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nav_nhaphang_C,
            this.nav_thongke_C,
            this.nav_danhmuc});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nav_nhaphang,
            this.nav_chungtu_C,
            this.nav_hanghoa,
            this.nav_nhacungcap,
            this.nav_mathang,
            this.nav_dmnhacungcap,
            this.nav_kho,
            this.nav_donvitinh,
            this.nav_list_S});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(140, 463);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nav_nhaphang_C
            // 
            this.nav_nhaphang_C.Caption = "Nhu cầu";
            this.nav_nhaphang_C.Expanded = true;
            this.nav_nhaphang_C.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.nav_nhaphang_C.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_list_S),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_nhaphang)});
            this.nav_nhaphang_C.Name = "nav_nhaphang_C";
            // 
            // nav_list_S
            // 
            this.nav_list_S.Caption = "Danh sách";
            this.nav_list_S.LargeImage = ((System.Drawing.Image)(resources.GetObject("nav_list_S.LargeImage")));
            this.nav_list_S.LargeImageIndex = 0;
            this.nav_list_S.Name = "nav_list_S";
            this.nav_list_S.SmallImage = ((System.Drawing.Image)(resources.GetObject("nav_list_S.SmallImage")));
            this.nav_list_S.SmallImageIndex = 0;
            this.nav_list_S.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_list_S_LinkClicked);
            // 
            // nav_nhaphang
            // 
            this.nav_nhaphang.Caption = "Nhu cầu";
            this.nav_nhaphang.LargeImage = global::SOURCE_FORM_QUOTATION.Properties.Resources.new_page;
            this.nav_nhaphang.LargeImageIndex = 1;
            this.nav_nhaphang.Name = "nav_nhaphang";
            this.nav_nhaphang.SmallImageIndex = 1;
            this.nav_nhaphang.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_nhaphang_LinkClicked);
            // 
            // nav_thongke_C
            // 
            this.nav_thongke_C.Caption = "Bảng kê";
            this.nav_thongke_C.Expanded = true;
            this.nav_thongke_C.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_chungtu_C),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_hanghoa),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_nhacungcap)});
            this.nav_thongke_C.Name = "nav_thongke_C";
            this.nav_thongke_C.Visible = false;
            // 
            // nav_chungtu_C
            // 
            this.nav_chungtu_C.Caption = "Chứng từ";
            this.nav_chungtu_C.Name = "nav_chungtu_C";
            // 
            // nav_hanghoa
            // 
            this.nav_hanghoa.Caption = "Hàng hóa";
            this.nav_hanghoa.Name = "nav_hanghoa";
            // 
            // nav_nhacungcap
            // 
            this.nav_nhacungcap.Caption = "Nhà cung cấp";
            this.nav_nhacungcap.Name = "nav_nhacungcap";
            // 
            // nav_danhmuc
            // 
            this.nav_danhmuc.Caption = "Danh mục";
            this.nav_danhmuc.Expanded = true;
            this.nav_danhmuc.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.nav_danhmuc.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_mathang),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_dmnhacungcap),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_kho),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nav_donvitinh)});
            this.nav_danhmuc.Name = "nav_danhmuc";
            // 
            // nav_mathang
            // 
            this.nav_mathang.Caption = "Sản phẩm";
            this.nav_mathang.LargeImage = global::SOURCE_FORM_QUOTATION.Properties.Resources.new_page;
            this.nav_mathang.Name = "nav_mathang";
            this.nav_mathang.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_mathang_LinkClicked);
            // 
            // nav_dmnhacungcap
            // 
            this.nav_dmnhacungcap.Caption = "Khách hàng";
            this.nav_dmnhacungcap.LargeImage = global::SOURCE_FORM_QUOTATION.Properties.Resources.business_user_info;
            this.nav_dmnhacungcap.Name = "nav_dmnhacungcap";
            this.nav_dmnhacungcap.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_dmnhacungcap_LinkClicked);
            // 
            // nav_kho
            // 
            this.nav_kho.Caption = "Kho";
            this.nav_kho.LargeImage = global::SOURCE_FORM_QUOTATION.Properties.Resources.home;
            this.nav_kho.Name = "nav_kho";
            this.nav_kho.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_kho_LinkClicked);
            // 
            // nav_donvitinh
            // 
            this.nav_donvitinh.Caption = "Đơn vị tính";
            this.nav_donvitinh.LargeImage = global::SOURCE_FORM_QUOTATION.Properties.Resources.computer_network;
            this.nav_donvitinh.Name = "nav_donvitinh";
            this.nav_donvitinh.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nav_donvitinh_LinkClicked);
            // 
            // pn_main_C
            // 
            this.pn_main_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_main_C.Location = new System.Drawing.Point(148, 0);
            this.pn_main_C.Margin = new System.Windows.Forms.Padding(0);
            this.pn_main_C.Name = "pn_main_C";
            this.pn_main_C.Size = new System.Drawing.Size(686, 490);
            this.pn_main_C.TabIndex = 7;
            // 
            // frm_panel_Quotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 490);
            this.Controls.Add(this.pn_main_C);
            this.Controls.Add(this.dock_left_C);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frm_panel_Quotation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_panel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dock_main_C)).EndInit();
            this.dock_left_C.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dock_main_C;
        private DevExpress.XtraBars.Docking.DockPanel dock_left_C;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nav_nhaphang_C;
        private DevExpress.XtraNavBar.NavBarItem nav_nhaphang;
        private DevExpress.XtraNavBar.NavBarGroup nav_thongke_C;
        private DevExpress.XtraNavBar.NavBarGroup nav_danhmuc;
        private DevExpress.XtraNavBar.NavBarItem nav_chungtu_C;
        private DevExpress.XtraNavBar.NavBarItem nav_hanghoa;
        private DevExpress.XtraNavBar.NavBarItem nav_nhacungcap;
        private DevExpress.XtraNavBar.NavBarItem nav_mathang;
        private DevExpress.XtraNavBar.NavBarItem nav_dmnhacungcap;
        private DevExpress.XtraNavBar.NavBarItem nav_kho;
        private DevExpress.XtraNavBar.NavBarItem nav_donvitinh;
        private System.Windows.Forms.Panel pn_main_C;
        private DevExpress.XtraNavBar.NavBarItem nav_list_S;
    }
}