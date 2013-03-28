using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace cosii5
{
    public class Recognizer
    {
        private Matrix<int> weights;
        private List<Vector<int>> sampleImages;

        public Recognizer(List<Vector<int>> images)
        {
            sampleImages = images;
            foreach (var image in images)
            {
                weights += image.ToRowMatrix() * image.ToColumnMatrix();
            }
        }


        public Vector<int> RecognizeAsynchronously(Vector<int> image)
        {
            while (true)
            {
                var matrix = weights * image.ToColumnMatrix();
                var newImage = Activation(matrix.Column(0));
                if (sampleImages.Any(image.Equals))
                {
                    break;
                }
                for (int i = 0; i < image.Count; i++)
                {
                    if (image[i] != newImage[i])
                    {
                        image[i] = newImage[i];
                        break;
                    }
                }
            }

            return image;
        }

        private Vector<int> Activation(Vector<int> input)
        {
            input.SetValues(input.Select(x => x > 0 ? 1 : -1).ToArray());
            return input;
        }
    }

}
