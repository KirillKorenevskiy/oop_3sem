using System;
using System.IO;
using System.IO.Compression;

public class KKRLog
{
    private readonly string logFilePath;

    public KKRLog(string filePath)
    {
        logFilePath = filePath;
    }

    public void WriteLog(string action, string details)
    {
        string logMessage = $"{DateTime.Now} - {action}: {details}";

        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
        }
    }
}


public class KKRDiskInfo
{
    public void GetFreeSpace(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);

        Console.WriteLine($"Свободное место на диске {drive.Name}: {drive.AvailableFreeSpace} bytes");
    }

    public void GetFileSystem(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);

        Console.WriteLine($"Файловая система диска {drive.Name}: {drive.DriveFormat}");
    }

    public void GetAllDisksInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"Имя диска: {drive.Name}");
                Console.WriteLine($"Объем: {drive.TotalSize} bytes");
                Console.WriteLine($"Доступный объем: {drive.AvailableFreeSpace} bytes");
                Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                Console.WriteLine();
            }
        }
    }
}


public class KKRFileInfo
{
    public void GetFullPath(string filePath)
    {
        string fullPath = Path.GetFullPath(filePath);

        Console.WriteLine($"Полный путь файла: {fullPath}");
    }

    public void GetFileDetails(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        Console.WriteLine($"Размер файла: {fileInfo.Length} bytes");
        Console.WriteLine($"Расширение файла: {fileInfo.Extension}");
        Console.WriteLine($"Имя файла: {fileInfo.Name}");
    }

    public void GetFileDates(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        Console.WriteLine($"Дата создания файла: {fileInfo.CreationTime}");
        Console.WriteLine($"Дата последнего изменения файла: {fileInfo.LastWriteTime}");
    }
}

public class KKRDirInfo
{
    public void GetFileCount(string directoryPath)
    {
        try
        {
            int fileCount = Directory.GetFiles(directoryPath).Length;

            Console.WriteLine($"Количество файлов в директории: {fileCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void GetCreationTime(string directoryPath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            Console.WriteLine($"Время создания директории: {directoryInfo.CreationTime}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void GetSubdirectoryCount(string directoryPath)
    {
        try
        {
            int subdirectoryCount = Directory.GetDirectories(directoryPath).Length;

            Console.WriteLine($"Количество поддиректориев: {subdirectoryCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void GetParentDirectories(string directoryPath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            Console.WriteLine("Родительские директории:");

            DirectoryInfo parentDirectory = directoryInfo.Parent;
            while (parentDirectory != null)
            {
                Console.WriteLine(parentDirectory.FullName);
                parentDirectory = parentDirectory.Parent;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}


public class KKRFileManager
{
    public void ReadFilesAndFolders(string drivePath)
    {
        try
        {
            string[] files = Directory.GetFiles(drivePath);
            string[] folders = Directory.GetDirectories(drivePath);

            Console.WriteLine("Список файлов:");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("Список папок:");
            foreach (string folder in folders)
            {
                Console.WriteLine(folder);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void CreateInspectDirectory()
    {
        try
        {
            string inspectPath = "KKRInspect";
            Directory.CreateDirectory(inspectPath);

            string filePath = Path.Combine(inspectPath, "kkrdirinfo.txt");
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine("Информация о директории KKRInspect");
                writer.WriteLine($"Дата создания: {DateTime.Now}");
            }

            string copyPath = Path.Combine(inspectPath, "kkrdirinfo_copy.txt");
            File.Copy(filePath, copyPath);

            string renamedPath = Path.Combine(inspectPath, "kkrdirinfo_renamed.txt");
            File.Move(copyPath, renamedPath);

            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void CreateFilesDirectory(string sourcePath, string extension)
    {
        try
        {
            string filesPath = "KKRFiles";
            Directory.CreateDirectory(filesPath);

            string[] files = Directory.GetFiles(sourcePath, $"*.{extension}");
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(filesPath, fileName);
                File.Copy(file, destinationPath);
            }

            Directory.Move(filesPath, Path.Combine("KKRInspect", filesPath));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    public void CreateArchive()
    {
        try
        {
            string sourcePath = "KKRFiles";
            string archivePath = "KKRFiles.zip";

            ZipFile.CreateFromDirectory(sourcePath, archivePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}


public class KKRLogAnalyzer
{
    public void AnalyzeLogFile(string logFilePath)
    {
        try
        {
            string logContent;
            using (StreamReader reader = new StreamReader(logFilePath))
            {
                logContent = reader.ReadToEnd();
            }

            string[] logEntries = logContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string filteredKeyword = "Детальная информация о файле";
            string[] filteredEntries = logEntries.Where(entry => entry.Contains(filteredKeyword)).ToArray();

            int entryCount = filteredEntries.Length;
            Console.WriteLine($"Количество записей: {entryCount}");

            DateTime currentHour = DateTime.Now;
            string[] currentHourEntries = filteredEntries.Where(entry => GetEntryDateTime(entry).Hour == currentHour.Hour).ToArray();

            string updatedLogContent = string.Join(Environment.NewLine, currentHourEntries);

            string updatedLogFilePath = Path.Combine(Path.GetDirectoryName(logFilePath), "updated_log.txt");
            using (StreamWriter writer = new StreamWriter(updatedLogFilePath))
            {
                writer.Write(updatedLogContent);
            }

            Console.WriteLine("Обновленный файл сохранен: " + updatedLogFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    private DateTime GetEntryDateTime(string entry)
    {
        string[] entryParts = entry.Split('-');
        string dateTimeString = entryParts[0].Trim();
        DateTime entryDateTime = DateTime.ParseExact(dateTimeString, "dd.MM.yyyy H:mm:ss", null);

        return entryDateTime;
    }
}

class Program
{

    static void Main()
    {

        Console.WriteLine("-------------------Задание 1-------------------");

        KKRLog log = new KKRLog("D:\\3 сем\\oop\\lab12\\kkrlogfile.txt");
        log.WriteLog("Действие пользователя", "Детальная информация о файле\n");

        Console.WriteLine("-------------------Задание 2-------------------");

        KKRDiskInfo diskInfo = new KKRDiskInfo();
        diskInfo.GetFreeSpace("C:\\");
        diskInfo.GetFileSystem("C:\\");
        diskInfo.GetAllDisksInfo();

        Console.WriteLine("-------------------Задание 3-------------------");

        KKRFileInfo fileInfo = new KKRFileInfo();
        fileInfo.GetFullPath("D:\\3 сем\\oop\\lab12\\kkrlogfile.txt");
        fileInfo.GetFileDetails("D:\\3 сем\\oop\\lab12\\kkrlogfile.txt");
        fileInfo.GetFileDates("D:\\3 сем\\oop\\lab12\\kkrlogfile.txt");

        Console.WriteLine("-------------------Задание 4-------------------");

        KKRDirInfo dirInfo = new KKRDirInfo();
        dirInfo.GetFileCount("D:\\3 сем\\oop\\lab12");
        dirInfo.GetCreationTime("D:\\3 сем\\oop\\lab12");
        dirInfo.GetSubdirectoryCount("D:\\3 сем\\oop\\lab12");
        dirInfo.GetParentDirectories("D:\\3 сем\\oop\\lab12");

        Console.WriteLine("-------------------Задание 5-------------------");

        KKRFileManager fileManager = new KKRFileManager();

        fileManager.ReadFilesAndFolders("D:\\3 сем");
        fileManager.CreateInspectDirectory();
        fileManager.CreateFilesDirectory("D:\\3 сем\\oop\\lab12", "txt");
        fileManager.CreateArchive();

        Console.WriteLine("-------------------Задание 6-------------------");

        KKRLogAnalyzer logAnalyzer = new KKRLogAnalyzer();

        logAnalyzer.AnalyzeLogFile("D:\\3 сем\\oop\\lab12\\kkrlogfile.txt");
    }
}
