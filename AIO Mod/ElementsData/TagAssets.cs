using AIO_Mod.ElementsData;
using System;
using System.Collections.Generic;
using TUNING;
using static STRINGS.UI.UISIDESCREENS;

namespace AIO_Mod
{
    internal class TagAssets
    {
        internal static void RegisterTags()
        {
            // Tag has an implicit conversion from String to Tag, so String is automatically converted to Tag
            Tag[] Fabrics = new Tag[1] { 
                RayonFabricConfig.ID 
            };
            GameTags.Fabrics = Util.Append<Tag>(GameTags.Fabrics, Fabrics);
        } 
    }
}

//public static List<Tag> GetNonLiquifiableSolids()
//{
//    return STORAGEFILTERS.NOT_EDIBLE_SOLIDS.Where<Tag>((Func<Tag, bool>)(item => Tag.op_Inequality(item, GameTags.Liquifiable))).ToList<Tag>();
//}

//public class Tags
//{
//    public static Tag RandomSand = TagManager.Create("ChemicalProcessing_RandomSand");
//    public static Tag MineralProcessing_GuidanceUnit = TagManager.Create(nameof(MineralProcessing_GuidanceUnit));
//    public static Tag MineralProcessing_Drillbit = TagManager.Create(nameof(MineralProcessing_Drillbit));
//    public static Tag RandomRecipeIngredient_DestroyOnCancel = TagManager.Create(nameof(RandomRecipeIngredient_DestroyOnCancel));
//    public static Tag AIO_HardenedAlloy = TagManager.Create(nameof(AIO_HardenedAlloy));
//    public static Tag AIO_CarrierGas = TagManager.Create(nameof(AIO_CarrierGas));
//}