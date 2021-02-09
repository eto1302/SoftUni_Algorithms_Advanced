using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Knapsack
{
    public class Item
    {
        public string name { get; set; }
        public int weight { get; set; }
        public int value { get; set; }
        public Item(string name, int weight, int value)
        {
            this.name = name;
            this.weight = weight;
            this.value = value;
        }
    }
    class Program
    {
        public static int[,] dpMatrix;
        public static List<Item> items;
        public static int capacity;
        static void Main(string[] args)
        {
            items = new List<Item>();
            capacity = int.Parse(Console.ReadLine());
            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                var parameters = input.Split().ToArray();
                items.Add(new Item(parameters[0],
                     int.Parse(parameters[1]),
                     int.Parse(parameters[2])));
            }
            dpMatrix = new int[items.Count + 1, capacity + 1];

            for (int itemIndex = 1; itemIndex < dpMatrix.GetLength(0); itemIndex++)
            {
                var currentItem = items[itemIndex - 1];

                for (int currentCapacity = 1; currentCapacity < dpMatrix.GetLength(1); currentCapacity++)
                {
                    if (currentItem.weight > currentCapacity) dpMatrix[itemIndex, currentCapacity] = dpMatrix[itemIndex - 1, currentCapacity];
                    else
                    {
                        dpMatrix[itemIndex, currentCapacity] = Math.Max(
                            dpMatrix[itemIndex - 1, currentCapacity],
                            dpMatrix[itemIndex - 1, currentCapacity - currentItem.weight] + currentItem.value);
                    }
                }
            }

            var selectedItems = new SortedSet<string>();
            var usedWeight = 0;
            var totalValue = 0;
            var row = dpMatrix.GetLength(0) - 1;
            var col = dpMatrix.GetLength(1) - 1;

            while (row > 0 && col > 0)
            {
                if (dpMatrix[row, col] != dpMatrix[row - 1, col])
                {
                    var selectedItem = items[row - 1];

                    selectedItems.Add(selectedItem.name);
                    usedWeight += selectedItem.weight;
                    totalValue += selectedItem.value;

                    col -= selectedItem.weight;
                }

                row--;
            }

            Console.WriteLine($"Total Weight: {usedWeight}");
            Console.WriteLine($"Total Value: {totalValue}");
            Console.WriteLine(string.Join(Environment.NewLine, selectedItems));
        }
    }
}
