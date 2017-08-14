using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.PurchaseOrders
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete, IModificationAudited
    {
        public virtual string PurchId { get; set; }
        public virtual string PurchName { get; set; }
        public virtual string InventSite { get; set; }
        public virtual string InventLocation { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual string PurchSecontractNum { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItem { get; set; }
    }
}
