using System;
using System.Collections.Generic;
using System.Linq;

namespace Cable_Merchant
{
    class Program
    {
        public static int[] bestPrices;
        public static List<int> sequence;
        static void Main(string[] args)
        {
            sequence = new List<int> {0};
            sequence.AddRange(
                Console.ReadLine().
                    Split().
                    Select(int.Parse));

            int connectorPrice = int.Parse(Console.ReadLine());

            bestPrices = new int[sequence.Count + 1];

            for (int length = 1; length < sequence.Count; length++)
            {
                var bestPrice = CutCable(length, connectorPrice);
                Console.Write(bestPrice + " ");
            }
        }

        private static int CutCable(int length, int connectorPrice)
        {
            if (length == 0) return 0;
            if (bestPrices[length] != 0) return bestPrices[length];
            var bestPrice = sequence[length];
            for (int i = 1; i < length; i++)
            {
                var currentPrice = sequence[i] + CutCable(length - i, connectorPrice) - 2 * connectorPrice;
                if (currentPrice > bestPrice)
                {
                    bestPrice = currentPrice;
                }
            }

            bestPrices[length] = bestPrice;
            return bestPrice;
        }
    }
}
