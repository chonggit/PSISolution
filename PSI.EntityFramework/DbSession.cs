﻿using Microsoft.EntityFrameworkCore;
using PSI.Data;

namespace PSI.EntityFramework
{
    public class DbSession : IDbSession, IDisposable
    {
        private readonly DbContext _context;

        public DbSession(DbContext dbContext)
        {
            _context = dbContext;
        }

        protected virtual DbContext Context => _context;

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

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return Context.Add(entity).Entity;
        }

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            return Context.Remove(entity).Entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            return Context.Update(entity).Entity;
        }

        public TEntity Find<TEntity>(object key) where TEntity : class
        {
            return Context.Find<TEntity>(key);
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            return (await Context.AddAsync(entity, cancellationToken)).Entity;
        }

        public Task<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync<TEntity>(object key, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public TEntity Attach<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> AttachAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}