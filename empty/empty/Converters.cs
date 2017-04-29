
namespace empty
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using System.Linq;
    using System.ComponentModel;
    
    public enum IsoCurrencyCode
    {
        AED,
        AOA,
        ARS,
        AUD,
        BAM,
        BGN,
        BMD,
        BRL,
        CAD,
        CHF,
        CLP,
        CNY,
        COP,
        CRC,
        CUP,
        CZK,
        DEM,
        DKK,
        DOP,
        EGP,
        EUR,
        FJD,
        GBP,
        GEL,
        HKD,
        HRK,
        HUF,
        IDR,
        ILS,
        INR,
        IQD,
        IRR,
        ISK,
        ITL,
        JMD,
        JPY,
        KPW,
        KRW,
        KYD,
        KZT,
        LBP,
        LKR,
        MAD,
        MNT,
        MWK,
        MXN,
        MYR,
        NGN,
        NOK,
        NZD,
        PAB,
        PEN,
        PHP,
        PKR,
        PLN,
        QAR,
        RON,
        RSD,
        RUR,
        SAR,
        SDG,
        SEK,
        SGD,
        SKK,
        THB,
        TRY,
        TWD,
        UAH,
        USD,
        UYU,
        VEF,
        VND,
        WST,
        XOF,
        YER,
        ZAR,
        ZMK,
    }
    public enum InvestmentStrategy
    {
        Activist,
        [System.ComponentModel.DescriptionAttribute("Convertible Bond Arbitrage")]
        Convertible_Bond_Arbitrage,
        [System.ComponentModel.DescriptionAttribute("Credit Distressed")]
        Credit_Distressed,
        [System.ComponentModel.DescriptionAttribute("Credit Long/Short")]
        Credit_LongSlashShort,
        [System.ComponentModel.DescriptionAttribute("Equity Long/Short")]
        Equity_LongSlashShort,
        [System.ComponentModel.DescriptionAttribute("Equity Market Neutral")]
        Equity_Market_Neutral,
        [System.ComponentModel.DescriptionAttribute("Event Driven")]
        Event_Driven,
        [System.ComponentModel.DescriptionAttribute("Fixed Income Arbitrage")]
        Fixed_Income_Arbitrage,
        [System.ComponentModel.DescriptionAttribute("Global Macro")]
        Global_Macro,
        [System.ComponentModel.DescriptionAttribute("Managed Futures (CTA)")]
        Managed_Futures_LeftParenthesisCTARightParenthesis,
        [System.ComponentModel.DescriptionAttribute("Risk Arbitrage")]
        Risk_Arbitrage,
        [System.ComponentModel.DescriptionAttribute("Volatility Arbitrage")]
        Volatility_Arbitrage,
        Multiple,
        [System.ComponentModel.DescriptionAttribute("Non Hedge Fund")]
        Non_Hedge_Fund,
        Other,
    }
    public enum InvestmentStyle
    {
        Systematic,
        [System.ComponentModel.DescriptionAttribute("Discretionary Top Down")]
        Discretionary_Top_Down,
        [System.ComponentModel.DescriptionAttribute("Discretionary Bottom Up")]
        Discretionary_Bottom_Up,
        [System.ComponentModel.DescriptionAttribute("Discretionary Unspecified")]
        Discretionary_Unspecified,
        Multiple,
        [System.ComponentModel.DescriptionAttribute("Not Applicable")]
        Not_Applicable,
    }
    public enum AssetClass
    {
        Equity,
        [System.ComponentModel.DescriptionAttribute("Sovereign Interest Rate")]
        Sovereign_Interest_Rate,
        Credit,
        [System.ComponentModel.DescriptionAttribute("Convertible Bond")]
        Convertible_Bond,
        Currency,
        [System.ComponentModel.DescriptionAttribute("Real Asset")]
        Real_Asset,
        Commodity,
        Insurance,
        Multiple,
        Other,
    }
    public enum InstrumentType
    {
        Securities,
        Derivatives,
        [System.ComponentModel.DescriptionAttribute("Physical Assets")]
        Physical_Assets,
        [System.ComponentModel.DescriptionAttribute("Securities & Derivatives")]
        Securities_Ampersand_Derivatives,
        [System.ComponentModel.DescriptionAttribute("Derivatives & Physical Assets")]
        Derivatives_Ampersand_Physical_Assets,
        [System.ComponentModel.DescriptionAttribute("Securities & Physical Assets")]
        Securities_Ampersand_Physical_Assets,
        [System.ComponentModel.DescriptionAttribute("Securities, Derivatives & Physical Assets")]
        SecuritiesComma_Derivatives_Ampersand_Physical_Assets,
    }
    public enum TradingStrategy
    {
        Directional,
        [System.ComponentModel.DescriptionAttribute("Relative Value")]
        Relative_Value,
        Multiple,
        [System.ComponentModel.DescriptionAttribute("Not Applicable")]
        Not_Applicable,
    }
    public enum MarketExposure
    {
        [System.ComponentModel.DescriptionAttribute("Long Only")]
        Long_Only,
        [System.ComponentModel.DescriptionAttribute("Short Only")]
        Short_Only,
        [System.ComponentModel.DescriptionAttribute("Long Bias")]
        Long_Bias,
        [System.ComponentModel.DescriptionAttribute("Variable Bias")]
        Variable_Bias,
        [System.ComponentModel.DescriptionAttribute("Short Bias")]
        Short_Bias,
        Neutral,
        Multiple,
    }
    public enum HoldingPeriod
    {
        [System.ComponentModel.DescriptionAttribute("More Than 24 Months")]
        More_Than_24_Months,
        [System.ComponentModel.DescriptionAttribute("12 to 24 Months")]
        Item12_To_24_Months,
        [System.ComponentModel.DescriptionAttribute("9 to 12 Months")]
        Item9_To_12_Months,
        [System.ComponentModel.DescriptionAttribute("6 to 9 Months")]
        Item6_To_9_Months,
        [System.ComponentModel.DescriptionAttribute("3 to 6 Months")]
        Item3_To_6_Months,
        [System.ComponentModel.DescriptionAttribute("1 to 3 Months")]
        Item1_To_3_Months,
        [System.ComponentModel.DescriptionAttribute("1 Week to 1 Month")]
        Item1_Week_To_1_Month,
        [System.ComponentModel.DescriptionAttribute("Less Than 1 Week")]
        Less_Than_1_Week,
        Multiple,
    }
    public enum ValueAtRiskMethodology
    {
        [System.ComponentModel.DescriptionAttribute("Historical Simulation")]
        Historical_Simulation,
        [System.ComponentModel.DescriptionAttribute("Monte Carlo Simulation")]
        Monte_Carlo_Simulation,
        Parametric,
        Other,
    }
    public enum AumCalculationMethod
    {
        GAAP,
        [System.ComponentModel.DescriptionAttribute("Backward Looking")]
        Backward_Looking,
        [System.ComponentModel.DescriptionAttribute("Forward Looking")]
        Forward_Looking,
    }
    public enum CounterpartyCount
    {
        [System.ComponentModel.DescriptionAttribute("0")]
        Item0,
        [System.ComponentModel.DescriptionAttribute("1")]
        Item1,
        [System.ComponentModel.DescriptionAttribute("2")]
        Item2,
        [System.ComponentModel.DescriptionAttribute("3")]
        Item3,
        [System.ComponentModel.DescriptionAttribute("4")]
        Item4,
        [System.ComponentModel.DescriptionAttribute("5")]
        Item5,
        [System.ComponentModel.DescriptionAttribute("6 to 9")]
        Item6_To_9,
        [System.ComponentModel.DescriptionAttribute("10 to 15")]
        Item10_To_15,
        [System.ComponentModel.DescriptionAttribute("15+")]
        Item15Plus,
    }
    public enum CounterpartyPercentage
    {
        [System.ComponentModel.DescriptionAttribute("0%")]
        Item0Percent,
        [System.ComponentModel.DescriptionAttribute("0% to 5%")]
        Item0Percent_To_5Percent,
        [System.ComponentModel.DescriptionAttribute("6% to 10%")]
        Item6Percent_To_10Percent,
        [System.ComponentModel.DescriptionAttribute("11% to 20%")]
        Item11Percent_To_20Percent,
        [System.ComponentModel.DescriptionAttribute("21% to 30%")]
        Item21Percent_To_30Percent,
        [System.ComponentModel.DescriptionAttribute("31% to 40%")]
        Item31Percent_To_40Percent,
        [System.ComponentModel.DescriptionAttribute("41% to 50%")]
        Item41Percent_To_50Percent,
        [System.ComponentModel.DescriptionAttribute("51% to 75%")]
        Item51Percent_To_75Percent,
        [System.ComponentModel.DescriptionAttribute("76% to 100%")]
        Item76Percent_To_100Percent,
    }
    public enum CounterpartyName
    {
        [System.ComponentModel.DescriptionAttribute("ABN AMRO Bank")]
        ABN_AMRO_Bank,
        [System.ComponentModel.DescriptionAttribute("Banco Itau")]
        Banco_Itau,
        [System.ComponentModel.DescriptionAttribute("Bank of America")]
        Bank_Of_America,
        [System.ComponentModel.DescriptionAttribute("Bank of Butterfield")]
        Bank_Of_Butterfield,
        [System.ComponentModel.DescriptionAttribute("Bank of Montreal")]
        Bank_Of_Montreal,
        [System.ComponentModel.DescriptionAttribute("Bank of New York Mellon")]
        Bank_Of_New_York_Mellon,
        [System.ComponentModel.DescriptionAttribute("Barclay Bank")]
        Barclay_Bank,
        [System.ComponentModel.DescriptionAttribute("Barclays Capital Securities")]
        Barclays_Capital_Securities,
        [System.ComponentModel.DescriptionAttribute("BlackRock Financial Management")]
        BlackRock_Financial_Management,
        [System.ComponentModel.DescriptionAttribute("BMO Nesbitt Burns")]
        BMO_Nesbitt_Burns,
        [System.ComponentModel.DescriptionAttribute("BNP Paribas")]
        BNP_Paribas,
        [System.ComponentModel.DescriptionAttribute("Brown Brothers Harriman")]
        Brown_Brothers_Harriman,
        BTIG,
        [System.ComponentModel.DescriptionAttribute("CACEIS Bank")]
        CACEIS_Bank,
        [System.ComponentModel.DescriptionAttribute("Caledonian Trust")]
        Caledonian_Trust,
        [System.ComponentModel.DescriptionAttribute("Cantor Fitzgerald")]
        Cantor_Fitzgerald,
        [System.ComponentModel.DescriptionAttribute("Charles Schwab")]
        Charles_Schwab,
        [System.ComponentModel.DescriptionAttribute("Citco Fund Services")]
        Citco_Fund_Services,
        [System.ComponentModel.DescriptionAttribute("Citigroup Global Markets")]
        Citigroup_Global_Markets,
        [System.ComponentModel.DescriptionAttribute("CME Group")]
        CME_Group,
        Convergex,
        [System.ComponentModel.DescriptionAttribute("Credit Suisse First Boston")]
        Credit_Suisse_First_Boston,
        [System.ComponentModel.DescriptionAttribute("Credit Suisse International")]
        Credit_Suisse_International,
        [System.ComponentModel.DescriptionAttribute("Deutsche Bank")]
        Deutsche_Bank,
        [System.ComponentModel.DescriptionAttribute("Fidelity Prime Services")]
        Fidelity_Prime_Services,
        [System.ComponentModel.DescriptionAttribute("First Republic Trust Company")]
        First_Republic_Trust_Company,
        [System.ComponentModel.DescriptionAttribute("GlobeOp Financial Services")]
        GlobeOp_Financial_Services,
        [System.ComponentModel.DescriptionAttribute("Goldman Sachs")]
        Goldman_Sachs,
        [System.ComponentModel.DescriptionAttribute("Horizon Cash Management")]
        Horizon_Cash_Management,
        HSBC,
        [System.ComponentModel.DescriptionAttribute("HSBC Private Bank")]
        HSBC_Private_Bank,
        [System.ComponentModel.DescriptionAttribute("ING Group")]
        ING_Group,
        [System.ComponentModel.DescriptionAttribute("Interactive Brokers")]
        Interactive_Brokers,
        [System.ComponentModel.DescriptionAttribute("Investec Prime Brokering")]
        Investec_Prime_Brokering,
        Jefferies,
        [System.ComponentModel.DescriptionAttribute("JP Morgan")]
        JP_Morgan,
        [System.ComponentModel.DescriptionAttribute("Julius Baer")]
        Julius_Baer,
        [System.ComponentModel.DescriptionAttribute("Kotak Mahindra")]
        Kotak_Mahindra,
        [System.ComponentModel.DescriptionAttribute("LaSalle Bank")]
        LaSalle_Bank,
        Merlin,
        [System.ComponentModel.DescriptionAttribute("Merrill Lynch International")]
        Merrill_Lynch_International,
        [System.ComponentModel.DescriptionAttribute("MF Global")]
        MF_Global,
        [System.ComponentModel.DescriptionAttribute("Mitsubishi UFJ Securities International")]
        Mitsubishi_UFJ_Securities_International,
        [System.ComponentModel.DescriptionAttribute("Mizuho International")]
        Mizuho_International,
        [System.ComponentModel.DescriptionAttribute("Mizuho Trust")]
        Mizuho_Trust,
        [System.ComponentModel.DescriptionAttribute("Montague Place Custody Services")]
        Montague_Place_Custody_Services,
        [System.ComponentModel.DescriptionAttribute("Morgan Stanley")]
        Morgan_Stanley,
        [System.ComponentModel.DescriptionAttribute("National Bank Trust")]
        National_Bank_Trust,
        Natixis,
        [System.ComponentModel.DescriptionAttribute("Newedge Group")]
        Newedge_Group,
        [System.ComponentModel.DescriptionAttribute("Nomura International")]
        Nomura_International,
        [System.ComponentModel.DescriptionAttribute("Northern Trust Corporation")]
        Northern_Trust_Corporation,
        [System.ComponentModel.DescriptionAttribute("Pictet & Cie")]
        Pictet_Ampersand_Cie,
        [System.ComponentModel.DescriptionAttribute("PNC Global Investment Servicing")]
        PNC_Global_Investment_Servicing,
        [System.ComponentModel.DescriptionAttribute("Rand Financial Services")]
        Rand_Financial_Services,
        [System.ComponentModel.DescriptionAttribute("RBC Dexia Investor Services")]
        RBC_Dexia_Investor_Services,
        [System.ComponentModel.DescriptionAttribute("Rothschild Bank")]
        Rothschild_Bank,
        [System.ComponentModel.DescriptionAttribute("Royal Bank of Scotland")]
        Royal_Bank_Of_Scotland,
        [System.ComponentModel.DescriptionAttribute("Safra National Bank of New York")]
        Safra_National_Bank_Of_New_York,
        [System.ComponentModel.DescriptionAttribute("Samsung Futures")]
        Samsung_Futures,
        [System.ComponentModel.DescriptionAttribute("Scotia Capital")]
        Scotia_Capital,
        [System.ComponentModel.DescriptionAttribute("SinoPac Securities Corporation")]
        SinoPac_Securities_Corporation,
        [System.ComponentModel.DescriptionAttribute("Skandinaviska Enskilda Banken")]
        Skandinaviska_Enskilda_Banken,
        [System.ComponentModel.DescriptionAttribute("Societe Generale")]
        Societe_Generale,
        [System.ComponentModel.DescriptionAttribute("Standard Chartered Bank")]
        Standard_Chartered_Bank,
        [System.ComponentModel.DescriptionAttribute("State Street")]
        State_Street,
        Swedbank,
        [System.ComponentModel.DescriptionAttribute("TD Bank")]
        TD_Bank,
        UBS,
        [System.ComponentModel.DescriptionAttribute("US Bank")]
        US_Bank,
        [System.ComponentModel.DescriptionAttribute("US Trust")]
        US_Trust,
        Wachovia,
        [System.ComponentModel.DescriptionAttribute("Wells Fargo & Company")]
        Wells_Fargo_Ampersand_Company,
        [System.ComponentModel.DescriptionAttribute("Counterparty A")]
        Counterparty_A,
        [System.ComponentModel.DescriptionAttribute("Counterparty B")]
        Counterparty_B,
        [System.ComponentModel.DescriptionAttribute("Counterparty C")]
        Counterparty_C,
        [System.ComponentModel.DescriptionAttribute("Counterparty D")]
        Counterparty_D,
        [System.ComponentModel.DescriptionAttribute("Counterparty E")]
        Counterparty_E,
        [System.ComponentModel.DescriptionAttribute("Counterparty F")]
        Counterparty_F,
        [System.ComponentModel.DescriptionAttribute("Counterparty G")]
        Counterparty_G,
        [System.ComponentModel.DescriptionAttribute("Counterparty H")]
        Counterparty_H,
        [System.ComponentModel.DescriptionAttribute("Counterparty I")]
        Counterparty_I,
        [System.ComponentModel.DescriptionAttribute("Counterparty J")]
        Counterparty_J,
        [System.ComponentModel.DescriptionAttribute("Counterparty K")]
        Counterparty_K,
        [System.ComponentModel.DescriptionAttribute("Counterparty L")]
        Counterparty_L,
        [System.ComponentModel.DescriptionAttribute("Counterparty M")]
        Counterparty_M,
        [System.ComponentModel.DescriptionAttribute("Counterparty N")]
        Counterparty_N,
        [System.ComponentModel.DescriptionAttribute("Counterparty O")]
        Counterparty_O,
        [System.ComponentModel.DescriptionAttribute("Counterparty P")]
        Counterparty_P,
        [System.ComponentModel.DescriptionAttribute("Counterparty Q")]
        Counterparty_Q,
        [System.ComponentModel.DescriptionAttribute("Counterparty R")]
        Counterparty_R,
        [System.ComponentModel.DescriptionAttribute("Counterparty S")]
        Counterparty_S,
        [System.ComponentModel.DescriptionAttribute("Counterparty T")]
        Counterparty_T,
        [System.ComponentModel.DescriptionAttribute("Counterparty U")]
        Counterparty_U,
        [System.ComponentModel.DescriptionAttribute("Counterparty V")]
        Counterparty_V,
        [System.ComponentModel.DescriptionAttribute("Counterparty W")]
        Counterparty_W,
        [System.ComponentModel.DescriptionAttribute("Counterparty X")]
        Counterparty_X,
        [System.ComponentModel.DescriptionAttribute("Counterparty Y")]
        Counterparty_Y,
        [System.ComponentModel.DescriptionAttribute("Counterparty Z")]
        Counterparty_Z,
    }
    public interface IAumPercentageRedemptionWithNoPenaltyAttribute
    {
        string AumPercentageRedeemableWithNoPenalty
        {
            get;
            set;
        }
    }
    public interface IAumPercentageRedemptionAttributes : IAumPercentageRedemptionWithNoPenaltyAttribute
    {
        string AumPercentageRedeemableWithPenalty
        {
            get;
            set;
        }
    }
    public interface IAumPercentageAttribute
    {
        string AumPercentage
        {
            get;
            set;
        }
    }
    public interface INonNegativeAssetValueAttribute
    {
        string AssetValue
        {
            get;
            set;
        }
    }
    public interface IAumPercentageAssetValueAttributes : IAumPercentageAttribute, INonNegativeAssetValueAttribute
    {
    }
    public interface IGrossNetPercentageAttributes
    {
        string GrossPercentage
        {
            get;
            set;
        }
        string NetPercentage
        {
            get;
            set;
        }
    }
    public interface INettedNonNettedLongShortAumPercentageAttributes
    {
        string NonNettedLongAumPercentage
        {
            get;
            set;
        }
        string NonNettedShortAumPercentage
        {
            get;
            set;
        }
        string NettedLongAumPercentage
        {
            get;
            set;
        }
        string NettedShortAumPercentage
        {
            get;
            set;
        }
    }
    public interface ILongShortPositionAttributes
    {
        string NumberOfLongPositions
        {
            get;
            set;
        }
        string NumberOfShortPositions
        {
            get;
            set;
        }
    }
    public interface ILongShortAumPercentagePositionAttributes : INettedNonNettedLongShortAumPercentageAttributes, ILongShortPositionAttributes
    {
    }
    public interface ILongShortIssuerAttributes
    {
        string NumberOfLongIssuers
        {
            get;
            set;
        }
        string NumberOfShortIssuers
        {
            get;
            set;
        }
    }
    public interface ILongShortAumPercentageIssuerAttributes : INettedNonNettedLongShortAumPercentageAttributes, ILongShortIssuerAttributes
    {
    }
    public interface ILongShortAumPercentageAttributes
    {
        string LongAumPercentage
        {
            get;
            set;
        }
        string ShortAumPercentage
        {
            get;
            set;
        }
    }
    public interface INettedNonNettedLongShortAssetValueAttributes
    {
        string NonNettedLongAssetValue
        {
            get;
            set;
        }
        string NonNettedShortAssetValue
        {
            get;
            set;
        }
        string NettedLongAssetValue
        {
            get;
            set;
        }
        string NettedShortAssetValue
        {
            get;
            set;
        }
    }
    public interface INonPositiveAssetValueAttribute
    {
        string AssetValue
        {
            get;
            set;
        }
    }
    public interface IAssetValueAttribute
    {
        string AssetValue
        {
            get;
            set;
        }
    }
    public interface IValueAtRiskAttributes
    {
        string ExposurePercentage
        {
            get;
            set;
        }
        string VarPercentage
        {
            get;
            set;
        }
        string CVarPercentage
        {
            get;
            set;
        }
    }
    public interface IBetaSensitivityAttribute
    {
        string Beta
        {
            get;
            set;
        }
    }
    public interface IDeltaSensitivityAttribute
    {
        string Delta
        {
            get;
            set;
        }
    }
    public interface IGammaVegaThetaSensitivityAttributes
    {
        string Gamma
        {
            get;
            set;
        }
        string Vega
        {
            get;
            set;
        }
        string Theta
        {
            get;
            set;
        }
    }
    public interface IBasisPointChangeSensitivityAttributes
    {
        string Cs01
        {
            get;
            set;
        }
        string Dv01
        {
            get;
            set;
        }
    }
    public interface IOneHundredBasedAverageDeltaAttribute
    {
        string AverageDelta
        {
            get;
            set;
        }
    }
    public interface IPercentageOfExposureInCalculationAttributes
    {
        string PercentageOfLongExposureInCalculation
        {
            get;
            set;
        }
        string PercentageOfShortExposureInCalculation
        {
            get;
            set;
        }
    }
    public interface IStressTestAttributes : IPercentageOfExposureInCalculationAttributes
    {
        string PortfolioReturnPercentage
        {
            get;
            set;
        }
    }
    public interface IHistoricalStressTestAttributes : IStressTestAttributes
    {
        string StartDate
        {
            get;
            set;
        }
    }
    public interface IAverageNumberOfYearsAttribute
    {
        string AverageNumberOfYears
        {
            get;
            set;
        }
    }
    public interface IAveragePercentageOfIssuanceAttribute
    {
        string AveragePercentageOfIssuance
        {
            get;
            set;
        }
    }
    public interface IAveragePercentageAttribute
    {
        string AveragePercentage
        {
            get;
            set;
        }
    }
    public interface IAverageCentsPerDollarAttribute
    {
        string AverageCentsPerDollar
        {
            get;
            set;
        }
    }
    public interface IAveragePriceAttribute
    {
        string AveragePrice
        {
            get;
            set;
        }
    }
    public interface IDecayFactorAttribute
    {
        string DecayFactor
        {
            get;
            set;
        }
    }
    public interface INumberOfDaysAttribute
    {
        string NumberOfDays
        {
            get;
            set;
        }
    }
    public interface IEquityAssetValueCounterpartyExposureAttribute
    {
        string Equity
        {
            get;
            set;
        }
    }
    public interface IOtherAssetValueCounterpartyExposureAttributes
    {
        string LongMarketValue
        {
            get;
            set;
        }
        string ShortMarketValue
        {
            get;
            set;
        }
        string Cash
        {
            get;
            set;
        }
        string OpenTradeEquity
        {
            get;
            set;
        }
        string AvailableLiquidity
        {
            get;
            set;
        }
        string RequiredMargin
        {
            get;
            set;
        }
    }
    public interface ICounterpartyCountEquityAssetValueCounterpartyExposureAttributes : IEquityAssetValueCounterpartyExposureAttribute
    {
        CounterpartyCount NumberOfCustodiansAndCounterparties
        {
            get;
            set;
        }
    }
    public interface ICounterpartyCountCounterpartyExposureAttributes : ICounterpartyCountEquityAssetValueCounterpartyExposureAttributes, IOtherAssetValueCounterpartyExposureAttributes, ILongShortAumPercentageAttributes
    {
    }
    public interface IAgreementCountEquityAssetValueCounterpartyExposureAttributes : IEquityAssetValueCounterpartyExposureAttribute
    {
        CounterpartyCount NumberOfFinancingAgreements
        {
            get;
            set;
        }
    }
    public interface IAgreementCountCounterpartyExposureAttributes : IAgreementCountEquityAssetValueCounterpartyExposureAttributes, IOtherAssetValueCounterpartyExposureAttributes, ILongShortAumPercentageAttributes
    {
    }
    public interface ICounterpartyNameCounterpartyExposureAttributes : IEquityAssetValueCounterpartyExposureAttribute, IOtherAssetValueCounterpartyExposureAttributes, ILongShortAumPercentageAttributes
    {
        CounterpartyName CounterpartyName
        {
            get;
            set;
        }
    }
    public class AumPercentageRedemptionWithNoPenaltyAttribute : IAumPercentageRedemptionWithNoPenaltyAttribute
    {
        public string AumPercentageRedeemableWithNoPenalty { get; set; }
    }
    public class AumPercentageRedemptionAttributes : IAumPercentageRedemptionAttributes
    {
        public string AumPercentageRedeemableWithPenalty { get; set; }
        public string AumPercentageRedeemableWithNoPenalty { get; set; }
    }
    public class AumPercentageAttribute : IAumPercentageAttribute
    {
        public string AumPercentage { get; set; }
    }
    public class NonNegativeAssetValueAttribute : INonNegativeAssetValueAttribute
    {
        public string AssetValue { get; set; }
    }
    public class AumPercentageAssetValueAttributes : IAumPercentageAssetValueAttributes
    {
        public string AumPercentage { get; set; }
        public string AssetValue { get; set; }
    }
    public class GrossNetPercentageAttributes : IGrossNetPercentageAttributes
    {
        public string GrossPercentage { get; set; }
        public string NetPercentage { get; set; }
    }
    public class NettedNonNettedLongShortAumPercentageAttributes : INettedNonNettedLongShortAumPercentageAttributes
    {
        public string NonNettedLongAumPercentage { get; set; }
        public string NonNettedShortAumPercentage { get; set; }
        public string NettedLongAumPercentage { get; set; }
        public string NettedShortAumPercentage { get; set; }
    }
    public class LongShortPositionAttributes : ILongShortPositionAttributes
    {
        public string NumberOfLongPositions { get; set; }
        public string NumberOfShortPositions { get; set; }
    }
    public class LongShortAumPercentagePositionAttributes : ILongShortAumPercentagePositionAttributes
    {
        public string NonNettedLongAumPercentage { get; set; }
        public string NonNettedShortAumPercentage { get; set; }
        public string NettedLongAumPercentage { get; set; }
        public string NettedShortAumPercentage { get; set; }
        public string NumberOfLongPositions { get; set; }
        public string NumberOfShortPositions { get; set; }
    }
    public class LongShortIssuerAttributes : ILongShortIssuerAttributes
    {
        public string NumberOfLongIssuers { get; set; }
        public string NumberOfShortIssuers { get; set; }
    }
    public class LongShortAumPercentageIssuerAttributes : ILongShortAumPercentageIssuerAttributes
    {
        public string NonNettedLongAumPercentage { get; set; }
        public string NonNettedShortAumPercentage { get; set; }
        public string NettedLongAumPercentage { get; set; }
        public string NettedShortAumPercentage { get; set; }
        public string NumberOfLongIssuers { get; set; }
        public string NumberOfShortIssuers { get; set; }
    }
    public class LongShortAumPercentageAttributes : ILongShortAumPercentageAttributes
    {
        public string LongAumPercentage { get; set; }
        public string ShortAumPercentage { get; set; }
    }
    public class NettedNonNettedLongShortAssetValueAttributes : INettedNonNettedLongShortAssetValueAttributes
    {
        public string NonNettedLongAssetValue { get; set; }
        public string NonNettedShortAssetValue { get; set; }
        public string NettedLongAssetValue { get; set; }
        public string NettedShortAssetValue { get; set; }
    }
    public class NonPositiveAssetValueAttribute : INonPositiveAssetValueAttribute
    {
        public string AssetValue { get; set; }
    }
    public class AssetValueAttribute : IAssetValueAttribute
    {
        public string AssetValue { get; set; }
    }
    public class ValueAtRiskAttributes : IValueAtRiskAttributes
    {
        public string ExposurePercentage { get; set; }
        public string VarPercentage { get; set; }
        public string CVarPercentage { get; set; }
    }
    public class BetaSensitivityAttribute : IBetaSensitivityAttribute
    {
        public string Beta { get; set; }
    }
    public class DeltaSensitivityAttribute : IDeltaSensitivityAttribute
    {
        public string Delta { get; set; }
    }
    public class GammaVegaThetaSensitivityAttributes : IGammaVegaThetaSensitivityAttributes
    {
        public string Gamma { get; set; }
        public string Vega { get; set; }
        public string Theta { get; set; }
    }
    public class BasisPointChangeSensitivityAttributes : IBasisPointChangeSensitivityAttributes
    {
        public string Cs01 { get; set; }
        public string Dv01 { get; set; }
    }
    public class OneHundredBasedAverageDeltaAttribute : IOneHundredBasedAverageDeltaAttribute
    {
        public string AverageDelta { get; set; }
    }
    public class PercentageOfExposureInCalculationAttributes : IPercentageOfExposureInCalculationAttributes
    {
        public string PercentageOfLongExposureInCalculation { get; set; }
        public string PercentageOfShortExposureInCalculation { get; set; }
    }
    public class StressTestAttributes : IStressTestAttributes
    {
        public string PortfolioReturnPercentage { get; set; }
        public string PercentageOfLongExposureInCalculation { get; set; }
        public string PercentageOfShortExposureInCalculation { get; set; }
    }
    public class HistoricalStressTestAttributes : IHistoricalStressTestAttributes
    {
        public string StartDate { get; set; }
        public string PortfolioReturnPercentage { get; set; }
        public string PercentageOfLongExposureInCalculation { get; set; }
        public string PercentageOfShortExposureInCalculation { get; set; }
    }
    public class AverageNumberOfYearsAttribute : IAverageNumberOfYearsAttribute
    {
        public string AverageNumberOfYears { get; set; }
    }
    public class AveragePercentageOfIssuanceAttribute : IAveragePercentageOfIssuanceAttribute
    {
        public string AveragePercentageOfIssuance { get; set; }
    }
    public class AveragePercentageAttribute : IAveragePercentageAttribute
    {
        public string AveragePercentage { get; set; }
    }
    public class AverageCentsPerDollarAttribute : IAverageCentsPerDollarAttribute
    {
        public string AverageCentsPerDollar { get; set; }
    }
    public class AveragePriceAttribute : IAveragePriceAttribute
    {
        public string AveragePrice { get; set; }
    }
    public class DecayFactorAttribute : IDecayFactorAttribute
    {
        public string DecayFactor { get; set; }
    }
    public class NumberOfDaysAttribute : INumberOfDaysAttribute
    {
        public string NumberOfDays { get; set; }
    }
    public class EquityAssetValueCounterpartyExposureAttribute : IEquityAssetValueCounterpartyExposureAttribute
    {
        public string Equity { get; set; }
    }
    public class OtherAssetValueCounterpartyExposureAttributes : IOtherAssetValueCounterpartyExposureAttributes
    {
        public string LongMarketValue { get; set; }
        public string ShortMarketValue { get; set; }
        public string Cash { get; set; }
        public string OpenTradeEquity { get; set; }
        public string AvailableLiquidity { get; set; }
        public string RequiredMargin { get; set; }
    }
    public class CounterpartyCountEquityAssetValueCounterpartyExposureAttributes : ICounterpartyCountEquityAssetValueCounterpartyExposureAttributes
    {
        public CounterpartyCount NumberOfCustodiansAndCounterparties { get; set; }
        public string Equity { get; set; }
    }
    public class CounterpartyCountCounterpartyExposureAttributes : ICounterpartyCountCounterpartyExposureAttributes
    {
        public CounterpartyCount NumberOfCustodiansAndCounterparties { get; set; }
        public string Equity { get; set; }
        public string LongMarketValue { get; set; }
        public string ShortMarketValue { get; set; }
        public string Cash { get; set; }
        public string OpenTradeEquity { get; set; }
        public string AvailableLiquidity { get; set; }
        public string RequiredMargin { get; set; }
        public string LongAumPercentage { get; set; }
        public string ShortAumPercentage { get; set; }
    }
    public class AgreementCountEquityAssetValueCounterpartyExposureAttributes : IAgreementCountEquityAssetValueCounterpartyExposureAttributes
    {
        public CounterpartyCount NumberOfFinancingAgreements { get; set; }
        public string Equity { get; set; }
    }
    public class AgreementCountCounterpartyExposureAttributes : IAgreementCountCounterpartyExposureAttributes
    {
        public CounterpartyCount NumberOfFinancingAgreements { get; set; }
        public string Equity { get; set; }
        public string LongMarketValue { get; set; }
        public string ShortMarketValue { get; set; }
        public string Cash { get; set; }
        public string OpenTradeEquity { get; set; }
        public string AvailableLiquidity { get; set; }
        public string RequiredMargin { get; set; }
        public string LongAumPercentage { get; set; }
        public string ShortAumPercentage { get; set; }
    }
    public class CounterpartyNameCounterpartyExposureAttributes : ICounterpartyNameCounterpartyExposureAttributes
    {
        public CounterpartyName CounterpartyName { get; set; }
        public string Equity { get; set; }
        public string LongMarketValue { get; set; }
        public string ShortMarketValue { get; set; }
        public string Cash { get; set; }
        public string OpenTradeEquity { get; set; }
        public string AvailableLiquidity { get; set; }
        public string RequiredMargin { get; set; }
        public string LongAumPercentage { get; set; }
        public string ShortAumPercentage { get; set; }
    }
}

