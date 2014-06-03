// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    /// <summary>
    /// Serves as the base class for classes that provides linked object information.
    /// </summary>
    public abstract class LinkedObjectBase<T> : IHideObjectMembers
    {
        /// <summary>
        /// Gets or sets the T object that is the parent of the current node.
        /// </summary>
        /// <value>The parent.</value>
        public T Parent
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the previous T object on the same level as the current one, relative to the T.ParentNode object (if one exists).
        /// </summary>
        /// <value>The previous sibling.</value>
        public T PreviousSibling
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the next T node on the same hierarchical level as the current one, relative to the T.ParentNode property (if one exists).
        /// </summary>
        /// <value>The next sibling.</value>
        public T NextSibling
        {
            get;
            protected internal set;
        }
    }
}