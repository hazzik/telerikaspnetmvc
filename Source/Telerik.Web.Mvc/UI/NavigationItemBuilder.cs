// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    using Resources;

    /// <summary>
    /// Defines the fluent interface for configuring navigation items
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class NavigationItemBuilder<TItem, TBuilder> where TItem : NavigationItem<TItem> where TBuilder : NavigationItemBuilder<TItem, TBuilder>, IHideObjectMembers
    {
        private readonly NavigationItem<TItem> item;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItemBuilder&lt;TItem, TBuilder&gt;"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        protected NavigationItemBuilder(NavigationItem<TItem> item)
        {
            this.item = item;
        }

        protected TItem Item
        {
            get
            {
                return item as TItem;
            }
        }

        /// <summary>
        /// Returns the inner navigation item
        /// </summary>
        /// <returns></returns>
        public TItem ToItem()
        {
            return item as TItem;
        }

        /// <summary>
        /// Sets the HTML attributes applied to the outer HTML element rendered for the item
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Attributes(new {@class="first-item"}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder HtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            item.HtmlAttributes.Clear();
            item.HtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the text displayed by the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Text(string value)
        {
            item.Text = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Makes the item visible or not. Invisible items are not rendered in the output HTML.
        /// </summary>
        /// <summary>
        /// Sets the text displayed by the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Visible((bool)ViewData["visible"])
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Visible(bool value)
        {
            item.Visible = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Enables or disables the item. Disabled item cannot be clicked, expanded or open (depending on the item type - menu, tabstrip, panelbar).
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Enabled((bool)ViewData["enabled"])
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Enabled(bool value)
        {
            item.Enabled = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Selects or unselects the item. By default items are not selected.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Selected(true))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Selected(bool value)
        {
            item.Selected = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the route to which the item should navigate.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Route("Default", new RouteValueDictionary{{"id", 1}}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Route(string routeName, RouteValueDictionary routeValues)
        {
            item.Route(routeName, routeValues);

            SetTextIfEmpty(routeName);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the route to which the item should navigate.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Route("Default", new {id, 1}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Route(string routeName, object routeValues)
        {
            item.Route(routeName, routeValues);

            SetTextIfEmpty(routeName);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the route to which the item should navigate.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").Route("Default"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Route(string routeName)
        {
            return Route(routeName, (object)null);
        }

        /// <summary>
        /// Sets the action to which the item should navigate
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("Index").Action("Index", "Home", new RouteValueDictionary{{"id", 1}}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            item.Action(actionName, controllerName, routeValues);

            SetTextIfEmpty(actionName);
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the action to which the item should navigate
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("Index").Action("Index", "Home", new {id, 1}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Action(string actionName, string controllerName, object routeValues)
        {
            item.Action(actionName, controllerName, routeValues);
            SetTextIfEmpty(actionName);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the action to which the item should navigate
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("Index").Action("Index", "Home"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Action(string actionName, string controllerName)
        {
            return Action(actionName, controllerName, (object)null);
        }

        /// <summary>
        /// Sets the URL to which the item should navigate
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("www.example.com").Url("http://www.example.com"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Url(string value)
        {
            if (item is IAsyncContentContainer) 
            {   
                if(!string.IsNullOrEmpty((item as IAsyncContentContainer).ContentUrl))
                    throw new NotSupportedException(TextResource.UrlAndContentUrlCannotBeSet);
            }

            item.Url(value);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the URL of the image that should be displayed by the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items.Add().Text("First Item").ImageUrl("~/Content/first.png"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder ImageUrl(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            item.ImageUrl = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes for the item image.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items
        ///                    .Add().Text("First Item")
        ///                    .ImageUrl("~/Content/first.png")
        ///                    .ImageHtmlAttributes(new {@class="first-item-image"}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder ImageHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            Item.ImageHtmlAttributes.Clear();
            Item.ImageHtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the sprite CSS class names.
        /// </summary>
        /// <param name="cssClasses">The CSS classes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items
        ///                    .Add().Text("First Item")
        ///                    .SpriteCssClasses("icon", "first-item")
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder SpriteCssClasses(params string[] cssClasses)
        {
            Item.SpriteCssClasses = String.Join(" ", cssClasses);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML content which the item should display (tab item or panelbar item).
        /// </summary>
        /// <param name="value">The action which renders the content.</param>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Menu()
        ///            .Name("Menu")
        ///            .Items(items => items
        ///                     .Add()
        ///                     .Text("First Item")
        ///                     .Content(() => 
        ///                     { 
        ///                         %&gt;
        ///                             &lt;strong&gt; First Item Content&lt;/strong&gt;
        ///                         &lt;% 
        ///                     });)
        ///            .Render();
        /// %&gt;
        /// </code>        
        public virtual TBuilder Content(Action value)
        {
            Guard.IsNotNull(value, "value");

            Item.Content = value;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes of the content element of the item.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items
        ///                    .Add().Text("First Item")
        ///                    .Content(() => { %&gt; &lt;strong&gt;First Item Content&lt;/strong&gt; &lt;% })
        ///                    .ContentHtmlAttributes(new {@class="first-item-content"})
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual TBuilder ContentHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            Item.ContentHtmlAttributes.Clear();
            Item.ContentHtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Makes the item navigate to the specified controllerAction method.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="controllerAction">The action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => items
        ///                    .Add().Text("First Item")
        ///                    .Action&lt;HomeController&gt;(controller => controller.Index()))
        ///                    
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder Action<TController>(Expression<Action<TController>> controllerAction) where TController : Controller
        {
            MethodCallExpression call = (MethodCallExpression) controllerAction.Body;

            string controllerName = typeof(TController).Name;

            if (!controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(TextResource.ControllerNameMustEndWithController, "controllerAction");
            }

            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            if (controllerName.Length == 0)
            {
                throw new ArgumentException(TextResource.CannotRouteToClassNamedController, "controllerAction");
            }

            if (call.Method.IsDefined(typeof(NonActionAttribute), false))
            {
                throw new ArgumentException(TextResource.TheSpecifiedMethodIsNotAnActionMethod, "controllerAction");
            }

            string actionName = call.Method.GetCustomAttributes(typeof(ActionNameAttribute), false)
                                           .OfType<ActionNameAttribute>()
                                           .Select(attribute => attribute.Name)
                                           .FirstOrDefault() ?? call.Method.Name;

            item.ControllerName = controllerName;
            item.ActionName = actionName;

            ParameterInfo[] parameters = call.Method.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                Expression arg = call.Arguments[i];
                object value;
                ConstantExpression ce = arg as ConstantExpression;

                if (ce != null)
                {
                    value = ce.Value;
                }
                else
                {
                    Expression<Func<object>> lambdaExpression = Expression.Lambda<Func<object>>(Expression.Convert(arg, typeof(object)));
                    Func<object> func = lambdaExpression.Compile();
                    value = func();
                }

                item.RouteValues.Add(parameters[i].Name, value);
            }

            return this as TBuilder;
        }

        private void SetTextIfEmpty(string value)
        {
            if (string.IsNullOrEmpty(item.Text))
            {
                item.Text = value;
            }
        }
    }
}