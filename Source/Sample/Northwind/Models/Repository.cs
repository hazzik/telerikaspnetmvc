namespace Northwind
{
    using System;
    using System.Collections.Generic;
    using System.Data.Metadata.Edm;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<TEntity, TId>
    {
        void Add(TEntity entity);

        void Delete(TId id);

        TEntity Get(TId id);

        IEnumerable<TEntity> All();
    }

    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    {
        private readonly Database database;

        public Repository() : this(EntityFrameworkObjectContextPerRequest.CurrentDatabase)
        {
        }

        public Repository(Database database)
        {
            this.database = database;
        }

        public void Add(TEntity entity)
        {
            database.AddObject(TypeName(), entity);
        }

        public void Delete(TId id)
        {
            database.DeleteObject(Get(id));
        }

        public TEntity Get(TId id)
        {
            var typeName = TypeName();

            var keyName = database.MetadataWorkspace
                                  .GetItems<EntityType>(DataSpace.CSpace)
                                  .Single(meta => meta.Name == typeName)
                                  .KeyMembers[0].Name;

            var param = Expression.Parameter(typeof(TEntity), "x");
            var left = Expression.Property(param, keyName);
            var right = Expression.Constant(id);
            var equal = Expression.Equal(left, right);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(equal, param).Compile();

            return All().SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> All()
        {
            return database.CreateQuery<TEntity>("[" + TypeName() + "]");
        }

        private static string TypeName()
        {
            return typeof(TEntity).Name;
        }
    }
}