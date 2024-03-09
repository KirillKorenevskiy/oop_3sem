using System;
using System.Runtime.Serialization;

namespace lab13{
    [Serializable]
    public abstract class Examination
    {
        public string Name { get; }
        public string Date { get; }
        public int Duration { get; }
        public string Subject { get; }

        public Examination(string name, string date, int duration, string subject)
        {
            Name = name;
            Date = date;
            Duration = duration;
            Subject = subject;
        }

        public Examination() : this("", "", 0, "")
        {
        }

        public virtual void Start()
        {
            Console.WriteLine("Начало испытания");
        }

        public abstract void End();

        public override string ToString()
        {
            return $"Type: {GetType().Name}";
        }
    }

    [Serializable]
    [DataContract]
    public class Test : Examination
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public int QuestionCount { get; set; }

        [DataMember]
        public string TestType { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Owner { get; set; }
        

        [NonSerialized]
        private string privateData;

        public Test(string name, string date, int duration, int questionCount, string testType, string subject, string privateData)
            : base(name, date, duration, subject)
        {
            QuestionCount = questionCount;
            TestType = testType;
            this.privateData = privateData;
        }

        public Test() : base()
        {
            QuestionCount = 0;
            TestType = "test";
            privateData = "kirill";
        }


        public override void Start()
        {
            Console.WriteLine("Начало теста");
        }

        public override void End()
        {
            Console.WriteLine("Конец теста");
        }

        public override string ToString()
        {
            return $"Тип: {GetType().Name}";
        }
    }
}
