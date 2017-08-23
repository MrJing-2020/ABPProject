using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Delivers.Dto
{
    [AutoMap(typeof(Deliver))]
    public class DeliverListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime DeliverTime { get; set; }
        /// <summary>
        /// 销售订单Id
        /// </summary>
        public string SalesOrderNum { get; set; }
        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string LogisticsCompany { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string LogisticsNum { get; set; }
    }
}
