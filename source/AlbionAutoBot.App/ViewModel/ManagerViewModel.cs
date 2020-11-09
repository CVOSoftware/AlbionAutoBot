using System.ComponentModel;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Message;
using AlbionAutoBot.App.ViewModel.Base;
using AlbionAutoBot.App.ViewModel.Component;
using AlbionAutoBot.App.View.Windows;

namespace AlbionAutoBot.App.ViewModel
{
    internal class ManagerViewModel : WindowBaseViewModel<ManagerWindow>
    {
        #region Fields

        private double _width;

        private double _height;

        private double _coordinateX;

        private double _coordinateY;

        private StartCaptureViewModel _startCaptureVM;

        #endregion

        public ManagerViewModel() : base()
        {
            StartCaptureVM = new StartCaptureViewModel();

            Messenger.Default.Register<StartCaptureWindowMsg>(this, OnStartCaptureWindow);
            Messenger.Default.Register<CloseCaptureWindowMsg>(this, OnClosingCaptureWindow);
        }

        #region Properties

        public double Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }

        public double Height
        {
            get => _height;
            set => SetValue(ref _height, value);
        }

        public double CoordinateX
        {
            get => _coordinateX;
            set => SetValue(ref _coordinateX, value);
        }

        public double CoordinateY
        {
            get => _coordinateY;
            set => SetValue(ref _coordinateY, value);
        }

        public StartCaptureViewModel StartCaptureVM
        {
            get => _startCaptureVM;
            set => SetValue(ref _startCaptureVM, value);
        }

        #endregion

        #region Commands

        #region CloseCommand

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand => RelayCommand.Register(ref _closeCommand, OnClose);

        private void OnClose(object commandParameter)
        {
            CloseCurrentMainWindow();
        }

        #endregion

        #endregion

        #region Message handlers

        private void OnStartCaptureWindow(StartCaptureWindowMsg message)
        {
            SetCollapseCurrentWindow();
            new CaptureViewModel();
        }

        private void OnClosingCaptureWindow(CloseCaptureWindowMsg message)
        {
            SetVisibleCurrentWindow();
        }

        #endregion

        #region Implementation WindowBaseViewModel

        protected override void OnClosing(object sender, CancelEventArgs e)
        {
            base.OnClosing(sender, e);
        }

        protected override void SetWindow()
        {
            _width = 300;
            _height = 400;
            CoordinateX = GetWorkAreaWidth() - _width - 20;
            CoordinateY = GetWorkAreaHeight() - _height - 20;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            StartCaptureVM.Dispose();    

            Messenger.Default.Unregister<StartCaptureWindowMsg>(this);
            Messenger.Default.Unregister<CloseCaptureWindowMsg>(this);
        }

        #endregion
    }
}
