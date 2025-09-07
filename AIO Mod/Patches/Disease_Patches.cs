using HarmonyLib;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using System.Collections.Generic;
using System.Reflection;
using AIO_Mod.Utils;

namespace AIO_Mod.Patches
{
    [HarmonyPatch(typeof(Disease), "InitializeElemGrowthArray")]
    internal static class Disease_ElemArray_SizeFix
    {
        private static bool Prefix(ref ElemGrowthInfo[] infoArray, ElemGrowthInfo default_value)
        {
            List<Element> elements = ElementLoader.elements;
            infoArray = new ElemGrowthInfo[elements.Count];
            for (int i = 0; i < elements.Count; i++)
            {
                infoArray[i] = default_value;
            }
            return false;
        }
    }
}

//  protected void InitializeElemGrowthArray(ref ElemGrowthInfo[] infoArray, ElemGrowthInfo default_value)
//  {
//      List<Element> elements = ElementLoader.elements;
//      infoArray = new ElemGrowthInfo[elements.Count];
//      for (int i = 0; i < elements.Count; i++)
//      {
//        infoArray[i] = default_value;
//      }

//      infoArray[ElementLoader.GetElementIndex(SimHashes.Polypropylene)] = new ElemGrowthInfo
//      {
//        underPopulationDeathRate = 2.66666675f,
//        populationHalfLife = 10f,
//        overPopulationHalfLife = 10f,
//        minCountPerKG = 0f,
//        maxCountPerKG = float.PositiveInfinity,
//        minDiffusionCount = int.MaxValue,
//        diffusionScale = 1f,
//        minDiffusionInfestationTickCount = byte.MaxValue
//      };
//      infoArray[ElementLoader.GetElementIndex(SimHashes.Vacuum)] = new ElemGrowthInfo
//      {
//        underPopulationDeathRate = 0f,
//        populationHalfLife = 0f,
//        overPopulationHalfLife = 0f,
//        minCountPerKG = 0f,
//        maxCountPerKG = float.PositiveInfinity,
//        diffusionScale = 0f,
//        minDiffusionInfestationTickCount = byte.MaxValue
//      };
//  }