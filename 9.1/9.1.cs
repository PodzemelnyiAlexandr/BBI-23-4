using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ProtoBuf;

[JsonDerivedType(typeof(Person_of_the_year), typeDiscriminator: "Person of the year")]
[JsonDerivedType(typeof(Discovery_of_the_year), typeDiscriminator: "Discovery of the year")]
[XmlInclude(typeof(Person_of_the_year))]
[XmlInclude(typeof(Discovery_of_the_year))]
[ProtoInclude(5, typeof(Person_of_the_year))]
[ProtoInclude(6, typeof(Discovery_of_the_year))]
[ProtoContract]
public abstract class Person
{
    protected string nomination;
    protected string name;  // вернуть прайваты
    protected int calls;
    protected double prop;
    [ProtoMember(1)]
    public string Nomination
    {
        get { return nomination; }
        set { nomination = value ?? string.Empty; }
    }
    [ProtoMember(2)]
    public string Name
    {
        get { return name; }
        set { name = value ?? string.Empty; }
    }
    [ProtoMember(3)]
    public int Calls
    {
        get { return calls; }
        set { calls = value; }
    }
    [ProtoMember(4)]
    public double Prop
    {
        get { return prop; }
        set { prop = value; }
    }
    public Person() { }
    public Person(string _name, int _calls)
    {
        name = _name;
        calls = _calls;
        nomination = "None";
    }
    
    public void Proportion(int count)
    {
        prop = (double) calls * 100 / count;
    }
    public void Print()
    {
        Console.WriteLine("{0,-40:s}{1,-20:f0}{2:f3}", name, calls, prop);
    }
    public static void Sort(Person[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            Person k = a[i];
            int j = i - 1;
            while (j >= 0 && a[j].calls < k.calls)
            {
                a[j + 1] = a[j];
                j--;
            }
        a[j + 1] = k;
        }
    }
}
[ProtoContract]
public class Person_of_the_year : Person
{
    public Person_of_the_year() : base() { }
    public Person_of_the_year(string _name, int _calls) : base(_name, _calls) 
    {
        nomination = "Person of the year";
    }
}
[ProtoContract]
public class Discovery_of_the_year : Person
{
    public Discovery_of_the_year() : base() { }
    public Discovery_of_the_year(string _name, int _calls) : base(_name, _calls) 
    {
        nomination = "Discovery of the year";
    }
}
public class Program
{
    static void Main()
    { 
        Person_of_the_year[] people = 
        {
            new Person_of_the_year("Pyotr", 110),
            new Person_of_the_year("Ivan", 40),
            new Person_of_the_year("Ruslan", 60),
            new Person_of_the_year("Vladimir", 150),
            new Person_of_the_year("Fyodor", 73)
        };
        Person.Sort(people);
        int count = 0;
        foreach (Person_of_the_year a in people) count += a.Calls;
        Console.WriteLine("Имя участника номинации '{0,-16:s}'{1,-20:s}{2:s}", "'Человек года'", "Количество голосов", "Доля от общего количества ответов");
        for (int i = 0; i < people.Length; i++)
        {
            people[i].Proportion(count);
            people[i].Print();
        }
        Console.WriteLine();

        Discovery_of_the_year[] discoveries =
        {
            new Discovery_of_the_year("Igor", 56),
            new Discovery_of_the_year("Valentin", 15),
            new Discovery_of_the_year("Artyom", 187),
            new Discovery_of_the_year("Sebastian", 93),
            new Discovery_of_the_year("Arian", 213)
        };
        Person.Sort(discoveries);
        count = 0;
        foreach (Discovery_of_the_year a in discoveries) count += a.Calls;
        Console.WriteLine("Имя участника номинации '{0,-16:s}'{1,-20:s}{2:s}", "'Открытие года'", "Количество голосов", "Доля от общего количества ответов");
        for (int i = 0; i < discoveries.Length; i++)
        {
            discoveries[i].Proportion(count);
            discoveries[i].Print();
        }
        Console.WriteLine();

        SerializeClass[] serializers = new SerializeClass[3]
        {
            new JSON(),
            new XML(), 
            new Binary()
        };
        string path = Environment.CurrentDirectory;
        path = Path.Combine(path, "Файлы к 9.1");
        if (!Directory.Exists(path))    
        {
            Directory.CreateDirectory(path);
        }
        string[] files = new string[3]
        {
            "JSON_1.json",
            "XML_1.xml",
            "Binary_1.bin"
        };

        int k = 0;
        Person[] participants = new Person[people.Length + discoveries.Length];
        for (int i = 0; i < people.Length; i++) participants[k++] = people[i];
        for (int i = 0; i < discoveries.Length; i++) participants[k++] = discoveries[i];

        for (int i = 0; i < serializers.Length; i++)
            serializers[i].Write(participants, Path.Combine(path, files[i]));

        for (int i = 0; i < serializers.Length; i++)
        {   
            Console.WriteLine($"From {files[i]}");
            Console.WriteLine("{0,-40:s}{1,-20:s}{2:s}", "Имя участника", "Количество голосов", "Доля от общего количества ответов");
            participants = serializers[i].Read<Person[]>(Path.Combine(path, files[i]));
            foreach (Person person in participants) person.Print();
            Console.WriteLine();
        }
    }
}