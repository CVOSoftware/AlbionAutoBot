using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Message;

namespace AlbionAutoBot.App.Service
{
    internal class ProcessMonitoringService : IDisposable
    {
        #region Const

        private const string MonitoringProcessName = "AlbionLauncher";

        #endregion

        #region Implementation Singleton pattern

        private static ProcessMonitoringService instance;

        private static object sync = new object();

        public static ProcessMonitoringService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new ProcessMonitoringService();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        private bool processMonitoring;

        private ProcessMonitoringService()
        {
            
        }

        public void Start()
        {
            if(processMonitoring)
            {
                return;
            }

            processMonitoring = true;

            Task.Run(() =>
            {
                while(processMonitoring)
                {
                    var monitoringStatus = Monitoring();
                    var message = new UpdateMonitoringStatusMsg(monitoringStatus);

                    Messenger.Default.Send(message);
                    Thread.Sleep(1000);
                };
            });
        }

        private bool Monitoring()
        {
            var process = Process.GetProcessesByName(MonitoringProcessName).FirstOrDefault();

            return process == null ? false : true;
        }

        #region Implementation IDisposable

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {

            }

            processMonitoring = false;
            disposed = true;
        }

        #endregion
    }
}
