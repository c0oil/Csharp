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

        

        public BitmapSource DetectImages(BitmapSource input, int clusters)
        {
            byte[] inputPixels = GetBytes(input);

            return GetBitmap(inputPixels);
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
