using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MatrixToString(GenerateMagicMatrix()));
        }

        static String MatrixToString(int[,] matrix)
        {
            var sb = new StringBuilder();
            for (int r = 0; r < 6; ++r)
            {
                for (int c = 0; c < 6; ++c)
                {
                    sb.Append(matrix[r, c] + " ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        static int[,] GenerateMagicMatrix()
        {
            var random = new Random(DateTime.Now.Millisecond);
            int[,] matrix = new int[6, 6];
            while (true)
            {
                var numbers = new HashSet<int>();
                for (int i = 1; i <= 36; ++i)
                {
                    numbers.Add(i);
                }

                for (int r = 0; r < 6; ++r)
                {
                    for (int c = 0; c < 6; ++c)
                    {
                        int randomIndex = random.Next(numbers.Count);
                        int chosenNumber = numbers.ElementAt(randomIndex);
                        matrix[r, c] = chosenNumber;
                        numbers.Remove(chosenNumber);
                    }
                }

                bool canLeave = true;
                for (int r = 0; r < 6; ++r)
                {
                    for (int c = 0; c < 6; ++c)
                    {
                        if (RowSum(matrix, r) != 111 && ColumnSum(matrix, c) != 111)
                        {
                            canLeave = false;
                            break;
                        }
                    }
                    if (!canLeave)
                    {
                        break;
                    }
                }
                Console.WriteLine(MatrixToString(matrix));
                if (canLeave)
                {
                    break;
                }
            }
            return matrix;
        }

        static int RowSum(int[,] matrix, int row)
        {
            int sum = 0;
            for (int c = 0; c < 6; ++c)
            {
                sum += matrix[row, c];
            }
            return sum;
        }

        static int ColumnSum(int[,] matrix, int column)
        {
            int sum = 0;
            for (int r = 0; r < 6; ++r)
            {
                sum += matrix[r, column];
            }
            return sum;
        }
    }
}
