using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace cosii5
{
    public class Recognizer
    {
        public int Ticks { get; set; }
        public Perceptron Perceptron { get; set; }

        public void Teach(IEnumerable<byte[]> images)
        {
            var selections = new List<Selection>();
            for (int i = 0; i < 3; i++)
            {
                selections.Add(new Selection
                                    { 
                                        Samples = images.Skip(i * 3).Take(3).Select(ConvertToImage).ToList(), 
                                        ClassIndex = i 
                                    });
            }
            Perceptron = new Perceptron(selections[0].Samples[0].Count, selections.Count);
            Ticks = Perceptron.Teach(selections);
        }

        public Vector Recognize(byte[] image)
        {
            return Perceptron.Recognize(ConvertToImage(image));
        }

        private Vector ConvertToImage(byte[] image)
        {
            var result = new double[image.Length/3];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = image[i * 3] > 0 ? 1.0 : 0;
            }
            return new DenseVector(result);
        }

        private byte[] ConvertFromImage(Vector<double> image)
        {
            var result = new List<byte>();
            for (int i = 0; i < image.Count; i++)
            {
                byte color = (byte)(image[i] > 0 ? 255 : 0);
                for (int j = 0; j < 3; j++)
                {
                    result.Add(color);
                }
            }
            return result.ToArray();
        }
    }

}
