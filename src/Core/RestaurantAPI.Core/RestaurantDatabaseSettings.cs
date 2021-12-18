using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAPI.Core
{
    public class RestaurantDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MenuCollectionName { get; set; } = null!;
    }
}
