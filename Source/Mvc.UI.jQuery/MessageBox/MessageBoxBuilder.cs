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
    /// Class used by the HTML helpers to build HTML tags for message box.
    /// </summary>
    public class MessageBoxBuilder : ViewComponentBuilderBase<MessageBox, MessageBoxBuilder>, IHideObjectMembers
    {
        public MessageBoxBuilder(MessageBox component) : base(component)
        {
        }

        /// <summary>
        /// The Type of message box.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public virtual MessageBoxBuilder MessageType(MessageBoxType type)
        {
            Component.MessageType = type;

            return this;
        }

        /// <summary>
        /// HTML markups for the message box.
        /// </summary>
        /// <param name="markups">The HTML markups.</param>
        /// <returns></returns>
        public virtual MessageBoxBuilder Content(Action markups)
        {
            Component.Content = markups;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme apply to the message box.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual MessageBoxBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}