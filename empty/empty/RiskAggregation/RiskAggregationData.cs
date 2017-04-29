using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using AlternativeSoft.Common;
using AlternativeSoft.Common.Utils;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RiskAggregationData : RaIdNavigate
    {
        public string FundName => FundAndInvestorDetails.FundName;

        [DataMember]
        public bool IdsImported { get; set; }

        [DataMember]
        public int TemplateVersion { get; set; }

        /// <summary>
        /// 1. Fund and Investor Details
        /// </summary>
        [DataMember]
        public RaFundAndInvestorDetails FundAndInvestorDetails;

        /// <summary>
        /// 2. Equity Exposure
        /// </summary>
        [DataMember]
        public RaEquityExposure EquityExposure;

        /// <summary>
        /// 3. Sovereign & Int Rate Exp
        /// </summary>
        [DataMember]
        public RaSovereignIntRateExp SovereignIntRateExp;

        /// <summary>
        /// 4. Credit (x Convertible Bonds)
        /// </summary>
        [DataMember]
        public RaCredit Credit;

        /// <summary>
        /// 5. Convertible Bond Exposure
        /// </summary>
        [DataMember]
        public RaConvertibleBondExposure ConvertibleBondExposure;
        
        /// <summary>
        /// 6. Currency Exposure
        /// </summary>
        [DataMember]
        public RaCurrencyExposure CurrencyExposure;

        /// <summary>
        /// 7. Real Ass. & Commodities Exp.
        /// </summary>
        [DataMember]
        public RaRealAssetsAndComExposure RealAssetsAndComExposure;

        /// <summary>
        /// 8. VaR
        /// </summary>
        [DataMember]
        public RaVaR VaR;

        /// <summary>
        /// 9. Sensivity
        /// </summary>
        [DataMember]
        public RaSensivity Sensivity;

        /// <summary>
        /// 10. Stress Test
        /// </summary>
        [DataMember]
        public RaStressTest StressTest;

        /// <summary>
        /// 11. Counter Party
        /// </summary>
        [DataMember]
        public RaCounterParty CounterParty;

        /// <summary>
        /// 12. Other
        /// </summary>
        [DataMember]
        public RaOtherExposure OtherExposure;

        /// <summary>
        /// Drop Down Lists
        /// </summary>
        [DataMember]
        public RaDropDowns DropDowns;

        public RiskAggregationData()
        {
            FundAndInvestorDetails = new RaFundAndInvestorDetails();
            EquityExposure = new RaEquityExposure();
            SovereignIntRateExp = new RaSovereignIntRateExp();
            Credit = new RaCredit();
            ConvertibleBondExposure = new RaConvertibleBondExposure();
            CurrencyExposure = new RaCurrencyExposure();
            RealAssetsAndComExposure = new RaRealAssetsAndComExposure();
            VaR = new RaVaR();
            Sensivity = new RaSensivity();
            StressTest = new RaStressTest();
            CounterParty = new RaCounterParty();
            OtherExposure = new RaOtherExposure();
            DropDowns = new RaDropDowns();
        }
        
        protected override void OnDeserialization()
        {
            subItems = new List<RaIdNavigate>
            {
                FundAndInvestorDetails,
                EquityExposure,
                SovereignIntRateExp,
                Credit,
                ConvertibleBondExposure,
                CurrencyExposure,
                RealAssetsAndComExposure,
                VaR,
                Sensivity,
                StressTest,
                CounterParty,
                OtherExposure,
            };

            foreach (RaIdNavigate item in subItems.OfType<RaIdNavigate>())
            {
                item.OnDeserialization(null);
            }
        }

        protected string SerializeXml()
        {
            XmlSerializer xs = new XmlSerializer(GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xs.Serialize(memoryStream, this);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        protected byte[] SerializeXmlBytes()
        {
            XmlSerializer xs = new XmlSerializer(GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xs.Serialize(memoryStream, this);
                return memoryStream.ToArray();
            }
        }

        public byte[] SerializeXmlCompressed()
        {
            byte[] xml = SerializeXmlBytes();
            return GzipUtils.GetCompressedContent(xml);
        }

        protected static RiskAggregationData DeserializeXml(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(RiskAggregationData));
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                var riskAggregationData = (RiskAggregationData)xs.Deserialize(memoryStream);
                riskAggregationData.OnDeserialization();
                RiskAggregationTemplateUpdater.SupportOldTemplates(riskAggregationData);
                return riskAggregationData;
            }
        }

        public static RiskAggregationData DeserializeXmlCompressed(byte[] data)
        {
            byte[] decompressedXml = GzipUtils.DecompressContent(data);
            XmlSerializer xs = new XmlSerializer(typeof(RiskAggregationData));
            using (MemoryStream memoryStream = new MemoryStream(decompressedXml))
            {
                var riskAggregationData = (RiskAggregationData)xs.Deserialize(memoryStream);
                riskAggregationData.OnDeserialization();
                RiskAggregationTemplateUpdater.SupportOldTemplates(riskAggregationData);
                return riskAggregationData;
            }
        }
    }
}