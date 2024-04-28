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
        char[] pattern = { ' ', ',', '.', '!', ';', ':', '?', '-', '(', ')' };
        text = text.Replace(". ", ".").Replace(", ", ",").Replace("! ", "!").Replace("; ", ";").Replace(": ", ":").Replace("? ", "?").Replace("- ", "-").Replace(" -", "-").Replace("( ", "(").Replace(") ", ")").Replace("– ", "–").Replace(" –", "–");
        if (Char.IsPunctuation(text[text.Length - 1])) text = text.Remove(text.Length - 1, 1);
        string[] words = text.Split(pattern);
        return words;
    }}
class Task8 : Task
{
    
    public Task8(string text) : base(text) 
    {
        this.text = text.Insert(text.Length, " ");
    }
    private static void AddSpaces(ref string a)
    {
        int i = 0;
        if (a != null) 
        {
            int length = a.Length;        
            while (length != 50)
            {
                if (a[i] == ' ') a = a.Insert(i++, " ");
                if (i < a.Length - 1) i++;
                else i = 0;
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
            AddSpaces(ref lines[j]);
            Console.WriteLine(lines[j]);
        }
        return base.ToString();
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
        for (int i = 35; i < chars + 35; i++) letters[i-35] = (char)i;
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
        for (int i = 0; i < array.Length; i++)
        {
            if (!char.IsLetter(array[i][0])) 
                for (int j = 0; j < codes.GetLength(0); j++)
                    if (array[i][0].ToString() == codes[j,1]) array[i] = array[i].Replace(codes[j,1], codes[j,0]);
            Console.Write(array[i] + " ");
        }
        return base.ToString();
    }
}
class Task13 : Task
{
    char[] letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
    int[] counts = new int[33];
    public Task13(string text) : base(text) { }
    public override string ToString()
    {
        char[] letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};
        int[] counts = new int[33];
        for (int i = 0; i < text.Length; i++) if (Char.IsUpper(text[i])) text = text.Replace(text[i], Char.ToLower(text[i]));
        string[] temp = Words(text);
        text = "";
        foreach (string a in temp) text += a + " "; 
        for (int i = 35; i < 68; i++)
            text = text.Replace(letters[i - 35], (char)i);
        string[] words = text.Split(" ");
        for (int i = 0; i < words.Length - 1; i++)
            counts[words[i][0] - 35]++;
        Console.WriteLine("{0,-10:s}{1:s}", "Буква", "Доля");
        for (int i = 0; i < letters.Length; i++)
            Console.WriteLine("{0,-10:s}{1:f3}", letters[i], (double)counts[i] / (words.Length - 1));
        return base.ToString();
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
            else if (Char.IsDigit(words[i][0]) && Char.IsPunctuation(words[i][words[i].Length - 1]))
                {
                    words[i] = words[i].Remove(words[i].Length - 1);
                    sum += Convert.ToDouble(words[i]);
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
        string text = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
        string encodedText = "П(# м*го#т$х исс#д/а$й уч%ые 2на3жили тре4жную т%д%цию в вы3бке #с/ Ам0о$и. Анализ дан&х +к0ал, что (*в*й уча'$к -з3ше$я #с*го +кр/а – 1л/еч)кая дея5ль*'ь. За п(#д$е д)яти#т6 ро' 2ъема вы3бки до'иг критич)ких +к0а5#й. Глав&ми факто-ми, сп(2'вующими этому, являются промыш#н&е 3бки, произ4д'4 древ)и&, -сшире$е сельскохозяй'в%&х угодий и незаконная д2ыча древ)и&. Это при4дит к серьез&м экологич)ким п(#д'в6м, таким как +5ря био-з*2-з6, ухудше$е климата и угроза выми-$я м*гих вид/ жи4т&х и -'е$й.";  
        string textRepeat = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ исследований данных показал, что основной участник разрушения исследований лесного покрова Амазонии – человеческая исследований деятельность. За последние десятилетия рост исследований показал объема вырубки Амазонии достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство что древесины, расширение сельскохозяйственных угодий и что незаконная добыча древесины. Это приводит к серьезным экологическим показал что последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и показал растений ученые.";
        string textWithNumbers = "После 2 многолетних исследований 234 ученые обнаружили тревожную тенденцию 12,6 в вырубке лесов Амазонии. Анализ данных показал 67, что основной участник 6664256 разрушения лесного покрова – человеческая деятельность. За последние 0,234 десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
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
            new Task15(textWithNumbers)
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