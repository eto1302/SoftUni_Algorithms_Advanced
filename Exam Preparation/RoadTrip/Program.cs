using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Knapsack
{
    class Program
    {
        public static int[,] DPMatrix;
        public static int capacity;
        public static int[] values, weights;
        public static void Main(string[] args)
        {
            Read();
            FillDPMatrix();
            Console.WriteLine("Maximum value: " + DPMatrix[DPMatrix.GetLength(0) - 1, DPMatrix.GetLength(1) - 1]);
        }
        public static void Read()
        {
            values = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            weights = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            capacity = int.Parse(Console.ReadLine());
            DPMatrix = new int[values.Count() + 1, capacity + 1];
        }
        public static void FillDPMatrix()
        {
            for (int i = 1; i < DPMatrix.GetLength(0); ++i)
            {
                int currentValue = values[i - 1];
                int currentWeight = weights[i - 1];
                for (int j = 1; j < DPMatrix.GetLength(1); ++j)
                {
                    if (j < currentWeight) DPMatrix[i, j] = DPMatrix[i - 1, j];
                    else DPMatrix[i, j] = Math.Max(
                        DPMatrix[i - 1, j],
                        DPMatrix[i - 1, j - currentWeight] + currentValue);
                }
            }
        }
    }
}