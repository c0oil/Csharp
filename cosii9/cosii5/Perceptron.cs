using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using System.Drawing;

namespace cosii5
{
    public sealed class Perceptron
    {
        private const double Alpha = 0.1;
        private const double StepError = 0.1;

        private Vector sigma;
        private List<Vector> centers;

        private int distributionLayerSize;
        private int hiddenLayerSize;
        private int outputLayerSize;

        private DenseMatrix w;

        public Perceptron(int pixelsCount, int sampleAmmount)
        {
            distributionLayerSize = pixelsCount;
            hiddenLayerSize = sampleAmmount;
            outputLayerSize = sampleAmmount;
            
            w = new DenseMatrix(hiddenLayerSize, outputLayerSize);

            FillWeightsWithRandomValues();
        }

        private void FillWeightsWithRandomValues()
        {
            for (int i = 0; i < hiddenLayerSize; i++)
            {
                for (int j = 0; j < outputLayerSize; j++)
                {
                    w[i, j] = RandomGenerator.NextDoubleFromMinusOneToPlusOne();
                }
            }
        }

        public int Teach(List<Selection> samples)
        {
            int iterations = 0;
            int classAmount = samples.Count;
            int samplesPerClass = samples[0].Count;

            CalculateClassCenters(samples);
            CalculateSigmas(samples);

            // Step 2. For each pair xr yr
            var allDks = new List<Vector>(); // deviations
            bool timeToLeave = false;
            while (!timeToLeave)
            {
                for (int classIndex = 0; classIndex < classAmount; classIndex++)
                //for (int sampleIndex = 0; sampleIndex < samplesPerClass; sampleIndex++)
                {
                    for (int sampleIndex = 0; sampleIndex < samplesPerClass; sampleIndex++)
                    //for (int classIndex = 0; classIndex < classAmount; classIndex++)
                    {
                        Vector g = new DenseVector(samples.Count);
                        Vector y = new DenseVector(samples.Count);
                        Vector d;// = new DenseVector(outputLayerSize);

                        Vector currentSample = samples[classIndex].Samples[sampleIndex];
                        for (int j = 0; j < hiddenLayerSize; j++)
                        {
                            g[j] = 0.0;
                            double result = 0.0;
                            for (int i = 0; i < currentSample.Count(); i++)
                            {
                                result += Math.Pow(currentSample[i] - centers[j][i], 2);
                            }
                            g[j] = Math.Exp(-result / Math.Pow(sigma[j], 2));
                        }

                        // calculate network's output
                        y.SetValues((w  * g.ToColumnMatrix()).ToColumnWiseArray());
                        /*for (int k = 0; k < outputLayerSize; k++)
                        {
                            double result = 0;
                            for (int j = 0; j < hiddenLayerSize; j++)
                            {
                                result += w[j, k] * g[j];
                            }
                            y[k] = result;
                            y[k] = 1.0 / (1.0 + Math.Exp(-y[k]));
                        }*/

                        //calculate deviation
                        Vector idealY = new DenseVector(outputLayerSize);
                        idealY[samples[classIndex].ClassIndex] = 1.0;

                        /*for (int k = 0; k < outputLayerSize; k++)
                        {
                            d[k] = idealY[k] - y[k];
                        }*/
                        d = new DenseVector(idealY - y);
                        allDks.Add(d);
                        

                        // configure network's knowledge
                        /*for (int j = 0; j < hiddenLayerSize; j++)
                        {
                            for (int k = 0; k < outputLayerSize; k++)
                            {
                                w[j, k] = w[j, k] + Alpha * d[k] * g[j];
                            }
                        }*/
                        w = w + new DenseMatrix((Alpha * (d.ToRowMatrix() * g.ToColumnMatrix())).ToArray());
                    }
                }

                // get max dk
                double maxDk = allDks.Max(v => v.Max());
                if (maxDk < StepError)
                {
                    timeToLeave = true;
                }
                iterations += 1;
                if(iterations > 10000)
                {
                    timeToLeave = true;
                }
                allDks.Clear();
                
            } // end while(!timeToLeave)
            return iterations;
        }

        public Vector Recognize(Vector<double> input)
        {
            Vector g = new DenseVector(new double[hiddenLayerSize]);
            Vector y = new DenseVector(new double[outputLayerSize]);

            // Calculate g[j] and y[k]
            for (int j = 0; j < hiddenLayerSize; j++)
            {
                g[j] = 0.0;
                double result = 0.0;
                for (int i = 0; i < input.Count(); i++)
                {
                    result += Math.Pow(input[i] - centers[j][i], 2);
                }
                result = result > 0 ? result : result * -1;

                g[j] = Math.Exp(-result / Math.Pow(sigma[j], 2));

            }

            for (int k = 0; k < outputLayerSize; k++)
            {
                y[k] = 0.0;
                for (int j = 0; j < hiddenLayerSize; j++)
                {
                    y[k] += w[j, k] * g[j];
                }

                y[k] = 1.0 / (1.0 + Math.Exp(-y[k]));
            }
            return y;
        }

        private void CalculateClassCenters(List<Selection> allSelections)
        {
            centers = new List<Vector>();
            int pixelsCount = distributionLayerSize;
            foreach (var selection in allSelections)
            {
                Vector center = new DenseVector(pixelsCount);
                for (int j = 0; j < selection.Count; j++)
                {
                    for (int k = 0; k < pixelsCount; k++)
                    {
                        center[k] += selection.Samples[j][k];
                    }
                }

                for (int j = 0; j < pixelsCount; j++)
                {
                    center[j] = (center[j] / selection.Count) > 0.5 ? 1 : 0;
                }

                //add center to selection
                selection.ClassCenter = center;
                centers.Add(new DenseVector(center));
            }
        }

        public void CalculateSigmas(List<Selection> allSelections)
        {
            sigma = new DenseVector(outputLayerSize);
            for (int i = 0; i < allSelections.Count; i++)
            {
                List<double> allSigmas = new List<double>();
                for (int j = 0; j < allSelections.Count; j++)
                {
                    if (i != j)
                    {
                        double sigmaValue = allSelections[i].CalculateSigma(allSelections[j].ClassCenter);
                        allSigmas.Add(sigmaValue);
                    }
                }

                // then get the minimal sigma/2
                double resultSigma = allSigmas.Min() / 2;
                sigma[i] = resultSigma;
            }
        }
    }
}
