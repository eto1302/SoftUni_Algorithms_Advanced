using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace EmergencyPlan
{
    class Edge
    {
        public int from;
        public int to;
        public TimeSpan weight;
    }
    class Program
    {
        public static long[] prev, exits;
        public static TimeSpan[] d;
        public static List<Edge> edges;
        public static int nodesCount, edgeCount;
        public static TimeSpan maximumTime;
        public static OrderedBag<int> priorityQueue;
        static void Main(string[] args)
        {
            Input();
            for (int i = 0; i < nodesCount; i++)
            {
                if (exits.Contains(i)) continue;
                d = d.Select(e => e = TimeSpan.MaxValue).ToArray();
                prev = prev.Select(e => e = -1).ToArray();
                d[i] = new TimeSpan(0,0,0);
                priorityQueue.Add(i);
                Dijkstra();
                var exitTime = TimeSpan.MaxValue;
                foreach (var exit in exits)
                {
                    if (d[exit] == TimeSpan.MaxValue) continue;
                    if (d[exit] < exitTime)
                    {
                        exitTime = d[exit];
                    }
                }
                if(exitTime == TimeSpan.MaxValue) Console.WriteLine($"Unreachable {i} (N/A)");
                else if(exitTime > maximumTime) Console.WriteLine($"Unsafe {i} ({exitTime})");
                else Console.WriteLine($"Safe {i} ({exitTime})");
            }
        }

        private static void Dijkstra()
        {
            while (priorityQueue.Count != 0)
            {
                var currentNode = priorityQueue.RemoveFirst();
                foreach (var child in edges.Where(e => e.from == currentNode))
                {
                    if (d[child.to].CompareTo(d[child.from] + child.weight) > 0)
                    {
                        d[child.to] = d[child.from] + child.weight;
                        prev[child.to] = child.from;
                        priorityQueue.Add(child.to);
                    }
                }
            }
        }

        private static void Input()
        {
            nodesCount = int.Parse(Console.ReadLine());
            InitializeDataStructures();
            exits = Console.ReadLine().Split().Select(long.Parse).ToArray();
            edgeCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < edgeCount; i++)
            {
                var inputParameters = Console.ReadLine().Split().ToArray();
                var from = int.Parse(inputParameters[0]);
                var to = int.Parse(inputParameters[1]);
                var minutes = int.Parse(inputParameters[2].Split(':').ToArray()[0]);
                var seconds = int.Parse(inputParameters[2].Split(':').ToArray()[1]);
                var weight = new TimeSpan(0, minutes, seconds);
                var edge = new Edge
                {
                    from = from,
                    to = to,
                    weight = weight
                };
                edges.Add(edge);
                var newEdge = new Edge
                {
                    from = edge.to,
                    to = edge.from,
                    weight = edge.weight
                };
                newEdge.from = edge.to;
                newEdge.to = edge.from;
                edges.Add(newEdge);
            }

            var input = Console.ReadLine().Split(':').Select(int.Parse).ToArray();
            maximumTime = new TimeSpan(0, input[0], input[1]);
        }

        private static void InitializeDataStructures()
        {
            d = new TimeSpan[nodesCount];
            prev = new long[nodesCount];
            priorityQueue = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int)(d[f].TotalSeconds - d[s].TotalSeconds)));
            edges = new List<Edge>();
        }
    }
}
