namespace theRealAllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }
  internal Ingredient Create(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.Create(ingredientData);
    return ingredient;
  }
  internal List<Ingredient> GetIngredients(int recipeId, string userId)
  {
    Recipe recipe = _recipesService.GetOne(recipeId, userId);
    List<Ingredient> ingredients = _repo.GetIngredients(recipeId);
    return ingredients;
  }

  internal string Destroy(int id, string userId)
  {
    Ingredient ingredient = _repo.GetOne(id);
    if (ingredient == null)
    {
      throw new Exception("could not find ingredient");
    }
    if (ingredient.CreatorId != userId)
    {
      throw new Exception("that is not yours filthy trash panda");
    }
    _repo.Destroy(id);
    return $"ingredient ID: {id} was purged.";

  }
}
