namespace theRealAllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }
  internal Favorite Create(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO facorites
    (recipeid, accountId)
    VALUES
    (@recipeid, @accountId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = id;
    return favoriteData;
  }
  internal List<MyRecipe> GetMyRecipes(string accountId)
  {
    string sql = @"
    SELECT
    rec.*,
    fav.*,
    creator.*
    FROM favorites fav
    JOIN recipes rec ON rec.id = fav.recipeId
    JOIN accounts creator ON rec.creatorId = creator.id
    WHERE fav.accountId = @accountId;
    ";
    List<MyRecipe> myRecipes = _db.Query<MyRecipe, Favorite, Account, MyRecipe>(sql, (rec, fav, creator) =>
    {
      rec.FavoriteId = fav.Id;
      rec.Creator = creator;
      return rec;
    }, new { accountId }).ToList();
    return myRecipes;
  }
  internal Favorite GetOne(int id)
  {
    string sql = @"
    SELECT
    *
    FROM favorites
    WHERE id = @id;
    ";
    return _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
  }
  internal void Destroy(int id)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }
}
