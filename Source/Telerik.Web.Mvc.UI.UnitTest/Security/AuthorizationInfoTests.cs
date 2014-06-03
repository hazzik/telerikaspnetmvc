namespace Telerik.Web.Mvc.UnitTest
{
    using Xunit;

    public class AuthorizationInfoTests
    {
        private const string Users = "Mort, Elvis, Einstein";
        private const string Roles = "User, Power User, Admin";

        private readonly AuthorizationInfo _authorizationInfo;

        public AuthorizationInfoTests()
        {
            _authorizationInfo = new AuthorizationInfo(Users, Roles);
        }

        [Fact]
        public void Empty_should_should_return_empty_users_and_role()
        {
            Assert.Equal(string.Empty, AuthorizationInfo.Empty.AllowedUsers);
            Assert.Equal(string.Empty, AuthorizationInfo.Empty.AllowedRoles);
        }

        [Fact]
        public void AllowedUsers_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal(Users, _authorizationInfo.AllowedUsers);
        }

        [Fact]
        public void AllowedRoles_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal(Roles, _authorizationInfo.AllowedRoles);
        }
    }
}