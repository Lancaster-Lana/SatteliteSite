namespace Sattelite.EntityFramework
{
    using System.Security.Authentication;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        protected string GetUserName()
        {
            if(User == null || User.Identity == null)
                throw new AuthenticationException("You should log in to the system");

            return User.Identity.Name;
        }

        protected void SetSucceedMessage(string succeedMsg)
        {
            ViewBag.SucceedMessage = succeedMsg;
        }

        protected void SetErrorMessage(string errorMsg)
        {
            ModelState.AddModelError("", errorMsg);
            ViewBag.ErrorMessage = errorMsg; //TODO: can be removed 
        }

        protected ActionResult ShowErrorMessage(string errorMsg)
        {
            return JavaScript(errorMsg);
        }
    }
}