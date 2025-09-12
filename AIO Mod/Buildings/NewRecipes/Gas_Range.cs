using HarmonyLib;
using System.Collections.Generic;

namespace LEM_Working_Backwards.Buildings.NewRecipes
{
    [HarmonyPatch(typeof(GourmetCookingStationConfig), "ConfigureBuildingTemplate")]
    public static class GasRangeRecipes
    {
        public static void Postfix()
        {

            var inputs = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(),  100f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 100f),
            };
            var outputs = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Hydrogen.CreateTag(),     30f,
                    ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
                new ComplexRecipe.RecipeElement(SimHashes.Naphtha.CreateTag(),      50f,
                    ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
                new ComplexRecipe.RecipeElement(SimHashes.SolidEthanol.CreateTag(), 30f,
                    ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
            };

            string id = ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", inputs, outputs);

            var recipe = new ComplexRecipe(id, inputs, outputs)
            {
                time = 20f,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                description = "Convert Dirt and Water into Hydrogen, Naphtha, and Solid Ethanol.",
                fabricators = new List<Tag> { TagManager.Create("GourmetCookingStation") }
            };

            Debug.Log("[LearningElementsMod] Added Gas Range recipe: 100kg Dirt + 100kg Water -> 30kg H2 + 50kg Naphtha + 30kg Solid Ethanol (20s)");
        }
    }
}
