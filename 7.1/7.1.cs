// Номер 3
// Сделать абстрактный класс, и от него создать 2-х наследников: человек года и открытие года. 
// Собрать 2 таблицы с ответами и вывести 2 таблицы (независимые).
using System.ComponentModel.DataAnnotations.Schema;

abstract class Person
{
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
    public void Print()
    {
        Console.WriteLine("{0,-40:s}{1,-20:f0}{2:f3}", name, calls, prop);
    }
}
class Person_of_the_year : Person
{
    public Person_of_the_year(string _name, int _calls) : base(_name, _calls) {}
}
class Discovery_of_the_year : Person
{
    public Discovery_of_the_year(string _name, int _calls) : base(_name, _calls) {}
}
class Program
{
    static void Sort(Person[] a)
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
        Sort(people);
        int count = 0;
        foreach (Person_of_the_year a in people) count += a.calls;
        Console.WriteLine("{0,-40:s}{1,-20:s}{2:s}", "Имя участника номинации 'Человек года'", "Количество голосов", "Доля от общего количества ответов");
        foreach (Person_of_the_year a in people) 
        {
            a.Proportion(count);
            a.Print();
        }
        Console.WriteLine();

        Discovery_of_the_year[] discoveries =
        {
            new Discovery_of_the_year("Игорь", 56),
            new Discovery_of_the_year("Валентин", 15),
            new Discovery_of_the_year("Артём", 187),
            new Discovery_of_the_year("Себастьян", 93),
            new Discovery_of_the_year("Ариан", 213)
        };
        Sort(discoveries);
        count = 0;
        foreach (Discovery_of_the_year a in discoveries) count += a.calls;
        Console.WriteLine("{0,-40:s}{1,-20:s}{2:s}", "Имя участника номинации 'Открытие года'", "Количество голосов", "Доля от общего количества ответов");
        foreach (Discovery_of_the_year a in discoveries) 
        {
            a.Proportion(count);
            a.Print();
        }
    }
}