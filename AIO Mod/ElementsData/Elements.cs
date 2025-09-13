using AIO_Mod.Utils;
using Klei.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static STRINGS.ELEMENTS;
using static STRINGS.UI.UISIDESCREENS;

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
        public static readonly Color32 RAWLITHIUM_COLOR = new Color32(10, 20, 30, byte.MaxValue);

        public static readonly Color32 LITHIUMBRINE_COLOR = new Color32((byte)107, (byte)117, (byte)125, byte.MaxValue);

        //public static ElementInfo LowGradeSand_Solid = ElementInfo.Solid("LowGradeSand", "solid_lowgradesand_kanim", LOWGRADESAND_COLOR);
        //public static ElementInfo Argentite_Solid = ElementInfo.Solid("ArgentiteOre", "raw_silverore_kanim", ARGENTITE_COLOR);
        //public static ElementInfo Aurichalcite_Solid = ElementInfo.Solid("AurichalciteOre", "raw_zincore_kanim", AURICHALCITE_COLOR);
        public static ElementInfo Borax_Solid = ElementInfo.Solid("SolidBorax", "solid_borax_kanim", BORAX_COLOR);
        public static ElementInfo Isopropane_Gas = ElementInfo.Gas("IsopropaneGas", ISOPROPANE_COLOR);
        //public static ElementInfo Silver_Solid = ElementInfo.Solid("SolidSilver", "solid_silver_kanim", SILVER_COLOR);
        public static ElementInfo SourWater_Liquid = ElementInfo.Liquid("SourWater", RAWLITHIUM_COLOR);
        //public static ElementInfo RawLithium = ElementInfo.Solid("RawLithium", "raw_lithium_kanim", RAWLITHIUM_COLOR);
        public static ElementInfo LithiumBrine = ElementInfo.Liquid("LithiumBrine", LITHIUMBRINE_COLOR);
        public static ElementInfo UnobtaniumAlloy = ElementInfo.Solid("UnobtaniumAlloy", RAWLITHIUM_COLOR);


        private static void SetAtmosphere(SimHashes element, Rottable.RotAtmosphereQuality quality)
        {
            Rottable.AtmosphereModifier.Add((int)element, quality);
        }

        public static void RegisterSubstances(List<Substance> list)
        {
            Material material = list.Find((Predicate<Substance>)(e => e.elementID == SimHashes.Diamond)).material;
            HashSet<Substance> collection = new HashSet<Substance>()
            {
                //Elements.LowGradeSand_Solid.CreateSubstanceFromElementTinted((SimHashes) (-1736594426)),
                //Elements.Argentite_Solid.CreateSubstanceFromElementTinted((SimHashes) 28407099, SILVER_COLOR), // new Color?(Elements.SILVER_COLOR)
                //Elements.Aurichalcite_Solid.CreateSubstanceFromElementTinted((SimHashes) (-755153220)),
                Elements.Borax_Solid.CreateSubstanceFromElementTinted((SimHashes)83003332),
                Elements.Isopropane_Gas.CreateSubstance(),
                //Elements.Silver_Solid.CreateSubstanceFromElementTinted((SimHashes) (-279785280)),
                Elements.SourWater_Liquid.CreateSubstance(),
                //Elements.RawLithium.CreateSubstance(false, material),
                Elements.LithiumBrine.CreateSubstance(),
                Elements.UnobtaniumAlloy.CreateSubstance(false, material)

            };

            list.AddRange((IEnumerable<Substance>)collection);
            //Elements.SetElementRottables();
        }

        public static void ClearReenabledVanillaElementCodexTags(ref List<ElementLoader.ElementEntry> elementList)
        {
            HashSet<string> ToUnhide = new HashSet<string>();
            UnhideElement(SimHashes.Electrum); // Electrum
            UnhideElement(SimHashes.CrushedRock); // CrushedRock

            //UnhideElement((SimHashes) 1102028305); // Syngas
            //UnhideElement((SimHashes) 660593444); // MoltenSyngas
            UnhideElement(SimHashes.SolidPropane); // SolidSyngas
            UnhideElement(SimHashes.Propane); // Propane
            UnhideElement(SimHashes.LiquidPropane); // LiquidPropane
            //UnhideElement((SimHashes)166493482); // SolidPropane
            //UnhideElement((SimHashes)1433229102); // Bitumen
            //UnhideElement((SimHashes) - 1901832310); // PhosphateNodules
            //UnhideElement((SimHashes) - 2070223827); // Aerogel
            //UnhideElement((SimHashes)118518245); // CarbonFibre
            //UnhideElement((SimHashes)2059777261); // FoolsGold
            //UnhideElement((SimHashes) - 1038746460); // SandCement
            //UnhideElement((SimHashes) - 47820500); // Radium
            //UnhideElement((SimHashes) - 1624413844); // Yellowcake
            //UnhideElement((SimHashes) - 325269471); // Brick
            //UnhideElement((SimHashes)1627140480); // Cement

            foreach (ElementLoader.ElementEntry elementEntry in elementList)
            {
                if (ToUnhide.Contains(elementEntry.elementId) && elementEntry.tags != null)
                {
                    List<string> list = (elementEntry.tags).ToList<string>();
                    list.Remove(GameTags.HideFromCodex.ToString());
                    list.Remove(GameTags.HideFromSpawnTool.ToString());
                    elementEntry.tags = list.ToArray();
                }
            }

            void UnhideElement(SimHashes element) => ToUnhide.Add(element.ToString());
        }

        internal static void ModifyExistingElements()
        {
            //Elements.PlasticGroup = ElementGrouping.GroupAllWith(GameTags.Plastic);

            //Elements.AddTagToElementAndEnable((SimHashes) - 1046145888, new Tag?(ModAssets.Tags.AIO_CarrierGas)); // Hydrogen
            //Elements.AddTagToElementAndEnable((SimHashes) - 1554872654, new Tag?(ModAssets.Tags.AIO_CarrierGas)); // Helium

            //Elements.AddTagToElementAndEnable(SimHashes.Syngas, new Tag?(GameTags.CombustibleGas)); //Syngas
            Elements.AddTagToElementAndEnable(SimHashes.Propane, new Tag?(GameTags.CombustibleGas)); // Propane
            Elements.AddTagToElementAndEnable((SimHashes) Isopropane_Gas, new Tag?(GameTags.CombustibleGas)); // Isopropane
            Elements.AddTagToElementAndEnable(SimHashes.LiquidPropane); // LiquidPropane
            Elements.AddTagToElementAndEnable(SimHashes.SolidPropane); // SolidPropane
            Elements.FixCachedStateTransitions();
            Elements.AddTagToElementAndEnable((SimHashes)SourWater_Liquid, new Tag?(GameTags.PlastifiableLiquid));

            Elements.AddTagToElementAndEnable(SimHashes.Naphtha, new Tag?(GameTags.CombustibleLiquid)); // Naphtha
            Elements.AddTagToElementAndEnable(SimHashes.MaficRock, new Tag?(GameTags.Crushable)); // MaficRock

            Element Hash_Electrum = ElementLoader.FindElementByHash(SimHashes.Electrum); // Electrum
            Hash_Electrum.highTempTransitionOreID = (SimHashes)Borax_Solid;
            Hash_Electrum.highTempTransitionOreMassConversion = 0.6f;
            Hash_Electrum.disabled = false;

            //ElementLoader.FindElementByHash((SimHashes)1433229102).materialCategory = GameTags.ManufacturedMaterial; // Bitumen
            //Elements.AddTagToElementAndEnable((SimHashes) - 1901832310, new Tag?(GameTags.ManufacturedMaterial)); // PhosphateNodules
            //Elements.AddTagToElementAndEnable((SimHashes) - 1901832310, new Tag?(GameTags.ConsumableOre)); // PhosphateNodules

            ElementLoader.FindElementByHash(SimHashes.CrushedRock).disabled = false; // CrushedRock
            Elements.AddTagToElementAndEnable(SimHashes.CrushedRock, new Tag?(GameTags.ConsumableOre)); // CrushedRock

            //Element Hash_Cement = ElementLoader.FindElementByHash((SimHashes)1627140480); // Cement
            //Hash_Cement.disabled = false;
            //Hash_Cement.thermalConductivity = 3.11f;
            //Hash_Cement.radiationAbsorptionFactor = 1f;

            //ElementLoader.FindElementByHash((SimHashes) - 325269471).highTemp = 2000f; // Brick
            //Elements.AddTagsToElementAndEnable((SimHashes) - 325269471, new Tag[3] // Brick
            //{
            //        GameTags.Crushable,
            //        GameTags.Insulator,
            //        GameTags.BuildableRaw
            //});

            //if (DlcManager.IsExpansion1Active())
            //{
            //    Elements.AddTagToElementAndEnable((SimHashes) - 47820500, new Tag?(GameTags.ConsumableOre)); // Radium
            //    Elements.AddTagToElementAndEnable((SimHashes) - 1624413844, new Tag?(GameTags.ManufacturedMaterial)); // Yellowcake
            //}

            Elements.AddTagToElementAndEnable(SimHashes.Carbon, new Tag?(GameTags.CombustibleSolid)); // Carbon
            Elements.AddTagToElementAndEnable(SimHashes.Peat, new Tag?(GameTags.CombustibleSolid)); // Peat
        }


        private static void FixCachedStateTransitions()
        {
            Element elementByHash = ElementLoader.FindElementByHash((SimHashes)Elements.Isopropane_Gas);
            if (elementByHash == null)
                return;
            elementByHash.highTempTransition = ElementLoader.FindElementByHash((SimHashes) (-1858722091)); // Propane
        }

        private static void AddTagToElementAndEnable(SimHashes element, Tag? tag = null)
        {
            SimHashes element1 = element;
            Tag[] tags;
            if (!tag.HasValue)
                tags = (Tag[])null;
            else
                tags = new Tag[1] { tag.Value };
            Elements.AddTagsToElementAndEnable(element1, tags);
        }

        private static void AddTagsToElementAndEnable(SimHashes element, Tag[] tags = null)
        {
            Element elementByHash = ElementLoader.FindElementByHash(element);
            if (elementByHash == null)
                return;
            elementByHash.disabled = false;
            if (tags == null || tags.Length == 0)
                return;
            if (elementByHash.oreTags == null)
            {
                elementByHash.oreTags = tags;
            }
            else
            {
                if (!((IEnumerable<Tag>)tags).Any<Tag>())
                    return;
                elementByHash.oreTags = ((IEnumerable<Tag>)Util.Concat<Tag>(elementByHash.oreTags, tags)).ToArray<Tag>();
            }
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

    }
}
