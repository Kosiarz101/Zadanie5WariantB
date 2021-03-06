﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WariantBZad5KamilŁozowski
{
    static class DFS
    {
        public static int Attractiveness { get; set; }
        public static int BestCrossroad { get; set; }
        public static int FirstStreetNumber { get; set; }
        public static int SecondCrossroad { get; set; }
        public static Stack<int> Find(LinkedList<Vertex>[] graph, int s)
        {
            int[] edges = new int[graph.Length];
            Stack<int> result = new Stack<int>();
            for(int i=0; i<edges.Length; i++)
            {
                edges[i] = 4;
            }
            //int[,] matrice = CreateMatrice(graph);  
            Attractiveness = 0;
            Find(graph, s, FirstStreetNumber, edges, result);
            return result;
        }
        private static void Find(LinkedList<Vertex>[] graph, int s, int streetNr, int[] edges, Stack<int> result)
        {
            int nextEdgeNumer = 0;
            Vertex nextEdge = null;
            int k = 0;
            if (graph[s-1]!=null)
            {
                while (graph[s-1].Count != 0)
                {
                    //Wyszukanie następnego skrzyżowania
                    int max = int.MinValue;
                    foreach(Vertex v in graph[s-1])     
                    {
                        if(v.Waga-v.Odleglosc>max)
                        {
                            max = v.Waga - v.Odleglosc;
                            nextEdgeNumer = k;
                            streetNr = v.NumerUlicy;
                        }
                        k++;                       
                    }
                    nextEdge = graph[s - 1].ElementAt(nextEdgeNumer); 
                    k = 0;
                    Attractiveness += nextEdge.Waga - nextEdge.Odleglosc;   //Uwzględnienie atrakcyjności atrakcji do sumy atrakcyjności przejażdżki
                    graph[s - 1].Remove(graph[s - 1].ElementAt(nextEdgeNumer));    //Usunięcie z listy incydencji
                    //Usunięcie kopii tej ulicy z listy incydencji
                    nextEdgeNumer = nextEdge.NumerSkrzyzowania;     
                    foreach(Vertex w in graph[nextEdgeNumer-1])
                    {
                        if(w.NumerSkrzyzowania==s && w.Odleglosc==nextEdge.Odleglosc && w.Waga==nextEdge.Waga)
                        {
                            break;
                        }
                        k++;
                    }
                    graph[nextEdgeNumer-1].Remove(graph[nextEdgeNumer - 1].ElementAt(k));
                    //Wywołanie rekurencyjne
                    Find(graph, nextEdgeNumer, streetNr, edges, result);                   
                }
                //Dodanie ulicy do wyniku 
                result.Push(streetNr);
            }        
        }
        private static int[,] CreateMatrice(LinkedList<Vertex>[] graph)
        {
            int[,] matrice = new int[graph.Length, graph.Length];
            for(int i=0; i<graph.Length; i++)
            {
                foreach (Vertex w in graph[i])
                {
                    matrice[i, w.NumerSkrzyzowania-1] = 1;
                }
            }
            return matrice;
        }
    }
}
