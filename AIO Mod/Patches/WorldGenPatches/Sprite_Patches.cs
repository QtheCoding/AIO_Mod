using HarmonyLib;
using System.Reflection;
using UnityEngine;
using System.IO;

namespace AIO_Mod.Patches.WorldGenPatches
{
    internal class Sprite_Patches
    {
        private static Texture2D LoadTexture(string path)
        {
            Texture2D texture2D = (Texture2D)null;
            if (File.Exists(path))
            {
                byte[] numArray = File.ReadAllBytes(path);
                texture2D = new Texture2D(1, 1);
                ImageConversion.LoadImage(texture2D, numArray);
            }
            else
                Debug.LogWarning((object)$"Texture does not exist at: {path}.");
            return texture2D;
        }

        [HarmonyPatch(typeof(Assets), "OnPrefabInit")]
        public class Assets_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                // can't do this.LoadTexture because it's a static method. Can only use "this." for non-static things
                Texture2D texture2D1 = Sprite_Patches.LoadTexture(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets/sprites/biomeIconBaator.png"));
                Sprite sprite1 = Sprite.Create(texture2D1, new Rect(0.0f, 0.0f, texture2D1.width, texture2D1.height), Vector2.zero);

                Texture2D texture2D2 = Sprite_Patches.LoadTexture(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets/sprites/biomeIconColdterra.png"));
                Sprite sprite2 = Sprite.Create(texture2D2, new Rect(0.0f, 0.0f, texture2D2.width, texture2D2.height), Vector2.zero);

                Assets.Sprites.Add("biomeIconBaator", sprite1);
                Assets.Sprites.Add("biomeIconColdterra", sprite2);
            }
        }
    }
}
