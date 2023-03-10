using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace APCoreProcess
{
    public class APCoreProcess
    {
        public static bool IssqlCe = true;
        public static DataTable Read(string sql)
        {            
         if (IssqlCe==false)
            return APCoreData.read(sql);
         else
            return APCoreDataSQLCE.read(sql);
        }
        public static DataTable Read(string sql,string storeName, string[,] arrParameter,string strPara)
        {
            if (IssqlCe == false)
                return APCoreData.read(sql,storeName,arrParameter,strPara);
            else
                return APCoreDataSQLCE.read(sql, storeName, arrParameter, strPara);
        }
        public static DataTable ReadStructure(string table)
        {
            if (IssqlCe == false)
                return APCoreData.readStructure(table);
            else
                return APCoreDataSQLCE.readStructure(table);
        }
        public static int Save(DataRow dr)
        {
            if (IssqlCe == false)
                return APCoreData.write(dr);
            else
                return APCoreDataSQLCE.write(dr);
        }
        public static int ExcuteSQL(string sql)
        {
            if (IssqlCe == false)
                return APCoreData.ExcuteSQL(sql);
            else
                return APCoreDataSQLCE.ExcuteSQL(sql);
        }
        /////////////////////////////////
        public static DataTable Read1(string sql)
        {
      
            return ConnectAccess.read1(sql);
       
        }

        public static DataTable ReadStructure1(string table)
        {
            return ConnectAccess.readStructure1(table);
        }
    }
}
