using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    public class Q_Manager
    {
        private Dictionary<string, MessageQueue> queues;
        public string Name { get; set; }

        public Q_Manager(string name)
        {

            this.queues = new Dictionary<string, MessageQueue>();
        }

        public void CreateQueue(string name, int limit, Encoding enc)
        {

            if (!this.queues.ContainsKey(name))
            {
                this.queues.Add(name, new MessageQueue(name, limit, enc));
            }
            else
            {

                throw new _QueueNameNotUniqueException($"Manager {this.Name} already contains queue with name: {name}");
            }
        }

        public void CreateQueue(string name, int limit)
        {

            if (!this.queues.ContainsKey(name))
            {

                this.queues.Add(name, new MessageQueue(name, limit));
            }
            else
            {

                throw new _QueueNameNotUniqueException($"Manager {this.Name} already contains queue with name: {name}");
            }
        }

        public void DeleteQueue(string queueName)
        {

            if (this.queues.ContainsKey(queueName))
            {

            }
            else
            {

                throw new _MessageQueueDoesNotExistException($"Message Queue {queueName} does not exist in manager {this.Name}");
            }
        }
    }
}
