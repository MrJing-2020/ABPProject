using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Products
{
    [Table("Product")]
    public class Product: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual float Price { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public Product()
        {
        }
        public Product(int tenantId, long organizationUnitId, string name, float price)
        {
            TenantId = tenantId;
            OrganizationUnitId = organizationUnitId;
            Name = name;
            Price = price;
        }
    }
}
