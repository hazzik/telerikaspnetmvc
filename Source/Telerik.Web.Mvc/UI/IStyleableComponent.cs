// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the basic building block of styleable component.
    /// </summary>
    public interface IStyleableComponent
    {
        /// <summary>
        /// Gets or sets the asset key.
        /// </summary>
        /// <value>The asset key.</value>
        string AssetKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style sheet files path.
        /// </summary>
        /// <value>The style sheet files path.</value>
        string StyleSheetFilesPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the style sheet file names.
        /// </summary>
        /// <value>The style sheet file names.</value>
        IList<string> StyleSheetFileNames
        {
            get;
        }
    }
}