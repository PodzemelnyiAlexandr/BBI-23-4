// 6.	Создать класс «Страна» с 2 классами-наследниками: «Россия» и «Япония». 
// Вывести в % ответы по каждому классу отдельно и по обеим странам вместе взятым. 
// Использовать динамическую связку: преобразование классов.

// Задача 3.6
// Японская радиокомпания провела опрос радиослушателей по трем вопросам:
// а). Какое животное Вы связываете с Японией и японцами?
// б). Какая черта характера присуща японцам больше всего?
// в). Какой неодушевленный предмет или понятие Вы связываете с Японией?
// Большинство опрошенных прислали ответы на все или часть вопросов.
// Составить программу получения первых пяти наиболее часто встречающихся
// ответов по каждому вопросу и доли (в %) каждого такого ответа.
// Предусмотреть необходимость сжатия столбца ответов в случае отсутствия
// ответов на некоторые вопросы.

using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ProtoBuf;

[JsonDerivedType(typeof(Russia), typeDiscriminator: "Russia")]
[JsonDerivedType(typeof(Japan), typeDiscriminator: "Japan")]
[XmlInclude(typeof(Russia))]
[XmlInclude(typeof(Japan))]
[ProtoInclude(5, typeof(Russia))]
[ProtoInclude(6, typeof(Japan))]
[ProtoContract]
public abstract class Country
    {
        protected string answer;
        protected int calls;
        protected double prop;
        [ProtoMember(2)]
        public string Answer
        {
            get { return answer; }
            set { answer = value ?? string.Empty; }
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
        public Country() { }
        public Country(string _answer, int _calls, int count)
        {
            answer = _answer;
            calls = _calls;
            prop = (double) calls * 100 / count;
        }
        public abstract void Print();
    }
[ProtoContract]
public class Russia : Country
{
    public Russia() : base() { }
    public Russia(string _answer, int _calls, int count) : base(_answer, _calls, count) {}
    public override void Print()
    {
        Console.WriteLine(" Ответ '{0}' в России был выбран {1} раз(а), доля от всех ответов: {2:f1}%", answer, calls, prop);
    }
}
[ProtoContract]
public class Japan : Country
{
    public Japan() : base() { }
    public Japan(string _answer, int _calls, int count) : base(_answer, _calls, count) {}
    public override void Print()
    {
        Console.WriteLine(" Ответ '{0}' в Японии был выбран {1} раз(а), доля от всех ответов: {2:f1}%", answer, calls, prop);
    }
}
[ProtoContract]
public class ArrayofAnswers
{
    [ProtoMember(1)]
    private Country[] answers;
    public Country[] Answers
    {
        get { return answers; }
        set { answers = value; }
    }
    public ArrayofAnswers() { }
    public ArrayofAnswers(Country[] answers)
    {
        this.answers = answers;
    }
}
class Program
{
    static void Sort(List<ArrayofAnswers> a)
        {
            for (int k = 0; k < a.Count; k++)
                for (int i = 1; i < a[0].Answers.Length; i++)
                    {
                        var q = a[k].Answers[i];
                        int j = i - 1;

                        while(j >= 0 && a[k].Answers[j].Prop < q.Prop)
                        {
                            a[k].Answers[j + 1] = a[k].Answers[j];
                            j--;
                        }
                        a[k].Answers[j + 1] = q;
                    }
        }
    static void Main()
    {

        
        string[,] japan = // каждый столбец - ответы одного опрошенного на все три вопроса
        {
            { "Панда",   "Панда",   "-",              "Лошадь", "Панда",        "Собака",    "Собака",   "Игуана", "Голубь",       "-" },
            { "Доброта", "Доброта", "Чистоплотность", "-",      "Аккуратность", "Честность", "Гордость", "-",      "Аккуратность", "Гордость" },
            { "Мир",     "Красота", "-",              "Страна", "Мир",          "Аниме",     "Роллы",    "Роллы",  "Роллы",        "-" }
        };
        List<ArrayofAnswers> totalJapan = new List<ArrayofAnswers>(3);
        int last, count;
        for (int k = 0; k < 3; k++)
        {
            Country[] tempJapan= new Country[japan.GetLength(1)];
            string[] AnswersSorted = new string[japan.GetLength(1)];  
            int[] AnswersCalls = new int[japan.GetLength(1)]; 
            last = 0; count = 0;
            for (int i = 0; i < japan.GetLength(1); i++)  
                if (japan[k,i] != "-")
                {
                    if (InArray(japan[k,i], AnswersSorted) == -1)
                    {
                        AnswersSorted[last] = japan[k,i];
                        AnswersCalls[last] += 1;
                        last++;
                    }
                    else AnswersCalls[InArray(japan[k,i], AnswersSorted)] += 1;
                    count++;
                }
            for (int i = 0; i < AnswersSorted.Length; i++) 
            {
                tempJapan[i] = new Japan(AnswersSorted[i], AnswersCalls[i], count);
            }
            totalJapan.Add(new ArrayofAnswers(tempJapan));
        }

        int InArray(string answer, string[] answers)  // метод, позволяющий узнать, есть ли элемент в массиве
        {
            for (int i = 0; i < last; i++) if (answers[i] == answer)
                    return i;
            return -1;
        }

        string[,] russia = // каждый столбец - ответы одного опрошенного на все три вопроса
        {
            { "Медведь",   "Медведь", "-",        "Лиса",   "Волк",           "Ёж",        "Медведь",  "Лиса",   "Голубь",   "-" },
            { "Доброта",   "Доброта", "Агрессия", "-",      "Неаккуратность", "Честность", "Гордость", "-",      "Гордость", "Гордость" },
            { "Мир",       "Красота", "-",        "Страна", "Водка",          "Водка",     "Водка",    "Холод",  "Холод",    "-" }
        };
        List<ArrayofAnswers> totalRussia = new List<ArrayofAnswers>(3);
        last = 0; count = 0;
        for (int k = 0; k < 3; k++)
        {
            Country[] tempRussia = new Country[russia.GetLength(1)];
            string[] AnswersSorted = new string[russia.GetLength(1)];  
            int[] AnswersCalls = new int[russia.GetLength(1)]; 
            last = 0; count = 0;
            for (int i = 0; i < russia.GetLength(1); i++)  
                if (russia[k,i] != "-")
                {
                    if (InArray(russia[k,i], AnswersSorted) == -1)
                    {
                        AnswersSorted[last] = russia[k,i];
                        AnswersCalls[last] += 1;
                        last++;
                    }
                    else AnswersCalls[InArray(russia[k,i], AnswersSorted)] += 1;
                    count++;
                }
            for (int i = 0; i < AnswersSorted.Length; i++) 
            {
                tempRussia[i] = new Russia(AnswersSorted[i], AnswersCalls[i], count);
            }
            totalRussia.Add(new ArrayofAnswers(tempRussia));
        }

        List<ArrayofAnswers> total = new List<ArrayofAnswers>() { new ArrayofAnswers(), new ArrayofAnswers(), new ArrayofAnswers() };
        for (int i = 0; i < total.Count; i++)
        {
            Country[] temp = new Country[totalJapan[i].Answers.Length + totalRussia[i].Answers.Length];
            int k = 0;
            for (int j = 0; j < totalRussia[i].Answers.Length; j++)
            {
                temp[k++] = totalRussia[i].Answers[j]; 
            }
            foreach (Japan answer in totalJapan[i].Answers) temp[k++] = answer; 
            total[i] = new ArrayofAnswers(temp);
        }
        
        Sort(total);

        SerializeClass[] serializers = new SerializeClass[3]
        {
            new JSON(),
            new XML(), 
            new Binary()
        };
        string path = Environment.CurrentDirectory;
        path = Path.Combine(path, "Файлы к 9.3");
        if (!Directory.Exists(path))    
        {
            Directory.CreateDirectory(path);
        }
        string[] files = new string[3]
        {
            "JSON_3.json",
            "XML_3.xml",
            "Binary_3.bin"
        };

        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(total, Path.Combine(path, files[i]));
        }

        Console.WriteLine("Для Японии:");
        for (int i = 0; i < serializers.Length; i++)
        {   
            Console.WriteLine($"From {files[i]}");
            total = serializers[i].Read<List<ArrayofAnswers>>(Path.Combine(path, files[i]));
            for (int k = 0; k < total.Count; k++)
            {
                Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
                int c = 0;
                for (int j = 0; j < 5; ) 
                {
                    if (total[k].Answers[c].GetType() == typeof(Japan))  
                    {
                        total[k].Answers[c].Print();
                        j++;
                    }
                    c++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        Console.WriteLine("Для России:");
        for (int i = 0; i < serializers.Length; i++)
        {   
            Console.WriteLine($"From {files[i]}");
            total = serializers[i].Read<List<ArrayofAnswers>>(Path.Combine(path, files[i]));
            for (int k = 0; k < total.Count; k++)
            {
                Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
                int c = 0;
                for (int j = 0; j < 5; ) 
                {
                    if (total[k].Answers[c].GetType() == typeof(Russia))  
                    {
                        total[k].Answers[c].Print();
                        j++;
                    }
                    c++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        Console.WriteLine("Для двух стран:");
        for (int i = 0; i < serializers.Length; i++)
        {   
            Console.WriteLine($"From {files[i]}");
            total = serializers[i].Read<List<ArrayofAnswers>>(Path.Combine(path, files[i]));
            for (int k = 0; k < total.Count; k++)
            {
                Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
                for (int j = 0; j < 5; j++) total[k].Answers[j].Print();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
     }
}