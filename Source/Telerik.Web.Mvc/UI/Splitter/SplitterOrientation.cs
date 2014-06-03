﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Specifies the orientation in which the splitter panes will be ordered
    /// </summary>
    public enum SplitterOrientation
    {
        /// <summary>
        /// Panes are oredered horizontally
        /// </summary>
        [ClientSideEnumValue("'horizontal'")]
        Horizontal = 0,

        /// <summary>
        /// Panes are oredered vertically
        /// </summary>
        [ClientSideEnumValue("'vertical'")]
        Vertical = 1
    }
}