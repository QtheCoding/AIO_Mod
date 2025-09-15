using HarmonyLib;
using System.Collections.Generic;

namespace LEM_Working_Backwards.Buildings.NewRecipes
{
    [HarmonyPatch(typeof(AdvancedCraftingTableConfig), "ConfigureBuildingTemplate")]
    public static class SolderingStationRecipes
    {
        public static void Postfix()
        {

            var inputs = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.NickelOre.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(),     100f),
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

            string id = ComplexRecipeManager.MakeRecipeID("AdvancedCraftingTable", inputs, outputs);

            var recipe = new ComplexRecipe(id, inputs, outputs)
            {
                time = 20f,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                description = "Process Nickel Ore and Water into Hydrogen, Naphtha, and Solid Ethanol.",
                fabricators = new List<Tag> { TagManager.Create("AdvancedCraftingTable") }
            };

            Debug.Log("Added Soldering Station recipe: 100kg Nickel Ore + 100kg Water -> 30kg H2 + 50kg Naphtha + 30kg Solid Ethanol (20s)");
        }
    }
}
