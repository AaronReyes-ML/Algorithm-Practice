using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FindInversions
{
    class Program
    {
        static void Main(string[] args)
        {

            int numberOfExecutions = 8;
            int size = numberOfExecutions;
            List<InversionList> listOfListsToSort = new List<InversionList>();

            for (int i = 0; i < numberOfExecutions; i++)
            {
                int[] temp = generateRandomArray(size);
                List<int> tempList = new List<int>();
                InversionList tempInvList = new InversionList();
                tempInvList.list = tempList;
                foreach (int item in temp)
                {
                    tempInvList.list.Add(item);
                }
                listOfListsToSort.Add(tempInvList);
                size = size * 2;
            }

            List<long> times = new List<long>();
            List<int> sizes = new List<int>();
            List<List<int>> listsUnSorted = new List<List<int>>();
            List<List<int>> listsSorted = new List<List<int>>();
            List<int> inversionsList = new List<int>();

            foreach (InversionList invList in listOfListsToSort)
            {
                Stopwatch inversionCountWatch = new Stopwatch();
                InversionList tempInvList = new InversionList();
                listsUnSorted.Add(invList.list);

                inversionCountWatch.Start();
                tempInvList = mergeSort(invList);
                inversionCountWatch.Stop();

                sizes.Add(tempInvList.list.Count);
                times.Add(inversionCountWatch.ElapsedTicks);
                listsSorted.Add(tempInvList.list);
                inversionsList.Add(tempInvList.inversions);
            }

            Console.WriteLine("Time taken: ");
            print(times);
            Console.WriteLine("Size of list: ");
            print(sizes);
            Console.WriteLine("Inversions found: ");
            print(inversionsList);

            /*
            int[] tempArray = { 1, 3, 4, 2, 5, 6, 7, 8};
            List<int> listToSort = new List<int>();
            InversionList InvList = new InversionList();

            InvList.list = listToSort;

            foreach (int item in tempArray)
            {
                InvList.list.Add(item);
            }

            print(InvList.list);
            InvList = mergeSort(InvList);
            print(InvList.list);
            Console.WriteLine(InvList.inversions);
            */

            Console.ReadKey();
        }

        static InversionList mergeSort(InversionList listToSort)
        {
            InversionList returnList = new InversionList();
            if (listToSort.list.Count == 1)
            {
                return listToSort;
            }
            int n = listToSort.list.Count / 2;
            InversionList part1 = new InversionList();
            InversionList part2 = new InversionList();

            part1.list = new List<int>(n);
            part2.list = new List<int>(n);

            for (int i = 0; i < n; i++)
            {
                part1.list.Add(listToSort.list[i]);
                part2.list.Add(listToSort.list[i + n]);
            }

            part1 = mergeSort(part1);
            part2 = mergeSort(part2);

            returnList = merge(part1, part2);
            returnList.inversions += part1.inversions + part2.inversions;

            return returnList;
        }

        static InversionList merge(InversionList partA, InversionList partB)
        {
            InversionList partC = new InversionList();
            partC.list = new List<int>();

            while (partA.list.Count != 0 && partB.list.Count != 0)
            {
                if (partA.list[0] > partB.list[0])
                {
                    partC.list.Add(partB.list[0]);
                    partB.list.RemoveAt(0);
                    partC.inversions += partA.list.Count;
                }
                else
                {
                    partC.list.Add(partA.list[0]);
                    partA.list.RemoveAt(0);
                }
            }

            while (partA.list.Count != 0)
            {
                partC.list.Add(partA.list[0]);
                partA.list.RemoveAt(0);
            }

            while (partB.list.Count != 0)
            {
                partC.list.Add(partB.list[0]);
                partB.list.RemoveAt(0);
            }

            return partC;
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

    public class InversionList
    {
        public List<int> list { get; set; }
        public int inversions { get; set; }
    }
}