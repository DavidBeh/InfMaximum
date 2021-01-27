using System;
using System.Collections.Generic;
using System.Linq;

namespace InfMaximum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Schreibe deinen Namen");
            string name = Console.ReadLine();
            Console.WriteLine("Schreibe dein Geburtsdatum");
            DateTime datum;
            if (DateTime.TryParse(Console.ReadLine(), out datum) == false)
            {
                exitError("Formatfehler");
            }
            int jahr = int.Parse(datum.Year.ToString().Remove(0, 2));
            int monat = datum.Month;
            int tag = datum.Day;
            Console.WriteLine($"Das Datum wird als {datum.ToString("dd.MM.yyyy")} interpretiert");

            string datumFormat = datum.Year < 2000 || datum.Year >= 2060 ? "dd/MM/yyyy" : "dd/MM/yy";

            List<int> indexe = new List<int>();
            List<int> maximale = new List<int>();

            int mAnz = Max<int>(new int[] { tag, monat, jahr }, ref indexe, ref maximale);

            Console.WriteLine("\n-------------------------");

            Console.WriteLine($"Du bist {name}, geboren am {datum.ToString(datumFormat)}");
            Console.WriteLine($"Die Zahl {maximale.First()} ist sicherlich deine Lieblingszahl ");
            bool comma = false;
            Console.Write("Die Zahl ist"); 
            if (indexe.Contains(0))
            {
                Console.Write(" dein GeburtsTAG");
                comma = true;
            }
            if (indexe.Contains(1))
            {
                Console.Write((comma ? !indexe.Contains(2) ? " und " : ", " : " ") + "dein Geburtsmonat");
                comma = true;
            }
            if (indexe.Contains(2))
            {
                Console.Write((comma ? " und " : "" )+ "die hinteren zwei Ziffern deines Geburtsjahr");
            }
            exit();
        }
        /// <summary>
        /// Gibt die Maximalen einer Enumeration aus
        /// </summary>
        /// <typeparam name="T">Vergleichbares</typeparam>
        /// <param name="werte">Werte die verglichen werden sollen</param>
        /// <param name="indexe">Indexe der Maximalen Werte aus werte</param>
        /// <param name="maximale">Maximale Werte</param>
        /// <returns></returns>
        public static int Max<T>(IEnumerable<T> werte, ref List<int> indexe, ref List<T> maximale)
        {
            T rekord = default(T);

            for (int i = 0; i < werte.Count(); i++)
            {
                T item = werte.ToList()[i];
                if (maximale == null || Comparer<T>.Default.Compare(item, rekord) > 0)
                {
                    indexe.Clear();
                    maximale.Clear();
                    rekord = item;
                }
                if (Comparer<T>.Default.Compare(item, rekord) == 0)
                {
                    indexe.Add(i);
                    maximale.Add(item);
                }
            }
            return maximale.Count;
        }

        public static void exit()
        {
            Console.WriteLine("\nDrücke eine beliebige Taste zum Beenden");
            Console.ReadKey();
            Console.WriteLine("\n");
            Environment.Exit(1);
        }
        public static void exitError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(error);
            exit();
        }
    }
}
