#if !CLR40
namespace System.ComponentModel.DataAnnotations
{
    using System;

    using Telerik.Web.Mvc.Infrastructure;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}
#endif