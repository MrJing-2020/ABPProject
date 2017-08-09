using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders
{
    public class SalesOrderItem: Entity, ISoftDelete
    {
        public virtual int SalesOrderId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string Inventid { get; set; }
        /// <summary>
        /// 产品批次
        /// </summary>
        public virtual string InventBatch { get; set; }
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
