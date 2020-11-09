using System;
using System.ComponentModel;
using System.Windows;

namespace AlbionAutoBot.App.ViewModel.Base
{
    internal abstract class WindowBaseViewModel<TWindow> : BindableBaseViewModel, IDisposable
        where TWindow : Window
    {
        private bool disposed;

        private TWindow window;

        protected WindowBaseViewModel()
        {
            SetWindow();
            BindingToWindow();
        }

        private void BindingToWindow()
        {
            window = Activator.CreateInstance<TWindow>();

            window.DataContext = this;
            window.Closing += OnClosing;
            window.Show();
        }

        protected virtual void OnClosing(object sender, CancelEventArgs e)
        {

        }

        protected virtual void SetWindow()
        {

        }

        public void SetCollapseCurrentWindow()
        {
            Application.Current.MainWindow.Visibility = Visibility.Collapsed;
        }

        public void SetVisibleCurrentWindow()
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

        public void SubscribeToEventClossing(CancelEventHandler eventHandler)
        {
            Application.Current.MainWindow.Closing += eventHandler;
        }

        public void CloseCurrentMainWindow()
        {
            Application.Current.MainWindow.Close();
        }

        public void CloseAllOwnedCurrentWindow()
        {
            var views = Application.Current.MainWindow.OwnedWindows;

            for (var i = 0; i < views.Count; i++)
            {
                views[i].Close();
            }
        }

        public double GetWorkAreaWidth()
        {
            return SystemParameters.WorkArea.Width;
        }

        public double GetWorkAreaHeight()
        {
            return SystemParameters.WorkArea.Height;
        }

        public Point GetCurrentWindowCoordinate()
        {
            var view = Application.Current.MainWindow;
            var x = view.Left;
            var y = view.Top;
            var point = new Point(x, y);

            return point;
        }

        #region Implementation IDisposable

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

            window.Closing -= OnClosing;

            disposed = true;
        }

        #endregion
    }
}
