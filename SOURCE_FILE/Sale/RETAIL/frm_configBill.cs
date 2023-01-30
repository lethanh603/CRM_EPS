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
    public partial class frm_configBill : DevExpress.XtraEditors.XtraForm
    {
        public frm_configBill()
        {
            InitializeComponent();
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput() == true)
                {
                    if (APCoreProcess.APCoreProcess.Read("sysConfigBill").Rows.Count == 0)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("insert into sysConfigBill (id,timestart,timeend,isNight) values ('CF000001','" + tm_start_I2.Text + "','" + tm_end_I2.Text + "','" + chk_isNight_I6.Checked + "')");
                    }
                    else
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("update sysConfigBill set timestart='" + tm_start_I2.Text + "', timeend='" + tm_end_I2.Text + "', isNight='" + chk_isNight_I6.Checked + "' where id='CF000001'");
                    }
                    this.Close();
                    
                }
                else
                {
                    Function.clsFunction.MessageInfo("Thông báo","Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
                }
            }
            catch { }
        }

        private bool checkInput()
        {
            bool flag = true;
            if (TimeSpan.Parse(tm_start_I2.Text.ToString()) > TimeSpan.Parse(tm_end_I2.Text.ToString()) && chk_isNight_I6.Checked==false)
            {
                flag = false;
            }
            return flag;
        }

        private void frm_configBill_Load(object sender, EventArgs e)
        {
            try
            {
                Function.clsFunction.TranslateForm(this, this.Name);
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select * from sysConfigBill");
                if (dt.Rows.Count > 0)
                {
                    tm_start_I2.Text = dt.Rows[0]["timestart"].ToString();
                    tm_end_I2.Text = dt.Rows[0]["timeend"].ToString();
                    chk_isNight_I6.Checked = Convert.ToBoolean(dt.Rows[0]["isNight"]);
                }
            }
            catch { }
        }
    }
}