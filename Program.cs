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
                Vertex vertex = new Vertex(int.Parse(entries[1]), int.Parse(entries[2]), int.Parse(entries[3]), i);

                //Znajdywanie najlepszego skrzyżowania z którego wystartuje trasa wycieczki
                if(vertex.Waga - vertex.Odleglosc>max)
                {
                    DFS.BestCrossroad = int.Parse(entries[0]);
                    DFS.FirstStreetNumber = i;
                    DFS.SecondCrossroad = int.Parse(entries[1]);
                    max = vertex.Waga - vertex.Odleglosc;
                }
                tab[int.Parse(entries[0]) - 1].AddLast(vertex);

                //Wpisywanie wierzchołka do listy incydencji
                if (tab[int.Parse(entries[1]) - 1] == null)
                {
                    tab[int.Parse(entries[1]) - 1] = new LinkedList<Vertex>();
                }
                Vertex vertex2 = new Vertex(int.Parse(entries[0]), int.Parse(entries[2]), int.Parse(entries[3]), i);
                tab[int.Parse(entries[1]) - 1].AddLast(vertex2);
            }           
            return tab;
        }
        static void Main(string[] args)
        {
            string sciezka = "Wyc_in_9_Łozowski — kopia.txt";
            LinkedList<Vertex>[] route = ReadFromFile(sciezka);
            ReadAllStreets(route);
            Stack<int> result = DFS.Find(route, DFS.BestCrossroad);
            SaveToFile(result);
        }
        static void ReadAllStreets(LinkedList<Vertex>[] route)
        {            
            //slave
            for(int i=0; i<route.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Ulice łączące {i+1} skrzyżowanie: ");
                Console.ResetColor();               
                foreach(Vertex road in route[i])
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Następne skrzyżowanie: {road.NumerSkrzyzowania}");
                    Console.ResetColor();
                    Console.WriteLine($"Poziom Atrakcyjności: {road.Waga}");
                    Console.WriteLine($"Dlugość: {road.Odleglosc} km");
                    Console.WriteLine($"Numer Ulicy: {road.NumerUlicy}");
                }
            }
        }       
        static void SaveToFile(Stack<int> result)
        {
            string sciezka = "Wyc_out_9_Łozowski.txt";
            bool secondCrossroad = false;
            StreamWriter sw = new StreamWriter(sciezka);
            if (DFS.Attractiveness >= 0)
            {
                sw.WriteLine("TAK");
                sw.WriteLine((result.Count-1).ToString());
                while (result.Count != 1)
                {
                    
                    if (secondCrossroad == false)
                    {
                        sw.Write(result.Pop());
                        sw.WriteLine(" " + DFS.SecondCrossroad);
                        secondCrossroad = true;
                    }
                    else
                        sw.WriteLine(result.Pop());
                }
            }              
            else
                sw.WriteLine("NIE");
            sw.Close();
        }
    }
}
