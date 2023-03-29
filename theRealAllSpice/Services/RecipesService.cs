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

  internal Recipe Get(int id)
  {
    Recipe recipe = _repo.Get(id);
    if (recipe == null)
    {
      throw new Exception("could not find that recipe 🤷‍♂️");
    }
    return recipe;
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

  internal Recipe Update(Recipe recipeUpdate, int id, string userId)
  {
    Recipe recipe = Get(id);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("awkward, that is not yours pal.");
    }
    recipe.Title = recipeUpdate.Title ?? recipe.Title;
    recipe.Instructions = recipeUpdate.Instructions ?? recipe.Instructions;
    recipe.Img = recipeUpdate.Img ?? recipe.Img;
    recipe.Category = recipeUpdate.Category ?? recipe.Category;
    bool edited = _repo.Update(recipe);
    if (edited == false)
    {
      throw new Exception("recipe not changed");
    }
    return recipe;
  }
  internal string Remove(int id, string userId)
  {
    Recipe recipe = GetOne(id, userId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("you cant do that you pirate");
    }
    _repo.Remove(id);
    return $"{recipe.Title} was deleted";
  }

}
