using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Security;

namespace LoyalHRM.Bussiness
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            { 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);           
                DevExpress.UserSkins.BonusSkins.Register();
                Application.Run(new SOURCE_FORM_DMWAREHOUSE.Presentation.frm_DMWAREHOUSE_SH());
            }
            catch  { };
        }
    }
}
