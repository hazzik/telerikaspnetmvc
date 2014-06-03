namespace Telerik.Web.Mvc
{
    public class SiteMapNodeFactory : IHideObjectMembers
    {
        private readonly SiteMapNode parent;

        public SiteMapNodeFactory(SiteMapNode parent)
        {
            Guard.IsNotNull(parent, "parent");

            this.parent = parent;
        }

        public SiteMapNodeBuilder Add()
        {
            SiteMapNode node = new SiteMapNode();

            parent.ChildNodes.Add(node);

            return new SiteMapNodeBuilder(node);
        }
    }
}