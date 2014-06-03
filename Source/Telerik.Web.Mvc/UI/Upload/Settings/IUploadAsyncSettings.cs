namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Defines an interface for asynchronous upload settings
    /// </summary>
    public interface IUploadAsyncSettings
    {
        /// <summary>
        /// Defines the Save action
        /// </summary>
        INavigatable Save { get; set; }

        /// <summary>
        /// Defines the Remove action
        /// </summary>
        INavigatable Remove { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to start the upload immediately after selecting a file
        /// </summary>
        bool AutoUpload { get; set; }
    }
}