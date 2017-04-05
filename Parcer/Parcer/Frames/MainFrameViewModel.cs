using System;
using System.Windows;
using System.Windows.Input;
using Parcer.BaseControls;
using Parcer.BusinesLogic;
using Parcer.Const;
using Parcer.ViewModel;

namespace Parcer.Frames
{
    public class MainFrameViewModel : ViewModelBase
    {
        private const string SampleFindSetting = MainSettings.SampleFindSetting;
        private const string SampleReplaceSeparator = MainSettings.SampleReplaceSeparator;
        private const string SampleReplaceSetting = MainSettings.SampleReplaceSetting;
        private const string SampleBuildSetting = MainSettings.SampleBuildSetting;
        private const string SampleInText = MainSettings.SampleInText;
        private const string SampleCode = MainSettings.SampleCode;

        private readonly Parser parser = new Parser();
        
        private ColorSource inText;
        public ColorSource InText
        {
            get { return inText; }
            set
            {
                inText = value;
                OnPropertyChanged(nameof(InText));
            }
        }

        private string additionalText;
        public string AdditionalText
        {
            get { return additionalText; }
            set
            {
                additionalText = value;
                OnPropertyChanged(nameof(AdditionalText));
            }
        }

        private string outText;
        public string OutText
        {
            get { return outText; }
            set
            {
                outText = value;
                OnPropertyChanged(nameof(OutText));
            }
        }

        private string replaceSeparator;
        public string ReplaceSeparator
        {
            get { return replaceSeparator; }
            set
            {
                replaceSeparator = value;
                OnPropertyChanged(nameof(ReplaceSeparator));
            }
        }

        private string replaceSetting;
        public string ReplaceSetting
        {
            get { return replaceSetting; }
            set
            {
                replaceSetting = value;
                OnPropertyChanged(nameof(ReplaceSetting));
            }
        }

        private string buildSetting;
        public string BuildSetting
        {
            get { return buildSetting; }
            set
            {
                buildSetting = value;
                OnPropertyChanged(nameof(BuildSetting));
            }
        }

        private string findSetting;
        public string FindSetting
        {
            get { return findSetting; }
            set
            {
                findSetting = value;
                OnPropertyChanged(nameof(FindSetting));
            }
        }

        private string codeString;
        public string CodeString
        {
            get { return codeString; }
            set
            {
                codeString = value;
                OnPropertyChanged(nameof(CodeString));
            }
        }

        public string DescriptionRules { get; } = MainSettings.DescriptionRules;

        private ICommand replaceCommand;
        public ICommand ReplaceCommand => GetDelegateCommand<object>(ref replaceCommand, OnReplace);

        private ICommand buildCommand;
        public ICommand BuildCommand => GetDelegateCommand<object>(ref buildCommand, OnBuild);
        
        private ICommand findCommand;
        public ICommand FindCommand => GetDelegateCommand<object>(ref findCommand, OnFind);

        private void OnReplace(object obj)
        {
            TryExecute(() =>
            {
                OutText = parser.ReplaceTree(InText.Text, FindSetting, ReplaceSetting, ReplaceSeparator);
                InText = GetUncolorText(InText.Text);
            });
        }

        private void OnBuild(object obj)
        {
            TryExecute(() =>
            {
                //OutText = parser.Replace(InText.Text, AdditionalText, FindSetting, ReplaceSetting, CodeString);
                OutText = parser.Build(InText.Text, FindSetting, BuildSetting, CodeString);
                InText = GetUncolorText(InText.Text);
            });
        }

        private void OnFind(object obj)
        {
            TryExecute(() =>
            {
                InText = new ColorSource { Text = InText.Text, HighlightWords = parser.Highlight(InText.Text, FindSetting) };
            });
        }

        private void TryExecute(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public MainFrameViewModel()
        {
            FindSetting = SampleFindSetting;
            ReplaceSetting = SampleReplaceSetting;
            ReplaceSeparator = SampleReplaceSeparator;
            BuildSetting = SampleBuildSetting;
            CodeString = SampleCode;
            InText = GetUncolorText(SampleInText);
        }

        private ColorSource GetUncolorText(string text)
        {
            return new ColorSource { Text = text };
        }
    }
}
