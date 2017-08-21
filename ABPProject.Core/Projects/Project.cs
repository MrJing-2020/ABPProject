using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Projects
{
    [Table("Project")]
    public class Project : Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete,IModificationAudited
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int AXProjectId { get; set; }
        public virtual DateTime BeginDate { get; set; }
        /// <summary>
        /// 风险
        /// </summary>
        public virtual string Risk { get; set; }
        /// <summary>
        /// 买入方式
        /// </summary>
        public virtual string BuyMethod { get; set; }
        /// <summary>
        /// 赎回方式
        /// </summary>
        public virtual string RedeemMethod { get; set; }
        /// <summary>
        /// 收益率
        /// </summary>
        public virtual decimal Yield { get; set; }
    }
}
