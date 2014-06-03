namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public interface IMenuItemContainer
    {
        IList<MenuItem> Items
        {
            get;
        }
    }
}