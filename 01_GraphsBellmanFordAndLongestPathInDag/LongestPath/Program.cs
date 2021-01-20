using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bellman_Ford
{
    class Edge
    {
        public int from { get; set; }
        public int to { get; set; }
        public int weight { get; set; }
    }
    class Program
    {
        public static int n, e;
        public static List<Edge> edges;
        public static int source, destination;
        public static int[] d;
        public static Stack<int> topologicallySortedVertices;
        static void Main(string[] args)
        {
            Input();
            InitializeStructures();
            TopologicalSort();
            FindLongestPath();
            Output();
        }

        private static void FindLongestPath()
        {
            while (topologicallySortedVertices.Count != 0)
            {
                int currentVertex = topologicallySortedVertices.Pop();
                foreach (var edge in edges.Where(e => e.from == currentVertex).ToArray())
                {
                    if (d[edge.to - 1] < d[edge.from - 1] + edge.weight)
                    {
                        d[edge.to - 1] = d[edge.from - 1] + edge.weight;
                    }
                }
            }


        }

        private static void Output()
        {
            Console.WriteLine(d[destination - 1]);
        }
        private static void TopologicalSort()
        {
            for (int i = 1; i < n + 1; i++)
            {
                TraverseChildren(i);
            }
        }

        private static void TraverseChildren(int i)
        {
            foreach (var edge in edges.Where(e => e.from == i).ToArray())
            {
                TraverseChildren(edge.to);
            }
            if (!topologicallySortedVertices.Contains(i)) topologicallySortedVertices.Push(i);
        }

        private static void InitializeStructures()
        {
            d = new int[n];
            topologicallySortedVertices = new Stack<int>();
            d = d.Select(e => e = int.MinValue).ToArray();
            d[source - 1] = 0;
        }

        private static void Input()
        {
            n = int.Parse(Console.ReadLine());
            e = int.Parse(Console.ReadLine());
            edges = new List<Edge>();
            for (int i = 0; i < e; i++)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                edges.Add(new Edge { from = inputParameters[0], to = inputParameters[1], weight = inputParameters[2] });
            }

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());
        }
    }
}
