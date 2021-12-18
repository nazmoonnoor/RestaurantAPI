using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RestaurantAPI.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMongoCollection<Domain.DishMenu> _menuCollection;

        public MenuService(
            IOptions<RestaurantDatabaseSettings> restaurantDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                restaurantDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                restaurantDatabaseSettings.Value.DatabaseName);

            _menuCollection = mongoDatabase.GetCollection<Domain.DishMenu>(
                restaurantDatabaseSettings.Value.MenuCollectionName);
        }

        public async Task<List<Domain.DishMenu>> GetAsync()
        {
            return await _menuCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Domain.DishMenu> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Menu Id are missing");

            if (!ObjectId.TryParse(id, out _))
                throw new ArgumentException("Menu Id is not valid");

            return await _menuCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Domain.DishMenu menu)
        {
            if (menu == null)
                throw new ArgumentException("Menu details are missing");

            if (menu.Name == null)
                throw new ArgumentException("Menu name are missing");

            if (menu.Price == 0 || menu.Price < 0)
                throw new ArgumentException("Menu price are missing");

            if (menu.Id != null)
                throw new ArgumentException("Menu Id should not be given.");

            menu.Id = ObjectId.GenerateNewId().ToString();
            
            await _menuCollection.InsertOneAsync(menu);
        }
        public async Task UpdateAsync(string id, Domain.DishMenu menu)
        {
            if (menu == null)
                throw new ArgumentException("Menu details are missing");

            if (menu.Name == null)
                throw new ArgumentException("Menu name are missing");

            if (menu.Price == 0 || menu.Price < 0)
                throw new ArgumentException("Menu price are missing");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Menu Id is missing");

            if (!ObjectId.TryParse(id, out _))
                throw new ArgumentException("Menu Id is not valid");

            await _menuCollection.ReplaceOneAsync(x => x.Id == id, menu);
        }

        public async Task RemoveAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Menu Id is missing");
            
            if (!ObjectId.TryParse(id, out _))
                throw new ArgumentException("Menu Id is not valid");

            await _menuCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
