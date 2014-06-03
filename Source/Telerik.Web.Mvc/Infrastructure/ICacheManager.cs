// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Web.Caching;

    /// <summary>
    /// Defines members that a class must implement in order to access System.Web.HttpRuntime.Cache object.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetItem(string key);

        /// <summary>
        /// Inserts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="onRemoveCallback">The on remove callback.</param>
        /// <param name="fileDependencies">The file dependencies.</param>
        void Insert(string key, object value, CacheItemRemovedCallback onRemoveCallback, params string[] fileDependencies);
    }
}