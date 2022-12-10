namespace AdventOfCode.Y2015.Solvers
{
    public class Day15 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var ingredients = ToIngredients(input);
            return GetPossibilties(ingredients, 0, 100).Select(CalculateScore).Max(score => score.Score);
        }

        public override object SolvePart2(string[] input)
        {
            var ingredients = ToIngredients(input);
            return GetPossibilties(ingredients, 0, 100).Select(CalculateScore).Where(score => score.Calories == 500).Max(score => score.Score);
        }

        private static List<Ingredient> ToIngredients(string[] lines)
        {
            var ingredients = new List<Ingredient>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                ingredients.Add(new(parts[0], int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[6]), int.Parse(parts[8]), int.Parse(parts[10])));
            }
            return ingredients;
        }

        private static List<Dictionary<Ingredient, int>> GetPossibilties(List<Ingredient> ingredients, int currentRecipe, int remainder)
        {
            if (currentRecipe == ingredients.Count - 1)
            {
                return new List<Dictionary<Ingredient, int>> { new Dictionary<Ingredient, int> { { ingredients[currentRecipe], remainder } } };
            }
            var possibilities = new List<Dictionary<Ingredient, int>>();
            var amount = 1;
            do
            {
                foreach (var recipe in GetPossibilties(ingredients, currentRecipe + 1, remainder - amount))
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

        private record struct Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
        private record struct RecipeScore(int Score, int Calories);
    }
}
