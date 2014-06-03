// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public interface IGridRenderer<T> where T : class
    {
        void GridStart();

        void GridEnd();

        void TableStart();

        void TableEnd();

        void HeaderStart();

        void HeaderEnd();

        void HeaderRowStart();

        void HeaderRowEnd();

        void HeaderCellStart(GridColumn<T> column);

        void HeaderCellEnd();

        void HeaderCellContent(GridColumn<T> column);

        void Pager();

        void EmptyRow();

        void FooterStart();

        void FooterEnd();
        
        void Colgroup();

        void FooterRowStart();
        
        void FooterRowEnd();

        void FooterCellStart();

        void FooterCellEnd();

        void BodyStart();

        void BodyEnd();

        void RowStart(GridRowRenderingContext<T> renderingContext);

        void RowEnd();

        void RowCellStart(GridCellRenderingContext<T> renderingContext);

        void RowCellContent(GridCellRenderingContext<T> renderingContext);

        void RowCellEnd();

		void LoadingIndicator();
    }
}