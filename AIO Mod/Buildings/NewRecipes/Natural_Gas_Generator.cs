using HarmonyLib;
using UnityEngine;
using AIO_Mod.ElementsData;

namespace AIO_Mod.Buildings.NewRecipes
{
    internal class Natural_Gas_Generator
    {
        [HarmonyPatch(typeof(MethaneGeneratorConfig), "DoPostConfigureComplete")]
        public class MethaneGeneratorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.GetComponent<EnergyGenerator>().formula.inputs = new EnergyGenerator.InputItem[1]
                {
                    new EnergyGenerator.InputItem(GameTags.CombustibleGas, 0.09f, 0.9f)
                    // allowed inputs
                };
                ConduitDispenser conduitDispenser = EntityTemplateExtensions.AddOrGet<ConduitDispenser>(go);
                SimHashes[] Combust_gases = new SimHashes[2] { (SimHashes) Elements.Isopropane_Gas, SimHashes.Propane };
                conduitDispenser.elementFilter = Util.Append<SimHashes>(conduitDispenser.elementFilter, Combust_gases);
                // elementFilter = which gases are kept and used
            }
        }
    }
}

//(SimHashes)Hash.SDBMLower("IsopropaneGas")