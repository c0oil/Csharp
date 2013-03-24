using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.Drawing;

namespace Hopfield
{
    public sealed class Perceptron
    {
        private int distributionLayerSize;
        private int hiddenLayerSize;
        private int outputLayerSize;

        private readonly Matrix v;
        private readonly Matrix w;

        private readonly Vector Q;
        private readonly Vector T;

        public Perceptron(int pixelsCount, int sampleAmmount)
        {
            this.distributionLayerSize = pixelsCount;
            this.hiddenLayerSize = pixelsCount / 2;
            this.outputLayerSize = sampleAmmount;

            v = new Matrix(m: distributionLayerSize, n: hiddenLayerSize);
            w = new Matrix(m: hiddenLayerSize, n: outputLayerSize);

            Q = new Vector(hiddenLayerSize);
            T = new Vector(outputLayerSize);

            FillEverythingWithRandomValues();
        }

        private void FillEverythingWithRandomValues()
        {
            for (int r = 0; r < v.RowCount; ++r)
            {
                for (int c = 0; c < v.ColumnCount; ++c)
                {
                    v[r, c] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
                }
            }

            for (int r = 0; r < w.RowCount; ++r)
            {
                for (int c = 0; c < w.ColumnCount; ++c)
                {
                    w[r, c] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
                }
            }

            for (int i = 0; i < Q.Length; ++i)
            {
                Q[i] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
            }

            for (int i = 0; i < T.Length; ++i)
            {
                T[i] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
            }
        }

        public void Teach(Bitmap[] samples, ref int iterations)
        {
            Vector input = new Vector(distributionLayerSize);
            Vector idealY = new Vector(outputLayerSize);
            Vector d = new Vector(outputLayerSize);

            var allInputs = new List<Vector>();
            var allIdealYs = new List<Vector>();
            var allds = new List<Vector>();

            for (int i = 0; i < samples.Length; ++i)
            {
                Bitmap sample = samples[i];
                input = sample.ToMatrix().ToVectorByRows();
                allInputs.Add(input.Clone());
                idealY = new Vector(outputLayerSize);
                idealY[i] = 1.0;
                allIdealYs.Add(idealY);
                allds.Add(d.Clone());
            }

            double alpha = 0.1;
            double beta = 0.1;
            double accuracy = 0.05;

            Vector g = new Vector(hiddenLayerSize);
            Vector y = new Vector(outputLayerSize);

            bool timeToLeave = false;
            iterations = 0;
            while (!timeToLeave)
            {
                // Step 2.
                for (int index = 0; index < samples.Length; ++index)
                {
                    // Calculating g[j] and y[k].
                    input = allInputs[index];
                    idealY = allIdealYs[index];

                    for (int j = 0; j < hiddenLayerSize; ++j)
                    {
                        g[j] = 0.0;
                        for (int i = 0; i < distributionLayerSize; ++i)
                        {
                            g[j] += v[i, j] * input[i];
                        }
                        g[j] += Q[j];
                        g[j] = 1.0 / (1.0 + Math.Exp(-g[j]));
                    }

                    for (int k = 0; k < outputLayerSize; ++k)
                    {
                        y[k] = 0.0;
                        for (int j = 0; j < hiddenLayerSize; ++j)
                        {
                            y[k] += w[j, k] * g[j];
                        }
                        y[k] += T[k];
                        y[k] = 1.0 / (1.0 + Math.Exp(-y[k]));
                    }

                    // Correcting network's knowledge.
                    for (int k = 0; k < outputLayerSize; ++k)
                    {
                        d[k] = idealY[k] - y[k];
                    }

                    for (int j = 0; j < hiddenLayerSize; ++j)
                    {
                        for (int k = 0; k < outputLayerSize; ++k)
                        {
                            w[j, k] = w[j, k] + alpha * y[k] * (1.0 - y[k]) * d[k] * g[j];
                        }
                    }

                    for (int k = 0; k < outputLayerSize; ++k)
                    {
                        T[k] = T[k] + alpha * y[k] * (1.0 - y[k]) * d[k];
                    }

                    for (int i = 0; i < distributionLayerSize; ++i)
                    {
                        for (int j = 0; j < hiddenLayerSize; ++j)
                        {
                            double ej = 0;
                            for (int k = 0; k < outputLayerSize; ++k)
                            {
                                ej += d[k] * y[k] * (1.0 - y[k]) * w[j, k];
                            }
                            v[i, j] = v[i, j] + beta * g[j] * (1.0 - g[j]) * ej * input[i];
                        }
                    }

                    for (int j = 0; j < hiddenLayerSize; ++j)
                    {
                        double ej = 0;
                        for (int k = 0; k < outputLayerSize; ++k)
                        {
                            ej += d[k] * y[k] * (1.0 - y[k]) * w[j, k];
                        }
                        Q[j] = Q[j] + beta * g[j] * (1.0 - g[j]) * ej;
                    }

                    allds[index] = d;
                }

                // Checking exit condition.
                double max = 0.0; // d[0] 
                for (int i = 0; i < samples.Length; ++i)
                {
                    for (int j = 0; j < outputLayerSize; ++j)
                    {
                        if (max < allds[i][j])
                        {
                            max = allds[i][j];
                        }
                    }
                }
                if (max < accuracy)
                {
                    timeToLeave = true;
                }
                iterations += 1;
                //System.Diagnostics.Debug.WriteLine("Max: {0}, D: {1}", max, accuracy);
            }
        }

        public Vector Recognize(Vector input)
        {
            Vector g = new Vector(hiddenLayerSize);
            Vector y = new Vector(outputLayerSize);

            // Calculating g[j] and y[k]
            for (int j = 0; j < hiddenLayerSize; ++j)
            {
                g[j] = 0.0;
                for (int i = 0; i < distributionLayerSize; ++i)
                {
                    g[j] += v[i, j] * input[i];
                }
                g[j] += Q[j];
                g[j] = 1.0 / (1.0 + Math.Exp(-g[j]));
            }

            for (int k = 0; k < outputLayerSize; ++k)
            {
                y[k] = 0.0;
                for (int j = 0; j < hiddenLayerSize; ++j)
                {
                    y[k] += w[j, k] * g[j];
                }
                y[k] += T[k];
                y[k] = 1.0 / (1.0 + Math.Exp(-y[k]));
            }

            Vector result = new Vector(outputLayerSize);
            for (int i = 0; i < outputLayerSize; ++i)
            {
                result[i] = y[i];
            }

            return result;
        }
    }
}
