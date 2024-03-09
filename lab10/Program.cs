using System;
using System.Linq;

class House
{
    public string Address { get; set; }
    public int Rooms { get; set; }
    public decimal Floor { get; set; }
    public int HouseNumber { get; set; }
    public int OwnerId { get; set; }
}

class Owner
{
    public int Id { get; set; }
    public string Name { get; set; }
}


class Program
{
    static void Main()
    {
        string[] months = { "June", "July", "May", "December", "January", "August", "February", "April", "November", "September", "October", "March" };

        int n = 5;
        var monthsWithLengthN = months.Where(month => month.Length == n);

        Console.WriteLine("Месяцы с длиной строки {0}:", n);
        foreach (var month in monthsWithLengthN)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var summerAndWinterMonths = months.Where(month => month == "June" || month == "July" || month == "August" || month == "December" || month == "January" || month == "February");

        Console.WriteLine("Летние и зимние месяцы:");
        foreach (var month in summerAndWinterMonths)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var monthsInAlphabeticalOrder = months.OrderBy(month => month);

        Console.WriteLine("Месяцы в алфавитном порядке:");
        foreach (var month in monthsInAlphabeticalOrder)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var monthsWithUAndLengthAtLeast4 = months.Where(month => month.Contains("u") && month.Length >= 4);
        int count = monthsWithUAndLengthAtLeast4.Count();

        Console.WriteLine("Количество месяцев, содержащих букву \"u\" и длиной имени не менее 4-х: {0}", count);

        Console.WriteLine("----------------------задание 2-------------------");

        List<House> houses = new List<House>
        {
            new House { Address = "ул. Центральная", Rooms = 3, Floor = 2, OwnerId = 1 },
            new House { Address = "пр. Ленина", Rooms = 4, Floor = 5, HouseNumber = 13, OwnerId = 2 },
            new House { Address = "ул. Пушкина", Rooms = 2, Floor = 3, OwnerId = 3 },
            new House { Address = "пр. Ленина", Rooms = 3, Floor = 4, HouseNumber = 13, OwnerId = 4 },
            new House { Address = "пр. Ленина", Rooms = 2, Floor = 7, HouseNumber = 13, OwnerId = 5 },
            new House { Address = "ул. Гагарина", Rooms = 2, Floor = 2, OwnerId = 6 },
            new House { Address = "ул. Садовая", Rooms = 5, Floor = 6},
            new House { Address = "пр. Кирова", Rooms = 4, Floor = 3, HouseNumber = 5 },
            new House { Address = "пр. Ленина", Rooms = 5, Floor = 2, HouseNumber = 13 },
            new House { Address = "ул. Московская", Rooms = 3, Floor = 5 },
            new House { Address = "ул. Ленинская", Rooms = 2, Floor = 1 },
            new House { Address = "ул. Победы", Rooms = 4, Floor = 4 }
        };

        Console.WriteLine("----------------------задание 3-------------------");


        int targetRooms = 3;
        var housesWithTargetRooms = houses.Where(house => house.Rooms == targetRooms);
        Console.WriteLine("Дома с {0} комнатами:", targetRooms);
        foreach (var house in housesWithTargetRooms)
        {
            Console.WriteLine("Адрес: {0}, Комнаты: {1}, Этаж: {2}", house.Address, house.Rooms, house.Floor);
        }
        Console.WriteLine();

        string targetAddress = "пр. Ленина";
        int targetHouseNumber = 13;
        var targetHouses = houses.Where(house => house.Address.Contains(targetAddress) && house.HouseNumber == targetHouseNumber)
                                 .Take(5);
        Console.WriteLine("Первые 5 домов на {0} {1}:", targetAddress, targetHouseNumber);
        foreach (var house in targetHouses)
        {
            Console.WriteLine("Адрес: {0}, Комнаты: {1}, Этаж: {2}", house.Address, house.Rooms, house.Floor);
        }
        Console.WriteLine();

        string targetStreet = "пр. Кирова";
        int houseCount = houses.Count(house => house.Address.Contains(targetStreet));
        Console.WriteLine("Количество домов на {0}: {1}", targetStreet, houseCount);
        Console.WriteLine();

        int targetRooms2 = 4;
        int minFloor = 2;
        int maxFloor = 5;
        var housesWithTargetRoomsAndFloorRange = houses.Where(house => house.Rooms == targetRooms2 && house.Floor >= minFloor && house.Floor <= maxFloor);
        Console.WriteLine("Дома с {0} комнатами и этажом от {1} до {2}:", targetRooms2, minFloor, maxFloor);
        foreach (var house in housesWithTargetRoomsAndFloorRange)
        {
            Console.WriteLine("Адрес: {0}, Комнаты: {1}, Этаж: {2}", house.Address, house.Rooms, house.Floor);
        }
        Console.WriteLine();


        Console.WriteLine("----------------------задание 4-------------------");


        var result = houses
        .Where(house => house.Rooms > 3) 
        .OrderBy(house => house.Floor) 
        .GroupBy(house => house.Address) 
        .Select(group => new
        {
            Address = group.Key,
            AverageRooms = group.Average(house => house.Rooms),
            MaxFloor = group.Max(house => house.Floor)
        })
        .TakeWhile(house => house.MaxFloor >= 3)
        .Skip(2) 
        .ToList();

        foreach (var house in result)
        {
            Console.WriteLine("Адрес: {0}, Среднее количество комнат: {1}, Максимальный этаж: {2}", house.Address, house.AverageRooms, house.MaxFloor);
        }


        Console.WriteLine("----------------------задание 5-------------------");

        List<Owner> owners = new List<Owner>
        {
            new Owner { Id = 1, Name = "Иванов" },
            new Owner { Id = 2, Name = "Петров" },
            new Owner { Id = 3, Name = "Сидоров" },
            new Owner { Id = 4, Name = "Кореневский"},
            new Owner { Id = 5, Name = "Юхневич"},
            new Owner { Id = 6, Name = "Бернович"}
        };

        var result1 = houses
        .Join(owners,
            house => house.OwnerId, 
            owner => owner.Id, 
            (house, owner) => new 
            {
                Address = house.Address,
                OwnerName = owner.Name
            });

        foreach (var house in result1)
        {
            Console.WriteLine("Адрес: {0}, Владелец: {1}", house.Address, house.OwnerName);
        }

    }
}