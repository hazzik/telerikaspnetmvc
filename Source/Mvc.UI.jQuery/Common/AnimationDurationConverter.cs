// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Collections.Generic;

    using Telerik.Web.Mvc.Infrastructure;

    public static class AnimationDurationConverter
    {
        private static readonly IDictionary<int, string> durationMap = BuildMap();

        public static string ToString(int duration)
        {
            return durationMap.ContainsKey(duration) ? durationMap[duration] : duration.ToString(Culture.Invariant);
        }

        private static IDictionary<int, string> BuildMap()
        {
            Dictionary<int, string> map = new Dictionary<int, string>
                                              {
                                                  { (int) AnimationDuration.Fast, "'fast'" },
                                                  { (int) AnimationDuration.Normal, "'normal'" },
                                                  { (int) AnimationDuration.Slow, "'slow'" }
                                              };

            return map;
        }
    }
}