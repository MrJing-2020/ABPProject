using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Contracts
{
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


        public virtual string Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string ContractNum { get; set; }

    }
}
