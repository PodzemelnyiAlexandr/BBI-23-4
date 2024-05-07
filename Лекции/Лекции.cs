// // и в классе, и в структуре, есть поля и методы
// // в классе больше вещей, которые можно хранить

// //программа, позволяющая покупателю проверить наличие сумки в магазине и купить её.
// class Bag
// {
//     private string _model;// Encapsulation 
//     private int _size;// Encapsulation
//     public bool InStock { get; private set; }// Abstraction + Encapsulation
//     public int Price { get; private set; }// Abstraction + Encapsulation
//     public Bag(string model, int size) // Abstraction + Encapsulation
//     {
//         _model = model;
//         _size = size;
//         InStock = true;
//         Price = new Random().Next(10, 20) * _size;
//     }
//     public void Print() // Abstraction
//     {
//         if (InStock)
//             Console.WriteLine(_model + " with " + _size + " size costs:" + Price);
//     }

//     public bool Purchase(bool payment) // Abstraction + Encapsulation
//     {
//         if (!InStock)
//         {
//             Console.WriteLine("Sold out"); 
//             return false;
//         }
//         else if (!payment)
//         {
//             Console.WriteLine("Not enough money"); 
//             return false;
//         }
//         else
//         {
//             Console.Write($"You bought a bag \"{_model}\"\n"); //\"- add a" into the string \n - jump
//             InStock = false;
//             return true;
//         }
//     }
// }

// class Program
// {
//     private struct Customer // Encapsulation (only Program knows about Customer)
//     {
//         public string Name { get; private set; } // Abstraction + Encapsulation 
//         private int _money; // Encapsulation
//         public Customer(string name) // Abstraction
//         {
//             Name = name;
//             _money = new Random().Next(500, 1000);
//         }
//         public bool Pay(int price) // Abstraction + Encapsulation
//         {
//             if (_money > price)
//             {
//                 _money -= price;
//                 return true;
//             }
//             else
//                 return false;
//         }
//     }

//     static void Main()
//     {
//         Customer me = new Customer("Mark");
//         Console.WriteLine(me.Name);

//         Bag[] bags =
//         {
//             new Bag("A", 24),
//             new Bag("A4", 28),
//             new Bag("BiX", 21),
//             new Bag("S7", 27)
//         };

//         foreach (Bag bag in bags) 
//             bag.Print();
//         foreach (Bag bag in bags)
//             bag.Purchase(me.Pay(bag.Price));
//         /* same:    if (me.Pay(bag.Price)) 
//         *               bag.Purchase(true);      */
//         Console.WriteLine("Rest bags:");
//         foreach (Bag bag in bags) 
//             bag.Print();
//     }
// }


// using System.Text.Json;
// using System.Text.Json.Serialization;

// #region общие элементы для 2-х заданий по строкам должны быть в базовом классе
// abstract class Task
// {
//     protected string text = "No text here yet";
//     // для десериализации обязательно прописывайте свойства для всех полей
//     public string Text
//     {
//         get => text;
//         protected set => text = value;
//     }
//     public Task(string text)
//     {
//         this.text = text;
//     }
// }
// #endregion 
// #region помечайте конструктор, который будет использоваться для десериализации (конструкторов может быть несколько в классе)
// class Task1 : Task
// {
//     [JsonConstructor]
//     public Task1(string text) : base(text) { }
//     public override string ToString()
//     {
//         return text;
//     }
// }
// class Task2 : Task
// {
//     private int amount = 1; 
//     public int Amount
//     {
//         get => amount;
//         private set => amount = value;
//     }
//     [JsonConstructor]
//     public Task2(string text, int amount = 0) : base(text)
//     {
//         this.amount = amount;
//     }
//     public override string ToString()
//     {
//         return amount.ToString();
//     }
// }
// #endregion 
// class JsonIO
// {
//     #region для тех, кто хочет максимум, используйте обобщение:
//     public static void Write<T>(T obj, string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             JsonSerializer.Serialize(fs, obj);
//         }
//     }
//     public static T Read<T>(string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             return JsonSerializer.Deserialize<T>(fs);
//         }
//         return default(T);
//     }
//     #endregion

//     #region для тех, кто запутался с обобщением, можно так (но это -1 балл):
//     public static void Write1(Task1 obj, string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             JsonSerializer.Serialize(fs, obj);
//         }
//     }
//     public static Task1 Read1(string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             return JsonSerializer.Deserialize<Task1>(fs);
//         }
//         return null;
//     }
//     public static void Write2(Task2 obj, string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             JsonSerializer.Serialize(fs, obj);
//         }
//     }
//     public static Task2 Read2(string filePath)
//     {
//         using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
//         {
//             return JsonSerializer.Deserialize<Task2>(fs);
//         }
//         return null;
//     }
//     #endregion
// }
// class Program
// {
//     static void Main()
//     {
//         #region создаете массив заданий (даже, если сделали всего одно) (2 задания по 5 баллов)
//         Task[] tasks = {
//             new Task1("Example"),       // решаете 1е задание
//             new Task2("Example", 25)    // решаете 2е задание
//         };
//         Console.WriteLine(tasks[0]);
//         Console.WriteLine(tasks[1]);
//         #endregion

//         #region 3е задание на работу с папками и файлами (5 баллов)
//         string path = @"C:\Log"; // исходную папку ищем в компьютере
//         string folderName = "Solution"; // если нужно создать подпапку
//         path = Path.Combine(path, folderName);
//         if (!Directory.Exists(path))    // создать отсутствующую подпапку
//         {
//             Directory.CreateDirectory(path);
//         }
//         string fileName1 = "cw_task_1.json"; // имена файлов
//         string fileName2 = "cw_task_2.json";

//         fileName1 = Path.Combine(path, fileName1);
//         fileName2 = Path.Combine(path, fileName2);

//         #region для тех, кто не делает 4е задание с сериализацией, создаете пустые файлы
//         if (!File.Exists(fileName1))
//         {
//             File.Create(fileName1);
//         }
//         #endregion

//         #endregion

//         #region 4е задание на JSON сериализацию (5 баллов)
//         if (!File.Exists(fileName2)) // создаем файл, если его нет
//         {
//             JsonIO.Write<Task1>(tasks[0] as Task1, fileName1);  // можно так приводить к нужному типу
//             JsonIO.Write<Task2>((Task2)tasks[1], fileName2);    // а можно так
//         }
//         else // читаем файл (если меняли логику заданий, то удалите старые файлы!)
//         {
//             var t1 = JsonIO.Read<Task1>(fileName1);
//             var t2 = JsonIO.Read<Task2>(fileName2);
//             Console.WriteLine(t1);
//             Console.WriteLine(t2);
//         }
//         #endregion

//     }
// }

// using System.Diagnostics.Contracts;
// using System.Net.Sockets;
// using System.Security.Cryptography;
// using System.Text.Json;
// using System.Text.Json.Serialization;

// class Person 
// {
//     public string name {get; set; }
//     public int age {get; set; }
//     public bool IsStudent {get; set; }
//     public Address Address {get; set; }
//     public string[] Hobbies {get; set; }
//     [JsonConstructor]
//     public Person(string name, int age, bool IsStudent, Address Address, string[] Hobbies)
//     {
//         this.name = name;
//         this.age = age;
//         this.IsStudent = IsStudent;
//         this.Address = Address;
//         this.Hobbies = Hobbies;
//     }
// }
// class Address
// {
//     public string Street {get; set; }
//     public string City {get; set; }
// }
// class Program
// {
//     static void Main()
//     {
//         var person = new Person
//         (
//             "John Doe",
//             30,
//             false,
//             new Address {Street = "123 main st", City = "Town"},
//             new[] { "reading", "hiking"}
//         );
//         string json = JsonSerializer.Serialize(person);
//         Console.WriteLine(json);
//     }

// }

// int last = 1;
// int first;
// first = ++last;
// Console.WriteLine("last is " + last);
// Console.WriteLine("First is " + first);

//После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";

// string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
// text = text.Insert(text.Length, " ");
// string[] lines = new string[(text.Length + 1) / 25];
// int first = 0, last = 49, i = 0;
// while (last < text.Length + 50)
// {
//     if ((last >= text.Length) || (text[last] != ' ')) last--;
//     else 
//     {
//         if (last > first) lines[i++] = text.Substring(first, last - first);
//         first = ++last; 
//         last += 50;
//     }
// }

// foreach (string a in lines) Console.WriteLine(a);

// static void AddSpace(string a)
// {
//     int i = 0;
//     while (a.Length != 50)
//     {
//         if (a[i] == ' ') a = a.Insert(i++, " ");
//         if (i < a.Length - 1) i++;
//         else i = 0;
//     }
// }

// for (int j = 0; j < lines.Length; j++)
// {
//     AddSpace(lines[j]);
//     Console.WriteLine(lines[j]);
// }

// char[] letters = new char[1999];
// for (int i = 0; i < 1999; i++) 
// {
//     letters[i] = (char)i;
//     Console.Write(letters[i] + " ");
// }
// Console.WriteLine();
// static char[] GetUniqueLetters(char[] letters, string text)
//     {
//         int count = 0;
//         for (int i = 0; i < letters.Length; i++)
//         {
//             for (int j = 0; j < text.Length; j++)
//             {
//                 if (letters[i] == text[j])
//                 {
//                     for (int k = i; k < letters.Length - 1; k++) // переделать в метод
//                     {
//                         letters[k] = letters[k+1];
//                     }
//                     count++;
//                 }
//             }
//         }
//         char[] newletters = new char[letters.Length - count];
//         for (int i = 0; i < letters.Length - count; i++) newletters[i] = letters[i];
//         return newletters;
//     }

// string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";

// letters = GetUniqueLetters(letters, text);

// foreach(char a in letters) Console.Write(a + " ");
// static string[] Words(string text)
//     {
//         char[] pattern = { ' ', ',', '.', '!', ';', ':', '?', '-', '(', ')' };
//         text = text.Replace(". ", ".").Replace(", ", ",").Replace("! ", "!").Replace("; ", ";").Replace(": ", ":").Replace("? ", "?").Replace("- ", "-").Replace("( ", "(").Replace(") ", ")");
//         string[] words = text.Split(pattern);
//         return words;
//     }
// static bool InArray(string[] array, string a)
// {
//     for (int i = 0; i < array.Length; i++) if (array[i] == a)
//                 return true;
//         return false;
// }
// static char[] GetUniqueLetters(string text, int chars)
//     {
//         char[] letters = new char[chars];
//         for (int i = 33; i < chars + 33; i++) letters[i-33] = (char)i;
//         int count = 0;
//         for (int i = 0; i < letters.Length; i++)
//         {
//             for (int j = 0; j < text.Length; j++)
//             {
//                 if (letters[i] == text[j])
//                 {
//                     for (int k = i; k < letters.Length - 1; k++) // переделать в метод, если будет еще где-то использоваться
//                     {
//                         letters[k] = letters[k+1];
//                     }
//                     count++;
//                 }
//             }
//         }
//         char[] newletters = new char[letters.Length - count];
//         for (int i = 0; i < letters.Length - count; i++) newletters[i] = letters[i];
//         return newletters;
//     }

// static string[] GetPairs(string text, int frequency) // Возвращает все пары, встречающиеся frequency раз или чаще, в порядке убывания
// {
//     string[] words = Words(text), pairs = new string[text.Length];
//     int[] counts = new int[text.Length];
//     int k = 0;
//     foreach (string word in words)
//     {
//         for (int i = 0; i < word.Length - 1; i++)
//         {
//             string pair = word.Substring(i, 2);
//             if (!InArray(pairs, pair))
//             {
//                 int count = 0;
//                 bool flag = false;
//                 for (int j = 0; j < text.Length - 1; j++)
//                 {
//                     if (pair == text.Substring(j, 2)) count++;
//                     if (count >= frequency && k < pairs.Length) 
//                     {
//                         flag = true;
//                         pairs[k] = pair;
//                         counts[k] = count;
//                     }
//                 }
//                 if (flag) k++;
//             }
//         }
//     }
//     int[] newcounts = new int[k];
//     for (int i = 0; i < k; i++) newcounts[i] = counts[i];
//     string[] newpairs = new string[k];
//     for (int i = 0; i < k; i++) newpairs[i] = pairs[i];
//     for (int i = 1; i < k; i++)
//         {
//             string tempP = newpairs[i];
//             int tempC = newcounts[i];
//             int j = i - 1;
//             while (j >= 0 && newcounts[j] < tempC)
//             {
//                 newpairs[j + 1] = newpairs[j];
//                 newcounts[j + 1] = newcounts[j];
//                 j--;
//             }
//         newpairs[j + 1] = tempP;
//         newcounts[j + 1] = tempC;
//         }
//     return newpairs;
// }


// string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
// int frequency = 4;

// string[] pairs = GetPairs(text, frequency);
// char[] letters = GetUniqueLetters(text, pairs.Length);
// foreach (string a in pairs) Console.Write(a + " ");
// foreach (char a in letters) Console.Write(a + " ");
// for (int i = 0; i < pairs.Length; i++) if (i < letters.Length)
//     {
//         text = text.Replace(pairs[i], new string(letters[i], 1));
//     }
// Console.WriteLine(text);
using System.Linq.Expressions;
using System.Reflection.Metadata;

// static string[] Words(string text)
//     {
//         char[] pattern = { ' ', ',', '.', '!', ';', ':', '?', '–', '(', ')' };
//         if (Char.IsPunctuation(text[text.Length - 1])) text = text.Remove(text.Length - 1, 1);
//         text = text.Replace("'", " ");
//         string[] words = text.Split(pattern, System.StringSplitOptions.RemoveEmptyEntries);
//         return words;
//     }

// char[] letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
// int[] counts = new int[33];
// string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
// for (int i = 0; i < text.Length; i++) if (Char.IsUpper(text[i])) text = text.Replace(text[i], Char.ToLower(text[i]));
// string[] temp = Words(text);
// text = "";
// foreach (string a in temp){
// text += a + " "; Console.WriteLine(a);}
// for (int i = 35; i < 68; i++)
//     text = text.Replace(letters[i - 35], (char)i);
// string[] words = text.Split(" ");
// Console.WriteLine(text);
// Console.WriteLine(counts[0]);
// for (int i = 0; i < words.Length - 1; i++)
// {
//     Console.WriteLine(words[i]);
//     counts[words[i][0] - 35]++;
// }
// Console.WriteLine("{0,-10:s}{1:s}", "Буква", "Доля");
// for (int i = 0; i < letters.Length; i++)
// {
//     Console.WriteLine("{0,-10:s}{1:f3}", letters[i], (double)counts[i] / (words.Length - 1));
// }
    


// string a = "325/";
// Console.WriteLine(a[-1]);

// string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";

// for (int i = 35; i < 68; i++)
// {
//     text = text.Replace(letters[i - 35], (char)i);
//     Console.WriteLine((char)i);
// }
// Console.WriteLine(text);

// string text = "William Shakespeare, widely regarded as one of the greatest writers in the English language, authored a total of 37 plays, along with numerous poems and sonnets. He was born in Stratford-upon-Avon, England, in 1564, and died in 1616. Shakespeare's most famous works, including 'Romeo and Juliet,' 'Hamlet,' 'Macbeth,' and 'Othello,' were written during the late 16th and early 17th centuries. 'Romeo and Juliet,' a tragic tale of young love, was penned around 1595. 'Hamlet,' one of his most celebrated tragedies, was written in the early 1600s, followed by 'Macbeth,' a gripping drama exploring themes of ambition and power, around 1606. 'Othello,' a tragedy revolving around jealousy and deceit, was also composed during this period, believed to be around 1603.";

// char[,] letters = 
//         {
//             { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'},
//             { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '$', '$', '$', '$', '$', '$', '$' }
//         };
// int language = 0;
// for (int i = 0; i < text.Length; i++)
// {
//     if (Char.IsLetter(text[i]))
//     {
//         if (text[i] > 64 && text[i] < 123)
//         {
//             language = 1;
//             break;
//         }
//     }
// }
// int[] counts = new int[33];
// for (int i = 0; i < text.Length; i++) if (Char.IsUpper(text[i])) text = text.Replace(text[i], Char.ToLower(text[i]));
// string[] temp = Words(text);
// text = "";
// foreach (string a in temp) text += a + " "; 
// for (int i = 35; i < 68; i++)
//     text = text.Replace(letters[language, i - 35], (char)i);
// string[] words = text.Split(" ");
// Console.WriteLine(text);
// for (int i = 0; i < words.Length - 1; i++)
//     if ((words[i][0] - 35) < 33) counts[words[i][0] - 35]++;
// Console.WriteLine("{0,-10:s}{1:s}", "Буква", "Доля");
// if (language == 0)
//     for (int i = 0; i < 33; i++)
//         Console.WriteLine("{0,-10:s}{1:f3}", letters[language, i], (double)counts[i] / (words.Length - 1));
// else
//     for (int i = 0; i < 26; i++)
//         Console.WriteLine("{0,-10:s}{1:f3}", letters[language, i], (double)counts[i] / (words.Length - 1));

// string text = "1 июля 2015 года Греция объявила о дефолте по государственному долгу, став первой развитой страной в истории, которая не смогла выплатить свои долговые обязательства в полном объеме. Сумма дефолта составила порядка 1,6 миллиарда евро. Этому предшествовали долгие переговоры с международными кредиторами, такими как Международный валютный фонд (МВФ), Европейский центральный банк (ЕЦБ) и Европейская комиссия (ЕК), о программах финансовой помощи и реструктуризации долга. Основными причинами дефолта стали недостаточная эффективность реформ, направленных на улучшение финансовой стабильности страны, а также политическая нестабильность, что вызвало потерю доверия со стороны международных инвесторов и кредиторов. Последствия дефолта оказались глубокими и долгосрочными: сокращение кредитного рейтинга страны, увеличение затрат на заемный капитал, рост стоимости заимствований и утрата доверия со стороны международных инвесторов. ";
// Console.WriteLine(text.Length);

// static void AddSpaces(ref string a)
//     {
//         int i = 0, k = 0;
//         if (a != null) 
//         {
//             int length = a.Length;        
//             while (length != 50)
//             {
//                 if (a[i] == ' ') 
//                 {
//                     a = a.Insert(i++, " ");
//                     k++;
//                 }
//                 if (i < a.Length - 1) i++;
//                 else 
//                 {
//                     i = 0;
//                     if (k == 0) return;
//                 }
//                 length = a.Length;
//             }
//         }
//     }

// string[] lines = new string[(text.Length + 1) / 25];
// int first = 0, last = 49, i = 0;
// while (last < text.Length + 50)
// {
//     if ((last >= text.Length) || (text[last] != ' ')) last--;
//     else 
//     {
//         if (last > first) lines[i++] = text.Substring(first, last - first);
//         first = ++last; 
//         last += 50;
//     }
// }
// for (int j = 0; j < lines.Length; j++)
// {
//     AddSpaces(ref lines[j]);
//     Console.WriteLine(lines[j]);
// }
    
string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";

// char[,] letters = 
//         {
//             { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'},
//             { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '$', '$', '$', '$', '$', '$', '$' }
//         };
//         int language = 0;
//         for (int i = 0; i < text.Length; i++)
//         {
//             if (Char.IsLetter(text[i]))
//             {
//                 if (text[i] > 64 && text[i] < 123)
//                 {
//                     language = 1;
//                     break;
//                 }
//                 else break;
//             }
//         }
//         int[] counts = new int[33];
//         for (int i = 0; i < text.Length; i++) if (Char.IsUpper(text[i])) text = text.Replace(text[i], Char.ToLower(text[i]));
//         string[] temp = Words(text);
//         text = "";
//         foreach (string a in temp) text += a + " "; 
//         for (int i = 35; i < 68; i++)
//             text = text.Replace(letters[language, i - 35], (char)i);
//         string[] words = text.Split(" ");
//         for (int i = 0; i < words.Length - 1; i++)
//             if ((words[i][0] - 35) < 33) counts[words[i][0] - 35]++;
//         Console.WriteLine("{0,-10:s}{1:s}", "Буква", "Доля");
//         if (language == 0)
//             for (int i = 0; i < 33; i++)
//                 if (counts[i] != 0) 
//                     Console.WriteLine("{0,-10:s}{1:f3}", letters[language, i], (double)counts[i] / (words.Length - 1));
//                 else continue;
// static char[] GetUniqueLetters(string text, int chars)
//     {
//         char[] letters = new char[chars];
//         int n = 35;
//         for (int i = 35; i < chars + 35; i++) letters[i-35] = (char)n++;
//         int count = 0;
//         for (int i = 0; i < letters.Length; i++)
//         {
//             for (int j = 0; j < text.Length; j++)
//             {
//                 if (letters[i] == text[j])
//                 {
//                     for (int k = i; k < letters.Length - 1; k++) // переделать в метод, если будет еще где-то использоваться
//                     {
//                         letters[k] = letters[k+1];
//                     }
//                     letters[letters.Length - 1] = (char)n++;
//                     count++;
//                 }
//             }
//         }
//         char[] newletters = new char[letters.Length - count];
//         for (int i = 0; i < letters.Length - count; i++) newletters[i] = letters[i];
//         return newletters;
//     }

// string[] codes = { "древесины", "и", "в", "движение", "дефолта", "со", "международных", "кредиторов", "стороны", "Фьорды", "and", "a", "the"};
// char[] letters = GetUniqueLetters(text, 30);
// string[] array = text.Split(" ");
// for (int i = 0; i < array.Length; i++)
// {
//     string temp = "";
//     foreach (char a in array[i]) if (Char.IsLetter(a)) temp += a;
//     for (int j = 0; j < codes.GetLength(0); j++)
//     {
//         if (temp == codes[j]) array[i] = array[i].Replace(codes[j], letters[j].ToString());
//     }
// }

// Console.WriteLine("Закодированный текст:");
// foreach (string a in array) Console.Write(a + " ");
// Console.WriteLine();
// Console.WriteLine("\nРаскодированный текст:");
// for (int i = 0; i < array.Length; i++)
// {
//     if (!char.IsLetter(array[i][0])) 
//         for (int j = 0; j < codes.GetLength(0); j++)
//             if (array[i][0] == letters[j]) array[i] = array[i].Replace(letters[j].ToString(), codes[j]);
//     Console.Write(array[i] + " ");
// }
