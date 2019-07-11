using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KthStatistic
{
    class Program
    {
        static void Main(string[] args)
        {
            Random kRandom = new Random();
            int numberOfExecutions = 8;
            int size = numberOfExecutions;
            List<List<int>> listOfListsToSort = new List<List<int>>();

            for (int i = 0; i < numberOfExecutions; i++)
            {
                int[] temp = generateRandomArray(size);
                List<int> tempList = new List<int>();
                foreach (int item in temp)
                {
                    tempList.Add(item);
                }
                listOfListsToSort.Add(tempList);
                size = size * 2;
            }

            List<long> times = new List<long>();
            List<int> sizes = new List<int>();
            List<int> ks = new List<int>();
            List<int> values = new List<int>();
            foreach (List<int> list in listOfListsToSort)
            {
                Stopwatch kthStatisticWatch = new Stopwatch();
                kthStatisticWatch.Start();
                List<int> tempList = mergeSort(list);
                int tempSize = tempList.Count;
                int k = kRandom.Next(0, tempSize);
                ks.Add(k);
                sizes.Add(tempSize);
                values.Add(findKthElement(k, tempList));
                kthStatisticWatch.Stop();

                times.Add(kthStatisticWatch.ElapsedTicks);
            }

            Console.WriteLine("Time taken: ");
            print(times);
            Console.WriteLine("Size of List: ");
            print(sizes);
            Console.WriteLine("Kth element : ");
            print(ks);
            Console.WriteLine("Corresponding Value: ");
            print(values);
            Console.ReadKey();

        }

        static List<int> mergeSort(List<int> listToSort)
        {
            if (listToSort.Count == 1)
            {
                return listToSort;
            }
            int n = listToSort.Count / 2;
            List<int> part1 = new List<int>(n);
            List<int> part2 = new List<int>(n);

            for (int i = 0; i < n; i++)
            {
                part1.Add(listToSort[i]);
                part2.Add(listToSort[i + n]);
            }

            part1 = mergeSort(part1);
            part2 = mergeSort(part2);

            return merge(part1, part2);
        }

        static List<int> merge(List<int> partA, List<int> partB)
        {
            List<int> partC = new List<int>();

            while (partA.Count != 0 && partB.Count != 0)
            {
                if (partA[0] > partB[0])
                {
                    partC.Add(partB[0]);
                    partB.RemoveAt(0);
                }
                else
                {
                    partC.Add(partA[0]);
                    partA.RemoveAt(0);
                }
            }

            while (partA.Count != 0)
            {
                partC.Add(partA[0]);
                partA.RemoveAt(0);
            }

            while (partB.Count != 0)
            {
                partC.Add(partB[0]);
                partB.RemoveAt(0);
            }

            return partC;
        }

        static int findKthElement(int k, List<int> list)
        {
            return list[k];
        }

        static int[] generateRandomArray(int n)
        {
            int[] randomArray = new int[n];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                int numberToAdd = rand.Next(1, 10000);
                while (randomArray.Contains(numberToAdd))
                {
                    numberToAdd = rand.Next(1, 10000);
                }
                randomArray[i] = numberToAdd;
            }


            return randomArray;
        }

        static void print(int[] arrayToPrint)
        {
            Console.Write("{ ");
            foreach (int number in arrayToPrint)
            {
                Console.Write(number + " ");
            }
            Console.Write("}");
            Console.WriteLine();
        }

        static void print(List<int> listToPrint)
        {
            Console.Write("{ ");
            foreach (int number in listToPrint)
            {
                Console.Write(number + " ");
            }
            Console.Write("}");
            Console.WriteLine();
        }

        static void print(List<long> listToPrint)
        {
            Console.Write("{ ");
            foreach (int number in listToPrint)
            {
                Console.Write(number + " ");
            }
            Console.Write("}");
            Console.WriteLine();
        }
    }
}
