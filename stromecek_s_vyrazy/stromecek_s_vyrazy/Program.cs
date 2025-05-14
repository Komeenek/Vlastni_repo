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

            if (realVstup[0] == "+" || realVstup[0] == "-" || realVstup[0] == "*" || realVstup[0] == "/")
            {
                Console.WriteLine(itk.VyhodnoceniPre(realVstup));

                Console.WriteLine("Zadal jsi prefix, takže strom nebude");
            }
            else
            {
                vyrazovyStrom.Vytvor(realVstup);

                Console.WriteLine("Infix: " + vyrazovyStrom.Show());

                Console.WriteLine("Prefix: " + vyrazovyStrom.ShowPre());

                Console.WriteLine("Postfix: " + vyrazovyStrom.ShowPost());

                Console.WriteLine(itk.VyhodnoceniPost(realVstup));
            }


            

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
                                if (x == 0)
                                    Console.WriteLine("Neděl nulou, klaune");
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


            for (int i = 0; i < vstup.Length; i++)
            {
                try
                {
                    if (float.TryParse(vstup[vstup.Length - 1 - i], NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                        cisla.Push(result);
                    else
                    {
                        switch (Convert.ToChar(vstup[vstup.Length - 1 - i]))
                        {
                            case '+':
                                Single x = cisla.Pop();
                                Single y = cisla.Pop();
                                cisla.Push(x + y);
                                break;
                            case '-':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(x - y);
                                break;
                            case '*':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                cisla.Push(x * y);
                                break;
                            case '/':
                                x = cisla.Pop();
                                y = cisla.Pop();
                                if (y == 0)
                                    Console.WriteLine("Neděl nulou, klaune");
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
                    Console.WriteLine("Asi málo operandů");
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
                    Console.WriteLine("Málo operandů");
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

                if (node.LeftSon != null || node.RightSon != null)
                    Stringovnik += "(";

                _show(node.LeftSon);

                Stringovnik += node.Value + " ";

                _show(node.RightSon);
                if (node.LeftSon != null || node.RightSon != null)
                {
                    Stringovnik = Stringovnik.Substring(0, Stringovnik.Length - 1);
                    Stringovnik += ") ";
                }
                    
            }

            if (Root == null)
                return "Strom je prazdny";
            _show(Root);

            return Stringovnik;
        }

        public string ShowPre()
        {

            string Stringovnik = "";

            void _show(Node<T> node)
            {
                if (node == null)
                    return;

                Stringovnik += node.Value + " ";

                _show(node.LeftSon);

                _show(node.RightSon);
            }

            if (Root == null)
                return "Strom je prazdny";
            _show(Root);

            return Stringovnik;
        }

        public string ShowPost()
        {

            string Stringovnik = "";

            void _show(Node<T> node)
            {
                if (node == null)
                    return;

                _show(node.LeftSon);

                _show(node.RightSon);

                Stringovnik += node.Value + " ";
            }

            if (Root == null)
                return "Strom je prazdny";
            _show(Root);

            return Stringovnik;
        }

    }
}

