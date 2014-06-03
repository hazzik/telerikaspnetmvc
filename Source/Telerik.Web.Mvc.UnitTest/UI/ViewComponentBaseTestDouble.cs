// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.Mvc;
    
    public class ViewComponentBaseTestDouble : ViewComponentBase
    {
        public bool HasEnsuredRequired;
        public bool HasWrittenHtml;

        public ViewComponentBaseTestDouble(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
        }

        public void InitializationScript(TextWriter writer)
        {
            base.WriteInitializationScript(writer);
        }

        public void CleanupScript(TextWriter writer)
        {
            base.WriteCleanupScript(writer);
        }

        public void CheckRequired()
        {
            base.EnsureRequired();
        }

        public void Html()
        {
            base.WriteHtml();
        }

        protected override void EnsureRequired()
        {
            base.EnsureRequired();
            HasEnsuredRequired = true;
        }

        protected override void WriteHtml()
        {
            base.WriteHtml();
            HasWrittenHtml = true;
        }
    }
}