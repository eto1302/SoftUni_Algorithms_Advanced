using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Wintellect.PowerCollections;

namespace CheapTownTour
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
        public static int nodes, edgeCount, result = 0;
        public static bool[] visited;
        static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            KruskalsAlgorithm();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine($"Total cost: {result}");
        }

        private static void KruskalsAlgorithm()
        {
            foreach (var edge in edges.OrderBy(e => e.weight).ToArray())
            {
                if (!visited[edge.first] || !visited[edge.second])
                {
                    visited[edge.first] = true;
                    visited[edge.second] = true;
                    result += edge.weight;
                }
            }
        }

        private static void InitializeDataStructures()
        {
            visited = new bool[nodes];
        }

        private static void Input()
        {
            edges = new List<Edge>();
            nodes = int.Parse(Console.ReadLine());
            edgeCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < edgeCount; i++)
            {
                var inputParameters = Console.ReadLine().Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                edges.Add(new Edge
                {
                    first = inputParameters[0],
                    second = inputParameters[1],
                    weight = inputParameters[2]
                });
            }
        }
    }
}
