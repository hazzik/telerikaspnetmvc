// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public interface IMenuRenderer
    {
        void MenuStart();

        void MenuEnd();

        void ItemStart(MenuItem item);

        void ItemEnd();

        void Link(MenuItem item);

        void GroupStart();

        void GroupEnd();

        void WriteContent(MenuItem item);
    }
}