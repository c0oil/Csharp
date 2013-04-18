using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace cosii5
{
    public class Recognizer
    {
        private List<Vector<double>> sampleImages;
        public int Ticks { get; set; }
        public Perceptron Perceptron { get; set; }

        public void Teach(IEnumerable<byte[]> images)
        {
            sampleImages = images.Select(ConvertToImage).ToList();
            Perceptron = new Perceptron(sampleImages.FirstOrDefault().Count, sampleImages.Count);
            Ticks = Perceptron.Teach(sampleImages);
        }

        public Vector<double> Recognize(byte[] image)
        {
            return Perceptron.Recognize(ConvertToImage(image));
        }

        private Vector<double> ConvertToImage(byte[] image)
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
