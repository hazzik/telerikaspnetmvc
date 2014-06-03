// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public class WindowClientEvents
    {
        public WindowClientEvents()
        {
            OnLoad = new ClientEvent();
            OnOpen = new ClientEvent();
            OnClose = new ClientEvent();
            OnMove = new ClientEvent();
            OnResize = new ClientEvent();
            OnRefresh = new ClientEvent();
            OnError = new ClientEvent();
        }

        public ClientEvent OnLoad { get; private set; }

        public ClientEvent OnOpen { get; private set; }

        public ClientEvent OnClose { get; private set; }

        public ClientEvent OnMove { get; private set; }

        public ClientEvent OnResize { get; private set; }

        public ClientEvent OnRefresh { get; private set; }

        public ClientEvent OnError { get; private set; }
    }
}