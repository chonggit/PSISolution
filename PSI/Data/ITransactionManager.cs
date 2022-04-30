namespace PSI.Data
{
    /// <summary>
    /// 管理事务
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// 请求开启事务处理
        /// </summary>
        void Demand();

        /// <summary>
        /// 取消事务，回滚操作
        /// </summary>
        void Cancel();
    }
}