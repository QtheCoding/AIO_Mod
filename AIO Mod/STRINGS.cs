using HarmonyLib;
using STRINGS;
using System;
using UnityEngine;
using Klei;
using System.Diagnostics.Contracts;

namespace AIO_Mod
{
    public class STRINGS
    {
        //ModUtil.RegisterForTranslation(typeof (STRINGS));
        public class CLUSTER_NAMES
        {
            public class AIO_CLUSTER
            {
                public static LocString NAME = "AIO Cluster";
                public static LocString DESCRIPTION = "A cluster containing all the biomes from the AIO mod.";
            }
        }

        public class ELEMENTS
        {
            public class SOLIDBORAX
            {
                public static LocString NAME = "Borax";
                public static LocString DESC = $"Borax, also known as sodium borate, is an important boron compound, mainly used in the manufacture of Fiberglass, and as a flux in metallurgy.";
            }
            public class ISOPROPANEGAS
            {
                public static LocString NAME = "Isopropane";
                public static LocString DESC = "(HC(CH<sub>3</sub>)<sub>3</sub>) Isopropane is a petrochemical refrigerant gas suitable for a variety of purposes. Degrades to at higher temperatures.";
                //LocString($"(HC(CH<sub>3</sub>)<sub>3</sub>) Isopropane is a petrochemical refrigerant gas suitable for a variety of purposes. Degrades to {STRINGS.UI.FormatAsLink("Propane", "PROPANE")} at higher temperatures.");
            }
            public class SOURWATER
            {
                public static LocString NAME = "Sour Water";
                public static LocString DESC = "An aqueous solution of Hydrogen Sulfide (H<sub>2</sub>S) and Ammonia (NH<sub>3</sub>). May occur naturally from aquifers exposed to hydrogen sulfide sources, but it is more common as a wastewater from industrial processes.";
            }
            public class LITHIUMBRINE
            {
                public static LocString NAME = "Lithium Brine";
                public static LocString DESC = "A highly concentrated solution of chloride salts in water, mostly Sodium Chloride, typically found in underground reservoirs. It is a primary source of lithium for various industrial applications, but has traces of other valuable metals.";

            }
            public class UNOBTANIUMALLOY
            {
                public static LocString NAME = "Neutronium Alloy";
                public static LocString DESC = "An insanely durable and heat resistant alloy.\nRequired in the construction of large space structures.\nVery sparkly";
            }
        }

        public class ITEMS
        {
            public class INGREDIENTS
            {
                public class RAYONFIBER
                {
                    public static LocString NAME = "Rayon Fiber";
                    public static LocString NAME_PLURAL = "Rayon Fibers";
                    public static LocString DESC = "Rayon is a synthetic fiber, chemically made from regenerated cellulose extracted from Lumber.";
                    public static LocString RECIPE_DESC = $"Produces {STRINGS.ITEMS.INGREDIENTS.RAYONFIBER.NAME_PLURAL} from the pulp of {{0}}.";
                }
            }
        }

        public class SUBWORLDS
        {
            public class AIO_SUBWORLD_1
            {
                public static LocString NAME = "Subworld 1";
                public static LocString DESC = $"{UI.FormatAsLink("Slime", "SLIMEMOLD")} may not be here.  \nMay have {UI.FormatAsLink("plant life", "PLANTS")} or {UI.FormatAsLink("critters", "CREATURES")}. \n\nLand devoid of {UI.FormatAsLink("liquids", "ELEMENTS_LIQUID")} and minuscule {UI.FormatAsLink("gas", "ELEMENTS_GAS")}, but has {UI.FormatAsLink("solid", "ELEMENTS_SOLID")}";
                public static LocString UTILITY = $"Land devoid of {UI.FormatAsLink("liquids", "ELEMENTS_LIQUID")} and minuscule {UI.FormatAsLink("gas", "ELEMENTS_GAS")}. \n\nBut has {UI.FormatAsLink("solid", "ELEMENTS_SOLID")}";
            }
        }

        public class WORLDS
        {
            public class AIO_WORLD_1
            {
                public static LocString NAME = "AIO World";
                public static LocString DESCRIPTION = "A world containing all the biomes from the AIO mod. \n\nHopefully it works.";
            }
        }
    }
}
// global::STRINGS.UI.FormatAsLink("Borax", nameof(SOLIDBORAX))
//{STRINGS.UI.FormatAsLink("Fiberglass", "SOLIDFIBERGLASS")}