using Lingva.WebAPI.Models.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Lingva.WebAPI.Infrastructure
{
    public class OptionsModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(OptionsModel))
            {
                return new BinderTypeModelBinder(typeof(OptionsModelBinder));
            }

            return null;
        }
    }
}
