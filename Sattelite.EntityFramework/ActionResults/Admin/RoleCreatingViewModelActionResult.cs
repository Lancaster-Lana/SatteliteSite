namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.Entities.ProjectAgg;
    using Sattelite.Entities.UserAgg;
    using Sattelite.Framework;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;
    using Sattelite.EntityFramework.Repository;

    public class RoleCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly IRoleRepository _roleRepository;

        public RoleCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression) : this(viewNameExpression,
                   DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        public RoleCreatingViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IRoleRepository roleRepository)
            : base(viewNameExpression)
        {
             this._roleRepository = roleRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new RoleCreatingViewModel();

            //viewModel.Permissiions = this._roleRepository.GetPermissions().ToList();

            this.GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._roleRepository, "RoleRepository");
            //Guard.ArgumentMustMoreThanZero(this._roleId, "RoleId");
        }

        #endregion
    }
}