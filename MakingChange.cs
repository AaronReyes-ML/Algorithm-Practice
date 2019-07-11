using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakingChange
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] demonArray = { 1, 3, 9, 19, 26};
            int n = 100;
            int[] ansArray = new int[n];

            Console.WriteLine("Set of values");
            printArray(demonArray);
            Console.WriteLine("Value to achieve: " + (n - 1));
            ansArray = ChangeMaking(demonArray, n);

            Console.ReadKey();

        }

        static int[] ChangeMaking(int[] denomArray, int n)
        {
            int[] nArray = new int[n];
            int[] fArray = new int[n];
            List<List<int>> listOfLists = new List<List<int>>();
            int m = denomArray.Count();
            nArray = initArray(nArray, n);

            for (int i = 1; i < n; i++)
            {
                int temp = int.MaxValue;
                int j = 0;

                List<int> mins = new List<int>();
                while ((j < m) && (i >= denomArray[j]))
                {
                    if (returnMin(fArray[i - denomArray[j]], temp))
                    {
                        int index = i - denomArray[j];
                        temp = fArray[index];
                        mins.Add(denomArray[j]);
                    }
                    j++;
                }
                fArray[i] = temp + 1;
                listOfLists.Add(mins);
                //printList(mins);
                //printArray(nArray);
                //printArray(fArray);
                //Console.WriteLine("---");
            }

            int numberOfCoins = fArray[fArray.Count() - 1];
            List<int> ans = new List<int>(numberOfCoins);
            ans.Add(listOfLists.Last().Last());
            int listIndex = n - 2;

            for (int listCounter = 1; listCounter < numberOfCoins; listCounter++)
            {
                int temp = ans.Last();
                listIndex = listIndex - temp;
                ans.Add((listOfLists.ElementAt(listIndex).Last()));
            }

            //printArray(fArray);
            //printListsOfLists(listOfLists);
            Console.WriteLine("Answer: " + numberOfCoins + " coins, with values : ");
            printList(ans);
            return fArray;
        }

        static bool returnMin(int oper1, int oper2)
        {
            if (oper1 < oper2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int returnMinNum(int oper1, int oper2)
        {
            if (oper1 < oper2)
            {
                return oper1;
            }
            else
            {
                return oper2;
            }
        }

        static int[] initArray(int[] arrayToinitialize, int n)
        {
            for (int i = 0; i < n; i++)
            {
                arrayToinitialize[i] = i;
            }

            return arrayToinitialize;
        }

        static void printArray(int[] arrayToPrint)
        {
            Console.Write("{ ");
            for (int i = 0; i < arrayToPrint.Count(); i++)
            {
                Console.Write(arrayToPrint[i] + " ");
            }
            Console.Write("}");
            Console.WriteLine();
        }

        static void printList(List<int> listToPrint)
        {
            Console.Write("{ ");
            for (int i = 0; i < listToPrint.Count; i++)
            {
                Console.Write(listToPrint[i] + " ");
            }
            Console.Write("}");
            Console.WriteLine();
        }

        static void printListsOfLists(List<List<int>> listToPrint)
        {
            Console.Write("{ ");
            foreach (List<int> list in listToPrint)
            {
                Console.Write("{ ");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write(list[i] + " ");
                }
                Console.Write("}");
            }
            Console.Write("}");
            Console.WriteLine();
        }
    }
}
