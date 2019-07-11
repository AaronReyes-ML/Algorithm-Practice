using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DynamicTimeWarping
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double[]> stockList = ReadFile();
            string[] company = { "Apple", "Micro", "Exxon", "ATT", "Verizon", "Johnson", "Chase", "Facebook", "Ge", "Gm" };
            List<Tuple<string, double>> comparisonTuples = new List<Tuple<string, double>>();
            List<string> comparisonsDone = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!comparisonsDone.Contains(company[j] + company[i]))
                    {
                        Tuple<string, double> tempTuple = new Tuple<string, double>((company[i] + " vs " + company[j]),
                        (DynamicTimeWarpingD(stockList[i], stockList[j])));
                        comparisonTuples.Add(tempTuple);
                        comparisonsDone.Add(company[i] + company[j]);
                    }
                }
            }

            /*
            foreach (Tuple<string, double> tuple in comparisonTuples)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2);
            }
            */

            comparisonTuples = (from tuple in comparisonTuples orderby tuple.Item2 select tuple).ToList();

            Console.WriteLine("Companies rated from most similar to least similar: ");
            foreach (Tuple<string, double> tuple in comparisonTuples)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2);
            }

            Console.ReadKey();
        }

        #region int

        static int DynamicTimeWarping(int[] arrayS, int[] arrayT)
        {
            int n = arrayS.Count();
            int m = arrayT.Count();

            int[,] warp = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                warp[i, 0] = int.MaxValue;
            }
            for (int i = 0; i < m; i++)
            {
                warp[0, 1] = int.MaxValue;
            }

            warp[0, 0] = 0;

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    int cost = distance(arrayS[i], arrayT[j]);
                    int value1 = warp[i - 1, j];
                    int value2 = warp[i, j - 1];
                    int value3 = warp[i - 1, j - 1];
                    int min = minimum(value1, value2, value3);

                    warp[i, j] = cost + min;
                }
            }

            return warp[n - 1, m - 1];
        }

        static int distance(int x, int y)
        {
            return Math.Abs(x - y);
        }

        static int minimum(int x, int y, int z)
        {
            if ((x == y) && (y == z))
            {
                return x;
            }

            else if ((x <= y) && (x <= z))
            {
                return x;
            }
            else if ((y <= x) && (y <= z))
            {
                return y;
            }
            else
            {
                return z;
            }
        }

        #endregion int

        #region double

        static double DynamicTimeWarpingD(double[] arrayS, double[] arrayT)
        {
            int n = arrayS.Count();
            int m = arrayT.Count();

            double[,] warp = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                warp[i, 0] = double.MaxValue;
            }
            for (int i = 0; i < m; i++)
            {
                warp[0, 1] = double.MaxValue;
            }

            warp[0, 0] = 0;

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    double cost = distance(arrayS[i], arrayT[j]);
                    double value1 = warp[i - 1, j];
                    double value2 = warp[i, j - 1];
                    double value3 = warp[i - 1, j - 1];
                    double min = minimum(value1, value2, value3);

                    warp[i, j] = cost + min;
                }
            }

            return warp[n - 1, m - 1];
        }

        static double distance(double x, double y)
        {
            return Math.Abs(x - y);
        }

        static double minimum(double x, double y, double z)
        {
            if ((x == y) && (y == z))
            {
                return x;
            }

            else if ((x <= y) && (x <= z))
            {
                return x;
            }
            else if ((y <= x) && (y <= z))
            {
                return y;
            }
            else
            {
                return z;
            }
        }

        #endregion double


        static List<double[]> ReadFile()
        {
            List<double[]> stocks = new List<double[]>();
            using (StreamReader sr = new StreamReader("StockData.txt"))
            {
                for (int i = 0; i < 10; i++)
                {
                    double[] tempArray = new double[251];
                    sr.ReadLine();
                    for (int j = 0; j < 251; j++)
                    {
                        tempArray[j] = double.Parse(sr.ReadLine());
                    }
                    stocks.Add(tempArray);
                }
            }

            return stocks;
        }
    }
}
