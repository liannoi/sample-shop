namespace Shop.WebUI.Controllers.Helpers
{
    public interface IControllerHelper
    {
        void CheckModelState();
        void PrepareViewBagsForCreate(string pageMessage);
        void PrepareViewBagsForUpdate(string pageMessage);
    }
}