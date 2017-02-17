using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ButtonClickForm
{
    public class UNIVERSALHack
    {
        private const int DEFAULT_TIMEOUT = 60000;

        public static int SW_HIDE = 0;
        public static int SW_SHOW = 5;
        public static int WS_SYSMENU = 0x80000;
        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000; //child window
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar
        
        public const int WM_COMMAND = 0x0111;
        public const int WM_DROPFILES = 0x233;
        public const int BN_CLICKED = 0;
        private const int ButtonId = 0x3F0;
        private const int ListId = 0x3EB;

        private const int nChars = 256;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int GlobalLock(IntPtr Handle);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int GlobalUnlock(IntPtr Handle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
         
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 X;
            public Int32 Y;

            public POINT(Int32 x, Int32 y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DROPFILES
        {
            public int size;    //<-- offset to filelist (this should be defined 20)
            public POINT pt;    //<-- where we "release the mouse button"
            public bool fND;    //<-- the point origins (0;0) (this should be false, if true, the origin will be the screens (0;0), else, the handle the the window we send in PostMessage)
            public bool WIDE;   //<-- ANSI or Unicode (should be false)
        }
        
        public string AppFolder = String.Empty;
        public string FilesPath = String.Empty;

        public bool IsOpen = false;

        public IntPtr Handle = IntPtr.Zero;
        private IntPtr hWndButton = IntPtr.Zero;
        private IntPtr hWndList = IntPtr.Zero;
        private Process MainProc = null;
        private String DefaultButtonText = String.Empty;

        public UNIVERSALHack(string AppFolder, string FilesPath)
        {
            this.AppFolder = AppFolder;
            this.FilesPath = FilesPath;
        }

        private byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }
                
        private ProcessModule GetModule(Process p)
        {
            ProcessModule pm = null;
            try { pm = p.MainModule; }
            catch
            {
                return null;
            }
            return pm;
        }

        public bool Open() 
        {
            //Check if it's already opened
            if (IsOpen) return false;
           
            //Check if the executable exists
            string AppPath = AppFolder + @"\WAAS_FDR.exe";
            if (!System.IO.File.Exists(AppPath)) return false;
                      
            try {
                //Start the Application
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = AppPath;
                Process.Start(startInfo);

                //Initialize variables
                Handle = IntPtr.Zero;
                hWndButton = IntPtr.Zero;
                DefaultButtonText = String.Empty;
                MainProc = null;

                //Find until fonuded or timeOut
                DateTime start = DateTime.Now;
                while (Handle == IntPtr.Zero && DateTime.Now.Subtract(start).Milliseconds < DEFAULT_TIMEOUT)
                {
                    Process[] localAll = Process.GetProcesses();
                    foreach (Process proc in localAll)
                    {
                        if (proc.MainWindowHandle != IntPtr.Zero)
                        {
                            ProcessModule pm = GetModule(proc);
                            if (pm != null && proc.MainModule.FileName.ToUpper() == AppPath.ToUpper())
                            {
                                Handle = proc.MainWindowHandle;
                                MainProc = proc;
                            }
                        }
                    }
                }
          
                if (Handle == IntPtr.Zero)
                    return false;

                //Get the handle of the button
                hWndButton = GetDlgItem(Handle, ButtonId);
                //Get the handle of the list
                hWndList = GetDlgItem(Handle, ListId);
            
                //Get default text of the button
                StringBuilder Buff = new StringBuilder(nChars);
                if (GetWindowText(hWndButton, Buff, nChars) > 0)
                    DefaultButtonText = Buff.ToString();
            
                //Is opend
                IsOpen = true;

            }
            catch (Exception ex)
            {
                //Exception
                return false;
            }

            return true;
        }

        public bool Close()
        {
            if (!IsOpen) return false;

            try { 
                string CurrentButtonText = String.Empty;
                StringBuilder Buff = new StringBuilder(nChars);
                while (CurrentButtonText != DefaultButtonText)
                {
                    if (GetWindowText(hWndButton, Buff, nChars) > 0)
                    {
                        CurrentButtonText = Buff.ToString();
                        Buff.Length = 0;
                    }
                }

                //Kill process
                MainProc.Kill();

                //Initialize variables
                Handle = IntPtr.Zero;
                hWndButton = IntPtr.Zero;
                hWndList = IntPtr.Zero;
                DefaultButtonText = String.Empty;
                MainProc = null;

                //Set IsOpen to false
                IsOpen = false;
            }
            catch (Exception ex)
            {
                //Exception
                return false;
            }

            return true;
        }

        public bool StartProcess()
        {
            if (!IsOpen || !System.IO.Directory.Exists(FilesPath)) return false;
            
            //IntPtr hwnd = this.Handle;
            DROPFILES s = new DROPFILES();
            s.size = 20;                            //<-- 20 is the size of this struct in memory
            s.pt = new POINT(10, 10);    //<-- drop file 20 pixels from left, total height minus 40 from top
            s.fND = false;                          //<-- the point 0;0 will be in the window
            s.WIDE = false;                         //<-- ANSI
            
            //Get all files from the folder

            string[] files = System.IO.Directory.GetFiles(FilesPath);
            //string[] files = { "C:\\Universal.0000\0", "C:\\Universal.0001\0" };         //<-- add null terminator at end

            Int32 lengthFiles = 0;
            foreach (String file in files)
            {
                lengthFiles += file.Length + 2;  //<-- add null terminator at end
            }

            Int32 filelen = Convert.ToInt32(lengthFiles);
            byte[] bytes = RawSerialize(s);
            int structlen = (int)bytes.Length;
            int size = structlen + filelen + 1;
            IntPtr p = Marshal.AllocHGlobal(size);  //<-- allocate memory and save pointer to p
            GlobalLock(p);                          //<-- lock p

            int i = 0;
            for (i = 0; i < structlen; i++)
            {
                Marshal.WriteByte(p, i, bytes[i]);
            }

            foreach (String file in files)
            {
                byte[] b = ASCIIEncoding.ASCII.GetBytes(file + "\0"); //<-- convert filepath to bytearray //<-- add null terminator at end
                for (int k = 0; k < b.Length; k++)
                {
                    Marshal.WriteByte(p, i, b[k]);
                    i++;
                }
            }

            //Write the end of the parameters to Send to the Window
            Marshal.WriteByte(p, i, 0);

            GlobalUnlock(p);
            PostMessage(Handle, WM_DROPFILES, p, IntPtr.Zero);
            
            //Process the files dragged
           int wParam = (BN_CLICKED << 16) | (ButtonId & 0xffff);
           SendMessage(Handle, WM_COMMAND, wParam, hWndButton);
                     
            //Release all objects'
            //Marshal.FreeHGlobal(p);

            return true;
        }

    }
}
