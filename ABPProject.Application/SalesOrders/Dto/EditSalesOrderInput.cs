using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(SalesOrder))]
    public class EditSalesOrderInput
    {
        public int? Id { get; set; }
        public  string SalesNum { get; set; }
        //public  string SalesName { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public  int ClientId { get; set; }
        /// <summary>
        /// 产品站点
        /// </summary>
        public  int InventSiteId { get; set; }
        /// <summary>
        /// 站点仓库
        /// </summary>
        public  int InventLocationId { get; set; }
        /// <summary>
        /// 销售合同Id
        /// </summary>
        public  int SalesContractId { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public  DateTime DeliveryDate { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public  string Consignee { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public  string DeliveryAddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public  string PostCode { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public  string DistributionMode { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public  string MobilePhone { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public  string InvoiceHeader { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public  string Instructions { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public  string PaymentMethod { get; set; }

        #region 方便修改操作提交数据(不用显示出来)
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public List<EditSalesOrderItemDto> SalesOrderItems { get; set; }

    }
}
