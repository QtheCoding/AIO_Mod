using HarmonyLib;
using System.Collections.Generic;

namespace LearningElementsMod.Buildings.Recipes
{

    [HarmonyPatch(typeof(SuitFabricatorConfig), "ConfigureBuildingTemplate")]
    public static class ExosuitForgeRecipes
    {
        public static void Postfix()
        {

            var inputs = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(),      100f),
            new ComplexRecipe.RecipeElement(SimHashes.NickelOre.CreateTag(), 100f),
            };
            var outputs = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), 20f),
            new ComplexRecipe.RecipeElement(SimHashes.Isoresin.CreateTag(),      40f),
            new ComplexRecipe.RecipeElement(SimHashes.ToxicMud.CreateTag(),      75f),
            };

            // ⬇⬇ Use "SuitFabricator" here
            string id = ComplexRecipeManager.MakeRecipeID("SuitFabricator", inputs, outputs);

            var recipe = new ComplexRecipe(id, inputs, outputs)
            {
                time = 60f,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                description = "Synthesize materials from Dirt and Nickel Ore.",
                // ⬇⬇ And here
                fabricators = new List<Tag> { TagManager.Create("SuitFabricator") }
            };

            Debug.Log("Added Exosuit Forge recipe: 100kg Dirt + 100kg Nickel Ore -> 20kg Plastic + 40kg Isosap + 75kg Polluted Mud (60s)");
        }
    }
}
