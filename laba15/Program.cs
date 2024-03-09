using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{

    static BlockingCollection<string> warehouse = new BlockingCollection<string>();

    static void Main()
    {

        Console.WriteLine("----------------------задание 1------------------------");

        int n = 1000000;

        Task<bool[]> primeTask = Task.Run(() => FindPrimes(n));
        Console.WriteLine("Идентификатор текущей задачи: " + Task.CurrentId);
        Console.WriteLine("Статус задачи: " + primeTask.Status);
        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 3; i++)
        {
            primeTask.Wait();
            bool[] primes = primeTask.Result;
            Console.WriteLine("Количество простых чисел: " + CountPrimes(primes));
            primeTask = Task.Run(() => FindPrimes(n));
        }

        stopwatch.Stop();
        Console.WriteLine("Время выполнения: " + stopwatch.Elapsed);
        Console.WriteLine("Основной поток завершен.");

        static bool[] FindPrimes(int n)
        {
            bool[] primes = new bool[n + 1];

            for (int i = 2; i <= n; i++)
            {
                primes[i] = true;
            }

            for (int p = 2; p * p <= n; p++)
            {
                if (primes[p] == true)
                {
                    for (int i = p * p; i <= n; i += p)
                    {
                        primes[i] = false;
                    }
                }
            }

            return primes;
        }

        static int CountPrimes(bool[] primes)
        {
            int count = 0;

            for (int i = 2; i < primes.Length; i++)
            {
                if (primes[i])
                {
                    count++;
                }
            }

            return count;
        }

        Console.WriteLine("----------------------задание 2------------------------");

        /*CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        Task<bool[]> primeTask1 = Task.Run(() => FindPrimes1(n, cancellationToken));
        Console.WriteLine("Идентификатор текущей задачи: " + Task.CurrentId);
        Console.WriteLine("Статус задачи: " + primeTask.Status);
        Stopwatch stopwatch1 = Stopwatch.StartNew();

        for (int i = 0; i < 3; i++)
        {
            primeTask1.Wait();

            if (primeTask.IsCompleted)
            {
                bool[] primes = primeTask1.Result;
                Console.WriteLine("Количество простых чисел: " + CountPrimes(primes));
            }
            else if (primeTask1.IsCanceled)
            {
                Console.WriteLine("Задача отменена.");
            }
            else if (primeTask1.IsFaulted)
            {
                Console.WriteLine("Задача завершилась с ошибкой.");
                Console.WriteLine("Ошибка: " + primeTask1.Exception.InnerException.Message);
            }

            primeTask1 = Task.Run(() => FindPrimes1(n, cancellationToken));

            if (i == 1)
            {
                cancellationTokenSource.Cancel();
            }
        }

        stopwatch1.Stop();
        Console.WriteLine("Время выполнения: " + stopwatch1.Elapsed);

        Console.WriteLine("Основной поток завершен.");


        static bool[] FindPrimes1(int n, CancellationToken cancellationToken)
        {
            bool[] primes = new bool[n + 1];

            for (int i = 2; i <= n; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Задача отменена во время выполнения.");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                primes[i] = true;
            }

            for (int p = 2; p * p <= n; p++)
            {
                if (primes[p] == true)
                {
                    for (int i = p * p; i <= n; i += p)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            Console.WriteLine("Задача отменена во время выполнения.");
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        primes[i] = false;
                    }
                }
            }

            return primes;
        }

        static int CountPrimes1(bool[] primes)
        {
            int count = 0;

            for (int i = 2; i < primes.Length; i++)
            {
                if (primes[i])
                {
                    count++;
                }
            }

            return count;
        }*/

        Console.WriteLine("----------------------задание 3------------------------");

        Task<double> task1 = Task.Run(() => CalculateSquare(5.0));
        Task<double> task2 = Task.Run(() => CalculateCube(3.0));
        Task<double> task3 = Task.Run(() => CalculateSquareRoot(16.0));

        Task<double> task4 = Task.Run(() =>
        {
            double result1 = task1.Result;
            double result2 = task2.Result;
            double result3 = task3.Result;

            return result1 + result2 + result3;
        });

        double finalResult = task4.Result;
        Console.WriteLine("Результат: " + finalResult);

        Console.WriteLine("Основной поток завершен.");

        static double CalculateSquare(double num)
        {
            return num * num;
        }

        static double CalculateCube(double num)
        {
            return num * num * num;
        }

        static double CalculateSquareRoot(double num)
        {
            return Math.Sqrt(num);
        }

        Console.WriteLine("----------------------задание 4------------------------");

        Task<int> task5 = Task.Run(() => CalculateSum(5, 3));
        Task<int> task6 = Task.Run(() => CalculateProduct(2, 4));

        Task<double> continuationTask5 = Task.WhenAll(task5, task6)
            .ContinueWith((tasks) =>
            {
                int sum = tasks.Result[0];
                int product = tasks.Result[1];

                return CalculateAverage(sum, product);
            });

        Task<double> continuationTask6 = Task.Run(async () =>
        {
            int sum = await task5;
            int product = await task6;

            return CalculateAverage(sum, product);
        });

        double result1 = continuationTask5.Result;
        double result2 = continuationTask6.GetAwaiter().GetResult();

        Console.WriteLine("Результат 1: " + result1);
        Console.WriteLine("Результат 2: " + result2);

        Console.WriteLine("Основной поток завершен.");

        static int CalculateSum(int a, int b)
        {
            return a + b;
        }

        static int CalculateProduct(int a, int b)
        {
            return a * b;
        }

        static double CalculateAverage(int sum, int product)
        {
            return (sum + product) / 2.0;
        }


        Console.WriteLine("----------------------задание 5------------------------");


        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Parallel.For(0, numbers.Length, i =>
        {
            numbers[i] *= 2;
        });
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }


        const int arraySize = 1000000;
        const int numArrays = 3;
        int[][] arrays = new int[numArrays][];
        Parallel.For(0, numArrays, i =>
        {
            arrays[i] = new int[arraySize];
            for (int j = 0; j < arraySize; j++)
            {
                arrays[i][j] = i * arraySize + j;
            }
        });


        int[] numberss = { 5, 8, 2, 1, 10, 3, 6, 4, 7, 9 };
        var sortedNumbers = numberss.AsParallel().OrderBy(num => num).ToArray();
        foreach (int num in sortedNumbers)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine("Основной поток завершен.");



        string text = "Hello, world!";
        string[] words = { "Hello", "world" };
        Parallel.ForEach(words, word =>
        {
            text = text.Replace(word, "");
        });
        Console.WriteLine(text);
        Console.WriteLine("Основной поток завершен.");


        Console.WriteLine("----------------------задание 6------------------------");

        Parallel.Invoke(
           () =>
           {
               Console.WriteLine("Задача 1 выполняется");
           },
           () =>
           {
               Console.WriteLine("Задача 2 выполняется");
           },
           () =>
           {
               Console.WriteLine("Задача 3 выполняется");
           }
        );
        Console.WriteLine("Основной поток завершен.");


        Console.WriteLine("----------------------задание 7------------------------");

        Task[] suppliers = new Task[5];
        for (int i = 0; i < suppliers.Length; i++)
        {
            int supplierId = i + 1;
            suppliers[i] = Task.Run(() => SupplierTask(supplierId, 3));
        }

        Task[] customers = new Task[10];
        for (int i = 0; i < customers.Length; i++)
        {
            int customerId = i + 1;
            customers[i] = Task.Run(() => CustomerTask(customerId));
        }

        Task.WaitAll(suppliers);
        warehouse.CompleteAdding();
        Task.WaitAll(customers);

        Console.WriteLine("Основной поток завершен.");


        static void SupplierTask(int supplierId, int maxProducts)
        {
            Random random = new Random();
            int productsDelivered = 0;

            while (productsDelivered < maxProducts)
            {
                Thread.Sleep(random.Next(1000, 3000));

                string product = $"Товар от поставщика {supplierId}";
                warehouse.Add(product);
                Console.WriteLine($"Завезен на склад: {product}");
                PrintWarehouseItems();

                productsDelivered++;
            }
        }

        static void CustomerTask(int customerId)
        {
            Random random = new Random();

            while (!warehouse.IsCompleted)
            {
                string product = null;
                if (warehouse.TryTake(out product))
                {
                    Console.WriteLine($"Покупатель {customerId} приобрел товар: {product}");
                    PrintWarehouseItems();
                }
                else
                {
                    Console.WriteLine($"Покупатель {customerId} ушел, товара нет.");
                }
                Thread.Sleep(random.Next(500, 2000));
            }
        }

        static void PrintWarehouseItems()
        {
            Console.WriteLine("Товары на складе:");
            foreach (string product in warehouse)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
        }


        static async Task DoAsyncTask()
        {
            Console.WriteLine("Начало асинхронной задачи.");
            await Task.Delay(2000);
            Console.WriteLine("Асинхронная задача выполнена.");
            await AnotherAsyncTask();
            Console.WriteLine("Все асинхронные задачи выполнены.");
        }

        static async Task AnotherAsyncTask()
        {
            Console.WriteLine("Начало еще одной асинхронной задачи.");
            await Task.Delay(1000);
            Console.WriteLine("Еще одна асинхронная задача выполнена.");
        }
    }
}

