using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using System.Drawing;

namespace cosii5
{
    public sealed class Perceptron
    {
        const double Alpha = 0.1;
        const double Beta = 0.1;
        const double Accuracy = 0.02;

        private int distributionLayerSize;
        private int hiddenLayerSize;
        private int outputLayerSize;
        private int numSamplesForClass;

        private readonly Matrix<double> v;
        private readonly Matrix<double> w;

        private readonly Vector<double> Q;
        private readonly Vector<double> T;

        public Perceptron(int pixelsCount, int sampleAmmount, int numSamples = 3)
        {
            distributionLayerSize = pixelsCount;
            hiddenLayerSize = pixelsCount / 2;
            outputLayerSize = sampleAmmount / numSamples;
            numSamplesForClass = numSamples;

            v = new DenseMatrix(distributionLayerSize, hiddenLayerSize);
            w = new DenseMatrix(hiddenLayerSize, outputLayerSize);

            Q = new DenseVector(hiddenLayerSize);
            T = new DenseVector(outputLayerSize);

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

            for (int i = 0; i < Q.Count; ++i)
            {
                Q[i] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
            }

            for (int i = 0; i < T.Count; ++i)
            {
                T[i] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
            }
        }

        public int Teach(List<Vector<double>> samples)
        {
            Vector<double> input = new DenseVector(distributionLayerSize);
            Vector idealY = new DenseVector(outputLayerSize);
            Vector d = new DenseVector(outputLayerSize);

            var allInputs = samples;
            var allIdealYs = new List<Vector>();
            var allds = new List<Vector>();

            for (int i = 0; i < samples.Count; ++i)
            {
                idealY = new DenseVector(outputLayerSize);
                idealY[i / numSamplesForClass] = 1.0;
                allIdealYs.Add(idealY);
                allds.Add(d);
            }

            Vector g = new DenseVector(hiddenLayerSize);
            Vector y = new DenseVector(outputLayerSize);

            bool timeToLeave = false;
            int iterations = 0;
            while (!timeToLeave)
            {
                // Step 2.
                for (int index = 0; index < samples.Count; ++index)
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
                            w[j, k] = w[j, k] + Alpha * y[k] * (1.0 - y[k]) * d[k] * g[j];
                        }
                    }

                    for (int k = 0; k < outputLayerSize; ++k)
                    {
                        T[k] = T[k] + Alpha * y[k] * (1.0 - y[k]) * d[k];
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
                            v[i, j] = v[i, j] + Beta * g[j] * (1.0 - g[j]) * ej * input[i];
                        }
                    }

                    for (int j = 0; j < hiddenLayerSize; ++j)
                    {
                        double ej = 0;
                        for (int k = 0; k < outputLayerSize; ++k)
                        {
                            ej += d[k] * y[k] * (1.0 - y[k]) * w[j, k];
                        }
                        Q[j] = Q[j] + Beta * g[j] * (1.0 - g[j]) * ej;
                    }

                    allds[index] = d;
                }

                // Checking exit condition.
                double max = 0.0; // d[0] 
                for (int i = 0; i < samples.Count; ++i)
                {
                    for (int j = 0; j < outputLayerSize; ++j)
                    {
                        if (max < allds[i][j])
                        {
                            max = allds[i][j];
                        }
                    }
                }
                if (max < Accuracy)
                {
                    timeToLeave = true;
                }
                iterations += 1;
                //System.Diagnostics.Debug.WriteLine("Max: {0}, D: {1}", max, accuracy);
            }
            return iterations;
        }

        public Vector Recognize(Vector<double> input)
        {
            Vector g = new DenseVector(hiddenLayerSize);
            Vector y = new DenseVector(outputLayerSize);

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

            Vector result = new DenseVector(outputLayerSize);
            for (int i = 0; i < outputLayerSize; ++i)
            {
                result[i] = y[i];
            }

            return result;
        }
    }
}
