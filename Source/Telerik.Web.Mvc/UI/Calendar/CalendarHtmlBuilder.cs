// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using Extensions;
    using Infrastructure;

    public class CalendarHtmlBuilder : ICalendarHtmlBuilder
    {
        public CalendarHtmlBuilder(Calendar calendar)
        {
            Calendar = calendar;
        }

        public Calendar Calendar
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            IHtmlNode div = new HtmlTag("div")
                            .Attributes(Calendar.HtmlAttributes)
                            .Attribute("id", Calendar.Id)
                            .PrependClass(UIPrimitives.Widget, "t-calendar");

            IHtmlNode headerDiv = new HtmlTag("div")
                                  .AddClass(UIPrimitives.Header);

            IHtmlNode span = new HtmlTag("span")
                             .Text(Calendar.DetermineFocusedDate().Value.ToString("MMMM yyyy"));

            headerDiv.Children.Add(span);

            div.Children.Add(headerDiv);

            return div;
        }

        public IHtmlNode ContentTag()
        {
            return new HtmlTag("table")
                   .AddClass(UIPrimitives.Content)
                   .Attributes(new { summary = "calendar widget", cellspacing = "0" });
        }

        public IHtmlNode HeaderTag()
        {
            IHtmlNode header = new HtmlTag("thead").AddClass("t-week-header");

            return header;
        }

        public IHtmlNode HeaderCellTag(string dayOfWeek)
        {
            IHtmlNode cell = new HtmlTag("th")
                             .Attributes(new { scope = "col", title = dayOfWeek})
                             .Text(dayOfWeek.Substring(0, 1));

            if (dayOfWeek.Length > 3)
                cell.Attribute("abbr", dayOfWeek.Substring(0, 3));

            return cell;
        }

        public IHtmlNode MonthTag()
        {
            return new HtmlTag("tbody");
        }

        public IHtmlNode RowTag()
        {
            return new HtmlTag("tr");
        }

        public IHtmlNode CellTag(DateTime day, string urlFormat, bool isOtherMonth)
        {
            IHtmlNode cell = new HtmlTag("td");

            if (isOtherMonth)
            {
                cell.AddClass("t-other-month");
            }
            else if(Calendar.Value != null && day.Day == Calendar.Value.Value.Day)
            {
                cell.AddClass(UIPrimitives.SelectedState);
            }

            if (Calendar.IsDateInRange(day))
            {
                var href = GetUrl(day, urlFormat);

                IHtmlNode link = new HtmlTag("a")
                                 .AddClass(UIPrimitives.Link + (href != "#" ? " t-action-link" : string.Empty))
                                 .Attribute("href", href)
                                 .Attribute("title", day.ToLongDateString())
                                 .Text(day.Day.ToString());

                cell.Children.Add(link);
            }
            else 
            {
                cell.Html("&nbsp;");
            }

            return cell;
        }

        private string GetUrl(DateTime day, string urlFormat)
        {
            string url = "#";
            if (urlFormat.HasValue())
            {
                if (Calendar.SelectionSettings.Dates.Count > 0)
                {
                    if (Calendar.SelectionSettings.Dates.Contains(day))
                    {
                        url = urlFormat.FormatWith(day.ToShortDateString());
                    }
                    else
                    {
                        url = "#";
                    }
                }
                else
                {
                    url = urlFormat.FormatWith(day.ToShortDateString());
                }
            }
            return url;
        }
    }
}