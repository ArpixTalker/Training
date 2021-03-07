using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    public class WaitersDistributor
    {
        private static readonly List<string> waiters = new List<string>();
        private static WaitersDistributor wd = new WaitersDistributor(); //Private static constructor shared for every variable it is saved in
        private int index = 0;

        private WaitersDistributor() { 

            waiters.Add("Mosh");
            waiters.Add("Leslie");
            waiters.Add("Rick");
            waiters.Add("Jimmy");
            waiters.Add("Ginny");
        }

        public static WaitersDistributor GetInstane() { //Returns Instance of the class

            return wd;
        }

        public string GetAvailableWaiterRotating() {

            var waiter = waiters[index];
            if (index < waiters.Count- 1)
            {
                index ++;
            }
            else {
                index = 0;
            }

            return waiter;
        }


        /*
         * MAIN
            var hostJohn = WaitersDistributor.GetInstane();
            var hostChloe = WaitersDistributor.GetInstane();

            for (int i = 0; i < 10; i++) {

                Console.WriteLine(hostJohn.GetAvailableWaiterRotating());
                Console.WriteLine(hostChloe.GetAvailableWaiterRotating());
            }
            Console.ReadLine();
       */
    }
}