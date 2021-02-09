using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BattlePoints
{
    class Program
    {
        public static int[] energies; //costs
        public static int[] points;   //values 
        public static int[,] DPMatrix;
        static void Main(string[] args)
        {
            energies = Console.ReadLine().Split().Select(int.Parse).ToArray();
            points = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int initialEnergy = int.Parse(Console.ReadLine());  
            DPMatrix = new int[energies.Length + 1, initialEnergy + 1];
            for (int i = 1; i < DPMatrix.GetLength(0); i++)
            {
                for (int j = 1; j < DPMatrix.GetLength(1); j++)
                {
                    if (j >= energies[i - 1])
                    {
                        DPMatrix[i, j] = Math.Max(DPMatrix[i - 1, j], DPMatrix[i - 1, j - energies[i - 1]] + points[i - 1]);
                    }
                    else
                    {
                        DPMatrix[i, j] = DPMatrix[i - 1, j];
                    }
                    
                }
            }
            
            Console.WriteLine(DPMatrix[DPMatrix.GetLength(0) - 1, DPMatrix.GetLength(1) - 1]);
        }
    }
}
