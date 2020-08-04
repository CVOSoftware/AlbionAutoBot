using System;
using System.Windows;
using System.ComponentModel;
using AlbionAutoBot.App.ViewModels.Base;

namespace AlbionAutoBot.App.Helpers
{
    public static class WindowHelper
    {
        public static void CollapseCurrentWindow()
        {
            Application.Current.MainWindow.Visibility = Visibility.Collapsed;
        }

        public static void VisibleCurrentWindow()
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

        public static void SubscribeToEventClossing(CancelEventHandler eventHandler)
        {
            Application.Current.MainWindow.Closing += eventHandler;
        }

        public static void Open<TView>(BaseViewModel viewModel, Action closeAction) where TView : Window
        {
            var view = Activator.CreateInstance<TView>();

            view.Owner = Application.Current.MainWindow;
            view.DataContext = viewModel;
            view.Closing += (o, e) => closeAction();
            view.Show();
        }

        public static void CloseCurrentMainWindow()
        {
            Application.Current.MainWindow.Close();
        }

        public static void CloseAllOwnedCurrentWindow()
        {
            var views = Application.Current.MainWindow.OwnedWindows;

            for(var i = 0; i < views.Count; i++)
            {
                views[i].Close();
            }
        }

        public static Point GetCurrentWindowCoordinate()
        {
            var view = Application.Current.MainWindow;
            var x = view.Left;
            var y = view.Top;
            var point = new Point(x, y);

            return point;
        }

        public static double GetWorkAreaWidth()
        {
            return SystemParameters.WorkArea.Width;
        }

        public static double GetWorkAreaHeight()
        {
            return SystemParameters.WorkArea.Height;
        }
    }
}
