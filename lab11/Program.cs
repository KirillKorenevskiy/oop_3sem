using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace lab11
{
    public static class Reflector
    {
        public static void GetAssemblyName(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                Assembly assembly = type.Assembly;
                Console.WriteLine($"Имя сборки: {assembly.FullName}");
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void HasPublicConstructors(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                ConstructorInfo[] constructors = type.GetConstructors();
                bool hasPublicConstructors = constructors.Any(c => c.IsPublic);
                Console.WriteLine($"Имеет публичные конструкторы: {hasPublicConstructors}");
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void GetPublicMethods(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var methodNames = methods.Select(m => m.Name);
                foreach (var methodName in methodNames)
                {
                    Console.WriteLine($"Метод: {methodName}");
                }
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void GetFieldsAndProperties(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var memberNames = fields.Select(f => f.Name).Concat(properties.Select(p => p.Name));
                foreach (var memberName in memberNames)
                {
                    Console.WriteLine($"Поле или Свойство: {memberName}");
                }
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void GetImplementedInterfaces(string className)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                var interfaces = type.GetInterfaces();
                foreach (var interfaceType in interfaces)
                {
                    Console.WriteLine($"Интерфейс: {interfaceType.FullName}");
                }
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void GetMethodsWithParameter(string className, Type parameterType)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var methodNames = methods
                    .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
                    .Select(m => m.Name);
                foreach (var methodName in methodNames)
                {
                    Console.WriteLine($"Метод с параметром типа '{parameterType.Name}': {methodName}");
                }
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        public static void InvokeMethodFromFile(string className, string methodName, string filePath)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                MethodInfo method = type.GetMethod(methodName);
                if (method != null)
                {
                    object instance = Activator.CreateInstance(type);
                    object[] parameters = GenerateMethodParameters(method);
                    method.Invoke(instance, parameters);
                    WriteParametersToFile(parameters, filePath);
                }
                else
                {
                    Console.WriteLine($"Метод '{methodName}' не найден.");
                }
            }
            else
            {
                Console.WriteLine($"Тип '{className}' не найден.");
            }
        }

        private static object[] GenerateMethodParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] parameterValues = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GenerateValueForType(parameters[i].ParameterType);
            }
            return parameterValues;
        }

        private static object GenerateValueForType(Type type)
        {
            if (type == typeof(int))
            {
                return 42;
            }
            else if (type == typeof(string))
            {
                return "!";
            }
            else
            {
                return null;
            }
        }

        private static void WriteParametersToFile(object[] parameters, string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(parameters);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Данные успешно записаны в файл: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи данных в файл: " + ex.Message);
            }
        }

        public static T Create<T>()
        {
            Type type = typeof(T);
            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Тип '{type.FullName}' не имеет публичных конструкторов.");
            }

            ConstructorInfo constructor = constructors[0];

            try
            {
                object instance = constructor.Invoke(null);
                return (T)instance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при создании объекта типа '{type.FullName}': {ex.Message}", ex);
            }
        }
    }


    class Program
    {
        static void Main()
        {
            Console.WriteLine("Исследование типа House:");
            Reflector.GetAssemblyName("lab11.House");
            Reflector.HasPublicConstructors("lab11.House");
            Reflector.GetPublicMethods("lab11.House");
            Reflector.GetFieldsAndProperties("lab11.House");
            Reflector.GetImplementedInterfaces("lab11.House");
            Reflector.GetMethodsWithParameter("lab11.House", typeof(int));

            Console.WriteLine();

            Console.WriteLine("Исследование типа Set:");
            Reflector.GetAssemblyName("lab11.Set");
            Reflector.HasPublicConstructors("lab11.Set");
            Reflector.GetPublicMethods("lab11.Set");
            Reflector.GetFieldsAndProperties("lab11.Set");
            Reflector.GetImplementedInterfaces("lab11.Set");
            Reflector.GetMethodsWithParameter("lab11.Set", typeof(int));

            string filePath = "D:\\3 сем\\oop\\lab11\\json1.json";
            Reflector.InvokeMethodFromFile("lab11.Set", "Add", filePath);

            Set set = Reflector.Create<Set>();

        }
    }
}
