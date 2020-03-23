using System.Web.Mvc;
using Shop.WebUI.Exceptions;

namespace Shop.WebUI.Controllers.Helpers
{
    public sealed class ControllerHelper : IControllerHelper
    {
        private readonly IControllerViewBagHelper _viewBagHelper;

        public ControllerHelper(IControllerViewBagHelper viewBagHelper)
        {
            _viewBagHelper = viewBagHelper;
        }

        public void CheckModelState()
        {
            if (!_viewBagHelper.Self.ModelState.IsValid) throw new InvalidModelStateException();
        }

        public void PrepareViewBagsForCreate(string pageMessage)
        {
            _viewBagHelper.PrepareForCreate(pageMessage);
        }

        public void PrepareViewBagsForUpdate(string pageMessage)
        {
            _viewBagHelper.PrepareForUpdate(pageMessage);
        }

        public sealed class ControllerViewBagHelper : IControllerViewBagHelper
        {
            public ControllerViewBagHelper(Controller self)
            {
                Self = self;
            }

            public Controller Self { get; }

            public void PrepareForCreate(string pageMessage)
            {
                Self.ViewBag.Title = "Create";
                Self.ViewBag.PageMessage = pageMessage;
                Self.ViewBag.ButtonText = "Add";
            }

            public void PrepareForUpdate(string pageMessage)
            {
                Self.ViewBag.Title = "Update";
                Self.ViewBag.PageMessage = pageMessage;
                Self.ViewBag.ButtonText = "Save";
            }
        }
    }
}