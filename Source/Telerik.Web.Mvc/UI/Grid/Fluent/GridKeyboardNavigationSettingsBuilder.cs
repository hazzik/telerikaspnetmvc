// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring <see cref="Grid{T}.KeyboardNavigation"/>
    /// </summary>
    public class GridKeyboardNavigationSettingsBuilder : IHideObjectMembers
    {
        private readonly GridKeyboardNavigationSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridKeyboardNavigationSettingsBuilder"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public GridKeyboardNavigationSettingsBuilder(GridKeyboardNavigationSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Enables or disables keyboard navigation.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .KeyboardNavigation(setting => setting.Enabled((bool)ViewData["enableKeyBoardNavigation"]))
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// The Enabled method is useful when you need to enable keyboard navigation based on certain conditions.
        /// </remarks>
        public virtual GridKeyboardNavigationSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;

            return this;
        }
    }
}
