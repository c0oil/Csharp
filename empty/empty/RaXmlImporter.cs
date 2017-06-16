

using System;
using System.Globalization;
using System.Xml;
using RaObjects.Objects;

namespace empty
{
    public class RaXmlImportGenerator
	{
  
        public static RiskAggregationData ImportRiskAggregationData(XmlReader reader, Action<RiskAggregationData> importAttributes)
        {
            RiskAggregationData result = new RiskAggregationData();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("fundAndInvestorDetails"))
				{
					result.FundAndInvestorDetails = ImportRaFundAndInvestorDetails(reader, null);
				}
				else if (reader.Name.Equals("equityExposure"))
				{
					result.EquityExposure = ImportRaEquityExposure(reader, null);
				}
				else if (reader.Name.Equals("sovereignInterestRateExposure"))
				{
					result.SovereignIntRateExp = ImportRaSovereignIntRateExp(reader, null);
				}
				else if (reader.Name.Equals("creditExposure"))
				{
					result.Credit = ImportRaCredit(reader, null);
				}
				else if (reader.Name.Equals("convertibleBondExposure"))
				{
					result.ConvertibleBondExposure = ImportRaConvertibleBondExposure(reader, null);
				}
				else if (reader.Name.Equals("currencyExposure"))
				{
					result.CurrencyExposure = ImportRaCurrencyExposure(reader, null);
				}
				else if (reader.Name.Equals("realAssetExposure"))
				{
					result.RealAssetsAndComExposure = ImportRaRealAssetsAndComExposure(reader, null);
				}
				else if (reader.Name.Equals("otherExposure"))
				{
					result.OtherExposure = ImportRaOtherExposure(reader, null);
				}
				else if (reader.Name.Equals("valueAtRisk"))
				{
					result.VaR = ImportRaVaR(reader, null);
				}
				else if (reader.Name.Equals("sensitivity"))
				{
					result.Sensivity = ImportRaSensivity(reader, null);
				}
				else if (reader.Name.Equals("stressTest"))
				{
					result.StressTest = ImportRaStressTest(reader, null);
				}
				else if (reader.Name.Equals("counterpartyExposure"))
				{
					result.CounterParty = ImportRaCounterParty(reader, null);
				}
            });
            return result;
        }
  
        public static RaFundAndInvestorDetails ImportRaFundAndInvestorDetails(XmlReader reader, Action<RaFundAndInvestorDetails> importAttributes)
        {
            RaFundAndInvestorDetails result = new RaFundAndInvestorDetails();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("fundName"))
				{
					result.FundName = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("date"))
				{
					result.Date = ImportSimpleTypes.ImportDateTimeNullable(reader);
				}
				else if (reader.Name.Equals("primaryInvestmentStrategy"))
				{
					result.PrimaryInvStrategy.Add(ImportSimpleTypes.ImportRaStringValue(reader));
				}
				else if (reader.Name.Equals("investorSize"))
				{
					ImportInvestorSize(reader, result, null);
				}
				else if (reader.Name.Equals("investorType"))
				{
					result.InvestorType.Add(ImportRaInvestorBreakdown(reader, null));
				}
				else if (reader.Name.Equals("investorLiquidity"))
				{
					result.InvestorLiquidity.Add(ImportRaInvestorLiquidity(reader, null));
				}
            });
            return result;
        }

		private static void ImportInvestorSize(XmlReader reader, RaFundAndInvestorDetails result, Action<RaFundAndInvestorDetails> importAttributes)
        {
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
			{
				if (reader.Name.Equals("topFiveLargestInvestors"))
				{
                    Action<RaInvestorBreakdown> importElementAttributes = (attribution) => ImportAumPercentageAssetValueAttributes(attribution, reader);
					result.TopInvestors = ImportRaInvestorBreakdown(reader, importElementAttributes);
				}
			});
		}
  
        public static RaInvestorBreakdown ImportRaInvestorBreakdown(XmlReader reader, Action<RaInvestorBreakdown> importAttributes)
        {
            RaInvestorBreakdown result = new RaInvestorBreakdown();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaInvestorBreakdown(reader, importAttributes));
            });
            return result;
        }
  
        public static RaInvestorLiquidity ImportRaInvestorLiquidity(XmlReader reader, Action<RaInvestorLiquidity> importAttributes)
        {
            RaInvestorLiquidity result = new RaInvestorLiquidity();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaInvestorLiquidity(reader, importAttributes));
            });
            return result;
        }
  
        public static RaEquityExposure ImportRaEquityExposure(XmlReader reader, Action<RaEquityExposure> importAttributes)
        {
            RaEquityExposure result = new RaEquityExposure();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfPositions"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortPositionAttributes(attribution, reader);
					result.AggregateNumberOfPositions = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("sectorExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Sectors = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Region = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("instrumentExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Instruments = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("marketCapitalExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.MarketCapitalExposure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItem(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaExposureItem ImportRaExposureItem(XmlReader reader, Action<RaExposureItem> importAttributes)
        {
            RaExposureItem result = new RaExposureItem();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaExposureItem(reader, importAttributes));
            });
            return result;
        }
  
        public static RaSovereignIntRateExp ImportRaSovereignIntRateExp(XmlReader reader, Action<RaSovereignIntRateExp> importAttributes)
        {
            RaSovereignIntRateExp result = new RaSovereignIntRateExp();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfPositions"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortPositionAttributes(attribution, reader);
					result.AggregateNumberOfPositions = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Region = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("maturityInstrumentExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.InstrumentsByMaturity = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("creditRatingExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.CreditRating = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItem(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaCredit ImportRaCredit(XmlReader reader, Action<RaCredit> importAttributes)
        {
            RaCredit result = new RaCredit();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfIssuers"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortIssuerAttributes(attribution, reader);
					result.AggregateNumberOfParentIssuers = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("sectorExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Sectors = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Region = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("creditTypeExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.CreditType = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("instrumentExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.CreditInstrument = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("priceAndYieldAndSpreadExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.PriceYieldAndSpread = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("creditRatingExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.CreditRating = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("maturityExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.MaturityBuckets = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("ownershipConcentrationExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.ConcentrationOfOwnership = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItemI(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaExposureItemI ImportRaExposureItemI(XmlReader reader, Action<RaExposureItemI> importAttributes)
        {
            RaExposureItemI result = new RaExposureItemI();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaExposureItemI(reader, importAttributes));
            });
            return result;
        }
  
        public static RaConvertibleBondExposure ImportRaConvertibleBondExposure(XmlReader reader, Action<RaConvertibleBondExposure> importAttributes)
        {
            RaConvertibleBondExposure result = new RaConvertibleBondExposure();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfIssuers"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortIssuerAttributes(attribution, reader);
					result.AggregateNumberOfParentIssuers = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("sectorExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Sectors = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Region = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("instrumentExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Instrument = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("derivativeSpecificExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Derivative = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("creditRatingExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.CreditRating = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("maturityExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.MaturityBuckets = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("ownershipConcentrationExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.ConcentrationOfOwnership = ImportRaExposureItemI(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItemI> importElementAttributes = (attribution) => ImportLongShortAumPercentageIssuerAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItemI(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaCurrencyExposure ImportRaCurrencyExposure(XmlReader reader, Action<RaCurrencyExposure> importAttributes)
        {
            RaCurrencyExposure result = new RaCurrencyExposure();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfPositions"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortPositionAttributes(attribution, reader);
					result.AggregateNumberOfParentIssuers = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionCurrencyExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.RegionalCurrencies = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("instrumentExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Instruments = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItem(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaRealAssetsAndComExposure ImportRaRealAssetsAndComExposure(XmlReader reader, Action<RaRealAssetsAndComExposure> importAttributes)
        {
            RaRealAssetsAndComExposure result = new RaRealAssetsAndComExposure();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalExposure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfPositions"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortPositionAttributes(attribution, reader);
					result.AggregateNumberOfParentIssuers = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Region = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("commodityTypeExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.CommodityType = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("commodityInstrumentExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.CommoditiesInstruments = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("realEstateExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.RealEstate = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("timberlandExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Timberland = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("infrastructureExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Infrastructure = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("liquidityExposure"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortAumPercentagePositionAttributes(attribution, reader);
					result.Liquidity = ImportRaExposureItem(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaOtherExposure ImportRaOtherExposure(XmlReader reader, Action<RaOtherExposure> importAttributes)
        {
            RaOtherExposure result = new RaOtherExposure();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("totalAssetValue"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAssetValueAttributes(attribution, reader);
					result.TotalInvestment = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalAumPercentage"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportNettedNonNettedLongShortAumPercentageAttributes(attribution, reader);
					result.PercentagePortfolio = ImportRaExposureItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("totalNumberOfPositions"))
				{
                    Action<RaExposureItem> importElementAttributes = (attribution) => ImportLongShortPositionAttributes(attribution, reader);
					result.AggregateNumberOfPositions = ImportRaExposureItem(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaVaR ImportRaVaR(XmlReader reader, Action<RaVaR> importAttributes)
        {
            RaVaR result = new RaVaR();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("calculationMethodology"))
				{
					ImportCalculationMethodology(reader, result, null);
				}
				else if (reader.Name.Equals("assetClassValueAtRisk"))
				{
                    Action<RaVaRItem> importElementAttributes = (attribution) => ImportValueAtRiskAttributes(attribution, reader);
					result.AssetClass = ImportRaVaRItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("regionValueAtRisk"))
				{
                    Action<RaVaRItem> importElementAttributes = (attribution) => ImportValueAtRiskAttributes(attribution, reader);
					result.Region = ImportRaVaRItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("sectorValueAtRisk"))
				{
                    Action<RaVaRItem> importElementAttributes = (attribution) => ImportValueAtRiskAttributes(attribution, reader);
					result.Sector = ImportRaVaRItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("exposureInCalculation"))
				{
					ImportPercentageOfExposureInCalculationAttributes(result, reader);
				}
            });
            return result;
        }

		private static void ImportCalculationMethodology(XmlReader reader, RaVaR result, Action<RaVaR> importAttributes)
        {
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
			{
				if (reader.Name.Equals("valueAtRiskMethodology"))
				{
					result.VaRMethodology = ImportSimpleTypes.ImportRaStringValue(reader);
				}
				else if (reader.Name.Equals("otherValueAtRiskMethodologyDetails"))
				{
					result.MethodologyDetails = ImportSimpleTypes.ImportRaStringValue(reader);
				}
				else if (reader.Name.Equals("decayFactor"))
				{
					ImportDecayFactorAttribute(result, reader);
				}
				else if (reader.Name.Equals("lookBackPeriod"))
				{
					ImportNumberOfDaysAttribute(result, reader);
				}
			});
		}
  
        public static RaVaRItem ImportRaVaRItem(XmlReader reader, Action<RaVaRItem> importAttributes)
        {
            RaVaRItem result = new RaVaRItem();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaVaRItem(reader, importAttributes));
            });
            return result;
        }
  
        public static RaSensivity ImportRaSensivity(XmlReader reader, Action<RaSensivity> importAttributes)
        {
            RaSensivity result = new RaSensivity();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("equity"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("sovereignInterestRate"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("credit"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("convertibleBond"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("currency"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("realAsset"))
				{
                    Action<RaSensivityItem> importElementAttributes = (attribution) => ImportSensitivityAttributes(attribution, reader);
					result.Sensivities.Add(ImportRaSensivityItem(reader, importElementAttributes));
				}
				else if (reader.Name.Equals("exposureInCalculation"))
				{
					ImportPercentageOfExposureInCalculationAttributes(result, reader);
				}
            });
            return result;
        }
  
        public static RaSensivityItem ImportRaSensivityItem(XmlReader reader, Action<RaSensivityItem> importAttributes)
        {
            RaSensivityItem result = new RaSensivityItem();
			importAttributes?.Invoke(result);
            return result;
        }
  
        public static RaStressTest ImportRaStressTest(XmlReader reader, Action<RaStressTest> importAttributes)
        {
            RaStressTest result = new RaStressTest();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("scenarios"))
				{
                    Action<RaStressTestItem> importElementAttributes = (attribution) => ImportStressTestAttributes(attribution, reader);
					result.Scenarios = ImportRaStressTestItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("historicalStressEvents"))
				{
                    Action<RaStressTestItem> importElementAttributes = (attribution) => ImportStressTestAttributes(attribution, reader);
					result.HistoricalStress = ImportRaStressTestItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("historicalStress"))
				{
                    Action<RaStressTestItemD> importElementAttributes = (attribution) => ImportHistoricalStressTestAttributes(attribution, reader);
					result.HistoricalPortfolioStress = ImportRaStressTestItemD(reader, importElementAttributes);
				}
            });
            return result;
        }
  
        public static RaStressTestItem ImportRaStressTestItem(XmlReader reader, Action<RaStressTestItem> importAttributes)
        {
            RaStressTestItem result = new RaStressTestItem();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaStressTestItem(reader, importAttributes));
            });
            return result;
        }
  
        public static RaStressTestItemD ImportRaStressTestItemD(XmlReader reader, Action<RaStressTestItemD> importAttributes)
        {
            RaStressTestItemD result = new RaStressTestItemD();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaStressTestItemD(reader, importAttributes));
            });
            return result;
        }
  
        public static RaCounterParty ImportRaCounterParty(XmlReader reader, Action<RaCounterParty> importAttributes)
        {
            RaCounterParty result = new RaCounterParty();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("tradingAssets"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyCountCounterpartyExposureAttributes(attribution, reader);
					result.TradingAssets = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("treasuryAssets"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyCountCounterpartyExposureAttributes(attribution, reader);
					result.TreasureAssets = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("otherAssets"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyCountCounterpartyExposureAttributes(attribution, reader);
					result.OtherAssets = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("tradingAssetsByCounterpartyAssetSize"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyNameCounterpartyExposureAttributes(attribution, reader);
					result.TradingAssetsBySize = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("tradingAssetsByCounterpartyRegion"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportAgreementCountCounterpartyExposureAttributes(attribution, reader);
					result.TradingAssetsByGeography = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("treasuryAssetsByCounterpartyRegion"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyCountCounterpartyExposureAttributes(attribution, reader);
					result.TreasureAssetsByGeography = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("otherAssetsByCounterpartyRegion"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportCounterpartyCountCounterpartyExposureAttributes(attribution, reader);
					result.OtherAssetsByGeography = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("tradingAssetsByLockupProfile"))
				{
                    Action<RaCounterPartyItem> importElementAttributes = (attribution) => ImportAgreementCountCounterpartyExposureAttributes(attribution, reader);
					result.TradingAssetsByLockup = ImportRaCounterPartyItem(reader, importElementAttributes);
				}
				else if (reader.Name.Equals("counterpartyAndFinancingSources"))
				{
					ImportCounterpartyAndFinancingSources(reader, result, null);
				}
            });
            return result;
        }
  
        public static RaCounterPartyItem ImportRaCounterPartyItem(XmlReader reader, Action<RaCounterPartyItem> importAttributes)
        {
            RaCounterPartyItem result = new RaCounterPartyItem();
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaCounterPartyItem(reader, importAttributes));
            });
            return result;
        }

		private static void ImportCounterpartyAndFinancingSources(XmlReader reader, RaCounterParty result, Action<RaCounterParty> importAttributes)
        {
			importAttributes?.Invoke(result);
			ForEachSubItems(reader, () =>
			{
				if (reader.Name.Equals("unregulatedPercentageOfNetCreditCounterpartyExposure"))
				{
					result.PercentAggNetCreditCpExp = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("uncollateralizedPercentageOfFinancing"))
				{
					result.PercentFinancingUncoll = ImportSimpleTypes.ImportString(reader);
				}
			});
		}
 
		private static void ImportAumPercentageAssetValueAttributes(RaInvestorBreakdown result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("aumPercentage"))
				{
					result.PercentAge = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("assetValue"))
				{
					result.Amount = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportNettedNonNettedLongShortAssetValueAttributes(RaExposureItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAssetValue"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAssetValue"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAssetValue"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAssetValue"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportNettedNonNettedLongShortAumPercentageAttributes(RaExposureItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAumPercentage"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAumPercentage"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAumPercentage"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAumPercentage"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportLongShortPositionAttributes(RaExposureItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("numberOfLongPositions"))
				{
					result.NumberOfLongPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
				else if (reader.Name.Equals("numberOfShortPositions"))
				{
					result.NumberOfShortPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportLongShortAumPercentagePositionAttributes(RaExposureItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAumPercentage"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAumPercentage"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAumPercentage"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAumPercentage"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("numberOfLongPositions"))
				{
					result.NumberOfLongPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
				else if (reader.Name.Equals("numberOfShortPositions"))
				{
					result.NumberOfShortPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportNettedNonNettedLongShortAssetValueAttributes(RaExposureItemI result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAssetValue"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAssetValue"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAssetValue"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAssetValue"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportNettedNonNettedLongShortAumPercentageAttributes(RaExposureItemI result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAumPercentage"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAumPercentage"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAumPercentage"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAumPercentage"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportLongShortIssuerAttributes(RaExposureItemI result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("numberOfLongIssuers"))
				{
					result.NumberOfLongIssuers = ImportSimpleTypes.ImportLongNullable(reader);
				}
				else if (reader.Name.Equals("numberOfShortIssuers"))
				{
					result.NumberOfShortIssuers = ImportSimpleTypes.ImportLongNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportLongShortAumPercentageIssuerAttributes(RaExposureItemI result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("nonNettedShortAumPercentage"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAumPercentage"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAumPercentage"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAumPercentage"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("numberOfLongIssuers"))
				{
					result.NumberOfLongIssuers = ImportSimpleTypes.ImportLongNullable(reader);
				}
				else if (reader.Name.Equals("numberOfShortIssuers"))
				{
					result.NumberOfShortIssuers = ImportSimpleTypes.ImportLongNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportValueAtRiskAttributes(RaVaRItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("exposurePercentage"))
				{
					result.PercentExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("varPercentage"))
				{
					result.VaR = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("cVarPercentage"))
				{
					result.CvaR = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportPercentageOfExposureInCalculationAttributes(RaVaR result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("percentageOfLongExposureInCalculation"))
				{
					result.LongExposureIncluded = ImportSimpleTypes.ImportRaDoubleValue(reader);
				}
				else if (reader.Name.Equals("percentageOfShortExposureInCalculation"))
				{
					result.ShortExposureIncluded = ImportSimpleTypes.ImportRaDoubleValue(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportDecayFactorAttribute(RaVaR result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("decayFactor"))
				{
					result.DecayFactor = ImportSimpleTypes.ImportRaDoubleValue(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportNumberOfDaysAttribute(RaVaR result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("numberOfDays"))
				{
					result.LookbackPeriod = ImportSimpleTypes.ImportRaIntValue(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportSensitivityAttributes(RaSensivityItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("beta"))
				{
					result.Beta = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("delta"))
				{
					result.Delta = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("gamma"))
				{
					result.Gamma = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("vega"))
				{
					result.Vega = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("theta"))
				{
					result.Theta = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("cs01"))
				{
					result.Cs01 = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("dv01"))
				{
					result.Dv01 = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportPercentageOfExposureInCalculationAttributes(RaSensivity result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("percentageOfLongExposureInCalculation"))
				{
					result.LongExposureIncluded = ImportSimpleTypes.ImportRaDoubleValue(reader);
				}
				else if (reader.Name.Equals("percentageOfShortExposureInCalculation"))
				{
					result.ShortExposureIncluded = ImportSimpleTypes.ImportRaDoubleValue(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportStressTestAttributes(RaStressTestItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("portfolioReturnPercentage"))
				{
					result.PortfolioReturn = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("percentageOfLongExposureInCalculation"))
				{
					result.PercentLongExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("percentageOfShortExposureInCalculation"))
				{
					result.PercentShortExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportHistoricalStressTestAttributes(RaStressTestItemD result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("portfolioReturnPercentage"))
				{
					result.PortfolioReturn = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("percentageOfLongExposureInCalculation"))
				{
					result.PercentLongExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("percentageOfShortExposureInCalculation"))
				{
					result.PercentShortExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("startDate"))
				{
					result.StartDate = ImportSimpleTypes.ImportDateTimeNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportCounterpartyCountCounterpartyExposureAttributes(RaCounterPartyItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("numberOfCustodiansAndCounterparties"))
				{
					result.Number = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("equity"))
				{
					result.Equity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longMarketValue"))
				{
					result.Lmv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortMarketValue"))
				{
					result.Smv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("cash"))
				{
					result.Cash = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("openTradeEquity"))
				{
					result.OteMmt = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("availableLiquidity"))
				{
					result.AvailableLiquidity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("requiredMargin"))
				{
					result.RequiredMargin = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longAumPercentage"))
				{
					result.LongAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortAumPercentage"))
				{
					result.ShortAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportCounterpartyNameCounterpartyExposureAttributes(RaCounterPartyItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("counterpartyName"))
				{
					result.Number = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("equity"))
				{
					result.Equity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longMarketValue"))
				{
					result.Lmv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortMarketValue"))
				{
					result.Smv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("cash"))
				{
					result.Cash = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("openTradeEquity"))
				{
					result.OteMmt = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("availableLiquidity"))
				{
					result.AvailableLiquidity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("requiredMargin"))
				{
					result.RequiredMargin = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longAumPercentage"))
				{
					result.LongAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortAumPercentage"))
				{
					result.ShortAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
 
		private static void ImportAgreementCountCounterpartyExposureAttributes(RaCounterPartyItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
				return;

			do
			{
				if (reader.Name.Equals("numberOfFinancingAgreements"))
				{
					result.Number = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("equity"))
				{
					result.Equity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longMarketValue"))
				{
					result.Lmv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortMarketValue"))
				{
					result.Smv = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("cash"))
				{
					result.Cash = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("openTradeEquity"))
				{
					result.OteMmt = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("availableLiquidity"))
				{
					result.AvailableLiquidity = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("requiredMargin"))
				{
					result.RequiredMargin = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("longAumPercentage"))
				{
					result.LongAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("shortAumPercentage"))
				{
					result.ShortAumExposurePercent = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
		}
				
        private static void ForEachSubItems(XmlReader reader, Action doAction)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }

            reader.Read();
            int depth = reader.Depth;
            do
            {
                if (reader.Depth == depth && reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
                {
                    doAction();
                }
            } while (reader.Read() && reader.Depth >= depth);
        }
		
		private static class ImportSimpleTypes
		{
			public static DateTime? ImportDateTimeNullable(XmlReader reader)
            {
                string value = ReadContent(reader);
				DateTime parsedValue;
				return string.IsNullOrEmpty(value) || !DateTime.TryParse(value, out parsedValue) ? (DateTime?)null : parsedValue;
			}

			public static long? ImportLongNullable(XmlReader reader)
            {
                double? doubleValue = ImportDoubleNullable(reader);
				return doubleValue == null ? (Int64?) null : (Int64) Math.Round(doubleValue.Value, MidpointRounding.AwayFromZero);
			}

			public static double? ImportDoubleNullable(XmlReader reader)
            {
                string value = ReadContent(reader);
				double parsedValue;
				return string.IsNullOrEmpty(value) || !double.TryParse(value, out parsedValue) ? (double?)null : parsedValue;
			}

			public static string ImportString(XmlReader reader)
			{
                return ReadContent(reader);
			}

		    private static string ReadContent(XmlReader reader)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    return reader.ReadElementContentAsString();
                }
                return reader.Value;
            }

			public static RaDoubleValue ImportRaDoubleValue(XmlReader reader)
			{
				return new RaDoubleValue { Value = ImportDoubleNullable(reader) };
			}

			public static RaStringValue ImportRaStringValue(XmlReader reader)
			{
				return new RaStringValue { Value = ImportString(reader) };
			}

			public static RaIntValue ImportRaIntValue(XmlReader reader)
			{
				return new RaIntValue { Value = ImportLongNullable(reader) };
			}
		}
	}
}
