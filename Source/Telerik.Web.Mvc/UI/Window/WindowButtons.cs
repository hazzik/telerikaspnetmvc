﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public class WindowButtons : IWindowButtonsContainer
    {
        private IList<IWindowButton> container;

        public WindowButtons()
        {
            container = new List<IWindowButton>();
        }

        public IList<IWindowButton> Container
        {
            get 
            {
                return this.container;
            }
        }
    }
}
