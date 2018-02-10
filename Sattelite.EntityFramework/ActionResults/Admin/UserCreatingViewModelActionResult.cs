namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Sattelite.Framework;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels.Admin.User;

    public class UserCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly IUserRepository _userRepository;

        public UserCreatingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression, DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public UserCreatingViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    IUserRepository userRepository) : base(viewNameExpression)
        {
            _userRepository = userRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new UserCreatingViewModel();
            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_userRepository, "UserRepository");
        }
    }
}