namespace AltNorthwind
{
    using System.Collections.Generic;

    using NHibernate;
    using NHibernate.Linq;

    public interface IRepository<TEntity, TId>
    {
        void Add(TEntity entity);

        void Delete(TId id);

        TEntity Get(TId id);

        IEnumerable<TEntity> All();
    }

    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    {
        private readonly ISession session;

        public Repository() : this(NHibernateSessionPerRequest.CurrentSession)
        {
        }

        public Repository(ISession session)
        {
            this.session = session;
        }

        public void Add(TEntity entity)
        {
            session.SaveOrUpdate(entity);
        }

        public void Delete(TId id)
        {
            session.Delete(Get(id));
        }

        public TEntity Get(TId id)
        {
            return session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> All()
        {
            return session.Linq<TEntity>();
        }
    }
}