// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            Console.WriteLine(linkedList.FindMinimum());
            Console.ReadLine();
        }
    }

    class Node
    {
        public Node(int value) // konstruktor
        {
            Value = value;
        }
        public int Value { get; }
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
    }


}
