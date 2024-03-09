using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace lab13
{

    public interface ISerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string serializedObj);
    }

    public class BinarySerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public T Deserialize<T>(string serializedObj)
        {
            var formatter = new BinaryFormatter();
            var bytes = Convert.FromBase64String(serializedObj);
            using (var stream = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public class SoapSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var xmlWriter = new XmlTextWriter(writer))
            {
                serializer.WriteObject(xmlWriter, obj);
                return writer.ToString();
            }
        }

        public T Deserialize<T>(string serializedObj)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serializedObj))
            using (var xmlReader = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(xmlReader);
            }
        }
    }

    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string serializedObj)
        {
            return JsonConvert.DeserializeObject<T>(serializedObj);
        }
    }

    public class XmlSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public T Deserialize<T>(string serializedObj)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var reader = new StringReader(serializedObj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }



    [Serializable]
    public class MyClass
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public MyClass(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public class ObjectSerialization
    {
        public void SerializeObjects(List<MyClass> objects, string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, objects);
            }
        }

        public List<MyClass> DeserializeObjects(string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (List<MyClass>)formatter.Deserialize(stream);
            }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("---------------------задание 1----------------------------");

            var test = new Test("Тест 1", "2023-11-12", 60, 10, "Тестовый тип", "Предмет", "Kirill");

            ISerializer binarySerializer = new BinarySerializer();
            string binarySerialized = binarySerializer.Serialize(test);
            Test binaryDeserialized = binarySerializer.Deserialize<Test>(binarySerialized);
            Console.WriteLine("Binary Serialization:");
            Console.WriteLine("Serialized: " + binarySerialized);
            Console.WriteLine();

            ISerializer soapSerializer = new SoapSerializer();
            string soapSerialized = soapSerializer.Serialize(test);
            Test soapDeserialized = soapSerializer.Deserialize<Test>(soapSerialized);
            Console.WriteLine("SOAP Serialization:");
            Console.WriteLine("Serialized: " + soapSerialized);
            Console.WriteLine();

            ISerializer jsonSerializer = new JsonSerializer();
            string jsonSerialized = jsonSerializer.Serialize(test);
            Test jsonDeserialized = jsonSerializer.Deserialize<Test>(jsonSerialized);
            Console.WriteLine("JSON Serialization:");
            Console.WriteLine("Serialized: " + jsonSerialized);
            Console.WriteLine();

            ISerializer xmlSerializer = new XmlSerializer();
            string xmlSerialized = xmlSerializer.Serialize(test);
            Test xmlDeserialized = xmlSerializer.Deserialize<Test>(xmlSerialized);
            Console.WriteLine("XML Serialization:");
            Console.WriteLine("Serialized: " + xmlSerialized);
            Console.WriteLine();


            Console.WriteLine("---------------------задание 2----------------------------");

            var objects = new List<MyClass>
            {
                new MyClass("Object 1", 20),
                new MyClass("Object 2", 30),
                new MyClass("Object 3", 40)
            };

            var serialization = new ObjectSerialization();

            string filePath = "objects.bin";
            serialization.SerializeObjects(objects, filePath);
            Console.WriteLine("Объекты serialized в файл");

            List<MyClass> deserializedObjects = serialization.DeserializeObjects(filePath);
            Console.WriteLine("Объекты deserialized из файла:");

            foreach (var obj in deserializedObjects)
            {
                Console.WriteLine($"Name: {obj.Name}, Age: {obj.Age}");
            }

            Console.WriteLine("---------------------задание 3----------------------------");

            string filePath1 = "D:\\3 сем\\oop\\lab13\\XMLFile1.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath1);

            XmlNodeList bookNodes = xmlDoc.SelectNodes("//Book");
            Console.WriteLine("Selector 1 - All Book elements:");
            foreach (XmlNode bookNode in bookNodes)
            {
                string title = bookNode.SelectSingleNode("Title").InnerText;
                string author = bookNode.SelectSingleNode("Author").InnerText;
                string year = bookNode.SelectSingleNode("Year").InnerText;

                Console.WriteLine($"Title: {title}, Author: {author}, Year: {year}");
            }
            Console.WriteLine();

            XmlNode firstBookTitleNode = xmlDoc.SelectSingleNode("//Book[1]/Title");
            if (firstBookTitleNode != null)
            {
                string firstBookTitle = firstBookTitleNode.InnerText;
                Console.WriteLine($"Selector 2 - Title of the first book: {firstBookTitle}");
            }

            Console.WriteLine("---------------------задание 4----------------------------");

            XDocument xmlDoc1 = new XDocument(
                new XElement("Students",
                    new XElement("Student",
                        new XElement("Name", "Кирилл"),
                        new XElement("Age", "18"),
                        new XElement("Grade", "10")
                    ),
                    new XElement("Student",
                        new XElement("Name", "Максим"),
                        new XElement("Age", "18"),
                        new XElement("Grade", "9")
                    )
                )
            );

            string filePath2 = "students.xml";
            xmlDoc1.Save(filePath2);
            Console.WriteLine("XML document saved.");

            xmlDoc1 = XDocument.Load(filePath2);

            var allStudents = xmlDoc1.Descendants("Student");
            Console.WriteLine("первый запрос - все студенты:");
            foreach (var student in allStudents)
            {
                string name = student.Element("Name").Value;
                string age = student.Element("Age").Value;
                string grade = student.Element("Grade").Value;

                Console.WriteLine($"Name: {name}, Age: {age}, Grade: {grade}");
            }
            Console.WriteLine();

            var firstName = xmlDoc1.Descendants("Student")
                .First()
                .Element("Name")
                .Value;
            Console.WriteLine($"второй запрос - имя первого студента: {firstName}");
        }
    }
}