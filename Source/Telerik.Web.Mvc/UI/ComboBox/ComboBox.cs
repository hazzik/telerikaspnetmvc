// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Collections.Generic;

    using Extensions;

    public class ComboBox : ViewComponentBase, IDropDown, IComboBoxRenderable
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        public ComboBox(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.list.js", "telerik.combobox.js" });

            UrlGenerator = urlGenerator;

            ClientEvents = new DropDownClientEvents();
            DataBinding = new AutoCompleteDataBindingConfiguration();
            DropDownHtmlAttributes = new RouteValueDictionary();
            InputHtmlAttributes = new RouteValueDictionary();

            Effects = new Effects();
            defaultEffects.Each(el => Effects.Container.Add(el));

            Filtering = new ComboBoxFilterSettings();

            Items = new List<DropDownItem>();
            SelectedIndex = -1;
            Enabled = true;
            Encoded = true;
            OpenOnFocus = false;
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

        public bool AutoFill
        {
            get;
            set;
        }

        public bool Encoded
        {
            get;
            set;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            set;
        }

        public DropDownClientEvents ClientEvents
        {
            get;
            private set;
        }

        public IDropDownDataBindingConfiguration DataBinding
        {
            get;
            private set;
        }

        public IDictionary<string, object> DropDownHtmlAttributes
        {
            get;
            private set;
        }

        public IDictionary<string, object> InputHtmlAttributes
        {
            get;
            private set;
        }

        public Effects Effects
        {
            get;
            set;
        }

        public ComboBoxFilterSettings Filtering
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the items of the ComboBox.
        /// </summary>
        public IList<DropDownItem> Items
        {
            get;
            private set;
        }

        public bool HighlightFirstMatch
        {
            get;
            set;
        }

        public int SelectedIndex 
        { 
            get; 
            set; 
        }
        
        public string Value
        { 
            get; 
            set; 
        }

        public bool Enabled 
        { 
            get; 
            set; 
        }

        public bool OpenOnFocus
        {
            get;
            set;
        }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tComboBox", writer);

            objectWriter.Start();

            objectWriter.Append("autoFill", AutoFill, true);
            objectWriter.Append("highlightFirst", HighlightFirstMatch, true);

            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            ClientEvents.SerializeTo(objectWriter);

            DataBinding.Ajax.SerializeTo<AutoCompleteBindingSettings>("ajax", objectWriter, this);
            DataBinding.WebService.SerializeTo<AutoCompleteBindingSettings>("ws", objectWriter, this);

            if (Filtering.Enabled)
            {
                objectWriter.Append("filter", (int)Filtering.FilterMode);
                objectWriter.Append("minChars", Filtering.MinimumChars, 0);
            }

            if (Items.Any())
            {
                objectWriter.AppendCollection("data", Items);
            }

            objectWriter.Append("selectedValue", Value.HasValue() ? Value : this.GetValueFromViewDataByName());
            objectWriter.Append("index", SelectedIndex, -1);

            if (DropDownHtmlAttributes.Any())
            {
                objectWriter.Append("dropDownAttr", DropDownHtmlAttributes.ToAttributeString());
            }

            objectWriter.Append("encoded", this.Encoded, true);
            objectWriter.Append("openOnFocus", this.OpenOnFocus, false);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            if (Items.Any())
            {
                if (Encoded)
                {
                    this.EncodeTextPropertyofItems();
                }

                this.SyncSelectedIndex();
            }

            IDropDownHtmlBuilder builder = new ComboBoxHtmlBuilder(this);

            builder.Build().WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}