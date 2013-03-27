using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra.Single;

namespace cosii5
{
    class Recognizer
    {
        public static Matrix GenerateWeightsMatrix(Vector[] samples)
        {
            /*int samplesCount = samples.Length;
            int vectorLength = samples[0].Length;
            Matrix weights = new Matrix(vectorLength, vectorLength);
            for (int i = 0; i < weights.RowCount; ++i)
            {
                for (int j = 0; j < weights.ColumnCount; ++j)
                {
                    if (i == j) continue;
                    for (int k = 0; k < samplesCount; ++k)
                    {
                        weights[i, j] += samples[k][i] * samples[k][j];
                    }
                }
            }
            return weights;*/
        }

        public static Matrix RecognizeAsynchronously(Matrix weights, Vector input)
        {
            /*Random rand = new Random(DateTime.Now.Millisecond);
            Vector oldOutput = input.Clone();
            Vector newOutput = input.Clone();
            int iterations = 0;
            int equalTimes = 0;
            while (true)
            {
                Console.WriteLine("iteration: {0}", iterations++);
                Console.WriteLine(oldOutput);
                for (int i = 0; i < input.Length; ++i)
                {
                    newOutput[i] = weights.GetRowVector(i).ArrayMultiply(newOutput).Sum() >= 0 ? +1 : -1;
                }
                Console.WriteLine(newOutput);
                if (newOutput.SequenceEqual(oldOutput)) //equalTimes += 1;
                //else equalTimes -= 1;
                //if (equalTimes >= 5)
                {
                    Console.WriteLine("Old and new are equal");
                    Matrix recognized = newOutput.ToMatrixByColumns(columnSize: (int)Math.Sqrt(input.Length));
                    return recognized;
                }
                oldOutput = newOutput.Clone();
            }*/
        }

        /*public static int RecognizeSynchronously(Matrix weights, Vector input, Vector[] samples)
        {
            Console.WriteLine("Recognize()");
            Console.WriteLine("input:");
            Vector oldOutput = input.Clone();
            int recognizedSampleIndex;
            int iterations = 0;
            while (true)
            {
                iterations += 1;
                Console.WriteLine("iteration: {0}", iterations);
                Vector newOutput = oldOutput.Clone();
                for (int r = 0; r < weights.RowCount; ++r)
                {
                    Vector weightsRow = weights.GetRowVector(r);
                    newOutput[r] = weightsRow.ArrayMultiply(oldOutput).Sum() >= 0 ? +1 : -1;
                }
                if (newOutput.SequenceEqual(oldOutput))
                {
                    for (int j = 0; j < samples.Length; ++j)
                    {
                        if (samples[j].SequenceEqual(newOutput))
                        {
                            recognizedSampleIndex = j;
                            Console.WriteLine("Recognized to be {0}", j);
                            return recognizedSampleIndex;
                        }
                    }
                }
                //Console.WriteLine("output:");
                //Console.WriteLine(newOutput.ToPrettyString());
                oldOutput = newOutput.Clone();
                if (iterations > 100)
                {
                    Console.WriteLine("Coulnd't recognize");
                    return -1;
                }
            }
        }*/
    }

}
