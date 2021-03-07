using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    public class MessageQueue
    {
        private Queue<string> mq;
        public int MessageLimit;
        public int ConnectedInputs;
        public int ConnectedOutputs;
        public string QueueName;
        public Encoding QueueEncoding;

        public MessageQueue(string name, int messageLimit, Encoding encoding) {

            this.QueueName = name;
            this.mq = new Queue<string>();
            this.ConnectedInputs = 0;
            this.ConnectedOutputs = 0;
            this.QueueEncoding = encoding;
            this.MessageLimit = messageLimit;
        }

        public MessageQueue(string name, int messageLimit)
        {

            this.QueueName = name;
            this.mq = new Queue<string>();
            this.ConnectedInputs = 0;
            this.ConnectedOutputs = 0;
            this.QueueEncoding = Encoding.UTF8;
            this.MessageLimit = messageLimit;
        }

        /* METHODS */

        public void Enqueue(string message) {

            if (this.MessageCount < this.MessageLimit)
            {
                this.mq.Enqueue(message);
            }
            else {

                throw new _MessageQueueException($"Message queue limit ({this.MessageLimit}) exceeded");
            }
        }

        public string Dequeue() {

            if (this.MessageCount > 0)
            {
                return this.mq.Dequeue();
            }
            else {
                throw new _MessageQueueException($"Message queue is empty");
            }
        }

        public void Truncate() {

            this.mq = new Queue<string>();
        }

        public string this[int index] {

            get { return this.mq.ElementAt(index); }
        }

        /* PROPERTIES */

        public int MessageCount {

            get { return this.mq.Count; }
        }

        public bool IsEmpty {

            get { return (this.MessageCount == 0 ? true : false); }
        }
    }


}
