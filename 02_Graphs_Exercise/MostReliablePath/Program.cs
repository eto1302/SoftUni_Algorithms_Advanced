using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Wintellect.PowerCollections;

namespace MostReliablePath
{
    public class Edge
    {
        public int first { get; set; }

        public int second { get; set; }

        public double weight { get; set; }
    }
    class Program
    {
        public static List<Edge> graph;
        public static int nodes, edges, source, destination;
        public static int[] prev;
        public static double[] d;
        public static OrderedBag<int> queue;
        static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            DijkstrasAlgorithm();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine($"Most reliable path reliability: {(Math.Round((decimal)d[destination], 4) * 100).ToString("F2")}%");
            List<int> path = new List<int>();
            while (destination != source)
            {
                path.Add(destination);
                destination = prev[destination];
            }
            path.Add(source);
            path.Reverse();
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static void DijkstrasAlgorithm()
        {
            while (queue.Count != 0)
            {
                int currentNode = queue.RemoveFirst();
                foreach (var edge in graph.Where(e => e.first == currentNode || e.second == currentNode).ToArray())
                {
                    int currentDestination = currentNode == edge.first ? edge.second : edge.first;
                    if (d[currentDestination] < d[currentNode] * edge.weight)
                    {
                        d[currentDestination] = d[currentNode] * edge.weight;
                        prev[currentDestination] = currentNode;
                        queue.Add(currentDestination);
                    }
                }
            }
        }

        private static void InitializeDataStructures()
        {
            prev = new int[nodes];
            d = new double[nodes];
            d = d.Select(e => e = double.MinValue).ToArray();
            d[source] = 1;
            queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int)(d[f] - d[s])));
            queue.Add(source);
        }

        private static void Input()
        {
            graph = new List<Edge>();
            nodes = int.Parse(Console.ReadLine());
            edges = int.Parse(Console.ReadLine());
            for (int i = 0; i < edges; i++)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                graph.Add(new Edge
                {
                    first = inputParameters[0],
                    second = inputParameters[1],
                    weight = inputParameters[2] != 100 ? (double)inputParameters[2] / 100 : 1
                });
            }

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());
        }
    }
}
