using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders.Dto
{
    public class SalesOrderItemDetailDto
    {
        /// <summary>
        /// 产品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品批次编号
        /// </summary>
        public string InventBatchNum { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int PurchCount { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public int PurchPrice { get; set; }
    }
}
