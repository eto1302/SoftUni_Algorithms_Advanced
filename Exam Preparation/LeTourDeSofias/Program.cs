using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Wintellect.PowerCollections;

namespace LeTourDeSofia
{
    class Edge
    {
        public int from;
        public int to;
        public int weight;
    }
    class Program
    {
        public static List<Edge> edges;
        public static int[] d;
        public static OrderedBag<int> priorityQueue;
        public static int source, nodesCount, edgeCount;
        static void Main(string[] args)
        {
            Read();
            Dijkstra();
            Output();
        }

        private static void Output()
        {
            if(d[0] != int.MaxValue) Console.WriteLine(d[0]);
            else Console.WriteLine(d.Count(e => e != int.MaxValue) + 1);
        }

        private static void Dijkstra()
        {
            foreach (var child in edges.Where(e => e.from == source).ToArray())
            {
                d[child.to] = child.weight;
                priorityQueue.Add(child.to);
            }
            while (priorityQueue.Count != 0)
            {
                int currentNode = priorityQueue.RemoveFirst();
                foreach (var child in edges.Where(e => e.from == currentNode).ToArray())
                {
                    if (d[child.to] > d[child.from] + child.weight)
                    {
                        d[child.to] = d[child.from] + child.weight;
                        priorityQueue.Add(child.to);
                    }
                }
            }
        }

        private static void Read()
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgeCount = int.Parse(Console.ReadLine());
            source = int.Parse(Console.ReadLine());
            InitializeDataStructures();
            for (int i = 0; i < edgeCount; i++)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                edges.Add(new Edge
                {
                    from = inputParameters[0],
                    to = inputParameters[1],
                    weight = inputParameters[2]
                });
            }
        }

        private static void InitializeDataStructures()
        {
            edges = new List<Edge>();
            d = new int[nodesCount];
            d = d.Select(e => e = int.MaxValue).ToArray();
            priorityQueue = new OrderedBag<int>(Comparer<int>.Create((first, second) => d[first] - d[second]));
        }
    }
}
