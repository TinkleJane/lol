using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ShaunaVayne.Bus.Command;
using System;
using System.Linq;

namespace ShaunaVayne.Api.ModelBinder
{
    public class GeneralCommandModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var isGeneralCommand = context.Metadata.ModelType.GetInterfaces()
                .Any(x => x.IsGenericType &&
                          x.GetGenericTypeDefinition() == typeof(ICommand<>));

            if (isGeneralCommand)
            {
                return new BinderTypeModelBinder(typeof(GeneralCommandModelBinder));
            }

            return null;
        }
    }
}
