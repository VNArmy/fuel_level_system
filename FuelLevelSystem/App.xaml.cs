using FuelLevelSystem.Model;
using FuelLevelSystem.ViewModel;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace FuelLevelSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static  FuelLevelSystem.Logger.Logger Log ;

        protected override void OnStartup(StartupEventArgs e)
        {
            Log = new FuelLevelSystem.Logger.Logger(typeof(App));
            Log.LogInfoMessage ("Hello World");
            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            //Show the login view
            //AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
            //IView loginWindow = new LoginWindow(viewModel);
            //loginWindow.ShowDialog();
        }
        
    }
}
