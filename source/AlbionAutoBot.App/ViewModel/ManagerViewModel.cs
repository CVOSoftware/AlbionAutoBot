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

        private double width;

        private double height;

        private double coordinateX;

        private double coordinateY;

        private StartCaptureViewModel startCaptureVM;

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
            get => width;
            set => SetValue(ref width, value);
        }

        public double Height
        {
            get => height;
            set => SetValue(ref height, value);
        }

        public double CoordinateX
        {
            get => coordinateX;
            set => SetValue(ref coordinateX, value);
        }

        public double CoordinateY
        {
            get => coordinateY;
            set => SetValue(ref coordinateY, value);
        }

        public StartCaptureViewModel StartCaptureVM
        {
            get => startCaptureVM;
            set => SetValue(ref startCaptureVM, value);
        }

        #endregion

        #region Commands

        #region CloseCommand

        private RelayCommand closeCommand;

        public RelayCommand CloseCommand => RelayCommand.Register(ref closeCommand, OnClose);

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
            width = 300;
            height = 400;
            CoordinateX = GetWorkAreaWidth() - width - 20;
            CoordinateY = GetWorkAreaHeight() - height - 20;
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
