using System;
using System.Linq;


public class Boss
{
    public event Action Upgrade;
    public event Action<int> TurnOn;

    public void TriggerUpgrade()
    {
        Upgrade?.Invoke();
    }

    public void TriggerTurnOn(int voltage)
    {
        TurnOn?.Invoke(voltage);
    }
}

public abstract class Device
{
    public string Name { get; set; }

    public Device(string name)
    {
        Name = name;
    }

    public abstract void ReactToUpgrade();

    public abstract void ReactToTurnOn(int voltage);
}

public class Laptop : Device
{
    public Laptop(string name) : base(name)
    {
    }

    public override void ReactToUpgrade()
    {
        Console.WriteLine($"{Name} получил обновление. Теперь он работает быстрее.");
    }

    public override void ReactToTurnOn(int voltage)
    {
        if (voltage < 100)
        {
            Console.WriteLine($"{Name} не может включиться из-за низкого напряжения.");
        }
        else
        {
            Console.WriteLine($"{Name} успешно включился.");
        }
    }
}

public class Robot : Device
{
    public Robot(string name) : base(name)
    {
    }

    public override void ReactToUpgrade()
    {
        Console.WriteLine($"{Name} обновил свое программное обеспечение. Появились новые возможности.");
    }

    public override void ReactToTurnOn(int voltage)
    {
        if (voltage > 200)
        {
            Console.WriteLine($"{Name} перегрузился и сломался.");
        }
        else
        {
            Console.WriteLine($"{Name} включился.");
        }
    }
}


class StringProcessor
{
    public static void ProcessString(string input)
    {
        Console.WriteLine("Исходная строка: " + input);
    }

    public static void RemovePunctuation(string input)
    {
        string result = new string(input.ToCharArray()
            .Where(c => !char.IsPunctuation(c))
            .ToArray());

        Console.WriteLine("Строка после удаления знаков препинания: " + result);
    }

    public static void AddSymbols(string input)
    {
        string result = input + "!!!";

        Console.WriteLine("Строка после добавления символов: " + result);
    }

    public static void ConvertToUpper(string input)
    {
        string result = input.ToUpper();

        Console.WriteLine("Строка после замены на заглавные буквы: " + result);
    }

    public static void RemoveExtraSpaces(string input)
    {
        string result = string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        Console.WriteLine("Строка после удаления лишних пробелов: " + result);
    }
    public static void ReplaceSubstring(string input)
    {
        string result =  input.Replace("строки", "Кирилл");
        Console.WriteLine("Строка после замены слов: " + result);
    }
}



public class Program
{
    static void Main()
    {
        Boss boss = new Boss();

        Laptop laptop1 = new Laptop("Ноутбук 1");
        laptop1.ReactToUpgrade();

        Robot robot1 = new Robot("Робот 1");
        robot1.ReactToTurnOn(150);

        Laptop laptop2 = new Laptop("Ноутбук 2");
        Robot robot2 = new Robot("Робот 2");

        boss.Upgrade += () => laptop1.ReactToUpgrade();
        boss.Upgrade += () => robot1.ReactToUpgrade();
        boss.Upgrade += () => Console.WriteLine("Универсальное устройство получило обновление.");

        boss.TurnOn += (voltage) => laptop2.ReactToTurnOn(voltage);
        boss.TurnOn += (voltage) => robot2.ReactToTurnOn(voltage);

        boss.TriggerUpgrade();
        boss.TriggerTurnOn(120);
        boss.TriggerTurnOn(250);

        Console.WriteLine();
        Console.WriteLine("------------------Задание 2--------------------");
        Console.WriteLine();

        string input = "Пример строки для обработки.";

        Action<string> stringProcessing = StringProcessor.ProcessString;

        stringProcessing += StringProcessor.RemovePunctuation;
        stringProcessing += StringProcessor.AddSymbols;
        stringProcessing += StringProcessor.ConvertToUpper;
        stringProcessing += StringProcessor.RemoveExtraSpaces;
        stringProcessing += StringProcessor.ReplaceSubstring;

        stringProcessing(input);
    }
}