using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace cosii5
{
    public class DigitalSignalProcessor
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private const int BytePerPixel = 3;
        private WriteableBitmap writeableBitmap;

        public BitmapSource Grayscale(BitmapSource input, int gmin, int gmax)
        {
            byte[] inputPixels = GetBytes(TransformBgr24(input));
            var outputPixels = new byte[inputPixels.Length];

            //inputPixels = GammaCorrection(inputPixels, width, height, gmin, gmax);  //GammaCorrection!!!!

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    byte r = inputPixels[BytePerPixel * (Width * y + x) + 2],
                         g = inputPixels[BytePerPixel * (Width * y + x) + 1],
                         b = inputPixels[BytePerPixel * (Width * y + x) + 0];


                    byte s = (byte)(0.3 * r + 0.59 * g + 0.11 * b);

                    outputPixels[BytePerPixel * (Width * y + x) + 2] = s;
                    outputPixels[BytePerPixel * (Width * y + x) + 1] = s;
                    outputPixels[BytePerPixel * (Width * y + x) + 0] = s;
                }
            }

            return GetBitmap(outputPixels);
        }

        public BitmapSource Binarization(BitmapSource input, int max)
        {
            byte[] inputPixels = GetBytes(input);
            var outputPixels = new byte[inputPixels.Length];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    byte r = inputPixels[BytePerPixel * (Width * y + x)];
                    byte s = (byte)(r < max ? 0 : 255);

                    outputPixels[BytePerPixel * (Width * y + x) + 2] = s;
                    outputPixels[BytePerPixel * (Width * y + x) + 1] = s;
                    outputPixels[BytePerPixel * (Width * y + x) + 0] = s;
                }
            }

            return GetBitmap(outputPixels);
        }

        /*private byte[] GammaCorrection(byte[] inputPixels, int width, int height, int gmin, int gmax)
        {
            var outputPixels = new byte[inputPixels.Length];

            int fmax = 0, fmin = 0;
            FindF(inputPixels, ref fmin, ref fmax);

            int fr = fmax - fmin, gr = gmax - gmin;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = inputPixels[BytePerPixel * (width * y + x) + 2],
                         g = inputPixels[BytePerPixel * (width * y + x) + 1],
                         b = inputPixels[BytePerPixel * (width * y + x) + 0];

                    outputPixels[BytePerPixel * (width * y + x) + 2] = (byte)((r - fmin) * gr / fr + gmin);
                    outputPixels[BytePerPixel * (width * y + x) + 1] = (byte)((g - fmin) * gr / fr + gmin);
                    outputPixels[BytePerPixel * (width * y + x) + 0] = (byte)((b - fmin) * gr / fr + gmin);
                }
            }
            return outputPixels;
        }

        private void FindF(byte[] inputBytes, ref int fmin, ref int fmax)
        {
            fmin = 255;
            fmax = 0;
            for (int i = 0; i < inputBytes.Length - 2; i += BytePerPixel)
            {
                byte r = inputBytes[i + 2],
                     g = inputBytes[i + 1],
                     b = inputBytes[i + 0];

                int f = (int)(0.3 * r + 0.59 * g + 0.11 * b);
                if (fmin > f) fmin = f;
                if (fmax < f) fmax = f;
            }
        }*/

        #region Local variable for Labeling
        private int numLabel;
        private byte[] inputFill;
        private int[] labelsFill;
        private Stack<Point> coordinates;
        private Dictionary<int, List<Point>> imagesFill; 
        #endregion
        private int[] Labeling(BitmapSource input)
        {
            numLabel = 1;
            inputFill = GetBytes(input);
            labelsFill = new int[inputFill.Length / BytePerPixel];
            imagesFill = new Dictionary<int, List<Point>>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    FoolFill(x, y); 
                }
            }

            return labelsFill;
        }
        
        private void FoolFill(int x, int y)
        {
            coordinates = new Stack<Point>();
            var points = new List<Point>();

            Fill(x, y, points);
            while (coordinates.Count > 0)
            {
                Point point = coordinates.Pop();
                Fill(point.X, point.Y, points);
            }

            if (points.Count > 0)
            {
                imagesFill.Add(numLabel, points);
                numLabel++;
            }
        }

        private void Fill(int x, int y, List<Point> points)
        {
            if (IsNotFilled(x, y))
            {
                labelsFill[Width*y + x] = numLabel;
                points.Add(new Point(x, y));
            }

            if (IsNotFilled(x, y - 1)) coordinates.Push(new Point(x, y - 1));
            if (IsNotFilled(x + 1, y)) coordinates.Push(new Point(x + 1, y));
            if (IsNotFilled(x, y + 1)) coordinates.Push(new Point(x, y + 1));
            if (IsNotFilled(x - 1, y)) coordinates.Push(new Point(x - 1, y));
        }

        private bool IsNotFilled(int x, int y)
        {
            return !(x < 0 || x > Width - 1 || y < 0 || y > Height - 1) 
                    && (inputFill[BytePerPixel * (Width * y + x)] == 255 && labelsFill[Width * y + x] == 0);
        }

        public BitmapSource DetectImages(BitmapSource input, int clusters)
        {
            byte[] inputPixels = GetBytes(input);

            Labeling(input);
            List<ImageInfo> imagesState = CalculateStats(inputPixels, imagesFill);
            Recognizer.Clusterization(imagesState, clusters);

            return GetBitmap(Colorize(inputPixels, imagesFill, imagesState));
        }

        private byte[] Colorize(byte[] inputPixels, Dictionary<int, List<Point>> images, List<ImageInfo> imagesState)
        {
            var outputPixels = new byte[inputPixels.Length];
            inputPixels.CopyTo(outputPixels, 0);
            byte[] rc = new byte[images.Count],
                   gc = new byte[images.Count],
                   bc = new byte[images.Count];

            var rand = new Random();
            for (int i = 0; i < images.Count; i++)
            {
                rc[i] = (byte)rand.Next(10, 255);
                gc[i] = (byte)rand.Next(10, 255);
                bc[i] = (byte)rand.Next(10, 255);
            }

            for (int i = 0; i < images.Count; i++)
            {
                if (imagesState[i].Area < 500)
                {
                    foreach (var point in images[i + 1])
                    {
                        outputPixels[BytePerPixel * (point.Y * Width + point.X) + 2] = 0;
                        outputPixels[BytePerPixel * (point.Y * Width + point.X) + 1] = 0;
                        outputPixels[BytePerPixel * (point.Y * Width + point.X) + 0] = 0;
                    }
                    continue;
                }
                foreach (var point in images[i + 1])
                {                    
                    outputPixels[BytePerPixel * (point.Y * Width + point.X) + 2] = rc[imagesState[i].Cluster];
                    outputPixels[BytePerPixel * (point.Y * Width + point.X) + 1] = gc[imagesState[i].Cluster];
                    outputPixels[BytePerPixel * (point.Y * Width + point.X) + 0] = bc[imagesState[i].Cluster];
                }
            }

            return outputPixels;
        }

        private List<ImageInfo> CalculateStats(byte[] pixels, Dictionary<int, List<Point>> pointsByLabel)
        {
            Func<double, double> square = x => Math.Pow(x, 2);
            var images = new List<ImageInfo>();

            foreach (var imagePoints in pointsByLabel)
            {
                var info = new ImageInfo {Label = imagePoints.Key};
                List<Point> points = imagePoints.Value;

                foreach (Point p in points)
                {
                    if (p.X == 0 || p.X + 1 == Width || p.Y == 0 || p.Y + 1 == Height)
                    {
                        info.Perimeter += 1;
                    }
                    else if (pixels[BytePerPixel * (p.Y * Width + p.X + 1)] == 0 
                          || pixels[BytePerPixel * (p.Y * Width + p.X - 1)] == 0
                          || pixels[BytePerPixel * ((p.Y + 1) * Width + p.X)] == 0 
                          || pixels[BytePerPixel * ((p.Y - 1) * Width + p.X)] == 0)
                    {
                        info.Perimeter += 1;
                    }
                }
                info.Area = points.Count;
                info.Compactness = square(info.Perimeter) / info.Area;

                info.XAverage = points.Sum(p => p.X) / info.Area;
                info.YAverage = points.Sum(p => p.Y) / info.Area;

                info.M20 = 0;
                info.M11 = 0;
                info.M02 = 0;
                foreach (Point p in points)
                {
                    info.M20 += (p.X - info.XAverage) * (p.X - info.XAverage);
                    info.M11 += (p.X - info.XAverage) * (p.Y - info.YAverage);
                    info.M02 += (p.Y - info.YAverage) * (p.Y - info.YAverage);
                }

                double tSqrt = Math.Sqrt(square(info.M20 - info.M02) + 4 * info.M11 * info.M11);
                info.Elongation = (info.M20 + info.M02 + tSqrt) / (info.M20 + info.M02 - tSqrt);
                info.Orientation = 0.5 * Math.Atan2(2 * info.M11, info.M20 - info.M02);

                images.Add(info);
            }

            return images;
        }

        private static BitmapSource TransformBgr24(BitmapSource input)
        {
            var formatedBitmapSource = new FormatConvertedBitmap();
            formatedBitmapSource.BeginInit();
            formatedBitmapSource.Source = input;
            formatedBitmapSource.DestinationPalette = input.Palette;
            formatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            formatedBitmapSource.EndInit();
            return formatedBitmapSource;
        }

        private byte[] GetBytes(BitmapSource imageSource)
        {
            writeableBitmap = new WriteableBitmap(imageSource);
            var buffer = new byte[imageSource.PixelHeight * imageSource.PixelWidth * BytePerPixel];
            writeableBitmap.CopyPixels(buffer, imageSource.PixelWidth * BytePerPixel, 0);
            return buffer;
        }

        private BitmapSource GetBitmap(byte[] buffer)
        {
            writeableBitmap.WritePixels(new Int32Rect(0, 0, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight), 
                                        buffer, writeableBitmap.PixelWidth * BytePerPixel, 0);
            return writeableBitmap;
        }
    }
}
