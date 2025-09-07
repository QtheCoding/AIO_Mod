using System;
using UnityEngine;

namespace AIO_Mod.Utils
{
    // Object => Texture => Texture2D, so no type casting needed
    public class ElementInfo
    {
        public string id;
        public Element.State state;
        public string anim;
        public Color color;
        public Color uiColor;
        public Color conduitColor;
        public bool isInitialized;

        public SimHashes SimHash { get; private set; }
        public Tag CreateTag() => this.Tag;
        public Tag Tag { get; private set; }

        public static implicit operator SimHashes(ElementInfo info) => info.SimHash;
        public override string ToString() => SimHash.ToString();

        public Element Get()
        {
            if (ElementLoader.elementTagTable != null)
                return ElementLoader.GetElement(this.Tag);
            Logger.dlogwarn("Trying to fetch element too early, elements are not loaded yet.");
            return null;
        }

        // constructor
        public ElementInfo(string id, string anim, Element.State state, Color color)
        {
            this.id = id;
            this.anim = anim;
            this.state = state;
            this.color = color;
            this.SimHash = ElementUtil.RegisterSimHash(id);
            ElementUtil.elements.Add(this);
            this.Tag = new Tag(id); // can't do this.Tag = Tag.id because the Tag type doesn't have a .id property
        }
         

        // creating a new Material that is a copy of the original material but with a tinted texture
        public Material CreateTintedMaterialCopy(SimHashes originalElement, Color? overrideColor = null)
        // overrideColor is used to specifically tint the texture tile. base color is still used for UI/conduit color/etc.
        {
            Substance substance = Assets.instance.substanceTable.GetSubstance(originalElement); // getting the substance for the original element via its SimHash/lookup
            if (substance == null)
            {
                Logger.error("No substance found for " + originalElement.ToString());
                return (Material)null;
            }

            Material tintedMaterialCopy = new Material(substance.material); // creating a new Material that is a copy of the original material
            Texture2D tintedTex = TintTextureWithColor(
                tintedMaterialCopy.mainTexture as Texture2D,
                this.id,
                overrideColor ?? this.color); // x ?? y => if x is not null, use x, otherwise use y
            tintedMaterialCopy.mainTexture = tintedTex; // reassigning .mainTexture to the tinted texture
            tintedMaterialCopy.name = "mat" + this.id;
            return tintedMaterialCopy;
        }

        private static Texture2D TintTextureWithColor(Texture sourceTexture, string name, Color tint)
        {
            Texture2D readableCopy = ElementInfo.GetReadableCopy(sourceTexture as Texture2D);
            if (readableCopy == null)
                return null;

            Color32[] pixels32 = readableCopy.GetPixels32();
            for (int i = 0; i < pixels32.Length; ++i)
            {
                Color c = pixels32[i]; // implicit cast Color32 -> Color
                float lum = c.grayscale * 1.5f; // luminance = brightness of pixels || grayscale = per-pixel brightness map/pattern || *1.5 = global brightness boost
                Color tinted = new Color(lum * tint.r, lum * tint.g, lum * tint.b, c.a); // keeping/applying grayscale pattern, but tinting it with the desired color
                pixels32[i] = tinted; // implicit cast Color -> Color32
            }

            readableCopy.SetPixels32(pixels32);
            readableCopy.Apply();
            readableCopy.name = name;
            return readableCopy;
        }

        // essentially cloning source Texture2D as readableCopy Texture2D which can then be used and edited
        public static Texture2D GetReadableCopy(Texture2D source)
        {
            if (source == null || source.width == 0 || source.height == 0)
                return null;
            // creating uninitialized/undefined texture until we blit
            RenderTexture temporary = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                (RenderTextureFormat) 7, // RenderTextureFormat = 7 => pixel format, 8 bits/channel RGBA
                (RenderTextureReadWrite) 1); // Using sRGB color correction

            // Copies the pixels ("blits") from source Texture2D to temporary RenderTexture 
            // i.e. Copying and Pasting so that you don't touch the original source Texture2D. 
            Graphics.Blit(source, temporary);

            // RT = RenderTexture
            RenderTexture active = RenderTexture.active; // Stores the active RT
            RenderTexture.active = temporary; // sets temporary RT as the active RT, meaning APIs (like .ReadPixels) will read from temporary
            Texture2D readableCopy = new Texture2D(source.width, source.height);

            // Copy pixels from the currently active RT (temporary RT) into readableCopy
            // .ReadPixels always copies from the RenderTexture.active
            readableCopy.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0); // entire area of Rect = rectangle. 
            // final (0,0) are final destination in readableCopy where the copied block will be placed (bottom left corner)
            readableCopy.Apply(); // commites the copied pixels to the Texture2D readableCopy

            RenderTexture.active = active; // sets RT.active back to the originally active RT (restores previous/original state)
            RenderTexture.ReleaseTemporary(temporary); // returns temporary RT from .GetTemporary() back into Unity's pool. temporary RT becomes invalid.

            return readableCopy;
        }


        public Substance CreateSubstanceFromElementTinted(SimHashes clonedMaterial, Color? overrideColor = null)
        {
            return this.CreateSubstance(
                material: this.CreateTintedMaterialCopy(clonedMaterial, overrideColor),
                cloneMaterialOrigin: clonedMaterial,
                clonedMaterialColorOverride: overrideColor);
        }

        public Substance CreateSubstance(
            bool specular = false,
            Material  material = null,
            Color? uiColor = null,
            Color? conduitColor = null,
            Color? specularColor = null,
            string normal = null, // tile texture for normal mapping -> _normal for tile texture, _kanim for debris texture
            SimHashes? cloneMaterialOrigin = null,
            Color? clonedMaterialColorOverride = null)
        {
            bool isCloned = cloneMaterialOrigin.HasValue;

            if (material == null)
            {
                material = (state == Element.State.Solid)
                    ? Assets.instance.substanceTable.solidMaterial
                    : Assets.instance.substanceTable.liquidMaterial;

                if (isCloned && state == Element.State.Solid)
                    material = CreateTintedMaterialCopy(cloneMaterialOrigin.Value, clonedMaterialColorOverride);
            }

            isInitialized = true;

            return ElementUtil.CreateSubstance(
                this.SimHash,
                specular,
                this.anim,
                this.state,
                this.color,
                material,
                uiColor ?? color,
                conduitColor ?? color,
                specularColor,
                normal,
                isCloned);
        }

        public Substance CreateSubstance(Color uiColor, Color conduitColor)
        {
            return CreateSubstance(uiColor: uiColor, conduitColor: conduitColor);
        }

        public static ElementInfo Solid(string id, Color color)
        {
            return new ElementInfo(id, id.ToLowerInvariant() + "_kanim", Element.State.Solid, color);
        }

        public static ElementInfo Solid(string id, string anim, Color color)
        {
            return new ElementInfo(id, anim, Element.State.Solid, color);
        }

        public static ElementInfo Liquid(string id, Color color)
        {
            return new ElementInfo(id, "liquid_tank_kanim", Element.State.Liquid, color);
        }

        public static ElementInfo Gas(string id, Color color)
        {
            return new ElementInfo(id, "gas_tank_kanim", Element.State.Gas, color);
        }

    }
}
