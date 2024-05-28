// using ConsoleApp44.Serialazer;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Security.Policy;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using System.Xml.Serialization;

// namespace ConsoleApp44
// {
//     internal class Program
//     {
//         static void Main(string[] args)
//         {
//             NaturalDisaster[] naturalDisasters = 
//             {
//                 new NaturalDisaster($"Disaster 1", new Earthquake("Землятресение", " - ", 9), "Япония", 2022),
//                 new NaturalDisaster($"Disaster 2", new Earthquake("Землятресение", " - ", 5), "Китай", 2002),
//                 new NaturalDisaster($"Disaster 3", new Earthquake("Землятресение", " 1 погибший ", 7), "Чили", 2011),
//                 new NaturalDisaster($"Disaster 4", new Earthquake("Землятресение", " - ", 5), "Непал", 2005),
//                 new NaturalDisaster($"Disaster 5", new Earthquake("Землятресение", " - ", 6), "Китай", 1994),
//                 new NaturalDisaster($"Disaster 6", new Hurricane("Ураган", " - ", 2), "Катрина", 1999),
//                 new NaturalDisaster($"Disaster 7", new Hurricane("Ураган", " 3 погибших ", 3), "Харви", 2001),
//                 new NaturalDisaster($"Disaster 8", new Hurricane("Ураган", " - ", 4), "Дориан", 2002),
//                 new NaturalDisaster($"Disaster 9", new Hurricane("Ураган", " - ", 3), "Ида", 2017),
//                 new NaturalDisaster($"Disaster 10", new Hurricane("Ураган", " - ", 1), "Дизан", 2009),
//                 new NaturalDisaster($"Disaster 11", new Fire("Пожар", " - ", 5), "Калифорния", 2023),
//                 new NaturalDisaster($"Disaster 12", new Fire("Пожар", " - ", 7), "Австралия", 1992),
//                 new NaturalDisaster($"Disaster 13", new Flood("Наводнение", " - ", 9), "Луизиана", 2020),
//                 new NaturalDisaster($"Disaster 14", new Flood("Наводнение", " 13 погибших ", 5), "Пакистан", 2021),
//                 new NaturalDisaster($"Disaster 15", new Flood("Наводнение", " - ", 7), "Индонезия", 2001),
//                 new NaturalDisaster($"Disaster 16", new Earthquake("Землятресение", " - ", 2), "Япония", 2023),
//                 new NaturalDisaster($"Disaster 17", new Earthquake("Землятресение", " - ", 2), "Китай", 2004),
//                 new NaturalDisaster($"Disaster 18", new Earthquake("Землятресение", " - ", 3), "Чили", 2015),
//                 new NaturalDisaster($"Disaster 19", new Earthquake("Землятресение", " 4 погибших ", 4), "Непал", 2017),
//                 new NaturalDisaster($"Disaster 20", new Earthquake("Землятресение", " - ", 3), "Китай", 1998)
//             };

//             int choice;
//             do
//             {
//                 ShowMenu();
//                 choice = Convert.ToInt32(Console.ReadLine());
//                 DoAction(choice, naturalDisasters);
//             }while (choice != 3);
//         }

//         public static void ShowMenu() 
//         {
//             Console.WriteLine("1 - запись в json\n2 - запись в xml\n3 - выйти\n");
//         }

//         public static void RecordingDataJson( NaturalDisaster[] naturalDisasters)
//         {
//             DisasterList disasterList = new DisasterList();
//             for (int i = 0; i < 15; i++)
//             {
//                 disasterList.AddDisaster(naturalDisasters[i]);
//             }
//             Console.WriteLine("Чтение данных из файла raw_data.json:");
//             DoJson(disasterList, "raw_data");

//             for (int i = 15; i < 20; i++)
//             {
//                 disasterList.AddDisaster(naturalDisasters[i]);
//             }

//             for (int i = 15; i < 20; i++)
//             {
//                 disasterList.AddDisaster(disasterList.GetElementFromList(i));
//             }
//             Console.WriteLine("Чтение данных из файла data.json:");
//             DoJson(disasterList, "data");
//         }

//         public static void RecordingDataXml(NaturalDisaster[] naturalDisasters)
//         {
//             DisasterList disasterList = new DisasterList();
//             disasterList.AddDisaster(naturalDisasters);

//             DisasterList disasterList2 = new DisasterList();
//             DisasterList disasterList3 = new DisasterList();
//             foreach (var disaster in disasterList.GetDisastersList())
//             {
//                 disasterList2.AddDisaster(disaster, 1990);
//             }

//             Console.WriteLine("Чтение данных (2000-x) из файла raw_data.xml:");
//             disasterList.RemoveDisaster(2000);
//             DoXml(disasterList, "raw_data");


//             Console.WriteLine("Чтение данных (1990-x) из файла data.xml:");
//             DoXml(disasterList2, "data");


//             Console.WriteLine("Чтение данных (отсортированных по ущербу 1990-x) из файла sort_data.xml:");
//             disasterList2.SortingByDamage();
//             DoXml(disasterList2, "sort_data");
//         }

//         public static void DoAction(int choice, NaturalDisaster[] naturalDisasters) 
//         {
           
//             switch (choice) 
//             {
//                 case 1:
//                     RecordingDataJson(naturalDisasters);   
//                     break;
//                 case 2:
//                     RecordingDataXml(naturalDisasters);
//                     break;
//                 case 3:
//                     Console.WriteLine("Завершение программы");
//                     break;
//                 default:
//                     Console.WriteLine("Вы ввели некорректную команду");
//                     break;
//             }
//         }
//         public static void DoXml(DisasterList disasterList, string path)
//         {
//             MyXmlSerializer myXmlSerializer = new MyXmlSerializer();

//             myXmlSerializer.Serializing(disasterList, @"../xml/" + path);

//             DisasterList newTeam = myXmlSerializer.Deserializing<DisasterList>(@"../xml/" + path);

//             for (int i = 0; i < newTeam.GetDisastersList().Count; i++)
//             {
//                 Console.WriteLine(newTeam.GetDisastersList()[i].ToString());
//             }
//         }
//         public static void DoJson(DisasterList disasterList, string path) 
//         {
//             MyJsonSerializer jsonSerializer = new MyJsonSerializer();
//             string jsonData = "";

//             foreach (NaturalDisaster list in disasterList.GetDisastersList())
//             {
//                 jsonData += list.ToString();
//             }
//             jsonSerializer.Serializing(jsonData, path);

//             dynamic newJsonData = jsonSerializer.Deserializing<dynamic>(path);

//             Console.WriteLine(newJsonData);
//         }
//     }
// }
