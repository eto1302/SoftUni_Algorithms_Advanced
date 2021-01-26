using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BigTrip
{
    public class Edge
    {
        public int first { get; set; }

        public int second { get; set; }

        public int weight { get; set; }
    }
    class Program
    {
        public static List<Edge> graph;
        public static List<int> nodes;
        public static int nodesCount, edges, source, destination;
        public static int[] prev;
        public static int[] d;
        static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            LongestPath();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine(d[destination]);
            List<int> path = new List<int>();
            while (destination != source)
            {
                path.Add(destination + 1);
                destination = prev[destination];
            }
            path.Add(source + 1);
            path.Reverse();
            Console.WriteLine(string.Join(" ", path));
        }

        private static void LongestPath()
        {
            TopologicalSort();
            foreach (var node in nodes)
            {
                foreach (var edge in graph.Where(e => e.first == node).ToArray())
                {
                    if (d[edge.second] < d[edge.first] + edge.weight)
                    {
                        d[edge.second] = d[edge.first] + edge.weight;
                        prev[edge.second] = edge.first;
                    }
                }
            }
        }

        private static void TopologicalSort()
        {
            for (int i = 0; i < nodesCount; ++i)
            {
                TraverseChildren(i);
            }

            nodes.Reverse();
        }

        private static void TraverseChildren(int i)
        {
            foreach (var edge in graph.Where(e=> e.first == i).ToArray())
            {
                TraverseChildren(edge.second);
            }
            if(!nodes.Contains(i)) nodes.Add(i);
        }

        private static void InitializeDataStructures()
        {
            prev = new int[nodesCount];
            d = new int[nodesCount];
            nodes = new List<int>();
            d = d.Select(e => e = int.MinValue).ToArray();
            d[source] = 0;
        }

        private static void Input()
        {
            graph = new List<Edge>();
            nodesCount = int.Parse(Console.ReadLine());
            edges = int.Parse(Console.ReadLine());
            for (int i = 0; i < edges; i++)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                graph.Add(new Edge
                {
                    first = inputParameters[0] - 1,
                    second = inputParameters[1] - 1,
                    weight = inputParameters[2]
                });
            }

            source = int.Parse(Console.ReadLine()) - 1;
            destination = int.Parse(Console.ReadLine()) - 1;
        }
    }
}
