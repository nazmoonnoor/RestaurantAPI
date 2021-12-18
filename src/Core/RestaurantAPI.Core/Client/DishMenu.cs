using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAPI.Core.Client
{
    public class DishMenu
    {
        public string? Id { get; set; }

        public string Name { get; set; } = null;
        public string ShortDescription { get; set; } = null;
        public double Price { get; set; }
        public string[] Category { get; set; }
        public string[] MealTime { get; set; }
        public bool IsActive { get; set; }
        public string PreparationTime { get; set; } = null;
    }
}
