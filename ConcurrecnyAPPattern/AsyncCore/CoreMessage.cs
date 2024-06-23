using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace ConcurrecnyAPPattern.Core
{
    public sealed class CoreMessage : IMessage
    {
        private IDictionary _dictionary;

        public CoreMessage(WaitCallback Task, AsyncCallback CallBack, object State)
        {
            _dictionary = new Hashtable
            {
                { "asyncTask", Task },
                { "asyncCallBack", CallBack },
                { "asyncState", State }
            };
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
