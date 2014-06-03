namespace Telerik.Web.Mvc.UnitTest
{
    using Xunit;

    public class SiteMapManagerTests
    {
        public SiteMapManagerTests()
        {
            SiteMapManager.Clear();
        }

        [Fact]
        public void SiteMaps_should_not_be_null()
        {
            Assert.NotNull(SiteMapManager.SiteMaps);
        }
    }
}