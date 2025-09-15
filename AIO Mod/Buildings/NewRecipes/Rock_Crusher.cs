using HarmonyLib;
using System.Collections.Generic;


namespace LEM_Working_Backwards.Buildings.NewRecipes
{
    [HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate")]
    public static class RockCrusherRecipes
    {
        public static void Postfix()
        {
            var inputs = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 100f),
            };
            var outputs = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 100f,
                    ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
            };

            string id = ComplexRecipeManager.MakeRecipeID("RockCrusher", inputs, outputs);

            var recipe = new ComplexRecipe(id, inputs, outputs)
            {
                time = 10f,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                description = "Crush Dirt into Sand.",
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };

            Debug.Log("Added Rock Crusher recipe (ConfigureBuildingTemplate): Dirt -> Sand");
        }
    }
}
