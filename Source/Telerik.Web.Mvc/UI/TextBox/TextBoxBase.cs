// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;


    public class TextBoxBase<T> : ViewComponentBase, ITextBox<T>, IInputComponent<T> where T : struct
    {
        internal readonly ITextBoxBaseHtmlBuilderFactory<T> rendererFactory;

        public TextBoxBase(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, ITextBoxBaseHtmlBuilderFactory<T> rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            this.rendererFactory = rendererFactory;

            Spinners = true;

            InputHtmlAttributes = new RouteValueDictionary();

            ClientEvents = new TextBoxBaseClientEvents();

            Enabled = true;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public new string Id
        {
            get
            {
                // Return from htmlattributes if user has specified
                // otherwise build it from name
                return InputHtmlAttributes.ContainsKey("id") ?
                       InputHtmlAttributes["id"].ToString() :
                       (!string.IsNullOrEmpty(Name) ? Name.Replace(".", HtmlHelper.IdAttributeDotReplacement) : null);
            }
        }

        public IDictionary<string, object> InputHtmlAttributes
        {
            get;
            private set;
        }

        public T? Value
        {
            get;
            set;
        }

        public T IncrementStep
        {
            get;
            set;
        }

        public T? MinValue
        {
            get;
            set;
        }

        public T? MaxValue
        {
            get;
            set;
        }

        public int NumberGroupSize
        {
            get;
            set;
        }

        public string NumberGroupSeparator
        {
            get;
            set;
        }

        public int NegativePatternIndex
        {
            get;
            set;
        }

        public string EmptyMessage 
        { 
            get; 
            set; 
        }

        public bool Spinners
        {
            get;
            set;
        }

        public string ButtonTitleUp
        {
            get;
            set;
        }

        public string ButtonTitleDown
        {
            get;
            set;
        }

        public TextBoxBaseClientEvents ClientEvents
        {
            get;
            private set;
        }

        public bool Enabled
        {
            get;
            set;
        }
        
#if MVC2 || MVC3
        public override void VerifySettings()
        {
            Name = Name ?? ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty);
            
            base.VerifySettings();
        }
#endif
    }
}
