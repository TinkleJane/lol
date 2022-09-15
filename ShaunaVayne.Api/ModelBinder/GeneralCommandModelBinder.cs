using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            if (string.IsNullOrEmpty(requestContent))
            {
                var queryList = bindingContext.ActionContext.HttpContext.Request.Query;
                var json = "{";
                for (var i = 0; i < queryList.Count; i++)
                {
                    var q = queryList.ElementAt(i);
                    json += $"{q.Key.Replace("Value.", "")}:'{q.Value}'";
                    if (i < queryList.Count - 1)
                    {
                        json += ", ";
                    }
                }

                json += "}";
                requestContent = json;
            }

            var subType = bindingContext.ModelType.GenericTypeArguments.First();
            var subInstance = JsonConvert.DeserializeObject(requestContent, subType);
            
            var x = bindingContext.ModelState.IsValid;

            var instance = Activator.CreateInstance(bindingContext.ModelType);
            var propertyInfo = bindingContext.ModelType.GetProperty("Value");
            propertyInfo.SetValue(instance, subInstance, null);

            bindingContext.Result = ModelBindingResult.Success(instance);

        }
    }
}
