using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Connect4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Main chceme mít co nejstručnější

            Console.WriteLine("Zadej počet sloupců tabulky: ");
            int pocetSloupcu = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Zadej počet řádků tabulky: ");
            int pocetRadku = Convert.ToInt32(Console.ReadLine());

            
            Console.WriteLine("Zadej počet symbolů potřebných pro výhru: ");
            int pocetNaVyhru = Convert.ToInt32(Console.ReadLine());

            // zeptáme se hráče na parametry pro hru (inspirujte se rozšířeními)

            // podle toho vytvoříme hru
            Hra hra = new Hra(pocetNaVyhru, pocetSloupcu, pocetRadku, 2); // voláme konstruktor při vytváření instance
            Hrac vitez = hra.Play(); // funkce Play vrací vítěze
            Console.WriteLine("Vyhrál " + vitez.Jmeno);
            Thread.Sleep(3000);
            Environment.Exit(0);
            // vypište vítěze
        }

    }

    

    class Hra
    {
        //konstruktor - jeho parametry by měly obsahovat vše, co je potřeba nastavit při inicializaci herního objektu (jedné instanci hry)
        public Hra(int pocetVyhernichZetonu, int pocetSloupcu, int pocetRadku, int pocetHracu)
        {
            this.pocetVyhernichZetonu = pocetVyhernichZetonu; // použili jsme slovo this, protože se parametr konstruktoru nazývá stejně jako datová položka v této třídě
            hraciPole = new char[pocetSloupcu, pocetRadku];

            hraci = new Hrac[pocetHracu]; // vytvoří pole ale už nevytvoří samotné hráče! zatím jsou v poli jen samé null hodnoty
            VytvorHrace(); // hráči v poli se vytvoří až v této funkci

        }



        // následující datové položky jsou privátní - není potřeba, aby byly vidět mimo třídu
        private int pocetVyhernichZetonu; // datová položka
        private char[,] hraciPole;
        private Hrac[] hraci;

        private void VytvorHrace()
        {
            for (int i = 0; i < hraci.Length; i++)
            {
                Console.WriteLine($"Napiš jméno hráče {i}:");
                string jmeno = Console.ReadLine();
                Console.WriteLine($"Jaký symbol pro něj budeš chtít použít (klidně ne 0):");
                char symbol = Convert.ToChar(Console.ReadLine());

                hraci[i] = new Hrac(jmeno, symbol); // až zde dojde k reálnému vytvoření hráče v paměti (konkrétně na *haldě*)
            }
        }

        void PrintTabulku(char[,] board)
        {
            int radky = board.GetLength(0);
            int sloupce = board.GetLength(1);

            Console.WriteLine("Hrací plán:");

            for (int i = 0; i < sloupce; i++)
            {
                for (int j = 0; j < radky; j++)
                {
                    if (board[j, i] == 0)
                    {
                        Console.Write("0" + " ");
                    }
                    else
                    {
                        Console.Write(board[j, i] + " ");
                    }
                    
                }
                Console.WriteLine(" ");
            }
        }

        // hru ovšem musíme nějak spustit, je tedy public, aby šla spustit i ze třídy Program, vrací vítězného hráče
        public Hrac Play()
        {
            // TODO - následující úkoly si zaslouží vlastní funkci/metodu
            PrintTabulku(hraciPole);
            int kamZahral = 0;
            Position posledniPozice = new Position();
            Console.WriteLine("Pokud se budete chtít vzdát, stačí napsat 'ff' místo čísla sloupce. Dále, pokud se pokusíte zahrát do řádku, který je plný, přijdete tím o svůj tah :)");
            int radek = -1;
            string vstup = "aaa";


            while (true)
            {
                if (JePlna(hraciPole))
                    break;

                Console.WriteLine("Na řadě je " + hraci[0].Jmeno + ", napiš, do kterého řádku chceš zahrát: ");
                vstup = Console.ReadLine();
                if (vstup == "ff")
                    return hraci[1];
                kamZahral = Convert.ToInt32(vstup) - 1;
                posledniPozice.Column = kamZahral;
                radek = ZjisteniRadku(hraciPole, kamZahral);
                if (radek != -1)
                {
                    posledniPozice.Row = radek;
                    hraciPole[posledniPozice.Row, posledniPozice.Column] = hraci[0].Symbol;
                }
                PrintTabulku(hraciPole);
                if (Check(hraciPole, pocetVyhernichZetonu, hraci[0], posledniPozice) == true)
                {
                    return hraci[0];
                }
                if (JePlna(hraciPole))
                    break;

                Console.WriteLine("Na řadě je " + hraci[1].Jmeno + ", napiš, do kterého řádku chceš zahrát: ");
                vstup = Console.ReadLine();
                if (vstup == "ff")
                    return hraci[0];
                kamZahral = Convert.ToInt32(vstup) - 1;
                posledniPozice = new Position();
                posledniPozice.Column = kamZahral;
                radek = ZjisteniRadku(hraciPole, kamZahral);
                if (radek != -1)
                {
                    posledniPozice.Row = radek;
                    hraciPole[posledniPozice.Row, posledniPozice.Column] = hraci[1].Symbol;
                }
                PrintTabulku(hraciPole);
                if (Check(hraciPole, pocetVyhernichZetonu, hraci[1], posledniPozice) == true)
                {
                    return hraci[1];
                }

                if (hraci[0].Jmeno == "komeenek")
                {
                    if (JePlna(hraciPole))
                        break;

                    Console.WriteLine("Na řadě je " + hraci[0].Jmeno + ", napiš, do kterého řádku chceš zahrát: ");
                    vstup = Console.ReadLine();
                    if (vstup == "ff")
                        return hraci[1];
                    kamZahral = Convert.ToInt32(vstup) - 1;
                    posledniPozice.Column = kamZahral;
                    radek = ZjisteniRadku(hraciPole, kamZahral);
                    if (radek != -1)
                    {
                        posledniPozice.Row = radek;
                        hraciPole[posledniPozice.Row, posledniPozice.Column] = hraci[0].Symbol;
                    }
                    PrintTabulku(hraciPole);
                    if (Check(hraciPole, pocetVyhernichZetonu, hraci[0], posledniPozice) == true)
                    {
                        return hraci[0];
                    }
                }

                if (hraci[1].Jmeno == "komeenek")
                {
                    if (JePlna(hraciPole))
                        break;

                    Console.WriteLine("Na řadě je " + hraci[1].Jmeno + ", napiš, do kterého řádku chceš zahrát: ");
                    vstup = Console.ReadLine();
                    if (vstup == "ff")
                        return hraci[0];
                    kamZahral = Convert.ToInt32(vstup) - 1;
                    posledniPozice = new Position();
                    posledniPozice.Column = kamZahral;
                    radek = ZjisteniRadku(hraciPole, kamZahral);
                    if (radek != -1)
                    {
                        posledniPozice.Row = radek;
                        hraciPole[posledniPozice.Row, posledniPozice.Column] = hraci[1].Symbol;
                    }
                    PrintTabulku(hraciPole);
                    if (Check(hraciPole, pocetVyhernichZetonu, hraci[1], posledniPozice) == true)
                    {
                        return hraci[1];
                    }
                }
            }

            Console.WriteLine("Je to remíza, gg");
            Thread.Sleep(3000);
            Environment.Exit(0);
            return hraci[0];
        }

        private int ZjisteniRadku(char[,] pole, int kamZahral)
        {
            int radky = pole.GetLength(0) - 1;

            for (int i = 0; i < pole.GetLength(0); i++)
            {
                if (pole[(radky - i), kamZahral] == 0)
                    return radky - i;
            }
            return -1;
        }
        private bool JePlna(char[,] pole)
        {
            for (int i = 0; i < pole.GetLength(1); i++)
            {
                if (pole[0, i] == 0)
                    return false;
            }
            return true;
        }

        private bool Check(char[,] pole, int pocetNaVyhru, Hrac hrac, Position posledniTah)
        {
            return CheckRow(pole, pocetNaVyhru, hrac, posledniTah) || CheckColumn(pole, pocetNaVyhru, hrac, posledniTah) || CheckDiagonal1(pole, pocetNaVyhru, hrac, posledniTah) || CheckDiagonal2(pole, pocetNaVyhru, hrac, posledniTah);
        }

        private bool CheckColumn(char[,] pole, int pocetNaVyhru, Hrac hrac, Position posledniTah)
        {
            int radek = posledniTah.Row;
            int sloupec = posledniTah.Column;
            int pocet = 1;
            while (true)
            {
                radek++;
                if (radek >= pole.GetLength(0)) break;
                else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            radek = posledniTah.Row;
            while (true)
            {
                radek--;
                if (radek < 0) break;
                else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            if (pocet >= pocetNaVyhru)
            {
                return true;
            }
            return false;
        }

        private bool CheckRow(char[,] pole, int pocetNaVyhru, Hrac hrac, Position posledniTah)
        {
            int radek = posledniTah.Row;
            int sloupec = posledniTah.Column;
            int pocet = 1;
            while (true)
            {
                sloupec++;
                if (sloupec >= pole.GetLength(1)) break;
                else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            sloupec = posledniTah.Column;
            while (true)
            {
                sloupec--;
                if (sloupec < 0) break;
                else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            if (pocet >= pocetNaVyhru)
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonal1(char[,] pole, int pocetNaVyhru, Hrac hrac, Position posledniTah)
        {
            int radek = posledniTah.Row;
            int sloupec = posledniTah.Column;
            int pocet = 1;
            while (true)
            {
                sloupec++;
                radek--;
                if (sloupec >= pole.GetLength(1) || radek < 0) break;
         else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            radek = posledniTah.Row;
            sloupec = posledniTah.Column;
            while (true)
            {
                sloupec--;
                radek++;
                if (sloupec < 0 || radek >= pole.GetLength(0)) break;
         else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            if (pocet >= pocetNaVyhru)
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonal2(char[,] pole, int pocetNaVyhru, Hrac hrac, Position posledniTah)
        {
            int radek = posledniTah.Row;
            int sloupec = posledniTah.Column;
            int pocet = 1;
            while (true)
            {
                sloupec--;
                radek--;
                if (sloupec < 0 || radek < 0) break;
         else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            radek = posledniTah.Row;
            sloupec = posledniTah.Column;
            while (true)
            {
                sloupec++;
                radek++;
                if (sloupec >= pole.GetLength(1) || radek >= pole.GetLength(0)) break;
         else if (pole[radek, sloupec] == hrac.Symbol)
                {
                    pocet++;
                }
                else break;
            }
            if (pocet >= pocetNaVyhru)
            {
                return true;
            }
            return false;
        }

    }

    class Hrac
    {
        // Jméno i Symbol budeme využívat i v třídě Hra, proto jsou PUBLIC
        // Protože jsou public, tak je píšeme s velkým počátečním písmenem
        public string Jmeno { get; }
        public char Symbol { get; }

        public Hrac(string jmeno, char symbol)
        {
            Jmeno = jmeno;
            Symbol = symbol;
        }
    }

    // struktura je jako třída, ale paměťově omezená, hodí se pro jednoduché datové typy jako například udržování dvojice souřadnic pro pozici ve hře
    struct Position
    {
        public int Row;
        public int Column;
    }



}
