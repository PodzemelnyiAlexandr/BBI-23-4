using ProtoBuf;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Tester.Serializers;
[Serializable]
[ProtoContract]
[ProtoInclude(4, typeof(Car))]
[ProtoInclude(5, typeof(Motocycle))]
public abstract class Vehicle
{
    [JsonIgnore]
    [NonSerialized]
    protected string _model;
    [JsonIgnore]
    [NonSerialized]
    protected int _length;
    [JsonIgnore]
    [NonSerialized]
    protected int _width;
    [JsonIgnore]
    [NonSerialized]
    protected double _weight;
    [JsonIgnore]
    [NonSerialized]
    private static int _counter = 10; // общая для всех переменных (для счетчиков)
    [JsonIgnore]
    public int Size { 
        get { return _width * _length; }
    }
    [ProtoMember(1)]
    [JsonInclude]
    public string Name
    {
        get { return _model; }
        set { _model = value ?? string.Empty; }
    }
    [JsonInclude]
    [ProtoMember(2)]
    public int Length
    {
        get { return _length; }
        set { _length = value; }
    }
    [JsonInclude]
    [ProtoMember(3)]
    public int Width
    {
        get { return _width; }
        set { _width = value;
            _weight = Length * Width / 5.0;
        }
    }
    public Vehicle() { }
    public Vehicle(string model, int length, int width, double weight) // Abstraction & Encapsulation
    {
        _model = model;
        _length = length;
        _width = width;
        _weight = weight;
    }
    [JsonConstructor]
    public Vehicle(string Name, int Length, int Width) : this(Name, Length, Width, Length * Width / 5.0) { }
    public virtual void Print() // will search logic change in heirs / будет искать переопределенную логику у наследников при вызове
    {
        _counter++;
        Console.WriteLine($"Model: {_model}, length: {_length}, {_width}, {_weight}");
    }
    public static void PrintCounter() => Console.WriteLine(_counter);
}
[Serializable]
[ProtoContract]
public class Car : Vehicle
{
    public Car(): base() { }
    public readonly int id; // object's id / ид для конкретного экземпляра
    private static int _id; // counter for objects / поле, которое будет вести подсчет для всех экземлпяров
    public Car(string model, int length, int width, double weight) : base(model, length, width, weight)
    {
        _id++;
        id = _id;
    }
    [JsonConstructor]
    public Car(string Name, int Length, int Width) : this(Name, Length, Width, Length * Width / 5.0) { }
    public override void Print() // change the heir logic / изменяем логику вывода в классе-наследнике
    {
        Console.WriteLine($"Car {id} model: {_model}, length: {_length}, {_width}, {_weight}");
    }
}
[Serializable]
[ProtoContract]
public class Motocycle : Vehicle
{
    public Motocycle(): base() { }
    [JsonConstructor]
    public Motocycle(string Name, int Length, int Width) : base(Name, Length, Width) { }
    private string _gasType;
    // got 5 variables for this constructor, sent 4 for parent / получил 5 переменных для этого конструктора, отправил 4 для родителя
    public Motocycle(string model, int length, int width, double weight, string gasType) : base(model, length, width, weight)
    {
        _gasType = gasType;
    }
}
class Program
{
    static void Main()
    {
        Car[] cars = new Car[4]
            {
                new Car("Tesla", 236, 78, 1389.54),
                new Car("Ford", 278, 80, 1469.5),
                new Car("Masda", 204, new Random().Next(50, 80), 1999.54),
                new Car("Toyota", 255, 98, 1440.00),
            };
        Motocycle[] motocycles = new Motocycle[4]
            {
                new Motocycle("Tesla", 236, 78, 1389.54, "gas"), // new field for heir-class constructor / новое поле для конструктора класса-наследника
                new Motocycle("Ford", 278, 80, 1469.5, "gas"),
                new Motocycle("Masda", 204, new Random().Next(50, 80), 1999.54, "gasolin"),
                new Motocycle("Toyota", 255, 98, 1440.00, "propan"),
            };

        Sort(cars);
        Sort(motocycles);

        foreach (var car in cars) // var = Car
        {
            car.Print();
        }
        foreach (var motocycle in motocycles) // var = Motocycle
        {
            motocycle.Print();
        }

        Vehicle[] vehicles = new Vehicle[cars.Length + motocycles.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            vehicles[i] = cars[i]; // converts heir class' object to the base / преобразует экземпляр класса-наследника в базовый
        }
        for (int i = 0; i < motocycles.Length; i++)
        {
            vehicles[i + cars.Length] = motocycles[i]; // here the same / тут тоже
        }
        foreach (var vehicle in vehicles) // var = Motocycle
        {
            vehicle.Print();
        }

        #region serialization
        
        MySerializer[] serializers = new MySerializer[3]
        {
            new JsonMySerializer(),
            new XmlMySerializer(),
            new BinMySerializer()
        };

        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folder = "Example";
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] file_names = new string[]
        {
            "example.json",
            "example.xml",
            "example.bin"
        };

        for (int i = 0; i < cars.Length - 1; i++)
        {
            serializers[i].Write(cars, Path.Combine(path, file_names[i]));
        }

        
        for (int i = 0; i < cars.Length - 1; i++)
        {
            cars = serializers[i].Read<Car[]>(Path.Combine(path, file_names[i]));
            Console.WriteLine($"From {file_names[i]}");
            foreach (var car in cars) // var = Car
            {
                car.Print();
            }
        }



        #endregion
    }
    static void Sort(Vehicle[] cars) // converts any heir class to the base / преобразует любой класс-наследник в базовый
    {
        for (int i = 0; i < cars.Length - 1; i++) // from 1 to n-1
        {
            for (int j = i + 1; j < cars.Length; j++) // from next to n
            {
                if (cars[i].Size > cars[j].Size)
                {
                    Vehicle temp = cars[i];
                    cars[i] = cars[j];
                    cars[j] = temp;
                }
            }
        }
    }
}