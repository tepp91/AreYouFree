using System;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace AreYouFree
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TRANSPARENT = 0x00000020;


        public MainWindow()
        {
            InitializeComponent();
        }

        public IntPtr GetHandle()
        {
            return new WindowInteropHelper( this ).Handle;
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            base.OnSourceInitialized( e );

            // クリックスルーにする
            IntPtr handle = new WindowInteropHelper( this ).Handle;

            int extendStyle = GetWindowLong( handle, GWL_EXSTYLE );
            extendStyle |= WS_EX_TRANSPARENT;
            SetWindowLong( handle, GWL_EXSTYLE, extendStyle );
        }

        private void Window_ContentRendered( object sender, EventArgs e )
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan( 0, 0, 5 );
            timer.Tick += ( s, ev ) => this.Hide();
            timer.Start();

        }

        [DllImport( "user32.dll" )]
        static extern int GetWindowLong( IntPtr hwnd, int index );

        [DllImport( "user32.dll" )]
        static extern int SetWindowLong( IntPtr hWnd, int index, int dwLong );
    }
}
