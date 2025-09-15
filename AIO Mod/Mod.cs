using HarmonyLib;
using KMod;
using AIO_Mod;
using AIO_Mod.Patches.WorldGenPatches;

namespace AIO_Mod
{
    internal class Mod : UserMod2
    {
        public static Mod Instance;
        public static Harmony HarmonyInstance;

        public virtual void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            
            TagAssets.RegisterTags();
            Debug.Log("AIO - adding temperature ranges and Strings");
            base.OnLoad(harmony);
            Temperature_Patches.OnLoad();
            //harmony.PatchAll();
        }
    }
}
