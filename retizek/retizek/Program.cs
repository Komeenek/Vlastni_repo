using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retizek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pocet_lidi = Convert.ToInt32(Console.ReadLine());
            
            string[] vsechny_dcojice = Console.ReadLine().Split();

            string[] zacatek_a_konec = Console.ReadLine().Split();

            int zacatek = Convert.ToInt32(zacatek_a_konec[0]) - 1;

            int konec = Convert.ToInt32(zacatek_a_konec[1]) - 1;

            bool[,] pratelstvi = new bool[pocet_lidi, pocet_lidi];

            for (int i = 0; i < vsechny_dcojice.Length; i++)
            {
                string[] dvojice = vsechny_dcojice[i].Split('-');

                pratelstvi[Convert.ToInt32(dvojice[0]) - 1, Convert.ToInt32(dvojice[1]) - 1] = true;

                pratelstvi[Convert.ToInt32(dvojice[1]) - 1, Convert.ToInt32(dvojice[0]) - 1] = true;
            }

            /*
            for (int i = 0; i < pocet_lidi; i++)
            {
                for (int y = 0; y < pocet_lidi; y++)
                {
                    Console.WriteLine(pratelstvi[i, y]);
                }
            }
            
            Console.ReadLine();
            */


        }


    }
}
