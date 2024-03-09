namespace lab11
{
    public class Set
    {
        public List<int> elements;

        public Set()
        {
            elements = new List<int>();
        }

        public Set(IEnumerable<int> collection)
        {
            elements = new List<int>(collection);
        }

        public void Add(int element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        public int this[int index]
        {
            get
            {
                return elements[index];
            }
            set
            {
                elements[index] = value;
            }
        }

        public static Set operator +(Set set1, Set set2)
        {
            Set result = new Set(set1.elements);
            foreach (int element in set2.elements)
            {
                result.Add(element);
            }
            return result;
        }

        public static bool operator <=(Set set1, Set set2)
        {
            foreach (int element in set1.elements)
            {
                if (!set2.elements.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator >=(Set set1, Set set2)
        {
            foreach (int element in set2.elements)
            {
                if (!set1.elements.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static implicit operator int(Set set)
        {
            return set.elements.Count;
        }

        public static Set operator ++(Set set)
        {
            Random random = new Random();
            int randomNumber = random.Next();
            set.Add(randomNumber);
            return set;
        }

        public int Length()
        {
            return elements.Count;
        }

        public override string ToString()
        {
            return string.Join(", ", elements);
        }
    }


    public class House
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
}