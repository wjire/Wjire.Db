namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 单连接读取数据
    /// </summary>
    public class SingleConnection : IUnitOfWork
    {

        public AbstractConnectionWrapper ConnectionWrapper { get; }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        public SingleConnection(string name)
        {
            ConnectionWrapper = ConnectionWrapperFactory.GetConnectionWrapper(name);
        }



        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            ConnectionWrapper.Connection?.Dispose();
        }
    }
}