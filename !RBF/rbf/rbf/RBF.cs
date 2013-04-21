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
    public sealed class RBF
    {
        private int distributionLayerSize;
        private int hiddenLayerSize;
        private int outputLayerSize;
        private List<Vector> t = new List<Vector>(); // class centers
        private double[,] w;
        private Vector sigma; // n: outputLayerSize

        public int Iterations { get; set; }

        public RBF(int pixelsCount, int classAmount)
        {
            distributionLayerSize = pixelsCount;
            hiddenLayerSize = classAmount;
            outputLayerSize = classAmount;

            w = new double[hiddenLayerSize, outputLayerSize];

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

        /// <summary>
        /// Corressponding pixel values in every ideal image
        /// of given class are summed and divided by total number of classes.
        /// Result is being rounded to 1 or 0.
        /// </summary>
        /// <param name="allSelections"></param>
        private void CalculateClassCenters(List<Selection> allSelections)
        {
            int pixelsCount = distributionLayerSize;
            foreach (Selection selection in allSelections)
            {
                Vector center = new Vector(n: pixelsCount);
                /*for (int k = 0; k < outputLayerSize; k++)
                {
                    center.Add(0.0);
                }*/

                for (int j = 0; j < selection.Count; j++)
                {
                    for (int k = 0; k < pixelsCount; k++)
                    {
                        center[k] += selection.GetSample(j)[k];
                    }
                }

                for (int j = 0; j < pixelsCount; j++)
                {
                    center[j] = (center[j] / selection.Count) > 0.5 ? 1 : 0;
                }

                //add center to selection
                selection.ClassCenter = center;
                t.Add(center.Clone());
            }
        }

        public void CalculateSigmas(List<Selection> allSelections)
        {
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

        public void Teach(List<Selection> allSelections)
        {
            int classAmount = allSelections.Count;
            sigma = new Vector(outputLayerSize);
            int samplesPerClass = allSelections[0].Count;
            int pixelsCount = distributionLayerSize;

            CalculateClassCenters(allSelections);
            CalculateSigmas(allSelections);

            // Step 2. For each pair xr yr
            List<Vector> allDks = new List<Vector>(); // deviations
            bool timeToLeave = false;
            Iterations = 0;
            while (!timeToLeave)
            {
                Iterations++;
                for (int sampleIndex = 0; sampleIndex < samplesPerClass; sampleIndex++)
                {
                    for (int classIndex = 0; classIndex < classAmount; classIndex++)
                    {
                        Vector g = new Vector(n: allSelections.Count);
                        Vector y = new Vector(n: allSelections.Count);
                        Vector d = new Vector(n: outputLayerSize);

                        Vector currentSample = allSelections[classIndex].GetSample(sampleIndex);
                        for (int j = 0; j < hiddenLayerSize; j++)
                        {
                            g[j] = 0.0;
                            double result = 0.0;
                            for (int i = 0; i < currentSample.Count(); i++)
                            {
                                result += Math.Pow(currentSample[i] - t[j][i], 2);
                            }
                            g[j] = Math.Exp(-result / Math.Pow(sigma[j], 2));
                        }

                        // calculate network's output
                        for (int k = 0; k < outputLayerSize; k++)
                        {
                            double result = 0;
                            for (int j = 0; j < hiddenLayerSize; j++)
                            {
                                result += w[j, k] * g[j];
                            }
                            y[k] = result;
                            y[k] = 1.0 / (1.0 + Math.Exp(-y[k]));
                        }

                        //calculate deviation
                        Vector idealY = new Vector(outputLayerSize);
                        idealY[allSelections[classIndex].ClassIndex] = 1.0;

                        for (int k = 0; k < outputLayerSize; k++)
                        {
                            d[k] = idealY[k] - y[k];
                        }
                        allDks.Add(d.Clone());
                        double alpha = 0.01;

                        // configure network's knowledge
                        for (int j = 0; j < hiddenLayerSize; j++)
                        {
                            for (int k = 0; k < outputLayerSize; k++)
                            {
                                w[j, k] = w[j, k] + alpha * d[k] * g[j];
                            }
                        }
                    }
                }

                // get max dk
                double maxDk = allDks.Max(v => v.Max());
                if (maxDk < 0.1)
                {
                    timeToLeave = true;
                }

                allDks.Clear();
            } // end while(!timeToLeave)
        }

        public Vector Recognize(Bitmap image)
        {
            Vector input = new Vector(new double[distributionLayerSize]);

            // image to "0/1" form
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    input[i * image.Height + j] = image.GetPixel(i, j).R > 0 ? 1 : 0;
                }
            }

            Vector g = new Vector(new double[hiddenLayerSize]);
            Vector y = new Vector(new double[outputLayerSize]);

            // Calculate g[j] and y[k]
            for (int j = 0; j < hiddenLayerSize; j++)
            {
                g[j] = 0.0;
                double result = 0.0;
                for (int i = 0; i < input.Count(); i++)
                {
                    result += Math.Pow(input[i] - t[j][i], 2);
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
    }
}
