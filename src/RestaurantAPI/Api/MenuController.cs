using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Domain;
using Domain = RestaurantAPI.Core.Domain;
using Client = RestaurantAPI.Core.Client;
using RestaurantAPI.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace RestaurantAPI.Api
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;
        public MenuController(IMapper mapper, IMenuService menuService)
        {
            _mapper = mapper;
            _menuService = menuService;
        }

        /// <summary>
        /// Retrieve all menus 
        /// </summary>
        /// <returns>If any, list of menus provided</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Client.DishMenu[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<List<Client.DishMenu>> Get()
        {
            var menus = await _menuService.GetAsync();
            return _mapper.Map<List<Client.DishMenu>>(menus);
        }

        /// <summary>
        /// Retrieve menu by id
        /// </summary>
        /// <param name="id">Menu identifier</param>
        /// <returns>If exist, it returns the menu</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Client.DishMenu), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Client.DishMenu>> Get(string id)
        {
            var menu = await _menuService.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            return _mapper.Map<Client.DishMenu>(menu); ;
        }

        /// <summary>
        /// Creates a new menu
        /// </summary>
        /// <param name="newMenu">Menu payload</param>
        /// <returns>If created, return the new menu</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Client.DishMenu), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(Client.DishMenu newMenu)
        {
            var menu = _mapper.Map<Domain.DishMenu>(newMenu);

            await _menuService.CreateAsync(menu);

            return CreatedAtAction(nameof(Get), new { id = menu.Id },
                _mapper.Map<Client.DishMenu>(menu));
        }
        
        /// <summary>
        /// Make changes of an existing menu
        /// </summary>
        /// <param name="id">Menu identifier</param>
        /// <param name="updatedMenu">Updated menu payload</param>
        /// <returns>Returns no contents</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(string id, Client.DishMenu updatedMenu)
        {
            var updated = await _menuService.GetAsync(id);

            if (updated is null)
            {
                return NotFound();
            }

            updatedMenu.Id = updated.Id;

            var menu = _mapper.Map<Domain.DishMenu>(updatedMenu);

            await _menuService.UpdateAsync(id, menu);

            return NoContent();
        }

        /// <summary>
        /// Removes a menu
        /// </summary>
        /// <param name="id">Menu identifier</param>
        /// <returns>Returns no contents</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var menu = await _menuService.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            await _menuService.RemoveAsync(menu.Id);

            return NoContent();
        }
    }
}
