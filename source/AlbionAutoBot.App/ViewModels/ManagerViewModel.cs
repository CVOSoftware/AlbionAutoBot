using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Commands;
using AlbionAutoBot.App.ViewModels.Base;
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

        #endregion

        public ManagerViewModel() : base()
        {
            SetWindow();
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

        #endregion

        #region Commands

        #region StartCaptureCommand

        private RelayCommand startCaptureCommand;

        public RelayCommand StartCaptureCommand => RelayCommand.Register(ref startCaptureCommand, OnStartCapture, CanStartCapture);

        private void OnStartCapture(object commandParameter)
        {
            var viewModel = new CaptureViewModel();

            SetCollapseCurrentWindow();
        }

        private bool CanStartCapture(object commandParameter)
        {
            return true;
        }

        #endregion

        #region CloseCommand

        private RelayCommand closeCommand;

        public RelayCommand CloseCommand => RelayCommand.Register(ref closeCommand, OnClose);

        private void OnClose(object commandParameter)
        {
            CloseCurrentMainWindow();
        }

        #endregion

        #endregion

        #region Implementation WindowBaseViewModel

        protected override void OnClosing(object sender, CancelEventArgs e)
        {
            base.OnClosing(sender, e);
        }

        #endregion

        private void SetWindow()
        {
            Width = 300;
            Height = 400;
            CoordinateX = GetWorkAreaWidth() - width - 20;
            CoordinateY = GetWorkAreaHeight() - height - 20;
        }
    }
}
