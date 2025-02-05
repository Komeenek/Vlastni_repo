using System;
using System.Collections.Generic;
using System.IO;
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
            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Inzert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            Student novacek = new Student(169, "Komeen", "Neplatil", 69, "2.C");

            tree.Inzert(novacek.Id, novacek);

            Console.WriteLine(tree.Fajnd(169).Value);


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

        public T FajndMin()
        {
            T _fajndMin(Node<T> node)
            {
                if (node == null)
                    return default;
                if (node.LeftSon == null)
                    return node.Value;
                return _fajndMin(node.LeftSon);
            }

            return _fajndMin(Root);
        }

        public void Inzert(int newKey, T newValue)
        {
            Node<T> node = new Node<T>(newKey, newValue);

            if (node == null) return;

            if (Root == null)
                Root = node;

            void _inzert(Node<T> nodik, Node<T> rootik)
            {
                if (nodik == null)
                    return;

                if (node.Key < rootik.Key)
                {
                    if (rootik.LeftSon == null)
                    {
                        rootik.LeftSon = nodik;
                        return;
                    }

                    _inzert(nodik, rootik.LeftSon);
                }

                if (node.Key > rootik.Key)
                {
                    if (rootik.RightSon == null)
                    {
                        rootik.RightSon = nodik;
                        return;
                    }
                    _inzert(nodik, rootik.RightSon);
                }

                if (node.Key == rootik.Key)
                    return;
            }

            _inzert(node, Root);

            return;

        }
    }
    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}