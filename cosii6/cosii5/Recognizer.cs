using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace cosii5
{
    class Recognizer
    {
        public static void Clusterization(List<ImageInfo> images, int clusters)
        {
            var propertyRanges = GeneratePropertyMinMaxes(images);

            var clusterMeans = new Centroid[clusters];
            for (int i = 0; i < clusters; i++)
            {
                clusterMeans[i] = GenerateRandomCentroidWithinRange(propertyRanges);
            }

            int iteration = 1;
            while (true)
            {
                var prevClusterMeans = new Centroid[clusters];
                for (int i = 0; i < clusterMeans.Length; ++i)
                {
                    prevClusterMeans[i] = new Centroid();
                    Centroid oldMean = prevClusterMeans[i];
                    clusterMeans[i].PropertyValues.CopyTo(oldMean.PropertyValues, 0);
                }

                foreach (ImageInfo image in images)
                {
                    image.Cluster = FindIndexOfNearestClusterMean(image, clusterMeans);
                }

                for (int i = 0; i < clusterMeans.Length; ++i)
                {
                    double[] propertyMedians = CalculateClusterMean(i, images);
                    if (propertyMedians == null) // has no images
                    {
                        clusterMeans[i] = GenerateRandomCentroidWithinRange(propertyRanges);
                        continue;
                    }
                    clusterMeans[i].PropertyValues = propertyMedians;
                }

                bool pointsAreStill = true;
                for (int i = 0; i < clusters; ++i)
                {
                    double distance = prevClusterMeans[i].DistanceTo(clusterMeans[i]);
                    if (distance > double.Epsilon)
                    {
                        pointsAreStill = false;
                        break;
                    }
                }

                iteration++;
                if (pointsAreStill) break;
            }
        }

        private static double[] CalculateClusterMean(int clusterIndex, IEnumerable<ImageInfo> images)
        {
            ImageInfo[] imagesForCluster = images.Where(im => im.Cluster == clusterIndex).ToArray();
            int len = imagesForCluster.Count();

            if (len == 0) return null; // centroid has no game

            var sortedPropertyValues = new List<double[]>();
            for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
            {
                double[] propertyValues = imagesForCluster.Select(im => im.GetPropertyByIndex(i))
                    .OrderBy(x => x)
                    .ToArray();
                sortedPropertyValues.Add(propertyValues);
            }
            var propertyMedians = new double[ImageInfo.PropertiesCount];
            if (len == 1)
            {
                for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
                {
                    propertyMedians[i] = imagesForCluster[0].GetPropertyByIndex(i);
                }
            }
            else if (len % 2 == 0) // even count
            {
                for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
                {
                    double[] sortedValues = sortedPropertyValues[i];
                    propertyMedians[i] = (sortedValues[(len / 2) - 1] + sortedValues[len / 2]) / 2;
                }
            }
            else if (len % 2 != 0) // odd count
            {
                for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
                {
                    double[] sortedValues = sortedPropertyValues[i];
                    propertyMedians[i] = sortedValues[len / 2];
                }
            }

            return propertyMedians;
        }

        private static int FindIndexOfNearestClusterMean(ImageInfo image, Centroid[] means)
        {
            int nearestMeanIndex = -1;
            double minDistance = double.MaxValue;
            for (int i = 0; i < means.Length; ++i)
            {
                double distanceToMean = means[i].DistanceTo(image);
                if (distanceToMean < minDistance)
                {
                    nearestMeanIndex = i;
                    minDistance = distanceToMean;
                }
            }
            return nearestMeanIndex;
        }

        private static MinMax[] GeneratePropertyMinMaxes(List<ImageInfo> images)
        {
            var minMaxes = new List<MinMax>();
            for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
            {
                double min = images.Select(im => im.GetPropertyByIndex(i)).Min();
                double max = images.Select(im => im.GetPropertyByIndex(i)).Max();
                minMaxes.Add(new MinMax { Min = min, Max = max });
            }

            return minMaxes.ToArray();
        }

        private static readonly Random RandCentroid = new Random(DateTime.Now.Millisecond);
        private static Centroid GenerateRandomCentroidWithinRange(MinMax[] ranges)
        {
            var centroid = new Centroid();
            for (int i = 0; i < ImageInfo.PropertiesCount; ++i)
            {
                MinMax minMax = ranges[i];
                centroid.PropertyValues[i] = RandCentroid.NextDouble() * (minMax.Max - minMax.Min) + minMax.Min;
            }
            return centroid;
        }

        public class MinMax
        {
            public double Max { get; set; }
            public double Min { get; set; }
        }
    }

    public class Centroid
    {
        public double[] PropertyValues { get; set; }
        public int Cluster { get; set; }

        public Centroid()
        {
            PropertyValues = new double[ImageInfo.PropertiesCount];
        }
    }

    public static class CentroidExtensions
    {
        public static double DistanceTo(this Centroid first, Centroid second)
        {
            double distance = 0.0;
            for (int i = 0; i < ImageInfo.PropertiesCount; i++)
            {
                distance += Math.Pow(first.PropertyValues[i] - second.PropertyValues[i], 2);
            }

            distance = Math.Sqrt(distance);
            return distance;
        }

        public static double DistanceTo(this Centroid first, ImageInfo info)
        {
            double distance = 0.0;
            for (int i = 0; i < ImageInfo.PropertiesCount; i++)
            {
                distance += Math.Pow(first.PropertyValues[i] - info.GetPropertyByIndex(i), 2);
            }
            distance = Math.Sqrt(distance);
            return distance;
        }
    }
}
