using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

class Software : IList<string>
{
    private List<string> softwareList;

    public Software()
    {
        softwareList = new List<string>();
    }

    public string this[int index]
    {
        get
        {
            return softwareList[index];
        }
        set
        {
            softwareList[index] = value;
        }
    }

    public int Count
    {
        get { return softwareList.Count; }
    }

    public bool IsReadOnly => false;

    public void Add(string item)
    {
        softwareList.Add(item);
    }

    public void Clear()
    {
        softwareList.Clear();
    }

    public bool Contains(string item)
    {
        return softwareList.Contains(item);
    }

    public void CopyTo(string[] array, int arrayIndex)
    {
        softwareList.CopyTo(array, arrayIndex);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return softwareList.GetEnumerator();
    }

    public int IndexOf(string item)
    {
        return softwareList.IndexOf(item);
    }

    public void Insert(int index, string item)
    {
        softwareList.Insert(index, item);
    }

    public bool Remove(string item)
    {
        return softwareList.Remove(item);
    }

    public void RemoveAt(int index)
    {
        softwareList.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return softwareList.GetEnumerator();
    }

    public void DisplaySoftwareList()
    {
        Console.WriteLine("Список программного обеспечения:");
        foreach (string software in softwareList)
        {
            Console.WriteLine(software);
        }
    }
}

class Program
{
    static void Main()
    {
        SortedList<int, Software> softwareCollection = new SortedList<int, Software>();

        Software software1 = new Software();
        software1.Add("Microsoft Word");
        software1.Add("Photoshop");
        software1.Add("Visual Studio");

        Software software2 = new Software();
        software2.Add("Google Chrome");
        software2.Add("Mozilla Firefox");

        softwareCollection.Add(1, software1);
        softwareCollection.Add(2, software2);

        softwareCollection[1].Add("AutoCAD");
        softwareCollection[2].Remove("Google Chrome");

        foreach (KeyValuePair<int, Software> item in softwareCollection)
        {
            Console.WriteLine("Ключ: " + item.Key);
            item.Value.DisplaySoftwareList();
            Console.WriteLine();
        }


        Console.WriteLine("------------------Задание 2--------------------");


        SortedList<int, char> collection1 = new SortedList<int, char>();
        collection1.Add(1, 'A');
        collection1.Add(2, 'B');
        collection1.Add(3, 'C');
        collection1.Add(4, 'D');
        collection1.Add(5, 'E');

        Console.WriteLine("Первая коллекция:");
        foreach (KeyValuePair<int, char> item in collection1)
        {
            Console.WriteLine($"Ключ: {item.Key}, Значение: {item.Value}");
        }
        Console.WriteLine();

        int n = 2;
        for (int i = 0; i < n; i++)
        {
            collection1.RemoveAt(0);
        }

        collection1.Add(6, 'F');
        collection1[7] = 'G';

        Dictionary<int, char> collection2 = new Dictionary<int, char>(collection1);

        Console.WriteLine("Вторая коллекция:");
        foreach (KeyValuePair<int, char> item in collection2)
        {
            Console.WriteLine($"Ключ: {item.Key}, Значение: {item.Value}");
        }
        Console.WriteLine();

        char searchValue = 'D';
        bool found = collection2.ContainsValue(searchValue);
        if (found)
        {
            Console.WriteLine($"Значение '{searchValue}' найдено во второй коллекции.");
        }
        else
        {
            Console.WriteLine($"Значение '{searchValue}' не найдено во второй коллекции.");
        }

        Console.WriteLine("------------------Задание 3--------------------");

        ObservableCollection<string> softwareCollection1 = new ObservableCollection<string>();

        softwareCollection1.CollectionChanged += SoftwareCollectionChanged;

        softwareCollection1.Add("Software 1");
        softwareCollection1.Add("Software 2");
        softwareCollection1.Add("Software 3");

        softwareCollection1.Remove("Software 2");

        DisplaySoftwareList(softwareCollection1);
    }

    static void SoftwareCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            Console.WriteLine("Добавлен элемент: " + e.NewItems[0]);
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            Console.WriteLine("Удален элемент: " + e.OldItems[0]);
        }
    }

    static void DisplaySoftwareList(ObservableCollection<string> softwareCollection)
    {
        Console.WriteLine("Список программного обеспечения:");
        foreach (string software in softwareCollection)
        {
            Console.WriteLine(software);
        }
    }
}
