using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Algorhitms
{
    public static class Sort
    {
        public static void DumbSort(this List<int> target) {

            for (int i = 0; i < target.Count; i++) {

                for (int j = 0; j < target.Count; j++) {

                    if (target[i] > target[j]) {

                        int temp = target[i];
                        target[i] = target[j];
                        target[j] = temp;
                    }
                }
            }
        }


        public static void BubbleSort(this List<int> target)
        {
            for (int i = 0; i < target.Count - 1; i++)
            {
                for (int j = 0; j < target.Count - i - 1; j++)
                {
                    if (target[j + 1] < target[j]) {

                        int temp = target[j+1];
                        target[j+1] = target[j];
                        target[j] = temp;
                    }
                }
            }
        }

        public static void CombSort(this List<int> target) {

            int range = (target.Count -1) / 2;
            while (range > 0) {
                Console.WriteLine(range);
                for (int i = range; i < target.Count; i++) {

                    if (target[i] < target[i - range]) {

                        int temp = target[i];
                        target[i] = target[i - range];
                        target[i - range] = temp;
                    }
                }
                range--;
            }
        }
    }
}
