namespace Telerik.Web.Mvc
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IGridModel
    {
        int Total
        {
            get;
        }
        
        IEnumerable Data
        {
            get;
        }
    }
    
    public class GridModel : IGridModel
    {
        public IEnumerable Data
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }
    }

    public class GridModel<T> : IGridModel
    {
        public IEnumerable<T> Data
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }

        IEnumerable IGridModel.Data
        {
            get 
            {
                return Data;
            }
        }
    }
}