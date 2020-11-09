using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using MVVMLight.Messaging;
using AlbionAutoBot.App.Message;
using AlbionAutoBot.App.ViewModel.Base;
using AlbionAutoBot.App.View.Windows;

namespace AlbionAutoBot.App.ViewModel
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

            Messenger.Default.Send(CloseCaptureWindowMsg.Instance);
        }

        #endregion
    }
}
