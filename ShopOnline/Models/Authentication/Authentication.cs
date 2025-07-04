using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopOnline.Models.Authentication
{
    public class Authentication: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("FullName") == null)
            {
               context.Result=new RedirectToRouteResult(new RouteValueDictionary
               {
                   {"controller","Access"},
                   {"action","Login"}
               });
            }
        }
    }
    
    
}
