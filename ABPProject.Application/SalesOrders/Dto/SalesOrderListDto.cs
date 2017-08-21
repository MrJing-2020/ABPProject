using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABPProject.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMapFrom(typeof(SalesOrder))]
    public class SalesOrderListDto: EntityDto<int>
    {
        /// <summary>
        /// 销售订单编号
        /// </summary>
        public string SalesNum { get; set; }
        public string ClientName { get; set; }
        public string ContractNum { get; set; }
        public DateTime CreationTime { get; set; }
        public int State { get; set; }
        public List<SalesOrderItemListDto> SalesOrderItems { get; set; }
    }
}
