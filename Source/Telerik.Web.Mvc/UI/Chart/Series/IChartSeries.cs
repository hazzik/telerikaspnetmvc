﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents chart series
    /// </summary>
    public interface IChartSeries
    {
        /// <summary>
        /// The series name.
        /// </summary>
        string Name 
        { 
            get;
            set; 
        }

        /// <summary>
        /// The series opacity
        /// </summary>
        double Opacity
        {
            get;
            set;
        }

        /// <summary>
        /// The series base color
        /// </summary>
        string Color
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer for the series
        /// </summary>
        IChartSerializer CreateSerializer();        
    }
}