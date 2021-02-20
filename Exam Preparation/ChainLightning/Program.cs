using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Wintellect.PowerCollections;

namespace ChainLightning
{
    public class Edge
    {
        public int from;
        public int to;
        public int weight;
    }
    class Program
    {
        public static List<Edge> edges;
        public static List<Edge> MSTResult;
        public static int[] damage, currentDamage;
        public static bool[] visited;
        public static int nodesCount, edgesCount, lightningCount;
        public static OrderedBag<Edge> priorityQueue;

        public static void Main(string[] args)
        {
            Read();
            ReadLightnings();
            Console.WriteLine(damage.Max());
        }
        public static void InitializeDataStructures()
        {
            edges = new List<Edge>();
            damage = new int[nodesCount];
            visited = new bool[nodesCount];
            currentDamage = new int[nodesCount];
            priorityQueue = new OrderedBag<Edge>(Comparer<Edge>.Create((first, second) => first.weight - second.weight));
        }
        public static void Read()
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());
            lightningCount = int.Parse(Console.ReadLine());
            InitializeDataStructures();
            for (int i = 0; i < edgesCount; ++i)
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
        public static void ReadLightnings()
        {
            for (int i = 0; i < lightningCount; ++i)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                visited = visited.Select(e => false).ToArray();
                currentDamage = currentDamage.Select(e => 0).ToArray();
                currentDamage[inputParameters[0]] = inputParameters[1];
                visited[inputParameters[0]] = true;
                PrimAlgorithm(inputParameters[0]);
            }
        }
        private static void PrimAlgorithm(int startNode)
        {
            priorityQueue.AddMany(edges.Where(e => e.to == startNode || e.@from == startNode));
            while (priorityQueue.Count != 0)
            {
                Edge currentEdge = priorityQueue.RemoveFirst();
                string current = $"{currentEdge.@from} {currentEdge.to} {currentEdge.weight}";
                if (!visited[currentEdge.to] || !visited[currentEdge.@from])
                {
                    if (currentDamage[currentEdge.@from] != 0) currentDamage[currentEdge.to] = currentDamage[currentEdge.@from] / 2;
                    else if (currentDamage[currentEdge.to] != 0) currentDamage[currentEdge.@from] = currentDamage[currentEdge.to] / 2;
                    var destination = visited[currentEdge.@from] ? currentEdge.to : currentEdge.@from;
                    visited[currentEdge.to] = true;
                    visited[currentEdge.@from] = true;
                    
                    priorityQueue.AddMany(edges.Where(e => e.to == destination || e.@from == destination));
                }
            }

            for (int i = 0; i < nodesCount; i++)
            {
                damage[i] += currentDamage[i];
            }

        }
    }
}