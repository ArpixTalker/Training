using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorhitms
{
    public static class Generator
    {
        public static readonly Random rand = new Random();

        public static List<int> GenerateRandomNumbers(int max, int count)
        {
            var output = new List<int>();
            for (int i = 0; i < count; i++)
            {

                output.Add(rand.Next(0, max +1));
            }
            return output;
        }
    }
}
