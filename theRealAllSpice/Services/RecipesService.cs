namespace theRealAllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }
  internal Recipe Create(Recipe recipeData)
  {
    Recipe recipe = _repo.Create(recipeData);
    return recipe;
  }
  internal List<Recipe> Get()
  {
    List<Recipe> recipes = _repo.Get();
    return recipes;
  }

  internal Recipe GetOne(int id, string userId)
  {
    Recipe recipe = _repo.GetOne(id);
    if (recipe == null)
    {
      throw new Exception("🤷‍♂️ I dunno bro");
    }
    if (recipe.CreatorId != userId)
    {
      throw new Exception("ummmm that is not yours criminal");
    }
    return recipe;
  }

}
