namespace FastMeiliSync.Shared.Router;

public static class Router
{
    const string BASE_URL = "meilisync/api";

    public static class RoleRoutes
    {
        public static class V1
        {
            public const string BASE_ROLE_URL = $"{BASE_URL}/v1/role";
            public const string Seed = "seed";
            public const string Paginate = "paginate";
        }
    }

    public static class UserRoutes
    {
        public static class V1
        {
            public const string BASE_USER_URL = $"{BASE_URL}/v1/user";
            public const string LogIn = "login";
            public const string Seed = "seed";
            public const string Logout = "logout";
            public const string Paginate = "paginate";
        }
    }

    public static class SourceRoutes
    {
        public static class V1
        {
            public const string BASE_SOURCE_URL = $"{BASE_URL}/v1/source";
            public const string Paginate = "paginate";
        }
    }

    public static class MeiliSearchRoutes
    {
        public static class V1
        {
            public const string BASE_MEILISEARCH_URL = $"{BASE_URL}/v1/meilisearch";
            public const string Paginate = "paginate";
        }
    }

    public static class SyncRoutes
    {
        public static class V1
        {
            public const string BASE_SYNC_URL = $"{BASE_URL}/v1/sync";
            public const string Paginate = "paginate";
            public const string Stop = "stop";
            public const string Start = "start";
        }
    }
}
