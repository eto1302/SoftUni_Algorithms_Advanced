using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Xml;

namespace MaximumTasksAssignment
{
    class Program
    {
        public static int[,] graph;
        private static int[] parents;
        private static int people, tasks, nodes, start, target;
        static void Main(string[] args)
        {
            people = int.Parse(Console.ReadLine());
            tasks = int.Parse(Console.ReadLine());

            nodes = people + tasks + 2;

            ReadGraph();

            parents = new int[nodes];
            Array.Fill(parents, -1);

            start = 0;
            target = nodes - 1;

            while (BFS(start, target))
            {
                var node = target;
                while (node != start)
                {
                    var parent = parents[node];

                    graph[parent, node] = 0;
                    graph[node, parent] = 1;

                    node = parent;
                }
            }

            for (int person = 1; person <= people; person++)
            {
                for (int task = people + 1; task <= people+tasks; task++)
                {
                    if (graph[task, person] > 0)
                    {
                        Console.WriteLine($"{(char)('A' - 1 + person)}-{task - people}");
                    }
                }
            }
        }

        private static bool BFS(int start,int target)
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();

            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == target) return true;

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        parents[child] = node;
                        visited[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }

            return false;
        }

        private static void ReadGraph()
        {
            graph = new int[nodes,nodes];
            var start = 0;
            var target = nodes - 1;

            for (int person = 1; person <= people; person++)
            {
                graph[start, person] = 1;
            }

            for (int task = people + 1; task <= people + tasks; task++)
            {
                graph[task, target] = 1;
            }

            for (int person = 1; person <= people; person++)
            {
                var personTasks = Console.ReadLine();
                for (int task = 0; task < personTasks.Length; task++)
                {
                    if (personTasks[task] == 'Y')
                    {
                        graph[person, people + 1 + task] = 1;
                    }
                }
            }
        }
    }
}

