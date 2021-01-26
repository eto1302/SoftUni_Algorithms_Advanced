using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CableNetwork
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
        public static List<Edge> resultEdges;
        public static int nodesCount, edgesCount, budget, initialBudget;
        public static bool[] visited;
        static void Main(string[] args)
        {
            Input();
            InitializeDataStructures();
            PrimsAlgorithm();
            Output();
        }

        private static void Output()
        {
            Console.WriteLine($"Budget used: {initialBudget - budget}");
        }

        private static void PrimsAlgorithm()
        {
            foreach (var edge in edges.OrderBy(e => e.weight).ToArray())
            {
                if (budget < edge.weight) break;
                if ((!visited[edge.first] && visited[edge.second]) || (visited[edge.first] && !visited[edge.second]))
                {
                    budget -= edge.weight;
                    visited[edge.first] = true;
                    visited[edge.second] = true;
                }
            }
        }

        private static void InitializeDataStructures()
        {

        }

        private static void Input()
        {
            edges = new List<Edge>();
            resultEdges = new List<Edge>();
            budget = int.Parse(Console.ReadLine());
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());
            initialBudget = budget;
            visited = new bool[nodesCount];
            for (int i = 0; i < edgesCount; i++)
            {
                string input = Console.ReadLine();
                var inputParameters = input.Split().Take(3).Select(int.Parse).ToArray();

                edges.Add(new Edge
                {
                    first = inputParameters[0],
                    second = inputParameters[1],
                    weight = inputParameters[2]
                });
                if (input.Split().Length == 4)
                {
                    resultEdges.Add(new Edge
                    {
                        first = inputParameters[0],
                        second = inputParameters[1],
                        weight = inputParameters[2]
                    });
                    visited[inputParameters[0]] = true;
                    visited[inputParameters[1]] = true;
                }


            }
        }
    }
}
