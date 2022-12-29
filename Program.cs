using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSHARPCRUDMVC.Models;
using CSHARPCRUDMVC.Presenters;
using CSHARPCRUDMVC._Repositories;
using CSHARPCRUDMVC.Views;
using System.Configuration;

namespace CSHARPCRUDMVC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                           
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    string sqlConnectionString = ConfigurationManager.ConnectionStrings["CSHARPCRUDMVC.Properties.Settings.SqlConnection"].ConnectionString;
                    IMainView view = new MainView();
                    new MainPresenter(view, sqlConnectionString);
                    Application.Run((Form)view);
                }
               
            
        }
    }
}
