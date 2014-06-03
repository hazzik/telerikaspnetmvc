// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI.Html;

    /// <summary>
    /// Telerik Chart for ASP.NET MVC is a view component for rendering charts.
    /// Features:
    /// <list type="bullet">
    ///     <item>Bar Chart</item>
    ///     <item>Column Chart</item>
    /// </list>
    /// For more information, see the online documentation.
    /// </summary>
    public class Chart<T> : ViewComponentBase, IChart where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chart{T}" /> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        /// <param name="urlGenerator">The URL Generator.</param>
        public Chart(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.chart.js" });

            ClientEvents = new ChartClientEvents();
            UrlGenerator = urlGenerator;
            Title = new ChartTitle();
            ChartArea = new ChartArea();
            PlotArea = new PlotArea();
            Legend = new ChartLegend();
            Series = new List<ChartSeriesBase<T>>();
            CategoryAxis = new ChartCategoryAxis<T>(this);
            ValueAxis = new ChartNumericAxis<T>(this);
            DataBinding = new ChartDataBindingSettings(this);
            SeriesDefaults = new ChartSeriesDefaults<T>(this);
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public IEnumerable<T> DataSource
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the client-side event handlers for the component
        /// </summary>
        public ChartClientEvents ClientEvents
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the URL generator.
        /// </summary>
        /// <value>The URL generator.</value>
        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Chart area.
        /// </summary>
        /// <value>
        /// The Chart area.
        /// </value>
        public ChartArea ChartArea
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Plot area.
        /// </summary>
        /// <value>
        /// The Plot area.
        /// </value>
        public PlotArea PlotArea
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Chart theme.
        /// </summary>
        /// <value>
        /// The Chart theme.
        /// </value>
        public string Theme
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Chart title.
        /// </summary>
        /// <value>
        /// The Chart title.
        /// </value>
        public ChartTitle Title
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Chart legend.
        /// </summary>
        /// <value>
        /// The Chart legend.
        /// </value>
        public ChartLegend Legend
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the chart series.
        /// </summary>
        public IList<ChartSeriesBase<T>> Series
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the default settings for all series.
        /// </summary>
        public ChartSeriesDefaults<T> SeriesDefaults
        {
            get;
            private set;
        }

        /// <summary>
        /// Configuration for the default category axis (if any)
        /// </summary>
        public IChartCategoryAxis CategoryAxis
        {
            get;
            set;
        }

        /// <summary>
        /// Configuration for the default value axis
        /// </summary>
        public IChartValueAxis ValueAxis
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the data binding configuration.
        /// </summary>
        public ChartDataBindingSettings DataBinding
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the series colors.
        /// </summary>
        public IEnumerable<string> SeriesColors
        {
            get;
            set;
        }

        /// <summary>
        /// Writes the initialization script.
        /// </summary>
        /// <param name="writer">The writer object.</param>
        public override void WriteInitializationScript(TextWriter writer)
        {
            var objectWriter = ClientSideObjectWriterFactory.Create(Id, "tChart", writer);

            objectWriter.Start();

            SerializeChartArea(objectWriter);

            SerializeTheme(objectWriter);

            SerializeTitle(objectWriter);

            SerializeLegend(objectWriter);

            SerializeSeries(objectWriter);

            SerializeSeriesDefaults(objectWriter);

            SerializeCategoryAxis(objectWriter);

            SerializeValueAxis(objectWriter);

            SerializeDataBinding(objectWriter);

            SerializeSeriesColors(objectWriter);

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        private void SerializeChartArea(IClientSideObjectWriter objectWriter)
        {
            var chartArea = ChartArea.CreateSerializer().Serialize();
            if (chartArea.Count > 0)
            {
                objectWriter.AppendObject("chartArea", chartArea);
            }
        }

        private void SerializeTheme(IClientSideObjectWriter objectWriter)
        {
            if (Theme.HasValue())
            {
                objectWriter.Append("theme", Theme);
            }
        }

        private void SerializeTitle(IClientSideObjectWriter objectWriter)
        {
            var titleData = Title.CreateSerializer().Serialize();
            if (titleData.Count > 0)
            {
                objectWriter.AppendObject("title", titleData);
            }
        }

        private void SerializeLegend(IClientSideObjectWriter objectWriter)
        {
            var legendData = Legend.CreateSerializer().Serialize();
            if (legendData.Count > 0)
            {
                objectWriter.AppendObject("legend", legendData);
            }
        }

        private void SerializeSeries(IClientSideObjectWriter objectWriter)
        {
            if (Series.Count > 0)
            {
                var serializedSeries = new List<IDictionary<string, object>>();
                foreach (var s in Series)
                {
                    serializedSeries.Add(s.CreateSerializer().Serialize());
                }

                objectWriter.AppendCollection("series", serializedSeries);
            }
        }

        private void SerializeSeriesDefaults(IClientSideObjectWriter objectWriter)
        {
            var seriesDefaultsData = SeriesDefaults.CreateSerializer().Serialize();
            if (seriesDefaultsData.Count > 0)
            {
                objectWriter.AppendObject("seriesDefaults", seriesDefaultsData);
            }
        }

        private void SerializeCategoryAxis(IClientSideObjectWriter objectWriter)
        {
            var categoryAxisData = CategoryAxis.CreateSerializer().Serialize();
            if (categoryAxisData.Count > 0)
            {
                objectWriter.AppendObject("categoryAxis", categoryAxisData);
            }
        }

        private void SerializeValueAxis(IClientSideObjectWriter objectWriter)
        {
            var valueAxisData = ValueAxis.CreateSerializer().Serialize();
            if (valueAxisData.Count > 0)
            {
                objectWriter.AppendObject("valueAxis", valueAxisData);
            }
        }

        private void SerializeDataBinding(IClientSideObjectWriter objectWriter)
        {
            if (DataBinding.Ajax.Enabled)
            {
                DataBinding.Ajax.SerializeTo("dataSource", objectWriter);
            }
        }

        private void SerializeSeriesColors(IClientSideObjectWriter objectWriter)
        {
            if (SeriesColors != null)
            {
                objectWriter.AppendCollection("seriesColors", SeriesColors);
            }
        }

        /// <summary>
        /// Writes the Chart HTML.
        /// </summary>
        /// <param name="writer">The writer object.</param>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            if (!HtmlAttributes.ContainsKey("id"))
            {
                HtmlAttributes["id"] = Id;
            }

            new ChartHtmlBuilder<T>(this)
                .Build()
                .WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}