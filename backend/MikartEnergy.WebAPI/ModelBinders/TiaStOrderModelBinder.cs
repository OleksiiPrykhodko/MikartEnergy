using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using MikartEnergy.Common.DTO.Configurator;

namespace MikartEnergy.WebAPI.ModelBinders
{
    public class TiaStOrderModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var form = bindingContext.HttpContext.Request.Form;

            if(form.TryGetValue("result", out StringValues resultValue))
            {
                var resultAsString = resultValue.ToString();
                var result = JsonSerializer.Deserialize<TiaStResultDTO[]>(resultAsString);
                if (result is null)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.CompletedTask;
                }
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }

}
