using AlbionAutoBot.App.ViewModels;
using AlbionAutoBot.App.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlbionAutoBot.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainViewModel();
            var view = new ManagerWindow();

            view.DataContext = viewModel;
            view.Show();
        }
    }
}
