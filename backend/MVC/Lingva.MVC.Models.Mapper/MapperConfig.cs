using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lingva.MVC.Models.Mapper
{
    [ExcludeFromCodeCoverage]
    public static class MapperConfig
    {
        [ExcludeFromCodeCoverage]
        public static List<Type> GetProfiles()
        {
            var result = new List<Type>();

            var assembliesToScan = new List<Assembly>(new[] { typeof(MapperConfig).GetTypeInfo().Assembly });

            var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles = allTypes
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            foreach (var profile in profiles)
            {
                result.Add(profile);
            }

            return result;
        }
    }
}
