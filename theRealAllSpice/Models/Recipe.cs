namespace theRealAllSpice.Models;

public class Recipe
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Instructions { get; set; }
  public string Img { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}
public class MyRecipe : Recipe
{
  public int FavoriteId { get; set; }
}