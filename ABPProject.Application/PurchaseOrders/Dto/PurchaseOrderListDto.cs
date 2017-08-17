using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace ABPProject.PurchaseOrders.Dto
{
    [AutoMapFrom(typeof(PurchaseOrder))]
    public class PurchaseOrderListDto: EntityDto<int>
    {
        /// <summary>
        /// 销售订单编号
        /// </summary>
        public string PurchNum { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNum { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        public List<PurchaseOrderItemListDto> PurchaseOrderItems { get; set; }

        /// <summary>
        /// 默认排序字段
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
