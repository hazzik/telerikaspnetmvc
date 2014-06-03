namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    public class AuthorizationInfo
    {
        private static readonly AuthorizationInfo empty = new AuthorizationInfo(string.Empty, string.Empty);

        public AuthorizationInfo(string users, string roles)
        {
            AllowedUsers = users ?? string.Empty;
            AllowedRoles = roles ?? string.Empty;
        }

        public static AuthorizationInfo Empty
        {
            [DebuggerStepThrough]
            get
            {
                return empty;
            }
        }

        public string AllowedUsers
        {
            get;
            private set;
        }

        public string AllowedRoles
        {
            get;
            private set;
        }
    }
}