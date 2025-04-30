using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stromecek_s_vyrazy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string vstup = Console.ReadLine();

            string[] realVstup = vstup.Split(' ');

            Vyhodnocovani itk = new Vyhodnocovani();

            VyrazovyStrom<string> vyrazovyStrom = new VyrazovyStrom<string>();

            vyrazovyStrom.Vytvor(realVstup);

            Console.WriteLine(vyrazovyStrom.Show());

            Console.WriteLine(itk.VyhodnoceniPost(realVstup));

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

    }

    class Node<T>
    {

        public string Value { get; set; }

        public Node<T> LeftSon { get; set; }

        public Node<T> RightSon { get; set; }

    }

    class VyrazovyStrom<T>
    {
        public Node<T> Root { get; set; }

        public void Vytvor(string[] vstup)
        {
            Stack<Node<T>> ZasobnikNodu = new Stack<Node<T>>();

            for (int i = 0; i < vstup.Length; i++)
            {
                string prvek = vstup[i];

                Node<T> uzlik = new Node<T>();
                uzlik.Value = prvek;
                try
                {
                    if (!float.TryParse(prvek, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                    {
                        uzlik.RightSon = ZasobnikNodu.Pop();
                        uzlik.LeftSon = ZasobnikNodu.Pop();
                    }
                    ZasobnikNodu.Push(uzlik);
                }

                catch
                {
                    Console.WriteLine("spatne!");
                }
            }

            Root = ZasobnikNodu.Pop();
        }

        public string Show()
        {

            string Stringovnik = "";

            void _show(Node<T> node)
            {
                if (node == null)
                    return;

                Stringovnik += "(";

                _show(node.LeftSon);

                Stringovnik += node.Value + " ";

                _show(node.RightSon);

                Stringovnik += ")";
            }

            if (Root == null)
                return "Strom je prazdny";
            _show(Root);

            return Stringovnik;
        }

    }
}

