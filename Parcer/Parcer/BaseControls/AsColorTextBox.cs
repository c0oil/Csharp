using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Parcer.BusinesLogic;
using RichTextBox = System.Windows.Controls.RichTextBox;

namespace Parcer.BaseControls
{
    public class AsColorTextBox : RichTextBox
    {
        public static readonly DependencyProperty ColorSourceProperty =
            DependencyProperty.Register("ColorSource", typeof(ColorSource), typeof(AsColorTextBox),
            new FrameworkPropertyMetadata(null, OnColorSourceChanged));

        private static void OnColorSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            AsColorTextBox target = (AsColorTextBox)d;
            ColorSource val = (ColorSource) args.NewValue;
            target.UpdateColorSource(val ?? new ColorSource());
        }

        public ColorSource ColorSource
        {
            get { return (ColorSource)GetValue(ColorSourceProperty); }
            set { SetValue(ColorSourceProperty, value); }
        }
        
        public AsColorTextBox()
        {
            Style noSpaceStyle = new Style(typeof(Paragraph));
            noSpaceStyle.Setters.Add(new Setter(Block.MarginProperty, new Thickness(0)));
            Resources.Add(typeof(Paragraph), noSpaceStyle);

            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            ColorSource.Text = new TextRange(Document.ContentStart, Document.ContentEnd).Text;
            ColorSource.HighlightWords = null;
        }

        private async void UpdateColorSource(ColorSource colorSource)
        {
            MemoryStream result = await Task.Run(() => ToDocumentStream(FillDocument(colorSource)));
            Document = (FlowDocument)XamlReader.Load(result);
        }

        private MemoryStream ToDocumentStream(object data)
        {
            MemoryStream stream = new MemoryStream();
            XamlWriter.Save(data, stream);
            stream.Position = 0;
            return stream;
        }

        private static FlowDocument FillDocument(ColorSource colorSource)
        {
            FlowDocument document = GetDefaultDocument();
            TextRange range = new TextRange(document.ContentStart, document.ContentEnd);

            document.BeginInit();

            if (colorSource.HighlightWords == null)
            {
                range.ClearAllProperties();
                range.Text = colorSource.Text;
            }
            else
            {
                range.Text = string.Empty;
                ParceLogic.EnumerateMatches(colorSource.HighlightWords,
                    (s, l) => TryAppendText(s, l, null, colorSource.Text, document),
                    (s, l, info) => TryAppendText(s, l, info.Color, colorSource.Text, document),
                    info => info.Start,
                    info => info.End - info.Start,
                    colorSource.Text.Length);
            }

            document.EndInit();
            return document;
        }

        private static void TryAppendText(int start, int length, Color? color, string text, FlowDocument document)
        {
            TextRange textrange = new TextRange(document.ContentEnd, document.ContentEnd) { Text = text.Substring(start, length) };
            if (color != null)
            {
                textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color.Value));
                textrange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                textrange.ClearAllProperties();
            }
        }

        private static FlowDocument GetDefaultDocument()
        {
            return new FlowDocument();
        }
    }

    public class ColorSource
    {
        public IEnumerable<ColorWord> HighlightWords { get; set; }
        public string Text { get; set; }
    }

    public class ColorWord
    {
        public string Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public Color? Color { get; set; }
    }
}
