using System;
using System.Linq;

namespace RodCutting
{
    class Program
    {
        public static int[] prices;
        public static int length;
        public static int[] bestPrices, bestPrevs;

        public static int CutRod(int length)
        {
            for (int i = 1; i <= length; i++)
            {
                var bestPrice = prices[i];
                var bestPrev = i;
                for (int j = 1; j < i; ++j)
                {
                    if (bestPrices[j] + bestPrices[i - j] > bestPrice)
                    {
                        bestPrice = bestPrices[j] + bestPrices[i - j];
                        bestPrev = j;
                    }
                }

                bestPrices[i] = bestPrice;
                bestPrevs[i] = bestPrev;
            }

            return bestPrices[length];
        }
        static void Main(string[] args)
        {
            prices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            bestPrices = new int[prices.Length];
            bestPrevs = new int[prices.Length];
            length = int.Parse(Console.ReadLine());
            Console.WriteLine(CutRod(length));
            while (length != 0)
            {
                Console.Write($"{bestPrevs[length]} ");
                length -= bestPrevs[length];
            }
        }
    }
}
