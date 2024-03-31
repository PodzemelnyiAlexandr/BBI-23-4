// 9.	Сделать абстрактный класс «Зимние виды спорта» с обязательным полем «Название дисциплины», 
// и от него наследников: «Фигурное катание» и «Конькобежный спорт». 
// В каждом из классов переопределить название дисциплины (и выводить в начале таблицы).

// Задача 2.9
// Результаты соревнований фигуристов по одному из видов многоборья
// представлены оценками семи судей в баллах (от 0,0 до 6,0). По результатам оценок
// судьи определяется место каждого участника у этого судьи. Места участников
// определяются далее по сумме мест, которые каждый участник занял у всех судей.
// Составить программу, определяющую по исходной таблице оценок фамилии и
// сумму мест участников в порядке занятых ими мест.

abstract class Participant
    {
        public string discipline {get; protected set;} 
        private string name;
        public double[] scores {get; private set;}
        private int[] JudgesPlaces;
        public int TotalScore {get; private set;}
        public Participant(string _name, double[] _scores)
        {
            name = _name;
            scores = _scores;
            JudgesPlaces = new int[7];
        }
        public void NewPlace(int _place, int i)
        {
            if (i < JudgesPlaces.Length)
            {
                JudgesPlaces[i] = _place;
                TotalScore += _place;
            }
        }
        public void Print(int i)
        {
            Console.WriteLine("{0,-22:s}{1,-8:f0}{2,-10:s}{3:f0}", discipline, i, name, TotalScore);
        }
    }
class Figure_Skating : Participant
{
    public Figure_Skating(string _name, double[] _scores) : base(_name, _scores) 
    {
        discipline = "Фигурное катание";
    }
}
class Sport_Skating : Participant
{
    public Sport_Skating(string _name, double[] _scores) : base(_name, _scores) 
    {
        discipline = "Конькобежный спорт";
    }
}

class Program
{
    static void ShellSort(Participant[] array, int index)
        {
            //расстояние между элементами, которые сравниваются
            int d = array.Length / 2;
            while (d >= 1)
            {
                for (int i=d; i < array.Length; i++)
                {
                    Participant k = array[i];
                    int j = i - d;

                    while(j>=0 && array[j].scores[index] < k.scores[index])
                    {
                        array[j + d] = array[j];
                        j -= d;
                    }
                array[j + d] = k;
                }
                d = d / 2;
            }
        }
    static void Main()
    {
        Figure_Skating[] figure =
        {
            new Figure_Skating("Валентин", [1, 1.5, 4, 2, 6, 5.8, 1.3]),
            new Figure_Skating("Артём", [3, 3.2, 5.5, 1.2, 3.5, 1, 5]),
            new Figure_Skating("Олег", [5, 5.4, 6, 5.3, 2.4, 5, 3]),
            new Figure_Skating("Игорь", [1.1, 2, 4.1, 3, 5.5, 4, 4.7]),
            new Figure_Skating("Ольга", [4, 4.5, 5, 3.4, 2, 5.3, 3,9])
        };

        for (int i = 0; i < 7; i++)
        {
            ShellSort(figure, i);
            for (int j = 0; j < figure.Length; j++)
            {
                figure[j].NewPlace(j + 1, i);
            }
        }

        for (int i=1; i < figure.Length; i++)
            {
                Figure_Skating k = figure[i];
                int j = i - 1;

                while(j>=0 && figure[j].TotalScore > k.TotalScore)
                {
                    figure[j + 1] = figure[j];
                    figure[j] = k;
                    j--;
                }
            }

        Sport_Skating[] sport =
        {
            new Sport_Skating("Игорь", [1, 5.1, 4, 2, 6, 5.8, 3.1]),
            new Sport_Skating("Арслан", [3, 3.2, 1.7, 1.2, 3.5, 1, 5]),
            new Sport_Skating("Вениамин", [5, 5.4, 3.9, 5.3, 2.4, 5, 3]),
            new Sport_Skating("Максим", [3.8, 2, 4.1, 3, 5.5, 4, 4.7]),
            new Sport_Skating("Евгения", [4, 4.5, 5, 3.4, 5.4, 5.3, 3,9])
        };

        for (int i = 0; i < 7; i++)
        {
            ShellSort(sport, i);
            for (int j = 0; j < sport.Length; j++)
            {
                sport[j].NewPlace(j + 1, i);
            }
        }

        for (int i=1; i < sport.Length; i++)
            {
                Sport_Skating k = sport[i];
                int j = i - 1;

                while(j>=0 && sport[j].TotalScore > k.TotalScore)
                {
                    sport[j + 1] = sport[j];
                    sport[j] = k;
                    j--;
                }
            }

        Console.WriteLine("{0,-22:s}{1,-8:s}{2,-10:s}{3:s}", "Название дисциплины", "Место", "Имя", "Сумма мест");
        for (int i = 0; i < figure.Length; i++) figure[i].Print(i + 1);
        for (int i = 0; i < sport.Length; i++) sport[i].Print(i + 1);

    }
}