using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private const string SourceName = "s{0}.bmp";
        private const string SourceFolderPath = @"D:\repository\C#\cosii8\";

        public DigitalSignalProcessor Dsp { get; set; }
        public RelayCommand RecognizeCommand { get; private set; }
        public RelayCommand TeachCommand { get; private set; }
        public RelayCommand NoiseCommand { get; private set; }
        public RelayCommand SelectSampleCommand { get; private set; }

        #region Bindble properties
        public double Sample1Suiteble { get; set; }
        public double Sample2Suiteble { get; set; }
        public double Sample3Suiteble { get; set; }
        public double LevelNoise { get; set; }

        public Dictionary<int, BitmapSource> BitmapSamples { get; set; }

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
            RecognizeCommand = new RelayCommand(OnRecognize) { IsEnabled = true };
            TeachCommand = new RelayCommand(OnTeach) { IsEnabled = true };
            NoiseCommand = new RelayCommand(OnNoise) { IsEnabled = true };
            SelectSampleCommand = new RelayCommand(OnSelectSample) { IsEnabled = true };
            Dsp = new DigitalSignalProcessor();

            BitmapSamples = new Dictionary<int, BitmapSource>();
            for (int i = 1; i < 10; i++)
            {
                var sample = new BitmapImage(new Uri(SourceFolderPath + string.Format(SourceName, i)));
                BitmapSamples.Add(i, Dsp.ToBinar(sample));
            }
            SelectedImage = BitmapSamples.FirstOrDefault().Value;
        }

        #region Command handlers

        private void OnSelectSample(object parametr)
        {
            SelectedImage = BitmapSamples[Convert.ToInt32(parametr) + 1];
        }

        private void OnNoise(object parametr)
        {
            var image = SelectedImage;
            NoisedImage = Dsp.Noize(image, LevelNoise);
        }

        private void OnTeach(object parametr)
        {
            
            Dsp.Teach(BitmapSamples.Select(x => x.Value).ToList());
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
            RecognizedImage = BitmapSamples[result.MaximumIndex() * 3 + 1];
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
