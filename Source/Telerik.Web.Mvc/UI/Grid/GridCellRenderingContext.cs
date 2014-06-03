namespace Telerik.Web.Mvc.UI
{
    using System;

    public class GridCellRenderingContext<T> : GridRowRenderingContext<T> where T : class
    {
        public GridCellRenderingContext(GridColumn<T> column, T rowItem, int rowIndex) : base(rowItem, rowIndex)
        {
            Column = column;
        }

        public GridColumn<T> Column
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            set;
        }

        public Action<T> Content
        {
            get;
            set;
        }
    }
}