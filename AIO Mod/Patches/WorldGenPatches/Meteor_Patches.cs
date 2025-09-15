using HarmonyLib;
using ProcGen;
using System.Collections.Generic;
using System.Reflection;

namespace AIO_Mod.Patches.WorldGenPatches
{
    internal class Meteor_Patches
    {
        [HarmonyPatch(typeof(WorldContainer), "RefreshFixedTraits")]
        public static class Baator_CrashFix
        {
            public static void Postfix(WorldContainer __instance)
            {
                if (__instance.IsModuleInterior || !__instance.GetSeasonIds().Contains("MeteorShowers"))
                    return;
                typeof(WorldContainer).GetField("m_seasonIds", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object)__instance, (object)new List<string>());
                if (SettingsCache.worlds.HasWorld(__instance.worldName))
                {
                    World worldData = SettingsCache.worlds.GetWorldData(__instance.worldName);
                    if (worldData.seasons.Count > 0)
                    {
                        typeof(WorldContainer).GetField("m_seasonIds", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object)__instance, (object)new List<string>((IEnumerable<string>)worldData.seasons));
                        Debug.Log((object)$"Baator: Fixed Meteor Showers for {__instance.worldName}.");
                    }
                }
                else
                    Debug.LogWarning((object)"Planet not found in world data");
            }
        }
    }
}
