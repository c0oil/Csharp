using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Parcer.BaseControls;
using Parcer.ViewModel;

namespace Parcer
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string descriptionRules =
@"'{' и '}' пишутся как'\{' и '\}' в поле Replace Setting

\d 	[0-9] 	Цифровой символ
\D 	[^0-9] 	Нецифровой символ
\s 	[ \f\n\r\t\v] 	Пробельный символ
\S 	[^ \f\n\r\t\v] 	Непробельный символ
\w 	[[:word:]] 	Буквенный или цифровой символ или знак подчёркивания
\W 	[^[:word:]] 	Любой символ, кроме буквенного или цифрового символа или знака подчёркивания

? 	Ноль или одно 	{0,1} 	colou?r 	color, colour
* 	Ноль или более 	{0,} 	colou*r 	color, colour, colouur и т. д.
+ 	Одно или более 	{1,} 	colou+r 	colour, colouur и т. д. (но не color)

(?:шаблон)   Если группа используется только для группировки и её результат в дальнейшем не потребуется
(шаблон)   Буден найден результат для этой группы";

        private string sampleFindSetting = "(\\S+)\\s+(\\S+)";
        private string sampleReplaceSetting = "\\{\\r\\n{0}, {1}\\r\\n\\}";
        private string sampleInText =
@"18,580	18,000 	1,450	1,500
13	12 	228	238
81	76	8.0	8.1
0.007	0.006	6,000	6,800";

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

        public string DescriptionRules => descriptionRules;

        private ICommand replaceCommand;
        public ICommand ReplaceCommand => GetDelegateCommand<object>(ref replaceCommand, OnReplace);
        
        private ICommand findCommand;
        public ICommand FindCommand => GetDelegateCommand<object>(ref findCommand, OnFind);

        private void OnReplace(object obj)
        {
            try
            {
                OutText = parser.Replace(InText.Text, FindSetting, ReplaceSetting);
                InText = new ColorSource { Text = InText.Text };

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnFind(object obj)
        {
            try
            {
                InText = new ColorSource { Text = InText.Text, HighlightWords = parser.Highlight(InText.Text, FindSetting) };

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public MainWindowViewModel()
        {
            FindSetting = sampleFindSetting;
            ReplaceSetting = sampleReplaceSetting;
            InText = new ColorSource { Text = sampleInText };
        }
    }

    public class Parser
    {
        private readonly Color[] colors =
        {
            Colors.CadetBlue,
            Colors.Yellow,
            Colors.YellowGreen,
            Colors.DeepPink,
        };

        public IEnumerable<ColorWord> Highlight(string inText, string setting)
        {
            IEnumerable<Tuple<int, int, int>> words = Find(inText, setting);
            return words.Select(x => new ColorWord
            {
                Color = colors[x.Item1 % colors.Length],
                Start = x.Item2,
                End = x.Item3,
            });
        }

        public IEnumerable<Tuple<int, int, int>> Find(string inText, string setting)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return null;
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            var matches = r.Matches(inText);
            if (matches.Count == 0)
            {
                return null;
            }
            
            var highlightedWords = new List<Tuple<int, int, int>>();
            foreach (Match match in matches)
            {
                highlightedWords.AddRange(FindMatch(match));
            }
            
            return highlightedWords;

        }

        public string Replace(string inText, string setting, string replaceSetting)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return inText;
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            var matches = r.Matches(inText);
            if (matches.Count == 0)
            {   
                return inText;
            }

            replaceSetting = Ecrane(replaceSetting);

            var stringBuilder = new StringBuilder();
            int startIndex = 0;
            foreach (Match match in matches)
            {
                string startString = inText.Substring(startIndex, match.Index - startIndex);
                stringBuilder.Append(startString);
                startIndex += startString.Length;

                string formatedMatch = FormatMatch(match, replaceSetting);
                stringBuilder.Append(formatedMatch);
                startIndex += match.Length;
            }
            
            string endString = inText.Substring(startIndex);
            stringBuilder.Append(endString);

            return stringBuilder.ToString();
        }

        private string Ecrane(string replaceSetting)
        {
            var simbols = new Dictionary<string, string>
            {
                { "f", "\f" },
                { "r", "\r" },
                { "t", "\t" },
                { "v", "\v" },
                { "n", "\n" },
                { "{", "{{" },
                { "}", "}}" },
            };

            var stringBuilder = new StringBuilder(replaceSetting);
            foreach (var simbol in simbols)
            {
                stringBuilder.Replace($"\\{simbol.Key}", $"{simbol.Value}");
            }
            return stringBuilder.ToString();
        }

        private IEnumerable<Tuple<int, int, int>> FindMatch(Match match)
        {
            return match.Groups.Cast<Group>().Skip(1).Select((x, i) => new Tuple<int, int, int>(i, x.Index, x.Index + x.Length));
        }

        private string FormatMatch(Match match, string replaceSetting)
        {
            object[] args = match.Groups.Cast<Group>().Skip(1).Select(x => x.Value).Cast<object>().ToArray();
            return string.Format(replaceSetting, args);
        }
    }
}
