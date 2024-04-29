// 8, 9, 10, 12, 13, 15

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

abstract class Task 
{
    protected string text;
    public Task(string text) 
    {
        this.text = text;
    }
    static protected string[] Words(string text)
    {
        char[] pattern = { ' ', ',', '.', '!', ';', ':', '?', '–', '(', ')' };
        if (Char.IsPunctuation(text[text.Length - 1])) text = text.Remove(text.Length - 1, 1);
        text = text.Replace("'", " ");
        string[] words = text.Split(pattern, System.StringSplitOptions.RemoveEmptyEntries);
        return words;
    }
}
class Task8 : Task
{
    
    public Task8(string text) : base(text) 
    {
        this.text = text.Insert(text.Length, " ");
    }
    private static void AddSpaces(ref string a)
    {
        int i = 0, k = 0;
        if (a != null) 
        {
            int length = a.Length;        
            while (length != 50)
            {
                if (a[i] == ' ') 
                {
                    a = a.Insert(i++, " ");
                    k++;
                }
                if (i < a.Length - 1) i++;
                else 
                {
                    i = 0;
                    if (k == 0) return;
                }
                length = a.Length;
            }
        }
    }
    public override string ToString()
    {
        string[] lines = new string[(text.Length + 1) / 25];
        int first = 0, last = 49, i = 0;
        while (last < text.Length + 50)
        {
            if ((last >= text.Length) || (text[last] != ' ')) last--;
            else 
            {
                if (last > first) lines[i++] = text.Substring(first, last - first);
                first = ++last; 
                last += 50;
            }
        }
        for (int j = 0; j < lines.Length; j++)
        {
            if (lines[j] != null)
            {
                AddSpaces(ref lines[j]);
                Console.WriteLine(lines[j]);
            }
            else break;
        }
        return "";
    }
}
class Task9 : Task 
{
    private int frequency; // количество повторений последовательностей из двух букв, считающееся частым
    public Task9(string text, int frequency) : base(text) 
    { 
        this.frequency = frequency;
    }
    static private char[] GetUniqueLetters(string text, int chars)
    {
        char[] letters = new char[chars];
        int n = 35;
        for (int i = 35; i < chars + 35; i++) letters[i-35] = (char)n++;
        int count = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            for (int j = 0; j < text.Length; j++)
            {
                if (letters[i] == text[j])
                {
                    for (int k = i; k < letters.Length - 1; k++) // переделать в метод, если будет еще где-то использоваться
                    {
                        letters[k] = letters[k+1];
                    }
                    letters[letters.Length - 1] = (char)n++;
                    count++;
                }
            }
        }
        char[] newletters = new char[letters.Length - count];
        for (int i = 0; i < letters.Length - count; i++) newletters[i] = letters[i];
        return newletters;
    }
    static private bool InArray(string[] array, string a)
    {
        for (int i = 0; i < array.Length; i++) if (array[i] == a)
                    return true;
            return false;
    }
    static string[] GetPairs(string text, int frequency) // Возвращает все пары, встречающиеся frequency раз или чаще, в порядке убывания
    {
        string[] words = Words(text), pairs = new string[text.Length];
        int[] counts = new int[text.Length];
        int k = 0;
        foreach (string word in words)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                string pair = word.Substring(i, 2);
                if (!InArray(pairs, pair))
                {
                    int count = 0;
                    bool flag = false;
                    for (int j = 0; j < text.Length - 1; j++)
                    {
                        if (pair == text.Substring(j, 2)) count++;
                        if (count >= frequency && k < pairs.Length) 
                        {
                            flag = true;
                            pairs[k] = pair;
                            counts[k] = count;
                        }
                    }
                    if (flag) k++;
                }
            }
        }
        int[] newcounts = new int[k];
        for (int i = 0; i < k; i++) newcounts[i] = counts[i];
        string[] newpairs = new string[k];
        for (int i = 0; i < k; i++) newpairs[i] = pairs[i];
        for (int i = 1; i < k; i++)
            {
                string tempP = newpairs[i];
                int tempC = newcounts[i];
                int j = i - 1;
                while (j >= 0 && newcounts[j] < tempC)
                {
                    newpairs[j + 1] = newpairs[j];
                    newcounts[j + 1] = newcounts[j];
                    j--;
                }
            newpairs[j + 1] = tempP;
            newcounts[j + 1] = tempC;
            }
        return newpairs;
    }   
    public override string ToString() 
    {
        string[] pairs = GetPairs(text, frequency);
        char[] letters = GetUniqueLetters(text, pairs.Length);
        Console.WriteLine("{0,-10:s}{1:s}", "Пара букв", "Код");
        for (int i = 0; i < pairs.Length; i++) if (i < letters.Length)
            {
                text = text.Replace(pairs[i], letters[i].ToString());
                Console.WriteLine("{0,-10:f0}{1:f0}", pairs[i], letters[i].ToString());
            }
        Console.WriteLine();
        return text;
    }
}
class Task10 : Task
{
    private string[,] codes;
    public Task10(string text, string[,] codes) : base(text)
    {
        this.codes = codes;
    }
    public override string ToString()
    {
        for (int i = 0; i < codes.GetLength(0); i++)
        {
            text = text.Replace(codes[i,1], codes[i,0].ToString());
        }
        return text;
    }
}
class Task12 : Task
{
    private string[,] codes = 
        {
            { "ученые", "#" },
            { "исследований", "$" },
            { "что", "%" },
            { "показал", "&" },
            { "Амазонии", "'" }
        };
    public Task12(string text) : base(text) { }
    public override string ToString()
    {
        string[] array = text.Split(" ");
        for (int i = 0; i < codes.GetLength(0); i++)
            for (int j = 0; j < array.Length; j++)
                array[j] = array[j].Replace(codes[i,0], codes[i,1]);
        Console.WriteLine("Закодированный текст:");
        foreach (string a in array) Console.Write(a + " ");
        Console.WriteLine();
        Console.WriteLine("\nРаскодированный текст:");
        for (int i = 0; i < array.Length; i++)
        {
            if (!char.IsLetter(array[i][0])) 
                for (int j = 0; j < codes.GetLength(0); j++)
                    if (array[i][0].ToString() == codes[j,1]) array[i] = array[i].Replace(codes[j,1], codes[j,0]);
            Console.Write(array[i] + " ");
        }
        return "";
    }
}
class Task13 : Task
{
    public Task13(string text) : base(text) { }
    public override string ToString()
    {
        char[,] letters = 
        {
            { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'},
            { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '$', '$', '$', '$', '$', '$', '$' }
        };
        int language = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (Char.IsLetter(text[i]))
            {
                if (text[i] > 64 && text[i] < 123)
                {
                    language = 1;
                    break;
                }
                else break;
            }
        }
        int[] counts = new int[33];
        for (int i = 0; i < text.Length; i++) if (Char.IsUpper(text[i])) text = text.Replace(text[i], Char.ToLower(text[i]));
        string[] temp = Words(text);
        text = "";
        foreach (string a in temp) text += a + " "; 
        for (int i = 35; i < 68; i++)
            text = text.Replace(letters[language, i - 35], (char)i);
        string[] words = text.Split(" ");
        for (int i = 0; i < words.Length - 1; i++)
            if ((words[i][0] - 35) < 33) counts[words[i][0] - 35]++;
        Console.WriteLine("{0,-10:s}{1:s}", "Буква", "Доля");
        if (language == 0)
            for (int i = 0; i < 33; i++)
                if (counts[i] != 0) 
                    Console.WriteLine("{0,-10:s}{1:f3}", letters[language, i], (double)counts[i] / (words.Length - 1));
                else continue;
        else
            for (int i = 0; i < 26; i++)
                if (counts[i] != 0)
                    Console.WriteLine("{0,-10:s}{1:f3}", letters[language, i], (double)counts[i] / (words.Length - 1));
        return "";
    }
}
class Task15 : Task
{
    public Task15(string text) : base(text) { }
    public override string ToString()
    {
        double sum = 0;
        string[] words = text.Split(" ");
        for (int i = 0; i < words.Length; i++)
        {
            if (Char.IsDigit(words[i][0]) && Char.IsDigit(words[i][words[i].Length - 1])) sum += Convert.ToDouble(words[i]);
            else if ((Char.IsDigit(words[i][0]) && Char.IsLetter(words[i][words[i].Length - 1])) || (Char.IsDigit(words[i][0]) && Char.IsPunctuation(words[i][words[i].Length - 1])))
            {
                string digit = "";
                for (int j = 0; j < words[i].Length; j++) if (Char.IsDigit(words[i][j])) digit += words[i][j];
                sum += Convert.ToDouble(digit);
            }
        }
        return sum.ToString();
    }
}
class Program
{
    static void Main()
    {
        int[] TaskNumbers = { 8, 9, 10, 12, 13, 15 };
        // string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
        string text = "William Shakespeare, widely regarded as one of the greatest writers in the English language, authored a total of 37 plays, along with numerous poems and sonnets. He was born in Stratford-upon-Avon, England, in 1564, and died in 1616. Shakespeare's most famous works, including 'Romeo and Juliet,' 'Hamlet,' 'Macbeth,' and 'Othello,' were written during the late 16th and early 17th centuries. 'Romeo and Juliet,' a tragic tale of young love, was penned around 1595. 'Hamlet,' one of his most celebrated tragedies, was written in the early 1600s, followed by 'Macbeth,' a gripping drama exploring themes of ambition and power, around 1606. 'Othello,' a tragedy revolving around jealousy and deceit, was also composed during this period, believed to be around 1603.";
        // string text = "Двигатель самолета – это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна.";
        // string text = "1 июля 2015 года Греция объявила о дефолте по государственному долгу, став первой развитой страной в истории, которая не смогла выплатить свои долговые обязательства в полном объеме. Сумма дефолта составила порядка 1,6 миллиарда евро. Этому предшествовали долгие переговоры с международными кредиторами, такими как Международный валютный фонд (МВФ), Европейский центральный банк (ЕЦБ) и Европейская комиссия (ЕК), о программах финансовой помощи и реструктуризации долга. Основными причинами дефолта стали недостаточная эффективность реформ, направленных на улучшение финансовой стабильности страны, а также политическая нестабильность, что вызвало потерю доверия со стороны международных инвесторов и кредиторов. Последствия дефолта оказались глубокими и долгосрочными: сокращение кредитного рейтинга страны, увеличение затрат на заемный капитал, рост стоимости заимствований и утрата доверия со стороны международных инвесторов.";
        // string text = "Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. Название происходит от древнескандинавского слова 'fjǫrðr'. Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. Они популярны среди туристов и известны под разными названиями: в Норвегии – 'фьорды', в Шотландии – 'фьордс', в Исландии – 'фьордар'. Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.";
        // string text = "Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось 20 сентября 1519 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий.";
        string encodedText = "П(# м*го#т$х исс#д/а$й уч%ые 2на3жили тре4жную т%д%цию в вы3бке #с/ Ам0о$и. Анализ дан&х +к0ал, что (*в*й уча'$к -з3ше$я #с*го +кр/а – 1л/еч)кая дея5ль*'ь. За п(#д$е д)яти#т6 ро' 2ъема вы3бки до'иг критич)ких +к0а5#й. Глав&ми факто-ми, сп(2'вующими этому, являются промыш#н&е 3бки, произ4д'4 древ)и&, -сшире$е сельскохозяй'в%&х угодий и незаконная д2ыча древ)и&. Это при4дит к серьез&м экологич)ким п(#д'в6м, таким как +5ря био-з*2-з6, ухудше$е климата и угроза выми-$я м*гих вид/ жи4т&х и -'е$й.";  
        string textRepeat = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ исследований данных показал, что основной участник разрушения исследований лесного покрова Амазонии – человеческая исследований деятельность. За последние десятилетия рост исследований показал объема вырубки Амазонии достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство что древесины, расширение сельскохозяйственных угодий и что незаконная добыча древесины. Это приводит к серьезным экологическим показал что последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и показал растений ученые.";
        string[,] codes = 
        {
            { "ле", "#" },
            { "ни", "$" },
            { "ен", "%" },
            { "ны", "&" },
            { "ст", "'" },
            { "ос", "(" },
            { "ес", ")" },
            { "но", "*" },
            { "по", "+" },
            { "ра", "-" },
            { "ов", "/" },
            { "аз", "0" },
            { "че", "1" },
            { "об", "2" },
            { "ру", "3" },
            { "во", "4" },
            { "те", "5" },
            { "ия", "6" }
        };
        Task[] tasks = 
        {
            new Task8(text),
            new Task9(text, 5),
            new Task10(encodedText, codes),
            new Task12(textRepeat),
            new Task13(text),
            new Task15(text)
        };
        for (int i = 0; i < tasks.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Задание №" + TaskNumbers[i]);
            Console.WriteLine();
            Console.WriteLine(tasks[i]);
        }
    }
}