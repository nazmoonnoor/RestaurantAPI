using AutoMapper;
using RestaurantAPI.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAPI.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Domain.DishMenu, Core.Client.DishMenu>()
                .ForMember(x => x.PreparationTime, opt => opt.MapFrom((src, des) =>
                    {
                        return src.PreparationTime.ToString();
                    }
                ))
                .ReverseMap()
                .ForMember(x => x.PreparationTime, opt => opt.MapFrom((src, des) =>
                  {
                      TimeSpan preparationTime;
                      if (!TimeSpan.TryParse(src.PreparationTime, out preparationTime))
                          throw new ArgumentException("Preparation time is not in valid format.");
                      return preparationTime;
                  }));
        }
    }
}
