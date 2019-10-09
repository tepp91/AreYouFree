using System;
using System.Windows;
using System.Windows.Interop;

namespace AreYouFree
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private const int WM_HOTKEY = 0x0312;

        private HotKey hotKey_;
        private MainWindow window_;
        private NotifyIcon notifyIcon_;

        private void Application_Startup( object sender, StartupEventArgs e )
        {
            notifyIcon_ = new NotifyIcon();
            notifyIcon_.Close += () => App.Current.Shutdown();

            window_ = new MainWindow();
            window_.ShowActivated = false;
            window_.Show();
            window_.Hide();

            IntPtr handle = window_.GetHandle();
            hotKey_ = new HotKey();
            hotKey_.Reigster( handle );

            HwndSource source = HwndSource.FromHwnd( handle );
            source.AddHook( new HwndSourceHook( WndProc ) );
        }

        private void Application_Exit( object sender, ExitEventArgs e )
        {
            notifyIcon_.Dispose();
            hotKey_.Unregister();
        }

        private static IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            if( msg == WM_HOTKEY )
            {
                App app = (App)App.Current;
                app.window_.Show();
            }

            return IntPtr.Zero;
        }
    }
}
