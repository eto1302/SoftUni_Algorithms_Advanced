using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FindBi_ConnectedComponents
{
    public class Edge
    {
        public Edge(int parent, int child)
        {
            this.Parent = parent;
            this.Child = child;
        }

        public int Child { get; set; }

        public int Parent { get; set; }
    }
    public class FindBiConnectedComponents
    {
        private static bool[] visited;
        private static int?[] parent;
        private static int[] depth;
        private static int[] lowpoint;

        private static Stack<Edge> biConnectedComponents;
        private static int ComponentCounter;
        private static Dictionary<int, List<Edge>> graph;

        public static void Main()
        {
            int nodes = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            graph = new Dictionary<int, List<Edge>>();
            lowpoint = new int[nodes];
            depth = new int[nodes];
            parent = new int?[nodes];
            visited = new bool[nodes];
            biConnectedComponents = new Stack<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                string[] parameters = Console.ReadLine().Split();
                int start = int.Parse(parameters[0]);
                int end = int.Parse(parameters[1]);

                if (!graph.ContainsKey(start))
                {
                    graph.Add(start, new List<Edge>());
                }
                if (!graph.ContainsKey(end))
                {
                    graph.Add(end, new List<Edge>());
                }
                graph[start].Add(new Edge(start, end));
                graph[end].Add(new Edge(end, start));
            }
            FindBiConnectedGroups(0, 0);
            Console.WriteLine($"Number of bi-connected components: {ComponentCounter}");
        }

        private static void FindBiConnectedGroups(int node, int d)
        {
            visited[node] = true;
            depth[node] = d;
            lowpoint[node] = d;
            int childCount = 0;
            foreach (var edge in graph[node])
            {
                var childNode = edge.Child;
                if (!visited[childNode])
                {
                    biConnectedComponents.Push(edge);
                    parent[childNode] = node;
                    FindBiConnectedGroups(childNode, d + 1);
                    childCount++;
                    if (lowpoint[childNode] >= depth[node])
                    {
                        ComponentCounter++;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[childNode]);
                }
                else if (childNode != parent[node] && depth[childNode] < depth[node])
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depth[childNode]);
                }
            }
        }
    }
}
