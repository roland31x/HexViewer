using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexviewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "test.txt";
            int count = 0;
            int offset = 0;
            int offsetcount = 0;
            Console.WriteLine(" Offset  |                Valori Caractere Hex             |     Continut");
            Console.WriteLine("-----------------------------------------------------------------------------");
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1];
                int[] bytes = new int[16];
                while ((fs.Read(b, 0, b.Length)) > 0)
                {
                    if (count == 0)
                    {
                        ConvertToHex(offset, ref count);
                        offset++;
                        Console.Write("0 ");
                        Console.Write(": ");
                    }
                   
                    ConvertToHex(b[0]);
                    bytes[count] = b[0];
                    count++;
                    Console.Write(" ");
                    if(count == 16)
                    {
                        Console.Write("| ");
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            if (bytes[i] < 31 || bytes[i] > 126 && bytes[i] < 159)
                            {
                                Console.Write(".");
                            }
                            else
                            {
                                Console.Write((char)bytes[i]);
                            }
                        }
                        count = 0;
                        Console.WriteLine();
                    }
                }
                int lastROW = count;

                while (count < 16)
                {
                    
                    Console.Write("   ");
                    
                    count++;
                }
                Console.Write("| ");
                for (int i = 0; i < lastROW; i++)
                {
                    if (bytes[i] < 31 || bytes[i] > 126 && bytes[i] < 159)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write((char)bytes[i]);
                    }
                }
                Console.WriteLine();
            }
        }
        static void ConvertToHex(int s,ref int count)
        {
            int intreg = s;
            int b2 = 16;
            int intregaux = intreg;
            int countb10 = 1;

            while (intregaux > 0)
            {
                // Console.WriteLine(intregaux);
                intregaux = intregaux / b2;
                countb10++;
            }

            //Console.WriteLine(intreg);

            string[] r = new string[countb10];
            int count2 = 0;
            while (intreg > 0)
            {
                r[count2] = Convert.ToString(intreg % b2);
                intreg = intreg / b2;
                count2++;
            }
            string[] hexb = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            for (int i = 0; i < r.Length; i++)
            {
                for (int j = 0; j < hexb.Length; j++)
                {
                    if (r[i] == Convert.ToString(j))
                    {
                        r[i] = hexb[j];
                    }
                }
            }
            int offsethelper = r.Length;
            if (count == 0)
            {
                while (offsethelper < 8)
                {
                    Console.Write("0");
                    offsethelper++;
                }
            }
            for (int i = r.Length - 2; i >= 0; i--)
            {
                Console.Write(r[i]);
            }
        }
        static void ConvertToHex(int s)
        {
            int intreg = s;
            int b2 = 16;
            int intregaux = intreg;
            int countb10 = 1;

            while (intregaux > 0)
            {
                // Console.WriteLine(intregaux);
                intregaux = intregaux / b2;
                countb10++;
            }

            //Console.WriteLine(intreg);

            string[] r = new string[countb10];
            int count2 = 0;
            while (intreg > 0)
            {
                r[count2] = Convert.ToString(intreg % b2);
                intreg = intreg / b2;
                count2++;
            }
            string[] hexb = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            for (int i = 0; i < r.Length; i++)
            {
                for (int j = 0; j < hexb.Length; j++)
                {
                    if (r[i] == Convert.ToString(j))
                    {
                        r[i] = hexb[j];
                    }
                }
            }
            if (r.Length < 3)
            {
                Console.Write("0");
            }
            for (int i = r.Length - 2; i >= 0; i--)
            {
                Console.Write(r[i]);
            }
        }
    }
}
