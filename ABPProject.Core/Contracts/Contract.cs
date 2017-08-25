using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Contracts
{
    /// <summary>
    /// 合同
    /// </summary>
    [Table("Contract")]
    public class Contract: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, ISoftDelete, IHasCreationTime, ICreationAudited, IModificationAudited
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual int SalesOrderId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string ContractNum { get; set; }
        /// <summary>
        /// 买方
        /// </summary>
        public virtual string Purchaser { get; set; }
        /// <summary>
        /// 卖方
        /// </summary>
        public virtual string Seller { get; set; }
        /// <summary>
        /// 商品包装和质量标准
        /// </summary>
        public virtual string GoodsStandard { get; set; }
        /// <summary>
        /// 交货地点
        /// </summary>
        public virtual string DeliverAddress { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime DeliverTime { get; set; }
        /// <summary>
        /// 争议解决方式
        /// </summary>
        public virtual string DisputeSolve { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public virtual string PeriodOfValidity { get; set; }
        /// <summary>
        /// 附加内容
        /// </summary>
        public virtual string AdditionalContent { get; set; }
    }
}
