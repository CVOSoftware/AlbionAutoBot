using System.Windows;
using AlbionAutoBot.App.Service;
using AlbionAutoBot.App.ViewModel;

namespace AlbionAutoBot.App
{
    public partial class App : Application
    {
        private ManagerViewModel managerVM;

        private ProcessMonitoringService monitoringService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            managerVM = new ManagerViewModel();
            monitoringService = ProcessMonitoringService.Initialize();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            managerVM.Dispose();
            monitoringService.Dispose();
        }
    }
}
