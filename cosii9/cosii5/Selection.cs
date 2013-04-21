using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace cosii5
{
    public sealed class Selection
    {
        public List<Vector> Samples { get; set; }
        public int ClassIndex { get; set; }
        public Vector ClassCenter { get; set; }
        public int Count
        {
            get { return Samples.Count; }
        }

        public double CalculateSigma(Vector anotherClassCenter)
        {
            double result = 0;
            for (int i = 0; i < ClassCenter.Count; i++)
            {
                result += Math.Pow(ClassCenter[i] - anotherClassCenter[i], 2);
            }
            return Math.Sqrt(result);
        }
    }
}
