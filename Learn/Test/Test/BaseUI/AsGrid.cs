using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Test.BaseUI
{
    public class AsGrid : Grid
    {
        public Collection<Field> Fields { get; set; }

        public AsGrid()
        {
            Fields = new Collection<Field>();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            InitializeSample();
        }

        private void InitializeSample()
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                var textControl = new TextBox { Text = Fields[i].Text, IsReadOnly = true };
                SetRow(textControl, i);
                Children.Add(textControl);
            }
        }
    }

    public class Field
    {
        public string Text { get; set; }
    }
}
