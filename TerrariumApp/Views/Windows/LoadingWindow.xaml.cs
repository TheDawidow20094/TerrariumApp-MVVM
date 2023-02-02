using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using TerrariumApp.Helpers;

namespace TerrariumApp
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private string _loadingText = string.Empty;
        private Thread _statusThread = null;
        private LoadingWindow _popUp = null;
        private AutoResetEvent _popUpStarted = null;
        private Window _owner;

        public LoadingWindow(string loadingText = "")
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(_loadingText))
            {
                _loadingText = Globals.Translation.LoadingText;
            }
            txtblLoadingText.Text = _loadingText;
        }

        public void Start(Window owner = null, string loadingText = "")
        {
            try
            {
                if (owner != null)
                {
                    _owner = owner;
                    _owner.IsEnabled = false;
                }
                _loadingText = loadingText;
                _statusThread = new Thread(() =>
                {
                    _popUp = new LoadingWindow(loadingText);
                    _popUp.Show();
                    _popUpStarted.Set();
                    System.Windows.Threading.Dispatcher.Run();
                });
                _statusThread.SetApartmentState(ApartmentState.STA);
                _statusThread.Priority = ThreadPriority.Normal;
                _statusThread.Start();
                _popUpStarted = new AutoResetEvent(false);
                _popUpStarted.WaitOne();
            }
            catch (Exception ex)
            {
                Globals.Log.WriteLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, Common.LogType.CriticalError, Globals.LocalUserData.UserId, Globals.LocalUserData.UserName);
            }
        }

        public void Stop()
        {
            if (_owner != null)
            {
                _owner.IsEnabled = true;
            }
            if (_popUp != null)
            {
                _popUp.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _popUp.Close();
                }));
            }
        }

        public void StopAndClose()
        {
            if (_owner != null)
            {
                _owner.IsEnabled = true;
            }
            if (_popUp != null)
            {
                _popUp.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _popUp.Close();
                }));
            }
            Owner = null;
            this.Close();
        }
    }
}

