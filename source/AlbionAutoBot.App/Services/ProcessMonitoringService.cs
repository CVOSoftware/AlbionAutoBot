﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Messages;

namespace AlbionAutoBot.App.Services
{
    internal class ProcessMonitoringService : IDisposable
    {
        #region Const

        private const string MonitoringProcessName = "AlbionLauncher";

        #endregion

        #region Implementation Singleton pattern

        private static ProcessMonitoringService instance;

        private static object sync = new object();

        public static ProcessMonitoringService Initialize()
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

        #endregion

        private bool processMonitoring;

        private ProcessMonitoringService()
        {
            processMonitoring = true;

            Start();
        }

        private void Start()
        {
            Task.Run(() =>
            {
                while(processMonitoring)
                {
                    var monitoringStatus = Monitoring();
                    var message = new UpdateMonitoringStatusMessage(monitoringStatus);

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
