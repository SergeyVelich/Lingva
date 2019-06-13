using AutoMapper;
using Lingva.BC.Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper
{
    [ExcludeFromCodeCoverage]
    public class AppMapperConfig
    {
        public AppMapperConfig()
        {
        }

        public static IMapper GetMapper()
        {
            List<Type> profiles = new List<Type>();
            profiles.AddRange(MapperConfig.GetProfiles());
            profiles.AddRange(Models.Mapper.MapperConfig.GetProfiles());

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            });
            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
