namespace Telerik.Web.Mvc
{
    using System;

    [Serializable]
    public enum SiteMapChangeFrequency
    {
        Automatic = 0,
        Daily = 1,
        Always = 2,
        Hourly = 3,
        Weekly = 4,
        Monthly = 5,
        Yearly = 6,
        Never = 7
    }
}