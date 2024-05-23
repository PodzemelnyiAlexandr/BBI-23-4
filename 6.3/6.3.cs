// // Задача 3.6
// // Японская радиокомпания провела опрос радиослушателей по трем вопросам:
// // а). Какое животное Вы связываете с Японией и японцами?
// // б). Какая черта характера присуща японцам больше всего?
// // в). Какой неодушевленный предмет или понятие Вы связываете с Японией?
// // Большинство опрошенных прислали ответы на все или часть вопросов.
// // Составить программу получения первых пяти наиболее часто встречающихся
// // ответов по каждому вопросу и доли (в %) каждого такого ответа.
// // Предусмотреть необходимость сжатия столбца ответов в случае отсутствия
// // ответов на некоторые вопросы.

// class Program
// {
//     struct Answer
//     {
//         private string answer;
//         private int calls;
//         public double prop {get; private set;}

//         public Answer(string _answer, int _calls, int count)
//         {
//             answer = _answer;
//             calls = _calls;
//             prop = (double) calls * 100 / count;
//         }
//         public void Print()
//         {
//             Console.WriteLine(" Ответ '{0}' был выбран {1} раз(а), доля от всех ответов: {2:f1}%", answer, calls, prop);
//         }
//     }

//     static void SortMatrix(Answer[,] a)
//         {
//             for (int k = 0; k < a.GetLength(0); k++)
//                 for (int i = 1; i < a.GetLength(1); i++)
//                     {
//                         Answer q = a[k, i];
//                         int j = i - 1;

//                         while(j >= 0 && a[k, j].prop < q.prop)
//                         {
//                             a[k, j + 1] = a[k, j];
//                             j--;
//                         }
//                         a[k, j + 1] = q;
//                     }
//         }
//     static void Main()
//     {
//         string[,] answers = // каждый столбец - ответы одного опрошенного на все три вопроса
//         {
//             { "Панда",   "Панда",   "-",              "Лошадь", "Панда",        "Собака",    "Собака",   "Игуана", "Голубь",       "-" },
//             { "Доброта", "Доброта", "Чистоплотность", "-",      "Аккуратность", "Честность", "Гордость", "-",      "Аккуратность", "Гордость" },
//             { "Мир",     "Красота", "-",              "Страна", "Мир",          "Аниме",     "Роллы",    "Роллы",  "Роллы",        "-" }
//         };
//         Answer[,] total = new Answer[3, answers.GetLength(1)];
//         int last, count;
//         for (int k = 0; k < 3; k++)
//         {
//             string[] AnswersSorted = new string[answers.GetLength(1)];  
//             int[] AnswersCalls = new int[answers.GetLength(1)]; 
//             last = 0; count = 0;
//             for (int i = 0; i < answers.GetLength(1); i++)  
//                 if (answers[k,i] != "-")
//                 {
//                     if (InArray(answers[k,i], AnswersSorted) == -1)
//                     {
//                         AnswersSorted[last] = answers[k,i];
//                         AnswersCalls[last] += 1;
//                         last++;
//                     }
//                     else AnswersCalls[InArray(answers[k,i], AnswersSorted)] += 1;
//                     count++;
//                 }
//             for (int i = 0; i < AnswersSorted.Length; i++) total[k, i] = new Answer(AnswersSorted[i], AnswersCalls[i], count);
//         }

//         int InArray(string answer, string[] answers)  // метод, позволяющий узнать, есть ли элемент в массиве
//         {
//             for (int i = 0; i < last; i++) if (answers[i] == answer)
//                     return i;
//             return -1;
//         }

//         SortMatrix(total);

//         for (int k = 0; k < 3; k++)
//         {
//             Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
//             for (int i = 0; i < 5; i++) total[k,i].Print();
//             Console.WriteLine();
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract partial class Festival
{
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TicketPrice { get; set; }

    public abstract decimal CalculatePrice();
    public override string ToString()
    {
        return $"Name: {Name}, Location: {Location}, Start Date: {StartDate}, End Date: {EndDate}, Total Amount: {TotalAmount:C}, Ticket Price: {TicketPrice:C}";
    }
}

public class MusicFestival : Festival
{
    public string Headliner { get; set; }
    public int NumberOfBands { get; set; }

    public override decimal CalculatePrice()
    {
        return TicketPrice * (EndDate - StartDate).Days;
    }
}

public class ComicsFestival : Festival
{
    public string MainCharacter { get; set; }
    public int NumberOfIssues { get; set; }

    public override decimal CalculatePrice()
    {
        return TicketPrice * NumberOfIssues;
    }
}

public class FoodFestival : Festival
{
    public string CuisineType { get; set; }
    public int NumberOfVendors { get; set; }

    public override decimal CalculatePrice()
    {
        return TicketPrice * NumberOfVendors;
    }
}

public interface ICounter
{
    int DailyFlow { get; set; }
    decimal Revenue { get; set; }
    decimal DailyRevenue { get; set; }
}

public partial class Festival : ICounter
{
    public int DailyFlow { get; set; }
    public decimal Revenue { get; set; }
    public decimal DailyRevenue { get; set; }
}

public class FestivalCalendar
{
    private List<Festival> festivals;

    public FestivalCalendar()
    {
        festivals = new List<Festival>();
    }

    public void AddFestival(Festival festival)
    {
        festivals.Add(festival);
    }

    public void ShowFestivals()
    {
        foreach (var festival in festivals)
        {
            Console.WriteLine(festival);
        }
    }

    public void ShowFestivals(DateTime startDate)
    {
        foreach (var festival in festivals)
        {
            if (festival.StartDate >= startDate)
            {
                Console.WriteLine(festival);
            }
        }
    }

    public void ShowFestivals(DateTime startDate, DateTime endDate)
    {
        foreach (var festival in festivals)
        {
            if (festival.StartDate >= startDate && festival.EndDate <= endDate)
            {
                Console.WriteLine(festival);
            }
        }
    }
}

public abstract class MySerializer
{
    public abstract void Read(string fileName);
    public abstract void Write(FestivalCalendar calendar, string fileName);
}

public class MyJsonSerializer : MySerializer
{
    public override void Read(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        FestivalCalendar calendar = JsonSerializer.Deserialize<FestivalCalendar>(jsonString);
        calendar.ShowFestivals();
    }

    public override void Write(FestivalCalendar calendar, string fileName)
    {
        string jsonString = JsonSerializer.Serialize(calendar);
        File.WriteAllText(fileName, jsonString);
    }
}

class Program
{
    static void Main(string[] args)
    {
        MusicFestival musicFestival1 = new MusicFestival
        {
            Name = "RockFest",
            Location = "New York",
            StartDate = new DateTime(2024, 6, 1),
            EndDate = new DateTime(2024, 6, 3),
            TotalAmount = 50000,
            TicketPrice = 50,
            Headliner = "Metallica",
            NumberOfBands = 10
        };

        ComicsFestival comicsFestival1 = new ComicsFestival
        {
            Name = "ComicCon",
            Location = "San Diego",
            StartDate = new DateTime(2024, 7, 10),
            EndDate = new DateTime(2024, 7, 12),
            TotalAmount = 100000,
            TicketPrice = 100,
            MainCharacter = "Spider-Man",
            NumberOfIssues = 20
        };

        FoodFestival foodFestival1 = new FoodFestival
        {
            Name = "Taste of Chicago",
            Location = "Chicago",
            StartDate = new DateTime(2024, 8, 15),
            EndDate = new DateTime(2024, 8, 17),
            TotalAmount = 80000,
            TicketPrice = 20,
            CuisineType = "American",
            NumberOfVendors = 50
        };

        FestivalCalendar calendar = new FestivalCalendar();
        calendar.AddFestival(musicFestival1);
        calendar.AddFestival(comicsFestival1);
        calendar.AddFestival(foodFestival1);

        MyJsonSerializer jsonSerializer = new MyJsonSerializer();
        jsonSerializer.Write(calendar, "raw_data.json");

        Console.WriteLine("All Festivals:");
        calendar.ShowFestivals();

        Console.WriteLine("\nFestivals from 01.07.2024:");
        calendar.ShowFestivals(new DateTime(2024, 7, 1));

        Console.WriteLine("\nFestivals from 01.07.2024 to 30.07.2024:");
        calendar.ShowFestivals(new DateTime(2024, 7, 1), new DateTime(2024, 7, 30));

        jsonSerializer.Read("raw_data.json");
    }
}
