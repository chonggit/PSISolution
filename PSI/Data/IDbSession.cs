namespace PSI.Data
{
    /// <summary>
    /// db session
    /// </summary>
    public interface IDbSession
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">添加的实体类型</typeparam>
        /// <param name="entity">添加的实体</param>
        /// <returns>已添加的实体</returns>
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity">添加的实体类型</typeparam>
        /// <param name="entity">添加的实体</param>
        /// <returns>已添加的实体</returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">删除的实体类型</typeparam>
        /// <param name="entity">删除的实体</param>
        /// <returns>被删除的实体</returns>
        TEntity Remove<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">删除的实体类型</typeparam>
        /// <param name="entity">删除的实体</param>
        /// <returns>被删除的实体</returns>
        Task<TEntity> RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">更新的实体类型</typeparam>
        /// <param name="entity">更新的实体</param>
        /// <returns>更新后的实体</returns>
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">更新的实体类型</typeparam>
        /// <param name="entity">更新的实体</param>
        /// <returns>更新后的实体</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="TEntity">查找的实体类型</typeparam>
        /// <param name="key">实体主键</param>
        /// <returns>查找到的实体</returns>
        TEntity Find<TEntity>(object key) where TEntity : class;

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="TEntity">查找的实体类型</typeparam>
        /// <param name="key">实体主键</param>
        /// <returns>查找到的实体</returns>
        Task<TEntity> FindAsync<TEntity>(object key, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// 保存所有更改到数据库
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 保存所有更改到数据库
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Queryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <summary>
        /// 附加 entity
        /// </summary>
        /// <typeparam name="TEntity">entity 类型</typeparam>
        /// <param name="entity">entity</param>
        /// <returns>entity</returns>
        TEntity Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 附加 entity
        /// </summary>
        /// <typeparam name="TEntity">entity 类型</typeparam>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>entity</returns>
        Task<TEntity> AttachAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    }
}