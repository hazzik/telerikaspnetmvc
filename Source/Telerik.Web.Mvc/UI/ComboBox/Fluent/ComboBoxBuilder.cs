﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="ComboBox"/> component.
    /// </summary>
    public class ComboBoxBuilder : DropDownBuilderBase<ComboBox, ComboBoxBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public ComboBoxBuilder(ComboBox component)
            : base(component)
        {
        }


        /// <summary>
        /// Use it to enable filtering of items.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .Filterable();
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder Filterable()
        {
            Component.Filtering.Enabled = true;

            return this;
        }

        /// <summary>
        /// Use it to configure filtering settings.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .Filterable(filtering => filtering.Enabled(true)
        ///                                               .FilterMode(AutoCompleteFilterMode.Contains));
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder Filterable(Action<ComboBoxFilterSettingsBuilder> filtering)
        {
            Guard.IsNotNull(filtering, "filtering");

            filtering(new ComboBoxFilterSettingsBuilder(Component.Filtering));

            return this;
        }

        /// <summary>
        /// Use it to enable filling the first matched item text.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .AutoFill(true)
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder AutoFill(bool autoFill)
        {
            Guard.IsNotNull(autoFill, "autoFill");

            Component.AutoFill = autoFill;

            return this;
        }

        /// <summary>
        /// Use it to configure Data binding.
        /// </summary>
        /// <param name="configurator">Action that configures the data binding options.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .DataBinding(dataBinding => dataBinding
        ///                .Ajax().Select("_AjaxLoading", "ComboBox")
        ///             );
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder DataBinding(Action<AutoCompleteDataBindingConfigurationBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new AutoCompleteDataBindingConfigurationBuilder(Component.DataBinding));

            return this;
        }

        /// <summary>
        /// Use it to enable highlighting of first matched item.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .HighlightFirstMatch(true)
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder HighlightFirstMatch(bool highlightFirstMatch)
        {
            Guard.IsNotNull(highlightFirstMatch, "highlightFirstMatch");

            Component.HighlightFirstMatch = highlightFirstMatch;

            return this;
        }

        /// <summary>
        /// Use it to set selected item index
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .SelectedIndex(0);
        /// %&gt;
        /// </code>
        /// </example>
        public ComboBoxBuilder SelectedIndex(int index)
        {
            if(index != -1) Guard.IsNotNegative(index, "index");

            Component.SelectedIndex = index;

            return this;
        }
    }
}
