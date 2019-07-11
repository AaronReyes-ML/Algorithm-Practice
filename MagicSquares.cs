using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MagicSquares
{
    class Program
    {
        static void Main(string[] args)
        {
	    ///Aaron Reyes
            Stopwatch MagicStopwatch = new Stopwatch();

            MagicStopwatch.Start();
            GenMagicSquares(3);
            MagicStopwatch.Stop();

            Console.WriteLine(MagicStopwatch.Elapsed.Milliseconds);
            /**
            int[] testMatrix = new int[] { 2, 7, 6, 9, 5, 1, 4, 3, 8};
            int[] testMatrix2 = new int[] { 4, 9, 2, 3, 5, 7, 8, 1, 6 };
            int[] testMatrix3 = new int[] { 7, 12, 1, 14, 2, 13, 8, 11, 16, 3, 10, 5, 9, 6, 15, 4 };
            int[] testMatrix4 = new int[] { 12, 1, 14, 7, 13, 8, 11, 2, 3, 10, 5, 16, 6, 15, 4, 9 };
            checkForMagicStatus(testMatrix4, 4);
            **/
            Console.ReadKey();
        }

        public static void GenMagicSquares(int n)
        {
            int[] matrixToPass = new int[n * n];
            
            for (int i = 0; i < n* n; i++)
            {
                matrixToPass[i] = i + 1;
            }

            GeneratePermutations(matrixToPass, n * n, n);
        }

        public static void GeneratePermutations(int[] fauxMatrix, int n, int order)
        {
            if (n == 1)
            {
                //PrintMatrix(fauxMatrix, order);
                //Console.WriteLine();
                if (checkForMagicStatus(fauxMatrix, order))
                {
                    PrintMatrix(fauxMatrix, order);
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    GeneratePermutations(fauxMatrix, n-1, order);

                    int s = 0;
                    if (n % 2 == 0)
                    {
                        s = i;
                    }
                    else
                    {
                        s = 0;
                    }

                    int temp = fauxMatrix[n - 1];
                    fauxMatrix[n - 1] = fauxMatrix[s];
                    fauxMatrix[s] = temp;
                }
            }
        }

        public static void PrintArray(int[] arrayToPrint)
        {
            foreach (int element in arrayToPrint)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        public static void PrintMatrix(int[] fauxMatrix, int n)
        {
            int counter = 0;
            foreach (int element in fauxMatrix)
            {
                if (counter % n == 0)
                {
                    Console.Write("\n");
                }
                Console.Write(element + " ");
                counter++;
            }
        }

        public static bool checkForMagicStatus(int[] fauxMatrix, int n)
        {
            bool magicStatus = true;

            //check first main diagonal
            int mainDiag1Sum = 0;
            for (int mainDiag1 = 0; mainDiag1 <= ((n* n) - 1); mainDiag1 += (n + 1))
            {
                mainDiag1Sum += fauxMatrix[mainDiag1];
            }

            //check other main diagonal
            int mainDiag2Sum = 0;
            for (int mainDiag2 = (n- 1); mainDiag2 <= ((n*n) - n); mainDiag2 += (n - 1))
            {
                mainDiag2Sum += fauxMatrix[mainDiag2];
            }

            //check all columns
            List<int> columnSums = new List<int>();
            for (int column = 0; column < n; column += 1)
            {
                int currentColumnSum = 0;
                for (int row = 0; row < ((n* n) - column); row += n)
                {
                    currentColumnSum += fauxMatrix[column + row];
                }
                columnSums.Add(currentColumnSum);
            }

            //check all rows

            List<int> rowSums = new List<int>(0);
            for (int row = 0; row <= ((n*n) - n); row += n)
            {
                int currentRowSum = 0;
                for (int column = 0; column < n; column += 1)
                {
                    currentRowSum += fauxMatrix[row + column];
                }
                rowSums.Add(currentRowSum);
            }

            List<int> allSums = new List<int>();

            allSums.Add(mainDiag1Sum);
            allSums.Add(mainDiag2Sum);
            foreach (int sum in columnSums)
            {
                allSums.Add(sum);
            }
            foreach (int sum in rowSums)
            {
                allSums.Add(sum);
            }

            int sumToCompare = allSums[0];
            for (int sumIndex = 0; sumIndex < allSums.Count; sumIndex++)
            {
                if (allSums[sumIndex] != sumToCompare)
                {
                    magicStatus = false;
                }
            }
            //Console.WriteLine(magicStatus);

            /**
            Console.WriteLine("Main Diagonal: " + mainDiag1Sum);
            Console.WriteLine("Main Diagonal2: " + mainDiag2Sum);
            Console.Write("Column Sums: ");
            foreach (int sum in columnSums)
            {
                Console.Write(sum + " ");
            }
            Console.Write("\n");
            Console.Write("Row Sums: ");
            foreach (int sum in rowSums)
            {
                Console.Write(sum + " ");
            }
            Console.Write("\n");
            Console.Write("All sums: ");
            foreach (int sum in allSums)
            {
                Console.Write(sum + " ");
            }
            Console.Write("\n");
            **/
            return magicStatus;
        }
    }
}
