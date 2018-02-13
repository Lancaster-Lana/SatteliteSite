namespace Sattelite.Web.Controllers
{
    using System.Web.Mvc;
    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.ActionResults.Client;

    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var currentUserIdentity = User.Identity;
            return new HomePageViewModelActionResult<HomeController>(x => x.Index(), currentUserIdentity);
        }

        /// <summary>
        /// News\article details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult NewsDetails(int id)
        {
            var currentUserIdentity = User.Identity;
            return new NewsDetailsViewModelActionResult<HomeController>(x => x.NewsDetails(id), currentUserIdentity, id);
        }

        public ActionResult ProjectDetails(int id)
        {
            var currentUserIdentity = User.Identity;
            return new ProjectDetailsViewModelActionResult<HomeController>(x => x.ProjectDetails(id), currentUserIdentity, id);
        }

        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            var currentUserIdentity = User.Identity;
            return new CategoryViewModelActionResult<HomeController>(x => x.Category(id), currentUserIdentity, id);
        }

        [AllowAnonymous]
        public ActionResult ErrorLogin()
        {
            SetErrorMessage("You should login before subscription to the category");
            var currentUserIdentity = User.Identity;
            return PartialView("_LoginRequired");
        }
    }
}
