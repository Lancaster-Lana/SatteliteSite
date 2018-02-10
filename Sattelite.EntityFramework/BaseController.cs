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

        /// <summary>
        /// Display success message on the page
        /// </summary>
        /// <param name="succeedMsg"></param>
        protected void SetSucceedMessage(string succeedMsg)
        {
            ViewBag.SucceedMessage = succeedMsg;
        }

        /// <summary>
        ///  Display error message on the page
        /// </summary>
        /// <param name="errorMsg"></param>
        protected void SetErrorMessage(string errorMsg)
        {
            ModelState.AddModelError("", errorMsg);
            ViewBag.ErrorMessage = errorMsg; //TODO: can be removed 
        }

        protected JavaScriptResult ShowSuccessMessageAlert(string errorMsg)
        {
            string errorScript = string.Format("successMessage('{0}');", errorMsg);
            return JavaScript(errorScript);
        }

        //public ContentResult ShowErrorAlert(string message)
        //{
        //    var script = string.Format(
        //        "<script language='javascript' type='text/javascript'> errorMessage('{0}');</script>", message);
        //    return Content(script);
        //}

        protected JavaScriptResult ShowErrorMessageAlert(string errorMsg)
        {
            string errorScript = string.Format("errorMessage('{0}');", errorMsg);
            return JavaScript(errorScript);
        }
    }
}