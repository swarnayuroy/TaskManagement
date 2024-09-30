using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace API_Service.Utils.Mapper
{
    public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
    public class MapModel: IMapperService
    {
        private static AutoMapper.Mapper _config; 

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            _config = new AutoMapper.Mapper(
                new MapperConfiguration(
                    cfg => cfg.CreateMap<TSource, TDestination>().ReverseMap()
                )
            );
            return _config.Map<TDestination>(source);
        }
    }
}