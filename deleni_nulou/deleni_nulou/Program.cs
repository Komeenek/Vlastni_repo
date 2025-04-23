using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deleni_nulou
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vyber si buď Prefix nebo Postfix (0 / 1)");
            string prepost = Console.ReadLine();
            
            Vyhodnocovani itk = new Vyhodnocovani();
            string vstup = Console.ReadLine();

            string[] realVstup = vstup.Split(' ');
            if (prepost == "0")
                Console.WriteLine(itk.VyhodnoceniPost(realVstup));
            else
                Console.WriteLine(itk.VyhodnoceniPre(realVstup));
            Console.ReadLine();
        }
    }

    class Vyhodnocovani
    {
        public float VyhodnoceniPost(string[] vstup)
        {
            Stack<float> cisla = new Stack<float>();


            for (int i = 0; i < vstup.Length; i++)
            {
                try
                {
                    if (float.TryParse(vstup[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                        cisla.Push(result);
                    else
                    {
                        switch (Convert.ToChar(vstup[i]))
                        {
                            case '+':
                                Single x = cisla.Pop();
                                Single y = cisla.Pop();
                                cisla.Push(y + x);
                                break;
                            case '-':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(y - x);
                                break;
                            case '*':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(y * x);
                                break;
                            case '/':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(y / x);
                                break;
                            default:
                                Console.WriteLine("Něco se pos*alo");
                                break;
                        }
                    }
                }

                catch 
                {
                    Console.WriteLine("Špatný vstup");
                }
            }

            return cisla.Pop();
        }

        public float VyhodnoceniPre(string[] vstup)
        {
            Stack<float> cisla = new Stack<float>();


            for (int i = vstup.Length - 1; i > -1; i--)
            {
                try
                {
                    if (float.TryParse(vstup[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                        cisla.Push(result);
                    else
                    {
                        switch (Convert.ToChar(vstup[i]))
                        {
                            case '+':
                                Single x = cisla.Pop();
                                Single y = cisla.Pop();
                                cisla.Push(y + x);
                                break;
                            case '-':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(x - y);
                                break;
                            case '*':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(y * x);
                                break;
                            case '/':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(x / y);
                                break;
                            default:
                                Console.WriteLine("Něco se pos*alo");
                                break;
                        }
                    }
                }

                catch
                {
                    Console.WriteLine("Špatný vstup");
                }
            }

            return cisla.Pop();
        }
    }
}
