using System.Windows;
using AlbionAutoBot.App.Service;
using AlbionAutoBot.App.ViewModel;

namespace AlbionAutoBot.App
{
    public partial class App : Application
    {
        private ManagerViewModel _managerVM;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _managerVM = new ManagerViewModel();
            ProcessMonitoringService.Instance.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _managerVM.Dispose();
            ProcessMonitoringService.Instance.Dispose();
        }
    }
}
