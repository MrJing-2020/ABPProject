using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.InventSites
{
    [Table("InventSite")]
    public class InventSite: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, ISoftDelete, IHasCreationTime, ICreationAudited
    {
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }


        public virtual string Name { get; set; }
        public virtual ICollection<InventLocation> InventLocation { get; set; }
    }
}
