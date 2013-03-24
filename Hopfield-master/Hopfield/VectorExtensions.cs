using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Hopfield
{
    public static class VectorExtensions
    {
        public static string ToPrettyString(this Vector v)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < v.Length; ++i)
            {
                sb.AppendFormat("{0:+0;-0} ", v[i]);
            }
            return sb.ToString();
        }

        public static Matrix ToMatrixByColumns(this Vector input, int columnSize)
        {
            int matrixSize = (int)Math.Sqrt(input.Length);
            Matrix columnsMatrix = new Matrix(m: matrixSize, n: matrixSize);
            for (int i = 0; i < matrixSize; ++i)
            {
                double[] column = input.Skip(i * matrixSize).Take(matrixSize).ToArray();
                columnsMatrix.SetColumnVector(new Vector(column), i);
            }
            return columnsMatrix;
        }
    }
}
