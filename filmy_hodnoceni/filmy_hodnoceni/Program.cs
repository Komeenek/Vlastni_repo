using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmy_hodnoceni
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Film film1 = new Film("Pavouci Muz", "Hustej", "Wimpy", 2011);
            Film film2 = new Film("Tenet", "Christopher", "Nolan", 2020);
            Film film3 = new Film("Shrek", "Andrew", "Adamson", 2001);

            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                film1.VsechnaHodnoceni.Add(rnd.Next(6));
                film2.VsechnaHodnoceni.Add(rnd.Next(6));
                film3.VsechnaHodnoceni.Add(rnd.Next(6));
            }

            film1.Hodnotitko();
            film2.Hodnotitko();
            film3.Hodnotitko();

            if (film1.Hodnoceni < 3)
                Console.WriteLine($"{film1.Nazev} je odpad! Má hodnocení jen {film1.Hodnoceni}.");
            if (film2.Hodnoceni < 3)
                Console.WriteLine($"{film2.Nazev} je odpad! Má hodnocení jen {film2.Hodnoceni}.");
            if (film3.Hodnoceni < 3)
                Console.WriteLine($"{film3.Nazev} je odpad! Má hodnocení jen {film3.Hodnoceni}.");

            Console.WriteLine(film1.Vypsavatko());
            Console.WriteLine(film2.Vypsavatko());
            Console.WriteLine(film3.Vypsavatko());

            Console.ReadLine();
        }
    }

    class Film
    {
        public Film(string nazev, string jmenoRezisera, string prijmeniRezisera, int rokVzniku)
        {
            Nazev = nazev;
            JmenoRezisera = jmenoRezisera;
            PrijmeniRezisera = prijmeniRezisera;
            RokVzniku = rokVzniku;
            VsechnaHodnoceni = new List<int>();
        }

        public string Nazev { get; set; }
        string JmenoRezisera { get; set; }
        string PrijmeniRezisera { get; set; }
        int RokVzniku { get; set; }

        public double Hodnoceni {  get; private set; }

        public List<int> VsechnaHodnoceni { get; set; }

        public void Hodnotitko() 
        {
            double soucet = 0;
            for (int i = 0; i < VsechnaHodnoceni.Count; i++)
            {
                soucet += VsechnaHodnoceni[i];
            }
            Hodnoceni = soucet/VsechnaHodnoceni.Count;
        }

        public void VypsavatkoHodnoceni(List<int> VsechnaHodnoceni)
        {

        }

        public string Vypsavatko()
        {
            string finalniString = "";
            finalniString += $"{Nazev} ({RokVzniku}; {PrijmeniRezisera}, {JmenoRezisera[0]}): {Hodnoceni} ⭐";
            return finalniString;
        }
    }
}
