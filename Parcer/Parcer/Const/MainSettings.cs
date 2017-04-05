namespace Parcer.Const
{
    public static class MainSettings
    {
        public const string DescriptionRules =
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

        public const string SampleFindSetting = "\"((\\w+)\\s*(\\w*))\"";
        // \{[^"]*"([^"]+)(?:(?:"[^"]+"([^"]+)")|(?:.+Messages(w+)))[^\}]*\}
        // \{[^"]*"([^"]+)(?:"[^"]+"([^"]+)")[^\}]*\}
        // ([^\t\r\n]+)\t([^\t\r\n]+)
        // (.+)\r\n
        public const string SampleReplaceSeparator = ";";
        public const string SampleReplaceSetting = "={0}{1};-{0};+{0}";
        public const string SampleBuildSetting = "\\{\\r\\n\"{0}\", \"{1}\"\\r\\n\\},\\r\\n";
        // "{0}"\t"{1}"\r\n
        //"{0}"\r\n

        /*private string sampleInText =
@"18,580	18,000 	1,450	1,500
13	12 	228	238
81	76	8.0	8.1
0.007	0.006	6,000	6,800";*/

        public const string SampleInText =
            @"{
    ""Specialized REITs"",
    ""Companies or Trusts engaged in the acquisition, development, ownership, leasing, management and operation of properties not classified elsewhere. Includes trusts that operate and invest in storage properties. It also includes REITs that do not generate a majority of their revenues and income from real estate rental and leasing operations.""
},
{
    ""Renewable Electricity"",
    ""Companies that engage in the generation and distribution of electricity using renewable sources, including, but not limited to, companies that produce electricity using biomass, geothermal energy, solar energy, hydropower, and wind power. Excludes companies manufacturing capital equipment used to generate electricity using renewable sources, such as manufacturers of solar power systems, installers of photovoltaic cells,  and companies involved in the provision of technology, components, and services mainly to this market.""
},
{
    ""Renewable Energy"",
    ""Companies that engage in the generation and distribution of electricity using renewable sources, including, but not limited to, companies that produce electricity using biomass, geothermal energy, solar energy, hydropower, and wind power. Excludes companies manufacturing capital equipment used to generate electricity using renewable sources, such as manufacturers of solar power systems, installers of photovoltaic cells,  and companies involved in the provision of technology, components, and services mainly to this market.""
},
{
    ""Financial Exchanges & Data"",
    ""Financial  exchanges  for  securities,  commodities,  derivatives and other financial instruments, and providers of financial decision support tools and products  including ratings agencies.""
},
{
    ""Motorcycle Manufacturers"",
    ""Companies that produce motorcycles, scooters or three-wheelers. Excludes bicycles classified in the Leisure Products Sub-Industry.""
},";

        public const string SampleCode =
            @"//return matches.OrderBy(x => x.First())
return matches";
    }
}