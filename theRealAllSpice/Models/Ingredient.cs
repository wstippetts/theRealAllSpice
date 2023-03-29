namespace theRealAllSpice.Models;

public class Ingredient
{
  public int Id { get; set; }
  public int RecipeId { get; set; }
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Quantity { get; set; }
  public Account Creator { get; set; }

}
