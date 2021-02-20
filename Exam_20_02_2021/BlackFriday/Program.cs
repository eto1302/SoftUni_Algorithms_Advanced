using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackFriday
{
    class Edge
    {
        public int from;
        public int to;
        public int weight;
    }
    class Program
    {
        public static List<Edge> edges, resultForest;
        public static bool[] visited, tempVisited;
        public static int nodeCount, edgeCount;

        static void Main(string[] args)
        {
            Input();
            Kruskal();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine(resultForest.Sum(e => e.weight));
        }

        private static void Kruskal()
        {
            foreach (var edge in edges.OrderBy(e => e.weight).ToList())
            {
                if (!visited[edge.from] || !visited[edge.to] || IsConnectingComponents(edge))
                {
                    resultForest.Add(edge);
                    visited[edge.from] = true;
                    visited[edge.to] = true;
                }
            }
        }

        private static bool IsConnectingComponents(Edge edge)
        {
            tempVisited = new bool[nodeCount];
            var visitedCount = DFS(resultForest[0].from, resultForest);
            List<Edge> tempEdgeList = new List<Edge>(resultForest);
            tempEdgeList.Add(edge);
            var current = tempEdgeList[0];
            tempVisited = new bool[nodeCount];
            return visitedCount < DFS(current.from, tempEdgeList);
        }

        private static int DFS(int currentNode, List<Edge> tempEdgeList)
        {
            foreach (var edge in tempEdgeList.Where(e => e.from == currentNode || e.to == currentNode).ToArray())
            {
                var currentTo = edge.from == currentNode ? edge.to : edge.from;
                if (tempVisited[currentTo]) continue;
                tempVisited[edge.from] = true;
                tempVisited[edge.to] = true;
                DFS(currentTo, tempEdgeList);
            }

            return tempVisited.Count(e => e == true);
        }

        private static void Input()
        {
            nodeCount = int.Parse(Console.ReadLine());
            edgeCount = int.Parse(Console.ReadLine());
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
            resultForest = new List<Edge>();
            visited = new bool[nodeCount];
        }
    }
}
