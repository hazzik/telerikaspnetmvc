namespace Telerik.Web.Mvc
{
    using UI;

    /// <summary>
    /// Defines basic building blocks of Global storage for web assets.
    /// </summary>
    public interface IWebAssetRegistry : IWebAssetLocator
    {
        /// <summary>
        /// Stores the specified asset group.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetGroup">The asset group.</param>
        /// <returns></returns>
        string Store(string contentType, WebAssetItemGroup assetGroup);

        /// <summary>
        /// Retrieves the web asset by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        WebAsset Retrieve(string id);
    }
}