using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbionAutoBot.App.Commands;
using AlbionAutoBot.App.Helpers;
using AlbionAutoBot.App.ViewModels.Base;
using AlbionAutoBot.App.Views.Windows;
using MVVMLight.Messaging;

namespace AlbionAutoBot.App.ViewModels
{
    internal class ManagerViewModel : BaseViewModel
    {
        #region Fields

        private double width;

        private double height;

        private double coordinateX;

        private double coordinateY;

        #endregion

        public ManagerViewModel()
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

            WindowHelper.CollapseCurrentWindow();
            WindowHelper.Open<CaptureWindow>(
                viewModel: viewModel, 
                closeAction: () => {
                WindowHelper.VisibleCurrentWindow();
            });
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
            WindowHelper.CloseCurrentMainWindow();
        }

        #endregion

        #endregion

        private void SetWindow()
        {
            width = 300;
            height = 400;
            coordinateX = WindowHelper.GetWorkAreaWidth() - width - 20;
            coordinateY = WindowHelper.GetWorkAreaHeight() - height - 20;
        }
    }
}
