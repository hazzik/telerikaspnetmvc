using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    public interface ITreeViewHtmlBuilder
    {
        IHtmlNode TreeViewTag();

        IHtmlNode ItemTag(TreeViewItem item);

        IHtmlNode ItemInnerContent(TreeViewItem item);

        IHtmlNode ItemContentTag(TreeViewItem item);

        IHtmlNode ChildrenTag(TreeViewItem item);
    }
}