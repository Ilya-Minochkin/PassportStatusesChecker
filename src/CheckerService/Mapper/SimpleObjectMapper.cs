using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CheckerService.Mapper
{
    public class SimpleObjectMapper
    {
        public static TResult GetMappedObject<TSource,  TResult>(TSource source, Action<IMapperConfigurationExpression> configAction)
        {
            var config = new MapperConfiguration(configAction);
            var mapper = new AutoMapper.Mapper(config);
            return mapper.Map<TSource, TResult>(source);
        }

        public static TResult GetMappedObject<TSource , TResult>(TSource source)
        {
            return GetMappedObject<TSource, TResult>(source, cfg => cfg.CreateMap<TSource, TResult>());
        }
    }
}
