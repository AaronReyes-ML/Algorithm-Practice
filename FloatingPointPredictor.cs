using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HW8_AaronReyes_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Aaron Reyes
            //Homework 8
            #region initilization

            int oneBitPredictor = 0;
            int twoBitPredictor = 0;
            int OneBGlobalHistory = 0;
            int TwoBGlobalHistory = 0;
            int addBits = 2;
            int globalBits = 0;
            int[] predictorData = new int[16384];

            List<int> AddList = new List<int>();
            List<int> TruthList = new List<int>();
            readInFLPT(AddList, TruthList);
            double totalNumber = AddList.Count;
            int[] addressMask = { 0, 0, 3, 7, 15};
            int[] globalMask = { 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047};

            #endregion initilization

            #region Floating Point

            Console.WriteLine("One Bit Floating Point");
            while (addBits < 5)
            {
                globalBits = 0;
                while (globalBits < 11)
                {
                    double timesWrong = 0;
                    for (int i = 0; i < AddList.Count; i++)
                    {
                        int addPortion = AddList[i] & (addressMask[addBits]);
                        int globalPortion = OneBGlobalHistory & (globalMask[globalBits]);
                        //int predictorDataAdress = (addPortion << globalBits) | globalPortion;
                        int predictorDataAdress = addPortion | globalPortion;
                        OneBGlobalHistory = (OneBGlobalHistory << 1) | oneBitPredictor;

                        //Console.WriteLine("Predictor State Precheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                        if (predictorData[predictorDataAdress] != TruthList[i])
                        {
                            timesWrong++;
                            if (oneBitPredictor == 0)
                            {
                                oneBitPredictor = 1;
                            }
                            else
                            {
                                oneBitPredictor = 0;
                            }
                        }

                        predictorData[predictorDataAdress] = oneBitPredictor;
                        //Console.WriteLine("Predictor State Postcheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                    }
                    Console.WriteLine("Address Bits: " + addBits + ", Global Bits: " + globalBits);
                    Console.WriteLine();
                    double percentWrong = (timesWrong / totalNumber) * 100;
                    percentWrong = 100 - percentWrong;
                    Console.WriteLine("Times Wrong: " + timesWrong + ", Percentage of total: " + percentWrong);
                    globalBits += 1;
                }
                addBits += 1;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            int[] predictorData2 = new int[16384];
            Console.WriteLine("Two Bit Floating Point");
            addBits = 2;
            while (addBits < 5)
            {
                globalBits = 0;
                while (globalBits < 11)
                {
                    double timesWrong = 0;
                    for (int i = 0; i < AddList.Count; i++)
                    {
                        int addPortion = AddList[i] & (addressMask[addBits]);
                        int globalPortion = OneBGlobalHistory & (globalMask[globalBits]);
                        //int predictorDataAdress = (addPortion << globalBits) | globalPortion;
                        int predictorDataAdress = addPortion | globalPortion;
                        if (twoBitPredictor <= 1)
                        {
                            TwoBGlobalHistory = (TwoBGlobalHistory << 1);
                        }
                        else
                        {
                            TwoBGlobalHistory = (TwoBGlobalHistory << 1) | 1;
                        }

                        //Console.WriteLine("Predictor State Precheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                        int twoBitTruthValue = 0;
                        if (twoBitPredictor <= 1)
                        {
                            twoBitTruthValue = 0;
                        }
                        else
                        {
                            twoBitTruthValue = 1;
                        }

                        if (predictorData2[predictorDataAdress] != TruthList[i])
                        {
                            timesWrong++;

                            if (twoBitPredictor == 0)
                            {
                                twoBitPredictor = 1;
                            }
                            else if (twoBitPredictor == 1)
                            {
                                twoBitPredictor = 2;
                            }
                            else if (twoBitPredictor == 2)
                            {
                                twoBitPredictor = 3;
                            }
                            else if (twoBitPredictor == 3)
                            {
                                twoBitPredictor = 0;
                            }
                        }
                        else
                        {
                            if (twoBitPredictor == 1)
                            {
                                twoBitPredictor = 0;
                            }
                            if (twoBitPredictor == 3)
                            {
                                twoBitPredictor = 2;
                            }
                        }

                        predictorData2[predictorDataAdress] = twoBitTruthValue;
                        //Console.WriteLine("Predictor State Postcheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                    }
                    Console.WriteLine("Address Bits: " + addBits + ", Global Bits: " + globalBits);
                    Console.WriteLine();
                    double percentWrong = (timesWrong / totalNumber) * 100;
                    percentWrong = 100 - percentWrong;
                    Console.WriteLine("Times Wrong: " + timesWrong + ", Percentage of total: " + percentWrong);
                    globalBits += 1;
                }
                addBits += 1;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            #endregion Floating Point

            #region Integer

            oneBitPredictor = 0;
            twoBitPredictor = 0;
            addBits = 2;
            globalBits = 0;

            readInINT(AddList, TruthList);

            int[] predictorData3 = new int[16384];
            totalNumber = AddList.Count;

            Console.WriteLine("One Bit Integer");
            while (addBits < 5)
            {
                globalBits = 0;
                while (globalBits < 11)
                {
                    double timesWrong = 0;
                    for (int i = 0; i < AddList.Count; i++)
                    {
                        int addPortion = AddList[i] & (addressMask[addBits]);
                        int globalPortion = OneBGlobalHistory & (globalMask[globalBits]);
                        //int predictorDataAdress = (addPortion << globalBits) | globalPortion;
                        int predictorDataAdress = addPortion | globalPortion;
                        OneBGlobalHistory = (OneBGlobalHistory << 1) | oneBitPredictor;

                        //Console.WriteLine("Predictor State Precheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                        if (predictorData3[predictorDataAdress] != TruthList[i])
                        {
                            timesWrong++;
                            if (oneBitPredictor == 0)
                            {
                                oneBitPredictor = 1;
                            }
                            else
                            {
                                oneBitPredictor = 0;
                            }
                        }

                        predictorData3[predictorDataAdress] = oneBitPredictor;
                        //Console.WriteLine("Predictor State Postcheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                    }
                    Console.WriteLine("Address Bits: " + addBits + ", Global Bits: " + globalBits);
                    Console.WriteLine();
                    double percentWrong = (timesWrong / totalNumber) * 100;
                    percentWrong = 100 - percentWrong;
                    Console.WriteLine("Times Wrong: " + timesWrong + ", Percentage of total: " + percentWrong);
                    globalBits += 1;
                }
                addBits += 1;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            int[] predictorData4 = new int[16384];
            Console.WriteLine("Two Bit Integer");
            addBits = 2;
            while (addBits < 5)
            {
                globalBits = 0;
                while (globalBits < 11)
                {
                    double timesWrong = 0;
                    for (int i = 0; i < AddList.Count; i++)
                    {
                        int addPortion = AddList[i] & (addressMask[addBits]);
                        int globalPortion = OneBGlobalHistory & (globalMask[globalBits]);
                        //int predictorDataAdress = (addPortion << globalBits) | globalPortion;
                        int predictorDataAdress = addPortion | globalPortion;
                        if (twoBitPredictor <= 1)
                        {
                            TwoBGlobalHistory = (TwoBGlobalHistory << 1);
                        }
                        else
                        {
                            TwoBGlobalHistory = (TwoBGlobalHistory << 1) | 1;
                        }

                        //Console.WriteLine("Predictor State Precheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                        int twoBitTruthValue = 0;
                        if (twoBitPredictor <= 1)
                        {
                            twoBitTruthValue = 0;
                        }
                        else
                        {
                            twoBitTruthValue = 1;
                        }

                        if (predictorData4[predictorDataAdress] != TruthList[i])
                        {
                            timesWrong++;

                            if (twoBitPredictor == 0)
                            {
                                twoBitPredictor = 1;
                            }
                            else if (twoBitPredictor == 1)
                            {
                                twoBitPredictor = 2;
                            }
                            else if (twoBitPredictor == 2)
                            {
                                twoBitPredictor = 3;
                            }
                            else if (twoBitPredictor == 3)
                            {
                                twoBitPredictor = 0;
                            }
                        }
                        else
                        {
                            if (twoBitPredictor == 1)
                            {
                                twoBitPredictor = 0;
                            }
                            if (twoBitPredictor == 3)
                            {
                                twoBitPredictor = 2;
                            }
                        }


                        predictorData4[predictorDataAdress] = twoBitTruthValue;
                        //Console.WriteLine("Predictor State Postcheck: " + oneBitPredictor);
                        //Console.WriteLine("Value in Predictor Table: " + predictorData[predictorDataAdress]);
                    }
                    Console.WriteLine("Address Bits: " + addBits + ", Global Bits: " + globalBits);
                    Console.WriteLine();
                    double percentWrong = (timesWrong / totalNumber) * 100;
                    percentWrong = 100 - percentWrong;
                    Console.WriteLine("Times Wrong: " + timesWrong + ", Percentage of total: " + percentWrong);
                    globalBits += 1;
                }
                addBits += 1;
            }

            #endregion Integer

            Console.ReadKey();
        }
        #region Read
        static void readInFLPT(List<int> AddList, List<int> TruthList)
        {

            using (StreamReader sr = new StreamReader("BranchFltPt.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string tempString = sr.ReadLine();
                    string hexString = tempString.Substring(2, 8);
                    int truthValue = int.Parse(tempString.Substring(11));
                    int hexVal = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

                    AddList.Add(hexVal);
                    TruthList.Add(truthValue);
                }
            }
        }

        static void readInINT(List<int> AddList, List<int> TruthList)
        {

            using (StreamReader sr = new StreamReader("BranchInt.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string tempString = sr.ReadLine();
                    string hexString = tempString.Substring(2, 8);
                    int truthValue = int.Parse(tempString.Substring(11));
                    int hexVal = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

                    AddList.Add(hexVal);
                    TruthList.Add(truthValue);
                }
            }
        }
        #endregion Read
    }
}
