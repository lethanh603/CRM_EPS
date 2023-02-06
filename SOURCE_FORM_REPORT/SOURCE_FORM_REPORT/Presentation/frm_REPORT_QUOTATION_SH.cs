using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using Function;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using System.Security.Cryptography;
using Newtonsoft.Json;
////F1 thêm, F2 sửa, F3 Xóa, F4 Lưu & Thêm, F5 Lưu & thoát, F6 In, F7 Nhập, F8 Xuất F9 Thoát, F10 Tim,F11 lam mới
namespace SOURCE_FORM_REPORT.Presentation
{
    public partial class frm_REPORT_QUOTATION_SH : DevExpress.XtraEditors.XtraForm
    {
        #region Contructor
        public frm_REPORT_QUOTATION_SH()
        {
            InitializeComponent();
        }        

        
    #endregion
        
        #region Var

        public bool statusForm = false;
        public string _sign = "KH";
        private int row_focus = -1;
        ControlsDev.clsGridLine clsG = new ControlsDev.clsGridLine();
        DataTable dts = new DataTable();
        private string arrCaption;
        private string arrFieldName;
        PopupMenu menu = new PopupMenu();

        #endregion

        #region FormEvent

        private void frm_DMCUSTOMERS_SH_Load(object sender, EventArgs e)
        {
            dte_fromdate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dte_todate_S.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            loadGridLookupStatus();
            loadGridLookupEmp();
            loadGridLookupBranch();
            loadGridLookupTeam();
            ControlDev.FormatControls.setContainsFilter(gv_cskh_C);
            //ControlDev.FormatControls.setContainsFilter(gv_device_C);
            ControlDev.FormatControls.setContainsFilter(bgv_list_C);
        }
        
        
        #endregion

        #region ButtonEvent


        #endregion

        #region Event

        private bool validate()
        {
            if (Convert.ToDateTime( dte_fromdate_S.EditValue) > Convert.ToDateTime(dte_todate_S.EditValue))
            {
                Function.clsFunction.MessageInfo("Thông báo","Từ ngày phải <= đến ngày");
                return false;
            }
            return true;
        }

        private void btn_search_S_Click(object sender, EventArgs e)
        {
            if (validate() == false)
            {
                return;
            }

            try
            {
                string sql = "";
                sql = @"
                SELECT E.IDEMP, E.StaffName,
                count(QS.quotationno) count_all,
                sum ( case when QS.idstatusquotation ='ST000006' then 1 else 0 end ) count_nc_ch,
                sum ( case when QS.idstatusquotation ='ST000007' then 1 else 0 end ) count_nc_tn,
                sum ( case when QS.idstatusquotation ='ST000008' then 1 else 0 end ) count_nc_ktn,
                sum ( case when QS.idstatusquotation ='ST000001' then 1 else 0 end ) count_hg,
                sum ( case when QS.idstatusquotation ='ST000002' then 1 else 0 end ) count_bg,
                sum ( case when QS.idstatusquotation ='ST000003' then 1 else 0 end ) count_tl,
                sum ( case when QS.idstatusquotation ='ST000004' then 1 else 0 end ) count_tc,
                sum ( case when QS.idstatusquotation ='ST000005' then 1 else 0 end ) count_tb

                FROM QUOTATION QS with(nolock)
                INNER JOIN EMPLOYEES E with(nolock) ON  expresion_join
                WHERE QS.dateimport between 'from_date' and 'to_date' {0}
                GROUP BY E.IDEMP, E.StaffName
                --select * from DMSTATUSQUOTATION
                ";

                string expEmp = " AND (E.idemp like '%" + glue_IDEMP_I1.EditValue.ToString().Trim() + "%' or  (charindex('" + clsFunction.GetIDEMPByUser() + "',E.idrecursive) >0  ) )  ";

                
                sql = string.Format(sql, expEmp);
                sql = sql.Replace("from_date", Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd")).Replace("to_date", Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd"));
                sql = sql.Replace("expresion_join", rg_auth_S.EditValue.ToString() == "1" ? "QS.IDEMP = E.IDEMP" : "QS.idemppo = E.IDEMP");
                //MessageBox.Show(sql);
                gct_list_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
                //loadCSKH("", "");
                //loadDevice("");
            }
            catch (Exception ex) { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                XtraReport report = null;


                report = XtraReport.FromFile(Application.StartupPath + "\\Report\\frx_cskh_new.repx", true);

                clsFunction.BindDataControlReport(report);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = @"with tem as 
                (
                SELECT E.idemp, E.StaffName, E.status,
                thue_bao,
                khong_nghe_may,
                so_dien_thoai_sai,
                nghe_may_hen_lai,
                dang_cham_soc,
                can_nhap_nhu_cau,
                can_nhap_thong_tin_thiet_bi_phu_tung,
                nghe_may_hen_lai2,
                dang_cham_soc2,
                can_nhap_thong_tin_thiet_bi_phu_tung2,
                isnull(avg(  DV.quantity),0) thiet_bi,
                isnull(avg(VX.quantity_voxe),0)  vo_xe,
                isnull(avg(BD.quantity),0) binh_dien,
                isnull(avg(VX.quantity_bacdan),0)  bac_dan

                FROM EMPLOYEES E with(nolock)
                LEFT JOIN 
		        (
							
		        select idemp, 
		        sum( (case when PC.idstatus='ST000016' then 1 else 0 end )) thue_bao,
		        sum(case when PC.idstatus='ST000014' then 1 else 0 end ) khong_nghe_may,
		        sum(case when PC.idstatus='ST000017' then 1 else 0 end ) so_dien_thoai_sai,
		        sum((case when PC.idstatus='ST000011' then 1 else 0 end )) nghe_may_hen_lai,
		        sum(case when PC.idstatus='ST000012' then 1 else 0 end ) dang_cham_soc,
		        sum(case when PC.idstatus='ST000013' then 1 else 0 end ) can_nhap_nhu_cau,
		        sum(case when PC.idstatus='ST000015' then 1 else 0 end ) can_nhap_thong_tin_thiet_bi_phu_tung
		        from PLANCRM PC with(nolock)    where  cast( PC.datecontact as date) between 'from_date' and 'to_date' {1}
		        group by idemp
		        ) PC
		        ON E.IDEMP = PC.idemp 
          
                LEFT JOIN
		        (
		        select idemp, 
		        sum((case when PC.idstatus='ST000011' then 1 else 0 end )) nghe_may_hen_lai2,
		        sum(case when PC.idstatus='ST000012' then 1 else 0 end ) dang_cham_soc2,
		        sum(case when PC.idstatus='ST000015' then 1 else 0 end ) can_nhap_thong_tin_thiet_bi_phu_tung2
		        from 
		        (
			        select idemp, 
		        idstatus, idcustomer, cast(datecontact as date) datecontact
		        from PLANCRM PC with(nolock)    where  cast( PC.datecontact as date) between 'from_date' and 'to_date'  {2}
		        group by idemp, idstatus, idcustomer, cast(datecontact as date)
		        )
		        PC    where  cast( PC.datecontact as date) between 'from_date' and 'to_date' 
		        group by idemp
	
		        )PC2
		        ON PC2.idemp = E.IDEMP  
                
                LEFT JOIN 
		        (select  sum(DV.quantity) quantity, E.idemp from DEVICEINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
			        where DV.createdDate between 'from_date' and 'to_date' 
			        group by E.idemp) DV 
                ON  E.idemp = DV.idemp 
                LEFT JOIN 
		        (
		        select sum(case when bacdanvoxe =0 then DV.quantity else 0 end) quantity_voxe,  sum(case when bacdanvoxe =1 then DV.quantity else 0 end) quantity_bacdan,
		        E.idemp from VOXEINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
			        where DV.createdDate between 'from_date' and 'to_date' 
			        group by  E.idemp
			        )
		        VX 
                ON  VX.idemp = E.idemp
                LEFT JOIN (select  sum(DV.quantity) quantity, E.idemp from BINHDIENINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
			        where DV.createdDate between 'from_date' and 'to_date' 
			        group by  E.idemp) BD 
                ON  E.idemp = BD.idemp

                WHERE E.status=1 {0}
                GROUP BY E.idemp, E.StaffName, E.status,thue_bao,
                khong_nghe_may,
                so_dien_thoai_sai,
                nghe_may_hen_lai,
                dang_cham_soc,
                can_nhap_nhu_cau,
                can_nhap_thong_tin_thiet_bi_phu_tung,
                nghe_may_hen_lai2,
                dang_cham_soc2,
                can_nhap_thong_tin_thiet_bi_phu_tung2,idrecursive
                )
                select idemp, staffName, thue_bao, khong_nghe_may,so_dien_thoai_sai,nghe_may_hen_lai,dang_cham_soc,can_nhap_nhu_cau,can_nhap_thong_tin_thiet_bi_phu_tung,
                (thue_bao+khong_nghe_may+so_dien_thoai_sai+nghe_may_hen_lai+dang_cham_soc+can_nhap_nhu_cau+can_nhap_thong_tin_thiet_bi_phu_tung) tong,
                --(nghe_may_hen_lai+dang_cham_soc+can_nhap_nhu_cau+can_nhap_thong_tin_thiet_bi_phu_tung) hop_le,
                (nghe_may_hen_lai2+dang_cham_soc2+can_nhap_nhu_cau+can_nhap_thong_tin_thiet_bi_phu_tung2) hop_le,
                    thiet_bi,vo_xe,binh_dien,bac_dan,N'{3}' sign, '{4}' date, N'{5}' dept
                from tem
                ";

                string expEmp = " AND (E.idemp like '%" + glue_IDEMP_I1.EditValue.ToString().Trim() + "%' or  (charindex('" + clsFunction.GetIDEMPByUser() + "',E.idrecursive) >0  ) )  ";
                string expStatus = " AND PC.idstatus like '%" + glue_iddepartment_I1.EditValue.ToString().Trim() + "%'";
                string expStatus2 = " AND PC.idstatus like '%" + glue_iddepartment_I1.EditValue.ToString().Trim() + "%'";
                string sDate = Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(dte_todate_S.EditValue).ToString("dd/MM/yyyy");
                string sign = clsFunction.GetEmpNameByUser();
                string dept = "";
                try
                {
                    dept = APCoreProcess.APCoreProcess.Read("select department from DMDEPARTMENT D with(nolock) inner join EMPLOYEES E with(nolock) ON E.iddepartment = D.iddepartment where e.IDEMP='" + clsFunction.GetIDEMPByUser() + "'").Rows[0][0].ToString();
                }
                catch (Exception ex)
                {
                    dept = "No department";
                }

                sql = string.Format(sql, expEmp, expStatus, expStatus, sign, sDate, dept);
                sql = sql.Replace("from_date", Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd")).Replace("to_date", Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd"));
                //MessageBox.Show(sql);

                dt = APCoreProcess.APCoreProcess.Read(sql);
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                    report.DataSource = ds;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    for (int i = 0; i < report.Parameters.Count; i++)
                    {
                        report.Parameters[i].Visible = false;
                    }

                    tool.ShowPreviewDialog();
                }
                else
                {
                    clsFunction.MessageInfo("Thông báo", "Không tìm thấy dữ liệu hoặc chưa nhập thông tin hàng hóa, vui lòng kiểm tra lại.");
                }
            }
            catch { }
        }

        private void loadDevice(string idEmp)
        {
            if (idEmp == "")
            {
                idEmp = clsFunction.GetIDEMPByUser();
            }
            string sql = @"
                            SELECT PC.idemp, E.StaffName, PC.idcustomer, C.customer,C.date1 ngay_tao,    
							case when C.idgroup =0 then 'Công ty' when C.idgroup=1 then 'Đại lý' else 'Khách lẻ' end doi_tuong   , 
							C.address,              
							isnull(avg(DV.quantity),0) thiet_bi,
							isnull(avg( VX.quantity_voxe),0) vo_xe,
							isnull(avg(BD.quantity),0) binh_dien,
							isnull(avg(VX.quantity_bacdan),0) bac_dan

                            FROM EMPLOYEES E with(nolock) 
                            LEFT JOIN EMPCUS PC with(nolock) ON E.IDEMP = PC.idemp
                            LEFT JOIN DMCUSTOMERS C with(nolock) ON PC.idcustomer = C.idcustomer
							LEFT JOIN DMFIELDS F with(nolock) ON F.idfields = C.idfields
							LEFT JOIN 
							(select DV.idcustomer, sum(DV.quantity) quantity, E.idemp from DEVICEINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
								where DV.createdDate between 'from_date' and 'to_date' 
								group by dv.idcustomer, E.idemp) DV 
                            ON  PC.idemp = DV.idemp and PC.idcustomer = DV.idcustomer
                            LEFT JOIN 
							(
							select DV.idcustomer,  sum(case when bacdanvoxe =0 then DV.quantity else 0 end) quantity_voxe,  sum(case when bacdanvoxe =1 then DV.quantity else 0 end) quantity_bacdan,
							E.idemp from VOXEINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
								where DV.createdDate between  'from_date' and 'to_date' 
								group by dv.idcustomer, E.idemp 
								)
							VX 
                            ON  VX.idemp = PC.idemp and PC.idcustomer = VX.idcustomer
                            LEFT JOIN (select DV.idcustomer, sum(DV.quantity) quantity, E.idemp from BINHDIENINFO DV with(nolock) inner join EMPCUS E with(nolock) on DV.idcustomer = E.idcustomer
								where DV.createdDate between 'from_date' and 'to_date' 
								group by dv.idcustomer, E.idemp) BD 
                            ON  PC.idemp = BD.idemp and PC.idcustomer = BD.idcustomer
                            WHERE 1=1 {0}
							GROUP BY
							PC.idemp, E.StaffName, PC.idcustomer, C.customer,C.date1 ,    
							case when C.idgroup =0 then 'Công ty' when C.idgroup=1 then 'Đại lý' else 'Khách lẻ' end    , 
							C.address,  F.fieldname
                    HAVING (isnull(avg(DV.quantity),0) +		isnull(avg( VX.quantity_voxe),0) +		isnull(avg(BD.quantity),0) +							isnull(avg(VX.quantity_bacdan),0)) >0
                            ";

            string expEmp = " AND ( E.idemp like '%" + idEmp + "%' )";

            sql = string.Format(sql, expEmp);
            sql = sql.Replace("from_date", Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd")).Replace("to_date", Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd"));
            //gct_device_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }

        private void loadCSKH(string idEmp, string idStatus)
        {
            if (idEmp == "")
            {
                idEmp = clsFunction.GetIDEMPByUser();
            }
            string sql = @"
                            SELECT distinct PC.idemp,C.idcustomer, count(PC.idcustomer) count,  E.StaffName, C.customer,C.date1 ngay_tao,    case when C.idgroup =0 then 'Công ty' when C.idgroup=1 then 'Đại lý' else 'Khách lẻ' end doi_tuong   , 
                            C.address,  F.fieldname 
                            FROM PLANCRM PC with(nolock)
                            INNER JOIN EMPLOYEES E with(nolock) ON E.IDEMP = PC.idemp
                            INNER JOIN DMCUSTOMERS C with(nolock) ON PC.idcustomer = C.idcustomer
							INNER JOIN DMFIELDS F with(nolock) ON F.idfields = C.idfields

                            WHERE cast( PC.datecontact as date) between '{0}' and '{1}' {2} {3}
				            GROUP BY 
                            PC.idemp, E.StaffName,  C.customer,C.date1 ,    case when C.idgroup =0 then 'Công ty' when C.idgroup=1 then 'Đại lý' else 'Khách lẻ' end    , 
                            C.address,  F.fieldname ,C.idcustomer
                            ";

            string expEmp = " AND (PC.idemp like '%" + idEmp + "%'   )";
            string expStatus = " AND PC.idstatus like '%" + idStatus + "%'";

            sql = string.Format(sql, Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd"), Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd"), expEmp, expStatus);

            gct_cskh_C.DataSource = APCoreProcess.APCoreProcess.Read(sql);
        }

        #region Methods



        private void loadGridLookupStatus()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Bộ phận" };
                string[] fieldname = new string[] { "iddepartment", "department" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_iddepartment_I1, "select '' iddepartment, 'All' department union select  iddepartment, department from DMDEPARTMENT with(nolock) where status =1", "department", "iddepartment", caption, fieldname, this.Name, glue_iddepartment_I1.Width);
            }
            catch { }
        }

        private void loadGridLookupBranch()
        {
            try
            {
                string[] caption = new string[] { "Mã CN", "Chi nhánh" };
                string[] fieldname = new string[] { "idstore", "store" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idstore_Ik1, "select '' idstatus, 'All' statusname union select  idstatus, statusname from DMSTATUS with(nolock) where status =1", "store", "idstore", caption, fieldname, this.Name, glue_idstore_Ik1.Width);
                glue_idstore_Ik1.EditValue = "CH000002";

            }
            catch { }
        }

        private void loadGridLookupTeam()
        {
            try
            {
                string[] caption = new string[] { "Mã", "Đội nhóm" };
                string[] fieldname = new string[] { "idteam", "teamname" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idteam_IK1, "select '' idteam, 'All' teamname union select  idteam, teamname from DMSTEAM with(nolock) where status =1", "teamname", "idteam", caption, fieldname, this.Name, glue_idteam_IK1.Width);
                glue_idteam_IK1.EditValue = "EMP000008";

            }
            catch { }
        }

        private void loadGridLookupEmp()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên Nhân Viên" };
                string[] fieldname = new string[] { "idemp", "staffname" };
                string department = "";
                department = glue_iddepartment_I1.EditValue.ToString();
                if (clsFunction.checkAdmin())
                {
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select '' idemp, 'All' staffname union select idemp,staffname from employees  where status=1 and iddepartment like '%"+department+"%'", "staffname", "idemp", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
                }
                else
                {
                    ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select idemp,staffname from employees where status=1  and iddepartment like '%"+department+"%' and CHARINDEX('" + clsFunction.GetIDEMPByUser() + "', idrecursive) >0 ", "staffname", "idemp", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
                }
                glue_IDEMP_I1.EditValue = clsFunction.GetIDEMPByUser();

            }
            catch { }
        }


        #endregion

        

        private void gct_cskh_C_DoubleClick(object sender, EventArgs e)
        {
            frmCSKH_DETAIL frm = new frmCSKH_DETAIL();
            frm.fromDate = Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd");
            frm.toDate = Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd");
            frm.idCustomer = gv_cskh_C.GetRowCellValue(gv_cskh_C.FocusedRowHandle, "idcustomer").ToString();
            frm.ShowDialog();
        }

        private void gv_device_C_DoubleClick(object sender, EventArgs e)
        {
            frm_Device_DETAIL frm = new frm_Device_DETAIL();
            frm.fromDate = Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd");
            frm.toDate = Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd");
            //frm.idEmp = gv_device_C.GetRowCellValue(gv_device_C.FocusedRowHandle, "idemp").ToString();
            //frm.idCustomer = gv_device_C.GetRowCellValue(gv_device_C.FocusedRowHandle, "idcustomer").ToString();
            frm.idStatus = glue_iddepartment_I1.EditValue.ToString();
            frm.ShowDialog();
        }

        private void bgv_list_C_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmp = bgv_list_C.GetRowCellValue(bgv_list_C.FocusedRowHandle, "idemp").ToString();
                string idStatus = glue_iddepartment_I1.EditValue.ToString();
                loadDevice(idEmp);
                loadCSKH(idEmp, idStatus);
            }
            catch (Exception ex) { }
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

        private void gct_list_C_Click(object sender, EventArgs e)
        {
           
        }

        private void bandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                string sql = @"
                select 
            Q.idemp, EM.StaffName, C.customer,invoiceeps, quotationno, nguon_bg, dateimport, datepo,  E.StaffName creadted_StaffName,
            QT.quotationtype, QS.statusquotation, Q.thoigiantt, Q.ngaydukien, sum(QD.quantity*QD.price) amount,
            Q.nguoi_can_thiep, Q.reason
            from QUOTATION Q with(nolock)
            INNER JOIN EMPLOYEES E with(nolock) 
            ON Q.IDEMP = E.IDEMP
            INNER JOIN EMPLOYEES EM with(nolock) 
            ON Q.idemppo	 = EM.IDEMP
            INNER JOIN DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
            INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON QT.idquotationtype = Q.idquotationtype
            INNER JOIN DMSTATUSQUOTATION QS with(nolock) ON QS.idstatusquotation =Q.idstatusquotation
            INNER JOIN QUOTATIONDETAIL QD with(nolock) ON QD.idexport =Q.idexport
            where Q.IDEMP = '{2}' and dateimport between '{0}' and '{1}'
            GROUP BY Q.idemp, EM.StaffName, C.customer,invoiceeps, quotationno, nguon_bg, dateimport, datepo,  E.StaffName,
            QT.quotationtype, QS.statusquotation, Q.thoigiantt, Q.ngaydukien,Q.nguoi_can_thiep, Q.reason
            ";
                string empId = bgv_list_C.GetFocusedRowCellValue("IDEMP").ToString();
                sql = string.Format(sql, Convert.ToDateTime(dte_fromdate_S.EditValue).ToString("yyyy-MM-dd"), Convert.ToDateTime(dte_todate_S.EditValue).ToString("yyyy-MM-dd"), empId);
                DataTable dt = APCoreProcess.APCoreProcess.Read(sql);

                gct_cskh_C.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

    

    }
}