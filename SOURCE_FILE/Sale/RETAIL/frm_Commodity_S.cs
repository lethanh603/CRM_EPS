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
    public partial class frm_Commodity_S : DevExpress.XtraEditors.XtraForm
    {
        public frm_Commodity_S()
        {
            InitializeComponent();
        }

        private void frm_Commodity_S_Load(object sender, EventArgs e)
        {
            addButtonGroup();
        }
        
        private void addButtonGroup()
        {
            DataTable dt = new DataTable();
            int x = 5;
            int y = 30;
            int w = 0;
            int h = 0;
            dt = APCoreProcess.APCoreProcess.Read("select * from dmgroup where status=1");
            if (dt.Rows.Count > 0)
            {
                gc_group_C.Controls.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SimpleButton btn = new SimpleButton();
                    btn.Height = 40;
                    btn.Width = 100;
                    btn.Name = dt.Rows[i]["idgroup"].ToString();
                    btn.Text = dt.Rows[i]["groupname"].ToString();
                    btn.Location = new Point(x+w,y+h);
                    if (i % 2 == 0)
                    {
                        x += 103;
                    }
                    else
                    {
                        y += 42;
                        x -= 103;
                    }
                    gc_group_C.Controls.Add(btn);
                }
            }
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  
    }
}