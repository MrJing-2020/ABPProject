using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders.Dto
{
    [AutoMap(typeof(PurchaseOrderItem))]
    public class PurchaseOrderItemListDto
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 产品批次
        /// </summary>
        public string InventBatchId { get; set; }
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
