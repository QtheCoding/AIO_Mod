using HarmonyLib;
using System.Collections.Generic;
using AIO_Mod.ElementsData;

namespace AIO_Mod.Patches
{
    internal class ElementLoader_Patches
    {
        // Prefix() = run before original method
        // Postfix() = run immediately after original method
        // [HarmonyPatch () ] = intercept (patch) the defined method
        // return true = run original method after this patch
        // return false = skip original method, use given method instead
        // [HarmonyPatch( typeof(someClass) , MethodName, new Type[] {...} ]
        // typeof(ClassName) is needed because it gives you a System.Type object that Harmony needs to identify the class/method

        [HarmonyPatch(typeof(ElementLoader), "Load")]
        public class ElementLoader_Load_Patch
        {
            public static void Prefix(
              Dictionary<string, SubstanceTable> substanceTablesByDlc)
            {
                Elements.RegisterSubstances(substanceTablesByDlc[""].GetList());
            }
        }

    }
}