using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Messages;
using AlbionAutoBot.App.Commands;
using AlbionAutoBot.App.ViewModels.Base;
using AlbionAutoBot.App.ViewModels.Components;
using AlbionAutoBot.App.Views.Windows;

namespace AlbionAutoBot.App.ViewModels
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

            Messenger.Default.Register<StartCaptureWindowMessage>(this, OnStartCaptureWindow);
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

        private void OnStartCaptureWindow(StartCaptureWindowMessage message)
        {
            SetCollapseCurrentWindow();
            new CaptureViewModel();
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

            Messenger.Default.Unregister<StartCaptureWindowMessage>(this);
        }

        #endregion
    }
}
