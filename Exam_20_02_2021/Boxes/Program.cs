using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    class Box
    {
        public int width;
        public int depth;
        public int height;
    }
    class Program
    {
        public static Box[] boxes;
        public static int[] currentCount, prev;
        public static int maxBoxCount = 0, maxBoxCountIndex = -1;
        static void Main(string[] args)
        {
            ReadBoxes();
            Solve();
            Output();
        }

        private static void Output()
        {
            if (boxes.Length == 1)
            {
                Console.WriteLine($"{boxes[0].width} {boxes[0].depth} {boxes[0].height}");
                return;
            }
            Stack<Box> resultStack = new Stack<Box>();
            while (maxBoxCountIndex != -1)
            {
                resultStack.Push(boxes[maxBoxCountIndex]);
                maxBoxCountIndex = prev[maxBoxCountIndex];
            }

            Console.WriteLine(string.Join(Environment.NewLine, resultStack.Select(e => $"{e.width} {e.depth} {e.height}")));
        }

        private static void Solve()
        {
            currentCount[0] = 0;
            prev[0] = -1;
            for (int i = 1; i < boxes.Length; i++)
            {
                var currentBox = boxes[i];
                prev[i] = -1;
                currentCount[i] = 0;
                for (int j = i - 1; j >= 0 ; j--)
                {
                    var previousBox = boxes[j];
                    if (previousBox.height < currentBox.height &&
                        previousBox.width < currentBox.width &&
                        previousBox.depth < currentBox.depth &&
                        currentCount[i] <= currentCount[j])
                    {
                        prev[i] = j;
                        currentCount[i] = currentCount[j] + 1;
                    }
                }

                if (maxBoxCount < currentCount[i])
                {
                    maxBoxCount = currentCount[i];
                    maxBoxCountIndex = i;
                }
            }
        }

        private static void ReadBoxes()
        {
            int n = int.Parse(Console.ReadLine());
            InitializeDataStructures(n);
            for (int i = 0; i < n; i++)
            {
                var inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                boxes[i] = new Box
                {
                    width = inputParameters[0],
                    depth = inputParameters[1],
                    height = inputParameters[2]
                };
            }
        }

        private static void InitializeDataStructures(int n)
        {
            currentCount = new int[n];
            prev = new int[n];
            boxes = new Box[n];
        }
    }
}
