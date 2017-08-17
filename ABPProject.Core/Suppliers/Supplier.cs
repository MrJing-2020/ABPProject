using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Suppliers
{
    [Table("Supplier")]
    public class Supplier: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }


        public virtual string Name { get; set; }
        public virtual string NameAlias { get; set; }
        public virtual string Company { get; set; }
        public virtual string Department { get; set; }
    }
}
