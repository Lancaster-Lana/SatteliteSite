namespace Sattelite.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using Sattelite.EntityFramework;
    using Sattelite.EntityFramework.ActionResults.Client;
    using Sattelite.EntityFramework.Repository;
    using System;
    using System.Globalization;

    [Authorize]
    //[RequireHttps]
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
            SetErrorMessage("You should login before subscription on the category");
            var currentUserIdentity = User.Identity;
            return PartialView("_LoginRequired");
        }
    }
}
