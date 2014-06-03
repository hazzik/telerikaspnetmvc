namespace Telerik.Web.Mvc
{
    using System;

    [Serializable]
    public enum SiteMapUpdatePriority
    {
        Automatic = 0,
        Low = 30,
        Normal = 50,
        High = 80,
        Critical = 100
    }
}