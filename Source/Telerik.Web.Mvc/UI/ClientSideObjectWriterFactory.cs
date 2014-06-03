// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.IO;

    /// <summary>
    /// Defines the factory to create <see cref="IClientSideObjectWriter"/>.
    /// </summary>
    public interface IClientSideObjectWriterFactory
    {
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="type">The type.</param>
        /// <param name="textWriter">The text writer.</param>
        /// <returns></returns>
        IClientSideObjectWriter Create(string id, string type, TextWriter textWriter);
    }

    /// <summary>
    /// Defines the factory to create <see cref="IClientSideObjectWriter"/>.
    /// </summary>
    public class ClientSideObjectWriterFactory : IClientSideObjectWriterFactory
    {
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="type">The type.</param>
        /// <param name="textWriter">The text writer.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Create(string id, string type, TextWriter textWriter)
        {
            return new ClientSideObjectWriter(id, type, textWriter);
        }
    }
}