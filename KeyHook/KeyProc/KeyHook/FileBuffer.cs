using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KeyHook
{

    public enum BufferStateType
    {
        STREAM1,
        STREAM2
    }

    public class FileBuffer
    {

        #region Members

        private const int MAX_STREAM = 65536;
               
        private StringBuilder Stream1 = null;
        private StringBuilder Stream2 = null;

        private BufferStateType _bufferState;
        public BufferStateType BufferState
        {
            get { return _bufferState; }
        }


        private StringBuilder _currentStream = null;
        public StringBuilder CurrentStream
        {
            get { return _currentStream; }
        }

        #endregion

        #region Constructor

        private FileBuffer()
        {
            //Create the StreamWriter
            Stream1 = new StringBuilder(MAX_STREAM);
            Stream2 = new StringBuilder(MAX_STREAM);

            //Set the first stream to use
            _currentStream = Stream1;
            _bufferState = BufferStateType.STREAM1;
        }

        #endregion

        #region Singletone Members

        private static object myLock = new object();
        private static FileBuffer mySingleton = null;


        public static FileBuffer GetInstance()
        {
            if (mySingleton == null)
            { // 1st check
                lock (myLock)
                {
                    if (mySingleton == null)
                    { // 2nd (double) check
                        mySingleton = new FileBuffer();
                    }
                }
            }
            return mySingleton;
        }

        #endregion

        #region Private Memebers

      

        #endregion

        #region Public Members

        public void SwitchBuffer()
        {
            //Change the current buffer
            if (BufferState == BufferStateType.STREAM1)
            {
                _currentStream = Stream2;
                _bufferState = BufferStateType.STREAM2;
            }
            else
            {
                _currentStream = Stream1;
                _bufferState = BufferStateType.STREAM1;
            }
        }

        public void Write(String pText)
        {
            //Check the buffer's length
            if ((_currentStream.Length + pText.Length) >= MAX_STREAM)
                SwitchBuffer();
            
            //Write in current buffer
            _currentStream.Append(pText);
        }

        #endregion
    }
}
