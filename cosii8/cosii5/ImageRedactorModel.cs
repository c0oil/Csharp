using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Single;
using Microsoft.Win32;
using cosii5.MVVMUtility;

namespace cosii5
{
    public class ImageRedactorModel : ViewModelBase
    {
        private const string SourceName1 = "sample1.bmp";
        private const string SourceName2 = "sample2.bmp";
        private const string SourceName3 = "sample3.bmp";
        private const string SourceFolderPath = @"D:\repository\C#\cosii8\";
        private List<BitmapSource> samples;

        public DigitalSignalProcessor Dsp { get; set; }
        public RelayCommand RecognizeCommand { get; private set; }
        public RelayCommand TeachCommand { get; private set; }
        public RelayCommand NoiseCommand { get; private set; }
        public RelayCommand SelectSampleCommand { get; private set; }

        #region Bindble properties
        public BitmapSource Sample1 { get; set; }
        public BitmapSource Sample2 { get; set; }
        public BitmapSource Sample3 { get; set; }
        public double LevelNoise { get; set; }
        public double Sample1Suiteble { get; set; }
        public double Sample2Suiteble { get; set; }
        public double Sample3Suiteble { get; set; }

        private int ticks;
        public int Ticks
        {
            get { return ticks; }
            set
            {
                ticks = value;
                OnPropertyChanged("Ticks");
            }
        }

        private BitmapSource selectedImage;
        public BitmapSource SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
                NoisedImage = selectedImage;
            }
        }

        private BitmapSource noisedImage;
        public BitmapSource NoisedImage
        {
            get { return noisedImage; }
            set
            {
                noisedImage = value;
                OnPropertyChanged("NoisedImage");
            }
        }

        private BitmapSource recognizedImage;
        public BitmapSource RecognizedImage
        {
            get { return recognizedImage; }
            set
            {
                recognizedImage = value;
                OnPropertyChanged("RecognizedImage");
            }
        }
        #endregion

        public ImageRedactorModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            Sample1 = new BitmapImage(new Uri(SourceFolderPath + SourceName1));
            OnPropertyChanged("Sample1");
            Sample2 = new BitmapImage(new Uri(SourceFolderPath + SourceName2));
            OnPropertyChanged("Sample2");
            Sample3 = new BitmapImage(new Uri(SourceFolderPath + SourceName3));
            OnPropertyChanged("Sample3");

            SelectedImage = Sample1;

            RecognizeCommand = new RelayCommand(OnRecognize) { IsEnabled = true };
            TeachCommand = new RelayCommand(OnTeach) { IsEnabled = true };
            NoiseCommand = new RelayCommand(OnNoise) { IsEnabled = true };
            SelectSampleCommand = new RelayCommand(OnSelectSample) { IsEnabled = true };
            Dsp = new DigitalSignalProcessor();
        }

        #region Command handlers

        private void OnSelectSample(object parametr)
        {
            switch (Convert.ToInt32(parametr))
            {
                case 1:
                    SelectedImage = Sample1;
                    break;
                case 2:
                    SelectedImage = Sample2;
                    break;
                case 3:
                    SelectedImage = Sample3;
                    break;
            }
        }

        private void OnNoise(object parametr)
        {
            var image = SelectedImage;
            NoisedImage = Dsp.Noize(image, LevelNoise);
        }

        private void OnTeach(object parametr)
        {
            samples = new List<BitmapSource> {Sample1, Sample2, Sample3};
            Dsp.Teach(samples);
            Ticks = Dsp.Recognizer.Ticks;
        }

        private void OnRecognize(object parametr)
        {
            Vector<double> result = Dsp.DetectImages(NoisedImage);
            Sample1Suiteble = result[0];
            OnPropertyChanged("Sample1Suiteble");
            Sample2Suiteble = result[1];
            OnPropertyChanged("Sample2Suiteble");
            Sample3Suiteble = result[2];
            OnPropertyChanged("Sample3Suiteble");
            RecognizedImage = samples[result.MaximumIndex()];
            Ticks = Dsp.Recognizer.Ticks;
        }

        public void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var ofd = new OpenFileDialog {InitialDirectory = @"D:\www_goodfon_ru"};
            if (ofd.ShowDialog() == true)
            {
                SelectedImage = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        public void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static void SaveImageToFile(string filePath, BitmapSource image)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        #endregion

    }
}
