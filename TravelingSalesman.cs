using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
	    ///Aaron Reyes
            WriteToFile(10);
            int[][] distanceMatrix = ReadFromFile(10);
            int[] startArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            //List<int> sumList = new List<int>();
            Dictionary<int, string> sumList = new Dictionary<int, string>();

            printMatrix(distanceMatrix, 10);
            Console.WriteLine("\n");

            Stopwatch executionTime = new Stopwatch();
            executionTime.Start();
            GeneratePermutations(startArray, 10, distanceMatrix, sumList, 10);
            executionTime.Stop();
            long totalExTime = executionTime.ElapsedTicks;

            sumList = sumList;
            var items = from KeyValuePair in sumList orderby KeyValuePair.Key ascending select KeyValuePair;
            string answer = items.ToArray()[0].ToString();

            WriteToFile(answer);
            Console.WriteLine();
            Console.ReadKey();
        }

        #region Write and Read At Start
        
        static int[][] ReadFromFile(int n)
        {

            int[][] adjecencyMatrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                adjecencyMatrix[i] = new int[n];
            }
            int counter = 0;
            using (StreamReader sr = new StreamReader("Graphs.txt"))
            {
                sr.ReadLine();

                for (int totalNumberOfCites = 0; totalNumberOfCites < n; totalNumberOfCites++)
                {
                    for (int totalNumberOfOthers = 0; totalNumberOfOthers < n; totalNumberOfOthers++)
                    {
                        int temp = sr.Read();
                        temp -= 48;
                        adjecencyMatrix[totalNumberOfCites][totalNumberOfOthers] = temp;
                        counter++;
                        if (counter % n == 0)
                        {
                            sr.Read();
                        }
                    }
                }
            }

            return adjecencyMatrix;
        }

        public static int[][] generateGraph(int n)
        {
            int[][] adjecencyMatrix = new int[n][];
            Random distanceRand = new Random();
            for (int i = 0; i < n; i++)
            {
                adjecencyMatrix[i] = new int[n];
            }

            for (int totalNumberOfCites = 0; totalNumberOfCites < n; totalNumberOfCites++)
            {
                for (int totalNumberOfOthers = 0; totalNumberOfOthers < n; totalNumberOfOthers++)
                {
                    if (totalNumberOfCites == totalNumberOfOthers)
                    {
                            adjecencyMatrix[totalNumberOfCites][totalNumberOfOthers] = 0;
                    }
                    else
                    {
                        adjecencyMatrix[totalNumberOfCites][totalNumberOfOthers] = distanceRand.Next(1, 10);
                    }
                }
            }

            for (int totalNumberOfCites = 0; totalNumberOfCites < n; totalNumberOfCites++)
            {
                for (int totalNumberOfOthers = 0; totalNumberOfOthers < n; totalNumberOfOthers++)
                {
                    int combineInt = adjecencyMatrix[totalNumberOfCites][totalNumberOfOthers];
                    adjecencyMatrix[totalNumberOfOthers][totalNumberOfCites] = combineInt;
                }
            }

            return adjecencyMatrix;
        }

        #endregion Write and Read At Start


        public static void GeneratePermutations(int[] cityNumber, int n, int[][] distanceMatrix, Dictionary<int, string> totalSums, int order)
        {
                if (n == 1)
                {
                    //PrintArray(cityNumber);
                    //ArrayToString(cityNumber);
                    SumTour(cityNumber, distanceMatrix, order, totalSums);
                    //SumTour(cityNumber, distanceMatrix, order, totalSums, false);
                }

                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        GeneratePermutations(cityNumber, n - 1, distanceMatrix, totalSums, order);

                        int j = 0;
                        if (n % 2 == 0)
                        {
                            j = i;
                        }
                        else
                        {
                            j = 0;
                        }

                        int t = cityNumber[n - 1];
                        cityNumber[n - 1] = cityNumber[j];
                        cityNumber[j] = t;
                    }
                }
        }

        public static void SumTour(int[] cityList, int[][] distanceList, int n, Dictionary<int, string> sumList)
        {
            int currentSum = 0;

            for (int cityIndex = 0; cityIndex < n; cityIndex++)
            {
                if (cityIndex != n - 1)
                {
                    currentSum += distanceList[cityList[cityIndex]][cityList[cityIndex + 1]];
                }
                else
                {
                    currentSum += distanceList[cityList[cityIndex]][cityList[0]];
                }
            }

            //Console.WriteLine("Sum: " + currentSum);
            //sumList.Add(currentSum);
            if (!sumList.ContainsKey(currentSum))
            {
                sumList.Add(currentSum, ArrayToString(cityList, false));
            }

        }

        public static int SumTour(int[] cityList, int[][] distanceList, int n, List<int> sumList, bool bo)
        {
            int currentSum = 0;

            for (int cityIndex = 0; cityIndex < n; cityIndex++)
            {
                if (cityIndex != n - 1)
                {
                    currentSum += distanceList[cityList[cityIndex]][cityList[cityIndex + 1]];
                }
                else
                {
                    currentSum += distanceList[cityList[cityIndex]][cityList[0]];
                }
            }

            Console.WriteLine("Sum: " + currentSum);

            return currentSum;
        }

        public static void PrintArray(int[] arrayToPrint)
        {
            foreach (int element in arrayToPrint)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        public static string ArrayToString(int[] arrayToConvert)
        {
            string fullString = "";
            foreach (int element in arrayToConvert)
            {
                fullString += element.ToString();
            }
            Console.WriteLine(fullString);
            return fullString;
        }
        public static string ArrayToString(int[] arrayToConvert, bool Condition)
        {
            string fullString = "";
            foreach (int element in arrayToConvert)
            {
                fullString += element.ToString();
            }
            //Console.WriteLine(fullString);
            return fullString;
        }

        public static void printMatrix(int[][] matrix,int n)
        {
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (counter % n == 0)
                    {
                        Console.Write("\n");
                    }
                    Console.Write(matrix[i][j] + " ");
                    counter++;
                }
            }
        }

        public static void WriteToFile(int n)
        {
            
            int[][] distanceMatrix = generateGraph(n);
            string filename = "Graphs.txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (StreamWriter sr = new StreamWriter("Graphs.txt"))
            {
                int counter = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (counter % n == 0)
                        {
                            sr.Write("\n");
                        }
                        sr.Write(distanceMatrix[i][j].ToString());
                        counter++;
                    }
                }
            }
        }

        public static void WriteToFile(string ANSWER)
        {
            using (StreamWriter sr = new StreamWriter("Graphs.txt", true))
            {
                sr.WriteLine();
                sr.WriteLine();

                sr.WriteLine(ANSWER);
            }
        }
    }
}
