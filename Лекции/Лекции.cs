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


using System.Text.Json;
using System.Text.Json.Serialization;

#region общие элементы для 2-х заданий по строкам должны быть в базовом классе
abstract class Task
{
    protected string text = "No text here yet";
    // для десериализации обязательно прописывайте свойства для всех полей
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
#endregion 
#region помечайте конструктор, который будет использоваться для десериализации (конструкторов может быть несколько в классе)
class Task1 : Task
{
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        return text;
    }
}
class Task2 : Task
{
    private int amount = 1; 
    public int Amount
    {
        get => amount;
        private set => amount = value;
    }
    [JsonConstructor]
    public Task2(string text, int amount = 0) : base(text)
    {
        this.amount = amount;
    }
    public override string ToString()
    {
        return amount.ToString();
    }
}
#endregion 
class JsonIO
{
    #region для тех, кто хочет максимум, используйте обобщение:
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
        return default(T);
    }
    #endregion

    #region для тех, кто запутался с обобщением, можно так (но это -1 балл):
    public static void Write1(Task1 obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static Task1 Read1(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<Task1>(fs);
        }
        return null;
    }
    public static void Write2(Task2 obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static Task2 Read2(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<Task2>(fs);
        }
        return null;
    }
    #endregion
}
class Program
{
    static void Main()
    {
        #region создаете массив заданий (даже, если сделали всего одно) (2 задания по 5 баллов)
        Task[] tasks = {
            new Task1("Example"),       // решаете 1е задание
            new Task2("Example", 25)    // решаете 2е задание
        };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);
        #endregion

        #region 3е задание на работу с папками и файлами (5 баллов)
        string path = @"C:\Log"; // исходную папку ищем в компьютере
        string folderName = "Solution"; // если нужно создать подпапку
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))    // создать отсутствующую подпапку
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "cw_task_1.json"; // имена файлов
        string fileName2 = "cw_task_2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);

        #region для тех, кто не делает 4е задание с сериализацией, создаете пустые файлы
        if (!File.Exists(fileName1))
        {
            File.Create(fileName1);
        }
        #endregion

        #endregion

        #region 4е задание на JSON сериализацию (5 баллов)
        if (!File.Exists(fileName2)) // создаем файл, если его нет
        {
            JsonIO.Write<Task1>(tasks[0] as Task1, fileName1);  // можно так приводить к нужному типу
            JsonIO.Write<Task2>((Task2)tasks[1], fileName2);    // а можно так
        }
        else // читаем файл (если меняли логику заданий, то удалите старые файлы!)
        {
            var t1 = JsonIO.Read<Task1>(fileName1);
            var t2 = JsonIO.Read<Task2>(fileName2);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
        }
        #endregion

    }
}

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