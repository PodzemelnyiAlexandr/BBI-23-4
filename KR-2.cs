using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Schema;
abstract class Task
{
    protected string text;
    public string Text { get => text; protected set => text = value; }
    protected char[] pattern = { ' ', ',', '.', '!', ';', ':', '?', '-', '(', ')' };
    public Task(string text)
    {
        this.text = text;
    }
    protected string[] Words(string Text)
    {
        text = text.Replace(". ", ".").Replace(", ", ",").Replace("! ", "!").Replace("; ", ";").Replace(": ", ":").Replace("? ", "?").Replace("- ", "-").Replace("( ", "(").Replace(") ", ")");
        string[] words = text.Split(pattern);
        return words;
    }
}

class Task1 : Task
{
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        string[] words = Words(text);
        Console.WriteLine(words[(words.Length - 1) / 2]);
        return base.ToString();
    }
}
class Task2 : Task
{
    [JsonConstructor]
    public Task2(string text) : base(text) { }
    public override string ToString()
    {
        string[] words = Words(text);
        string[] repeats = new string[words.Length];
        int k = 0;
        for (int i = 0; i < words.Length - 1; i++)
        {
            if (words[i] != " ")
            {
                string word = words[i];
                bool flag = true;
                for (int j = 1 + 1; j < words.Length; j++)
                {
                    if (words[j] == word)
                    {
                        if (flag)
                        {
                            repeats[k++] = word;
                            flag = false;
                        }
                        for (int h = j; h < words.Length; h++)
                        {
                            words[j] = words[j + 1];
                            words[j + 1] = " ";
                        }
                    }
                }
            }
        }
        foreach (string a in repeats) Console.WriteLine(a);
        return base.ToString();
    }
}
class JsonIO
{
    public static void Write<T>(T obj, string FilePath)
    {
        using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string FilePath)
    {
        using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
        return default(T);
    }
}
    
internal class Program
{
    static void Main(string[] args)
    {
        Task[] tasks =
        {
            new Task1("Я, маменька, себе новое знакомство завёл. Лукьян Лукьяныч Чебаков, отличнейний человек! Он капитан в отставке."),
            new Task2("Я, маменька, себе новое знакомство завёл. Лукьян Лукьяныч Чебаков, отличнейний человек! Он капитан в отставке.")
        };
        tasks[0].ToString();
        tasks[1].ToString();
        Console.WriteLine(tasks[0].ToString());
        Console.WriteLine(tasks[1].ToString());
        string path = @"C:\Users\m2310266\Documents";
        string name = "Answer";
        path = Path.Combine(path, name);
        if (!Directory.Exists(path)) 
        {
            Directory.CreateDirectory(path);
        }
        string Name1 = "cw2_1.json";
        string Name2 = "cw2_2.json";
        Name1 = Path.Combine(path, Name1);
        Name2 = Path.Combine(path, Name2);
        if (!File.Exists(Name1))
        {
            JsonIO.Write<Task1>((Task1)tasks[0], Name1);
            JsonIO.Write<Task2>((Task2)tasks[0], Name2);
        }
        else
        {
            Console.WriteLine(JsonIO.Read<Task1>(Name1));
            Console.WriteLine(JsonIO.Read<Task2>(Name2));
        }
    }
}



