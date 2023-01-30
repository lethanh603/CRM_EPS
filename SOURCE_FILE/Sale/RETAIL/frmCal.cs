using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_RETAIL.Presentation
{
    public partial class frmCal : DevExpress.XtraEditors.XtraForm
    {
        public frmCal()
        {
            InitializeComponent();
        }

        #region Var

        public string commodity = "";
        public string table = "";
         public string idexport = "";
         public string idcommodity = "";
        public decimal price = 100000;
        public decimal quantity = 0;
        public decimal amoundiscount = 0;
        string number = "0";
        public delegate void PassDataQ(double quantity);
        public PassDataQ passDataq;
        public delegate void PassDataP(double price);
        public PassDataP passDatap;
        public delegate void PassDataD(double dis);
        public PassDataD passDatad;
        public delegate void PassDataAD(double adis);
        public PassDataAD passDataad;
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void PassDataGive(bool value);
        public PassDataGive passDataGive;
        #endregion

        #region FormEvent

        private void frmCal_Load(object sender, EventArgs e)
        {
            txt_quantity_S.Focus();
            cal_amount_S.Value = price;
            lbl_table_S.Text = table;
            lbl_commodity_S.Text = commodity;
            txt_quantity_S.Text = quantity.ToString();
            lbl_strquantity_S.Text = txt_quantity_S.Text;
            cal_amountdiscount_S.Value = quantity * amoundiscount;            
            lbl_strquantity_S.Text = quantity.ToString();
            cal_total_S.Value = Convert.ToDecimal(lbl_strquantity_S.Value) * price - cal_amountdiscount_S.Value;
            if (cal_amount_S.Value !=0)
                cal_discount_S.Value = cal_amountdiscount_S.Value/(Convert.ToDecimal(txt_quantity_S.Text) * cal_amount_S.Value)*100;
            DataTable dtM = new DataTable();
            dtM = APCoreProcess.APCoreProcess.Read("select give,isdiscount from exportdetail where idexport='"+ idexport +"' and idcommodity ='"+ idcommodity +"'");
            if (dtM.Rows.Count > 0)
            {
                chk_tang_I6.Checked = dtM.Rows[0]["give"].ToString() == "" ? false : Convert.ToBoolean(dtM.Rows[0]["give"]);
                chk_directpromotion_I6.Checked = dtM.Rows[0]["isdiscount"].ToString() == "" ? false : Convert.ToBoolean(dtM.Rows[0]["isdiscount"]);
            }
        }

        #endregion

        #region ButtonEvent

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            passData(false);
            this.Close();
        }

        private void bn_argree_S_Click(object sender, EventArgs e)
        {
            try
            {
                passDatap(Convert.ToDouble(cal_total_S.Value));
                passDatad(Convert.ToDouble(cal_discount_S.Value));
                passDataad(Convert.ToDouble(Convert.ToDouble( cal_amountdiscount_S.Value)/(Convert.ToDouble(txt_quantity_S.Text))));
                passDataq(Convert.ToDouble(txt_quantity_S.Text));
                passData(true);
                if (chk_tang_I6.Checked == true)
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set give=1 where idexport='" + idexport + "' and idcommodity='"+ idcommodity +"' ");
                    passDataGive(true);
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set isdiscount=1 where idexport='" + idexport + "' and idcommodity='" + idcommodity + "' ");
                }
                else
                {
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set give=0 where idexport='" + idexport + "' and idcommodity='" + idcommodity + "' ");
                    passDataGive(false);
                    APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set isdiscount=0 where idexport='" + idexport + "' and idcommodity='" + idcommodity + "' ");
                }    
            }
            catch { }
            this.Close();
        }

        private void btn_1_S_Click(object sender, EventArgs e)
        {
            number += "1";
            txt_quantity_S.Text = number;
        }

        private void btn_2_S_Click(object sender, EventArgs e)
        {
            number += "2";
            txt_quantity_S.Text = number;
        }

        private void btn_3_S_Click(object sender, EventArgs e)
        {
            number += "3";
            txt_quantity_S.Text = number;
        }

        private void btn_4_S_Click(object sender, EventArgs e)
        {
            number += "4";
            txt_quantity_S.Text = number;
        }

        private void btn_5_S_Click(object sender, EventArgs e)
        {
            number += "5";
            txt_quantity_S.Text = number;
        }

        private void btn_6_S_Click(object sender, EventArgs e)
        {
            number += "6";
            txt_quantity_S.Text = number;
        }

        private void btn_7_S_Click(object sender, EventArgs e)
        {
            number += "7";
            txt_quantity_S.Text = number;
        }

        private void btn_8_S_Click(object sender, EventArgs e)
        {
            number += "8";
            txt_quantity_S.Text = number;
        }

        private void btn_9_S_Click(object sender, EventArgs e)
        {
            number += "9";
            txt_quantity_S.Text = number;
        }

        private void btn_0_S_Click(object sender, EventArgs e)
        {
            number += "1";
            txt_quantity_S.Text = number;
        }

        private void btn___S_Click(object sender, EventArgs e)
        {
            number = number.Substring(0, number.Length - 1);
            if (number == "")
                txt_quantity_S.Text = "0";
            else
                txt_quantity_S.Text = number;
        }

        private void btn_x_S_Click(object sender, EventArgs e)
        {
            number = "0";
            txt_quantity_S.Text = number;
           
        }

        private void cal_discount_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = 0;
                double amountdiscount = 0;
                discount = Convert.ToDouble(cal_discount_S.Value);
                amountdiscount = Convert.ToDouble(cal_amountdiscount_S.Value);
                if (Convert.ToInt16(Convert.ToDouble(amountdiscount) / (Convert.ToDouble( price) * Convert.ToDouble(txt_quantity_S.Text)) * 100) != Convert.ToInt16( Convert.ToDouble( cal_discount_S.Value)))
                {
                    amountdiscount =  Convert.ToDouble(lbl_strquantity_S.Text) * Convert.ToDouble(price) * (discount / 100);
                }
                cal_amountdiscount_S.Value = Convert.ToDecimal(Convert.ToDouble( amountdiscount));
                cal_total_S.Value = Convert.ToDecimal( Convert.ToDouble(lbl_strquantity_S.Text) * Convert.ToDouble( cal_amount_S.Value) - Convert.ToDouble( cal_amountdiscount_S.Value));
            }
            catch { }
        }

        private void cal_amountdiscount_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = 0;
                double amountdiscount = 0;
                discount = Convert.ToInt32(cal_discount_S.Value);
                amountdiscount = Convert.ToInt32(cal_amountdiscount_S.Value);
                if (Convert.ToDouble(txt_quantity_S.Text) * (double)price * ((double)discount / 100) != (double)amountdiscount)
                {
                    discount = Convert.ToDouble(Convert.ToDouble(amountdiscount) / (Convert.ToDouble(txt_quantity_S.Text) * Convert.ToDouble(price)) * 100);
                }
                cal_discount_S.Value = Convert.ToDecimal(discount);
                cal_total_S.Value = Convert.ToDecimal(lbl_strquantity_S.Text) * cal_amount_S.Value - cal_amountdiscount_S.Value;
            }
            catch { }
        }

        private void chk_tang_S_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tang_I6.Checked == true && checkIsGive()==false)
            {
                cal_discount_S.Value = 0;
                cal_discount_S.EditValue = false;
                cal_amountdiscount_S.EditValue = false;
                chk_directpromotion_I6.Checked = false;
                frm_Reason frm = new frm_Reason();
                frm.idexport = idexport;
                frm.idcommodity =idcommodity ;
                frm.status = 2;
                frm.passData = new frm_Reason.PassData(getValueGive);
                frm.ShowDialog();
            }
            else
            {
            
                cal_discount_S.EditValue = true;
                cal_amountdiscount_S.EditValue = true;
            }
        }

        private bool checkIsGive()
        {
            bool flag = false;
            if (APCoreProcess.APCoreProcess.Read("select give from exportdetail where idexport='" + idexport + "' and idcommodity='"+ idcommodity +"'  and give =1").Rows.Count > 0)
                flag = true;
            return flag;
        }

        private bool checkIsDiscount()
        {
            bool flag = false;
            if (APCoreProcess.APCoreProcess.Read("select isdiscount from exportdetail where idexport='" + idexport + "' and idcommodity='" + idcommodity + "'  and give =1").Rows.Count > 0)
                flag = true;
            return flag;
        }

        private void getValueGive(bool val)
        {
            if (val == true)
            {
                chk_tang_I6.Checked = true;
                txt_quantity_S.Text = "1";
                lbl_strquantity_S.Text = "1";

            }
            else
                chk_tang_I6.Checked = false;
        }

        #endregion

        private void txt_quantity_S_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //cal_amountdiscount_S.Value = Convert.ToDecimal(txt_quantity_S.Text) * (Convert.ToDecimal(cal_amountdiscount_S.Value) / quantity);
                lbl_strquantity_S.Value =Convert.ToDecimal( txt_quantity_S.Text);
            }
            catch { }
        }

        private void chk_directpromotion_I6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_directpromotion_I6.Checked == true && checkIsDiscount()==false)
            {
                chk_tang_I6.Checked = false;
                frm_Reason frm = new frm_Reason();
                frm.idexport = idexport;
                frm.idcommodity = idcommodity;
                frm.status =3;
                frm.passData = new frm_Reason.PassData(getValuePromotion);
                frm.ShowDialog();
            }

        }

        private void getValuePromotion(bool val)
        {
            if (val == true)
            {
                chk_directpromotion_I6.Checked = true;
                cal_amountdiscount_S.Properties.ReadOnly = false;
                cal_discount_S.Properties.ReadOnly = false;
                APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set isdiscount=1 where idexport='"+ idexport + "' and idcommodity='"+ idcommodity +"'  ");
            }
            else
            {
                chk_directpromotion_I6.Checked = false;
                cal_amountdiscount_S.Properties.ReadOnly = true;
                cal_discount_S.Properties.ReadOnly = true;
                APCoreProcess.APCoreProcess.ExcuteSQL("update exportdetail set isdiscount=0 where idexport='" + idexport + "' and idcommodity='" + idcommodity + "'  ");
            }
        }

        private void frmCal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                bn_argree_S.PerformClick();
            }
            if (e.KeyCode == Keys.F9)
            {
                btn_exit_S.PerformClick();
            }
        }

        #region Methods


        #endregion
    }
}