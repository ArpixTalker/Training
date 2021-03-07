using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using DesignPatterns;
using Algorhitms;
using Extensions;
using Components;
using QueueManager;

namespace _Training
{   
    class Program
    {
        
        static void Main(string[] args)
        {

            var list = Generator.GenerateRandomNumbers(100,17);
            foreach (int a in list)
            {
                Console.WriteLine(a);
            }
            list.CombSort();
            Console.WriteLine("SORT");
            foreach (int a in list)
            {
                Console.WriteLine(a);
            }


            Console.ReadLine();
            return;
            /*
            var qm = new Q_Manager("manager");

            qm.CreateQueue("testQ",100);

            

            var queue = new MessageQueue("TEST.QUEUE", 100 ,Encoding.UTF8);

            queue.Enqueue("message A");
            queue.Enqueue("message B");
            queue.Enqueue("message C");
            queue.Enqueue("message D");

            while (!queue.IsEmpty) {

                queue.Dequeue();
            }
            Console.ReadLine();

/*
            int option = 4;
            Action<int, int> callback;

            switch (option) {

                case 0:
                    callback = (x, y) =>  Console.WriteLine(x + y);
                    break;
                case 1:
                    callback = (x, y) => Console.WriteLine(x - y);
                    break;
                case 2:
                    callback = (x, y) => Console.WriteLine(x * y);
                    break;
                default:
                    callback = (x, y) => Console.WriteLine("UNDEFINED OPTION");
                    break;
                    
            }

            DoSomeWorkWithInteger(option, callback);
            /*
            var t = new ThreadStart(() => {

                while (true)
                {
                    var command = Console.ReadLine();
                    if (command == "clear")
                    {
                        Console.Clear();
                    }
                }
            });

            var thr = new Thread(t);
            thr.Start();
            var server = new ApiServer(13000,ApiServer.GetLocalhostIP());

            server.StartServer();
            /*
            var auth = new Authenticator();

            auth.AuthenticateUser("testUser", Encryptor.HashString("testPassword", "SHA256"), out string message, out int state);
            Console.WriteLine($"Connection Result: STATE:{state} MESSAGE:{message}");
            Console.ReadLine();*/
        }

        static void DoSomeWorkWithInteger(int option, Action<int,int> callback) {

            Console.WriteLine($"Option: {option}");
            int a = 10;
            int b = 12;
            callback(a,b);
            Console.ReadLine();
        }
    }
}
