namespace Shop.WebUI.ClientApp
{
    public static class Consts
    {
        #region User-side

        public static string SessionCartName => "Cart";

        #endregion

        #region Paths

        public static string NotPhotoPath => $"{RequestStatic}/not-photo.png";
        public static string GoodsPhotosDirectory => $"{RequestRoot}/img-goods";

        #region Helpers

        private static string RequestRoot => "/ClientApp/res";
        private static string RequestStatic => $"{RequestRoot}/static";

        #endregion

        #endregion
    }
}