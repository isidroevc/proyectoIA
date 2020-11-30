using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIA.Util
{
    class CapturadorDefault : ICapturador
    {
        public int Frecuencia { get; set; }
        public EventHandler<Stream> OnScreenShot { get; set; }

        public List<Stream> GetImage()
        {
            List<Process> processes = new List<Process>(Process.GetProcessesByName("chrome"));
            List<Stream> streams = new List<Stream>();
            RECT rect = new RECT();
            foreach (Process process in processes)
            {
                if (process.MainWindowHandle == IntPtr.Zero) continue;
                GetWindowRect(process.MainWindowHandle, ref rect);
                Stream currentStream = new MemoryStream();
                int height = Math.Abs(rect.Top - rect.Bottom);
                int width = Math.Abs(rect.Right - rect.Left);
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
                    }
                    bitmap.Save(currentStream, ImageFormat.Bmp);
                    streams.Add(currentStream);
                }
            }
            
            for(int i = 0, c = Screen.AllScreens.Length; i < c; i++)
            {

            }
            return streams;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public void StartCapture()
        {
            throw new NotImplementedException();
        }
    }
}
