namespace theRealAllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipesService _recipesService;

  public FavoritesService(RecipesService recipesService, FavoritesRepository repo)
  {
    _recipesService = recipesService;
    _repo = repo;
  }
  internal List<MyRecipe> GetMyRecipes(string accountId)
  {
    List<MyRecipe> myRecipes = _repo.GetMyRecipes(accountId);
    return myRecipes;
  }
  internal Favorite Create(Favorite favoriteData)
  {
    Recipe recipe = _recipesService.GetOne(favoriteData.RecipeId, favoriteData.AccountId);
    Favorite favorite = _repo.Create(favoriteData);
    return favorite;
  }
  internal string Destroy(int id, string userId)
  {
    Favorite favorite = _repo.GetOne(id);
    if (favorite == null)
    {
      throw new Exception("🤷‍♀️ I lost your favorite selection bro");
    }
    if (favorite.AccountId != userId)
    {
      throw new Exception("that isn't yours hoser");
    }
    _repo.Destroy(id);
    return $"no longer a favorited recipe. ID: {id}";
  }
}
