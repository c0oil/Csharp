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
        private List<DenseVector> sampleImages;

        public Recognizer(IEnumerable<byte[]> images)
        {
            sampleImages = images.Select(x => new DenseVector(Array.ConvertAll(x, c => c == 0 ? -1.0 : 1.0))).ToList();
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
        }


        public byte[] RecognizeAsynchronously(byte[] image)
        {
            var vectImage = new DenseVector(Array.ConvertAll(image, c => c == 0? -1.0: 1.0));

            while (true)
            {
                var matrix = weights * vectImage.ToColumnMatrix();
                var newImage = Activation(matrix.Column(0));
                if (sampleImages.Any(vectImage.Equals))
                {
                    break;
                }
                for (int i = 0; i < vectImage.Count; i++)
                {
                    if (!vectImage[i].Equals(newImage[i]))
                    {
                        vectImage[i] = newImage[i];
                        break;
                    }
                }
            }

            return vectImage.Select(x => (byte)(x > 0? 255: 0) ).ToArray();
        }

        private Vector<double> Activation(Vector<double> input)
        {
            input.SetValues(input.Select(x => x > 0 ? 1.0 : -1.0).ToArray());
            return input;
        }
    }

}
