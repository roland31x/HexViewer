using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hexviewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = null; // nu conteaza
            FilePath(ref path);
            //OverridePath(ref path); // //TREBUIE SETAT UN PATH EXISTENT LA FUNCTIA "OVERRIDEPATH" PRIMA DATA !!!
            HexViewer(path);
        }

        private static void OverridePath(ref string path) // pentru a seta manual un path catre un fisier care nu se afla in folderul solutiei
        {
            path = @"c:\testfolder\test.txt"; // asta e doar exemplul meu, s-ar putea sa nu existe acest path pe orice calculator
        }

        static void HexViewer(string path)
        {
            int count = 0;
            int offset = 0;
            Console.WriteLine(" Offset  |                Valori Caractere Hex             |     Continut");
            Console.WriteLine("-----------------------------------------------------------------------------");
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1];  // stocheaza byte-ul din filestream
                int[] bytes = new int[16]; // stocheaza caracterele citite 
                while ((fs.Read(b, 0, b.Length)) > 0)
                {
                    if (count == 0)
                    {
                        ConvertToHexOffset(offset);
                        offset++;
                        Console.Write("0 ");
                        Console.Write(": ");
                    }
                    ConvertToHex(b[0]);
                    bytes[count] = b[0];
                    count++;
                    Console.Write(" ");
                    if (count == 16)
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
        static void FilePath(ref string path)
        {
            path = Directory.GetCurrentDirectory();
            path += @"\TestFolder"; // testfolderu se va afla intotdeauna in folderul solutiei ( unde este si executabilul )
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine(path);
                Console.WriteLine("s-a creat folderul 'TestFolder' in path-ul de mai sus in care se pot pune fisierele de testat");
            }
            if (Directory.Exists(path))
            {
                path += @"\test.txt"; // schimbam aici ce fisier citim din testfolder.
                Console.WriteLine(path);
                if (!File.Exists(path))
                {
                    Console.WriteLine("s-a creat fisierul de mai sus care este testat implicit");
                    Thread.Sleep(100);
                    using (FileStream fs = File.Create(path))
                    {
                        for (byte i = 0; i < 100; i++)
                        {
                            fs.WriteByte(i);
                        }
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("eroare de gasire fisier (oops)");
                return;
            }
        }
        static void ConvertToHexOffset(int s) // pentru afisarea coloanei offsetului
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
            string[] hexb = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

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
            while (offsethelper < 8) // pentru a avea 0-uri inaintea offsetului
            {
                Console.Write("0");
                offsethelper++;
            }
            for (int i = r.Length - 2; i >= 0; i--)
            {
                Console.Write(r[i]);
            }
        }
        static void ConvertToHex(int s) // pentru conversia din baza 10 in baza 16
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
            string[] hexb = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

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
            if (r.Length < 3) // pentru a avea o lungime hex de minim 2 caractere.
            {
                Console.Write("0");
                if(r.Length < 2)
                {
                    Console.Write("0");
                }
            }
            for (int i = r.Length - 2; i >= 0; i--)
            {
                Console.Write(r[i]);
            }
        }
    }
}
