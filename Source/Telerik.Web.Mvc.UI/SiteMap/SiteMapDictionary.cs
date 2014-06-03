namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class SiteMapDictionary : IDictionary<string, SiteMapBase>
    {
        private readonly IDictionary<string, SiteMapBase> innerDictionary = new Dictionary<string, SiteMapBase>(StringComparer.OrdinalIgnoreCase);

        private static Func<SiteMapBase> defaultSiteMapFactory = CreateDefaultSiteMapFactory();

        private SiteMapBase defaultSiteMap;

        public static Func<SiteMapBase> DefaultSiteMapFactory
        {
            [DebuggerStepThrough]
            get
            {
                return defaultSiteMapFactory;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNull(value, "value");

                defaultSiteMapFactory = value;
            }
        }

        public SiteMapBase DefaultSiteMap
        {
            [DebuggerStepThrough]
            get
            {
                if (defaultSiteMap == null)
                {
                    defaultSiteMap = DefaultSiteMapFactory();
                }

                return defaultSiteMap;
            }

            [DebuggerStepThrough]
            set
            {
                defaultSiteMap = value;
            }
        }

        public int Count
        {
            [DebuggerStepThrough]
            get
            {
                return innerDictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            [DebuggerStepThrough]
            get
            {
                return innerDictionary.IsReadOnly;
            }
        }

        public ICollection<string> Keys
        {
            [DebuggerStepThrough]
            get
            {
                return innerDictionary.Keys;
            }
        }

        public ICollection<SiteMapBase> Values
        {
            [DebuggerStepThrough]
            get
            {
                return innerDictionary.Values;
            }
        }

        public SiteMapBase this[string key]
        {
            [DebuggerStepThrough]
            get
            {
                return innerDictionary[key];
            }

            [DebuggerStepThrough]
            set
            {
                innerDictionary[key] = value;
            }
        }

        public SiteMapDictionary Register<TSiteMap>(string name, Action<TSiteMap> configure) where TSiteMap : SiteMapBase, new()
        {
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotNull(configure, "configure");

            TSiteMap siteMap = new TSiteMap();
            configure(siteMap);

            Add(name, siteMap);

            return this;
        }

        [DebuggerStepThrough]
        public void Add(KeyValuePair<string, SiteMapBase> item)
        {
            innerDictionary.Add(item);
        }

        [DebuggerStepThrough]
        public void Add(string key, SiteMapBase value)
        {
            innerDictionary.Add(key, value);
        }

        [DebuggerStepThrough]
        public void Clear()
        {
            innerDictionary.Clear();
        }

        [DebuggerStepThrough]
        public bool Contains(KeyValuePair<string, SiteMapBase> item)
        {
            return innerDictionary.Contains(item);
        }

        [DebuggerStepThrough]
        public bool ContainsKey(string key)
        {
            return innerDictionary.ContainsKey(key);
        }

        [DebuggerStepThrough]
        public void CopyTo(KeyValuePair<string, SiteMapBase>[] array, int arrayIndex)
        {
            innerDictionary.CopyTo(array, arrayIndex);
        }

        [DebuggerStepThrough]
        public IEnumerator<KeyValuePair<string, SiteMapBase>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        [DebuggerStepThrough]
        public bool Remove(KeyValuePair<string, SiteMapBase> item)
        {
            return innerDictionary.Remove(item);
        }

        [DebuggerStepThrough]
        public bool Remove(string key)
        {
            return innerDictionary.Remove(key);
        }

        [DebuggerStepThrough]
        public bool TryGetValue(string key, out SiteMapBase value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        [DebuggerStepThrough]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static Func<SiteMapBase> CreateDefaultSiteMapFactory()
        {
            Func<SiteMapBase> factory = () =>
                                               {
                                                   XmlSiteMap siteMap = new XmlSiteMap(new PathResolver(), new FileSystemWrapper(), new CacheManagerWrapper());

                                                   siteMap.Load();

                                                   return siteMap;
                                               };

            return factory;
        }
    }
}