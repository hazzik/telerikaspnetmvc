// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    public static class CalendarExtension
    {
        public static DateTime? DetermineFocusedDate(this Calendar calendar)
        {
            DateTime? focusedDate = DateTime.Today;
            if (calendar.Value != null) {
                focusedDate = calendar.Value;
            }

            if (calendar.MinDate > focusedDate.Value) 
            {
                focusedDate = calendar.MinDate;
            }
            else if (calendar.MaxDate < focusedDate.Value) 
            {
                focusedDate = calendar.MaxDate;
            }

            return focusedDate;
        }
    }
}
