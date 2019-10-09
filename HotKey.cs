using System;
using System.Runtime.InteropServices;

namespace AreYouFree
{
    class HotKey
    {
        const int MOD_ALT = 0x0001;
        const int MOD_CTRL = 0x0002;
        const int MOD_SHIFT = 0x0004;
        const int MOD_WIN = 0x0008;

        IntPtr hWnd_;
        int id_;

        public bool Reigster( IntPtr hWnd )
        {
            int modKey = MOD_ALT | MOD_CTRL | MOD_SHIFT;
            int key = 'I';

            for( int i = 0; i <= 0xbfff; i++ )
            {
                if( RegisterHotKey( hWnd, i, modKey, key ) != 0 )
                {
                    hWnd_ = hWnd;
                    id_ = i;
                    return true;
                }
            }
            return false;
        }

        public void Unregister()
        {
            UnregisterHotKey( hWnd_, id_ );
        }

        [DllImport( "user32.dll" )]
        extern static int RegisterHotKey( IntPtr hWnd, int id, int modKey, int key );

        [DllImport( "user32.dll" )]
        extern static int UnregisterHotKey( IntPtr hWnd, int id ); 
    }
}
