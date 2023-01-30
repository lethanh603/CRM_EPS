using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace Function
{
    public class clsOptionType
    {
        public void FistInLastOut(DataTable dt,DateTime Startday, DateTime EndDay)
        {
            try
            {
                DateTime time = Startday;
                BindingSource bngArrIORun = new BindingSource();
                bngArrIORun.DataSource = dt;
                while (DateTime.Compare(time, EndDay) <= 0)
                {
                    string str = time.ToShortDateString();
                    bngArrIORun.Filter = str;
                    if (bngArrIORun.Count > 0)
                    {
                        if (bngArrIORun.Count == 1)
                        {
                            NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                        }
                        else
                        {
                            int num2 = bngArrIORun.Count - 1;
                            for (int i = 0; i <= num2; i++)
                            {
                                bngArrIORun.Position = i;
                                if (i == 0)
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                                }
                                else if (i == (bngArrIORun.Count - 1))
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                }
                                else
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "K" }, null, false, true);
                                }
                            }
                        }
                    }
                    bngArrIORun.RemoveFilter();
                    time = DateAndTime.DateAdd(DateInterval.Day, 1.0, time);
                }
                bngArrIORun.RemoveFilter();
                bngArrIORun = null;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("FistInLastOut: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }

        public void GetCheckInOut(DataTable dt ,string UserFullCode, DateTime StartDay, DateTime EndDay, bool RemoveOut, string RemoveStart, string RemoveEnd)
        {
            DateTime expression = DateAndTime.DateAdd(DateInterval.Day, -1.0, StartDay);
            BindingSource bngArrIORun = new BindingSource();
            try
            {
                string str;
             
                bngArrIORun.DataSource = dt;
                {
                    bngArrIORun.DataSource = APCoreProcess.APCoreProcess.Read("Select * from CheckInOut Where UserFullCode='" + UserFullCode + "'  And TimeDate >='" + Strings.Format(StartDay, "MM/dd/yyyy") + "' And TimeDate <='" + Strings.Format(EndDay, "MM/dd/yyyy") + "' Order by TimeStr");
                    str = "Select UserFullCode From InOutCol where UserFullCode='" + UserFullCode + "' And TimeDate='" + Strings.Format(expression, "MM/dd/yyyy") + "' And Overday=" + Conversions.ToString(1);
                }
                if (dt.Rows.Count>0 && RemoveOut)
                {
                    bngArrIORun.MoveFirst();
                    string left = Strings.Format(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), "HH:mm");
                    for (DateTime time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null)); ((Operators.CompareString(left, RemoveStart, false) >= 0) & (Operators.CompareString(left, RemoveEnd, false) <= 0)) & (DateTime.Compare(time2, StartDay) == 0); time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null)))
                    {
                        bngArrIORun.RemoveCurrent();
                        if (bngArrIORun.Count <= 0)
                        {
                            return;
                        }
                        bngArrIORun.MoveFirst();
                        left = Strings.Format(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), "HH:mm");
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("GetCheckInOut: " + exception.Message, MsgBoxStyle.OkOnly, null);
                bngArrIORun.DataSource = null;
                ProjectData.ClearProjectError();
            }
        }
        public void SaveInOutColRun(DataTable dtb)
        {
            try
            {
                if (dtb.Rows.Count > 0)
                {
                    long num2 = dtb.Rows.Count - 1;
                    for (long i = 0L; i <= num2; i += 1L)
                    {
      
                        {
                            string str2 = "Insert Into InOutCol (UserFullCode,TimeDate,CheckIn,CheckOut,Overday) Values(@UserFullCode,@TimeDate,@CheckIn,@CheckOut,@Overday)";
                            SqlCommand command2 = new SqlCommand(str2);
                            command2.Parameters.AddWithValue("@UserFullCode", RuntimeHelpers.GetObjectValue(dtb.Rows[(int)i]["UserFullCode"]));
                            command2.Parameters.AddWithValue("@TimeDate", RuntimeHelpers.GetObjectValue(dtb.Rows[(int)i]["TimeDate"]));
                            command2.Parameters.AddWithValue("@CheckIn", RuntimeHelpers.GetObjectValue(dtb.Rows[(int)i]["TimeIn"]));
                            command2.Parameters.AddWithValue("@CheckOut", RuntimeHelpers.GetObjectValue(dtb.Rows[(int)i]["TimeOut"]));
                            command2.Parameters.AddWithValue("@Overday", RuntimeHelpers.GetObjectValue(Interaction.IIf(Operators.ConditionalCompareObjectEqual(dtb.Rows[(int)i]["Overday"], 0, false), false, true)));
                            command2.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("SaveInOutColRun: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }


        public void TheoIDMay(DataTable dt, int AutoMin)
        {

            try
            {
                BindingSource bngArrIORun = new BindingSource();
                bngArrIORun.DataSource = dt ;
                if (bngArrIORun.Count != 0)
                {
                    DateTime time=new DateTime();
                    DateTime time2=new DateTime();
                    bngArrIORun.Position = 0;
                    if (Operators.ConditionalCompareObjectNotEqual(Operators.ModObject(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "MachineNo" }, null), 2), 0, false))
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                        time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    else if (Operators.ConditionalCompareObjectEqual(Operators.ModObject(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "MachineNo" }, null), 2), 0, false))
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                        time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    int num5 = bngArrIORun.Count - 1;
                    for (int i = 1; i <= num5; i++)
                    {
                        int num2;
                        bngArrIORun.Position = i;
                        int num4 = i - 1;
                        int num3 = i;
                        if (Operators.ConditionalCompareObjectNotEqual(Operators.ModObject(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "MachineNo" }, null), 2), 0, false))
                        {
                            num2 = (int)DateAndTime.DateDiff(DateInterval.Minute, time, Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                            if (num2 < AutoMin)
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                            }
                            else
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                                time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                        }
                        else if (Operators.ConditionalCompareObjectEqual(Operators.ModObject(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "MachineNo" }, null), 2), 0, false))
                        {
                            num2 = (int)DateAndTime.DateDiff(DateInterval.Minute, time2, Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                            if (num2 < AutoMin)
                            {
                                bngArrIORun.Position = num4;
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                                bngArrIORun.Position = num3;
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                            else
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                        }
                    }
                    bngArrIORun = null;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("TheoIDMay: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }

        public void TheoModeInOut(DataTable dt,int AutoMin)
        {
            try
            {
                BindingSource bngArrIORun = new BindingSource() ;
                bngArrIORun.DataSource = dt;
                if (bngArrIORun.Count != 0)
                {
                    DateTime time3 = new DateTime();
                    DateTime time2 = new DateTime();
                    bngArrIORun.MoveFirst();
                    long position = bngArrIORun.Position;
                    if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "OriginType" }, null), "I", false))
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                        time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    else
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                        time3 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    long num3 = bngArrIORun.Count - 1;
                    for (long i = 1L; i <= num3; i += 1L)
                    {
                        DateTime time;
                        bngArrIORun.Position = (int)i;
                        if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "OriginType" }, null), "I", false))
                        {
                            time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            if (DateAndTime.DateDiff(DateInterval.Minute, time2, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) < AutoMin)
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                            }
                            else
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                                time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                        }
                        else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "OriginType" }, null), "O", false))
                        {
                            time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            if (DateAndTime.DateDiff("n", time3, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) < AutoMin)
                            {
                                bngArrIORun.Position = (int)(i - 1L);
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                                bngArrIORun.Position = (int)i;
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                            }
                            else
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                            }
                            time3 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                        }
                    }
                    bngArrIORun = null;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("TheoModeInOut: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }

        public void TheoPhanGio(DataTable dt,int AutoMin, int InOutID)
        {
            try
            {
                BindingSource bngArrIORun = new BindingSource();
                bngArrIORun.DataSource = dt;
                if (bngArrIORun.Count != 0)
                {
                    DateTime time=new DateTime();
                    DateTime time2=new DateTime();
                    bngArrIORun.Position = 0;
                    string gioXet = Strings.Format(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), "HH:mm");
                    if (this.XacdinhIn(gioXet, InOutID))
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                        time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    else if (this.XacdinhOut(gioXet, InOutID))
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                        time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                    }
                    else
                    {
                        NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "K" }, null, false, true);
                    }
                    int num5 = bngArrIORun.Count - 1;
                    for (int i = 1; i <= num5; i++)
                    {
                        int num2;
                        bngArrIORun.Position = i;
                        int num3 = i;
                        int num4 = i - 1;
                        gioXet = Strings.Format(RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), "HH:mm");
                        if (this.XacdinhIn(gioXet, InOutID))
                        {
                            if (Information.IsDBNull(time))
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                                time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                            else
                            {
                                num2 = (int)DateAndTime.DateDiff(DateInterval.Minute, time, Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                if (num2 < AutoMin)
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                                }
                                else
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "I" }, null, false, true);
                                    time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                }
                            }
                        }
                        else if (this.XacdinhOut(gioXet, InOutID))
                        {
                            if (Information.IsDBNull(time2))
                            {
                                NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                            }
                            else
                            {
                                num2 = (int)DateAndTime.DateDiff(DateInterval.Minute, time2, Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null)), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                if (num2 < AutoMin)
                                {
                                    bngArrIORun.Position = num4;
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "L" }, null, false, true);
                                    bngArrIORun.Position = num3;
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                    time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                }
                                else
                                {
                                    NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "O" }, null, false, true);
                                    time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                }
                            }
                        }
                        else
                        {
                            NewLateBinding.LateIndexSetComplex(bngArrIORun.Current, new object[] { "NewType", "K" }, null, false, true);
                        }
                    }
                    bngArrIORun = null;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("TheoPhanGio: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }

        private bool XacdinhOut(string GioXet, int InOutID)
        {
            bool flag = false; 
            try
            {
                bool flag2=false;
                string strText = "Select StartOut,EndOut From InOut Where InOutID=" + Conversions.ToString(InOutID);
                BindingSource source = new BindingSource
                {
                    DataSource = APCoreProcess.APCoreProcess.Read(strText)
                };
                BindingSource source2 = source;
                if (source2.Count == 0)
                {
                    return false;
                }
                int num2 = source2.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    source2.Position = i;
                    if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectGreaterEqual(GioXet, NewLateBinding.LateIndexGet(source2.Current, new object[] { "StartOut" }, null), false), Operators.CompareObjectLessEqual(GioXet, NewLateBinding.LateIndexGet(source2.Current, new object[] { "EndOut" }, null), false))))
                    {
                        flag2 = true;
                        break;
                    }
                    flag2 = false;
                }
                source2 = null;
                flag = flag2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("XacdinhOut: " + exception.Message, MsgBoxStyle.Critical, null);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }


        private bool XacdinhIn(string GioXet, int InOutID)
        {
            bool flag=false;
            try
            {
                bool flag2=false;
                string strText = "Select StartIn,EndIn From InOut Where InOutID=" + Conversions.ToString(InOutID);
                BindingSource source = new BindingSource
                {
                    DataSource = APCoreProcess.APCoreProcess.Read(strText)
                };
                BindingSource source2 = source;
                if (source2.Count == 0)
                {
                    return false;
                }
                int num2 = source2.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    source2.Position = i;
                    if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectGreaterEqual(GioXet, NewLateBinding.LateIndexGet(source2.Current, new object[] { "StartIn" }, null), false), Operators.CompareObjectLessEqual(GioXet, NewLateBinding.LateIndexGet(source2.Current, new object[] { "EndIn" }, null), false))))
                    {
                        flag2 = true;
                        break;
                    }
                    flag2 = false;
                }
                source2 = null;
                flag = flag2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("XacdinhIn: " + exception.Message, MsgBoxStyle.Critical, null);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }


        public void TheoTuDong(DataTable dt,int AutoMax, int AutoMin, int AutoInterVal, int Songay, string StartTime, string EndTime)
        {
            string str3 = "";
            string str = "";
            try
            {
                BindingSource bngArrIORun = new BindingSource();
                bngArrIORun.DataSource = dt;
                if (bngArrIORun.Count != 0)
                {
                    bngArrIORun.MoveFirst();
                    DataRow row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                    row["NewType"] = "I";
                    int num6 = bngArrIORun.Count - 1;
                    for (int i = 1; i <= num6; i++)
                    {
                        double num2=0;
                        double num3=0;
                        int num4 = i;
                        int num5 = i - 1;
                        bngArrIORun.Position = num4;
                        DateTime time = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                        bngArrIORun.Position = num5;
                        DateTime time2 = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                        DateTime dateValue = Conversions.ToDate(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
                        string str2 = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "NewType" }, null));
                        DateTime time4 = DateAndTime.DateAdd(DateInterval.Day, (double)Songay, dateValue);
                        DateTime time6 = Conversions.ToDate(Conversions.ToString(dateValue) + " " + StartTime);
                        DateTime time5 = Conversions.ToDate(Conversions.ToString(time4) + " " + EndTime);
                        switch (str2)
                        {
                            case "I":
                                if ((DateTime.Compare(time, time6) >= 0) & (DateTime.Compare(time, time5) <= 0))
                                {
                                    num2 = DateAndTime.DateDiff("n", time2, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                    if ((num2 >= AutoMin) & (num2 <= AutoMax))
                                    {
                                        bngArrIORun.Position = num4;
                                        row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                        row["NewType"] = "O";
                                    }
                                    else if (num2 > AutoMax)
                                    {
                                        bngArrIORun.Position = num4;
                                        row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                        row["NewType"] = "I";
                                    }
                                    else
                                    {
                                        bngArrIORun.Position = num4;
                                        row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                        row["NewType"] = "L";
                                        str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                        str3 = "I";
                                    }
                                }
                                else
                                {
                                    bngArrIORun.Position = num4;
                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                    row["NewType"] = "I";
                                }
                                break;

                            case "O":
                                num3 = DateAndTime.DateDiff("n", time2, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                if (num3 >= AutoInterVal)
                                {
                                    bngArrIORun.Position = num4;
                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                    row["NewType"] = "I";
                                }
                                else
                                {
                                    bngArrIORun.Position = num4;
                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                    row["NewType"] = "O";
                                    bngArrIORun.Position = num5;
                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                    row["NewType"] = "L";
                                    str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                    str3 = "O";
                                }
                                break;

                            default:
                                if (str2 == "L")
                                {
                                    switch (str3)
                                    {
                                        case "I":
                                            if ((DateTime.Compare(time, time6) >= 0) & (DateTime.Compare(time, time5) <= 0))
                                            {
                                                num2 = DateAndTime.DateDiff("n", time2, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                                if ((num2 >= AutoMin) & (num2 <= AutoMax))
                                                {
                                                    bngArrIORun.Position = num4;
                                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                    row["NewType"] = "O";
                                                }
                                                else if (num2 > AutoMax)
                                                {
                                                    bngArrIORun.Position = num4;
                                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                    row["NewType"] = "I";
                                                }
                                                else
                                                {
                                                    bngArrIORun.Position = num4;
                                                    row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                    row["NewType"] = "L";
                                                    str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
                                                    str3 = "I";
                                                }
                                            }
                                            else
                                            {
                                                bngArrIORun.Position = num4;
                                                row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                row["NewType"] = "I";
                                            }
                                            break;

                                        case "O":
                                            num3 = DateAndTime.DateDiff("n", str, time, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                                            if (num3 >= AutoInterVal)
                                            {
                                                bngArrIORun.Position = num4;
                                                row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                row["NewType"] = "I";
                                            }
                                            else
                                            {
                                                bngArrIORun.Position = num4;
                                                row = (DataRow)NewLateBinding.LateGet(bngArrIORun.Current, null, "row", new object[0], null, null, null);
                                                row["NewType"] = "O";
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                    bngArrIORun = null;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("TheoTuDong: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }

        private void UpdateOriginInOut(int UserEnrollNumber, DateTime TimeStr, string NewType)
        {
            try
            {          
                {
                    SqlCommand command2 = new SqlCommand("Update CheckInOut Set NewType=" + NewType + " Where UserEnrollNumber=" + Conversions.ToString(UserEnrollNumber) + " And TimeStr='" + Strings.Format(TimeStr, "MM/dd/yyyy HH:mm:ss") + "'");
                    command2.Parameters.AddWithValue("@NewType", NewType);
                    command2.ExecuteNonQuery();
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("UpdateOriginInOut: " + exception.Message, MsgBoxStyle.Critical, null);
                ProjectData.ClearProjectError();
            }
        }
        //public void ChiaHaiCot(DataTable dt,int MaxGet)
        //{
        //    string str = "";
        //    string str2 = "";
        //    int num = 0;
        //    try
        //    {
        //        BindingSource bngArrIORun = new BindingSource();
        //        bngArrIORun.DataSource = dt;
        //        if (bngArrIORun.Count != 0)
        //        {
        //            bool flag;
        //            bool flag2;
        //            DataRow row;
        //            bngArrIORun.MoveFirst();
        //            if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "NewType" }, null), "I", false))
        //            {
        //                row = this.dtIORun.NewRow();
        //                row["UserFullCode"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "UserFullCode" }, null));
        //                row["TimeDate"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
        //                row["TimeIn"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                row["Overday"] = 0;
        //                str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                this.dtIORun.Rows.Add(row);
        //                flag = true;
        //                flag2 = false;
        //                num++;
        //                str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //            }
        //            else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "NewType" }, null), "O", false))
        //            {
        //                row = this.dtIORun.NewRow();
        //                row["UserFullCode"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "UserFullCode" }, null));
        //                row["TimeDate"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
        //                row["TimeOut"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                row["Overday"] = 0;
        //                this.dtIORun.Rows.Add(row);
        //                str2 = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                num++;
        //                flag = true;
        //                flag2 = false;
        //            }
        //            long num3 = bngArrIORun.Count - 1;
        //            for (long i = 1L; i <= num3; i += 1L)
        //            {
        //                bngArrIORun.Position = (int)i;
        //                if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "NewType" }, null), "I", false))
        //                {
        //                    row = this.dtIORun.NewRow();
        //                    row["UserFullCode"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "UserFullCode" }, null));
        //                    row["TimeDate"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
        //                    row["TimeIn"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                    row["Overday"] = 0;
        //                    this.dtIORun.Rows.Add(row);
        //                    str = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                    num++;
        //                    flag = true;
        //                    flag2 = false;
        //                }
        //                else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "NewType" }, null), "O", false))
        //                {
        //                    str2 = Conversions.ToString(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                    if (flag)
        //                    {
        //                        if (DateAndTime.DateDiff("n", str, str2, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) <= MaxGet)
        //                        {
        //                            row = this.dtIORun.Rows[num - 1];
        //                            row["TimeOut"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                            row["Overday"] = DateAndTime.DateDiff("d", DateAndTime.DateValue(str), DateAndTime.DateValue(str2), FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
        //                            flag = false;
        //                            flag2 = true;
        //                        }
        //                        else
        //                        {
        //                            row = this.dtIORun.NewRow();
        //                            row["UserFullCode"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "UserFullCode" }, null));
        //                            row["TimeDate"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
        //                            row["TimeOut"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                            row["Overday"] = 0;
        //                            this.dtIORun.Rows.Add(row);
        //                            num++;
        //                            flag = false;
        //                            flag2 = true;
        //                        }
        //                    }
        //                    else if (flag2)
        //                    {
        //                        row = this.dtIORun.NewRow();
        //                        row["UserFullCode"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "UserFullCode" }, null));
        //                        row["TimeDate"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeDate" }, null));
        //                        row["TimeOut"] = RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(bngArrIORun.Current, new object[] { "TimeStr" }, null));
        //                        row["Overday"] = 0;
        //                        this.dtIORun.Rows.Add(row);
        //                        num++;
        //                        flag = false;
        //                        flag2 = true;
        //                    }
        //                }
        //            }
        //            this.SaveInOutColRun(this.dtIORun);
        //            bngArrIORun = null;
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        Interaction.MsgBox("ChiaHaiCot: " + exception.Message, MsgBoxStyle.Critical, null);
        //        ProjectData.ClearProjectError();
        //    }
        //}

 

    }
}
