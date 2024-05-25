namespace Menu.Models
{
    public class DishIngredient
    {
        public int DishId { get; set; }
        public Dish? Dish { get; set; } // Non-nullable Dish property

        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}

