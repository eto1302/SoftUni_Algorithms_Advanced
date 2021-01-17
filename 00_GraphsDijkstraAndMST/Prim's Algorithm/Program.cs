using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Prim_s_Algorithm
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
        public static OrderedBag<Edge> queue;
        public static List<Edge> MSTResult;
        public static bool[] visited;

        public static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            PrimAlgorithm();
            Output();
        }

        private static void PrimAlgorithm()
        {

            while (visited.Count(e => e == false) != 0)
            {
                int currentNode = Array.FindIndex(visited, w => w == false);
                queue.AddMany(edges.Where(e => e.first == currentNode || e.second == currentNode));
                while (queue.Count != 0)
                {
                    Edge currentEdge = queue.RemoveFirst();
                    if (!visited[currentEdge.first] || !visited[currentEdge.second])
                    {
                        MSTResult.Add(new Edge { first = currentEdge.first, second = currentEdge.second, weight = currentEdge.weight });
                        visited[currentEdge.first] = true;
                        visited[currentEdge.second] = true;
                        currentNode = currentNode == currentEdge.first ? currentEdge.second : currentEdge.second;
                        queue.AddMany(edges.Where(e => e.first == currentNode || e.second == currentNode));
                    }
                }
            }

        }

        private static void Output()
        {
            foreach (var edge in MSTResult.OrderBy(e => e.first).ThenBy(e => e.second))
            {
                Console.WriteLine($"{edge.first} - {edge.second}");
            }
        }

        private static void InitializeDataStructures()
        {
            queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.weight - s.weight));
            visited = new bool[Math.Max(edges.Max(e => e.first), edges.Max(e => e.second)) + 1];
            MSTResult = new List<Edge>();
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
        }
    }
}
