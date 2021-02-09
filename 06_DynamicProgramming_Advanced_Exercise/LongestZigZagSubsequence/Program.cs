using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LongestZigZagSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {

            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[,] Dp = new int[2, numbers.Length];
            Dp[0, 0] = 1;
            Dp[1, 0] = 1;

            int[,] parent = new int[2, numbers.Length];

            parent[0, 0] = -1;
            parent[1, 0] = -1;

            var bestSize = 0;
            var lastRowIdx = 0;
            var lastColIdx = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                var currentNumber = numbers[current];
                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevNumber = numbers[prev];

                    if (currentNumber > prevNumber &&
                        Dp[1, prev] + 1 >= Dp[0, current])
                    {
                        Dp[0, current] = Dp[1, prev] + 1;
                        parent[0, current] = prev;
                    }

                    if (currentNumber < prevNumber &&
                        Dp[0, prev] + 1 >= Dp[1, current])
                    {
                        Dp[1, current] = Dp[0, prev] + 1;
                        parent[1, current] = prev;
                    }
                }

                if (Dp[0, current] > bestSize)
                {
                    bestSize = Dp[0, current];

                    lastRowIdx = 0;
                    lastColIdx = current;
                }

                if (Dp[1, current] > bestSize)
                {
                    bestSize = Dp[1, current];
                    lastRowIdx = 1;
                    lastColIdx = current;
                }
            }
            var zigZagSeq = new Stack<int>();

            while (lastColIdx != -1)
            {
                zigZagSeq.Push(numbers[lastColIdx]);

                lastColIdx = parent[lastRowIdx, lastColIdx];

                lastRowIdx = lastRowIdx == 0 ? 1 : 0;
            }

            Console.WriteLine(string.Join(" ", zigZagSeq));
        }
    }
}
