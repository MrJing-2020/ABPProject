using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.PurchaseOrders.Dto
{
    [AutoMap(typeof(PurchaseOrderItem))]
    public class EditPurchaseOrderItemDto
    {
        public int? Id { get; set; }
        public virtual int PurchaseOrderId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Required]
        public virtual string ProductId { get; set; }
        /// <summary>
        /// 产品批次
        /// </summary>
        [Required]
        public virtual string InventBatchId { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        [Required]
        public virtual int PurchCount { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        [Required]
        public virtual int PurchPrice { get; set; }
    }
}
