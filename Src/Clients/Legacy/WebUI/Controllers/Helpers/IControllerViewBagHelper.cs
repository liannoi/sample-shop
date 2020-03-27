using System.Web.Mvc;

namespace Shop.Legacy.WebUI.Controllers.Helpers
{
    public interface IControllerViewBagHelper
    {
        Controller Self { get; }
        void PrepareForCreate(string pageMessage);
        void PrepareForUpdate(string pageMessage);
    }
}