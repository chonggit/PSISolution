using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Inventory
{
    /// <summary>
    /// 物料主数据
    /// </summary>
    public class Items
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public virtual string ItemCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string ItemName { get; set; }
    }
}
