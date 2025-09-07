using HarmonyLib;
using Klei.AI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AIO_Mod.Utils
{
    public class ElementUtil
    {
        public static readonly Dictionary<SimHashes, string> SimHashNameLookup = new Dictionary<SimHashes, string>();
        public static readonly Dictionary<string, object> ReverseSimHashNameLookup = new Dictionary<string, object>();
        public static readonly List<ElementInfo> elements = new List<ElementInfo>();

        public static bool SimHashToString_EnumPatch(Enum __instance, ref string __result)
        {
            if (!(__instance is SimHashes))
                return true;
            SimHashes key = (SimHashes)__instance;
            return !ElementUtil.SimHashNameLookup.TryGetValue(key, out __result);
        }

        public static bool SimhashParse_EnumPatch(Type enumType, string value, ref object __result)
        {
            return !enumType.Equals(typeof(SimHashes)) || !ElementUtil.ReverseSimHashNameLookup.TryGetValue(value, out __result);
        }

        public static SimHashes RegisterSimHash(string name)
        {
            SimHashes key = (SimHashes)Hash.SDBMLower(name);
            Logger.l($"Element: {name}, simhash {key.ToString()}");
            ElementUtil.SimHashNameLookup.Add(key, name);
            ElementUtil.ReverseSimHashNameLookup.Add(name, key);
            return key;
        }

        public static void SetTexture_Main(Material material, string texture)
        {
            ElementUtil.SetTexture(material, texture, "_MainTex");
        }

        public static void SetTexture_ShineMask(Material material, string texture, Color? specularColor = null)
        {
            ElementUtil.SetTexture(material, texture, "_ShineMask");
            if (!specularColor.HasValue)
                return;
            material.SetColor("_ShineColour", specularColor.Value);
        }

        public static void SetTexture_NormalNoise(Material material, string normal)
        {
            ElementUtil.SetTexture(material, normal, "_NormalNoise");
        }

        public static Substance CreateSubstance(
          SimHashes id,
          bool specular,
          string anim,
          Element.State state,
          Color color,
          Material material,
          Color uiColor,
          Color conduitColor,
          Color? specularColor,
          string normal,
          bool isCloned = false)
        {
            KAnimFile kanimFile = Assets.Anims.Find(a => a.name == anim);
                // Assests.Anims = collecttion of all loaded kanim files in the game
                // .Find(...) = searches through the collection
                // 'a => a.name == anim' = lambda expression, for each item 'a' in the list, check if a.name matches string 'anim' -> if found returns that KAnimFile, if not returns null
            if (kanimFile == null)
                kanimFile = Assets.Anims.Find(a => a.name == "glass_kanim");
            Material material1 = new Material(material);
            if (state == Element.State.Solid && !isCloned)
            {
                ElementUtil.SetTexture_Main(material1, id.ToString().ToLowerInvariant());
                if (specular)
                    ElementUtil.SetTexture_ShineMask(material1, id.ToString().ToLowerInvariant() + "_spec", specularColor);
                if (!Util.IsNullOrWhiteSpace(normal))
                    ElementUtil.SetTexture_NormalNoise(material1, normal);
            }
            Substance substance = ModUtil.CreateSubstance(id.ToString(), state, kanimFile, material1, color, uiColor, conduitColor);
            Traverse.Create(substance).Field("anims").SetValue(new KAnimFile[1] { kanimFile });
            // substance.anims = new KAnimFile[1] { kanimFile };
            return substance;
        }

        //ModUtil.CreateSubstance(id.ToString(), state, kanimFile, material1, color, uiColor, conduitColor)
        // --------------------------------------------------
        //public static Substance CreateSubstance(string name, Element.State state, KAnimFile kanim, Material material, Color32 colour, Color32 ui_colour, Color32 conduit_colour)
        //{
        //    return new Substance
        //    {
        //        name = name,
        //        nameTag = TagManager.Create(name),
        //        elementID = (SimHashes)Hash.SDBMLower(name),
        //        anim = kanim,
        //        colour = colour,
        //        uiColour = ui_colour,
        //        conduitColour = conduit_colour,
        //        material = material,
        //        renderedByWorld = ((state & Element.State.Solid) == Element.State.Solid)
        //    };
        //}

        public static string ModPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static void SetTexture(Material material, string texture, string property)
        {
            Texture2D texture1;
            if (!ElementUtil.TryLoadTexture(Path.Combine(ElementUtil.ModPath, "assets", "textures", texture + ".png"), out texture1))
                return;
            material.SetTexture(property, (Texture)texture1);
        }

        public static bool TryLoadTexture(string path, out Texture2D texture)
        {
            texture = ElementUtil.LoadTexture(path);
            return texture != null;
        }

        public static Texture2D LoadTexture(string path, bool warnIfFailed = true)
        {
            Texture2D texture2D = (Texture2D)null;
            if (File.Exists(path))
            {
                byte[] numArray = ElementUtil.TryReadFile(path);
                texture2D = new Texture2D(1, 1);
                ImageConversion.LoadImage(texture2D, numArray);
            }
            else if (warnIfFailed)
                Logger.dlogwarn($"Could not load texture at path {path}.");
            return texture2D;
        }

        public static byte[] TryReadFile(string texFile)
        {
            try
            {
                return File.ReadAllBytes(texFile);
            }
            catch (Exception ex)
            {
                Logger.dlogwarn("Could not read file: " + ex?.ToString());
                return (byte[])null;
            }
        }

        public static void AddModifier(Element element, float decor, float overHeat)
        {
            if ((double)decor != 0.0)
                element.attributeModifiers.Add(new AttributeModifier(((Resource)((ModifierSet)Db.Get()).BuildingAttributes.Decor).Id, decor, element.name, true, false, true));
            if ((double)overHeat == 0.0)
                return;
            element.attributeModifiers.Add(new AttributeModifier(((Resource)((ModifierSet)Db.Get()).BuildingAttributes.OverheatTemperature).Id, overHeat, element.name, false, false, true));
        }

        //public static ElementsAudio.ElementAudioConfig GetCrystalAudioConfig(SimHashes id)
        //{
        //    ElementsAudio.ElementAudioConfig configForElement = ElementsAudio.Instance.GetConfigForElement((SimHashes) - 2123557039);
        //    return new ElementsAudio.ElementAudioConfig()
        //    {
        //        elementID = id,
        //        ambienceType = (AmbienceType) (- 1),
        //        solidAmbienceType = (SolidAmbienceType)10,
        //        miningSound = "PhosphateNodule",
        //        miningBreakSound = configForElement.miningBreakSound,
        //        oreBumpSound = configForElement.oreBumpSound,
        //        floorEventAudioCategory = "tileglass",
        //        creatureChewSound = configForElement.creatureChewSound
        //    };
        //}

        public static ElementsAudio.ElementAudioConfig CopyElementAudioConfig(
          ElementsAudio.ElementAudioConfig reference,
          SimHashes id)
        {
            return new ElementsAudio.ElementAudioConfig()
            {
                elementID = reference.elementID,
                ambienceType = reference.ambienceType,
                solidAmbienceType = reference.solidAmbienceType,
                miningSound = reference.miningSound,
                miningBreakSound = reference.miningBreakSound,
                oreBumpSound = reference.oreBumpSound,
                floorEventAudioCategory = reference.floorEventAudioCategory,
                creatureChewSound = reference.creatureChewSound
            };
        }

        public static ElementsAudio.ElementAudioConfig CopyElementAudioConfig(
          SimHashes referenceId,
          SimHashes id)
        {
            ElementsAudio.ElementAudioConfig configForElement = ElementsAudio.Instance.GetConfigForElement(referenceId);
            return new ElementsAudio.ElementAudioConfig()
            {
                elementID = configForElement.elementID,
                ambienceType = configForElement.ambienceType,
                solidAmbienceType = configForElement.solidAmbienceType,
                miningSound = configForElement.miningSound,
                miningBreakSound = configForElement.miningBreakSound,
                oreBumpSound = configForElement.oreBumpSound,
                floorEventAudioCategory = configForElement.floorEventAudioCategory,
                creatureChewSound = configForElement.creatureChewSound
            };
        }
    }
}
