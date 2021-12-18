using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Services
{
    public interface IMenuService
    {
        Task<List<Domain.DishMenu>> GetAsync();
        Task<Domain.DishMenu> GetAsync(string id);
        Task CreateAsync(Domain.DishMenu menu);
        Task UpdateAsync(string id, Domain.DishMenu menu);
        Task RemoveAsync(string id);
    }
}
