using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders
{
    [Table("PurchaseOrderItem")]
    public class PurchaseOrderItem: Entity, ISoftDelete
    {
        public virtual int PurchaseOrderId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual int ProductId { get; set; }
        /// <summary>
        /// 产品批次Id
        /// </summary>
        public virtual int InventBatchId { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public virtual int PurchCount { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public virtual int PurchPrice { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
