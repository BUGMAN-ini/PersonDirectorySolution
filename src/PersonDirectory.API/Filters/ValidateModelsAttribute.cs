using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using PersonDirectory.API.Resources;

namespace PersonDirectory.API.Filters
{
    public class ValidateModelsAttribute : ActionFilterAttribute
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ValidateModelsAttribute(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(new { Errors = errors });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
