using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Protocols
{
    /// <summary>
    /// 服务协议
    /// </summary>
    [Table("Protocol")]
    public class Protocol: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 甲方
        /// </summary>
        public virtual string FirstParty { get; set; }
        /// <summary>
        /// 乙方
        /// </summary>
        public virtual string SecondParty { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public virtual string ServiceContent { get; set; }
        /// <summary>
        /// 费用内容
        /// </summary>
        public virtual string CostContent { get; set; }
        /// <summary>
        /// 甲方权利和义务
        /// </summary>
        public virtual string FirstRightObligation { get; set; }
        /// <summary>
        /// 乙方权利和义务
        /// </summary>
        public virtual string SecondRightObligation { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public virtual string PeriodOfValidity { get; set; }
        /// <summary>
        /// 争议解决方式
        /// </summary>
        public virtual string DisputeSolve { get; set; }
    }
}
