using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperConfig
    {
        [ExcludeFromCodeCoverage]
        public MapperConfig()
        {
        }

        [ExcludeFromCodeCoverage]
        public static IMapper GetMapper()
        {

            List<Type> profiles = new List<Type>();
            profiles.AddRange(Lingva.BC.Mapper.MapperConfig.GetProfiles());
            profiles.AddRange(Lingva.Web.App.Models.Mapper.MapperConfig.GetProfiles());

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
