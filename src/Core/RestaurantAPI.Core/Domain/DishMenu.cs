using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAPI.Core.Domain
{
    /// <summary>
    /// Represents the dish (menu) model
    /// </summary>
    public class DishMenu
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null;
        public string ShortDescription { get; set; } = null;
        public double Price { get; set; }
        public string[] Category { get; set; }
        public string[] MealTime { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan PreparationTime { get; set; }
    }
}

//a.name
//b. short description
//c.price
//d. category (starter, main course, dessert, beverage, ...)
//e.when it is available based on time of day (breakfast, dinner, lunch, weekdays/-
//ends)
//f.it should be able to deactivate a dish, for example when it’s sold out
//g. how long the guest approximately has to wait for the dish after they order
//If you can think of more properties that are needed, you can include them.
