using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace stromecek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node<string> node1 = new Node<string>(1, "kekw");
            Node<string> node2 = new Node<string>(2, "sussy");
            Node<string> node3 = new Node<string>(3, "lol");
            Node<string> node4 = new Node<string>(4, "kkt");
            Node<string> node0 = new Node<string>(0, "flejk");

            node3.LeftSon = node1;
            node1.RightSon = node3;
            node1.LeftSon = node0;
            node3.LeftSon = node2;
            node3.RightSon = node4;

            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            tree.Root = node1;

            Console.WriteLine(tree.Show());

            Console.WriteLine(tree.Fajnd(1).Value);

            Console.ReadLine();
        }

    }

    class Node<T>
    {
        public int Key { get; set; }

        public T Value { get; set; }


        public Node<T> LeftSon { get; set; }

        public Node<T> RightSon { get; set; }

        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
    }

    class BinarySearchTree<T>
    {
        public Node<T> Root { get; set; }

        public string Show()
        {

            string Stringovnik = "";

            void _show(Node<T> node)
            {
                if (node == null)
                    return;

                _show(node.LeftSon);

                Stringovnik += node.Key.ToString() + " ";

                _show(node.RightSon);
            }

            if (Root == null)
                return "Strom je prazdny";
            _show(Root);

            return Stringovnik;
        }

        public Node<T> Fajnd(int key)
        {
            Node<T> _fajnd(Node<T> node, int key2)
            {
                if (node == null)
                    return null;

                if (node.Key == key2)
                    return node;

                if (key2 < node.Key)
                    return _fajnd(node.LeftSon, key2);
                else
                    return _fajnd(node.RightSon, key2);
            }

            return _fajnd(Root, key);
        }
    }
}