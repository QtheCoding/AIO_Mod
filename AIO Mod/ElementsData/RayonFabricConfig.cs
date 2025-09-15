using Klei.AI;
using System.Collections.Generic;
using UnityEngine;

namespace AIO_Mod.ElementsData
{
    internal class RayonFabricConfig : IEntityConfig
    {
        public static string ID = "RayonFiber";
        public static readonly Tag TAG = TagManager.Create(RayonFabricConfig.ID);
        private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, STRINGS.ITEMS.INGREDIENTS.RAYONFIBER.NAME, true, false, true);

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                RayonFabricConfig.ID, 
                STRINGS.ITEMS.INGREDIENTS.RAYONFIBER.NAME, 
                STRINGS.ITEMS.INGREDIENTS.RAYONFIBER.DESC, 
                1f, 
                true, 
                Assets.GetAnim("rayon_fiber_kanim"), 
                "object", 
                (Grid.SceneLayer)26, 
                (EntityTemplates.CollisionShape)0, 
                0.35f, 
                0.35f, 
                true, 
                0, 
                (SimHashes)976099455, 
                new List<Tag>()
                {
                    GameTags.IndustrialIngredient,
                    GameTags.BuildingFiber
                });
            EntityTemplateExtensions.AddOrGet<EntitySplitter>(looseEntity);
            EntityTemplateExtensions.AddOrGet<SimpleMassStatusItem>(looseEntity);
            EntityTemplateExtensions.AddOrGet<PrefabAttributeModifiers>(looseEntity).AddAttributeDescriptor(this.decorModifier);
            return looseEntity;
        }

        public string[] GetDlcIds() => (string[])null;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
