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
    public class TabBuilder : ViewComponentBuilderBase<Tab, TabBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public TabBuilder(Tab component) : base(component)
        {
        }

        /// <summary>
        /// Specify the tab items
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <returns></returns>
        public virtual TabBuilder Items(Action<TabItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            TabItemFactory factory = new TabItemFactory(Component);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Specify the opacity and duration for animation.
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder Animation(string opacity, int duration)
        {
            Component.AnimationOpacity = opacity;
            Component.AnimationDuration = duration;

            return this;
        }

        /// <summary>
        /// Specify the opacity and duration for animation.
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder Animation(string opacity, AnimationDuration duration)
        {
            return Animation(opacity, (int)duration);
        }

        /// <summary>
        /// Specify the opacity and duration for tab open
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder OpenAnimation(string opacity, int duration)
        {
            Component.OpenAnimationOpacity = opacity;
            Component.OpenAnimationDuration = duration;

            return this;
        }

        /// <summary>
        /// Specify the opacity and duration for tab open
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder OpenAnimation(string opacity, AnimationDuration duration)
        {
            return OpenAnimation(opacity, (int)duration);
        }

        /// <summary>
        /// Specify the opacity and duration for tab close
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder CloseAnimation(string opacity, int duration)
        {
            Component.CloseAnimationOpacity = opacity;
            Component.CloseAnimationDuration = duration;

            return this;
        }

        /// <summary>
        /// Specify the opacity and duration for tab close
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual TabBuilder CloseAnimation(string opacity, AnimationDuration duration)
        {
            return CloseAnimation(opacity, (int)duration);
        }

        /// <summary>
        /// Specify the event on which to trigger the accordion.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <returns></returns>
        public virtual TabBuilder OpenOn(string eventName)
        {
            Component.OpenOn = eventName;

            return this;
        }

        /// <summary>
        /// Set to true to allow an already selected tab to become unselected again upon reselection.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual TabBuilder CollapsibleContent(bool value)
        {
            Component.CollapsibleContent = value;

            return this;
        }

        /// <summary>
        /// Specify whether or not to cache remote tabs content, e.g. load only once or with every click.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual TabBuilder CacheAjaxResponse(bool value)
        {
            Component.CacheAjaxResponse = value;

            return this;
        }

        /// <summary>
        /// The HTML content of this string is shown in a tab title while remote content is loading. Pass in empty string to deactivate that behavior.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual TabBuilder SpinnerText(string text)
        {
            Component.SpinnerText = text;

            return this;
        }

        /// <summary>
        /// Set up an automatic rotation through tabs of a tab pane.
        /// </summary>
        /// <param name="durationInMilliseconds">The duration in milliseconds until the next tab in the cycle gets activated.</param>
        /// <returns></returns>
        public virtual TabBuilder Rotate(int durationInMilliseconds)
        {
            Component.RotationDurationInMilliseconds = durationInMilliseconds;
            Component.RotationContinue = false;

            return this;
        }

        /// <summary>
        /// Specify whether or not to continue the rotation after a tab has been selected by a user
        /// </summary>
        /// <param name="durationInMilliseconds">The duration in milliseconds.</param>
        /// <returns></returns>
        public virtual TabBuilder RotateWithContinue(int durationInMilliseconds)
        {
            Component.RotationDurationInMilliseconds = durationInMilliseconds;
            Component.RotationContinue = true;

            return this;
        }

        /// <summary>
        /// This event is triggered when clicking a tab.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnSelect(Action javaScript)
        {
            Component.OnSelect = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when a tab is shown.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnShow(Action javaScript)
        {
            Component.OnShow = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when a tab is added.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnAdd(Action javaScript)
        {
            Component.OnAdd = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when a tab is removed.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnRemove(Action javaScript)
        {
            Component.OnRemove = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when a tab is enabled.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnEnable(Action javaScript)
        {
            Component.OnEnable = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when a tab is disabled.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnDisable(Action javaScript)
        {
            Component.OnDisable = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered after the content of a remote tab has been loaded.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual TabBuilder OnLoad(Action javaScript)
        {
            Component.OnLoad = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme applies to the accordion.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual TabBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}