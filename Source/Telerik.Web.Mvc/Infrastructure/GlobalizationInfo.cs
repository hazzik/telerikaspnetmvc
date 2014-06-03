namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Collections.Generic;
    using System.Globalization;

    public class GlobalizationInfo
    {
        private readonly IDictionary<string, object> globalization = new Dictionary<string, object>();
        
        public GlobalizationInfo(CultureInfo cultureInfo)
        {
            DateTimeFormatInfo dateTimeFormats = cultureInfo.DateTimeFormat;

            globalization["shortDate"] = dateTimeFormats.ShortDatePattern;
            globalization["longDate"] = dateTimeFormats.LongDatePattern;
            globalization["fullDateTime"] = dateTimeFormats.FullDateTimePattern;
            globalization["sortableDateTime"] = dateTimeFormats.SortableDateTimePattern;
            globalization["universalSortableDateTime"] = dateTimeFormats.UniversalSortableDateTimePattern;
            globalization["generalDateShortTime"] = dateTimeFormats.ShortDatePattern + " " + dateTimeFormats.ShortTimePattern;
            globalization["generalDateTime"] = dateTimeFormats.ShortDatePattern + " " + dateTimeFormats.LongTimePattern;
            globalization["monthDay"] = dateTimeFormats.MonthDayPattern;
            globalization["monthYear"] = dateTimeFormats.YearMonthPattern;
            globalization["days"] = dateTimeFormats.DayNames;
            globalization["abbrDays"] = dateTimeFormats.AbbreviatedDayNames;
            globalization["abbrMonths"] = dateTimeFormats.AbbreviatedMonthNames;
            globalization["months"] = dateTimeFormats.MonthNames;
            globalization["am"] = dateTimeFormats.AMDesignator;
            globalization["pm"] = dateTimeFormats.PMDesignator;
            globalization["dateSeparator"] = dateTimeFormats.DateSeparator;
            globalization["timeSeparator"] = dateTimeFormats.TimeSeparator;
        }

        public IDictionary<string, object> ToDictionary()
        {
            return globalization;
        }
    }
}