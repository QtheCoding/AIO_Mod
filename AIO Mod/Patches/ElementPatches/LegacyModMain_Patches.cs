using AIO_Mod.ElementsData;
using HarmonyLib;

namespace AIO_Mod.Patches
{
    internal class LegacyModMain_Patches
    {
        [HarmonyPatch(typeof(LegacyModMain), "ConfigElements")]
        public class LegacyModMain_ConfigElements_Patch
        {
            public static void Postfix(LegacyModMain __instance) => Elements.ConfigureElements();
        }

        //[HarmonyPatch(typeof(LegacyModMain), "LoadEntities")]
        //public class LegacyModMain_LoadEntities_Patch
        //{
        //    public static void Postfix(LegacyModMain __instance)
        //    {
        //        AdditionalRecipes.RegisterRecipes_PostLoadEntities();
        //    }
        //}
    }
}
