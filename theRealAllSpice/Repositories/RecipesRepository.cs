namespace theRealAllSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }
  internal Recipe Create(Recipe recipeData)
  {
    string sql = @"
        INSERT INTO recipes
        (title, img, category, instructions, creatorId)
        VALUES 
        (@title, @img, @category, @instructions, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
    int id = _db.ExecuteScalar<int>(sql, recipeData);
    recipeData.Id = id;
    return recipeData;
  }
  internal List<Recipe> Get()
  {
    string sql = @"
        SELECT
        rec.*,
        acc.* 
        FROM recipes rec
        JOIN accounts acc ON acc.id = rec.creatorId;
        ";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }
  internal Recipe Get(int id)
  {
    string sql = @"
    SELECT
    *
    FROM recipes
    WHERE id = @id;
    ";
    Recipe recipe = _db.Query<Recipe>(sql, new { id }).FirstOrDefault();
    return recipe;
  }

  internal Recipe GetOne(int id)
  {
    string sql = @"
    SELECT
    rec.*,
    acc.*
    FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE rec.id = @id;
    ";
    return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { id }).FirstOrDefault();
  }

  internal bool Update(Recipe update)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @title,
    instructions = @instructions,
    img = @img,
    category = @category
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }
  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM recipes
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }
}
