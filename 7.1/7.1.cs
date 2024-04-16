// Номер 3
// Сделать абстрактный класс, и от него создать 2-х наследников: человек года и открытие года. 
// Собрать 2 таблицы с ответами и вывести 2 таблицы (независимые).
using System.ComponentModel.DataAnnotations.Schema;

abstract class Person
{
    public string nomination {get; protected set;}
    private string name;
    public int calls {get; private set;}
    private double prop;
    public Person(string _name, int _calls)
    {
        name = _name;
        calls = _calls;
    }
    public void Proportion(int count)
    {
        prop = (double) calls * 100 / count;
    }
    public static void NewPrint(Person[] a, int count)
    {
        Console.WriteLine("Имя участника номинации {0,-16:s}{1,-20:s}{2:s}", a[0].nomination, "Количество голосов", "Доля от общего количества ответов");
        foreach (Person i in a) 
        {
            i.Proportion(count);
            Console.WriteLine("{0,-40:s}{1,-20:f0}{2:f3}", i.name, i.calls, i.prop);
        }
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
class Person_of_the_year : Person
{
    public Person_of_the_year(string _name, int _calls) : base(_name, _calls) 
    {
        nomination = "'Человек года'";
    }
}
class Discovery_of_the_year : Person
{
    public Discovery_of_the_year(string _name, int _calls) : base(_name, _calls) 
    {
        nomination = "'Открытие года'";
    }
}
class Program
{
   
    
    static void Main()
    { 
        Person_of_the_year[] people = 
        {
            new Person_of_the_year("Пётр", 110),
            new Person_of_the_year("Иван", 40),
            new Person_of_the_year("Руслан", 60),
            new Person_of_the_year("Владимир", 150),
            new Person_of_the_year("Фёдор", 73)
        };
        Person.Sort(people);
        int count = 0;
        foreach (Person_of_the_year a in people) count += a.calls;
        Person.NewPrint(people, count);
        Console.WriteLine();

        Discovery_of_the_year[] discoveries =
        {
            new Discovery_of_the_year("Игорь", 56),
            new Discovery_of_the_year("Валентин", 15),
            new Discovery_of_the_year("Артём", 187),
            new Discovery_of_the_year("Себастьян", 93),
            new Discovery_of_the_year("Ариан", 213)
        };
        Person.Sort(discoveries);
        count = 0;
        foreach (Discovery_of_the_year a in discoveries) count += a.calls;
        Person.NewPrint(discoveries, count);
    }
}