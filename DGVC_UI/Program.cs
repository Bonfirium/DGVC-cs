using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestProject;

namespace DGVC_UI
{ 
    
    static class Program
    {
        public static string commit;
        public static readonly GolosManager GolosManager = new GolosManager();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new FormWelcome());
            Application.Run(new menu());



        }
    }
}
