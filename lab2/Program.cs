using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;

namespace lab2
{
    partial class House
    {
        private int id;
        private int apartmentNumber;
        private double area;
        private int floor;
        private int roomCount;
        private string street;
        private const string BuildingType = "Девятиэтажка";
        private int operatingPeriod;

        private static int counter = 0;

        public House()
        {
            id = IdHashFun();
            apartmentNumber = 13;
            area = 123;
            floor = 7;
            roomCount = 3;
            street = "белорусская";
            operatingPeriod = 18;
        }

        public House(int apartmentNumber, double area, int floor, int roomCount, string street, int operatingPeriod)
        {
            id = IdHashFun();
            this.apartmentNumber = apartmentNumber;
            this.area = area;
            this.floor = floor;
            this.roomCount = roomCount;
            this.street = street;
            this.operatingPeriod = operatingPeriod;
        }

        // Конструктор с параметрами по умолчанию
        public House(int apartmentNumber, double area, string street)
        {
            id = IdHashFun();
            this.apartmentNumber = apartmentNumber;
            this.area = area;
            floor = 1;
            roomCount = 1;
            this.street = street;
            operatingPeriod = 17;
        }

        static House()
        {
            Console.WriteLine("статический конструктор");
        }

        private int IdHashFun()
        {
            int uniID = counter.GetHashCode();
            counter++;
            return uniID;
        }

        public int Id
        {
            get 
            { 
                return id; 
            }
            private set 
            { 
                id = value;
            }
        }

        public int ApartmentNumber
        {
            get
            {
                return apartmentNumber;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Неверное значение номера");
                else
                    apartmentNumber = value;
            }
        }

        public double Area
        {
            get 
            {
                return area; 
            }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Неверное значение площади");
                else
                    area = value;
            }
        }

        public int Floor
        {
            get 
            {
                return floor; 
            }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Неверное значение этажа");
                else
                    floor = value; 
            }

        }

        public int RoomCount
        {
            get 
            { 
                return roomCount; 
            }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Неверное значение количества комнат");
                else
                    roomCount = value; 
            }
        }

        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    street = value;
                else
                    throw new ArgumentException("Неверное значение улицы");
            }
        }

        public int OperatingPeriod
        {
            get
            {
                return operatingPeriod;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Неверное значение срока эксплуатации");
                else
                    operatingPeriod = value;
            }
        }

        public void YearsOld()
        {
            int age = operatingPeriod;
            if (age > 20)
            {
                Console.WriteLine("Требуется капитальный ремонт");
            }
        }
    }


    class PrivConstr
    {
        private PrivConstr()
        {
            Console.WriteLine("Приватный конструктор");
        }
    }

    partial class House
    {
        public static void ShowDublicateInfo()
        {
            Console.WriteLine($"Количество экземпляров {counter}");
        }

        public void PrintInfo()
        {
            Console.WriteLine("Информация:");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Номер квартиры: {ApartmentNumber}");
            Console.WriteLine($"Площадь: {Area} кв.м.");
            Console.WriteLine($"Этаж: {Floor}");
            Console.WriteLine($"Количество комнат: {RoomCount}");
            Console.WriteLine($"Улица: {Street}");
            Console.WriteLine($"Тип дома: {BuildingType}");
            Console.WriteLine($"Срок эксплуатации: {OperatingPeriod}");
        }

        public void AreaWithBalcony(ref int liveBalconyArea, out double totalArea)
        {
            liveBalconyArea += 5;
            totalArea = Area + liveBalconyArea;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            House other = (House)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"House [ID: {Id}, Номер квартиры: {ApartmentNumber}, Площадь: {Area} кв.м., Улица: {Street}]";
        }


        //метод для выполнения 3 задания
        public static void FlatsByRoomAndFloor(House[] houses, int roomCount, int minFloor, int maxFloor)
        {
            Console.WriteLine($"Квартиры с {roomCount} комнат на этажах {minFloor}-{maxFloor}:");
            foreach (House house in houses)
            {
                if (house.RoomCount == roomCount && house.Floor >= minFloor && house.Floor <= maxFloor)
                {
                    Console.WriteLine($"Номер квартиры: {house.ApartmentNumber}, Этаж: {house.Floor}");
                }
            }
        }
    }



    class MainFun
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("----------------2--------------");
            Console.WriteLine();

            House house1 = new House(66, 80.5, 5, 3, "Герцена", 17);
            House house2 = new House(11, 90.7, "Белорусская");

            house1.Floor = 6;
            house2.RoomCount = 4;

            house1.PrintInfo();
            Console.WriteLine();
            int liveBalconyArea = 12;
            double totalArea;
            house2.AreaWithBalcony(ref  liveBalconyArea, out totalArea);
            house2.PrintInfo();
            Console.WriteLine();

            Console.WriteLine($"house1 равен house2: {house1.Equals(house2)}");

            Console.WriteLine($"house1 является House: {house1 is House}");
            Console.WriteLine();

            Console.WriteLine($"house1 hash: {house1.GetHashCode()}");
            Console.WriteLine($"house2 hash: {house2.GetHashCode()}");
            Console.WriteLine($"house1 строковое представление: {house1.ToString()}");
            Console.WriteLine();
            Console.WriteLine($"house2 строковое представление: {house2.ToString()}");

            Console.WriteLine();
            Console.WriteLine("----------------3--------------");
            Console.WriteLine();

            House[] houses = new House[]
            {
                new House(101, 82, 5, 3, "Бобруйская", 23),
                new House(137, 90, 4, 4, "Набережная", 41),
                new House(100, 75, 6, 2, "Солнечная", 18),
                new House(19, 100, 5, 3, "Новая Боровая", 5),
                new House(89, 60, 7, 4, "Розы Люксембург", 10)
            };

            int targetRoomCount = 3;
            int minTargetFloor = 4;
            int maxTargetFloor = 6;

            House.FlatsByRoomAndFloor(houses, targetRoomCount, minTargetFloor, maxTargetFloor);

            Console.WriteLine();
            Console.WriteLine("----------------4--------------");
            Console.WriteLine();

            var house = new
            {
                ApartmentNumber = 67,
                Area = 80.5,
                RoomCount = 3,
                Floor = 5,
                Address = "",
                AgeOfBuilding = 45
            };

            Console.WriteLine($"Номер квартиры: {house.ApartmentNumber}");
            Console.WriteLine($"Площадь: {house.Area} кв.м.");
            Console.WriteLine($"Количество комнат: {house.RoomCount}");
            Console.WriteLine($"Эатаж: {house.Floor}");
            Console.WriteLine($"Адрес: {house.Address}");
            Console.WriteLine($"Возраст здания: {house.AgeOfBuilding}");
        }
    }
}