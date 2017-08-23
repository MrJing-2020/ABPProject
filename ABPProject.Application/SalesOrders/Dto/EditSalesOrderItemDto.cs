using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(SalesOrderItem))]
    public class EditSalesOrderItemDto
    {
        public int? Id { get; set; }
        public int SalesOrderId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品批次
        /// </summary>
        [Required]
        public int InventBatchId { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        [Required]
        public int PurchCount { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        [Required]
        public int PurchPrice { get; set; }
    }
}
