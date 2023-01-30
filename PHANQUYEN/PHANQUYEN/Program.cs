using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PHANQUYEN.Bussiness
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            Application.Run(new PHANQUYEN.Presentation.frm_phanquyen_SH());
        }   
    }
}
