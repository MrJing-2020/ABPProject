using System;
using System.Collections.Generic;

namespace ABPProject.SalesOrders.Dto
{
    public class SalesOrderDetailDto
    {
        public int? Id { get; set; }
        public string SalesNum { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 产品站点
        /// </summary>
        public string InventSiteName { get; set; }
        /// <summary>
        /// 站点仓库
        /// </summary>
        public string InventLocationName { get; set; }
        /// <summary>
        /// 销售合同Id
        /// </summary>
        public string SalesContractNum { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string DeliveryAddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string DistributionMode { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceHeader { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Instructions { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentMethod { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public int State { get; set; }
        public decimal TotalPrices { get; set; }
        public List<SalesOrderItemDetailDto> SalesOrderItems { get; set; }
    }
}
