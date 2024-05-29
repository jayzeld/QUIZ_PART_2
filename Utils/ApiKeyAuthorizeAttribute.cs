using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIDiscussion.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private const string API_KEY_NAME = "X-AUF-API-KEY";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME,out var extractedAPIKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API key was not provide"
                };
                return;
            }

            var apiKey = "HELLOAPIKEY";
            if (!apiKey.Equals(extractedAPIKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Client"
                };
                return;
            }
            
        }
    }
}
