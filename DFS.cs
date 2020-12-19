using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WariantBZad5KamilŁozowski
{
    static class DFS
    {
        public static Stack<int> Find(LinkedList<Vertex>[] graph, int s)
        {
            int[] edges = new int[graph.Length];
            Stack<int> result = new Stack<int>();
            for(int i=0; i<edges.Length; i++)
            {
                edges[i] = 4;
            }
            //int[,] matrice = CreateMatrice(graph);           
            Find(graph, s, edges, result);
            return result;
        }
        private static void Find(LinkedList<Vertex>[] graph, int s, int[] edges, Stack<int> result)
        {
            int nextEdgeNumer = 0;
            Vertex nextEdge = null;
            int k = 0;
            if (graph[s-1]!=null)
            {
                while (graph[s-1].Count != 0)
                {                   
                    nextEdge = graph[s - 1].Last.Value;
                    graph[s - 1].RemoveLast();
                    nextEdgeNumer = nextEdge.Numer;
                    foreach(Vertex w in graph[nextEdgeNumer-1])
                    {
                        if(w.Numer==s && w.Odleglosc==nextEdge.Odleglosc && w.Waga==nextEdge.Waga)
                        {
                            break;
                        }
                        k++;
                    }
                    graph[nextEdgeNumer-1].Remove(graph[nextEdgeNumer - 1].ElementAt(k));

                    Find(graph, nextEdgeNumer, edges, result);
                }
                result.Push(s);
            }
           
        }
        private static int[,] CreateMatrice(LinkedList<Vertex>[] graph)
        {
            int[,] matrice = new int[graph.Length, graph.Length];
            for(int i=0; i<graph.Length; i++)
            {
                foreach (Vertex w in graph[i])
                {
                    matrice[i, w.Numer-1] = 1;
                }
            }
            return matrice;
        }
    }
}
