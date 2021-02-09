using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestIncreasingSubsequence
{
    class Program
    {
        public static int[] numbers;
        public static int[] lengths;
        public static int[] prev;
        static void Main(string[] args)
        {
            numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            lengths = new int[numbers.Length];
            prev = new int[numbers.Length];
            prev[0] = -1;
            lengths[0] = 1;
            int indexOfMaximumLength = -1, maxLen = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                var bestLength = 1;
                var prevIndex = -1;
                var currentNum = numbers[i];
                for (int j = i - 1; j >= 0 ; j--)
                {
                    var previousNumber = numbers[j];
                    if (previousNumber < currentNum && bestLength <= lengths[j] + 1)
                    {
                        bestLength = lengths[j] + 1;
                        prevIndex = j;
                    }
                }

                lengths[i] = bestLength;
                prev[i] = prevIndex;
                if (bestLength > maxLen)
                {
                    maxLen = bestLength;
                    indexOfMaximumLength = i;
                }
            }
            List<int> resultNumbers = new List<int>();
            while (indexOfMaximumLength != -1)
            {
                resultNumbers.Add(numbers[indexOfMaximumLength]);
                indexOfMaximumLength = prev[indexOfMaximumLength];
            }
            resultNumbers.Reverse();
            Console.WriteLine(string.Join(" ", resultNumbers));
        }
    }
}
