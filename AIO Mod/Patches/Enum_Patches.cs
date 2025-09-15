using AIO_Mod.Utils;
using HarmonyLib;
using System;

namespace AIO_Mod.Patches
{
    // Prefix() = run before original method
    // Postfix() = run immediately after original method
    // [HarmonyPatch () ] = intercept (patch) the defined method
        // return true = run original method after this patch
        // return false = skip original method, use given method instead
    // [HarmonyPatch( typeof(someClass) , MethodName, new Type[] {...} ]
        // typeof(ClassName) is needed because it gives you a System.Type object that Harmony needs to identify the class/method

    public class Enum_Patches
    {
        public static class EnumPatch
        {
            [HarmonyPatch(typeof(Enum), "ToString", new Type[] { })]
            public class SimHashes_ToString_Patch
            {
                public static bool Prefix(ref Enum __instance, ref string __result)
                {
                    return ElementUtil.SimHashToString_EnumPatch(__instance, ref __result);
                }
            }

            [HarmonyPatch(typeof(Enum), "Parse", new Type[] { typeof(Type), typeof(string), typeof(bool) })]
            private class SimHashes_Parse_Patch
            {
                private static bool Prefix(Type enumType, string value, ref object __result)
                {
                    return ElementUtil.SimhashParse_EnumPatch(enumType, value, ref __result);
                }
            }
        }
    }
}
