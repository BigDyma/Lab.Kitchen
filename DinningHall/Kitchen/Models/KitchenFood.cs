namespace Kitchen.Models
{
    public class KitchenFood : Food 
    {
        public KitchenFoodState State { get; set; }

        public KitchenFood(Food food)
        {
            this.Name = food.Name;
            this.PreparationTime = food.PreparationTime;
            this.CookingApparatus = food.CookingApparatus;
            this.Complexity = food.Complexity;
            State = KitchenFoodState.NotStarted;
        }
    }
}