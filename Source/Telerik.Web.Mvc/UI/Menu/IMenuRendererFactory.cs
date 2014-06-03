// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

using System.Web.UI;

namespace Telerik.Web.Mvc.UI
{
    public interface IMenuRendererFactory
    {
        IMenuRenderer Create(Menu menu, HtmlTextWriter writer);
    }
}