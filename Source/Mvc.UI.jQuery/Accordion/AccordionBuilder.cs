// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for accordion.
    /// </summary>
    public class AccordionBuilder : ViewComponentBuilderBase<Accordion, AccordionBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public AccordionBuilder(Accordion component) : base(component)
        {
        }

        /// <summary>
        /// Specify the accordion items 
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <returns></returns>
        public virtual AccordionBuilder Items(Action<AccordionItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            AccordionItemFactory factory = new AccordionItemFactory(Component);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Animates accordion with the specified effect name.
        /// </summary>
        /// <param name="effectName">Name of the effect.</param>
        /// <returns></returns>
        public virtual AccordionBuilder Animate(string effectName)
        {
            Component.AnimationName = effectName;

            return this;
        }

        /// <summary>
        /// If set, the highest content part is used as height reference for all other parts. Provides more consistent animations.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual AccordionBuilder AutoHeight(bool value)
        {
            Component.AutoHeight = value;

            return this;
        }

        /// <summary>
        /// If set, clears height and overflow styles after finishing animations. This enables accordions to work with dynamic content. Won't work together with autoHeight.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual AccordionBuilder ClearStyle(bool value)
        {
            Component.ClearStyle = value;

            return this;
        }

        /// <summary>
        /// Specify the the event on which to trigger the accordion.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <returns></returns>
        public virtual AccordionBuilder OpenOn(string eventName)
        {
            Component.OpenOn = eventName;

            return this;
        }

        /// <summary>
        /// Whether all the sections can be closed at once. Allows collapsing the active section by the triggering event (click is the default).
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual AccordionBuilder CollapsibleContent(bool value)
        {
            Component.CollapsibleContent = value;

            return this;
        }

        /// <summary>
        /// If set, the accordion completely fills the height of the parent element. Overrides autoheight.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual AccordionBuilder FillSpace(bool value)
        {
            Component.FillSpace = value;

            return this;
        }

        /// <summary>
        /// Icons to use for headers.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual AccordionBuilder Icon(string name)
        {
            Component.Icon = name;

            return this;
        }

        /// <summary>
        /// Icons to use for header selected.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual AccordionBuilder SelectedIcon(string name)
        {
            Component.SelectedIcon = name;

            return this;
        }

        /// <summary>
        /// This event is triggered every time the accordion changes. If the accordion is animated, the event will be triggered upon completion of the animation; otherwise, it is triggered immediately.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual AccordionBuilder OnChange(Action javaScript)
        {
            Component.OnChange = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme applies to the accordion.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual AccordionBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}