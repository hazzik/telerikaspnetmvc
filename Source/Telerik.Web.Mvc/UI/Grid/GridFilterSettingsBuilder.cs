namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Defines the fluent interface for configuring <see cref="Grid{T}.Filtering"/>.
    /// </summary>
    public class GridFilterSettingsBuilder : IHideObjectMembers
    {
        private GridFilterSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridFilterSettingsBuilder"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public GridFilterSettingsBuilder(GridFilterSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Enables or disables filtering
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Filterable(filtering => filtering.Enabled((bool)ViewData["enableFiltering"]))
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// The Enabled method is useful when you need to enable filtering based on certain conditions.
        /// </remarks>
        public void Enabled(bool value)
        {
            settings.Enabled = value;
        }
    }
}
