using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using namespace;

namespace ConnectGame
{
    // MODIFIKÁTORY PŘÍSTUPU (ACCESSIBILITY MODIFIERS)
    // public, private, protected, ... internal
    // -> ZAPOUZDŘENÍ (viditelnost určitých částí kódu)
    internal class Program
    {
        // v C# je vše ve třídě, v třídě jsou vlasnotsi, a funkce, datové položky
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

            int hrac = 2;
            int pocetKamenuNaVyhru = 5;

            Hra hra1 = new Hra(5, 6, 7);

            Console.WriteLine(hra1.Play());
            Console.ReadLine();

        }

    }

    class Hra
    {
        //konstruktor
        public Hra(int pocetVyhernichZetonu, int sirkaPole, int vyskaPole)
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu;
            hraciPole = new int[sirkaPole, vyskaPole];
        }

        int pocetVyhernichZetonu; // datová položka
        int[,] hraciPole;

        public Hrac Play()
        {
            Hrac hrac1 = new Hrac();
            Hrac hrac2 = new Hrac();

            string[] prezdivky = NacteniPrezdivek();

            hrac1.Jmeno = prezdivky[0];
            hrac2.Jmeno = prezdivky[1];







            return hrac1;

            // Tah
            // Check
            // střídání hračů
        }
        string[] NacteniPrezdivek()
        {
            Console.WriteLine("Napiš přezdívku prvního hráče:");
            string prezdivka1 = Console.ReadLine();
            Console.WriteLine("Napiš přezdívku druhého hráče:");
            string prezdivka2 = Console.ReadLine();

            return new string[] { prezdivka1, prezdivka2 };
        } 


        bool Check(int[,] board, int[] soucasnaPozice, int hrac, int pocetKamenuNaVyhru)
        {
            int radek = soucasnaPozice[0];
            int sloupec = soucasnaPozice[1];
            return CheckColumn(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckRow(board, radek, sloupec, hrac, pocetKamenuNaVyhru) || CheckDiag(board, radek, sloupec, hrac, pocetKamenuNaVyhru);
        }

        bool CheckColumn(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 0;
            int pocetRadku = gameField.GetLength(0);

            for (int i = radek; i < pocetRadku; i++)
            {
                if (gameField[i, sloupec] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        bool CheckRow(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = sloupec + 1; i < pocetSloupcu; i++)
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }


            for (int i = sloupec - 1; i >= 0; i--)
            {
                if (gameField[radek, i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        bool CheckDiag(int[,] gameField, int radek, int sloupec, int currentPlayer, int reqForWin)
        {
            int pocet = 1;
            int pocetRadku = gameField.GetLength(0);
            int pocetSloupcu = gameField.GetLength(1);

            for (int i = 1; radek + i < pocetRadku && sloupec + i < pocetSloupcu; i++)
            {
                if (gameField[radek + i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek - i >= 0 && sloupec - i >= 0; i++)
            {
                if (gameField[radek - i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }


            pocet = 1;


            for (int i = 1; radek - i >= 0 && sloupec + i < pocetSloupcu; i++)
            {
                if (gameField[radek - i, sloupec + i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek + i < pocetRadku && sloupec - i >= 0; i++)
            {
                if (gameField[radek + i, sloupec - i] == currentPlayer)
                {
                    pocet++;
                    if (pocet >= reqForWin)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

    }

    class Hrac
    {
        public string Jmeno;
        public string Symbol;
    }
    struct Position
    {
        public int Row;
        public int Column;
    }


}