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
        public static int[] d, prev;
        public static bool containsNegativeCycle;
        static void Main(string[] args)
        {
            Input();
            InitializeStructures();
            BellmanFordAlgorithm();
            Output();
        }

        private static void Output()
        {
            if (containsNegativeCycle)
            {
                Console.WriteLine("Negative Cycle Detected");
                return;
            }
            Console.WriteLine(GetPath(destination));
            Console.WriteLine(d[destination - 1]);
        }

        private static string GetPath(int i)
        {
            List<int> path = new List<int>();
            while (i != source - 1)
            {
                path.Add(i);
                i = prev[i - 1];
            }
            path.Reverse();
            return string.Join(" ", path);
        }

        private static void BellmanFordAlgorithm()
        {
            for (int i = 0; i < n; ++i)
            {
                bool hasChanged = false;
                for (int j = 0; j < n; ++j)
                {
                    if (d[j] == int.MaxValue) continue;
                    foreach (var edge in edges.Where(e => e.from - 1 == j).ToArray())
                    {
                        if (d[j] + edge.weight < d[edge.to - 1])
                        {
                            d[edge.to - 1] = d[j] + edge.weight;
                            prev[edge.to - 1] = j + 1;
                            hasChanged = true;
                        }
                    }
                }
                if (!hasChanged) break;
            }

            foreach (var edge in edges)
            {
                if (d[edge.to - 1] > d[edge.from - 1] + edge.weight)
                {
                    containsNegativeCycle = true;
                    return;
                }
            }
        }

        private static void InitializeStructures()
        {
            d = new int[n];
            prev = new int[n];
            d = d.Select(e => e = int.MaxValue).ToArray();
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
