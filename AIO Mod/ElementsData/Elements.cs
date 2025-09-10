using AIO_Mod.Utils;
using Klei.AI;
using System.Collections.Generic;
using UnityEngine;

namespace AIO_Mod.ElementsData
{
    public class Elements
    {
        //public static ElementGrouping PlasticGroup;
        public static readonly Color32 LOWGRADESAND_COLOR = new Color32((byte)59, (byte)46, (byte)12, byte.MaxValue);
        public static readonly Color32 ARGENTITE_COLOR = new Color32((byte)140, (byte)131, (byte)104, byte.MaxValue);
        public static readonly Color32 AURICHALCITE_COLOR = new Color32((byte)162, (byte)235, (byte)187, byte.MaxValue);
        public static readonly Color32 BORAX_COLOR = new Color32((byte)245, (byte)241, (byte)211, byte.MaxValue);
        public static readonly Color32 ISOPROPANE_COLOR = new Color32((byte)167, (byte)151, (byte)232, byte.MaxValue);
        public static readonly Color32 SILVER_COLOR = new Color32((byte)107, (byte)117, (byte)125, byte.MaxValue);
        public static readonly Color32 SOURWATER_COLOR = new Color32((byte)130, (byte)104, (byte)65, byte.MaxValue);

        public static readonly Color32 LITHIUMBRINE_COLOR = new Color32((byte)107, (byte)117, (byte)125, byte.MaxValue);

        //public static ElementInfo LowGradeSand_Solid = ElementInfo.Solid("LowGradeSand", "solid_lowgradesand_kanim", LOWGRADESAND_COLOR);
        //public static ElementInfo Argentite_Solid = ElementInfo.Solid("ArgentiteOre", "raw_silverore_kanim", ARGENTITE_COLOR);
        //public static ElementInfo Aurichalcite_Solid = ElementInfo.Solid("AurichalciteOre", "raw_zincore_kanim", AURICHALCITE_COLOR);
        public static ElementInfo Borax_Solid = ElementInfo.Solid("SolidBorax", "solid_borax_kanim", BORAX_COLOR);
        public static ElementInfo Isopropane_Gas = ElementInfo.Gas("IsopropaneGas", ISOPROPANE_COLOR);
        //public static ElementInfo Silver_Solid = ElementInfo.Solid("SolidSilver", "solid_silver_kanim", SILVER_COLOR);
        public static ElementInfo SourWater_Liquid = ElementInfo.Liquid("SourWater", SOURWATER_COLOR);

        public static ElementInfo LithiumBrine = ElementInfo.Liquid("LithiumBrine", LITHIUMBRINE_COLOR);


        private static void SetAtmosphere(SimHashes element, Rottable.RotAtmosphereQuality quality)
        {
            Rottable.AtmosphereModifier.Add((int)element, quality);
        }

        public static void RegisterSubstances(List<Substance> list)
        {
            HashSet<Substance> collection = new HashSet<Substance>()
            {
                //Elements.LowGradeSand_Solid.CreateSubstanceFromElementTinted((SimHashes) (-1736594426)),
                //Elements.Argentite_Solid.CreateSubstanceFromElementTinted((SimHashes) 28407099, SILVER_COLOR), // new Color?(Elements.SILVER_COLOR)
                //Elements.Aurichalcite_Solid.CreateSubstanceFromElementTinted((SimHashes) (-755153220)),
                Elements.Borax_Solid.CreateSubstanceFromElementTinted((SimHashes)83003332),
                Elements.Isopropane_Gas.CreateSubstance(),
                //Elements.Silver_Solid.CreateSubstanceFromElementTinted((SimHashes) (-279785280)),
                Elements.SourWater_Liquid.CreateSubstance(),

                Elements.LithiumBrine.CreateSubstance(),

            };

            list.AddRange((IEnumerable<Substance>)collection);
            //Elements.SetElementRottables();
        }


        public static void AddElementOverheatModifier(SimHashes element, float degreeIncrease)
        {
            Element elementByHash = ElementLoader.FindElementByHash(element);
            if (elementByHash == null)
                return;
            AttributeModifier attributeModifier = new AttributeModifier(Db.Get().BuildingAttributes.OverheatTemperature.Id, degreeIncrease, elementByHash.name, false, false, true);
            elementByHash.attributeModifiers.Add(attributeModifier);
        }

        public static void AddElementDecorModifier(SimHashes element, float decorBonusMultiplier)
        {
            Element elementByHash = ElementLoader.FindElementByHash(element);
            if (elementByHash == null)
                return;
            AttributeModifier attributeModifier = new AttributeModifier("Decor", decorBonusMultiplier, elementByHash.name, true, false, true);
            elementByHash.attributeModifiers.Add(attributeModifier);
        }

        //internal static void ConfigureElements()
        //{
        //    AddElementOverheatModifier((SimHashes)Elements.Borax_Solid, 100f);
        //    //AddElementDecorModifier((SimHashes)Elements.Borax_Solid, -0.25f);
        //}

        public static void FinalizeElements()
        {
            foreach (var info in ElementUtil.elements)
            {
                var element = ElementLoader.FindElementByHash(info.SimHash);
                if (element == null)
                {
                    Debug.LogError($"[AIO Mod] Element {info.id} with hash {info.SimHash} failed to register!");
                }
                else
                {
                    Debug.Log($"[AIO Mod] Element {info.id} finalized successfully.");
                }
            }
        }
    }
}
