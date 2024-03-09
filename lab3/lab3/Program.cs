using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    public interface ICollectionType<T>
    {
        void Add(T element);
        void Remove(T element);
        void Display();
    }

    public class Set
    {
        public List<int> elements;

        public Set()
        {
            elements = new List<int>();
        }

        public Set(IEnumerable<int> collection)
        {
            elements = new List<int>(collection);
        }

        public void Add(int element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        public int this[int index]
        {
            get 
            { 
                return elements[index]; 
            }
            set 
            { 
                elements[index] = value; 
            }
        }

        public static Set operator +(Set set1, Set set2)
        {
            Set result = new Set(set1.elements);
            foreach (int element in set2.elements)
            {
                result.Add(element);
            }
            return result;
        }

        public static bool operator <=(Set set1, Set set2)
        {
            foreach (int element in set1.elements)
            {
                if (!set2.elements.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator >=(Set set1, Set set2)
        {
            foreach (int element in set2.elements)
            {
                if (!set1.elements.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static implicit operator int(Set set)
        {
            return set.elements.Count;
        }

        public static Set operator ++(Set set)
        {
            Random random = new Random();
            int randomNumber = random.Next();
            set.Add(randomNumber);
            return set;
        }

        public int Length()
        {
            return elements.Count;
        }

        public override string ToString()
        {
            return string.Join(", ", elements);
        }


        public class Production
        {
            public int Id { get; set; }
            public string OrganizationName { get; set; }

            public Production(int id, string organizationName)
            {
                Id = id;
                OrganizationName = organizationName;
            }
        }


        public class Developer
        {
            public string FIO { get; set; }
            public int Id { get; set; }
            public string Department { get; set; }

            public Developer(string fIO, int id, string department)
            {
                FIO = fIO;
                Id = id;
                Department = department;
            }
        }
    }

    public static class Extensions
    {
        public static string Encrypt(this string str)
        {
            return "Encrypted: " + str;
        }

        public static bool IsOrdered(this Set set)
        {
            for (int i = 1; i < set.Length(); i++)
            {
                if (set[i] < set[i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }


    public static class StatisticOperation
    {
        public static int Sum(Set set)
        {
            int sum = 0;
            for (int i = 0; i < set.Length(); i++)
            {
                sum += set[i];
            }
            return sum;
        }

        public static int Difference(Set set)
        {
            if (set.Length() == 0)
            {
                throw new InvalidOperationException("сет пустой");
            }

            int min = set.elements.Min();
            int max = set.elements.Max();
            return max - min;
        }

        public static int Count(Set set)
        {
            return set.Length();
        }
    }


    public static class StatisticExtensions
    {
        public static int WordCount(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            string[] words = str.Split(' ');
            return words.Length;
        }

        public static int Sum(this Set set)
        {
            int sum = 0;
            for (int i = 0; i < set.Length(); i++)
            {
                sum += set[i];
            }
            return sum;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------Задание 1--------------");

            Set set1 = new Set(new int[] { 1, 2, 3 });
            Set set2 = new Set(new int[] { 3, 4, 5 });

            Console.WriteLine("Сет 1: " + string.Join(", ", set1));
            Console.WriteLine("Сет 2: " + string.Join(", ", set2));

            Set set3 = set1 + set2;
            Console.WriteLine("Сет 1 + Сет 2: " + set3);

            bool isSubset = set1 <= set2;
            Console.WriteLine("Сет 1 <= Сет 2: " + isSubset);

            bool isSuperset = set1 >= set2;
            Console.WriteLine("Сет 1 >= Сет 2: " + isSuperset);

            int setSize = (int)set1;
            Console.WriteLine("Размер первого сета: " + setSize);

            Set set4 = set1++;
            Console.WriteLine("Первый сет с рандомным элементом: " + set4);

            int element = set1[0];
            Console.WriteLine("Элемент на позиции 0 первого сета: " + element);

            // Методы расширения
            string encryptedString = "Я Кирилл".Encrypt();
            Console.WriteLine("Зашифрованная строка: " + encryptedString);

            Set orderedSet = new Set(new int[] { 1, 2, 3, 4 });
            bool isOrdered = orderedSet.IsOrdered();
            Console.WriteLine("Упорядочен ли сет? " + isOrdered);

            Console.WriteLine("---------------Задание 2---------------------");

            Set.Production production = new Set.Production(1, "КореневскийИнвест");
            Console.WriteLine("ID: " + production.Id);
            Console.WriteLine("Имя организации: " + production.OrganizationName);

            Console.WriteLine("------------------Задание 3--------------");

            Set.Developer dev1 = new Set.Developer("Кореневский Кирилл Русланович", 1, "FlutterDepartment");
            Console.WriteLine("ФИО: " + dev1.FIO);
            Console.WriteLine("ID: " + dev1.Id);
            Console.WriteLine("Отдел: " + dev1.Department);

            Console.WriteLine("------------------Задание 4--------------");

            Set sett = new Set(new int[] { 118, 34, 1, 15, 9 });

            int sum = StatisticOperation.Sum(sett);
            int difference = StatisticOperation.Difference(sett);
            int count = StatisticOperation.Count(sett);

            Console.WriteLine("Сумма: " + sum);
            Console.WriteLine("Разница: " + difference);
            Console.WriteLine("Количество: " + count);

            Console.WriteLine("------------------Задание 5--------------");

            string text = "Привет меня зовут Кирилл ";
            int wordCount = text.WordCount();
            Console.WriteLine("Word Count: " + wordCount);

            Set set = new Set(new int[] {5, 10, 4, 19});
            int summa = set.Sum();
            Console.WriteLine("Сумма: " + summa);

        }
    }
}