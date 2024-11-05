// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            LinkedList druhyList = new LinkedList();
            druhyList.Add(1);
            druhyList.Add(2);
            druhyList.Add(3);
            druhyList.Add(7);
            druhyList.Add(5);
            druhyList.Add(6);
            linkedList.Add(8);
            linkedList.Add(9);
            linkedList.Add(7);
            linkedList.Add(8);
            linkedList.Add(9);
            linkedList.Add(7);
            //linkedList = linkedList.DestruktivniPrunik(druhyList);
            linkedList.DestruktivniSjednoceni(druhyList);
            linkedList.PrintLinkedList();
            Console.ReadLine();
        }
    }

    class Node
    {
        public Node(int value) // konstruktor
        {
            Value = value;
        }
        public int Value { get; set; }
        public Node Next { get; set; }
    }
    class LinkedList
    {
        public Node Head { get; set; }

        public void Add(int value) // pridat prvek do seznamu, složitost O(1)
        {
            if (Head == null) // když je prázdný
            {
                Head = new Node(value);
            }
            else
            {
                Node newNode = new Node(value);
                newNode.Next = Head;
                Head = newNode;

            }
        }
        public bool Find(int value) // složitost O(n)
        {

            Node node = Head;
            while (node != null)
            {
                if (node.Value == value)
                    return true;
                node = node.Next;
            }
            return false;
        }

        public int FindMinimum() // složitost O(n)
        {
            Node node = Head;

            if (Head == null) // když je prázdný
            {
                Console.WriteLine("Seznam je prázdný");
                return 0;
            }

            int minimum = Head.Value;

            while (node != null)
            {
                if (node.Value < minimum)
                    minimum = node.Value;
                node = node.Next;
            }
            return minimum;
        }

        public void PrintLinkedList()
        {
            Node node = Head;

            if (Head == null) // když je prázdný
            {
                Console.WriteLine("Seznam je prázdný");
            }

            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }

        public void SortLinkedList()
        {
            Node node = Head;

            if (Head == null) // když je prázdný
            {
                Console.WriteLine("Seznam je prázdný");
            }
            else
            {
                bool vysorteno = false;
                while (vysorteno == false)
                {
                    node = Head;
                    vysorteno = true;
                    while (node.Next != null)
                    {
                        if (node.Value > node.Next.Value)
                        {
                            int buffer = node.Value;
                            node.Value = node.Next.Value;
                            node.Next.Value = buffer;
                            vysorteno = false;
                        }
                        node = node.Next;

                    }
                }
            }
        }

        public LinkedList DestruktivniPrunik(LinkedList druhyList)
        {

            Node node = Head;

            if (Head == null) // když je prázdný
            {
                Console.WriteLine("Seznam je prázdný");
                return new LinkedList();
            }

            if (druhyList.Find(Head.Value) == false)
            {
                Head = Head.Next;
            }

            while (node.Next != null)
            {
                if (druhyList.Find(node.Next.Value) == false)
                {
                    node.Next = node.Next.Next;
                }
                node = node.Next;
            }
            return OdstraneniDuplikatu();
        }

        public LinkedList OdstraneniDuplikatu()
        {
            Node node = Head;

            if (node == null)
            {
                Console.WriteLine("seznam je prázdný");
                return new LinkedList();
            }
            LinkedList listHodnot = new LinkedList();
            listHodnot.Add(node.Value);
            
            while (node.Next != null)
            {

                if (listHodnot.Find(node.Next.Value) == false)
                {
                    listHodnot.Add(node.Next.Value);
                }
                
                node = node.Next;
            }
            return listHodnot;
            
        }

        public void DestruktivniSjednoceni(LinkedList druhyList)
        {
            Node node = Head;

            if (Head == null)
            {
                Head = druhyList.Head;
                return;
            }

            while (node.Next != null)
            {
                node = node.Next;
            }

            node.Next = druhyList.Head;
            return;
        }
    }

}

