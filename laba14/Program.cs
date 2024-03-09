using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.Loader;


class CustomAssemblyLoadContext : AssemblyLoadContext
{
    protected override Assembly Load(AssemblyName assemblyName)
    {
        return LoadFromAssemblyName(assemblyName);
    }
}

class Program
{
    static int counter = 0;

    static int currentNumber = 1;
    static int n = 10;
    static object locker = new object();

    static void Main()
    {
        Console.WriteLine("--------------------------Задание 1-------------------------");

        try
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    Console.WriteLine($"ID: {process.Id}");
                    Console.WriteLine($"Имя: {process.ProcessName}");
                    Console.WriteLine($"Приоритет: {process.BasePriority}");
                    Console.WriteLine($"Время запуска: {process.StartTime}");
                    Console.WriteLine($"Текущее состояние: {process.Responding}");
                    Console.WriteLine($"Общее время использования процессора: {process.TotalProcessorTime}");
                    Console.WriteLine();
                }
                catch (System.ComponentModel.Win32Exception)
                {
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("--------------------------Задание 2------------------------");

        var currentDomain = AppDomain.CurrentDomain;
        Console.WriteLine($"Current domain: {currentDomain.FriendlyName}");
        Console.WriteLine("Детали: " + currentDomain.SetupInformation);
        var assemblies = currentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            Console.WriteLine("Assembly: " + assembly.FullName);
        }

        try
        {
            var newDomain = AppDomain.CreateDomain("NewDomain");
            newDomain.Load("System.Data");
            AppDomain.Unload(newDomain);
        }
        catch (Exception ex)
        {
            Console.WriteLine("устаревшие методы");
        }



        Console.WriteLine("---------------------------------Задание 3---------------------------");

        Console.WriteLine("Введите число:");
        int n = int.Parse(Console.ReadLine());

        using (StreamWriter file1 = new StreamWriter("prime_numbers.txt"))
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i <= n; i++)
                {
                    if (IsPrime(i))
                    {
                        Console.WriteLine(i);
                        file1.WriteLine(i);
                    }
                    Thread.Sleep(1000);
                }
            });

            thread.Start();
            thread.Join();
            Console.WriteLine($"Состояние потока: {thread.ThreadState}");
            Console.WriteLine($"поток ID: {thread.ManagedThreadId}");
        }


        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }




        Console.WriteLine("-----------------------Задание 4--------------------");

        StreamWriter file = File.AppendText("numbers.txt");

        Thread evenThread = new Thread(() =>
        {
            for (int i = 2; i <= n; i += 2)
            {
                lock (locker)
                {
                    while (currentNumber != i) Monitor.Wait(locker);
                    Console.WriteLine(i);
                    file.WriteLine(i);
                    currentNumber++;
                    Monitor.PulseAll(locker);
                }
                Thread.Sleep(1000);
            }
        });
        evenThread.Priority = ThreadPriority.Lowest;
        evenThread.Start();

        Thread oddThread = new Thread(() =>
        {
            for (int i = 1; i <= n; i += 2)
            {
                lock (locker)
                {
                    while (currentNumber != i) Monitor.Wait(locker);
                    Console.WriteLine(i);
                    file.WriteLine(i);
                    currentNumber++;
                    Monitor.PulseAll(locker);
                }
                Thread.Sleep(500);
            }
        });
        oddThread.Start();
        evenThread.Join();
        oddThread.Join();
        file.Close();


        Console.WriteLine("-----------------------Задание 5--------------------");

        Timer timer = new Timer(DoTask, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        Console.WriteLine("Повторяющаяся задача запущена.");
        Console.WriteLine("Нажмите любую клавишу для остановки задачи.");
        Console.ReadKey();
        timer.Dispose();
        Console.WriteLine("Повторяющаяся задача остановлена.");
        Console.ReadKey();

        static void DoTask(object state)
        {
            counter++;
            Console.WriteLine($"Выполняется повторяющаяся задача. Счетчик: {counter}");
        }
    }
}
