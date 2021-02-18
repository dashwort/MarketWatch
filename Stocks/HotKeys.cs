using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Stocks
{
    public class HotKeys
    {
        public HotKeys()
        {

        }


        private const uint MOD_NONE = 0x0000; //[NONE]
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        // private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] uint fsModifiers, [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        // called when hotkey is pressed
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        void RegisterHotkeySetup()
        {
            //if (Application.Current.MainWindow != null)
            //{
            //    var helper = new WindowInteropHelper(Application.Current.MainWindow);
            //    _source = HwndSource.FromHwnd(helper.Handle);
            //    _source.AddHook(HwndHook);
            //    RegisterHotKey();
            //}
        }

        private void RegisterHotKey()
        {
            //var helper = new WindowInteropHelper(Application.Current.MainWindow);
            //const uint VK_F10 = 0x79;
            //const uint MOD_CTRL = 0x0002;

            //if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_F10))
            //{
            //    Console.WriteLine("Warning unable to register hotkey");
            //}
        }

        private void UnregisterHotKey()
        {
            //var helper = new WindowInteropHelper(Application.Current.MainWindow);
            //UnregisterHotKey(helper.Handle, HOTKEY_ID);
            //Console.WriteLine("Unregistering hotkeys");
        }
    }
}
