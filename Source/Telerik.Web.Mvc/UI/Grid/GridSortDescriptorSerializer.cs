// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Extensions;

    public static class GridSortDescriptorSerializer
    {
        private const string ColumnDelimiter = "~";
        private const string DirectionDelimiter = "-";
        private const string Ascending = "asc";
        private const string Descending = "desc";

        public static string Serialize(IEnumerable<SortDescriptor> descriptors)
        {
            List<string> sortExpressions = new List<string>();

            foreach (SortDescriptor column in descriptors)
            {
                sortExpressions.Add(SerializeDescriptor(column));
            }

            return string.Join(ColumnDelimiter, sortExpressions.ToArray());
        }

        public static IList<SortDescriptor> Deserialize(string from)
        {
            IList<SortDescriptor> orderBy = new List<SortDescriptor>();

            if (!string.IsNullOrEmpty(from))
            {
                string[] sortDescriptors = from.Split(ColumnDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (sortDescriptors.Length > 0)
                {
                    foreach (string descriptor in sortDescriptors)
                    {
                        string[] parts = descriptor.Split(DirectionDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 2)
                        {
                            SortDescriptor column = DeserializeColumn(parts);
                            orderBy.Add(column);
                        }
                    }
                }
            }

            return orderBy;
        }

        private static string SerializeDescriptor(SortDescriptor column)
        {
            return "{0}{1}{2}".FormatWith(
                            column.Member,
                            DirectionDelimiter,
                            column.SortDirection == ListSortDirection.Ascending ? Ascending : Descending);
        }

        private static SortDescriptor DeserializeColumn(string[] from)
        {
            SortDescriptor descriptor = new SortDescriptor
                                            {
                                                Member = from[0],
                                                SortDirection = from[1].IsCaseInsensitiveEqual(Descending) ? ListSortDirection.Descending : ListSortDirection.Ascending
                                            };

            return descriptor;
        }
    }
}