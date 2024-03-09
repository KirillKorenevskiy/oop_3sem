using System;
using System.Collections.Generic;
using Newtonsoft.Json;


public interface ICollectionType<T>
{
    void Add(T element);
    void Remove(T element);
    void Display();
    T Find(Predicate<T> predicate);
}

public class Set<T> : ICollectionType<T> where T : IComparable<T>
{
    private List<T> elements;

    public Set()
    {
        elements = new List<T>();
    }

    public Set(IEnumerable<T> collection)
    {
        elements = new List<T>(collection);
    }

    public void Add(T element)
    {
        if (!elements.Contains(element))
        {
            elements.Add(element);
        }
    }

    public void Remove(T element)
    {
        elements.Remove(element);
    }

    public void Display()
    {
        foreach (T element in elements)
        {
            if (element is Examination examination)
            {
                examination.Display();
            }
            else
            {
                Console.WriteLine(element);
            }
        }
    }

    public T Find(Predicate<T> predicate)
    {
        try
        {
            return elements.Find(predicate);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при поиске элемента: " + ex.Message);
            return default(T);
        }
        finally
        {
            Console.WriteLine("Блок finally");
        }
    }

    public int Length()
    {
        return elements.Count;
    }

    public override string ToString()
    {
        return string.Join(", ", elements);
    }


    public void SaveToJsonFile(string filePath)
    {
        string json = JsonConvert.SerializeObject(elements, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void LoadFromJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            elements = JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}

public class Examination : IComparable<Examination>
{
    public string Subject { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }

    public Examination(string subject, DateTime date, string location)
    {
        Subject = subject;
        Date = date;
        Location = location;
    }

    public void Display()
    {
        Console.WriteLine($"Предмет: {Subject}, Дата: {Date}, Место: {Location}");
    }

    public int CompareTo(Examination other)
    {
        return Date.CompareTo(other.Date);
    }
}


class Program
{
    static void Main()
    {
        Set<int> intSet = new Set<int>();
        intSet.Add(1);
        intSet.Add(2);
        intSet.Add(3);
        intSet.Display();

        Set<double> doubleSet = new Set<double>();
        doubleSet.Add(1.5);
        doubleSet.Add(2.5);
        doubleSet.Add(3.5);
        doubleSet.Display();

        Set<string> stringSet = new Set<string>();
        stringSet.Add("Я");
        stringSet.Add("Кирилл");
        stringSet.Display();

        int foundInt = intSet.Find(x => x == 2);
        Console.WriteLine("Найден int: " + foundInt);

        double foundDouble = doubleSet.Find(x => x > 2.0);
        Console.WriteLine("Найден double: " + foundDouble);

        string foundString = stringSet.Find(x => x.StartsWith("W"));
        Console.WriteLine("Найден string: " + foundString);

        Console.WriteLine();    

        Set<Examination> examinationSet = new Set<Examination>();
        examinationSet.Add(new Examination("Математика", new DateTime(2023, 10, 1), "Аудитория 101"));
        examinationSet.Add(new Examination("Физика", new DateTime(2023, 10, 5), "Аудитория 202"));
        examinationSet.Display();


        Set<Examination> collection = new Set<Examination>();
        string filePath = "data.json";
        collection.SaveToJsonFile(filePath);
        collection.LoadFromJsonFile(filePath);
    }
}