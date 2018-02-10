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

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return new DetailsViewModelActionResult<HomeController>(x => x.Details(id), id);
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
