using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace API_Service.Utils.Mapper
{
    public class MapModel<TSource, TDestination>
    {
        private static AutoMapper.Mapper _mapper = new AutoMapper.Mapper(
            new MapperConfiguration(
                config => config.CreateMap<TSource, TDestination>().ReverseMap()
            )
        );

        public static TDestination Map(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}