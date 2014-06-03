// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class GridBindingSettings : IClientSerializable
    {
        private readonly IGrid grid;

        public GridBindingSettings(IGrid grid)
        {
            this.grid = grid;
            Select = new RequestSettings();
            Insert = new RequestSettings();
            Update = new RequestSettings();
            Delete = new RequestSettings();
        }

        public bool Enabled
        {
            get;
            set;
        }

        public RequestSettings Select
        {
            get;
            private set;
        }

        public RequestSettings Insert
        {
            get;
            private set;
        }

        public RequestSettings Update
        {
            get;
            private set;
        }

        public RequestSettings Delete
        {
            get;
            private set;
        }
        
        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (Enabled)
            {
                Func<string,string> encoder = (string url) => grid.IsSelfInitialized ? HttpUtility.UrlDecode(url) : url;

                var urlBuilder = grid.UrlBuilder;
                
                var urls = new Dictionary<string, string>();

                urls["selectUrl"] = encoder(urlBuilder.Url(Select));

                if (Insert.HasValue())
                {
                    urls["insertUrl"] = encoder(urlBuilder.Url(Insert));
                }

                if (Update.HasValue())
                {
                    urls["updateUrl"] = encoder(urlBuilder.Url(Update));
                }

                if (Delete.HasValue())
                {
                    urls["deleteUrl"] = encoder(urlBuilder.Url(Delete));
                }
                
                writer.AppendObject(key, urls);
            }
        }
    }
}
