using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Lingva.ASP.Infrastructure.Binders
{
    public class OptionsModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //if (context.Metadata.ModelType == typeof(OptionsModel))
            //{
            //    return new BinderTypeModelBinder(typeof(OptionsModelBinder));
            //}

            return null;
        }
    }
}
