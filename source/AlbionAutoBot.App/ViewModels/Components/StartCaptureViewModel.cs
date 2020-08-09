using System;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Messages;
using AlbionAutoBot.App.Commands;
using AlbionAutoBot.App.ViewModels.Base;

namespace AlbionAutoBot.App.ViewModels.Components
{
    internal class StartCaptureViewModel : BindableBaseViewModel, IDisposable
    {
        private bool monitoringStatus;

        public StartCaptureViewModel()
        {
            Messenger.Default.Register<UpdateMonitoringStatusMessage>(this, OnUpdateMonitoringStatus);
        }

        #region Commands

        #region StartCaptureCommand

        private RelayCommand startCaptureCommand;

        public RelayCommand StartCaptureCommand => RelayCommand.Register(ref startCaptureCommand, OnStartCapture, CanStartCapture);

        private void OnStartCapture(object commandParameter)
        {
            Messenger.Default.Send(StartCaptureWindowMessage.Instance);
            Messenger.Default.Unregister<UpdateMonitoringStatusMessage>(this);
        }

        private bool CanStartCapture(object commandParameter)
        {
            return monitoringStatus;
        }

        #endregion

        #endregion

        #region Message handlers

        private void OnUpdateMonitoringStatus(UpdateMonitoringStatusMessage message)
        {
            UpdateUI(() =>
            {
                RelayCommand.RaiseCanExecuteChanged();
                monitoringStatus = message.MonitoringStatus;
            });
        }

        #endregion

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

            Messenger.Default.Unregister<UpdateMonitoringStatusMessage>(this);

            disposed = true;
        }

        #endregion
    }
}
