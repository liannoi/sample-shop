using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.WebUI.Identity.Stores
{
    public class ShopIdentityContextInitializer : DropCreateDatabaseIfModelChanges<ShopIdentityContext>
    {
        protected override async void Seed(ShopIdentityContext context)
        {
            await Task.Factory.StartNew(SeedAsync);
            base.Seed(context);
        }

        #region Helpers

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private async Task SeedAsync()
        {
            await SendCreateRoleRequest(Consts.UserRoleName);
            await SendCreateRoleRequest(Consts.AdministratorRoleName);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private async Task SendCreateRoleRequest(string roleName)
        {
            await new HttpClient().GetAsync($"{Consts.RootUrl}/Identity/CreateRole?roleName={roleName}");
        }

        #endregion
    }
}