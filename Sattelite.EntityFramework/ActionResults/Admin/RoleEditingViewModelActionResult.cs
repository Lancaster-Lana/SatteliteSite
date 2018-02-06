namespace Sattelite.EntityFramework.ActionResults.Admin
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sattelite.EntityFramework.Repository;
    using Sattelite.Framework;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.ViewModels.Admin.Role;

    public class RoleEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly IRoleRepository _roleRepository;
        private readonly int _roleId;

        public RoleEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId, DependencyResolver.Current.GetService<IRoleRepository>())
        {
        }

        public RoleEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression, int roleId, IRoleRepository roleRepository)
            : base(viewNameExpression)
        {
            this._roleRepository = roleRepository;
            this._roleId = roleId;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var item = this._roleRepository.GetById(this._roleId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", this._roleId).ToNotNullErrorMessage());

            var viewModel = item.MapTo<RoleEditingViewModel>();

            viewModel.Name = item.Name;
            viewModel.Description = item.Description;
            //TODO: permissions

            this.GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._roleRepository, "RoleRepository");
            Guard.ArgumentMustMoreThanZero(this._roleId, "RoleId");
        }

        #endregion
    }
}