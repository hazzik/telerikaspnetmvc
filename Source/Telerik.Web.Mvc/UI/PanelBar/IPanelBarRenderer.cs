// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public interface IPanelBarRenderer
    {
        void PanelBarStart();

        void PanelBarEnd();

        void ListGroupStart(PanelBarItem item);

        void ListGroupEnd();

        void ListItemStart(PanelBarItem item);

        void ListItemEnd();

        void HeaderItemContent(PanelBarItem item);

        void ItemContent(PanelBarItem item);

        void WriteContent(PanelBarItem item);
    }
}