using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConvexHull
{
    class Program
    {
        static void Main(string[] args)
        {

            int numberOfExecutions = 8;
            int size = numberOfExecutions;
            List<LinkedList<Tuple<int, int>>> listOfLinkedListsToHull = new List<LinkedList<Tuple<int, int>>>();

            for (int i = 0; i < numberOfExecutions; i++)
            {
                int[] tempX = generateRandomArray(size);
                int[] tempY = generateRandomArray(size);
                LinkedList<Tuple<int, int>> tempLinkedList = new LinkedList<Tuple<int, int>>();

                for (int j = 0; j < tempX.Count(); j++)
                {
                    Tuple<int, int> tempTuple = new Tuple<int, int>(tempX[j], tempY[j]);
                    tempLinkedList.AddLast(tempTuple);
                }
                listOfLinkedListsToHull.Add(tempLinkedList);
                size = size * 2;
            }

            List<long> times = new List<long>();
            List<int> sizes = new List<int>();

            foreach (LinkedList<Tuple<int, int>> linkedList in listOfLinkedListsToHull)
            {
                Stopwatch convexHullWatch = new Stopwatch();
                LinkedList<Tuple<int, int>> tempHull = new LinkedList<Tuple<int, int>>();

                convexHullWatch.Start();
                tempHull = sortByX(linkedList);
                sizes.Add(tempHull.Count);
                tempHull = convexHull(tempHull);
                convexHullWatch.Stop();

                times.Add(convexHullWatch.ElapsedTicks);
            }

            Console.WriteLine("Time taken: ");
            print(times);
            Console.WriteLine("Size of list: ");
            print(sizes);

            
            int[] xArray = { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] yArray = { 5, 4, 1, 8, 0, 10, 3, 14 };

            List<Tuple<int, int>> pointList = new List<Tuple<int, int>>();
            LinkedList<Tuple<int, int>> hull = new LinkedList<Tuple<int, int>>();

            for (int i = 0; i < xArray.Count(); i++)
            {
                Tuple<int, int> tempTuple = new Tuple<int, int>(xArray[i], yArray[i]);
                pointList.Add(tempTuple);
            }
            pointList = sortByX(pointList);

            foreach (Tuple<int, int> point in pointList)
            {
                LinkedListNode<Tuple<int, int>> tempNode = new LinkedListNode<Tuple<int, int>>(point);
                hull.AddLast(tempNode);
            }
            Console.WriteLine("Original");
            PrintLinkedList(hull);

            hull = convexHull(hull);
            Console.WriteLine("Convex Hull");
            PrintLinkedList(hull);
            

            Console.ReadKey();
        }

        static LinkedList<Tuple<int, int>> convexHull(LinkedList<Tuple<int, int>> setOfPoints)
        {
            if (setOfPoints.Count <= 3)
            {
                return setOfPoints;
            }

            int n = setOfPoints.Count / 2;

            LinkedList<Tuple<int, int>> partA = new LinkedList<Tuple<int, int>>();
            LinkedList<Tuple<int, int>> partB = new LinkedList<Tuple<int, int>>();

            for (int i = 0; i < n; i++)
            {
                LinkedListNode<Tuple<int, int>> tempNode1 = new LinkedListNode<Tuple<int, int>>(setOfPoints.ElementAt(i));
                LinkedListNode<Tuple<int, int>> tempNode2 = new LinkedListNode<Tuple<int, int>>(setOfPoints.ElementAt(i+n));

                partA.AddLast(tempNode1);
                partB.AddLast(tempNode2);
            }

            partA = convexHull(partA);
            partB = convexHull(partB);

            return merge(partA, partB);
        }

        static LinkedList<Tuple<int, int>> merge(LinkedList<Tuple<int, int>> partA, LinkedList<Tuple<int, int>> partB)
        {
            LinkedList<Tuple<int, int>> hull = new LinkedList<Tuple<int, int>>();
            //Tuple<int, int> point1 = partA.Last.Value;
            //Tuple<int, int> point2 = partB.First.Value;
            Tuple<int, int> point1 = findMaxX(partA);
            Tuple<int, int> point2 = findMinX(partB);

            double x1 = point1.Item1;
            double x2 = point2.Item1;

            double xCoordinate = ((x2 - x1) / 2) + x1;
            //double xCoordinate = ((distance(point1, point2)) / 2) + (double)point1.Item1;

            LinkedList<Tuple<int, int>> upperTangent = findUpperTangent(partA, partB, xCoordinate);
            LinkedList<Tuple<int, int>> lowerTangent = findLowerTangent(partA, partB, xCoordinate);

            LinkedListNode<Tuple<int, int>> ai = new LinkedListNode<Tuple<int, int>>(upperTangent.ElementAt(0));
            LinkedListNode<Tuple<int, int>> bj = new LinkedListNode<Tuple<int, int>>(upperTangent.ElementAt(1));

            LinkedListNode<Tuple<int, int>> ak = new LinkedListNode<Tuple<int, int>>(lowerTangent.ElementAt(0));
            LinkedListNode<Tuple<int, int>> bm = new LinkedListNode<Tuple<int, int>>(lowerTangent.ElementAt(1));

            hull.AddLast(ai);
            hull.AddLast(bj);

            int k = 0;
            while (partB.ElementAt(k) != bj.Value)
            {
                k++;
            }
            int j = 0;
            while (partA.ElementAt(j) != ak.Value)
            {
                j++;
            }

            while (partB.ElementAt(k) != bm.Value && (k+1 < partB.Count))
            {
                if (!hull.Contains(partB.ElementAt(k)))
                {
                    LinkedListNode<Tuple<int, int>> bListTemp = new LinkedListNode<Tuple<int, int>>(partB.ElementAt(k));
                    hull.AddLast(bListTemp);
                    k++;
                }
                else
                {
                    k++;
                }
            }

            hull.AddLast(bm.Value);
            hull.AddLast(ak.Value);

            while (partA.ElementAt(j) != ai.Value && (j+1 < partA.Count))
            {
                if (!hull.Contains(partA.ElementAt(j)))
                {
                    LinkedListNode<Tuple<int, int>> aListTemp = new LinkedListNode<Tuple<int, int>>(partA.ElementAt(j));
                    hull.AddLast(aListTemp);
                    j++;
                }
                else
                {
                    j++;
                }
            }

            //Console.WriteLine("Intermediate Hull");
            //PrintLinkedList(hull);

            return hull;
        }

        static LinkedList<Tuple<int, int>> findUpperTangent(LinkedList<Tuple<int, int>> partA, LinkedList<Tuple<int, int>> partB, double xCoordinate)
        {
            double max = Double.MinValue;
            LinkedList<Tuple<int, int>> upperTangent = new LinkedList<Tuple<int, int>>();

            foreach (Tuple<int, int> point1 in partA)
            {
                foreach (Tuple<int, int> point2 in partB)
                {
                    if (yCoordinate(point1, point2, xCoordinate) > max)
                    {
                        upperTangent.Clear();
                        max = yCoordinate(point1, point2, xCoordinate);
                        LinkedListNode<Tuple<int, int>> tempNode1 = new LinkedListNode<Tuple<int, int>>(point1);
                        LinkedListNode<Tuple<int, int>> tempNode2 = new LinkedListNode<Tuple<int, int>>(point2);
                        upperTangent.AddLast(tempNode1);
                        upperTangent.AddLast(tempNode2);
                    }
                }
            }
            //Console.WriteLine("Upper Tangent");
            //PrintLinkedList(upperTangent);

            return upperTangent;
        }

        static LinkedList<Tuple<int, int>> findLowerTangent(LinkedList<Tuple<int, int>> partA, LinkedList<Tuple<int, int>> partB, double xCoordinate)
        {
            double min = Double.MaxValue;
            LinkedList<Tuple<int, int>> lowerTangent = new LinkedList<Tuple<int, int>>();

            foreach (Tuple<int, int> point1 in partA)
            {
                foreach (Tuple<int, int> point2 in partB)
                {
                    if (yCoordinate(point1, point2, xCoordinate) < min)
                    {
                        lowerTangent.Clear();
                        min = yCoordinate(point1, point2, xCoordinate);
                        LinkedListNode<Tuple<int, int>> tempNode1 = new LinkedListNode<Tuple<int, int>>(point1);
                        LinkedListNode<Tuple<int, int>> tempNode2 = new LinkedListNode<Tuple<int, int>>(point2);
                        lowerTangent.AddLast(tempNode1);
                        lowerTangent.AddLast(tempNode2);
                    }
                }
            }
            //Console.WriteLine("Lower Tangent");
            //PrintLinkedList(lowerTangent);

            return lowerTangent;
        }

        static double distance(Tuple<int, int> point1, Tuple<int, int> point2)
        {
            return (Math.Sqrt(Math.Abs(Math.Pow((point2.Item1 - point1.Item1), 2) + Math.Pow((point2.Item2 - point1.Item1), 2))));
        }

        static double yCoordinate(Tuple<int, int> point1, Tuple<int, int> point2, double xCoordinate)
        {
            double slope = (double)(point2.Item2 - point1.Item2) / (double)(point2.Item1 - point1.Item1);
            double y1 = slope * (xCoordinate - point1.Item1) + (point1.Item2);

            return y1;
        }

        static Tuple<int, int> findMaxX(LinkedList<Tuple<int, int>> setOfPoints)
        {
            int max = int.MinValue;
            Tuple<int, int> maxXPoint = new Tuple<int, int>(setOfPoints.ElementAt(0).Item1, setOfPoints.ElementAt(0).Item2);
            foreach (Tuple<int, int> point in setOfPoints)
            {
                if (point.Item1 > max)
                {
                    max = point.Item1;
                    maxXPoint = point;
                }
            }

            return maxXPoint;
        }

        static Tuple<int, int> findMinX(LinkedList<Tuple<int, int>> setOfPoints)
        {
            int min = int.MaxValue;
            Tuple<int, int> minXPoint = new Tuple<int, int>(setOfPoints.ElementAt(0).Item1, setOfPoints.ElementAt(0).Item2);
            foreach (Tuple<int, int> point in setOfPoints)
            {
                if (point.Item1 < min)
                {
                    min = point.Item1;
                    minXPoint = point;
                }
            }

            return minXPoint;
        }

        static void PrintTuple(List<Tuple<int, int>> pointList)
        {
            foreach (Tuple<int, int> tupleToPrint in pointList)
            {
                Console.WriteLine("(" + tupleToPrint.Item1 + "," + tupleToPrint.Item2 + ")");
            }
        }

        static void PrintLinkedList(LinkedList<Tuple<int, int>> pointList)
        {
            foreach (Tuple<int, int> tupleToPrint in pointList)
            {
                Console.WriteLine("(" + tupleToPrint.Item1 + "," + tupleToPrint.Item2 + ")");
            }
            //Console.WriteLine("(" + pointList.ElementAt(0).Item1 + "," + pointList.ElementAt(0).Item2 + ")");
        }

        static List<Tuple<int, int>> sortByX(List<Tuple<int, int>> pointList)
        {
            pointList = (from Tuple<int, int> a in pointList orderby a.Item1 select a).ToList<Tuple<int, int>>();

            return pointList;
        }

        static LinkedList<Tuple<int, int>> sortByX(LinkedList<Tuple<int, int>> pointList)
        {
            List<Tuple<int, int>> tempList = new List<Tuple<int, int>>();

            tempList = (from Tuple<int, int> a in pointList orderby a.Item1 select a).ToList<Tuple<int, int>>();

            foreach (Tuple<int, int> point in tempList)
            {
                pointList.AddLast(point);
            }

            return pointList;
        }

        static int[] generateRandomArray(int n)
        {
            int[] randomArray = new int[n];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                int numberToAdd = rand.Next(1, 1000);
                while (randomArray.Contains(numberToAdd))
                {
                    numberToAdd = rand.Next(1, 10000);
                }
                randomArray[i] = numberToAdd;
            }

            return randomArray;
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
    }
}
