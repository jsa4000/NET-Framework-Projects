using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ButtonClicking
{
	class Program
	{
		const int WM_COMMAND = 0x0111;
        const int WM_DROPFILES = 0x233;
		const int BN_CLICKED = 0;
        const int ButtonId = 0x3F0;
        const int ListId = 0x3EB;

        const string fn = @"C:\Users\javier.santos\Desktop\New folder\WAAS_FDR.exe";
                
      	[DllImport("user32.dll")]
		static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int GlobalLock(IntPtr Handle);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int GlobalUnlock(IntPtr Handle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        
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
        class DROPFILES
        {
            public int size;    //<-- offset to filelist (this should be defined 20)
            public POINT pt;    //<-- where we "release the mouse button"
            public bool fND;    //<-- the point origins (0;0) (this should be false, if true, the origin will be the screens (0;0), else, the handle the the window we send in PostMessage)
            public bool WIDE;   //<-- ANSI or Unicode (should be false)
        }

        public static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

		static void Main(string[] args)
		{
            //IntPtr handle = IntPtr.Zero;
            //Process[] localAll = Process.GetProcesses();
            //Process mainProc = null;
            //foreach (Process proc in localAll)
            //{
            //    if (proc.MainWindowHandle != IntPtr.Zero)
            //    {
            //        ProcessModule pm = GetModule(proc);
            //        if (pm != null && proc.MainModule.FileName.ToUpper() == fn.ToUpper()) { 
            //            handle = proc.MainWindowHandle;
            //            mainProc = proc;
            //        }
            //    }
            //}

            ProcessStartInfo startInfo = new ProcessStartInfo();
	        startInfo.FileName = fn;
	        Process.Start(startInfo);

            IntPtr handle = IntPtr.Zero;
            Process mainProc = null;

            while (handle == IntPtr.Zero ) {

                Process[] localAll = Process.GetProcesses();
                foreach (Process proc in localAll)
                {
                    if (proc.MainWindowHandle != IntPtr.Zero)
                    {
                        ProcessModule pm = GetModule(proc);
                        if (pm != null && proc.MainModule.FileName.ToUpper() == fn.ToUpper()) { 
                            handle = proc.MainWindowHandle;
                            mainProc = proc;
                        }
                    }
                }
            }
           
			if (handle == IntPtr.Zero)
			{
				Console.WriteLine("Not found");
				return;
			}
			Console.WriteLine("{0:X}", handle);

            //Process the file computation'
            IntPtr hWndList = GetDlgItem(handle, ListId);
            
            //IntPtr hwnd = this.Handle;
            DROPFILES s = new DROPFILES();
            s.size = 20;                            //<-- 20 is the size of this struct in memory
            s.pt = new POINT(10, 10);    //<-- drop file 20 pixels from left, total height minus 40 from top
            s.fND = false;                          //<-- the point 0;0 will be in the window
            s.WIDE = false;                         //<-- ANSI
            
            string[] files = {"C:\\Universal.0000\0","C:\\Universal.0001\0"};         //<-- add null terminator at end
           
            Int32 lengthFiles = 0;
            foreach (String file in files) {
                lengthFiles += file.Length;
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
                Console.WriteLine("Wrote header byte " + i.ToString() + " of " + size.ToString());
            }

           foreach (String file in files)       {
               byte[] b = ASCIIEncoding.ASCII.GetBytes(file); //<-- convert filepath to bytearray
               for (int k = 0; k < file.Length; k++)
               {
                   Marshal.WriteByte(p, i, b[k]);
                   Console.WriteLine("Wrote filename byte " + i.ToString() + " of " + size.ToString());
                   i++;
               }
           }
      
            Marshal.WriteByte(p, i, 0);

            GlobalUnlock(p);
            PostMessage(handle, WM_DROPFILES, p, IntPtr.Zero);
                
            //Process the file computation'
			IntPtr hWndButton = GetDlgItem(handle, ButtonId);

            //Get default text
            String DefaultButtonText = String.Empty;
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(hWndButton, Buff, nChars) > 0)
            {
                DefaultButtonText = Buff.ToString();
            }

			int wParam = (BN_CLICKED << 16) | (ButtonId & 0xffff);
			SendMessage(handle, WM_COMMAND, wParam, hWndButton);
			Console.WriteLine("Message sent");
                
            Buff.Length = 0;
            string CurrentButtonText = String.Empty;
            while (CurrentButtonText != DefaultButtonText) {
              if (GetWindowText(hWndButton, Buff, nChars) > 0){
                   CurrentButtonText =Buff.ToString();
                   Buff.Length = 0;
              }
             }
            
            //kILL PROCESS
            mainProc.Kill();
            //mainProc.WaitForExit();
            
            //Release all objects'
            //Marshal.FreeHGlobal(p);
           
		}

		/// <summary>
		/// Some modules might be restricted from access for security purposes.
		/// This will catch that error and others.
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private static ProcessModule GetModule(Process p)
		{
			ProcessModule pm = null;
			try { pm = p.MainModule; }
			catch
			{
				return null;
			}
			return pm;
		}
	}
}
