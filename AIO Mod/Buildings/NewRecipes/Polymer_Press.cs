

using HarmonyLib;
using UnityEngine;

namespace AIO_Mod.Buildings.NewRecipes
{
    internal class Polymer_Press
    {
        [HarmonyPatch(typeof(Polymerizer), "OnStorageChanged")]
        public class Polymerizer_OnStorageChanged_Patch
        {
            public static void Postfix(Polymerizer __instance, object data)
            {
                if (!(__instance is EthanolPolymerizer ethanolPolymerizer))
                    return;
                GameObject gameObject = (GameObject)data;
                if (gameObject == null || !KPrefabIDExtensions.HasTag(gameObject, PolymerizerConfig.INPUT_ELEMENT_TAG))
                    return;
                ethanolPolymerizer.UpdateEthanolMeter();

                // just add GameTags.PlastifiableLiquid to make a liquid a plastic monomer
            }
        }
    }
}
