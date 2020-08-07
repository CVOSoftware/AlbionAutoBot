using System.Windows;
using AlbionAutoBot.App.ViewModels;

namespace AlbionAutoBot.App
{
    public partial class App : Application
    {
        private ManagerViewModel managerViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            managerViewModel = new ManagerViewModel();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            managerViewModel.Dispose();
        }
    }
}
