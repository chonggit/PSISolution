using NHibernate.Mapping.ByCode.Conformist;
using PSI.Inventory;

namespace PSI.NHibernate.Mappings
{
    /// <summary>
    /// 物料主数据映射配置
    /// </summary>
    internal class ItemsMappings : ClassMapping<Items>
    {
        public ItemsMappings()
        {
            Table("Items");
            Id(item => item.ItemCode, id =>
            {
                id.Length(20);
            });
            Property(item => item.ItemName, p =>
            {
                p.Length(100);
                p.NotNullable(true);
            });
        }
    }
}
