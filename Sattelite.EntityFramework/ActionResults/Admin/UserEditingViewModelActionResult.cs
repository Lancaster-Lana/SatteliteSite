using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;
using Sattelite.Entities.UserAgg;
using Sattelite.Framework;
using Sattelite.Framework.Extensions;
using Sattelite.EntityFramework.ViewModels.Admin.User;
using Sattelite.Entities.ProjectAgg;
using Sattelite.EntityFramework.Repository;

namespace Sattelite.EntityFramework.ActionResults.Admin
{
    public class UserEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables

        private readonly int _userId;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Ctror

        public UserEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId,
                  DependencyResolver.Current.GetService<IUserRepository>(),
                  DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public UserEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, int userId,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
            : base(viewNameExpression)
        {
            _userId = userId;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var user = _userRepository.GetById(_userId);

            if (user == null)
                throw new NoNullAllowedException(string.Format("User with id={0}", _userId).ToNotNullErrorMessage());

            var viewModel = user.MapTo<UserEditingViewModel>();

            GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_userRepository, "UserRepository");
            Guard.ArgumentMustMoreThanZero(_userId, "UserId");
        }
    }
}