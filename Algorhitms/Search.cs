using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorhitms
{
    public static class Search
    {

        public static List<int> BinarySearch(List<int> target, int value) {

            var output = new List<int>();
            int currentIndex = target.Count / 2;
            int increment = currentIndex;

            while (increment >= 1) {

                
                increment /= 2;
                //Console.WriteLine($"Current index {currentIndex},Increment:{increment}");
                if (target[currentIndex] > value) {

                    currentIndex -= increment;

                } else if (target[currentIndex] < value) {

                    currentIndex += increment;

                } else {

                    output.Add(currentIndex);
                    int rootindex = currentIndex;

                    while (target[currentIndex+1] == value) {

                        output.Add(currentIndex + 1);
                        currentIndex++;
                    }
                    currentIndex = rootindex;

                    while (target[currentIndex - 1] == value) {

                        output.Add(currentIndex -1);
                        currentIndex--;
                    }
                    output.Sort();
                    return output;
                }
            }
            return new List<int>();
        }
    }
}
