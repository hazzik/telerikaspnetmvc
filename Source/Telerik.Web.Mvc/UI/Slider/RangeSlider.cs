﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI.Html;
    using System.Web.UI;
    using Telerik.Web.Mvc.Resources;

    public class RangeSlider<T> : ViewComponentBase where T : struct, IComparable
    {
        private readonly IRangeSliderHtmlBuilderFactory rendererFactory;

        public RangeSlider(ViewContext viewContext, IClientSideObjectWriterFactory writerFactory, IRangeSliderHtmlBuilderFactory rendererFactory)
            : base(viewContext, writerFactory)
        {
            this.rendererFactory = rendererFactory;

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.draganddrop.js", "telerik.slider.js" });

            Orientation = SliderOrientation.Horizontal;
            TickPlacement = SliderTickPlacement.Both;
            MinValue = (T)Convert.ChangeType(0, typeof(T));
            MaxValue = (T)Convert.ChangeType(10, typeof(T));
            SmallStep = (T)Convert.ChangeType(1, typeof(T));
            LargeStep = (T)Convert.ChangeType(5, typeof(T));
            ClientEvents = new SliderBaseClientEvents();
            Enabled = true;
            Settings = new SliderTooltipSettings();
        }

        public SliderOrientation Orientation
        {
            get;
            set;
        }

        public SliderTickPlacement TickPlacement
        {
            get;
            set;
        }

        public T MinValue
        {
            get;
            set;
        }

        public T MaxValue
        {
            get;
            set;
        }

        public T SmallStep
        {
            get;
            set;
        }

        public T LargeStep
        {
            get;
            set;
        }

        public SliderBaseClientEvents ClientEvents
        {
            get;
            private set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public SliderTooltipSettings Settings
        {
            get;
            set;
        }

        public T? SelectionStart
        {
            get;
            set;
        }

        public T? SelectionEnd
        {
            get;
            set;
        }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            var objectWriter = ClientSideObjectWriterFactory.Create(Id, "tRangeSlider", writer);

            objectWriter.Start();

            SerializeProperties(objectWriter);

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        private void SerializeProperties(IClientSideObjectWriter objectWriter)
        {
            objectWriter.Append("orientation", Orientation, SliderOrientation.Horizontal);
            objectWriter.Append("tickPlacement", TickPlacement, SliderTickPlacement.Both);
            objectWriter.AppendObject("selectionStart", SelectionStart);
            objectWriter.AppendObject("selectionEnd", SelectionEnd);
            objectWriter.AppendObject("smallStep", SmallStep);
            objectWriter.AppendObject("largeStep", LargeStep);
            objectWriter.AppendObject("minValue", MinValue);
            objectWriter.AppendObject("maxValue", MaxValue);
            objectWriter.Append("enabled", Enabled, true);

            Settings.SerializeTo("tooltip", objectWriter);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            if (!SelectionStart.HasValue)
            {
                SelectionStart = MinValue;
            }

            if (!SelectionEnd.HasValue) 
            {
                SelectionEnd = MaxValue;
            }

            T[] componentValues = new T[] { SelectionStart.Value, SelectionEnd.Value };

            Func<object, T[]> converter = val =>
            {
                return (T[])val;
            };

            T[] values = null;
            ModelState state;
            ViewDataDictionary viewData = ViewContext.ViewData;

            object valueFromViewData = viewData.Eval(Name);

            if (viewData.ModelState.TryGetValue(Name, out state)
             && viewData.ModelState.IsValidField(Name)
             && (state.Value != null))
            {
                values = state.Value.ConvertTo(typeof(T), state.Value.Culture) as T[];
            }
            else if (componentValues != null)
            {
                values = componentValues;
            }
            else if (valueFromViewData != null)
            {
                values = converter(valueFromViewData);
            }

            var builder = rendererFactory.Create(new RangeSliderRenderingData
            {
                Id = Id,
                Name = Name,
                HtmlAttributes = HtmlAttributes,
                MaxValue = MaxValue,
                MinValue = MinValue,
                SmallStep = SmallStep,
                SelectionStart = values[0],
                SelectionEnd = values[1],
                Enabled = Enabled
            });

            builder.Build().WriteTo(writer);

            base.WriteHtml(writer);
        }

        public override void VerifySettings()
        {
            base.VerifySettings();

            if (MinValue.CompareTo(MaxValue) >= 0)
            {
                throw new ArgumentException(TextResource.MinPropertyMustBeLessThenMaxProperty.FormatWith("Min", "Max"));
            }

            if (SelectionStart.Value.CompareTo(SelectionEnd.Value) > 0)
            {
                throw new ArgumentException(TextResource.FirstPropertyShouldNotBeBiggerThenSecondProperty.FormatWith("SelectionStart", "SelectionEnd"));
            }

            if (SelectionStart.Value.CompareTo(MinValue) < 0 || SelectionStart.Value.CompareTo(MaxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("SelectionStart", "Min", "Max"));
            }

            if (SelectionEnd.Value.CompareTo(MinValue) < 0 || SelectionEnd.Value.CompareTo(MaxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("SelectionEnd", "Min", "Max"));
            }

            if (SmallStep.CompareTo(LargeStep) > 0)
            {
                throw new ArgumentException(TextResource.FirstPropertyShouldNotBeBiggerThenSecondProperty.FormatWith("SmallStep", "LargeStep"));
            }

            if (SmallStep.CompareTo((T)Convert.ChangeType(0, typeof(T))) <= 0)
            {
                throw new ArgumentException(TextResource.PropertyMustBeBiggerThenZero.FormatWith("SmallStep"));
            }
        }
    }
}