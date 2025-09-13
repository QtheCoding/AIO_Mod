
using System;
using HarmonyLib;
using UnityEngine;

namespace AIO_Mod.Patches.TagSprites
{
    internal class TagUiSprite_Patches
    {
        [HarmonyPatch(typeof(Def), "GetUISprite", new Type[] { typeof(object), typeof(string), typeof(bool) })]
        public class Def_GetUISprite_Patch
        {
            public static void Postfix(object item, ref Tuple<Sprite, Color> __result)
            {
                TagUiSprites.SetMissingTagSprites(item, ref __result);
            }
        }
    }
}
