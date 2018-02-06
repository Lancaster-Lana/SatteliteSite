namespace Sattelite.EntityFramework.ActionResults.Client
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.EntityFramework.ViewModels;

    /// <summary>
    /// Action result for list of application roles
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    public class RolesViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly int _roleId;
        private readonly IRoleRepository _roleRepository;
        private readonly string _titleSearchText;
        private readonly int _currentPage;
        private readonly int _numOfPage;

        public RolesViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                string titleSearchText,
                int currentPage,
                int roleId = 0)
            : this(viewNameExpression,
                   titleSearchText,
                   currentPage,
                   roleId,
                   DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        public RolesViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    string titleSearchText,
                    int currentPage,
                    int roleId,
                    IRoleRepository roleRepository)
            : base(viewNameExpression)
        {
            _roleRepository = roleRepository;
            _roleId = roleId;

            _titleSearchText = titleSearchText;
            _currentPage = currentPage;

            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            int numOfRecords = 10; //TODO:

            var roles = _roleRepository.GetRoles();
            var rolePageViewModel = new RoleListViewModel
            {
                Roles = roles?.ToList(),
                PagingData = new PagingViewModel(_currentPage, _numOfPage, numOfRecords,
                                                  string.Format("{0}","/Admin/Role/Index/{page}"), null)
            };
            GetViewResult(rolePageViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(_roleRepository, "RoleRepository");
            Guard.ArgumentMustMoreThanZero(_numOfPage, "NumOfPage");
        }
    }
}