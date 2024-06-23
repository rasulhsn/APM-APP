using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ConcurrecnyAPPattern.Core
{
    public class CoreAsyncResult : IAsyncResult, IMessageSink
    {
        private IMessage _rMessage;
        private ManualResetEvent _waitHandle;

        private bool _isCompleted;
        private bool _isInvokeAsyncCallBack;
        private object _state;

        private WaitCallback _task;
        private AsyncCallback _callBack;

        public CoreAsyncResult()
        {
            this._waitHandle = new ManualResetEvent(false);
            this._isCompleted = false;
            this._isInvokeAsyncCallBack = false;
        }

        public bool IsInvokeAsyncCallBack
        {
            get
            {
                return this._isInvokeAsyncCallBack;
            }
        }

        public object AsyncState
        {
            get
            {
                return _state;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                return _waitHandle;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return this._isCompleted;
            }
        }

        public IMessageSink NextSink
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        private void asyncTask(object asyncResult)
        {
            if (_task != null)
                _task.Invoke(null);

            if(_callBack != null)
            {
                this._isInvokeAsyncCallBack = false;
                this._callBack.Invoke(this);
            }

            this._waitHandle.Set();
            this._isCompleted = true;
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            this._rMessage = msg;
            this._task = (WaitCallback)msg.Properties["asyncTask"];
            this._callBack = (AsyncCallback)msg.Properties["asyncCallBack"];
            this._state = (object)msg.Properties["asyncState"];

            ThreadPool.QueueUserWorkItem(asyncTask, this);

            return this._rMessage;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotImplementedException();
        }
    }
}
