﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace PSI.Data
{
    /// <summary>
    /// NHibernate Db Session
    /// </summary>
    internal class NHDbSession : IDbSession, IDisposable
    {
        private readonly ISession _session;

        public NHDbSession(ISession session)
        {
            _session = session;
        }

        protected virtual ISession Session => _session;

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    _session.Dispose();
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

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            Session.Save(entity);

            return entity;
        }

        public TEntity Find<TEntity>(object key) where TEntity : class
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

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Session.SaveAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Session.DeleteAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Session.UpdateAsync(entity, cancellationToken);

            return entity;
        }

        public Task<TEntity> FindAsync<TEntity>(object key, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Session.GetAsync<TEntity>(key, cancellationToken);
        }

        public void SaveChanges()
        {
            _session.Flush();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _session.FlushAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _session.Query<TEntity>();
        }

        public TEntity Attach<TEntity>(TEntity entity) where TEntity : class
        {
            return Session.Merge(entity);
        }

        public Task<TEntity> AttachAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Session.MergeAsync(entity, cancellationToken);
        }
    }
}
