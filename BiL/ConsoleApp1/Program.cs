using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] pole = NacteniVstupu();

            VypsaniPole(pole);
            for (int i = 0; i < 20; i++)
            {
                Krok(pole);
                VypsaniPole(pole);
            }

            Console.ReadLine();
        }

        static char[,] NacteniVstupu()
        {

            int sirka = Convert.ToInt32(Console.ReadLine());
            int vyska = Convert.ToInt32(Console.ReadLine());

            char[,] pole = new char[vyska,sirka];

            for (int i = 0; i < vyska; i++) 
            { 
                string radek = Console.ReadLine();

                for (int j = 0; j < sirka; j++)
                {
                    pole[i, j] = radek[j];
                }
            }

            return pole;
        }

        static void VypsaniPole(char[,] pole)
        {
            for (int i = 0;i < pole.GetLength(0);i++)
            {
                string stringovnik = "";
                for (int j = 0;j < pole.GetLength(1);j++)
                {
                    stringovnik += pole[i, j];
                }
                Console.WriteLine(stringovnik);
            }
        }

        static int[] NajdiMrBeasta(char[,] pole)
        {
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                    if (pole[i, j] == '>')
                        return new int [3] {i, j, 0};
                    else if (pole[i, j] == 'v')
                        return new int[3] { i, j, 1};
                    else if (pole[i, j] == '<')
                        return new int[3] { i, j, 2};
                    else if (pole[i, j] == '^')
                        return new int[3] { i, j, 3};
            }
            return null;
        }

        static char[,] Krok(char[,] pole)
        {
            int[] pozice = NajdiMrBeasta(pole);
            bool spravne = false;
            while (!spravne)
            {
                if 
            }


            return pole;
        } 
    }
}
