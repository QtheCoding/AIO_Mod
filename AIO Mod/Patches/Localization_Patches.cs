using Klei;
using HarmonyLib;

namespace AIO_Mod.Patches
{
    public class Localization_Patches
    {
        [HarmonyPatch(typeof(Localization), "Initialize")]
        public static class Localization_Initialize_Patch
        {
            public static void Postfix()
            {
                // Register all LocString fields under your own strings root class
                LocString.CreateLocStringKeys(typeof(AIO_Mod.STRINGS), "");
            }
        }
    }
}
