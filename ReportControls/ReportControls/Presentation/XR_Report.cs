using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace ReportControls.Presentation
{
    public partial class XR_Report : DevExpress.XtraReports.UI.XtraReport
    {
        private DataSet ds = new DataSet();

        public XR_Report(String sql, String connect)
        {
            InitializeComponent();
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                loaddata3(sql, connect);
                loaddata2(sql, connect);                                 
            }
            else
            {
                loaddata3_CE(sql, connect);
                loaddata2_CE(sql, connect);
            }
        }

        public XR_Report(String sql,string[,] arrParam, String connect)
        {
            InitializeComponent();
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                loaddata3_pro(sql, arrParam, connect);
                loaddata2_pro(sql, arrParam, connect);          
            }
            else
            {
                loaddata3_CE(sql, connect);
                loaddata2_CE(sql, connect);
            }
        }

        private SqlDataAdapter loaddata3(String sql, String connect)
        {
            SqlDataAdapter daSelect = new SqlDataAdapter(sql, connect);     
            this.DataAdapter = daSelect;
            return daSelect;       
        }

        public  SqlDataAdapter loaddata3_pro(string namePro, string[,] arrParameters, string connect)
        {
           SqlDataAdapter adapt = new SqlDataAdapter();
            try
            {
                
                SqlCommand cmd = new SqlCommand(namePro);
                SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
                {
                    cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
                }
                sqlConnection1.Open();
                adapt = new SqlDataAdapter(cmd);
                //adapt.SelectCommand = cmd;

                
                sqlConnection1.Close();

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
            return adapt;
        }

        public DataSet loaddata2(String sql, String connect)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, connect);
            da.Fill(ds, "Data");
            this.DataMember = "table";
            this.DataSource = ds;
            return ds;      
        }

        public DataSet loaddata2_pro(string namePro, string[,] arrParameters, string connect)
        {
            DataSet dt = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(namePro);
                SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
                {
                    cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
                }
                sqlConnection1.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                //adapt.SelectCommand = cmd;

                adapt.Fill(dt);
                sqlConnection1.Close();

            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
            if (dt.Tables.Count > 0)
                return dt;
            else
                return null;
        }

        private SqlCeDataAdapter loaddata3_CE(String sql, String connect)
        {
            SqlCeDataAdapter daSelect = new SqlCeDataAdapter(sql, connect);
            this.DataAdapter = daSelect;
            return daSelect;
        }
                
        public DataSet loaddata2_CE(String sql, String connect)
        {
            SqlCeDataAdapter da = new SqlCeDataAdapter(sql, connect);
            da.Fill(ds, "Data");
            this.DataMember = "table";
            this.DataSource = ds;
            return ds;
        }

    }
}
