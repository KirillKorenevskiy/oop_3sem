using System;
using System.Text;

namespace lab1
{
    class BooleanExample
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("--------------------------------------задание 1");

            bool val1 = true;
            sbyte val2 = 23;
            short val3 = -1109;
            int val4 = 51092;
            long val5 = -7091821871L;
            byte val6 = 62;
            ushort val7 = 42019;
            uint val8 = 1151092;
            ulong val9 = 17091821871L;
            float val10 = 43.27F;
            double val11 = -11092.53D;
            char val12 = 'x';
            decimal val13 = 53005.25M;
            nint val14 = -42;
            nuint val15 = 43;
            Console.WriteLine(val1);
            Console.WriteLine(val2);
            Console.WriteLine(val3);
            Console.WriteLine(val4);
            Console.WriteLine(val5);
            Console.WriteLine(val6);
            Console.WriteLine(val7);
            Console.WriteLine(val8);
            Console.WriteLine(val9);
            Console.WriteLine(val10);
            Console.WriteLine(val11);
            Console.WriteLine(val12);
            Console.WriteLine(val13);
            Console.WriteLine(val14);
            Console.WriteLine(val15);

            Console.WriteLine("----------------------------");


            // char -> int -> long -> float -> double - неявное
            // double -> float -> long -> int -> char - явное
            char ch = '1';
            int chToint = ch;
            Console.WriteLine(chToint);

            int i = 13128;
            long intTolong = i;
            Console.WriteLine(intTolong);

            long l = 24572874652;
            float longTofloat = l;
            Console.WriteLine(longTofloat);

            char ch2 = '5';
            long ch2Tolong = ch2;
            Console.WriteLine(ch2Tolong);

            int i2 = 4528;
            double intTodouble = i2;
            Console.WriteLine(intTodouble);

            double num = 5.23;
            int inum = (int)num;   
            Console.WriteLine(inum);

            int i3 = 10;
            double iTod = i3;
            Console.WriteLine(iTod);

            float fl = 1.25f;
            int con = Convert.ToInt32(fl);
            Console.WriteLine(con);


            Console.WriteLine("----------------------------");

            //упаковка
            int intt = 123;
            object o = intt;
            Console.WriteLine(o);
            //распаковка
            o = 123;
            i = (int)o;
            Console.WriteLine(i);


            Console.WriteLine("----------------------------");

            var a2 = 5;
            var a3 = 4.8;
            var result = a2 + a3;
            Console.WriteLine(result);


            Console.WriteLine("----------------------------");

            int? nullableInt = 10;
            if (nullableInt.HasValue)
            {
                int value = nullableInt.Value;
                Console.WriteLine("nullableInt: " + value);
            }
            else
            {
                Console.WriteLine("nullableInt = null");
            }


            //var variable = 10;
            //variable = "Привет!";

            Console.WriteLine("------------------------------------задание 2");

            string str1 = "jdfiusahiuhs";
            string str2 = "jdfiusaahfaofoasga";
            if(String.Compare(str1, str2)==0)
            {
                Console.WriteLine("Строки равны");
            }
            else
            {
                Console.WriteLine("Строки не равны");
            }

            Console.WriteLine("----------------------------");

            string str3 = "Привет, ";
            string str4 = "меня зовут Кирилл, ";
            string str5 = "я студент";
            string concan = str3 + str4 + str5;
            Console.WriteLine(concan);
            string cop = string.Copy(str5);
            Console.WriteLine(cop);
            string sub = str4.Substring(2, 6);
            Console.WriteLine(sub);
            string[] words = str3.Split('и');
            foreach(string word in words)
            {
                Console.WriteLine(word);
            }
            string ins = str4.Insert(19, "Русланович ");
            Console.WriteLine(ins);
            string rem = str4.Remove(5, 5);
            Console.WriteLine(rem);
            string name = "Кирилл";
            int age = 18;
            string greeting = $"Привет, я {name}! Мне {age} лет.";
            Console.WriteLine(greeting);

            Console.WriteLine("----------------------------");

            string str6 = "";
            string str7 = null;
            Console.WriteLine($"Пустая строка: {string.IsNullOrEmpty(str6)}");
            Console.WriteLine($"Null строка: {string.IsNullOrEmpty(str7)}");

            string combined = str6 + "Кирилл";
            Console.WriteLine($"строка: {combined}");
            Console.WriteLine($"Длина пустой строки: {str6.Length}");

            Console.WriteLine("----------------------------");

            StringBuilder sB = new StringBuilder(" Кирилл");
            sB.Remove(1, 3);
            Console.WriteLine(sB.ToString());
            sB.Insert(0, "Привет, я ");
            sB.Append("!");
            Console.WriteLine(sB.ToString());

            Console.WriteLine("--------------------------------------задание 3");

            int[,] mat = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($"{mat[row, col]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");

            string[] array = new string[] { "Кирилл ", "Максим ", "Никита", "Саша" };
            for (int elem = 0; elem < 4; elem++)
            {
                Console.Write($"{array[elem]} ");
            }
            int length = array.Length;
            Console.WriteLine($"\nдлина: {length}");
            Console.Write("позиция элемента: ");
            int position = Convert.ToInt32(Console.ReadLine());
            Console.Write("новое значение: ");
            string newValue = Console.ReadLine();
            if (position >= 0 && position < length)
            {
                array[position] = newValue;
            }
            else
            {
                Console.WriteLine("Введена недопустимая позиция элемента.");
            }
            for (int elem1 = 0; elem1 < 4; elem1++)
            {
                Console.Write($"{array[elem1]} ");
            }

            Console.WriteLine("----------------------------");

            double[][] arr2 = new double[3][];
            for (int a = 0; a < arr2.Length; a++)
            {
                Console.Write($"Введите {a + 1}-ю строку: ");
                string[] inputValues = Console.ReadLine().Split(' ');
                arr2[a] = new double[inputValues.Length];
                for (int j = 0; j < inputValues.Length; j++)
                {
                    double value;
                    if (double.TryParse(inputValues[j], out value))
                    {
                        arr2[a][j] = value;
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение. Введите число.");
                        j--;
                    }
                }
            }
            for (int a = 0; a < arr2.Length; a++)
            {
                for (int j = 0; j < arr2[a].Length; j++)
                {
                    Console.Write(arr2[a][j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");

            var arrayy = new[] { 1, 2, 3, 4, 5 };
            var inputString = "Пример строки";

            Console.WriteLine("--------------------------------------задание 4");
            var arr3 = (42, "Кирилл", 'A', "меня зовут Кирилл", 123456789UL);

            Console.WriteLine("----------------------------");

            Console.WriteLine(arr3);
            Console.WriteLine(arr3.Item1);
            Console.WriteLine(arr3.Item3);
            Console.WriteLine(arr3.Item5);

            Console.WriteLine("----------------------------");

            var (item1, item2, item3, item4, item5) = arr3;
            Console.WriteLine(item1);
            Console.WriteLine(item2); 
            Console.WriteLine(item3);
            Console.WriteLine(item4);
            Console.WriteLine(item5);

            var (_, _, _, item44, _) = arr3;
            Console.WriteLine(item44);
            
            int item11 = arr3.Item1;
            string item22 = arr3.Item2;
            char item33 = arr3.Item3;
            string item444 = arr3.Item4;
            ulong item55 = arr3.Item5;

            Console.WriteLine("----------------------------");

            var tup1 = (1, "Привет", 'A');
            var tup2 = (1, "Привет", 'A');
            var tup3 = (2, "Кирилл", 'B');
            bool areEqual1 = tup1 == tup2;
            bool areEqual2 = tup1 == tup3;
            Console.WriteLine(areEqual1);
            Console.WriteLine(areEqual2);
            bool areEqual3 = tup1.Equals(tup2);
            bool areEqual4 = tup1.Equals(tup3);
            Console.WriteLine(areEqual3);
            Console.WriteLine(areEqual4);

            Console.WriteLine("--------------------------------------задание 5");

            int[] numbers = { 5, 2, 9, 1, 7 };
            string text = "Привет, я Кирилл!";

            static (int max, int min, int sum, char firstLetter) ProcessArrayAndString(int[] array, string str)
            {
                int max = int.MinValue;
                int min = int.MaxValue;
                int sum = 0;
                char firstLetter = str[0];

                foreach (int num in array)
                {
                    if (num > max)
                        max = num;

                    if (num < min)
                        min = num;

                    sum += num;
                }

                return (max, min, sum, firstLetter);
            }

            var resultat = ProcessArrayAndString(numbers, text);

            Console.WriteLine($"Max: {resultat.max}");
            Console.WriteLine($"Min: {resultat.min}");
            Console.WriteLine($"Сумма: {resultat.sum}");
            Console.WriteLine($"1я буква: {resultat.firstLetter}");

            Console.WriteLine("--------------------------------------задание 6");

            void CheckedFunction()
            {
                checked
                {
                    int maxValue = int.MaxValue;
                    Console.WriteLine(maxValue);
                }
            }
            void UncheckedFunction()
            {
                unchecked
                {
                    int maxValue = int.MaxValue + 1;
                    Console.WriteLine(maxValue);
                }
            }
            CheckedFunction();
            UncheckedFunction();
        }
    }
}