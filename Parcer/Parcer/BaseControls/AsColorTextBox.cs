﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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
        
        private void UpdateColorSource(ColorSource colorSource)
        {
            var document = GetDefaultDocument();

            TextRange range = new TextRange(document.ContentStart, document.ContentEnd);

            if (colorSource.HighlightWords == null)
            {
                range.ClearAllProperties();
                range.Text = colorSource.Text;
                Document = document;
                return;
            }

            range.Text = string.Empty;
            Action <int, int> tryAppendText = (start, length) =>
            {
                if (length > 0)
                {
                    AppendText(colorSource.Text.Substring(start, length), document);
                }
            };

            int currPosition = 0;
            foreach (ColorWord info in colorSource.HighlightWords)
            {
                tryAppendText(currPosition, info.Start - currPosition);
                AppendText(colorSource.Text.Substring(info.Start, info.End - info.Start), info.Color, document);
                currPosition = info.End;
            }
            tryAppendText(currPosition, colorSource.Text.Length - currPosition);

            Document = document;
        }
        
        private void AppendText(string textData, FlowDocument document)
        {
            TextRange textrange = new TextRange(document.ContentEnd, document.ContentEnd);
            textrange.Text = textData;
            textrange.ClearAllProperties();
        }

        private void AppendText(string textData, Color color, FlowDocument document)
        {
            TextRange textrange = new TextRange(document.ContentEnd, document.ContentEnd);
            textrange.Text = textData;
            textrange.ClearAllProperties();
            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            textrange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
        }

        private FlowDocument GetDefaultDocument()
        {
            return new FlowDocument();
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
    }

    public class ColorSource
    {
        public IEnumerable<ColorWord> HighlightWords { get; set; }
        public string Text { get; set; }
    }

    public struct ColorWord
    {
        public int Start { get; set; }
        public int End { get; set; }
        public Color Color { get; set; }
    }
}
