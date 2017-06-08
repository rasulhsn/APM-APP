using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrecnyAPPattern.Core
{
    public sealed class CoreMessage : IMessage
    {
        private IDictionary _dictionary;
        //private WaitCallback _wCallBack;
        //private object _objState;

        public CoreMessage(WaitCallback Task,AsyncCallback CallBack,object State)
        {
            _dictionary = new Hashtable();
            _dictionary.Add("asyncTask", Task);
            _dictionary.Add("asyncCallBack", CallBack);
            _dictionary.Add("asyncState", State);
        }

        public IDictionary Properties
        {
            get
            {
                return _dictionary;
            }
        }
    }
}
