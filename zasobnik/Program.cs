using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zasobnik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("zavorky/soucet:");
            string vstup = Console.ReadLine();

            if (vstup == "zavorky")
            {
                Console.WriteLine("vstup:");
                string zavorky = Console.ReadLine();
                Console.WriteLine(Ozavorkovani(zavorky));
            }

            else if (vstup == "soucet")
            {
                Console.WriteLine("cislo:");
                string[] soucty = vsechny_soucty(Convert.ToInt32(Console.ReadLine()));
                for (int i = 0; i < soucty.Length; i++)
                {
                    Console.WriteLine(soucty[i]);
                }
            }
            Console.ReadLine();
        }

        static bool Ozavorkovani(string vstup)
        {
            
            char[] otevrene_zavorky = { '(', '[', '{' };
            char[] uzavrene_zavorky = { ')', ']', '}' };

            
            Stack<char> zasobnicek = new Stack<char>();

            foreach (char znak in vstup)
            {
                
                if (otevrene_zavorky.Contains(znak))
                {
                    zasobnicek.Push(znak);
                }
                
                else if (uzavrene_zavorky.Contains(znak))
                {
                    
                    if (zasobnicek.Count == 0)
                    {
                        return false;
                    }

                    else if (zasobnicek.Pop() != otevrene_zavorky[Array.IndexOf(uzavrene_zavorky, znak)])
                    {
                        return false;
                    }
                }
            }
            if (zasobnicek.Count != 0)
                return false;

            return true;
        }

        static string[] vsechny_soucty(int cislo)
        {
            string[] soucty = {"1+1+1+1+1"};

            return soucty;
        }
    
    
    }
}