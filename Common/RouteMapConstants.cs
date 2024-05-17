namespace Starting_Project.Common
{
    public class RouteMapConstants
    {
        public const string BaseRoute = "api";
        public const string CreateApplicationRoute = BaseRoute + "/createApplication";
        public const string EditApplicationRoute = BaseRoute + "/editApplication/{id}";
        public const string GetApplicationRoute = BaseRoute + "/getApplication/{id}";
        public const string SubmitApplicationRoute = BaseRoute + "/submitApplication";
    }
}
