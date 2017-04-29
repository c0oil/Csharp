using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RaObjects.Objects
{
    public static class ServiceInfo
    {
        public const string Namespace = "http://aaa.com/";
    }

    public class RiskAggregationTemplateUpdater
    {
        private readonly List<Action<RiskAggregationData>> updateList = new List<Action<RiskAggregationData>>
        {
            U1,     // from 2011 to 2013
            U1_2,   // from 2013 to 2016
        };

        public void Update(RiskAggregationData data)
        {
            CorrectIds(data);
            foreach (Action<RiskAggregationData> update in updateList)
            {
                update(data);
            }
        }

        #region Manualy correct and initialize ids for first version ra template in db

        private void CorrectIds(RiskAggregationData data)
        {
            if (data.IdsImported)
            {
                CorrectImportedIds(data);
            }
            else
            {
                ManualyInitIds(data);
                ManualyCorrectExposureIds(data);
            }
            CheckIds(data);
        }

        private void CheckIds(IIdNavigate item)
        {
            if (string.IsNullOrEmpty(item.Id?.Id))
            {
                Debug.WriteLine($"No id: {item.Name}");
            }

            if (item.SubItems != null)
            {
                foreach (IIdNavigate subItem in item.SubItems)
                {
                    CheckIds(subItem);
                }
            }
        }

        private void CorrectImportedIds(RiskAggregationData item)
        {
            item.GetById("9").ChangeId("10.8", "9.8");
            item.GetById("11.8").ChangeId("", "11.8.1");
        }
        
        private void ManualyInitIds(IIdNavigate item, string parentId = null)
        {
            if (item.SubItems == null)
            {
                return;
            }

            int subItemId = 1; 
            int incSubItemId = 1; 
            int skipSubItem = 0;
            CorrectInitIds(ref subItemId, ref skipSubItem, ref incSubItemId, ref parentId);

            for (int i = skipSubItem; i < item.SubItems.Count(); i++)
            {
                IIdNavigate subItem = item.SubItems.ElementAt(i);
                if (subItem.Id == null || subItem.Id.IsEmpty())
                {
                    subItem.Id = parentId == null ? $"{subItemId}" : $"{parentId}.{subItemId}";
                }
                subItemId += incSubItemId;
                ManualyInitIds(subItem, subItem.Id.Id);
            }
        }

        private void ManualyCorrectExposureIds(RiskAggregationData item)
        {
            var exposureIds = new List<int> { 2, 3 ,4 ,5 ,6 ,7, 12 };
            foreach (int exposureId in exposureIds)
            {
                for (int i = 1; i <= 3; i++)
                {
                    item.GetById($"{exposureId}.{i}").Id = $"{exposureId}.{i}.{1}";
                }
            }
        }

        private void CorrectInitIds(ref int firstSubItemId, ref int skipSubItem, ref int incSubItemId, ref string parentId)
        {
            if (parentId == "1.6")
            {
                incSubItemId = 4;
            }
            else if (parentId == "8")
            {
                firstSubItemId = 5; // first subitem is 8.5 in "8.VaR"
            }
            else if (parentId == "5.7.6" || parentId == "5.7.5")
            {
                skipSubItem = 1; // skip wrong imported item "Exposure to bonds with market value:"
            }
            else if (parentId == "9")
            {
                firstSubItemId = 8; // first subitem is 9.8 in "9.Sensetivity"
            }
            else if (parentId != null && parentId.StartsWith("11") && parentId.Split('.').Length == 3)
            {
                parentId += ".1"; // no 2 grade in "11.Counter Party"
            }
        }

        #endregion

        private static void U1(RiskAggregationData data)
        {
            IIdNavigate averagePortfolioDelta = data.GetById("5.7.6");
            if (averagePortfolioDelta != null)
            {
                averagePortfolioDelta.Rename("5.7.6.1", "Less than 25%");
                averagePortfolioDelta.Rename("5.7.6.2", "Greater than or equal to 25% and less than 50%.");
                averagePortfolioDelta.Rename("5.7.6.3", "Greater than or equal to 50% and less than 75%.");
                averagePortfolioDelta.Rename("5.7.6.4", "Greater than or equal to 75%.");
            }
            data.Sensivity.SensivityCaption = "9.8 Asset Class";
            data.Rename("9.8", "9.8 Asset Class"); // color cell don't import
            data.Rename("10.2.1.2", "September 11th  (10th to 17th September 2001)");
        }

        private static void U1_2(RiskAggregationData data)
        {
            // 1p. rename tables. update in all

            data.Rename("1.7", "1.7 Investor Breakdown");
            data.Rename("2.8", "2.8 Instrument Liquidity");
            data.Rename("3.7", "3.7 Instrument Liquidity");
            data.Rename("4.12", "4.12 Instrument Liquidity");
            data.Rename("5.11", "5.11 Instrument Liquidity");
            data.Rename("6.4", "6.4 Regional currencies (FX and non FX Instruments)");
            data.Rename("6.5", "6.5 Instruments (Non Base Currency Leg of FX Instruments Only)");
            data.Rename("6.6", "6.6 Instrument Liquidity (Non Base Currency Leg of FX Instruments Only)");
            data.Rename("7.10", "7.10 Instrument Liquidity");

            // 2p. rename grade 1,2,3. aggregate with new name

            // 3p. new row. show in new template

            // 4p. remove row. show in new template

            // 6p. move block. historical and portf: for old sector - only old, for new sector - both.
            
            MoveSector<RaExposureItem>(data, "2");
            MoveSector<RaExposureItemI>(data, "4");
            MoveSector<RaExposureItemI>(data, "5");
        }

        private static void MoveSector<T>(RiskAggregationData data, string id)
            where T : class, IMovedSubItems<T>, IIdNavigate, new()
        {
            string oldId = $"{id}.4.6";
            string newId = $"{id}.4.14";
            if (data.GetById($"{oldId}.7") == null || data.GetById($"{oldId}.8") == null)
                return;

            int version = 2;
            data.AddNewMovedItem<T>(newId, "Real Estate");
            data.Move<T>($"{oldId}.7", $"{newId}.1", version);
            data.Move<T>($"{oldId}.7.1", $"{newId}.1.1", version);
            data.Move<T>($"{oldId}.7.2", $"{newId}.1.2", version);
            data.Move<T>($"{oldId}.7.4", $"{newId}.1.4", version);
            data.Move<T>($"{oldId}.7.6", $"{newId}.1.5", version);
            data.Move<T>($"{oldId}.7.7", $"{newId}.1.6", version);
            data.Move<T>($"{oldId}.7.8", $"{newId}.1.7", version);
            data.MoveWithSubItems<T>($"{oldId}.8", $"{newId}.2", version);
        }

        public static void SupportOldTemplates(RiskAggregationData data)
        {
            var updater = new RiskAggregationTemplateUpdater();
            updater.Update(data);
        }

        public static int GetVersion(string version)
        {
            if (version == "This Version August 2016")
            {
                return (int) TemplateVersions.V2016;
            }
            return (int)TemplateVersions.V2011Or2013;
        }

        public enum TemplateVersions
        {
            V2011Or2013 = 0,
            V2016 = 2,
        }
    }
}