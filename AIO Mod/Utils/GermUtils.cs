using Klei.AI;
using Klei.AI.DiseaseGrowthRules;

namespace AIO_Mod.Utils
{
    internal class GermUtils
    {
        public static ElementGrowthRule DieInElement(
            SimHashes element,
            float scale = 1f)
        {
            ElementGrowthRule elementGrowthRule = new Klei.AI.DiseaseGrowthRules.ElementGrowthRule(element);
            elementGrowthRule.populationHalfLife = new float?(10f / scale);
            elementGrowthRule.overPopulationHalfLife = new float?(10f / scale);
            elementGrowthRule.minDiffusionCount = new int?((int)(100000.0 * (double)scale));
            elementGrowthRule.diffusionScale = new float?(1f / 1000f);
            return elementGrowthRule;
        }
    }
}
