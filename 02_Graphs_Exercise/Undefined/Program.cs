using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Undefined
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
        public static int nodes, edges, source, destination;
        public static int[] prev;
        public static int[] d;
        public static bool containsNegativeCycle;
        static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            BellmanFordsAlgorithm();
            Output();
        }

        private static void Output()
        {
            if (containsNegativeCycle)
            {
                Console.WriteLine($"Undefined");
                return;
            }

            List<int> path = new List<int>();
            int tempDestination = destination;
            while (tempDestination != source)
            {
                path.Add(tempDestination + 1);
                tempDestination = prev[tempDestination];
            }
            path.Add(source + 1);
            path.Reverse();
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(d[destination]);
        }

        private static void BellmanFordsAlgorithm()
        {
            for (int i = 0; i < nodes; i++)
            {
                bool hasChanged = false;
                for (int j = 0; j < nodes; j++)
                {
                    if (d[j] == int.MaxValue) continue;
                    foreach (var edge in graph.Where(e => e.first == j).ToArray())
                    {
                        if (d[edge.second] > d[edge.first] + edge.weight)
                        {
                            d[edge.second] = d[edge.first] + edge.weight;
                            prev[edge.second] = j;
                            hasChanged = true;
                        }
                    }
                }
                if (!hasChanged) break;
                hasChanged = false;
            }

            foreach (var edge in graph)
            {
                if (d[edge.second] > d[edge.first] + edge.weight)
                {
                    containsNegativeCycle = true;
                    return;
                }
            }
        }

        private static void InitializeDataStructures()
        {
            prev = new int[nodes];
            d = new int[nodes];
            d = d.Select(e => e = int.MaxValue).ToArray();
            d[source] = 0;
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
