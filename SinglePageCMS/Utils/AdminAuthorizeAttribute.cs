using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Requires an admin session for all actions on <see cref="BaseController"/> unless <see cref="AllowAnonymousAttribute"/> is present.
/// </summary>
public class AdminAuthorizeAttribute : AuthorizeAttribute {

    public override void OnAuthorization(AuthorizationContext filterContext) {
        if (filterContext == null) {
            return;
        }
        if (SkipAuthorization(filterContext)) {
            return;
        }
        if (Util.oturumAcikmi()) {
            return;
        }
        filterContext.Result = new RedirectResult(VirtualPathUtility.ToAbsolute("~/admin"));
    }

    private static bool SkipAuthorization(AuthorizationContext filterContext) {
        if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) {
            return true;
        }
        if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) {
            return true;
        }
        return filterContext.ActionDescriptor.GetCustomAttributes(typeof(ChildActionOnlyAttribute), true).Any();
    }
}
