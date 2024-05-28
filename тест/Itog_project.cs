using System;
using System.Collections.Generic;
using System.IO;
using Classes;
using Serializers;
public class Program
{
    static void Main(string[] args)
    {
        Random r = new Random();
        string[] names = { "Potato", "Cabbage", "Roots", "Nuts", "Fear", "Steel", "Flex", "Chill", "Rave", "Flow", "Intro", "Outro", "Jejesusu", "SSS", "$$$", "Punk" };
        string[] cities = { "New York", "San Diego", "Chicago", "Moscow", "Rio", "Astana", "London", "Los Angeles", "Hollywood", "Samara", "Irkutsk", "Barnaul", "Brisben" };
        string[] stars = { "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] characters = { "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] cuicines = { "", "", "", "", "", "", "", "", "", "", "", "" };
        var festivals = new List<Festival> { };
        for (int i = 0; i < 10; i++)
        {
            int y1 = r.Next(2020, 2023);
            int m1 = r.Next(1, 11);
            int d1 = r.Next(1, 28);
            int y2 = r.Next(y1 + 1, 2024);
            int m2 = r.Next(m1 + 1, 12);
            int d2 = r.Next(d1 + 1, 29);
            festivals.Add(new MusicFestival(names[r.Next(0, names.Length - 1)], cities[r.Next(0, cities.Length-1)], new DateTime(y1, m1, d1), new DateTime(y2, m2, d2), r.Next(10, 30), stars[r.Next(0, stars.Length - 1)], r.Next(1,10)));
        }
        for (int i = 0; i < 10; i++)
        {
            int y1 = r.Next(2020, 2024);
            int m1 = r.Next(1, 12);
            int d1 = r.Next(1, 30);
            int y2 = r.Next(y1, 2024);
            int m2 = r.Next(m1, 12);
            int d2 = r.Next(d1, 30);
            festivals.Add(new ComicsFestival(names[r.Next(0, names.Length - 1)], cities[r.Next(0, cities.Length - 1)], new DateTime(y1, m1, d1), new DateTime(y2, m2, d2), r.Next(10, 30), characters[r.Next(0, characters.Length - 1)], r.Next(1, 10)));
        }
        for (int i = 0; i < 10; i++)
        {
            int y1 = r.Next(2020, 2024);
            int m1 = r.Next(1, 12);
            int d1 = r.Next(1, 30);
            int y2 = r.Next(y1, 2024);
            int m2 = r.Next(m1, 12);
            int d2 = r.Next(d1, 30);
            festivals.Add(new FoodFestival(names[r.Next(0, names.Length - 1)], cities[r.Next(0, cities.Length - 1)], new DateTime(y1, m1, d1), new DateTime(y2, m2, d2), r.Next(10, 30), characters[r.Next(0, characters.Length - 1)], r.Next(1, 10)));
        }
        FestivalCalendar calendar = new FestivalCalendar();
        foreach (var festival in festivals)
        {
            calendar.AddFestival(festival);
        }
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderName = "Files";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string file = Path.Combine(path, "raw_data.json");

        MyJsonSerializer jsonSerializer = new MyJsonSerializer();
        if (File.Exists(file))
        {
            File.Delete(file);
        }
        jsonSerializer.Write(calendar, file);

        Console.WriteLine("All Festivals:");
        calendar.ShowFestivals();

        Console.WriteLine("\nFestivals from 01.04.2023:");
        calendar.ShowFestivals(new DateTime(2023, 4, 1));

        Console.WriteLine("\nFestivals from 20.05.2020 to 21.09.2021:");
        calendar.ShowFestivals(new DateTime(2020, 5, 20), new DateTime(2021, 9, 21));
        Console.WriteLine("ReadFile");
        jsonSerializer.Read(file);
        Console.ReadKey();
    }
}
