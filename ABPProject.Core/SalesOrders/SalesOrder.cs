using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders
{
    [Table("SalesOrder")]
    public class SalesOrder : Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete, IModificationAudited
    {
        /// <summary>
        /// 销售订单编号
        /// </summary>
        public virtual string SalesId { get; set; }
        public virtual string SalesName { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string ClientId { get; set; }
        /// <summary>
        /// 产品站点
        /// </summary>
        public virtual string InventSite { get; set; }
        /// <summary>
        /// 站点仓库
        /// </summary>
        public virtual string InventLocation { get; set; }
        /// <summary>
        /// 销售合同编号
        /// </summary>
        public virtual string SalesContractNum { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public virtual DateTime DeliveryDate { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public virtual string Consignee { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public virtual string DeliveryAddress { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public virtual string PostCode { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public virtual string DistributionMode { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public virtual string MobilePhone { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public virtual string InvoiceHeader { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public virtual string Instructions { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual string PaymentMethod { get; set; }

        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        public virtual ICollection<SalesOrderItem> SalesOrderItem { get; set; }
    }
}
