using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Delivers.Dto
{
    [AutoMap(typeof(Deliver))]
    public class EditDeliverInput
    {
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }

        public int? Id { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliverTime { get; set; }
        /// <summary>
        /// 销售订单Id
        /// </summary
        [Required]
        public int SalesOrderId { get; set; }
        /// <summary>
        /// 物流公司名称
        /// </summary>
        [Required]
        public string LogisticsCompany { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        [Required]
        public string LogisticsNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
