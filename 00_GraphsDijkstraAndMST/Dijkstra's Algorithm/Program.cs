using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Dijkstra_s_Algorithm
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
        public static int[] d, prev;
        public static OrderedBag<int> queue;
        public static int startNode, endNode;
        public static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            DijkstraAlgorithm();
            Output();
        }

        private static void DijkstraAlgorithm()
        {
            while (queue.Count != 0)
            {
                var minNode = queue.RemoveFirst();
                var childrenEdges = edges.Where(e => e.first == minNode || e.second == minNode).ToArray();
                if (d[minNode] == int.MaxValue) break;
                foreach (var edge in childrenEdges)
                {
                    var child = minNode == edge.first ? edge.second : edge.first;
                    var weight = edge.weight;
                    if (d[child] == int.MaxValue)
                    {
                        queue.Add(child);

                    }

                    int newDistance = d[minNode] + weight;
                    if (newDistance < d[child])
                    {
                        d[child] = newDistance;
                        prev[child] = minNode;
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => d[f] - d[s]));
                    }
                }
            }
        }

        private static void Output()
        {
            if (d[endNode] == int.MaxValue) Console.WriteLine("There is no such path.");
            else
            {
                Console.WriteLine(d[endNode]);
                List<int> path = new List<int>();
                while (true)
                {
                    path.Add(endNode);
                    int parent = prev[endNode];
                    endNode = parent;
                    if (endNode == startNode) break;
                }
                path.Add(startNode);
                path.Reverse();
                Console.WriteLine(string.Join(" ", path));
            }
        }

        private static void InitializeDataStructures()
        {
            d = new int[Math.Max(edges.Max(e => e.first), edges.Max(e => e.second)) + 1];
            prev = new int[Math.Max(edges.Max(e => e.first), edges.Max(e => e.second)) + 1];
            d = d.Select(e => int.MaxValue).ToArray();
            d[startNode] = 0;
            queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => d[f] - d[s]));
            queue.Add(startNode);
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
            startNode = int.Parse(Console.ReadLine());
            endNode = int.Parse(Console.ReadLine());
        }
    }
}
