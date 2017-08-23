using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Delivers
{
    [Table("Deliver")]
    public class Deliver: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete, IModificationAudited
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime DeliverTime { get; set; }
        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual int SalesOrderId { get; set; }
        /// <summary>
        /// 物流公司名称
        /// </summary>
        public virtual string LogisticsCompany { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public virtual string LogisticsNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
