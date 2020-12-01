using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunaVayne.Api.ModelBinder
{

    public class GeneralCommandModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var requestReader = new StreamReader(bindingContext.ActionContext.HttpContext.Request.Body);
            var requestContent = await requestReader.ReadToEndAsync();

            var subType = bindingContext.ModelType.GenericTypeArguments.First();
            var subInstance = JsonConvert.DeserializeObject(requestContent, subType);

            var instance = Activator.CreateInstance(bindingContext.ModelType);
            var propertyInfo = bindingContext.ModelType.GetProperty("Value");
            propertyInfo.SetValue(instance, subInstance, null);

            bindingContext.Result = ModelBindingResult.Success(instance);

        }
    }
}
