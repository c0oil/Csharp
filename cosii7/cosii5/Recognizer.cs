using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace cosii5
{
    public class Recognizer
    {
        private Matrix<double> weights;
        private List<Vector<double>> sampleImages;
        public int Ticks { get; set; }

        public Recognizer(IEnumerable<byte[]> images)
        {
            sampleImages = images.Select(ConvertToImage).ToList();

            foreach (var image in sampleImages)
            {
                if (weights == null)
                {
                    weights = image.ToColumnMatrix()*image.ToRowMatrix();
                }
                else
                {
                    weights += image.ToColumnMatrix()*image.ToRowMatrix();
                }
            }

            for (int i = 0; i < weights.ColumnCount; i++)
            {
                weights[i, i] = 0;
            }
        }


        public byte[] RecognizeAsynchronously(byte[] image)
        {
            var vectImage = ConvertToImage(image);

            Ticks = 0;
            while (true)
            {
                Ticks++;
                var matrix = weights * vectImage.ToColumnMatrix();
                var newImage = Activation(matrix.Column(0));
                if (sampleImages.Any(vectImage.Equals))
                {
                    break;
                }

                int i = 0;
                while (i < vectImage.Count)
                {
                    if (!vectImage[i].Equals(newImage[i]))
                    {
                        vectImage[i] = newImage[i];
                        break;
                    }
                    i++;
                }
                if (i == vectImage.Count)
                {
                    break;
                }
            }

            return ConvertFromImage(vectImage);
        }

        private Vector<double> ConvertToImage(byte[] image)
        {
            var result = new double[image.Length/3];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = image[i * 3] > 0 ? 1.0 : -1.0;
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

        private Vector<double> Activation(Vector<double> input)
        {
            input.SetValues(input.Select(x => x >= 0 ? 1.0 : -1.0).ToArray());
            return input;
        }
    }

}
