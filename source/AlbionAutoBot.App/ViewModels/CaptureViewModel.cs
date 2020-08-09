using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Messages;
using AlbionAutoBot.App.ViewModels.Base;
using AlbionAutoBot.App.Views.Windows;

namespace AlbionAutoBot.App.ViewModels
{
    internal class CaptureViewModel : WindowBaseViewModel<CaptureWindow>
    {
        public CaptureViewModel() : base()
        {

        }

        #region Implementation WindowBaseViewModel

        protected override void OnClosing(object sender, CancelEventArgs e)
        {
            base.OnClosing(sender, e);

            Messenger.Default.Send(ClosingCaptureWindowMessage.Instance);
        }

        #endregion
    }
}
