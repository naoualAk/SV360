using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements
{
    class CloseMessageBox 
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        const UInt32 WM_CLOSE = 0x0010;

        Thread thread;

        public CloseMessageBox()
        {

            thread = new Thread(Show);
            thread.Start();
        }

        public void Close()
        {
            IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, "Connexion M221");
            if (hWnd != IntPtr.Zero)
                PostMessage(hWnd, WM_CLOSE, 0, 0);

            if (thread.IsAlive)
                thread.Abort();
        }

        public void Show()
        {
            MessageBox.Show("Connexion à l'automate", "Connexion M221");
        }
    }
}
