using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VirtualAddressTranslation
{
    class Program
    {
        static void Main(string[] args)
        {
            ///
            /// HW5(AaronReyes)
            /// 2016/10/05
            ///
            List<int> ValuesFromFileList = new List<int>();
            ValuesFromFileList = ReadInFromFile();

            int VirtAddWidth = ValuesFromFileList[0];
            int TLBEntries = ValuesFromFileList[1];
            int TLBOrganization = ValuesFromFileList[2];
            int PageSize = ValuesFromFileList[3];
            int PhysAddWidth = ValuesFromFileList[4];
            int L1Blocks = ValuesFromFileList[5];
            int L1BlockSize = ValuesFromFileList[6];
            int L1Org = ValuesFromFileList[7];

            Console.WriteLine("Initial Values");
            Console.WriteLine("Virtual Address Width : " + VirtAddWidth);
            Console.WriteLine("TLB Entries : " + TLBEntries);
            Console.WriteLine("TLB Orginization : " + TLBOrganization);
            Console.WriteLine("Page Size : " + PageSize);
            Console.WriteLine("Physical Adress Width : " + PhysAddWidth);
            Console.WriteLine("L1 Blocks : " + L1Blocks);
            Console.WriteLine("L1 Block Size : " + L1BlockSize);
            Console.WriteLine("L1 Orginization: " + L1Org);
            Console.WriteLine();

            //Virtual Adress
            double PageOffset = Math.Log(PageSize, 2);
            double TLBindex = Math.Log((TLBEntries / TLBOrganization), 2);

            //TLB
            double TLBTag = (VirtAddWidth) - (TLBindex + PageOffset);
            double PhysPage = PhysAddWidth - PageOffset;

            //Cache
            double BlockIndex = Math.Log(L1BlockSize, 2);
            double CacheIndex = Math.Log((L1Blocks / L1Org), 2);
            double CacheTag = PhysAddWidth - (BlockIndex + CacheIndex);

            //Data
            double Data = PageOffset - BlockIndex;
            double CacheData = PageSize / BlockIndex;

            Console.WriteLine("Item |" + " Value");
            Console.WriteLine("a    | " + VirtAddWidth);
            Console.WriteLine("b    | " + TLBTag);
            Console.WriteLine("c    | " + TLBindex);
            Console.WriteLine("d    | " + PageOffset);
            Console.WriteLine("e    | " + TLBindex);
            Console.WriteLine("f    | " + TLBTag);
            Console.WriteLine("g    | " + PhysPage);
            Console.WriteLine("h    | " + PhysAddWidth);
            Console.WriteLine("i    | " + PhysPage);
            Console.WriteLine("j    | " + PageOffset);
            Console.WriteLine("k    | " + CacheTag);

            Console.WriteLine("l    | " + CacheIndex);
            Console.WriteLine("m    | " + BlockIndex);
            Console.WriteLine("n    | " + CacheIndex);
            Console.WriteLine("o    | " + CacheTag);
            Console.WriteLine("p    | " + CacheData);
            Console.WriteLine("q    | " + PhysPage);
            Console.WriteLine("r    | " + PageOffset);
            Console.WriteLine("s    | " + PageSize);
            Console.WriteLine("t    | " + Data);
            Console.WriteLine("u    | " + BlockIndex);
            Console.WriteLine("v    | " + BlockIndex);
            Console.ReadKey();
        }

        public static List<int> ReadInFromFile()
        {


            string TempString;
            string VirtAddWidthString = "Null";
            string TLBEntriesString = "Null";
            string TLBOrganizationString = "Null";
            string PageSizeString = "Null";
            string PhysAddWidthString = "Null";
            string L1BlocksString = "Null";
            string L1BlockSizeString = "Null";
            string L1OrgString = "Null";

            int VirtAddWidth = -1;
            int TLBEntries = -1;
            int TLBOrganization = -1;
            int PageSize = -1;
            int PhysAddWidth = -1;
            int L1Blocks = -1;
            int L1BlockSize = -1;
            int L1Org = -1;

            List<int> ValuesFromFileList = new List<int>();

            try
            {

            StreamReader sr = new StreamReader("info.txt");
            TempString = sr.ReadLine();
            VirtAddWidthString = TempString.Substring(22);
            VirtAddWidth = int.Parse(VirtAddWidthString);

            TempString = sr.ReadLine();
            TLBEntriesString = TempString.Substring(12);
            TLBEntries = int.Parse(TLBEntriesString);

            TempString = sr.ReadLine();
            TLBOrganizationString = TempString.Substring(17);
            if (TLBOrganizationString.Contains("8"))
            {
                TLBOrganization = 8;
            }
            else if (TLBOrganizationString.Contains("2"))
            {
                TLBOrganization = 2;
            }
            else if (TLBOrganizationString.Contains("4"))
            {
                TLBOrganization = 4;
            }
            else if (TLBOrganizationString.Contains("16"))
            {
                TLBOrganization = 16;
            }

            TempString = sr.ReadLine();
            PageSizeString = TempString.Substring(10);
            PageSize = int.Parse(PageSizeString);

            TempString = sr.ReadLine();
            PhysAddWidthString = TempString.Substring(23);
            PhysAddWidth = int.Parse(PhysAddWidthString);

            TempString = sr.ReadLine();
            L1BlocksString = TempString.Substring(10);
            L1Blocks = int.Parse(L1BlocksString);

            TempString = sr.ReadLine();
            L1BlockSizeString = TempString.Substring(14);
            L1BlockSize = int.Parse(L1BlockSizeString);

            TempString = sr.ReadLine();
            L1OrgString = TempString.Substring(16);
            if (L1OrgString.Contains("2"))
            {
                L1Org = 2;
            }
            else if (L1OrgString.Contains("4"))
            {
                L1Org = 4;
            }
            else if (L1OrgString.Contains("8"))
            {
                L1Org = 8;
            }
            else if (L1OrgString.Contains("16"))
            {
                L1Org = 16;
            }

            ValuesFromFileList.Add(VirtAddWidth);
            ValuesFromFileList.Add(TLBEntries);
            ValuesFromFileList.Add(TLBOrganization);
            ValuesFromFileList.Add(PageSize);
            ValuesFromFileList.Add(PhysAddWidth);
            ValuesFromFileList.Add(L1Blocks);
            ValuesFromFileList.Add(L1BlockSize);
            ValuesFromFileList.Add(L1Org);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ValuesFromFileList;
        }
    }
}
