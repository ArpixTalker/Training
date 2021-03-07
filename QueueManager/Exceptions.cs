using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    [Serializable]
    class _MessageQueueException : Exception
    {
        public _MessageQueueException()
        {

        }

        public _MessageQueueException(string message)
            : base(String.Format($"MessageQueueException: {message}"))
        {

        }
    }

    [Serializable]
    class _QueueNameNotUniqueException : Exception
    {
        public _QueueNameNotUniqueException() {

        }

        public _QueueNameNotUniqueException(string message)
             : base(String.Format($"QueueNameNotUniqueException: {message}"))
        {

        }
    }

    class _MessageQueueDoesNotExistException : Exception {


        public _MessageQueueDoesNotExistException() {
            
        }

        public _MessageQueueDoesNotExistException(string message)
            : base (String.Format($"MessageQueueDoesNotExistException: {message}")) {

        }
    }
}
