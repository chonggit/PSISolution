using PSI.Data;
using NHibernate;

namespace PSI.NHibernate
{
    public class DbSession : IDbSession, IDisposable
    {
        private readonly ISession _session;

        public DbSession(ISession session)
        {
            _session = session;
        }

        protected virtual ISession Session => _session;

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return (TEntity)Session.Save(entity);
        }

        public TEntity? Find<TEntity>(object key) where TEntity : class
        {
            return Session.Get<TEntity>(key);
        }

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Delete(entity);

            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Update(entity);

            return entity;
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~DbSession()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}