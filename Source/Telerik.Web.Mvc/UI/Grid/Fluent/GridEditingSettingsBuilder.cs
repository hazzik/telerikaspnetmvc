// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring grid editing.
    /// </summary>
    public class GridEditingSettingsBuilder : IHideObjectMembers
    {
        private readonly GridEditingSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridEditingSettingsBuilder"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public GridEditingSettingsBuilder(GridEditingSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Enables or disables grid editing.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Orders")
        ///             .Editable(settings => settings.Enabled(true))
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// The Enabled method is useful when you need to enable grid editing on certain conditions.
        /// </remarks>
        public GridEditingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;
            
            return this;
        }

        public GridEditingSettingsBuilder Mode(GridEditMode mode)
        {
            settings.Mode = mode;

            return this;
        }

        public GridEditingSettingsBuilder Window(Action<WindowBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new WindowBuilder(settings.PopUp));

            return this;
        }

#if MVC2 || MVC3
        /// <summary>
        /// Specify an editor template which to be used for InForm or PopUp modes
        /// </summary>
        /// <param name="templateName">name of the editor template</param>
        /// <remarks>This settings is applicable only when Mode is <see cref="GridEditMode.InForm"/> 
        /// or <see cref="GridEditMode.PopUp"/></remarks>
        public GridEditingSettingsBuilder TemplateName(string templateName)
        {
            Guard.IsNotNullOrEmpty(templateName, "templateName");

            settings.TemplateName = templateName;
            return this;
        }
#endif
        /// <summary>
        /// Enables or disables delete confirmation.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Orders")
        ///             .Editable(settings => settings.DisplayDeleteConfirmation(true))
        /// %&gt;
        /// </code>
        /// </example>
        public GridEditingSettingsBuilder DisplayDeleteConfirmation(bool value)
        {
            settings.DisplayDeleteConfirmation = value;
            
            return this;
        }

        /// <summary>
        /// Gets the HTML attributes of the form rendered during editing
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        public GridEditingSettingsBuilder FormHtmlAttributes(object attributes)
        {
            MergeAttributes(settings.FormHtmlAttributes, attributes);

            return this;
        }

        private static void MergeAttributes(IDictionary<string, object> target, object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            target.Clear();
            target.Merge(attributes);
        }
    }
}
