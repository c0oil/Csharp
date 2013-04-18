using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;

namespace RBFTry
{
    public sealed class Selection
    {
        private List<Vector> samples = new List<Vector>();
        private Vector classCenter = new Vector(new double[0]);

        public int ClassIndex { get; set; }
        public Vector ClassCenter
        {
            get { return classCenter; }
            set { classCenter = value.Clone(); }
        }
        public int Count
        {
            get { return samples.Count; }
        }

        public void AddSample(Bitmap sample)
        {
            Vector input = new Vector(sample.Height * sample.Width);
            // convert bitmap to vector
            for (int i = 0; i < sample.Height; i++)
            {
                for (int j = 0; j < sample.Width; j++)
                {
                    input[i * sample.Height + j] = sample.GetPixel(i, j).R > 0 ? 1 : 0;
                }
            }
            samples.Add(input.Clone());
        }

        public Vector GetSample(int index)
        {
            return samples[index];
        }

        public double CalculateSigma(Vector anotherClassCenter)
        {
            double result = 0;
            for (int i = 0; i < classCenter.Count(); i++)
            {
                result += Math.Pow(classCenter[i] - anotherClassCenter[i], 2);
            }
            return Math.Sqrt(result);
        }
    }
}
