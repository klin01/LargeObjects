using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using LargeObjects;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Scenario1();
            //Scenario2();
            Scenario1();
        }

        public static void Scenario1()
        {
            var list = new List<double[,]>();
            try
            {
                for (var i = 0; i < 100; i++)
                {
                    try
                    {
                        list.Add(new double[121, 340000]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(i.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Scenario2()
        {
            long memoryBefore = GC.GetTotalMemory(true);
            var list = new double[121, 340000];
            long memoryAfter = GC.GetTotalMemory(true);

            Console.WriteLine((memoryAfter - memoryBefore).ToString() + "Bytes");
        }

        public static void Scenario3()
        {
            var list = new List<BigMultiArray<double>>();
            try
            {
                for (var i = 0; i < 100; i++)
                {
                    try
                    {
                        list.Add(new BigMultiArray<double>(121, 340000));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(i.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
