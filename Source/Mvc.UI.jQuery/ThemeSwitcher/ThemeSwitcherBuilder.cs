// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for theme switcher
    /// </summary>
    public class ThemeSwitcherBuilder : ViewComponentBuilderBase<ThemeSwitcher, ThemeSwitcherBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSwitcherBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public ThemeSwitcherBuilder(ThemeSwitcher component) : base(component)
        {
        }

        /// <summary>
        /// Specify the initials theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder InitialTheme(string theme)
        {
            Component.InitialTheme = theme;

            return this;
        }

        /// <summary>
        /// Specify the height the theme switcher.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder Height(int value)
        {
            Component.Height = value;

            return this;
        }

        /// <summary>
        /// Specify Width of the theme switcher.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder Width(int value)
        {
            Component.Width = value;

            return this;
        }

        /// <summary>
        /// Specify the initial text of the theme switcher
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder InitialText(string text)
        {
            Component.InitialText = text;

            return this;
        }

        /// <summary>
        /// Specify the button pre text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreText", Justification = "To align with the same parameter name of client side jQuery object.")]
        public virtual ThemeSwitcherBuilder ButtonPreText(string text)
        {
            Component.ButtonPreText = text;

            return this;
        }

        /// <summary>
        /// If set select pane will be closed after a theme is selected .
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder CloseOnSelect(bool value)
        {
            Component.CloseOnSelect = value;

            return this;
        }

        /// <summary>
        /// Specify the button height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder ButtonHeight(int height)
        {
            Component.ButtonHeight = height;

            return this;
        }

        /// <summary>
        /// Specify the cookie name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder CookieName(string name)
        {
            Component.CookieName = name;

            return this;
        }

        /// <summary>
        /// Specify the action to be called when theme switcher is opened.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder OnOpen(Action javaScript)
        {
            Component.OnOpen = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the action to be called when theme switcher is selected.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder OnSelect(Action javaScript)
        {
            Component.OnSelect = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the action to be called when theme switcher is closed.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ThemeSwitcherBuilder OnClose(Action javaScript)
        {
            Component.OnClose = javaScript;

            return this;
        }
    }
}