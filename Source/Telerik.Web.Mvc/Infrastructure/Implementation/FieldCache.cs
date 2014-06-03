// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class FieldCache : IFieldCache
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField;

        private readonly ICache cache;
        
        public FieldCache(ICache cache)
        {
            this.cache = cache;
        }

        public IEnumerable<FieldInfo> GetFields(Type type)
        {
            return cache.Get(type.AssemblyQualifiedName, () => type.GetFields(Flags).Where(field => !field.IsInitOnly));
        }
    }
}