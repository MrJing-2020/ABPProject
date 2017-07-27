using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Prodect
{
    [Table("Product")]
    public class Product: Entity, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        public virtual int TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }
        public virtual string Name { get; set; }
        public virtual float Price { get; set; }
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
