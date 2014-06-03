// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Infrastructure;

    /// <summary>
    /// Contains extension methods of IEnumerable&lt;T&gt;.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the provided delegate for each item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="action">The action to be applied.</param>
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            Guard.IsNotNull(instance, "instance");
            Guard.IsNotNull(action, "action");

            foreach (T item in instance)
            {
                action(item);
            }
        }
    }
}