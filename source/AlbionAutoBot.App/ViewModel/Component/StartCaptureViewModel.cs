using System;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Message;
using AlbionAutoBot.App.ViewModel.Base;

namespace AlbionAutoBot.App.ViewModel.Component
{
    internal class StartCaptureViewModel : BindableBaseViewModel, IDisposable
    {
        private bool monitoringStatus;

        public StartCaptureViewModel()
        {
            Messenger.Default.Register<UpdateMonitoringStatusMsg>(this, OnUpdateMonitoringStatus);
        }

        #region Commands

        #region StartCaptureCommand

        private RelayCommand startCaptureCommand;

        public RelayCommand StartCaptureCommand => RelayCommand.Register(ref startCaptureCommand, OnStartCapture, CanStartCapture);

        private void OnStartCapture(object commandParameter)
        {
            Messenger.Default.Send(StartCaptureWindowMsg.Instance);
            Messenger.Default.Register<CloseCaptureWindowMsg>(this, OnClosingCaptureWindow);
            Messenger.Default.Unregister<UpdateMonitoringStatusMsg>(this);
        }

        private bool CanStartCapture(object commandParameter)
        {
            return monitoringStatus;
        }

        #endregion

        #endregion

        #region Message handlers

        private void OnUpdateMonitoringStatus(UpdateMonitoringStatusMsg message)
        {
            UpdateUI(() =>
            {
                RelayCommand.RaiseCanExecuteChanged();
                monitoringStatus = message.MonitoringStatus;
            });
        }

        private void OnClosingCaptureWindow(CloseCaptureWindowMsg message)
        {
            Messenger.Default.Unregister<CloseCaptureWindowMsg>(this);
            Messenger.Default.Register<UpdateMonitoringStatusMsg>(this, OnUpdateMonitoringStatus);
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

            Messenger.Default.Unregister<UpdateMonitoringStatusMsg>(this);
            Messenger.Default.Unregister<CloseCaptureWindowMsg>(this);

            disposed = true;
        }

        #endregion
    }
}
