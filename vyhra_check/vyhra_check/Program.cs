using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vyhra_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] board = new int[6, 7]{
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 2, 1, 0 },
            { 2, 2, 2, 2, 2, 1, 0 }
};
            int[] position = { 5, 0 };
            Console.WriteLine(VyhraCheck(board, 5, position));

            Console.ReadLine();
        }

        static bool VyhraCheck(int[,] matice, int VyherniPocet, int[] pozice)
        {
            int posledni_symbol = matice[pozice[0], pozice[1]];
            
            bool CheckRadku()
            {
                int[] radek = new int[2 * VyherniPocet - 1];

                for (int x = 0; x < 2 * VyherniPocet - 1; x++)
                {
                    try
                    {
                        radek[x] = matice[pozice[1] - VyherniPocet];
                    }
                }

                for (int i = 0; i < VyherniPocet; i++)
                {
                    if (radek[i] == posledni_symbol)
                    {
                        for (int j = 0; j < VyherniPocet; j++)
                        {
                            if (j != posledni_symbol)
                                return false;
                        }
                        return true;
                    }
                }
                return false;
            }

            return CheckRadku();
        }
    }
}
