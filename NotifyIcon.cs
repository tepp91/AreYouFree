using System.Windows.Forms;

namespace AreYouFree
{
    delegate void NotifyIconSettingEventHandler();
    delegate void NotifyIconCloseEventHanlder();

    class NotifyIcon : System.IDisposable
    {
        /// <summary>アイコンイメージファイルのパス</summary>
        private readonly string IconImagePath = "pack://application:,,,/Resources/icon.ico";
        /// <summary>通知アイコン本体</summary>
        private System.Windows.Forms.NotifyIcon notifyIcon_;

        /// <summary>
        /// 終了イベント
        /// </summary>
        public event NotifyIconCloseEventHanlder Close = delegate { };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NotifyIcon()
        {
            notifyIcon_ = new System.Windows.Forms.NotifyIcon();
            notifyIcon_.Visible = true;

            System.IO.Stream stream = App.GetResourceStream( new System.Uri( IconImagePath ) ).Stream;
            notifyIcon_.Icon = new System.Drawing.Icon( stream );

            var menuStrip = new ContextMenuStrip();

            // 閉じる
            var itemClose = new ToolStripMenuItem();
            itemClose.Text = "閉じる";
            itemClose.Click += ( s, e ) => Close();
            menuStrip.Items.Add( itemClose );

            notifyIcon_.ContextMenuStrip = menuStrip;
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~NotifyIcon()
        {
            Dispose();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            if( notifyIcon_ != null )
            {
                notifyIcon_.Visible = false;
                notifyIcon_.Icon = null;
                notifyIcon_.Dispose();
                notifyIcon_ = null;
            }
        }
    }
}
