using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AIO_Mod.Patches.TagSprites
{
    internal class TagUiSprites
    {
        internal static void SetMissingTagSprites(object item, ref Tuple<Sprite, Color> __result)
        {
            if (!((__result.first).name == "unknown") || !(item is Tag))
                return;
            Tag tag = (Tag)item;
            if (tag == GameTags.CombustibleGas)
            {
                __result.first = Assets.GetSprite("ui_combustible_gases");
                __result.second = Color.white;
            }
            else if (tag == GameTags.CombustibleLiquid)
            {
                __result.first = Assets.GetSprite("ui_combustible_liquids");
                __result.second = Color.white;
            }
            else
            {
                if (!(tag == GameTags.CombustibleSolid))
                    return;
                __result.first = Assets.GetSprite("ui_combustible_solids");
                __result.second = Color.white;
            }
        }
    }
}
