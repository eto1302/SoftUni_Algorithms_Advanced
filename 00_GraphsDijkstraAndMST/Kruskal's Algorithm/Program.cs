using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Kruskal_s_Algorithm
{
    public class Edge
    {
        public int first { get; set; }
        public int second { get; set; }
        public int weight { get; set; }
    }

    class Program
    {
        public static List<Edge> edges;
        public static List<Edge> resultForest;
        public static bool[] visited;
        public static void Main(string[] args)
        {
            Input();
            KruskalAlgorithm();
            Output();
        }

        private static void KruskalAlgorithm()
        {
            foreach (var edge in edges.OrderBy(e => e.weight).ToList())
            {
                if (!visited[edge.first] || !visited[edge.second])
                {
                    resultForest.Add(new Edge
                    {
                        first = edge.first,
                        second = edge.second,
                        weight = edge.weight
                    });
                    visited[edge.first] = true;
                    visited[edge.second] = true;
                }
            }
        }

        private static void Output()
        {
            foreach (var edge in resultForest.OrderBy(e => e.first).ThenBy(e => e.second))
            {
                Console.WriteLine(edge.first + " - " + edge.second);
            }
        }
        private static void Input()
        {
            edges = new List<Edge>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var inputParameters = Console.ReadLine().Split(", ")
                    .Select(int.Parse).ToArray();

                edges.Add(new Edge
                {
                    first = inputParameters[0],
                    second = inputParameters[1],
                    weight = inputParameters[2]

                });

            }
            visited = new bool[Math.Max(edges.Max(e => e.first), edges.Max(e => e.second)) + 1];
            resultForest = new List<Edge>();
        }
    }
}

