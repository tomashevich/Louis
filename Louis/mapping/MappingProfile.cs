using AutoMapper;
using Louis.Entities;
using Louis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Louis.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {                   // source, destination
            CreateMap<Entities.Product, Models.Product>()
                .ForMember(dest=> dest.LastUpdated, opt=>opt.MapFrom(src =>src.ModifiedOn))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Models.Product, Entities.Product>()
                  .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => src.LastUpdated))
            
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Photo));
          
        }
    }
}
