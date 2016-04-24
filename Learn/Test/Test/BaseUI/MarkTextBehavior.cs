using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Test.BaseUI
{
    /*
    Mask Character  Accepts  Required?  
    0  Digit (0-9)  Required  
    9  Digit (0-9) or space  Optional  
    #  Digit (0-9) or space  Required  
    L  Letter (a-z, A-Z)  Required  
    ?  Letter (a-z, A-Z)  Optional  
    &  Any character  Required  
    C  Any character  Optional  
    A  Alphanumeric (0-9, a-z, A-Z)  Required  
    a  Alphanumeric (0-9, a-z, A-Z)  Optional  
       Space separator  Required 
    .  Decimal separator  Required  
    ,  Group (thousands) separator  Required  
    :  Time separator  Required  
    /  Date separator  Required  
    $  Currency symbol  Required  

    In addition, the following characters have special meaning:

    Mask Character  Meaning  
    <  All subsequent characters are converted to lower case  
    >  All subsequent characters are converted to upper case  
    |  Terminates a previous < or >  
    \  Escape: treat the next character in the mask as literal text rather than a mask symbol  

    */

    public class TextBoxInputMaskBehavior : Behavior<TextBox>
    {
        public const char DefaultPromptChar = '_';

        #region DependencyProperties

        public static readonly DependencyProperty InputMaskProperty =
          DependencyProperty.Register("InputMask", typeof(string), typeof(TextBoxInputMaskBehavior), null);

        public string InputMask
        {
            get { return (string)GetValue(InputMaskProperty); }
            set { SetValue(InputMaskProperty, value); }
        }

        public static readonly DependencyProperty PromptCharProperty =
           DependencyProperty.Register("PromptChar", typeof(char), typeof(TextBoxInputMaskBehavior),
                                        new PropertyMetadata(DefaultPromptChar));

        public char PromptChar
        {
            get { return (char)GetValue(PromptCharProperty); }
            set { SetValue(PromptCharProperty, value); }
        }

        #endregion

        public MaskedTextProvider Provider { get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;

            DataObject.AddPastingHandler(AssociatedObject, Pasting);

            if (AssociatedObject.IsLoaded)
            {
                AssociatedObjectLoaded(null, null);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;

            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            Provider = new MaskedTextProvider(InputMask, CultureInfo.CurrentCulture);
            Provider.Set(AssociatedObject.Text);
            Provider.PromptChar = PromptChar;
            AssociatedObject.Text = Provider.ToDisplayString();
            
            var textProp = DependencyPropertyDescriptor.FromProperty(TextBox.TextProperty, typeof(TextBox));
            if (textProp != null)
            {
                textProp.AddValueChanged(AssociatedObject, (s, args) => UpdateText());
            }
        }

        void AssociatedObjectPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TreatSelectedText();
            var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);

            if (Keyboard.IsKeyToggled(Key.Insert))
            {
                if (Provider.Replace(e.Text, position))
                    position++;
            }
            else
            {
                if (Provider.InsertAt(e.Text, position))
                    position++;
            }

            position = GetNextCharacterPosition(position);
            RefreshText(position);

            e.Handled = true;
        }

        void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    TreatSelectedText();
                    var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);
                    if (Provider.InsertAt(" ", position))
                    {
                        RefreshText(position);
                    }

                    e.Handled = true;
                    break;

                case Key.Back:
                    if (TreatSelectedText())
                    {
                        RefreshText(AssociatedObject.SelectionStart);
                    }
                    else if (AssociatedObject.SelectionStart != 0 && Provider.RemoveAt(AssociatedObject.SelectionStart - 1))
                    {
                        RefreshText(AssociatedObject.SelectionStart - 1);
                    }

                    e.Handled = true;
                    break;

                case Key.Delete:
                    if (TreatSelectedText())
                    {
                        RefreshText(AssociatedObject.SelectionStart);
                    }
                    else if (Provider.RemoveAt(AssociatedObject.SelectionStart))
                    {
                        RefreshText(AssociatedObject.SelectionStart);
                    }

                    e.Handled = true;
                    break;
            }
        }
        
        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                TreatSelectedText();
                int position = GetNextCharacterPosition(AssociatedObject.SelectionStart);
                if (Provider.InsertAt(pastedText, position))
                {
                    RefreshText(position);
                }
            }

            e.CancelCommand();
        }

        private void UpdateText()
        {
            if (Provider.ToDisplayString().Equals(AssociatedObject.Text))
            {
                return;
            }

            var success = Provider.Set(AssociatedObject.Text);
            SetText(success ? Provider.ToDisplayString() : AssociatedObject.Text);
        }
        
        private bool TreatSelectedText()
        {
            return AssociatedObject.SelectionLength > 0 && 
                Provider.RemoveAt(AssociatedObject.SelectionStart, AssociatedObject.SelectionStart + AssociatedObject.SelectionLength - 1);
        }

        private void RefreshText(int position)
        {
            SetText(Provider.ToDisplayString());
            AssociatedObject.SelectionStart = position;
        }

        private void SetText(string text)
        {
            AssociatedObject.Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        private int GetNextCharacterPosition(int startPosition)
        {
            var position = Provider.FindEditPositionFrom(startPosition, true);
            return position == -1 ? startPosition : position;
        }
    }

}
