using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WariantBZad5KamilŁozowski
{
    class Program
    {
        private static LinkedList<Vertex>[] ReadFromFile(string sciezka)
        {
            int max = int.MinValue;
            string[] data = File.ReadAllLines(sciezka);
            LinkedList<Vertex>[] tab = new LinkedList<Vertex>[int.Parse(data[0])];            
            for(int i=1; i<data.Length; i++)
            {
                //Wpisywanie wierzchołka do listy incydencji
                string[] entries = data[i].Split(' ');
                if(tab[int.Parse(entries[0]) - 1] == null)
                {
                    tab[int.Parse(entries[0]) - 1] = new LinkedList<Vertex>();
                }                
                Vertex vertex = new Vertex(int.Parse(entries[1]), int.Parse(entries[2]), int.Parse(entries[3]));

                //Znajdywanie najlepszego skrzyżowania z którego wystartuje trasa wycieczki
                if(vertex.Waga - vertex.Odleglosc>max)
                {
                    DFS.bestCrossroad = int.Parse(entries[0]) - 1;
                }
                tab[int.Parse(entries[0]) - 1].AddLast(vertex);

                //Wpisywanie wierzchołka do listy incydencji
                if (tab[int.Parse(entries[1]) - 1] == null)
                {
                    tab[int.Parse(entries[1]) - 1] = new LinkedList<Vertex>();
                }
                Vertex vertex2 = new Vertex(int.Parse(entries[0]), int.Parse(entries[2]), int.Parse(entries[3]));
                tab[int.Parse(entries[1]) - 1].AddLast(vertex2);
            }           
            return tab;
        }
        static void Main(string[] args)
        {
            string sciezka = "Wyc_in_9_Łozowski.txt";
            LinkedList<Vertex>[] route = ReadFromFile(sciezka);
            ReadAllStreets(route);
            Stack<int> result = DFS.Find(route, DFS.bestCrossroad);
            SaveToFile(result);
        }
        static void ReadAllStreets(LinkedList<Vertex>[] route)
        {           
            for(int i=0; i<route.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Ulice łączące {i+1} skrzyżowanie: ");
                Console.ResetColor();               
                foreach(Vertex road in route[i])
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Następne skrzyżowanie: {road.Numer}");
                    Console.ResetColor();
                    Console.WriteLine($"Poziom Atrakcyjności: {road.Waga}");
                    Console.WriteLine($"Dlugość: {road.Odleglosc} km");
                }
            }
        }       
        static void SaveToFile(Stack<int> result)
        {
            string sciezka = "Wyc_out_9_Łozowski.txt";
            StreamWriter sw = new StreamWriter(sciezka);
            if (DFS.attractiveness >= 0)
                sw.WriteLine("TAK");
            else
                sw.WriteLine("NIE");
            while (result.Count != 0)
            {
                sw.WriteLine(result.Pop());
            }
            sw.Close();
        }
    }
}
