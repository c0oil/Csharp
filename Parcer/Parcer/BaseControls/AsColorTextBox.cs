using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

            document.BeginInit();
            range.Text = string.Empty;
            Action <int, int> tryAppendText = (start, length) =>
            {
                if (length > 0)
                {
                    AppendText(colorSource.Text.Substring(start, length), document);
                }
            };

            int currPosition = 0;
            foreach (TreeItem<ColorWord> word in colorSource.HighlightWords)
            {
                ColorWord info = word.Item;
                tryAppendText(currPosition, info.Start - currPosition);
                AppendText(colorSource.Text.Substring(info.Start, info.End - info.Start), info.Color, document);
                //AppendText(word, colorSource.Text.Substring(info.Start, info.End - info.Start), info.Color, document);
                currPosition = info.End;
            }
            tryAppendText(currPosition, colorSource.Text.Length - currPosition);
            document.EndInit();

            Document = document;
        }
        
        private void AppendText(string textData, FlowDocument document)
        {
            TextRange textrange = new TextRange(document.ContentEnd, document.ContentEnd);
            textrange.Text = textData;
            textrange.ClearAllProperties();
        }

        /*private void AppendText(TreeItem<ColorWord> word, string textData, Color color, FlowDocument document)
        {
            TextRange textrange = new TextRange(document.ContentEnd, document.ContentEnd);
            textrange.Text = textData;
            textrange.ClearAllProperties();
            foreach (TreeItem<ColorWord> item in word.Childrens)
            {
                ColorText(textrange.Start, textrange.End, item, word);
            }
        }

        private void ColorText(TextPointer startPointer, TextPointer endPointer, TreeItem<ColorWord> word, TreeItem<ColorWord> parent)
        {
            foreach (ColorWord colorWord in word.Reverse())
            {
                int startOffset = colorWord.Start - parent.Item.Start;
                int endOffset = colorWord.End - parent.Item.End;
                var s = startPointer.GetPositionAtOffset(startOffset, LogicalDirection.Forward);
                var e = endPointer.GetPositionAtOffset(endOffset, LogicalDirection.Forward);


                TextRange textrange = new TextRange(s, e);
                textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(colorWord.Color));
                textrange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
        }*/

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
        public IEnumerable<TreeItem<ColorWord>> HighlightWords { get; set; }
        //public IEnumerable<ColorWord> HighlightWords { get; set; }
        public string Text { get; set; }
    }

    public struct ColorWord
    {
        public string Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public Color Color { get; set; }
    }
}
