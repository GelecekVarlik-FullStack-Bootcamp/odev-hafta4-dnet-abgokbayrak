using AutoMapper;
using BMS.Entity.Mapper;
using System;

namespace BMS.Bll.Mapper
{
    internal class ObjectMapper
    {
        static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        }
        );

        public static IMapper Mapper => lazy.Value;
    }
}
