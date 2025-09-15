using HarmonyLib;
using ProcGen;
using System;
using System.Collections.Generic;

namespace AIO_Mod.Patches.WorldGenPatches
{
    internal class Temperature_Patches
    {
        public static Dictionary<Temperature.Range, string> TemperatureTable = new Dictionary<Temperature.Range, string>();
        public static Dictionary<string, object> TemperatureReverseTable = new Dictionary<string, object>();

        private static void AddHashToTable(Temperature.Range hash, string id)
        {
            Temperature_Patches.TemperatureTable.Add(hash, id);
            Temperature_Patches.TemperatureReverseTable.Add(id, (object)hash);
        }

        public static void OnLoad()
        {
            Debug.Log((object)"AIO - WorldGenPatches / temperature_patch - Adding HellHot");
            Temperature.Range range1 = (Temperature.Range)Hash.SDBMLower("HellHot");
            if (!TemperatureTable.ContainsKey(range1))
                AddHashToTable(range1, "HellHot");

            Debug.Log((object)"AIO - WorldGenPatches / temperature_patch - Adding CaniaCold");
            Temperature.Range range2 = (Temperature.Range)Hash.SDBMLower("CaniaCold");
            if (!TemperatureTable.ContainsKey(range2))
                AddHashToTable(range2, "CaniaCold");

            Debug.Log((object)"AIO - WorldGenPatches / temperature_patch - Adding SurfaceRandom");
            Temperature.Range range3 = (Temperature.Range)Hash.SDBMLower("SurfaceRandom");
            if (TemperatureTable.ContainsKey(range3))
                return;
            AddHashToTable(range3, "SurfaceRandom");
        }

        [HarmonyPatch(typeof(Enum), "ToString", new Type[] { })]
        public static class Temperatures_ToString_Patch
        {
            public static bool Prefix(ref Enum __instance, ref string __result)
            {
                if (!(__instance is Temperature.Range))
                    return true;
                return !Temperature_Patches.TemperatureTable.TryGetValue((Temperature.Range)__instance, out __result);
                //return !(__instance is Temperature.Range) || !Temperature_Patches.TemperatureTable.TryGetValue((Temperature.Range)__instance, out __result);
            }
        }

        [HarmonyPatch(typeof(Enum), "Parse", new Type[] { typeof(Type), typeof(string), typeof(bool) })]
        public static class Temperatures_Parse_Patch
        {
            public static bool Prefix(Type enumType, string value, ref object __result)
            {
                return !enumType.Equals(typeof(Temperature.Range)) || !Temperature_Patches.TemperatureReverseTable.TryGetValue(value, out __result);
            }
        }
    }
}
