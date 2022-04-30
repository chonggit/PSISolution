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
        TEntity Add<TEntity>(TEntity entity) where TEntity:class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity">删除的实体类型</typeparam>
        /// <param name="entity">删除的实体</param>
        /// <returns>被删除的实体</returns>
        TEntity Remove<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity">更新的实体类型</typeparam>
        /// <param name="entity">更新的实体</param>
        /// <returns>更新后的实体</returns>
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="TEntity">查找的实体类型</typeparam>
        /// <param name="key">实体主键</param>
        /// <returns>查找到的实体</returns>
        TEntity? Find<TEntity>(object key) where TEntity : class;
    }
}