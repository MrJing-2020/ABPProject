using Abp.AutoMapper;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(SalesOrderItem))]
    public class SalesOrderItemListDto
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string InventId { get; set; }
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
    }
}
