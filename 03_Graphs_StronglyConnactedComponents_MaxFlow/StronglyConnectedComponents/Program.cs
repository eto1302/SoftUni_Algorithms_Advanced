using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Xml;

namespace StronglyConnectedComponents
{
    class Program
    {
        public static int nodesCount;
        public static List<int>[] graph, reversedGraph;
        public static Stack<int> stack;
        public static bool[] visited;
        static void Main(string[] args)
        {
            Input();
            InitializeVariables();
            for (int currentNode = 0; currentNode < nodesCount; ++currentNode) DFS(currentNode, graph, visited, stack);
            reversedGraph = ReverseGraph();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine("Strongly Connected Components: ");
            var connectedComponents = FindSCC();
            foreach (var componentsComponent in connectedComponents)
            {
                //{10}
                Console.WriteLine($@"{{{string.Join(", ", componentsComponent)}}}");
            }
        }

        private static List<Stack<int>> FindSCC()
        {
            visited = new bool[nodesCount];
            var connectedComponents = new List<Stack<int>>();
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!visited[node])
                {
                    var componentElements = new Stack<int>();
                    DFS(node, reversedGraph, visited, componentElements);
                    connectedComponents.Add(componentElements);
                }
            }

            return connectedComponents;
        }

        private static List<int>[] ReverseGraph()
        {
            var tempReversedGraph = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                tempReversedGraph[i] = new List<int>();
            }

            for (int node = 0; node < nodesCount; node++)
            {
                var children = graph[node];
                foreach (var child in children)
                {
                    tempReversedGraph[child].Add(node);
                }
            }

            return tempReversedGraph;
        }

        private static void DFS(int currentNode, List<int>[] graph, bool[] visited, Stack<int> stack)
        {

            if (visited[currentNode])
                return;
            visited[currentNode] = true;
            foreach (int child in graph[currentNode].Where(e => e != null).ToArray())
            {
                DFS(child, graph, visited, stack);
            }
            stack.Push(currentNode);
        }

        private static void InitializeVariables()
        {
            visited = new bool[nodesCount];
            stack = new Stack<int>();
        }

        private static void Input()
        {
            nodesCount = int.Parse(Console.ReadLine());
            graph = new List<int>[nodesCount];
            graph = graph.Select(e => e = new List<int>()).ToArray();
            int lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                var inputParameters = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToArray();
                int parent = inputParameters[0];
                var children = inputParameters.Skip(1).ToList();
                graph[parent] = children;
            }

            
        }
    }
}
