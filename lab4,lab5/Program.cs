using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using System.Diagnostics;

public interface IExam
{
    void StartExam();
} 

public abstract class Examination : IExam
{
    public string Name { get; }
    public string Date { get; }
    public int Duration { get; }
    public ExamType Type { get; }
    public string Subject { get; }

    public Examination(string name, string date, int duration, ExamType type, string subject)
    {
        Name = name;
        Date = date;
        Duration = duration;
        Type = type;
        Subject = subject;
    }

    public virtual void Start()
    {
        Console.WriteLine("Начало испытания");
    }

    void IExam.StartExam()
    {
        Start();
    }

    public abstract void End();

    public override string ToString()
    {
        return $"Type: {GetType().Name}";
    }
}

public class Test : Examination
{
    public int QuestionCount { get; }
    public string TestType { get; }

    public Test(string name, string date, int duration, int questionCount, string testType, ExamType type, string subject)
        : base(name, date, duration, type, subject)
    {
        QuestionCount = questionCount;
        TestType = testType;
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

public class Exam : Examination
{
    public int AssignmentCount { get; }
    public string DifficultyLevel { get; }
    public string Subject { get; }

    public Exam(string name, string date, int duration, int assignmentCount, string difficultyLevel, ExamType type, string subject)
        : base(name, date, duration, type, subject)
    {
        AssignmentCount = assignmentCount;
        DifficultyLevel = difficultyLevel;
        Subject = subject;

        if (string.IsNullOrEmpty(name))
        {
            throw new CustomException("Неверное имя экзамена");
        }

        if (assignmentCount < 0)
        {
            throw new CustomException("Неверное количество заданий");
        }
    }

    public override void Start()
    {
        Console.WriteLine("Начало экзамена");
    }

    public override void End()
    {
        Console.WriteLine("Конец экзамена");
    }

    public override string ToString()
    {
        return $"Тип: {GetType().Name} - Дата: {Date}, Продолжительность: {Duration}";
    }


}

public class FinalExam : Exam, IExam
{
    public string ThesisTopic { get; }
    public int StudentCount { get; }

    public FinalExam(string name, string date, int duration, int assignmentCount, string difficultyLevel,
        string thesisTopic, int studentCount, ExamType type, string subject)
        : base(name, date, duration, assignmentCount, difficultyLevel, type, subject)
    {
        ThesisTopic = thesisTopic;
        StudentCount = studentCount;
    }

    public override void Start()
    {
        Console.WriteLine("Начало выпускного экзамена");
    }

    public override void End()
    {
        Console.WriteLine("Конец выпускного экзамена");
    }

    public override string ToString()
    {
        return $"Тип: {GetType().Name} - Дата: {Date}, Продолжительность: {Duration}";
    }
}

public partial class Question
{
    public string Text { get; }
    public List<string> AnswerOptions { get; }
    public string CorrectAnswer { get; }

    public Question(string text, List<string> answerOptions, string correctAnswer)
    {
        Text = text;
        AnswerOptions = answerOptions;
        CorrectAnswer = correctAnswer;
    }
}

public class Printer
{
    public void IAmPrinting(Examination examination)
    {
        Console.WriteLine(examination.ToString());
    }
}


public enum ExamType
{
    Test,
    Exam,
    FinalExam
}

public struct ExamResult
{
    public string StudentName;
    public int Score;

    public ExamResult(string studentName, int score)
    {
        StudentName = studentName;
        Score = score;
    }
 
}

public class Session
{
    private List<Examination> examinations;

    public Session()
    {
        examinations = new List<Examination>();
    }

    public void AddExamination(Examination examination)
    {
        examinations.Add(examination);
    }

    public void RemoveExamination(Examination examination)
    {
        examinations.Remove(examination);
    }

    public void PrintExaminations()
    {
        foreach (var examination in examinations)
        {
            Console.WriteLine($"Имя: {examination.Name}, Предмет: {examination.Subject}");
        }
    }

    public List<Exam> FindExamsBySubject(string subject)
    {
        List<Exam> exams = new List<Exam>();

        foreach (var examination in examinations)
        {
            if (examination is Exam exam && exam.Subject == subject)
            {
                exams.Add(exam);
            }
        }

        return exams;
    }

    public int GetTotalExaminationsCount()
    {
        return examinations.Count;
    }

    public int GetTestsWithQuestionCount(int questionCount)
    {
        int count = 0;

        foreach (var examination in examinations)
        {
            if (examination is Test test && test.QuestionCount == questionCount)
            {
                count++;
            }
        }

        return count;
    }
}



public class SessionController
{
    private Session session;

    public SessionController()
    {
        session = new Session();
    }

    public void AddExamination(Examination examination)
    {
        session.AddExamination(examination);
    }

    public void RemoveExamination(Examination examination)
    {
        session.RemoveExamination(examination);
    }

    public void PrintExaminations()
    {
        session.PrintExaminations();
    }

    public List<Exam> FindExamsBySubject(string subject)
    {
        return session.FindExamsBySubject(subject);
    }

    public int GetTotalExaminationsCount()
    {
        return session.GetTotalExaminationsCount();
    }

    public int GetTestsWithQuestionCount(int questionCount)
    {
        return session.GetTestsWithQuestionCount(questionCount);
    }


    public void LoadFromFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                if (data.Length == 9)
                {
                    string type = data[0].Trim();
                    string name = data[1].Trim();
                    string date = data[2].Trim();
                    int duration = int.Parse(data[3].Trim());

                    if (type == "Exam")
                    {
                        int assignmentCount = int.Parse(data[4].Trim());
                        string difficultyLevel = data[5].Trim();
                        ExamType examType = Enum.Parse<ExamType>(data[6].Trim());
                        string subject = data[7].Trim();

                        session.AddExamination(new Exam(name, date, duration, assignmentCount, difficultyLevel, examType, subject));
                    }
                    else if (type == "Test")
                    {
                        int questionCount = int.Parse(data[4].Trim());
                        string testType = data[5].Trim();
                        ExamType examType = Enum.Parse<ExamType>(data[6].Trim());
                        string subject = data[7].Trim();

                        session.AddExamination(new Test(name, date, duration, questionCount, testType, examType, subject));
                    }
                }
            }

            Console.WriteLine("Данные загружены");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }


    public void LoadFromJsonFile(string filePath)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);
            List<Exam> loadedExams = JsonConvert.DeserializeObject<List<Exam>>(jsonData);

            foreach (Exam exam in loadedExams)
            {
                session.AddExamination(exam);
            }

            Console.WriteLine("Данные из JSON файла загружены");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}




public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }

    public override string ToString()
    {
        return "CustomException: " + Message;
    }
}

public class CustomInvalidOperationException : InvalidOperationException
{
    public CustomInvalidOperationException(string message) : base(message)
    {
    }
}

public class CustomChildException : CustomException
{
    public CustomChildException(string message) : base(message)
    {
    }
}







public class Program
{
    public static void Main()
    {
        Test test = new Test("Тест по математике", "2023-09-25", 60, 10, "Открытого типа", ExamType.FinalExam, "Математика");
        Exam exam = new Exam("Экзамен по физике", "2023-09-26", 90, 5, "Продвинутый", ExamType.Test, "Тервер");
        FinalExam finalExam = new FinalExam("Выпускной экзамен", "2023-09-27", 120, 3, "Эксперт",
            "Исследование в области физики", 50, ExamType.Exam, "Физика");

        Examination exam1 = test;
        IExam exam2 = (IExam)finalExam;

        if (exam1 is Test)
        {
            Console.WriteLine("exam1 относится к типу Test");
        }

        if (exam2 is FinalExam)
        {
            Console.WriteLine("exam2 относится к типу FinalExam");
        }

        Test testObj = exam1 as Test;
        if (testObj != null)
        {
            Console.WriteLine("Приведение exam1 в Test успешно");
        }

        FinalExam finalExamObj = exam2 as FinalExam;
        if (finalExamObj != null)
        {
            Console.WriteLine("Приведение exam2 в FinalExam успешно");
        }

        Console.WriteLine();

        Console.WriteLine($"Тест: {test.Name}, Дата: {test.Date}, Продолжительность: {test.Duration}, " +
                          $"Количество вопросов: {test.QuestionCount}, Тип теста: {test.TestType}");

        Console.WriteLine($"Экзамен: {exam.Name}, Дата: {exam.Date}, Продолжительность: {exam.Duration}, " +
                          $"Количество заданий: {exam.AssignmentCount}, Уровень сложности: {exam.DifficultyLevel}");

        Console.WriteLine($"Выпускной экзамен: {finalExam.Name}, Date: {finalExam.Date}, Продолжительность: {finalExam.Duration}, " +
                          $"Количество заданий: {finalExam.AssignmentCount}, Уровень сложности: {finalExam.DifficultyLevel}, " +
                          $"Тема: {finalExam.ThesisTopic}, Количество студентов: {finalExam.StudentCount}");

        Console.WriteLine();

        List<string> answerOptions = new List<string> { "Вариант 1", "Вариант 2", "Вариант 3", "Вариант 4" };
        Question question = new Question("Сталиция Франции?", answerOptions, "Вариант 2");
        Console.WriteLine(question);

        Console.WriteLine("------------------------------------------");

        List<Examination> examinations = new List<Examination>()
        {
            new Test("Тест по математике", "2023-09-25", 60, 10, "Открытого типа", ExamType.Test, "Математика"),
            new Exam("Экзамен по физике", "2023-09-26", 90, 5, "Продвинутый", ExamType.Exam, "Математика"),
            new FinalExam("Выпускной экзамен", "2023-09-27", 120, 3, "Эксперт","Исследование в области физики", 50, ExamType.FinalExam, "Математика")
        };

        Printer printer = new Printer();

        foreach (Examination examination in examinations)
        {
            printer.IAmPrinting(examination);
        }

        Console.WriteLine("------------------------пятая лаба-------------------------");

        ExamResult result1 = new ExamResult("Кирилл Кореневский", 9);
        Console.WriteLine(result1.StudentName);
        Console.WriteLine(result1.Score);

        Console.WriteLine(finalExam.Type);

        Console.WriteLine("------------------------------------------------------");

        Session session = new Session();

        session.AddExamination(new Exam("Экзамен по физике", "2023-09-26", 90, 5, "Продвинутый", ExamType.Test, "Физика"));
        session.AddExamination(new Test("Тест по математике", "2023-09-25", 60, 10, "Открытого типа", ExamType.FinalExam, "Математика"));
        session.AddExamination(new Test("Тест по терверу", "2023-09-25", 60, 10, "Открытого типа", ExamType.FinalExam, "Тервер"));
        session.AddExamination(new Exam("Экзамен по ооп", "2023-09-26", 90, 5, "Продвинутый", ExamType.Test, "ооп"));
        session.AddExamination(new Test("Тест по реакту", "2023-09-25", 60, 10, "Открытого типа", ExamType.FinalExam, "реакт"));

        Console.WriteLine("Все экзамены:");
        session.PrintExaminations();
        Console.WriteLine();

        string subject = "Математика";
        List<Exam> mathExams = session.FindExamsBySubject(subject);
        Console.WriteLine($"Экзамены по предмету '{subject}':");
        foreach (var examm in mathExams)
        {
            Console.WriteLine($"Имя: {examm.Name}, Предмет: {examm.Subject}, Сложность: {examm.DifficultyLevel}");
        }
        Console.WriteLine();

        int totalExaminationsCount = session.GetTotalExaminationsCount();
        Console.WriteLine($"Всего экзаменов: {totalExaminationsCount}");
        Console.WriteLine();

        int questionCount = 10;
        int testCount = session.GetTestsWithQuestionCount(questionCount);
        Console.WriteLine($"Количество тестов с {questionCount} вопросов: {testCount}");

        Console.WriteLine("---------------------------1 доп к 5 лабе---------------------------");

        SessionController sessionController = new SessionController();
        sessionController.LoadFromFile("D:/3 сем/oop/lab4,lab5/examinations.txt");

        Console.WriteLine("---------------------------2 доп к 5 лабе---------------------------");


        SessionController sessionController2 = new SessionController();
        sessionController2.LoadFromJsonFile("D:/3 сем/oop/lab4,lab5/examinations.json");


        Console.WriteLine("---------------------------6 лаба---------------------------");
        Console.WriteLine();
        Console.WriteLine("---------------------------2 задание---------------------------");


        try
        {
            Examination invalidExam = new Exam("", "2023-10-10", -1, 10, "Easy", ExamType.Exam, "Mathematics");
        }
        catch (CustomException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошло исключение: " + ex.Message);
        }

        try
        {
            int[] numbers = new int[int.MaxValue];
        }
        catch (OutOfMemoryException ex)
        {
            Console.WriteLine("Ошибка работы с памятью: " + ex.Message);
        }

        try
        {
            string fileContent = System.IO.File.ReadAllText("abc.txt");
            Console.WriteLine("Содержимое файла: " + fileContent);
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.WriteLine("Ошибка работы с файлами: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Блок finally");
        }

        try
        {
            int result = DivideNumbers(10, 0);
            Console.WriteLine("Результат деления: " + result);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Ошибка деления на ноль");
            Console.WriteLine("Место исключения: " + ex.StackTrace);
            Console.WriteLine("Диагностика: " + ex.StackTrace);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine("Место исключения: " + ex.StackTrace);
            Console.WriteLine("Диагностика: " + ex.StackTrace);
        }
        finally
        {
            Console.WriteLine("Блок finally");
        }

        int DivideNumbers(int dividend, int divisor)
        {
            Assert.IsTrue(divisor != 0, "Делитель не может быть нулем.");
            try
            {
                return dividend / divisor;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Ошибка деления на ноль: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                throw;
            }
        }

        try
        {
            int[] array = { 1, 2, 3 };
            int value = array[5];
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.WriteLine("Ошибка неверного индекса: " + ex.Message);
            Debugger.Break();
        }

    }   
}