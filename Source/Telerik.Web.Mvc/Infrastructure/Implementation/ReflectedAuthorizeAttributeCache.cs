// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;

    internal class ReflectedAuthorizeAttributeCache : IReflectedAuthorizeAttributeCache
    {
        private readonly IAuthorizeAttributeBuilder builder;
        private readonly ICache cache;

        public ReflectedAuthorizeAttributeCache(ICache cache, IAuthorizeAttributeBuilder builder)
        {
            this.cache = cache;
            this.builder = builder;
        }

        public IAuthorizeAttribute GetAttribute(Type attributeType)
        {
            var ctor = cache.Get(attributeType.AssemblyQualifiedName, () => builder.Build(attributeType));
            
            return ctor.Invoke(new object[0]) as IAuthorizeAttribute;
        }
    }
}