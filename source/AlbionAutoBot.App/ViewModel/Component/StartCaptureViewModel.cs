using System;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Message;
using AlbionAutoBot.App.ViewModel.Base;

namespace AlbionAutoBot.App.ViewModel.Component
{
    internal class StartCaptureViewModel : BindableBaseViewModel, IDisposable
    {
        private bool _monitoringStatus;

        public StartCaptureViewModel()
        {
            Messenger.Default.Register<UpdateMonitoringStatusMsg>(this, OnUpdateMonitoringStatus);
        }

        #region Commands

        #region StartCaptureCommand

        private RelayCommand _startCaptureCommand;

        public RelayCommand StartCaptureCommand => RelayCommand.Register(ref _startCaptureCommand, OnStartCapture, CanStartCapture);

        private void OnStartCapture(object commandParameter)
        {
            Messenger.Default.Send(StartCaptureWindowMsg.Instance);
            Messenger.Default.Register<CloseCaptureWindowMsg>(this, OnClosingCaptureWindow);
            Messenger.Default.Unregister<UpdateMonitoringStatusMsg>(this);
        }

        private bool CanStartCapture(object commandParameter)
        {
            return _monitoringStatus;
        }

        #endregion

        #endregion

        #region Message handlers

        private void OnUpdateMonitoringStatus(UpdateMonitoringStatusMsg message)
        {
            UpdateUI(() =>
            {
                RelayCommand.RaiseCanExecuteChanged();
                _monitoringStatus = message.MonitoringStatus;
            });
        }

        private void OnClosingCaptureWindow(CloseCaptureWindowMsg message)
        {
            Messenger.Default.Unregister<CloseCaptureWindowMsg>(this);
            Messenger.Default.Register<UpdateMonitoringStatusMsg>(this, OnUpdateMonitoringStatus);
        }

        #endregion

        #region Implementation IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

            }

            Messenger.Default.Unregister<UpdateMonitoringStatusMsg>(this);
            Messenger.Default.Unregister<CloseCaptureWindowMsg>(this);

            _disposed = true;
        }

        #endregion
    }
}
