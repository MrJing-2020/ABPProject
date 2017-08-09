using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Projects
{
    [Table("Project")]
    public class Project : Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public Project() { }

        public Project(int tenantId, long organizationUnitId,string name,string description)
        {
            this.TenantId = tenantId;
            this.OrganizationUnitId = organizationUnitId;
            this.Name = name;
            this.Description = description;
        }

    }
}
