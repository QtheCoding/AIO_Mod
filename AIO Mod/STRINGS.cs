using HarmonyLib;
using STRINGS;
using System;
using UnityEngine;
using Klei;

namespace AIO_Mod
{
    public class STRINGS
    {
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
        }
    }
}
// global::STRINGS.UI.FormatAsLink("Borax", nameof(SOLIDBORAX))
//{STRINGS.UI.FormatAsLink("Fiberglass", "SOLIDFIBERGLASS")}