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

abstract class Country
    {
        protected string answer;
        protected int calls;
        public double prop {get; private set;}

        public Country(string _answer, int _calls, int count)
        {
            answer = _answer;
            calls = _calls;
            prop = (double) calls * 100 / count;
        }
        public abstract void Print();
    }
class Russia : Country
{
    public Russia(string _answer, int _calls, int count) : base(_answer, _calls, count) {}
    public override void Print()
    {
        Console.WriteLine(" Ответ '{0}' в России был выбран {1} раз(а), доля от всех ответов: {2:f1}%", answer, calls, prop);
    }
}
class Japan : Country
{
    public Japan(string _answer, int _calls, int count) : base(_answer, _calls, count) {}
    public override void Print()
    {
        Console.WriteLine(" Ответ '{0}' в Японии был выбран {1} раз(а), доля от всех ответов: {2:f1}%", answer, calls, prop);
    }
}
class Program
{
    

    static void SortMatrix(Country[,] a)
        {
            for (int k = 0; k < a.GetLength(0); k++)
                for (int i = 1; i < a.GetLength(1); i++)
                    {
                        Country q = a[k, i];
                        int j = i - 1;

                        while(j >= 0 && a[k, j].prop < q.prop)
                        {
                            a[k, j + 1] = a[k, j];
                            j--;
                        }
                        a[k, j + 1] = q;
                    }
        }
    static void Main()
    {

        
        string[,] japan = // каждый столбец - ответы одного опрошенного на все три вопроса
        {
            { "Медведь",   "Медведь", "-",        "Лиса",   "Волк",           "Ёж",        "Медведь",  "Лиса",   "Голубь",   "-" },
            { "Доброта",   "Доброта", "Агрессия", "-",      "Неаккуратность", "Честность", "Гордость", "-",      "Гордость", "Гордость" },
            { "Мир",       "Красота", "-",        "Страна", "Водка",          "Водка",     "Водка",    "Холод",  "Холод",    "-" }
        };
        Japan[,] totalJapan = new Japan[3, japan.GetLength(1)];
        int last, count;
        for (int k = 0; k < 3; k++)
        {
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
            for (int i = 0; i < AnswersSorted.Length; i++) totalJapan[k, i] = new Japan(AnswersSorted[i], AnswersCalls[i], count);
        }

        int InArray(string answer, string[] answers)  // метод, позволяющий узнать, есть ли элемент в массиве
        {
            for (int i = 0; i < last; i++) if (answers[i] == answer)
                    return i;
            return -1;
        }

        SortMatrix(totalJapan);

        Console.WriteLine("Для Японии:");
        for (int k = 0; k < 3; k++)
        {
            Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
            for (int i = 0; i < 5; i++) totalJapan[k,i].Print();
            Console.WriteLine();
        }
        Console.WriteLine();

        string[,] russia = // каждый столбец - ответы одного опрошенного на все три вопроса
        {
            { "Панда",   "Панда",   "-",              "Лошадь", "Панда",        "Собака",    "Собака",   "Игуана", "Голубь",       "-" },
            { "Доброта", "Доброта", "Чистоплотность", "-",      "Аккуратность", "Честность", "Гордость", "-",      "Аккуратность", "Гордость" },
            { "Мир",     "Красота", "-",              "Страна", "Мир",          "Аниме",     "Роллы",    "Роллы",  "Роллы",        "-" }
        };
        Russia[,] totalRussia = new Russia[3, russia.GetLength(1)];
        last = 0; count = 0;
        for (int k = 0; k < 3; k++)
        {
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
            for (int i = 0; i < AnswersSorted.Length; i++) totalRussia[k, i] = new Russia(AnswersSorted[i], AnswersCalls[i], count);
        }

        SortMatrix(totalRussia);

        Console.WriteLine("Для России:");
        for (int k = 0; k < 3; k++)
        {
            Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
            for (int i = 0; i < 5; i++) totalRussia[k,i].Print();
            Console.WriteLine();
        }
        Console.WriteLine();

        Country[,] total = new Country[3, totalJapan.GetLength(1) + totalRussia.GetLength(1)];
        for (int k = 0; k < 3; k++)
        {
            int n = 0;
            for (int i = 0; i < totalJapan.GetLength(1); i++) total[k, n++] = totalJapan[k, i];
            for (int i = 0; i < totalRussia.GetLength(1); i++) total[k, n++] = totalRussia[k, i];
        }

        SortMatrix(total);

        Console.WriteLine("Для двух стран:");
        for (int k = 0; k < 3; k++)
        {
            Console.WriteLine("5 наиболее часто встречающихся ответов на {0} вопрос:", k + 1);
            for (int i = 0; i < 5; i++) total[k,i].Print();
            Console.WriteLine();
        }
    }
}