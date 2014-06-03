namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    public class GridRowRenderingContext<T> where T : class
    {
        public GridRowRenderingContext(T dataItem, int index)
        {
            DataItem = dataItem;
            Index = index;
            IsAlternate = ((index % 2) != 0);
            HtmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public T DataItem
        {
            get;
            private set;
        }

        public int Index
        {
            get;
            private set;
        }

        public bool IsAlternate
        {
            get;
            private set;
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }
    }
}