using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public partial class Day15 : SolverWithLines
    {
        public override object SolvePart1(string[] input) => ToScores(input).Max(score => score.Score);
        public override object SolvePart2(string[] input) => ToScores(input).Where(score => score.Calories == 500).Max(score => score.Score);

        private static RecipeScore[] ToScores(string[] lines) => FindPossibilties(ToIngredients(lines), 0, 100).Select(CalculateScore).ToArray();

        private static List<Ingredient> ToIngredients(string[] lines)
        {
            var ingredients = new List<Ingredient>();
            foreach (var line in lines)
            {
                var match = IngredientRegex().Match(line);
                ingredients.Add(new(match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value)));
            }
            return ingredients;
        }

        private static List<Dictionary<Ingredient, int>> FindPossibilties(List<Ingredient> ingredients, int currentRecipe, int remainder)
        {
            if (currentRecipe == ingredients.Count - 1)
            {
                return [new() { { ingredients[currentRecipe], remainder } }];
            }
            var possibilities = new List<Dictionary<Ingredient, int>>();
            var amount = 1;
            do
            {
                foreach (var recipe in FindPossibilties(ingredients, currentRecipe + 1, remainder - amount))
                {
                    recipe.Add(ingredients[currentRecipe], amount);
                    possibilities.Add(recipe);
                }
            } while (amount++ < remainder - (ingredients.Count - 1 - currentRecipe));
            return possibilities;
        }

        private static RecipeScore CalculateScore(Dictionary<Ingredient, int> recipe)
        {
            var capacity = 0;
            var durability = 0;
            var flavor = 0;
            var texture = 0;
            var calories = 0;
            foreach (var step in recipe)
            {
                capacity += step.Value * step.Key.Capacity;
                durability += step.Value * step.Key.Durability;
                flavor += step.Value * step.Key.Flavor;
                texture += step.Value * step.Key.Texture;
                calories += step.Value * step.Key.Calories;
            }
            return new(Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) * Math.Max(0, texture), calories);
        }

        [GeneratedRegex(@"(.+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)")]
        private static partial Regex IngredientRegex();

        private record class Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
        private record class RecipeScore(int Score, int Calories);
    }
}
